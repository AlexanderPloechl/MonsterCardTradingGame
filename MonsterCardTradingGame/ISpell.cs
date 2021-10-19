using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    interface ISpell : ICard
    {
        ElementType ElementType { get; set; }
    }
}
