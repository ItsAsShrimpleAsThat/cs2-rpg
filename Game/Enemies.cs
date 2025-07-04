using CounterStrike2GSI.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    internal static class Enemies
    {
        public static EnemyPrefab[] earthEnemies =
        {
            new EnemyPrefab("Stoneback", Type.Earth, 0.0, 0.0, IndefiniteArticle.a, new BattleActions[] { BattleActions.Strike }),
            new EnemyPrefab("Dustcaller", Type.Earth, 0.1, 0.2, IndefiniteArticle.a, new BattleActions[] { BattleActions.Strike })
        };

        public static EnemyPrefab[] fireEnemies =
        {
            new EnemyPrefab("Blazehound", Type.Fire, 0.0, 0.0, IndefiniteArticle.a, new BattleActions[] { BattleActions.Strike }),
            new EnemyPrefab("Kindlekin", Type.Fire, 0.15, 0.1, IndefiniteArticle.a, new BattleActions[] { BattleActions.Strike })
        };

        public static EnemyPrefab[] poisonEnemies =
        {
            new EnemyPrefab("Dartite", Type.Poison, 0.0, 0.0, IndefiniteArticle.a, new BattleActions[] { BattleActions.Strike }),
            new EnemyPrefab("Blightling", Type.Poison, 0.12, 0.12, IndefiniteArticle.a, new BattleActions[] { BattleActions.Strike })
        };

        public static EnemyPrefab[] waterEnemies =
        {
            new EnemyPrefab("Coralhyde", Type.Water, 0.0, 0.0, IndefiniteArticle.a, new BattleActions[] { BattleActions.Strike }),
            new EnemyPrefab("Shrimpene", Type.Water, 0.1, 0.2, IndefiniteArticle.a, new BattleActions[] { BattleActions.Strike })
        };

        public static EnemyPrefab[] GetPrefabsFromType(Type type)
        {
            return type switch
            {
                Type.Earth => earthEnemies,
                Type.Fire => fireEnemies,
                Type.Poison => poisonEnemies,
                Type.Water => waterEnemies
            };
        }

        public static EnemyPrefab GetRandomPrefabFromType(Type type)
        {
            EnemyPrefab[] prefabs = GetPrefabsFromType(type);
            return prefabs[RNG.Next(0, prefabs.Length)];
        }

        // This is where enemy battle difficulty is scaled. Change if game is too easy/difficult I might make it exponential if it turns out too easyyyy
        public static int PlayerXPtoEnemyXP(int playerXP)
        {
            return (int)(playerXP * ((RNG.NextDouble_n1_1() * 0.1) + (1.0 - 0.2)));
        }
    }
}
