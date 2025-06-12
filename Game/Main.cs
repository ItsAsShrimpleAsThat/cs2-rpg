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
        private static Dictionary<string, Player> players = new Dictionary<string, Player>();
        public static void RecieveInput(ChatMessage message)
        {
            if (message.message != null)
            {
                if (message.message.StartsWith("!"))
                {
                    Player? player;

                    string[] splittedMsg = message.message.Split(" ");
                    string responsePrefix = "@ " + message.author + " ";
                    string cmd = splittedMsg[0];

                    if (GameConstants.allCommands.Contains(cmd))
                    {
                        if (players.TryGetValue(message.author, out player))
                        {
                            // Player has joined playing right now so process their command
                            switch (splittedMsg[0].ToLower())
                            {
                                case "!rpg":
                                    ChatSender.SendChatMessage(responsePrefix + "You have already joined CS2 RPG. Type !help for a list of commands");
                                    break;

                                case "!explore":
                                    if (player.playerState == PlayerState.Free)
                                    {
                                        player.playerState = PlayerState.Exploring;
                                        Destination[] destinations = player.GetExplorationOptions();
                                        ChatSender.SendChatMessage(responsePrefix + "Where would you like to explore? Respond with !option #. " + player.PresentAsOptions(destinations, GameConstants.dest2Name));
                                        player.optionCallback = player.Explore;
                                    }
                                    else
                                    {
                                        ChatSender.SendChatMessage(responsePrefix + "You cannot start an exploration while you are currently " + GameConstants.pstate2Name[player.playerState].ToLower() + "!");
                                    }
                                    break;

                                case "!givexp":
                                    Console.WriteLine("HELLO I AM GIVING XP ALERT ALERT");
                                    player.xp += 50;
                                    ChatSender.SendChatMessage(responsePrefix + "Your XP is " + player.xp);
                                    break;

                                case "!option":
                                    if (player.isAwaitingOption)
                                    {
                                        int pickedOption = -1;
                                        if (int.TryParse(splittedMsg[1], out pickedOption))
                                        {
                                            if (pickedOption > 0 && pickedOption < player.maxAwaitingOption + 1)
                                            {
                                                if(player.optionCallback != null)
                                                {
                                                    player.optionCallback(pickedOption);
                                                }
                                                
                                                player.isAwaitingOption = false;
                                                player.maxAwaitingOption = -1;
                                            }
                                            else
                                            {
                                                ChatSender.SendChatMessage(responsePrefix + "Your option must be between 1 and " + player.maxAwaitingOption + " inclusive.");
                                            }
                                        }
                                        else
                                        {
                                            ChatSender.SendChatMessage(responsePrefix + "Your option is not a valid number.");
                                        }
                                    }
                                    else
                                    {
                                        ChatSender.SendChatMessage(responsePrefix + "You haven't been asked to choose an option.");
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            if (splittedMsg[0].ToLower() == "!rpg")
                            {
                                // They asked to join game, so add them to the game :)
                                players.Add(message.author, new Player(message.author));
                                ChatSender.SendChatMessage(responsePrefix + "You have successfuly joined CS2 RPG. Type !help for a list of commands");
                                Console.WriteLine("All connected players are: " + PlayerlistToString());
                            }
                        }
                    }
                }
            }
        }

        private static string PlayerlistToString()
        {
            string plist = "";

            foreach (var (playername, _) in players)
            {
                plist += playername + ", ";
            }

            plist = plist.Substring(0, plist.Length - 2);

            return plist;
        }
    }
}
