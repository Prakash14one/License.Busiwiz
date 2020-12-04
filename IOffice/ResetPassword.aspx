<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Login.master" AutoEventWireup="true"
    CodeFile="ResetPassword.aspx.cs" Inherits="ShoppingCart_Admin_ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <!-- Codrops top bar -->
        <div class="codrops-top">
        </div>
        <!--/ Codrops top bar -->
        <div id="container_demo">
            <a class="hiddenanchor" id="toregister"></a><a class="hiddenanchor" id="tologin">
            </a>
            <div id="wrapper">
                <div id="login" class="animate form">
                    <form runat="server" id="fmLogin" autocomplete="on">
                    <%--<asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="false"></asp:Label>--%>
                    <h1>
                        Reset Password</h1>
                    <p>
                        <label for="username" class="uname">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                        </label>
                    </p>
                    <asp:Panel Width="100%" ID="Panel1" runat="server">
                        <table width="100%" cellspacing="3">
                            <tr>
                                <td align="right" style="width: 41%">
                                    <asp:Label ID="Label1" runat="server" Text="Question 1 : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblque1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label2" runat="server" Text="Answer 1 : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtans1" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtans1" ValidationGroup="2"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label3" runat="server" Text="Question 2 : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblque2" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label4" runat="server" Text="Answer 2 : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtans2" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtans2" ValidationGroup="2"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label5" runat="server" Text="Question 3 : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblque3" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label6" runat="server" Text="Answer 3 : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtans3" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtans3" ValidationGroup="2"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <p class="login button" style="margin-right: 190px">
                                        <asp:Button ID="Button1" runat="server" ValidationGroup="1" Text="Confirm" OnClick="Button1_Click" />
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Image ID="img1" runat="server" Height="13" Width="13" ImageUrl="~/Account/images/true.gif"
                                        Visible="false" ImageAlign="Middle" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table width="100%" cellspacing="3">
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel2" Width="100%" runat="server" Visible="False">
                                    <table width="100%" cellspacing="3">
                                        <tr>
                                            <td align="right" style="width: 41%">
                                                <asp:Label ID="Label7" runat="server" Text="New Password : "></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtpass" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtpass"
                                                    ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label8" runat="server" Text="Confirm Password : "></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtresetpass" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtresetpass"
                                                    ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                             <p class="login button" style="margin-right: 190px">
                                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" ValidationGroup="1" OnClick="btnsubmit_Click" />
                                               
                                                    </p>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td colspan="2">
                                         <asp:Button ID="btncancel" runat="server" Text="Cancel" Visible="false"
                                                    OnClick="btncancel_Click" />
                                        </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
