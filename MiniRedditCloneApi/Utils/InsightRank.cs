using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Utils
{
    public class InsightRank
    {
        public static double GetInsightHotScore(Insight insight)
        {
            var kudos = insight.InsightReactions.Where(reaction => reaction.Reaction == Reaction.Kudos);
            var crits = insight.InsightReactions.Where(reaction => reaction.Reaction == Reaction.Crit);
            var score = kudos.Count() - crits.Count();
            var insightAge = (DateTime.UtcNow - insight.CreatedAt).TotalHours;
            if (score >= 0)
            {
                return Math.Log10(Math.Max(Math.Abs(score), 1)) - (insightAge / 24.0);
            }
            else
            {
                return -1 * (Math.Log10(Math.Max(Math.Abs(score), 1)) + (insightAge / 24.0));
            }
        }

        public static double GetInsightRisingScore(Insight insight)
        {
            var kudos = insight.InsightReactions.Where(reaction => reaction.Reaction == Reaction.Kudos);
            var crits = insight.InsightReactions.Where(reaction => reaction.Reaction == Reaction.Crit);
            var score = kudos.Count() - crits.Count();
            var insightAge = (DateTime.UtcNow - insight.CreatedAt).TotalHours;
            return score / Math.Max(insightAge, 1) / 1.0;
        }

        public static double GetInsightControversialScore(Insight insight)
        {
            var kudos = insight.InsightReactions.Where(reaction => reaction.Reaction == Reaction.Kudos);
            var crits = insight.InsightReactions.Where(reaction => reaction.Reaction == Reaction.Crit);
            var sum = kudos.Count() + crits.Count();
            var score = kudos.Count() - crits.Count();
            var insightAge = (DateTime.UtcNow - insight.CreatedAt).TotalHours;
            return sum / Math.Max(Math.Abs(score), 1);
        }

        public static double GetInsightTopScore(Insight insight)
        {
            var kudos = insight.InsightReactions.Where(reaction => reaction.Reaction == Reaction.Kudos);
            var crits = insight.InsightReactions.Where(reaction => reaction.Reaction == Reaction.Crit);
            return (kudos.Count() - crits.Count()) / 1.0;
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:29 PM
# Update Fri, Jan  9, 2026  9:26:14 PM
# Update Fri, Jan  9, 2026  9:35:12 PM
