using System.Windows;

namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///      An object that the different types of actions in which a player or dealer can have in a Blackjack game.        
    /// </summary>
    public class Actions
    {
        // The player actions that can be perform in Blackjack
        public enum PlayerActionEnum
        {
            S,
            H,
            D,
            P,
            R,
            BUST
        };

        // The dealers actions that can be perform in Blackjack
        public enum DealerActionEnum
        {
            S,
            H,
            BUST
        };

        // The actions that can be entered into the Hard/Soft values of the Player Strategy.
        public enum PlayerActionEnum_HS
        {
            S,
            H,
            D,
            R
        }

        // The actions that can be entered into the Pair values of the Player Strategy.
        public enum PlayerActionEnum_Pair
        {
            S,
            H,
            D,
            R,
            P
        }

        // Convert a given action to a string for strategy table representation.
        public string convertActionToCode(PlayerActionEnum PlayerActionEnum)
        {
            switch (PlayerActionEnum)
            {
                case PlayerActionEnum.S:
                    return "S";
                case PlayerActionEnum.H:
                    return "H";
                case PlayerActionEnum.P:
                    return "P";
                case PlayerActionEnum.D:
                    return "D";
                case PlayerActionEnum.R:
                    return "R";
                default:
                    MessageBox.Show("ERROR DETECTED: Problem with Blackjack PlayerActions");
                    return "ERROR";
            }
        }
    }
}



