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
        public Items id;
        public string description;
        public Action<Player> effect;

        public Item(Items id, string name, string description, Action<Player> effect) 
        {
            this.name = name;
            this.description = description; 
            this.effect = effect;
            this.id = id;
        }
    }
}
