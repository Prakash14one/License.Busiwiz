<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MessageDrafts.aspx.cs" Inherits="Account_MessageDrafts"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Src="~/Account/UserControl/MessageList.ascx" TagName="MsgList" TagPrefix="MsgList" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/ExternalInternalMessage1.ascx"
    TagName="extmsg" TagPrefix="extmsg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
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
            <asp:Panel ID="pnlmsg" runat="server" Visible="False" Width="100%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </asp:Panel>
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
          <div style="clear: both;">
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 10%">
                        <label>
                            <asp:Label ID="lblInbox" runat="server" Text="Draft" Style="font-weight: 700"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <asp:Button ID="imgbtnsend" runat="server" Text="Send" OnClick="imgbtnsend_Click"
                            CssClass="btnSubmit" Visible="false" />
                        <asp:Button ID="imgbtndiscard" runat="server" Text="Discard" OnClick="imgbtndiscard_Click"
                            CssClass="btnSubmit" Visible="false" />
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
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 10%">
                        <MsgList:MsgList runat="server" ID="MsgListView" />
                    </td>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlgrid" runat="server">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr align="left">
                                                <td>
                                                    <div id="mydiv" class="closed">
                                                        <table width="100%">
                                                            <tr align="center">
                                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                    <asp:Label ID="Label28" runat="server" Font-Italic="True" Text="List of Message Drafts"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <cc11:PagingGridView ID="gridDraft" runat="server" DataKeyNames="MsgId" GridLines="Both"
                                                            AllowPaging="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                            AutoGenerateColumns="False" OnPageIndexChanging="gridDraft_PageIndexChanging"
                                                            OnRowCommand="gridDraft_RowCommand" OnRowDataBound="gridDraft_RowDataBound" EmptyDataText="There is no Message."
                                                            Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkMsg" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="HeaderChkbox" runat="server" />
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <%--<asp:BoundField DataField="MsgDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy-HH:mm}">
                                            <HeaderStyle CssClass="wideColumn"  />
                                            <ItemStyle CssClass="wideColumn"  />
                                        </asp:BoundField>--%>
                                                                <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-Width="11%">
                                                                    <ItemTemplate>
                                                                        <a href='MessageCompose.aspx?MsgId=<%# Eval("MsgId")%>' style="color: Black">
                                                                            <%#  Eval("MsgDate", "{0:MM/dd/yyyy-HH:mm}")%></a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png" />
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sent To" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblSentTo"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-Width="55%" SortExpression="MsgSubject">
                                                                    <ItemTemplate>
                                                                        <a href='MessageCompose.aspx?MsgId=<%# Eval("MsgId")%>' style="color: Black">
                                                                            <%#  Eval("MsgSubject") %></a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </cc11:PagingGridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Literal ID="CheckBoxIDsArray" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
