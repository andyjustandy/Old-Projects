<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="editStaff.aspx.cs" Inherits="EditStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>Edit Staff Details</h3>
    <table>
        <tr>
            <td>
                <p>
                    <asp:Label ID="lblStaff" runat="server" Text="Staff Member: "></asp:Label>
                </p>
            </td>
            <td>
                <p>
                    <asp:DropDownList ID="ddlSelectStaff" runat="server" AutoPostBack="false"></asp:DropDownList> 
                </p>
                </td>
        </tr>
        <tr>
            <td>
                <p>
                    <asp:Label ID="lblFirstname" runat="server" Text="Firstname: "></asp:Label>
                </p>
            </td>
            <td>
                <p>
                    <asp:TextBox ID="txtbFirstname" runat="server"></asp:TextBox> 
                    <asp:Button ID="btFirstname" runat="server" Text="Edit" OnClick="Edit_Firstname_Click"/>
                    <asp:Label ID="lblFirstnameOutput" runat="server" Text=""></asp:Label>
                </p>
            </td>

        </tr>
        <tr>
            <td>
                <p>
                    <asp:Label ID="lblSurname" runat="server" Text="Surname: "></asp:Label>
                </p>
            </td>
            <td>
                <p>
                    <asp:TextBox ID="txtbSurname" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSurname" runat="server" Text="Edit" OnClick="Edit_Surname_Click"/>
                    <asp:Label ID="lblSurnameOutput" runat="server" Text=""></asp:Label>
                </p>
            </td>
        </tr>
        <tr>
            <td>
                <p>
                    <asp:Label ID="lblEmail" runat="server" Text="Email: "></asp:Label>

                </p>
            </td>
            <td>
                <p>
                    <asp:TextBox ID="txtbEmail" runat="server"></asp:TextBox>
                    <asp:Button ID="btnEmail" runat="server" Text="Edit" OnClick="Edit_Email_Click"/>
                    <asp:Label ID="lblEmailOutput" runat="server" Text=""></asp:Label>
                </p>
            </td>
        </tr>
        <tr>
            <td>
                <p>
                    <asp:Label ID="lblContactNumber" runat="server" Text="Contact Number: "></asp:Label>
                </p>
            </td>
            <td>
                <p>
                    <asp:TextBox ID="txtbContactNum" runat="server"></asp:TextBox>
                    <asp:Button ID="btwContactNum" runat="server" Text="Edit" OnClick="Edit_ContactNum_Click"/>
                    <asp:Label ID="lblContactNumOutput" runat="server" Text=""></asp:Label>

                </p>
            </td>
        </tr> 
    <%-- Extra Functionality: <asp:Button ID="btnEdit" runat="server" Text="Update All Changes" OnClick="Edit_All_Click"/>--%>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
