# magishira.dev

My personal site: projects, photos, a homelab write-up, experience, and a blog. There's also a CSS-3D CRT you can smack.

It's built to load like a static page, because it mostly is one.

- **Static server rendering.** Blazor renders plain HTML on the server. No WebAssembly download and no SignalR circuit.
- **One stylesheet, one small script.** The site works with JavaScript off. The script adds the theme toggle, the CRT's tilt and smack, the photo strip, the lightbox, and the search and filter.
- **No web fonts, no icon font, no analytics.** Icons are inline SVG paths, from Material Icons and Simple Icons.
- **Fingerprinted, pre-compressed assets** with a one-year cache. HTML is `no-cache`, so a reload always gets the current page.
- **Motion respects `prefers-reduced-motion`.** That covers the CRT, the scroll effects, and the theme crossfade.

## Run it

You need the [.NET 10 SDK](https://dotnet.microsoft.com/download).

```
dotnet run
```

Open the URL it prints. Development mode also shows draft blog posts.

To build for production:

```
dotnet publish -c Release -o out
ASPNETCORE_ENVIRONMENT=Production dotnet out/Magishira.dll
```

## Deploy

The site runs on a NixOS VM. A Cloudflare tunnel carries magishira.dev, elane.dev, elane.gay and their `www.` names to it, so nothing on the box is open to the internet. The app 301s the elane domains and every `www.` name to https://magishira.dev. The VM's config lives in `deploy/`.

`deploy/deploy.sh` and the commands below SSH to `$DEPLOY_HOST`. Set it in `deploy/.env`, which git ignores, or pass the host as the script's first argument.

To ship the committed site:

```
deploy/deploy.sh
```

It publishes `HEAD` in a temp directory, copies it to `/var/lib/magishira/app`, and restarts the service.

To change the VM itself, edit `deploy/configuration.nix`, then:

```
. deploy/.env
tar --no-xattrs -C deploy -c flake.nix flake.lock configuration.nix | ssh "$DEPLOY_HOST" 'mkdir -p /root/magishira && tar -x -C /root/magishira && nixos-rebuild switch --flake /root/magishira#magishira'
```

## Where things live

| Path | What it is |
|---|---|
| `Components/Pages/` | One file per route: home, projects, photos, blog, experience, about, 404 |
| `Components/Layout/MainLayout.razor` | Header, nav, footer |
| `Components/ProjectList.cs` | Projects. `Featured` puts one on the home page. |
| `Components/PhotoList.cs` | Photos, with alt text and tags |
| `Components/PostList.cs` | Blog post metadata |
| `Components/Posts/` | Blog post bodies, one Razor page each |
| `Components/Crt.razor` | The monitor |
| `wwwroot/app.css` | All the styles |
| `wwwroot/site.js` | All the script |
| `Program.cs` | Pipeline, domain redirects, RSS feed, `security.txt` |
| `deploy/` | NixOS config for the VM, and the deploy script |

## Adding content

### A project

Add a row to `Components/ProjectList.cs`:

```csharp
new("Name", "One or two sentences about what it is.", "https://github.com/...", "https://live.example", Featured: false, "Tag", "Tag"),
```

Use `null` for a missing source or live link.

### A photo

Export at 1600px on the long edge as WebP, with metadata stripped so no GPS ships:

```
cwebp -q 75 -m 6 -metadata none -resize 1600 0 in.jpg -o wwwroot/photos/name.webp
```

Then add a row to `Components/PhotoList.cs` with the file, width, height, title, caption, alt text, and tags. New tags become filter chips automatically. Photos tagged `Graduation` stay off the home page strip.

### A blog post

1. Add a row to `Components/PostList.cs` with a slug, title, date, summary, and tags.
2. Copy `Components/Posts/HelloWorld.razor`, then change the `@page` route and the `Slug`.
3. Write the body as HTML inside `<Post>`.

Posts stay drafts until you set `Draft` to `false`. Drafts render only in Development: in Production they 404 and stay out of the index, the home page, and the feed at `/blog/feed.xml`.
