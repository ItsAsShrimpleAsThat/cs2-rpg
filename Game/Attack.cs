using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    internal class Attack
    {
        public int baseDamage;
        public double dmgVariance;
        public Type effectiveType; // More effect against this type
        public Type resonantType; // Less effective against this type


        public Attack(int baseDamage, double dmgVariance, Type effectiveType, Type resonantType)
        {
            this.baseDamage = baseDamage;
            this.dmgVariance = dmgVariance;
            this.effectiveType = effectiveType;
            this.resonantType = resonantType;
        }

        public (int damageDealt, int opponentDefense) CalculateDamageAndNewDefense(int attackerXP, int opponenetDefense, Type opponentType)
        {
            double scaledDmg = baseDamage + attackerXP * GameConstants.attackDmgXPScale;
            if (effectiveType == opponentType && effectiveType != Type.Neutral)
            {
                scaledDmg *= GameConstants.attackEffectiveModifier;
            }
            if (resonantType == opponentType && resonantType != Type.Neutral)
            {
                scaledDmg *= GameConstants.attackAffinityModifier;
            }

            // randomize dmg a bit
            scaledDmg *= (1.0 + ((new Random().NextDouble() * 2) - 1.0) * dmgVariance);

            // Half life's armor system
            double desiredAbsorption = scaledDmg * GameConstants.defenseAbsorption;

            int actualAbsorption = (int)Math.Min(desiredAbsorption, opponenetDefense);

            opponenetDefense -= actualAbsorption;
            int finalDmg = (int)scaledDmg - actualAbsorption;

            Console.WriteLine("Dmg dealt float: " + (scaledDmg - actualAbsorption).ToString() + " dmg absorbed by armor: " + actualAbsorption.ToString());

            return (damageDealt: finalDmg, opponentDefense: opponenetDefense);
        }
    }
}
