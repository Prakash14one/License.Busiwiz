<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailDocApprove.aspx.cs" Inherits="EmailDocApprove" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
  <script language="javascript" type="text/javascript">


        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }  


    </script>
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <table width="100%" cellpadding="2" cellspacing="3">
     
       <tr>
       <td colspan="2" align="center">
       <table width="80%">
       <tr>
       <td>
       
       </td>
       <td>
       </td>
       </tr>
          <tr>
       <td>
       
       </td>
       <td align="right">
                                
                                 <input type="button" value="Print" id="Button3" runat="server" onclick="javascript:CallPrint('divPrint')"
                                style="background-color: #CCCCCC; width: 51px;" /></td>
       </tr>
       </table>
       </td>
       </tr>
       
       <tr>
       <td colspan="2" align="center">
        <asp:Panel ID="pnlgrid" runat="server" Width="100%" >
       <table width="80%">
       <tr>
       <td colspan="2">
       <table width="100%">
        <tr>
       <td style="width:30%;" >
       <asp:Image ID="img1" Width="176px" Height="106px" runat="server" />
       </td>
        <td style="width:40%;">
        </td>
       <td align="left"><span style="color:#996600"><b><asp:Label ID="Wname" runat="server"></asp:Label> </b> </span>
       <br />
       <b><asp:Label ID="lblciti" runat="server"></asp:Label> </b><br />
              <b><asp:Label ID="lblstate" runat="server"></asp:Label> </b><br />
                     <b><asp:Label ID="lblcountry" runat="server"></asp:Label> </b><br />
       </td>
       </tr>
       </table>
       </td>
       </tr>
        
       <tr>
       <td colspan="2">
       
     <b>  <asp:Label ID="lblconfo" runat="server" Text="Document Approval Confirmation"></asp:Label></b>
       </td>
       </tr>
          <tr>
       <td colspan="2">
         <asp:Label ID="lblmsg" runat="server" Text="Your Document approval has been successfully recorded."></asp:Label>
       </td >
     
       </tr>
        <tr>
       <td colspan="2">
        <br />
       </td >
     
       </tr>
        <tr>
       <td align="right" >
       <asp:Label ID="lbldidtext" runat="server" Text="Doc Id :"></asp:Label>
       </td >
     <td align="left">  <asp:Label ID="lblDocId" runat="server" ></asp:Label>
     </td>       
     </tr>
     <tr>
       <td align="right" >
       <asp:Label ID="lbldoctitletext" runat="server" Text="Doc Title :"></asp:Label>
       </td >
     <td align="left">  <asp:Label ID="lbldoctitle" runat="server" ></asp:Label>
     </td>       
     </tr>
     <tr>
       <td align="right" >
       <asp:Label ID="lblEmpnametext" runat="server" Text="Employee Name :"></asp:Label>
       </td >
     <td align="left">  <asp:Label ID="lblemployeename" runat="server" ></asp:Label>
     </td>       
     </tr>
     <tr>
       <td align="right" >
       <asp:Label ID="lblappdatetext" runat="server" Text="Approval DateTime :"></asp:Label>
       </td >
     <td align="left">  <asp:Label ID="lblapprovaldatetime" runat="server" ></asp:Label>
     </td>       
     </tr>
     <tr>
       <td colspan="2">
       <br /></td>
     </tr>
       <tr>
       <td colspan="2">
       <table width="100%">
        <tr>
       <td style="width:30%;" >
      <b> <asp:Label ID="lblth" runat="server" Text="Thanking You"></asp:Label></b>
       </td>
        <td style="width:40%;">
        </td>
       <td align="left"><span style="color:#996600">
     
      
       </td>
       </tr>
       </table>
       </td>
       </tr>
    <tr>
       <td colspan="2">
       <table width="100%">
        <tr>
       <td style="width:30%;" >
       <b> <asp:Label ID="lbls" runat="server" Text="Sincerely"></asp:Label></b>
       </td>
        <td style="width:40%;">
        </td>
       <td align="left"><span style="color:#996600">
     
      
       </td>
       </tr>
       </table>
       </td>
       </tr>
     
      <tr>
       <td colspan="2" align="left">
       
     <br />
     </td>
     </tr>
     <tr>
       <td colspan="2">
       <table width="100%">
        <tr>
       <td style="width:30%;" >
        <b> <asp:Label ID="Label2" runat="server" Text="For,"></asp:Label></b>
       </td>
        <td style="width:40%;">
        </td>
       <td align="left">
    
      
       </td>
       </tr>
       </table>
       </td>
       </tr>
      <tr>
       <td colspan="2">
       <table width="100%">
        <tr>
       <td style="width:30%;" >
        <b> <asp:Label ID="lblcname" runat="server" ></asp:Label></b>
       </td>
        <td style="width:40%;">
        </td>
       <td align="left">
    
      
       </td>
       </tr>
       </table>
       </td>
       </tr>
     
       </table>
       </asp:Panel>
       </td>
       </tr>
       </table>
    </div>
    </form>
</body>
</html>
