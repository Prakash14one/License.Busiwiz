<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="InventoryMediaMaster.aspx.cs" Inherits="ShoppingCart_Admin_InventoryMediaMaster"
    Title="Untitled Page" %>

<%@ Register Src="~/ShoppingCart/Admin/UserControl/UControlWizardpanel.ascx" TagName="pnl"
    TagPrefix="pnl" %>
<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="uppae" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        <label>
                            <asp:Label ID="Label2" runat="server" Text="Business Name "></asp:Label>
                            <asp:RequiredFieldValidator ID="CustomValid3" runat="server" ControlToValidate="ddlwarehouse"
                                ErrorMessage="*" SetFocusOnError="true" InitialValue="0" ValidationGroup="1">
                            </asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td style="width: 25%">
                        <label>
                            <asp:DropDownList ID="ddlwarehouse" runat="server" ValidationGroup="1" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </label>
                    </td>
                    <td style="width: 25%">
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Select Media Type "></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldVdator3" runat="server" ControlToValidate="DropDownList2"
                                ErrorMessage="*" InitialValue="0" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td style="width: 25%">
                        <label>
                            <asp:DropDownList ID="DropDownList2" runat="server">
                            </asp:DropDownList>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        <label>
                            <asp:Label ID="Label4" runat="server" Text="Select Product "></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidar2" runat="server" ControlToValidate="DropDownList1"
                                ErrorMessage="*" SetFocusOnError="true" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td style="width: 75%" colspan="3">
                        <asp:DropDownList ID="DropDownList1" runat="server" ValidationGroup="1" Width="100%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        <label>
                            <asp:Label ID="Label5" runat="server" Text="File Title "></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="TextBox1"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                        </label>
                    </td>
                    <td style="width: 25%">
                        <label>
                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="20">
                            </asp:TextBox>
                            <asp:Label ID="lblsadjk" runat="server" Text="(Max 20 Chars,A-Z,0-9,_)"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 25%">
                        <label>
                            <asp:Label ID="Label6" runat="server" Text="Upload Media File "></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FileUpload1"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td style="width: 25%">
                        <label>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        <label>
                            <asp:Label ID="Label7" runat="server" Text="Description"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([.a-zA-Z0-9\s]*)"
                                ControlToValidate="txttaskinstruction" ValidationGroup="1">
                            </asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,300})$"
                                ControlToValidate="txttaskinstruction" ValidationGroup="2"></asp:RegularExpressionValidator>
                        </label>
                    </td>
                    <td style="width: 75%" colspan="3">
                        <label>
                            <asp:TextBox ID="txttaskinstruction" runat="server" Height="128px" MaxLength="300"
                                Width="100%" AutoPostBack="True" TextMode="MultiLine"></asp:TextBox>
                            <asp:Label ID="jdsfk" runat="server" Text="(Max. 300 Chars,A-Z,0-9,.)"></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">
                    </td>
                    <td style="width: 25%">
                        <label>
                            <asp:Button ID="ImageButton1" runat="server" OnClick="Button1_Click" ValidationGroup="1"
                                Text="Submit" />
                            <asp:Button ID="ImageButton3" runat="server" Text="Cancel" OnClick="ImageButton3_Click" />
                        </label>
                    </td>
                    <td style="width: 25%">
                    </td>
                    <td style="width: 25%">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ImageButton1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
