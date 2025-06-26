using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public abstract class Buff
    {
        public virtual string name => "Null Buff"; // This is how to make "fields" overrideable I think. Basically its a one line function that returns "Null Buff" that we can then override. I think
        public virtual int maxInstances => 1;
        public abstract void ApplyBuff(ref Attack attack);
        public abstract bool ShouldRemoveBuff();
    }
}
