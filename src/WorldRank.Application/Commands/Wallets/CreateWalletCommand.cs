using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Domain.Enums;

namespace WorldRank.Application.Commands.Wallets
{
    public record CreateWalletCommand(int playerId, Currency currency, decimal balance) : IRequest<int>;
   
}
