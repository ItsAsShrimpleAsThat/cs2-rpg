using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public struct EnemyPrefab
    {
        public string name;
        public Type type;
        public double hpVariance;
        public double defenseVariance;
        public IndefiniteArticle indefiniteArticle;
        public BattleActions[] battleActions;

        public EnemyPrefab(string name, Type type,  double hpVariance, double defenseVariance, IndefiniteArticle indefiniteArticle, BattleActions[] battleActions)
        {
            this.name = name;
            this.type = type;
            this.hpVariance = hpVariance;
            this.defenseVariance = defenseVariance;
            this.indefiniteArticle = indefiniteArticle;
            this.battleActions = battleActions;
        }
    }
}
