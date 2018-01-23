using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuantBank_Developer_Test
{
    // Main logic for the QuantBank application.
    public class LogicHelper
    {
        private bool Pin_Allowed;
        private Generator Generator = new Generator();
        private ValidationHelper ValidationHelper = new ValidationHelper();

        /// <summary>
        /// Generates a random pin, checking if the pin complies with all of the validation
        /// rules. If the pin conflicts with one of the validation rules, ValidationChecks will 
        /// return false and a new pin will be generated (repeating the process).
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public int GeneratePin(Customer customer)
        {
            Pin_Allowed = false;
            int pin = 0000;
            // Keeps on trying to generate a new pin, until the pin is a valid pin and accepted.
            while (!Pin_Allowed)
            {
                pin = Generator.Generate_Number();

                // VALIDATION CHECKS
                Pin_Allowed = ValidationHelper.ValidataionChecks(customer, pin);
            }
            return pin;
        }
    }
}
