using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldRank.Models;

namespace WorldRank.Exceptions
{
    public class WalletException: Exception
    {

        //Default
        public WalletException()
        :base("Forbidden Wallet operation")
        {}

        //Constructor tht accepts walletId value

        public WalletException(int walletId)
        :base($"Forbidden operation for Wallet with ID {walletId}")
        {}

        //Custom
        public WalletException(string message)
        :base(message)
        {}

        //Inner exception 
        public WalletException(string message, Exception innerException)
        :base(message, innerException)
        {}
    }
}