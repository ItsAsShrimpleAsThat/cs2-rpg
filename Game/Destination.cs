using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public enum Destination : int
    {
        Forest = 0,
        Desert = 1,
        Grassland = 2,
        Mountain = 3,
        Lake = 4,
        Swamp = 5
    }

    public static class Destinations
    {
        public static string DestinationToName(Destination dest)
        {
            return dest switch
            {
                Destination.Forest => "Forest",
                Destination.Desert => "Desert",
                Destination.Grassland => "Grassland",
                Destination.Mountain => "Mountain",
                Destination.Lake => "Lake",
                Destination.Swamp => "Swamp",
                 _ => throw new NotImplementedException()
            };
        }
    }
}
