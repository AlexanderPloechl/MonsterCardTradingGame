using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class Stack
    {
        public List<ICard> cards;

        public Stack()
        {
            cards = new List<ICard>();
        }
        void AddCardsFromPackageToStacks(List<ICard> PackageToOpen)
        {
            cards.AddRange(PackageToOpen);
        }
    }
}
