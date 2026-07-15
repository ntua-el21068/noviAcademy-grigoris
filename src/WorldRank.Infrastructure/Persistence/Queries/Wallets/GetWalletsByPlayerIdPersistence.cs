using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Infrastructure;
using WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Persistence.Queries.Wallets
{
    public class GetWalletsByPlayerIdPersistence : IGetWalletsByPlayerIdPersistence
    {
        private readonly WorldRankDbContext _context;
        public GetWalletsByPlayerIdPersistence(WorldRankDbContext context)
        {
            _context = context;
        }

        public async Task<List<Wallet>> Get(int playerId, CancellationToken cancellationToken)
        {
            var wallets = await _context.Wallets.Where(w => w.PlayerId == playerId).ToListAsync(cancellationToken);
            return wallets;
        }
    }
}
