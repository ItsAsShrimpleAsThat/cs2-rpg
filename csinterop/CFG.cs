using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.csinterop
{
    public static class CFG
    {
        public static void WriteCFGFile(string name, string[] contents)
        {
            try
            {
                Directory.CreateDirectory(Path.Combine([Constants.csCFGPath, "cs2rpg"]));
                File.WriteAllLines(Path.Combine([Constants.csCFGPath, "cs2rpg", name + ".cfg"]), contents);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in creating CFG: " + ex);
            }
        }

        public static void CreateLooperCFG()
        {
            WriteCFGFile(Constants.loopCFGFilename, Constants.loopCFGContents);
        }
    }
}
