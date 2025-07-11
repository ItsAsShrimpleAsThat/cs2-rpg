using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public static class Options
    {
        public static T[] PickNRandomElementsFromArray<T>(T[] source, int num)
        {
            return Shuffle<T>(source).Take(num).ToArray();
        }

        public static T[] Shuffle<T>(T[] items)
        {
            for (int i = items.Length - 1; i > 0; i--)
            {
                int j = RNG.Next(i + 1);
                (items[i], items[j]) = (items[j], items[i]);
            }

            return items;
        }
        public static string PresentAsOptions<T>(T[] options, Dictionary<T, string> nameLookup, out string lastOptionsString)
        {
            string optionsString = "";
            for (int i = 0; i < options.Length; i++)
            {
                optionsString += "[" + (i + 1) + "]" + " " + nameLookup[options[i]] + (i == options.Length - 1 ? "" : ", ");
            }

            lastOptionsString = optionsString;
            return optionsString;
        }

        public static string PresentAsOptions<T>(T[] options, Func<T, string> nameLookup, out string lastOptionsString)
        {
            string optionsString = "";
            for (int i = 0; i < options.Length; i++)
            {
                optionsString += "[" + (i + 1) + "]" + " " + nameLookup(options[i]) + (i == options.Length - 1 ? "" : ", ");
            }

            lastOptionsString = optionsString;
            return optionsString;
        }
    }
}
