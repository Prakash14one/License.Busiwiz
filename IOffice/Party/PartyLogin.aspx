<%@ Page Language="C#" MasterPageFile="~/Master/Login.master" AutoEventWireup="true"
    CodeFile="PartyLogin.aspx.cs" Inherits="PartyLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="codrops-top">
        </div>
        <div id="container_demo">
            <a class="hiddenanchor" id="toregister"></a><a class="hiddenanchor" id="tologin">
            </a>
            <div id="wrapper">
                <div id="login" class="animate form">
                    <form runat="server" id="fmLogin" autocomplete="on">
                    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                    <h1>
                      Party Log In</h1>
                    <p>
                        <label for="username" class="uname">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                         
                        </label>
                    </p>
                    <p>
                        <label for="username" class="uname">
                            Company ID
                        </label>
                        <asp:TextBox ID="txtcompanyid" runat="server" CssClass="cssTextbox" TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcompanyid"
                            ErrorMessage="*" ValidationGroup="1" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <label for="username" class="uname">
                            User Name
                        </label>
                        <asp:TextBox ID="txtuname" runat="server" CssClass="cssTextbox" TabIndex="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtuname"
                            ErrorMessage="*" ValidationGroup="1" Display="Dynamic" EnableClientScript="False">
                        </asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <label for="password" class="youpasswd">
                            Password
                        </label>
                        <asp:TextBox ID="txtpass" runat="server" CssClass="cssTextbox" TextMode="Password"
                            TabIndex="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpass"
                            ErrorMessage="*" ValidationGroup="1" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <label for="password" class="youpasswd">
                            <asp:CheckBox ID="chkremember" runat="server" Text=" Remember me" TabIndex="4" />
                        </label>
                    </p>
                    <p class="login button">
                        <%--<asp:Button ID="btnLogin" runat="server" Text="Login" ValidationGroup="vgAdminLogin" />--%>
                        <asp:Button ID="btnsignin" runat="server" Text="Sign In" OnClick="btnsignin_Click"
                            ValidationGroup="1" TabIndex="5" />
                    </p>
                    <p>
                        <%--<asp:HyperLink runat="server" Text="Forget Password" ID="hlForgetPassword"></asp:HyperLink> --%>
                        <asp:HyperLink ID="hplforgotpass" runat="server" NavigateUrl="~/ShoppingCart/Admin/ForgotPassword.aspx"
                            TabIndex="6">
                                                            Forgot password? </asp:HyperLink>
                       
                    </p>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
