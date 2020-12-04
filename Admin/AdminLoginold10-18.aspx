<%@ Page Language="C#" MasterPageFile="~/Admin/BeforeAdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminLoginold10-18.aspx.cs" Inherits="AdminLogin" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table id="pagelogintbl" cellpadding="0" cellspacing="0">
        <tr>
            <td>
            <asp:Panel Width="100%" runat="server" ID="pnl1" DefaultButton="btnLogin">
 <table cellpadding="0" cellspacing="0" id="logintbl">
            <tr>
                <td class="loginhdr" colspan="3">
                    Login</td>
            </tr>
     <tr>
         <td class="col1">
         </td>
         <td class="col2" colspan="2">
             &nbsp;</td>
     </tr>
            <tr>
                <td class="col1">
                    User :</td>
                <td class="col2" colspan="2">
                    <asp:TextBox ID="txtUser" runat="server" Width="141px" TabIndex="1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="col1">
                    Password :</td>
                <td class="col2" colspan="2">
                    <asp:TextBox ID="txtPassword" runat="server" Width="141px" TabIndex="2" 
                        TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="col1">
                </td>
                <td class="col2" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="col1">
                </td>
                <td class="col2" colspan="2">
                    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" 
                        TabIndex="3" Width="75px" /></td>
            </tr>
            <tr>
                <td class="col1">
                </td>
                <td class="col2" colspan="2">
                    <asp:Label ID="lblmsg" runat="server"></asp:Label></td>
            </tr>
        </table>
  </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

