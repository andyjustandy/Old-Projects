using System.Data;
using System.Text;

namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///      A class which describes a Evolution Simulation Report.  
    ///      The report is produced typically after a performing the Genetic Algorithm. Constructing and creating an evolution report. 
    ///      Harnesses the Game Simulation Report (player strategy, dealer strategy, simulation output and game rules), Evolution Simulation Input. To produce the report. 
    /// </summary>
    class EvolutionSimulationReport
    {
        // Stores the final Evolution Simulation Report string as a StringBuilder.
        private StringBuilder _ReportString;

        /* GENETIC ALGORITHM SIMULATION RULES */
        private int _INITIAL_BREEDING_POOL; 
        private int _MUTATION_CHANCE_PER_CELL; 
        private int _NEWBLOOD_CHANCE_PER_CHILD;
        private EvolutionSimulationInput.EnumFitnessMeasurement _FITNESS_MEASUREMENT_OPTION;
        // Pecentage weight (mutable)
        private int _PW_CROSSOVER_ROW_CHANCE; 
        private int _PW_CROSSOVER_COL_CHANCE; 
        private int _PW_STAND_HS_CHANCE; 
        private int _PW_HIT_HS_CHANCE; 
        private int _PW_DOUBLE_HS_CHANCE; 
        private int _PW_SURRENDER_HS_CHANCE; 
        private int _PW_STAND_PAIR_CHANCE;
        private int _PW_HIT_PAIR_CHANCE; 
        private int _PW_DOUBLE_PAIR_CHANCE; 
        private int _PW_SPLIT_PAIR_CHANCE; 
        private int _PW_SURRENDER_PAIR_CHANCE; 
        // Termination Conditions
        private bool _T_GENERATION_COUNT_ON; 
        private int _T_GENERATION_COUNT; 
        private bool _T_STAGNATION_POINT_ON; 
        private int _T_STAGNATION_POINT; 
        private bool _T_TARGET_YIELD_ON;
        private decimal _T_TARGET_YIELD; 
        private bool _T_TARGET_PROFIT_ON; 
        private int _T_TARGET_PROFIT; 
        private bool _T_TARGET_WINS_ON; 
        private int _T_TARGET_WINS; 

        // Stores the current Game Simulation Report, Evolution Simulation Input and the best current strategy.
        GameSimulationReport _GSR;
        EvolutionSimulationInput _ESI;
        PlayerStrategy _BEST_STRATEGY;

        #region GETTERS AND SETTERS
        // Gets the report string (ESI string builder)
        public StringBuilder GetReportString()
        {
            return _ReportString;
        }
        #endregion

        // The constructor for the EvolutionSimulationReport, stores all of the local report variables and creates the evolution report. Based on the GSR and ESI.
        public EvolutionSimulationReport(PlayerStrategy the_best_strategy, GameSimulationReport GSR, EvolutionSimulationInput ESI, int total_generation_count)
        {
            /* SETS LOCAL VARIABLES */
            _INITIAL_BREEDING_POOL = ESI.GetInitialBreedingPool();
            _MUTATION_CHANCE_PER_CELL = ESI.GetMutationChancePerCell();
            _NEWBLOOD_CHANCE_PER_CHILD = ESI.GetNewbloodChancePerChild();
            _FITNESS_MEASUREMENT_OPTION = ESI.GetFitnessMeasurementOption();
            _PW_CROSSOVER_ROW_CHANCE = ESI.GetCrossoverRowChance();
            _PW_CROSSOVER_COL_CHANCE = ESI.GetCrossoverColumnChance();
            _PW_STAND_HS_CHANCE = ESI.GetStandHSChance();
            _PW_HIT_HS_CHANCE = ESI.GetHitHSChance();
            _PW_DOUBLE_HS_CHANCE = ESI.GetDoubleHSChance();
            _PW_SURRENDER_HS_CHANCE = ESI.GetSurrenderHSChance();
            _PW_STAND_PAIR_CHANCE = ESI.GetStandPAIRChance();
            _PW_HIT_PAIR_CHANCE = ESI.GetHitPAIRChance();
            _PW_DOUBLE_PAIR_CHANCE = ESI.GetDoublePAIRChance();
            _PW_SPLIT_PAIR_CHANCE = ESI.GetSplitPAIRChance();
            _PW_SURRENDER_PAIR_CHANCE = ESI.GetSurrenderPAIRChance();
            _T_GENERATION_COUNT_ON = ESI.GetGenerationCountOn();
            _T_GENERATION_COUNT = ESI.GetGenerationCount();
            _T_STAGNATION_POINT_ON = ESI.GetStagnationPointOn();
            _T_STAGNATION_POINT = ESI.GetStagnationPoint();
            _T_TARGET_YIELD_ON = ESI.GetTargetYieldOn();
            _T_TARGET_YIELD = ESI.GetTargetYield();
            _T_TARGET_PROFIT_ON = ESI.GetTargetProfitOn();
            _T_TARGET_PROFIT = ESI.GetTargetProfit();
            _T_TARGET_WINS_ON = ESI.GetTargetWinsOn();
            _T_TARGET_WINS = ESI.GetTargetWins();
            GameSimulationReport _GSR = GSR;
            EvolutionSimulationInput _ESI = ESI;
            PlayerStrategy _BEST_STRATEGY = the_best_strategy;
            StringBuilder ReportString = new StringBuilder();

            // Constructs and creates the Top Strategy. Converting the strategy (datatable) to a string format.
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("----------------------------------------------------- TOP STRATEGY PRODUCED -----------------------------------------------------");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("| Strategy Name: " + _BEST_STRATEGY.GetStrategyName());
            DataTable dt = _BEST_STRATEGY.GetPlayerStrategyGrid();
            #region - Converts: DataTable -> String Table -
            // Writes the strategyTable (DataTable) to a string table. Uses no loops and manually typed to ensure formatting is fully flexible. 
            ReportString.AppendLine("|        |        2        |        3        |        4        |        5        |        6        |        7        |        8        |        9        |       10        |        A        |");
            ReportString.AppendLine("------------------------------------------------------------------------------------------------------------------------------------------");
            int rowIndex = 0; // starting row index
            int number_columns = 10;
            //                     ("|Hard5 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");   // Test to ensure columns align 
            ReportString.Append("| Hard5  |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard6 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard6  |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard7 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard7  |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard8 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard8  |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard9 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard9  |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard10|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard10 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard11|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard11 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard12|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard12 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard13|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard13 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard14|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard14 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard15|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard15 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard16|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard16 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard17|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard17 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard18|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard18 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard19|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard19 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard20|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard20 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Hard21|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Hard21 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }

            ReportString.AppendLine("------------------------------------------------------------------------------------------------------------------------------------------"); // Creates the row splitter
            rowIndex++;
            // Creates the row ->  ("|Soft13|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft13 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft14|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft14 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft15|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft15 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft16|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft16 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft17|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft17 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft18|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft18 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft19|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft19 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft20|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft20 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Soft21|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Soft21 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            ReportString.AppendLine("------------------------------------------------------------------------------------------------------------------------------------------"); // Creates the row splitter
            rowIndex++;
            // Creates the row ->  ("|Pair2 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair2  |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair3 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair3  |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair4 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair4  |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair5 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair5  |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair6 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair6  |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair7 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair7  |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair8 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair8  |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair9 |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair9  |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|Pair10|  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| Pair10 |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            rowIndex++;
            // Creates the row ->  ("|PairA |  s  |  s  |  s  |  s  |  s  |  s  |  s  |  s  |   s  |   s  |");
            ReportString.Append("| PairA  |");  // Creates the opening row header
            for (int col = 0; col < number_columns; col++)
            {
                CreateRow(ReportString, dt, rowIndex, col);
            }
            ReportString.AppendLine(" ");
            ReportString.AppendLine(" ");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------"); // Creates the row splitter
            #endregion
            ReportString.AppendLine(" ");
            ReportString.AppendLine(" ");

            // Constructs and creates the Evolution Simulation Input rules. Converting the ESI rules (variables) to a string format.
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("------------------------------------------------------ EVOLUTION SIMULATION RULES ------------------------------------------------------");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Breeding Pool Size: " + _INITIAL_BREEDING_POOL);
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Mutation Chance (per cell): " + _MUTATION_CHANCE_PER_CELL + "%");
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| New Blood Chance (per child): " + _NEWBLOOD_CHANCE_PER_CHILD + "%");
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Fitness Measurement: " + _FITNESS_MEASUREMENT_OPTION.ToString());
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Crossover cut (breeding)");
            ReportString.AppendLine("|     Row Chance: " + _PW_CROSSOVER_ROW_CHANCE + "%");
            ReportString.AppendLine("|     Column Chance: " + _PW_CROSSOVER_COL_CHANCE + "%");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Cell Mutation (hard/soft hands)");
            ReportString.AppendLine("|     Stand Chance: " + _PW_STAND_HS_CHANCE + "%");
            ReportString.AppendLine("|     Hit Chance: " + _PW_HIT_HS_CHANCE + "%");
            ReportString.AppendLine("|     Double Chance: " + _PW_DOUBLE_HS_CHANCE + "%");
            ReportString.AppendLine("|     Surrender Chance: " + _PW_SURRENDER_HS_CHANCE + "%");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Cell Mutation (pair hands)");
            ReportString.AppendLine("|     Stand Chance: " + _PW_STAND_PAIR_CHANCE + "%");
            ReportString.AppendLine("|     Hit Chance: " + _PW_HIT_PAIR_CHANCE + "%");
            ReportString.AppendLine("|     Double Chance: " + _PW_DOUBLE_PAIR_CHANCE + "%");
            ReportString.AppendLine("|     Split Chance: " + _PW_SPLIT_PAIR_CHANCE + "%");
            ReportString.AppendLine("|     Surrender Chance: " + _PW_SURRENDER_PAIR_CHANCE + "%");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("|  ");
            ReportString.AppendLine("| Cell Mutation (pair hands)");
            ReportString.AppendLine("|     Stand Chance: " + _PW_STAND_PAIR_CHANCE + "%");
            ReportString.AppendLine("|     Hit Chance: " + _PW_HIT_PAIR_CHANCE + "%");
            ReportString.AppendLine("|     Double Chance: " + _PW_DOUBLE_PAIR_CHANCE + "%");
            ReportString.AppendLine("|     Split Chance: " + _PW_SPLIT_PAIR_CHANCE + "%");
            ReportString.AppendLine("|     Surrender Chance: " + _PW_SURRENDER_PAIR_CHANCE + "%");

            // Appends the GameSimulationReport (Game Report, Player and Dealer Strategy, Game Rules)
            ReportString.Append(GSR.GetReportString());

            // Sets the Final report string for the Evolution Simulation Report
            _ReportString = ReportString;
        }

        // Generates a the rows for the string strategy, used when converting a strategy to a string (text format). 
        public static void CreateRow(StringBuilder string_builder, DataTable dt, int row, int col)
        {
            if (col == 8)
                string_builder.Append("  "); // Adds an extra white space for the format incase of the 10 column [8] string table.
            if (col == 9)
                string_builder.AppendLine("        " + dt.Rows[row][col].ToString() + "        |"); // Adds the column data and format for the string table.
            else
                string_builder.Append("        " + dt.Rows[row][col].ToString() + "        |"); // Adds the column data and format for the string table.

        }
    }
}
