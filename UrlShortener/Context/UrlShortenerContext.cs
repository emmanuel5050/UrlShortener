using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;
using UrlShortener.Entities;

namespace UrlShortener.Context
{
    public class UrlShortenerContext : DbContext
    {
        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options) : base (options) 
        {
            
        }
        public DbSet<TinyUrl> TinyUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TinyUrl>(builder => 
            {
                builder.Property(s => s.ShortCode).HasMaxLength(9);
                
                builder.HasIndex(s => s.ShortCode).IsUnique();

            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        
    }
}
