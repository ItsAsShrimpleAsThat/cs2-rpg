using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public enum Type
    {
        Earth,
        Fire,
        Poison,
        Water,
        Neutral
    }

    public static class Types
    {
        public static Type[] TypesInDests(Destination dest)
        {
            return dest switch
            {
                Destination.Forest => new Type[] { Type.Earth, Type.Poison },
                Destination.Desert => new Type[] { Type.Earth, Type.Fire },
                Destination.Grassland => new Type[] { Type.Poison, Type.Fire },
                Destination.Mountain => new Type[] { Type.Earth, Type.Water },
                Destination.Lake => new Type[] { Type.Water, Type.Fire },
                Destination.Swamp => new Type[] { Type.Water, Type.Poison },
                _ => throw new NotImplementedException()
            };
        }
    }
}
