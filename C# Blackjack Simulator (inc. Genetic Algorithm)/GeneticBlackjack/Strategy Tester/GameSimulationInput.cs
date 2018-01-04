namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///      A class which describes a Simulation Input.  
    ///      Stores all of the game simulation input variables (input by user), to be later fed to the game Simulation Process.   
    /// </summary>
    public class GameSimulationInput
    {
        /* GAME SIMULATION INPUT VARIABLES */
        private PlayerStrategy _PLAYER_STRATEGY; public PlayerStrategy GetPlayerStrategy() { return _PLAYER_STRATEGY; }
        public void SetPlayerStrategy(PlayerStrategy strategy)
        {
            _PLAYER_STRATEGY = strategy;
        }
        private decimal _BALANCE; public decimal Get_InitialBalance() { return _BALANCE; }
        private decimal _BET_AMOUNT; public decimal GetFlatBetAmount() {return _BET_AMOUNT; }

        /* ENVIRONMENT INPUT VARIABLES */
        DealerStrategy _DEALERS_STRATEGY; public DealerStrategy GetDealerStrategy() {return _DEALERS_STRATEGY; }
        private int _NUMBER_OF_DECKS; public int GetNumberOfDecks() {return _NUMBER_OF_DECKS; }
        private int _DEAL_PERCENTAGE; public int GetDealPercentage() {return _DEAL_PERCENTAGE; }
        private int _NUMBER_OF_SIMULATION_HANDS; public int GetNumberOfSimulationHands() {return _NUMBER_OF_SIMULATION_HANDS; }
        
        // The variation of the double on rule that is harnesses
        private EnumDoubleOn _DOUBLE_ON_OPTION; public EnumDoubleOn GetDoubleOnOption() {return _DOUBLE_ON_OPTION; }
        public enum EnumDoubleOn
        {
            Any_2_Cards,
            Hard_9to11,
            HardORSoft_9to11
        };

        // Can the player double after a split.
        private bool _DOUBLE_AFTER_SPLIT; public bool GetDoubleAfterSplit() {return _DOUBLE_AFTER_SPLIT; }

        // The variation of the Surrender rule that is harnesses
        private EnumSurrender _SURRENDER_OPTION; public EnumSurrender GetSurrenderOption() {return _SURRENDER_OPTION; }
        public enum EnumSurrender
        {
            No_Surrender,
            Late_Standard_Surrender,
            Full_Early_Surrender
        }

        // The variation of the Splitting rule that is harnesses
        private int _NUMBER_OF_SPLIT_OPTION; public int GetNumberOfSplits() {return _NUMBER_OF_SPLIT_OPTION; }
        public enum EnumResplit
        {
            No_Resplit,
            Resplit_Upto_3hands,
            Resplit_Upto_4hands,
            Resplit_Upto_5hands
        }

        // Can the player split aces?
        private bool _CANNOT_SPLIT_ACES;  public bool GetCannotSplitAces() { return _CANNOT_SPLIT_ACES; }

        // Can the player hit split aces?
        private bool _HIT_SPLIT_ACES; public bool GetHitSplitAces() { return _HIT_SPLIT_ACES; }

        // Can player player split 4s, 5s or 10s?
        private bool _CANNOT_SPLIT_4s_5s_10s; public bool GetCannotSplit4s5s10s() { return _CANNOT_SPLIT_4s_5s_10s; }

        // The variation of the shuffling rule that is harnesses.
        private bool _SHUFFLE_AFTER_EACH_HAND; public bool GetShuffleAfterEachHand() { return _SHUFFLE_AFTER_EACH_HAND; }

        // The variation of the Card Bonus rule that is harnesses.
        private EnumCardBonus _CARD_BONUS_OPTION; public EnumCardBonus GetCardBonusOption() { return _CARD_BONUS_OPTION; }
        public enum EnumCardBonus
        {
            No_Card_Trick_Bonus, 
            Five_Card_Trick_Bonus,
            Six_Card_Trick_Bonus,
            Seven_Card_Trick_Bonus,
            Five_Card_21_Pays2to1,
            Five_Above_Card_21_Pays2to1
        };

        // The variation of the Lucky 777 rule that is harnesses.
        private bool _LUCKY_777; public bool GetLucky777(){ return _LUCKY_777; }

        // The variation of the Blackjack Pays amount rule that is harnesses.
        private EnumBlackJackPays _BJ_PAYS_OPTION; public EnumBlackJackPays GetBJPaysOption() {return _BJ_PAYS_OPTION; }
        public enum EnumBlackJackPays
        {
            Pays3to2,
            Pays1to1,
            Pays6to5,
            Pays2to1,
        };

        // The variation of the Suited Blackjack rule that is harnesses
        private bool _SUITED_BJ_PAYS_2to1; public bool GetSuitedBJPays2to1() {return _SUITED_BJ_PAYS_2to1; }

        // The constructor for the game simulation Input, that sets all of the game simulation input parameters.
        public GameSimulationInput(PlayerStrategy player_strategy, decimal balance, decimal bet_amount, DealerStrategy dealer_strategy,
            int number_of_decks, int deal_percentage, int number_of_simulation_hands, EnumDoubleOn double_on, bool double_after_split,
            EnumSurrender surrender_option, int number_of_replit_option, bool cannot_split_aces, bool hit_split_aces, 
            bool cannot_split_4s_5s_10s, bool shuffle_after_each_hand, EnumCardBonus card_bonus_option,
            bool lucky_777, EnumBlackJackPays bj_pays_option, bool suited_bj_pays_2to1)
        {
            _PLAYER_STRATEGY = player_strategy;
            _BALANCE = balance;
            _BET_AMOUNT = bet_amount;

            _DEALERS_STRATEGY = dealer_strategy;
            _NUMBER_OF_DECKS = number_of_decks;
            _DEAL_PERCENTAGE = deal_percentage;
            _NUMBER_OF_SIMULATION_HANDS = number_of_simulation_hands;

            _DOUBLE_ON_OPTION = double_on;
            _DOUBLE_AFTER_SPLIT = double_after_split;
            _SURRENDER_OPTION = surrender_option;
            _NUMBER_OF_SPLIT_OPTION = number_of_replit_option;
            _CANNOT_SPLIT_ACES = cannot_split_aces;
            _HIT_SPLIT_ACES = hit_split_aces;
            _CANNOT_SPLIT_4s_5s_10s = cannot_split_4s_5s_10s;
            _SHUFFLE_AFTER_EACH_HAND = shuffle_after_each_hand;
            _CARD_BONUS_OPTION = card_bonus_option;
            _LUCKY_777 = lucky_777;
            _BJ_PAYS_OPTION = bj_pays_option;
            _SUITED_BJ_PAYS_2to1 = suited_bj_pays_2to1; 
    }
    }
}
