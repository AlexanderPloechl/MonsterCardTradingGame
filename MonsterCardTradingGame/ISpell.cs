﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    interface ISpell : ICard
    {
        public ElementType ElementType { get; set; }
    }
}
