using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class Deck
    {
        readonly int NumberOfCardsInDeck = 4;
        List<ICard> cards;

        public Deck(List<ICard> stack)
        {
            for(int i = 0; i < NumberOfCardsInDeck; i++)
            {
                AddCardToDeck(stack);
            }
        }
        void AddCardToDeck(List<ICard> stack)
        {
            //show cards in stack stack.showCards
            //choose card
            //remove card from stack and add to deck
        }
        void RemoveCardFromDeck()
        {

        }
        void ReplaceCardInDeck(List<ICard> stack)
        {
            RemoveCardFromDeck();
            AddCardToDeck(stack);
        }
    }
}
