using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Infrastructure
{
    public interface ICreatePlayerPersistence
    {
        Task<int> Persist(Player player, CancellationToken cancellationToken);
    }
}
