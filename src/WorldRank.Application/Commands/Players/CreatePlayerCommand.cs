using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace WorldRank.Application.Commands.Players
{
    public record CreatePlayerCommand(string Name, int Score) : IRequest<int>;
    
    
}
