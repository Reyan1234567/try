namespace MiniRedditCloneApi.Shared
{
    public class Result<T>
    {
        public bool Success { get; init; }
        public ErrorType? ErrorType { get; set; } = null;
        public string? Error { get; set; }
        public T Data { get; set; } = default!;

        public static Result<T> Ok(T data) => new() { Success = true, Data = data };

        public static Result<T> Fail(ErrorType? errorType, string error) => new() { Success = false, ErrorType = errorType, Error = error };
    }
}
# File update Fri, Jan  9, 2026  9:16:29 PM
# Update Fri, Jan  9, 2026  9:26:13 PM
# Update Fri, Jan  9, 2026  9:35:11 PM
