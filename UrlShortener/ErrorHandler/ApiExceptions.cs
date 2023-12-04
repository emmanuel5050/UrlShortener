namespace UrlShortener.ErrorHandler
{
    public class ApiExceptions
    {
        public ApiExceptions(int statusCode, string errorMessage, string details)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Details { get; set; }
    }
}