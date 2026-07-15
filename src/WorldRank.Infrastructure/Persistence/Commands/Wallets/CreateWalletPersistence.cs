using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using WorldRank.Application.Infrastructure;
using WorldRank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WorldRank.Domain.Exceptions;

namespace WorldRank.Infrastructure.Persistence.Commands.Wallets
{
    public class CreateWalletPersistence : ICreateWalletPersistence
    {
        private readonly WorldRankDbContext _context;
        public CreateWalletPersistence(WorldRankDbContext context)
        {
            _context = context;
        }
        public async Task<int> Persist(Wallet wallet, CancellationToken cancellationToken)
        {
            var foundwallet = await _context.Wallets.FirstOrDefaultAsync(w => w.PlayerId == wallet.PlayerId && w.Currency == wallet.Currency, cancellationToken);
            if (foundwallet is not null)
                throw new DuplicateWalletException(wallet.PlayerId, wallet.Currency);
            var allIds = (await _context.Wallets.Select(p => p.Id).ToListAsync(cancellationToken)).ToHashSet();
            int id;
            do
            {
                id = Random.Shared.Next(1, int.MaxValue);
            }
            while (allIds.Contains(id));
            _context.Add(new Wallet(id, wallet.PlayerId, wallet.Currency, wallet.Balance));
            await _context.SaveChangesAsync(cancellationToken);
            return id;
            
        }
    }
}
