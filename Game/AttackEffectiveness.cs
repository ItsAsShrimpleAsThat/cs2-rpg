using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public enum AttackEffectiveness
    {
        NotEffectiveTypeMatch,
        EffectiveTypeMatch,
        EffectiveCrit,
        EffectiveNonTypeMatchCrit,
        EffectiveTypeMatchCrit,
        Effective
    }
}
