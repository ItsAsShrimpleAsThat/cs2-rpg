using cs2_rpg.csinterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg
{
    public static class Constants
    {
        public static readonly string csLogPath = @"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\game\csgo\console.log";
        public static string cfgSubfolder = "cs2rpg";
        public static readonly string csCFGPath = @"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\game\csgo\cfg\" + cfgSubfolder + "\\";

        public static readonly string loopCFGFilename = @"looper";
        public static readonly string chatCFGFilename = @"sendchat";

        public static readonly int loopDeltaTime = 150;
        public static readonly int numLoopFiles = 10;
        public static readonly int numLoopRepeatsPerFile = 70_000;
        public static readonly string loopMacro = "l";
        public static readonly string loopStartExecName = "startbot";
        public static readonly string asyncStarterFile = "loopasyncstarterDONOTRUN";
    }
}
