using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Infrastructure;
using WorldRank.Domain.Enums;
using WorldRank.Domain.Exceptions;

namespace WorldRank.Infrastructure.Persistence.Commands.Wallets
{
    public class DepositToWalletPersistence : IDepositToWalletPersistence
    {
        private readonly WorldRankDbContext _context;

        public DepositToWalletPersistence(WorldRankDbContext context)
        {
            _context = context;
        }
        
        public async Task<decimal> Persist(int playerId, decimal amount, Currency currency, CancellationToken cancellationToken)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.PlayerId == playerId && w.Currency == currency, cancellationToken);
            if (wallet is null)
                throw new WalletNotFoundException(playerId, currency);
            wallet.Deposit(amount);
            await _context.SaveChangesAsync(cancellationToken);
            return wallet.Balance;
            
        }
    }
}
