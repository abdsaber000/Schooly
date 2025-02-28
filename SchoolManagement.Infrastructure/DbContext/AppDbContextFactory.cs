using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Infrastructure.DbContext;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory() + "/../SchoolManagement.Api/")
            .AddJsonFile("appsettings.json")
            .Build();
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        string connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        optionsBuilder.UseSqlServer(connectionString);
        return new AppDbContext(optionsBuilder.Options);
    }
}