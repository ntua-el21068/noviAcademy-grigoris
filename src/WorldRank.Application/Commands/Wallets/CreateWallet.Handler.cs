using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Caching;
using WorldRank.Application.Infrastructure;
using WorldRank.Domain.Entities;
using WorldRank.Domain.Exceptions;

namespace WorldRank.Application.Commands.Wallets
{
    public class CreateWalletHandler : IRequestHandler<CreateWalletCommand, int>
    {
        private readonly ICreateWalletPersistence _createWalletPersistence;
        private readonly IGetPlayerByIdPersistence _getPlayerByIdPersistence;
        private readonly ICache _cache;
        public CreateWalletHandler(
            ICreateWalletPersistence createWalletPersistence,
            IGetPlayerByIdPersistence getPlayerByIdPersistence,
            ICache cache
            )
        {
            _createWalletPersistence = createWalletPersistence;
            _getPlayerByIdPersistence = getPlayerByIdPersistence;
            _cache = cache;
        }
        
        

        public async Task<int> Handle(CreateWalletCommand command, CancellationToken cancellationToken)
        {
            if(command.balance < 0)
            {
                throw new InvalidAmountException(command.balance);
            }
            var player = await _getPlayerByIdPersistence.Get(command.playerId, cancellationToken);
            if(player is null)
                throw new PlayerNotFoundException(command.playerId);
            var wallet = new Wallet(0, command.playerId, command.currency, command.balance);
            var id = await _createWalletPersistence.Persist(wallet, cancellationToken);
            _cache.Remove($"WalletsByPlayer_{command.playerId}");
            return id;
        }
    }
}
