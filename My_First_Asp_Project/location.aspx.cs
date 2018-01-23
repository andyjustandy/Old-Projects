using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class location : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Creates a connection to the database
        string CS = ConfigurationManager.ConnectionStrings["rde_463612ConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(CS))
        {

        }

        // Handles the POST request
        if (Request.HttpMethod == "POST")
        {

        }

        // Handles the GET request
        if (Request.HttpMethod == "GET")
        {
            if (Request.QueryString.Count > 0)
            {
                string v = Request.QueryString[0];
                if (v != null)
                {
                    Response.Write("param is ");
                    Response.Write(v);
                }
            }
            if (Request.QueryString.Count > 1)
            {
                foreach (string varName in Request.QueryString)
                {
                    string v = Request.QueryString[varName];
                    if (v != null)
                    {
                        Response.Write(v + " is in ");
                        Response.Write("Here");
                    }
                }
            }

        }
    }
}