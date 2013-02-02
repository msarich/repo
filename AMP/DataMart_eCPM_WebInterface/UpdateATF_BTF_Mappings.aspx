<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="UpdateATF_BTF_Mappings.aspx.cs" Inherits="DataMart_eCPM_WebInterface.UpdateATF_BTF_Mappings" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:HiddenField ID="hfATF_BTF_Mappings_Id" runat="server" />
    <table>
        <tr>
            <td>
                Position:
            </td>
            <td>
                <asp:TextBox ID="tbPosition" runat="server" Width="400px"  MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ATF/BTF:
            </td>
            <td>
                <asp:DropDownList ID="ddlATF_BTF" runat="server">
                    <asp:ListItem Text="ATF" Value="ATF"></asp:ListItem>
                    <asp:ListItem Text="BTF" Value="BTF"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                USAT Site:
            </td>
            <td>
                <asp:CheckBox ID="cbUSATSite" runat="server"></asp:CheckBox>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnAdd" runat="server" OnClick="AddNewRecord" Text="Insert"/>
    <asp:Button ID="btnUpdate" runat="server" OnClick="UpdateRecord" Text="Update"/>
    <asp:Button ID="btnDelete" runat="server" OnClick="DeleteRecord" Text="Delete"/>
    <asp:Button ID="btnCancel" runat="server" OnClick="Cancel" Text="Cancel"/>
</asp:Content>
