using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace QuantBank_Developer_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool debug = false;
        private LogicHelper LogicHelper = new LogicHelper();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_GeneratePin_Click(object sender, RoutedEventArgs e)
        {           
            Customer customer = Create_Customer();

            try
            {
                int pin = LogicHelper.GeneratePin(customer);
                lbl_ResultPin.Content = pin.ToString();
            }
            catch 
            {
                MessageBox.Show("Please enter ALL customer details.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // Creates a new customer object, using the input values from the user.
        private Customer Create_Customer()
        {
            try
            {
                Customer customer = new Customer(
                   txtb_FirstName.Text,
                   txtb_LastName.Text,
                   txtb_DOB.Text,
                   txtb_SortCode.Text,
                   txtb_AccountNumber.Text,
                   txtb_PreviousPin1.Text,
                   txtb_PreviousPin2.Text,
                   txtb_PreviousPin3.Text);

                return customer;
            }
            catch (Exception e)
            {
                // Could not process customer (input error). If debug mode is enabled.
                if(debug)MessageBox.Show("Could not process customer, input error: " + e);
            }
            return null;
        }

        #region --- USER INPUT VALIDATION ---

        // Key validation for numeric textbox.
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

        // Checks the character length of the number entered, does not allow more than MAX LENTH.
        private void Number_Length_Validation(TextBox textbox, KeyEventArgs e, int MAX_LENGTH)
        {
            if (textbox.Text.Count() >= MAX_LENGTH)
            {
                e.Handled = true;
            }
        }


        private void txtb_SortCode_KeyDown(object sender, KeyEventArgs e)
        {
            Numeric_Validation(sender, e);
            Number_Length_Validation(txtb_SortCode, e, 6);
        }

        private void txtb_AccountNumber_KeyDown(object sender, KeyEventArgs e)
        {
            Numeric_Validation(sender, e);
            Number_Length_Validation(txtb_AccountNumber, e, 9);
        }

        private void txtb_PreviousPin1_KeyDown(object sender, KeyEventArgs e)
        {
            Numeric_Validation(sender, e);
            Number_Length_Validation(txtb_PreviousPin1, e, 4);
        }

        private void txtb_PreviousPin2_KeyDown(object sender, KeyEventArgs e)
        {
            Numeric_Validation(sender, e);
            Number_Length_Validation(txtb_PreviousPin2, e, 4);
        }

        private void txtb_PreviousPin3_KeyDown(object sender, KeyEventArgs e)
        {
            Numeric_Validation(sender, e);
            Number_Length_Validation(txtb_PreviousPin3, e, 4);
        }

        #endregion


    }
}
