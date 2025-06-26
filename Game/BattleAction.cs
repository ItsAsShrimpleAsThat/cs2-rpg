using cs2_rpg.csinterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public static class BattleAction
    {
        public static BattleActionType GetBattleActionType(BattleActions action)
        {
            if ((int)action == 0)                                    // Use Item action id
            {
                return BattleActionType.UseItem;
            }
            else if ((int)action >= 1 && (int)action <= 999)         // Attack action id range
            {
                return BattleActionType.Attack;
            }
            else if ((int)action >= 1000 && (int)action <= 1999)    // Self buff action id range
            {
                return BattleActionType.SelfBuff;
            }
            else if ((int)action >= 2000 && (int)action <= 2999)    // Status effect action id range
            {
                return BattleActionType.StatusEffect;
            }
            else if ((int)action >= 3000 && (int)action <= 3999)    // Defend action id range
            {
                return BattleActionType.Defend;
            }
            return BattleActionType.NullActionType;
        }

        public static string BattleActionToName(BattleActions state)
        {
            return state switch
            {
                BattleActions.UseItem => "Use Item",
                BattleActions.Strike => "Strike",
                BattleActions.Focus => "Focus",
                BattleActions.Sting => "Sting",
                BattleActions.Defend => "Defend",
                 _ => throw new NotImplementedException()
            };
        }
    }
}
