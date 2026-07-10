using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldRank.Domain;

namespace WorldRank.Application.Strategies
{
    public class SubtractFundsStrategy : IFundsStrategy
    {
        public FundsOperation Operation => FundsOperation.Subtract;
        public void Execute(Wallet wallet, decimal amount) => wallet.Withdraw(amount);
    }
}