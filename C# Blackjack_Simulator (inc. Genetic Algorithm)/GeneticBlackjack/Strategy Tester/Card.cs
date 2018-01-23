namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///     A class which describes a card Blackjack. This will be used by the Player and the Dealer as they require cards to make there hands. 
    /// </summary>
    public class Card
    {

        // The various card face values a card can have.
        public enum CardFaceENUM
        {
            Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King
        };

        // The various suits a card can be.
        public enum CardSuitENUM
        {
            Spades, Clubs, Hearts, Diamonds
        };

        // Stores the card face value: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, J, Q, K].
        private CardFaceENUM card_Face;

        // Stores the suit of the card [Spades, Clubs, Hearts, Diamonds].
        private CardSuitENUM card_Suit;

        // Stores the value of the card: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10].
        private int card_Value { set; get; }

        // Stores if the card is an Ace card.
        private bool isAce { set; get; }

        #region GETTER and SETTERS
        // Gets the card face number (stirng enum).
        public CardFaceENUM GetCardFace()
        {
            return card_Face;
        }

        // Gets the card suit (enum).
        public CardSuitENUM GetCardSuit()
        {
            return card_Suit;
        }

        // Gets the cards value.
        public int GetCardValue()
        {
            return card_Value;
        }
        #endregion

        // Overrides the ToString method to represent a card in a more understanding format. E.g. "Five of Diamonds".
        public override string ToString()
        {
            string s = card_Value + " of " + card_Suit;

            return s;
        }

        // The constructor to create a new card using the card face and its suit.
        public Card(CardFaceENUM card_face, CardSuitENUM card_suit)
        {
            this.card_Face = card_face;
            this.card_Suit = card_suit;
            isAce = false;

            // Sets the values of the card based on the card_face
            switch (card_face)
            {
                case CardFaceENUM.Ace:
                    card_Value = 1;
                    isAce = true;
                    return;
                case CardFaceENUM.Two:
                    card_Value = 2;
                    return;
                case CardFaceENUM.Three:
                    card_Value = 3;
                    return;
                case CardFaceENUM.Four:
                    card_Value = 4;
                    return;
                case CardFaceENUM.Five:
                    card_Value = 5;
                    return;
                case CardFaceENUM.Six:
                    card_Value = 6;
                    return;
                case CardFaceENUM.Seven:
                    card_Value = 7;
                    return;
                case CardFaceENUM.Eight:
                    card_Value = 8;
                    return;
                case CardFaceENUM.Nine:
                    card_Value = 9;
                    return;
                case CardFaceENUM.Ten:
                    card_Value = 10;
                    return;
                case CardFaceENUM.Jack:
                    card_Value = 10;
                    return;
                case CardFaceENUM.Queen:
                    card_Value = 10;
                    return;
                case CardFaceENUM.King:
                    card_Value = 10;
                    return;
            }
        }

        // Returns the card is ace status.
        public bool GetIsAce()
        {
            return isAce;
        }
    }
}
