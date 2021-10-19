using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class Package //: IMonster, ISpell
    {
        //public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public int damage => throw new NotImplementedException();

        //public ElementType elementType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public ElementType ElementType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public MonsterType MonsterType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public void attack()
        //{
        //    throw new NotImplementedException();
        //}
        List<ICard> CardsInPackage;
        public Package()
        {
            CardsInPackage = new List<ICard>();
            Monster monster = new Monster();
            CardsInPackage.Add(monster);
        }
        void AddCardToList()
        {
            Monster test = new Monster();
            CardsInPackage.Add(test);
        }
        public void PrintPackageContents()
        {
            foreach(ICard Card in CardsInPackage)
            {
                Console.WriteLine(Card.name);
            }
        }
    }
}
