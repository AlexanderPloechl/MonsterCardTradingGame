using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    interface ICard
    {
        string name { get; set; }
        int damage { get; set; }
        void attack();
    }
}
