<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="PaymnentOption.aspx.cs" Inherits="ShoppingCart_Admin_PaymnentOption"
    Title="PaymnetOption" %>

<%@ Register Src="~/ShoppingCart/Admin/UserControl/UControlWizardpanel.ascx" TagName="pnl"
    TagPrefix="pnl" %>
<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td width="30%">
                                <label>
                                    <asp:Label ID="lbgfd" Text="Business Name" runat="server"></asp:Label>
                                    <asp:RequiredFieldValidator ID="red2" runat="server" ControlToValidate="ddlSearchByStore"
                                         ErrorMessage="*" InitialValue="-Select-" SetFocusOnError="True"
                                        ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td width="30%" colspan="2">
                                <label>
                                    <asp:DropDownList ID="ddlSearchByStore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchByStore_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td width="40%" colspan="3">
                                <label>
                                    <asp:Label ID="Label6" Text="Default account where to deposite funds" runat="server"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Payment Method Type Allowed"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 15%">
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Online Sales"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 15%">
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Retail Sales"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 15%" colspan="3">
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Cash Bank Balances Group"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblpaypal" runat="server" Text="Paypal"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="onpaypal" runat="server" Enabled="false" />
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="retpaypal" runat="server" Enabled="false" />
                            </td>
                            <td style="width: 15%">
                                <asp:DropDownList ID="ddlpayacc" runat="server" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 3%">
                                <asp:ImageButton ID="LinkButton13" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                    OnClick="LinkButton97666667_Click" ToolTip="AddNew" ImageAlign="Bottom" Width="20px" />
                            </td>
                            <td style="width: 3%">
                                <asp:ImageButton ID="imgdeptrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                    ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton13_Click"
                                    ToolTip="Refresh" Width="20px" ImageAlign="Bottom" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblcreditcard" runat="server" Text="Credit Card"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="oncreditcart" runat="server" Enabled="false" />
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="retcreditcart" runat="server" Enabled="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlcreditcardacc" runat="server" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton2" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                    OnClick="LinkButton97666667_Click" ToolTip="AddNew" ImageAlign="Bottom" Width="20px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Refresh" Height="20px"
                                    ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                    ImageAlign="Bottom" OnClick="ImageButton1_Click1" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblcheque" runat="server" Text="Cheque"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="oncheque" runat="server" Enabled="false" />
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="retcheque" runat="server" Enabled="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlcheque" Enabled="False" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton5" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                    OnClick="LinkButton97666667_Click" ToolTip="AddNew" ImageAlign="Bottom" Width="20px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton4" runat="server" AlternateText="Refresh" Height="20px"
                                    ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                    ImageAlign="Bottom" OnClick="ImageButton4_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblcredit" runat="server" Text="Credit"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="oncredit" runat="server" Enabled="false" />
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="retcredit" runat="server" Enabled="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlcredit" Enabled="False" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton7" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                    OnClick="LinkButton97666667_Click" ToolTip="AddNew" ImageAlign="Bottom" Width="20px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton6" runat="server" AlternateText="Refresh" Height="20px"
                                    ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                    ImageAlign="Bottom" OnClick="ImageButton6_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblcreditcard_oiffline" runat="server" Text="Credit Card (offline)"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="oncreditcartoff" runat="server" Enabled="false" />
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="retcreditcartoff" runat="server" Enabled="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlcreditoffacc" Enabled="False" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton9" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                    OnClick="LinkButton97666667_Click" ToolTip="AddNew" ImageAlign="Bottom" Width="20px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton8" runat="server" AlternateText="Refresh" Height="20px"
                                    ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                    ImageAlign="Bottom" OnClick="ImageButton8_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lbldo" runat="server" Text="Cash On Delivery"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="ondo" runat="server" Enabled="false" />
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="retdo" runat="server" Enabled="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlcashondeli" Enabled="False" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton11" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                    OnClick="LinkButton97666667_Click" ToolTip="AddNew" ImageAlign="Bottom" Width="20px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton10" runat="server" AlternateText="Refresh" Height="20px"
                                    ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                    ImageAlign="Bottom" OnClick="ImageButton10_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblcash" runat="server" Text="Cash"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="oncash" runat="server" Enabled="false" />
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="retcash" runat="server" Enabled="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlcash" runat="server" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton13" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                    OnClick="LinkButton97666667_Click" ToolTip="AddNew" ImageAlign="Bottom" Width="20px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton12" runat="server" AlternateText="Refresh" Height="20px"
                                    ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                    ImageAlign="Bottom" OnClick="ImageButton12_Click" />
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label8" runat="server" Text="Gift Card"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="chkgiftcardonline" runat="server" Enabled="false" />
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="chkgiftcardretail" runat="server" Enabled="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlgiftcardacc" runat="server" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td>
                              <asp:ImageButton ID="ImageButton16" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                    OnClick="LinkButtonImageButton16_Click" ToolTip="AddNew" ImageAlign="Bottom"
                                    Width="20px" />
                            </td>
                            <td>
                              <asp:ImageButton ID="ImageButton17" runat="server" AlternateText="Refresh" Height="20px"
                                    ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                    ImageAlign="Bottom" OnClick="ImageButtonImageButton17_Click" />
                            </td>
                        </tr>
                         <tr>
                            <td align="left">
                                <asp:Label ID="Label7" runat="server" Text="Debit/Credit Card"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="chkdebitcreditcardonline" runat="server" Enabled="false" Visible="False" />
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="chkdebitcreditcardretail" runat="server" Enabled="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddldebitcreditcardacc" runat="server" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton14" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                    OnClick="LinkButtonImageButton14_Click" ToolTip="AddNew" ImageAlign="Bottom"
                                    Width="20px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton15" runat="server" AlternateText="Refresh" Height="20px"
                                    ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                    ImageAlign="Bottom" OnClick="ImageButtonImageButton15_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                            <br />
                            </td>
                        </tr>
                         <tr>
                            <td align="left">
                               
                            </td>
                            <td >
                                 <asp:Button ID="ImageButton3" runat="server" Text="Change" CssClass="btnSubmit" ValidationGroup="1"
                                    OnClick="ImageButton1_Click" />
                            </td>
                            <td >
                                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Cancel" CssClass="btnSubmit" />
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                            <td>
                               
                            </td>
                        </tr>
                      
                    </table>
                </fieldset>
                <%-- </td>
                </tr>
            </table>  --%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
