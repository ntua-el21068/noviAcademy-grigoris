using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Caching;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Queries.Players
{
    public record GetAllPlayersQuery() : IRequest<List<Player>>, ICacheableQuery
    {
        public string CacheKey => "GetAllPlayers";
    }



}
