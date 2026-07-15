using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Infrastructure;
using WorldRank.Domain.Exceptions;

namespace WorldRank.Application.Commands.Wallets
{
    public class DepositToWalletHandler : IRequestHandler<DepositToWalletCommand, decimal>
    {
        private readonly IDepositToWalletPersistence _depositWalletPersistence;
        //private readonly IGetWalletsByPlayerIdPersistence _getWalletsByPlayerIdPersistence;

        public DepositToWalletHandler(IDepositToWalletPersistence depositWalletPersistence)
        {
            _depositWalletPersistence = depositWalletPersistence;
        }
        public async Task<decimal> Handle(DepositToWalletCommand request, CancellationToken cancellationToken)
        {
            if (request.amount <= 0)
                throw new InvalidAmountException(request.amount);
            //Wallet existence validation logic
            decimal balance = await _depositWalletPersistence.Persist(request.playerId, request.amount, request.currency, cancellationToken);
            return balance;

        }
    }
}
