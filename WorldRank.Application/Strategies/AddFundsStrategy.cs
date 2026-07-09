using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldRank.Domain;

namespace WorldRank.Application.Strategies
{
    public class AddFundsStrategy : IFundsStrategy
    {
        public FundsOperation Operation => FundsOperation.Add;
        public void Execute(Wallet wallet, decimal amount) => wallet.Deposit(amount);
    }
}