using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SchoolManagement.Infrastructure.DbContext;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlServer("server = DESKTOP-SG7M06R; database=SchoolManagement; Trusted_Connection=True; TrustServerCertificate=true");
        return new AppDbContext(optionsBuilder.Options);
    }
}