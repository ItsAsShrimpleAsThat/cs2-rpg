using System;
using System.Text;

namespace cs2_rpg.csinterop
{
    public static class ChatListener
    {
        public static void ParseMessageFromConsole(Action<ChatMessage> callback)
        {
            using var fs = new FileStream(Constants.csLogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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
                        try
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

                            callback(new ChatMessage(username, message));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in parsing chat message: " + ex.Message);
                            Console.WriteLine("Raw Console line is:");
                            Console.WriteLine(line);
                        }
                    }
                }
            }
        }
    }
}
