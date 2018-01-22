using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sandbox : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
        }
    }
    protected void ddlSelectionChange(object sender, GridViewPageEventArgs e)
    {
        try
        {
                string query =
                    "SELECT staff.firstname, staff.surname, location.buildingname, location.room, staffLocation.time" +
                " FROM staffLocation INNER JOIN location ON staffLocation.locationID = location.LocationID" +
                " INNER JOIN staff ON staffLocation.usernameID = staff.UsernameID" +
                " INNER JOIN staffLocation AS staffLocation_1 ON location.LocationID = staffLocation_1.locationID" +
                " AND staff.UsernameID = staffLocation_1.usernameID WHERE (staffLocation.usernameID = '" + ddlSelectStaff.SelectedItem.ToString() + "')" +
                " AND (staffLocation.time BETWEEN CURRENT_TIMESTAMP - 1 AND CURRENT_TIMESTAMP)  ORDER BY staffLocation.time DESC";

                SqlDataSource1.SelectCommand = query;
        }
        catch
        {
            lblOutput.Text = "No staff found by this name.";
        }
    }

}