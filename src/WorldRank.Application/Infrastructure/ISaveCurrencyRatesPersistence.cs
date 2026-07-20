using WorldRank.Domain.Entities;

namespace WorldRank.Application.Infrastructure;

public interface ISaveCurrencyRatesPersistence
{
    Task Replace(IReadOnlyList<CurrencyRate> toRemove, IReadOnlyList<CurrencyRate> toAdd, CancellationToken cancellationToken);
    Task<IReadOnlyList<CurrencyRate>> GetByDate(DateTime date, CancellationToken cancellationToken);
}