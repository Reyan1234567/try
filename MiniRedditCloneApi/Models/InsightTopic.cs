namespace MiniRedditCloneApi.Models
{
    public class InsightTopic
    {
        public int InsightId { get; set; }
        public int TopicId { get; set; }

        public Insight Insight { get; set; } = null!;
        public Topic Topic { get; set; } = null!;
    }
}
# File update Fri, Jan  9, 2026  9:16:17 PM
# Update Fri, Jan  9, 2026  9:25:58 PM
# Update Fri, Jan  9, 2026  9:34:42 PM
