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
            Console.WriteLine("Creating Looper CFG Files...");
            CFG.CreateLooperCFGs();

            Thread listenerThread = new Thread(() => ChatListener.ParseMessageFromConsole(HandleMessage));
            listenerThread.IsBackground = true;
            listenerThread.Start();
            Console.WriteLine("Started Listening to CS2 Chat...");

            Thread senderThread = new Thread(ChatSender.StartMessageSender);
            senderThread.IsBackground = true;
            senderThread.Start();

            Thread.Sleep(500);
            ChatSender.SendChatMessage("CS2 RPG is active. Type !rpg to join.");

            Console.WriteLine("Finished starting. Press Enter to stop cleanly");
            Console.ReadLine();
            CFG.ClearMessage();
        }

        static void HandleMessage(ChatMessage message)
        {
            if (false)
            {
                Console.WriteLine("=== MESSAGE RECIEVED! ===");
                Console.WriteLine("Username: " + message.author);
                Console.WriteLine("Message: " + message.message);
            }

            ChatSender.CheckIfMessageSent(message.message);
            Game.Main.RecieveInput(message);
        }
    }
}
