<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="UpdateDivisions.aspx.cs" Inherits="DataMart_eCPM_WebInterface.UpdateDivisions" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:HiddenField ID="hfDivisionID" runat="server" />
    <table>
        <tr>
            <td>
                Division Name:
            </td>
            <td>
                <asp:TextBox ID="tbDivisionName" runat="server" Width="500px" MaxLength="120"/>
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnAdd" runat="server" OnClick="AddNewRecord" Text="Insert"/>
    <asp:Button ID="btnUpdate" runat="server" OnClick="UpdateRecord" Text="Update"/>
    <asp:Button ID="btnDelete" runat="server" OnClick="DeleteRecord" Text="Delete"/>
    <asp:Button ID="btnCancel" runat="server" OnClick="Cancel" Text="Cancel"/>
</asp:Content>
