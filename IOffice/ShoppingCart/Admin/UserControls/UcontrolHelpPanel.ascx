<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcontrolHelpPanel.ascx.cs"
    Inherits="ShoppingCart_Admin_UserControls_UC_Title" %>
<div id="right_content">
    <asp:Panel runat="server" ID="PNLTITLE">
        <div class="divHeaderLeft" style="height: 56px">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 40%">
                        <div>
                            <h2>
                                <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/ShoppingCart/images/HelpIconHHH.png"
                                    Visible="false" />
                            </h2>
                        </div>
                    </td>
                    <td style="width: 35%">
                        <div style="float: right">
                            <table width="100%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td align="center" >
                                        <asp:Label ID="Label2" runat="server" Visible="false" Text="Control Panel " 
                                            ForeColor="AntiqueWhite" Font-Size="14px" Height="14px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" BorderStyle="Ridge" PostBackUrl="~/ShoppingCart/Admin/Accountingafterlogin.aspx"
                                            Text="Accounting" ToolTip="Accounting" Visible="false" Font-Bold="true" CssClass="btnSubmit" BackColor="#416271"
                                            ForeColor="White" />
                                        <asp:Button ID="Button3" runat="server" BorderStyle="Ridge" PostBackUrl="~/ShoppingCart/Admin/Ifileafterlogin.aspx"
                                            Text="Filling Cabinet" ToolTip="Filling Cabinet" Visible="false" Font-Bold="true" CssClass="btnSubmit" BackColor="#416271"
                                            ForeColor="White" />
                                        <asp:Button ID="Button141" runat="server" BorderStyle="Ridge" PostBackUrl="~/ShoppingCart/Admin/Iofficeafterlogin.aspx"
                                            Text="Office Management" ToolTip="office Management" Visible="false" Font-Bold="true" CssClass="btnSubmit" BackColor="#416271"
                                            ForeColor="White" />
                                        <asp:Button ID="Button5" runat="server" BorderStyle="Ridge" PostBackUrl="~/ShoppingCart/Admin/communicationafterlogin.aspx"
                                            Text="Communication" ToolTip="Communication" Font-Bold="true" Visible="false" CssClass="btnSubmit" BackColor="#416271"
                                            ForeColor="White" />
                                        <asp:Button ID="Button6" runat="server" BorderStyle="Ridge" PostBackUrl="~/ShoppingCart/Admin/timekeepingafterlogin.aspx"
                                            Text="Time Keeping" ToolTip="Time Keeping" Font-Bold="true" Visible="false" CssClass="btnSubmit" BackColor="#416271"
                                            ForeColor="White" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td style="width: 25%">
                        <div style="float: right">
                            <asp:UpdatePanel ID="testup1" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlshow" runat="server">
                                        <table width="100%" cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblcay" runat="server" Text="Business" ForeColor="White" 
                                                        Visible="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlwarehouse" runat="server" OnSelectedIndexChanged="ddlbus_SelectedIndexChanged"
                                                        AutoPostBack="true" Visible="False">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HyperLink ID="lblop" runat="server" ForeColor="White" Target="_blank" 
                                                        Text="Year" Visible="False"></asp:HyperLink>
                                                </td>
                                                <td>
                                                    <%--<asp:Label ID="lblopenaccy" runat="server" Font-Bold="True" ForeColor="White" ></asp:Label>--%>
                                                    <asp:LinkButton ID="lblopenaccy" runat="server" ForeColor="White" 
                                                        onclick="lblopenaccy_Click" Visible="False"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlwarehouse" EventName="SelectedIndexChanged" />
                                    
                                    <asp:PostBackTrigger ControlID="lblopenaccy"  />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <div style="clear: both;">
    </div>
    <asp:Panel runat="server" ID="pnlhelp" Style="border: solid 1px black">
        <h3>
            <asp:Label ID="lblDetail" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt">
            </asp:Label>
        </h3>
    </asp:Panel>
</div>
