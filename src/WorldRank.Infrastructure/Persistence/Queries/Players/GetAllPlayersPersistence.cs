using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Infrastructure;
using WorldRank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WorldRank.Infrastructure.Persistence.Queries.Players
{
    public class GetAllPlayersPersistence : IGetAllPlayersPersistence
    {
        private readonly WorldRankDbContext _context;

        public GetAllPlayersPersistence(WorldRankDbContext context)
        {
            _context = context;
        }
        public async Task<List<Player>> Get(CancellationToken cancellationToken)
        {
            return await _context.Players.ToListAsync(cancellationToken);
        }
    }
    
}
