<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/LoginMaster.master" CodeFile="AdminLogin13may2016.aspx.cs" Inherits="Default3" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<asp:Content ID="Content1" ContentPlaceHolderID="admin" Runat="Server">

      <div id="login">
<strong>Welcome Please login.</strong>
<form action="javascript:void(0);" method="get">
<fieldset>
<br />
<asp:TextBox ID="txtUser" runat="server" placeholder="UserName" Width="90%"> </asp:TextBox>

<br />
 <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" Width="90%" 
        TextMode="Password"></asp:TextBox>
 <br /><br />
<p><a href="passwordrecovery.aspx" style="color: #333333">Forgot Password?</a></p>
<p><asp:Button ID="btnLogin" runat="server" Text="Login" 
        onclick="btnLogin_Click"  /></p>
</fieldset>
</form>
</div> <!-- end login -->
</asp:Content>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
--%>