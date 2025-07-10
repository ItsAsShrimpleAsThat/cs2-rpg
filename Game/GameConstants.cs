using CounterStrike2GSI.Nodes;
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

        public static string[] allCommands = { "!rpg", "!explore", "!help", "!option", "!opt", "!givemed" };

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

        public static Dictionary<Items, Item> itemEnum2Item = new Dictionary<Items, Item>()
        {
            { Items.JuiceBox, new Item(Items.JuiceBox, "Juice Box", "Restores 10 HP",                 IndefiniteArticle.a, (p) => { p.health = Math.Min(p.health + 10,          p.maxHP); }) },
            { Items.Bandage, new Item(Items.Bandage, "Bandage", "Restores 25% of max health",         IndefiniteArticle.a, (p) => { p.health = Math.Min(p.health + p.maxHP / 4, p.maxHP); }) },
            { Items.Medkit, new Item(Items.Medkit, "Medkit", "Restores 50% of max health",            IndefiniteArticle.a, (p) => { p.health = Math.Min(p.health + p.maxHP / 2, p.maxHP); }) },
            { Items.MaxPotion, new Item(Items.MaxPotion, "Max Potion", "Restores 100% of max health", IndefiniteArticle.a, (p) => { p.health = p.maxHP; }) },
            { Items.PaddedVest, new Item(Items.PaddedVest, "Padded Vest", "Gives 8 defense",          IndefiniteArticle.a, (p) => { p.defense += 8; }) },
            { Items.Chainmail, new Item(Items.Chainmail, "Chainmail", "Gives 15 defense",             IndefiniteArticle.a, (p) => { p.defense += 15; }) },
        };

        public static Items[] legendaryItems = { };
        public static Items[] rareItems = { Items.MaxPotion };
        public static Items[] uncommonItems = { Items.Medkit, Items.Chainmail };
        public static Items[] commonItems = { Items.JuiceBox, Items.Bandage, Items.PaddedVest };

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

        public static readonly double legendaryRarity = 0.01;
        public static readonly double rareRarity = 0.05;
        public static readonly double uncommonRarity = 0.25;
    }
}
