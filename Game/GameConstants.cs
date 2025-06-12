using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    internal static class GameConstants
    {
        public static Destination[] allDestinations = { Destination.Forest, Destination.Desert, Destination.Grassland, Destination.Mountain, Destination.Lake };
        public static Dictionary<Destination, string> dest2Name = new Dictionary<Destination, string>()
        {
            { Destination.Forest , "Forest" },
            { Destination.Desert , "Desert" },
            { Destination.Grassland , "Grassland" },
            { Destination.Mountain , "Mountain" },
            { Destination.Lake , "Lake" }
        };

        public static Dictionary<PlayerState, string> pstate2Name = new Dictionary<PlayerState, string>()
        {
            { PlayerState.Free , "Free" },
            { PlayerState.Exploring , "Exploring" },
            { PlayerState.InBattle , "In a battle" },
            { PlayerState.InShop , "In a shop" },
            { PlayerState.Dead , "Dead" },
        };

        public static string[] allCommands = { "!rpg", "!explore", "!help", "!option", "!givexp" };
    }
}
