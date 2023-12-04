using UrlShortener.DTOs;
using UrlShortener.Model;

namespace UrlShortener.Interfaces
{
    public interface IUrlShortenerService
    {
        Task<BaseResponse<TinyUrlDTO>> GetTinyUrlAsync(string longUrl, CancellationToken cancellationToken = default);

        Task<string> GetLongUrlAsync(string shortCode, CancellationToken cancellationToken = default);
    }
}
