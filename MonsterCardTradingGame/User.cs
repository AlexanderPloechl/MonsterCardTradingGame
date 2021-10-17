using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class User
    {
        private Stack _stack { get; set; }
        private Deck _deck { get; set; }
        public string name { get; set; }
        public string password { get; set; }
    }
}
