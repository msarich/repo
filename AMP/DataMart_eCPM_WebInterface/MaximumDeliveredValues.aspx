<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="MaximumDeliveredValues.aspx.cs" Inherits="DataMart_eCPM_WebInterface.MaximumDeliveredValues" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>Maximum Delivered Values</h1>
    <br />
    <asp:LinkButton ID="lbViewData" runat="server" OnClick="ViewData" Text="View data that exceeds those values."></asp:LinkButton>
    <br />
    <br />
    <div style="overflow-x:auto;">
        <asp:GridView ID="gvMaximumDeliveredValues" runat="server" AllowSorting="true" EmptyDataText="There are no maximum delivered values." OnSorting="gvMaximumDeliveredValuesSorting">
        </asp:GridView>
    </div>
</asp:Content>
