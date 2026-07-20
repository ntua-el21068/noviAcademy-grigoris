using MediatR;
using Quartz;
using WorldRank.Application.Commands.CurrencyRates;
using WorldRank.Application.HttpClients;

namespace WorldRank.Application.Jobs;

[DisallowConcurrentExecution]
public class DataFetchJob : IJob
{
    private readonly IEcbHttpClient _ecbHttpClient;
    private readonly ISender _sender;


    public DataFetchJob(IEcbHttpClient ecbHttpClient, ISender sender)
    {
        _ecbHttpClient = ecbHttpClient;
        _sender = sender;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var rates = await _ecbHttpClient.GetLatestRatesAsync(context.CancellationToken);
        await _sender.Send(new SaveCurrencyRatesCommand(rates), context.CancellationToken);
    }
}