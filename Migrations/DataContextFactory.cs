using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WeightTracker.Api.Migrations
{
    #pragma warning disable CS1591
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return new DataContext(
                new DbContextOptionsBuilder<DataContext>().Options,
                config
            );
        }
    }
    #pragma warning restore CS1591
}
