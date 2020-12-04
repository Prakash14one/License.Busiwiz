<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table id="pagelogintbl" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Panel Width="100%" runat="server" ID="pnl1" DefaultButton="btnLogin" Style="position: relative">
                    <table cellpadding="0" cellspacing="0" id="logintbl" style="border: 10px solid #BCBCBC;
                        margin: 150px 50px 150px 350px; width: 358px; background-color: #EFEFEF; height: 218px;">
                        <tr>
                            <td class="loginhdr" colspan="2" style="height: 26px">
                                Login
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 12px; font-size: 12px;">
                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                            </td>
                           
                        </tr>
                        <tr>
                            <td style="width: 140px; height: 20px; font-size: 12px; color: #000000; font-weight: bold;"
                                align="right">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Client Id :
                            </td>
                            <td style="width: 160px; height: 20px;">
                                <asp:TextBox ID="txtClientId" runat="server" TabIndex="1" Width="134px"></asp:TextBox>
                              
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 140px; height: 20px; font-size: 12px; color: #000000; font-weight: bold;"
                                align="right">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Username :
                            </td>
                            <td style="width: 160px; height: 20px;">
                                <asp:TextBox ID="txtUser" runat="server" TabIndex="2" Width="134px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUser"
                                    Display="Dynamic" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 140px; height: 20px; font-size: 12px; color: #000000; font-weight: bold;"
                                align="right">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Password :
                            </td>
                            <td style="width: 160px; height: 20px;">
                                <asp:TextBox ID="txtPassword" runat="server" TabIndex="3" TextMode="Password" Width="134px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1" style="width: 140px">
                            </td>
                            <td class="col2" style="width: 160px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" TabIndex="4" ValidationGroup="1"
                                    Text="Login" Height="19px" Width="55px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 12px; font-size: 12px;" colspan="2" align="center">
                                <u><a href="passwordrecovery.aspx">Forgot UserName/Password? </a></u>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 12px; font-size: 12px;" colspan="2" align="center">
                                <u><a href="ClientInfo.aspx">Sign up as New IT Company </a></u>
                            </td>
                        </tr>
                        <tr>
                            <td class="col2" colspan="2" style="height: 20px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
