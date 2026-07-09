using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldRank.Domain;

namespace WorldRank.Application.Strategies
{
    public class ForceSubtractFundsStrategy
    {
        public FundsOperation Operation => FundsOperation.ForceSubtract;
        public void Execute(Wallet wallet, decimal amount) => wallet.ForceWithdraw(amount);
    }
}