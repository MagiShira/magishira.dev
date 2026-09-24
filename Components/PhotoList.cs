namespace Magishira.Components;

// one list feeds the home strip and the /photos gallery, files live in wwwroot/photos at 1600px on the long edge
public static class PhotoList
{
    public record Photo(string File, int Width, int Height, string Title, string Caption, string Alt, params string[] Tags);

    // the home strip skips graduation portraits, they only live in the full gallery
    public static IEnumerable<Photo> Strip => Items.Where(p => !p.Tags.Contains("Graduation"));

    public static readonly Photo[] Items =
    [
        new("photos/frog-balloon.webp", 1600, 1067, "Frog", "Great Reno Balloon Race, September 2024.", "A frog-shaped hot air balloon seen from below against a blue sky", "Balloons", "Sky"),
        new("photos/tulip-balloon.webp", 1067, 1600, "Tulips", "Great Reno Balloon Race, September 2024.", "A yellow hot air balloon decorated with red tulips, seen from below", "Balloons", "Sky"),
        new("photos/brothers-balloon.webp", 1600, 1067, "Brothers", "Great Reno Balloon Race, September 2024.", "A rainbow-checkered hot air balloon seen from below", "Balloons", "Sky"),
        new("photos/cleanroom-wafer.webp", 1600, 1067, "Wafer", "Davidson Foundation Cleanroom booth, Fall 2026 ASUN Club Fair.", "Someone in a cleanroom suit holding up a silicon wafer at a club fair booth", "Campus", "People", "Tech"),
        new("photos/hackathon-pointing.webp", 1600, 1067, "Debugging", "UNR ACM Hackathon, April 2026.", "A mentor pointing at a laptop screen while two students look on", "Hackathon", "People"),
        new("photos/hackathon-focus.webp", 1600, 1067, "Heads down", "UNR ACM Hackathon, April 2026.", "A participant typing intently at a laptop", "Hackathon", "People"),
        new("photos/hackathon-editing.webp", 1600, 1067, "Cut", "UNR ACM Hackathon, April 2026.", "A participant in headphones editing video on a laptop", "Hackathon", "People"),
        new("photos/hackathon-rock-on.webp", 1600, 1067, "Go Pack", "UNR ACM Hackathon, April 2026.", "A smiling participant throwing up the Wolf Pack hand sign", "Hackathon", "People"),
        new("photos/sticker-laptop.webp", 1600, 1067, "Fuel", "UNR ACM Hackathon, April 2026.", "A laptop covered in stickers, surrounded by Red Bull cans", "Hackathon", "Tech"),
        new("photos/grad-arms-crossed.webp", 1067, 1600, "", "John's graduation, University of Nevada, Reno, May 2025.", "A graduate in cap, gown, and blue First Generation stole, arms crossed on a tree-lined path", "Graduation", "People", "Campus"),
        new("photos/grad-blossoms.webp", 1067, 1600, "", "John's graduation, University of Nevada, Reno, May 2025.", "A graduate leaning against a flowering tree outside a brick building", "Graduation", "People", "Campus"),
        new("photos/grad-lawn.webp", 1067, 1600, "", "John's graduation, University of Nevada, Reno, May 2025.", "A graduate sitting on the grass in cap and gown", "Graduation", "People", "Campus"),
        new("photos/grad-brick.webp", 1067, 1600, "", "John's graduation, University of Nevada, Reno, May 2025.", "A graduate seated against a brick wall, looking off to the side", "Graduation", "People", "Campus"),
        new("photos/grad-arbor.webp", 1067, 1600, "", "John's graduation, University of Nevada, Reno, May 2025.", "A graduate seated on a rock under a wisteria arbor", "Graduation", "People", "Campus"),
        new("photos/grad-portrait.webp", 1067, 1600, "", "John's graduation, University of Nevada, Reno, May 2025.", "A close portrait of a graduate under green leaves", "Graduation", "People", "Campus"),
        new("photos/grad-railing.webp", 1067, 1600, "", "John's graduation, University of Nevada, Reno, May 2025.", "A graduate leaning on a white railing with arms spread", "Graduation", "People", "Campus"),
        new("photos/grad-go-pack.webp", 1067, 1600, "", "John's graduation, University of Nevada, Reno, May 2025.", "A graduate in a First Generation stole sitting on steps, throwing the Wolf Pack hand sign", "Graduation", "People", "Campus"),
        new("photos/grad-cap-raised.webp", 1600, 1600, "", "John's graduation, University of Nevada, Reno, May 2025.", "A graduate under a brick arch holding a mortarboard high", "Graduation", "People", "Campus"),
        new("photos/grad-quad.webp", 1067, 1600, "", "John's graduation, University of Nevada, Reno, May 2025.", "Looking over a graduate's shoulder down the tree-lined Quad toward the administration building", "Graduation", "People", "Campus"),
        new("photos/grad-cap-wave.webp", 1067, 1600, "", "John's graduation, University of Nevada, Reno, May 2025.", "A graduate seen from behind, waving a mortarboard toward the Quad", "Graduation", "People", "Campus"),
        new("photos/grad-mackay.webp", 1600, 1067, "", "John's graduation, University of Nevada, Reno, May 2025.", "A graduate pointing at the camera beside the Mackay statue's inscribed base", "Graduation", "People", "Campus"),
    ];
}
