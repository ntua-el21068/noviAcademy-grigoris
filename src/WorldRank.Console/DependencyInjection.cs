using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using WorldRank.Application;
using WorldRank.Infrastructure;

namespace WorldRank.Console;

public static class DependencyInjection
{
	// Composition root: wires up every layer's services in one place.
	public static IServiceCollection AddWorldRank(this IServiceCollection services, bool useDatabase)
{
    services.AddLogging(builder =>
    {
        builder.ClearProviders();
        builder.SetMinimumLevel(LogLevel.Trace);
        builder.AddNLog();
    });

    services.AddApplication();
    services.AddInfrastructure(useDatabase);

    return services;
}
}
