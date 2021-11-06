using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class Battle
    {
        private Dictionary<HashSet<ElementType>, ElementType> SuperiorElement = new Dictionary<HashSet<ElementType>, ElementType> { { new HashSet<ElementType> { ElementType.Fire, ElementType.Water }, ElementType.Water }, { new HashSet<ElementType> { ElementType.Normal, ElementType.Fire }, ElementType.Fire }, { new HashSet<ElementType> { ElementType.Normal, ElementType.Water }, ElementType.Normal } };
        private User _player1;
        private User _player2;
        public Battle(User player1, User player2)
        {
            //step 1 get random card form both players
            //step 2 check if spell or monster
            //step 3 call correct function
            //
        }
        
    }
}
