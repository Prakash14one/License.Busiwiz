<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="employeeafterlogin.aspx.cs" Inherits="ShoppingCart_Admin_EmployeeLogin" %>
       <%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upbh" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 12px">
                    <asp:Label ID="lblmsg" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnlsetupwizardlabel" runat="server" Width="100%" Visible="False">
                    <fieldset>
                        <label>
                            <asp:Label ID="lblsetupwizardlabel" runat="server" Text="Please fill all your batch details before using this application.."></asp:Label>
                        </label>
                    </fieldset></asp:Panel>
                <asp:Panel ID="pnlintime" runat="server" Width="100%" Visible="False">
                    <fieldset>
                        <table width="100%" style="text-align: center">
                            <tr>
                                <td style="text-align: center">
                                    <label>
                                        <asp:Label ID="Label1date" Visible="false" runat="server"></asp:Label>
                                        <asp:Label ID="time22" Visible="false" runat="server"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="You are login for the first time for the day. Please click here to mark your attendance"></asp:Label>
                                    </label>
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnin" runat="server" Text="Attendance - IN" CssClass="btnSubmit"
                                        OnClick="btnin_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Pnlcnfout" runat="server" Width="100%" Visible="False">
                    <fieldset>
                        <table width="100%" style="text-align: center">
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Your in time for the day is "></asp:Label>
                                        <asp:Label ID="lbltime" runat="server"></asp:Label>
                                        &nbsp;&nbsp;
                                        <asp:Label ID="Label5" runat="server" Text="Are you leaving office for the day?"></asp:Label>
                                    </label>
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnout" runat="server" Text="Attendance - OUT" CssClass="btnSubmit"
                                        OnClick="btnout_Click" />
                                    <asp:Button ID="Button3" runat="server" Text="Ext Visit Request" CssClass="btnSubmit"
                                        OnClick="Button3_Click" />
                                    <asp:Button ID="Button6" runat="server" Text="Going for Ext Visit" CssClass="btnSubmit"
                                        Visible="false" OnClick="Button6_Click" />
                                    <asp:Button ID="Button7" runat="server" Text="Cancel Request" CssClass="btnSubmit"
                                        Visible="false" OnClick="Button7_Click" />
                                    <br />
                                    <asp:Label ID="Label39" runat="server" Text="External Visit Request Pending" Visible="false"
                                        ForeColor="#416271" Font-Bold="true" Font-Size="14px"></asp:Label>
                                    <asp:Label ID="Label40" runat="server" Text="External Visit Request Approved" Visible="false"
                                        ForeColor="#416271" Font-Bold="true" Font-Size="14px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Pnlout" runat="server" Width="100%" Visible="False">
                    <fieldset>
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="Your Attendance for the day "></asp:Label>
                                        <asp:Label ID="lbldate" Font-Bold="true" runat="server"></asp:Label>
                                        &nbsp;&nbsp;
                                        <asp:Label ID="Label7" runat="server" Text="is IN Time "></asp:Label>
                                        <asp:Label ID="lblintime" Font-Bold="true" runat="server"></asp:Label>
                                        &nbsp;&nbsp;
                                        <asp:Label ID="Label8" runat="server" Text="Out Time "></asp:Label>
                                        <asp:Label ID="lblout" Font-Bold="true" runat="server"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
                <div style="clear: both;">
                </div>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlEntry" runat="server" Width="100%" Visible="false">
                                <fieldset>
                                    <label>
                                        <asp:Label ID="Label10" runat="server" Text="Going out for Office Work?"></asp:Label>
                                    </label>
                                    <asp:RadioButton ID="rdomain" Text="Yes" runat="server" Visible="true" OnCheckedChanged="Unnamed1_CheckedChanged"
                                        AutoPostBack="True" />
                                </fieldset></asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlEntryBox" runat="server" Visible="false" Width="100%">
                                <fieldset>
                                    <label>
                                        <asp:Label ID="lblGatepassREQNo" runat="server" Text="Gatepass Requsition Number "></asp:Label><asp:Label
                                            ID="lblreq" CssClass="lblSuggestion" runat="server"></asp:Label></label>
                                    <label>
                                        <asp:Label ID="lblDateSubmitted" Text="Date Submitted " runat="server"></asp:Label><asp:Label
                                            ID="lbldt" CssClass="lblSuggestion" runat="server"></asp:Label></label>
                                    <label>
                                        <asp:Label ID="lblStatus" Text="Status " runat="server"></asp:Label><asp:Label ID="lblapproval"
                                            CssClass="lblSuggestion" runat="server"></asp:Label></label>
                                    <div style="float: right;">
                                        <asp:Button ID="btnPrint" Text="Profile" OnClick="btnPrint_Click" runat="server"
                                            CssClass="btnSubmit" />
                                    </div>
                                </fieldset></asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlReturn" runat="server" Width="100%" Visible="false">
                                <fieldset>
                                    <label>
                                        <asp:Label ID="lblReturn" Text="Return Back To Office" runat="server"></asp:Label></label>
                                    <label>
                                        <asp:RadioButton ID="rdoReturn" OnCheckedChanged="Return_CheckedChanged" Text="Yes"
                                            runat="server" Visible="true" AutoPostBack="True" />
                                    </label>
                                </fieldset></asp:Panel>
                        </td>
                    </tr>
                </table>
                <div>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; background-color: #416271;" align="center">
                                <asp:Label ID="Label1" runat="server" Text="Reminder" Font-Bold="False" Font-Size="17px"
                                    ForeColor="White"></asp:Label>
                            </td>
                            <td style="width: 50%; background-color: #416271;" align="center">
                                <asp:Label ID="Labelb1" runat="server" Text="My Today Task"
                                    Font-Bold="False" Font-Size="17px" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #000000; width: 50%;" valign="top">
                               <%-- <asp:Panel ID="Panel1" runat="server" Width="100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="lbl" runat="server" Text="Pay Period "></asp:Label>
                                                                <asp:Label ID="lblcdate" runat="server" Text="" Visible="false"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlpayperiod" runat="server">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Button ID="btngo" runat="server" CssClass="btnSubmit" Width="50px" Text=" Go "
                                                                    OnClick="btngo_Click" />
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <asp:Panel ID="Panel4" runat="server" Height="250px" ScrollBars="Vertical">
                                                    <asp:GridView ID="grdattendance" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        PageSize="6" Width="100%" EmptyDataText="No Record Found." AllowPaging="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate" runat="server" Text='<%#Bind("AtDate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="In Time" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblintime" runat="server" Text='<%#Bind("InTimeforcalculation") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Out Time" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblintime" runat="server" Text='<%#Bind("OutTimeforcalculation") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Overtime" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblintime" runat="server" Text='<%#Bind("Overtime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>--%>

                                   <asp:Panel ID="Panel2" runat="server" Height="300px" Width="100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 100%">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label9" runat="server" Text="Status "></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:DropDownList ID="ddlfilterstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterstatus_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Save" OnClick="Button1_Click"
                                                                    Visible="false" />
                                                                <asp:Button ID="ImageButton6" runat="server" Text="Add new" CssClass="btnSubmit"
                                                                    OnClick="ImageButton6_Click" />
                                                                <%--<asp:ImageButton ID="ImageButton6" runat="server" AlternateText="Add New" Height="20px"
                                                            ImageAlign="Middle" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" ToolTip="AddNew"
                                                            Width="20px" />--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Panel ID="Panel14" runat="server" Height="100%" Width="100%">
                                                                    <asp:GridView ID="grdreminder" runat="server" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                        AlternatingRowStyle-CssClass="alt" PageSize="6" AllowSorting="True" AutoGenerateColumns="False"
                                                                        Width="100%" EmptyDataText="No Record Found." AllowPaging="True">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblremindermasterid" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblreminderdate" runat="server" Text='<%#Bind("Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle Width="10%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Reminder Note" ItemStyle-Width="40%" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblremindernote" runat="server" Text='<%#Bind("ReminderNote") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle Width="40%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Completed" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle Width="10%" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="pgr" />
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td align="right">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>




                            </td>
                            <td style="border: 1px solid #000000; width: 50%" valign="top">
                               <%-- <asp:Panel ID="Panel2c" runat="server" Width="100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnldy" runat="server" Visible="true">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label28" runat="server" Text="Hours to date"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="lbltohour" runat="server" Text=""></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label30" runat="server" Text="Overtime"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="lblovert" runat="server" Text=""></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label31" runat="server" Text="Total Hours"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="lbltothour" runat="server" Text=""></asp:Label>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <asp:Panel ID="pnlgrddailyatt" runat="server" Height="250px" ScrollBars="Vertical">
                                                    <asp:GridView ID="grdmydailyatten" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        PageSize="6" Width="100%" EmptyDataText="No Record Found." DataKeyNames="AttendanceId"
                                                        AllowPaging="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Date" SortExpression="date" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate" runat="server" Text='<%#Bind("date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Day" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblday" runat="server" Text='<%#Bind("day") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Hours" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblhours" runat="server" Text='<%#Bind("hours") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Overtime" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblintime" runat="server" Text='<%#Bind("Overtime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Hours" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblintime" runat="server" Text='<%#Bind("totalhours") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>--%>

                           <table width="100%">
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:TextBox ID="txtestartdate" runat="server" Width="70px" OnTextChanged="txtestartdate_TextChanged" Visible="false"></asp:TextBox>
                                                        <cc1:CalendarExtender runat="server" ID="cal1" TargetControlID="txtestartdate">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                            MaskType="Date" TargetControlID="txtestartdate">
                                                        </cc1:MaskedEditExtender>
                                                    </label>
                                                    <label>
                                                        <asp:Button ID="btngo" Text="Go" runat="server" CssClass="btnSubmit" OnClick="btngo_Click" Visible="false" />
                                                    </label>
                                                    <%-- <label>
                                                        <asp:ImageButton ID="ImageButcxton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                    </label>--%>
                                                </td>
                                                <td valign="bottom">
                                                </td>
                                                <td align="right">
                                                    <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click" ForeColor="Black">Fill Report</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="clear: both;">
                                        </div>
                                        <asp:Panel ID="panel4" runat="server" Height="290px">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskId"
                                                            AllowSorting="True" Width="100%" EditRowStyle-VerticalAlign="Top" CssClass="mGrid"
                                                            PageSize="5" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" RowStyle-VerticalAlign="Top"
                                                            EmptyDataText="No Record Found." OnRowCommand="GridView9_RowCommand1" AllowPaging="True"
                                                            OnPageIndexChanging="GridView9_PageIndexChanging">
                                                            <RowStyle VerticalAlign="Top" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Date" SortExpression="taskallocationdate" ItemStyle-Width="5%"
                                                                    HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltaskallocationdate" runat="server" Text='<%# Eval("taskallocationdate","{0:MM/dd/yyyy}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Task Name" SortExpression="taskallocationdate" ItemStyle-Width="30%"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltaskname" runat="server" Text='<%# Eval("TaskMasterName")%>'></asp:Label>
                                                                        <asp:Label ID="lbltaskid" runat="server" Text='<%# Eval("taskid")%>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Budgeted Minute" SortExpression="EUnitsAlloted" ItemStyle-Width="5%"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblbudgetedminute123" runat="server" Text='<%# Eval("EUnitsAlloted")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Budgeted Empl.Cost" SortExpression="Rate" ItemStyle-Width="5%"
                                                                    HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbeemplcostute123" runat="server" Text='<%# Eval("Rate","{0:###,###.##}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Actual Minute" SortExpression="unitsused" ItemStyle-Width="5%"
                                                                    HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblunitsused" runat="server" Text='<%# Eval("unitsused")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtactualunit123" runat="server" Width="50px" Text='<%# Eval("unitsused")%>'> </asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="txtaddress_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtactualunit123"
                                                                            ValidChars="">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Actual Empl.Cost" ItemStyle-Width="5%" SortExpression="ActualRate"
                                                                    HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblactempcost" runat="server" Text='<%# Eval("ActualRate","{0:###,###.##}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Task Report" SortExpression="taskreport" ItemStyle-Width="35%"
                                                                    HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltaskreport" runat="server" Text='<%# Eval("taskreport")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txttaskreport" runat="server" TextMode="MultiLine" Text='<%# Eval("taskreport")%>'
                                                                            Height="60px" Width="300px"> </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Supervisor Note" SortExpression="supervisornote" Visible="false"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsupervisornote" runat="server" TextMode="MultiLine" Text='<%# Eval("supervisornote")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtsupervisornote" runat="server" Text='<%# Eval("supervisornote")%>'
                                                                            Width="200px"> </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemStyle Width="20%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status" SortExpression="StatusName" ItemStyle-Width="5%"
                                                                    HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblstatusname" runat="server" Text='<%# Eval("StatusName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList ID="ddlstatusfill" runat="server">
                                                                        </asp:DropDownList>
                                                                        <asp:Label ID="lblstatusid123" runat="server" Text='<%# Eval("Status")%>' Visible="false"></asp:Label>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Add Attachment" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderImageUrl="~/Account/images/attach.png" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%--<asp:Label ID="lblattachment" runat="server" Text="Add"></asp:Label>--%>
                                                                        <asp:ImageButton ID="lblattachment" runat="server" ToolTip="Attachment" ImageUrl="~/Account/images/attach.png" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%#Eval("TaskId") %>'
                                                                            ForeColor="Black" CommandName="Add">Add</asp:LinkButton>
                                                                        <%--<asp:Label ID="lblstatusid123" runat="server" Text='<%# Eval("Status")%>' Visible="false"></asp:Label>--%>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Docs" SortExpression="DocumentId" HeaderStyle-Width="4%"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("DocumentId") %>' CommandName="Send"
                                                                            CommandArgument='<%#Eval("TaskId") %>' ForeColor="Black"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:CommandField HeaderText="Edit" ShowEditButton="True" HeaderStyle-HorizontalAlign="Left"
                                                                    ButtonType="Image" EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/updategrid.jpg"
                                                                    CancelImageUrl="~/images/delete.gif" HeaderImageUrl="~/Account/images/edit.gif"
                                                                    HeaderStyle-Width="2%" Visible="false" />
                                                            </Columns>
                                                            <PagerStyle CssClass="pgr" />
                                                            <EditRowStyle VerticalAlign="Top" />
                                                            <AlternatingRowStyle CssClass="alt" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>





                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Panel ID="pnlremd" runat="server" >
                        <table id="subinnertbl" width="100%">
                            <tr>
                                <td style="width: 50%; background-color: #416271;" align="center">
                                    <asp:Label ID="Label12" runat="server" Text="My Weekly Goal" Font-Bold="False" Font-Size="16px"
                                        ForeColor="White"></asp:Label>
                                </td>
                                <td style="width: 50%; background-color: #416271;"  align="center">
                                 <asp:Label ID="Label28" runat="server" Text="My Pending Projects" Font-Bold="False" Font-Size="16px"
                                        ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #000000; width: 50%">
                                 

                                 <asp:Panel ID="Panel8" runat="server" style="    height: 264px;" >
                                  
                                      <div>

                                            <label>
                                                <asp:Label ID="Label30" runat="server" Text="Select" Visible="false"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:DropDownList ID="ddlgoals" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlgoals_SelectedIndexChanged" Visible="false">
                                                    <asp:ListItem Selected="true" Text="Weekly Goals" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Monthly Goals" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </label>
                                        </div>
                                       
                                        <asp:Panel ID="panel45" runat="server"  style="    margin-top: -209px;">
                                            <%--<fieldset>
                                                <legend>
                                                    <asp:Label ID="Label14" runat="server" Text="Weekly Goals"></asp:Label>
                                                </legend>--%>
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel589" runat="server"  Visible="false">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="right" style="width: 30%" valign="top">
                                                                        <label>
                                                                            <asp:Label ID="lblwnamefilter" runat="server" Text="Business Name"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                    <td align="left" valign="top">
                                                                        <label>
                                                                            <asp:DropDownList ID="ddlsearchByStore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsearchByStore_SelectedIndexChanged"
                                                                                Width="250px" Enabled="false">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <%--<asp:Panel ID="Panel622" runat="server" Width="100%" Visible="False">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="right" style="width: 30%">
                                                                        <label>
                                                                            <asp:Label ID="Label31" runat="server" Text="Employee Name"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <label>
                                                                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" Width="250px"
                                                                                Enabled="false" 
                                                                          >
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <label>
                                                            <asp:Label ID="Label23sdf" runat="server" Text="Year" ></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="True" Width="60px" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="Label23sdfdf" runat="server" Text="Month"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="True" Width="135px"
                                                                OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="Label32" runat="server" Text="Week"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:DropDownList ID="DropDownList8" runat="server" AutoPostBack="true" Width="180px"
                                                                OnSelectedIndexChanged="DropDownList8_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                </tr>
                                              
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" DataKeyNames="masterid"
                                                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                            AllowPaging="True" OnPageIndexChanging="grid_PageIndexChanging" PageSize="5"
                                                            Width="100%" AllowSorting="True" OnRowCommand="grid_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Week" SortExpression="Week" HeaderStyle-Width="25%"
                                                                    HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lweekmoneptname" runat="server" Text='<%#Bind("Week")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Weekly Goal Title" HeaderStyle-HorizontalAlign="Left"
                                                                    SortExpression="title">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltaskstatus" runat="server" Text='<%# Eval("title")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-Width="2%"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("masterid") %>'
                                                                            CommandName="view" ToolTip="View Profile" Height="20px" ImageUrl="~/Account/images/viewprofile.jpg"
                                                                            Width="20px"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerSettings FirstPageImageUrl="~/images/firstpg.gif" FirstPageText="" LastPageImageUrl="~/images/lastpg.gif"
                                                                LastPageText="" NextPageImageUrl="~/images/nextpg.gif" NextPageText="" PreviousPageImageUrl="~/images/prevpg.gif"
                                                                PreviousPageText="" />
                                                            <PagerStyle CssClass="pgr" />
                                                            <EmptyDataTemplate>
                                                                No Record Found.
                                                            </EmptyDataTemplate>
                                                            <AlternatingRowStyle CssClass="alt" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--</fieldset>--%></asp:Panel>
                                        <asp:Panel ID="panel777" runat="server" Height="300px" Visible="false">
                                            <%-- <fieldset>
                                                <legend>
                                                    <asp:Label ID="Label20" runat="server" Text="Monthly Goals"></asp:Label>
                                                </legend>--%>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel13" runat="server" Width="100%" Visible="false">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="right" style="width: 30%" valign="top">
                                                                        <label>
                                                                            <asp:Label ID="Label55" runat="server" Text="Business Name"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                    <td align="left" valign="top">
                                                                        <label>
                                                                            <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Width="250px"
                                                                                Enabled="False" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel17" runat="server" Width="100%" Visible="False">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="right" style="width: 30%">
                                                                        <label>
                                                                            <asp:Label ID="Label57" runat="server" Text="Employee Name"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <label>
                                                                            <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="true" Width="250px"
                                                                                Enabled="False">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="panelyrmon" runat="server" Width="100%" Visible="true">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <label>
                                                                            <asp:Label ID="Label58" runat="server" Text="Year"></asp:Label>
                                                                        </label>
                                                                        <label>
                                                                            <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True" Width="70px"
                                                                                OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                        <label>
                                                                            <asp:Label ID="Label59" runat="server" Text="Month"></asp:Label>
                                                                        </label>
                                                                        <label>
                                                                            <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="True" Width="130px"
                                                                                OnSelectedIndexChanged="DropDownList7_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <%--      <tr>
                                                        <td>
                                                            <asp:Panel ID="panel10" runat="server" Width="100%">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td align="right" style="width: 30%">
                                                                            <label>
                                                                                <asp:Label ID="Label24" runat="server" Text="Filter by Related Goals"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <label>
                                                                                <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="true" Width="250px"
                                                                                    OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged">
                                                                                    <asp:ListItem Value="0" Selected="True" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="1" Text="Yearly Goals of Employee"></asp:ListItem>
                                                                                    <asp:ListItem Value="2" Text="Monthly Goals of Business"></asp:ListItem>
                                                                                    <asp:ListItem Value="3" Text="Monthly Goals of Department"></asp:ListItem>
                                                                                    <asp:ListItem Value="4" Text="Monthly Goals of Division"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel11" runat="server" Width="100%" Visible="true">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td align="right" style="width: 30%">
                                                                            <label>
                                                                                <asp:Label ID="Label25" runat="server" Text="Yearly Goal Name"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <label>
                                                                                <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="true" Width="250px"
                                                                                    OnSelectedIndexChanged="DropDownList7_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Panel ID="Panel14" runat="server" Visible="false">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="text-align: right; width: 30%;">
                                                                            <label>
                                                                                <asp:Label ID="Label26" runat="server" Text="Business Monthly Goal"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td style="text-align: right; width: 70%;">
                                                                            <label>
                                                                                <asp:DropDownList ID="DropDownList12" runat="server" Width="250px" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="DropDownList12_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Panel ID="Panel17" runat="server" Visible="false">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="text-align: right; width: 30%;">
                                                                            <label>
                                                                                <asp:Label ID="Label39" runat="server" Text="Department Monthly Goal"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td style="text-align: right; width: 70%;">
                                                                            <label>
                                                                                <asp:DropDownList ID="DropDownList13" runat="server" Width="250px" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="DropDownList13_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Panel ID="Panel13" runat="server" Visible="false">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="text-align: right; width: 30%;">
                                                                            <label>
                                                                                <asp:Label ID="Label27" runat="server" Text="Division Monthly Goal"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td style="text-align: right; width: 70%;">
                                                                            <label>
                                                                                <asp:DropDownList ID="DropDownList14" runat="server" Width="250px" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="DropDownList14_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>--%>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" DataKeyNames="masterid"
                                                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                            PageSize="5" AllowPaging="True" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                            OnRowCommand="GridView1_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Month" SortExpression="Month" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbfrgtrikookortame" runat="server" Text='<%#Bind("Month")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Monthly Goal Title" HeaderStyle-HorizontalAlign="Left"
                                                                    SortExpression="title">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltaskstatus" runat="server" Text='<%# Eval("title")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-Width="2%"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("masterid") %>'
                                                                            CommandName="view" ToolTip="View Profile" Height="20px" ImageUrl="~/Account/images/viewprofile.jpg"
                                                                            Width="20px"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerSettings FirstPageImageUrl="~/images/firstpg.gif" FirstPageText="" LastPageImageUrl="~/images/lastpg.gif"
                                                                LastPageText="" NextPageImageUrl="~/images/nextpg.gif" NextPageText="" PreviousPageImageUrl="~/images/prevpg.gif"
                                                                PreviousPageText="" />
                                                            <PagerStyle CssClass="pgr" />
                                                            <EmptyDataTemplate>
                                                                No Record Found.
                                                            </EmptyDataTemplate>
                                                            <AlternatingRowStyle CssClass="alt" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--</fieldset>--%></asp:Panel>
                                </asp:Panel>


                                </td>
                                <td style="border: 1px solid #000000; width: 50%">

                                 <table style="width: 100%">
                                    <tr>
                                        <td style="width: 100%">
                                             <asp:Panel ID="Panel1" runat="server" Height="420px">
                                   
                                        <asp:Panel ID="panel18" runat="server" Height="350px">
                                            <table width="100%">
                                            <tr>
                                             <td align="right">
                                                    <asp:LinkButton ID="LinkButton12" runat="server" OnClick="LinkButton12_Click" ForeColor="Black" visible="false">Edit Status</asp:LinkButton>
                                                </td>
                                            
                                            </tr>
                                                <tr>
                                                    <td colspan="2">
                                                    

                                                        <asp:Panel ID="Panel19" runat="server" Width="100%" Visible="false">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="left" style="width: 30%;">
                                                                        <label>
                                                                            <asp:Label ID="Label61" runat="server" Text="Business Name"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                    <td style="width: 70%;">
                                                                        <label>
                                                                            <asp:DropDownList Width="250px" ID="DropDownList9" runat="server" AutoPostBack="True"
                                                                                Enabled="False" OnSelectedIndexChanged="DropDownList9_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" width="100%">
                                                        <asp:Panel ID="Panel20" runat="server" Width="100%" Visible="False">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width: 30%">
                                                                        <label>
                                                                            <asp:Label ID="Label65" runat="server" Text="Employee Name"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                    <td style="width: 70%">
                                                                        <label>
                                                                            <asp:DropDownList ID="DropDownList10" runat="server" AutoPostBack="true" Width="250px"
                                                                                Enabled="False" 
                                                                            onselectedindexchanged="DropDownList10_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel24" runat="server" Width="100%" Visible="false">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <label>
                                                                            <asp:Label ID="Label66" runat="server" Text=" Status"></asp:Label>
                                                                        </label>
                                                                        <label>
                                                                            <asp:DropDownList ID="DropDownList11" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList11_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                                                                <asp:ListItem Value="1" Text="Pending" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="Completed"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr style="vertical-align: top; height: 80%">
                                                    <td colspan="2">
                                                       <%-- <asp:GridView ID="GridView12" runat="server" AutoGenerateColumns="False" DataKeyNames="EEndDate"
                                                            Width="100%" AllowSorting="True" CssClass="mGrid" GridLines="Both" AllowPaging="true"
                                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found."
                                                            OnPageIndexChanging="GridView12_PageIndexChanging" PageSize="5" 
                                                            OnRowCommand="GridView12_RowCommand"  
                                                            >
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Business" SortExpression="Wname" Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblname" runat="server" Text='<%# Eval("Wname")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Department" SortExpression="Departmentname" Visible="true"
                                                                    HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldeprtment" runat="server" Text='<%# Eval("Departmentname")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Division" SortExpression="businessname" Visible="true"
                                                                    HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldesfdspartment" runat="server" Text='<%# Eval("businessname")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Employee" SortExpression="EmployeeName" Visible="true"
                                                                    HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldesdEmployeNamepartment" runat="server" Text='<%# Eval("EmployeeName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Project Name" HeaderStyle-Width="45%" SortExpression="projectname"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblmisionname" runat="server" Text='<%# Eval("projectname")%>'></asp:Label>
                                                                        <asp:Label ID="Label38" runat="server" Text='<%# Eval("ProjectId")%>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status" SortExpression="status" Visible="true" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-Width="6%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblstatuss" runat="server" Text='<%# Eval("status")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" Width="6%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Target Date" SortExpression="EEndDate" HeaderStyle-Width="8%"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltargetedate" runat="server" Text='<%# Eval("EEndDate","{0:mm/dd/yyyy}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbltotalesum" runat="server" Text="Total"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Budgeted Cost Planned" SortExpression="budgetedcost"
                                                                    Visible="false" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblbudecost" runat="server" Text='<%# Eval("budgetedcost")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblfootter" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Budgeted Cost Allocated" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Right" SortExpression="bdcost" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ldsfsdsfrrlbudcost" runat="server" Text='<%# Eval("bdcost")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblfootter1" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Actual Cost" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Right" SortExpression="ActualCost" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ldsfsgd1344udfgfgfst" runat="server" Text='<%# Eval("ActualCost")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblfootter2" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Job Cost" HeaderStyle-Width="7%" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkjob3" runat="server" Checked='<%# Eval("Addjob")%>' Enabled="false" />
                                                                        <asp:LinkButton ID="lnklob3" runat="server" ForeColor="Black" CommandName="vie" CommandArgument='<%#Eval("ProjectId") %>'
                                                                            Text="Go"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Docs" SortExpression="DocumentId" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-Width="4%" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton5" runat="server" ForeColor="Black" Text='<%#Eval("DocumentId") %>'
                                                                            CommandName="Send" CommandArgument='<%#Eval("ProjectId") %>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" Width="4%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ProjectId") %>'
                                                                            ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" Width="15px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                                    HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ToolTip="Delete"
                                                                            ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                                        </asp:ImageButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-Width="2%"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("ProjectId") %>'
                                                                            CommandName="view" ToolTip="View Profile" Height="20px" ImageUrl="~/Account/images/viewprofile.jpg"
                                                                            Width="20px"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>--%>



                                                               <asp:GridView ID="grdmonthlygoal" runat="server" CssClass="mGrid"   onrowcommand="grdmonthlygoal_RowCommand1" PageSize="5"  AutoGenerateColumns="False" Width="100%"  
                                                    PagerStyle-CssClass="prg" AllowPaging="True"     onpageindexchanging="grdmonthlygoal_PageIndexChanging1"  EmptyDataText="No Record Found."
                                                      onrowdatabound="grdmonthlygoal_RowDataBound" >
                                                    <Columns>
                                                        <asp:TemplateField Visible="False" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblproId" runat="server" Text='<%#Bind("ProjectMaster_Id")%>'></asp:Label>
                                                                <asp:Label ID="lblemp" runat="server" Text='<%#Bind("ProjectMaster_Employee_Id")%>'></asp:Label>
                                                               <%-- <asp:Label ID="lbldep" runat="server" Text='<%#Bind("ProjectMaster_DeptID")%>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dept Name" HeaderStyle-Width="10%" Visible="false">
                                                            <ItemTemplate>
                                                              <%--  <asp:Label ID="Label120" runat="server" Text='<%#Bind("Name")%>' ></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Name" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label121" runat="server" Text='<%#Bind("EmployeeName")%>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Project Title" >
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="Gray" CommandArgument='<%# Eval("ProjectMaster_Id") %>'
                                                                                            CommandName="view111"  Text='<%# Eval("ProjectMaster_ProjectTitle") %>'
                                                                    ToolTip="View Project Profile" >LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                           <asp:TemplateField HeaderText="Priority" Visible="false">
                                                            <ItemTemplate >
                                                              <%--  <asp:Label ID="Label_Priority" runat="server" Text='<%# Eval("Priority") %>' ></asp:Label>--%>
                                                                 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Start Date" Visible="false"  >
                                                            <ItemTemplate >
                                                                <asp:Label ID="Label122" runat="server" Text='<%# Eval("ProjectMaster_StartDate","{0:dd.MM.yyyy}") %>' ></asp:Label>
                                                                 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="End Date" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label123" runat="server" Text='<%# Eval("ProjectMaster_EndDate","{0:dd.MM.yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Target End Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label20" runat="server" Text='<%# Eval("ProjectMaster_TargetEndDate","{0:dd.MM.yyyy}") %>' ForeColor="Red" Font-Bold="True" Visible="false"></asp:Label>
                                                                 <%--<asp:Label ID="Label23" runat="server" Text=" / Overdue" ForeColor="Red" Font-Bold="True" ></asp:Label>--%>
                                                                
                                                                <asp:Label ID="Label124" runat="server" Text='<%# Eval("ProjectMaster_TargetEndDate","{0:dd.MM.yyyy}") %>' ></asp:Label>
                                                                <%--<asp:Label ID="Label25" runat="server" Text=" / Due Today"  Font-Bold="True" ></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Upcoming Deadlines">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label125" runat="server" Text='<%# Eval("deadline") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label126" runat="server" Text='<%# Eval("ProjectMaster_ProjectStatus") %>'></asp:Label>
                                                                 <%--<asp:Label ID="lblcompleted" runat="server" Text='<%# Eval("ProjectMaster_ProjectStatus") %>' ForeColor="Green" Font-Bold="True"></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/adda.png" HeaderStyle-HorizontalAlign="Left" >
                                                            <ItemTemplate>
                                                                
                                                                <asp:ImageButton ID="Image" runat="server" ImageUrl="~/Account/images/adda.png" CommandArgument='<%# Eval("ProjectMaster_Id") %>'
                                                                    CommandName="View" ToolTip="Add Progress Report" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" Visible="false" >
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ProjectMaster_Id") %>'
                                                                    ImageUrl="~/Account/images/edit.gif"  ToolTip="Edit"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton4" runat="server" CommandName="Delete" CommandArgument='<%# Eval("ProjectMaster_Id") %>'
                                                                    ImageUrl="~/Account/images/delete.gif" ToolTip="Delete" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                              
                                                
                                          
                                            <input id="Hidden4" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                            <input id="Hidden5" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                        










                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                  
                                </asp:Panel>
                                        </td>
                                    </tr>
                                </table>


                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div>
                    <table id="Table1" width="100%">
                        <tr>
                            <td style="width: 50%; background-color: #416271;" align="center">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                          <td align="right" style="width: 30%;">
                                             <asp:Label ID="lblle" runat="server" Text="My Leave Balance" Font-Bold="False" Font-Size="17px"
                                                ForeColor="White"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 20%;">
                                            <asp:LinkButton ID="LinkButton1" Text="Add Leave Request" ForeColor="White" runat="server"
                                                OnClick="LinkButton1_Click"></asp:LinkButton>
                                        </td>
                                       
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; background-color: #416271;" align="center">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="right" style="width: 30%;">
                                            <asp:Label ID="Label3" runat="server" Text="External Visit Request" Font-Bold="False"
                                                Font-Size="17px" ForeColor="White"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 20%;">
                                            <asp:LinkButton ID="lblexvis" Text="Add External Visit Request" ForeColor="White" runat="server"
                                                OnClick="lblexvis_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #000000; width: 50%" valign="top">
                                <asp:Panel ID="Panel3" runat="server" Height="250px">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    DataKeyNames="Id" EmptyDataText="No Record Found." Width="100%" AllowSorting="True"
                                                    PageSize="6" AllowPaging="True">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Leave Type" HeaderStyle-Width="30%" SortExpression="EmployeeLeaveTypeName"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblempleavetype" runat="server" Text='<%# Bind("EmployeeLeaveTypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Leaves Available" HeaderStyle-Width="150px" SortExpression="NumberofleaveEarned"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lblnoofleave" runat="server" Text='<%# Eval("NumberofleaveEarned")%>'
                                                                    ForeColor="Black" OnClick="lblleaveencask_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                                <asp:GridView ID="gridFileAttach" runat="server" AllowPaging="true" AlternatingRowStyle-CssClass="alt"
                                                    AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="FileNameChanged" GridLines="Both"
                                                    OnPageIndexChanging="gridFileAttach_PageIndexChanging" OnRowCommand="gridFileAttach_RowCommand"
                                                    PagerStyle-CssClass="pgr" PageSize="5" Width="100%">
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="Remove" HeaderImageUrl="~/Account/images/delete.gif"
                                                            HeaderText="Remove" ImageUrl="~/Account/images/delete.gif" ItemStyle-Width="5%" />
                                                        <asp:BoundField DataField="FileName" HeaderStyle-HorizontalAlign="Left" HeaderText="Attached File Name"
                                                            ItemStyle-HorizontalAlign="Left" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="border: 1px solid #000000; width: 50%" valign="top">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:Panel ID="Panel5" runat="server" Height="250px" Width="100%">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Panel ID="Panel6" runat="server" Height="100%" Width="100%">
                                                                <asp:GridView ID="grdgatepassreport" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                    AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                    PageSize="6" DataKeyNames="Id" OnRowCommand="grdgatepassreport_RowCommand" EmptyDataText="No Record Found."
                                                                    OnPageIndexChanging="grdgatepassreport_PageIndexChanging" AllowSorting="True">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Date" SortExpression="Date" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDate" Text='<%#Bind("Date","{0:MM/dd/yyyy}") %>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle Width="5%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="3%" HeaderText="Req No" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGatePass" Text='<%#Bind("GatepassREQNo") %>' runat="server"></asp:Label>
                                                                                <asp:Label ID="lblmasterid123" Width="40px" Visible="false" Text='<%#Bind("Id") %>'
                                                                                    runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                            <ItemStyle Width="5%"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Employee Name" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEmployeeName" Text='<%#Bind("EmployeeName") %>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Party visited" Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblpnName" Text='<%#Bind("Compname") %>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Out Time (Approved)" ItemStyle-Width="6%" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtOuttime" Text='<%#Bind("ExpectedOutTime") %>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle Width="6%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="In Time (Approved)" ItemStyle-Width="6%" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="txtInTime" Text='<%#Bind("ExpectedInTime") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle Width="6%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" ItemStyle-HorizontalAlign="Center"
                                                                            ItemStyle-Width="2%">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                                                    CommandName="view" ToolTip="View Profile" ImageUrl="~/Account/images/viewprofile.jpg">
                                                                                </asp:ImageButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle CssClass="pgr" />
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;
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
                                <asp:Label ID="Label17" runat="server" Text="Internal Message Center" Font-Size="17px"
                                    ForeColor="White"></asp:Label>
                                &nbsp;
                            </td>
                            <td style="width: 50%; background-color: #416271;" align="center">
                                <asp:Label ID="Label18" runat="server" Text="External Message Center" Font-Size="17px"
                                    ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #000000; width: 50%">
                                <asp:Label ID="lblmsg2" runat="server" ForeColor="Red"></asp:Label>
                                <div style="clear: both;">
                                </div>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label19" runat="server" Text="Select"></asp:Label>
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
                                                    PageSize="10" OnSorting="GridView3_Sorting1" OnRowDataBound="GridView3_RowDataBound">
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
                                                                <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" Width="20px"
                                                                    Height="20px" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png"
                                                                    Width="20px" Height="20px" />
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
                                                                    <asp:Label ID="Label20" Text="Business" runat="server"></asp:Label>
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
                                                                    <asp:Label ID="Label25" runat="server" Text="Attachment"></asp:Label>
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
                                                                <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" Width="20px"
                                                                    Height="20px" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png"
                                                                    Width="20px" Height="20px" />
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
                                                    EmptyDataText="There is no Message." Width="100%" AllowSorting="True" OnSorting="gridDelete_Sorting">
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
                                                                <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" Width="20px"
                                                                    Height="20px" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png"
                                                                    Width="20px" Height="20px" />
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
                                                                <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" Width="20px"
                                                                    Height="20px" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png"
                                                                    Width="20px" Height="20px" />
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="border: 1px solid #000000; width: 50%">
                                <div style="clear: both;">
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" type="hidden" /></label>
                                </div>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label29" runat="server" Text="Select"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlexternal" runat="server" AutoPostBack="true" Width="100px"
                                                    OnSelectedIndexChanged="ddlexternal_SelectedIndexChanged">
                                                    <asp:ListItem Selected="true" Text="Inbox" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Compose" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Sent" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Deleted" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Drafts" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label35" runat="server" Text="Email ID"> </asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlempemail" runat="server" AutoPostBack="True" DataTextField="EmailId"
                                                    Width="150px" DataValueField="CompanyEmailId" OnSelectedIndexChanged="ddlempemail_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td align="right" valign="bottom">
                                            <asp:Panel ID="panelinboxext1" runat="server">
                                                <table>
                                                    <tr>
                                                        <td style="text-align: right; width: 50%">
                                                            <asp:LinkButton ID="lbg" Text="More.." runat="server" CssClass="btnSubmit" OnClick="taskmore_Click1"
                                                                ForeColor="Black"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
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
                                            <td colspan="2">
                                                <asp:GridView ID="gridInbox" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    DataKeyNames="MsgDetailId" OnRowDataBound="gridInbox_RowDataBound" AllowPaging="True"
                                                    OnPageIndexChanging="gridInbox_PageIndexChanging" EmptyDataText="There is no Message."
                                                    AllowSorting="True" OnSorting="gridInbox_Sorting" Width="100%" PageSize="10">
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
                                                                <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" Width="20px"
                                                                    Height="20px" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png"
                                                                    Width="20px" Height="20px" />
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
                                            <td colspan="2">
                                                <label>
                                                    <asp:Label ID="Label37" Text="Business" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:DropDownList ID="ddlwarehouseext" Width="150px" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlwarehouseext_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    <asp:Label ID="klblStoreName" Text="From" runat="server"></asp:Label>
                                                    <asp:Label ID="Label27" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValisddator2" runat="server" Display="Dynamic"
                                                        ControlToValidate="DropDownList15" ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                                </label>
                                                <label>
                                                    <asp:DropDownList ID="DropDownList15" runat="server" DataTextField="EmailId" DataValueField="CompanyEmailId"
                                                        Width="150px">
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lblAddresses"
                                                        ErrorMessage="*" ValidationGroup="1" Width="16px"></asp:RequiredFieldValidator>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Account/images/addbook.png"
                                                        ToolTip="Click here to add Addresses" />
                                                </label>
                                            </td>
                                            <td style="width: 42%">
                                                <label>
                                                    <%--    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always">
                                                        <ContentTemplate>--%>
                                                    <asp:TextBox ID="lblAddresses" runat="server" Width="200px"></asp:TextBox>
                                                    &nbsp;
                                                    <%--  </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="Button1"></asp:AsyncPostBackTrigger>
                                                        </Triggers>
                                                    </asp:UpdatePanel>--%>
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
                                                    <asp:TextBox ID="txtsub" runat="server" Width="200px" MaxLength="200" onKeydown="return mask(event)"
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
                                                    <asp:TextBox ID="TxtMsgDetail" runat="server" TextMode="MultiLine" Width="200px"
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
                                                    <asp:Label ID="Label26" runat="server" Text="Attachment"></asp:Label>
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
                                                    <asp:Label ID="Label52" runat="server" Text="Select Email ID"> </asp:Label>
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
                                                                <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" Width="20px"
                                                                    Height="20px" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png"
                                                                    Width="20px" Height="20px" />
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
                                                    <asp:Label ID="Label54" runat="server" Text="Select Email ID"> </asp:Label>
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
                                                                <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" Width="20px"
                                                                    Height="20px" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png"
                                                                    Width="20px" Height="20px" />
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
                                                    <asp:Label ID="Label56" runat="server" Text="Select Email ID"> </asp:Label>
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
                                                                <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" Width="20px"
                                                                    Height="20px" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png"
                                                                    Width="20px" Height="20px" />
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
                 <div>
                    <asp:Panel ID="pnlsupperatt" runat="server">
                        <table width="100%">
                            <tr>
                                <td style="width: 50%; background-color: #416271;" align="center">
                                    <asp:Label ID="lbltodaypres" runat="server" Text="Today's Presence of My Assistants"
                                        Font-Size="17px" ForeColor="White"></asp:Label>
                                </td>
                                <td style="width: 50%; background-color: #416271;" align="center">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label33" runat="server" Text="Today's Absense of My Assistants" Font-Size="17px"
                                                    ForeColor="White"></asp:Label>
                                            </td>
                                            <td style="width: 05px;">
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Visible="false"
                                                    OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #000000; width: 50%" valign="top">
                                    <asp:Panel ID="pnldf" runat="server" Height="250px" Width="100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 100%" valign="top">
                                                    <asp:Panel ID="pnlscrollatt" runat="server">
                                                        <asp:GridView ID="gridattendance" runat="server" AutoGenerateColumns="False" DataKeyNames="DateId"
                                                            Width="100%" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AllowPaging="false"
                                                            PageSize="5" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found.">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Employee" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblemployeename123" runat="server" Text='<%# Eval("EmployeeName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date" Visible="false" SortExpression="Date" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblinstructiondate123" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyy}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="In Time" SortExpression="InTimeforcalculation" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblintimeforcalculation" runat="server" Text='<%# Eval("InTimeforcalculation")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Out Time" SortExpression="OutTimeforcalculation" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblouttimecalculate" runat="server" Text='<%# Eval("OutTimeforcalculation")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Out of Office?" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lblouttm" runat="server" Text='<%# Eval("GTA")%>' ForeColor="Black"
                                                                            OnClick="lbloutoff_Click"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerStyle CssClass="pgr" />
                                                            <AlternatingRowStyle CssClass="alt" />
                                                        </asp:GridView>
                                                    </asp:Panel>
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
                                <td style="border: 1px solid #000000; width: 50%" valign="top">
                                    <asp:Panel ID="Panel10" runat="server" Height="250px" Width="100%" ScrollBars="Vertical">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        PageSize="5" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        OnRowCommand="GridView8_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Department" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lbldept" Text='<%# Eval("DeptName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Employee" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblemployeeid" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reason" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="linking" runat="server" ForeColor="Black" CommandName="View"
                                                                        Text='<%# Eval("Reason") %>' CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                                                    <%--Text='<%# Eval("Late") %>'--%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left"  />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Absense since<br>no. of days" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lbllategone" Text='<%# Eval("Days") %>'></asp:Label>
                                                                    <%--Text='<%# Eval("Late") %>'--%>
                                                                </ItemTemplate>
                                                              <HeaderStyle HorizontalAlign="Left"  Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Absense in<br>past 30 days" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblearlycome" Text='<%# Eval("Absent") %>'></asp:Label>
                                                                    <%--Text='<%# Eval("Early") %>'--%>
                                                                </ItemTemplate>
                                                              <HeaderStyle HorizontalAlign="Left"  Width="95px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div>
                    <asp:Panel ID="pnlnormalapp" runat="server">
                        <table width="100%">
                            <tr>
                                <td style="width: 50%; background-color: #416271;" align="center">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label42" runat="server" Text="List of Leave Requests from My Assistants"
                                                    Font-Size="17px" ForeColor="White"></asp:Label>
                                            </td>
                                            <td align="right" >
                                                <asp:LinkButton ID="LinkButton2" Text="Add Leave Request" ForeColor="White" runat="server"
                                                    OnClick="LinkButton1_Click" Visible="false"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%; background-color: #416271;" align="center">
                                    <asp:Label ID="Label51" runat="server" Text="List of Gate Pass Request from My Assistants"
                                        Font-Size="17px" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #000000; width: 50%" valign="top">
                                    <asp:Panel ID="Panel9" runat="server" Height="250px" Width="100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 100%">
                                                    <asp:GridView ID="grdleaveapp" runat="server" EmptyDataText="No Record Found." AutoGenerateColumns="False"
                                                        Width="100%" DataKeyNames="id" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" AllowPaging="false" PageSize="6">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Employee" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblEmpName" runat="server" Text='<%# Bind("empname")%>' ForeColor="Black"
                                                                        OnClick="lblleavedate_Click"></asp:LinkButton>
                                                                    <asp:Label ID="Label20" runat="server" Visible="false" Text='<%# Bind("id")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblleaveid" runat="server" Text='<%# Bind("EmployeeLeaveTypeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="From Date" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblFrmDate" runat="server" Text='<%# Bind("fromdate","{0:MM/dd/yyyy}")%>'
                                                                        ForeColor="Black" OnClick="lblleavedate_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="To Date" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblToDate" runat="server" Text='<%# Bind("Todate","{0:MM/dd/yyyy}")%>'
                                                                        ForeColor="Black" OnClick="lblleavedate_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="DropDownList26" runat="server" Width="70px">
                                                                        <asp:ListItem Value="0">Pending</asp:ListItem>
                                                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                                                        <asp:ListItem Value="2">Rejected</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblapprovestatus" runat="server" Text='<%# Bind("Status")%>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <HeaderTemplate>
                                                                    <asp:ImageButton ID="lblheadte" runat="server" Height="20px" Width="20px" ToolTip="Note"
                                                                        ImageUrl="~/ShoppingCart/images/AppNote.png" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="lblnote" runat="server" Height="20px" Width="20px" OnClick="Appn_Click"
                                                                        ToolTip="Note" ImageUrl="~/ShoppingCart/images/AppNote.png" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Save" HeaderStyle-Width="25px">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btncom" runat="server" OnClick="lblrateLeave_Click" Text="Save" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" align="right">
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="border: 1px solid #000000; width: 50%" valign="top">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100%" valign="top">
                                                <asp:Panel ID="Panel11" runat="server" Height="250px" Width="100%" ScrollBars="Vertical">
                                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="grdgatepass" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                    Width="100%" EmptyDataText="No Record Found." GridLines="None" CssClass="mGrid"
                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="false"
                                                                    PageSize="6">
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Employee">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lblemp" runat="server" Text='<%#Bind("EmployeeName") %>' ForeColor="Black"
                                                                                    OnClick="lblempgate_Click"></asp:LinkButton>
                                                                                <asp:Label ID="Label19" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Party Name">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lblpn" runat="server" Text='<%#Bind("Compname") %>' ForeColor="Black"
                                                                                    OnClick="lblempgate_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Out Time">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbloutt" runat="server" Text='<%#Bind("ExpectedOutTime") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="In Time">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblintime" runat="server" Text='<%#Bind("ExpectedInTime") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="70px" HeaderText="Status">
                                                                            <%--  <EditItemTemplate>
                                                                            <asp:DropDownList ID="DropDownList18" runat="server">
                                                                                <asp:ListItem Value="1">Pending</asp:ListItem>
                                                                                <asp:ListItem Value="2">Approved</asp:ListItem>
                                                                                <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </EditItemTemplate>--%>
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="DropDownList18" runat="server" Width="70px">
                                                                                    <asp:ListItem Value="1">Pending</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Approved</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:Label ID="lblsapp" runat="server" Visible="false" Text='<%#Bind("Approved") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <HeaderTemplate>
                                                                                <asp:ImageButton ID="lblheadte" runat="server" Height="20px" Width="20px" ToolTip="Note"
                                                                                    ImageUrl="~/ShoppingCart/images/AppNote.png" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="lblnote" runat="server" Height="20px" Width="20px" OnClick="Appngt_Click"
                                                                                    ToolTip="Note" ImageUrl="~/ShoppingCart/images/AppNote.png" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Save" HeaderStyle-Width="25px">
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btncom" runat="server" OnClick="lblrategt_Click" Text="Save" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle CssClass="pgr" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%" align="right">
                                                <asp:LinkButton ID="LinkButton11" Text="More.." ForeColor="Black" runat="server"
                                                    OnClick="LinkButton2_Click"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div>
                    <asp:Panel ID="pnlempdev" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td style="width: 100%; background-color: #416271;" align="center">
                                    <asp:Label ID="Label53" runat="server" Text="Frequent Late Commers/Absentees" Font-Size="17px"
                                        ForeColor="White"></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #000000; width: 100%" valign="top">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    List of My Assistants deviating in past 30 days
                                                </label>
                                                <asp:Panel ID="panerldf" runat="server" Visible="false">
                                                    <label>
                                                        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Width="70px">
                                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="3" Value="3" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                            <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                            <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                        Times
                                                    </label>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlattla" runat="server" ScrollBars="Vertical">
                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        AllowPaging="true" PageSize="10" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Employee" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <a href='Employeeprofile.aspx?EmployeeMasterID=<%# Eval("EmployeeMasterID")%>' style="color: Black">
                                                                        <%#  Eval("EmployeeName")%></a>
                                                                    <asp:Label runat="server" ID="lblemployeeid" Text='<%# Eval("EmployeeMasterID") %>'
                                                                        Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Late Coming" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <a href='EmployeeAttendanceReportDetail.aspx?ID=1&EmployeeMasterID=<%# Eval("EmployeeMasterID")%>'
                                                                        style="color: Black">
                                                                        <%#  Eval("LateCome")%></a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Late Going" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <a href='EmployeeAttendanceReportDetail.aspx?ID=2&EmployeeMasterID=<%# Eval("EmployeeMasterID")%>'
                                                                        style="color: Black">
                                                                        <%#  Eval("LateGo")%></a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Early Coming" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <a href='EmployeeAttendanceReportDetail.aspx?ID=3&EmployeeMasterID=<%# Eval("EmployeeMasterID")%>'
                                                                        style="color: Black">
                                                                        <%#  Eval("EarlyCome")%></a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Early Going" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <a href='EmployeeAttendanceReportDetail.aspx?ID=4&EmployeeMasterID=<%# Eval("EmployeeMasterID")%>'
                                                                        style="color: Black">
                                                                        <%#  Eval("EarlyGo")%></a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Absense">
                                                                <ItemTemplate>
                                                                    <a href='EmployeeAttendanceReportDetail.aspx?ID=5&EmployeeMasterID=<%# Eval("EmployeeMasterID")%>'
                                                                        style="color: Black">
                                                                        <%#  Eval("Absent")%></a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
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
                                                        <asp:Label ID="Label21" Text="User Category" runat="server"></asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlusertype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlusertype_SelectedIndexChanged"
                                                            Width="150px">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label22" Text="Search" runat="server"></asp:Label>
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
                                                                        <asp:Label ID="Label23" runat="server" Text="User Type"></asp:Label>
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
                                                                        <asp:Label ID="Label24" runat="server" Text="Job Applied For"></asp:Label>
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
                                                                    <asp:CheckBox ID="HeaderChkbox" runat="server" OnCheckedChanged="HeaderChkbox_CheckedChanged"
                                                                        AutoPostBack="true" />
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
                                            <asp:Label ID="lblPartyType" Text="User Category" runat="server"></asp:Label>
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
                                                            <asp:Label ID="Label36" runat="server" Text="User Type"></asp:Label>
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
                                                            <asp:Label ID="Label434" runat="server" Text="Job Applied For"></asp:Label>
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
                                                        <asp:CheckBox ID="HeaderChkbox1" runat="server" AutoPostBack="true" OnCheckedChanged="HeaderChkbox1_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkParty123" runat="server" />
                                                        <asp:Label ID="Label17" runat="server" Text='<%# Eval("emailid") %>' Visible="false"></asp:Label>
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
                  <div>
                    <asp:Panel ID="pnlappnote" runat="server" Width="400px" CssClass="modalPopup">
                        <fieldset>
                            <legend></legend>
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:ImageButton ID="imgappnote" runat="server" CssClass="btnSubmit" ImageUrl="~/Account/images/closeicon.png"
                                            AlternateText="Close" CausesValidation="False" Height="16px" Width="16px"></asp:ImageButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Note
                                        </label>
                                        <label>
                                            <asp:TextBox ID="lblleaveappnotes" runat="server" TextMode="MultiLine" Height="104px"
                                                Width="300px"></asp:TextBox>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset></asp:Panel>
                    <asp:Button ID="brnapphi" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="pnlappnote" TargetControlID="brnapphi" CancelControlID="imgappnote">
                    </cc1:ModalPopupExtender>
                </div>
                <div>
                    <asp:Panel ID="pnlgpapp" runat="server" Width="400px" CssClass="modalPopup">
                        <fieldset>
                            <legend></legend>
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:ImageButton ID="imggpa" runat="server" CssClass="btnSubmit" ImageUrl="~/Account/images/closeicon.png"
                                            AlternateText="Close" CausesValidation="False" Height="16px" Width="16px"></asp:ImageButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Note
                                        </label>
                                        <label>
                                            <asp:TextBox ID="txtgpaapp" runat="server" TextMode="MultiLine" Height="104px" Width="300px"></asp:TextBox>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset></asp:Panel>
                    <asp:Button ID="btngpaap" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender6" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="pnlgpapp" TargetControlID="btngpaap" CancelControlID="imggpa">
                    </cc1:ModalPopupExtender>
                </div>
                <div>
                    <asp:Panel ID="Panel15" runat="server" BorderStyle="Solid" BorderWidth="5px" BackColor="WhiteSmoke"
                        BorderColor="#416271" Width="400px" Height="100px">
                        <table id="Table10" cellpadding="0" cellspacing="0" width="100%" align="center">
                            <tr>
                                <td align="right">
                                    <asp:ImageButton ID="ImageButton7" runat="server" CssClass="btnSubmit" ImageUrl="~/Account/images/closeicon.png"
                                        AlternateText="Close" CausesValidation="False" Height="16px" Width="16px"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label60" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <asp:Label ID="Label34" runat="server" Text="" Visible="false"></asp:Label>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="HiddenButton22as12aa" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender13" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel15" TargetControlID="HiddenButton22as12aa" CancelControlID="ImageButton7">
                    </cc1:ModalPopupExtender>
                </div>
                <asp:Panel ID="Panel16" runat="server" BackColor="LightGray" BorderColor="#416271"
                    Width="320px" Height="100px" BorderStyle="Solid" BorderWidth="10px">
                    <table id="Table6" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label11" runat="server" ForeColor="Black"> Are you leaving the office for the day ?</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnyes" runat="server" Text="Yes" OnClick="btnyes_Click" CssClass="btnSubmit" />
                                <asp:Button ID="btnignore" runat="server" Text="Cancel" OnClick="btnignore_Click1"
                                    CssClass="btnSubmit" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel16" TargetControlID="HiddenButton222" CancelControlID="btnignore">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="Panel7" runat="server" BackColor="LightGray" BorderColor="#416271"
                    Width="320px" Height="100px" BorderStyle="Solid" BorderWidth="10px">
                    <table id="Table5" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label41" runat="server" ForeColor="Black">Are you coming back from external visit ?</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="Button8" runat="server" Text="Yes" CssClass="btnSubmit" OnClick="Button8_Click" />
                                <asp:Button ID="Button9" runat="server" Text="No" CssClass="btnSubmit" OnClick="Button9_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="HiddenButton2223" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel7" TargetControlID="HiddenButton2223" CancelControlID="Button9">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="Panel12" runat="server" CssClass="GridPnl" Width="500px" BorderStyle="Solid"
                    BorderWidth="5px" BackColor="LightGray" BorderColor="#416271">
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
                                <asp:Label ID="Label13" runat="server" Text=" Add new Reminder"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%">
                                <label>
                                    <asp:Label ID="Label14" runat="server" Text="Date"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox1"
                                        ErrorMessage="*" ValidationGroup="11"> </asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td style="width: 50%">
                                <label>
                                    <asp:TextBox ID="TextBox1" runat="server" Width="70px"></asp:TextBox>
                                </label>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="TextBox1"
                                    TargetControlID="TextBox1">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <asp:Panel ID="pnlmetipanel" runat="server" Visible="false">
                                <td style="width: 50%">
                                    <asp:Label ID="Label15" runat="server" Text="Status"></asp:Label>
                                </td>
                                <td style="width: 50%">
                                    <asp:DropDownList ID="ddlstatus" runat="server" ValidationGroup="11" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </asp:Panel>
                        </tr>
                        <tr>
                            <td style="width: 50%">
                                <label>
                                    <asp:Label ID="Label16" runat="server" Text="Reminder Note"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtremindernote"
                                        ErrorMessage="*" ValidationGroup="11"> </asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td style="width: 50%">
                                <label>
                                    <asp:TextBox ID="txtremindernote" runat="server" TextMode="MultiLine" Width="350px"
                                        Height="150px"></asp:TextBox>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%">
                            </td>
                            <td style="width: 50%">
                                <asp:Button ID="Button4" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="Button4_Click"
                                    ValidationGroup="11" />
                                <asp:Button ID="Button5" runat="server" Text="Close" CssClass="btnSubmit" OnClick="Button5_Click" />
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
                <asp:Button ID="btngrt" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel12" TargetControlID="btngrt" runat="server" CancelControlID="ImageButton4">
                </cc1:ModalPopupExtender>
            </div>


                        <div>
            
                  <asp:Panel ID="Panel25" runat="server" BackColor="White" BorderColor="#999999" Width="620px"
                    Height="300px" BorderStyle="Solid" BorderWidth="10px">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td>
                                <table style="width: 100%; font-weight: bold; color: #000000;" bgcolor="#CCCCCC">
                                    <tr>
                                        <td>
                                            Documents
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="ImageButton2" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                OnClick="ImageButton3_Click111" Width="15px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlof" ScrollBars="Vertical" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView11" runat="server" AutoGenerateColumns="False" PageSize="20"
                                                    Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    EmptyDataText="No Record Found." OnRowCommand="GridView11_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Doc ID" SortExpression="DocumentId" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" ForeColor="Black" runat="server" Text='<%#Eval("DocumentId") %>'
                                                                    CommandName="View" HeaderStyle-HorizontalAlign="Left" CommandArgument='<%#Eval("DocumentId") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="DocumentDate" HeaderText="Date" HeaderStyle-Width="2%"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%" />
                                                        <asp:BoundField DataField="DocumentTitle" HeaderText="Title" HeaderStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="DocType" HeaderText="Cabinet-Drawer-Folder" HeaderStyle-HorizontalAlign="Left" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtenderAddnew" runat="server" BackgroundCssClass="modalBackground"
                    Enabled="True" PopupControlID="Panel25" TargetControlID="HiddenButton222">
                </cc1:ModalPopupExtender>
                <asp:Button ID="Button10" runat="server" Style="display: none" />
            
            </div>


        
  
  
    <%--     <asp:Panel ID="Pnlstatus2" runat="server" BackColor="LightGray" BorderColor="#416271"
                    Width="320px" Height="200px" BorderStyle="Solid" BorderWidth="10px">
                    <table id="Table4" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td width="50%">
                                <label>
                                <asp:Label ID="Label68" runat="server" Text=" Project Complete: "></asp:Label>
                                </label>
                            </td>
                            <td width="50%">
                               
                                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="false" 
                                    Text="Yes" />
                           
                             
                            </td>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                           <td width="10%" align="center">--%>
                                                        <%--<asp:Button ID="Button6" CssClass="btnSubmit" runat="server" Text="Add" OnClick="Button6_Click"
                                                            ValidationGroup="2"  />--%>
                                                    <%--        <asp:Button ID="Button11" CssClass="btnSubmit" runat="server" Text="Update" 
                                                            ValidationGroup="2" onclick="Button11_Click"  />
                                                              <asp:Button ID="Button12" CssClass="btnSubmit" runat="server" Text="Cancel" 
                                                            />


                                                    </td>
                        </tr>
                    </table>
                </asp:Panel>--%>





      
              
        <%--    </div>--%>
     
            <asp:Panel ID="Panel28" runat="server" Visible="true"  Width="100%" >


             <div style="position:relative;  margin: -8px 0px 0px 0px;  width:100%; background-color: #dddddd;" >
           
              <fieldset>
        <legend>
            <asp:Label ID="Label119" runat="server" Text="Add Progress Report"></asp:Label>
        </legend>
         <asp:Panel ID="pnl_licence" runat="server">
        
              <fieldset>
               <div style="margin-left: 1%">
                <asp:Label ID="lblstsmsg" runat="server" ForeColor="Red"></asp:Label>
                </div>
              <table width="70%" align="center"  >

       
                            <tr>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label62" runat="server" Text="Status Report: "> </asp:Label>
                                    </label>
                                    <%--<asp:CheckBox ID="ChkProDesc" AutoPostBack="true" runat="server" OnCheckedChanged="ChkProDesc_CheckedChanged" />--%>
                                </td>
                                <td width="70%">
                                    <label style="width:100%">
                                       
                                            <cc2:HtmlEditor ID="txtprogressjkjk"   runat="server" Visible="false"></cc2:HtmlEditor>
                                             <asp:TextBox ID="txtprogress" runat="server" Width="750px" Height="100px" TextMode="MultiLine"> </asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            
                             <tr>
                                <td>
                                    <label>
                                        Reporting Date: 
                                    </label>
                                </td>
                                <td colspan="1">
                                    <label>
                                        <asp:TextBox ID="txtreportingdate" runat="server" Width="100px"> </asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtreportingdate"
                                            PopupButtonID="txtreportingdate"  Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
                                   
                                   
                                       
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label63" runat="server" Text=" Project Complete: "></asp:Label>
                                    </label>
                                    
                                </td>
                                <td width="70%">
                                    <label>
                                    <asp:CheckBox ID="ChckBox1" AutoPostBack="true" runat="server" Text="Yes" oncheckedchanged="ChckBox1_CheckedChanged1" 
                                         />
                                        <%--<asp:TextBox ID="txtcompltdate" runat="server" Width="100px" Visible="false">
                                        </asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtcompltdate"
                                            PopupButtonID="ImageButtoncompl">
                                        </cc1:CalendarExtender>
                                    
                                        <asp:ImageButton ID="ImageButtoncompl" runat="server" ImageUrl="~/images/cal_actbtn.jpg" Visible="false"/>--%>
                                    </label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label>
                                    Reminder Date:
                                    </label>
                                </td>
                                <td colspan="1">
                                    <label>
                                        <asp:TextBox ID="txt_reminder" runat="server" Width="100px"> </asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_reminder" PopupButtonID="txt_reminder"  Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
                                   
                                   
                                       
                                    </label>
                                </td>
                            </tr>
                              <tr>
                                <td colspan="5">
                                    <label>
                                        <asp:Label ID="Label64" runat="server" Text="Upload Documents: "></asp:Label>
                                    </label>
                                    <asp:CheckBox ID="Chkupld" runat="server" AutoPostBack="True"  oncheckedchanged="Chkupld_CheckedChanged" Visible="False">
                                    </asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:Panel ID="Pnlstsup" runat="server" >
                                      <fieldset>
                                            <table width="80%" >
                                                <tr>
                                                    <td width="30%">
                                                        <label>
                                                            <asp:Label ID="Label67" runat="server" Text=" File Name"></asp:Label>
                                                            <asp:Label ID="Label68" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtststitle"
                                                                ErrorMessage="*" ValidationGroup="2"> </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtststitle" Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"  ValidationGroup="1"> </asp:RegularExpressionValidator>
                                                            <label>
                                                            <asp:TextBox ID="txtststitle" onKeydown="return mask(event)" MaxLength="30" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span10',30)" runat="server"></asp:TextBox>
                                                        </label>
                                                         <label>
                                                        Max <span ID="Span1">30</span>
                                                        <asp:Label ID="Label69" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td width="30%">
                                                        <asp:RadioButtonList ID="rdoselct" runat="server" AutoPostBack="True"
                                                            RepeatDirection="Horizontal" 
                                                            onselectedindexchanged="rdoselct_SelectedIndexChanged">
                                                            <asp:ListItem Value="1" Enabled ="true">Audio File</asp:ListItem>
                                                            <asp:ListItem Value="2">Other Files</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td width="25%">
                                                        <asp:Panel ID="Panel27" runat="server" Visible="false">
                                                            <label>
                                                                <%--<asp:Label ID="Label13" runat="server" Text="Other File"></asp:Label>--%>
                                                            <asp:FileUpload ID="FileUpload2" runat="server" />
                                                            </label>
                                                        </asp:Panel>
                                                        <asp:Panel ID="Pnlad" runat="server" Visible="false">
                                                            <label>
                                                                <asp:Label ID="Label70" runat="server" Text=" Audio File"></asp:Label>
                                                            </label>
                                                            <label>
                                                            
                                                                <asp:FileUpload ID="FileUpload3" runat="server" />
                                                            
                                                            </label>
                                                        </asp:Panel>
                                                    </td>
                                                    <td width="10%">
                                                        <%--<asp:Button ID="Button6" CssClass="btnSubmit" runat="server" Text="Add" OnClick="Button6_Click"
                                                            ValidationGroup="2"  />--%>
                                                            <asp:Button ID="Button11" CssClass="btnSubmit" runat="server" Text="Add" 
                                                            ValidationGroup="2" onclick="Button111_Click"  />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5">
                                                        <asp:GridView ID="gridstsFileAttach" runat="server" CssClass="mGrid" GridLines="Both"
                                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                            Width="70%" onrowcommand="gridstsFileAttach_RowCommand">
                                                            <Columns>
                                                              <%--  <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblstspdfurl" runat="server" Text='<%#Bind("PDFURL") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="URL" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblststitle" runat="server" Text='<%#Bind("Title") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Audio URL" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblstsaudiourl" runat="server" Text='<%#Bind("AudioURL") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Document File" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldoc" runat="server" Text='<%#Bind("Doc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:ButtonField CommandName="Delete1" ItemStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderText="Delete" ImageUrl="~/Account/images/delete.gif" Text="Delete" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="width: 100%" width="50%">
                                    <asp:Button ID="Button12" runat="server" CssClass="btnSubmit" 
                                        onclick="Button12_Click" Text="Submit" />
                                    <asp:Button ID="Button16" runat="server" CssClass="btnSubmit" 
                                        onclick="Button16_Click" Text="Cancel" />
                                </td>
                            
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                <div style="margin-left: 1%">
                <asp:Label ID="Label71" runat="server" ForeColor="Red"></asp:Label>
            </div>
                            </tr>
           
            </table>       
            </fieldset>           
             
      </asp:Panel> 


       </fieldset>
       </div>


            </asp:Panel>







  <asp:Button ID="btnsdsd" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender7" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel28" TargetControlID="btnsdsd" >
                </cc1:ModalPopupExtender>


















  
  
 









        </ContentTemplate>

                <Triggers> <%--<asp:PostBackTrigger ControlID="Button3" />--%>
           
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11" />
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button11"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="rdoselct"></asp:PostBackTrigger>

<asp:PostBackTrigger ControlID="txtprogress"></asp:PostBackTrigger>

        </Triggers>


















    </asp:UpdatePanel>
</asp:Content>
