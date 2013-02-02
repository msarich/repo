<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="DataMart_eCPM_WebInterface._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div>
        <h1>Import 3rd Party Files</h1>
    </div>
    <br />
    <div id="ImportDiv" style="display:block;" >
        1. Copy the text to the <a href="<%=thirdPartyUploadLocation%>">repository</a>
        <br />
        2.  Refresh the drop down list of files
        <br />
        3.  Select the file to import and follow the instructions.
        <br />
        <br />
        <div style="margin-left:20px;">
            <asp:UpdatePanel ID="upImport" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                        <asp:DropDownList ID="ddlFileList" runat="server" onchange="fileListChanged()">
                            <asp:ListItem Text="Select File" Value="SelectFile"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:LinkButton ID="lbRefreshFileList" runat="server" OnClick="RefreshFileList" Text="Refresh"></asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <asp:Label ID="lblImportStarted" runat="server" ForeColor="Green"/>
            <asp:Label ID="lblSelectFileDescription" runat="server" Text="Select File Description:"/>
            <asp:LinkButton ID="lbRefreshStatus" runat="server" Text="(Refresh Status)" OnClick="RefreshStatus"></asp:LinkButton>
            <asp:RadioButtonList ID="rblImportLocation" runat="server"></asp:RadioButtonList>
            <asp:Button ID="btnImport" runat="server" Text="Import" OnClick="ImportFile"/>
        </div>
    </div>
    <hr />
    <h1>Compute Data</h1>
    <br />
    Build GDMN Table&nbsp;
    <asp:DropDownList ID="ddlMonthGDMNExport" runat="server"/>
    <asp:TextBox ID="tbYearGDMNExport" runat="server" MaxLength="4" Width="40px">YYYY</asp:TextBox>
    &nbsp;<asp:LinkButton ID="lbExportGDMNTableToExcel" runat="server" OnClick="ExportCSV_Click" Text="Export Data" TableName="GDMN"></asp:LinkButton> 
    <br />
    <asp:CheckBox ID="cbBuildHeliosData" runat="server" Text="With Helios Data" style="margin-left:20px;"/>&nbsp;<asp:Label ID="lblBuildHeliosDataStatus" runat="server"></asp:Label>
    <br />
    <asp:CheckBox ID="cbCreateGDMNTable" runat="server" Text="With DFP Data" style="margin-left:20px;"/>&nbsp;<asp:Label ID="lblCreateGDMNTable" runat="server"></asp:Label>
    <br />
    <br />
    <asp:CheckBox ID="cbCreateGDMN6LevelTable" runat="server" Text="Build GDMN_6Level Table" />
    &nbsp;<asp:Label ID="lblCreateGDMN6LevelTable" runat="server"></asp:Label>
    &nbsp;<asp:DropDownList ID="ddlMonthGDMN6Export" runat="server"/>
    <asp:TextBox ID="tbYearGDMN6Export" runat="server" MaxLength="4" Width="40px">YYYY</asp:TextBox>
    &nbsp;<asp:LinkButton ID="lbExportGDMN6LevelTableToExcel" runat="server" OnClick="ExportCSV_Click" Text="Export Data" TableName="GDMN_6Level"></asp:LinkButton>   
    <br />
    <br />
                <span>Select computation date:</span>
                <asp:DropDownList ID="ddlMonth" runat="server">
                </asp:DropDownList>
                <asp:TextBox ID="tbYear" runat="server" MaxLength="4" Width="40px">YYYY</asp:TextBox>
                &nbsp;
                <span>Email Upon Completion:</span>
                <asp:TextBox ID="tbEmail" runat="server" Width="300px"></asp:TextBox>
                &nbsp;
                <asp:Button ID="btnExecuteComputeData" runat="server" Text="Execute" OnClick="Execute" OnClientClick="return IsValid();"/>

<script type="text/javascript">

    /*function displayImportDiv() {
        if (document.getElementById("ImportDiv").style.display == "block") {
            document.getElementById("ImportDiv").style.display = "none";
        }
        else {
            document.getElementById("ImportDiv").style.display = "block";
        }
    }*/

    function fileListChanged() {
        var e = document.getElementById("<%=ddlFileList.ClientID%>");
        if (e.options[e.selectedIndex].value == "SelectFile") {
            document.getElementById("<%=lblSelectFileDescription.ClientID%>").style.display = "none";
            document.getElementById("<%=lbRefreshStatus.ClientID%>").style.display = "none";
            document.getElementById("<%=btnImport.ClientID%>").style.display = "none";
            document.getElementById("<%=rblImportLocation.ClientID%>").style.display = "none";
        }
        else {
            document.getElementById("<%=lblSelectFileDescription.ClientID%>").style.display = "inline";
            document.getElementById("<%=lbRefreshStatus.ClientID%>").style.display = "inline";
            document.getElementById("<%=btnImport.ClientID%>").style.display = "block";
            document.getElementById("<%=rblImportLocation.ClientID%>").style.display = "block";
        }
        document.getElementById("<%=lblImportStarted.ClientID%>").style.display = "none";
    }

    function IsValid() {
        var atLeastOneBoxChecked = false;
        if (document.getElementById("<%=cbBuildHeliosData.ClientID%>").checked) {
            atLeastOneBoxChecked = true;
        }
        if (document.getElementById("<%=cbCreateGDMNTable.ClientID%>").checked) {
            atLeastOneBoxChecked = true;
        }
        if (document.getElementById("<%=cbCreateGDMN6LevelTable.ClientID%>").checked) {
            atLeastOneBoxChecked = true;
        }
        if (atLeastOneBoxChecked == false) {
            alert("You must select at least one checkbox.");
        }
        return atLeastOneBoxChecked;
    }

</script>

</asp:Content>
