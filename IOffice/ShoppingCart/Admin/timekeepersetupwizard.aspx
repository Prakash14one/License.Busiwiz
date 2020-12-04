<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="TimeKeeperSetupWizard.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <asp:Panel ID="paneltimekeeper" runat="server" Width="100%">
                    <fieldset>
                        <table width="100%" cellspacing="0" cellpadding="2">
                            <tr>
                                <td style="width: 85%" colspan="2">
                                    <asp:Label ID="Label69" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Time Keeper"></asp:Label>
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
                                    <asp:Label ID="Label70" runat="server" Font-Bold="true" Text="Would you like to go through the Time Keeper Setup Wizard?"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 5%">
                                    <label>
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel123" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel124" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image40" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel125" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image41" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel126" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton37" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label71" runat="server" Font-Bold="true" Text="Part A"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label72" runat="server" Font-Bold="true" Text="Time Keeping and Payrolls Rules"></asp:Label>
                                                </td>
                                                <%--<td style="width: 15%">
                                                </td>--%>
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
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="There are default rules set for employee time keeping and payroll calculation."></asp:Label>
                                                        <br />
                                                        <label>
                                                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Would you like to view or change them?"></asp:Label>
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Yes" 
                                                        OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="True" />
                                                    </label>
                                                    <%-- <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Black" 
                                                        Font-Bold="true" onclick="LinkButton2_Click">  Yes</asp:LinkButton>--%>
                                                </td>
                                                <%--   <td>
                                                </td>--%>
                                                <%--<td>
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel127" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel128" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image42" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel129" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image43" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel130" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton38" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label73" runat="server" Font-Bold="true" Text="Part B"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label74" runat="server" Font-Bold="true" Text="Door and Heat Control Setup"></asp:Label>
                                                </td>
                                                <%-- <td style="width: 15%">
                                                </td>--%>
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
                                                        Do you have an IP based door entrance control or heat and light control which you <br />
                                                        would like to set up to interface with iTimekeeper?
                                                    </label>
                                                    <label>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" Text="Yes" 
                                                        OnCheckedChanged="CheckBox2_CheckedChanged" AutoPostBack="True" />
                                                    </label>
                                                    <%--<asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton1_Click">  Yes</asp:LinkButton>--%>
                                                </td>
                                                <%-- <td>
                                                </td>--%>
                                                <%--<td>
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
