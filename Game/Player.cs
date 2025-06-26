using CounterStrike2GSI.Nodes;
using cs2_rpg.csinterop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public class Player : Actor
    {
        public string username;
        public PlayerState playerState;
        public bool isAwaitingOption = false;
        public int maxAwaitingOption = -1;
        public int xp = 5;
        public int money = 5;
        public Action<int>? optionCallback;
        private int[] optionsIDs = { };
        private Enemy? currentEnemy;
        private int livesRemaining = 2;
        private double deathMoneyMultiplier = 0.5;
        private int inventorySize = 6;
        private Dictionary<Item, int> inventory = new Dictionary<Item, int>();

        public Player(string username) : base(GameConstants.baseHealth, GameConstants.baseHealth, GameConstants.baseDefense, 5, Type.Neutral, new BattleActions[5] { BattleActions.Strike, BattleActions.Focus, BattleActions.Sting, BattleActions.Defend, BattleActions.UseItem })
        {
            this.username = username;
            this.playerState = PlayerState.Free;

            this.maxHP = (int)(XP.XPtoHP(xp));
            this.health = this.maxHP;
            this.defense = (int)(XP.XPtoDefense(xp));
        }

        private Random random = new Random();

        public void Explore(int option)
        {
            int pickedDestID = optionsIDs[option];
            Destination pickedDestination = (Destination)pickedDestID;
            string destName = Destinations.DestinationToName(pickedDestination);

            if (random.Next(0, 2) == 0)
            {
                Enemy enemy = MakeEnemy(pickedDestination);
                ChatSender.SendChatMessage("You explored the " + destName + " and encountered " + enemy.WithIndefiniteArticle() + "!", username);
                StartBattle(enemy);
            }
            else
            {
                ChatSender.SendChatMessage("You explored the " + destName + " and found a [item]", username);
                playerState = PlayerState.Free;
            }
        }

        public Destination[] GetExplorationOptions()
        {
            Destination[] destinations = PickNRandomElementsFromArray<Destination>(GameConstants.allDestinations, 3);
            StartAwaitingOptions(destinations);
            optionsIDs = destinations.Select(d=>(int)d).ToArray();

            return destinations;
        }

        public void DoBattleOption(int option)
        {
            BattleActions action = battleActions[option];

            DoBattleOption(action, currentEnemy, username, WonBattle, EnemysTurn, true);
        }

        public void EnemysTurn()
        {
            if (currentEnemy != null)
            {
                currentEnemy.DoBattleOption(currentEnemy.GetRandomBattleaction(0.0), this, username, LostBattle, StartPlayersTurn, false);
            }
        }
        
        public void WonBattle()
        {
            if (currentEnemy != null)
            {
                int moneyEarned = (int)((currentEnemy.xp * GameConstants.moneyWinScale + GameConstants.baseMoneyReward) * ((((random.NextDouble() * 2.0) - 1.0) * GameConstants.moneyRewardVariance) + 1.0));
                int xpEarned = (int)((currentEnemy.xp * GameConstants.xpWinScale + GameConstants.baseXPReward) * ((((random.NextDouble() * 2.0) - 1.0) * GameConstants.xpRewardVariance) + 1.0));
                ChatSender.SendChatMessage("You successfully defeated the " + currentEnemy.name + "! " + " You earned $" + moneyEarned.ToString() + " and gained " + xpEarned + " xp!", username);

                int oldXP = xp;

                money += moneyEarned;
                xp += xpEarned;

                int oldMaxHP = maxHP;
                maxHP = XP.XPtoHP(xp);
                health += maxHP - oldMaxHP; // Heal player by the amount of HP they gained

                int preRewardHeal = health;
                health += maxHP >> 1; // Heal 50%;

                health = Math.Min(health, maxHP); // make sure hp doesnt go over maxhp

                ChatSender.SendChatMessage("Your stats improved! XP: " + oldXP + "→" + xp + ", Max Health: " + oldMaxHP + "→" + maxHP + ", and you regenerated " + (health - preRewardHeal) + " HP! You are currently at " + health + "/" + maxHP + " HP and " + defense + " defense!", username);

                currentEnemy = null;
                playerState = PlayerState.Free;
                StopAwaitingOptions();
            }
        }

        public void LostBattle()
        {
            if(livesRemaining > 0)
            {
                livesRemaining--;
                //TODO: different lives remaining = different death messages, like 1 life says "be careful"
                ChatSender.SendChatMessage("You died! Respawning, but you have " + livesRemaining + (livesRemaining == 1 ? " life remaining." : " lives remaining."), username);

                money = (int)(money * deathMoneyMultiplier);
                inventory.Clear();
                health = maxHP;
            }
            else
            {
                ChatSender.SendChatMessage("Game Over! You cannot respawn. Type !rpg to restart", username);
                GameOver();
            }

            currentEnemy = null;
            playerState = PlayerState.Free;
            StopAwaitingOptions();
        }

        public void GameOver()
        {

        }

        public void StartPlayersTurn()
        {
            ChatSender.SendChatMessage(GetBattleVs(currentEnemy), username);
            ChatSender.SendChatMessage("It's your turn. " + PresentAsOptions<BattleActions>(battleActions, BattleAction.BattleActionToName), username);
            StartAwaitingOptions(battleActions);
            optionCallback = DoBattleOption;
        }

        public void StartAwaitingOptions<T>(T[] options)
        {
            isAwaitingOption = true;
            maxAwaitingOption = options.Length;
        }

        public void StopAwaitingOptions()
        {
            isAwaitingOption = false;
            maxAwaitingOption = -1;
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

        public string PresentAsOptions<T>(T[] options, Func<T, string> nameLookup)
        {
            string optionsString = "";
            for (int i = 0; i < options.Length; i++)
            {
                optionsString += "[" + (i + 1) + "]" + " " + nameLookup(options[i]) + (i == options.Length - 1 ? "" : ", ");
            }

            return optionsString;
        }

        public void StartBattle(Enemy enemy)
        {
            playerState = PlayerState.InBattle;
            currentEnemy = enemy;

            //if (random.Next(0, 2) == 0)
            //TODO: remove this
            if (random.Next(0, 1) == 0)
            {
                StartPlayersTurn();
            }
            else
            {
                // move to start enemy's turn method
                ChatSender.SendChatMessage("It's the enemy's turn. They did something, i don't know what they did yet because I haven't implemented this yet", username);
            }
        }

        private Enemy MakeEnemy(Destination dest)
        {
            Type enemyType = Types.TypesInDests(dest)[random.Next(0, 2)];
            EnemyPrefab prefab = Enemies.GetRandomPrefabFromType(enemyType);

            int enemyXP = Enemies.PlayerXPtoEnemyXP(xp);
            int enemyHP = (int)(XP.XPtoHP(enemyXP) * (1.0 + prefab.hpVariance * (random.NextDouble() * 2 - 1)));
            int enemyDefense = (int)(XP.XPtoDefense(enemyXP) * (1.0 + prefab.defenseVariance * (random.NextDouble() * 2 - 1)));
            return new Enemy(prefab.name, prefab.type, enemyHP, enemyHP, enemyDefense, enemyXP, prefab.indefiniteArticle, prefab.battleActions);
        }

        public void FindItem()
        {

        }

        private string GetBattleVs(Enemy enemy)
        {
            return enemy.name + " [lvl: " + XP.XPToLevel(enemy.xp) + " | health: " + enemy.health.ToString() + "/" + enemy.maxHP.ToString() + " | type: " + enemy.type + "] ---ＶＳ--- " + username + " [ lvl: " + XP.XPToLevel(xp) + " | health: " + health.ToString() + "/" + maxHP.ToString() + "]";
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
