<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcontrolHelpPanelForShoppingcart.ascx.cs" 
Inherits="ShoppingCart_Admin_UserControl_UcontrolHelpPanel" %>
  <%--   <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />--%>
<link href="../../../css/MasterMain.css" rel="stylesheet" type="text/css" />

  <%--  <table width="100%"  runat="server" >
        <tr>
            <td style="width: 27px" rowspan="1" colspan="1"  >
                <img runat="Server"  src="../images/help.png" />&nbsp;</td>
            <td  style="font-family: Arial;font-size:10px;width:100%;" rowspan="1" colspan="1"  >
                <asp:Label ID="lblDetail" runat="server"  ></asp:Label></td>
            
        </tr>
        
       
    </table>
</asp:Panel>--%>
<asp:UpdatePanel ID="upp" runat="server" >
<ContentTemplate>
<asp:Panel runat="server" ID="PNLTITLE">
<table width="100%" id="subinnertbl">
        <tr>
            
            <td   colspan="1" bgcolor="#CCCCCC" style="width:60%; font-family: Calibri; font-weight: bold; font-size: 14px; color: #000000;">
               
                 <asp:Label ID="lbltitle" runat="server"   >
                 </asp:Label>
                </td>
                <td align="left" bgcolor="#CCCCCC" style="width:40%;font-family: Calibri; font-weight: bold; font-size: 14px; color: #000000;">
                            <asp:DropDownList ID="ddlwarehouse" runat="server"  onselectedindexchanged="ddlbus_SelectedIndexChanged" AutoPostBack="true" Font-Size="10px" Height="18"></asp:DropDownList>
                
                             <asp:Label ID="lblacdate" runat="server" Text="A/c Yr-"></asp:Label>   
                               <asp:Label ID="lblopenaccy"   runat="server" >
                               
                                </asp:Label>
                         
                                &nbsp; <asp:HyperLink id="lblop" runat="server" Target="_blank" Text="Change">
                                </asp:HyperLink>
                               <%-- <asp:Button ID="btn" runat="server" Text="Change" onclick="ImageButton5_Click" />--%>
                </td>
                
                
             
               
        </tr>
        <tr>
            
            <td  align="left" colspan="2">
              </td>
           
        </tr>
        
       
    </table>
    </asp:Panel>
<asp:Panel runat="server" ID="pnlhelp" style="border:solid 1px black" Width="100%" BackColor="#CCCCCC">
<table width="100%">
        <tr>
            <td style="width:27px;">
                <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/ShoppingCart/images/HelpIcon.png" />--%>
                <asp:Image ID="Image2" runat="server" ImageUrl="~/ShoppingCart/images/HelpIconHHH.png" />
                </td>
            <td align="left">
                <asp:Label ID="lblDetail" runat="server"  Font-Bold="True" Font-Names="Tahoma" Font-Size="7pt" ></asp:Label></td>
            
        </tr>
        
   
    </table>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="ddlwarehouse" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>


