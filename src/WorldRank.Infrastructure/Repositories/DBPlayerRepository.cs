using Microsoft.Extensions.Logging;
using WorldRank.Application.Interfaces;
using WorldRank.Domain.Entities;
using WorldRank.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace WorldRank.Infrastructure.Repositories;

public class DBPlayerRepository : IPlayerRepository
{
    private readonly WorldRankDbContext _context;
    private readonly ILogger<DBPlayerRepository> _logger;

    public DBPlayerRepository(WorldRankDbContext context, ILogger<DBPlayerRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddPlayerAsync(Player player, CancellationToken cancellationToken)
    {
        _context.Players.Add(player);
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Player {PlayerId} ({Name}) added with score {Score}", player.Id, player.Name, player.Score);
    }

    public async Task<IEnumerable<Player>> GetAllPlayersAsync(CancellationToken cancellationToken)
    {
        var players = await _context.Players.ToListAsync(cancellationToken);
        return players;
    }

    public async Task DeletePlayerAsync(int playerId, CancellationToken cancellationToken)
    {
        var player = await _context.Players.SingleOrDefaultAsync(p => p.Id == playerId, cancellationToken);
        if (player is null)
        {
            _logger.LogWarning("Delete skipped: player {PlayerId} not found", playerId);
            return;
        }

        _context.Players.Remove(player);
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Player {PlayerId} deleted", playerId);
    }

    public async Task<Player?> FindPlayerAsync(int playerId, CancellationToken cancellationToken)
    {
        var player = await _context.Players.SingleOrDefaultAsync(p => p.Id == playerId, cancellationToken);
        if (player is null)
        {
            _logger.LogWarning("Find skipped: player {PlayerId} not found", playerId);
            return null;
        }
        return player;
    }

    public async Task<IEnumerable<IGrouping<int, Player>>> GroupPlayersByScoreAsync(CancellationToken cancellationToken)
    {
        var players = await _context.Players.ToListAsync();
        var grouped = players.GroupBy(p=>p.Score).OrderByDescending(g => g.Key);
        return grouped;
    }
}
