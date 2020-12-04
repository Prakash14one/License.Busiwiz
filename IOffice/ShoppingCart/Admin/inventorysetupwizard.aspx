<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="inventorysetupwizard.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default2"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <asp:Panel ID="pnelinventory" runat="server" Width="100%">
                    <fieldset>
                        <table width="100%" cellspacing="0" cellpadding="2">
                            <tr>
                                <td style="width: 85%" colspan="2">
                                    <asp:Label ID="Label45" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Inventory Management"></asp:Label>
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
                                    <asp:Label ID="Label46" runat="server" Font-Bold="true" Text="Would you like to go through the Inventory Management Setup Wizard?"></asp:Label>
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
                                    <asp:Panel ID="Panel81" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <%--<asp:Panel ID="Panel82" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image23" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel83" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image24" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel84" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton27" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>--%>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label47" runat="server" Font-Bold="true" Text="Part A"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label48" runat="server" Font-Bold="true" Text="Inventory Location Setup"></asp:Label>
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
                                                            Do you want to add a location for your inventory?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton2_Click" Visible="false">  Yes</asp:LinkButton>
                                                        <br />
                                                        <label>
                                                            Do you want to add a room at a specific inventory location?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox8" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox8_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton1_Click" Visible="false">  Yes</asp:LinkButton>
                                                        <br />
                                                        <label>
                                                            Do you want to add a rack in a specific inventory room?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton3_Click" Visible="false">  Yes</asp:LinkButton>
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                                <%-- <td>
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel85" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <%--<asp:Panel ID="Panel86" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image25" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel87" Visible="false" runat="server" Width="100%">
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel88" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton28" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>--%>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label49" runat="server" Font-Bold="true" Text="Part B"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label50" runat="server" Font-Bold="true" Text="Inventory Category Setup"></asp:Label>
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
                                                            Do you want to add a new category of inventory?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox3" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox3_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton4_Click" Visible="false">  Yes</asp:LinkButton>
                                                        <br />
                                                        <label>
                                                            Do you want to add a new sub category for your inventory?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox4" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox4_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton5" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton5_Click" Visible="false">  Yes</asp:LinkButton>
                                                        <br />
                                                        <label>
                                                            Do you want to add a new sub sub category for your inventory?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox5" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox5_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton6_Click" Visible="false">  Yes</asp:LinkButton>
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
                                    <asp:Panel ID="Panel89" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <%-- <asp:Panel ID="Panel90" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image26" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel91" Visible="false" runat="server" Width="100%">
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel92" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton29" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>--%>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label51" runat="server" Font-Bold="true" Text="Part C"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label52" runat="server" Font-Bold="true" Text="Inventory Items Setup"></asp:Label>
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
                                                            Would you like to add inventory Items?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox6" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox6_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton7" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton7_Click" Visible="false">  Yes</asp:LinkButton>
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                                <%-- <td>
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel93" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <%--<asp:Panel ID="Panel94" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image27" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel95" Visible="false" runat="server" Width="100%">
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel96" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton30" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>--%>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label53" runat="server" Font-Bold="true" Text="Part D"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label54" runat="server" Font-Bold="true" Text="Initial Inventory Setup"></asp:Label>
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
                                                        If you are an exisiting business and currently have inventory on hand, you can set
                                                        up an opening inventory amount
                                                        <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton9_Click">  here  </asp:LinkButton>
                                                        .
                                                        <br />
                                                        <label>
                                                            Would you like to add an initial inventory?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox7" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox7_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton8" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton8_Click" Visible="false">  Yes</asp:LinkButton>
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
                    </fieldset>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
