using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QuantBet_Developer_Challenge
{
    /// <summary>
    /// Collection of the logic used to complete the QuantBet Developer Challenge.
    /// </summary>
    class LogicHelper
    {
        bool debug = false;

        /// <summary>
        /// Performs the Divisor Algorithm on the two input numbers, then returns the answer.
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns> Returns the result of the Divisor Algorithm </returns>
        public int Calculate_Divisor_Algorithm(long number1, long number2)
        {
            // Holds the highest common denominator between two numbers
            int result = 0;
            long dividend;
            long divisor;
            long quotent;
            long remainder;

            try
            {
                // Divisor Algorithm Begins: Identify the (Dividend), (Divisor), (Quotend), (Remainder)
                if (number1 >= number2)
                {
                    dividend = number1;
                    divisor = number2;
                }
                else
                {
                    dividend = number2;
                    divisor = number1;
                }
                quotent = dividend / divisor;
                remainder = dividend - (divisor * quotent);

                // If the remainder is equal to zero, highest common denominator is found (same numbers)
                if (remainder == 0)
                {
                    result = (int)divisor;
                }
                else
                {
                    // While the remainder is not equal to 0, keep performing the Divisor Algorithm
                    while (remainder != 0)
                    {
                        dividend = divisor;
                        divisor = remainder;
                        quotent = dividend / divisor;
                        remainder = dividend - (divisor * quotent);
                    }
                    // When the remainder is zero, the highest common denominator is equal to the divisor
                    result = (int)divisor;
                }
                // Sets the result to the Results textbox on screen
                return result;
            }
            catch
            {
                // If there are no number found a message to the UI is sent
                if(debug) MessageBox.Show("Error: No numbers found at this URL");
            }
            return result;
        }

        /// <summary>
        /// 1) Extracts the 2 numbers from the HTML 
        /// 2) calculates the solution using Division Algorithm
        /// 3) Inputs the answer to the webpage
        /// 4) Submits the answer by clicking the go button
        /// </summary>
        public void Extraction_Calculate_And_Submit(WebBrowser webbrowser_display)
        {
            // Extracts the numbers from the html and stores them into an array
            long[] numbers = Extract_Numbers(webbrowser_display);

            if (numbers.Length == 2)
            {
                // Calculates and stores the answer from the Divisor Algorithm
                long answer = Calculate_Divisor_Algorithm(numbers[0], numbers[1]);

                // Inputs the answer to the QuantBet webpage
                Assign_Answer_To_Browser(webbrowser_display, answer);

                // Locates and clicks the submit 'Go' button.
                Simulate_Go_Click(webbrowser_display);
            }
        }

        /// <summary>
        /// Updates the display of the web browser display in the UI with the url input text. 
        /// </summary>
        public void Update_Webbrowser_Display(WebBrowser webbrowser_display, TextBox txt_URL)
        {
            try
            {
                // Checks to see if the input url is in the correct format
                string url_string = txt_URL.Text.ToString();
                if (!url_string.StartsWith("http://") && !url_string.StartsWith("https://"))
                {
                    url_string = "https://" + url_string;
                }
                // Navigates the the input url from the textbox
                webbrowser_display.Navigate(url_string);
            }
            catch
            {
                // If the Url cannot be excecuted, nothing will happen.
            }
        }

        /// <summary>
        /// Extracts the numbers required to solved the challenge, then returns the numbers in an array (long).
        /// </summary>
        /// <returns> Returns the retreived numbers in an array of long integers. </returns>
        private long[] Extract_Numbers(WebBrowser webbrowser_display)
        {
            // Initialises the list of cities and dates
            List<long> numbers = new List<long>();
            try
            {
                // Stores the html document from the currently loaded url
                // Cast the web browser document as a mshtml.HTMLDocument
                var document = webbrowser_display.Document as HTMLDocument;

                // Gets all of the elements with the tag name: input 
                var input_elements = document.getElementsByTagName("strong");

                // Add the numbers into the numbers list
                foreach (IHTMLElement element in input_elements)
                {
                    long number = Int64.Parse(element.innerText);
                    numbers.Add(number);
                }
                // Return the numbers list in the form of an array
                return numbers.ToArray();
            }
            catch (Exception e)
            {
                if (debug) MessageBox.Show("Could not extract numbers from the Url provided: " + e.ToString());
                else MessageBox.Show("Could not extract numbers from the Url provided. ");
            }
            return numbers.ToArray();
        }

        /// <summary>
        /// Assigns the answer number to the answer box field in the browser, using the HTML Document.
        /// </summary>
        private void Assign_Answer_To_Browser(WebBrowser webbrowser_display, long calculation_result)
        {
            // Required getting the reference: Microsoft.mshtml

            // Cast the web browser document as a mshtml.HTMLDocument
            var document = webbrowser_display.Document as HTMLDocument;

            // Gets all of the elements with the tag name: input 
            var input_elements = document.getElementsByTagName("input");

            // Checks through each of the input elements, if there is a match assign the username/password to the input text
            foreach (IHTMLElement element in input_elements)
            {
                // If the element's id is 'calchainput' the input box id, then input the calculation result
                if (element.getAttribute("id") == "calchainput")
                {
                    element.innerText = calculation_result.ToString();
                }
            }
        }

        /// <summary>
        /// Locates the 'Go' submit button in the HTML and performs a click on the 'Go' button.
        /// </summary>
        private void Simulate_Go_Click(WebBrowser webbrowser_display)
        {
            // Required getting the reference: Microsoft.mshtml
            // Cast the web browser document as a mshtml.HTMLDocument
            var document = webbrowser_display.Document as HTMLDocument;

            // Gets all of the elements with the tag name: input 
            var input_buttons = document.getElementsByTagName("button");

            // Checks through each of the button elements, if there is a match click the button
            foreach (IHTMLElement button in input_buttons)
            {
                // If the element's class attribute is equal to 'btn btn--wide push-top--20' the Go button, click the button 
                if (button.getAttribute("className") == "btn btn--wide push-top--20")
                {
                    button.click();
                }
            }
        }

    }
}
