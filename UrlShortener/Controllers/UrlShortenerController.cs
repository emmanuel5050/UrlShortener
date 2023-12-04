using Microsoft.AspNetCore.Mvc;
using UrlShortener.DTOs;
using UrlShortener.Interfaces;
using UrlShortener.Model;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IUrlShortenerService _urlShortenerService;
        public UrlShortenerController(IUrlShortenerService urlShortenerService)
        {
            _urlShortenerService = urlShortenerService;
        }
        [HttpPost("shorten-url")]
        public async Task<IActionResult> GetShortenUrl(ShortUrlRequestModel requestUrl)
        {
            if (!Uri.TryCreate(requestUrl.Url, UriKind.Absolute, out _))
                return BadRequest("Invalid Url specified");
            var result = await _urlShortenerService.GetTinyUrlAsync(requestUrl.Url);
            return Ok(result);
            
        }
        [HttpGet("retrive-original-url/{shortcode}")]
        public async Task<IActionResult> GetLongUrl(string shortcode)
        {
            var longurl = await _urlShortenerService.GetLongUrlAsync(shortcode);
            if (longurl == null) return NotFound();
            return Ok(longurl);

        }
    }
}
