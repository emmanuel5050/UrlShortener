namespace UrlShortener.Model
{
    public class UrlShortenerSettings
    {
        public string BaseUrl { get; set; }

        public int ShortCodeLength { get; set; }

        public int DefaultExpirationCachedOnDays { get; set; } = 10;
    }
}
