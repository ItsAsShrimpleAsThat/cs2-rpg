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
        public int defense;
        public int xp;
        public IndefiniteArticle indefiniteArticle;

        public Enemy(string name, Type type, int health, int maxHP, int defense, int xp, IndefiniteArticle indefiniteArticle)
        {
            this.name = name;
            this.type = type;
            this.health = health;
            this.maxHP = maxHP;
            this.defense = defense;
            this.xp = xp;
            this.indefiniteArticle = indefiniteArticle;
        }

        public string WithIndefiniteArticle()
        {
            return indefiniteArticle.ToString() + " " + name;
        }
    }
}
