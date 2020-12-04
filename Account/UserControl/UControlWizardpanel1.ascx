<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UControlWizardpanel1.ascx.cs" Inherits="Account_UserControl_UControlWizardpanel1" %>
 <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />
 <%--<asp:ImageImageButton id="ImageImageButton1" runat="server"></asp:ImageImageButton> 
<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
<asp:Panel ID="Panel2" runat="server" Visible="false">

<asp:Label ID="Label1" runat="server" Text="Company Master Setup Wizard" Font-Bold="True" Font-Size="X-Large" Font-Names="Arial" ForeColor="#946702"></asp:Label></asp:Panel> <table cellpadding="0" cellspacing="0" id="wizPanel">
    <tr>
        <td colspan="1" style="height: 15px">
            &nbsp;</td>
    </tr>
            <tr>
                <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnStep1" runat="server" BackColor="LightGray"  Text="Step 1" OnClick="btnStep1_Click" ForeColor="Black" ImageUrl="~/Account/images/btnstep1.jpg" /></td>
               </tr>
               <%--<tr>
               <td class="fcsm">
                                                 <asp:Panel ID="pnllbl" runat="server" >
                                                    <asp:Label ID="lblHeandName" runat="server" Text=""></asp:Label>
                                                    </asp:Panel></td></tr>--%>
               <tr>
                <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="BtnStep2" runat="server" BackColor="LightGray"  Text="Step 2" OnClick="BtnStep2_Click" ForeColor="Black" ImageUrl="~/Account/images/BtnStep2.jpg" /></td>
                    </tr>
               <tr>
                <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="BtnStep3" runat="server" BackColor="LightGray"  Text="Step 3" OnClick="BtnStep3_Click" ForeColor="Black"  ImageUrl="~/Account/images/BtnStep3.jpg"  />
                </td>
                </tr>
               <tr>
                 <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnStep4" runat="server"  BackColor="LightGray"  Text="Step 4" OnClick="btnStep4_Click" ForeColor="Black" ImageUrl="~/Account/images/BtnStep4.jpg"  /></td>
                    </tr>
               <tr>
                <td style="height: 36px">
                    </td>
                    </tr>
               <tr>
                <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnStep5" runat="server" BackColor="LightGray"  Text="Step 5" OnClick="btnStep5_Click" ForeColor="Black" ImageUrl="~/Account/images/BtnStep5.jpg"  /></td>
                    </tr>
               <tr>
                <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnstep6" runat="server" BackColor="LightGray"  Text="Step 6" OnClick="btnstep6_Click" ForeColor="Black" ImageUrl="~/Account/images/BtnStep6.jpg"  />
                </td>
                </tr>
               <tr>
                 <td style="width: 3px; height: 36px">
                 
                    <asp:ImageButton ID="btnstep7" runat="server" BackColor="LightGray"  Text="Step 7" OnClick="btnstep7_Click" ForeColor="Black" ImageUrl="~/Account/images/BtnStep7.jpg"  /></td>
                    </tr>
               <tr>
                <td style="height: 36px">
                    </td>
                    </tr>
               <tr>
                <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnstep8" runat="server" BackColor="LightGray"  Text="Step 8" OnClick="btnstep8_Click" ForeColor="Black" ImageUrl="~/Account/images/BtnStep8.jpg"  /></td>
                    </tr>
               <tr>
                <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnstep9" runat="server" BackColor="LightGray"  Text="Step 9" OnClick="btnstep9_Click" ForeColor="Black" ImageUrl="~/Account/images/BtnStep9.jpg"  />
                </td>
                </tr>
               <tr>
                <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnstep10" runat="server" BackColor="LightGray"  Text="Step 10" OnClick="btnstep10_Click" ForeColor="Black" ImageUrl="~/Account/images/BtnStep10.jpg"  />
                </td>
                </tr>
               <tr>
                <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnstep11" runat="server" BackColor="LightGray"  Text="Step 11"   ForeColor="Black" OnClick="btnstep11_Click" ImageUrl="~/Account/images/BtnStep11.jpg"  />
                </td>
                </tr>
               <tr>
                 <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnstep12" runat="server" BackColor="LightGray"  Text="Step 12"     ForeColor="Black" OnClick="btnstep12_Click"  ImageUrl="~/Account/images/step122.jpg"  /> 
                </td>
                </tr>
               <tr>
                 <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnStep13" runat="server" BackColor="LightGray"  Text="Step 13"     ForeColor="Black" OnClick="btnstep13_Click"  ImageUrl="~/Account/images/BtnStep13.jpg"  /> 
                </td>
                </tr>
               <tr>
                 <%--<td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnStep13" runat="server" BackColor="LightGray"  Text="Step 13"  ImageUrl="~/Account/images/BtnStep13.jpg"   ForeColor="Black" OnClick="btnStep13_Click"     />
                </td>--%>
                 <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnStep14" runat="server" BackColor="LightGray"  Text="Step 14"   ForeColor="Black" ImageUrl="~/Account/images/BtnStep14.jpg"  OnClick="btnStep14_Click"    />
                </td>
           </tr>
               
        </table>
 