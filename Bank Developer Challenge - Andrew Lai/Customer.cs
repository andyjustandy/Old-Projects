using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantBank_Developer_Test
{
    /// <summary>
    /// A class that represents a customer of QuantBank.
    /// </summary>
    public class Customer
    {
        // Local Variables
        private string first_name;
        private string last_name;
        private string date_of_birth;
        private int sortcode;
        private int accountnumber;
        private int previous_pin_1; 
        private int previous_pin_2;
        private int previous_pin_3;

        // Getters
        public int GetSortCode()
        {
            return sortcode;
        }
        public int GetAccountNumber()
        {
            return accountnumber;
        }
        public int GetPrevious_Pin1()
        {
            return previous_pin_1;
        }
        public int GetPrevious_Pin2()
        {
            return previous_pin_2;
        }
        public int GetPrevious_Pin3()
        {
            return previous_pin_3;
        }

        // Customer constructor
        public Customer(string first_name, string last_name, string date_of_birth, string sortcode, string accountnumber, 
            string previous_pin_1, string previous_pin_2, string previous_pin_3)
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.date_of_birth = date_of_birth;
            this.sortcode = Convert.ToInt32(sortcode);
            this.accountnumber = Convert.ToInt32(accountnumber);
            this.previous_pin_1 = Convert.ToInt32(previous_pin_1);
            this.previous_pin_2 = Convert.ToInt32(previous_pin_2);
            this.previous_pin_3 = Convert.ToInt32(previous_pin_3);
        }
    }
}
