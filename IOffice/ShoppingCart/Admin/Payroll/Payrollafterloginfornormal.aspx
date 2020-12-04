<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="Payrollafterloginfornormal.aspx.cs" Inherits="Payrollafterloginfornormal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd" runat="server">
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
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" type="hidden" /></label>
                                </fieldset></asp:Panel>
                        </td>
                    </tr>
                </table>
                <div>
                    <table width="100%">
                        <tr>
                            <td style="width: 100%; background-color: #416271;" align="center">
                                <asp:Label ID="Label28" runat="server" Text="My Payroll Details" Font-Bold="False"
                                    Font-Size="17px" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #000000; width: 100%">
                                <asp:Panel ID="pnlGrdemp" runat="server" Visible="true" Width="100%" ScrollBars="None">
                                    <asp:GridView ID="grdallemp" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                        EmptyDataText="No Record Found." PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        CssClass="mGrid" Width="100%" DataKeyNames="SalId" OnSorting="grdallemp_Sorting"
                                        HeaderStyle-HorizontalAlign="Left" OnRowCreated="grdallemp_RowCreated" ShowFooter="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Pay period" SortExpression="EmployeeName" Visible="true"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblempname" runat="server" Text='<%#Bind("EmployeeName") %>'
                                                        OnClick="lblfe1_Click" ForeColor="Black"></asp:LinkButton><asp:Label ID="lblEmployeeId"
                                                            runat="server" Text='<%#Bind("EmployeeId") %>' Visible="false"></asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remu" SortExpression="Remorig" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrem" runat="server" Text='<%#Bind("Remorig") %>'></asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/Period" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="115px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblrate" runat="server" Text='<%# Bind("remrate") %>' OnClick="lblrate_Click"
                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblactunit" runat="server" Text='<%# Bind("Actunit","{0:n}") %>'
                                                        OnClick="lblActunit_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfootremunit" runat="server" Font-Size="15px" ForeColor="Black"
                                                        Font-Bold="true"></asp:Label></FooterTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount" SortExpression="remamt" Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblremamt" runat="server" Text='<%# Bind("remamt","{0:n}") %>'
                                                        OnClick="lblSlip_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfootremamt" runat="server" Font-Size="15px" ForeColor="Black" Font-Bold="true"></asp:Label></FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other Rem" SortExpression="Remother" Visible="true"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblotherrem" runat="server" Text='<%# Bind("Remother","{0:n}") %>'
                                                        OnClick="lblSlip_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfoototherrem" runat="server" Font-Size="15px" ForeColor="Black"
                                                        Font-Bold="true"></asp:Label></FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remuneration" SortExpression="Remtotal" Visible="true"
                                                ItemStyle-BackColor="#ccffff" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbltotrem" runat="server" Text='<%# Bind("Remtotal","{0:n}") %>'
                                                        OnClick="lblSlip_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfootRemtotal" runat="server" Font-Size="15px" ForeColor="Black"
                                                        Font-Bold="true"></asp:Label></FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle BackColor="#CCFFFF" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Non-Govt Dedu" SortExpression="Ded1" Visible="true"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblded1" runat="server" Text='<%# Bind("Ded1","{0:n}") %>' OnClick="lblSlip_Click"
                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfootnongovttotded" runat="server" Font-Size="15px" ForeColor="Black"
                                                        Font-Bold="true"></asp:Label></FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Govt Dedu" SortExpression="Ded2" Visible="true"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblded2" runat="server" Text='<%#Bind("Ded2","{0:n}") %>' OnClick="lblSlip_Click"
                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfootgovtotalded" runat="server" Font-Size="15px" ForeColor="Black"></asp:Label></FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net" SortExpression="remnetamt" Visible="true"
                                                ItemStyle-BackColor="#99ccff" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblnetamt" runat="server" Text='<%# Bind("remnetamt","{0:n}") %>'
                                                        OnClick="lblSlip_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfootnetpay" runat="server" Font-Size="15px" ForeColor="Black" Font-Bold="true"></asp:Label></FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle BackColor="#99CCFF" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Paid/<br>Unpaid" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpaidamt" runat="server" Text='<%#Bind("Paid") %>'></asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" Visible="false" HeaderStyle-Width="20px"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinedit" runat="server" CommandArgument='<%# Bind("EmployeeId") %>'
                                                        CommandName="Edit1" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <PagerStyle CssClass="pgr" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table id="subinnertbl" width="100%">
                        <tr>
                            <td style="width: 50%; background-color: #416271;" align="center">
                                <asp:Label ID="Label1" runat="server" Text="My Attendance" Font-Bold="False" Font-Size="17px"
                                    ForeColor="White"></asp:Label>
                            </td>
                            <td style="width: 50%; background-color: #416271;" align="center">
                                <asp:Label ID="Label12" runat="server" Text="My Daily Hours in Current Payperiod"
                                    Font-Bold="False" Font-Size="17px" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #000000; width: 50%;" valign="top">
                                <asp:Panel ID="Panel1" runat="server" Width="100%">
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
                                </asp:Panel>
                            </td>
                            <td style="border: 1px solid #000000; width: 50%" valign="top">
                                <asp:Panel ID="Panel2" runat="server" Width="100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnldy" runat="server" Visible="true">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label9" runat="server" Text="Hours to date"></asp:Label>
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
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
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
                                            <asp:LinkButton ID="lblexvis" Text="Add Leave Request" ForeColor="White" runat="server"
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
                                                <asp:Label ID="Label35" runat="server" Text="Email ID">
                                                </asp:Label>
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
                                                            <asp:LinkButton ID="LinkButton7" Text="More.." runat="server" CssClass="btnSubmit"
                                                                OnClick="taskmore_Click1" ForeColor="Black"></asp:LinkButton>
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
                    <asp:Panel ID="pblpayrollemp" runat="server">
                        <table width="100%">
                            <tr>
                                <td style="width: 100%; background-color: #416271;" align="center">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label32" runat="server" Text="Payroll Details of My Assistants for the Pay Period"
                                                    Font-Bold="False" Font-Size="17px" ForeColor="White"></asp:Label>
                                            </td>
                                            <td style="width: 05px;">
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpayperiod_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #000000; width: 100%" valign="top">
                                    <asp:Panel ID="Panel8" runat="server" Visible="true" Width="100%" ScrollBars="Vertical">
                                        <asp:GridView ID="grdsupersala" runat="server" AutoGenerateColumns="False" AllowSorting="false"
                                            EmptyDataText="No Record Found." PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            CssClass="mGrid" Width="100%" DataKeyNames="SalId" HeaderStyle-HorizontalAlign="Left"
                                            OnRowCreated="grdsupersala_RowCreated" ShowFooter="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Employee" SortExpression="EmployeeName" Visible="true"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblempname" runat="server" Text='<%#Bind("EmployeeName") %>'
                                                            OnClick="lblfe1super_Click" ForeColor="Black"></asp:LinkButton><asp:Label ID="lblEmployeeId"
                                                                runat="server" Text='<%#Bind("EmployeeId") %>' Visible="false"></asp:Label></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remu" SortExpression="Remorig" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrem" runat="server" Text='<%#Bind("Remorig") %>'></asp:Label></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate/Period" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="115px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblrate" runat="server" Text='<%# Bind("remrate") %>' OnClick="lblratesuper_Click"
                                                            ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblactunit" runat="server" Text='<%# Bind("Actunit","{0:n}") %>'
                                                            OnClick="lblActunitsuper_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfootremunit" runat="server" Font-Size="15px" Font-Bold="true" ForeColor="Black"></asp:Label></FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" SortExpression="remamt" Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblremamt" runat="server" Text='<%# Bind("remamt","{0:n}") %>'
                                                            OnClick="lblSlipsuper_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfootremamt" runat="server" Font-Size="15px" Font-Bold="true" ForeColor="Black"></asp:Label></FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Other Rem" SortExpression="Remother" Visible="true"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblotherrem" runat="server" Text='<%# Bind("Remother","{0:n}") %>'
                                                            OnClick="lblSlipsuper_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfoototherrem" runat="server" Font-Size="15px" Font-Bold="true"
                                                            ForeColor="Black"></asp:Label></FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total" SortExpression="Remtotal" Visible="true" ItemStyle-BackColor="#ccffff"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbltotrem" runat="server" Text='<%# Bind("Remtotal","{0:n}") %>'
                                                            OnClick="lblSlipsuper_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfootRemtotal" runat="server" Font-Size="15px" Font-Bold="true"
                                                            ForeColor="Black"></asp:Label></FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle BackColor="#CCFFFF" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Non-Govt Dedu" SortExpression="Ded1" Visible="true"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblded1" runat="server" Text='<%# Bind("Ded1","{0:n}") %>' OnClick="lblSlipsuper_Click"
                                                            ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfootnongovttotded" runat="server" Font-Size="15px" Font-Bold="true"
                                                            ForeColor="Black"></asp:Label></FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Govt Dedu" SortExpression="Ded2" Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblded2" runat="server" Text='<%#Bind("Ded2","{0:n}") %>' OnClick="lblSlipsuper_Click"
                                                            ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfootgovtotalded" runat="server" Font-Size="15px" Font-Bold="true"
                                                            ForeColor="Black"></asp:Label></FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net" SortExpression="remnetamt" Visible="true" ItemStyle-BackColor="#99ccff"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblnetamt" runat="server" Text='<%# Bind("remnetamt","{0:n}") %>'
                                                            OnClick="lblSlipsuper_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfootnetpay" runat="server" Font-Size="15px" ForeColor="Black" Font-Bold="true"></asp:Label></FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle BackColor="#99CCFF" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <PagerStyle CssClass="pgr" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div>
                    <asp:Panel ID="pnlsupperatt" runat="server">
                        <table  width="100%">
                            <tr>
                                <td style="width: 50%; background-color: #416271;" align="center">
                                    <asp:Label ID="lbltodaypres" runat="server" Text="Today's Presence of My Assistants" Font-Size="17px"
                                        ForeColor="White"></asp:Label>
                                </td>
                                <td style="width: 50%; background-color: #416271;" align="center">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label33" runat="server" Text="Today's Absense of My Assistants" Font-Size="17px" ForeColor="White"></asp:Label>
                                            </td>
                                            <td style="width: 05px;">
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Visible="false" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
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
                                                    <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" 
                                                        Width="100%" PageSize="5" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
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
                                                            <asp:TemplateField HeaderText="Reason" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="linking" runat="server" ForeColor="Black" CommandName="View"
                                                                        Text='<%# Eval("Reason") %>' CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                                                    <%--Text='<%# Eval("Late") %>'--%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Absense since<br>no. of days" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lbllategone" Text='<%# Eval("Days") %>'></asp:Label>
                                                                    <%--Text='<%# Eval("Late") %>'--%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Absense in<br>past 30 days" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblearlycome" Text='<%# Eval("Absent") %>'></asp:Label>
                                                                    <%--Text='<%# Eval("Early") %>'--%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
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
                    <table  width="100%">
                        <tr>
                            <td style="width: 50%; background-color: #416271;" align="center">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="right" >
                                            <asp:Label ID="Label42" runat="server" Text="List of Leave Requests from My Assistants" Font-Size="17px"
                                                ForeColor="White"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 150px;">
                                            <asp:LinkButton ID="LinkButton2" Text="Add Leave Request" ForeColor="White" runat="server"
                                                OnClick="LinkButton1_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; background-color: #416271;" align="center">
                                <asp:Label ID="Label51" runat="server" Text="List of Gate Pass Request from My Assistants" Font-Size="17px"
                                    ForeColor="White"></asp:Label>
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
                                                    AlternatingRowStyle-CssClass="alt" AllowPaging="false" PageSize="6" >
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
                                                        <asp:TemplateField 
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderTemplate>
                                                              <asp:ImageButton ID="lblheadte" runat="server" Height="20px" Width="20px" 
                                                                    ToolTip="Note" ImageUrl="~/ShoppingCart/images/AppNote.png" />
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
                                                                PageSize="6" >
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
                                                                     <asp:TemplateField 
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderTemplate>
                                                              <asp:ImageButton ID="lblheadte" runat="server" Height="20px" Width="20px" 
                                                                    ToolTip="Note" ImageUrl="~/ShoppingCart/images/AppNote.png" />
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
                                                            <asp:LinkButton ID="LinkButton11" Text="More.." ForeColor="Black" runat="server" OnClick="LinkButton2_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                </div>
                
                    <div>   <asp:Panel ID="pnlempdev" runat="server" Width="100%">
                    <table  width="100%">
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
                                                        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                                                            Width="70px">
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
                    <asp:Panel ID="pnlappnote" runat="server"  Width="400px" CssClass="modalPopup">
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
                           <asp:TextBox ID="lblleaveappnotes" runat="server" TextMode="MultiLine" 
                                   Height="104px" Width="300px"></asp:TextBox>
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
                    <asp:Panel ID="pnlgpapp" runat="server"  Width="400px" CssClass="modalPopup">
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
                           <asp:TextBox ID="txtgpaapp" runat="server" TextMode="MultiLine" 
                                   Height="104px" Width="300px"></asp:TextBox>
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
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel12" TargetControlID="HiddenButton2223" runat="server" CancelControlID="ImageButton4">
                </cc1:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
