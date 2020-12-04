<%@ Control Language="C#" AutoEventWireup="true" 
CodeFile="~/ioffice/Shoppingcart/Admin/UserControl/UControlWizardMaster1.ascx.cs" 
Inherits="ShoppingCart_Admin_UserControl_UControlWizardMaster1" %>
     <link href="../../../css/main.css" rel="stylesheet" type="text/css" />
 
    <asp:Panel ID="Panel1" runat="server" Width="750px">
        <table width="100%" >
        <tr> <td  > 
            <strong><span style="color: #330033; font-family: Tahoma">Setup Wizard</span></strong></td>  </tr>
            
            <tr>
            <td>
                <asp:ImageButton   ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/Master1.png" OnClick="ImgBtnStep01_1_Click" />
                                           
            </td>
            <td>
                <asp:ImageButton     ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/shipping2.png" OnClick="ImgBtnStep01_2_Click" />
                                           
            </td>
            <td>
                <asp:ImageButton      ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/Account1.png" OnClick="ImgBtnStep01_3_Click" />
                                           
            </td>
            <td>
                <asp:ImageButton      ID="ImageButton4" runat="server" ImageUrl="~/ShoppingCart/images/Inventory1.png" OnClick="ImgBtnStep01_4_Click" />
                                           
            </td>
            <td>
                <asp:ImageButton      ID="ImageButton5" runat="server" ImageUrl="~/Shoppingcart/images/salespur1.png" OnClick="ImgBtnStep01_5_Click" />
                                           
            </td>
           <td>
                <asp:ImageButton    ID="ImageButton6" runat="server" ImageUrl="~/ShoppingCart/images/Services.png" OnClick="ImgBtnStep01_6_Click" />
                                           
            </td>
            
            
            </tr>
            
            
           
      
        </table>
    </asp:Panel>
 