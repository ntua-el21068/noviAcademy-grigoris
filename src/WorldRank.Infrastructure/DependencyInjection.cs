using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorldRank.Application.Interfaces;
using WorldRank.Infrastructure.Persistence;
using WorldRank.Infrastructure.Repositories;

namespace WorldRank.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, bool useDatabase, string? connectionString = null)
    {
        if (useDatabase)
        {
            services.AddDbContext<WorldRankDbContext>(
                options => options.UseSqlite(connectionString ?? "Data Source=worldrank.db"),
                ServiceLifetime.Singleton);

            services.AddSingleton<IPlayerRepository, DBPlayerRepository>();
            services.AddSingleton<IWalletRepository, DBWalletRepository>();
        }
        else
        {
            services.AddSingleton<IPlayerRepository, InMemoryPlayerRepository>();
            services.AddSingleton<IWalletRepository, InMemoryWalletRepository>();
        }

        return services;
    }
}