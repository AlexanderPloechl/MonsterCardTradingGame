using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class Spell : ICard
    {
        public Spell(string name, int damage, ElementType elementType)
        {
            this.name = name;
            this.damage = damage;
            this.elementType = elementType;
        }
        public string name { get; private set; }
        public int damage { get; }
        public ElementType elementType { get; private set; }
    }
}
