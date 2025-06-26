using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game.Buffs
{
    public class FocusBuff : Buff
    {
        public override string name => "Focus";
        public override int maxInstances => 1;

        public int turnsLeft = 2;
        private double dmgMultiplier = 1.5;
        private double critChanceMultiplier = 2;
        public override void ApplyBuff(ref Attack attack)
        {
            attack.critChance *= critChanceMultiplier;
            attack.baseDamage = (int)(attack.baseDamage * dmgMultiplier);
            turnsLeft--;
        }

        public override bool ShouldRemoveBuff()
        {
            return turnsLeft <= 0;
        }
    }
}
