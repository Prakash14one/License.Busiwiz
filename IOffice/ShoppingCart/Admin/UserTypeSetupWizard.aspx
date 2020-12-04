<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="UserTypeSetupWizard.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <asp:Panel ID="panelusertype" runat="server" Width="100%">
                    <fieldset>
                        <table width="100%" cellspacing="0" cellpadding="2">
                            <tr>
                                <td style="width: 85%" colspan="2">
                                    <asp:Label ID="Label17" runat="server" Font-Bold="true" Font-Size="X-Large" Text="User Type"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="Label18" runat="server" Font-Bold="true" Text="Would you like to go through the Usertype Setup Wizard?"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 5%">
                                    <label>
                                        <asp:LinkButton ID="LinkButton8" runat="server" ForeColor="Black" Visible="false">View All</asp:LinkButton>
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel22" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel23" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image9" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel24" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image52" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel44" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton39" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label85" runat="server" Font-Bold="true" Text="Part A"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label86" runat="server" Font-Bold="true" Text="Would you like to add contacts (customers, vendors, candidates, others, ect.)?"></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                </td>
                                                <%--<td style="width: 5%">
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        <label>
                                                            Would you like to add users without login details ?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Yes" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                        <br />
                                                        <label>
                                                            Would you like to add users with login details ?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" Text="Yes" AutoPostBack="true" OnCheckedChanged="CheckBox2_CheckedChanged" />
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                                <%-- <td>
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <%--<td>
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
