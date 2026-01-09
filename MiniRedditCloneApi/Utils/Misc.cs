namespace MiniRedditCloneApi.Utils
{
    public class Misc
    {
        public static string GetImageContentType(byte[] imageData)
        {
            if (imageData.Length >= 4)
            {
                // PNG: 89 50 4E 47
                if (imageData[0] == 0x89 && imageData[1] == 0x50 && imageData[2] == 0x4E && imageData[3] == 0x47)
                    return "image/png";
                // JPEG: FF D8 FF
                if (imageData[0] == 0xFF && imageData[1] == 0xD8 && imageData[2] == 0xFF)
                    return "image/jpeg";

                if (imageData[0] == 0x52 && imageData[1] == 0x49 && imageData[2] == 0x46 && imageData[3] == 0x46 &&
    imageData[8] == 0x57 && imageData[9] == 0x45 && imageData[10] == 0x42 && imageData[11] == 0x50)
                    return "image/webp";

                // GIF: 47 49 46 38
                if (imageData[0] == 0x47 && imageData[1] == 0x49 && imageData[2] == 0x46 && imageData[3] == 0x38)
                    return "image/gif";
            }
            return "application/octet-stream";
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:29 PM
# Update Fri, Jan  9, 2026  9:26:14 PM
# Update Fri, Jan  9, 2026  9:35:12 PM
