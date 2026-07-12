using Microsoft.Extensions.Logging;
using WorldRank.Application.Interfaces;
using WorldRank.Domain.Entities;
using WorldRank.Infrastructure.Persistence;

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

    public void AddPlayer(Player player)
    {
        _context.Players.Add(player);
        _context.SaveChanges();
        _logger.LogInformation("Player {PlayerId} ({Name}) added with score {Score}", player.Id, player.Name, player.Score);
    }

    public IEnumerable<Player> GetAllPlayers() => _context.Players.ToList();

    public void DeletePlayer(int playerId)
    {
        var player = _context.Players.SingleOrDefault(p => p.Id == playerId);
        if (player is null)
        {
            _logger.LogWarning("Delete skipped: player {PlayerId} not found", playerId);
            return;
        }

        _context.Players.Remove(player);
        _context.SaveChanges();
        _logger.LogInformation("Player {PlayerId} deleted", playerId);
    }

    public Player? FindPlayer(int playerId) => _context.Players.SingleOrDefault(p => p.Id == playerId);

    public IEnumerable<IGrouping<int, Player>> GroupPlayersByScore()
    {
        return _context.Players
            .ToList()
            .GroupBy(p => p.Score)
            .OrderByDescending(g => g.Key);
    }
}
