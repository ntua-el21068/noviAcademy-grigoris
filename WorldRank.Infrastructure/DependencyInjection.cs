using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorldRank.Application;
using WorldRank.Infrastructure.Persistence.Context;

namespace WorldRank.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IPlayerRepository, InMemoryPlayerRepository>();
        services.AddSingleton<IWalletRepository, InMemoryWalletRepository>();
        services.AddDbContext<WorldRankDbContext>(options =>

        {
            options.UseSqlServer("Server=localhost,1433;Database=WorldRank;User Id=SA;Password=12345678Grigoris!;TrustServerCertificate=True;");
        }
        );
        return services;
    }
}