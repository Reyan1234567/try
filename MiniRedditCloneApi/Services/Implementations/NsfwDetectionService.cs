using System.Net.Http.Headers;
using Microsoft.VisualBasic;
using MiniRedditCloneApi.DTOs.Response;
using MiniRedditCloneApi.Services.Interfaces;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Services.Implementations
{
    public class NsfwDetectionService(HttpClient httpClient) : INsfwDetectionService
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<Result<NsfwDetectionResponse>> CheckImageAsync(Stream imageStream, string filename, bool allowNSFW)
        {
            using var content = new MultipartFormDataContent();

            var fileContent = new StreamContent(imageStream);
            var fileExtension = Strings.Split(filename, ".").Last();
            fileContent.Headers.ContentType = new MediaTypeHeaderValue($"image/{fileExtension}");
            content.Add(fileContent, "image", $"avatar.{fileExtension}");
            var response = await _httpClient.PostAsync("/moderate/image", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                return Result<NsfwDetectionResponse>.Fail(ErrorType.ServerError, $"{errorJson}");
            }
            var data = await response.Content.ReadFromJsonAsync<NsfwDetectionResponse>();
            return Result<NsfwDetectionResponse>.Ok(data!);
        }

        public async Task<Result<NsfwDetectionResponse>> CheckTextAsync(string text, bool allowNSFW)
        {
            var request = new Dictionary<string, object>()
            {
                {"text", text},
                {"allow_nsfw", allowNSFW}
            };

            var response = await _httpClient.PostAsJsonAsync("/moderate/text", request);

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                return Result<NsfwDetectionResponse>.Fail(ErrorType.ServerError, $"{errorJson}");
            }
            var data = await response.Content.ReadFromJsonAsync<NsfwDetectionResponse>();
            return Result<NsfwDetectionResponse>.Ok(data!);
        }
    }
}# File update Fri, Jan  9, 2026  9:16:25 PM
# Update Fri, Jan  9, 2026  9:26:09 PM
# Update Fri, Jan  9, 2026  9:35:02 PM
