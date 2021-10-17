using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class Package : ICard
    {
        public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int damage => throw new NotImplementedException();

        public ElementType elementType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void attack()
        {
            throw new NotImplementedException();
        }
    }
}
