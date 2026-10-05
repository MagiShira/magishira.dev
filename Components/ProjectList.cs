namespace Magishira.Components;

// one list feeds the home selection (Featured) and the /projects page
public static class ProjectList
{
    public record Project(string Name, string Blurb, string? Source, string? Live, bool Featured, params string[] Tags);

    public static readonly Project[] Items =
    [
        new("OpenUI for GlobalProtect-OpenConnect", "A graphical front-end for the GlobalProtect VPN client on Linux, with password and SSO sign-in.", "https://github.com/MagiShira/globalprotect-openconnect-openui", null, true, "Rust", "Linux"),
        new("2hu.gay", "The five PC-98 Touhou games (1996 – 1998), playable in the browser in Japanese or English. A custom emulator core rebuilt for speed, so bullet-hell peaks stay smooth.", null, "https://2hu.gay", true, "Emulation", "WebAssembly", "ASP.NET Core"),
        new("nevadaesports.org", "Nevada eSports' public site: teams, the Welcome Week LAN, game nights, and the esports academic program. Built for a 2,000-member community.", null, "https://www.nevadaesports.org", true, "Blazor", ".NET"),
        new("portal.nve.gg", "The Nevada eSports Event Portal: servers, staff, and events in one place, with Discord and Microsoft sign-in.", null, "https://portal.nve.gg", false, ".NET", "SSO"),
        new("NVE Broadcast Control", "Live broadcast graphics for college esports, built for Nevada eSports (NVE): OBS overlays driven from a native control panel, with real-time scoreboards and a Stream Deck plugin.", null, null, false, "Rust", "Tauri", "OBS", "Internal"),
        new("Operating System Deployment UI", "A preflight tool for imaging university computers. It checks the machine and collects what technicians used to type by hand, so setup errors stop before they start. Handles AD-bound provisioning and devices that can't run Intune. The deployment standard since March 2026, at about 12 deployments a week.", null, null, true, "PowerShell", "SCCM", "Internal"),
        new("BACON", "Badge Administration & Convention Operations Nexus: single sign-on and operations for SNAFU Con. An OAuth 2.0 / OpenID Connect identity provider with PKCE, refresh tokens, a consent flow, and introspection and revocation, backed by a full test suite and an admin UI. Plus merchant, point-of-sale, and volunteer workflows with manager approval, on Symfony 7.4 and PHP 8.", null, "https://register.snafucon.com", true, "PHP", "Symfony"),
        new("UNR 3D Printing Club Wiki", "The club's reference for printers, filaments, slicing, and safety. MediaWiki, with a forked extension for approving member accounts.", null, "https://wiki.unr3dp.org", false, "MediaWiki"),
        new("ConMap", "Offline-first map for SNAFU Con 2026: rooms, schedules, and the Merchants' Room booth by booth. Works with no signal.", null, "https://map.elane.dev", true, "C#", "Blazor", "PWA"),
        new("PackWatch", "Security and moderation tooling for the Nevada eSports Discord community.", null, null, false, "SQL", "RegEx", "Internal"),
        new("magishira.dev", "This site. Static-rendered Blazor, one stylesheet, no client runtime. The CRT is CSS. Smack it.", "https://github.com/MagiShira/magishira.dev", null, false, "C#", "Blazor", "CSS"),
    ];
}
