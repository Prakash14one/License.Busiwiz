<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MessageDeletedItemsExt.aspx.cs" Inherits="Account_MessageDeletedItemsExt"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Src="~/Account/UserControl/MessageList1.ascx" TagName="MsgList" TagPrefix="MsgList" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/ExternalInternalMessage.ascx"
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
        <table width="100%">
            <tr>
                <td colspan="3">
                    <asp:Panel ID="pnlmsg" runat="server" Visible="False" Width="100%">
                        <table width="100%">
                            <tr>
                                <td align="left" class="lblpnl">
                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <extmsg:extmsg ID="msgwzd" runat="server" />
                    <%--Deleted Messages--%>
                </td>
            </tr>
            <tr>
                <td width="12%">
                    <label>
                        <asp:Label ID="Label2" runat="server" Text="Select Email ID" Visible="false"></asp:Label>
                    </label>
                </td>
                <td colspan="2" width="85%">
                    <label>
                        <asp:DropDownList ID="ddlempemail" runat="server" DataTextField="EmailId" DataValueField="CompanyEmailId"
                            OnSelectedIndexChanged="ddlempemail_SelectedIndexChanged" AutoPostBack="True"
                            Width="250px" Visible="false">
                        </asp:DropDownList>
                    </label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    <%-- <pnlhelp:pnlhelp ID="pnlHlp" runat="server" />--%>
                </td>
            </tr>
        </table>
        <asp:Panel ID="panelemails" runat="server" Height="172px" ScrollBars="Vertical" Width="310px">
            <table>
                <tr>
                    <td>
                        <label>
                            <asp:Label ID="Label15" runat="server" Text="Select Email ID"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <asp:Button ID="buttondual" runat="server" CssClass="btnSubmit" Text="Select" OnClick="buttondual_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No Email Id's are available." Width="280px" 
                            OnRowDataBound="GridView2_RowDataBound" 
                            onpageindexchanging="GridView2_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <%-- <HeaderTemplate>
                                        <asp:CheckBox ID="HeaderChkbox" runat="server" AutoPostBack="True" OnCheckedChanged="HeaderChkbox_CheckedChanged1" />
                                    </HeaderTemplate>--%>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkParty" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" runat="server" Text='<%#  Eval ("EmailId")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="No. of Unread Emails" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1we3" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <%--   <table width="100%">
            <tr>
                <td style="width: 12%">
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
                                    <asp:CheckBox ID="chkdate" runat="server" AutoPostBack="true" Text="By Date" OnCheckedChanged="chkdate_CheckedChanged" />
                                </td>
                                <td style="width: 29%; height: 1%">
                                    <asp:Panel ID="pnldate" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                                        OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="Date Range" Value="0"></asp:ListItem>
                                                        <asp:ListItem Selected="True" Text="Period" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width: 61%; height: 1%" valign="bottom">
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
                                                    <cc1:calendarextender id="CalendarExtender2" runat="server" popupbuttonid="ImageButton2"
                                                        targetcontrolid="txtestartdate">
                                                    </cc1:calendarextender>
                                                    <cc1:maskededitextender id="MaskedEditExtender1" runat="server" mask="99/99/9999"
                                                        masktype="Date" targetcontrolid="txtestartdate">
                                                    </cc1:maskededitextender>
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="txteenddate" runat="server" Width="70px"></asp:TextBox>
                                                    </label>
                                                    <cc1:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImageButton1"
                                                        targetcontrolid="txteenddate">
                                                    </cc1:calendarextender>
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                    </label>
                                                    <cc1:maskededitextender id="MaskedEditExtender2" runat="server" mask="99/99/9999"
                                                        masktype="Date" targetcontrolid="txteenddate">
                                                    </cc1:maskededitextender>
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
                                                        <asp:Label ID="Label4" runat="server" Text="Period"></asp:Label>
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
                                    <asp:CheckBox ID="chkuser" runat="server" AutoPostBack="true" Text="By User" OnCheckedChanged="chkuser_CheckedChanged" />
                                </td>
                                <td style="width: 29%; height: 1%">
                                    <asp:Panel ID="pnluser" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                                                        AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Text="By Usertype" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="By Username" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width: 61%; height: 1%" valign="bottom">
                                    <asp:Panel ID="pnlusertype" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label5" runat="server" Text="Usertype"></asp:Label>
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
                                                        <asp:Label ID="Label6" runat="server" Text="UserType"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:DropDownList ID="ddlusertype1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlusertype1_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label71" runat="server" Text="UserName"></asp:Label>
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
                            <tr>
                                <td style="width: 25%" colspan="2" rowspan="2" valign="top">
                                    <asp:CheckBox ID="chktaskproject" runat="server" AutoPostBack="true" Text="By Project and Task"
                                        OnCheckedChanged="chktaskproject_CheckedChanged" />
                                </td>
                                <td style="width: 75%">
                                    <asp:Panel ID="pnltaskproject" Visible="false" runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label8" runat="server" Text="Project"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:DropDownList ID="ddlproject" runat="server" Width="350px" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlproject_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label10" runat="server" Text="Task"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:DropDownList ID="ddltask" runat="server" Width="350px" AutoPostBack="true" OnSelectedIndexChanged="ddltask_SelectedIndexChanged">
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
        </asp:Panel>--%>
        <table width="100%">
            <tr>
                <td style="width: 150px">
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="Trash" Font-Bold="false" Font-Size="Large"></asp:Label>
                    </label>
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 16%">
                                <asp:Button ID="imgbtnmovetoInbox" runat="server" Text="Move to Inbox" OnClick="imgbtnmovetoInbox_Click"
                                    CssClass="btnSubmit" />
                                <asp:Button ID="imgbtndiscard" runat="server" Text="Discard" OnClick="imgbtndiscard_Click"
                                    CssClass="btnSubmit" />
                            </td>
                            <td style="width: 25%">
                                <label>
                                    <asp:Label ID="Label14" runat="server" Text="Search"></asp:Label>
                                </label>
                                <label>
                                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="60"></asp:TextBox>
                                </label>
                            </td>
                            <td style="width: 5%">
                                <asp:Panel ID="panelsearch" runat="server" Visible="true">
                                    <asp:Button ID="btngo" runat="server" Text="Go" CssClass="btnSubmit" OnClick="btngo_Click" />
                                </asp:Panel>
                            </td>
                            <td valign="top" style="width: 10%">
                                <asp:CheckBox ID="chkfilter" runat="server" AutoPostBack="true" Text="Filters?" OnCheckedChanged="chkfilter_CheckedChanged" />
                            </td>
                            <td>
                                <div style="float: right;">
                                    <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                        CausesValidation="False" OnClick="Button1_Click1" />
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
                <td style="width: 150px">
                    <MsgList:MsgList runat="server" ID="MsgListView" />
                </td>
                <td align="center">
                    <asp:Panel ID="gridpnl" runat="server">
                        <table width="100%">
                            <tr align="left">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label28" runat="server" Font-Italic="True" Text="List of External Message Deleted Items"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <cc11:PagingGridView ID="gridDelete" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                                        GridLines="Both" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        DataKeyNames="MsgDetailId" OnRowDataBound="gridDelete_RowDataBound" AllowPaging="True"
                                        OnPageIndexChanging="gridDelete_PageIndexChanging" EmptyDataText="There is no Message."
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
                                            <asp:BoundField DataField="MsgDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="13%" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy-HH:mm}">
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="25%">
                                                <ItemTemplate>
                                                    <a href="MessageViewExt.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>"><b><font color="black">
                                                        <%#  Eval ("Compname")  %></b></font></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="35%">
                                                <ItemTemplate>
                                                    <a href="MessageViewExt.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>"><b><font color="black">
                                                        <%#  Eval("MsgSubject") %></b></font></a>
                                                </ItemTemplate>
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
        <asp:Panel ID="panelfilter" runat="server" BorderStyle="Outset" Height="230px" Width="1100px"
            BackColor="#CCCCCC" BorderColor="#666666">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnltype" runat="server" Visible="false" Width="100%">
                            <table width="100%">
                                <tr>
                                    <td style="width: 10%">
                                        <asp:CheckBox ID="chkdate" runat="server" AutoPostBack="true" Text="By Date" OnCheckedChanged="chkdate_CheckedChanged" />
                                    </td>
                                    <td style="width: 30%">
                                        <asp:Panel ID="pnldate" runat="server" Visible="false">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                                            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Text="Date Range" Value="0"></asp:ListItem>
                                                            <asp:ListItem Selected="True" Text="Period" Value="1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 60%" valign="bottom">
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
                                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:TextBox ID="txteenddate" runat="server" Width="70px"></asp:TextBox>
                                                        </label>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                                                            TargetControlID="txteenddate">
                                                        </cc1:CalendarExtender>
                                                        <label>
                                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                        </label>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                            MaskType="Date" TargetControlID="txteenddate">
                                                        </cc1:MaskedEditExtender>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlperiod" runat="server" Visible="false">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label4" runat="server" Text="Period"></asp:Label>
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
                                <tr>
                                    <td style="width: 10%">
                                        <asp:CheckBox ID="chkuser" runat="server" AutoPostBack="true" Text="By User" OnCheckedChanged="chkuser_CheckedChanged" />
                                    </td>
                                    <td style="width: 30%">
                                        <asp:Panel ID="pnluser" runat="server" Visible="false">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                                                            AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                                                            <asp:ListItem Selected="True" Text="By Usertype" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="By Username" Value="1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 60%" valign="bottom">
                                        <asp:Panel ID="pnlusertype" runat="server" Visible="false">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label5" runat="server" Text="Usertype"></asp:Label>
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
                                                            <asp:Label ID="Label6" runat="server" Text="UserType"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:DropDownList ID="ddlusertype1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlusertype1_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="Label71" runat="server" Text="UserName"></asp:Label>
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
                                <tr>
                                    <td style="width: 25%" colspan="2" rowspan="2" valign="top">
                                        <asp:CheckBox ID="chktaskproject" runat="server" AutoPostBack="true" Text="By Project and Task"
                                            OnCheckedChanged="chktaskproject_CheckedChanged" />
                                    </td>
                                    <td style="width: 75%">
                                        <asp:Panel ID="pnltaskproject" Visible="false" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label8" runat="server" Text="Project"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:DropDownList ID="ddlproject" runat="server" Width="350px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlproject_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label10" runat="server" Text="Task"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:DropDownList ID="ddltask" runat="server" Width="350px" AutoPostBack="true" OnSelectedIndexChanged="ddltask_SelectedIndexChanged">
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
                <tr>
                    <td align="center">
                        <asp:Panel ID="plllll" runat="server" Visible="false">
                            <asp:Button ID="buttongo" runat="server" Text="Go" OnClick="buttongo_Click" CssClass="btnSubmit" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="btncancel_Click" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <input id="Hidden3" runat="Server" name="hdnMaintypeAdd" type="hidden" style="width: 4px" />
        <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
            PopupControlID="panelfilter" TargetControlID="Hidden3" X="150" Y="100">
        </cc1:ModalPopupExtender>
    </div>
</asp:Content>
