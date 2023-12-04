using AutoMapper;
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
        private readonly IMapper _mapper;
        public TinyUrlRepository(UrlShortenerContext context, IMemoryCache memoryCache, IMapper mapper)
        {
            _context = context;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }
        public async Task<TinyUrlDTO?> GetUrlAsync(string shortCode, CancellationToken cancellationToken)
        {
            if (_memoryCache.TryGetValue(shortCode, out string CachedUrlEntity))
            {
                var tinyUrlEntity = JsonConvert.DeserializeObject<TinyUrl>(CachedUrlEntity);
                var tinyUrlDTO =_mapper.Map<TinyUrlDTO>(tinyUrlEntity);

                return tinyUrlDTO;
            }

            var tinyUrl = await _context.TinyUrls.FirstOrDefaultAsync(x => x.ShortCode == shortCode, cancellationToken);
            if (tinyUrl != null)
            {
                var tinyUrlDT = _mapper.Map<TinyUrlDTO>(tinyUrl);
                return tinyUrlDT;
            }
            return null;          
        }

        public async Task<bool> InsertAsync(TinyUrl tinyUrl, CancellationToken cancellationToken)
        {
            await _context.TinyUrls.AddAsync(tinyUrl, cancellationToken);
            _memoryCache.Set(tinyUrl.ShortCode, JsonConvert.SerializeObject(tinyUrl));

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public Task<bool> ShortCodeExists(string shortCode)
        {
            return _context.TinyUrls.AnyAsync(x => x.ShortCode == shortCode);
        }
    }
}
