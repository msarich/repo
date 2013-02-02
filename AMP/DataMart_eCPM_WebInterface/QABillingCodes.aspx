<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="QABillingCodes.aspx.cs" Inherits="DataMart_eCPM_WebInterface.QABillingCodes" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>Missing Billing Codes</h1>
    <br />
    Modify this data prior to updating the GDMN tables to force billing codes to be included.
    <br />
    <br />
    <span style="color:red">IMPORTANT</span>: Any updates to this information will be overwritten the following day during the daily import.
    <br />
    <br />
    <asp:LinkButton ID="lbExportBillingCodesDataToExcel" runat="server" OnClick="ExportCSV_Click" Text="Export Data" TableName="BillingCodes"></asp:LinkButton>
    <br />
    <br />
    <div style="overflow-x:auto;">
        <asp:GridView ID="gvBillingCodes" runat="server" AllowSorting="true" EmptyDataText="There are no billing codes." OnSorting="gvBillingCodesSorting" OnRowCommand="UpdateRow">
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="Update" ImageUrl="~/Images/edititem.gif"/>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

