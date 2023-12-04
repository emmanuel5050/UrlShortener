using System.ComponentModel.DataAnnotations;

namespace UrlShortener.DTOs
{
    public class ShortUrlRequestModel
    {
        [Url(ErrorMessage = "Invalid URL. Please enter a valid URL beginning with http:// or https://.")]
        public string Url { get; set; } 
    }
}
