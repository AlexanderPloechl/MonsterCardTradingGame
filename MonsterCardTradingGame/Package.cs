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
        List<ICard> CardsInPackage;
        public Package()
        {
            CardsInPackage = new List<ICard>();
            //for (int i = 0; i < NumberOfCardsInPackage; i++)
            //{
            //    CardsInPackage.Add(new Monster());
            //}
            CardsInPackage.Add(new Monster("testmonster1", 2, MonsterType.Dragon));
            CardsInPackage.Add(new Monster("testmonster2", 4, MonsterType.Wizard));
            CardsInPackage.Add(new Spell("testspell1", 6, ElementType.Fire));
            CardsInPackage.Add(new Spell("testspell2", 8, ElementType.Fire));
            CardsInPackage.Add(new Spell("testspell3", 10, ElementType.Fire));
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
