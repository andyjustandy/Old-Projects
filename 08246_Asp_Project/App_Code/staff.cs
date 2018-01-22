using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for staff
/// </summary>
public class Staff
{
    public int usernameID { get; set; }
    public string firstName { get; set; }
    public string surname { get; set; }
    public string email { get; set; }
    public int contactNumber { get; set; }

    public Staff(int usernameID, string firstName, string surname, string email, int contactNumber)
	{
        this.usernameID = usernameID;
        this.firstName = firstName;
        this.surname = surname;
        this.email = email;
        this.contactNumber = contactNumber;
	}
}