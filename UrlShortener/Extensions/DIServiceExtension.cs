using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using UrlShortener.Context;
using UrlShortener.Interfaces;
using UrlShortener.Services;

namespace UrlShortener.Extensions
{
    public static class DIServiceExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITinyUrlRepository, TinyUrlRepository>();
            services.AddScoped<IUrlShortenerService, UrlShortenerService>();

            services.AddDbContext<UrlShortenerContext>(options => options.UseSqlServer(config.GetConnectionString("DbConnectionString")));
            return services;
        }
    }
}
