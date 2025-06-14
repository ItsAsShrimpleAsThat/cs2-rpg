using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    internal static class XP
    {
        public static int XPToLevel(int xp)
        {
            return (int)Math.Floor(0.4 * Math.Sqrt(xp));
        }

        public static int XPtoHP(int xp)
        {
            return GameConstants.baseHealth + (int)(0.2 * xp);
        }

        public static int XPtoDefense(int xp)
        {
            return GameConstants.baseDefense + (int)(0.05 * xp);
        }
    }
}
