using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WorldRank.Infrastructure.Persistence;

public class WorldRankDbContextFactory : IDesignTimeDbContextFactory<WorldRankDbContext>
{
    public WorldRankDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WorldRankDbContext>();
        optionsBuilder.UseSqlite("Data Source=worldrank.db");
        return new WorldRankDbContext(optionsBuilder.Options);
    }
}
