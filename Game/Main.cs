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
                    string cmd = splittedMsg[0];

                    if (GameConstants.allCommands.Contains(cmd))
                    {
                        if (players.TryGetValue(message.author, out player))
                        {
                            // Player has joined playing right now so process their command
                            switch (splittedMsg[0].ToLower())
                            {
                                // ========== Start Game ========== \\
                                case "!rpg":
                                    ChatSender.SendChatMessage("You have already joined CS2 RPG. Type !help for a list of commands", player.username);
                                    break;

                                // ==========   Explore   ========== \\
                                case "!explore":
                                    if (player.playerState == PlayerState.Free)
                                    {
                                        player.playerState = PlayerState.Exploring;
                                        Destination[] destinations = player.GetExplorationOptions();
                                        ChatSender.SendChatMessage("Where would you like to explore? Respond with !option #. " + Options.PresentAsOptions(destinations, Destinations.DestinationToName), player.username);
                                        player.optionCallback = player.Explore;
                                    }
                                    else
                                    {
                                        ChatSender.SendChatMessage("You cannot start an exploration while you are currently " + PlayerStates.PlayerStateToName(player.playerState).ToLower() + "!", player.username);
                                    }
                                    break;

                                case "!givemed":
                                    Console.WriteLine("HELLO I AM GIVING MEDKIT ALERT ALERT");
                                    player.GiveItem(GameConstants.itemEnum2Item[Items.Medkit]);
                                    break;

                                case "!option": 
                                case "!opt":
                                    if (player.isAwaitingOption)
                                    {
                                        int pickedOption = -1;
                                        if (int.TryParse(splittedMsg[1], out pickedOption))
                                        {
                                            if (pickedOption > 0 && pickedOption < player.maxAwaitingOption + 1)
                                            {
                                                player.StopAwaitingOptions();

                                                if (player.optionCallback != null)
                                                {
                                                    player.optionCallback(pickedOption - 1);
                                                }
                                            }
                                            else
                                            {
                                                ChatSender.SendChatMessage("Your option must be between 1 and " + player.maxAwaitingOption + " inclusive.", player.username);
                                            }
                                        }
                                        else
                                        {
                                            ChatSender.SendChatMessage("Your option is not a valid number.", player.username);
                                        }
                                    }   
                                    else
                                    {
                                        ChatSender.SendChatMessage("You haven't been asked to choose an option.", player.username);
                                    }
                                    break;
                                case "!help":
                                    ChatSender.SendChatMessage("Here are all commands in CS2 RPG. !rpg - join the rpg (you've already done this!) | !help - this command | !explore - begin an exploration to find an item or fight an enemy");
                                    ChatSender.SendChatMessage("!option [#] or !opt [#] - select an option if you've been prompted to");
                                    break;
                            }
                        }
                        else
                        {
                            if (splittedMsg[0].ToLower() == "!rpg")
                            {
                                // They asked to join game, so add them to the game :)
                                players.Add(message.author, new Player(message.author));
                                ChatSender.SendChatMessage("You have successfuly joined CS2 RPG. Type !help for a list of commands", message.author);
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
