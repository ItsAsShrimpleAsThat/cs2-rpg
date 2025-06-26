using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public class Item
    {
        public string name;
        public string description;
        public Action<Player> effect;
    }
}
