using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Commands.Wallets;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Infrastructure
{
    public interface ICreateWalletPersistence
    {
        Task<int> Persist(Wallet wallet, CancellationToken cancellationToken);
    }
}
