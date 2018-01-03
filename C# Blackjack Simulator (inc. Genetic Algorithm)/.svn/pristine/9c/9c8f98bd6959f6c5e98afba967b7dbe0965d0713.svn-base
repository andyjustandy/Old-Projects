using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///     A class which describes a Game Simulation Process.  
    ///      The Game Simulation, will harness the Game Simulation Input to run the Blackjack game hands (Strategy Tester). To later produce a game report.
    /// </summary>
    class GameSimulation
    {
        // Sets the DEBUG MODE
        private static bool debug = false;  

        /* LOCAL GAME SIMULATION VARIABLES */
        private static int _CARDS_PER_DECK = 52;
        
        // The number of hands played count
        private static int _handCount = 0;                
        private static int _SIMULATION_HANDS;
        private static int _DECK_MULTIPLIER;

        // Index for the next card to be dealt
        public static int _DECK_DEALING_INDEX = 0;      
        private static Deck _gameDeck;
        private static double _DECK_DEAL_PERCENTAGE;
        
        // Default Set to Split two times, therefore 3 hands
        private static int _SPLIT_LIMIT = 2;  
        private static GameSimulationInput.EnumBlackJackPays _BJ_PAYS_OPTION;
        private static bool _LUCKY_777_OPTION;
        private static bool _SUITED_BJ_PAYS_2to1_OPTON;
        private static GameSimulationInput.EnumCardBonus _CARD_BONUS_OPTION;
        private static bool _CANNOT_SPLIT_4s_5s_10s;
        private static bool _HIT_SPLIT_ACES_OPTION;
        private static bool _CANNOT_SPLIT_ACES;
        private static bool _DOUBLE_AFTER_SPLIT;
        private static bool _SHUFFLE_AFTER_EACH_HAND;
        private static GameSimulationInput.EnumDoubleOn _ON_DOUBLE;
        private static GameSimulationInput.EnumSurrender _SURRENDER_OPTION;
        private static int SPLIT_COUNT;
        private static bool _IsGeneticAlgorithm = false;

        /* SIMULATION REPORT VARIABLES */
        private static decimal _Report_InitialBalance;
        private static decimal _Report_FinalBalance;
        private static decimal _Report_FlatBetAmount;
        // Note: _Report_Fitness_Yield is calculated in the GameSimulationReport class 
        // Note: _Report_Profit_Loss is calculated in the GameSimulationReport class   
        private static int _Report_Hands_Simulated;
        private static int _Report_Total_Hands_To_Simulate;
        private static int _Report_Total_Decks;
        private static int _Report_Total_Times_Shuffled;
        private static int _Report_Total_Hands_Won;
        private static int _Report_Total_Hands_Lost;
        private static int _Report_Total_Hands_Draw;
        private static int _Report_Total_Hands_Surrendered;
        private static int _Report_Total_Hands_BJ_Natural_Won;     

        // The row headers for the strategy grid. 
        private static string[] handEnumString = { "HARD_FIVE","HARD_SIX","HARD_SEVEN","HARD_EIGHT","HARD_NINE","HARD_TEN",
            "HARD_ELEVEN","HARD_TWELVE","HARD_THIRTEEN","HARD_FOURTEEN","HARD_FIFTEEN","HARD_SIXTEEN","HARD_SEVENTEEN",
            "HARD_EIGHTEEN","HARD_NINTEEN","HARD_TWENTY","HARD_TWENTYONE","SOFT_THIRTEEN","SOFT_FOURTEEN","SOFT_FIFTEEN",
            "SOFT_SIXTEEN","SOFT_SEVENTEEN","SOFT_EIGHTEEN","SOFT_NINETEEN","SOFT_TWENTY","SOFT_TWENTYONE",
            "PAIR_TWO","PAIR_THREE","PAIR_FOUR","PAIR_FIVE","PAIR_SIX","PAIR_SEVEN","PAIR_EIGHT","PAIR_NINE","PAIR_TEN","PAIR_ACE","BUST" };

        // Runs the Blackjack game simulation, using the Game Simulation Input (player strategy, dealer strategy, simulation output and game rules).
        public static GameSimulationReport RunGameSimulation(TextBox txtb_ReportView, GameSimulationInput GSI, bool IsGeneticAlgorithm)
        {
            /* SETS THE LOCAL VARIABLES */
            _handCount = 0;             // Resets the hand count to zero
            _DECK_DEALING_INDEX = 0;    // Resets the deck dealing index to zero
            _IsGeneticAlgorithm = IsGeneticAlgorithm;
            _SIMULATION_HANDS = GSI.GetNumberOfSimulationHands();
            _DECK_MULTIPLIER = GSI.GetNumberOfDecks();
            _DECK_DEAL_PERCENTAGE = GSI.GetDealPercentage();
            _BJ_PAYS_OPTION = GSI.GetBJPaysOption();
            _LUCKY_777_OPTION = GSI.GetLucky777();
            _SUITED_BJ_PAYS_2to1_OPTON = GSI.GetSuitedBJPays2to1();
            _CARD_BONUS_OPTION = GSI.GetCardBonusOption();
            _CANNOT_SPLIT_4s_5s_10s = GSI.GetCannotSplit4s5s10s();
            _SPLIT_LIMIT = GSI.GetNumberOfSplits();
            _HIT_SPLIT_ACES_OPTION = GSI.GetHitSplitAces();
            _CANNOT_SPLIT_ACES = GSI.GetCannotSplitAces();
            _DOUBLE_AFTER_SPLIT = GSI.GetDoubleAfterSplit();
            _ON_DOUBLE = GSI.GetDoubleOnOption();
            _SHUFFLE_AFTER_EACH_HAND = GSI.GetShuffleAfterEachHand();
            _SURRENDER_OPTION = GSI.GetSurrenderOption();

            _Report_InitialBalance = GSI.Get_InitialBalance();     // Reports (stores) the inital balance.
            _Report_FlatBetAmount = GSI.GetFlatBetAmount();        // Reports (stores) the flat bet amount.
            _Report_Total_Hands_To_Simulate = _SIMULATION_HANDS;   // Reports (stores) the total hands to simulate.
            _Report_Total_Decks = _DECK_MULTIPLIER;                // Reports (stores) the the number of decks.

            // Resets all of the Game Simulation Report counters to zero.
            _Report_Hands_Simulated = 0;
            _Report_Total_Times_Shuffled = 0;
            _Report_Total_Hands_Won = 0;
            _Report_Total_Hands_Lost = 0;
            _Report_Total_Hands_Draw = 0;
            _Report_Total_Hands_Surrendered = 0;
            _Report_Total_Hands_BJ_Natural_Won = 0;

            
            // Creates a new deck
            _gameDeck = new Deck(_CARDS_PER_DECK, _DECK_MULTIPLIER);
            if (debug) MessageBox.Show("Deck CREATED.");         

            // Creates the dealer
            Dealer dealer = new Dealer(GSI.GetDealerStrategy(), debug);

            // Creates a list of the players
            List<Player> PlayerList = new List<Player>();
            // Creates a new player
            PlayerStrategy player_strategy = GSI.GetPlayerStrategy();
            PlayerList.Add(new Player(1000, GSI.Get_InitialBalance(), GSI.GetFlatBetAmount(), player_strategy, debug));
            bool hasMoney = true; // Stores if the player has money 

            // holds the message box b string
            StringBuilder player_cards_string = new StringBuilder();
            StringBuilder dealer_cards_string = new StringBuilder();

            /* This while loop controls the player hand and dealing interactions */
            while (_handCount <= _SIMULATION_HANDS)
            {
                foreach (Player player in PlayerList)
                {
                    // If the player is out of money to bet, the simulation will be taken to the end of the simulation.
                    if (player.GetPlayerBalance() - player.GetPlayerBet() <= 0)
                    {
                        hasMoney = false;
                        goto OutOfMoney;
                    }
                }
                _handCount++;
                // Calculates the current percentage of cards dealt out.
                int totalCards = _CARDS_PER_DECK * _DECK_MULTIPLIER;
                int currentPercentage = (int)(((double)_DECK_DEALING_INDEX / (double)totalCards) * 100);

                /* If the amount of cards dealt is less than the deal percentage allowed deal cards. 
                   Otherwise shuffle deck. */
                if (currentPercentage <= _DECK_DEAL_PERCENTAGE || _DECK_DEALING_INDEX == 0)
                {
                    // START OF A NEW ROUND OF BLACKJACK

                    // Deals one card to each player playing and then deals one card to the dealer,
                    foreach (Player player in PlayerList)
                    {
                        if (_DECK_DEALING_INDEX == 52 * (_DECK_MULTIPLIER))
                        {
                            // Shuffles the deck and sets the deck dealing index back to 0.
                            _gameDeck.ShuffleDeck();
                            _DECK_DEALING_INDEX = 0;
                            player.DealtoPlayer(_gameDeck.deck[_DECK_DEALING_INDEX], 0, _gameDeck); // zero represents deck 1 
                        }
                        else // Deals the a card.
                            player.DealtoPlayer(_gameDeck.deck[_DECK_DEALING_INDEX], 0, _gameDeck); // zero represents deck 1 
                    }
                        if (_DECK_DEALING_INDEX == 52 * (_DECK_MULTIPLIER))
                        {
                            // Shuffles the deck and sets the deck dealing index back to 0.
                            _gameDeck.ShuffleDeck();
                            _DECK_DEALING_INDEX = 0;
                            dealer.DealtoDealer(_gameDeck.deck[_DECK_DEALING_INDEX], _gameDeck);
                        }
                        else // Deals the a card.
                            dealer.DealtoDealer(_gameDeck.deck[_DECK_DEALING_INDEX], _gameDeck);
                        // deals an additional card to each player and then the dealer.
                        foreach (Player player in PlayerList)
                    {
                            if (_DECK_DEALING_INDEX == 52 * (_DECK_MULTIPLIER))
                            {
                                // Shuffles the deck and sets the deck dealing index back to 0.
                                _gameDeck.ShuffleDeck();
                                _DECK_DEALING_INDEX = 0;
                                player.DealtoPlayer(_gameDeck.deck[_DECK_DEALING_INDEX], 0, _gameDeck); // zero represents deck 1 
                            }
                            else // Deals the a card.
                                player.DealtoPlayer(_gameDeck.deck[_DECK_DEALING_INDEX], 0, _gameDeck); // zero represents deck 1 
                        }
                    if (_DECK_DEALING_INDEX == 52 * (_DECK_MULTIPLIER))
                    {
                        // Shuffles the deck and sets the deck dealing index back to 0.
                        _gameDeck.ShuffleDeck();
                        _DECK_DEALING_INDEX = 0;
                        dealer.DealtoDealer(_gameDeck.deck[_DECK_DEALING_INDEX], _gameDeck);
                    }
                    else // Deals the a card.
                        dealer.DealtoDealer(_gameDeck.deck[_DECK_DEALING_INDEX], _gameDeck);
                    player_cards_string.Clear();
                    dealer_cards_string.Clear();
                    foreach (Player player in PlayerList)
                    {
                        // For each card in the players Hand 1, index = 0.
                        foreach (Card card in player.GetPlayerHand(0).GetHand())
                        {
                            player_cards_string.Append("[" + card.ToString() + "]");
                        }
                        player_cards_string.Append(" " + player.GetPlayerHand(0).GetHandValue());
                        player_cards_string.Append("-" + handEnumString[(int)player.GetPlayerHand(0).GetHandEnumValue(_CANNOT_SPLIT_4s_5s_10s, _CANNOT_SPLIT_ACES)]);
                        player_cards_string.Append("\n Bet: " + player.GetPlayerBet() + "     Balance: £" + player.GetPlayerBalance());
                    }
                    foreach (Card card in dealer.GetDealerHand().GetHand())
                    {
                        dealer_cards_string.Append("[" + card.ToString() + "]");
                    }
                    dealer_cards_string.Append(" " + dealer.GetDealerHand().GetHandValue());
                    dealer_cards_string.Append("-" + handEnumString[(int)dealer.GetDealerHand().GetHandEnumValue(_CANNOT_SPLIT_4s_5s_10s, _CANNOT_SPLIT_ACES)]);

                    if (debug) MessageBox.Show("Round " + _handCount.ToString() + ": \nPlayer>" + player_cards_string +
                          "\nDealer>" + dealer_cards_string +
                          "\nIndex: " + _DECK_DEALING_INDEX);

                    #region - PLAYERS TURN -
                    /* PLAYERS TURN */
                    bool players_turn = true;

                    while (players_turn)
                    {
                        Actions.PlayerActionEnum _playerAction;
                        // Goes through the list of player getting the Action from the strategy grid, using the Hand Value Enum (e.g. Hard13), Dealer up card.
                        foreach (Player player in PlayerList)
                        {
                            int _hands_bust_counter = 0;
                            // Loops through each hand that the player has.
                            for (int handIndex = 0; handIndex < player.GetPlayerHandList().Count; handIndex++)
                            {
                                // Calculates the Hand Enum Value. E.g. Hard5 for a 3+2.
                                Hand.HandEnum handEnumValue = player.GetPlayerHand(handIndex).GetHandEnumValue(_CANNOT_SPLIT_4s_5s_10s, _CANNOT_SPLIT_ACES);
                                player_strategy = player.GetPlayerStrategy();
                                _playerAction = GetPlayerAction(handEnumValue, dealer.GetDealerUpCard(), player_strategy);

                                // Checks for the players hand/s if they have bust. Player turn will keep looping until all hands are bust (player might have split)
                                if (_playerAction == Actions.PlayerActionEnum.BUST)
                                {
                                    if (player.GetPlayerHand(handIndex).GetIsBust() == true)
                                    {
                                        if (debug) MessageBox.Show("Player Bust 1 Hand");
                                        _hands_bust_counter++;
                                    }
                                    if (_hands_bust_counter == player.GetPlayerHandList().Count)
                                    {
                                        // If all of the hands are bust end players turn
                                        if (debug) MessageBox.Show("Player Bust All Hands.");
                                        goto PlayerTurnEnded;
                                    }
                                    // Player Busts - pass onto dealer
                                }
                                else // Otherwise carry on with player turn
                                {
                                    switch (_playerAction)
                                    {
                                        // STAND ACTION
                                        case Actions.PlayerActionEnum.S:
                                        STAND:  // a portal exit to the stand action
                                            players_turn = false;
                                            if (debug) MessageBox.Show("Player Action Stands.");
                                            goto PlayerTurnEnded;

                                        // HIT ACTION
                                        case Actions.PlayerActionEnum.H:
                                        HIT:  // a portal exit to the stand action

                                            // If the the player cannot hit split aces i.e. _HIT_SPLIT_ACES_OPTION = false. Then the hand will be forced to stand.
                                            if (_HIT_SPLIT_ACES_OPTION == false && player.GetPlayerHand(handIndex).GetIsSplitAce() == true)
                                                goto STAND;
                                            if (debug) MessageBox.Show("Player Action Hits.");
                                            // Deals a card to the player (hits)
                                            if (_DECK_DEALING_INDEX == 52 * (_DECK_MULTIPLIER))
                                            {
                                                _gameDeck.ShuffleDeck();
                                                _DECK_DEALING_INDEX = 0;
                                                player.DealtoPlayer(_gameDeck.deck[_DECK_DEALING_INDEX], 0, _gameDeck); // zero represents deck 1 
                                            }
                                            else
                                                player.DealtoPlayer(_gameDeck.deck[_DECK_DEALING_INDEX], 0, _gameDeck); // zero represents deck 1 
                                            if (debug)
                                            {
                                                foreach (Hand hand in player.GetPlayerHandList())
                                                {
                                                    MessageBox.Show("Player's hand [" + player.GetPlayerHandList().IndexOf(hand) + "]: " + hand.GetHandValue());
                                                }
                                            }
                                            break;

                                        // DOUBLE ACTION
                                        case Actions.PlayerActionEnum.D:
                                            switch (_ON_DOUBLE)
                                            {
                                                case GameSimulationInput.EnumDoubleOn.Any_2_Cards:
                                                    // The player can double as normal with any card, carries on to the doubling of any cards.
                                                    break;
                                                case GameSimulationInput.EnumDoubleOn.Hard_9to11:
                                                    // If player can only double on Hard 9, Hard 10, Hard 11.
                                                    if (handEnumValue == Hand.HandEnum.HARD_NINE || handEnumValue == Hand.HandEnum.HARD_TEN || handEnumValue == Hand.HandEnum.HARD_ELEVEN)
                                                    {
                                                        // Player Can Double.
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        // Player cannot double, therefore overide with a HIT action. 
                                                        if (debug) MessageBox.Show("Hand Action DOUBLE Override to HIT: \nCan't double hand. Can only double Hard 9-11.\n Hand Value: " + handEnumValue.ToString());
                                                        goto HIT;
                                                    }
                                                case GameSimulationInput.EnumDoubleOn.HardORSoft_9to11:
                                                    // If player can only double on Hard/Soft 9, Hard/Soft 10, Hard/Soft 11.
                                                    if (handEnumValue == Hand.HandEnum.HARD_NINE || handEnumValue == Hand.HandEnum.HARD_TEN || handEnumValue == Hand.HandEnum.HARD_ELEVEN ||
                                                        handEnumValue == Hand.HandEnum.SOFT_NINETEEN || handEnumValue == Hand.HandEnum.SOFT_TWENTY || handEnumValue == Hand.HandEnum.SOFT_TWENTYONE)
                                                    {
                                                        // Player Can Double.
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        // Player cannot double, therefore overide with a HIT action. 
                                                        if (debug) MessageBox.Show("Hand Action DOUBLE Override to HIT: \nCan't double hand. Can only double Hard/Soft 9-11.\n Hand Value: " + handEnumValue.ToString());
                                                        goto HIT;
                                                    }
                                                default:
                                                    break;
                                            }
                                            // If the hand is split and the player cannot double after split.
                                            if (SPLIT_COUNT > 0 && _DOUBLE_AFTER_SPLIT.Equals(false))
                                                goto HIT;

                                            // Sets the bet bool to true
                                            player.GetPlayerHand(handIndex).SetIsBetDoubled(true);
                                            if (debug) MessageBox.Show("Player Action Doubles.");

                                            // Deals a card to the player (hits)
                                            if (_DECK_DEALING_INDEX == 52 * (_DECK_MULTIPLIER))
                                            {
                                                _gameDeck.ShuffleDeck();
                                                _DECK_DEALING_INDEX = 0;
                                                player.DealtoPlayer(_gameDeck.deck[_DECK_DEALING_INDEX], 0, _gameDeck); // zero represents deck 1 
                                            }
                                            else
                                                player.DealtoPlayer(_gameDeck.deck[_DECK_DEALING_INDEX], 0, _gameDeck); // zero represents deck 1 
                                            if (debug)
                                            {
                                                foreach (Hand hand in player.GetPlayerHandList())
                                                {
                                                    MessageBox.Show("Player's hand [" + player.GetPlayerHandList().IndexOf(hand) + "]: " + hand.GetHandValue());
                                                }
                                            }
                                            players_turn = false;
                                            goto PlayerTurnEnded;

                                        // SURRENDER ACTION
                                        case Actions.PlayerActionEnum.R:
                                            switch (_SURRENDER_OPTION)
                                            {
                                                // If player cannot surrender. i.e. Surrender Option is disabled, then the action will be overriden to a HIT.
                                                case GameSimulationInput.EnumSurrender.No_Surrender:
                                                    if (debug) MessageBox.Show("Hand Action SURRENDER Override to HIT: \nCan't surrender hand while the Surrender rule disabled.\n Hand Value: " + handEnumValue.ToString());
                                                    goto HIT;

                                                // If player can full early surrender (surrender anytime). 
                                                case GameSimulationInput.EnumSurrender.Full_Early_Surrender:
                                                    /* Can surrender hand. Therefore will fall through the surrender option switch. To the Surrender process. */
                                                    break;

                                                // If player can only early surrender (surrender after dealer checks for Blackjack)
                                                case GameSimulationInput.EnumSurrender.Late_Standard_Surrender:
                                                    // If the Player has 2 cards and Dealer has a 10 or Ace upcard (dealer checks for blackjack)
                                                    if (player.GetPlayerHand(handIndex).GetHand().Count == 2
                                                    && dealer.GetDealerUpCard().GetCardFace() == Card.CardFaceENUM.Ace || dealer.GetDealerUpCard().GetCardFace() == Card.CardFaceENUM.Ten)
                                                    {
                                                        /* Can surrender hand. Therefore will fall through the surrender option switch. To the Surrender process. */
                                                    }
                                                    else // Otherwise cannot surrender the hand. Will be overriden to a HIT action.
                                                    {
                                                        if (debug) MessageBox.Show("Hand Action SURRENDER Override to HIT: \nCan only surrender when dealer checks for Blackjack.\n Hand Value: " + handEnumValue.ToString());
                                                        goto HIT;
                                                    }
                                                    break;
                                            }
                                            // Get half money back and end players turn
                                            player.PlayerBalance_Add((player.GetPlayerBet() / 2));
                                            player.GetPlayerHand(handIndex).SetIsSurrendered(true);
                                            players_turn = false;

                                            if (debug) MessageBox.Show("Player Action Surrender");
                                            goto PlayerTurnEnded;

                                        // SPLIT ACTION
                                        case Actions.PlayerActionEnum.P:
                                            SplitHand(player.GetPlayerHandList(), handIndex);
                                            if (debug) MessageBox.Show("Player Action Split");
                                            break;
                                    }
                                }
                            }
                        }

                    }
                PlayerTurnEnded:
                    {
                        if (debug) MessageBox.Show("Player Turn Ended");
                    }
                    #endregion

                    #region - DEALERS TURN -
                    /* DEALERS TURN */
                    bool _dealers_turn = true;
                    // Gets Action from the strategy grid, using the Hand Value Enum (e.g. Hard13). Then Excecuting the action.
                    while (_dealers_turn)
                    {
                        Actions.DealerActionEnum _dealerAction;

                        // Gets the dealer action from the dealer's strategy grid. 
                        _dealerAction = GetDealerAction(dealer.GetDealerHand().GetHandEnumValue(_CANNOT_SPLIT_4s_5s_10s, _CANNOT_SPLIT_ACES), dealer.GetDealerStrategy());

                        // Checks if the dealers hand is bust.
                        if (_dealerAction == Actions.DealerActionEnum.BUST)
                        {
                            if (debug) MessageBox.Show("Dealer Bust.");
                            // Dealer bust, end dealer's turn.
                            goto DealerTurnEnded; // Dealer Busts - calculate the winner.
                        }
                        else // Otherwise carry on with dealers turn
                        {
                            switch (_dealerAction)
                            {
                                // STAND ACTION
                                case Actions.DealerActionEnum.S:
                                    _dealers_turn = false;
                                    if (debug) MessageBox.Show("Dealer Action Stands.");
                                    goto DealerTurnEnded;
                                // HIT ACTION
                                case Actions.DealerActionEnum.H:
                                    if (debug) MessageBox.Show("Dealer Action Hits.");
                                    // Deals a card to the dealer (hits)
                                    if(_DECK_DEALING_INDEX == 52 * (_DECK_MULTIPLIER))
                                    {
                                        // Shuffles the deck and sets the deck dealing index back to 0.
                                        _gameDeck.ShuffleDeck();
                                        _DECK_DEALING_INDEX = 0;
                                        dealer.DealtoDealer(_gameDeck.deck[_DECK_DEALING_INDEX], _gameDeck);
                                    } else // Deals the a card.
                                        dealer.DealtoDealer(_gameDeck.deck[_DECK_DEALING_INDEX], _gameDeck);

                                    if (debug)
                                    {
                                        MessageBox.Show("Dealer's hand: " + dealer.GetDealerHand().GetHandValue());
                                    }
                                    break;
                            }
                        }
                    }
                DealerTurnEnded:
                    {
                        if (debug) MessageBox.Show("Dealer Turn Ended");
                    }
                    #endregion

                    /* CALCULATES THE RESULT OF THE HANDS FOR EACH PLAYER (WINNER?) */
                    foreach (Player player in PlayerList)
                    {
                        foreach (Hand playerHand in player.GetPlayerHandList())
                        {
                            Hand.ResultEnum result;

                            if (playerHand.IsSurrendered() == true) // If the player surrendered it will set the result to surrendered.
                                result = Hand.ResultEnum.Surrendered;
                            else
                                result = CalculateHandResult(playerHand, dealer.GetDealerHand()); // If player did not surrender, then will calculate the result.

                            switch (result)
                            {
                                case Hand.ResultEnum.Lose:
                                    _Report_Total_Hands_Lost++;  // Adds a counter to the total hands lost (report)

                                    // player loses and player has doubled, double the initial bet is deducted
                                    if (playerHand.GetIsBetDoubled() == true)   // The bet was doubled
                                    {
                                        player.PlayerBalance_Minus((player.GetPlayerBet() * 2));
                                        if (debug) MessageBox.Show("LOSE DOUBLE: -£" + player.GetPlayerBet() * 2 + ": " + player.GetPlayerBalance());
                                    }
                                    else // Normal bet (inital bet deducted)
                                    {
                                        player.PlayerBalance_Minus(player.GetPlayerBet());
                                        if (debug) MessageBox.Show("LOSE: -£" + player.GetPlayerBet() + ": " + player.GetPlayerBalance());
                                    }
                                    break;
                                case Hand.ResultEnum.Win:
                                    _Report_Total_Hands_Won++;  // Adds a counter to the total hands won (report)

                                    double bet_multiplier = 1;
                                    #region BONUS CONDITIONS
                                    // If the hand is a Lucky777, and the LUCKY_777_OPTION is enabled. Add 3 to multiplier.
                                    if (playerHand.GetIsLucky777() == true && _LUCKY_777_OPTION == true)
                                    { bet_multiplier = bet_multiplier + 2; if (debug) MessageBox.Show("BONUS LUCKY 777: 3:1"); }

                                    if (_CARD_BONUS_OPTION != GameSimulationInput.EnumCardBonus.No_Card_Trick_Bonus) // if card bonuses allowed
                                    {
                                        GameSimulationInput.EnumCardBonus hand_bonus_status = playerHand.GetCardBonusStatus();
                                        // 5 card trick add 3:2 = + 0.5 to the payout multiplier.
                                        if (_CARD_BONUS_OPTION == GameSimulationInput.EnumCardBonus.Five_Card_Trick_Bonus && hand_bonus_status == GameSimulationInput.EnumCardBonus.Five_Card_Trick_Bonus)
                                        { bet_multiplier = bet_multiplier + 0.5; if (debug) MessageBox.Show("BONUS 5 Card Trick: 3:2"); }
                                        // 6 card trick add 3:2 = + 0.5 to the payout multiplier.
                                        if (_CARD_BONUS_OPTION == GameSimulationInput.EnumCardBonus.Six_Card_Trick_Bonus && hand_bonus_status == GameSimulationInput.EnumCardBonus.Six_Card_Trick_Bonus)
                                            bet_multiplier = bet_multiplier + 0.5;
                                        // 7 card trick add 3:2 = + 0.5 to the payout multiplier.
                                        if (_CARD_BONUS_OPTION == GameSimulationInput.EnumCardBonus.Seven_Card_Trick_Bonus && hand_bonus_status == GameSimulationInput.EnumCardBonus.Six_Card_Trick_Bonus)
                                            bet_multiplier = bet_multiplier + 0.5;
                                        // 5 card 21 add 2:1 = + 1 to the payout multiplier.
                                        if (_CARD_BONUS_OPTION == GameSimulationInput.EnumCardBonus.Five_Card_21_Pays2to1 && hand_bonus_status == GameSimulationInput.EnumCardBonus.Five_Card_21_Pays2to1)
                                            bet_multiplier = bet_multiplier + 1;
                                        // 5+ card 21 add 2:1 = + 1 to the payout multiplier.
                                        if (_CARD_BONUS_OPTION == GameSimulationInput.EnumCardBonus.Five_Above_Card_21_Pays2to1 && hand_bonus_status == GameSimulationInput.EnumCardBonus.Five_Above_Card_21_Pays2to1)
                                            bet_multiplier = bet_multiplier + 1;
                                    }
                                    #endregion
                                    // player wins and the hand bet has been doubled, doubled the initial bet will be added
                                    if (playerHand.GetIsBetDoubled() == true)   // The best was doubled
                                    {
                                        bet_multiplier = bet_multiplier + 1; // bet multiplier x2

                                        player.PlayerBalance_Add((player.GetPlayerBet() * 2));
                                        if (debug) MessageBox.Show("WIN DOUBLE: +£" + (decimal)((double)player.GetPlayerBet() * (double)bet_multiplier) + ": " + player.GetPlayerBalance());

                                    }
                                    else // Normal bet (initial) bet added to balance
                                    {
                                        // bet multiplier x1
                                        player.PlayerBalance_Add(player.GetPlayerBet());
                                        if (debug) MessageBox.Show("WIN: +£" + (decimal)((double)player.GetPlayerBet() * (double)bet_multiplier) + ": " + player.GetPlayerBalance());
                                    }
                                    break;
                                case Hand.ResultEnum.Push:
                                    // Adds a counter to the total hands drawn (report)
                                    _Report_Total_Hands_Draw++;  

                                    // Push nothing happens, no money in or out. (balance stays the same) 
                                    if (debug) MessageBox.Show("PUSH: Balance: " + player.GetPlayerBalance());

                                    break;
                                case Hand.ResultEnum.Surrendered:
                                    // Adds a counter to the total hands surrendered (report)
                                    _Report_Total_Hands_Surrendered++;  

                                    // Player surrendred, minus only half the players bet (player keeps half of the initial bet)
                                    player.PlayerBalance_Minus((player.GetPlayerBet() / 2));
                                    if (debug) MessageBox.Show("SURRENDER: -£" + player.GetPlayerBet() / 2 + ": " + player.GetPlayerBalance());

                                    break;
                                case Hand.ResultEnum.Blackjack:
                                    // Adds a counter to the total hands won (report)
                                    _Report_Total_Hands_Won++;  
                                    
                                    // Adds a counter to the total blackjacks won (report)
                                    _Report_Total_Hands_BJ_Natural_Won++;  

                                    // If Suited Blackjack Pays 2:1 Option is selected and the hand is a suited blackjack, then it will pay 2:1.
                                    if (_SUITED_BJ_PAYS_2to1_OPTON == true && playerHand.GetIsSuitedBlackjack() == true)
                                    {
                                        // Suited Blackjack pays 2:1
                                        player.PlayerBalance_Add(player.GetPlayerBet() * (2));
                                        if (debug) MessageBox.Show("SUITED BLACKJACK WON: +£" + player.GetPlayerBet() * (2) + ": " + player.GetPlayerBalance());
                                    }
                                    else 
                                    {
                                        // Hand is not a suited Blackjack, but a regular Blackjack.
                                        switch (_BJ_PAYS_OPTION)
                                        {
                                            case GameSimulationInput.EnumBlackJackPays.Pays1to1: // Blackjack pays at 1:1
                                                player.PlayerBalance_Add((decimal)((double)player.GetPlayerBet() * (double)(1.1)));
                                                if (debug) MessageBox.Show("BLACKJACK WON: +£" + (decimal)((double)player.GetPlayerBet() * (double)(1.1)) + ": " + player.GetPlayerBalance());
                                                break;
                                            case GameSimulationInput.EnumBlackJackPays.Pays2to1: // Blackjack pays at 2:1
                                                player.PlayerBalance_Add((player.GetPlayerBet() * (2)));
                                                if (debug) MessageBox.Show("BLACKJACK WON: +£" + player.GetPlayerBet() * (2) + ": " + player.GetPlayerBalance());
                                                break;
                                            case GameSimulationInput.EnumBlackJackPays.Pays3to2: // Blackjack pays at 3:2
                                                player.PlayerBalance_Add((decimal)((double)player.GetPlayerBet() * (double)(1.5)));
                                                if (debug) MessageBox.Show("BLACKJACK WON: +£" + (decimal)((double)player.GetPlayerBet() * (double)(1.5)) + ": " + player.GetPlayerBalance());
                                                break;
                                            case GameSimulationInput.EnumBlackJackPays.Pays6to5: // Blackjack pays at 6:5
                                                player.PlayerBalance_Add((decimal)((double)player.GetPlayerBet() * (double)(1.2)));
                                                if (debug) MessageBox.Show("BLACKJACK WON: +£" + (decimal)((double)player.GetPlayerBet() * (double)(1.2)) + ": " + player.GetPlayerBalance());
                                                break;
                                        }
                                    }
                                    break;
                                case Hand.ResultEnum.None:
                                    if (debug) MessageBox.Show("No hand result found, in the ResultEnum for a Hand Round");
                                    break;
                            }
                        }
                    }
                    // Sets the cards from the dealer and players, making the lists holding the cards are empty for the next round.
                    ResetHands(dealer, PlayerList);
                }
                else
                {
                    // Shuffles the cards if past the deal percentage.
                    _gameDeck.ShuffleDeck();    
                    if (debug) MessageBox.Show("Deck SHUFFLED.");

                    // Sets the Dealing Index back to zero
                    _DECK_DEALING_INDEX = 0;

                    // Adds a counter to the total times shuffled (report)
                    _Report_Total_Times_Shuffled++; 
                }


                // Before finishing the final hand simulation.
                if (_handCount == _SIMULATION_HANDS)
                {
                    foreach (Player player in PlayerList)
                    {
                        // Reports (stores) the players final balance and hand count.
                        _Report_FinalBalance = player.GetPlayerBalance();
                        _Report_Hands_Simulated = _handCount;
                    }
                }
            }
        /* AFTER SIMULATION */
        OutOfMoney:    // If the player is out of money to bet, the simulation will be taken to the end of the simulation.
            if(hasMoney == false)
            {
                if (debug) MessageBox.Show("Player Out of Money");
                foreach (Player player in PlayerList)
                {
                    // Reports (stores) the players final balance and hand count.
                    _Report_FinalBalance = player.GetPlayerBalance();
                    _Report_Hands_Simulated = _handCount;
                }
            }


            if (debug) // Displays the deck composition (Testing purposes).
            {
                #region COMPOSITION
                // Counts the number of cards in the currentDeck 
                int count = 0;
                // Hold the composition of the deck 
                int[] composition = new int[13];
                Card[] currentDeck = _gameDeck.deck;
                StringBuilder stringbuilder = new StringBuilder();
                foreach (Card c in currentDeck)
                {
                    count++;

                    stringbuilder.Append("\n" + count + "." + c.GetCardFace().ToString().ToUpper() + "of" + c.GetCardSuit().ToString().ToUpper());

                    // this records the card face to later calculate how many cards with that card face are in the deck. e.g. A, 2, 3, ... Q, K. 
                    composition[(int)c.GetCardFace()]++;
                }

                // adds the composition of card faces e.g. A, 2, 3, ... Q, K. 
                stringbuilder.Append("\n\n COMPOSITION: \n");
                for (int i = 0; i < composition.Length; i++)
                {
                    if (i == 0)
                        stringbuilder.Append("A: " + composition[i] + "\n");
                    else if (i == 10)
                        stringbuilder.Append("J: " + composition[i] + "\n");
                    else if (i == 11)
                        stringbuilder.Append("Q: " + composition[i] + "\n");
                    else if (i == 12)
                        stringbuilder.Append("K: " + composition[i] + "\n");
                    else
                        stringbuilder.Append(i + 1 + ": " + composition[i] + "\n");       // requires "i+1" so it starts at "2:" since "A" is index "1:"
                }
                #endregion
                txtb_ReportView.Text = stringbuilder.ToString();
            }

            /* CREATES THE GAME SIMULATION REPORT */
            GameSimulationReport GameReport = new GameSimulationReport(_Report_InitialBalance, _Report_FinalBalance, _Report_FlatBetAmount, _Report_Hands_Simulated,
                _Report_Total_Hands_To_Simulate, _Report_Total_Decks, _Report_Total_Times_Shuffled, _Report_Total_Hands_Won,
                _Report_Total_Hands_Lost, _Report_Total_Hands_Draw, _Report_Total_Hands_Surrendered, _Report_Total_Hands_BJ_Natural_Won,
                _SHUFFLE_AFTER_EACH_HAND, _DECK_DEAL_PERCENTAGE, _SPLIT_LIMIT, _CANNOT_SPLIT_4s_5s_10s, _CANNOT_SPLIT_ACES,
                _HIT_SPLIT_ACES_OPTION, _DOUBLE_AFTER_SPLIT, _ON_DOUBLE, _CARD_BONUS_OPTION, _LUCKY_777_OPTION,
            _SUITED_BJ_PAYS_2to1_OPTON, _BJ_PAYS_OPTION, player_strategy, dealer.GetDealerStrategy());

            // Sets the Report View (TextBoxView) to the report string.
            if(!_IsGeneticAlgorithm)
            txtb_ReportView.Text = GameReport.GetReportString().ToString();

            // Returns the Game Simulation Report.
            return GameReport;
        }

        // Increments the Deck Dealing Index (when a card is dealt)
        public static void IncrementDealingIndex()
        {
            _DECK_DEALING_INDEX++;
        }

        // Gets the player's action from the strategy grid
        public static Actions.PlayerActionEnum GetPlayerAction(Hand.HandEnum? playerHandEnum, Card dealerUpCard, PlayerStrategy playerStrategy)
        {

            // RETURN PLAYER'S ACTION
            return playerStrategy.GetActionFromStrategy((int)playerHandEnum, dealerUpCard.GetCardValue());
        }

        // Gets the dealer's action from the strategy grid
        public static Actions.DealerActionEnum GetDealerAction(Hand.HandEnum? dealerHandEnum, DealerStrategy dealerStrategy)
        {

            // RETURN DEALER'S ACTION
            return dealerStrategy.GetActionFromStrategy((int)dealerHandEnum);
        }

        // Calculates the result of the current game hands at the end of a round of Blackjack.
        public static Hand.ResultEnum CalculateHandResult(Hand player_hand, Hand dealer_hand)
        {
            // if the player hand busts
            if (player_hand.GetIsBust() == true)
            {
                // player loses (dealer wins)
                return Hand.ResultEnum.Lose;
            }
            else  // player hand is not bust 
            {
                //if dealer hand is bust
                if (dealer_hand.GetIsBust())
                {
                    // player wins
                    return Hand.ResultEnum.Win;
                }
                else //player and dealer have not bust
                {                
                    // The hand with Blackjack wins
                    if (dealer_hand.GetHasBlackjack() == true && player_hand.GetHasBlackjack() == false)
                        return Hand.ResultEnum.Lose;
                    // If the player has Blackjack and the dealer does not, the player will win - with the Blackjack Bonus.
                    if (player_hand.GetHasBlackjack() == true && dealer_hand.GetHasBlackjack() == false)
                        return Hand.ResultEnum.Blackjack;

                    // If hands are equal
                    if (player_hand.GetHandValue() == dealer_hand.GetHandValue())
                        return Hand.ResultEnum.Push;

                    // If player has more than dealer -> player wins
                    if (player_hand.GetHandValue() > dealer_hand.GetHandValue())
                        return Hand.ResultEnum.Win;
                    // If the player has less than the dealer -> player loses
                    if (player_hand.GetHandValue() < dealer_hand.GetHandValue())
                        return Hand.ResultEnum.Lose;
                }
            }
            // The hand combination was not caught by a condition
            return Hand.ResultEnum.None;
        }

        // Splits a hand into multiple hands (Split function)
        public static void SplitHand(List<Hand> handList, int hand_split_index)
        {
            SPLIT_COUNT++;
            // only split if in split range
            if (SPLIT_COUNT < _SPLIT_LIMIT)
            {
                {
                    int number_of_hands = handList.Count;
                    bool isPairOfAces = false;
                    // Checks the hand that we  are splitting, if the hand is a pair of aces before we split the hand.
                    if (handList[hand_split_index].GetHand()[0].GetCardFace().Equals(Card.CardFaceENUM.Ace))
                    {
                        isPairOfAces = true;
                    };

                    // Stores the pair cards and clears the current pair hand
                    Card pair1 = handList[hand_split_index].GetHand()[0];
                    Card pair2 = handList[hand_split_index].GetHand()[1];
                    handList[hand_split_index].GetHand().Remove(handList[hand_split_index].GetHand()[1]); // Removes the pair2 (second pair) from the first hand
                    handList[hand_split_index].Deal(_gameDeck.deck[_DECK_DEALING_INDEX]); // Deals an additional card from the deck (to make a hand of two cards)

                    // Creates the new hand second pair hand
                    handList.Add(new Hand(debug));
                    handList[number_of_hands].Add(pair2); // Adds the second pair to the new split hand (if there are 2 hands, the index for the 3rd hand will be [2])
                    handList[number_of_hands].Deal(_gameDeck.deck[_DECK_DEALING_INDEX]); // Deals an additional card from the deck (to make a hand of two cards)

                    // If the hand was a pair of aces before we split the hand, then it will make both (the original and new hand a split pair of aces)
                    if (isPairOfAces)
                    {
                        handList[hand_split_index].SetIsSplitAce(true);
                        handList[number_of_hands].SetIsSplitAce(true);
                    }

                    if (debug)
                    {
                        StringBuilder player_cards_string = new StringBuilder();

                        foreach (Hand hand in handList)
                        {
                            player_cards_string.Append("\n\nHand " + handList.IndexOf(hand));
                            // For each card in the players Hand 1, index = 0.
                            foreach (Card card in hand.GetHand())
                            {
                                player_cards_string.Append("[" + card.ToString() + "]");
                            }
                            player_cards_string.Append(" " + hand.GetHandValue());
                            player_cards_string.Append("-" + handEnumString[(int)hand.GetHandEnumValue(_CANNOT_SPLIT_4s_5s_10s, _CANNOT_SPLIT_ACES)]);
                        }
                        if (debug) MessageBox.Show(player_cards_string.ToString());
                    }
                }
            }
            else
            {
                foreach (Hand hand in handList)
                {
                    hand.SetSplitLimitReached(true);
                }
            }
        }

        // Clears the hands of all players and the dealer.
        public static void ResetHands(Dealer dealer, List<Player> players)
        {
            // For each hand in the hand list for each player, reset (clear) their hands.
            foreach (Player p in players)
            {
                try
                {
                    // Removes all of the extra hands (if the player split)
                    p.GetPlayerHandList().RemoveRange(1, p.GetPlayerHandList().Count - 1); 
                }
                catch
                { // do nothing 
                }
                foreach (Hand h in p.GetPlayerHandList())
                {
                    h.Clear();
                    h.SetIsBust(false);
                    h.SetIsBetDoubled(false);
                    h.SetIsBlackjack(false);
                    h.SetIsSurrendered(false);
                    h.SetSplitLimitReached(false);
                    h.SetIsLucky777(false);
                    h.SetIsSuitedBlackjack(false);
                    h.SetIsSplitAce(false);
                    h.SetCardBonusStatus(GameSimulationInput.EnumCardBonus.No_Card_Trick_Bonus);
                }
                SPLIT_COUNT = 0;            
                // Clears the dealers hand
                Hand d = dealer.GetDealerHand();
                d.Clear();
                d.SetIsBust(false);
                d.SetIsBetDoubled(false);
                d.SetIsBlackjack(false);
                d.SetIsSurrendered(false);

                // LOG MESSAGE.
                if (debug) MessageBox.Show("Hand RESET.");


            }
        }
    }
}


    





