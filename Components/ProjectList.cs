namespace Magishira.Components;

// the projects shown on the home page
public static class ProjectList
{
    public record Project(string Name, string Blurb, string? Source, string? Live, params string[] Tags);

    public static readonly Project[] Items =
    [
        new("OpenUI for GlobalProtect-OpenConnect", "A graphical front-end for the GlobalProtect VPN client on Linux, with password and SSO sign-in.", "https://github.com/MagiShira/globalprotect-openconnect-openui", null, "Rust", "Linux"),
        new("2hu.gay", "The five PC-98 Touhou games (1996 – 1998), playable in the browser in Japanese or English. A custom emulator core rebuilt for speed, so bullet-hell peaks stay smooth.", null, "https://2hu.gay", "Emulation", "WebAssembly", "ASP.NET Core"),
        new("nevadaesports.org", "Nevada eSports' public site: teams, the Welcome Week LAN, game nights, and the esports academic program. Built for a 2,000-member community.", null, "https://www.nevadaesports.org", "Blazor", ".NET"),
        new("Operating System Deployment UI", "A preflight tool for imaging university computers. It checks the machine and collects what technicians used to type by hand, so setup errors stop before they start. Handles AD-bound provisioning and devices that can't run Intune. The deployment standard since March 2026, at about 12 deployments a week.", null, null, "PowerShell", "SCCM", "Internal"),
        new("BACON", "Badge Administration & Convention Operations Nexus: single sign-on and operations for SNAFU Con. An OAuth 2.0 / OpenID Connect identity provider with PKCE, refresh tokens, a consent flow, and introspection and revocation, backed by a full test suite and an admin UI. Plus merchant, point-of-sale, and volunteer workflows with manager approval, on Symfony 7.4 and PHP 8.", null, "https://register.snafucon.com", "PHP", "Symfony"),
        new("ConMap", "Offline-first map for SNAFU Con 2026: rooms, schedules, and the Merchants' Room booth by booth. Works with no signal.", null, "https://map.elane.dev", "C#", "Blazor", "PWA"),
    ];
}
