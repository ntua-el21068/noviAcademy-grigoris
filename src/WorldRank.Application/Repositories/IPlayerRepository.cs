using WorldRank.Domain.Entities;

namespace WorldRank.Application.Repositories;

public interface IPlayerRepository
{
	Task AddPlayerAsync(Player player, CancellationToken cancellationToken);

	Task<IEnumerable<Player>>  GetAllPlayersAsync(CancellationToken cancellationToken);

	Task DeletePlayerAsync(int playerId, CancellationToken cancellationToken);

	Task<Player?> FindPlayerAsync(int playerId, CancellationToken cancellationToken);

	Task<IEnumerable<IGrouping<int, Player>>> GroupPlayersByScoreAsync(CancellationToken cancellationToken);
}
