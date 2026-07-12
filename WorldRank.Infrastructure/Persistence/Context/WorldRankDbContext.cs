using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldRank.Domain;


namespace WorldRank.Infrastructure.Persistence.Context 
{
    public class WorldRankDbContext :  DbContext
    {
        public DbSet<Player> Players {get; set;}
        public DbSet<Wallet> Wallets {get; set;}
        public WorldRankDbContext(DbContextOptions<WorldRankDbContext> options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(x =>
        {
            x.ToTable("Players");
            x.HasKey(x => x.Id);
            x.Property(y=>y.Id).ValueGeneratedNever();
            x.Property(y => y.Name).HasMaxLength(100).IsRequired();
            x.Property(y=>y.Score).IsRequired();
        }
        );
        base.OnModelCreating(modelBuilder);
    }
    }
}