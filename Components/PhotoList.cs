namespace Magishira.Components;

// photos for the home strip, files live in wwwroot/photos at 1600px on the long edge
public static class PhotoList
{
    public record Photo(string File, int Width, int Height, string Title, string Caption, string Alt);

    public static readonly Photo[] Items =
    [
        new("photos/frog-balloon.webp", 1600, 1067, "Frog", "Great Reno Balloon Race, September 2024.", "A frog-shaped hot air balloon seen from below against a blue sky"),
        new("photos/tulip-balloon.webp", 1067, 1600, "Tulips", "Great Reno Balloon Race, September 2024.", "A yellow hot air balloon decorated with red tulips, seen from below"),
        new("photos/brothers-balloon.webp", 1600, 1067, "Brothers", "Great Reno Balloon Race, September 2024.", "A rainbow-checkered hot air balloon seen from below"),
        new("photos/sticker-laptop.webp", 1600, 1067, "Fuel", "UNR ACM Hackathon, April 2026.", "A laptop covered in stickers, surrounded by Red Bull cans"),
    ];
}
