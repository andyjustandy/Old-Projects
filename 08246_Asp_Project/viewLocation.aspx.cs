using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages___Copy_selectLocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initial load of the page
        if (!IsPostBack)
        {
            /* Populates the drop down list with the names of the staff when the page is loaded (only) 
            saving processing memory */
            PopulateDropDownList();
            ddlSelectLocation.Items.Insert(0, new ListItem("--Please Select--", "0"));
        }

        // Loads the grid view if there is a selection
        if (ddlSelectLocation.SelectedItem != null)
            PopulateGrid();
    }
    // Populates the drop down list with the locations
    protected void PopulateDropDownList()
    {
        // Create a new connection to the SQL database
        string CS = ConfigurationManager.ConnectionStrings["rde_463612ConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(CS))
        {
            // The SQL query to retrieve the staff details using the their unique LocationID
            SqlCommand sqlQuery = new SqlCommand("SELECT buildingname + ' ' + room AS Title, LocationID FROM location ORDER BY buildingname ASC", conn);
            // Opens the connection
            conn.Open();
            // Sets the data source to the SQL query
            ddlSelectLocation.DataSource = sqlQuery.ExecuteReader();
            ddlSelectLocation.DataTextField = "Title";
            ddlSelectLocation.DataValueField = "LocationID";
            ddlSelectLocation.DataBind();

            // Closes the connection
            conn.Close();
        }
    }

    // Populates the grid view with the people in that location
    protected void PopulateGrid()
    {
        // Create a new connection to the SQL database
        string CS = ConfigurationManager.ConnectionStrings["rde_463612ConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(CS))
        {
            // The SQL query to retrieve the staff details using the their unique UsernameID
            SqlCommand sqlQuery = new SqlCommand(
                "SELECT location.buildingname, location.room, staff.firstname, staff.surname, staffLocation.time" +
                " FROM staffLocation INNER JOIN staff ON staffLocation.usernameID = staff.UsernameID" +
                " INNER JOIN location ON staffLocation.locationID = location.LocationID" +
                " INNER JOIN staffLocation AS staffLocation_1 ON staff.UsernameID = staffLocation_1.usernameID" +
                " AND location.LocationID = staffLocation_1.locationID WHERE (staffLocation.locationID = '" + ddlSelectLocation.SelectedValue.ToString() + "')" +
                " ORDER BY staffLocation.locationID", conn);
            // Opens the connection
            conn.Open();
            // Sets the data source to the SQL query
            grdSelectedLocations.DataSource = sqlQuery.ExecuteReader();
            grdSelectedLocations.DataBind();
            // Closes the connection
            conn.Close();
        }
    }
}