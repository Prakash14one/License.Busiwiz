<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="SaleManagementSetupWizard.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <asp:Panel ID="panelsalesmanagement" runat="server" Width="100%">
                    <fieldset>
                        <table width="100%" cellspacing="0" cellpadding="2">
                            <tr>
                                <td style="width: 85%" colspan="2">
                                    <asp:Label ID="Label37" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Sales Management"></asp:Label>
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
                                    <asp:Label ID="Label38" runat="server" Font-Bold="true" Text="Would you like to go through the Sales Management Setup Wizard?"></asp:Label>
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
                                    <asp:Panel ID="Panel67" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel68" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image19" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel69" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image20" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel70" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton24" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label39" runat="server" Font-Bold="true" Text="Part A"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label40" runat="server" Font-Bold="true" Text="Tax Setup"></asp:Label>
                                                </td>
                                                <%-- <td style="width: 15%">
                                                </td>
                                               <td style="width: 5%">
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
                                                            Do you want to set up taxes on sales for your business?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Yes" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox1_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton1_Click" Visible="false"> Yes</asp:LinkButton>
                                                        <br />
                                                        <label>
                                                            It is recommended for any business charging sales tax on their product/ 
                                                    service to set up taxation options right now.
                                                        </label>
                                                    </label>
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
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel71" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel72" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image21" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel73" Visible="false" runat="server" Width="100%">
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel74" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton25" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label41" runat="server" Font-Bold="true" Text="Part B"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label42" runat="server" Font-Bold="true" Text="Online Sales Setup"></asp:Label>
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
                                                            Do you want to setup default payment options for online and retail sales 
                                                    of your business?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" Text="Yes" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox2_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton2_Click" Visible="false"> Yes</asp:LinkButton>
                                                    </label>
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
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel75" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel76" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image22" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel77" Visible="false" runat="server" Width="100%">
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel78" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton26" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label43" runat="server" Font-Bold="true" Text="Part C"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label44" runat="server" Font-Bold="true" Text="Discount Setup"></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                </td>
                                                <%-- <td style="width: 5%">
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
                                                            Do you want to set up a discount for customers based on the volume of 
                                                    items ordered?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox3" runat="server" Text="Yes" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox3_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton3_Click" Visible="false"> Yes</asp:LinkButton>
                                                            <br />
                                                        <label>
                                                            Do you want to set up discount based on customer category?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox4" runat="server" Text="Yes" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox4_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton4_Click" Visible="false"> Yes</asp:LinkButton>   
                                                             <br />                                                   
                                                        <label>
                                                            Do you want to setup a discount on orders based on the order value?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox5" runat="server" Text="Yes" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox5_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton5" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton5_Click" Visible="false"> Yes</asp:LinkButton>
                                                    </label>
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
                    </fieldset></asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
