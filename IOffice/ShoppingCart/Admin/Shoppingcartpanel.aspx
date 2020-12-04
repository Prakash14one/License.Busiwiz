<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="Shoppingcartpanel.aspx.cs" Inherits="ShoppingCart_Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset>
        <legend>Eplaza Master Information </legend>
        <asp:Panel ID="pnlwebsiteadd" runat="server">
            <label><asp:Label ID="Label2" runat="server" Text="Do you wish to use your own domain name for your shopping cart? For example, "></asp:Label><asp:Label ID="Label4" runat="server" Text="http://www.yourcompanyname.com/shoppingcart/default.aspx?cid="></asp:Label><asp:Label ID="lblcid1" runat="server" Text="Label"></asp:Label></label>
            <asp:CheckBox ID="CheckBox1" runat="server" Text="Yes" AutoPostBack="true" 
                TextAlign="Right" oncheckedchanged="CheckBox1_CheckedChanged" />
            <asp:Panel ID="pnladd" Visible="false" runat="server">
                <label><asp:Label ID="Label6" runat="server" Text="Your Domain Name :"></asp:Label></label>
                <label><asp:DropDownList ID="ddlhyper" runat="server">
                        <asp:ListItem Text="http" Value="0"></asp:ListItem><asp:ListItem Text="https" Value="1"></asp:ListItem></asp:DropDownList></label>
                <label><asp:TextBox ID="txtdomain" runat="server"></asp:TextBox></label>
                 
                <label><asp:Label ID="lbldot" runat="server" Text="."></asp:Label></label>
                <label><asp:DropDownList ID="ddldomaintype" runat="server">
                        <asp:ListItem Text="com" Value="0"></asp:ListItem><asp:ListItem Text="us" Value="0"></asp:ListItem><asp:ListItem Text="ca" Value="0"></asp:ListItem><asp:ListItem Text="in" Value="0"></asp:ListItem><asp:ListItem Text="uk" Value="0"></asp:ListItem><asp:ListItem Text="eu" Value="0"></asp:ListItem><asp:ListItem Text="net" Value="0"></asp:ListItem><asp:ListItem Text="org" Value="0"></asp:ListItem><asp:ListItem Text="biz" Value="0"></asp:ListItem></asp:DropDownList></label>
                <div style="clear: both;">
            </div>
            <div style="text-align:center;">
                <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btnSubmit" 
                    onclick="Button1_Click" />
            </div>
            </asp:Panel>
        </asp:Panel>
        <div style="clear: both;">
            </div>
        <asp:Panel ID="pnlwebsitelabel" runat="server">
            <label><asp:Label ID="Label1" runat="server" Text="Your master URL for your shopping cart is "></asp:Label><asp:Label ID="lblwebsite" ForeColor="#457cec" runat="server" Text=""></asp:Label></label>
            <div style="clear: both;">
            </div>
            <label><asp:Label ID="Label3" runat="server" Text="You have mentioned at the time of registration that you plan to use your own domain name - "></asp:Label><asp:Label ID="txtwebsite1" ForeColor="#457cec" runat="server" Text=""></asp:Label></label>
            <div style="clear: both;">
            </div>
            <label><asp:Label ID="Label5" runat="server" Text="If you plan to use your own domain name for your shopping cart, please ensure that the DNS record for your domain name - "></asp:Label><asp:Label ID="txtwebsite12" ForeColor="#457cec" runat="server" Text=""></asp:Label><asp:Label ID="Label7" runat="server" Text="- is changed to forward the traffic to http://www.eplaza.us"></asp:Label></label>
            <div style="clear: both;">
            </div>
            <label><asp:Label ID="Label8" runat="server" Text="Consult your domain registrar for completing this process. "></asp:Label></label>
            <div style="clear: both;">
            </div>
            <label><asp:Label ID="Label9" runat="server" Text="Once you have completed this, your URL for the shopping cart will be as below:"></asp:Label></label>
            <div style="clear: both;">
            </div>
            <label><asp:Label ID="Label10" ForeColor="#457cec" runat="server" Text="http://"></asp:Label><asp:Label ID="txtwebsite13" ForeColor="#457cec" runat="server" Text=""></asp:Label><asp:Label ID="Label12" ForeColor="#457cec" runat="server" Text="/shoppingcart/default.aspx?cid="></asp:Label><asp:Label ID="lblcid" ForeColor="#457cec" runat="server" Text=""></asp:Label></label>
            
        </asp:Panel>
        <div style="clear: both;">
            </div>
            <label>
                <asp:Label ID="Label14" runat="server" Text="You have provided the following paypal ID at the time of registration. Please note that all of the payments from your customers will be deposited in your paypal account directly."></asp:Label>
            </label>
             <div style="clear: both;">
            </div>
            <label>
                <asp:Label ID="Label15" runat="server" Text="Paypal ID"></asp:Label>
            </label>
            <label>
                <asp:Label ID="txtPaypalId" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
            </label>
    </fieldset>
</asp:Content>
