using System.Collections.Generic;
using System.Windows;

namespace GeneticBlackjackSimulator
{
    /// <summary>
    ///     A class which describes a playing hand in Blackjack. This will be used by the Player and the Dealer as they require playing hands. 
    /// </summary>
    public class Hand
    {
        bool debug = false;

        // A list of card objects, representing a hand in Blackjack
        List<Card> handCards;     
        
        // A hand can be a hard, soft or pair or bust.
        HandTypeEnum handType;

        // A hand can have many states throughout the game simulaton process 
        bool _isBust = false;
        bool _hasBlackjack = false;
        bool _isSplitAce = false;
        bool _isBetDoubled = false;
        bool _isSurrendered = false;
        bool _splitLimitReached = false;
        bool _isLucky777 = false;
        bool _isSuitedBlackJack = false;
        bool _cannotSplitHand = false;
        bool _CANNOT_SPLIT_4s_5s_10s_OPTION = false;
        bool _CANNOT_SPLIT_ACES = false;
        GameSimulationInput.EnumCardBonus _cardBonusStatus = GameSimulationInput.EnumCardBonus.No_Card_Trick_Bonus;

        // Holds the different type of hands can be. A can be one of the following, hard, soft, pair or bust at any given time. 
        public enum HandTypeEnum
        {
            HARD,
            SOFT,
            PAIR,
            BUST
        };

        // The different results a hand can be in a Blackjack game.
        public enum ResultEnum
        {
            Lose,
            Win,
            Push,
            Surrendered,
            Blackjack,
            None
        }

        // Hold all possible hands that a player or dealer can have in the game of Blackjack.
        public enum HandEnum
        {
            HARD_FIVE,
            HARD_SIX,
            HARD_SEVEN,
            HARD_EIGHT,
            HARD_NINE,
            HARD_TEN,
            HARD_ELEVEN,
            HARD_TWELVE,
            HARD_THIRTEEN,
            HARD_FOURTEEN,
            HARD_FIFTEEN,
            HARD_SIXTEEN,
            HARD_SEVENTEEN,
            HARD_EIGHTEEN,
            HARD_NINTEEN,
            HARD_TWENTY,
            HARD_TWENTYONE,

            SOFT_THIRTEEN,
            SOFT_FOURTEEN,
            SOFT_FIFTEEN,
            SOFT_SIXTEEN,
            SOFT_SEVENTEEN,
            SOFT_EIGHTEEN,
            SOFT_NINETEEN,
            SOFT_TWENTY,
            SOFT_TWENTYONE,

            PAIR_TWO,
            PAIR_THREE,
            PAIR_FOUR,
            PAIR_FIVE,
            PAIR_SIX,
            PAIR_SEVEN,
            PAIR_EIGHT,
            PAIR_NINE,
            PAIR_TEN,
            PAIR_ACE,
            BUST
        };

        #region GETTERS AND SETTERS 

        // Gets the hand (list of cards).
        public List<Card> GetHand()
        {
            return this.handCards;
        }

        // Gets and sets the bet is doubled status.
        public bool GetIsBetDoubled()
        {
            return _isBetDoubled;
        }
        public void SetIsBetDoubled(bool is_doubled)
        {
            _isBetDoubled = is_doubled;
        }

        // Gets and sets the hand split ace status.
        public void SetIsSplitAce(bool isSplitAce)
        {
            _isSplitAce = isSplitAce;
        }
        public bool GetIsSplitAce()
        {
            return _isSplitAce;
        }

        // Gets and sets the hand is surrendered status.
        public void SetIsSurrendered(bool is_surrendered)
        {
            _isSurrendered = is_surrendered;
        }
        public bool IsSurrendered()
        {
            return _isSurrendered;
        }

        // Gets and sets the split limit reached status.
        public void SetSplitLimitReached(bool is_split_limit_reached)
        {
            _splitLimitReached = is_split_limit_reached;
        }
        public bool GetSplitLimitReached()
        {
            return _splitLimitReached;
        }

        // Gets and sets the hand is Blackjack status.
        public bool GetHasBlackjack()
        {
            return _hasBlackjack;
        }
        public void SetIsBlackjack(bool is_blackjack)
        {
            _hasBlackjack = is_blackjack;
        }

        // Gets and sets the hand is Lucky 777 status.
        public void SetIsLucky777(bool is_lucky_777)
        {
            _isLucky777 = is_lucky_777;
        }
        public bool GetIsLucky777()
        {
            return _isLucky777;
        }

        // Gets and sets the hand is suited Blackjack status.
        public void SetIsSuitedBlackjack(bool isSuitedBlackjack)
        {
            _isSuitedBlackJack = isSuitedBlackjack;
        }
        public bool GetIsSuitedBlackjack()
        {
            return _isSuitedBlackJack;
        }

        // Gets and sets the hand card bonus status.
        public void SetCardBonusStatus(GameSimulationInput.EnumCardBonus bonus_status)
        {
            _cardBonusStatus = bonus_status;
        }
        public GameSimulationInput.EnumCardBonus GetCardBonusStatus()
        {
            return _cardBonusStatus;
        }

        // Clears the hand. I.e. Clears the list of cards (hand).
        public void Clear()
        {
            this.handCards.Clear();
        }

        // Gets the number value of the hand, by adding the value of the cards in the hand.
        public int GetHandValue()
        {
            int value = 0,
                ace_count = 0;

            foreach (Card card in this.handCards)
            {
                // Checks if the card is an ace
                if (card.GetIsAce())            
                {
                    ace_count++;
                }
                else
                    value += card.GetCardValue();
            }
            for (int i = 0; i < ace_count; i++)
            {
                if (value + 11 > 21)
                    value += 1;
                else value += 11;
            }
            return value;
        }

        // Gets and Sets the hand bust status 
        public bool GetIsBust()
        {
            return _isBust;
        }
        public void SetIsBust(bool is_bust)
        {
            _isBust = is_bust;
        }

        #endregion

        // Creates a new hand, but also sets the debug state to the same as the debug state in the main program. To enable or disable debugging (testing)
        public Hand(bool debug)
        {
            this.debug = debug;
            handCards = new List<Card>();
        }

        // Deals a card to the hand by adding a card to the hand card list.
        public void Deal(Card card)
        {
            handCards.Add(card);
            GameSimulation.IncrementDealingIndex();
            if(debug) MessageBox.Show("Draws a " + card.ToString());
        }
        // This adds a card to the hand card list and do not increment the deck indexer. This is used when splitting pairs, to add a card to another hand (not from the deck)
        public void Add(Card card)
        {
            handCards.Add(card);
            if(debug) MessageBox.Show("Add a card " + card.ToString());
        }

        // Gets the hand enum value e.g. Hard 13.
        public HandEnum GetHandEnumValue(bool cannot_split_4s_5s_10s, bool cannot_split_aces)
        {
            _CANNOT_SPLIT_4s_5s_10s_OPTION = cannot_split_4s_5s_10s;
            _CANNOT_SPLIT_ACES = cannot_split_aces;

            int value = 0,
                ace_count = 0;

            // Gets the count of all the aces in the hand and adds the value of the hand
            foreach (Card card in handCards)
            {
                if (card.GetIsAce())            // checks if the card is an ace
                {
                    ace_count++;
                }
                else
                    value += card.GetCardValue();
            }
            for (int i = 0; i < ace_count; i++)
            {
                if (value + 11 > 21)
                {
                    value += 1; // ace value as 1
                    handType = HandTypeEnum.HARD;
                }
                else  // ace value as 11
                {
                    value += 11; 
                    handType = HandTypeEnum.SOFT;
                }
            }
            // After adding the card values, the check for busting is done.
            if (value > 21)
            {
                handType = HandTypeEnum.BUST;
            }
            else
            { // If the hand is not bust, check for the hand type
                // Check if the Hand is a Luck777 Hand  i.e. 7, 7, 7. 
                if(value == 21 && handCards.Count == 3) // check if there are 3 cards and total 21
                {
                    // Check if all three cards are 7's;
                    if(handCards[0].GetCardFace() == Card.CardFaceENUM.Seven && handCards[1].GetCardFace() == Card.CardFaceENUM.Seven 
                        && handCards[2].GetCardFace() == Card.CardFaceENUM.Seven)
                    {
                        _isLucky777 = true;
                    }
                }
                // Checks to see if the Hand is a Blackjack.
                if(value == 21 && handCards.Count == 2)
                {
                    _hasBlackjack = true;
                    if(handCards[0].GetCardSuit() == handCards[1].GetCardSuit())
                    {
                        _isSuitedBlackJack = true;
                    }
                }
                // If there are no aces, the value will be a hard value 
                if (ace_count == 0)
                {
                    handType = HandTypeEnum.HARD;
                }

                int numbef_of_cards = handCards.Count;
                // Checks for Card Bonus's
                if(numbef_of_cards >= 5)
                {
                    if (numbef_of_cards == 5)                    // If 5 card trick pays 3to2.
                        _cardBonusStatus = GameSimulationInput.EnumCardBonus.Five_Card_Trick_Bonus;
                    if (numbef_of_cards == 6)                    // If 6 card trick pays 3to2.
                        _cardBonusStatus = GameSimulationInput.EnumCardBonus.Six_Card_Trick_Bonus;
                    if (numbef_of_cards == 7)                    // If 7 card trick pays 3to2.
                        _cardBonusStatus = GameSimulationInput.EnumCardBonus.Seven_Card_Trick_Bonus;
                    if (value == 21 && numbef_of_cards == 5)     // If 21 and 5 card trick pays 2to1
                        _cardBonusStatus = GameSimulationInput.EnumCardBonus.Five_Card_21_Pays2to1;
                    if (value == 21 && numbef_of_cards > 5)      // If 21 and 5+ card trick pays 2to1
                        _cardBonusStatus = GameSimulationInput.EnumCardBonus.Five_Above_Card_21_Pays2to1;
                }
     
                // Checks the hand for pairs (if there is 2 cards)
                if (handCards.Count == 2 && handCards[0].GetCardValue() == handCards[1].GetCardValue())
                {
                    handType = HandTypeEnum.PAIR;

                    // If the cannot split 4s, 5s and 10s option is selected, it sets hands to _cannotSplitHand to True;
                    if (_CANNOT_SPLIT_4s_5s_10s_OPTION == true)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
                    {
                        if (value == 8 || value == 10 || value == 20)
                            _cannotSplitHand = true;
                    }
                    // If the cannot split aces option is selected, it sets hands to _cannotSplitHand to True;
                    if (_CANNOT_SPLIT_ACES == true)
                    {
                        if (handCards[0].GetCardFace().Equals(Card.CardFaceENUM.Ace) && handCards[1].GetCardFace().Equals(Card.CardFaceENUM.Ace))
                            _cannotSplitHand = true;
                    }

                    /* If this hand is to be Split in the strategy (however the split limit is reached) then the value will be turned to a Hard
                     * E.g. Pairs of 8's will become Hard 16 -> input as a strategy parameter. Also Pair of Ace's will become Hard 12.
                     * ***/
                    if (_splitLimitReached == true || _cannotSplitHand == true)
                    {
                        /* If the pair is an Ace, the value will equal a total of 1+1 = 2. Therefore there will not be a HardEnum Match. 
                         * This is because the value of two Ace's should be 12 (11+1). Therefore we set the value to 12.
                         * This will change from a Pair Enum to a Hard Enum.
                         * ***/
                        if (value == 2)
                        {
                            value = 13;
                            handType = HandTypeEnum.SOFT;
                            if (debug) MessageBox.Show("SPLIT LIMIT REACHED: Therefore - Pair of Aces, treated as Soft 13.");

                        }
                        /* If the pair is a pair of Two's, the value will equal 2+2 =4. Therefore there will not be a HardEnum Match.
                         * In this rare case: What should be a Hard4 will be treated the same as a Hard 5.
                         * Changng the Pair enum to a Hard enum.
                         * ***/
                        else if (value == 4)
                        {
                            value = 5;
                            handType = HandTypeEnum.HARD;
                            if (debug) MessageBox.Show("SPLIT LIMIT REACHED: Therefore - Pair of Fours, treated as Hard 5.");
                        }

                        else
                        {
                            handType = HandTypeEnum.HARD;
                        }
                    }
                    
                }

            }

                    


                // returns the Hand Enum (e.g. HARD_FIVE) depending on the type (hard, soft, pair) of hand
                switch (handType)
            {
                case HandTypeEnum.HARD:
                    switch (value)
                    {
                        case 5:
                            return HandEnum.HARD_FIVE;
                        case 6:
                            return HandEnum.HARD_SIX;
                        case 7:
                            return HandEnum.HARD_SEVEN;
                        case 8:
                            return HandEnum.HARD_EIGHT;
                        case 9:
                            return HandEnum.HARD_NINE;
                        case 10:
                            return HandEnum.HARD_TEN;
                        case 11:
                            return HandEnum.HARD_ELEVEN;
                        case 12:
                            return HandEnum.HARD_TWELVE;
                        case 13:
                            return HandEnum.HARD_THIRTEEN;
                        case 14:
                            return HandEnum.HARD_FOURTEEN;
                        case 15:
                            return HandEnum.HARD_FIFTEEN;
                        case 16:
                            return HandEnum.HARD_SIXTEEN;
                        case 17:
                            return HandEnum.HARD_SEVENTEEN;
                        case 18:
                            return HandEnum.HARD_EIGHTEEN;
                        case 19:
                            return HandEnum.HARD_NINTEEN;
                        case 20:
                            return HandEnum.HARD_TWENTY;
                        case 21:
                            return HandEnum.HARD_TWENTYONE;
                        default:
                            MessageBox.Show("ERROR DETECTED: In the HARD hand enum");
                            break;
                    }
                    break;

                case HandTypeEnum.SOFT:
                    switch (value)
                    {
                        case 13:
                            return HandEnum.SOFT_THIRTEEN;
                        case 14:
                            return HandEnum.SOFT_FOURTEEN;
                        case 15:
                            return HandEnum.SOFT_FIFTEEN;
                        case 16:
                            return HandEnum.SOFT_SIXTEEN;
                        case 17:
                            return HandEnum.SOFT_SEVENTEEN;
                        case 18:
                            return HandEnum.SOFT_EIGHTEEN;
                        case 19:
                            return HandEnum.SOFT_NINETEEN;
                        case 20:
                            return HandEnum.SOFT_TWENTY;
                        case 21:
                            _hasBlackjack = true;
                            return HandEnum.SOFT_TWENTYONE;
                        default:
                            MessageBox.Show("ERROR DETECTED: In the SOFT hand enum");
                            break;
                    }
                    break;

                case HandTypeEnum.PAIR:
                    switch (handCards[0].GetCardFace())
                    {
                        case Card.CardFaceENUM.Two:
                            return HandEnum.PAIR_TWO;
                        case Card.CardFaceENUM.Three:
                            return HandEnum.PAIR_THREE;
                        case Card.CardFaceENUM.Four:
                            return HandEnum.PAIR_FOUR;
                        case Card.CardFaceENUM.Five:
                            return HandEnum.PAIR_FIVE;
                        case Card.CardFaceENUM.Six:
                            return HandEnum.PAIR_SIX;
                        case Card.CardFaceENUM.Seven:
                            return HandEnum.PAIR_SEVEN;
                        case Card.CardFaceENUM.Eight:
                            return HandEnum.PAIR_EIGHT;
                        case Card.CardFaceENUM.Nine:
                            return HandEnum.PAIR_NINE;
                        case Card.CardFaceENUM.Ten:
                            return HandEnum.PAIR_TEN;
                        case Card.CardFaceENUM.Jack:
                            return HandEnum.PAIR_TEN;
                        case Card.CardFaceENUM.Queen:
                            return HandEnum.PAIR_TEN;
                        case Card.CardFaceENUM.King:
                            return HandEnum.PAIR_TEN;

                        case Card.CardFaceENUM.Ace:
                            return HandEnum.PAIR_ACE;
                        default:
                            MessageBox.Show("ERROR DETECTED: In the PAIR hand enum");
                            break;
                    }
                    break;
                case HandTypeEnum.BUST:
                    _isBust = true;
                    // If bust, it will return Bust
                    return HandEnum.BUST;
            }
            MessageBox.Show("The hand composition was not recognised: Therefore returned BUST as default.");
            return HandEnum.BUST;
        }
    }
}
