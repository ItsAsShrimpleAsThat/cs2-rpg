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
        public BattleActions[] battleActions;

        public Enemy(string name, Type type, int health, int maxHP, int defense, int xp, IndefiniteArticle indefiniteArticle, BattleActions[] battleActions)
        {
            this.name = name;
            this.type = type;
            this.health = health;
            this.maxHP = maxHP;
            this.defense = defense;
            this.xp = xp;
            this.indefiniteArticle = indefiniteArticle;
            this.battleActions = battleActions;
        }

        public string WithIndefiniteArticle()
        {
            return indefiniteArticle.ToString() + " " + name;
        }

        public Attack[] GetAllAttacks() // Can we all agree that linq is stupid? why is there no .map() or .filter()
        {
            return (from action in battleActions
                   where BattleAction.GetBattleActionType(action) == BattleActionType.Attack
                   select action).Select(i => GameConstants.battleAction2Attack[i]).ToArray();

        }

        /// <summary>
        /// Intelligence should be between 0-1. Higher intelligence means more likely to pick the best possible attack;
        /// </summary>
        /// <returns></returns>
        public Attack GetRandomAttack(double intelligence)
        {
            intelligence = Math.Min(Math.Max(intelligence, 0.0), 1.0); // clamp01

            return GetAllAttacks()[0];
        }
    }
}
