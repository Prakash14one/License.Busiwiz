<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="AttendenceApproval.aspx.cs" Inherits="Add_Attendence_Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <%--  <legend>Add Employee Payroll</legend>--%>
                    <%-- <label>
                    <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="Single employee by pay period" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="All employees by date"></asp:ListItem>
                    </asp:RadioButtonList>
                </label>--%>
                    <label>
                        <asp:Label ID="statuslable" runat="server" ForeColor="#CC0000"></asp:Label>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="lblBusinessName" Text="Business Name" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlwarehouse" runat="server" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged"
                            Width="250px" AutoPostBack="True">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="lblBatchName" Text="Batch Name" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlbatchmaster" runat="server">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="lblDate" Text="Date" runat="server"></asp:Label>
                        <asp:RequiredFieldValidator ID="rddd" runat="server" ControlToValidate="txtdate"
                            ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rghjk" runat="server" ErrorMessage="*" ControlToValidate="txtdate"
                            ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtdate" runat="server" Width="75px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgbtncal"
                            TargetControlID="txtdate" Format="MM/dd/yyyy">
                        </cc1:CalendarExtender>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Button ID="btngo" runat="server" OnClick="btngo_Click" TabIndex="4" Text=" Go "
                        ValidationGroup="1" CssClass="btnSubmit" />
                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblAttendance" Text="List of Attendance Approval" runat="server"></asp:Label></legend>
                    <div style="float: right;">
                        <asp:Button ID="btncancel0" runat="server" CausesValidation="false" OnClick="btncancel0_Click"
                            Text="Printable Version" CssClass="btnSubmit" />
                        <input id="Button7" runat="server" visible="false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" class="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" ScrollBars="None" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" style="font-size: 20px; font-weight: bold; color: #000000; font-style: italic;">
                                                    <asp:Label ID="lblCompany" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="font-size: 20px; font-weight: bold; color: #000000; font-style: italic;">
                                                    <asp:Label ID="Label1" runat="server" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;
                                                    font-style: italic;">
                                                    <asp:Label ID="lblheadtext" runat="server" Text="List of Attendance Approval"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="text-align: left; font-size: 16px; font-style: italic;">
                                                    <asp:Label ID="lblbatch" runat="server"></asp:Label>
                                                    ,
                                                    <asp:Label ID="lblemp" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        CssClass="mGrid" DataKeyNames="AttendanceId" EmptyDataText="No Record Found."
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" OnSorting="GridView1_Sorting"
                                        AllowSorting="True" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        OnRowCommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblempname" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                                    <asp:Label ID="lblempid" runat="server" Text='<%# Bind("EmployeeId") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Height="30px" Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date","{0:MM/dd/yyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Height="30px" Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reg Intime" HeaderStyle-Font-Bold="true" SortExpression="InTime"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtreqintime" runat="server" Text='<%# Eval("InTime")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Height="30px" Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual Intime" HeaderStyle-Font-Bold="true" SortExpression="InTimeforcalculation"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtactintime" runat="server" Width="40px" Enabled="true" Text='<%# Eval("InTimeforcalculation")%>'>
                                                    </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtactintime"
                                                        ErrorMessage="Only HH:MM" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$"
                                                        ValidationGroup="2"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="ghttt1t" runat="server" ControlToValidate="txtactintime"
                                                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="2" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="txtaddress_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtactintime" ValidChars=":">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:Label ID="lblactintime" runat="server" Text='<%# Eval("InTimeforcalculation")%>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Height="30px" Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Reg Outime" SortExpression="OutTime"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtreqouttime" runat="server" Text='<%# Eval("OutTime")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Height="30px" Width="25px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Actual Outtime" SortExpression="OutTimeforcalculation"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtactouttime" runat="server" Width="40px" Enabled="true" Text='<%# Eval("OutTimeforcalculation")%>'>
                                                    </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularEessionValidator3" runat="server" ControlToValidate="txtactouttime"
                                                        ErrorMessage="Only HH:MM" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$"
                                                        ValidationGroup="2"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="gttt" runat="server" ControlToValidate="txtactouttime"
                                                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="2" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="txtaddressr_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtactouttime" ValidChars=":">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:Label ID="lblactouttime" runat="server" Text='<%# Eval("OutTimeforcalculation")%>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Height="30px" Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Batch Reg Hours" SortExpression="BatchRequiredhours"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtbatchreqhour" runat="server" Text='<%# Eval("BatchRequiredhours")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                 <ItemStyle Height="30px" Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Work Hour" SortExpression="Payabledays"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="txttotalwork" runat="server" Text='<%# Eval("TotalHourWork")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Height="30px" Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Pay Day" SortExpression="Payabledays"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Payabledays" runat="server" Text='<%# Eval("Payabledays")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Height="30px" Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Overtime" SortExpression="Overtime"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblovertime" runat="server" Text='<%# Eval("Overtime")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Height="30px" Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Approved By Supervisor/  Admin "
                                                SortExpression="sname" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsname" runat="server" Text='<%# Bind("sname") %>'></asp:Label>
                                                    <%--   <asp:Label ID="lblsupervisorid" runat="server" 
                                                Text='<%# Bind("SupervisorId") %>' Visible="false"></asp:Label>--%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Height="30px" Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Half Day Leave" SortExpression="ConsiderHalfDayLeave"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkhalfday" runat="server" Checked='<%# Bind("ConsiderHalfDayLeave") %>'>
                                                    </asp:CheckBox>
                                                    <asp:CheckBox ID="chkhalfdayap" runat="server" Visible="false" Checked='<%# Bind("ConsiderHalfDayLeave") %>'>
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Height="30px" Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Full Day Leave" SortExpression="ConsiderFullDayLeave"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkfullday" runat="server" Checked='<%# Bind("ConsiderFullDayLeave") %>' />
                                                    <asp:CheckBox ID="chkfulldayap" runat="server" Visible="false" Checked='<%# Bind("ConsiderFullDayLeave") %>'>
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Height="30px" Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Overtime Approved" SortExpression="Varify"
                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkoverapprove" runat="server" Checked='<%# Bind("Overtimeapprove") %>'
                                                        Width="5%" />
                                                    <asp:CheckBox ID="chkoverapproveap" runat="server" Visible="false" Checked='<%# Bind("Overtimeapprove") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Height="30px" Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText=" Attendance Approved"
                                                SortExpression="Overtimeapprove" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chlapproved" runat="server" Checked='<%# Bind("Varify") %>' />
                                                    <asp:CheckBox ID="chlapprovedap" runat="server" Visible="false" Checked='<%# Bind("Varify") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Height="30px" Width="30px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </cc11:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" ValidationGroup="2" OnClick="btnsubmit_Click"
                        Visible="False" CssClass="btnSubmit" />
                </fieldset>
            </div>
            <!--end of right content-->
            <div style="clear: both;">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlwarehouse" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="Sorting" />
            <asp:AsyncPostBackTrigger ControlID="btngo" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnsubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
