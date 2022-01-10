using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class Deck
    {
        public readonly int NumberOfCardsInDeck = 4;
        public List<ICard> cards;

        public Deck()
        {
            cards = new List<ICard>();
        }

        public Deck(List<ICard> stack)
        {
            //for(int i = 0; i < NumberOfCardsInDeck; i++)
            //{
            //    AddCardToDeck(stack);
            //}
        }
        public void FillDeck(List<ICard> _cards)
        {
            cards = _cards;
        }
        void RemoveCardFromDeck()
        {

        }
        void ReplaceCardInDeck(List<ICard> stack)
        {
            //RemoveCardFromDeck();
            //AddCardToDeck(stack);
        }
    }
}
