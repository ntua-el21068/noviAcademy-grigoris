using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldRank.Exceptions
{
    public class InsufficientFundsException : WalletException
    {
        public int WalletId {get;}

        public InsufficientFundsException(int walletId)
        : base($"Wallet with ID {walletId} tried to become negative (forbidden)")
        {
            WalletId = walletId;
        }
    }
}