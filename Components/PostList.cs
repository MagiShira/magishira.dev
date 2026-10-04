namespace Magishira.Components;

// blog metadata, each post is also a Razor page in Components/Posts that wraps its body in <Post Slug="...">
// to publish one add a row here, add the page and set Draft: false
public static class PostList
{
    public record Post(string Slug, string Title, DateOnly Date, string Summary, bool Draft = false);

    public static readonly Post[] Items =
    [
        new("hello-world", "Hello, world", new(2026, 10, 4), "A sample post that shows every building block. Drafts only render in Development.", true),
    ];

    public static bool ShowDrafts { get; set; } // set from Program.cs, true in Development only

    public static IEnumerable<Post> Visible => Items.Where(p => ShowDrafts || !p.Draft).OrderByDescending(p => p.Date);

    public static Post? Find(string slug) => Visible.FirstOrDefault(p => p.Slug == slug);
}
