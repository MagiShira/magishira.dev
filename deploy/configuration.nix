{ pkgs, modulesPath, ... }:

let
  # From `cloudflared tunnel create magishira`. The ID isn't secret, the credentials file is: it lives in /var/lib/secrets on the box.
  tunnelId = "1af3acfb-754c-41d2-9b74-b1236b93ce61";
in
{
  imports = [ (modulesPath + "/profiles/qemu-guest.nix") ];

  boot.initrd.availableKernelModules = [ "ahci" "virtio_pci" "virtio_scsi" "sd_mod" "sr_mod" ];
  boot.loader.systemd-boot.enable = true;
  boot.loader.efi.canTouchEfiVariables = true;

  # Partitioned by hand at install. These labels have to match.
  fileSystems."/" = { device = "/dev/disk/by-partlabel/root"; fsType = "ext4"; };
  fileSystems."/boot" = { device = "/dev/disk/by-partlabel/ESP"; fsType = "vfat"; options = [ "umask=0077" ]; };
  swapDevices = [ { device = "/dev/disk/by-partlabel/swap"; } ];

  networking.hostName = "magishira";
  networking.useDHCP = true; # only SSH listens on the LAN, the site goes out through the tunnel

  time.timeZone = "America/Los_Angeles";

  services.qemuGuest.enable = true;

  services.openssh = {
    enable = true;
    settings = {
      PasswordAuthentication = false;
      PermitRootLogin = "prohibit-password";
    };
  };

  users.users.root.openssh.authorizedKeys.keys = [
    "ssh-ed25519 AAAAC3NzaC1lZDI1NTE5AAAAIBawuuJ6MBf4DiQ2RbjAyZgbHNJxmG+3QYNchWYXfH91"
  ];

  # The published app is copied into /var/lib/magishira/app by deploy.sh, it isn't built by Nix.
  systemd.services.magishira = {
    wantedBy = [ "multi-user.target" ];
    after = [ "network.target" ];
    environment = {
      ASPNETCORE_ENVIRONMENT = "Production";
      ASPNETCORE_URLS = "http://127.0.0.1:5000";
      ASPNETCORE_FORWARDEDHEADERS_ENABLED = "true"; # trust cloudflared's X-Forwarded-Proto so the feed and redirects say https
      HOME = "/var/lib/magishira"; # data protection keys persist here
    };
    serviceConfig = {
      ExecStart = "${pkgs.dotnetCorePackages.aspnetcore_10_0}/bin/dotnet Magishira.dll";
      WorkingDirectory = "/var/lib/magishira/app";
      DynamicUser = true;
      StateDirectory = "magishira";
      Restart = "always";
    };
  };

  services.cloudflared = {
    enable = true;
    tunnels.${tunnelId} = {
      credentialsFile = "/var/lib/secrets/cloudflared-${tunnelId}.json";
      ingress = {
        "magishira.dev" = "http://127.0.0.1:5000";
        "www.magishira.dev" = "http://127.0.0.1:5000"; # the app 301s every name but magishira.dev to it
        "elane.dev" = "http://127.0.0.1:5000";
        "www.elane.dev" = "http://127.0.0.1:5000";
        "elane.gay" = "http://127.0.0.1:5000";
        "www.elane.gay" = "http://127.0.0.1:5000";
      };
      default = "http_status:404";
    };
  };

  nix.settings.experimental-features = [ "nix-command" "flakes" ];
  nix.gc = {
    automatic = true;
    dates = "weekly";
    options = "--delete-older-than 30d";
  };

  system.stateVersion = "26.05";
}
