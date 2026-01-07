using System.Text.Json.Serialization;

namespace MiniRedditCloneApi.DTOs.Response
{
    public enum ModerationDecision
    {
        ALLOW,
        PENDING_MODERATION,
        REJECT
    }

    public class ModerationSignal
    {
        public required string Category { get; set; }
        public required string Label { get; set; }
        public float Confidence { get; set; }
    }

    public class NsfwDetectionResponse
    {
        [JsonPropertyName("decision")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required ModerationDecision Decision { get; set; }
        [JsonPropertyName("is_nsfw")]
        public bool IsNSFW { get; set; }
        [JsonPropertyName("reason")]
        public required string Reason { get; set; }
        [JsonPropertyName("signals")]
        public required ModerationSignal Signal { get; set; }
    }
}# File update Fri, Jan  9, 2026  9:16:03 PM
# Update Fri, Jan  9, 2026  9:25:43 PM
# Update Fri, Jan  9, 2026  9:34:29 PM
// Logic update: sLotgDqS5tJL
// Logic update: D3bBiRs72DFX
// Logic update: avw3gGf1ubeG
