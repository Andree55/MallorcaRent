using MallorcaRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MallorcaRent.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)   {  }

        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Reservation> Reservations => Set<Reservation>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=mallorcarent.db");
            }
        }
    }
}
