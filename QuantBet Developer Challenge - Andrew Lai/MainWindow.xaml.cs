using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QuantBet_Developer_Challenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LogicHelper logic = new LogicHelper();

        public MainWindow()
        {
            InitializeComponent();
        }

        #region ---   MANUAL SOLUTION   ---
        // When the Calculate button is clicked, the Divisor Algorithm is performed on the two numbers.      
        private void btn_Calculate_Click(object sender, RoutedEventArgs e)
        {
            long number1 = 0;
            long number2 = 0;

            try
            {
                // Checks if two numbers have been input, by trying to parse the text
                number1 = long.Parse(txtb_Number1.Text);
                number2 = long.Parse(txtb_Number2.Text);
                
                // Performs to Diviser Algorithm to get the 'result' answer
                long result = logic.Calculate_Divisor_Algorithm(number1, number2);

                // Displays the answer to the GUI
                txtb_Result.Content = "Answer = " + result.ToString();
            }
            catch
            {
                // Cannot parse numbers: Do nothing...
            }
        }

        // Upon clicking the textbox to input a new number, the existing content is erased.
        private void txtb_Number1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtb_Number1.Text = "";
        }

        // Upon clicking the textbox to input a new number, the existing content is erased.
        private void txtb_Number2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtb_Number2.Text = "";
        }

        // Validates the key input to allow only numeric values
        private void txtb_Number1_KeyDown(object sender, KeyEventArgs e)
        {
            Numeric_Validation(sender, e);
            Number_Length_Validation(txtb_Number1, e);
        }

        // Validates the key input to allow only numeric values
        private void txtb_Number2_KeyDown(object sender, KeyEventArgs e)
        {
            Numeric_Validation(sender, e);
            Number_Length_Validation(txtb_Number2, e);
        }

        // Key validation for numeric textbox
        public void Numeric_Validation(object sender, KeyEventArgs e)
        {
            {
                // Allows numeric values (no decimal)
                bool isNumPadNumeric = (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9);
                bool isNumeric = (e.Key >= Key.D0 && e.Key <= Key.D9);

                // Will not allow a number held plus a modifier (no decimal)
                if ((isNumeric || isNumPadNumeric) && Keyboard.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                    return;
                }

                // Allows keyboard controls
                bool isControl = ((Keyboard.Modifiers != ModifierKeys.None && Keyboard.Modifiers != ModifierKeys.Shift)
                    || e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Insert
                    || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up
                    || e.Key == Key.Tab
                    || e.Key == Key.PageDown || e.Key == Key.PageUp
                    || e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Escape
                    || e.Key == Key.Home || e.Key == Key.End);

                // Discard keys that is not a numeric value or keyboard control
                e.Handled = !isControl && !isNumeric && !isNumPadNumeric;
            }
        }

        // Checks the character length of the number entered, does not allow more than 19characters
        private void Number_Length_Validation(TextBox textbox, KeyEventArgs e)
        {
            int MAX_LENGTH = 19;
            if (textbox.Text.Count() >= MAX_LENGTH)
            {
                e.Handled = true;
            }
        }
        #endregion

        #region ---   AUTOMATIC SOLUTION   ---
        // Search button will use the input Url link and display the webpage in the application browser.
        private void btn_SearchUrl_Click(object sender, RoutedEventArgs e)
        {
            logic.Update_Webbrowser_Display(webbrowser_display, txt_URL);
        }

        // Extracts the 2 numbers from the HTML, calculates the solution using the Divisor Algorithm and submits the form.
        private void btn_Calculate_And_Submit_Click(object sender, RoutedEventArgs e)
        {
            logic.Extraction_Calculate_And_Submit(webbrowser_display);
        }

        /// <summary>
        /// The automatic solution to the developers challenge. Performs all of the action:
        /// - Loads the webpage in the application browser, using url
        /// - Automatically extract and store required data to solve the math question
        /// - Calculates the answer to the question using the Divisor Algorithm
        /// - Inputs and submits the question to the webpage
        /// 
        /// Note: The time delay may need increasing from 200 (o.3 seconds) depending on how fast the users computer can load the webpage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_AutomaticSubmission_Click(object sender, RoutedEventArgs e)
        {
            // Updates the browser display with url input
            logic.Update_Webbrowser_Display(webbrowser_display, txt_URL);

            // Waits for the webpage to load
            await Task.Delay(300);

            // Extracts the 2 numbers from the HTML, calculates the solution and submits the form
            logic.Extraction_Calculate_And_Submit(webbrowser_display);
        }
        #endregion

    }
}
