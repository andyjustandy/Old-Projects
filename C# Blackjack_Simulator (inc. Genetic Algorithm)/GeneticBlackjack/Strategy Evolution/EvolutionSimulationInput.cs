namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///      A class which describes a Evolution Simulation Input.  
    ///      Stores all of the evolution simulation input variables (input by user), to be later fed to the Evolution Simulation Process.   
    /// </summary>
    public class EvolutionSimulationInput
    {
        /* GENETIC ALGORITHM INPUT VARIABLES */
        private int _INITIAL_BREEDING_POOL; public int GetInitialBreedingPool() { return _INITIAL_BREEDING_POOL; }
        private int _MUTATION_CHANCE_PER_CELL; public int GetMutationChancePerCell() { return _MUTATION_CHANCE_PER_CELL; }
        private int _NEWBLOOD_CHANCE_PER_CHILD; public int GetNewbloodChancePerChild() { return _NEWBLOOD_CHANCE_PER_CHILD; }

        // Gets the fitness measurement input (Yield target, Profit target or Wins target).
        private EnumFitnessMeasurement _FITNESS_MEASUREMENT_OPTION; public EnumFitnessMeasurement GetFitnessMeasurementOption() { return _FITNESS_MEASUREMENT_OPTION; }
        public enum EnumFitnessMeasurement
        {
            yield,
            profit,
            wins,
            none
        }
        // Pecentage weight (mutable)
        private int _PW_CROSSOVER_ROW_CHANCE; public int GetCrossoverRowChance() { return _PW_CROSSOVER_ROW_CHANCE; }
        private int _PW_CROSSOVER_COL_CHANCE; public int GetCrossoverColumnChance() { return _PW_CROSSOVER_COL_CHANCE; }
        private int _PW_STAND_HS_CHANCE; public int GetStandHSChance() { return _PW_STAND_HS_CHANCE; }
        private int _PW_HIT_HS_CHANCE; public int GetHitHSChance() { return _PW_HIT_HS_CHANCE; }
        private int _PW_DOUBLE_HS_CHANCE; public int GetDoubleHSChance() { return _PW_DOUBLE_HS_CHANCE; }
        private int _PW_SURRENDER_HS_CHANCE; public int GetSurrenderHSChance() { return _PW_SURRENDER_HS_CHANCE; }
        private int _PW_STAND_PAIR_CHANCE; public int GetStandPAIRChance() { return _PW_STAND_PAIR_CHANCE; }
        private int _PW_HIT_PAIR_CHANCE; public int GetHitPAIRChance() { return _PW_HIT_PAIR_CHANCE; }
        private int _PW_DOUBLE_PAIR_CHANCE; public int GetDoublePAIRChance() { return _PW_DOUBLE_PAIR_CHANCE; }
        private int _PW_SPLIT_PAIR_CHANCE; public int GetSplitPAIRChance() { return _PW_SPLIT_PAIR_CHANCE; }
        private int _PW_SURRENDER_PAIR_CHANCE; public int GetSurrenderPAIRChance() { return _PW_SURRENDER_PAIR_CHANCE; }
        // Termination Conditions
        private bool _T_GENERATION_COUNT_ON; public bool GetGenerationCountOn() { return _T_GENERATION_COUNT_ON; }
        private int _T_GENERATION_COUNT; public int GetGenerationCount() { return _T_GENERATION_COUNT; }
        private bool _T_STAGNATION_POINT_ON; public bool GetStagnationPointOn() { return _T_STAGNATION_POINT_ON; }
        private int _T_STAGNATION_POINT; public int GetStagnationPoint() { return _T_STAGNATION_POINT; }
        private bool _T_TARGET_YIELD_ON; public bool GetTargetYieldOn() { return _T_TARGET_YIELD_ON; }
        private decimal _T_TARGET_YIELD; public decimal GetTargetYield() { return _T_TARGET_YIELD; }
        private bool _T_TARGET_PROFIT_ON; public bool GetTargetProfitOn() { return _T_TARGET_PROFIT_ON; }
        private int _T_TARGET_PROFIT; public int GetTargetProfit() { return _T_TARGET_PROFIT; }
        private bool _T_TARGET_WINS_ON; public bool GetTargetWinsOn() { return _T_TARGET_WINS_ON; }
        private int _T_TARGET_WINS; public int GetTargetWins() { return _T_TARGET_WINS; }

        // The constructor for the Evolution Simulation Input, that sets all of the evolution simulation input parameters.
        public EvolutionSimulationInput(int initial_breeding_pool, int mutation_chance_per_cell, int newbool_chance_per_child,
            EnumFitnessMeasurement fitness_measurement_option, int crossover_row_chance, int crossover_col_chance,
            int stand_hs_chance, int hit_hs_chance, int double_hs_chance, int surrender_hs_chance, int stand_pair_chance, 
            int hit_pair_chance, int double_pair_chance, int split_pair_chance, int surrender_pair_chance,
            bool generation_count_on, int generation_count, bool stagnation_point_on, int stagnation_point, bool target_yield_on, decimal target_yield,
            bool target_profit_on, int target_profit, bool target_wins_on, int target_wins)
        {
            // Setting local variables
            this._INITIAL_BREEDING_POOL = initial_breeding_pool;
            this._MUTATION_CHANCE_PER_CELL = mutation_chance_per_cell;
            this._NEWBLOOD_CHANCE_PER_CHILD = newbool_chance_per_child;
            this._FITNESS_MEASUREMENT_OPTION = fitness_measurement_option;
            this._PW_CROSSOVER_ROW_CHANCE = crossover_row_chance;
            this._PW_CROSSOVER_COL_CHANCE = crossover_col_chance;
            this._PW_STAND_HS_CHANCE = stand_hs_chance;
            this._PW_HIT_HS_CHANCE = hit_hs_chance;
            this._PW_DOUBLE_HS_CHANCE = double_hs_chance;
            this._PW_SURRENDER_HS_CHANCE = surrender_hs_chance;
            this._PW_STAND_PAIR_CHANCE = stand_pair_chance;
            this._PW_HIT_PAIR_CHANCE = hit_pair_chance;
            this._PW_DOUBLE_PAIR_CHANCE = double_pair_chance;
            this._PW_SPLIT_PAIR_CHANCE = split_pair_chance;
            this._PW_SURRENDER_PAIR_CHANCE = surrender_pair_chance;
            this._T_GENERATION_COUNT_ON = generation_count_on;
            this._T_GENERATION_COUNT = generation_count;
            this._T_STAGNATION_POINT_ON = stagnation_point_on;
            this._T_STAGNATION_POINT = stagnation_point;
            this._T_TARGET_YIELD_ON = target_yield_on;
            this._T_TARGET_YIELD = target_yield;
            this._T_TARGET_PROFIT_ON = target_profit_on;
            this._T_TARGET_PROFIT = target_profit;
            this._T_TARGET_WINS_ON = target_wins_on;
            this._T_TARGET_WINS = target_wins;
    }
    }
}
