using System;
using Moq;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Caching;
using WorldRank.Application.Repositories;
using WorldRank.Application.Services;
using WorldRank.Domain.Entities;

namespace WorldRank.Tests.Services
{
    public class PlayerServiceTest
    {
        private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();
        private readonly Mock<ICache> _cacheMock = new();
        private PlayerService _sut;

        public PlayerServiceTest()
        {
            _sut = new(_playerRepositoryMock.Object, _cacheMock.Object);
        }

       

        [Fact]
        public async Task GetPlayerById_IdExists_ReturnsPlayer()
        {
            // Arrange
            int id = 1;
            var expectedPlayer = new Player(id, "Maria");

            _cacheMock
                .Setup(c => c.TryGet(It.IsAny<string>(), out It.Ref<Player?>.IsAny))
                .Returns(false);

            _playerRepositoryMock
                .Setup(r => r.FindPlayerAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedPlayer);

            // Act
            var player = await _sut.GetPlayerById(id, CancellationToken.None);

            // Assert
            Assert.NotNull(player);
            Assert.Equal("Maria", player.Name);
            Assert.Equal(id, player.Id);
        }



    }
}
