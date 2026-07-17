using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using Quartz;
using System.Text.Json.Serialization;
using WorldRank.Application.Caching;
using WorldRank.Application.HttpClients;
using WorldRank.Application.Jobs;
using WorldRank.Application.Services;
using WorldRank.Application.Strategies;
using WorldRank.Gateway;
using WorldRank.Infrastructure;
using WorldRank.Infrastructure.Caching;

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
builder.Services.AddInfrastructure(connectionString: "Server=localhost;Database=WorldRank;Trusted_Connection=True;TrustServerCertificate=True;");

//Register EcbHttpClient 
builder.Services.AddHttpClient<IEcbHttpClient, EcbHttpClient>(client =>
{
    client.BaseAddress = new Uri("https://www.ecb.europa.eu/");
});

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

//CurrencyRate scheduled data fetch (everyday at 16:00)
builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("DataFetchJob");

    q.AddJob<DataFetchJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("DataFetchJob-trigger")
        //.WithCronSchedule("0 0 16 * * ?"));
        .WithSimpleSchedule(s => s.WithIntervalInSeconds(30).RepeatForever()));
});

builder.Services.AddQuartzHostedService(opts => opts.WaitForJobsToComplete = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.Redirect("/swagger"));
}

app.MapControllers();

app.Run();