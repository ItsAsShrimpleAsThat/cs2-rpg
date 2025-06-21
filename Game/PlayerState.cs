using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public enum PlayerState
    {
        Free,
        Exploring,
        InBattle,
        InShop,
        Dead
    }

    public static class PlayerStates
    {
        public static string PlayerStateToName(PlayerState state)
        {
            return state switch
            {
                PlayerState.Free => "Free",
                PlayerState.Exploring => "Exploring",
                PlayerState.InBattle => "In a battle",
                PlayerState.InShop => "In a shop",
                PlayerState.Dead => "Dead",
                _ => throw new NotImplementedException()
            };
        }
    }
}
