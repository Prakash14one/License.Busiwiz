<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="CustomerPaymentAfter.aspx.cs" Inherits="CustomerPaymentAfter" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
 <table id="pagetbl" cellpadding="0" cellspacing="0">
 <tr><td class="hdng">Thank you !!</td></tr>
 <tr><td class="txtbg">
 <table id="pagetbl" cellpadding="0" cellspacing="0">
 <tr><td class="col2">
 <asp:Panel ID="Panel3" runat="server" Width="100%">
    
                                                        <%       Response.Write(FillInfo());   %>
                                    
                                                        </asp:Panel>
     <br />
    <%--<asp:Button ID="btnLoginNow" runat="server" Text="Login Now" OnClick="btnLoginNow_Click" />--%>
    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login Now" /></td></tr></table>
 
 </td></tr>
 <tr><td class="btmcontentbg"></td></tr></table>
 
                                                       
</asp:Content>

