<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ATF_BTF_Mappings.aspx.cs" Inherits="DataMart_eCPM_WebInterface.ATF_BTF_Mappings" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>ATF/BTF</h1>
    <br />
    <asp:LinkButton ID="lbExportSalesOriginsDataToExcel" runat="server" OnClick="ExportCSV_Click" Text="Export Data" TableName="SalesOrigins"></asp:LinkButton>
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
        <asp:GridView ID="gvATFBTFMappings" runat="server" AutoGenerateColumns="false" AllowSorting="true" OnSorting="gvATFBTFMappingsSorting" DataKeyNames="ATF_BTF_Mappings_Id" OnRowCommand="UpdateRow">
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="Update" ImageUrl="~/Images/edititem.gif"/> 
                <asp:BoundField headertext="Position" datafield="Position" SortExpression="Position" />
                <asp:BoundField headertext="ATF_BTF" datafield="ATF_BTF" SortExpression="ATF_BTF" />
                <asp:BoundField  headertext="USAT SITE" datafield="USAT SITE" SortExpression="USAT SITE" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
