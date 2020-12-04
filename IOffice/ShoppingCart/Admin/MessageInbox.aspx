<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MessageInbox.aspx.cs" Inherits="Account_MessageInbox"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Src="~/Account/UserControl/ExternalInternalMessage1.ascx" TagName="extmsg"
    TagPrefix="extmsg" %>
<%@ Register Src="~/Account/UserControl/MessageList.ascx" TagName="MsgList" TagPrefix="MsgList" %>
<%@ Register Src="~/IOffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
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
        <asp:Panel ID="pnlmsg" runat="server" Visible="False" Width="100%">
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
        </asp:Panel>
        <extmsg:extmsg ID="msgwzd" runat="server" />
        <div style="clear: both;">
        </div>
        <div style="padding-left: 1%">
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
                                                        <asp:Label ID="Label1" runat="server" Text="To"></asp:Label>
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
                                                        <asp:Label ID="Label2" runat="server" Text="Period"></asp:Label>
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
                                                        <asp:Label ID="Label3" runat="server" Text="Usertype"></asp:Label>
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
                                                        <asp:Label ID="Label4" runat="server" Text="UserType"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:DropDownList ID="ddlusertype1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlusertype1_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label5" runat="server" Text="UserName"></asp:Label>
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
                        <asp:Label ID="lblInbox" runat="server" Text="Inbox" Font-Bold="False" Font-Size="Large"></asp:Label>
                    </label>
                </td>
                <td>
                    <asp:Button ID="imgbtndiscard" runat="server" CssClass="btnSubmit" Text="Discard"
                        OnClick="imgbtndiscard_Click" Visible="False" />
                </td>
                <td>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            CausesValidation="False" OnClick="Button1_Click" />
                        <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                </td>
                <%-- <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" ToolTip="Delete"
                        ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                        OnClick="llinkbb_Click"></asp:ImageButton>--%>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 10%">
                    <MsgList:MsgList runat="server" ID="MsgListView" />
                </td>
                <td>
                    <asp:Panel ID="pnlgrid" runat="server">
                        <table width="100%">
                            <tr align="left">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label28" runat="server" Font-Italic="True" Text="List of Message Inbox"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="gridInbox" runat="server" GridLines="Both" AllowPaging="True"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        DataKeyNames="MsgDetailId" OnRowDataBound="gridInbox_RowDataBound" OnPageIndexChanging="gridInbox_PageIndexChanging"
                                        EmptyDataText="There is no Message." AllowSorting="True" OnSorting="gridInbox_Sorting"
                                        Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" OnRowDeleting="gridInbox_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMsg" runat="server" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="HeaderChkbox" runat="server" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MsgDate" HeaderStyle-HorizontalAlign="Left" HeaderText="Date"
                                                HeaderStyle-Width="11%" SortExpression="MsgDate" DataFormatString="{0:MM/dd/yyyy-HH:mm}">
                                                <HeaderStyle HorizontalAlign="Left" Width="11%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="From" SortExpression="MsgDetailId" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <a href="MessageView.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>&Status=<%# Eval("MsgStatusId")%>">
                                                        <b><font color="black">
                                                            <%#  Eval ("Compname")  %></b></font></a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" SortExpression="MsgSubject"
                                                HeaderStyle-Width="55%">
                                                <ItemTemplate>
                                                    <a href="MessageView.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>&Status=<%# Eval("MsgStatusId")%>">
                                                        <b><font color="black">
                                                            <%#  Eval("MsgSubject") %></b></font></a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="55%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MsgStatusName" HeaderStyle-HorizontalAlign="Left" HeaderText="Status"
                                                SortExpression="MsgStatusName" HeaderStyle-Width="10%">
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:BoundField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </cc11:PagingGridView>
                                    <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                    <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                    <%-- </asp:Panel>--%>
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
