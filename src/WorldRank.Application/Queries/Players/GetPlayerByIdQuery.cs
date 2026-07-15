using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Queries.Players
{
    public record GetPlayerByIdQuery(int Id) : IRequest<Player?>;
    
    
}
