<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/IOffice/Shoppingcart/Admin/UserControl/UControlWizardMaster2ship.ascx.cs" Inherits="ShoppingCart_Admin_UserControl_UControlWizardMaster2ship" %>
     <link href="../../../css/main.css" rel="stylesheet" type="text/css" />
 
    <asp:Panel ID="Panel1" runat="server" Width="745px">
        <table >
        <tr> <td colspan="13"> 
            
                <asp:ImageButton  ID="ImgBtnStep1_1" runat="server" ImageUrl="~/ShoppingCart/images/Companyname.png" OnClick="ImgBtnStep1_1_Click" />
                                           
            </td>
            <td>
                <asp:ImageButton   ID="ImgBtnStep1_2" runat="server" ImageUrl="~/ShoppingCart/images/Selectmethod.png" OnClick="ImgBtnStep1_2_Click" />
                                           
            </td>
            <td>
                <asp:ImageButton   ID="ImgBtnStep1_3" runat="server" ImageUrl="~/ShoppingCart/images/Freerule.png" OnClick="ImgBtnStep1_3_Click" />
                                           
            </td>
            <td>
                <asp:ImageButton   ID="ImgBtnStep1_4" runat="server" ImageUrl="~/ShoppingCart/images/Handlngchrg.png" OnClick="ImgBtnStep1_4_Click" />
                                           
            </td>
            <%--<td>
                <asp:ImageButton   ID="ImgBtnStep1_5" runat="server" ImageUrl="~/ShoppingCart/images/2.5.png" OnClick="ImgBtnStep1_5_Click" />
                                           
            </td>
            <td>
                <asp:ImageButton   ID="ImgBtnStep1_6" runat="server" ImageUrl="~/ShoppingCart/images/2.6.png" OnClick="ImgBtnStep1_6_Click" />
                                           
            </td>--%>
            </tr>
        </table>
    </asp:Panel>
 