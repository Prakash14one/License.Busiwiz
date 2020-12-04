<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Login.master" AutoEventWireup="true"
    CodeFile="ResetLoginInformation.aspx.cs" Inherits="ShoppingCart_Admin_ForgotPassword" %>

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
                        Reset Login Information</h1>
                    <p>
                        <label for="username" class="uname">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                        </label>
                    </p>
                    <asp:Panel ID="Panel2" runat="server">
                        <table width="100%" cellspacing="3">
                            <tr>
                                <td colspan="2" align="left">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Step1 : Please write your User ID or Email ID "></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="28%">
                                    <asp:Label ID="Label2" runat="server" Text="User ID  "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtuserid" MaxLength="20" runat="server" Width="200px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([._a-zA-Z0-9\s]*)"
                                        ControlToValidate="txtuserid" ValidationGroup="1"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="Label3" runat="server" Text="OR"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label4" runat="server" Text="Email ID  "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtuname" runat="server" MaxLength="30" Width="200px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtuname"
                                        ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="Button1" runat="server" ValidationGroup="1" Text="Confirm" OnClick="Button1_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Image ID="img1" runat="server" Height="13" Width="13" ImageUrl="~/Account/images/true.gif"
                                        Visible="false" ImageAlign="Middle" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel Width="100%" Visible="false" ID="Panel1" runat="server">
                        <table width="100%" cellspacing="3">
                            <tr>
                                <td colspan="2" align="left">
                                    Step 2 : Security Question
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="lblchkque" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 28%">
                                    <asp:Label ID="Label5" runat="server" Text=" Question 1 : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblque1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label6" runat="server" Text="Answer 1 : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtans1" MaxLength="30" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtans1" ValidationGroup="2"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label7" runat="server" Text="Question 2 : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblque2" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label8" runat="server" Text="Answer 2 : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtans2" MaxLength="30" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtans2" ValidationGroup="2"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label9" runat="server" Text="Question 3 : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblque3" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label10" runat="server" Text="Answer 3 : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtans3" MaxLength="30" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtans3" ValidationGroup="2"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <p class="login button">
                        <%--<asp:Button ID="btnLogin" runat="server" Text="Login" ValidationGroup="vgAdminLogin" />--%>
                        <asp:Button ID="ImageButton6" runat="server" OnClick="ImageButton1_Click" ValidationGroup="2"
                            Text="Send" Visible="False" />
                        <%--<asp:Button ID="btnsignin" runat="server" Text="Sign In" 
                                                            onclick="btnsignin_Click" ValidationGroup="1" TabIndex="5" 
                                                            />--%>
                    </p>
                    <p>
                        <%--<asp:HyperLink runat="server" Text="Forget Password" ID="hlForgetPassword"></asp:HyperLink> --%>
                        <%--<asp:HyperLink ID="hplforgotpass" runat="server" 
                                                            NavigateUrl="~/ShoppingCart/Admin/ForgotPassword.aspx" TabIndex="6">
                                                            Forgot password? </asp:HyperLink>--%>
                        <%--<asp:HyperLink runat="server" Text="New Sign Up" ID="HyperLink1"></asp:HyperLink>--%>
                        <br />
                        <%--<asp:LinkButton ID="Lnkbtn1" runat="Server" 
                                                           
                                                            Text="New Sign Up"  
                                                        TabIndex="7" onclick="Lnkbtn1_Click">
                                                        </asp:LinkButton>--%>
                    </p>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
