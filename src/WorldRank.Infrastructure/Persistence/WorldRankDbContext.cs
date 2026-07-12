using Microsoft.EntityFrameworkCore;
using WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Persistence;

public class WorldRankDbContext : DbContext
{
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Wallet> Wallets => Set<Wallet>();

    public WorldRankDbContext(DbContextOptions<WorldRankDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorldRankDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}