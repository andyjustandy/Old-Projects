using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///     A view that contains a list of strategies (Breeding Pool) to be logged to the screen in the 
    ///     Evolution simulation process.
    /// </summary>
    public class StrategyViewModel : ObservableCollection<PlayerStrategyFitnessString>
    {

        public StrategyViewModel() : base() { }

        // Creates a new random number and creates a RandomValue from that.
        public void AddValue(List<PlayerStrategy> elite10Strategies)
        {
            this.Clear();
            foreach (PlayerStrategy strategy in elite10Strategies)
            {
                this.Add(new PlayerStrategyFitnessString { Value = (strategy) });
            }
        }
    }
}
