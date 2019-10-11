using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeightTracker.Api.Entities;

namespace WeightTracker.Api.Migrations
{
    public class DataContext : DbContext
    {
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WeighIn> WeighIns { get; set; }

        private readonly IConfiguration _config;

        public DataContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("SqlServer"));
        }
    }
}
