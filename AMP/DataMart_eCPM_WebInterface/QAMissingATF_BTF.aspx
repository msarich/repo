<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="QAMissingATF_BTF.aspx.cs" Inherits="DataMart_eCPM_WebInterface.QAMissingATF_BTF" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>ATF_BTF (Missing)</h1>
    <br />
    <asp:LinkButton ID="lbExportATF_BTFMissingDataToExcel" runat="server" OnClick="ExportCSV_Click" Text="Export Data" TableName="ATF_BTFMissing"></asp:LinkButton>
    <br />
    <br />
    <div style="overflow-x:auto;">
        <asp:GridView ID="gvATF_BTFMissing" runat="server" AllowSorting="true" EmptyDataText="There are no missing ATF_BTF records." OnSorting="gvATF_BTFMissingSorting">
        </asp:GridView>
    </div>
</asp:Content>
