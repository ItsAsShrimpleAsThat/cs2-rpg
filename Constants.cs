using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg
{
    public static class Constants
    {
        public static readonly string csLogPath = @"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\game\csgo\console.log";
        public static readonly string csCFGPath = @"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\game\csgo\cfg\";

        public static readonly string loopCFGFilename = @"chatloop";
        public static readonly string chatCFGFilename = @"sendchat";
        public static readonly int loopSleepTime = 150;

        public static readonly string[] loopCFGContents = { "echo test",
                                                            "sleep " + loopSleepTime.ToString(),
                                                            "exec_async cs2rpg/" + loopCFGFilename };
    }
}
