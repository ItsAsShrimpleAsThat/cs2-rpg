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
        public int money = 5;
        public Action<int>? optionCallback;
        private int[] optionsIDs = { };
        private Enemy? currentEnemy;
        private int livesRemaining = 2;
        private double deathMoneyMultiplier = 0.5;
        public int inventorySize = 6;
        public Dictionary<Items, int> inventoryCounts = new();
        public List<Item> inventory = new();

        public Player(string username) : base(GameConstants.baseHealth, GameConstants.baseHealth, GameConstants.baseDefense, 5, Type.Neutral, new BattleActions[5] { BattleActions.Strike, BattleActions.Focus, BattleActions.Sting, BattleActions.Defend, BattleActions.UseItem })
        {
            this.username = username;
            this.playerState = PlayerState.Free;

            this.maxHP = (int)(XP.XPtoHP(xp));
            this.health = this.maxHP;
            this.defense = (int)(XP.XPtoDefense(xp));
        }

        public void Explore(int option)
        {
            int pickedDestID = optionsIDs[option];
            Destination pickedDestination = (Destination)pickedDestID;
            string destName = Destinations.DestinationToName(pickedDestination);

            if (RNG.Next(0, 1) == 1)
            {
                Enemy enemy = MakeEnemy(pickedDestination);
                ChatSender.SendChatMessage("You explored the " + destName + " and encountered " + enemy.WithIndefiniteArticle() + "!", username);
                StartBattle(enemy);
            }
            else
            {
                Item foundItem = Item.RandomItem();
                ItemGiveResult giveResult = Item.CanGiveItem(this, foundItem);

                if (giveResult == ItemGiveResult.Success)
                {
                    ChatSender.SendChatMessage("You explored the " + destName + " and found a " + foundItem.name, username);
                }
                else if (giveResult == ItemGiveResult.InventoryFull)
                {
                    ChatSender.SendChatMessage("Couldn't pick up the " + foundItem.name + " you found because your inventory is full", username);
                }
                else if (giveResult == ItemGiveResult.ItemStackFull)
                {
                    ChatSender.SendChatMessage("Couldn't pick up the " + foundItem.name + " you found because you hit the max stack size (" + foundItem.maxStackSize + ")", username);
                }

                playerState = PlayerState.Free;
            }
        }

        public Destination[] GetExplorationOptions()
        {
            Destination[] destinations = Options.PickNRandomElementsFromArray<Destination>(GameConstants.allDestinations, 3);
            StartAwaitingOptions(destinations);
            optionsIDs = destinations.Select(d=>(int)d).ToArray();

            return destinations;
        }

        public void PlayerTurn(int option)
        {
            BattleActions action = battleActions[option];
            BattleActionType type = BattleAction.GetBattleActionType(action);

            if (type == BattleActionType.UseItem)
            {
                if (inventory.Count > 0)
                {
                    ChatSender.SendChatMessage("Which item would you like to use? " + Options.PresentAsOptions<Item>(inventory.ToArray(), (Item item) => item.name + " (" + inventoryCounts[item.id] + ")"), username);
                    StartAwaitingOptions(inventory.ToArray());
                    optionCallback = UseItem;
                }
                else
                {
                    ChatSender.SendChatMessage("You have no items.", username);
                    StartPlayersTurn();
                }
            }
            else
            {
                DoBattleOption(action, BattleAction.GetBattleActionType(action), currentEnemy, username, WonBattle, EnemysTurn, true);
            }
        }

        public void EnemysTurn()
        {
            if (currentEnemy != null)
            {
                BattleActions chosenAction = currentEnemy.GetRandomBattleaction(0.0);
                currentEnemy.DoBattleOption(chosenAction, BattleAction.GetBattleActionType(chosenAction), this, username, LostBattle, StartPlayersTurn, false);
            }
        }

        public void UseItem(int option)
        {
            Item itemToUse = inventory[option];
            inventoryCounts[itemToUse.id]--;

            if (inventoryCounts[itemToUse.id] <= 0)
            {
                inventory.RemoveAt(option);
                inventoryCounts.Remove(itemToUse.id);
            }

            ChatSender.SendChatMessage("You used " + itemToUse.name + ".", username);
            itemToUse.effect(this);

            EnemysTurn();
        }

        public void GiveItem(Item item)
        {
            if(inventory.Contains(item))
            {
                inventoryCounts[item.id]++;
            }
            else
            {
                inventory.Add(item);
                inventoryCounts.Add(item.id, 1);
            }
        }
        
        public void WonBattle()
        {
            if (currentEnemy != null)
            {
                int moneyEarned = (int)((currentEnemy.xp * GameConstants.moneyWinScale + GameConstants.baseMoneyReward) * ((RNG.NextDouble_n1_1() * GameConstants.moneyRewardVariance) + 1.0));
                int xpEarned = (int)((currentEnemy.xp * GameConstants.xpWinScale + GameConstants.baseXPReward) * ((RNG.NextDouble_n1_1() * GameConstants.xpRewardVariance) + 1.0));
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
            ChatSender.SendChatMessage("It's your turn. " + Options.PresentAsOptions<BattleActions>(battleActions, BattleAction.BattleActionToName), username);
            StartAwaitingOptions(battleActions);
            optionCallback = PlayerTurn;
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

        public void StartBattle(Enemy enemy)
        {
            playerState = PlayerState.InBattle;
            currentEnemy = enemy;

            //if (random.Next(0, 2) == 0)
            //TODO: remove this
            if (RNG.Next(0, 1) == 0)
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
            Type enemyType = Types.TypesInDests(dest)[RNG.Next(0, 2)];
            EnemyPrefab prefab = Enemies.GetRandomPrefabFromType(enemyType);

            int enemyXP = Enemies.PlayerXPtoEnemyXP(xp);
            int enemyHP = (int)(XP.XPtoHP(enemyXP) * (1.0 + prefab.hpVariance * RNG.NextDouble_n1_1()));
            int enemyDefense = (int)(XP.XPtoDefense(enemyXP) * (1.0 + prefab.defenseVariance * RNG.NextDouble_n1_1()));
            return new Enemy(prefab.name, prefab.type, enemyHP, enemyHP, enemyDefense, enemyXP, prefab.indefiniteArticle, prefab.battleActions);
        }

        private string GetBattleVs(Enemy enemy)
        {
            return enemy.name + " [lvl: " + XP.XPToLevel(enemy.xp) + " | health: " + enemy.health.ToString() + "/" + enemy.maxHP.ToString() + " | type: " + enemy.type + "] ---ＶＳ--- " + username + " [ lvl: " + XP.XPToLevel(xp) + " | health: " + health.ToString() + "/" + maxHP.ToString() + "]";
        }

        
    }
}
