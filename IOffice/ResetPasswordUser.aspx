<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Login.master" AutoEventWireup="true"
    CodeFile="ResetPasswordUser.aspx.cs" Inherits="ShoppingCart_Admin_ForgotPassword" %>

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
                <div id="login">
                    <form runat="server" id="fmLogin" autocomplete="on">
                    <h1>
                        Reset Login Information
                    </h1>
                    <asp:Panel ID="Panel2" runat="server">
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="28%">
                                </td>
                                <td>
                                    <label>
                                        Login Information
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <asp:Label ID="Label1" runat="server" Text="Company ID " style="font-size: 16px;"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtcompanyid" MaxLength="40" Width="220px" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcompanyid"
                                        ErrorMessage="*" ValidationGroup="1" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                        ControlToValidate="txtcompanyid" ValidationGroup="1"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <asp:Label ID="Label2" runat="server" Text="User ID  " style="font-size: 16px;"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtuname" MaxLength="30" Width="220px" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtuname"
                                        ErrorMessage="*" ValidationGroup="1" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([@._a-zA-Z0-9\s]*)"
                                        ControlToValidate="txtuname" ValidationGroup="1"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <asp:Label ID="Label3" runat="server" Text="Password" style="font-size: 16px;"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpass" MaxLength="30" Width="220px" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpass"
                                        ErrorMessage="*" ValidationGroup="1" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                </td>
                                <td align="left">
                                    <p class="login button" style="text-align: left">
                                        <asp:Button ID="Button1" runat="server" ValidationGroup="1" style="width: 100px;" Text="Confirm" OnClick="Button1_Click" />
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                </td>
                                <td align="left">
                                    <asp:Image ID="img1" runat="server" Height="13" Width="13" ImageUrl="~/Account/images/true.gif"
                                        Visible="false" ImageAlign="Middle" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel Width="100%" Visible="false" ID="pnlsecurityquestion" runat="server">
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label4" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    Please change your login information
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" Text=" Change UserID"
                                        OnCheckedChanged="CheckBox1_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel runat="server" ID="pnlchangeuserid" Width="100%" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td width="40%">
                                                    New user ID
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtnewuserid" Width="220px" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    Confirm user ID
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtconfirmuserid" Width="220px" runat="server"></asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="txtnewuserid"
                                                        ControlToValidate="txtconfirmuserid" ErrorMessage="*">
                                                    </asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="true" Text=" Change Password"
                                        OnCheckedChanged="CheckBox2_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel runat="server" ID="pnlchangepassword" Width="100%" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td width="40%">
                                                    New password
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtnewpassword" Width="220px" runat="server" TextMode="Password"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    Confirm password
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtconfirmpassword" Width="220px" runat="server" TextMode="Password"></asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtnewpassword"
                                                        ControlToValidate="txtconfirmpassword" ErrorMessage="*"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <asp:Panel runat="server" ID="panelforcandidate">
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="CheckBox3" runat="server" AutoPostBack="true" Text=" Change Employee Code"
                                            OnCheckedChanged="CheckBox3_CheckedChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel runat="server" ID="pnlchangeempcode" Width="100%" Visible="false">
                                            <table width="100%">
                                                <tr>
                                                    <td width="40%">
                                                        New Employee Code
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtempcode" Width="220px" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        Confirm Employee Code
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtconfirmempcode" Width="220px" runat="server"></asp:TextBox>
                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtempcode"
                                                            ControlToValidate="txtconfirmempcode" ErrorMessage="*"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <label>
                                        Set your Security Questions
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    Question 1&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList Width="226px" ID="ddlquestion1" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlquestion1"
                                        ValidationGroup="10" InitialValue="0" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    Answer 1&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtanswer1" Width="220px" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtanswer1"
                                        ValidationGroup="10" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    Question 2
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlquestion2" Width="226px" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlquestion2"
                                        ValidationGroup="10" InitialValue="0" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    Answer 2
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtanswer2" Width="220px" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtanswer2"
                                        ValidationGroup="10" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    Question 3&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlquestion3" Width="226px" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlquestion3"
                                        InitialValue="0" ValidationGroup="10" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    Answer 3
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtanswer3" Width="220px" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtanswer3"
                                        ValidationGroup="10" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td>
                                    <p class="login button" style="text-align: left">
                                        <asp:Button ID="btnsubmit" runat="server" style="width: 100px;" Text="Submit" OnClick="btnsubmit_Click"
                                            ValidationGroup="10" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server"  ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>
</asp:Content>
