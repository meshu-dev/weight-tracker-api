using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Web.Helpers;
using WeightTracker.Api.Entities;

namespace WeightTracker.Api.Migrations
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WeighIn> WeighIns { get; set; }

        public DataContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("SqlServer"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>()
              .HasData(
                new { Id = 1, Name = "Kilograms", ShortName = "kg" },
                new { Id = 2, Name = "Pounds", ShortName = "lbs" },
                new { Id = 3, Name = "Stone", ShortName = "st" },
                new { Id = 4, Name = "Stone & Pounds", ShortName = "st lbs" }
              );

            modelBuilder.Entity<User>()
              .HasData(
                new {
                    Id = 1,
                    UnitId = 1,
                    Email = "harmeshuppal@gmail.com",
                    Password = Crypto.HashPassword("12345"),
                    FirstName = "Mesh",
                    LastName = "Uppal"
                }
              );
        }
    }
}
