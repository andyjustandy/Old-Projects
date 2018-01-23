using System;
using System.Data;
using System.Text;
using System.Windows;

namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///      A class which describes a Game Simulation Report.  
    ///      The report is produced typically after a performing the Game Simulation. Constructing and creating report on the game simulations.
    ///      Harnesses the Game Simulation Input (player strategy, dealer strategy, simulation output and game rules) to create the report string. 
    /// </summary>
    public class GameSimulationReport
    {
        // Stores the final Game Simulation Report string as a StringBuilder.
        private StringBuilder _ReportString;

        /* SIMULATION VARIABLES */
        private decimal _InitialBalance;
        private decimal _FinalBalance;
        private decimal _FlatBetAmount;
        
        // MS - money staked, MR - money returned, MR-MS is a net profit(or loss), yield = (MR-MS)/MS*100. i.e. (FinalBalance-Inital)/Initial*100 (%). 
        private decimal _Fitness_Yield;  
        private decimal _Profit_Loss;

        /* GAME SIMULATION OUTPUTS */
        private int _Hands_Simulated;
        private int _Total_Hands_To_Simulate;
        private int _Total_Decks;
        private int _Total_Times_Shuffled;
        private int _Total_Hands_Won;
        private int _Total_Hands_Lost;
        private int _Total_Hands_Draw;
        private int _Total_Hands_Surrendered;
        private int _Total_Hands_BJ_Natural_Won;

        /* SIMULATION RULES */
        private bool _SHUFFLE_AFTER_EACH_HAND;
        private double _DECK_DEAL_PERCENTAGE;
        private int _SPLIT_LIMIT;
        private bool _CANNOT_SPLIT_4s_5s_10s;
        private bool _CANNOT_SPLIT_ACES;
        private bool _HIT_SPLIT_ACES_OPTION;
        private bool _DOUBLE_AFTER_SPLIT;
        private GameSimulationInput.EnumDoubleOn _ON_DOUBLE_OPTION;
        private GameSimulationInput.EnumCardBonus _CARD_BONUS_OPTION;
        private bool _LUCKY_777_OPTION;
        private bool _SUITED_BJ_PAYS_2to1_OPTON;
        private GameSimulationInput.EnumBlackJackPays _BJ_PAYS_OPTION;

        /* PLAYING STRATEGIES */
        PlayerStrategy _player_strategy;
        DealerStrategy _dealer_strategy;

        #region GETTERS AND SETTER
        // Gets the Final Game Simulation Report.
        public StringBuilder GetReportString()
        {
            return _ReportString;
        }

        // Gets the Player Strategy used to construct the report.
        public PlayerStrategy GetPlayerStrategy()
        {
            return _player_strategy;
        }

        // Gets the Fitness Yield from the report.
        public decimal GetFitnessYield()
        {
            return _Fitness_Yield;
        }

        // Gets the Fitness Profit from the report.
        public decimal GetProfitLoss()
        {
            return _Profit_Loss;
        }

        // Gets the Fitness Wins from the report.
        public int GetWins()
        {
            return _Total_Hands_Won;
        }
        #endregion

        // The constructor for the GameSimulationReport, stores all of the local report variables and creates the game report. Based on the Game Simulation Inputs (GSI) and Game Simulation output.
        public GameSimulationReport(decimal initial_balance, decimal final_balance, decimal flat_bet_amount, int hands_simulated,
            int total_hands_to_simulate, int total_decks, int total_times_shuffled, int total_hands_won, int total_hands_lost,
            int total_hands_draw, int total_hands_surrendered, int total_hands_bj_natural_won, bool shuffle_after_each_hand, double deck_deal_percentage,
            int split_limit, bool cannot_split_4s_5s_10s, bool cannot_split_aces, bool hit_split_aces_option, bool double_after_split,
            GameSimulationInput.EnumDoubleOn on_double_option, GameSimulationInput.EnumCardBonus card_bonus_option, bool lucky_777_option,
            bool suited_bj_pays_2to1_option, GameSimulationInput.EnumBlackJackPays bj_pays_option, PlayerStrategy player_strategy,
            DealerStrategy dealer_strategy)
        {
            #region - Sets all local variables -
            decimal yield = (decimal)((final_balance - initial_balance) / (decimal)(initial_balance));
            this._Fitness_Yield = Math.Round(yield, 5);
            this._InitialBalance = initial_balance;
            this._FinalBalance = final_balance;
            this._FlatBetAmount = flat_bet_amount;
            // MS - money staked, MR - money returned, MR-MS is a net profit(or loss), yield = ((MR-MS)/MS)*100 (%). 

            this._Profit_Loss = final_balance-initial_balance;
            this._Hands_Simulated = hands_simulated;
            this._Total_Hands_To_Simulate = total_hands_to_simulate;
            this._Total_Decks = total_decks;
            this._Total_Times_Shuffled = total_times_shuffled;
            this._Total_Hands_Won = total_hands_won;
            this._Total_Hands_Lost = total_hands_lost;
            this._Total_Hands_Draw = total_hands_draw;
            this._Total_Hands_Surrendered = total_hands_surrendered;
            this._Total_Hands_BJ_Natural_Won = total_hands_bj_natural_won;

            this._SHUFFLE_AFTER_EACH_HAND = shuffle_after_each_hand;
            this._DECK_DEAL_PERCENTAGE = deck_deal_percentage;
            this._SPLIT_LIMIT = split_limit;
            this._CANNOT_SPLIT_4s_5s_10s = cannot_split_4s_5s_10s;
            this._CANNOT_SPLIT_ACES = cannot_split_aces;
            this._HIT_SPLIT_ACES_OPTION = hit_split_aces_option;
            this._DOUBLE_AFTER_SPLIT = double_after_split;
            this._ON_DOUBLE_OPTION = on_double_option;
            this._CARD_BONUS_OPTION = card_bonus_option;
            this._LUCKY_777_OPTION = lucky_777_option;
            this._SUITED_BJ_PAYS_2to1_OPTON = suited_bj_pays_2to1_option;
            this._BJ_PAYS_OPTION = bj_pays_option;
            this._player_strategy = player_strategy;
            this._dealer_strategy = dealer_strategy;
            #endregion

        StringBuilder ReportString = new StringBuilder();
            // Constructs and creates the Game Simulation output report. Converting the game output (variables) to a string format.
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("---------------------------------------------------- GAME SIMULATION REPORT ----------------------------------------------------");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("| ");
            ReportString.AppendLine("| Yield (%): " + _Fitness_Yield);
            ReportString.AppendLine("| Profit/Loss: " + _Profit_Loss);
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("| ");
            ReportString.AppendLine("| Initial Balance: " + _InitialBalance);
            ReportString.AppendLine("| ");
            ReportString.AppendLine("| Final Balance: " + _FinalBalance);
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Flat Bet Amount: " + _FlatBetAmount);
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Number of Hands Simulated: (" + _Hands_Simulated + "/" + _Total_Hands_To_Simulate +")");
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Number of Decks: " + _Total_Decks);
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Times Shuffled: " + _Total_Times_Shuffled);
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Hands Won: " + _Total_Hands_Won);
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Hands Lost: " + _Total_Hands_Lost);
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Hands Drawn: " + _Total_Hands_Draw);
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Hands Surrendered: " + _Total_Hands_Surrendered);
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Blackjacks Won: " + _Total_Hands_BJ_Natural_Won);
            ReportString.AppendLine("________________________________________________________________________________________________________________________________________ ");
            ReportString.AppendLine(" ");
            ReportString.AppendLine(" ");
            
            // Constructs and creates the Game Simulation Input rules. Converting the input rules (variables) to a string format.
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("------------------------------------------------------GAME SIMULATION RULES ------------------------------------------------------");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            
            // Creates a string for the value yes or no (bool values). 
            string yesORnoORenum = "";

            // Sets the bool value to a yes or no
            if (_SHUFFLE_AFTER_EACH_HAND == true)
                yesORnoORenum = "yes";
            else yesORnoORenum = "no";
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Shuffle after each hand: " + yesORnoORenum);
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| % Dealt until shuffle: " + _DECK_DEAL_PERCENTAGE + "%");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Split Limit: " + _SPLIT_LIMIT);
            ReportString.AppendLine("|  ");
            
            // Converts the bool value to a yes or no
            if (_CANNOT_SPLIT_4s_5s_10s == true)
                yesORnoORenum = "no";
            else yesORnoORenum = "yes";
            ReportString.AppendLine("| Split 4s, 5s or 10s: " + yesORnoORenum);    
            ReportString.AppendLine("|  ");
            
            // Converts the bool value to a yes or no
            if (_CANNOT_SPLIT_ACES == true)
                yesORnoORenum = "no";
            else yesORnoORenum = "yes";
            ReportString.AppendLine("| Split Aces: " + yesORnoORenum);
            ReportString.AppendLine(" ");
            
            // Converts the bool value to a yes or no
            if (_HIT_SPLIT_ACES_OPTION == true)
                yesORnoORenum = "yes";
            else yesORnoORenum = "no";
            ReportString.AppendLine("| Hit Split Aces: " + yesORnoORenum);
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("|  ");
            
            // Converts the bool value to a yes or no
            if (_DOUBLE_AFTER_SPLIT == true)
                yesORnoORenum = "yes";
            else yesORnoORenum = "no";
            ReportString.AppendLine("| Double After Split: " + yesORnoORenum);
            ReportString.AppendLine("|  ");
            
            // Converts the enum value to an appropriate output message (string)
            switch (_ON_DOUBLE_OPTION)
            {
                case GameSimulationInput.EnumDoubleOn.Any_2_Cards:
                    yesORnoORenum = "any 2 cards";
                    break;
                case GameSimulationInput.EnumDoubleOn.HardORSoft_9to11:
                    yesORnoORenum = "hard/soft values 9-11";
                    break;
                case GameSimulationInput.EnumDoubleOn.Hard_9to11:
                    yesORnoORenum = "hard values 9-11";
                    break;
                default:
                    MessageBox.Show("Error, located in creating the simulation report. Double On Enum.");
                    yesORnoORenum = "any 2 cards";
                    break;
            }
            ReportString.AppendLine("| Double Option: " + yesORnoORenum);
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("|  ");
            
            // Converts the enum value to an appropriate output message (string)
            switch (_CARD_BONUS_OPTION)
            {
                case GameSimulationInput.EnumCardBonus.Five_Above_Card_21_Pays2to1:
                    yesORnoORenum = "5+ card trick 21 (pays 2:1)";
                    break;
                case GameSimulationInput.EnumCardBonus.Five_Card_21_Pays2to1:
                    yesORnoORenum = "5 card trick 21 (pays 2:1)";
                    break;
                case GameSimulationInput.EnumCardBonus.Five_Card_Trick_Bonus:
                    yesORnoORenum = "5 card trick bonus (pays 3:2)";
                    break;
                case GameSimulationInput.EnumCardBonus.No_Card_Trick_Bonus:
                    yesORnoORenum = "no card trick bonus";
                    break;
                case GameSimulationInput.EnumCardBonus.Seven_Card_Trick_Bonus:
                    yesORnoORenum = "7 card trick bonus (pays 3:2)";
                    break;
                case GameSimulationInput.EnumCardBonus.Six_Card_Trick_Bonus:
                    yesORnoORenum = "6 card trick bonus (pays 3:2)";
                    break;
                default:
                    MessageBox.Show("Error, located in creating the simulation report. Card Bonus Option Enum.");
                    yesORnoORenum = "no card trick bonus";
                    break;
            }
            ReportString.AppendLine("| Card Bonus Option: " + yesORnoORenum);
            ReportString.AppendLine("|  ");
            
            // Converts the bool value to enabled or disabled
            if (_LUCKY_777_OPTION == true)
                yesORnoORenum = "enabled";
            else yesORnoORenum = "disabled";
            ReportString.AppendLine("| Lucky 777: " + yesORnoORenum);
            ReportString.AppendLine("|  ");
            
            // Converts the bool value to enabled or disabled
            if (_SUITED_BJ_PAYS_2to1_OPTON == true)
                yesORnoORenum = "enabled";
            else yesORnoORenum = "disabled";
            ReportString.AppendLine("| Suited Blackjack pays 2-1: " + yesORnoORenum);
            ReportString.AppendLine("|  ");

            // Converts the enum value to an appropriate output message (string)
            switch (_BJ_PAYS_OPTION)
            {
                case GameSimulationInput.EnumBlackJackPays.Pays1to1:
                    yesORnoORenum = "1:1";
                    break;
                case GameSimulationInput.EnumBlackJackPays.Pays2to1:
                    yesORnoORenum = "2:1";
                    break;
                case GameSimulationInput.EnumBlackJackPays.Pays3to2:
                    yesORnoORenum = "3:2";
                    break;
                case GameSimulationInput.EnumBlackJackPays.Pays6to5:
                    yesORnoORenum = "6:5";
                    break;
                default:
                    MessageBox.Show("Error, located in creating the simulation report. Card Bonus Option Enum.");
                    yesORnoORenum = "3:2";
                    break;
            }
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Blackjack pays: " + yesORnoORenum);
            ReportString.AppendLine("________________________________________________________________________________________________________________________________________ ");
            ReportString.AppendLine(" ");
            ReportString.AppendLine(" ");

            // Constructs and creates the Player Strategy. Converting the strategy (datatable) to a string format.
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("--------------------------------------------------------- PLAYER'S STRATEGY ---------------------------------------------------------");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");

            ReportString.AppendLine("| Strategy Name: " + _player_strategy.GetStrategyName());

            DataTable dt = _player_strategy.GetPlayerStrategyGrid();
            int nbColumns = _player_strategy.GetNbColumns();
            #region - Converts: DataTable -> String Table -
            // Writes the strategyTable (DataTable) to a string table. Uses no loops and manually typed to ensure formatting is fully flexible. 
            ReportString.AppendLine("|        |        2        |        3        |        4        |        5        |        6        |        7        |        8        |        9        |       10        |        A        |");
            ReportString.AppendLine("------------------------------------------------------------------------------------------------------------------------------------------");
            int rowIndex = 0; // starting row index
            //                     ("|Hard5 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");   // Test to ensure columns align 
            ReportString.Append("| Hard5  |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard6 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard6  |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard7 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard7  |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard8 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard8  |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard9 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard9  |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard10|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard10 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard11|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard11 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard12|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard12 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard13|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard13 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard14|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard14 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard15|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard15 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard16|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard16 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard17|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard17 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard18|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard18 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard19|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard19 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard20|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard20 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard21|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard21 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }

            ReportString.AppendLine("------------------------------------------------------------------------------------------------------------------------------------------"); // Creates the row splitter
            rowIndex++;
            // Creates the row ->  ("|Soft13|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft13 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft14|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft14 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft15|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft15 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft16|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft16 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft17|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft17 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft18|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft18 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft19|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft19 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft20|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft20 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft21|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft21 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            ReportString.AppendLine("------------------------------------------------------------------------------------------------------------------------------------------"); // Creates the row splitter
            rowIndex++;
            // Creates the row ->  ("|Pair2 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair2  |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair3 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair3  |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair4 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair4  |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair5 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair5  |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair6 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair6  |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair7 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair7  |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair8 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair8  |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair9 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair9  |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair10|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair10 |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|PairA |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| PairA  |");  // Creates the opening row header
            for (int col = 0; col < nbColumns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            ReportString.AppendLine(" ");
            ReportString.AppendLine(" ");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------"); // Creates the row splitter
            #endregion

            dt = _dealer_strategy.GetDealerStrategyGrid();
            ReportString.AppendLine();
            // Constructs and creates the Dealer Strategy. Converting the strategy (datatable) to a string format.
            ReportString.AppendLine("--------------------------------------------------------- DEALER'S STRATEGY ---------------------------------------------------------");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("| Strategy Name: " + _dealer_strategy.GetStrategyName());
            #region - Converts: DataTable -> String Table -
            // Writes the strategyTable (DataTable) to a string table. Uses no loops and manually typed to ensure formatting is fully flexible. 
            ReportString.AppendLine("|   _   |         _        |");
            ReportString.AppendLine("---------------------");

            //                     ("|Hard5 |  s  |   // Test to ensure columns align 
            ReportString.AppendLine("| Hard5  |        " + dt.Rows[0][0].ToString() + "        |");

            // Creates the row ->  ("|Hard6 |  s  |  
            ReportString.AppendLine("| Hard6  |        " + dt.Rows[1][0].ToString() + "        |");

            // Creates the row ->  ("|Hard7 |  s  | 
            ReportString.AppendLine("| Hard7  |        " + dt.Rows[2][0].ToString() + "        |");

            // Creates the row ->  ("|Hard8 |  s  |
            ReportString.AppendLine("| Hard8  |        " + dt.Rows[3][0].ToString() + "        |");

            // Creates the row ->  ("|Hard9 |  s  |  
            ReportString.AppendLine("| Hard9  |        " + dt.Rows[4][0].ToString() + "        |");

            // Creates the row ->  ("|Hard10|  s  | 
            ReportString.AppendLine("| Hard10 |        " + dt.Rows[5][0].ToString() + "        |");

            // Creates the row ->  ("|Hard11|  s  | 
            ReportString.AppendLine("| Hard11 |        " + dt.Rows[6][0].ToString() + "        |");

            // Creates the row ->  ("|Hard12|  s  | 
            ReportString.AppendLine("| Hard12 |        " + dt.Rows[7][0].ToString() + "        |");

            // Creates the row ->  ("|Hard13|  s  |  
            ReportString.AppendLine("| Hard13 |        " + dt.Rows[8][0].ToString() + "        |");

            // Creates the row ->  ("|Hard14|  s  |  
            ReportString.AppendLine("| Hard14 |        " + dt.Rows[9][0].ToString() + "        |");

            // Creates the row ->  ("|Hard15|  s  | 
            ReportString.AppendLine("| Hard15 |        " + dt.Rows[10][0].ToString() + "        |");

            // Creates the row ->  ("|Hard16|  s  |  
            ReportString.AppendLine("| Hard16 |        " + dt.Rows[11][0].ToString() + "        |");

            // Creates the row ->  ("|Hard17|  s  |  
            ReportString.AppendLine("| Hard17 |        " + dt.Rows[12][0].ToString() + "        |");

            // Creates the row ->  ("|Hard18|  s  | 
            ReportString.AppendLine("| Hard18 |        " + dt.Rows[13][0].ToString() + "        |");

            // Creates the row ->  ("|Hard19|  s  | 
            ReportString.AppendLine("| Hard19 |        " + dt.Rows[14][0].ToString() + "        |");

            // Creates the row ->  ("|Hard20|  s  |  
            ReportString.AppendLine("| Hard20 |        " + dt.Rows[15][0].ToString() + "        |");

            // Creates the row ->  ("|Hard21|  s  | 
            ReportString.AppendLine("| Hard21 |        " + dt.Rows[16][0].ToString() + "        |");

            ReportString.AppendLine("---------------------");

            // Creates the row ->  ("|Soft13|  s  |  
            ReportString.AppendLine("| Soft13 |        " + dt.Rows[17][0].ToString() + "        |");

            // Creates the row ->  ("|Soft14|  s  |  
            ReportString.AppendLine("| Soft14 |        " + dt.Rows[18][0].ToString() + "        |");

            // Creates the row ->  ("|Soft15|  s  |  
            ReportString.AppendLine("| Soft15 |        " + dt.Rows[19][0].ToString() + "        |");

            // Creates the row ->  ("|Soft16|  s  |  
            ReportString.AppendLine("| Soft16 |        " + dt.Rows[20][0].ToString() + "        |");

            // Creates the row ->  ("|Soft17|  s  |  
            ReportString.AppendLine("| Soft17 |        " + dt.Rows[21][0].ToString() + "        |");

            // Creates the row ->  ("|Soft18|  s  | 
            ReportString.AppendLine("| Soft18 |        " + dt.Rows[22][0].ToString() + "        |");

            // Creates the row ->  ("|Soft19|  s  | 
            ReportString.AppendLine("| Soft19 |        " + dt.Rows[23][0].ToString() + "        |");

            // Creates the row ->  ("|Soft20|  s  |  
            ReportString.AppendLine("| Soft20 |        " + dt.Rows[24][0].ToString() + "        |");

            // Creates the row ->  ("|Soft21|  s  |  
            ReportString.AppendLine("| Soft21 |        " + dt.Rows[25][0].ToString() + "        |");

            ReportString.AppendLine("---------------------");

            // Creates the row ->  ("|Pair2 |  s  |  
            ReportString.AppendLine("| Pair2  |        " + dt.Rows[26][0].ToString() + "        |");

            // Creates the row ->  ("|Pair3 |  s  |  
            ReportString.AppendLine("| Pair3  |        " + dt.Rows[27][0].ToString() + "        |");

            // Creates the row ->  ("|Pair4 |  s  |  
            ReportString.AppendLine("| Pair4  |        " + dt.Rows[28][0].ToString() + "        |");

            // Creates the row ->  ("|Pair5 |  s  |  
            ReportString.AppendLine("| Pair5  |        " + dt.Rows[29][0].ToString() + "        |");

            // Creates the row ->  ("|Pair6 |  s  |  
            ReportString.AppendLine("| Pair6  |        " + dt.Rows[30][0].ToString() + "        |");

            // Creates the row ->  ("|Pair7 |  s  |  
            ReportString.AppendLine("| Pair7  |        " + dt.Rows[31][0].ToString() + "        |");

            // Creates the row ->  ("|Pair8 |  s  | 
            ReportString.AppendLine("| Pair8  |        " + dt.Rows[32][0].ToString() + "        |");

            // Creates the row ->  ("|Pair9 |  s  | 
            ReportString.AppendLine("| Pair9  |        " + dt.Rows[33][0].ToString() + "        |");

            // Creates the row ->  ("|Pair10|  s  |  
            ReportString.AppendLine("| Pair10 |        " + dt.Rows[34][0].ToString() + "        |");

            // Creates the row ->  ("|PairA |  s  | 
            ReportString.AppendLine("| PairA  |        " + dt.Rows[35][0].ToString() + "        |");

            ReportString.AppendLine("---------------------");
            #endregion


            this._ReportString = ReportString;
        }

        // Generates a the rows for the string strategy, used when converting a strategy to a string (text format). 
        public void CreateRow(StringBuilder string_builder, DataTable dt, int row, int col)
        {
            if (col == 8)
                string_builder.Append("  "); // Adds an extra white space for the format incase of the 10 column [8] string table.
            if(col == 9)
            string_builder.AppendLine("        " + dt.Rows[row][col].ToString() + "        |"); // Adds the column data and format for the string table.
            else
                string_builder.Append("        " + dt.Rows[row][col].ToString() + "        |"); // Adds the column data and format for the string table.
        }
    }
}
