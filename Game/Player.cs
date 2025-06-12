using CounterStrike2GSI.Nodes;
using cs2_rpg.csinterop;
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
        public PlayerState playerState;
        public bool isAwaitingOption = false;
        public int maxAwaitingOption = -1;
        public int xp = 0;
        public int health;
        public int maxHP;
        public Action<int>? optionCallback;
        private int[] optionsIDs = { };
        private BattleAction[] battleActions = new BattleAction[5];

        public Player(string username)
        {
            this.username = username;
            this.playerState = PlayerState.Free;
           
        }

        private Random random = new Random();

        public void Explore(int option)
        {
            int pickedDestID = optionsIDs[option - 1];
            Destination pickedDestination = (Destination)pickedDestID;
            string destName = GameConstants.dest2Name[pickedDestination];

            if (random.Next(0, 2) == 0)
            {
                ChatSender.SendChatMessage("You explored the " + destName + " and encountered a(/n) enemy!", username);
                StartBattle(new Enemy("Test Enemy", Type.Water, 10, 10, 50, 50, 350));
            }
            else
            {
                ChatSender.SendChatMessage("You explored the " + destName + " and found a [item]", username);
                playerState = PlayerState.Free;
            }
        }

        public Destination[] GetExplorationOptions()
        {
            isAwaitingOption = true;
            Destination[] destinations = PickNRandomElementsFromArray<Destination>(GameConstants.allDestinations, 3);
            maxAwaitingOption = destinations.Length;
            optionsIDs = destinations.Select(d=>(int)d).ToArray();

            return destinations;
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

        public void StartBattle(Enemy enemy)
        {
            ChatSender.SendChatMessage(GetBattleVs(enemy), username);
            playerState = PlayerState.InBattle;

            if (random.Next(0, 2) == 0)
            {
                ChatSender.SendChatMessage("It's your turn. What would you like to do? Respond with !option #. " + PresentAsOptions<BattleAction>(battleActions, GameConstants.battleAction2Name), username);
            }
            else
            {
                ChatSender.SendChatMessage("It's the enemy's turn. They did something, i don't know what they did yet because I haven't implemented this yet", username);
            }
        }

        public void MyTurn(Enemy enemy)
        {
            
        }

        public void FindItem()
        {

        }

        private string GetBattleVs(Enemy enemy)
        {
            return enemy.name + " [lvl: " + XP.XPToLevel(enemy.xp) + ", health: " + enemy.health.ToString() + "/" + enemy.maxHP.ToString() + ", type: " + enemy.type + "] ===vs=== " + username + " [ lvl: " + XP.XPToLevel(xp) + ", health: " + health.ToString() + "/" + maxHP.ToString() + "]";
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
