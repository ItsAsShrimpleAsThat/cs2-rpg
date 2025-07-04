using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public static class RNG
    {
        public static Random rand = new Random();

        public static double NextDouble()
        {
            return rand.NextDouble();
        }

        public static double NextDouble_n1_1()
        {
            return (rand.NextDouble() * 2.0) - 1.0;
        }

        public static int Next(int min, int max)
        {
            return rand.Next(min, max);
        }

        public static int Next(int max)
        {
            return rand.Next(max);
        }
    }
}
