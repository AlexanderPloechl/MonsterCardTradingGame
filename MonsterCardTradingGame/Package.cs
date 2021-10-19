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
            for (int i = 0; i < NumberOfCardsInPackage; i++)
            {
                CardsInPackage.Add(new Monster());
            }
        }

        public void PrintPackageContents()
        {
            foreach (ICard Card in CardsInPackage)
            {
                Console.WriteLine(Card.name);
            }
        }
    }
}
