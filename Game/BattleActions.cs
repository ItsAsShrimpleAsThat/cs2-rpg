using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public enum BattleActions : int
    {
        UseItem = 0,

        // ========== Attacks ========== (1-999)
        Strike = 1,

        // ========== Self Buffs ==========  (1000-1999)
        Focus = 1000,

        // ========== Status Effects ========== (2000-2999)
        Sting = 2000,

        // ========== Defense ========== (3000-3999)
        Defend = 3000
    }
}
