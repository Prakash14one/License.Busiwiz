<%@ Page Title="" Language="C#" MasterPageFile="~/MainHome.master" AutoEventWireup="true"
    CodeFile="ResetLoginInformation.aspx.cs" Inherits="ShoppingCart_Admin_ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="right_content" class="columns_contents_Inner">
        <a class="hiddenanchor" id="toregister"></a><a class="hiddenanchor" id="tologin">
        </a>
        <div id="stylized" class="myform" align="center">
            <fieldset style="padding: 0 0% 0 25%">
                <table width="100%" align="center">
                    <tr>
                        <td colspan="2" align="left">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Panel2" runat="server" Width="100%">
                    <table width="100%" align="center">
                        <tr>
                            <td colspan="2">
                                <label>
                                    <asp:Label ID="Label12" runat="server" Text="Reset Login Information" Width="300px"
                                        Font-Size="18px"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Step1 : Please write your User ID or Email ID "
                                        Width="450px"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="18%">
                                <asp:Label ID="Label2" runat="server" Text="User ID  "></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtuserid" MaxLength="20" runat="server" Width="200px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([._a-zA-Z0-9\s]*)"
                                    ControlToValidate="txtuserid" ValidationGroup="1"></asp:RegularExpressionValidator>
                                <asp:Image ID="img1" runat="server" Height="13" Width="13" ImageUrl="~/Account/images/true.gif"
                                    Visible="false" ImageAlign="Middle" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                <asp:Image ID="img2" runat="server" Height="13" Width="13" ImageUrl="~/Account/images/true.gif"
                                    Visible="false" ImageAlign="Middle" />
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
                            <td>
                            </td>
                            <td colspan="2" align="left">
                                <asp:Button ID="Button1" runat="server" ValidationGroup="1" Text="Confirm" OnClick="Button1_Click"
                                    Width="205px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel Width="100%" Visible="false" ID="Panel1" runat="server">
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="left">
                                <label>
                                    <asp:Label ID="Label11" runat="server" Text="Step 2 : Security Question" Width="450px"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblchkque" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 18%">
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
                                <asp:TextBox ID="txtans1" MaxLength="30" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtans1" ValidationGroup="2"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
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
                                <asp:TextBox ID="txtans2" MaxLength="30" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtans2" ValidationGroup="2"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
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
                                <asp:TextBox ID="txtans3" MaxLength="30" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtans3" ValidationGroup="2"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table width="100%">
                    <tr>
                        <td align="right" style="width: 18%">
                        </td>
                        <td>
                            <asp:Button ID="ImageButton6" runat="server" OnClick="ImageButton1_Click" ValidationGroup="2"
                                Text="Send" Visible="False" Width="205px" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
</asp:Content>
