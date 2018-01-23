using System;
using System.Security.Cryptography;
using System.Text;
using static GeneticBlackjackSimulator.Card;

namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///    A class which describes aplaying deck in Blackjack. This will be used by the Player and the Dealer as they require cards from the deck.
    ///     Cards from the deck will be dealt to the Player/Dealer, when all the cards have been used. The deck will be shuffled and randomised.
    /// </summary>
    public class Deck 
    {
        /* LOCAL DECK VARIABLES */
        int DECK_MULTIPLIER;
        // A deck will be made from a stack (list) of cards.
        public Card[] deck { get; }
        private int NUMBER_OF_CARDS{ get; }
        // The deck will be shuffled using a random number generator. However Using Cryptography to create a new seed every time (creating a secure random).
        private Random rand;

        // Overrides the ToString method when represending a game deck (used to show the deck composition).
        public override string ToString()
        {
            StringBuilder stringbuilder = new StringBuilder();
            foreach (Card c in deck)
            {
                stringbuilder.Append(c.ToString() + " ");
            }
            return stringbuilder.ToString();
        }

        // The constructor to create a new deck. The deck will be constructed from the nunber of in a deck, time the deck multiplier E.g. (52* 6 deck_multiplier)
        public Deck(int cardsPerDeck, int deckMultiplier)
        {
            this.DECK_MULTIPLIER = deckMultiplier;

            this.NUMBER_OF_CARDS = cardsPerDeck * deckMultiplier;

            CardFaceENUM[] faces = {CardFaceENUM.Ace, CardFaceENUM.Two, CardFaceENUM.Three, CardFaceENUM.Four, CardFaceENUM.Five,
                CardFaceENUM.Six, CardFaceENUM.Seven, CardFaceENUM.Eight, CardFaceENUM.Nine, CardFaceENUM.Ten, CardFaceENUM.Jack,
                CardFaceENUM.Queen, CardFaceENUM.King };
            CardSuitENUM[] suits = { CardSuitENUM.Hearts, CardSuitENUM.Clubs, CardSuitENUM.Spades, CardSuitENUM.Diamonds };
            deck = new Card[NUMBER_OF_CARDS];

            // creates the random number (shuffle) for the new deck and sets the seed to the generated Secure Rand.
            rand = new Random(GetSecureRandomSeed());

            for (int i = 0; i < deck.Length; i++)
            {
                deck[i] = new Card(faces[i % (13)], suits[i % 4]);
            }

            ShuffleDeck();
        }

        // Generate a Secure Random using cryptography, will be used as the seed for the Pseudo Random.
        public int GetSecureRandomSeed()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[32];
                rng.GetBytes(randomNumber);
                int secure_seed = BitConverter.ToInt32(randomNumber, 0);

                return secure_seed;
            }
        }

        // Shuffles the deck with a random set of cards.
        public void ShuffleDeck()
        {
            for (int first = 0; first < deck.Length; first++)
            {
                int second = rand.Next(NUMBER_OF_CARDS);
                Card temp = deck[first];
                deck[first] = deck[second];
                deck[second] = temp;
            }
        }


        /* Gets the composition of the deck, e.g. 4 Ace's, 4 Two's, 4 Three's... 4 King's. 
           Stores them into an array. */
        public int[] DeckComposition()
        {
            // There will always be 13 types of cards in a deck. A = index0, 2 = index1, 3 = index2... K = index12.
            int[] composition = new int[13];
            foreach (Card card in this.deck)
            {
                string stringCard = card.ToString();

                if (stringCard.StartsWith("A"))
                {
                    composition[0] = composition[0] + 1;
                }
                else if (stringCard.StartsWith("2"))
                {
                    composition[1] = composition[1] + 1;
                }
                else if (stringCard.StartsWith("3"))
                {
                    composition[2] = composition[2] + 1;
                }
                else if (stringCard.StartsWith("4"))
                {
                    composition[3] = composition[3] + 1;
                }
                else if (stringCard.StartsWith("5"))
                {
                    composition[4] = composition[4] + 1;
                }
                else if (stringCard.StartsWith("6"))
                {
                    composition[5] = composition[5] + 1;
                }
                else if (stringCard.StartsWith("7"))
                {
                    composition[6] = composition[6] + 1;
                }
                else if (stringCard.StartsWith("8"))
                {
                    composition[7] = composition[7] + 1;
                }
                else if (stringCard.StartsWith("9"))
                {
                    composition[8] = composition[8] + 1;
                }
                else if (stringCard.StartsWith("10"))
                {
                    composition[9] = composition[9] + 1;
                }
                else if (stringCard.StartsWith("J"))
                {
                    composition[10] = composition[10] + 1;
                }
                else if (stringCard.StartsWith("Q"))
                {
                    composition[11] = composition[11] + 1;
                }
                else if (stringCard.StartsWith("K"))
                {
                    composition[12] = composition[12] + 1;
                }
            }
            return composition;
        }
    }
}










