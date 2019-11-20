using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
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

        private readonly int StandardTestUserId = 3;

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
            modelBuilder.Entity<User>()
              .HasData(
                new
                {
                    Id = 3,
                    RoleId = 2,
                    UnitId = 2,
                    Email = "test2@gmail.com",
                    Password = Crypto.HashPassword("abcdefg"),
                    FirstName = "Tester",
                    LastName = "Two"
                }
              );

            modelBuilder.Entity<WeighIn>()
              .HasData(
                new
                {
                    Id = 1,
                    UserId = StandardUserId,
                    Value = "144",
                    Date = new DateTime(2019, 9, 7, 8, 00, 00)
                },
                new
                {
                    Id = 2,
                    UserId = StandardUserId,
                    Value = "144",
                    Date = new DateTime(2019, 9, 14, 8, 00, 00)
                },
                new
                {
                    Id = 3,
                    UserId = StandardTestUserId,
                    Value = "160",
                    Date = new DateTime(2019, 9, 10, 9, 30, 00)
                },
                new
                {
                    Id = 4,
                    UserId = StandardUserId,
                    Value = "143",
                    Date = new DateTime(2019, 9, 21, 8, 00, 00)
                },
                new
                {
                    Id = 5,
                    UserId = StandardTestUserId,
                    Value = "159",
                    Date = new DateTime(2019, 9, 23, 9, 30, 00)
                },
                new
                {
                    Id = 6,
                    UserId = StandardUserId,
                    Value = "141.4",
                    Date = new DateTime(2019, 9, 28, 8, 00, 00)
                },
                new
                {
                    Id = 7,
                    UserId = StandardUserId,
                    Value = "141",
                    Date = new DateTime(2019, 10, 5, 8, 00, 00)
                },
                new
                {
                    Id = 8,
                    UserId = StandardUserId,
                    Value = "140",
                    Date = new DateTime(2019, 10, 12, 8, 00, 00)
                },
                new
                {
                    Id = 9,
                    UserId = StandardUserId,
                    Value = "139.4",
                    Date = new DateTime(2019, 10, 19, 8, 00, 00)
                },
                new
                {
                    Id = 10,
                    UserId = StandardUserId,
                    Value = "138.2",
                    Date = new DateTime(2019, 10, 26, 8, 00, 00)
                },
                new
                {
                    Id = 11,
                    UserId = StandardTestUserId,
                    Value = "158.5",
                    Date = new DateTime(2019, 10, 30, 9, 30, 00)
                },
                new
                {
                    Id = 12,
                    UserId = StandardUserId,
                    Value = "138.4",
                    Date = new DateTime(2019, 11, 2, 8, 00, 00)
                },
                new
                {
                    Id = 13,
                    UserId = StandardUserId,
                    Value = "137.6",
                    Date = new DateTime(2019, 11, 9, 8, 00, 00)
                },
                new
                {
                    Id = 14,
                    UserId = StandardUserId,
                    Value = "136.4",
                    Date = new DateTime(2019, 11, 16, 8, 00, 00)
                },
                new
                {
                    Id = 15,
                    UserId = StandardUserId,
                    Value = "136",
                    Date = new DateTime(2019, 11, 19, 8, 00, 00)
                }
              );
        }
    }
    #pragma warning restore CS1591
}
