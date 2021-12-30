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
        private bool _endGame;
        private int _roundCounter;
        private User _winner;
        public Battle(User player1, User player2)
        {
            _player1 = player1;
            _player2 = player2;
        }
        
        public void Fight()
        {
            while (!_endGame)
            {
                if (_player1.GetRandomCardFromDeck() is Monster && _player2.GetRandomCardFromDeck() is Monster)
                {
                    //Monster fight
                    Console.WriteLine("MonsterFight");
                }
                if (_player1.GetRandomCardFromDeck() is Spell && _player2.GetRandomCardFromDeck() is Spell)
                {
                    //Spell fight
                    Console.WriteLine("SpellFight");
                }
                if (_player1.GetRandomCardFromDeck() is Monster && _player2.GetRandomCardFromDeck() is Spell)
                {
                    //Monster Spell fight
                    Console.WriteLine("MonsterSpellFight");
                }
                if (_player1.GetRandomCardFromDeck() is Spell && _player2.GetRandomCardFromDeck() is Monster)
                {
                    //Spell Monster fight
                    Console.WriteLine("SpellMonsterFight");
                }
                _roundCounter++;
                if (_player1.GetNumberOfCardsInDeck() == 0 || _player2.GetNumberOfCardsInDeck() == 0 || _roundCounter >= 100)
                {
                    _endGame = true;
                }
            }
            if(_roundCounter < 100)
            {
                if (_player1.GetNumberOfCardsInDeck() > 0)
                {
                    _winner = _player1;
                }
                else if(_player2.GetNumberOfCardsInDeck() > 0)
                {
                    _winner = _player2;
                }
                else
                {
                    Console.WriteLine("error while assigning winner");
                }
            }
            else
            {
                _winner = null;
            }
        }
    }
}
