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
        private static readonly string sendchatcfgpath = Path.Combine([Constants.csCFGPath, Constants.chatCFGFilename + ".cfg"]);
        private static readonly string tempsendchatcfgpath = Path.Combine([Constants.csCFGPath, Constants.chatCFGFilename + "temp" + ".cfg"]);

        public static void WriteCFGFile(string name, string[] contents)
        {
            try
            {
                Directory.CreateDirectory(Constants.csCFGPath);
                File.WriteAllLines(Path.Combine([Constants.csCFGPath, name + ".cfg"]), contents);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in creating CFG: " + ex);
            }
        }

        public static void CreateLooperCFGs()
        {
            Directory.CreateDirectory(Constants.csCFGPath);

            long accumulatedDelay = 0;
            for (int i = 0; i < Constants.numLoopFiles; i++)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine([Constants.csCFGPath, Constants.loopCFGFilename + i.ToString() + ".cfg"])))
                {
                    if (i == 0)
                    {
                        sw.WriteLine("alias \"" + Constants.loopMacro + "\" \"exec " + Constants.cfgSubfolder + "/" + Constants.chatCFGFilename + "\"");
                    }

                    sw.WriteLine("sleep " + accumulatedDelay.ToString());

                    for (int repeat = 0; repeat < Constants.numLoopRepeatsPerFile; repeat++)
                    {
                        sw.WriteLine("sleep " + Constants.loopDeltaTime);
                        sw.WriteLine(Constants.loopMacro);
                        accumulatedDelay += Constants.loopDeltaTime;
                    }
                }

                accumulatedDelay += Constants.loopDeltaTime;

                Console.WriteLine("Finished writing " + (i + 1).ToString() + "/" + Constants.numLoopFiles.ToString() + " loop CFG files");
            }

            using (StreamWriter sw = new StreamWriter(Path.Combine([Constants.csCFGPath, Constants.loopStartExecName + ".cfg"])))
            {
                sw.WriteLine("sv_cheats true");
                sw.WriteLine("exec_async " + Constants.cfgSubfolder + "/" + Constants.asyncStarterFile);
            }

            using (StreamWriter sw = new StreamWriter(Path.Combine([Constants.csCFGPath, Constants.asyncStarterFile + ".cfg"])))
            {
                for (int i = 0; i < Constants.numLoopFiles; i++)
                {
                    sw.WriteLine("exec_async " + Constants.cfgSubfolder + "/" + Constants.loopCFGFilename + i.ToString());
                }
            }

            using (StreamWriter sw = new StreamWriter(sendchatcfgpath))
            {
                sw.Write("");
            }

            Console.WriteLine("Finisehd writing CFG files");
            Console.WriteLine("Assuming 150fps average framerate, bot should run for " + (Constants.loopDeltaTime * Constants.numLoopRepeatsPerFile / 150.0 * Constants.numLoopFiles / 60.0 / 60.0).ToString() + " hours");
        }

        public static void SetMessage(string message)
        {
            using (StreamWriter sw = new StreamWriter(tempsendchatcfgpath))
            {
                sw.Write("say \"" + message + "\"");
            }
            File.Replace(tempsendchatcfgpath, sendchatcfgpath, null);
        }

        public static void ClearMessage()
        {
            using (StreamWriter sw = new StreamWriter(sendchatcfgpath))
            {
                sw.Write("");
            }
        }
    }
}
