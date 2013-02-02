<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="UpdateTablesHouse.aspx.cs" Inherits="DataMart_eCPM_WebInterface.UpdateTablesHouse" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
        <tr>
            <td>
                Advertiser:
            </td>
            <td>
                <asp:TextBox ID="tbName" runat="server" Width="500px" MaxLength="64"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Is House:
            </td>
            <td>
                <asp:DropDownList ID="ddlIsHouse" runat="server">
                    <asp:ListItem Text="-Select A Value-" Value="DoNotSave" />
                    <asp:ListItem Text="True" Value="1" />
                    <asp:ListItem Text="False" Value="0" />
                </asp:DropDownList>
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
