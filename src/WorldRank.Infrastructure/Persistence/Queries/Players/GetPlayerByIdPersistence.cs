using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Infrastructure;
using WorldRank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WorldRank.Infrastructure.Persistence.Queries.Players
{
    public class GetPlayerByIdPersistence : IGetPlayerByIdPersistence
    {
        private readonly WorldRankDbContext _context;
        
        public GetPlayerByIdPersistence(WorldRankDbContext context)
        {
            _context = context;
        }

        public async Task<Player?> Get(int id, CancellationToken cancellationToken)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p  => p.Id == id, cancellationToken);
            return player;
        }
    }
}
