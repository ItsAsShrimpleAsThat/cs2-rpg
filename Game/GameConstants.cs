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

        public static Dictionary<BattleActions, string> battleAction2Name = new Dictionary<BattleActions, string>()
        {
            { BattleActions.UseItem , "Use Item" },
            { BattleActions.Strike , "Strike" },
            { BattleActions.Focus , "Focus" },
            { BattleActions.Sting , "Sting" },
            { BattleActions.Defend , "Defend" }
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

        public static BattleActions[] attackActions = { BattleActions.Strike };

        public static Dictionary<BattleActions, Attack> battleAction2Attack = new Dictionary<BattleActions, Attack>()
        {
            { BattleActions.Strike , new Attack(10, 0.1, Type.Neutral, Type.Neutral) },
        };

        public static readonly int baseHealth = 30;
        public static readonly int baseDefense = 8;
        public static readonly double attackDmgXPScale = 0.1;
        public static readonly double attackAffinityModifier = 0.75;
        public static readonly double attackEffectiveModifier = 1.5;
        public static readonly double defenseAbsorption = 0.8;
        public static readonly double moneyWinScale = 0.5;
        public static readonly double baseMoneyReward = 5;
        public static readonly double moneyRewardVariance = 0.25;
        public static readonly double xpWinScale = 0.75;
        public static readonly double baseXPReward = 7;
        public static readonly double xpRewardVariance = 0.25;
    }
}
