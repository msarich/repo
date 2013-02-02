<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="TablesHouse.aspx.cs" Inherits="DataMart_eCPM_WebInterface.TablesHouse" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>House</h1>
    <br />
    This table represents those advertisers that should be flagged as House campaigns.  If the name of the advertiser shows up in the GDMN as House and shouldn’t be, add them to this table but mark “Is House” as FALSE.
    <br />
    <br />
    <asp:LinkButton ID="lbExportHouseDataToExcel" runat="server" OnClick="ExportCSV_Click" Text="Export Data" TableName="House"></asp:LinkButton>
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
        <asp:GridView ID="gvHouse" runat="server" AutoGenerateColumns="false" AllowSorting="true" OnSorting="gvHouseSorting" OnRowCommand="UpdateRow">
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="Update" ImageUrl="~/Images/edititem.gif"/>
                <asp:BoundField headertext="Advertiser" datafield="name" SortExpression="name" />
                <asp:BoundField headertext="Is House" datafield="is_house" SortExpression="is_house" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
