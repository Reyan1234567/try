using MiniRedditCloneApi.DTOs.Response;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Services.Interfaces
{
    public interface INsfwDetectionService
    {
        Task<Result<NsfwDetectionResponse>> CheckImageAsync(Stream imageStream, string filename, bool allowNSFW);
        Task<Result<NsfwDetectionResponse>> CheckTextAsync(string text, bool allowNSFW);
    }
}# File update Fri, Jan  9, 2026  9:16:28 PM
# Update Fri, Jan  9, 2026  9:26:12 PM
# Update Fri, Jan  9, 2026  9:35:09 PM
