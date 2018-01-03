using System.ComponentModel;

namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///    A class which describes a strategy to be added to the ListView and converted to a fitness string. Which will be logged to the screen.
    ///    Contains the INotifyPropertyChanged interface to notify when a new strategy list has been populated
    /// </summary>
    public class PlayerStrategyFitnessString : INotifyPropertyChanged
    {
        // This variabe will hold a strategy that will be added to the ListView (Top Breeding Pool).
        private PlayerStrategy _strategy;

        // Getter and Setter for the strategy fitness string.
        public PlayerStrategy Value
        {
            get
            {
                return _strategy;
            }
            set
            {
                if (_strategy != value)
                    _strategy = value;

                OnPropertyChanged("Value");
            }
        }

        // Handles the property changed. In this case ,the value will be a strategy which will be displayed in the ListView (Top Breeding Pool).     
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
