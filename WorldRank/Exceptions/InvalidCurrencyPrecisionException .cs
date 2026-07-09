using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldRank.Exceptions
{
    public class InvalidCurrencyPrecisionException : WalletException
    {
        public decimal AttemptedAmount {get;}

        public InvalidCurrencyPrecisionException(decimal attemptedAmount)
        : base($"The amount {attemptedAmount} has an invalid number of decimal places for this operation")
        {
            AttemptedAmount = attemptedAmount;
        }
    }
}