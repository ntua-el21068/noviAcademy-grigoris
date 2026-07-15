using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorldRank.Application.Repositories;
using WorldRank.Infrastructure.Persistence;
using WorldRank.Infrastructure.Repositories;

namespace WorldRank.Infrastructure;

public static class DependencyInjection
{
   public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString = null)
        {
            services.AddDbContext<WorldRankDbContext>(
                options => options.UseSqlServer(connectionString ?? "Server=localhost;Database=WorldRank;Trusted_Connection=True;TrustServerCertificate=True;"),
                ServiceLifetime.Scoped);

            services.AddScoped<IPlayerRepository, DBPlayerRepository>();
            services.AddScoped<IWalletRepository, DBWalletRepository>();

            return services;
        }
        }