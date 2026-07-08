using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WorldRank.Models
{
    public class InMemoryPlayerRepository: IPlayerRepository
    {
        //In order to achieve minimum complexity for delete and find player operations we will use a dictionary for caching
        private Dictionary<int, Player> _allPlayers=new();

        public Player AddPlayer(Player p)
        {
            if(p==null)throw new ArgumentNullException(nameof(p));
            _allPlayers.Add(p.Id, p);
            return p;
        }
        public Player? FindPlayer(int playerId)
        {
            if(_allPlayers.TryGetValue(playerId, out Player? foundPLayer)){
                return foundPLayer;
            }
            return null;
        }

        public void DeletePlayer(int playerId)
        {
            if (!_allPlayers.Remove(playerId))
            {
                throw new KeyNotFoundException($"Player {playerId} does not exist.");
            }
        }
        public IEnumerable<IGrouping<int, Player>> GroupPlayersByScore
        {
            get
            {
                if(_allPlayers == null){
                throw new NullReferenceException(nameof(_allPlayers));
            }
            return _allPlayers.Values.GroupBy( p => p.Score );
            }
        }


   // these methods are needed for the updated console version, as the previous list of players is now a custom InMemoryPlayerReposritory of players
        public int CountPlayers()
        {
            return(_allPlayers.Count);
        }

        public IEnumerable<Player> GetAll()
        {
            return _allPlayers.Values;
        }

        public Player? FindPlayerByName(string name)
        {
            return _allPlayers.Values.FirstOrDefault(p => p.Name == name);
        }



    }
}