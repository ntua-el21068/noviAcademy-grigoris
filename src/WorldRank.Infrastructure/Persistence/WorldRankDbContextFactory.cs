using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WorldRank.Infrastructure.Persistence;

public class WorldRankDbContextFactory : IDesignTimeDbContextFactory<WorldRankDbContext>
{
    public WorldRankDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WorldRankDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=WorldRank;Trusted_Connection=True;TrustServerCertificate=True;");
        return new WorldRankDbContext(optionsBuilder.Options);
    }
}
