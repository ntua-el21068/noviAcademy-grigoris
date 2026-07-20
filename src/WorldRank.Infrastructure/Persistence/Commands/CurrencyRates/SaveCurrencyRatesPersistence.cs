using Microsoft.EntityFrameworkCore;
using WorldRank.Application.Infrastructure;
using WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Persistence.Commands.CurrencyRates;

public class SaveCurrencyRatesPersistence : ISaveCurrencyRatesPersistence
{
    private readonly WorldRankDbContext _context;

    public SaveCurrencyRatesPersistence(WorldRankDbContext context)
    {
        _context = context;
    }


    public async Task<IReadOnlyList<CurrencyRate>> GetByDate(DateTime date, CancellationToken cancellationToken)
    {
        return await _context.CurrencyRates
            .Where(r => r.Date == date)
            .ToListAsync(cancellationToken);
    }

    public async Task Replace(IReadOnlyList<CurrencyRate> toRemove, IReadOnlyList<CurrencyRate> toAdd, CancellationToken cancellationToken)
    {
        _context.RemoveRange(toRemove);
        await _context.AddRangeAsync(toAdd, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }


}