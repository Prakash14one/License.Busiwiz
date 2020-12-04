<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="BatchworkingDay.aspx.cs" Inherits="Add_Batch_working_Day" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <script language="javascript" type="text/javascript">
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
    <asp:UpdatePanel ID="pnnn1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False" />
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" Text="Add New Weekly Work Schedule" runat="server" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <asp:Panel ID="pnladd" Visible="false" runat="server">
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label3" Text="A. Select working days and schedules" runat="server"></asp:Label>
                            </legend>
                            <label>
                                <asp:Label ID="lblBusinessNamedd" Text="Business Name" runat="server"></asp:Label>
                                <asp:Label ID="Label8" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlstorename"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="--Select--" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlstorename" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstorename_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="lblBatchNamedd" Text="Batch Name" runat="server"></asp:Label>
                                <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlbatchname"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlbatchname" runat="server" AutoPostBack="True" Width="147px"
                                    OnSelectedIndexChanged="ddlbatchname_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:HyperLink ID="LinkButton152" runat="server" NavigateUrl="../Admin/BatchTimingManage.aspx"
                                    Target="_blank" Visible="false"></asp:HyperLink>
                                <asp:LinkButton ID="LNK" runat="server" OnClick="LNK_Click" Visible="false"></asp:LinkButton>
                            </label>
                            <div style="clear: both;">
                            </div>
                            <asp:Panel ID="pnlworking" Visible="false" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblselectwday" Text="Select the Working Days" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblselectbt" Text="Select the Batch Timing" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="WorkingDay">
                                                <asp:Label ID="lblMonday" runat="server" Text="Monday"></asp:Label>
                                                <div style="padding-left: 6%">
                                                    <asp:CheckBox ID="chkmoday" runat="server" AutoPostBack="True" OnCheckedChanged="chkmoday_CheckedChanged" />
                                                </div>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlMonday" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="lblMondaySchedule" runat="server" Text="Schedule"></asp:Label>
                                                    <asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlmonday"
                                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlmonday" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="WorkingDay">
                                                <asp:Label ID="lblTuesday" runat="server" Text="Tuesday"></asp:Label>
                                                <div style="padding-left: 6%">
                                                    <asp:CheckBox ID="chktuesday" runat="server" AutoPostBack="True" OnCheckedChanged="chktuesday_CheckedChanged" />
                                                </div>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlTuesday" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="lblTuesdaySchedule" runat="server" Text="Schedule"></asp:Label>
                                                    <asp:Label ID="Label7" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddltuesday"
                                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddltuesday" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="WorkingDay">
                                                <asp:Label ID="lblWednesday" runat="server" Text="Wednesday"></asp:Label>
                                                <div style="padding-left: 6%">
                                                    <asp:CheckBox ID="chkwednseday" runat="server" AutoPostBack="True" OnCheckedChanged="chkwednseday_CheckedChanged" />
                                                </div>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlWednesday" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="lblWednesdaySchedule" runat="server" Text="Schedule"></asp:Label>
                                                    <asp:Label ID="Label9" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlwednesday"
                                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlwednesday" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="WorkingDay">
                                                <asp:Label ID="lblThursday" runat="server" Text="Thursday"></asp:Label>
                                                <div style="padding-left: 6%">
                                                    <asp:CheckBox ID="chkthursday" runat="server" AutoPostBack="True" OnCheckedChanged="chkthursday_CheckedChanged" />
                                                </div>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlThursday" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="lblThursdaySchedule" runat="server" Text="Schedule"></asp:Label>
                                                    <asp:Label ID="Label10" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlthursday"
                                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlthursday" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="WorkingDay">
                                                <asp:Label ID="lblFriday" runat="server" Text="Friday"></asp:Label>
                                                <div style="padding-left: 6%">
                                                    <asp:CheckBox ID="chkfriday" runat="server" AutoPostBack="True" OnCheckedChanged="chkfriday_CheckedChanged" />
                                                </div>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlFriday" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="lblFridaySchedule" runat="server" Text="Schedule"></asp:Label>
                                                    <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlfriday"
                                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlfriday" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="WorkingDay">
                                                <asp:Label ID="lblSaturday" runat="server" Text="Saturday"></asp:Label>
                                                <div style="padding-left: 6%">
                                                    <asp:CheckBox ID="chksaturday" runat="server" AutoPostBack="True" OnCheckedChanged="chksaturday_CheckedChanged" />
                                                </div>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlSaturday" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="lblSaturdaySchedule" runat="server" Text="Schedule"></asp:Label>
                                                    <%-- <asp:Label ID="Label12" runat="server" Text="*" CssClass="labelstar"></asp:Label>
               <asp:RequiredFieldValidator ID="ReqedFieldValidator18" runat="server" ControlToValidate="ddlsaturday" Display="Dynamic" SetFocusOnError="true"
                                    InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>--%>
                                                    <asp:DropDownList ID="ddlsaturday" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="WorkingDay">
                                                <asp:Label ID="lblSunday" runat="server" Text="Sunday"></asp:Label>
                                                <div style="padding-left: 6%">
                                                    <asp:CheckBox ID="chksunday" runat="server" AutoPostBack="True" OnCheckedChanged="chksunday_CheckedChanged" />
                                                </div>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlSunday" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="lblSundaySchedule" runat="server" Text="Schedule"></asp:Label>
                                                    <%--<asp:Label ID="Label13" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                     <asp:RequiredFieldValidator ID="RequiredFieldVidator19" runat="server" SetFocusOnError="true" Display="Dynamic"
                         ControlToValidate="ddlsunday" InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>--%>
                                                    <asp:DropDownList ID="ddlsunday" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </fieldset>
                        <div style="clear: both;">
                        </div>
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label4" Text="B. Select the last day of the week" runat="server"></asp:Label>
                            </legend>
                            <table width="100%">
                                <tr>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="lblRegularlastday" runat="server" Text="Regular last day of the week "></asp:Label>
                                            <asp:Label ID="Label14" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddllastdayofweek"
                                                InitialValue="0" ValidationGroup="1" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddllastdayofweek" runat="server">
                                                <asp:ListItem Value="1" Text="Monday"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Tuesday"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Wednesday"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Thursday"></asp:ListItem>
                                                <asp:ListItem Selected="True" Value="5" Text="Friday"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="Saturday"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="Sunday"></asp:ListItem>
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                        <label>
                                            First day of this Batch for first week
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtstartdate" runat="server" Width="75px" MaxLength="10" AutoPostBack="True"
                                                OnTextChanged="txtstartdate_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton1"
                                                Format="MM/dd/yyyy" TargetControlID="txtstartdate">
                                            </cc1:CalendarExtender>
                                        </label>
                                        <label>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                        <label>
                                            Last day of this Batch for first week
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label18" runat="server" Text=""></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <div style="clear: both;">
                        </div>
                        <asp:Button ID="ImageButton2" Text="Submit" runat="server" OnClick="ImageButton2_Click1"
                            ValidationGroup="1" CssClass="btnSubmit" />
                        <asp:Button ID="ImageButton49" Text="Update" runat="server" OnClick="ImageButton49_Click"
                            ValidationGroup="1" CssClass="btnSubmit" Visible="False" />
                        <asp:Button ID="ImageButton48" Text="Cancel" runat="server" OnClick="ImageButton48_Click"
                            CssClass="btnSubmit" />
                        <div style="clear: both;">
                        </div>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Working Days and Time Schedule" runat="server"></asp:Label></legend>
                    <label>
                        <asp:Label ID="lblselectbusiness" Text="Filter by Business Name" runat="server"></asp:Label>
                    </label>
                    <label>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label12" Text="Filter by Batch Status" runat="server" Visible="true"></asp:Label>
                    </label>
                    <label>
                        <asp:DropDownList ID="ddlfilterstatus" runat="server" Width="100px" AutoPostBack="True"
                            Visible="true" OnSelectedIndexChanged="ddlfilterstatus_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="3"></asp:ListItem>
                            <asp:ListItem Selected="True" Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:CheckBox ID="chkallbatch" runat="server" AutoPostBack="true" Text="Show batches only with a current effective date"
                            TextAlign="Right" OnCheckedChanged="chkallbatch_CheckedChanged" Checked="true"
                            Visible="false" />
                    </label>
                    <div style="float: right;">
                        <asp:Button ID="Button4" runat="server" Text="Printable Version" OnClick="Button4_Click"
                            CssClass="btnSubmit" />
                        <input id="Button2" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" class="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="lblCompany" Font-Size="20px" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="Label15" Font-Size="20px" runat="server" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblCompany0" Font-Size="20px" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                    <asp:Label ID="Label1" Font-Size="18px" runat="server" Text="List of Working Days"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td style="font-size: 16px; font-weight: normal;">
                                                    <asp:Label ID="Label13" runat="server" Text="Status :"></asp:Label>
                                                    <asp:Label ID="lblfilstatus" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="gridworkingday" runat="server" AllowSorting="True" Width="100%"
                                        AutoGenerateColumns="False" GridLines="Both" AllowPaging="true" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="ID"
                                        EmptyDataText="No Record Found." OnRowDeleting="gridworkingday_RowDeleting" OnRowCommand="gridworkingday_RowCommand"
                                        PageSize="20" OnRowEditing="gridworkingday_RowEditing" OnSorting="gridworkingday_Sorting"
                                        OnPageIndexChanging="gridworkingday_PageIndexChanging">
                                        <RowStyle />
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="WName" ItemStyle-Width="19%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWName" runat="server" Text='<%# Bind("WName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch Name" SortExpression="BName" ItemStyle-Width="8%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBName" runat="server" Text='<%# Bind("BName") %>'></asp:Label>
                                                    <asp:Label ID="lblBatchId" runat="server" Text='<%# Bind("BId") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Monday" SortExpression="Monday" ItemStyle-Width="5%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMonday1" runat="server" Checked='<%# Eval("Monday")%>' Visible="false" />
                                                    <asp:Label ID="lblMonday1" runat="server" Text='<%# Bind("d") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tuesday" SortExpression="Tuesday" ItemStyle-Width="5%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkTuesday1" runat="server" Checked='<%# Eval("Tuesday")%>' Visible="false" />
                                                    <asp:Label ID="lblTuesday1" runat="server" Text='<%# Bind("d1") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Wednesday" SortExpression="Wednesday" ItemStyle-Width="5%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkWednesday1" runat="server" Checked='<%# Eval("Wednesday")%>'
                                                        Visible="false" />
                                                    <asp:Label ID="lblWednesday1" runat="server" Text='<%# Bind("d2") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Thursday" SortExpression="Thursday" ItemStyle-Width="5%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkThursday1" runat="server" Checked='<%# Eval("Thursday")%>' Visible="false" />
                                                    <asp:Label ID="lblThursday1" runat="server" Text='<%# Bind("d3") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Friday" SortExpression="Friday" ItemStyle-Width="5%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chFriday1" runat="server" Checked='<%# Eval("Friday")%>' Visible="false" />
                                                    <asp:Label ID="lblFriday1" runat="server" Text='<%# Bind("d4") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Saturday" SortExpression="Saturday" ItemStyle-Width="5%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSaturday1" runat="server" Checked='<%# Eval("Saturday")%>' Visible="false" />
                                                    <asp:Label ID="lblsaturday1" runat="server" Text='<%# Bind("d5") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sunday" SortExpression="Sunday" ItemStyle-Width="5%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chksunday1" runat="server" Checked='<%# Eval("Sunday")%>' Visible="false" />
                                                    <asp:Label ID="lblsunday1" runat="server" Text='<%# Bind("d6") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Day of Week" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="6%" SortExpression="LastDayOftheWeek">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbllastday" runat="server" Text='<%# Bind("LastDayOftheWeek") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Button3" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="vi"
                                                        ImageUrl="~/Account/images/edit.gif" ToolTip="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:ButtonField CommandName="Edit" HeaderText="Editcccc" ShowHeader="True" Text="Edit"
                                                ButtonType="Image" ItemStyle-Width="3%" HeaderImageUrl="~/Account/images/edit.gif"
                                                ImageUrl="~/Account/images/edit.gif" />--%>
                                            <asp:ButtonField CommandName="Delete" HeaderText="Deleteccccc" ShowHeader="True"
                                                Text="Delete" ButtonType="Image" Visible="false" ItemStyle-Width="3%" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                ImageUrl="~/Account/images/delete.gif" />
                                        </Columns>
                                    </cc11:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
            <div style="clear: both;">
            </div>
            <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" Width="300px">
                <fieldset>
                    <legend>Confirmation </legend>
                    <table id="Table2" cellpadding="3" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Please note that you will not be able to change the working days, or the batch hours once you submit this schedule."></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label17" runat="server" Text="Would you like to proceed?"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="Button1" CssClass="btnSubmit" Text="Confirm" runat="server" OnClick="Button1_Click" />
                                &nbsp;<asp:Button ID="Button3" CssClass="btnSubmit" Text="Cancel" runat="server" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel3" TargetControlID="HiddenButton222" CancelControlID="Button3">
            </cc1:ModalPopupExtender>
            <!--end of right content-->
            <div style="clear: both;">
            </div>
            <asp:Panel ID="pnlconfirm" runat="server" CssClass="modalPopup" Width="450px">
                <fieldset>
                    <table cellpadding="0" cellspacing="0" width="100%" id="subinnertbl">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="lblconmsg" runat="server">
                                You cannot edit this batch as there are employees listed in this batch.
                                If you want to change this batch, please create a new batch. You can transfer all employees of this batch to the new batch you are creating.
                                    </asp:Label>
                                </label>
                                <div style="clear: both;">
                                </div>
                                <label>
                                    <asp:Label ID="Label16" runat="server" Text="Would you like to proceed with creating a new batch? "></asp:Label>
                                </label>
                                <%--<asp:CheckBox ID="CheckBox2" runat="server" Text="Yes" TextAlign="Right" />--%>
                                <div style="clear: both;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <asp:Button ID="ImageButton3" CssClass="btnSubmit" Text=" Yes " runat="server" OnClick="ImageButton3_Click" />
                                &nbsp;
                                <asp:Button ID="ImageButton5" CssClass="btnSubmit" Text=" Cancel " runat="server"
                                    OnClick="ImageButton5_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <asp:Button ID="hdbtn" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender145" runat="server" BackgroundCssClass="modalBackground"
                Enabled="True" PopupControlID="pnlconfirm" TargetControlID="hdbtn">
            </cc1:ModalPopupExtender>
            <div style="clear: both;">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
