<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="UpdateSalesOrigins.aspx.cs" Inherits="DataMart_eCPM_WebInterface.UpdateSalesOrigins" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:HiddenField ID="hfID" runat="server" />
    <table>
        <tr>
            <td>
                Primary:
            </td>
            <td>
                <asp:Label ID="lblPrimary" runat="server"/>
                </td>
                </tr>
                <tr>
                <td>Secondary:</td><td>
                <asp:TextBox ID="tbSecondary" runat="server" MaxLength=64 Width=200px />
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnUpdate" runat="server" OnClick="UpdateRecord" Text="Update"/>
    <asp:Button ID="btnCancel" runat="server" OnClick="Cancel" Text="Cancel"/>
</asp:Content>
