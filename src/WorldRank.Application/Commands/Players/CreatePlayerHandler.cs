using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using WorldRank.Application.Infrastructure;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Commands.Players
{
    public class CreatePlayerHandler : IRequestHandler<CreatePlayerCommand, int>
    {
        private ICreatePlayerPersistence _createPlayerPersistence;

        public CreatePlayerHandler(ICreatePlayerPersistence createPlayerPersistence)
        {
            _createPlayerPersistence = createPlayerPersistence;
        }
        public async Task<int> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
        {
            var player = new Player(0, command.Name, command.Score);
            int id = await _createPlayerPersistence.Persist(player, cancellationToken);
            return id;
        }
    }
}
