<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_Navigation.ascx.cs" Inherits="UserControls_Navigation" %>
<DynamicMenuStyle CssClass="adjustedZIndex" />
<asp:Menu ID="Menu2" runat="server" Font-Bold="true" Orientation="Horizontal" CssClass="Menu" Font-Size="14px" Font-Names="Verdana"
    ForeColor="White" Width="100%">
    <StaticMenuItemStyle Font-Size="14px"  Height="40px" />
    <DynamicMenuItemStyle Font-Size="14px" CssClass="DynamicMenu" Height="40px" HorizontalPadding="25px" />
    <DynamicHoverStyle Font-Size="14px"  CssClass="menuhover" />
    <DynamicMenuStyle CssClass="adjustedZIndex" />
    <StaticHoverStyle Font-Size="14px" CssClass="menuhover" />
    <Items>
        <asp:MenuItem Text="Master">
            <asp:MenuItem Text="My profile" Enabled="true"></asp:MenuItem>
            <asp:MenuItem Text="my Forum" Enabled="true"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="A/c Receivable">
            <asp:MenuItem Text="car" Enabled="true"></asp:MenuItem>
            <asp:MenuItem Text="Item For sale" Enabled="true"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="A/c Payable">
            <asp:MenuItem Text="Create a Forum"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Cash & Bank">
            <asp:MenuItem Text="post Answer">
                <asp:MenuItem Text="Ask Question"></asp:MenuItem>
                <asp:MenuItem Text="FAQ"></asp:MenuItem>
            </asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Payroll">
            <asp:MenuItem Text="Contact us"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Inventory">
            <asp:MenuItem Text="About Us" Enabled="true"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Reports">
            <asp:MenuItem Text="About Us" Enabled="true"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Acc.Corner">
            <asp:MenuItem Text="About Us" Enabled="true"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Communication">
            <asp:MenuItem Text="About Us" Enabled="true"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Doc Cabinet">
            <asp:MenuItem Text="About Us" Enabled="true"></asp:MenuItem>
        </asp:MenuItem>
    </Items>
</asp:Menu>
