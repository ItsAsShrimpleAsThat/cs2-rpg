using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    internal static class GameConstants
    {
        public static Destination[] allDestinations = { Destination.Forest, Destination.Desert, Destination.Grassland, Destination.Mountain, Destination.Lake };
        public static Dictionary<Destination, string> dest2Name = new Dictionary<Destination, string>()
        {
            { Destination.Forest , "Forest" },
            { Destination.Desert , "Desert" },
            { Destination.Grassland , "Grassland" },
            { Destination.Mountain , "Mountain" },
            { Destination.Lake , "Lake" },
            { Destination.Swamp , "Swamp" }
        };

        public static Dictionary<PlayerState, string> pstate2Name = new Dictionary<PlayerState, string>()
        {
            { PlayerState.Free , "Free" },
            { PlayerState.Exploring , "Exploring" },
            { PlayerState.InBattle , "In a battle" },
            { PlayerState.InShop , "In a shop" },
            { PlayerState.Dead , "Dead" },
        };

        public static Dictionary<BattleAction, string> battleAction2Name = new Dictionary<BattleAction, string>()
        {
            { BattleAction.UseItem , "Use Item" },
            { BattleAction.Strike , "Strike" },
            { BattleAction.Focus , "Focus" },
            { BattleAction.Sting , "Sting" },
            { BattleAction.Defend , "Defend" }
        };

        public static string[] allCommands = { "!rpg", "!explore", "!help", "!option", "!givexp" };

        public static Dictionary<Destination, Type[]> typesInDests = new Dictionary<Destination, Type[]>()
        {
            { Destination.Forest , new Type[] { Type.Earth, Type.Poison } },
            { Destination.Desert , new Type[] { Type.Earth, Type.Fire } },
            { Destination.Grassland , new Type[] { Type.Poison, Type.Fire} },
            { Destination.Mountain , new Type[] { Type.Earth, Type.Water} },
            { Destination.Lake , new Type[] { Type.Water, Type.Fire} },
            { Destination.Swamp , new Type[] { Type.Water, Type.Poison} },
        };

        public static readonly int baseHealth = 30;
        public static readonly int baseDefense = 8;
    }
}
