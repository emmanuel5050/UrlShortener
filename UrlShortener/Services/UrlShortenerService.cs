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
        private readonly IConfiguration _configuration;
        public UrlShortenerService(ITinyUrlRepository tinyUrlRepository, IConfiguration configuration)
        {
            _tinyUrlRepository = tinyUrlRepository;
            _configuration = configuration;
        }
        public async Task<BaseResponse<string>> GetLongUrlAsync(string shortCode, CancellationToken cancellationToken)
        {
            var shortUrl = await _tinyUrlRepository.GetUrlAsync(shortCode, cancellationToken);
            if (shortUrl != null) return new BaseResponse<string> { Data = shortUrl.LongUrl, Message = "long Url successfully retrieved", Status = true };
            return new BaseResponse<string> { Data = null, Message = "Invalid shortcode", Status = false };
        }
        public async Task<BaseResponse<TinyUrlDTO>> GetTinyUrlAsync(string longUrl, CancellationToken cancellationToken)
        {
            string shortcode = string.Empty;
            bool isFound = true;
            while(isFound)
            {
                shortcode = GenerateRandomString(9);
                isFound = await _tinyUrlRepository.ShortCodeExists(shortcode);
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
