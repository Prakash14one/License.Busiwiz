<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcontrolHelpPanel.ascx.cs" Inherits="Account_UserControl_UcontrolHelpPanel" %>
  <style type="text/css">
      .style1
      {
          width: 20px;
      }
  </style>
  <%--   <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />--%>


  <%--  <table width="100%"  runat="server" >
        <tr>
            <td style="width: 27px" rowspan="1" colspan="1"  >
                <img runat="Server"  src="../images/help.png" />&nbsp;</td>
            <td  style="font-family: Arial;font-size:10px;width:100%;" rowspan="1" colspan="1"  >
                <asp:Label ID="lblDetail" runat="server"  ></asp:Label></td>
            
        </tr>
        
       
    </table>
</asp:Panel>--%>
<asp:Panel runat="server" ID="pnlhelp" style="border:solid 1px blue" Width="100%">
<table width="100%">
        <tr>
            <td class="style1">
                <img id="Img1" runat="Server"  src="../images/help.png" alt=""/></td>
            <td>
                <asp:Label ID="lblDetail" runat="server" style="font-size:10px; font-family:Arial;" Width="100%" ></asp:Label></td>
            
        </tr>
        
       
    </table>
</asp:Panel>
