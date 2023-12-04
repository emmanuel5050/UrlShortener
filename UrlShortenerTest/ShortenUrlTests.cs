using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using UrlShortener.Context;
using UrlShortener.Interfaces;
using UrlShortener.Services;

namespace UrlShortenerTest
{
    public class ShortenUrlTests
    {
        private readonly Mock<UrlShortenerContext> _context;
        private readonly Mock<IMemoryCache> _cache;

        private readonly TinyUrlRepository _tinyUrlRepository;
        public ShortenUrlTests()
        {
            _cache = new Mock<IMemoryCache>();
            _context = new Mock<UrlShortenerContext>();

            _tinyUrlRepository = new TinyUrlRepository(_context.Object, _cache.Object);
            
        }
        [Fact]
        public async void GetLongUrlAsync_Succeed()
        {
            // Arrange
            var shortcode = "shortcode";

            // Act
            var response = await _tinyUrlRepository.GetLongUrlAsync(shortcode, default);
            //response.Should().Be()
        }
    }
}