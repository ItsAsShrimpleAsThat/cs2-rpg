using CounterStrike2GSI;
using CounterStrike2GSI.EventMessages;
using cs2_rpg.csinterop;
using System.IO;
using System.Text;
using System.Threading;

namespace cs2_rpg
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Thread listenerThread = new Thread(() => ChatListener.ParseMessageFromConsole(HandleMessage));
            listenerThread.IsBackground = true;
            listenerThread.Start();
            Console.WriteLine("Started Listening to CS2 Chat...");

            Console.WriteLine("Finished starting. Press Enter to stop cleanly");
            Console.ReadLine();
        }

        static void HandleMessage(ChatMessage message)
        {
            Console.WriteLine("Username: " + message.author);
            Console.WriteLine("Message: " + message.message);
        }
    }
}
