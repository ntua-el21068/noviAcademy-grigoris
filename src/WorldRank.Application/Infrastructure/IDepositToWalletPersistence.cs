using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Domain.Enums;

namespace WorldRank.Application.Infrastructure
{
    public interface IDepositToWalletPersistence 
    {
        Task<decimal> Persist(int playerId, decimal amount, Currency currency, CancellationToken cancellationToken);
    }
}
