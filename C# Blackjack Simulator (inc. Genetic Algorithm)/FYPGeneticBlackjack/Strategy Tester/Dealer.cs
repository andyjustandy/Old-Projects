namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///     A class which describes a dealer in Blackjack. 
    ///     The dealer will be used within the game simulation to play against a player. Using its dealer strategy and playing by the game input variables.
    /// </summary>
    public class Dealer
    {
        /* LOCAL DEALER VARIABLES */
        private DealerStrategy _dealer_Strategy;   
        private Hand _dealer_Hand;                 // holds the dealer's hand
        private Card _dealer_UpCard;               // holds the dealer's up card (revealed card)

        // The constructor to create a new Dealer. The dealer will take in a dealer playing strategy to be used in the game simulation.
        public Dealer(DealerStrategy dealer_strategy, bool debug)
        {
            this._dealer_Strategy = dealer_strategy;
            this._dealer_Hand = new Hand(debug);
        }

        #region SETTERS and GETTERS
        // Gets the dealers hand (list of cards)
        public Hand GetDealerHand()
        {
            return _dealer_Hand;
        }
        
        // Gets the dealers up card (revealed card)
        public Card GetDealerUpCard()
        {
            return _dealer_UpCard;
        }

        // Gets the dealers playing strategy
        public DealerStrategy GetDealerStrategy()
        {
            return _dealer_Strategy;
        }
        #endregion

        // Adds a card to the dealers hand and updates the index of the next card to be dealt.
        public void DealtoDealer(Card card, Deck deck)
        {
            try
            {
                // Deals a card to the dealer.
                _dealer_Hand.Deal(card);

                // If the dealer has two cards, the second card dealt will be the Up-Card.
                if (_dealer_Hand.GetHand().Count == 2)
                    _dealer_UpCard = _dealer_Hand.GetHand()[0];
            } catch
            {
                // The cards go over the deck limit.
                deck.ShuffleDeck();
                GameSimulation._DECK_DEALING_INDEX = 0;

                // Deals a card to the dealer.
                _dealer_Hand.Deal(card);

                // If the dealer has two cards, the second card dealt will be the Up-Card.
                if (_dealer_Hand.GetHand().Count == 2)
                    _dealer_UpCard = _dealer_Hand.GetHand()[0];
            }   
        }
    }
}
