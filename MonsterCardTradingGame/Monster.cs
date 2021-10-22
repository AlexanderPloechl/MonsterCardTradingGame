﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class Monster : ICard
    {
        public Monster(string name, int damage, MonsterType monsterType)
        {
            this.name = name;
            this.damage = damage;
            this.monsterType = monsterType;
        }

        public string name { get;set; }
        public int damage { get; set; }
        public MonsterType monsterType { get; set; }
        public void attack()
        {
            throw new NotImplementedException();
        }
    }
}
