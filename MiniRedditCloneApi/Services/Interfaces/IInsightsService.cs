using System.Security.Claims;
using MiniRedditCloneApi.DTOs.Insight;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Services.Interfaces
{
    public interface IInsightsService
    {
        Task<Result<List<Insight>>> GetPopularInsightsAsync(ClaimsPrincipal user, int page, int pageSize);
        Task<Result<Insight>> CreateInsightAsync(ClaimsPrincipal user, int herdId, CreateInsightDTO dto);
        Task<Result<byte[]>> GetInsightMediaAsync(int insightId);
        Task<Result<List<Insight>>> GetHerdInsightsAsync(ClaimsPrincipal user, int herdId, InsightSort sort, TopTimeRange? range, int page, int pageSize);
        Task<Result<string>> ToggleInsightKudosAsync(ClaimsPrincipal user, int id);
        Task<Result<string>> ToggleInsightCritAsync(ClaimsPrincipal user, int id);
        Task<Result<List<Insight>>> GetPendingInsightsAsync(ClaimsPrincipal user, int herdId);
        Task<Result<List<Insight>>> GetModeratorRemovedInsightsAsync(ClaimsPrincipal user, int herdId);
        Task<Result<List<Insight>>> GetMyInsightsAsync(ClaimsPrincipal user);
    }
}
# File update Fri, Jan  9, 2026  9:16:28 PM
# Update Fri, Jan  9, 2026  9:26:12 PM
# Update Fri, Jan  9, 2026  9:35:08 PM
