using CounterStrike2GSI;
using CounterStrike2GSI.EventMessages;
using System.IO;
using System.Text;

namespace cs2_rpg
{
    class Program
    {
        private const string logPath = @"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\game\csgo\console.log";

        static void Main(string[] args)
        {
            using var fs = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var sr = new StreamReader(fs, Encoding.UTF8);

            fs.Seek(0, SeekOrigin.End);

            while (true)
            {
                string? line = sr.ReadLine();

                if (line == null)
                {
                    Thread.Sleep(100);
                    continue;
                }

                // filter any empty lines
                if (!string.IsNullOrWhiteSpace(line) && !line.Contains('\0'))
                {
                    // check if console line is chat message
                    if (line.Substring(16, 5) == "[ALL]")
                    {
                        // there's an invisible unicode U+200E character dividing the username from the message. thanks Pandaptable (nemmy) i stole this from you :3
                        string[] parts = line.Substring(22, line.Length - 22).Split("\u200e");

                        string username = parts[0];
                        string message;

                        if (parts[1].Substring(0, 1) == ":")
                        {
                            message = parts[1].Substring(2, parts[1].Length - 2);
                        }
                        else
                        {
                            message = parts[1].Substring(9, parts[1].Length - 9);
                        }

                        Console.WriteLine("Full Line: " + line);
                        Console.WriteLine("Username: " + username);
                        Console.WriteLine("Message: " + message);
                    }
                }
            }
        }
    }
}
