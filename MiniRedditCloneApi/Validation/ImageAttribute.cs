using System.ComponentModel.DataAnnotations;

namespace MiniRedditCloneApi.Validation
{
    public class ImageAttribute : ValidationAttribute
    {
        private readonly string[] _allowedTypes = ["image/png", "image/jpeg", "image/gif", "image/webp"];
        private readonly int _maxBytes = 2 * 1024 * 1024;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not IFormFile file || file.Length == 0)
                return ValidationResult.Success;
            if (!_allowedTypes.Contains(file.ContentType)) return new ValidationResult("Only PNG, JPEG, WEBP or GIF images are allowed.");

            if (file.Length > _maxBytes) return new ValidationResult("Image file size must be less than 2MB.");

            using var ms = new MemoryStream();
            file.CopyTo(ms);
            var bytes = ms.ToArray();

            if (!IsValidImageSignature(bytes)) return new ValidationResult("Invalid image file.");
            return ValidationResult.Success;
        }

        private static bool IsValidImageSignature(byte[] fileBytes)
        {
            if (fileBytes.Length > 8 &&
            fileBytes[0] == 0x89 && fileBytes[1] == 0x50 && fileBytes[2] == 0x4E && fileBytes[3] == 0x47 &&
            fileBytes[4] == 0x0D && fileBytes[5] == 0x0A && fileBytes[6] == 0x1A && fileBytes[7] == 0x0A)
                return true; // PNG

            if (fileBytes.Length > 3 &&
                fileBytes[0] == 0xFF && fileBytes[1] == 0xD8 && fileBytes[2] == 0xFF)
                return true; // JPEG

            if (fileBytes.Length >= 12 &&
                fileBytes[0] == 0x52 && fileBytes[1] == 0x49 && fileBytes[2] == 0x46 && fileBytes[3] == 0x46 &&
                fileBytes[8] == 0x57 && fileBytes[9] == 0x45 && fileBytes[10] == 0x42 && fileBytes[11] == 0x50)
                return true; // WEBP

            if (fileBytes.Length > 4 &&
                fileBytes[0] == 0x47 && fileBytes[1] == 0x49 && fileBytes[2] == 0x46 && fileBytes[3] == 0x38)
                return true; // GIF

            return false;

        }
    }
}
# File update Fri, Jan  9, 2026  9:16:30 PM
# Update Fri, Jan  9, 2026  9:26:14 PM
# Update Fri, Jan  9, 2026  9:35:14 PM
