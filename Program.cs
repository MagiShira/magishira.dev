using System.Xml.Linq;
using Magishira.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();
builder.Services.AddResponseCompression(o => o.EnableForHttps = true); // static assets are pre-compressed, this covers the HTML

var app = builder.Build();
PostList.ShowDrafts = app.Environment.IsDevelopment();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseResponseCompression();
app.Use(async (ctx, next) => // HTML has to revalidate, fingerprinted assets keep their year-long cache
{
    ctx.Response.OnStarting(() => { if (ctx.Response.ContentType?.StartsWith("text/html") == true) ctx.Response.Headers.CacheControl = "no-cache"; return Task.CompletedTask; });
    await next();
});
app.UseAntiforgery();

app.MapStaticAssets();
app.MapGet("/.well-known/security.txt", () => Results.Text("Contact: mailto:shira@magishira.dev\nExpires: 2027-10-04T00:00:00.000Z\nPreferred-Languages: en\nCanonical: https://magishira.dev/.well-known/security.txt\n", "text/plain")); // dotfolders aren't served from wwwroot
app.MapGet("/blog/feed.xml", (HttpContext ctx) => // RSS 2.0, published posts only
{
    var site = $"{ctx.Request.Scheme}://{ctx.Request.Host}";
    XElement El(string n, object v) => new(n, v);
    var rss = new XElement("rss", new XAttribute("version", "2.0"),
        new XElement("channel",
            El("title", "magishira.dev"), El("link", site + "/blog"), El("description", "Writing by Shira."),
            PostList.Published.Select(p => new XElement("item",
                El("title", p.Title), El("link", $"{site}/blog/{p.Slug}"), El("guid", $"{site}/blog/{p.Slug}"),
                El("pubDate", p.Date.ToDateTime(new TimeOnly(12, 0)).ToString("R")), El("description", p.Summary)))));
    return Results.Text(rss.ToString(), "application/rss+xml");
});
app.MapRazorComponents<App>();

app.Run();
