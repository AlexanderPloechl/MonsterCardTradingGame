using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class Battle
    {
        private Dictionary<HashSet<ElementType>, ElementType> SuperiorElement = new Dictionary<HashSet<ElementType>, ElementType>(HashSet<ElementType>.CreateSetComparer()) { { new HashSet<ElementType> { ElementType.Fire, ElementType.Water }, ElementType.Water }, { new HashSet<ElementType> { ElementType.Normal, ElementType.Fire }, ElementType.Fire }, { new HashSet<ElementType> { ElementType.Normal, ElementType.Water }, ElementType.Normal } };
        private Dictionary<MonsterType, HashSet<MonsterType>> MonstersWeaknessesAgainstMonsters = new Dictionary<MonsterType, HashSet<MonsterType>> { { MonsterType.Goblin, new HashSet<MonsterType> { MonsterType.Dragon } }, { MonsterType.Ork, new HashSet<MonsterType> { MonsterType.Wizard } }, { MonsterType.Dragon, new HashSet<MonsterType> { MonsterType.Fireelf } } };
        private Dictionary<MonsterType, HashSet<ElementType>> MonstersWeaknessesAgainstSpells = new Dictionary<MonsterType, HashSet<ElementType>> { { MonsterType.Knight, new HashSet<ElementType> { ElementType.Water } } };
        private Dictionary<ElementType, HashSet<MonsterType>> SpellsWeaknessesAgainstMonsters = new Dictionary<ElementType, HashSet<MonsterType>> { { ElementType.Fire , new HashSet<MonsterType> { MonsterType.Kraken } }, { ElementType.Water , new HashSet<MonsterType> { MonsterType.Kraken } }, { ElementType.Normal , new HashSet<MonsterType> { MonsterType.Kraken } } };
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
            Monster player1sMonster = (Monster)player1sCard;
            HashSet<MonsterType> weaknessPlayer1sMonster;// = new HashSet<MonsterType>();
            MonstersWeaknessesAgainstMonsters.TryGetValue(player1sMonster.monsterType, out weaknessPlayer1sMonster);
            Monster player2sMonster = (Monster)player2sCard;
            HashSet<MonsterType> weaknessPlayer2sMonster;// = new HashSet<MonsterType>();
            MonstersWeaknessesAgainstMonsters.TryGetValue(player2sMonster.monsterType, out weaknessPlayer2sMonster);
            if(weaknessPlayer1sMonster == null)
            {
                //create empty hashset in case there is no weakness, to enable "Contains"
                weaknessPlayer1sMonster = new HashSet<MonsterType>();
            }
            if(weaknessPlayer2sMonster == null)
            {
                //create empty hashset in case there is no weakness, to enable "Contains"
                weaknessPlayer2sMonster = new HashSet<MonsterType>();
            }
            if (weaknessPlayer1sMonster.Contains(player2sMonster.monsterType) && !weaknessPlayer2sMonster.Contains(player1sMonster.monsterType))
            {
                //player1 weak against player2 and player2 not weak against player1
                Console.WriteLine("\n --------------");
                Console.WriteLine($" {player1sMonster.name} is weak against {player2sMonster.name} therefor ...");
                return _player2;
            }
            else if(weaknessPlayer2sMonster.Contains(player1sMonster.monsterType) && !weaknessPlayer1sMonster.Contains(player2sMonster.monsterType))
            {
                //player2 weak against player1 and player1 not weak against player2
                Console.WriteLine("\n --------------");
                Console.WriteLine($" {player2sMonster.name} is weak against {player1sMonster.name} therefor ...");
                return _player1;
            }
            else if(weaknessPlayer1sMonster.Contains(player2sMonster.monsterType) && weaknessPlayer2sMonster.Contains(player1sMonster.monsterType))
            {
                //both are weak against each other
                Console.WriteLine("\n --------------");
                Console.WriteLine($" {player1sMonster.name} and {player2sMonster.name} are both weak against each other therefor ...");
                return null;
            }
            else
            {
                return StandardDamageComparison(player1sCard, player2sCard);
            }
        }
        private User SpellVsRandom(ICard player1sCard, ICard player2sCard)
        {
            if (player1sCard is Monster && player2sCard is Monster)
            {
                //no monsters allowed
                Console.WriteLine("error while checking for card type");
            }
            else if (player1sCard is Spell && player2sCard is Monster)
            {
                Spell player1sSpell = (Spell)player1sCard;
                HashSet<MonsterType> weaknessPlayer1sSpell;// = new HashSet<MonsterType>();
                SpellsWeaknessesAgainstMonsters.TryGetValue(player1sSpell.elementType, out weaknessPlayer1sSpell);
                Monster player2sMonster = (Monster)player2sCard;
                HashSet<ElementType> weaknessPlayer2sMonster;// = new HashSet<ElementType>();
                MonstersWeaknessesAgainstSpells.TryGetValue(player2sMonster.monsterType, out weaknessPlayer2sMonster);
                if(weaknessPlayer1sSpell == null)
                {
                    //create empty hashset in case there is no weakness, to enable "Contains"
                    weaknessPlayer1sSpell = new HashSet<MonsterType>();
                }
                if(weaknessPlayer2sMonster == null)
                {
                    //create empty hashset in case there is no weakness, to enable "Contains"
                    weaknessPlayer2sMonster = new HashSet<ElementType>();
                }
                if (weaknessPlayer1sSpell.Contains(player2sMonster.monsterType) && !weaknessPlayer2sMonster.Contains(player1sSpell.elementType))
                {
                    //player1 weak against player2 and player2 not weak against player1
                    Console.WriteLine("\n --------------");
                    Console.WriteLine($" {player1sSpell.name} is weak against {player2sMonster.name} therefor ...");
                    return _player2;
                }
                else if (weaknessPlayer2sMonster.Contains(player1sSpell.elementType) && !weaknessPlayer1sSpell.Contains(player2sMonster.monsterType))
                {
                    //player2 weak against player1 and player1 not weak against player2
                    Console.WriteLine("\n --------------");
                    Console.WriteLine($" {player2sMonster.name} is weak against {player1sSpell.name} therefor ...");
                    return _player1;
                }
                else if (weaknessPlayer1sSpell.Contains(player2sMonster.monsterType) && weaknessPlayer2sMonster.Contains(player1sSpell.elementType))
                {
                    //both are weak against each other
                    Console.WriteLine("\n --------------");
                    Console.WriteLine($" {player1sSpell.name} and {player2sMonster.name} are both weak against each other therefor ...");
                    return null;
                }
            }
            else if (player1sCard is Monster && player2sCard is Spell)
            {
                Monster player1sMonster = (Monster)player1sCard;
                HashSet<ElementType> weaknessPlayer1sMonster;// = new HashSet<ElementType>();
                MonstersWeaknessesAgainstSpells.TryGetValue(player1sMonster.monsterType, out weaknessPlayer1sMonster);
                Spell player2sSpell = (Spell)player2sCard;
                HashSet<MonsterType> weaknessPlayer2sSpell;// = new HashSet<MonsterType>();
                SpellsWeaknessesAgainstMonsters.TryGetValue(player2sSpell.elementType, out weaknessPlayer2sSpell);
                if(weaknessPlayer1sMonster == null)
                {
                    //create empty hashset in case there is no weakness, to enable "Contains"
                    weaknessPlayer1sMonster = new HashSet<ElementType>();
                }
                if(weaknessPlayer2sSpell == null)
                {
                    //create empty hashset in case there is no weakness, to enable "Contains"
                    weaknessPlayer2sSpell = new HashSet<MonsterType>();
                }
                if (weaknessPlayer1sMonster.Contains(player2sSpell.elementType) && !weaknessPlayer2sSpell.Contains(player1sMonster.monsterType))
                {
                    //player1 weak against player2 and player2 not weak against player1
                    Console.WriteLine("\n --------------");
                    Console.WriteLine($" {player1sMonster.name} is weak against {player2sSpell.name} therefor ...");
                    return _player2;
                }
                else if (weaknessPlayer2sSpell.Contains(player1sMonster.monsterType) && !weaknessPlayer1sMonster.Contains(player2sSpell.elementType))
                {
                    //player2 weak against player1 and player1 not weak against player2
                    Console.WriteLine("\n --------------");
                    Console.WriteLine($" {player2sSpell.name} is weak against {player1sMonster.name} therefor ...");
                    return _player1;
                }
                else if (weaknessPlayer1sMonster.Contains(player2sSpell.elementType) && weaknessPlayer2sSpell.Contains(player1sMonster.monsterType))
                {
                    //both are weak against each other
                    Console.WriteLine("\n --------------");
                    Console.WriteLine($" {player1sMonster.name} and {player2sSpell.name} are both weak against each other therefor ...");
                    return null;
                }
            }

            if (player1sCard.elementType != player2sCard.elementType)
            {
                ElementType superiorElement;
                SuperiorElement.TryGetValue(new HashSet<ElementType> { player1sCard.elementType, player2sCard.elementType }, out superiorElement);
                User winner;
                if (player1sCard.elementType == superiorElement)
                {
                    //player1sCard superior
                    Console.WriteLine($" {player1sCard.elementType} is effective against {player2sCard.elementType}!");
                    if (player1sCard.damage * DAMAGEMANIPULATOR > player2sCard.damage / DAMAGEMANIPULATOR)
                    {
                        Console.WriteLine($" {player1sCard.damage * DAMAGEMANIPULATOR} ({player1sCard.damage}x{DAMAGEMANIPULATOR}) > {player2sCard.damage / DAMAGEMANIPULATOR} ({player2sCard.damage}/{DAMAGEMANIPULATOR})");
                        winner = _player1;
                    }
                    else if (player1sCard.damage * DAMAGEMANIPULATOR < player2sCard.damage / DAMAGEMANIPULATOR)
                    {
                        Console.WriteLine($" {player1sCard.damage * DAMAGEMANIPULATOR} ({player1sCard.damage}x{DAMAGEMANIPULATOR}) < {player2sCard.damage / DAMAGEMANIPULATOR} ({player2sCard.damage}/{DAMAGEMANIPULATOR})");
                        winner = _player2;
                    }
                    else
                    {
                        Console.WriteLine($" {player1sCard.damage * DAMAGEMANIPULATOR} ({player1sCard.damage}x{DAMAGEMANIPULATOR}) = {player2sCard.damage / DAMAGEMANIPULATOR} ({player2sCard.damage}/{DAMAGEMANIPULATOR})");
                        winner = null;
                    }
                    return winner;
                }
                else if (player2sCard.elementType == superiorElement)
                {
                    //player2sCard superior
                    if (player1sCard.damage / DAMAGEMANIPULATOR > player2sCard.damage * DAMAGEMANIPULATOR)
                    {
                        Console.WriteLine($" {player1sCard.damage / DAMAGEMANIPULATOR} ({player1sCard.damage}/{DAMAGEMANIPULATOR}) > {player2sCard.damage * DAMAGEMANIPULATOR} ({player2sCard.damage}x{DAMAGEMANIPULATOR})");
                        winner = _player1;
                    }
                    else if (player1sCard.damage / DAMAGEMANIPULATOR < player2sCard.damage * DAMAGEMANIPULATOR)
                    {
                        Console.WriteLine($" {player1sCard.damage / DAMAGEMANIPULATOR} ({player1sCard.damage}/{DAMAGEMANIPULATOR}) < {player2sCard.damage * DAMAGEMANIPULATOR} ({player2sCard.damage}x{DAMAGEMANIPULATOR})");
                        winner = _player1;
                    }
                    else
                    {
                        Console.WriteLine($" {player1sCard.damage / DAMAGEMANIPULATOR} ({player1sCard.damage}/{DAMAGEMANIPULATOR}) = {player2sCard.damage * DAMAGEMANIPULATOR} ({player2sCard.damage}x{DAMAGEMANIPULATOR})");
                        winner = _player1;
                    }
                    return winner;
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
                return StandardDamageComparison(player1sCard, player2sCard);
            }
        }

        private User StandardDamageComparison(ICard player1sCard, ICard player2sCard)
        {
            User winner;
            Console.WriteLine("\n --------------");
            Console.WriteLine($" {player1sCard.damage} dmg VS {player2sCard.damage} dmg");
            if (player1sCard.damage > player2sCard.damage)
            {
                winner = _player1;
            }
            else if (player1sCard.damage < player2sCard.damage)
            {
                winner = _player2;
            }
            else
            {
                winner = null;
            }
            return winner;
        }

        public User Fight()
        {
            Console.WriteLine("\n _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _");
            Console.WriteLine($" {_player1.GetName()} <p1elo> VS {_player2.GetName()} <p2elo>" );
            Console.WriteLine(" _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _");
            ICard player1CurrentCard;
            ICard player2CurrentCard;
            while (!_endGame)
            {
                player1CurrentCard = _player1.GetRandomCardFromDeck();
                player2CurrentCard = _player2.GetRandomCardFromDeck();
                if (player1CurrentCard is Monster && player2CurrentCard is Monster)
                {
                    //Monster fight
                    Console.WriteLine("\n --------------");
                    Console.WriteLine($" {player1CurrentCard.name} VS {player2CurrentCard.name}");
                    _winner = MonsterVsMonster(player1CurrentCard, player2CurrentCard);
                }
                else if(player1CurrentCard is Spell || player2CurrentCard is Spell){
                    //Spell involved
                    Console.WriteLine("\n --------------");
                    Console.WriteLine($" {player1CurrentCard.name} VS {player2CurrentCard.name}");
                    _winner = SpellVsRandom(player1CurrentCard, player2CurrentCard);
                }
                else
                {
                    Console.WriteLine("error while defining card type");
                }
                _roundCounter++;
                if(_winner == _player1)
                {
                    //player one gets card of player two
                    _player1.AddCardToDeck(player2CurrentCard);
                    _player2.RemoveCardFromDeck(player2CurrentCard);
                    Console.WriteLine($" {_player1.GetName()}'s Card: {player1CurrentCard.name} WINS!");
                }
                else if(_winner == _player2)
                {
                    //player two gets card of player one
                    _player2.AddCardToDeck(player1CurrentCard);
                    _player1.RemoveCardFromDeck(player1CurrentCard);
                    Console.WriteLine($" {_player2.GetName()}'s Card: {player2CurrentCard.name} WINS!");
                }
                else
                {
                    //draw
                    Console.WriteLine($" {_player1.GetName()}'s Card: {player1CurrentCard.name} and { _player2.GetName()}'s Card: {player2CurrentCard.name} TIED!");
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
                    Console.WriteLine("Error while assigning winner");
                }
            }
            else
            {
                _winner = null;
            }
            return _winner;
        }
    }
}
