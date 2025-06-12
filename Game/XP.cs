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
    }
}
