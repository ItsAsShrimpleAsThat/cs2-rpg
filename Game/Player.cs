using CounterStrike2GSI.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    class Player
    {
        public string username;
        public bool isAwaitingOption = false;
        public int maxAwaitingOption = -1;
        public int xp = 0;
        public Action<int> optionCallback;

        public Player(string username)
        {
            this.username = username;
        }

        private Random random = new Random();

        public void Explore()
        {
            if(random.Next(0, 2) == 0)
            {
                
            }
        }

        public Destination[] GetExplorationOptions()
        {
            isAwaitingOption = true;
            maxAwaitingOption = 3;
            return PickNRandomElementsFromArray<Destination>(GameConstants.allDestinations, 3);
        }

        public string PresentAsOptions<T>(T[] options, Dictionary<T, string> nameLookup)
        {
            string optionsString = "";
            for(int i = 0; i < options.Length; i++)
            {
                optionsString += "[" + (i + 1) + "]" + " " + nameLookup[options[i]] + (i == options.Length - 1 ? "" : ", ");
            }

            return optionsString;
        }

        public void StartBattle()
        {

        }

        public void FindItem()
        {

        }
        
        public T[] PickNRandomElementsFromArray<T>(T[] source, int num)
        {
            return Shuffle<T>(source).Take(num).ToArray();
        }

        public T[] Shuffle<T>(T[] items)
        {
            for (int i = items.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (items[i], items[j]) = (items[j], items[i]);
            }

            return items;
        }
    }
}
