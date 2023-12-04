using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Entities
{
    public class TinyUrl
    {
        [Key]
        public Guid Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public string ShortCode { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
