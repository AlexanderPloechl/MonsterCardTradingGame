using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class Stack
    {
        int NumberOfCardsStack;
        public List<ICard> cards;

        public Stack()
        {
            NumberOfCardsStack = 0;
        }
        void AddCardsFromPackageToStacks(List<ICard> PackageToOpen)
        {
            cards.AddRange(PackageToOpen);
        }
        void RemoveCardFromStack()
        {

        }
        void AddCardToStack()
        {

        }
    }
}
