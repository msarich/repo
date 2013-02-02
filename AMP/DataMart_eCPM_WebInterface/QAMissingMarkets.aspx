<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="QAMissingMarkets.aspx.cs" Inherits="DataMart_eCPM_WebInterface.QAMissingMarkets" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>New Markets in DFP not identified in the Data Warehouse</h1>
    <br />
    <asp:LinkButton ID="lbExportMissingMarketsDataToExcel" runat="server" OnClick="ExportCSV_Click" Text="Export Data" TableName="MissingMarkets"></asp:LinkButton>
    <br />
    <br />
    <div style="overflow-x:auto;">
        <asp:GridView ID="gvMissingMarkets" runat="server" AutoGenerateColumns="false" AllowSorting="true" EmptyDataText="There are no missing markets." OnSorting="gvMissingMarketsSorting" DataKeyNames="AdUnit_1" OnRowCommand="AddRowToMarket">
            <Columns>
                <asp:ButtonField ButtonType="Link" CommandName="Add" Text="Add" />
                <asp:BoundField headertext="Market Name" datafield="MarketName" SortExpression="MarketName" />
                <asp:BoundField headertext="Division Name" datafield="DivisionName" SortExpression="DivisionName" />
                <asp:BoundField  headertext="Website Name" datafield="WebsiteName" SortExpression="WebsiteName" />
                <asp:BoundField headertext="App" datafield="App" SortExpression="App" />
                <asp:BoundField headertext="Customer" datafield="Customer" SortExpression="Customer" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
