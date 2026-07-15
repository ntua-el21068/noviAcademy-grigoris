using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Infrastructure
{
    public interface IGetWalletsByPlayerIdPersistence
    {
        Task<List<Wallet>> Get(int playerId, CancellationToken cancellationToken);
    }
}
