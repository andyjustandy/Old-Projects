using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditLocation : System.Web.UI.Page
{
    // The connection string
    protected string CS = ConfigurationManager.ConnectionStrings["rde_463612ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initial load of the page
        if (!IsPostBack)
        {
            /* Populates the drop down list with the names of the staff when the page is loaded (only) 
            saving processing memory */
            PopulateStaffList();
            ddlSelectStaff.Items.Insert(0, new ListItem("--Please Select--", "0"));

            PopulateBuildingList();
            ddlSelectBuilding.Items.Insert(0, new ListItem("--Please Select--", "0"));
        }
        if(IsPostBack)
        {
            CreateUpdateLocation();
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

    /// <summary>
    /// 
    /// </summary>
    protected void PopulateBuildingList()
    {
        // Create a new connection to the SQL database
        using (SqlConnection conn = new SqlConnection(CS))
        {
            // The SQL query to retrieve the staff details using the unique LocationID
            SqlCommand sqlQuery = new SqlCommand("SELECT LocationID, buildingname FROM location ORDER BY buildingname ASC", conn);
            // Opens the connection
            conn.Open();
            // Sets the data source to the SQL query
            ddlSelectBuilding.DataSource = sqlQuery.ExecuteReader();
            ddlSelectBuilding.DataTextField = "buildingname";
            ddlSelectBuilding.DataValueField = "LocationID";
            ddlSelectBuilding.DataBind();
            // Closes the connection
            conn.Close();
        }
    }

    public static String GetTimestamp(DateTime value)
    {
        return value.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }

    protected void CreateUpdateLocation()
    {
        try
        {
            var timeStamp = GetTimestamp(DateTime.Now);

            // Create a new connection to the SQL database
            using (SqlConnection conn = new SqlConnection(CS))
            {
                // The SQL query to retrieve the staff details using the unique LocationID
                SqlCommand sqlQuery = new SqlCommand("INSERT INTO [rde_463612].[dbo].staffLocation (usernameID, locationID, time)" +
                "VALUES ('" + ddlSelectStaff.SelectedValue + "', " + ddlSelectBuilding.SelectedValue + ", '" + timeStamp + "');", conn);
                // Opens the connection
                conn.Open();
                // Sets the data source to the SQL query
                sqlQuery.ExecuteReader();
                // Closes the connection
                conn.Close();
                lblOutput.Text = "Location Update Successful.";
            }
        }
        catch
        {
            lblOutput.Text = "Update error: Please select a staff member and building.";
        }
    }

    #region Additional Function
    //protected void PopulateroomList()
    //{
    //    // Create a new connection to the SQL database
    //    string CS = ConfigurationManager.ConnectionStrings["rde_463612ConnectionString"].ConnectionString;
    //    using (SqlConnection conn = new SqlConnection(CS))
    //    {
    //        try
    //        {
    //            // The SQL query to retrieve the staff details using the unique LocationID
    //            SqlCommand sqlQuery = new SqlCommand("SELECT room FROM location WHERE (buildingname = '" + ddlSelectBuilding.SelectedItem.ToString() + "') AND (room <> '') ORDER BY room", conn);
    //            // Opens the connection
    //            conn.Open();
    //            // Sets the data source to the SQL query
    //            ddlSelectBuilding.DataSource = sqlQuery.ExecuteReader();
    //            ddlSelectBuilding.DataTextField = "room";
    //            ddlSelectBuilding.DataValueField = "LocationID";
    //            ddlSelectBuilding.DataBind();

    //            // Closes the connection
    //            conn.Close();
    //        }
    //        catch
    //        {
    //            lblroom.Text = "Select a building first.";
    //            ddlSelectroom.Visible = false;
    //        }

    //    }
    //}
    //protected void ViewButtonClick()
    //{
    //    lblroom.Visible = true;
    //    ddlSelectroom.Visible = true;
    //}
    #endregion

}