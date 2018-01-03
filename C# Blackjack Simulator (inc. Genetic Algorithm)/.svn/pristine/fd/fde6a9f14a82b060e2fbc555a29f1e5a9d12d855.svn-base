using System.Collections.Generic;

namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///     A class which describes a player in Blackjack. 
    ///     The player will be used within the game simulation to play against a dealer. Using its player strategy and other input variables.
    /// </summary>
    public class Player
    {
        // For handling multiple players.
        private int _player_ID;
        private decimal _player_Balance;         
        private decimal _player_Bet;
        private List<Hand> _player_Hand_List;
            
        // Hold the strategy the player will be using in the game simulation.
        private PlayerStrategy _player_Strategy;          

        #region SETTERS and GETTERS
        // Gets and Sets the Player's hand
        public List<Hand> GetPlayerHandList()
        {
            return _player_Hand_List;
        }
        public Hand GetPlayerHand(int handIndex)
        {
            return _player_Hand_List[handIndex];
        }

        // Gets Player's Strategy
        public PlayerStrategy GetPlayerStrategy()
        {
            return _player_Strategy;
        }

        // Gets the player balance
        public decimal GetPlayerBalance()
        {
            return _player_Balance;
        }

        // Sets the players balance by adding or subtracting an amount. Typicalling adding for a win and subtracting for a loss.
        public void PlayerBalance_Add(decimal amount)
        {
            _player_Balance = _player_Balance + amount;
        }
        public void PlayerBalance_Minus(decimal amount)
        {
            _player_Balance = _player_Balance - amount;
        }

        // Gets the players bet amount.
        public decimal GetPlayerBet()
        {
            return _player_Bet;
        }
        #endregion

        // The constructor uses input from the user to create a new player in Blackjack.
        public Player(int player_id, decimal player_balance, decimal player_bet, PlayerStrategy player_strategy, bool debug)
        {
            // Sets the local player variables.
            this._player_ID = player_id;
            this._player_Balance = player_balance;
            this._player_Bet = player_bet;
            this._player_Strategy = player_strategy;
            this._player_Hand_List = new List<Hand>();
            this._player_Hand_List.Add(new Hand(debug));
        }

        // Adds a card to the players hand and updates the index of the next card to be dealt.
        public void DealtoPlayer(Card card, int handIndex, Deck deck)
        {
            try
            {
                // Deals a card to the player.
                this._player_Hand_List[handIndex].Deal(card);
            } catch
            {
                // The cards go over the deck limit.
                deck.ShuffleDeck();
                GameSimulation._DECK_DEALING_INDEX = 0;

                // Deals a card to the player.
                this._player_Hand_List[handIndex].Deal(card);
            }
        }
    }
}
