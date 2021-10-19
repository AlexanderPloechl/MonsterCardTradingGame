using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class Stack : IMonster, ISpell
    {
        List<ICard> stack;

        public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int damage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ElementType elementType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ElementType ElementType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MonsterType MonsterType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void attack()
        {
            throw new NotImplementedException();
        }
    }
}
