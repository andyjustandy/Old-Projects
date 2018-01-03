using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///      A static instance which represents a Evolution Simulation Process.  
    ///      The Evolution Simulation, will harness the Evolution Simulation Input and Blackjack Game Simulator (Strategy Tester) to run the genetic algorithm.
    /// </summary>
    public static class EvolutionSimulation
    {
        private static bool debug = false;

        /* BREEDING POOL */
        private static int _INITIAL_BREEDING_POOL;             // 100 Recommended

        /* MUTATION RATES  */
        private static int _PW_MUTATION_PER_CELL;              // = 5 Recommended
        private static int _PW_NEWBLOOD_MUTATE_WHOLE_STRATEGY; // = 4 Recommended

        /* CROSSOVER RATES */
        private static int _PW_ROW_CROSSOVER = 50;
        private static int _PW_COLUMN_CROSSOVER = 50;

        /* STRATEGY CREATION (RANDOM) */
        // The percentage weight (PW) a cell will be a Stand, Hit, Double or Surrender. (out of 100%)
        private static int _PW_STAND_HS = 32;
        private static int _PW_HIT_HS = 32;
        private static int _PW_DOUBLE_HS = 32;
        private static int _PW_SURRENDER_HS = 4;
        // The percentage weight (PW)  that a cell will be a Stand, Hit, Double, Surrender or Split. (out of 100%)
        private static int _PW_STAND_PAIR = 24;
        private static int _PW_HIT_PAIR = 24;
        private static int _PW_DOUBLE_PAIR = 24;
        private static int _PW_SURRENDER_PAIR = 4;
        private static int _PW_SPLIT_PAIR = 24;

        /* TERMINATION CONDITIONS */
        private static bool _GENERATION_COUNT_ON;
        private static int _GENERATION_COUNT;
        private static bool _STAGNATION_POINT_ON;
        private static int _STAGNATION_POINT;

        /* TARGET FITNESS MEASUREMENT */
        private static EvolutionSimulationInput.EnumFitnessMeasurement _FITNESS_MEASUREMENT_OPTION;
        private static bool _TARGET_YIELD_ON;
        private static decimal _TARGET_YIELD;
        private static bool _TARGET_PROFIT_ON;
        private static int _TARGET_PROFIT;
        private static bool _TARGET_WINS_ON;
        private static int _TARGET_WINS;

        /* GAME SIMULATION */
        private static List<PlayerStrategy> _BreedingPoolList = new List<PlayerStrategy>();
        private static int _Report_ChildCount = 0;
        private static int _StrategyColumns = 10;
        private static int _StrategyRows = 36;
        private static GameSimulationInput _GSI;
        private static EvolutionSimulationInput _ESI;       
        private static int _Stagnation_Count;

        // This is used as a placeholder when running a Game Simulation. Avoiding unnecessary UI output.
        private static TextBox blankTextBox; 

        // INITIAL: The initial method to run the genetic algorithm (must be called before the main loop to initialise).
        public static void InitialiseEvolutionSimulation(GameSimulationInput GSI, EvolutionSimulationInput ESI)
        {
            _GSI = GSI;
            _ESI = ESI;
            _INITIAL_BREEDING_POOL = ESI.GetInitialBreedingPool();
            _PW_MUTATION_PER_CELL = ESI.GetMutationChancePerCell();
            _PW_NEWBLOOD_MUTATE_WHOLE_STRATEGY = ESI.GetNewbloodChancePerChild();

            _PW_ROW_CROSSOVER = ESI.GetCrossoverRowChance();
            _PW_COLUMN_CROSSOVER = ESI.GetCrossoverColumnChance();

            _PW_STAND_HS = ESI.GetStandHSChance();
            _PW_HIT_HS = ESI.GetHitHSChance();
            _PW_DOUBLE_HS = ESI.GetDoubleHSChance();
            _PW_SURRENDER_HS = ESI.GetSurrenderHSChance();

            _PW_STAND_PAIR = ESI.GetStandPAIRChance();
            _PW_HIT_PAIR = ESI.GetHitPAIRChance();
            _PW_DOUBLE_PAIR = ESI.GetDoublePAIRChance();
            _PW_SURRENDER_PAIR = ESI.GetSurrenderPAIRChance();
            _PW_SPLIT_PAIR = ESI.GetSplitPAIRChance();

            // Termination Conditions
            _GENERATION_COUNT_ON = ESI.GetGenerationCountOn();
            _GENERATION_COUNT = ESI.GetGenerationCount();
            _STAGNATION_POINT_ON = ESI.GetStagnationPointOn();
            _STAGNATION_POINT = ESI.GetStagnationPoint();

            _FITNESS_MEASUREMENT_OPTION = ESI.GetFitnessMeasurementOption();
            _TARGET_YIELD_ON = ESI.GetTargetYieldOn();
            _TARGET_YIELD = ESI.GetTargetYield();
            _TARGET_PROFIT_ON = ESI.GetTargetProfitOn();
            _TARGET_PROFIT = ESI.GetTargetProfit();
            _TARGET_WINS_ON = ESI.GetTargetWinsOn();
            _TARGET_WINS = ESI.GetTargetWins();

            // Set Local Variables
            _BreedingPoolList.Clear();
            _Report_ChildCount = 0;
            _Stagnation_Count = 0;

            // ACTION 1: CREATE AN INITIAL POOL (up to 500) PLAYER STRATEGIES (BREEDING POOL).
            _BreedingPoolList = new List<PlayerStrategy>();
            for (int i = 0; i < _INITIAL_BREEDING_POOL; i++)
            {
                _BreedingPoolList.Add(new PlayerStrategy("Initial-" + i, CreateRandomPlayerStrategy(), _FITNESS_MEASUREMENT_OPTION));
            }

            if (debug) MessageBox.Show("Initial " + _INITIAL_BREEDING_POOL + " Breeding Pool Created.");
            if (debug) DataTableToMessageBox("sample pool number 1", _BreedingPoolList[0]);
            if (debug) DataTableToMessageBox("sample pool number 50", _BreedingPoolList[49]);

            // ACTION 1.1: SIMULATE THE INITIAL POOL TO GET THE FITNESS VALUES
            if (debug) MessageBox.Show("Calculating initial pool fitness by simulating games.");
            foreach (PlayerStrategy strategy in _BreedingPoolList)
            {
                _GSI.SetPlayerStrategy(strategy);  // Sets the strategy as the player strategy in the Game Simulation Input.
                // Creates a game report for the strategy.
                GameSimulationReport game_report = GameSimulation.RunGameSimulation(blankTextBox,_GSI, true);
                // Sets the Fitness values to the each of the strategies.
                strategy.SetGameSimulationReport(game_report);
                strategy.SetFitnessYield(game_report.GetFitnessYield());
                strategy.SetFitnessProfit(game_report.GetProfitLoss());
                strategy.SetFitnessWins(game_report.GetWins());
            }
            // Sorted the breeding pool with highest fitness at the top.
            _BreedingPoolList.Sort();
            _BreedingPoolList.Reverse();
        }

        // MAIN: The main method to run the genetic algorithm loop.
        public static List<PlayerStrategy> RunGeneticAlgorithm()
        {
            // Action 2: SELECT 2 STRATEGIES FOR BREEDING, The mother and father.
            PlayerStrategy mother = _BreedingPoolList[GenerateRandomNumber(0, _INITIAL_BREEDING_POOL - 1)];
            PlayerStrategy father = _BreedingPoolList[GenerateRandomNumber(0, _INITIAL_BREEDING_POOL - 1)];
            if (debug) MessageBox.Show("Select 2 strategies for breeding (mother and father).");
            if (debug) DataTableToMessageBox("mother", mother);
            if (debug) DataTableToMessageBox("father", father);

            // Action 3: BREED STRATEGY
            if (debug) MessageBox.Show("Breed the two strategies.");
            PlayerStrategy child = BreedStrategies(mother, father, _PW_ROW_CROSSOVER, _PW_COLUMN_CROSSOVER);
            if (debug) DataTableToMessageBox("mother", mother);
            if (debug) DataTableToMessageBox("father", father);
            if (debug) DataTableToMessageBox("child", child);

            // Action 4.1: MUTATE WHOLE STRATEGY (NEW BLOOD)
            MutateWholeStrategy(child, _PW_NEWBLOOD_MUTATE_WHOLE_STRATEGY);
            if (debug) DataTableToMessageBox("child", child);

            // Action 4.2: MUTATE STRATEGY CELLS
            MutateStrategyCells(child, _PW_MUTATION_PER_CELL);
            if (debug) DataTableToMessageBox("child mutated", child);

            // Action 5: RUN GAME SIMULATION WITH STRATEGY
            _GSI.SetPlayerStrategy(child);  // Sets the child strategy as the player strategy in the Game Simulation Input.
            if (debug) MessageBox.Show("Simulate Blackjack Game with the child strategy.");
            GameSimulationReport GameReport = GameSimulation.RunGameSimulation(blankTextBox, _GSI, true);

            // Action 6: FITNESS PRODUCED
            child.SetGameSimulationReport(GameReport);
            child.SetFitnessYield(GameReport.GetFitnessYield());
            child.SetFitnessProfit(GameReport.GetProfitLoss());
            child.SetFitnessWins(GameReport.GetWins());

            // Action 7: KEEPING THE ELITES, removing the weak (low fitness) in the breeding pool.
            EliteSelectionProcess(child);

            // Check if the breeding pool is stagnant.
            if (_STAGNATION_POINT_ON == true)
            {
                if (_Stagnation_Count >= _STAGNATION_POINT)
                {
                    // If the stagnation count has reached the stagnation point set, then the Top Strategy will be set to stagnant. If it remains stagnant the process will end. 
                    _BreedingPoolList[0].SetIsStagnant(true);
                }
            }
            // Returns the current breeding pool (list)
            return _BreedingPoolList;
        }

        // Generates a random strategy, using the cell percentage weights from the input to randomise each cell that is generated to created a strategy grid. 
        public static DataTable CreateRandomPlayerStrategy()
        {
            // Creates the structure of the Strategy grid
            DataTable strategyTable = new DataTable();
            strategyTable.Columns.Add("2");
            strategyTable.Columns.Add("3");
            strategyTable.Columns.Add("4");
            strategyTable.Columns.Add("5");
            strategyTable.Columns.Add("6");
            strategyTable.Columns.Add("7");
            strategyTable.Columns.Add("8");
            strategyTable.Columns.Add("9");
            strategyTable.Columns.Add("10");
            strategyTable.Columns.Add("A");

            // For each of the index rows in the strategy grid
            for (int row = 0; row < _StrategyRows; row++)
            {
                strategyTable.Rows.Add();
                // Generates the Hard/Soft Table (part 1)
                if(row >= 0 && row <= 26) 
                {
                    for(int col = 0; col < _StrategyColumns; col++) // index for each column
                    { 
                        // Gets a random Hard/Soft player strategy action
                        strategyTable.Rows[row][col] = GetRandomAction_HS(_PW_STAND_HS,_PW_HIT_HS,_PW_DOUBLE_HS, _PW_SURRENDER_HS); 
                    }
                }
                // Generates the Pair Table (part 2)
                if (row > 26) 
                {
                    for (int col = 0; col < _StrategyColumns; col++) // index for each column
                    {
                        // Gets a random Hard/Soft player strategy action
                        strategyTable.Rows[row][col] = GetRandomAction_PAIR(_PW_STAND_PAIR, _PW_HIT_PAIR, _PW_DOUBLE_PAIR, _PW_SURRENDER_PAIR, _PW_SPLIT_PAIR);
                    }
                }

            }
            return strategyTable;
        }

        // Gets a random action for the HS (hard/soft) action table.
        public static Actions.PlayerActionEnum_HS GetRandomAction_HS(int pw_stand_hs, int pw_hit_hs, int pw_double_hs, int pw_surrender_hs)
        {
            // Uses a secure (random) seed to create a random number between 1-100.
            int randNum = GenerateRandomNumber(1, 100); // 1% to 100%

            // Sets the start index for the between function.
            int indexStart = 0;     
            if (IsBetween(randNum, indexStart, indexStart + pw_stand_hs))
                return Actions.PlayerActionEnum_HS.S;

            // Sets the index start to the end of the previous Percentage Weight (out of 100).
            indexStart = indexStart + _PW_STAND_HS;     
            if (IsBetween(randNum, indexStart, indexStart + pw_hit_hs))
                return Actions.PlayerActionEnum_HS.H;

            // Sets the index start to the end of the previous Percentage Weight (out of 100).
            indexStart = indexStart + _PW_HIT_HS;       
            if (IsBetween(randNum, indexStart, indexStart + pw_double_hs))
                return Actions.PlayerActionEnum_HS.D;

            // Sets the index start to the end of the previous Percentage Weight (out of 100).
            indexStart = indexStart + _PW_DOUBLE_HS;    
            if (IsBetween(randNum, indexStart, indexStart + pw_surrender_hs))
                return Actions.PlayerActionEnum_HS.R;

            if (debug) MessageBox.Show("Error Found: located in the GetRandom Action Hard/Soft.\nOVERRIDE: To [S] Action.");
            return Actions.PlayerActionEnum_HS.S;
        }

        // Gets a random action for the PAIR (pair) action table.
        public static Actions.PlayerActionEnum_Pair GetRandomAction_PAIR(int pw_stand_pair, int pw_hit_pair, int pw_double_pair, int pw_surrender_pair, int pw_split_pair)
        {
            // Uses a secure (random) seed to create a random number between 1-100.
            int randNum = GenerateRandomNumber(1, 100); // 1% to 100%

            // Sets the start index for the between function.
            int indexStart = 0;     
            if (IsBetween(randNum, 0, indexStart + pw_stand_pair))
                return Actions.PlayerActionEnum_Pair.S;

            // Sets the index start to the end of the previous Percentage Weight (out of 100).
            indexStart = indexStart + _PW_STAND_PAIR;     
            if (IsBetween(randNum, indexStart, indexStart + pw_hit_pair))
                return Actions.PlayerActionEnum_Pair.H;

            // Sets the index start to the end of the previous Percentage Weight (out of 100).
            indexStart = indexStart + _PW_HIT_PAIR;       
            if (IsBetween(randNum, indexStart, indexStart + pw_double_pair))
                return Actions.PlayerActionEnum_Pair.D;

            // Sets the index start to the end of the previous Percentage Weight (out of 100).
            indexStart = indexStart + _PW_DOUBLE_PAIR;   
            if (IsBetween(randNum, indexStart, indexStart + pw_surrender_pair))
                return Actions.PlayerActionEnum_Pair.R;

            // Sets the index start to the end of the previous Percentage Weight (out of 100).
            indexStart = indexStart + _PW_SURRENDER_PAIR;
            if (IsBetween(randNum, indexStart, indexStart + pw_split_pair))
                return Actions.PlayerActionEnum_Pair.P;

            if (debug) MessageBox.Show("Error Found: located in the GetRandom Action Pair.\nOVERRIDE: To [S] Action.");
            return Actions.PlayerActionEnum_Pair.S;
        }

        // Checks if an item is beteen a given start and end value.
        public static bool IsBetween<T>(this T item, T start, T end)
        {
            return Comparer<T>.Default.Compare(item, start) > 0
                && Comparer<T>.Default.Compare(item, end) <= 0;
        }

        // Generates a Secure Random number by using a completely random seed each time.
        public static int GenerateRandomNumber(int numbers_from, int numbers_to)
        {
            Random rand = new Random(GetSecureRandomSeed());
            int randNum = rand.Next(numbers_from, numbers_to + 1);
            return randNum;
        }

        // Generate a Secure Random using cryptography, will be used as the seed for the Pseudo Random.
        public static int GetSecureRandomSeed()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[32];
                rng.GetBytes(randomNumber);

                int secure_seed = BitConverter.ToInt32(randomNumber, 0);

                return secure_seed;
            }
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

        // Breed two selected strategies together, by using the crossover (cutting) at a randomised row or column.
        public static PlayerStrategy BreedStrategies(PlayerStrategy mother, PlayerStrategy father, int pw_row_crossover, int pw_col_crossover)
        {
            _Report_ChildCount++; // Reports (adds) a counter to the child counter.

            // Generates a random number to select a crossover method. Either a Row crossover or Column crossover.
            int randColumnOrRow = GenerateRandomNumber(1, 100);

            int indexStart = 0;     // Sets the start index for the between function.
            if (IsBetween(randColumnOrRow, indexStart, indexStart + pw_row_crossover))
            {
                return RowCrossoverMethod(mother, father); // Perform row crossover function
            }

            indexStart = indexStart + pw_row_crossover;
            if(IsBetween(randColumnOrRow, indexStart, indexStart + pw_col_crossover)) 
            {
                return ColumnCrossoverMethod(mother, father); // Perform column crossover function
            }

            return ColumnCrossoverMethod(mother, father);
        }

        // Performs the breeding of two selected (mother and father) strategies via the Crossover Column method.
        public static PlayerStrategy ColumnCrossoverMethod(PlayerStrategy mother, PlayerStrategy father)
        {
            // An array of the column header names.
            String[] columnNames = { "2","3", "4", "5", "6","7","8","9","10","A" };

            List<string> listColumnNames_1stCut = new List<string>();  // The child will have the first column cut from the mother
            List<string> listColumnNames_2ndCut = new List<string>();  // and the second colomn cut from the father.

            // Generates the cut interception for column 
            int columnCut = GenerateRandomNumber(1, 9);

            // Starting col index is 1, the loop will loop through 10 times (10 column names)
            for(int col = 1; col < columnNames.Length +1; col++)
            {
                // Populates the child 1st cut with the columns of the mothers 1st cut.
                if(col <= columnCut)
                {
                    listColumnNames_1stCut.Add(columnNames[col-1]);
                }
                // Populates the child 2nd cut with the columns of the fathers 2nd cut.
                if (col > columnCut)
                {
                    listColumnNames_2ndCut.Add(columnNames[col-1]);
                }
            }

            // Coverts the columns name lists to a string arrays
            String[] columnNames_1stCut = new String[listColumnNames_1stCut.Count()];
            columnNames_1stCut = listColumnNames_1stCut.ToArray();
            String[] columnNames_2stCut = new String[listColumnNames_2ndCut.Count()];
            columnNames_2stCut = listColumnNames_2ndCut.ToArray();

            // Prepares the mother and father table cuts for the child merge.
            DataTable mothers_1stCut = mother.GetPlayerStrategyGrid().DefaultView.ToTable(false, columnNames_1stCut);
            DataTable fathers_2ndCut = father.GetPlayerStrategyGrid().DefaultView.ToTable(false, columnNames_2stCut);

            // Merges the mothers 1st cut with the fathers second cut.
            DataTable child = MergeTableColumns(mothers_1stCut, fathers_2ndCut);

            // Creates the new child strategy.
            PlayerStrategy child_strategy = new PlayerStrategy("Child" + _Report_ChildCount, child, _FITNESS_MEASUREMENT_OPTION);

            if (debug) MessageBox.Show("Column Crossover Method Performed: Cut on column " + columnCut );

            // Returns the child strategy.
            return child_strategy;
        }

        // Performs the breeding of two selected (mother and father) strategies via the Crossover row method.
        public static PlayerStrategy RowCrossoverMethod(PlayerStrategy mother, PlayerStrategy father)
        {
            // Generates the cut interception for the rows (36 rows) 
            int columnCut = GenerateRandomNumber(1, _StrategyRows);

            // Prepares the mother and father tables for the child merge.
            DataTable motherTable = mother.GetPlayerStrategyGrid();
            DataTable fatherTable = father.GetPlayerStrategyGrid();

            // Creates a new childTable.
            DataTable child = new DataTable();
            child.Columns.Add("2");
            child.Columns.Add("3");
            child.Columns.Add("4");
            child.Columns.Add("5");
            child.Columns.Add("6");
            child.Columns.Add("7");
            child.Columns.Add("8");
            child.Columns.Add("9");
            child.Columns.Add("10");
            child.Columns.Add("A");

            // Starting row index is 1, the loop will loop through 36 times (36 rows)
            for (int row = 1; row < _StrategyRows + 1; row++)
            {
                // Populates the child with the 1st cut rows of the mothers table.
                if (row <= columnCut)
                {
                    child.ImportRow(motherTable.Rows[row - 1]);
                }
                // Populates the child with the 2nd cut rows of the fathers table.
                if (row > columnCut)
                {
                    child.ImportRow(fatherTable.Rows[row - 1]);
                }
            }

            // Creates the new child strategy.
            PlayerStrategy child_strategy = new PlayerStrategy("Child" + _Report_ChildCount, child, _FITNESS_MEASUREMENT_OPTION);

            if (debug) MessageBox.Show("Row Crossover Method Performed: Cut on row " + columnCut);

            // Returns the child.
            return child_strategy;
        }

        // Merges two data tables together via the coloumn indexes.
        public static DataTable MergeTableColumns(DataTable mother, DataTable father)
        {
            if (mother == null || father == null) throw new ArgumentNullException("mother or father", "Both tables must not be null");

            // First adds columns from mother (table 1)
            DataTable child = mother.Clone();  
            foreach (DataColumn col in father.Columns)
            {
                string newColumnName = col.ColumnName;

                while (child.Columns.Contains(newColumnName))
                {
                    newColumnName = string.Format(col.ColumnName);
                }
                child.Columns.Add(newColumnName, col.DataType);
            }
            // Merges the father (table 2) coloumns by index, to createa a row (merged rows).
            var mergedRows = mother.AsEnumerable().Zip(father.AsEnumerable(),
                (r1, r2) => r1.ItemArray.Concat(r2.ItemArray).ToArray());
            foreach (object[] rowFields in mergedRows)
                child.Rows.Add(rowFields);

            // Returns the child.
            return child;
        }

        // If the New Blood percentage has been hit, the child strategy will be re rolled to an entirely new strategy (new blood).
        public static PlayerStrategy MutateWholeStrategy(PlayerStrategy child, int mutate_whole_stratgy_rate)
        {
            // Uses a secure (random) seed to create a random number between 1-100.
            int randNum = GenerateRandomNumber(1, 100); // 1% to 100%

            // If the random number hits the mutation percentage, the child strategy will be re-rolled to an entirly random strategy (new blood).
            if(randNum <= _PW_NEWBLOOD_MUTATE_WHOLE_STRATEGY)
            {
                child.SetPlayerStrategyGrid(CreateRandomPlayerStrategy());
                if (debug) MessageBox.Show("Whole Strategy Mutated.\n Mutation Rate: " + _PW_NEWBLOOD_MUTATE_WHOLE_STRATEGY + "%");
            }
            // Othersie the whole strategy is not mutated.
            else
            {
                if (debug) MessageBox.Show("Whole Strategy NOT Mutated.\n Mutation Rate: " + _PW_NEWBLOOD_MUTATE_WHOLE_STRATEGY + "%");
            }
                return child;
        }

        // Iterates through each of the cells of a child strategy, with a percent chance to mutate. When mutated the cell will be mutation to a new randomised action.
        public static PlayerStrategy MutateStrategyCells(PlayerStrategy child, int mutate_strategy_cell_rate)
        {
            // Prepares the child table (gets the child datatable)
            DataTable childTable = child.GetPlayerStrategyGrid();

            int cells_mutated = 0;
            int rowIndex = 0;
            foreach (DataRow row in childTable.Rows)
            {
                for (int col = 0; col < _StrategyColumns; col++)
                {
                    // Uses a secure (random) seed to create a random number between 1-100.
                    int randNum = GenerateRandomNumber(1, 100); // 1% to 100%

                    // If the random number hits the mutation percentage, the child strategy cell will be re-rolled to a random action.
                    if (randNum <= mutate_strategy_cell_rate)
                    {
                        cells_mutated++;
                        if (rowIndex >= 0 && rowIndex <= 26) // The Hard/Soft Table (part 1)
                        {
                            // Gets a random Hard/Soft player strategy action
                            row[col] = GetRandomAction_HS(_PW_STAND_HS, _PW_HIT_HS, _PW_DOUBLE_HS, _PW_SURRENDER_HS);

                        }
                        if (rowIndex > 26) // The Pair Table (part 2)
                        {
                            // Gets a random Pair player strategy action
                            row[col] = GetRandomAction_PAIR(_PW_STAND_PAIR, _PW_HIT_PAIR, _PW_DOUBLE_PAIR, _PW_SURRENDER_PAIR, _PW_SPLIT_PAIR);
                        }
                    }
                }
                rowIndex++;
            }
            if (debug) MessageBox.Show("Mutate Strategy Cells.\nCell Mutation Rate: " + mutate_strategy_cell_rate + "%" +
                "\nCells Mutated: " + cells_mutated);

            // The child's strategy grid is updated.
            child.SetPlayerStrategyGrid(childTable);

            // The child is returned.
            return child;
        }

        // Converts a Datatable (strategy grid) to a message box, mainly used for debugging.
        public static void DataTableToMessageBox(String stringHeader, PlayerStrategy strategy)
        {
            StringBuilder ReportString = new StringBuilder();

            ReportString.AppendLine(" ");
            ReportString.AppendLine(" ");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("--------------------------------------------------------- " +stringHeader.ToUpper() + " ---------------------------------------------------------");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");

            ReportString.AppendLine("| Strategy Name: " + strategy.GetStrategyName());

            DataTable dt = strategy.GetPlayerStrategyGrid();
            #region - Converts: DataTable -> String Table -
            // Writes the strategyTable (DataTable) to a string table. Uses no loops and manually typed to ensure formatting is fully flexible. 
            ReportString.AppendLine("|        |        2        |        3        |        4        |        5        |        6        |        7        |        8        |        9        |       10        |        A        |");
            ReportString.AppendLine("------------------------------------------------------------------------------------------------------------------------------------------");
            int rowIndex = 0; // starting row index
            int number_columns = _StrategyColumns;
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

            MessageBox.Show(ReportString.ToString());
            #endregion
        }

        // Converts a Datatable (strategy grid) a String Builder. This allows the strategy to be represented in a text form for exporting.
        public static String DataTableToString(String stringHeader, PlayerStrategy strategy)
        {
            StringBuilder ReportString = new StringBuilder();

            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            ReportString.AppendLine("--------------------------------------------------------- " + stringHeader.ToUpper() + " ---------------------------------------------------------");
            ReportString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------------");

            ReportString.AppendLine("| Strategy Name: " + strategy.GetStrategyName());

            DataTable dt = strategy.GetPlayerStrategyGrid();
            #region - Converts: DataTable -> String Table -
            // Writes the strategyTable (DataTable) to a string table. Uses no loops and manually typed to ensure formatting is fully flexible. 
            ReportString.AppendLine("|        |        2        |        3        |        4        |        5        |        6        |        7        |        8        |        9        |       10        |        A        |");
            ReportString.AppendLine("------------------------------------------------------------------------------------------------------------------------------------------");
            int rowIndex = 0; // starting row index
            int number_columns = _StrategyColumns;
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

            return ReportString.ToString();
            #endregion
        }

        // The selected and replacement of the elite strategies in the breeding pool is done here.
        public static void EliteSelectionProcess(PlayerStrategy child)
        {
            // Adds a counter to the stagnation counter. If there is no replacement done to the breeding pool, this will stack up.
            _Stagnation_Count++;
            if (debug)
            {
                StringBuilder fitnessListString = new StringBuilder();
                fitnessListString.AppendLine("Top 100 Elite's - Fitness: Target " + _FITNESS_MEASUREMENT_OPTION.ToString());
                fitnessListString.AppendLine("-------------------------------");
                foreach (PlayerStrategy strategy in _BreedingPoolList)
                {
                    switch (_FITNESS_MEASUREMENT_OPTION)
                    {
                        case EvolutionSimulationInput.EnumFitnessMeasurement.yield:
                            fitnessListString.AppendLine(strategy.GetFitnessYield().ToString());
                            break;
                        case EvolutionSimulationInput.EnumFitnessMeasurement.profit:
                            fitnessListString.AppendLine(strategy.GetFitnessProfit().ToString());
                            break;
                        case EvolutionSimulationInput.EnumFitnessMeasurement.wins:
                            fitnessListString.AppendLine(strategy.GetFitnessWins().ToString());
                            break;
                    }

                }
                MessageBox.Show(fitnessListString.ToString());
            }

            switch (_FITNESS_MEASUREMENT_OPTION)
            {
                case EvolutionSimulationInput.EnumFitnessMeasurement.yield:
                    // If the fitness of the new child strategy is better than the worst in the list. Put the new child into the breeding pool take out the other.
                    if (_BreedingPoolList[99].GetFitnessYield() < child.GetFitnessYield())
                    {
                        // If there is a replacement in the breeding pool the stagnation counter is set to zero (0).
                        _Stagnation_Count = 0;
                        int poolIndex = 0;
                        int replaceIndex = 0;
                        bool replace = false;
                        foreach (PlayerStrategy strategy in _BreedingPoolList)
                        {
                            // If the childs fitness is greater than a strategy in the pool, it will then take its place. Moving all other indexes down.
                            if (strategy.GetFitnessYield() < child.GetFitnessYield())
                            {
                                replace = true;
                                replaceIndex = poolIndex;
                                break;
                            }
                            poolIndex++;
                        }
                        if (replace)
                        {
                            _BreedingPoolList.Insert(poolIndex, child);
                            _BreedingPoolList.RemoveAt(99);
                            if (debug)
                            {
                                MessageBox.Show("Child Replaced. Fitness Value: " + child.GetFitnessYield().ToString());

                                StringBuilder fitnessListString = new StringBuilder();
                                fitnessListString.AppendLine("Top 100 Elite's - Fitness:");
                                fitnessListString.AppendLine("-------------------------------");
                                foreach (PlayerStrategy strategy in _BreedingPoolList)
                                {
                                    fitnessListString.AppendLine(strategy.GetFitnessYield().ToString());
                                }
                                MessageBox.Show(fitnessListString.ToString());
                            }
                        }
                    }
                    break;
                case EvolutionSimulationInput.EnumFitnessMeasurement.profit:
                    // If the fitness of the new child strategy is better than the worst in the list. Put the new child into the breeding pool take out the other.
                    if (_BreedingPoolList[99].GetFitnessProfit() < child.GetFitnessProfit())
                    {
                        // If there is a replacement in the breeding pool the stagnation counter is set to zero (0).
                        _Stagnation_Count = 0;
                        int poolIndex = 0;
                        int replaceIndex = 0;
                        bool replace = false;
                        foreach (PlayerStrategy strategy in _BreedingPoolList)
                        {
                            // If the childs fitness is greater than a strategy in the pool, it will then take its place. Moving all other indexes down.
                            if (strategy.GetFitnessProfit() < child.GetFitnessProfit())
                            {
                                replace = true;
                                replaceIndex = poolIndex;
                                break;
                            }
                            poolIndex++;
                        }
                        if (replace)
                        {
                            _BreedingPoolList.Insert(poolIndex, child);
                            _BreedingPoolList.RemoveAt(99);
                            if (debug)
                            {
                                MessageBox.Show("Child Replaced. Fitness Value: " + child.GetFitnessProfit().ToString());

                                StringBuilder fitnessListString = new StringBuilder();
                                fitnessListString.AppendLine("Top 100 Elite's - Fitness:");
                                fitnessListString.AppendLine("-------------------------------");
                                foreach (PlayerStrategy strategy in _BreedingPoolList)
                                {
                                    fitnessListString.AppendLine(strategy.GetFitnessProfit().ToString());
                                }
                                MessageBox.Show(fitnessListString.ToString());
                            }
                        }
                    }
                    break;
                case EvolutionSimulationInput.EnumFitnessMeasurement.wins:
                    // If the fitness of the new child strategy is better than the worst in the list. Put the new child into the breeding pool take out the other.
                    if (_BreedingPoolList[99].GetFitnessWins() < child.GetFitnessWins())
                    {
                        // If there is a replacement in the breeding pool the stagnation counter is set to zero (0).
                        _Stagnation_Count = 0;
                        int poolIndex = 0;
                        int replaceIndex = 0;
                        bool replace = false;
                        foreach (PlayerStrategy strategy in _BreedingPoolList)
                        {
                            // If the childs fitness is greater than a strategy in the pool, it will then take its place. Moving all other indexes down.
                            if (strategy.GetFitnessWins() < child.GetFitnessWins())
                            {
                                replace = true;
                                replaceIndex = poolIndex;
                                break;
                            }
                            poolIndex++;
                        }
                        if (replace)
                        {
                            _BreedingPoolList.Insert(poolIndex, child);
                            _BreedingPoolList.RemoveAt(99);
                            if (debug)
                            {
                                MessageBox.Show("Child Replaced. Fitness Value: " + child.GetFitnessWins().ToString());

                                StringBuilder fitnessListString = new StringBuilder();
                                fitnessListString.AppendLine("Top 100 Elite's - Fitness:");
                                fitnessListString.AppendLine("-------------------------------");
                                foreach (PlayerStrategy strategy in _BreedingPoolList)
                                {
                                    fitnessListString.AppendLine(strategy.GetFitnessWins().ToString());
                                }
                                MessageBox.Show(fitnessListString.ToString());
                            }
                        }
                    }
                    break;
            }
        }

    }
}
