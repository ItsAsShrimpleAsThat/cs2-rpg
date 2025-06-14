using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    internal static class TypeHelper
    {
        public static Type EffectiveAgainst(Type type)
        {
            return type switch
            {
                Type.Earth => Type.Poison,
                Type.Fire => Type.Earth,
                Type.Poison => Type.Water,
                Type.Water => Type.Fire,
                Type.Neutral => Type.Neutral,
                _ => throw new NotImplementedException()
            };
        }
    }
}
