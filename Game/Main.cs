using cs2_rpg.csinterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public static class Main
    {
        private static Player player = new Player("gabe newell");
        public static void RecieveInput(ChatMessage message)
        {
            if (message.message != null)
            {
                if (message.message.StartsWith("!"))
                {
                    string[] splittedMsg = message.message.Split(" ");
                    string responsePrefix = "@ " + message.author + " ";

                    switch (splittedMsg[0].ToLower())
                    {
                        case "!rpg":
                            ChatSender.SendChatMessage(responsePrefix + "Welcome to CS2 RPG! Type !explore to explore");
                            break;
                        case "!explore":
                            Destination[] destinations = player.GetExplorationOptions();
                            ChatSender.SendChatMessage(responsePrefix + "Where would you like to explore? Respond with !option #. " + player.PresentAsOptions(destinations, GameConstants.dest2Name));
                            break;
                        case "!option":
                            if(player.isAwaitingOption)
                            {
                                int pickedOption = -1;
                                if (int.TryParse(splittedMsg[1], out pickedOption))
                                {
                                    if (pickedOption > 0 && pickedOption < player.maxAwaitingOption)
                                    {
                                        
                                    }
                                }
                            }
                            else
                            {
                                ChatSender.SendChatMessage(responsePrefix + "You haven't been asked to choose an option.");
                            }
                            break;
                    }
                }
            }
        }
    }
}
