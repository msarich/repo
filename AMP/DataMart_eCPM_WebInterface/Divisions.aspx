<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Divisions.aspx.cs" Inherits="DataMart_eCPM_WebInterface.Divisions" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>Divisions</h1>
    <br />
    <div>
        <asp:Button ID="btnAddRow" runat="server" Text="Add New" OnClick="AddRow"/>
    </div>
    <br />
    <div>
        <asp:GridView ID="gvDivisions" runat="server" AutoGenerateColumns="false" AllowSorting="true" OnSorting="gvDivisionsSorting" DataKeyNames="DivisionID" OnRowCommand="UpdateRow">
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="Update" ImageUrl="~/Images/edititem.gif"/>
                <asp:BoundField headertext="Division Name" datafield="DivisionName" SortExpression="DivisionName" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
