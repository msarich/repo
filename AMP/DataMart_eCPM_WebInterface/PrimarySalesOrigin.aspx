<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="PrimarySalesOrigin.aspx.cs" Inherits="DataMart_eCPM_WebInterface.PrimarySalesOrigin" %>

<%@ Register src="LogicControlWhen.ascx" tagname="LogicControlWhen" tagprefix="uc1" %>
<%@ Register src="LogicControlInLike.ascx" tagname="LogicControlInLike" tagprefix="uc2" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>Primary Sales Origin Logic</h1>
    <br />
    Sales Origin:
    <asp:DropDownList ID="ddlSalesOrigin" runat="server">
        <asp:ListItem Text="- Select a Sales Origin -" Value="NoneSelected"></asp:ListItem>
        <asp:ListItem Text="Local" Value="Local"></asp:ListItem>
        <asp:ListItem Text="House" Value="House"></asp:ListItem>
        <asp:ListItem Text="Internal" Value="Internal"></asp:ListItem>
        <asp:ListItem Text="Remnant" Value="Remnant"></asp:ListItem>
        <asp:ListItem Text="National" Value="National"></asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="btnChangeSalesOrigin" runat="server" Text="Load Sales Origin Logic" OnClientClick="return ClientSalesOriginChanged();" OnClick="ChangeSalesOrigin" />
    <br />
    <asp:Button ID="btnAddTableRow" runat="server" Text="Add" OnClick="AddTableRow" Visible="false"/>
    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="UpdateLogic" Visible="false"/>
    <br />
    <uc1:LogicControlWhen ID="lcilDefault" runat="server" Visible="false">
        <LogicControlInLikes>
            <uc2:LogicControlInLike runat="server" Visible="true"></uc2:LogicControlInLike>
            <uc2:LogicControlInLike runat="server" Visible="false"></uc2:LogicControlInLike>
            <uc2:LogicControlInLike runat="server" Visible="false"></uc2:LogicControlInLike>
        </LogicControlInLikes>
    </uc1:LogicControlWhen>

<script type="text/javascript">

    function ClientSalesOriginChanged() {
        return confirm("If you change the selected Sales Origin all unsaved data will be lost.  Do you wish to continue?");
    }

</script>

</asp:Content>
