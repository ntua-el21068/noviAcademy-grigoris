using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldRank.Domain;

namespace WorldRank.Application.Strategies
{
    public interface IFundsStrategy
    {
        FundsOperation Operation {get;}
        void Execute(Wallet wallet, decimal amount);
    }
}