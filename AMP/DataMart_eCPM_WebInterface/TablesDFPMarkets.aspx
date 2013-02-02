<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="TablesDFPMarkets.aspx.cs" Inherits="DataMart_eCPM_WebInterface.TablesDFPMarkets" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>DFP Markets</h1>
    <br />
    <asp:LinkButton ID="lbExportDFPMarketsDataToExcel" runat="server" OnClick="ExportCSV_Click" Text="Export Data" TableName="DFPMarkets"></asp:LinkButton>
    <br />
    <br />
    <div>
        <asp:Button ID="btnAddRow" runat="server" Text="Add New" OnClick="AddRow"/>
        <br />
        -OR- <asp:FileUpload ID="fuFileUpload" runat="server" Width="350px"/>
        <asp:CheckBox ID="cbFileIncludesHeader" runat="server" Text="File Includes Header Row" />
        <asp:Button ID="btnAppendFile" runat="server" Text="Append Records From CSV File" OnClick="AppendRecordsFromFile"/>
    </div>
    <br />
    <div>
        <asp:GridView ID="gvDFPMarkets" runat="server" AutoGenerateColumns="false" AllowSorting="true" OnSorting="gvDFPMarketsSorting" AllowPaging="true" OnPageIndexChanging="gvDFPMarketsPageIndexChanging" OnPageIndexChanged="gvDFPMarketsPageIndexChanged" PageSize="50" DataKeyNames="ID, sales_origin_id" OnRowCommand="UpdateRow">
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="Update" ImageUrl="~/Images/edititem.gif"/>
                <asp:BoundField headertext="ID" datafield="ID" SortExpression="ID" />
                <asp:BoundField headertext="Market Code" datafield="Market Code" SortExpression="Market Code" />
                <asp:BoundField headertext="Division" datafield="Division" SortExpression="Division" />
                <asp:BoundField headertext="GDMN Website" datafield="GDMN_Website" SortExpression="GDMN_Website" />
                <asp:BoundField headertext="GDMN Site" datafield="GDMN_Site" SortExpression="GDMN_Site" />
                <asp:BoundField headertext="sales_origin_id" Visible="false" datafield="sales_origin_id" SortExpression="sales_origin_id" />
                <asp:BoundField headertext="Primary Sales Origin" datafield="primary" SortExpression="primary" />
                <asp:BoundField headertext="Secondary Sales Origin" datafield="secondary" SortExpression="secondary" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
