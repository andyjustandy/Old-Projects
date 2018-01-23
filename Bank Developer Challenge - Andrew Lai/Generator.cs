using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuantBank_Developer_Test
{
    // Random number generating logic.
    public class Generator
    {
        /// <summary>.
        /// Generates a random number using the Secure random number as the seed of a Pseudo random number generator.
        /// </summary>
        /// <returns> Returns the Pseudo Secure random number. </returns>
        public int Generate_Number()
        {
            // Sets a new random seed to the generated from a Secure Random number.
            Random rand = new Random(GetSecureRandomSeed());

            // Generates a random number between 1000 and 9999 (Random 4 digit number).
            int random_number = rand.Next(1000, 9999);

            // Returns a random 4 digit number.
            return random_number;
        }

        /// <summary>
        /// Generate a Secure Random using cryptography, will be used as the seed for the Pseudo Random.
        /// Generates the secure random number by converting the last 4 bytes (32 bits) into an integer.
        /// </summary>
        /// <returns> Returns a random secure seed as an integer. </returns>
        private int GetSecureRandomSeed()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                // Creates a 32 byte array
                byte[] randomNumber = new byte[32];

                // Stores the bytes into the byte array
                rng.GetBytes(randomNumber);

                // Converts the bytes into 32 bits (the size of an integer)
                int secure_seed = BitConverter.ToInt32(randomNumber, 0);

                // Returns the secure seed as an integer
                return secure_seed;
            }
        }
    }
}
