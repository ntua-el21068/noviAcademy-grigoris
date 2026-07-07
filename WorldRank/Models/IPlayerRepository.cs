using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

///- An `IPlayerRepository` interface with `AddPlayer(Player p)` , `FindPlayer(int playerId)`, `DeletePlayer(int playerId)`,  (optional) `GroupPlayersByScore`

namespace WorldRank.Models
{
    public interface IPlayerRepository
    {
        Player AddPlayer(Player p);
        Player? FindPlayer(int playerId);
        void DeletePlayer(int playerId);
        IEnumerable<IGrouping<int, Player>> GroupPlayersByScore {get;}
        
    }
}