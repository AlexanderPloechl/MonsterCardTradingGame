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
        private const int DAMAGEMANIPULATOR = 2;
        public Battle(User player1, User player2)
        {
            _player1 = player1;
            _player2 = player2;
        }

        private User MonsterVsMonster(ICard player1sCard, ICard player2sCard)
        {
            //todo special stuff (knights cant swim)
            if(player1sCard.damage > player2sCard.damage)
            {
                return _player1;
            }
            else if(player1sCard.damage < player2sCard.damage)
            {
                return _player2;
            }
            return null;
        }

        private User MonsterVsSpell(ICard player1sCard, ICard player2sCard)
        {
            
        }

        private User SpellVsSpell(ICard player1sCard, ICard player2sCard)
        {
            if(player1sCard.elementType != player2sCard.elementType)
            {
                ElementType superiorElement;
                SuperiorElement.TryGetValue(new HashSet<ElementType> { player1sCard.elementType, player1sCard.elementType }, out superiorElement);
                if (player1sCard.elementType == superiorElement)
                {
                    //player1sCard superior
                    if (player1sCard.damage * DAMAGEMANIPULATOR > player2sCard.damage / DAMAGEMANIPULATOR)
                    {
                        return _player1;
                    }
                    else if (player1sCard.damage * DAMAGEMANIPULATOR < player2sCard.damage / DAMAGEMANIPULATOR)
                    {
                        return _player2;
                    }
                    return null;
                }
                else if (player2sCard.elementType == superiorElement)
                {
                    //player2sCard superior
                    if (player1sCard.damage / DAMAGEMANIPULATOR > player2sCard.damage * DAMAGEMANIPULATOR)
                    {
                        return _player1;
                    }
                    else if (player1sCard.damage / DAMAGEMANIPULATOR < player2sCard.damage * DAMAGEMANIPULATOR)
                    {
                        return _player2;
                    }
                    return null;
                }
                else
                {
                    Console.WriteLine("error while choosing multiplier");
                    return null;
                }
            }
            else
            {
                //same element type
                if (player1sCard.damage > player2sCard.damage)
                {
                    return _player1;
                }
                else if (player1sCard.damage < player2sCard.damage)
                {
                    return _player2;
                }
                return null;
            }
        }

        public void Fight()
        {
            ICard player1CurrentCard;
            ICard player2CurrentCard;
            while (!_endGame)
            {
                player1CurrentCard = _player1.GetRandomCardFromDeck();
                player2CurrentCard = _player2.GetRandomCardFromDeck();
                if (player1CurrentCard is Monster && player2CurrentCard is Monster)
                {
                    //Monster fight
                    Console.WriteLine("MonsterFight");
                    _winner = MonsterVsMonster(player1CurrentCard, player2CurrentCard);
                }
                else if (player1CurrentCard is Spell && player2CurrentCard is Spell)
                {
                    //Spell fight
                    Console.WriteLine("SpellFight");
                }
                else if (player1CurrentCard is Monster && player2CurrentCard is Spell)
                {
                    //Monster Spell fight
                    Console.WriteLine("MonsterSpellFight");
                }
                else if (player1CurrentCard is Spell && player2CurrentCard is Monster)
                {
                    //Spell Monster fight
                    Console.WriteLine("SpellMonsterFight");
                }
                else
                {
                    Console.WriteLine("error while defining card type");
                }
                _roundCounter++;
                if(_winner == _player1)
                {
                    //player one gets card of player two
                }
                else if(_winner == _player2)
                {
                    //player two gets card of player one
                }
                else
                {
                    //draw
                }
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
            if(_winner != null)
            {
                GiveRewards(_winner);
            }
        }

        private void GiveRewards(User winner)
        {
            //elo und so
        }
    }
}
