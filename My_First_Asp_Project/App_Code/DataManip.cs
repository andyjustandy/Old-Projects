using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DataManip
/// </summary>
public static class DataManipulation
{
    private static SqlConnection conn;
    private static SqlCommand command;

    static DataManipulation()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionA"].ToString();
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("", conn);
    }

    //public static ArrayList Get
}