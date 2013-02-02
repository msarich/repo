<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="UpdateTablesDFPAdvertisers.aspx.cs" Inherits="DataMart_eCPM_WebInterface.UpdateTablesDFPAdvertisers" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
        <tr>
            <td>
                Name:
            </td>
            <td>
                <asp:TextBox ID="tbName" runat="server" Width="500px" MaxLength="64"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Match:
            </td>
            <td>
                <asp:DropDownList ID="ddlMatch" runat="server">
                    <asp:ListItem Text="-Select A Value-" Value="DoNotSave" />
                    <asp:ListItem Text="E" Value="E" />
                    <asp:ListItem Text="L" Value="L" />
                </asp:DropDownList>
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
