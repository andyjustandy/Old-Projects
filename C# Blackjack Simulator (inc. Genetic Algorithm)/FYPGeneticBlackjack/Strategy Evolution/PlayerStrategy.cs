using System;
using System.Data;

namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///     A class which describes a Blackjack playing strategy.  
    ///      Implements the IComparable<PlayerStrategy> interface, which is utilised in sorting a list of player strategies. 
    ///      Example, sorting breeding pool by comparing the strategies based on a specific target fitness (enum).
    /// </summary>
    public class PlayerStrategy : IComparable<PlayerStrategy>
    {
        // Variables for a Player Strategy grid.
        private int _nbColumns = 10;
        private int _nbRows = 36;
        private string _strategyName;
        private DataTable _strategyGrid;
        private decimal? _fitness_yield = null;
        private decimal? _fitness_profit = null;
        private int? _fitness_wins = null;
        private EvolutionSimulationInput.EnumFitnessMeasurement _FITNESS_MEASUREMENT_OPTION;
        private GameSimulationReport _GAME_SIMULATION_REPORT;
        private bool _IsStagnant = false;

        // Constructor without a Target Fitness input set (used for when simulating Blackjack game only)
        public PlayerStrategy(string strategy_name, DataTable player_data_table)
        {
            // Setting the local variables
            this._strategyName = strategy_name;
            this._strategyGrid = player_data_table;
        }

        // Constructor with a Target Fitness input set (used for when using the evolution simulation - GA)
        public PlayerStrategy(string strategy_name, DataTable player_data_table, EvolutionSimulationInput.EnumFitnessMeasurement fitness_measurement_option)
        {
            // Setting the local variables
            this._strategyName = strategy_name;
            this._strategyGrid = player_data_table;
            _FITNESS_MEASUREMENT_OPTION = fitness_measurement_option;
        }

        // Overriding the to string method for a PlayerStrategy to show the name and the fitness values.
        public override string ToString()
        {
            string return_string = _strategyName + " | Yield: " + _fitness_yield + 
                "| Profit: " + _fitness_profit + "| Wins: " +_fitness_wins;
            return return_string;
        }
        #region SETTERS AND GETTERS
        // Gets the number of columns for a strategy grid.
        public int GetNbColumns()
        {
            return this._nbColumns; 
        }

        // Gets the number of rows for a strategy grid.
        public int GetNbRows()
        {
            return this._nbRows;
        }

        // Returns the Strategy's name
        public string GetStrategyName()
        {
            return this._strategyName; 
        }
        public void SetStrategyName(string name)
        {
            _strategyName = name;
        }
        
        // Gets and Sets the Game Simulation Report (GSR)
        public GameSimulationReport GetGameSimulationReport()
        {
            return _GAME_SIMULATION_REPORT;
        }
        public void SetGameSimulationReport(GameSimulationReport GSR)
        {
            _GAME_SIMULATION_REPORT = GSR;
        }

        // Returns the strategy grid to be populated into the data grid view 
        public DataTable GetPlayerStrategyGrid()
        {
            return _strategyGrid;
        }
        public void SetPlayerStrategyGrid(DataTable strategy)
        {
            _strategyGrid = strategy;
        }

        // Returns the strategy's fitness yield value 
        public decimal? GetFitnessYield()
        {
            return _fitness_yield;
        }
        public void SetFitnessYield(decimal? fitness)
        {
            _fitness_yield = fitness;
        }
        // Returns the strategy's fitness profit value 
        public decimal? GetFitnessProfit()
        {
            return _fitness_profit;
        }
        public void SetFitnessProfit(decimal? profit)
        {
            _fitness_profit = profit;
        }

        // Returns the strategy's fitness wins value 
        public int? GetFitnessWins()
        {
            return _fitness_wins;
        }
        public void SetFitnessWins(int? wins)
        {
            _fitness_wins = wins;
        }
        
        // Returns if the strategy is stagnant (for the genetic algorithm and breeding pool)
        public bool GetIsStagnant()
        {
            return _IsStagnant;
        }
        public void SetIsStagnant(bool stagnant)
        {
            _IsStagnant = stagnant;
        }

        // This gets a player's action from the player strategy grid, by the row and column indexers.
        public Actions.PlayerActionEnum GetActionFromStrategy(int? row, int column)
        {
            if(row == (int)Hand.HandEnum.BUST)
            {
                // If player busts
                return Actions.PlayerActionEnum.BUST;
            }
            // Sets the column index to the number - 2 (with will match the column index in the strategy grid.
            int col = column - 2;
            // if the column is an ace (in the enum) sets the column to the last index of the strategy.
            if (col < 0)
            {
                col = 9;
            }
            Actions.PlayerActionEnum action;
            Enum.TryParse(_strategyGrid.Rows[(int) row][col].ToString(), out action);
            return action;
        }
        #endregion

        // Converts a Datatable into a player strategy grid (note: not a PlayerStrategy).
        Actions.PlayerActionEnum[,] ToPlayerStrategyGrid(DataTable dataTable)
        {
            Actions.PlayerActionEnum[,] strategyArray = new Actions.PlayerActionEnum[_nbColumns, _nbRows];

            for (int row = 0; row < _nbRows; row++)
            {
                for (int col = 0; col < _nbColumns; col++)
                {
                    strategyArray[row, col] = (Actions.PlayerActionEnum)dataTable.Rows[row][col];
                }
            }
            return strategyArray;
        }

        // Compares the fitness value to sort the PlayerStrategy Breeding Pool (list of strategies).
        public int CompareTo(PlayerStrategy other)
        {
            // Checks for null values.
            if (other == null || _fitness_yield == null || _fitness_wins == null || _fitness_profit == null)
                return 0;
            if (_fitness_yield == other._fitness_yield)
                return 0;

            switch (_FITNESS_MEASUREMENT_OPTION)
            {
                case EvolutionSimulationInput.EnumFitnessMeasurement.yield:
                    //if (this._fitness_yield == other._fitness_yield)
                    //    return 0;
                    //if (this._fitness_yield > other._fitness_yield)
                    //    return 1;
                    //if (this._fitness_yield < other._fitness_yield)
                    //    return -1;
                    return this._fitness_yield.Value.CompareTo(other._fitness_yield);
                    case EvolutionSimulationInput.EnumFitnessMeasurement.profit:
                    //if (this._fitness_profit == other._fitness_profit)
                    //    return 0;
                    //if (this._fitness_profit > other._fitness_profit)
                    //    return 1;
                    //if (this._fitness_profit < other._fitness_profit)
                    //    return -1;
                    return this._fitness_profit.Value.CompareTo(other._fitness_profit);
                case EvolutionSimulationInput.EnumFitnessMeasurement.wins:
                    //if (this._fitness_wins == other._fitness_wins)
                    //    return 0;
                    //if (this._fitness_wins > other._fitness_wins)
                    //    return 1;
                    //if (this._fitness_wins < other._fitness_wins)
                    //    return -1;
                    return this._fitness_wins.Value.CompareTo(other._fitness_wins);
            }
            return 0;

        }
    }
}
