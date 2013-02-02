<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="UpdateQABillingCodes.aspx.cs" Inherits="DataMart_eCPM_WebInterface.UpdateQABillingCodes" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
                 
        <tr>
            <td>
                id:
            </td>
            <td>
                <asp:TextBox ID="tbId" runat="server" Width="500px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                name:
            </td>
            <td>
                <asp:TextBox ID="tbName" runat="server" Width="500px" MaxLength="128"></asp:TextBox>
            </td>
        </tr>
        <!--<tr>
            <td>
                address:
            </td>
            <td>
                <asp:TextBox ID="tbaddress" runat="server" Width="500px" MaxLength="64"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                appliedLabels:
            </td>
            <td>
                <asp:TextBox ID="tbappliedLabels" runat="server" Width="500px" MaxLength="16"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                comment:
            </td>
            <td>
                <asp:TextBox ID="tbcomment" runat="server" Width="500px" MaxLength="1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                creditStatus:
            </td>
            <td>
                <asp:TextBox ID="tbcreditStatus" runat="server" Width="500px" MaxLength="8"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                email:
            </td>
            <td>
                <asp:TextBox ID="tbemail" runat="server" Width="500px" MaxLength="1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                enableSameAdvertiserCompetitiveExclusion:
            </td>
            <td>
                <asp:TextBox ID="tbenableSameAdvertiserCompetitiveExclusion" runat="server" Width="500px" MaxLength="8"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                externalId:
            </td>
            <td>
                <asp:TextBox ID="tbexternalId" runat="server" Width="500px" MaxLength="8"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                faxPhone:
            </td>
            <td>
                <asp:TextBox ID="tbfaxPhone" runat="server" Width="500px" MaxLength="1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                primaryPhone:
            </td>
            <td>
                <asp:TextBox ID="tbprimaryPhone" runat="server" Width="500px" MaxLength="16"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                thirdPartyCompanyId:
            </td>
            <td>
                <asp:TextBox ID="tbthirdPartyCompanyId" runat="server" Width="500px" MaxLength="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                type:
            </td>
            <td>
                <asp:TextBox ID="tbtype" runat="server" Width="500px" MaxLength="32"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                creditStatusSpecified:
            </td>
            <td>
                <asp:TextBox ID="tbcreditStatusSpecified" runat="server" Width="500px" MaxLength="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                enableSameAdvertiserCompetitiveExclusionSpecified:
            </td>
            <td>
                <asp:TextBox ID="tbenableSameAdvertiserCompetitiveExclusionSpecified" runat="server" Width="500px" MaxLength="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                idSpecified:
            </td>
            <td>
                <asp:TextBox ID="tbidSpecified" runat="server" Width="500px" MaxLength="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                lastModifiedDateTime:
            </td>
            <td>
                <asp:TextBox ID="tblastModifiedDateTime" runat="server" Width="500px" MaxLength="32"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                thirdPartyCompanyIdSpecified:
            </td>
            <td>
                <asp:TextBox ID="tbthirdPartyCompanyIdSpecified" runat="server" Width="500px" MaxLength="8"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                typeSpecified:
            </td>
            <td>
                <asp:TextBox ID="tbtypeSpecified" runat="server" Width="500px" MaxLength="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                appliedTeamIds:
            </td>
            <td>
                <asp:TextBox ID="tbappliedTeamIds" runat="server" Width="500px" MaxLength="1"></asp:TextBox>
            </td>
        </tr>-->
    </table>
    <asp:Button ID="btnUpdate" runat="server" OnClick="UpdateRecord" Text="Update"/>
    <asp:Button ID="btnCancel" runat="server" OnClick="Cancel" Text="Cancel"/>
</asp:Content>