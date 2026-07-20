using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Domain.Entities;
using WorldRank.Domain.Enums;
using WorldRank.Domain.Exceptions;

namespace WorldRank.Tests.Entities
{
    public class WalletTests
    {
        [Fact(DisplayName = "Verify Input is configured exactly as given in new Wallet corresponding fields")]
        public void Constructor_ProperInput_InitializedCorrectly()
        {
            // Arrange
            int id = 0;
            int playerId = 123;
            decimal balance = 100.3m;
            Currency currency = Currency.EUR;
            bool isBlocked = true;

            // Act 
            var wallet = new Wallet(id, playerId, currency, balance, isBlocked);

            // Assert
            Assert.Equal(id, wallet.Id);
            Assert.Equal(playerId, wallet.PlayerId);
            Assert.Equal(balance, wallet.Balance);
            Assert.Equal(currency, wallet.Currency);
            Assert.Equal(isBlocked, wallet.IsBlocked);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void Deposit_NonPositiveAmount_ThrowsInvalidAmountException(int amount)
        {
            // Arrange
            var wallet = new Wallet(0, 0, Currency.EUR, 30.3m, false);

            // Act & Assert
            Assert.Throws<InvalidAmountException>(() => wallet.Deposit(amount));
        }






    }
    }
