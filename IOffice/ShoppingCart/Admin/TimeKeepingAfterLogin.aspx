<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="TimeKeepingAfterLogin.aspx.cs" Inherits="Add_Employee_Payroll_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="products_box">
        <div style="padding-left: 1%">
            <asp:Label ID="lblmsg" runat="server" Visible="true" ForeColor="Red"></asp:Label>
            <asp:Label ID="lblentry" runat="server" Visible="true" ForeColor="Red"></asp:Label>
        </div>
        <asp:Panel ID="pnlsetupwizardlabel" runat="server" Width="100%" Visible="False">
            <fieldset>
                <label>
                    <asp:Label ID="lblsetupwizardlabel" runat="server" Text="You cannot record your attendance because part of your batch information is missing. Please consult your supervisor to have your batch details completed."></asp:Label>
                </label>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnlintime" runat="server" Width="100%" Visible="False">
            <fieldset>
                <label>
                    <asp:Label ID="Label1date" Visible="false" runat="server"></asp:Label>
                    <asp:Label ID="time22" Visible="false" runat="server"></asp:Label>
                </label>
                <label>
                    <asp:Label ID="lblyou" runat="server" Text="You are login for the first time for the day. Please click here to mark your attendance"></asp:Label>
                </label>
                <div style="float: right;">
                    <asp:Button ID="btnin" runat="server" Text="Attendance - IN" OnClick="btnin_Click" />
                </div>
            </fieldset>
        </asp:Panel>
        <div style="clear: both;">
        </div>
        <asp:Panel ID="Pnlcnfout" runat="server" Width="100%" Visible="False">
            <fieldset>
                <label>
                    <asp:Label ID="lbltimelist" Text="Your in time today is" runat="server"></asp:Label>
                </label>
                <label>
                    <asp:Label ID="lbltime" runat="server"></asp:Label>
                </label>
                <label>
                    <asp:Label ID="lblrurunnin" Text="Are you leaving the office for the day?" runat="server"></asp:Label>
                </label>
                <div style="float: right;">
                    <asp:Button ID="btnout" runat="server" Text="Attendance - OUT" OnClick="btnout_Click" />
                </div>
            </fieldset>
        </asp:Panel>
        <div style="clear: both;">
        </div>
        <asp:Panel ID="Pnlout" runat="server" Width="100%" Visible="False">
            <fieldset>
                <label>
                    <asp:Label ID="lblAttendancereport" Text="Your Attendance for the day" runat="server"></asp:Label>
                </label>
                <label>
                    <asp:Label ID="lbldate" Font-Bold="true" runat="server"></asp:Label>
                </label>
                <label>
                    <asp:Label ID="lblisintime" Text="is IN Time" runat="server"></asp:Label>
                    <asp:Label ID="lblintime" Font-Bold="true" runat="server"></asp:Label>
                </label>
                <label>
                    <asp:Label ID="lblouttimelbl" Text="Out Time" runat="server"></asp:Label>
                    <asp:Label ID="lblout" Font-Bold="true" runat="server"></asp:Label>
                </label>
            </fieldset>
        </asp:Panel>
        <div style="clear: both;">
        </div>
        <div style="clear: both;">
        </div>
        <div>
            <table id="subinnertbl" width="100%">
                <tr>
                    <td style="width: 50%; background-color: #416271;" align="center">
                        <asp:Label ID="Label1" runat="server" Text="Today's Presence" Font-Size="16px" ForeColor="White"></asp:Label>
                    </td>
                    <td style="width: 50%; background-color: #416271;" align="center">
                        <asp:Label ID="Label12" runat="server" Text="Reminder Note" Font-Size="16px" ForeColor="White"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid #000000; width: 50%">
                        <asp:Panel ID="Panel1" runat="server" Height="360" Width="100%">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="height: 28px">
                                                    <asp:Label ID="lbltc" runat="server" Text="Status"></asp:Label>
                                                </td>
                                                <td style="height: 28px">
                                                    <asp:DropDownList ID="ddlpresentstatus" Width="150px" runat="server">
                                                        <asp:ListItem Value="0">All</asp:ListItem>
                                                        <asp:ListItem Value="1">Absent</asp:ListItem>
                                                        <asp:ListItem Value="2">Present</asp:ListItem>
                                                        <asp:ListItem Value="3">Late Entry In Time</asp:ListItem>
                                                        <asp:ListItem Value="4">Early Entry In Time</asp:ListItem>
                                                        <asp:ListItem Value="5">Late Departure Out Time</asp:ListItem>
                                                        <asp:ListItem Value="6">Early Departure Out Time</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="height: 28px">
                                                    <asp:Label ID="hjgf" runat="server" Text="Batch"></asp:Label>
                                                </td>
                                                <td style="height: 28px">
                                                    <asp:DropDownList ID="ddlbatch" Width="150px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl" runat="server" Text="Current Date "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblcdate" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPeriod" runat="server" Text="Period "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtfrdate" runat="server" Width="65px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtfrdate">
                                                    </cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldVaator1" runat="server" ControlToValidate="txtfrdate"
                                                        ValidationGroup="10" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfrdate" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtfedate" runat="server" Width="65px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtfedate">
                                                    </cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequidFieldValidator2" runat="server" ControlToValidate="txtfedate"
                                                        ValidationGroup="10" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureName="en-AU"
                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfedate" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btngo" runat="server" Text=" Go " ValidationGroup="10" OnClick="btngo_Click"
                                                        CssClass="btnSubmit" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <asp:GridView ID="gridattendance" runat="server" AutoGenerateColumns="False" DataKeyNames="DateId"
                                            Width="100%" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AllowPaging="true"
                                            PageSize="6" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found."
                                            OnPageIndexChanging="gridattendance_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName" ItemStyle-Width="20%"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemployeename123" runat="server" Text='<%# Eval("EmployeeName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="7%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinstructiondate123" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="In Time" ItemStyle-Width="7%" SortExpression="InTimeforcalculation"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblintimeforcalculation" runat="server" Text='<%# Eval("InTimeforcalculation")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Out Time" ItemStyle-Width="7%" SortExpression="OutTimeforcalculation"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblouttimecalculate" runat="server" Text='<%# Eval("OutTimeforcalculation")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" SortExpression="Status" ItemStyle-Width="3%"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" align="right">
                                        <asp:LinkButton ID="taskmore" Text="More.." runat="server" OnClick="taskmore_Click"
                                            CssClass="btnSubmit" ForeColor="Black"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td style="border: 1px solid #000000; width: 50%">
                        <asp:Panel ID="Panel2" runat="server" Height="360px" Width="100%">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td align="right" style="text-align: center;" colspan="3">
                                                    <asp:Label ID="lblmes" runat="server" Style="text-align: center; color: #CC0000;"
                                                        Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 15%">
                                                    Status :
                                                </td>
                                                <td align="left" style="width: 20%">
                                                    <asp:DropDownList ID="ddlfilterstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterstatus_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="width: 15%">
                                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Save" CssClass="btnSubmit" />
                                                    <%--<asp:Button ID="Button2" runat="server" Text="Add new" />--%>&nbsp;&nbsp;
                                                    <asp:ImageButton ID="ImageButton6" runat="server" AlternateText="Add New" Height="20px"
                                                        ImageAlign="Middle" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" ToolTip="AddNew"
                                                        Width="20px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="width: 50%">
                                                    <asp:Panel ID="Panel14" runat="server" Height="100%" Width="100%">
                                                        <asp:GridView ID="grdreminder" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                            Width="100%" EmptyDataText="No Record Found." AllowPaging="true" PageSize="6"
                                                            OnPageIndexChanging="grdreminder_PageIndexChanging">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblremindermasterid" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblreminderdate" runat="server" Text='<%#Bind("Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Reminder Note" ItemStyle-Width="40%" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblremindernote" runat="server" Text='<%#Bind("ReminderNote") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Completed" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <%-- <PagerStyle CssClass="GridPager" />
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                                                        <FooterStyle CssClass="GridFooter" />
                                                        <RowStyle CssClass="GridRowStyle" />--%>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                            <td></td> 
                                            <td></td>
                                            <td align="right"> 
                                                    &nbsp;</td>                      
                                       </tr>--%>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" align="right">
                                        <asp:LinkButton ID="remindermore" Text="More.." runat="server" OnClick="remindermore_Click"
                                            CssClass="btnSubmit" Visible="False"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table id="Table1" width="100%">
                <tr>
                    <td style="width: 50%; background-color: #416271;" align="center">
                        <asp:Label ID="lblle" runat="server" Text="List of Leave Request" Font-Size="16px"
                            ForeColor="White"></asp:Label>
                    </td>
                    <td style="width: 50%; background-color: #416271;" align="center">
                        <asp:Label ID="Label3" runat="server" Text="List of Gate Pass Approval" Font-Size="16px"
                            ForeColor="White"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid #000000; width: 50%">
                        <asp:Panel ID="Panel3" runat="server" Height="300px" Width="100%">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:GridView ID="GridView1" runat="server" EmptyDataText="No Record Found." AutoGenerateColumns="False"
                                            Width="100%" DataKeyNames="id" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                            AlternatingRowStyle-CssClass="alt" AllowPaging="true" PageSize="6" OnPageIndexChanging="GridView1_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("empname")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Type" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblleaveid" runat="server" Text='<%# Bind("EmployeeLeaveTypeName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From Date" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFrmDate" runat="server" Text='<%# Bind("fromdate","{0:MM/dd/yyyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Date" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblToDate" runat="server" Text='<%# Bind("Todate","{0:MM/dd/yyyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approval Status" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblapprovestatus" runat="server" Text='<%# Bind("Status")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" align="right">
                                        <asp:LinkButton ID="LinkButton1" Text="Add Leave Request.." ForeColor="Black" runat="server"
                                            OnClick="LinkButton1_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td style="border: 1px solid #000000; width: 50%">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <asp:Panel ID="Panel4" runat="server" Height="300px" Width="100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <%-- <asp:Panel ID="Panel6" runat="server" ScrollBars="Vertical" Height="100%" Width="100%">--%>
                                                    <asp:GridView ID="grdgatepass" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                        Width="100%" EmptyDataText="No Record Found." GridLines="None" CssClass="mGrid"
                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true"
                                                        PageSize="6" OnPageIndexChanging="grdgatepass_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="40%" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblemp" runat="server" Text='<%#Bind("EmployeeName") %>' Visible="true"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Out Time" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbloutt" runat="server" Text='<%#Bind("ExpectedOutTime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="In Time" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblintime" runat="server" Text='<%#Bind("ExpectedInTime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approved" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsapp" runat="server" Text='<%#Bind("Approved") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <%-- <PagerStyle CssClass="GridPager" />
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                                                        <FooterStyle CssClass="GridFooter" />
                                                        <RowStyle CssClass="GridRowStyle" />--%>
                                                    </asp:GridView>
                                                    <%-- </asp:Panel>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" align="right">
                                                    <asp:LinkButton ID="LinkButton2" Text="More.." ForeColor="Black" runat="server" OnClick="LinkButton2_Click"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table id="Table2" width="100%">
                <tr>
                    <td style="width: 50%; background-color: #416271;" align="center">
                        <asp:Label ID="Label4" runat="server" Text="Internal Message Center" Font-Size="16px"
                            ForeColor="White"></asp:Label>
                        &nbsp;
                    </td>
                    <td style="width: 50%; background-color: #416271;" align="center">
                        <asp:Label ID="Label5" runat="server" Text="External Message Center" Font-Size="16px"
                            ForeColor="White"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid #000000; width: 50%">
                        <asp:Panel ID="Panel5" runat="server" Height="470px" Width="100%">
                            <asp:Label ID="lblmsg2" runat="server" ForeColor="Red"></asp:Label>
                            <div style="clear: both;">
                            </div>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label6" runat="server" Text="Select"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:DropDownList ID="ddlinternal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlinternal_SelectedIndexChanged">
                                                <asp:ListItem Selected="true" Text="Inbox" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Compose" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Sent" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Deleted" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Drafts" Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td align="right" valign="bottom">
                                        <asp:Panel ID="panelsent1" runat="server" Visible="false">
                                            <table>
                                                <tr>
                                                    <td style="text-align: right; width: 50%">
                                                        <asp:LinkButton ID="LinkButton3" Text="More.." runat="server" CssClass="btnSubmit"
                                                            OnClick="LinkButton3_Click" ForeColor="Black"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="panelinbox11" runat="server">
                                            <table>
                                                <tr>
                                                    <td style="text-align: right; width: 50%">
                                                        <asp:LinkButton ID="LinkButton4" Text="More.." runat="server" CssClass="btnSubmit"
                                                            OnClick="LinkButton4_Click" ForeColor="Black"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="paneldelete1" runat="server" Visible="false">
                                            <table>
                                                <tr>
                                                    <td style="text-align: right; width: 50%">
                                                        <asp:LinkButton ID="LinkButton5" Text="More.." runat="server" CssClass="btnSubmit"
                                                            OnClick="LinkButton5_Click" ForeColor="Black"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="paneldraft1" runat="server" Visible="false">
                                            <table>
                                                <tr>
                                                    <td style="text-align: right; width: 50%">
                                                        <asp:LinkButton ID="LinkButton6" Text="More.." runat="server" CssClass="btnSubmit"
                                                            OnClick="LinkButton6_Click" ForeColor="Black"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <div style="clear: both;">
                            </div>
                            <asp:Panel ID="panelinbox" runat="server" Height="460px">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView3" runat="server" GridLines="None" AllowPaging="True" CssClass="mGrid"
                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="MsgDetailId"
                                                EmptyDataText="There is no Message." AllowSorting="True" Width="100%" AutoGenerateColumns="False"
                                                EnableModelValidation="True" OnPageIndexChanging="GridView3_PageIndexChanging"
                                                PageSize="5" OnSorting="GridView3_Sorting1" OnRowDataBound="GridView3_RowDataBound">
                                                <Columns>
                                                    <%--<asp:BoundField DataField="MsgDate" HeaderStyle-HorizontalAlign="Left" HeaderText="Date"
                                                                    HeaderStyle-Width="10%" SortExpression="MsgDate" DataFormatString="{0:MM/dd/yyyy}" />--%>
                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" SortExpression="MsgDate"
                                                        HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#  Eval("MsgDate", "{0:MM/dd/yyyy}")%>'></asp:Label>
                                                            <asp:Label ID="Label5" runat="server" Visible="false" Text='<%#  Eval("MsgDate", "{0:HH:mm:ss}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From" SortExpression="MsgDetailId" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <a href="MessageView.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>&Status=<%# Eval("MsgStatusId")%>">
                                                                <font color="gray">
                                                                    <%#  Eval("Compname")%></font> </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" SortExpression="MsgSubject"
                                                        HeaderStyle-Width="50%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsubbbject" runat="server" Text='<%#Eval("MsgSubject")%>' ToolTip='<%#Eval("MsgDetail")%>'></asp:Label>
                                                            <%--<a href="MessageView.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>&Status=<%# Eval("MsgStatusId")%>"
                                                                                visible="false"><font color="gray">
                                                                                    <%#  Eval("MsgSubject")%></font></a>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="60%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png" />
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="MsgStatusName" HeaderStyle-HorizontalAlign="Left" HeaderText="Status"
                                                        SortExpression="MsgStatusName" HeaderStyle-Width="10%" Visible="false"></asp:BoundField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="panelcompose" runat="server" Height="460px" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel runat="server" ID="Panel26">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 8%">
                                                            <label>
                                                                <asp:Label ID="Label7" Text="Business" runat="server"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td style="width: 42%">
                                                            <label>
                                                                <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 8%">
                                                            <label>
                                                                <asp:Label ID="Label43" Text="To" runat="server"></asp:Label>
                                                                <asp:Label ID="Label44" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox6"
                                                                    ErrorMessage="*" ValidationGroup="9" Width="13px"></asp:RequiredFieldValidator>
                                                                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Account/images/addbook.png"
                                                                    ToolTip="Click here to add Addresses" />
                                                            </label>
                                                        </td>
                                                        <td style="width: 42%">
                                                            <label>
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="TextBox6" runat="server" Width="300px" ReadOnly="True"></asp:TextBox>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 8%" valign="top">
                                                            <label>
                                                                <asp:Label ID="Label45" Text="Subject" runat="server"></asp:Label>
                                                                <br />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Character"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([._:a-zA-Z0-9\s]*)"
                                                                    ControlToValidate="TextBox7" ValidationGroup="9"></asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td style="width: 42%">
                                                            <label>
                                                                <asp:TextBox ID="TextBox7" runat="server" Width="300px" MaxLength="200" onKeydown="return mask(event)"
                                                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\_:a-zA-Z.0-9\s]+$/,'Span4',200)"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label runat="server" ID="Label46" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="Span4" cssclass="labelcount">200</span>
                                                                <asp:Label ID="Label47" runat="server" Text="(A-Z 0-9 _ . :)" CssClass="labelcount"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 8%" valign="top">
                                                            <label>
                                                                <asp:Label ID="Label48" runat="server" Text="Message"></asp:Label>
                                                                <br />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Character"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)"
                                                                    ControlToValidate="TextBox8" ValidationGroup="9"></asp:RegularExpressionValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="*"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1000})$"
                                                                    ControlToValidate="TextBox8" ValidationGroup="9"></asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td style="width: 42%">
                                                            <label>
                                                                <asp:TextBox ID="TextBox8" runat="server" TextMode="MultiLine" Width="300px" Height="60px"
                                                                    onkeypress="return checktextboxmaxlength(this,1000,event)" onKeydown="return mask(event)"
                                                                    onkeyup="return check(this,/[\\/!#$%^'&*>:;={}[]|\/]/g,/^[\a-z().@+A-Z,0-9_-\s]+$/,'Span5',1000)"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label runat="server" ID="Label49" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="Span5" cssclass="labelcount">1000</span>
                                                                <asp:Label ID="Label50" runat="server" Text="(A-Z 0-9 , . @ ) ( - +)" CssClass="labelcount"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 8%" valign="top">
                                                            <label>
                                                                <asp:Label ID="Label14" runat="server" Text="Attachment"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td style="width: 42%">
                                                            <label>
                                                                <asp:FileUpload ID="fileuploadadattachment" runat="server" />
                                                            </label>
                                                            <label>
                                                                <asp:Button ID="imgbtnattach" runat="server" Text="Attach" OnClick="imgbtnattach_Click"
                                                                    ValidationGroup="2" CssClass="btnSubmit" />
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 8%" valign="top">
                                                        </td>
                                                        <td style="width: 42%">
                                                            <asp:Panel runat="server" Visible="false" ID="PnlFileAttachLbl" ScrollBars="Vertical"
                                                                Height="90px">
                                                                <asp:GridView ID="gridFileAttach" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                                    AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                    DataKeyNames="FileNameChanged" OnRowCommand="gridFileAttach_RowCommand" PageSize="5"
                                                                    OnPageIndexChanging="gridFileAttach_PageIndexChanging" Width="100%">
                                                                    <Columns>
                                                                        <asp:ButtonField CommandName="Remove" ImageUrl="~/Account/images/delete.gif" HeaderImageUrl="~/Account/images/delete.gif"
                                                                            ItemStyle-Width="5%" HeaderText="Remove" ButtonType="Image"></asp:ButtonField>
                                                                        <asp:BoundField DataField="FileName" HeaderText="Attached File Name" HeaderStyle-HorizontalAlign="Left"
                                                                            ItemStyle-HorizontalAlign="Left" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 8%">
                                                        </td>
                                                        <td style="width: 42%">
                                                            <asp:CheckBox ID="CheckBox4" runat="server" Text="Include Signature" />
                                                            <asp:CheckBox ID="CheckBox5" runat="server" Text="Include Picture" />
                                                            <%--<asp:CheckBox ID="chkattachment" runat="server" Text="Add Attachment" AutoPostBack="true"
                                                            OnCheckedChanged="chkattachment_CheckedChanged" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 9%">
                                                        </td>
                                                        <td style="width: 41%">
                                                            <asp:Button ID="Button15" runat="server" Text="Send" ValidationGroup="9" CssClass="btnSubmit"
                                                                OnClick="Button15_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input id="Hidden3" runat="server" name="hdnFileName" style="width: 1px" type="hidden" />
                                            <asp:Literal ID="CheckBoxIDsArray" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <input id="hdnFileName" runat="server" name="hdnFileName" style="width: 1px" type="hidden" />
                                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="panelsent" runat="server" Height="460px" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdSentMailList" runat="server" DataKeyNames="MsgId" AutoGenerateColumns="False"
                                                OnRowDataBound="grdSentMailList_RowDataBound" GridLines="None" AllowPaging="True"
                                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                PageSize="5" OnPageIndexChanging="grdSentMailList_PageIndexChanging" EmptyDataText="There is no Message."
                                                AllowSorting="True" OnSorting="grdSentMailList_Sorting" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        SortExpression="MsgId" ItemStyle-ForeColor="Black" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label21z" runat="server" Text='<%#  Eval("MsgDate", "{0:MM/dd/yyyy}")%>'></asp:Label>
                                                            <asp:Label ID="Label51z" runat="server" Visible="false" Text='<%#  Eval("MsgDate", "{0:HH:mm:ss}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sent To" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="17%">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSentTo" Text='<%# Eval("From1")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderText="Subject" SortExpression="MsgSubject" ItemStyle-ForeColor="Black"
                                                        ItemStyle-Font-Bold="true" HeaderStyle-Width="55%">
                                                        <ItemTemplate>
                                                            <a href='MessageViewSent.aspx?MsgId=<%# Eval("MsgId")%>' style="color: Black">
                                                                <%#  Eval("MsgSubject")%>
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png" />
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="paneldelete" runat="server" Height="460px" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gridDelete" runat="server" AutoGenerateColumns="False" DataKeyNames="MsgDetailId"
                                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                PageSize="5" OnRowDataBound="gridDelete_RowDataBound" AllowPaging="True" OnPageIndexChanging="gridDelete_PageIndexChanging"
                                                EmptyDataText="No Record Found." Width="100%" AllowSorting="True" OnSorting="gridDelete_Sorting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        SortExpression="MsgDate" ItemStyle-ForeColor="Black" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label21x" runat="server" Text='<%#  Eval("MsgDate", "{0:MM/dd/yyyy}")%>'></asp:Label>
                                                            <asp:Label ID="Label51x" runat="server" Visible="false" Text='<%#  Eval("MsgDate", "{0:HH:mm:ss}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="25%">
                                                        <ItemTemplate>
                                                            <a href="MessageView.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>"><b><font color="black">
                                                                <%#  Eval("Compname")%></b></font></a>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="55%">
                                                        <ItemTemplate>
                                                            <a href="MessageView.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>"><b><font color="black">
                                                                <%#  Eval("MsgSubject")%></b></font></a>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png" />
                                                        </HeaderTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="paneldrafts" runat="server" Height="460px" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gridDraft" runat="server" DataKeyNames="MsgId" GridLines="None"
                                                AllowPaging="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                AutoGenerateColumns="False" OnPageIndexChanging="gridDraft_PageIndexChanging"
                                                PageSize="5" OnRowCommand="gridDraft_RowCommand" OnRowDataBound="gridDraft_RowDataBound"
                                                EmptyDataText="There is no Message." Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label21y" runat="server" Text='<%#  Eval("MsgDate", "{0:MM/dd/yyyy}")%>'></asp:Label>
                                                            <asp:Label ID="Label51y" runat="server" Visible="false" Text='<%#  Eval("MsgDate", "{0:HH:mm:ss}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sent To" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="17%">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSentTo" Text='<%# Eval("Compname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="55%" SortExpression="MsgSubject">
                                                        <ItemTemplate>
                                                            <a href='MessageCompose.aspx?MsgId=<%# Eval("MsgId")%>' style="color: Black">
                                                                <%#  Eval("MsgSubject")%></a>
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
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:Panel>
                    </td>
                    <td style="border: 1px solid #000000; width: 50%">
                        <asp:Panel ID="Panel6" runat="server" Height="470px" Width="100%">
                            <div style="clear: both;">
                            </div>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label29" runat="server" Text="Select"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:DropDownList ID="ddlexternal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlexternal_SelectedIndexChanged">
                                                <asp:ListItem Selected="true" Text="Inbox" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Compose" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Sent" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Deleted" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Drafts" Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                            <div style="clear: both;">
                            </div>
                            <asp:Panel ID="panelinboxext" runat="server" Height="460px">
                                <asp:Label ID="lblmsg3" runat="server" ForeColor="Red"></asp:Label>
                                <div style="clear: both;">
                                </div>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label35" runat="server" Text="Select Email ID">
                                                </asp:Label>
                                            </label>
                                            <label>
                                                <asp:DropDownList ID="ddlempemail" runat="server" AutoPostBack="True" DataTextField="EmailId"
                                                    Width="250px" DataValueField="CompanyEmailId" OnSelectedIndexChanged="ddlempemail_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td align="right" valign="bottom">
                                            <asp:Panel ID="panelinboxext1" runat="server">
                                                <table>
                                                    <tr>
                                                        <td style="text-align: right; width: 50%">
                                                            <asp:LinkButton ID="LinkButton7" Text="More.." runat="server" CssClass="btnSubmit"
                                                                OnClick="taskmore_Click1" ForeColor="Black"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="gridInbox" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                DataKeyNames="MsgDetailId" OnRowDataBound="gridInbox_RowDataBound" AllowPaging="True"
                                                OnPageIndexChanging="gridInbox_PageIndexChanging" EmptyDataText="There is no Message."
                                                AllowSorting="True" OnSorting="gridInbox_Sorting" Width="100%" PageSize="5">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label21q" runat="server" Text='<%#  Eval("MsgDate", "{0:MM/dd/yyyy}")%>'></asp:Label>
                                                            <asp:Label ID="Label51q" runat="server" Visible="false" Text='<%#  Eval("MsgDate", "{0:HH:mm:ss}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        SortExpression="PartyName">
                                                        <ItemTemplate>
                                                            <a href="MessageViewExt.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>&Status=<%# Eval("MsgStatusId")%>">
                                                                <font color="gray">
                                                                    <%#  Eval("Compname")%></font></a>
                                                            <asp:Label ID="lblemail" runat="server" Text='<%#  Eval ("Compname")  %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        SortExpression="MsgSubject">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsubbbjesdsct" runat="server" Text='<%#Eval("MsgSubject")%>' ToolTip='<%#Eval("MsgDetail")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png" />
                                                        </HeaderTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="MsgStatusName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderText="Status" SortExpression="MsgStatusName" Visible="false"></asp:BoundField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="panelcomposeext" runat="server" Height="460px" Visible="false">
                                <asp:Label ID="lblmsg1" runat="server" ForeColor="Red"></asp:Label>
                                <div style="clear: both;">
                                </div>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 8%">
                                            <label>
                                                <asp:Label ID="Label37" Text="Business" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 42%">
                                            <label>
                                                <asp:DropDownList ID="ddlwarehouseext" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouseext_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                            <label>
                                                <asp:Label ID="klblStoreName" Text="From" runat="server"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:DropDownList ID="DropDownList15" runat="server" DataTextField="EmailId" DataValueField="CompanyEmailId"
                                                    Width="230px">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                    <td style="width: 8%">
                                        
                                    </td>
                                    <td style="width: 42%">
                                        
                                    </td>
                                </tr>--%>
                                    <tr>
                                        <td style="width: 8%" valign="top">
                                            <label>
                                                <asp:Label ID="lblTo" Text="To" runat="server"></asp:Label>
                                                <asp:Label ID="Labezzxcccl36" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lblAddresses"
                                                    ErrorMessage="*" ValidationGroup="1" Width="16px"></asp:RequiredFieldValidator>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Account/images/addbook.png"
                                                    ToolTip="Click here to add Addresses" />
                                            </label>
                                        </td>
                                        <td style="width: 42%">
                                            <label>
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="lblAddresses" runat="server" Width="300px"></asp:TextBox>
                                                        &nbsp;
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="Button1"></asp:AsyncPostBackTrigger>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 8%" valign="top">
                                            <label>
                                                <asp:Label ID="lblSubject" Text="Subject" runat="server"></asp:Label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([._:a-zA-Z0-9\s]*)"
                                                    ControlToValidate="txtsub" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td style="width: 42%">
                                            <label>
                                                <asp:TextBox ID="txtsub" runat="server" Width="300px" MaxLength="200" onKeydown="return mask(event)"
                                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\_:a-zA-Z.0-9\s]+$/,'Span3',200)"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:Label runat="server" ID="sadasd" Text="Max" CssClass="labelcount"></asp:Label>
                                                <span id="Span3" cssclass="labelcount">200</span>
                                                <asp:Label ID="Labezxcxcl37" runat="server" Text="(A-Z 0-9 _ . :)" CssClass="labelcount"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 8%" valign="top">
                                            <label>
                                                <asp:Label ID="Labelzxc36" runat="server" Text="Message"></asp:Label>
                                                <br />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)"
                                                    ControlToValidate="TxtMsgDetail" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="*"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1000})$"
                                                    ControlToValidate="TxtMsgDetail" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td style="width: 42%">
                                            <label>
                                                <asp:TextBox ID="TxtMsgDetail" runat="server" TextMode="MultiLine" Width="300px"
                                                    Height="60px" onkeypress="return checktextboxmaxlength(this,1000,event)" onKeydown="return mask(event)"
                                                    onkeyup="return check(this,/[\\/!#$%^'&*>:;={}[]|\/]/g,/^[\a-z().@+A-Z,0-9_-\s]+$/,'div2',1000)"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:Label runat="server" ID="Label112" Text="Max" CssClass="labelcount"></asp:Label>
                                                <span id="div2" cssclass="labelcount">1000</span>
                                                <asp:Label ID="Labelzx37" runat="server" Text="(A-Z 0-9 , . @ ) ( - +)" CssClass="labelcount"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 8%" valign="top">
                                            <label>
                                                <asp:Label ID="Label15" runat="server" Text="Attachment"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 42%">
                                            <label>
                                                <asp:FileUpload ID="fileupload1" runat="server" />
                                            </label>
                                            <label>
                                                <asp:Button ID="btnattachext" runat="server" Text="Attach" ValidationGroup="2" CssClass="btnSubmit"
                                                    OnClick="btnattachext_Click" />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 8%" valign="top">
                                        </td>
                                        <td style="width: 42%">
                                            <asp:Panel runat="server" Visible="false" ID="panelattachext" ScrollBars="Vertical"
                                                Height="90px">
                                                <asp:GridView ID="gridattachext" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                    AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    DataKeyNames="FileNameChanged" PageSize="5" Width="100%" OnPageIndexChanging="gridattachext_PageIndexChanging"
                                                    OnRowCommand="gridattachext_RowCommand">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="Remove" ImageUrl="~/Account/images/delete.gif" HeaderImageUrl="~/Account/images/delete.gif"
                                                            ItemStyle-Width="5%" HeaderText="Remove" ButtonType="Image"></asp:ButtonField>
                                                        <asp:BoundField DataField="FileName" HeaderText="Attached File Name" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" />
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 8%">
                                        </td>
                                        <td style="width: 42%">
                                            <asp:CheckBox ID="chksignature" runat="server" Text="Include Signature" />
                                            <asp:CheckBox ID="chkpicture" runat="server" Text="Include Picture" />
                                            <%--<asp:CheckBox ID="chkattachmentext" runat="server" Text="Add Attachment" AutoPostBack="true"
                                            OnCheckedChanged="chkattachmentext_CheckedChanged" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 8%">
                                        </td>
                                        <td style="width: 42%">
                                            <asp:Button ID="imgbtnsend" runat="server" Text="Send" OnClick="imgbtnsend_Click"
                                                ValidationGroup="1" CssClass="btnSubmit" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="panelsentext" runat="server" Height="460px" Visible="false">
                                <div style="clear: both;">
                                </div>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label52" runat="server" Text="Select Email ID">
                                                </asp:Label>
                                            </label>
                                            <label>
                                                <asp:DropDownList ID="ddlempemailsent" runat="server" AutoPostBack="True" DataTextField="EmailId"
                                                    Width="250px" DataValueField="CompanyEmailId" OnSelectedIndexChanged="ddlempemailsent_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td align="right" valign="bottom">
                                            <asp:Panel ID="panelsentext1" runat="server" Visible="false">
                                                <table>
                                                    <tr>
                                                        <td style="text-align: right; width: 50%">
                                                            <asp:LinkButton ID="LinkButton8" Text="More.." runat="server" CssClass="btnSubmit"
                                                                OnClick="LinkButton8_Click" ForeColor="Black"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="GridView5" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                PageSize="5" AlternatingRowStyle-CssClass="alt" DataKeyNames="MsgId" GridLines="None"
                                                AutoGenerateColumns="False" AllowPaging="True" EmptyDataText="There is no Message."
                                                AllowSorting="True" Width="100%" OnPageIndexChanging="GridView5_PageIndexChanging"
                                                OnRowDataBound="GridView5_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" SortExpression="Date"
                                                        HeaderStyle-Width="13%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label21w" runat="server" Text='<%#  Eval("MsgDate", "{0:MM/dd/yyyy}")%>'></asp:Label>
                                                            <asp:Label ID="Label51w" runat="server" Visible="false" Text='<%#  Eval("MsgDate", "{0:HH:mm:ss}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sent To" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSentTo"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email ID" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSeasdntTo" Text='<%# Eval("FromEmail") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" SortExpression="MsgSubject"
                                                        HeaderStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <a href='MessageViewSentExt.aspx?MsgId=<%# Eval("MsgId")%>' style="color: Black">
                                                                <%#  Eval("MsgSubject")%>
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png" />
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="paneldeleteext" runat="server" Height="460px" Visible="false">
                                <div style="clear: both;">
                                </div>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label54" runat="server" Text="Select Email ID">
                                                </asp:Label>
                                            </label>
                                            <label>
                                                <asp:DropDownList ID="DropDownList16" runat="server" AutoPostBack="True" DataTextField="EmailId"
                                                    Width="250px" DataValueField="CompanyEmailId" OnSelectedIndexChanged="DropDownList16_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td align="right" valign="bottom">
                                            <asp:Panel ID="paneldeleteext1" runat="server" Visible="false">
                                                <table>
                                                    <tr>
                                                        <td style="text-align: right; width: 50%">
                                                            <asp:LinkButton ID="LinkButton9" Text="More.." runat="server" CssClass="btnSubmit"
                                                                OnClick="LinkButton9_Click" ForeColor="Black"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" PageSize="5"
                                                GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                DataKeyNames="MsgDetailId" AllowPaging="True" EmptyDataText="There is no Message."
                                                Width="100%" OnPageIndexChanging="GridView6_PageIndexChanging" OnRowDataBound="GridView6_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" SortExpression="Date"
                                                        HeaderStyle-Width="13%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label21e" runat="server" Text='<%#  Eval("MsgDate", "{0:MM/dd/yyyy}")%>'></asp:Label>
                                                            <asp:Label ID="Label51e" runat="server" Visible="false" Text='<%#  Eval("MsgDate", "{0:HH:mm:ss}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="25%">
                                                        <ItemTemplate>
                                                            <a href="MessageViewExt.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>"><b><font color="black">
                                                                <%#  Eval("Compname")%></b></font></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="35%">
                                                        <ItemTemplate>
                                                            <a href="MessageViewExt.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>"><b><font color="black">
                                                                <%#  Eval("MsgSubject")%></b></font></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png" />
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="paneldraftsext" runat="server" Height="460px" Visible="false">
                                <div style="clear: both;">
                                </div>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label56" runat="server" Text="Select Email ID">
                                                </asp:Label>
                                            </label>
                                            <label>
                                                <asp:DropDownList ID="DropDownList17" runat="server" AutoPostBack="True" DataTextField="EmailId"
                                                    Width="250px" DataValueField="CompanyEmailId" OnSelectedIndexChanged="DropDownList17_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td align="right" valign="bottom">
                                            <asp:Panel ID="paneldraftsext1" runat="server" Visible="false">
                                                <table>
                                                    <tr>
                                                        <td style="text-align: right; width: 50%">
                                                            <asp:LinkButton ID="LinkButton10" Text="More.." runat="server" CssClass="btnSubmit"
                                                                OnClick="LinkButton10_Click" ForeColor="Black"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="GridView7" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                PageSize="5" AlternatingRowStyle-CssClass="alt" DataKeyNames="MsgId" GridLines="None"
                                                AutoGenerateColumns="False" AllowPaging="True" EmptyDataText="There is no Message."
                                                Width="100%" OnPageIndexChanging="GridView7_PageIndexChanging" OnRowDataBound="GridView7_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="13%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label21r" runat="server" Text='<%#  Eval("MsgDate", "{0:MM/dd/yyyy}")%>'></asp:Label>
                                                            <asp:Label ID="Label51r" runat="server" Visible="false" Text='<%#  Eval("MsgDate", "{0:HH:mm:ss}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="13%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sent To" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSentTo"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email ID" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSeasdntTo" Text='<%# Eval("ToEmailId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <a href='MessageComposeExt.aspx?MsgId=<%# Eval("MsgId")%>' style="color: Black">
                                                                <%#  Eval("MsgSubject")%></a>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png" />
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
        <div style="clear: both;">
        </div>
        <div>
            <asp:Panel ID="Panel16" runat="server" CssClass="modalPopup" Width="300px">
                <table id="Table6" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label11" runat="server" ForeColor="Black">
                                        Are you sure you wish to enter outtime ? As you can make entry of 
                            outtime once in a day.</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnyes" runat="server" Text="Confirm" OnClick="btnyes_Click" CssClass="btnSubmit" />
                            <asp:Button ID="btnignore" runat="server" Text="Cancel" CssClass="btnSubmit" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel16" TargetControlID="HiddenButton222" CancelControlID="btnignore">
            </cc1:ModalPopupExtender>
        </div>
        <div style="clear: both;">
        </div>
        <div>
            <asp:Panel ID="Panel1612" runat="server" CssClass="modalPopup" Width="300px">
                <table id="Table612" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1112" runat="server" ForeColor="Black">
                                        Are you sure you wish to enter intime ? As you can make entry of 
                            intime once in a day.</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnyes12" runat="server" Text="Confirm" CssClass="btnSubmit" OnClick="btnyes12_Click" />
                            <asp:Button ID="btnignore12" runat="server" Text="Cancel" CssClass="btnSubmit" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="HiddenButton22212" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender122212" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel1612" TargetControlID="HiddenButton22212" CancelControlID="btnignore12">
            </cc1:ModalPopupExtender>
        </div>
        <div>
            <asp:Panel ID="Panel16121" runat="server" CssClass="modalPopup" Width="950px" Height="550px"
                BorderWidth="10px" BorderStyle="Groove">
                <table id="Table6121" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <div style="height: 180px">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label11121" runat="server" ForeColor="Black">
                            You are logging in for the first time. 
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label2" runat="server" ForeColor="Black">
                            We recommend that you use our set up wizard.
                            </asp:Label>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td align="center">
                            <asp:Label ID="Label4" runat="server" ForeColor="Black">
                            Please go to the menu "Getting Started" and click on the "Set Up Control Panel" to get started.
                            </asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <div style="height: 20px">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnyes121" runat="server" Text="Yes, I'd like to start the set up wizard"
                                CssClass="btnSubmit" OnClick="btnyes121_Click" />
                            <asp:Button ID="btnignore121" runat="server" Text="No thank you, not right now" CssClass="btnSubmit" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="HiddenButton222121" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1222121" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel16121" TargetControlID="HiddenButton222121" CancelControlID="btnignore121">
            </cc1:ModalPopupExtender>
        </div>
        <div>
            <asp:Panel ID="Panel161212" runat="server" CssClass="modalPopup" Width="950px" Height="550px"
                BorderWidth="10px" BorderStyle="Groove">
                <table id="Table6adas1212" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <div style="height: 180px">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label1asdasd11212" runat="server" ForeColor="Black">
                            You are loggin in for the first time.
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="Labasdsadel22" runat="server" ForeColor="Black">
                            We recommend you to use our set up wizard.
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="Labeasdsadl42" runat="server" ForeColor="Black">
                            Please go to the menu &quot;Getting Started&quot; and click on the &quot;Set Up Control Panel&quot; 
                            to get started.
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="height: 20px">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnyes1212" runat="server" Text="Yes, Lets Start the Setup Wizard"
                                CssClass="btnSubmit" OnClick="btnyes1212_Click" />
                            <asp:Button ID="btnignore1212" runat="server" Text="Not Right Now" CssClass="btnSubmit" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="HiddenButton2221212" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender12221212" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel161212" TargetControlID="HiddenButton2221212" CancelControlID="btnignore1212">
            </cc1:ModalPopupExtender>
        </div>
        <div style="clear: both;">
        </div>
        <div>
            <asp:Panel ID="Panel12" runat="server" Width="500px" BackColor="#CCCCCC" BorderColor="#AAAAAA"
                BorderStyle="Outset">
                <table>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                Width="16px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="font-weight: bold">
                            <asp:Label ID="lblAddReminderlbl" Text="Add new Reminder" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                            <asp:Label ID="lblDatelbl" Text="Date" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox1"
                                ErrorMessage="*" ValidationGroup="11">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 50%">
                            <asp:TextBox ID="TextBox1" runat="server" Width="70px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox1">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureName="en-AU"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TextBox1" />
                            <%-- <cc1:CalendarExtender CssClass="cal_Theme1" ID="CalendarExtender1" runat="server"
                                        PopupButtonID="TextBox1" TargetControlID="TextBox1">
                                    </cc1:CalendarExtender>   --%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                            <asp:Label ID="lblStatuslbl" Text="Status" runat="server"></asp:Label>
                        </td>
                        <td style="width: 50%">
                            <asp:DropDownList ID="ddlstatus" runat="server" ValidationGroup="11" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                            <asp:Label ID="lblReminderNote" Text="Reminder Note" runat="server"></asp:Label>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtremindernote"
                                ValidationGroup="11"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 50%">
                            <asp:TextBox ID="txtremindernote" runat="server" TextMode="MultiLine" Width="350px"
                                Height="150px" MaxLength="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                        </td>
                        <td style="width: 50%">
                            <asp:Button ID="Button4" runat="server" Text="Submit" OnClick="Button4_Click" CssClass="btnSubmit"
                                ValidationGroup="11" />
                            <asp:Button ID="Button5" runat="server" Text="Close" CssClass="btnSubmit" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                        </td>
                        <td style="width: 50%">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="modalBackground"
                PopupControlID="Panel12" TargetControlID="ImageButton6" runat="server" CancelControlID="ImageButton4">
            </cc1:ModalPopupExtender>
        </div>
        <div>
            <asp:Panel ID="Panel6666" runat="server" CssClass="modalPopup" BorderWidth="10px"
                BackColor="#d1cec5" Width="467px">
                <table cellpadding="2" cellspacing="2" align="center" style="width: 460px">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lbllatemessagereason" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="Label28" runat="server" Text="You are late/early, please  input the reason for being deviation. otherwise your attendance not approved."
                                ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label27" runat="server" Text="input the reason for being deviation :"
                                ForeColor="Black"></asp:Label>
                        </td>
                        <td align="left" width="50%">
                            <asp:TextBox ID="latereaso" runat="server" Text="" Height="100px" MaxLength="500"
                                TextMode="MultiLine" Width="210px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqsd" runat="server" ControlToValidate="latereaso"
                                ErrorMessage="*" ValidationGroup="5" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnaddlater" runat="server" Text="Add" OnClick="btnaddlater_Click"
                                CssClass="btnSubmit" />
                        </td>
                    </tr>
                </table>
                <asp:Button ID="Button6" runat="server" Style="display: none" />
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel6666" TargetControlID="Button6">
            </cc1:ModalPopupExtender>
        </div>
        <div>
            <asp:Panel ID="panelinter" runat="server" Visible="false">
                <asp:Panel ID="pnladdress" runat="server" BorderStyle="Outset" ScrollBars="Vertical"
                    Height="500px" Width="380px" BackColor="#CCCCCC" BorderColor="#666666">
                    <table id="innertbl1" visible="false">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSelectRecipient" Text="Select Receipient" runat="server"></asp:Label>
                                        </td>
                                        <td width="10%">
                                            <asp:Button ID="Button2" OnClick="Button2_Click" runat="server" Text="Insert" Font-Size="13px"
                                                Font-Names="Arial" ToolTip="Insert" CssClass="btnSubmit"></asp:Button>
                                        </td>
                                        <td width="10%">
                                            <asp:ImageButton ID="ibtnCancelCabinetAdd" runat="server" ImageUrl="~/Account/images/closeicon.png"
                                                AlternateText="Close" CausesValidation="False" ToolTip="Close"></asp:ImageButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label8" Text="User Type" runat="server"></asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlusertype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlusertype_SelectedIndexChanged"
                                                    Width="150px">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label9" Text="Search" runat="server"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtsearch" runat="server" OnTextChanged="txtsearch_TextChanged"
                                                    AutoPostBack="True" Width="150px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Panel runat="server" ID="pnlusertypeother" Visible="false">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label10" runat="server" Text="Company Name"></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:DropDownList ID="ddlcompany1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcompname_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Panel runat="server" ID="pnlusertypecandidate" Visible="false">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label13" runat="server" Text="Job Applied For"></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:DropDownList ID="ddlcandi11" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcandi_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:GridView ID="grdPartyList" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                DataKeyNames="PartyId" GridLines="None" EmptyDataText="No Parties are available for e-mail."
                                                OnRowDataBound="grdPartyList_RowDataBound" Width="338px" OnPageIndexChanging="grdPartyList_PageIndexChanging">
                                                <EmptyDataRowStyle ForeColor="Peru" />
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="HeaderChkbox" runat="server" OnCheckedChanged="HeaderChkbox_CheckedChanged" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkParty" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Contactperson" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                        Visible="false">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Compname" HeaderText="Company Name" HeaderStyle-HorizontalAlign="Left"
                                                        Visible="false">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <%--<asp:BoundField DataField="Name" HeaderText="LastName - FirstName" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>--%>
                                                    <asp:BoundField DataField="Name" HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <%--<asp:BoundField DataField="dname" HeaderText="Department - Designation" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>--%>
                                                    <asp:BoundField DataField="PartType" HeaderText="Type" HeaderStyle-HorizontalAlign="Left"
                                                        Visible="false">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CName" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VacancyPositionTitle" HeaderText="Job Applied For" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="pnladdress" TargetControlID="ImageButton5" X="950" Y="-200" CancelControlID="ibtnCancelCabinetAdd">
                </cc1:ModalPopupExtender>
            </asp:Panel>
        </div>
        <div>
            <asp:Panel ID="panelexter" runat="server" Visible="false">
                <asp:Panel ID="Panel21" runat="server" BorderStyle="Outset" Height="550px" Width="380px"
                    BackColor="#CCCCCC" BorderColor="#666666">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    <asp:TextBox ID="TextBox4" runat="server" Width="155px" ValidationGroup="3" Visible="False"></asp:TextBox>
                                </label>
                            </td>
                            <td align="left">
                                <asp:Button ID="Button13" runat="server" Text="Quick Add Party" CssClass="btnSubmit"
                                    Font-Size="13px" Font-Names="Arial" ValidationGroup="3" OnClick="Button2111_Click"
                                    Visible="False"></asp:Button>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                    ErrorMessage="Enter Valid Email" ControlToValidate="TextBox4" ValidationGroup="3"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                            <td align="left">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="lblselre" Text="Select Recipient" runat="server"></asp:Label>
                                </label>
                            </td>
                            <td align="left">
                                <asp:Button ID="Button14" OnClick="Button1_Click1" CssClass="btnSubmit" runat="server"
                                    Text="Insert" Font-Size="13px" Font-Names="Arial"></asp:Button>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="ImageButton3" runat="server" CssClass="btnSubmit" ImageUrl="~/Account/images/closeicon.png"
                                    AlternateText="Close" CausesValidation="False" OnClick="ImageButton1_Click" Height="16px"
                                    Width="16px"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="lblPartyType" Text="Party Type" runat="server"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlpartytype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpartytype_SelectedIndexChanged"
                                        Width="150px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label38" Text="Search" runat="server"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="TextBox5" runat="server" OnTextChanged="txtsearch_TextChanged111"
                                        AutoPostBack="True" Width="150px"></asp:TextBox>
                                </label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Panel runat="server" ID="Panel22" Visible="false">
                                    <table>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label36" runat="server" Text="Company Name"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:DropDownList ID="ddlcompname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcompname_SelectedIndexChanged111">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Panel runat="server" ID="Panel23" Visible="false">
                                    <table>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label40" runat="server" Text="Job Applied For"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:DropDownList ID="ddlcandi" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcandi_SelectedIndexChanged111">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="GridView4" runat="server" AllowPaging="true" AllowSorting="true"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid"
                                    DataKeyNames="PartyId" EmptyDataText="No Parties are available for e-mail." GridLines="None"
                                    PagerStyle-CssClass="pgr" Width="338px" OnPageIndexChanging="GridView4_PageIndexChanging"
                                    OnRowDataBound="GridView4_RowDataBound">
                                    <EmptyDataRowStyle ForeColor="Peru" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="HeaderChkbox" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkParty" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Contactperson" HeaderStyle-HorizontalAlign="Left" HeaderText="Name"
                                            Visible="false">
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Compname" HeaderStyle-HorizontalAlign="Left" HeaderText="Company Name"
                                            Visible="false">
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Employee Name">
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PartType" HeaderStyle-HorizontalAlign="Left" HeaderText="Type"
                                            Visible="false">
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CName" HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VacancyPositionTitle" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Job Applied For">
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel21" TargetControlID="ImageButton1" X="950" Y="-200" CancelControlID="ImageButton3">
                </cc1:ModalPopupExtender>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
