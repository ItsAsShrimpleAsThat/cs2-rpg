using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public abstract class Buff
    {
        public abstract Attack ApplyBuff(Attack attack);
    }
}
