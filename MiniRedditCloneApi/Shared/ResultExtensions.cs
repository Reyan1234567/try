using Microsoft.AspNetCore.Mvc;

namespace MiniRedditCloneApi.Shared
{
    public static class ResultExtensions
    {
        public static IActionResult ToErrorActionResult<T>(this Result<T> result, ControllerBase controller)
        {
            return result.ErrorType switch
            {
                ErrorType.BadRequest => controller.BadRequest(new { error = result.Error }),
                ErrorType.Unauthorized => controller.Unauthorized(new { error = result.Error }),
                ErrorType.Forbidden => controller.StatusCode(403, new { error = result.Error }),
                ErrorType.NotFound => controller.NotFound(new { error = result.Error }),
                ErrorType.Conflict => controller.Conflict(new { error = result.Error }),
                ErrorType.ServerError => controller.StatusCode(500, new { error = result.Error }),
                _ => controller.StatusCode(500, new { error = "Unknown error" })
            };
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:29 PM
# Update Fri, Jan  9, 2026  9:26:13 PM
# Update Fri, Jan  9, 2026  9:35:11 PM
