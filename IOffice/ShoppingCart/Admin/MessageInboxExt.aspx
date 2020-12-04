<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="MessageInboxExt.aspx.cs" Inherits="Account_MessageInboxExt"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Src="~/Account/UserControl/ExternalInternalMessage.ascx" TagName="extmsg"
    TagPrefix="extmsg" %>
<%@ Register Src="~/Account/UserControl/MessageList1.ascx" TagName="MsgList" TagPrefix="MsgList" %>
<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function ChangeCheckBoxState(id, checkState) {
            var cb = document.getElementById(id);
            if (cb != null)
                cb.checked = checkState;
        }
        // For Document
        function ChangeAllCheckBoxStates(checkState) {
            if (CheckBoxIDs != null) {
                for (var i = 0; i < CheckBoxIDs.length; i++)
                    ChangeCheckBoxState(CheckBoxIDs[i], checkState);
            }
        }
        function ChangeHeaderAsNeeded() {
            if (CheckBoxIDs != null) {
                for (var i = 1; i < CheckBoxIDs.length; i++) {
                    var cb = document.getElementById(CheckBoxIDs[i]);
                    if (!cb.checked) {
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
        <table width="100%">
            <tr>
                <td colspan="3" align="left">
                    <asp:Panel ID="pnlmsg" runat="server" Visible="False" Width="100%">
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="hdr" colspan="3">
                    <extmsg:extmsg ID="msgwzd" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="12%">
                </td>
                <td colspan="2">
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="Select Email ID">
                        </asp:Label>
                    </label>
                    <label>
                        <asp:DropDownList ID="ddlempemail" runat="server" AutoPostBack="True" DataTextField="EmailId"
                            Width="300px" DataValueField="CompanyEmailId" OnSelectedIndexChanged="ddlempemail_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <asp:Panel Visible="false" ID="equalto" runat="server">
                        <label>
                            Unread Emails :
                        </label>
                        <label>
                            <asp:Label ID="Label16" runat="server" Text=""></asp:Label>
                        </label>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 150px">
                    <label>
                        <asp:Label ID="Label2" runat="server" Text="My Account"></asp:Label>
                    </label>
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%">
                                <asp:Button ID="imgbtndiscard" runat="server" Text="Discard" OnClick="imgbtndiscard_Click"
                                    CssClass="btnSubmit" Visible="false" />
                                <asp:Button ID="ImageButton1" runat="server" Text="Spam" CssClass="btnSubmit" OnClick="ImageButton1_Click"
                                    Visible="false" />
                                <asp:Button ID="ImageButton2" runat="server" Text="Save" OnClick="ImageButton2_Click"
                                    CssClass="btnSubmit" Visible="false" />
                                <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btnSubmit" Visible="false"
                                    OnClick="btndelete_Click" />
                                <asp:Button ID="btnemailrule" runat="server" Text="Set Email Rule" CssClass="btnSubmit"
                                    Visible="false" OnClick="btnemailrule_Click" />
                                <asp:Button ID="btnreply" runat="server" Text="Reply" CssClass="btnSubmit" Visible="false"
                                    OnClick="btnreply_Click" />
                                <asp:Button ID="btnforward" runat="server" Text="Forward" CssClass="btnSubmit" Visible="false"
                                    OnClick="btnforward_Click" />
                            </td>
                            <td align="left">
                                <label>
                                    <asp:Label ID="Label14" runat="server" Text="Search"></asp:Label>
                                </label>
                                <label>
                                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="60" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                                </label>
                            </td>
                            <td align="left">
                                <asp:Panel ID="panelsearch" runat="server" Visible="false">
                                    <asp:Button ID="btngo" runat="server" Text="Go" CssClass="btnSubmit" OnClick="btngo_Click" />
                                </asp:Panel>
                            </td>
                            <td valign="top" align="left">
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
                <td style="width: 150px" valign="top">
                    <MsgList:MsgList runat="server" ID="MsgListView" />
                </td>
                <td align="center">
                    <asp:Panel ID="pnlgrid" runat="server">
                        <table width="100%">
                            <tr align="left">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label28" runat="server" Font-Italic="True" Text="List of External Message Inbox"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <cc11:PagingGridView ID="gridInbox" runat="server" AllowPaging="True" AllowSorting="True"
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid"
                                        DataKeyNames="MsgDetailId" EmptyDataText="There is no Message." GridLines="Both"
                                        OnPageIndexChanging="gridInbox_PageIndexChanging" OnRowDataBound="gridInbox_RowDataBound"
                                        OnSorting="gridInbox_Sorting" PagerStyle-CssClass="pgr" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMsg" runat="server" AutoPostBack="True" OnCheckedChanged="chkMsg_CheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="HeaderChkbox" runat="server" AutoPostBack="True" OnCheckedChanged="HeaderChkbox_CheckedChanged" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MsgDate" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="10%" HeaderText="Date" ItemStyle-HorizontalAlign="Left" SortExpression="MsgDate">
                                            </asp:BoundField>
                                            <%--"{0:MM/dd/yyyy-HH:mm}"--%>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="From" ItemStyle-HorizontalAlign="Left"
                                                SortExpression="PartyName" HeaderStyle-Width="25%">
                                                <ItemTemplate>
                                                    <a href='MessageViewExt.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>&amp;Status=<%# Eval("MsgStatusId")%>'>
                                                        <b><font color="black">
                                                            <%#  Eval ("Compname")  %></font></b></font></a>
                                                    <asp:Label ID="lblemail" runat="server" Text='<%#  Eval ("Compname")  %>' Visible="false"></asp:Label> 
                                                    <asp:Label ID="lblemail123" runat="server" Text='<%#  Eval ("Email")  %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <%--    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Email ID" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Subject" ItemStyle-HorizontalAlign="Left"
                                                SortExpression="MsgSubject" HeaderStyle-Width="45%">
                                                <ItemTemplate>
                                                    <a href='MessageViewExt.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>&amp;Status=<%# Eval("MsgStatusId")%>'>
                                                        <b><font color="black">
                                                            <%#  Eval("MsgSubject") %></font></b></font></a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:Image ID="ImgFIle" runat="server" ImageUrl="~/Account/images/attach.png" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:Image ID="ImgFIleHeader" runat="server" ImageUrl="~/Account/images/attach.png" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MsgStatusName" HeaderStyle-HorizontalAlign="Left" HeaderText="Status"
                                                HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" SortExpression="MsgStatusName">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%--<asp:ButtonField CommandName="AddParty" ButtonType="Button" HeaderText="Add To Partylist" HeaderStyle-Width="20px" Text="Add Party" />
                                            --%>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" HeaderText="Add To Partylist"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnAddparty" runat="server" OnClick="btnAddparty_Click" Text="Add Party" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </cc11:PagingGridView>
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                </td>
                        </table>
                    </asp:Panel>
                    <asp:Literal ID="CheckBoxIDsArray" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table id="subinnertbl" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" colspan="3" style="height: 23px">
                    <asp:Panel ID="Pane42342l5" runat="server" BackColor="#E0E0E0" BorderColor="#C0C0FF"
                        BorderStyle="Outset" Width="835px">
                        <table id="subinnertbl1" cellpadding="0" cellspacing="0" class="col2">
                            <tr>
                                <td class="subinnertblfc" colspan="4">
                                    Manage Party Information :
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="label" colspan="4">
                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col3" style="width: 17%">
                                    <label>
                                        Party Type
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6445" runat="server" ControlToValidate="ddlPartyType"
                                            InitialValue="--Select--" ValidationGroup="111">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col4" style="margin-left: 40px">
                                    <label>
                                        <asp:DropDownList ID="ddlPartyType" runat="server" ValidationGroup="111" Width="148px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td class="col3" style="width: 15%">
                                    <label>
                                        Department
                                    </label>
                                </td>
                                <td class="col4">
                                    <label>
                                        <asp:DropDownList ID="ddldept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldept_SelectedIndexChanged"
                                            ValidationGroup="1" Width="148px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col3" style="width: 17%">
                                    <label>
                                        User Name
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6447" runat="server" ControlToValidate="tbUserName"
                                            ErrorMessage="RequiredFieldValidator" ValidationGroup="111">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col4" width="20%">
                                    <label>
                                        <asp:TextBox ID="tbUserName" runat="server" ValidationGroup="111" Width="145px"></asp:TextBox>
                                    </label>
                                </td>
                                <td class="col3" style="width: 15%">
                                    <label>
                                        Designation
                                    </label>
                                </td>
                                <td class="col4">
                                    <label>
                                        <asp:DropDownList ID="ddldesignation" runat="server" ValidationGroup="1" Width="148px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="col3" style="width: 17%">
                                    <label>
                                        Password
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6449" runat="server" ControlToValidate="tbPassword"
                                            ErrorMessage="RequiredFieldValidator" ValidationGroup="111">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col4">
                                    <label>
                                        <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" ValidationGroup="111"
                                            Width="145px"></asp:TextBox>
                                    </label>
                                </td>
                                <td class="col3" style="width: 15%">
                                    <label>
                                        Phone
                                    </label>
                                </td>
                                <td class="col4">
                                    <label>
                                        <asp:TextBox ID="tbPhone" runat="server" Width="145px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col3" style="width: 17%">
                                    <label>
                                        Confirm Password
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tbPassword"
                                            ControlToValidate="tbConPassword" ErrorMessage="*" ValidationGroup="111"></asp:CompareValidator>
                                    </label>
                                </td>
                                <td class="col4">
                                    <label>
                                        <asp:TextBox ID="tbConPassword" runat="server" TextMode="Password" ValidationGroup="111"
                                            Width="145px"></asp:TextBox>
                                    </label>
                                </td>
                                <td class="col3" style="width: 15%">
                                </td>
                                <td class="col4">
                                </td>
                            </tr>
                            <tr>
                                <td class="col3" style="width: 17%">
                                    <label>
                                        Name
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6451" runat="server" ControlToValidate="tbName"
                                            ErrorMessage="RequiredFieldValidator" ValidationGroup="111">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col4">
                                    <label>
                                        <asp:TextBox ID="tbName" runat="server" ValidationGroup="111" Width="145px"></asp:TextBox>
                                    </label>
                                </td>
                                <td class="col3" style="width: 15%">
                                </td>
                                <td class="col4">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 17px">
                                </td>
                            </tr>
                            <tr>
                                <td class="col3" style="width: 17%">
                                    <label>
                                        Company Name
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6453" runat="server" ControlToValidate="tbCompanyName"
                                            ErrorMessage="RequiredFieldValidator" ValidationGroup="111">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col4">
                                    <label>
                                        <asp:TextBox ID="tbCompanyName" runat="server" ValidationGroup="111" Width="145px"></asp:TextBox>
                                    </label>
                                </td>
                                <td class="col3" style="width: 15%">
                                    <label>
                                        Email
                                    </label>
                                </td>
                                <td class="col4">
                                    <label>
                                        <asp:TextBox ID="tbEmail" runat="server" Width="145px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="col3" style="width: 17%">
                                    <label>
                                        Address
                                    </label>
                                </td>
                                <td class="col4">
                                    <label>
                                        <asp:TextBox ID="tbAddress" runat="server" Width="145px"></asp:TextBox>
                                    </label>
                                </td>
                                <td class="col3" style="width: 15%">
                                    <label>
                                        Website
                                    </label>
                                </td>
                                <td class="col4">
                                    <label>
                                        <asp:TextBox ID="tbWebsite" runat="server" Width="145px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col3" style="width: 17%">
                                    <label>
                                        Country
                                    </label>
                                </td>
                                <td class="col4">
                                    <label>
                                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                            ValidationGroup="1" Width="148px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td class="col3" style="width: 15%">
                                    <label>
                                        ZipCode
                                    </label>
                                </td>
                                <td class="col4">
                                    <label>
                                        <asp:TextBox ID="tbZipCode" runat="server" ValidationGroup="1" Width="145px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col3" style="width: 17%">
                                    <label>
                                        State
                                    </label>
                                </td>
                                <td class="col4" style="margin-left: 40px">
                                    <label>
                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                            ValidationGroup="1" Width="148px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td class="col3" style="width: 15%">
                                    <label>
                                        Active
                                    </label>
                                </td>
                                <td class="col4">
                                    <label>
                                        <asp:DropDownList ID="ddlActive" runat="server" Width="148px">
                                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Deactive" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col3" style="width: 17%">
                                    <label>
                                        City
                                    </label>
                                </td>
                                <td class="col4">
                                    <label>
                                        <asp:DropDownList ID="ddlCity" runat="server" ValidationGroup="1" Width="148px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td class="col3" style="width: 15%">
                                </td>
                                <td class="col4">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4" style="height: 31px">
                                    <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="Add" Width="50px"
                                        ValidationGroup="111" CssClass="btnSubmit" />
                                    <asp:Button ID="btncancel" runat="server" OnClick="btncancel_Click" Text="Cancel"
                                        Width="50px" CssClass="btnSubmit" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <asp:Panel ID="Panel3" runat="server" Height="16px" ScrollBars="Both" Width="750px">
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="Button12424" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender142422" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Pane42342l5" TargetControlID="Button12424">
                    </cc1:ModalPopupExtender>
                </td>
            </tr>
        </table>
        <asp:Panel ID="panelemails" runat="server" Visible="false">
            <table>
                <tr style="height: 1px">
                    <td>
                        <label>
                            <asp:Label ID="Label15" runat="server" Text="Select Email ID"></asp:Label>
                        </label>
                    </td>
                    <td valign="bottom">
                        <asp:Button ID="buttondual" runat="server" CssClass="btnSubmit" Text="Select" OnClick="buttondual_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="Show messages from other email ID's"
                                OnCheckedChanged="CheckBox2_CheckedChanged" TextAlign="Left" AutoPostBack="true"
                                Visible="false" />
                        </label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No Email Id's are available." Width="280px" OnRowDataBound="GridView2_RowDataBound"
                            OnPageIndexChanging="GridView2_PageIndexChanging">
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
                                        <asp:Label ID="lalaparty" runat="server" Text='<%#  Eval ("CompanyEmailId")  %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unread" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1we3" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" BorderStyle="Outset" BackColor="#CCCCCC" BorderColor="#666666"
            Width="320px">
            <table>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            <asp:Label ID="Label11" runat="server" Text="Do you want to mark this sender as Spam ?"></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnokkk" runat="server" CssClass="btnSubmit" Text="Yes" OnClick="btnokkk_Click" />
                        <asp:Button ID="btnno" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="btnno_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <input id="Hidden1" runat="Server" name="hdnMaintypeAdd" type="hidden" style="width: 4px" />
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
            PopupControlID="Panel2" TargetControlID="Hidden1" X="500" Y="-200">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel4" runat="server" BorderStyle="Outset" BackColor="#CCCCCC" BorderColor="#666666"
            Width="320px">
            <table>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            <asp:Label ID="Label12" runat="server" Text="Do you want to delete all the messages from this sender ?"></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Yes" OnClick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="Button2_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <input id="Hidden2" runat="Server" name="hdnMaintypeAdd" type="hidden" style="width: 4px" />
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
            PopupControlID="Panel4" TargetControlID="Hidden2" X="500" Y="-200">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnladdress" runat="server" BorderStyle="Outset" Height="150px" Width="380px"
            BackColor="#CCCCCC" BorderColor="#666666" ScrollBars="Both">
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No Email Id's are available." Width="350px">
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <%--  <HeaderTemplate>
                                        <asp:CheckBox ID="HeaderChkbox" runat="server" AutoPostBack="True" OnCheckedChanged="HeaderChkbox_CheckedChanged1" />
                                    </HeaderTemplate>--%>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkParty" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" runat="server" Text='<%#  Eval ("EmailId")  %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr" />
                            <AlternatingRowStyle CssClass="alt" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Button runat="server" ID="buttonGo11" CssClass="btnSubmit" Text="Go" OnClick="buttonGo11_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <input id="hdnMaintypeAdd" runat="Server" name="hdnMaintypeAdd" type="hidden" style="width: 4px" />
        <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
            PopupControlID="pnladdress" TargetControlID="hdnMaintypeAdd" X="750" Y="300">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="panelfilter" runat="server" BorderStyle="Outset" Height="220px" Width="1100px"
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
                            <asp:Button ID="Button3" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="btncancel_Click" />
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
