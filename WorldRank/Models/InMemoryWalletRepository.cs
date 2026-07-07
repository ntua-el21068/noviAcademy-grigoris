using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldRank.Models
{
    public class InMemoryWalletRepository: IWalletRepository
    {
        private Dictionary<int, Dictionary<CurrencyTypes, Wallet>> _allWallets = 
            new Dictionary<int, Dictionary<CurrencyTypes, Wallet>>();
        public void Add(Wallet wallet, int playerId)
        {
            if (wallet == null) throw new ArgumentNullException(nameof(wallet));
            if (!_allWallets.ContainsKey(playerId))
            {//Users first wallet
                _allWallets[playerId] = new Dictionary<CurrencyTypes, Wallet>();
            }

            var playerWallets = _allWallets[playerId];

            if (playerWallets.ContainsKey(wallet.Currency))
            {
                throw new InvalidOperationException("You can only have one wallet for each currency.");
            }

            playerWallets.Add(wallet.Currency, wallet);
        }

        public Dictionary<CurrencyTypes, Wallet> GetByPlayer(int playerId)
        {


            return new Dictionary<CurrencyTypes, Wallet>();
        }
    }
}
