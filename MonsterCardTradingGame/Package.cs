using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class Package
    {
        const int NumberOfCardsInPackage = 5;
        public List<ICard> CardsInPackage;
        public const int price = 5;
        public Package()
        {
            CardsInPackage = new List<ICard>();
            CardsInPackage.Add(new Monster("WaterGoblin", 10, MonsterType.Goblin));
            CardsInPackage.Add(new Monster("FireTroll", 15, MonsterType.Troll));
            CardsInPackage.Add(new Spell("FireSpell", 10, ElementType.Fire));
            CardsInPackage.Add(new Spell("WaterSpell", 20, ElementType.Water));
            CardsInPackage.Add(new Spell("RegularSpell", 10, ElementType.Normal));
        }

        public void PrintPackageContents()
        {
            foreach (ICard Card in CardsInPackage)
            {
                Console.WriteLine($"{Card.name} - {Card.damage}");
            }
        }
    }
}
