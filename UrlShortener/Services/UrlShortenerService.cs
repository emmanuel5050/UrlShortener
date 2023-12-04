using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UrlShortener.Context;
using UrlShortener.DTOs;
using UrlShortener.Entities;
using UrlShortener.Interfaces;
using UrlShortener.Model;

namespace UrlShortener.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private readonly ITinyUrlRepository _tinyUrlRepository;
        private readonly UrlShortenerContext _context;
        private readonly IConfiguration _configuration;
        public UrlShortenerService(ITinyUrlRepository tinyUrlRepository, UrlShortenerContext context, IConfiguration configuration)
        {
            _tinyUrlRepository = tinyUrlRepository;
            _context = context;
            _configuration = configuration;
        }
        public async Task<string> GetLongUrlAsync(string shortCode, CancellationToken cancellationToken)
        {
            var shortUrl = await _context.TinyUrls.FirstOrDefaultAsync(sc=>sc.ShortCode == shortCode);
            if (shortUrl != null) return shortUrl.LongUrl;
            return null;
        }
        public async Task<BaseResponse<TinyUrlDTO>> GetTinyUrlAsync(string longUrl, CancellationToken cancellationToken)
        {
            string shortcode = string.Empty;
            bool isFound = true;
            while(isFound)
            {
                shortcode = GenerateRandomString(9);
                isFound = await _context.TinyUrls.AnyAsync(x => x.ShortCode == shortcode);
            }
            var saveUrlTodb = new TinyUrl { Id = Guid.NewGuid(), LongUrl = longUrl, ShortCode = shortcode, ShortUrl = GenerateUrl(shortcode)};

            var isInserted = await _tinyUrlRepository.InsertAsync(saveUrlTodb);
            if (isInserted)
            {
                var response= new TinyUrlDTO { Id = saveUrlTodb.Id, ShortUrl = saveUrlTodb.ShortUrl, LongUrl = saveUrlTodb.ShortUrl, ShortCode = saveUrlTodb.ShortCode };

                return new BaseResponse<TinyUrlDTO> { Data = response, Message = "Tiny Url successfully generated" };
            }
            return new BaseResponse<TinyUrlDTO> { Data = null, Message = "An error occurred, unable to generate url.", 
                Status = false };

        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            byte[] randomBytes = new byte[length];
            StringBuilder builder = new StringBuilder(length);

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            for (int i = 0; i < length; i++)
            {
                int index = randomBytes[i] % chars.Length;
                builder.Append(chars[index]);
            }

            return builder.ToString();
        }

        private string GenerateUrl(string shortCode)
        => $"{_configuration.GetValue<string>("BaseUrl")}/{shortCode}";

    }
}
