<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="UpdateTablesDFPMarkets.aspx.cs" Inherits="DataMart_eCPM_WebInterface.UpdateTablesDFPMarkets" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
        <tr>
            <td>
                ID:
            </td>
            <td>
                <asp:TextBox ID="tbId" runat="server" Width="500px" MaxLength="64"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Market Code:
            </td>
            <td>
                <asp:TextBox ID="tbCode" runat="server" Width="125px"  MaxLength="16"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Division:
            </td>
            <td>
                <asp:DropDownList ID="ddlDivision" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                GDMN Website:
            </td>
            <td>
                <asp:TextBox ID="tbGDMNWebsite" runat="server" Width="500px" MaxLength="255"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                GDMN Site:
            </td>
            <td>
                <asp:TextBox ID="tbGDMNSite" runat="server" Width="500px" MaxLength="255"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Sales Origin (Primary/Secondary):
            </td>
            <td>
                <asp:DropDownList ID="ddlSalesOrigin" runat="server"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnAdd" runat="server" OnClick="AddNewRecord" Text="Insert"/>
    <asp:Button ID="btnUpdate" runat="server" OnClick="UpdateRecord" Text="Update"/>
    <asp:Button ID="btnDelete" runat="server" OnClick="DeleteRecord" Text="Delete"/>
    <asp:Button ID="btnCancel" runat="server" OnClick="Cancel" Text="Cancel"/>
    <br />
    <asp:Label ID="lblError" runat="server" ForeColor="Red"/>
</asp:Content>
