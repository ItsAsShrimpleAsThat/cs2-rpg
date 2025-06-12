using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    internal class Enemy
    {
        public string name;
        public Type type;
        public int health;
        public int maxHP;
        public int attack;
        public int defense;
        public int xp;

        public Enemy(string name, Type type, int health, int maxHP, int attack, int defense, int xp)
        {
            this.name = name;
            this.type = type;
            this.health = health;
            this.maxHP = maxHP;
            this.attack = attack;
            this.defense = defense;
            this.xp = xp;
        }
    }
}
