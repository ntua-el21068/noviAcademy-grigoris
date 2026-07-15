
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Infrastructure
{
    public interface IGetPlayerByIdPersistence
    {
        Task<Player?> Get(int id, CancellationToken cancellationToken);
    }
}
