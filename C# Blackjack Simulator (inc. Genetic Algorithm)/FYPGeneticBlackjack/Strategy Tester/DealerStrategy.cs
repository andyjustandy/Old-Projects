using System;
using System.Data;

namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///      A class which describes a Blackjack dealer's playing strategy.  
    ///      In the Blackjack simulation a dealer will have a set strategy (usually stand of soft 17).
    /// </summary>
    public class DealerStrategy
    {
        /* THE DEALER STRATEGY */
        private string _strategyName;

        // The playing strategy grid is stored in the form of a DataTable.
        private DataTable _strategyGrid;

        // The number of rows in a strategy grid.
        private int _nbRows = 36;

        // The Constructor for to create a new dealers strategy. Consists of a strategy name and a strategy grid in the form of a DataTable.
        public DealerStrategy(string strategy_name, DataTable dealer_data_table)
        {
            this._strategyName = strategy_name;
            // Creates a new strategy grid (2D datatable)
            this._strategyGrid = dealer_data_table;
        }

        #region SETTERS AND GETTERS
       // Gets the number of rows in a dealer's strategy.
        public int GetNbRows()
        {
            return this._nbRows;
        }

        // Gets the name of the dealers strategy.
        public string GetStrategyName()
        {
            return this._strategyName;
        }

        // Returns the strategy grid to be populated into the data grid view 
        public DataTable GetDealerStrategyGrid()
        {
            return _strategyGrid;
        }

        // Gets a action from the Dealer's strategy grid corresponding the a indexing row. Returns the Action.
        public Actions.DealerActionEnum GetActionFromStrategy(int? row)
        {
            int col = 0;
            if (row == (int)Hand.HandEnum.BUST)
            {
                // If player busts, return Dealers hand as Bust.
                return Actions.DealerActionEnum.BUST;
            }

            Actions.DealerActionEnum action;
            Enum.TryParse(_strategyGrid.Rows[(int)row][col].ToString(), out action);

            // Returns the action.
            return action;
        }
        #endregion

    }
}
