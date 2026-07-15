using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Infrastructure;
using WorldRank.Application.Queries.Players;
using WorldRank.Domain.Entities;
using WorldRank.Domain.Exceptions;

namespace WorldRank.Application.Queries.Wallets
{
    public class GetWalletsByPlayerIdHandler : IRequestHandler<GetWalletsByPlayerIdQuery, List<Wallet>>
    {
        private readonly IGetWalletsByPlayerIdPersistence _getWalletsByPlayerIdPersistence;
        private readonly IGetPlayerByIdPersistence _getPlayerByIdPersistence;

        public GetWalletsByPlayerIdHandler(
            IGetWalletsByPlayerIdPersistence getWalletsByPlayerIdPersistence,
            IGetPlayerByIdPersistence getPlayerByIdPersistence)
        {
            _getWalletsByPlayerIdPersistence = getWalletsByPlayerIdPersistence;
            _getPlayerByIdPersistence = getPlayerByIdPersistence;
        }
        public async Task<List<Wallet>> Handle(GetWalletsByPlayerIdQuery request, CancellationToken cancellationToken)
        {
            var player = await _getPlayerByIdPersistence.Get(request.playerId, cancellationToken);
            if (player is null)
                throw new PlayerNotFoundException(request.playerId);
            var wallets = await _getWalletsByPlayerIdPersistence.Get(request.playerId, cancellationToken);
            return wallets;
        }
    }
}
