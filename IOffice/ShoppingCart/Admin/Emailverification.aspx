<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Emailverification.aspx.cs" Inherits="ShoppingCart_Admin_Emailverification" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:Panel ID="pnlconfirmationmessage" runat="server" >
    <table style="width:100%">
    <tr>
    <td>
    <asp:Label ID="lblmsg1" runat="server" > </asp:Label>
     <asp:Label ID="lblmsg2" runat="server" > </asp:Label>
      <asp:Label ID="lblmsg3" runat="server" > </asp:Label>
    </td>
    </tr>
    
    </table>
    </asp:Panel>
     
     <asp:Panel ID="pnloption1user" runat="server" Visible="false" >
      <table>
     <tr>
     <td>
     
      <asp:Label ID="Label4" runat="server" Text="For user" > </asp:Label>
     <asp:Label ID="lbloption1username" runat="server" > </asp:Label>
      <asp:Label ID="Label6" runat="server" Text="for the company" > </asp:Label>
      <asp:Label ID="lbloption1companyname" runat="server" > </asp:Label>
      
     </td>
     </tr>
     </table>
     </asp:Panel>
     
     <asp:Panel ID="pnloptionforcompany" runat="server" Visible="false" >
     <table>
     <tr>
     <td>
      <asp:Label ID="Label3" runat="server" Text="For all users of the company" > </asp:Label>
     <asp:Label ID="lblcompanyname" runat="server"  > </asp:Label>
      
     </td>
     </tr>
     </table>
     </asp:Panel>
    </div>
    </form>
</body>
</html>
