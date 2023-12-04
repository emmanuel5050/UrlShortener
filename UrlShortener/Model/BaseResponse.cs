namespace UrlShortener.Model
{
    public  class BaseResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public static BaseResponse Success(string? message = null)
        {
            return new BaseResponse()
            {
                Status = true,
                Message = message ?? "Successful",
            };
        }

        public static BaseResponse Failure(string? message = null)
        {
            return new BaseResponse()
            {
                Message = message ?? "Failed",
            };
        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        public static BaseResponse<T> Success(T data, string? message = null)
        {
            return new BaseResponse<T>()
            {
                Status = true,
                Message = message ?? "Successful",
                Data = data
            };
        }

        public static BaseResponse<T> Failure(T data, string? message = null)
        {
            return new BaseResponse<T>()
            {
                Message = message ?? "Failed",
                Data = data
            };
        }
    }
}
