namespace ConferenceRoom.Api.Models
{
    public class Result<T>
    {
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
        public static Result<T> Success(T data) => new Result<T> { Data = data };
        public static Result<T> Failure(string errorMessage) => new Result<T> { ErrorMessage = errorMessage };
    }

}
