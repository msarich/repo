<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="TablesDFPAdvertisers.aspx.cs" Inherits="DataMart_eCPM_WebInterface.TablesDFPAdvertisers" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>DFP Advertisers</h1>
    <br />
    <asp:LinkButton ID="lbExportDFPAdvertisersDataToExcel" runat="server" OnClick="ExportCSV_Click" Text="Export Data" TableName="DFPAdvertisers"></asp:LinkButton>
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
        <asp:GridView ID="gvDFPAdvertisers" runat="server" AutoGenerateColumns="false" AllowSorting="true" OnSorting="gvDFPAdvertisersSorting" DataKeyNames="sales_origin_id" OnRowCommand="UpdateRow">
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="Update" ImageUrl="~/Images/edititem.gif"/>
                <asp:BoundField headertext="Name" datafield="name" SortExpression="name" />
                <asp:BoundField headertext="Match" datafield="match" SortExpression="match" />
                <asp:BoundField headertext="Sales Origin" datafield="sales_origin" SortExpression="sales_origin" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
