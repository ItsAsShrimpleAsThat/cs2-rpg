using cs2_rpg.Game.Buffs;
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

        public static string[] allCommands = { "!rpg", "!explore", "!help", "!option", "!opt", "!givexp" };

        public static BattleActions[] attackActions = { BattleActions.Strike };

        public static Dictionary<BattleActions, Attack> battleAction2Attack = new Dictionary<BattleActions, Attack>()
        {
            { BattleActions.Strike , new Attack(10, 0.1, Type.Neutral, Type.Neutral, 1.0/24.0, "Strike") },
        };
        public static Dictionary<BattleActions, Buff> battleAction2Buff = new Dictionary<BattleActions, Buff>()
        {
            { BattleActions.Focus , new FocusBuff() }
        };

        public static Dictionary<AttackEffectiveness, string> attackEffectivenessDialogue = new Dictionary<AttackEffectiveness, string>()
        {
            { AttackEffectiveness.Effective , "It was effective!"},
            { AttackEffectiveness.EffectiveTypeMatch , "It was super effective! (Type match)"},
            { AttackEffectiveness.NotEffectiveTypeMatch , "It was not effective! (Type mismatch)"},
            { AttackEffectiveness.EffectiveCrit , "It was super effective! (Critical hit!)"},
            { AttackEffectiveness.EffectiveTypeMatchCrit , "It was super duper effective! (Crit and Type match!)"},
            { AttackEffectiveness.EffectiveNonTypeMatchCrit , "It was effective! (Critical, but Type mismatch)"}
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
