using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewStaff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {           
        // Initial load of the page
        if (!IsPostBack)
        {
            /* Populates the drop down list with the names of the staff when the page is loaded (only) 
            saving processing memory */
            PopulateDropDownList();
            ddlSelectStaff.Items.Insert(0, new ListItem("--Please Select--", "0"));
        }

        // Loads the grid view if there is a selection
        if (ddlSelectStaff.SelectedItem != null)
        PopulateGrid();

    }
    // Populates the grid view with the data about the staff
    protected void PopulateGrid()
    {
        // Create a new connection to the SQL database
        string CS = ConfigurationManager.ConnectionStrings["rde_463612ConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(CS))
        {
            // The SQL query to retrieve the staff details using the their unique UsernameID
            SqlCommand sqlQuery = new SqlCommand(
                "SELECT staff.firstname, staff.surname, location.buildingname, location.room, staffLocation.time" +
                " FROM staffLocation INNER JOIN location ON staffLocation.locationID = location.LocationID" +
                " INNER JOIN staff ON staffLocation.usernameID = staff.UsernameID" +
                " INNER JOIN staffLocation AS staffLocation_1 ON location.LocationID = staffLocation_1.locationID" +
                " AND staff.UsernameID = staffLocation_1.usernameID WHERE (staffLocation.usernameID = '" + ddlSelectStaff.SelectedValue.ToString() + "')" +
                " AND (staffLocation.time BETWEEN CURRENT_TIMESTAMP - 1 AND CURRENT_TIMESTAMP)  ORDER BY staffLocation.time DESC", conn);
            // Opens the connection
            conn.Open();
            // Sets the data source to the SQL query
            grdSelectedStaff.DataSource = sqlQuery.ExecuteReader();
            grdSelectedStaff.DataBind();
            // Closes the connection
            conn.Close();
        }
    }

    // Populates the drop down list with the names of the staff
    protected void PopulateDropDownList()
    {
        // Create a new connection to the SQL database
        string CS = ConfigurationManager.ConnectionStrings["rde_463612ConnectionString"].ConnectionString;
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
}