using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Infrastructure;
using WorldRank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WorldRank.Infrastructure.Persistence.Commands.Players
{
    public class CreatePlayerPersistence : ICreatePlayerPersistence
    {

        private readonly WorldRankDbContext _context;

        public CreatePlayerPersistence(WorldRankDbContext worldRankDbContext)
        {
            _context = worldRankDbContext;
        }

        public async Task<int> Persist(Player player, CancellationToken cancellationToken)
        {
            var allIds = (await _context.Players.Select(p => p.Id).ToListAsync(cancellationToken)).ToHashSet();
            int id;
            do
            {
                id = Random.Shared.Next(1, int.MaxValue);
            }
            while (allIds.Contains(id));
            _context.Add(new Player(id, player.Name, player.Score));
            await _context.SaveChangesAsync(cancellationToken);
            return id;

        }
    }
}
