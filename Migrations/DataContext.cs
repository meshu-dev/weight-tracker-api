using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.IO;
using WeightTracker.Api.Entities;

namespace WeightTracker.Api.Migrations
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<WeighIn> WeighIns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = getConfig();

            optionsBuilder
                .UseSqlServer(config.GetConnectionString("SqlServer"));
        }

        private IConfiguration getConfig()
        {
            string pathToContentRoot = Directory.GetCurrentDirectory();
            string json = Path.Combine(pathToContentRoot, "appsettings.json");

            if (!File.Exists(json))
            {
                string pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                pathToContentRoot = Path.GetDirectoryName(pathToExe);
            }

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(pathToContentRoot)
                .AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            return config;
        }
    }
}
