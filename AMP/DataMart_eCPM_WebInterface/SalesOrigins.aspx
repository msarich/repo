<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="SalesOrigins.aspx.cs" Inherits="DataMart_eCPM_WebInterface.SalesOrigins" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>sales Origins</h1>
    <br />
    <div>
    </div>
    <br />
    <div>
        <asp:GridView ID="gvSalesOrigins" runat="server" AutoGenerateColumns="false" 
            AllowSorting="false" DataKeyNames="SalesOriginID" OnRowCommand="UpdateRow">
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="Update" ImageUrl="~/Images/edititem.gif"/>
                <asp:BoundField headertext="Primary" datafield="primary" SortExpression="primary" />
                <asp:BoundField headertext="Secondary" datafield="secondary" SortExpression="secondary" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
