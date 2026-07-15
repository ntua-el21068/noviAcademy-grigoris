using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Infrastructure
{
    public interface IGetAllPlayersPersistence
    {
        Task<List<Player>> Get(CancellationToken cancellationToken);
    }
}
