using Magishira.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();
builder.Services.AddResponseCompression(o => o.EnableForHttps = true); // static assets are pre-compressed, this covers the HTML

var app = builder.Build();

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
app.MapRazorComponents<App>();

app.Run();
