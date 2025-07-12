using Microsoft.EntityFrameworkCore;
using WeatherSearch.Models;

namespace WeatherSearch.Data
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }

        public DbSet<SearchedLocations> SearchedLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchedLocations>()
                .HasIndex(l => new { l.Name, l.Country })
                .IsUnique();
        }
    }
}