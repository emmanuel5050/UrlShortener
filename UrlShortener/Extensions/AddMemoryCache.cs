namespace UrlShortener.Extensions
{
    public static class AddMemoryCache
    {
        public static void ConfigureMemoryCache(this IServiceCollection services)
        {
            services.AddMemoryCache();
        } 
    }
}
