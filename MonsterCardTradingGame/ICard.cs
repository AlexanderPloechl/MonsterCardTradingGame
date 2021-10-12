using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    interface ICard
    {
        public string name { get; set; }
        public int damage { get; }
        public ElementType elementType { get; set; }

        void attack();
    }
}
