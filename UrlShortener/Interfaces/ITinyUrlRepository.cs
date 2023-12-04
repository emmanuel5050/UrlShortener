using UrlShortener.DTOs;
using UrlShortener.Entities;

namespace UrlShortener.Interfaces
{
    public interface ITinyUrlRepository
    {
        Task<bool> InsertAsync(TinyUrl tinyUrl, CancellationToken cancellationToken = default );

        Task<string> GetLongUrlAsync(string shortCode, CancellationToken cancellationToken = default);

        Task<TinyUrlDTO> GetShortCodeAsync(string longUrl, CancellationToken cancellationToken = default);
    }
}
