namespace Magishira.Components;

// one list feeds the home strip and the /photos gallery, files live in wwwroot/photos at 1600px on the long edge
public static class PhotoList
{
    public record Photo(string File, int Width, int Height, string Title, string Caption, string Alt);

    public static readonly Photo[] Items =
    [
        new("photos/frog-balloon.webp", 1600, 1067, "Frog", "Great Reno Balloon Race, September 2024.", "A frog-shaped hot air balloon seen from below against a blue sky"),
        new("photos/tulip-balloon.webp", 1067, 1600, "Tulips", "Great Reno Balloon Race, September 2024.", "A yellow hot air balloon decorated with red tulips, seen from below"),
        new("photos/brothers-balloon.webp", 1600, 1067, "Brothers", "Great Reno Balloon Race, September 2024.", "A rainbow-checkered hot air balloon seen from below"),
        new("photos/cleanroom-wafer.webp", 1600, 1067, "Wafer", "Davidson Foundation Cleanroom booth, Fall 2026 ASUN Club Fair.", "Someone in a cleanroom suit holding up a silicon wafer at a club fair booth"),
        new("photos/hackathon-pointing.webp", 1600, 1067, "Debugging", "UNR ACM Hackathon, April 2026.", "A mentor pointing at a laptop screen while two students look on"),
        new("photos/hackathon-focus.webp", 1600, 1067, "Heads down", "UNR ACM Hackathon, April 2026.", "A participant typing intently at a laptop"),
        new("photos/hackathon-editing.webp", 1600, 1067, "Cut", "UNR ACM Hackathon, April 2026.", "A participant in headphones editing video on a laptop"),
        new("photos/hackathon-rock-on.webp", 1600, 1067, "Go Pack", "UNR ACM Hackathon, April 2026.", "A smiling participant throwing up the Wolf Pack hand sign"),
        new("photos/sticker-laptop.webp", 1600, 1067, "Fuel", "UNR ACM Hackathon, April 2026.", "A laptop covered in stickers, surrounded by Red Bull cans"),
    ];
}
