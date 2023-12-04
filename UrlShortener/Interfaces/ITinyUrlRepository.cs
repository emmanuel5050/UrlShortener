using UrlShortener.DTOs;
using UrlShortener.Entities;

namespace UrlShortener.Interfaces
{
    public interface ITinyUrlRepository
    {
        Task<bool> InsertAsync(TinyUrl tinyUrl, CancellationToken cancellationToken = default );

        Task<TinyUrlDTO?> GetUrlAsync(string shortCode, CancellationToken cancellationToken = default);
        Task<bool> ShortCodeExists(string shortCode);

    }
}
