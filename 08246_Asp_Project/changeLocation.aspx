<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="changeLocation.aspx.cs" Inherits="EditLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>Update Staff Location</h3>

    <asp:Label ID="lblStaff" runat="server" Text="Staff Member: "></asp:Label> <asp:DropDownList ID="ddlSelectStaff" runat="server" AutoPostBack="false" ></asp:DropDownList>
    <asp:Label ID="lblBuilding" runat="server" Text="Building: "></asp:Label> <asp:DropDownList ID="ddlSelectBuilding" runat="server" AutoPostBack="false" ></asp:DropDownList>
<%-- Additional Room functionality - <asp:Button ID="btwViewRooms" runat="server" Text="View Rooms" AutoPostBack="false" />
    <asp:Label ID="lblRoom" runat="server" Text="Room:     "></asp:Label> <asp:DropDownList ID="ddlSelectRoom" runat="server" AutoPostBack="false" ></asp:DropDownList>--%>
    <asp:Button ID="btnUpdateLocation" runat="server" Text="Update" />
    <asp:Label ID="lblOutput" runat="server" Text=""></asp:Label>
    
    <p>




    </p>
        

</asp:Content>