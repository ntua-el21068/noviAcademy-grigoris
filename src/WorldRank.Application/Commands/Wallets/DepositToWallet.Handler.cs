using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Caching;
using WorldRank.Application.Infrastructure;
using WorldRank.Domain.Exceptions;

namespace WorldRank.Application.Commands.Wallets
{
    public class DepositToWalletHandler : IRequestHandler<DepositToWalletCommand, decimal>
    {
        private readonly IDepositToWalletPersistence _depositWalletPersistence;
        private readonly IGetWalletsByPlayerIdPersistence _getWalletsByPlayerIdPersistence;
        private readonly ICache _cache;

        public DepositToWalletHandler(
            IDepositToWalletPersistence depositWalletPersistence, 
            IGetWalletsByPlayerIdPersistence getWalletsByPlayerIdPersistence,
            ICache cache
            )
        {
            _depositWalletPersistence = depositWalletPersistence;
            _getWalletsByPlayerIdPersistence = getWalletsByPlayerIdPersistence;
            _cache = cache;
        }
        public async Task<decimal> Handle(DepositToWalletCommand request, CancellationToken cancellationToken)
        {
            if (request.amount <= 0)
                throw new InvalidAmountException(request.amount);
            var wallets = await _getWalletsByPlayerIdPersistence.Get(request.playerId, cancellationToken);
            bool exist = wallets.Any(w => w.Currency == request.currency);
            if (!exist)
                throw new WalletNotFoundException(request.playerId, request.currency);

                decimal balance = await _depositWalletPersistence.Persist(request.playerId, request.amount, request.currency, cancellationToken);
            _cache.Remove($"WalletsByPlayerId_{request.playerId}");
            return balance;

        }
    }
}
