using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WeightTracker.Api.Entities
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _config;

        public DataContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WeighIn> WeighIns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("SQLServer"));
        }
    }
}
