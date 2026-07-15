using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Queries.Wallets
{
    public record GetWalletsByPlayerIdQuery(int playerId) : IRequest<List<Wallet>>;
    
}
