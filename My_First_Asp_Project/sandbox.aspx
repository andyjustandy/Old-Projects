<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="sandbox.aspx.cs" 
    Inherits="sandbox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p>SANDBOX
        
        <br />
        
        <br />
            Select a staff member:
            <asp:DropDownList ID="ddlSelectStaff" runat="server" DataSourceID="StaffFullName" 
                DataTextField="Title" DataValueField="Title" AutoPostBack="True">
            </asp:DropDownList>
            <asp:SqlDataSource ID="StaffFullName" runat="server" ConnectionString="<%$ ConnectionStrings:rde_463612ConnectionString %>" SelectCommand=
                    "SELECT location.buildingname + ' ' + location.room AS Building, location.LocationID FROM location INNER JOIN staffLocation ON location.LocationID = staffLocation.locationID INNER JOIN staff ON staffLocation.usernameID = staff.UsernameID WHERE (location.buildingname &lt;&gt; '')">
            </asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ 
                ConnectionStrings:rde_463612ConnectionString %>" SelectCommand=
                "SELECT staff.firstname, staff.surname, location.buildingname, location.room, 
                staffLocation.time FROM staffLocation INNER JOIN location ON staffLocation.locationID = location.LocationID
                INNER JOIN staff ON staffLocation.usernameID = staff.UsernameID 
                INNER JOIN staffLocation AS staffLocation_1 ON location.LocationID = staffLocation_1.locationID 
                AND staff.UsernameID = staffLocation_1.usernameID WHERE (staffLocation.usernameID = 'Alai') 
                AND (staffLocation.time BETWEEN CURRENT_TIMESTAMP - 1 AND CURRENT_TIMESTAMP) 
                ORDER BY staffLocation.time DESC">
            </asp:SqlDataSource>
            
            <asp:GridView ID="grdSelectedStaff" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="White" />
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
    </p>
        
    <table ID="tblUpdateLocation" runat="server">
    <tr>
        <td>

        </td>
        <td style="width: 93px">

        </td>
    </tr>   
    <tr>
        <td>

        </td>
        <td style="width: 93px">

        </td>
    </tr>
            <tr>
        <td>

        </td>
        <td style="width: 93px">

        </td>
    </tr>
    </table>
    <asp:Label ID="lblOutput" runat="server" Text="Label"></asp:Label>

</asp:Content>


