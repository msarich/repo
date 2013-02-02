<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="QASalesOrigins.aspx.cs" Inherits="DataMart_eCPM_WebInterface.QASalesOrigins" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>Sales Origins (Errors)</h1>
    <br />
    <asp:LinkButton ID="lbExportATF_BTFDataToExcel" runat="server" OnClick="ExportCSV_Click" Text="Export Data" TableName="ATF_BTF"></asp:LinkButton>
    <br />
    <br />
    <div style="overflow-x:auto;">
        <asp:GridView ID="gvSalesOrigins" runat="server" AllowSorting="true" EmptyDataText="There are no errant sales origins." OnSorting="gvSalesOriginsSorting">
        </asp:GridView>
    </div>
</asp:Content>
