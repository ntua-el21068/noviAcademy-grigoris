using WorldRank.Application.Caching;
using WorldRank.Application.Repositories;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Services;

public class PlayerService
{
	private readonly IPlayerRepository _playerRepository;
	private ICache _cache;

	public PlayerService(IPlayerRepository playerRepository, ICache cache)
	{
		_playerRepository = playerRepository;
		_cache = cache;
	}

	public async Task<IEnumerable<Player>> GetAllPlayers(CancellationToken cancellationToken)
	{
		if (_cache.TryGet("AllPlayersKey", out IEnumerable<Player>? cached) && cached is not null)
		{
			return cached;
		}
		var players = await _playerRepository.GetAllPlayersAsync(cancellationToken);

		_cache.Set("AllPlayersKey", players, TimeSpan.FromSeconds(60));
		return players;
	}

	public async Task<Player?> GetPlayerById(int playerId, CancellationToken cancellationToken)
	{
		var cacheKey = $"Player_{playerId}";
        if (_cache.TryGet(cacheKey, out Player? cached) && cached is not null)
        {
            return cached;
        }

        var player = await _playerRepository.FindPlayerAsync(playerId, cancellationToken);
		if(player is not null)
		_cache.Set(cacheKey, player, TimeSpan.FromSeconds(60));
		return player;
	}

	public async Task<Player> AddPlayer(string name, int score, CancellationToken cancellationToken)
	{
		var player = new Player(await(GeneratePlayerIdAsync(cancellationToken)), name);
		player.AddScore(score);
		await _playerRepository.AddPlayerAsync(player, cancellationToken);
		_cache.Remove("AllPlayersKey");
		return player;
	}

    //public async Task<Player> AddPlayer(int id, string name, int score, CancellationToken cancellationToken)
    //{
    //    var player = new Player(id, name);
    //    player.AddScore(score);
    //    await _playerRepository.AddPlayerAsync(player, cancellationToken);
    //    _cache.Remove("AllPlayersKey");
    //    return player;
    //}

    //public void ListPlayers()
    //{
    //	var all = _playerRepository.GetAllPlayers().ToList();

    //	if (all.Count == 0)
    //	{
    //		Console.WriteLine("No players registered.");
    //		return;
    //	}

    //		foreach (var player in all)
    //			Console.WriteLine(player);
    //	}

    // public void ListPlayersByScore()
    // {
    // 	var groups = _playerRepository.GroupPlayersByScore().ToList();

    // 	if (groups.Count == 0)
    // 	{
    // 		Console.WriteLine("No players registered.");
    // 		return;
    // 	}

    // 	foreach (var group in groups)
    // 	{
    // 		Console.WriteLine($"Score {group.Key}:");
    // 		foreach (var player in group)
    // 			Console.WriteLine($"  {player}");
    // 	}
    // }

    // public void FindPlayerByName()
    // {
    // 	Console.Write("Search by name: ");
    // 	var term = Console.ReadLine() ?? string.Empty;

    // 	var player = _playerRepository.GetAllPlayers()
    // 		.FirstOrDefault(p => p.Name.Equals(term, StringComparison.OrdinalIgnoreCase));

    // 	Console.WriteLine(player is null ? "No player found." : player.ToString());
    // }

    // public void FindPlayerById()
    // {
    // 	var playerId = Prompts.PromptPlayerId();
    // 	if (playerId is null)
    // 		return;

    // 	var player = _playerRepository.FindPlayer(playerId.Value);

    // 	Console.WriteLine(player is null ? "No player found." : player.ToString());
    // }



    // public void DeletePlayer()
    // {
    // 	var playerId = Prompts.PromptPlayerId();
    // 	if (playerId is null)
    // 		return;

    // 	_playerRepository.DeletePlayer(playerId.Value);
    // 	Console.WriteLine("Player deleted (if it existed).");
    // }

    // Generates a random, unique player id (avoids collisions with already-registered players).
    private async Task<int> GeneratePlayerIdAsync(CancellationToken cancellationToken)
	{
		var existingIds = (await _playerRepository.GetAllPlayersAsync(cancellationToken)).Select(p => p.Id).ToHashSet();

		int id;
		do
		{
			id = Random.Shared.Next(1, int.MaxValue);
		}
		while (existingIds.Contains(id));

		return id;
	}
}
