using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using UrlShortener.Context;
using UrlShortener.DTOs;
using UrlShortener.Entities;
using UrlShortener.Interfaces;

namespace UrlShortener.Services
{
    public class TinyUrlRepository : ITinyUrlRepository
    {
        private readonly UrlShortenerContext _context;
        private readonly IMemoryCache _memoryCache;
        public TinyUrlRepository(UrlShortenerContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public async Task<string> GetLongUrlAsync(string shortCode, CancellationToken cancellationToken)
        {
            if (_memoryCache.TryGetValue(shortCode, out string longUrl))
            {
                var tinyUrlDTO = JsonConvert.DeserializeObject<TinyUrlDTO>(longUrl);
                return tinyUrlDTO.LongUrl;
            }

            var tinyUrl = await _context.TinyUrls.FirstOrDefaultAsync(x => x.ShortCode == shortCode, cancellationToken);
            return tinyUrl?.LongUrl;
        }

        public async Task<bool> InsertAsync(TinyUrl tinyUrl, CancellationToken cancellationToken)
        {
            await _context.TinyUrls.AddAsync(tinyUrl, cancellationToken);
            _memoryCache.Set(tinyUrl.ShortCode, JsonConvert.SerializeObject(tinyUrl));

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        Task<TinyUrlDTO> ITinyUrlRepository.GetShortCodeAsync(string longUrl, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
