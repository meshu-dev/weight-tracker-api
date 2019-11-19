using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Web.Helpers;
using WeightTracker.Api.Entities;

namespace WeightTracker.Api.Migrations
{
#pragma warning disable CS1591
    public class DataContext : DbContext
    {
        public DbSet<Unit> Units { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WeighIn> WeighIns { get; set; }

        private readonly IConfiguration _config;

        private readonly int AdminUserId = 1;

        private readonly int StandardUserId = 2;

        public DataContext(
            DbContextOptions options,
            IConfiguration config
        ) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("SqlServer"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            createInitialData(modelBuilder);

            bool addTestData = bool.Parse(_config.GetConnectionString("AddTestData"));

            if (addTestData == true)
                createTestData(modelBuilder);
        }

        private void createInitialData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>()
              .HasData(
                new { Id = 1, Name = "Kilograms", ShortName = "kg" },
                new { Id = 2, Name = "Pounds", ShortName = "lbs" },
                new { Id = 3, Name = "Stone", ShortName = "st" },
                new { Id = 4, Name = "Stone & Pounds", ShortName = "st lbs" }
              );

            modelBuilder.Entity<Role>()
              .HasData(
                new { Id = 1, Name = "Admin" },
                new { Id = 2, Name = "Standard" }
              );

            modelBuilder.Entity<User>()
              .HasData(
                new
                {
                    Id = AdminUserId,
                    RoleId = 1,
                    UnitId = 1,
                    Email = "harmeshuppal@gmail.com",
                    Password = Crypto.HashPassword("12345"),
                    FirstName = "Mesh",
                    LastName = "Uppal"
                },
                new
                {
                    Id = StandardUserId,
                    RoleId = 2,
                    UnitId = 1,
                    Email = "test@gmail.com",
                    Password = Crypto.HashPassword("abcde"),
                    FirstName = "Test",
                    LastName = "Man"
                }
              );
        }

        private void createTestData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeighIn>()
              .HasData(
                new
                {
                    Id = 1,
                    UserId = StandardUserId,
                    Value = "150",
                    Date = System.DateTime.Now
                },
                new
                {
                    Id = 2,
                    UserId = StandardUserId,
                    Value = "160",
                    Date = System.DateTime.Now
                }
              );
        }
    }
    #pragma warning restore CS1591
}
