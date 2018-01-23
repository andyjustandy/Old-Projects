using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditStaff : System.Web.UI.Page
{
    protected string CS = ConfigurationManager.ConnectionStrings["rde_463612ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            /* Populates the drop down list with the names of the staff when the page is loaded (only) 
            saving processing memory */
            PopulateStaffList();
            ddlSelectStaff.Items.Insert(0, new ListItem("--Please Select--", "0"));
        }

    }

    // Populates the drop down list with the names of the staff
    protected void PopulateStaffList()
    {
        using (SqlConnection conn = new SqlConnection(CS))
        {
            // The SQL query to retrieve the staff details using the their unique UsernameID
            SqlCommand sqlQuery = new SqlCommand("SELECT firstname + ' ' + surname AS Title, UsernameID FROM staff ORDER BY firstname ASC", conn);
            // Opens the connection
            conn.Open();
            // Sets the data source to the SQL query
            ddlSelectStaff.DataSource = sqlQuery.ExecuteReader();
            ddlSelectStaff.DataTextField = "Title";
            ddlSelectStaff.DataValueField = "UsernameID";
            ddlSelectStaff.DataBind();
            // Closes the connection
            conn.Close();
        }
    }
    protected void ClearAllFields()
    {
        PopulateStaffList();
        ddlSelectStaff.Items.Insert(0, new ListItem("--Please Select--", "0"));
        txtbFirstname.Text = "";
        txtbSurname.Text = "";
        txtbEmail.Text = "";
        txtbContactNum.Text = "";
    }

    protected bool ValidationFirstName()
    {
        // Removes all irrelevant validation output messages
        lblSurnameOutput.Text = "";
        lblEmailOutput.Text = "";
        lblContactNumOutput.Text = "";
        if (ddlSelectStaff.SelectedItem.Text == "--Please Select--")
        {
            lblFirstnameOutput.Text = "Input error detected: Please pick a Staff member from the drop down menu.";
            return false;
        }
        if (txtbFirstname.Text == "")
        {
            lblFirstnameOutput.Text = "Input error detected: Please enter a new firstname.";
            return false;
        }
        if (!txtbFirstname.Text.All(Char.IsLetter))
        {
            lblFirstnameOutput.Text = "Input error detected: Please enter an appropriate firstname (letters a-z).";
            return false;
        }
        return true;
    }
    protected bool ValidationSurname()
    {
        // Removes all irrelevant validation output messages
        lblFirstnameOutput.Text = "";
        lblEmailOutput.Text = "";
        lblContactNumOutput.Text = "";
        if (ddlSelectStaff.SelectedItem.Text == "--Please Select--")
        {
            lblSurnameOutput.Text = "Input error detected: Please pick a Staff member from the drop down menu.";
            return false;
        }
        if (txtbSurname.Text == "")
        {
            lblSurnameOutput.Text = "Input error detected: Please enter an appropriate surname.";
            return false;
        }
        if (!txtbSurname.Text.All(Char.IsLetter))
        {
            lblSurnameOutput.Text = "Input error detected: Please enter an appropriate appropriate surname (letters a-z).";
            return false;
        }
        return true;
    }
    protected bool ValidationEmail()
    {
        // Removes all irrelevant validation output messages
        lblFirstnameOutput.Text = "";
        lblSurnameOutput.Text = "";
        lblContactNumOutput.Text = "";
        if (ddlSelectStaff.SelectedItem.Text == "--Please Select--")
        {
            lblEmailOutput.Text = "Input error detected: Please pick a Staff member from the drop down menu.";
            return false;
        }

        if (txtbEmail.Text == "")
        {
            lblEmailOutput.Text = "Input error detected: Please enter an appropriate email.";
            return false;
        }
        try
        {
            var mail = new System.Net.Mail.MailAddress(txtbEmail.Text);
        }
        catch
        {
            lblEmailOutput.Text = "Input error detected: Please enter a valid email address.";
            return false;
        }
        return true;
    }
    protected bool ValidationContactNum()
    {
        // Removes all irrelevant validation output messages
        lblSurnameOutput.Text = "";
        lblEmailOutput.Text = "";
        lblContactNumOutput.Text = "";
        if (ddlSelectStaff.SelectedItem.Text == "--Please Select--")
        {
            lblContactNumOutput.Text = "Input error detected: Please pick a Staff member from the drop down menu.";
            return false;
        }
        if (txtbContactNum.Text == "")
        {
            lblContactNumOutput.Text = "Input error detected: Please enter an apprpriate number.";
            return false;
        }
        if (!txtbContactNum.Text.All(Char.IsNumber))
        {
            lblContactNumOutput.Text = "Input error detected: Please enter an appropriate contact number (numbers 0-9).";
            return false;
        }
        if (txtbContactNum.Text.Length > 15 || txtbContactNum.Text.Length < 7)
        {
            lblContactNumOutput.Text = "Input error detected: Please a contact number at appropriate length. Must have at most 15 numbers and at least 7  (standard geographic maximum and minimum).";
            return false;
        }
        return true;
    }


    protected void Edit_Firstname_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationFirstName())
            {
                // Create a new connection to the SQL database
                using (SqlConnection conn = new SqlConnection(CS))
                {
                    // The SQL query to retrieve the staff details using the unique UsernameID
                    SqlCommand sqlQuery = new SqlCommand("UPDATE [rde_463612].[dbo].staff SET firstname = '" + txtbFirstname.Text + "' WHERE UsernameID = '" + ddlSelectStaff.SelectedValue + "';", conn);
                    // Opens the connection
                    conn.Open();
                    // Sets the data source to the SQL query
                    sqlQuery.ExecuteReader();
                    // Closes the connection
                    conn.Close();
                    lblFirstnameOutput.Text = "Firstname change to " + txtbFirstname.Text + " Successful.";
                    // Sets all the input fields back to default (empty)
                    ClearAllFields();
                    // Removes all irrelevant validation output messages
                    lblSurnameOutput.Text = "";
                    lblEmailOutput.Text = "";
                    lblContactNumOutput.Text = "";
                }
            }
        }
        catch
        {
            lblFirstnameOutput.Text = "Edit error caught: Please check your input.";
        }
    }

    protected void Edit_Surname_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationSurname())
            {
                // Create a new connection to the SQL database
                using (SqlConnection conn = new SqlConnection(CS))
                {
                    // The SQL query to retrieve the staff details using the unique UsernameID
                    SqlCommand sqlQuery = new SqlCommand("UPDATE [rde_463612].[dbo].staff SET surname = '" + txtbSurname.Text + "' WHERE UsernameID = '" + ddlSelectStaff.SelectedValue + "';", conn);
                    // Opens the connection
                    conn.Open();
                    // Sets the data source to the SQL query
                    sqlQuery.ExecuteReader();
                    // Closes the connection
                    conn.Close();
                    lblSurnameOutput.Text = "Surname change to " + txtbSurname.Text + " Successful.";
                    // Sets all the input fields back to default (empty)
                    ClearAllFields();
                    // Removes all irrelevant validation output messages
                    lblFirstnameOutput.Text = "";
                    lblEmailOutput.Text = "";
                    lblContactNumOutput.Text = "";
                }
            }
        }
        catch
        {
            lblSurnameOutput.Text = "Edit error caught: Please check your input.";
        }
    }

    protected void Edit_Email_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationEmail())
            {
                // Create a new connection to the SQL database
                using (SqlConnection conn = new SqlConnection(CS))
                {
                    // The SQL query to retrieve the staff details using the unique UsernameID
                    SqlCommand sqlQuery = new SqlCommand("UPDATE [rde_463612].[dbo].staff SET email = '" + txtbEmail.Text + "' WHERE UsernameID = '" + ddlSelectStaff.SelectedValue + "';", conn);
                    // Opens the connection
                    conn.Open();
                    // Sets the data source to the SQL query
                    sqlQuery.ExecuteReader();
                    // Closes the connection
                    conn.Close();
                    lblEmailOutput.Text = "Email change to " + txtbEmail.Text + " Successful.";
                    // Removes all irrelevant validation output messages
                    lblFirstnameOutput.Text = "";
                    lblSurnameOutput.Text = "";
                    lblContactNumOutput.Text = "";
                }
            }
        }
        catch
        {
            lblEmailOutput.Text = "Edit error caught: Please check your input.";
        }
    }

    protected void Edit_ContactNum_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationContactNum())
            {
                // Create a new connection to the SQL database
                using (SqlConnection conn = new SqlConnection(CS))
                {
                    // The SQL query to retrieve the staff details using the unique UsernameID
                    SqlCommand sqlQuery = new SqlCommand("UPDATE [rde_463612].[dbo].staff SET contactnumber = '" + txtbContactNum.Text + "' WHERE UsernameID = '" + ddlSelectStaff.SelectedValue + "';", conn);
                    // Opens the connection
                    conn.Open();
                    // Sets the data source to the SQL query
                    sqlQuery.ExecuteReader();
                    // Closes the connection
                    conn.Close();
                    lblContactNumOutput.Text = "Contact number change to " + txtbContactNum.Text + " Successful.";
                    // Removes all irrelevant validation output messages
                    lblFirstnameOutput.Text = "";
                    lblSurnameOutput.Text = "";
                    lblEmailOutput.Text = "";
                }
            }
        }
        catch
        {
            // Catches exception and sends the user an error message
            lblContactNumOutput.Text = "Edit error caught: Please check your input.";
        }
    }
}