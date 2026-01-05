using System.ComponentModel.DataAnnotations;
using MiniRedditCloneApi.Validation;

namespace MiniRedditCloneApi.DTOs.Insight
{
    public class CreateInsightDTO
    {
        [Required(ErrorMessage = "Please enter a title.")]
        public required string Title { get; set; }
        public string? Text { get; set; }
        public bool IsNSFW { get; set; } = false;
        public List<int> TopicIds { get; set; } = [];

        [Image]
        public IFormFile? Media { get; set; }
    }
}
# File update Fri, Jan  9, 2026  9:16:01 PM
# Update Fri, Jan  9, 2026  9:25:40 PM
# Update Fri, Jan  9, 2026  9:34:26 PM
// Logic update: qsZ5lppeWTYp
// Logic update: fGXmU063ST2w
// Logic update: ZzIJEnLRuwy7
// Logic update: QUQj8ic5wkSW
// Logic update: bXJQByeL9781
