<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MessageDeletedItems.aspx.cs" Inherits="ShoppingCart_Admin_MessageDeletedItems" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Src="~/Account/UserControl/MessageList.ascx" TagName="MsgList" TagPrefix="MsgList" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/ExternalInternalMessage1.ascx"
    TagName="extmsg" TagPrefix="extmsg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function ChangeCheckBoxState(id, checkState)
        {
            var cb = document.getElementById(id);
            if (cb != null)
               cb.checked = checkState;
        }
        // For Document
        function ChangeAllCheckBoxStates(checkState)
        {
            if (CheckBoxIDs != null)
            {
               for (var i = 0; i < CheckBoxIDs.length; i++)
               ChangeCheckBoxState(CheckBoxIDs[i], checkState);
            }
        }        
        function ChangeHeaderAsNeeded()
        {
            if (CheckBoxIDs != null)
            {
                for (var i = 1; i < CheckBoxIDs.length; i++)
                {
                    var cb = document.getElementById(CheckBoxIDs[i]);
                    if (!cb.checked)
                    {
                       ChangeCheckBoxState(CheckBoxIDs[0], false);
                       return;
                    }
                }        
               ChangeCheckBoxState(CheckBoxIDs[0], true);
            }
        }
          function CallPrint(strid) {
            var prtContent = document.getElementById('<%= gridpnl.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            }
    </script>

    <style type="text/css">
        .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
    </style>
    <div class="products_box">
        <div style="padding-left: 1%">
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <extmsg:extmsg ID="msgwzd" runat="server" />
        <div style="clear: both;">
        </div>
        <table width="100%">
            <tr>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                    <label>
                        <asp:CheckBox ID="chkfilter" runat="server" AutoPostBack="true" Text="Filters?" OnCheckedChanged="chkfilter_CheckedChanged" />
                    </label>
                </td>
                <td style="width: 80%">
                    <asp:Panel ID="pnltype" runat="server" Visible="false" Width="100%">
                        <table width="100%">
                            <tr style="height: 2%">
                                <td style="width: 10%; height: 1%">
                                    <label>
                                        <asp:CheckBox ID="chkdate" runat="server" AutoPostBack="true" Text="By Date" OnCheckedChanged="chkdate_CheckedChanged" />
                                    </label>
                                </td>
                                <td style="width: 27%; height: 1%">
                                    <asp:Panel ID="pnldate" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                                            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Text="Date Range" Value="0"></asp:ListItem>
                                                            <asp:ListItem Selected="True" Text="Period" Value="1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width: 63%; height: 1%" valign="bottom">
                                    <asp:Panel ID="pnlfromto" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label7" runat="server" Text="From"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="txtestartdate" runat="server" Width="70px"></asp:TextBox>
                                                    </label>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton2"
                                                        TargetControlID="txtestartdate">
                                                    </cc1:CalendarExtender>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtestartdate">
                                                    </cc1:MaskedEditExtender>
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label2" runat="server" Text="To"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="txteenddate" runat="server" Width="70px"></asp:TextBox>
                                                    </label>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                                                        TargetControlID="txteenddate">
                                                    </cc1:CalendarExtender>
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                    </label>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txteenddate">
                                                    </cc1:MaskedEditExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="buttongo" runat="server" Text="Go" OnClick="buttongo_Click" CssClass="btnSubmit" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlperiod" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label3" runat="server" Text="Period"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:DropDownList ID="ddlperiod" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlperiod_SelectedIndexChanged">
                                                            <asp:ListItem Selected="True" Text="All" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Today" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Yesterday" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="This Week" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Last Week" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="Last 2 Weeks" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="This Month" Value="6"></asp:ListItem>
                                                            <asp:ListItem Text="Last Month" Value="7"></asp:ListItem>
                                                            <asp:ListItem Text="Last 2 Months" Value="8"></asp:ListItem>
                                                            <asp:ListItem Text="Current Year" Value="9"></asp:ListItem>
                                                            <asp:ListItem Text="Last Year" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="Last 2 Years" Value="11"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr style="height: 2%">
                                <td style="width: 10%; height: 1%">
                                    <label>
                                        <asp:CheckBox ID="chkuser" runat="server" AutoPostBack="true" Text="By User" OnCheckedChanged="chkuser_CheckedChanged" />
                                    </label>
                                </td>
                                <td style="width: 27%; height: 1%">
                                    <asp:Panel ID="pnluser" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                                                            AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                                                            <asp:ListItem Selected="True" Text="By Usertype" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="By Username" Value="1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width: 63%; height: 1%" valign="bottom">
                                    <asp:Panel ID="pnlusertype" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label4" runat="server" Text="Usertype"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:DropDownList ID="ddlusertype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlusertype_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlusername" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label5" runat="server" Text="UserType"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:DropDownList ID="ddlusertype1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlusertype1_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label6" runat="server" Text="UserName"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:DropDownList ID="ddlusername" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlusername_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <div style="float: right;">
            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" Text="Search" TextAlign="Left"
                OnCheckedChanged="CheckBox1_CheckedChanged" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div style="clear: both;">
        </div>
        <asp:Panel ID="Panel1" runat="server" Visible="false">
            <div style="float: right;">
                <label>
                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="60"></asp:TextBox>
                </label>
                <label>
                    <asp:Button ID="btngo" runat="server" Text="Go" CssClass="btnSubmit" OnClick="btngo_Click" />
                </label>
            </div>
        </asp:Panel>
        <table width="100%">
            <tr>
                <td style="width: 10%">
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="Trash" Font-Bold="false" Font-Size="Large"></asp:Label>
                        <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                        <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                        <input id="hdnMaintypeAdd" runat="Server" name="hdnMaintypeAdd" type="hidden" style="width: 4px" />
                        <input id="Hidden1" runat="Server" name="Hidden1" type="hidden" style="width: 4px" />
                    </label>
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td valign="bottom">
                                <asp:Button ID="imgbtnmovetoInbox" runat="server" Text="Move to Inbox" OnClick="imgbtnmovetoInbox_Click"
                                    CssClass="btnSubmit" Visible="False" />
                                <asp:Button ID="imgbtndiscard" runat="server" Text="Discard" OnClick="imgbtndiscard_Click"
                                    CssClass="btnSubmit" Visible="False" />
                            </td>
                            <td>
                                <div style="float: right;">
                                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                        CausesValidation="False" OnClick="Button1_Click" />
                                    <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        type="button" value="Print" visible="false" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 10%">
                    <MsgList:MsgList runat="server" ID="MsgListView" />
                </td>
                <td>
                    <asp:Panel ID="gridpnl" runat="server">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr align="left">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label28" runat="server" Font-Italic="True" Text="List of Message Deleted Items"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="gridDelete" runat="server" AutoGenerateColumns="False" DataKeyNames="MsgDetailId"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="Both"
                                        OnRowDataBound="gridDelete_RowDataBound" AllowPaging="True" OnPageIndexChanging="gridDelete_PageIndexChanging"
                                        EmptyDataText="No Record Found." Width="100%" AllowSorting="True" OnSorting="gridDelete_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMsg" runat="server" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="HeaderChkbox" runat="server" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MsgDate" HeaderStyle-HorizontalAlign="Left" HeaderText="Date"
                                                DataFormatString="{0:MM/dd/yyyy-HH:mm}" HeaderStyle-Width="11%">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <a href="MessageView.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>"><b><font color="black">
                                                        <%#  Eval("Compname")%></b></font></a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="55%">
                                                <ItemTemplate>
                                                    <a href="MessageView.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>"><b><font color="black">
                                                        <%#  Eval("MsgSubject") %></b></font></a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc11:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Literal ID="CheckBoxIDsArray" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
