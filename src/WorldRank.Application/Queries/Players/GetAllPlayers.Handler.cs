using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Infrastructure;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Queries.Players
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayersQuery, List<Player>>
    {
        IGetAllPlayersPersistence _getAllPlayersPersistence;

        public GetAllPlayersHandler(IGetAllPlayersPersistence getAllPlayersPersistence)
        {
            _getAllPlayersPersistence = getAllPlayersPersistence;
        }

        public async Task<List<Player>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
        {
            return await  _getAllPlayersPersistence.Get( cancellationToken);
        }
    }
}
