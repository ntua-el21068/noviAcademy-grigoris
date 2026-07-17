using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.DTOs;

namespace WorldRank.Application.HttpClients
{
    public interface IEcbHttpClient
    {
        Task<IReadOnlyList<CurrencyRateDto>> GetLatestRatesAsync(CancellationToken cancellationToken = default);
    }
}
