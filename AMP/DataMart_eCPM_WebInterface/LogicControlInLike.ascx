<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LogicControlInLike.ascx.cs" Inherits="DataMart_eCPM_WebInterface.LogicControlInLike" %>
<table>
    <tr>
        <td>
            <asp:DropDownList ID="ddlCategory" runat="server">
                <asp:ListItem Text="Select One"></asp:ListItem>
                <asp:ListItem Text="Billing Code in Advertiser Field" Value="StdCmpgnCustomer"></asp:ListItem>
                <asp:ListItem Text="Advertiser" Value="StdCmpgnAdvertiser"></asp:ListItem>
                <asp:ListItem Text="Calculated Payment Type" Value="CalculatedPayType"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="text-align:right;">
            <asp:DropDownList ID="ddlInOperator" runat="server">
                <asp:ListItem Text="AND"></asp:ListItem>
                <asp:ListItem Text="OR"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            in:
        </td>
        <td>
            <asp:TextBox ID="tbInValues" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="text-align:right;">
            <asp:DropDownList ID="ddlNotInOperator" runat="server">
                <asp:ListItem Text="AND"></asp:ListItem>
                <asp:ListItem Text="OR"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            not in:
        </td>
        <td>
            <asp:TextBox ID="tbNotInValues" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="text-align:right;">
            <asp:DropDownList ID="ddlLikeOperator" runat="server">
                <asp:ListItem Text="AND"></asp:ListItem>
                <asp:ListItem Text="OR"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            like:
        </td>
        <td>
            <asp:TextBox ID="tbLikeValues" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="text-align:right;">
            <asp:DropDownList ID="ddlNotLikeOperator" runat="server">
                <asp:ListItem Text="AND"></asp:ListItem>
                <asp:ListItem Text="OR"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            not like:
        </td>
        <td>
            <asp:TextBox ID="tbNotLikeValues" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
</table>

