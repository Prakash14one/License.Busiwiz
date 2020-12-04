<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="OAsetupwizard.aspx.cs" Inherits="OAsetupwizard"
    Title="Setup Wizard" %>

<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <table width="100%">
                    <tr>
                        <td style="width: 60%">
                            <label>
                                <asp:Label ID="Label1" runat="server" Text="A business is already set up with the name "></asp:Label>
                                <asp:Label ID="lblbusiness" runat="server" Text="" ForeColor="Black"></asp:Label>
                                <asp:Label ID="Label2" runat="server" Text=" Do you wish to edit any information for your newly set business?"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 10%">
                            <asp:Panel ID="pnlyesno" runat="server">
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </td>
                        <td style="width: 10%">
                            <asp:Panel ID="pnldone" runat="server" Visible="false">
                                <label>
                                    <asp:Label ID="done1" runat="server" Text="Done"></asp:Label>
                                </label>
                            </asp:Panel>
                        </td>
                        <td style="width: 10%">
                            <asp:Panel ID="pnlnotrequire" runat="server" Visible="false">
                                <label>
                                    <asp:Label ID="Label12" runat="server" Text="Not Required"></asp:Label>
                                </label>
                            </asp:Panel>
                        </td>
                        <td style="width: 10%">
                            <asp:Panel ID="pnltryagain" runat="server" Visible="false">
                                <label>
                                    <asp:Label ID="notdone1" runat="server" Text="Not Done"></asp:Label>
                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Try Again</asp:LinkButton>
                                </label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Panel runat="server" ID="pnldepartment" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 60%">
                                            <label>
                                                <asp:Label ID="Label3" runat="server" Text="There are default departments created for your business. For a full listing, please click "></asp:Label>
                                                <asp:LinkButton ID="LinkButton15" runat="server" OnClick="LinkButton15_Click">here.</asp:LinkButton>
                                                <asp:Label ID="Label58" runat="server" Text=" Would you like to add any personalized departments?"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnldeptyesno" runat="server">
                                                <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList2_SelectedIndexChanged">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnldeptdone" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="done2" runat="server" Text="Done"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnldeptnotrequire" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label13" runat="server" Text="Not Required"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnldepttryagain" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="notdone2" runat="server" Text="Not Done"></asp:Label>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Try Again</asp:LinkButton>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Panel runat="server" ID="pnldesignation" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 60%">
                                            <label>
                                                <asp:Label ID="Label4" runat="server" Text="There are default designation created for each of the default department. For a full listing, please click "></asp:Label>
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">here.</asp:LinkButton>
                                                <asp:Label ID="Label6" runat="server" Text=" Would you like to add any personalized designation?"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnldegnyesno" runat="server">
                                                <asp:CheckBoxList ID="CheckBoxList3" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList3_SelectedIndexChanged">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnldegndone" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="done3" runat="server" Text="Done"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnldegnnotrequire" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label14" runat="server" Text="Not Required"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnldegntryagain" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="notdone3" runat="server" Text="Not Done"></asp:Label>
                                                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">Try Again</asp:LinkButton>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Panel runat="server" ID="pnlemployee" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 60%">
                                            <label>
                                                <asp:Label ID="Label7" runat="server" Text="Do you wish to add employees to your business now?"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlempyesno" runat="server">
                                                <asp:CheckBoxList ID="CheckBoxList5" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList5_SelectedIndexChanged">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlempdone" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="done5" runat="server" Text="Done"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlempnotrequire" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label16" runat="server" Text="Not Required"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlemptryagain" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="notdone5" runat="server" Text="Not Done"></asp:Label>
                                                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click">Try Again</asp:LinkButton>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Panel runat="server" ID="pnlcabinet" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 60%">
                                            <label>
                                                <asp:Label ID="Label5" runat="server" Text="The current accounting year of your business is <strat date - end date>"></asp:Label>
                                                <asp:Label ID="Label37" runat="server" ></asp:Label>
                                                <asp:Label ID="Label39" runat="server" Text="-" ></asp:Label>
                                                
                                                <asp:Label ID="Label38" runat="server" ></asp:Label>
                                                <asp:Label ID="Label36" runat="server" Text="Do you wish to change your current accounting year? "></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlcabinetyesno" runat="server">
                                                <asp:CheckBoxList ID="CheckBoxList4" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList4_SelectedIndexChanged">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlcabinetdone" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label8" runat="server" Text="Done"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlcabinetnotrequire" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label9" runat="server" Text="Not Required"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlcabinettryagain" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label10" runat="server" Text="Not Done"></asp:Label>
                                                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click">Try Again</asp:LinkButton>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Panel runat="server" ID="pnldrawer" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 60%">
                                            <label>
                                                <asp:Label ID="Label15" runat="server" Text="Do you wish to add opening balance to your business for the current accounting year?"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnldraweryesno" runat="server">
                                                <asp:CheckBoxList ID="CheckBoxList6" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList6_SelectedIndexChanged">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnldrawerdone" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label17" runat="server" Text="Done"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnldrawernotrequire" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label18" runat="server" Text="Not Required"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnldrawertryagain" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label19" runat="server" Text="Not Done"></asp:Label>
                                                    <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click">Try Again</asp:LinkButton>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Panel runat="server" ID="pnlfolder" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 60%">
                                            <label>
                                                <asp:Label ID="Label20" runat="server" Text="Do you wish to setup taxes on sales for your business?"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlfolderyesno" runat="server">
                                                <asp:CheckBoxList ID="CheckBoxList7" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList7_SelectedIndexChanged">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlfolderdone" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label21" runat="server" Text="Done"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlfoldernotrequire" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label22" runat="server" Text="Not Required"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlfoldertryagain" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label23" runat="server" Text="Not Done"></asp:Label>
                                                    <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click">Try Again</asp:LinkButton>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Panel runat="server" ID="pnlemailrule" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 60%">
                                            <label>
                                                <asp:Label ID="Label24" runat="server" Text="Do you wish to setup default payment option for online and retail sales of your business?"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlemailruleyesno" runat="server">
                                                <asp:CheckBoxList ID="CheckBoxList8" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList8_SelectedIndexChanged">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlemailruledone" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label25" runat="server" Text="Done"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlemailrulenotrequire" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label26" runat="server" Text="Not Required"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlemailruletryagain" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label27" runat="server" Text="Not Done"></asp:Label>
                                                    <asp:LinkButton ID="LinkButton9" runat="server" OnClick="LinkButton9_Click">Try Again</asp:LinkButton>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Panel runat="server" ID="pnlftprule" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 60%">
                                            <label>
                                                <asp:Label ID="Label28" runat="server" Text="Do you wish to set up a discount based on the volume of items ordered?"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlftpruleyesno" runat="server">
                                                <asp:CheckBoxList ID="CheckBoxList9" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList9_SelectedIndexChanged">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlftpruledone" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label29" runat="server" Text="Done"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlftprulenotrequire" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label30" runat="server" Text="Not Required"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlftpruletryagain" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label31" runat="server" Text="Not Done"></asp:Label>
                                                    <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton10_Click">Try Again</asp:LinkButton>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Panel runat="server" ID="pnlfolderrule" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 60%">
                                            <label>
                                                <asp:Label ID="Label32" runat="server" Text="Do you wish to set up a discount for your customers based on their order value?"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlfolderruleyesno" runat="server">
                                                <asp:CheckBoxList ID="CheckBoxList10" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList10_SelectedIndexChanged">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlfolderruledone" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label33" runat="server" Text="Done"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlfolderrulenotrequire" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label34" runat="server" Text="Not Required"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Panel ID="pnlfolderruletryagain" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label35" runat="server" Text="Not Done"></asp:Label>
                                                    <asp:LinkButton ID="LinkButton11" runat="server" OnClick="LinkButton11_Click">Try Again</asp:LinkButton>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                   
                    <tr>
                        <td colspan="5" align="center">
                            <asp:Panel runat="server" ID="pnlfinish" Width="100%" Visible="false">
                                <asp:Button ID="Button34" CssClass="btnSubmit" runat="server" Text="Close" 
                                    onclick="Button34_Click" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel11" runat="server" BackColor="White" BorderColor="#999999" Width="30%"
                    ScrollBars="None" BorderStyle="Solid" BorderWidth="10px">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <label>
                                    <asp:Label ID="Label11" runat="server" Text="Have you done adding Businesses ?"></asp:Label>
                                </label>
                                <label>
                                    <asp:Button ID="Button2" runat="server" Text="Yes" CssClass="btnSubmit" OnClick="Button2_Click" />
                                </label>
                                <label>
                                    <asp:Button ID="Button3" runat="server" Text="No" CssClass="btnSubmit" OnClick="Button3_Click" />
                                </label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel11" TargetControlID="HiddenButton222" runat="server">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel112" runat="server" BackColor="White" BorderColor="#999999" Width="30%"
                    ScrollBars="None" BorderStyle="Solid" BorderWidth="10px">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <label>
                                    <asp:Label ID="Label112" runat="server" Text="Have you done adding Departments ?"></asp:Label>
                                </label>
                                <label>
                                    <asp:Button ID="btnpnl2" runat="server" Text="Yes" CssClass="btnSubmit" OnClick="btnpnl2_Click" />
                                </label>
                                <label>
                                    <asp:Button ID="btnpnl22" runat="server" Text="No" CssClass="btnSubmit" OnClick="btnpnl22_Click" />
                                </label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="HiddenButton223" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender12" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel112" TargetControlID="HiddenButton223" runat="server">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel12" runat="server" BackColor="White" BorderColor="#999999" Width="70%"
                    ScrollBars="None" BorderStyle="Solid" BorderWidth="10px">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:GridView AutoGenerateColumns="False" ID="GridView1" runat="server" Width="100%"
                                    EmptyDataText="No Record Found." CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Department Name" ItemStyle-Width="50%" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrddeptname123" runat="server" Text='<%# Eval("Departmentname")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation Name" ItemStyle-Width="50%" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldesignation123" runat="server" Text='<%# Eval("DesignationName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btnSubmit" OnClick="Button1_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="hidednn" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel12" TargetControlID="hidednn" runat="server" CancelControlID="Button1">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel113" runat="server" BackColor="White" BorderColor="#999999" Width="30%"
                    ScrollBars="None" BorderStyle="Solid" BorderWidth="10px">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <label>
                                    <asp:Label ID="Label113" runat="server" Text="Have you done adding Designations ?"></asp:Label>
                                </label>
                                <label>
                                    <asp:Button ID="btnpnl3" runat="server" Text="Yes" CssClass="btnSubmit" OnClick="btnpnl3_Click" />
                                </label>
                                <label>
                                    <asp:Button ID="btnpnl33" runat="server" Text="No" CssClass="btnSubmit" OnClick="btnpnl33_Click" />
                                </label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="HiddenButton224" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender13" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel113" TargetControlID="HiddenButton224" runat="server">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="employeepopup" runat="server" BackColor="White" BorderColor="#999999"
                    Width="30%" ScrollBars="None" BorderStyle="Solid" BorderWidth="10px">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <label>
                                    <asp:Label ID="Label48" runat="server" Text="Have you done adding Employees ?"></asp:Label>
                                </label>
                                <label>
                                    <asp:Button ID="Button4" runat="server" Text="Yes" CssClass="btnSubmit" OnClick="Button4_Click" />
                                </label>
                                <label>
                                    <asp:Button ID="Button5" runat="server" Text="No" CssClass="btnSubmit" OnClick="Button5_Click" />
                                </label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button6" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="Modalemployeepopup" BackgroundCssClass="modalBackground"
                    PopupControlID="employeepopup" TargetControlID="Button6" 
                    runat="server">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="cabinetpopup" runat="server" BackColor="White" BorderColor="#999999"
                    Width="35%" ScrollBars="None" BorderStyle="Solid" BorderWidth="10px">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <label>
                                    <asp:Label ID="Label49" runat="server" Text="Have you set your current accounting year ?"></asp:Label>
                                </label>
                                <label>
                                    <asp:Button ID="Button7" runat="server" Text="Yes" CssClass="btnSubmit" OnClick="Button7_Click" />
                                </label>
                                <label>
                                    <asp:Button ID="Button8" runat="server" Text="No" CssClass="btnSubmit" OnClick="Button8_Click" />
                                </label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button9" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtendercabinetpopup" BackgroundCssClass="modalBackground"
                    PopupControlID="cabinetpopup" TargetControlID="Button9" 
                    runat="server">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="drawerpopup" runat="server" BackColor="White" BorderColor="#999999"
                    Width="30%" ScrollBars="None" BorderStyle="Solid" BorderWidth="10px">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <label>
                                    <asp:Label ID="Label50" runat="server" Text="Have you add opening balance ?"></asp:Label>
                                </label>
                                <label>
                                    <asp:Button ID="Button10" runat="server" Text="Yes" CssClass="btnSubmit" OnClick="Button10_Click" />
                                </label>
                                <label>
                                    <asp:Button ID="Button11" runat="server" Text="No" CssClass="btnSubmit" OnClick="Button11_Click" />
                                </label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button12" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtenderdrawerpopup" BackgroundCssClass="modalBackground"
                    PopupControlID="drawerpopup" TargetControlID="Button12" 
                    runat="server">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="folderpopup" runat="server" BackColor="White" BorderColor="#999999"
                    Width="30%" ScrollBars="None" BorderStyle="Solid" BorderWidth="10px">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <label>
                                    <asp:Label ID="Label51" runat="server" Text="Have you set taxes on sales ?"></asp:Label>
                                </label>
                                <label>
                                    <asp:Button ID="Button13" runat="server" Text="Yes" CssClass="btnSubmit" OnClick="Button13_Click" />
                                </label>
                                <label>
                                    <asp:Button ID="Button14" runat="server" Text="No" CssClass="btnSubmit" OnClick="Button14_Click" />
                                </label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button15" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtenderfolderpopup" BackgroundCssClass="modalBackground"
                    PopupControlID="folderpopup" TargetControlID="Button15" 
                    runat="server">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="emailrulepopup" runat="server" BackColor="White" BorderColor="#999999"
                    Width="30%" ScrollBars="None" BorderStyle="Solid" BorderWidth="10px">
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label52" runat="server" Text="Have you  set default payment option ?"></asp:Label>
                                </label>
                                <label>
                                    <asp:Button ID="Button16" runat="server" Text="Yes" CssClass="btnSubmit" OnClick="Button16_Click" />
                                </label>
                                <label>
                                    <asp:Button ID="Button17" runat="server" Text="No" CssClass="btnSubmit" OnClick="Button17_Click" />
                                </label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button18" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtenderemailrulepopup" BackgroundCssClass="modalBackground"
                    PopupControlID="emailrulepopup" TargetControlID="Button18" 
                    runat="server">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="ftprulepopup" runat="server" BackColor="White" BorderColor="#999999"
                    Width="50%" ScrollBars="None" BorderStyle="Solid" BorderWidth="10px">
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label53" runat="server" Text="Have you  set the discount based on the volume of items ordered ?"></asp:Label>
                                </label>
                                <label>
                                    <asp:Button ID="Button19" runat="server" Text="Yes" CssClass="btnSubmit" OnClick="Button19_Click" />
                                </label>
                                <label>
                                    <asp:Button ID="Button20" runat="server" Text="No" CssClass="btnSubmit" OnClick="Button20_Click" />
                                </label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button21" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtenderftprulepopup" BackgroundCssClass="modalBackground"
                    PopupControlID="ftprulepopup" TargetControlID="Button21" 
                    runat="server">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="folderrulepopup" runat="server" BackColor="White" BorderColor="#999999"
                    Width="50%" ScrollBars="None" BorderStyle="Solid" BorderWidth="10px">
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label54" runat="server" Text="Have you  set discount for your customers based on their order value ?"></asp:Label>
                                </label>
                                <label>
                                    <asp:Button ID="Button22" runat="server" Text="Yes" CssClass="btnSubmit" OnClick="Button22_Click" />
                                </label>
                                <label>
                                    <asp:Button ID="Button23" runat="server" Text="No" CssClass="btnSubmit" OnClick="Button23_Click" />
                                </label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button24" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtenderfolderrulepopup" BackgroundCssClass="modalBackground"
                    PopupControlID="folderrulepopup" TargetControlID="Button24" 
                    runat="server">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
               
                <asp:Panel ID="pnldeptgrid" runat="server" BackColor="White" BorderColor="#999999"
                    Width="65%" ScrollBars="Vertical" BorderStyle="Solid" BorderWidth="10px">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:GridView AutoGenerateColumns="False" ID="griddocmaintype" runat="server" Width="100%"
                                    EmptyDataText="No Record Found." CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Department Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrddeptname" runat="server" Text='<%# Eval("Departmentname")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="Button36" runat="server" Text="Close" CssClass="btnSubmit" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button37" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtenderpnldeptgrid" BackgroundCssClass="modalBackground"
                    PopupControlID="pnldeptgrid" TargetControlID="Button37" 
                    runat="server">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
