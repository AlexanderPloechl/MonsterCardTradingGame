using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class Monster : ICard
    {
        public Monster()
        {
            this.name = "test";
            this.damage = 100;
            this.MonsterType = MonsterType.Dragon;
        }

        public string name { get;set; }

        public int damage { get; set; }
        public MonsterType MonsterType { get; set; }
        public void attack()
        {
            throw new NotImplementedException();
        }
    }
}
