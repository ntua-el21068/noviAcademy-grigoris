using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Infrastructure;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Queries.Players
{
    public class GetPlayerByIdHandler : IRequestHandler<GetPlayerByIdQuery, Player?>
    {
        private readonly IGetPlayerByIdPersistence _getPlayerByIdPersistence;

        public GetPlayerByIdHandler(IGetPlayerByIdPersistence getPlayerByIdPersistence)
        {
            _getPlayerByIdPersistence = getPlayerByIdPersistence;
        }

        public async Task<Player?> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            var Player = await _getPlayerByIdPersistence.Get(request.Id , cancellationToken);
            return Player;
        }
    }
}
