using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public class Attack
    {
        public string name;
        public int baseDamage;
        public double dmgVariance;
        public double critChance;
        public Type effectiveType; // More effect against this type
        public Type resonantType; // Less effective against this type


        public Attack(int baseDamage, double dmgVariance, Type effectiveType, Type resonantType, double critChance, string name)
        {
            this.baseDamage = baseDamage;
            this.dmgVariance = dmgVariance;
            this.effectiveType = effectiveType;
            this.resonantType = resonantType;
            this.name = name;
        }

        public AttackEffectiveness AddCrit(AttackEffectiveness noCrit)
        {
            if (noCrit == AttackEffectiveness.EffectiveTypeMatch) return AttackEffectiveness.EffectiveTypeMatchCrit;
            else if (noCrit == AttackEffectiveness.NotEffectiveTypeMatch) return AttackEffectiveness.EffectiveNonTypeMatchCrit;
            else return AttackEffectiveness.EffectiveCrit;
        }

        public (int damageDealt, int opponentDefense, AttackEffectiveness effectiveness) CalculateDamageAndNewDefense(int attackerXP, int opponenetDefense, Type opponentType)
        {
            AttackEffectiveness effectiveness = AttackEffectiveness.Effective;

            double scaledDmg = baseDamage + attackerXP * GameConstants.attackDmgXPScale;
            if (effectiveType == opponentType && effectiveType != Type.Neutral)
            {
                effectiveness = AttackEffectiveness.EffectiveTypeMatch;
                scaledDmg *= GameConstants.attackEffectiveModifier;
            }
            if (resonantType == opponentType && resonantType != Type.Neutral)
            {
                effectiveness = AttackEffectiveness.NotEffectiveTypeMatch;
                scaledDmg *= GameConstants.attackAffinityModifier;
            }

            // randomize dmg a bit
            scaledDmg *= (1.0 + ((RNG.NextDouble() * 2) - 1.0) * dmgVariance);

            // Crits because i guess rpgs have to do that
            if(RNG.NextDouble() <= critChance)
            {
                scaledDmg *= 1.5;
                effectiveness = AddCrit(effectiveness);
            }

            // Half life's armor system
            double desiredAbsorption = scaledDmg * GameConstants.defenseAbsorption;

            int actualAbsorption = (int)Math.Min(desiredAbsorption, opponenetDefense);

            opponenetDefense -= actualAbsorption;
            int finalDmg = (int)scaledDmg - actualAbsorption;

            Console.WriteLine("Dmg dealt float: " + (scaledDmg - actualAbsorption).ToString() + " dmg absorbed by armor: " + actualAbsorption.ToString());

            return (damageDealt: finalDmg, opponentDefense: opponenetDefense, effectiveness: effectiveness);
        }
    }
}
