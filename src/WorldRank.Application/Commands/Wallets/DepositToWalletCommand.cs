using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Domain.Enums;

namespace WorldRank.Application.Commands.Wallets
{
    public record DepositToWalletCommand(int playerId, decimal amount, Currency currency) : IRequest<decimal>;
    
}
