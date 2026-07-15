using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Queries.Players
{
    public record GetAllPlayersQuery() : IRequest<List<Player>>;
    

    
}
