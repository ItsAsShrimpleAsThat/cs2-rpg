using CounterStrike2GSI.Nodes;
using cs2_rpg.csinterop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public class Actor
    {
        public int health;
        public int maxHP;
        public int defense;
        public int xp;
        public Type type;
        public BattleActions[] battleActions;
        public List<Buff> activeBuffs = new List<Buff>();

        public Actor(int health, int maxHP, int defense, int xp, Type type, BattleActions[] battleActions)
        {
            this.health = health;
            this.maxHP = maxHP;
            this.defense = defense;
            this.xp = xp;
            this.type = type;
            this.battleActions = battleActions;
        }

        public void DoAttack(Attack attack, Actor opponent, Action attackerWonBattle, string username, bool isPlayer, ref bool doEnemysTurn)
        {
            if (attack != null)
            {
                List<Buff> buffsToRemove = new List<Buff>();
                foreach (Buff buff in activeBuffs)
                {
                    buff.ApplyBuff(ref attack);
                    Console.WriteLine("Applying buff: " + buff.name);

                    if (buff.ShouldRemoveBuff())
                    {
                        buffsToRemove.Add(buff);
                    }
                }

                (int dmgDealt, int newDefense, AttackEffectiveness effectiveness) = attack.CalculateDamageAndNewDefense(xp, opponent.defense, opponent.type);
                opponent.health -= dmgDealt;
                opponent.defense = newDefense;

                if (isPlayer)
                {
                    ChatSender.SendChatMessage("You used " + attack.name + "! " + GameConstants.attackEffectivenessDialogue[effectiveness] + " →→→ Enemy is now at " + Math.Max(opponent.health, 0) + "/" + opponent.maxHP + " HP and " + opponent.defense + " defense.", username);
                }
                else
                {
                    ChatSender.SendChatMessage("Enemy used " + attack.name + "! " + GameConstants.attackEffectivenessDialogue[effectiveness] + " →→→ You are now at " + Math.Max(opponent.health, 0) + "/" + opponent.maxHP + " HP and " + opponent.defense + " defense.", username);
                }

                foreach (Buff toRemove in buffsToRemove)
                {
                    activeBuffs.Remove(toRemove);
                    ChatSender.SendChatMessage(toRemove.name + " wore out!", username);
                }

                if (opponent.health <= 0)
                {
                    doEnemysTurn = false;
                    attackerWonBattle();
                }
            }
        }




        public void DoBattleOption(BattleActions action, Actor opponent, string username, Action attackerWonBattle, Action opponentsTurn, bool isPlayer)
        {
            bool doEnemysTurn = true;

            if (opponent != null)
            {
                BattleActionType? type = BattleAction.GetBattleActionType(action);

                if (type == BattleActionType.UseItem)
                {
                    ChatSender.SendChatMessage("What item would you like to use? Just kidding bitch, I haven't implemented this", username);
                }
                else if (type == BattleActionType.Attack)
                {
                    Attack attack = GameConstants.battleAction2Attack[action];
                    DoAttack(attack, opponent, attackerWonBattle, username, isPlayer, ref doEnemysTurn);
                }
                else if (type == BattleActionType.SelfBuff)
                {
                    Buff addedBuff = GameConstants.battleAction2Buff[action];

                    activeBuffs.Add(addedBuff);
                    if (isPlayer)
                    {
                        ChatSender.SendChatMessage("You used " + BattleAction.BattleActionToName(action) + ", which gave you the " + addedBuff.name + " buff!", username);
                    }
                    else
                    {
                        ChatSender.SendChatMessage("Enemy used " + BattleAction.BattleActionToName(action) + ", which gave it the " + addedBuff.name + " buff!", username);
                    }
                }
                else if (type == BattleActionType.StatusEffect)
                {
                    
                }
                else if (type == BattleActionType.Defend)
                {

                }

                if (doEnemysTurn)
                {
                    opponentsTurn();
                }
            }
        }
    }
}
