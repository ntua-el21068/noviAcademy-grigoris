using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using WorldRank.Application.Services;
using WorldRank.Application.Strategies;
using WorldRank.Infrastructure;
using WorldRank.Infrastructure.Caching;
using WorldRank.Application.Caching;
using Autofac;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
{
    cb.RegisterModule<WorldRank.Application.ApplicationModule>();
    cb.RegisterModule<WorldRank.Infrastructure.InfrastructureModule>();
});

// Logging via NLog
builder.Logging.ClearProviders();
builder.Logging.AddNLog("nlog.config");

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICache, MemoryCacheStore>();

// Register the DB context + DB-backed repositories
builder.Services.AddInfrastructure(connectionString: "Data Source=worldrank.db");

// Register the application services 
//builder.Services.AddScoped<PlayerService>();
//builder.Services.AddScoped<WalletService>();

// Funds strategies (WalletService needs IEnumerable<IFundsStrategy>)
builder.Services.AddSingleton<IFundsStrategy, AddFundsStrategy>();
builder.Services.AddSingleton<IFundsStrategy, SubtractFundsStrategy>();
builder.Services.AddSingleton<IFundsStrategy, ForceSubtractFundsStrategy>();

// Controllers, with enums serialized as strings (e.g. Currency).
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.Redirect("/swagger"));
}

app.MapControllers();

app.Run();