using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldRank.Models;

namespace WorldRank.Exceptions
{
    public class DuplicateWalletException : WalletException
    {
        public int PlayerId {get;}
        public CurrencyTypes Currency {get;}

        public DuplicateWalletException(int playerId, CurrencyTypes currency)
        : base($"Operation failed: Player {playerId} already owns a {currency} wallet")
        {
            PlayerId = playerId;
            Currency = currency;

        }
    }
}