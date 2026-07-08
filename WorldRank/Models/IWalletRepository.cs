using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldRank.Models
{
    internal interface IWalletRepository
    {
         void Add(Wallet wallet, int playerId);
         Dictionary<CurrencyTypes, Wallet> GetByPlayer(int playerId);
    }
}
