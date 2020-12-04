<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="BatchTimingManage.aspx.cs" Inherits="Add_Batch_Timing"
    Title="Batch Timing Add/Manage" %>

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
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
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
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" />
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False" />
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" Text="Add New Batch Time" runat="server" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <asp:Panel ID="pnladd" Visible="false" runat="server">
                        <table width="100%">
                            <tr>
                                <td width="25%">
                                    <label>
                                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                        <asp:Label ID="lblBusinessNamedd" Text="Business Name" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td width="25%">
                                    <label>
                                        <asp:DropDownList ID="ddlstorename" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstorename_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td width="25%">
                                </td>
                                <td width="25%">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblBatchNamedd" Text="Batch Name" runat="server"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:DropDownList ID="ddlbatchmaster" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlbatchmaster_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgadd" runat="server" Height="20px" ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                            OnClick="imgadd_Click" ToolTip="Add New" Width="20px" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgref" runat="server" Height="20px" ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                            OnClick="imgref_Click" ToolTip="Refresh" Width="20px" />
                                    </label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblTimeSchedule" Text="Time Schedule" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddltimeschedule" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblStartTime" Text="Batch Start Time" runat="server"></asp:Label>
                                        <asp:Label ID="Label12" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtstarttime"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtstarttime"
                                            ErrorMessage="*" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtstarttime" runat="server" Width="80px" MaxLength="5"></asp:TextBox>
                                    </label>
                                    <label>
                                        <span class="lblInfoMsg">(24Hour(HH:MM))</span>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lblEndTime" Text="Batch End Time" runat="server"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtendtime"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtendtime"
                                            ErrorMessage="*" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToCompare="txtstarttime"
                                            ControlToValidate="txtendtime" ErrorMessage="*" Operator="GreaterThan"></asp:CompareValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtendtime" runat="server" Width="80px" MaxLength="5"></asp:TextBox>
                                    </label>
                                    <label>
                                        <span class="lblInfoMsg">(24Hour(HH:MM))</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblFirstBreakStartTime" Text="First Break Start Time" runat="server"></asp:Label>
                                        <%-- <asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtbrakestart"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtbrakestart"
                                            ErrorMessage="*" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtstarttime"
                                            ControlToValidate="txtbrakestart" ErrorMessage="*" Operator="GreaterThanEqual"
                                            ValidationGroup="1"></asp:CompareValidator>
                                        <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToCompare="txtendtime"
                                            ControlToValidate="txtbrakestart" ErrorMessage="*" Operator="LessThan" ValidationGroup="1"></asp:CompareValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtbrakestart" runat="server" Width="80px" MaxLength="5"></asp:TextBox>
                                    </label>
                                    <label>
                                        <span class="lblInfoMsg">(24Hour(HH:MM))</span>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lblFirstBreakEnd" Text="First Break End Time" runat="server"></asp:Label>
                                        <%--<asp:Label ID="Label7" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtbrakeend"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtbrakeend"
                                            ErrorMessage="*" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtbrakestart"
                                            ControlToValidate="txtbrakeend" ErrorMessage="*" Operator="GreaterThanEqual"
                                            ValidationGroup="1"></asp:CompareValidator>
                                        <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToCompare="txtendtime"
                                            ControlToValidate="txtbrakeend" ErrorMessage="*" Operator="LessThan" ValidationGroup="1"></asp:CompareValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtbrakeend" runat="server" Width="80px" MaxLength="5"></asp:TextBox>
                                    </label>
                                    <label>
                                        <span class="lblInfoMsg">(24Hour(HH:MM))</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblSecondBreakStart" Text="Second Break Start Time" runat="server"></asp:Label>
                                        <%-- <asp:Label ID="Label8" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtsecondbrakestart"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtsecondbrakestart"
                                            ErrorMessage="*" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="txtbrakeend"
                                            ControlToValidate="txtsecondbrakestart" ErrorMessage="*" Operator="GreaterThanEqual"
                                            ValidationGroup="1"></asp:CompareValidator>
                                        <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToCompare="txtendtime"
                                            ControlToValidate="txtsecondbrakestart" ErrorMessage="*" Operator="LessThan"
                                            ValidationGroup="1"></asp:CompareValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtsecondbrakestart" runat="server" Width="80px" MaxLength="5"></asp:TextBox>
                                    </label>
                                    <label>
                                        <span class="lblInfoMsg">(24Hour(HH:MM))</span>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lblSecondBreakEnd" Text="Second Break End Time" runat="server"></asp:Label>
                                        <%--<asp:Label ID="Label9" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtsecondbrakeend"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtsecondbrakeend"
                                            ErrorMessage="*" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToCompare="txtsecondbrakestart"
                                            ControlToValidate="txtsecondbrakeend" ErrorMessage="*" Operator="GreaterThanEqual"
                                            ValidationGroup="1"></asp:CompareValidator>
                                        <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToCompare="txtendtime"
                                            ControlToValidate="txtsecondbrakeend" ErrorMessage="*" Operator="LessThan" ValidationGroup="1"></asp:CompareValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtsecondbrakeend" runat="server" Width="80px" MaxLength="5"></asp:TextBox>
                                    </label>
                                    <label>
                                        <span class="lblInfoMsg">(24Hour(HH:MM))</span>
                                    </label>
                                </td>
                            </tr>
                            <%--<tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblEStartDate" Text="Effective Start Date" runat="server"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txteffectstart"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txteffectstart" runat="server" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txteffectstart"
                                            TargetControlID="txteffectstart">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txteffectstart">
                                        </cc1:MaskedEditExtender>
                                    </label>
                                    <label>
                                        <span class="lblInfoMsg">(MM/DD/YYYY)</span>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lblEEndDate" Text="Effective End Date" runat="server"></asp:Label>
                                        <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txteffectend"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToCompare="txteffectstart"
                                            ControlToValidate="txteffectend" ErrorMessage="*" Operator="GreaterThan" Type="Date"
                                            ValidationGroup="1"></asp:CompareValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txteffectend" runat="server" Width="80px" ></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txteffectend"
                                            TargetControlID="txteffectend">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txteffectend">
                                        </cc1:MaskedEditExtender>
                                    </label>
                                    <label>
                                        <span class="lblInfoMsg">(MM/DD/YYYY)</span>
                                    </label>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlstatus" runat="server" Width="100px">
                                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:CheckBox ID="chkactive" runat="server" Text="Active"  />              --%>
                                    </label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Set up working days for this new batch (Recommended)."
                                        TextAlign="Right" Checked="true" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Button ID="ImageButton48" Text="Submit" runat="server" OnClick="ImageButton48_Click"
                                        ValidationGroup="1" CssClass="btnSubmit" />
                                    <asp:Button ID="ImageButton49" Text="Update" runat="server" OnClick="ImageButton49_Click"
                                        CssClass="btnSubmit" ValidationGroup="1" />
                                </td>
                                <td colspan="2">
                                    <asp:Button ID="ImageButton4" Text="Cancel" runat="server" OnClick="ImageButton4_Click"
                                        CssClass="btnSubmit" />
                                </td>
                            </tr>
                        </table>
                        <div style="clear: both;">
                        </div>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblBatchTimingList" Text="List of Batch Timings" runat="server"></asp:Label></legend>
                    <label>
                        <asp:Label ID="lblselectbusiness" Text="Filter by Business Name" runat="server"></asp:Label>
                        <asp:DropDownList ID="DropDownList3" runat="server" Width="145px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label4" Text="Filter by Status" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlfilterstatus" runat="server" Width="100px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlfilterstatus_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="3"></asp:ListItem>
                            <asp:ListItem Selected="True" Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:CheckBox ID="chkallbatch" runat="server" AutoPostBack="true" Text="Show batches only with a current effective date"
                            TextAlign="Right" OnCheckedChanged="chkallbatch_CheckedChanged" Checked="true" />
                    </label>
                    <div style="float: right;">
                        <asp:Button ID="Button2" runat="server" Text="Printable Version" OnClick="Button2_Click"
                            CssClass="btnSubmit" />
                        <input id="Button1" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
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
                                                <td style="font-size: 20px;">
                                                    <asp:Label ID="lblCompany" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td style="font-size: 18px;">
                                                    <asp:Label ID="Label10" runat="server" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td style="font-size: 18px;">
                                                    <asp:Label ID="Label3" runat="server" Text="List of Batch Timings"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td style="font-size: 16px; font-weight: normal;">
                                                    <asp:Label ID="Label5" runat="server" Text="Status :"></asp:Label>
                                                    <asp:Label ID="lblfilstatus" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="grdbatchtiming" runat="server" AllowSorting="True" DataKeyNames="ID"
                                        EmptyDataText="No Record Found." 
                                        OnRowDeleting="grdbatchtiming_RowDeleting" OnRowCommand="grdbatchtiming_RowCommand"
                                        OnSorting="grdbatchtiming_Sorting" OnRowEditing="grdbatchtiming_RowEditing1"
                                        AllowPaging="True" PageSize="20" AutoGenerateColumns="False" GridLines="Both"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        OnPageIndexChanging="grdbatchtiming_PageIndexChanging" Width="100%">
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                                            <asp:TemplateField HeaderText="Business" SortExpression="WName" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWName" runat="server" Text='<%# Bind("WName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch Name" SortExpression="BName" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBName" runat="server" Text='<%# Bind("BName") %>'></asp:Label>
                                                    <asp:Label ID="lblBatchId" runat="server" Text='<%# Bind("BId") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Time Schedule" SortExpression="TimeScheduleMasterId"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSName1" runat="server" Text='<%# Bind("SName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Time" SortExpression="StartTime" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartTime1" runat="server" Text='<%# Bind("StartTime") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Time" SortExpression="EndTime" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEndTime1" runat="server" Text='<%# Bind("EndTime") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="First Break Start Time" SortExpression="FirstBreakStart"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFirstBatchStartTime1" runat="server" Text='<%# Bind("FirstBreakStart") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="First Break End Time" SortExpression="FirstBreakEndTime"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFirstBatchEndTime1" runat="server" Text='<%# Bind("FirstBreakEndTime") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Second Break Start Time" SortExpression="SecondBreakStartTime"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="11%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSecondBatchStartTime1" runat="server" Text='<%# Bind("SecondBreakStartTime") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Second Break End Time" SortExpression="SecondBreakEndTime"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="11%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSecondBatchEndTime1" runat="server" Text='<%# Bind("SecondBreakEndTime") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Working Hours" SortExpression="total" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltot" runat="server" Text='<%# Bind("total") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="status" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="chkactive" runat="server" Text='<%# Eval("status")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnedit" runat="server" CommandName="Edit" ImageUrl="~/Account/images/edit.gif"
                                                        ToolTip="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:ButtonField CommandName="Edit" HeaderText="Edit" ShowHeader="True" Text="Edit"
                                                ButtonType="Image" HeaderImageUrl="~/Account/images/edit.gif" ImageUrl="~/Account/images/edit.gif" />--%>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" CommandArgument='<%#Eval("ID") %>'
                                                        ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                        ToolTip="Delete"></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="2%" />
                                            </asp:TemplateField>
                                            <%--<asp:ButtonField CommandName="Delete" HeaderText="Delete" ShowHeader="True" Text="Delete" ButtonType="Image" ImageUrl="~/Account/images/delete.gif"  HeaderImageUrl="~/ShoppingCart/images/trash.jpg" />--%>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </cc11:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <!--end of right content-->
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnlconfirm" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA"
                    BorderStyle="Outset" Width="450px">
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
                                    <asp:Label ID="Label6" runat="server" Text="Would you like to proceed with creating a new batch? "></asp:Label>
                                </label>
                                <asp:CheckBox ID="CheckBox2" runat="server" Text="Yes" TextAlign="Right" />
                                <div style="clear: both;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <asp:Button ID="ImageButton3" CssClass="btnSubmit" Text=" Yes " runat="server" OnClick="ImageButton3_Click" />
                                &nbsp;
                                <asp:Button ID="ImageButton5" CssClass="btnSubmit" Text=" No " runat="server" OnClick="ImageButton5_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="hdbtn" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender145" runat="server" BackgroundCssClass="modalBackground"
                    Enabled="True" PopupControlID="pnlconfirm" TargetControlID="hdbtn">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel1" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA" BorderStyle="Outset"
                    Width="450px">
                    <table cellpadding="0" cellspacing="0" width="100%" id="Table1">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label7" runat="server">
                                You cannot delete this batch as there are employees listed in this batch.
                                If you want to change this batch, please create a new batch. You can transfer all employees of this batch to the new batch you are creating.
                                    </asp:Label>
                                </label>
                                <div style="clear: both;">
                                </div>
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="Would you like to proceed with creating a new batch? "></asp:Label>
                                </label>
                                <asp:CheckBox ID="CheckBox3" runat="server" Text="Yes" TextAlign="Right" />
                                <div style="clear: both;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <asp:Button ID="Button3" CssClass="btnSubmit" Text=" Yes " runat="server" OnClick="Button3_Click" />
                                &nbsp;
                                <asp:Button ID="Button4" CssClass="btnSubmit" Text=" No " runat="server" OnClick="Button4_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button5" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                    Enabled="True" PopupControlID="Panel1" TargetControlID="Button5">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
