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
        private static Random rand = new Random();
        public static EnemyPrefab[] earthEnemies = 
        {
            new EnemyPrefab("Stoneback", Type.Earth, 0.0, 0.0),
            new EnemyPrefab("Dustcaller", Type.Earth, 0.1, 0.2)
        };

        public static EnemyPrefab[] fireEnemies =
        {
            new EnemyPrefab("Blazehound", Type.Fire, 0.0, 0.0),
            new EnemyPrefab("Kindlekin", Type.Fire, 0.15, 0.1)
        };

        public static EnemyPrefab[] poisonEnemies =
        {
            new EnemyPrefab("Dartite", Type.Poison, 0.0, 0.0),
            new EnemyPrefab("Blightling", Type.Poison, 0.12, 0.12)
        };

        public static EnemyPrefab[] waterEnemies =
        {
            new EnemyPrefab("Coralhide", Type.Water, 0.0, 0.0),
            new EnemyPrefab("Shrimpene", Type.Water, 0.1, 0.2)
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
            return prefabs[rand.Next(0, prefabs.Length)];
        }

        // This is where enemy battle difficulty is scaled. Change if game is too easy/difficult I might make it exponential if it turns out too easyyyy

        public static int PlayerXPtoEnemyXP(int playerXP)
        {
            return (int)(playerXP * (0.95 + rand.NextDouble() * 0.1)) + 5;
        }

        public static int XPtoHP(int xp)
        {
            return GameConstants.baseHealth + (int)(0.2 * xp);
        }

        public static int XPtoDefense(int xp)
        {
            return GameConstants.baseDefense + (int)(0.05 * xp);
        }
    }
}
