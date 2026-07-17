using MediatR;
using WorldRank.Application.Infrastructure;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Commands.CurrencyRates;

public class SaveCurrencyRatesHandler : IRequestHandler<SaveCurrencyRatesCommand>
{
    private readonly ISaveCurrencyRatesPersistence _persistence;

    public SaveCurrencyRatesHandler(ISaveCurrencyRatesPersistence persistence)
    {
        _persistence = persistence;
    }

    public async Task Handle(SaveCurrencyRatesCommand command, CancellationToken cancellationToken)
    { 
        var entities = command.Rates
            .Select(dto => new CurrencyRate(dto.Currency, dto.Rate, dto.Date))
            .ToList();

        if (entities.Count == 0)
            return;


        var date = entities[0].Date;
        var existing = await _persistence.GetByDate(date, cancellationToken);

        await _persistence.Replace(existing, entities, cancellationToken);
    }
}