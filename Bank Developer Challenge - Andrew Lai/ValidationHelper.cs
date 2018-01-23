using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantBank_Developer_Test
{
    public class ValidationHelper
    {
        /// <summary>
        /// ALL VALIDATION METHOD CHECKS SHOULD BE ADDED HERE.
        /// This method goes through each of the pin validation checks, returning true if the pin passes all of the validation checks.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="pin"></param>
        /// <returns> Returns true if the pin is valide, returns false if the pin is not valid. </returns>
        public bool ValidataionChecks(Customer customer, int pin)
        {
            // Validation 1: Pin four digits long
            if (PinLengthCheck(pin))
            {
                return false;
            }
            // Validation 2: Only allow 2 consecutive numbers e.g. 1211
            if (ConsecutiveNumberCheck(pin))
            {
                return false;
            }
            // Validation 3: Must not contain complete consecutive number sequence e.g. 1234
            if (SequenceCheck(pin))
            {
                return false;
            }
            // Validation 4: Distinct from the past three pins
            if (PastPinCheck(customer, pin))
            {
                return false;
            }
            // Validation 5: Cannot be contain in the users sortcode
            if (SortCodeContainCheck(customer, pin))
            {
                return false;
            }
            // Validation 6: Cannot be contain in the users account number
            if (SortCodeContainCheck(customer, pin))
            {
                return false;
            }
            //ADDITIONAL Validation 7: Pin cannot begin with the number 1.
            if (BeginWithOneCheck(pin))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validation 1: Pin four digits long e.g. 1023
        /// </summary>
        /// <param name="pin"></param>
        /// <returns> Returns true if the pin does not comply with this validation rule. </returns>
        public bool PinLengthCheck(int pin)
        {
            if (pin.ToString().Length == 4)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        
        /// <summary>
        /// Validation 2: Only allow 2 consecutive numbers e.g. 1211
        /// </summary>
        /// <param name="pin"></param>
        /// <returns> Returns true if the pin does not comply with this validation rule. </returns>
        public bool ConsecutiveNumberCheck(int pin)
        {
            int MAX_CONSECUTIVE_NUMBERS = 2;

            char[] pin_array = pin.ToString().ToCharArray();
            int consecutiveCount = 1;

            // Loops through each number, except the end number (Length-1) therefore there is always a char to compare
            for(int i = 0; i < pin_array.Length-1; i++)
            {
                if (pin_array[i] == pin_array[i+1])
                {
                    consecutiveCount++;
                }
                else
                {
                    consecutiveCount = 1;
                }
                if(consecutiveCount > MAX_CONSECUTIVE_NUMBERS)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validation 3: Must not contain complete consecutive number sequence e.g. 1234
        /// </summary>
        /// <param name="pin"></param>
        /// <returns> Returns true if the pin does not comply with this validation rule. </returns>
        public bool SequenceCheck(int pin)
        {
            char[] pin_array = pin.ToString().ToCharArray();
            int sequenceCount = 1;

            // Loops through each number, except the end number (Length-1) therefore there is always a value to compare
            for (int i = 0; i < pin_array.Length - 1; i++)
            {
                if (pin_array[i]+1 == pin_array[i+1])
                {
                    sequenceCount++;
                }
                else
                {
                    return false;
                }
            }
            if (sequenceCount == pin.ToString().Length)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Validation 4: Distinct from the past three pins.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="pin"></param>
        /// <returns> Returns true if the pin does not comply with this validation rule. </returns>
        public bool PastPinCheck(Customer customer, int pin)
        {
            if (pin == customer.GetPrevious_Pin1() || pin == customer.GetPrevious_Pin2() || pin == customer.GetPrevious_Pin3())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validation 5: Pin cannot be contained in the users sortcode.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="pin"></param>
        /// <returns> Returns true if the pin does not comply with this validation rule. </returns>
        public bool SortCodeContainCheck(Customer customer, int pin)
        {
            if (customer.GetSortCode().ToString().Contains(pin.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validation 6: Pin cannot be contained in the users account number.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="pin"></param>
        /// <returns> Returns true if the pin does not comply with this validation rule. </returns>
        public bool AccountNumberContainCheck(Customer customer, int pin)
        {      
            if (customer.GetAccountNumber().ToString().Contains(pin.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ADDITIONAL Validation 7: Pin cannot begin with the number 1.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="pin"></param>
        /// <returns> Returns true if the pin does not comply with this validation rule. </returns>
        public bool BeginWithOneCheck(int pin)
        {
            if (pin.ToString().StartsWith("1"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
