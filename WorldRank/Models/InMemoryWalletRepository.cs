using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldRank.Exceptions;

namespace WorldRank.Models
{
    public class InMemoryWalletRepository: IWalletRepository
    {
        private Dictionary<int, Dictionary<CurrencyTypes, Wallet>> _allWallets =  new (); //No need for custom constructor code snippet, we can rely on the default constructor

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
                throw new DuplicateWalletException(playerId, wallet.Currency);
            }

            playerWallets.Add(wallet.Currency, wallet);
        }

        public Dictionary<CurrencyTypes, Wallet> GetByPlayer(int playerId)
        {
           if (_allWallets.TryGetValue(playerId, out var playerWallets))
    {
        return playerWallets;
    }
    //empty dict in case of no wallet entity assigned to the player
    return new Dictionary<CurrencyTypes, Wallet>();
        }
    }
}
