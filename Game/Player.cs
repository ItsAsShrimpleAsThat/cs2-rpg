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
    class Player
    {
        public string username;
        public PlayerState playerState;
        public bool isAwaitingOption = false;
        public int maxAwaitingOption = -1;
        public int xp = 5;
        public int money = 5;
        public int health = GameConstants.baseHealth;
        public int maxHP = GameConstants.baseHealth;
        public int defense = GameConstants.baseDefense;
        public Action<int>? optionCallback;
        private int[] optionsIDs = { };
        private BattleActions[] battleActions = new BattleActions[5] { BattleActions.Strike, BattleActions.Focus, BattleActions.Sting, BattleActions.Defend, BattleActions.UseItem };
        private Enemy? currentEnemy;
        private List<Buff> activeBuffs = new List<Buff>();

        public Player(string username)
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
            string destName = GameConstants.dest2Name[pickedDestination];

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

            if (currentEnemy != null) // To make the warnings go away
            {
                BattleActionType? type = BattleAction.GetBattleActionType(action);

                if (type == BattleActionType.UseItem)
                {
                    ChatSender.SendChatMessage("What item would you like to use? Just kidding bitch, I haven't implemented this", username);
                }
                else if (type == BattleActionType.Attack)
                {
                    Attack attack = GameConstants.battleAction2Attack[action];

                    if (attack != null)
                    {
                        // THIS IS NOT DONE, BUT I WANNA GO TO SLEEP SO THIS IS WHAT WE'RE LEAVING IT AT TONIGHT
                        (int dmgDealt, int newDefense, AttackEffectiveness effectiveness) = attack.CalculateDamageAndNewDefense(xp, currentEnemy.defense, currentEnemy.type);
                        currentEnemy.health -= dmgDealt;
                        currentEnemy.defense = newDefense;

                        ChatSender.SendChatMessage("You used " + attack.name + "! " + GameConstants.attackEffectivenessDialogue[effectiveness] + " →→→ Enemy is now at " + currentEnemy.health + "/" + currentEnemy.maxHP + " HP and " + currentEnemy.defense + " defense.", username);

                        if(currentEnemy.health <= 0)
                        {
                            WonBattle();
                        }

                        EnemysMove();
                    }
                }
                else if (type == BattleActionType.SelfBuff)
                {
                    
                }
                else if (type == BattleActionType.StatusEffect)
                {

                }
                else if (type == BattleActionType.Defend)
                {

                }
            }
        }

        public void EnemysMove()
        {
            Attack chosenAttack = currentEnemy.GetRandomAttack(0.0);

            (int dmgDealt, int newDefense, AttackEffectiveness effectiveness) = chosenAttack.CalculateDamageAndNewDefense(currentEnemy.xp, defense, Type.Neutral);

            health -= dmgDealt;
            defense = newDefense;

            ChatSender.SendChatMessage("Enemy used " + chosenAttack.name + "! " + GameConstants.attackEffectivenessDialogue[effectiveness] + " →→→ You are now at " + health + "/" + maxHP + " HP and " + defense + " defense.", username);

            StartPlayersTurn();
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

        public void StartPlayersTurn()
        {
            ChatSender.SendChatMessage(GetBattleVs(currentEnemy), username);
            ChatSender.SendChatMessage("It's your turn. " + PresentAsOptions<BattleActions>(battleActions, GameConstants.battleAction2Name), username);
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
            Type enemyType = GameConstants.typesInDests[dest][random.Next(0, 2)];
            EnemyPrefab prefab = Enemies.GetRandomPrefabFromType(enemyType);

            int enemyXP = Enemies.PlayerXPtoEnemyXP(xp);
            int enemyHP = (int)(XP.XPtoHP(enemyXP) * (1.0 + prefab.hpVariance * (random.NextDouble() * 2 - 1)));
            int enemyDefense = (int)(XP.XPtoDefense(enemyXP) * (1.0 + prefab.defenseVariance * (random.NextDouble() * 2 - 1)));
            return new Enemy(prefab.name, prefab.type, enemyHP, enemyHP, enemyDefense, enemyXP, prefab.indefiniteArticle, prefab.battleActions);
        }
        
        public void MyTurn(Enemy enemy)
        {
            
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
