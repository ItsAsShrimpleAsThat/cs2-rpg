using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public class Enemy : Actor
    {
        public string name;
        public IndefiniteArticle indefiniteArticle;
        
        public Enemy(string name, Type type, int health, int maxHP, int defense, int xp, IndefiniteArticle indefiniteArticle, BattleActions[] battleActions) : base(health, maxHP, defense, xp, type, battleActions)
        {
            this.name = name;
            this.indefiniteArticle = indefiniteArticle;
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

        public BattleActions GetRandomBattleaction(double intelligence)
        {
            intelligence = Math.Min(Math.Max(intelligence, 0.0), 1.0); // clamp01

            return battleActions[0];
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
