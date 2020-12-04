<%@ Page Title="Attendance Rule Master" Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="Payrollrules.aspx.cs" Inherits="Payrollrules" %>

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

        function mask(evt, max_len) {



            if (evt.keyCode == 13) {

                return true;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }




        }

        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');

                alert("You have entered an invalid character");

            }




            counter = document.getElementById(id);

            if (txt1.value.length <= max_len) {
                remaining_characters = max_len - txt1.value.length;
                counter.innerHTML = remaining_characters;
            }


        }
        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
            }
        } 
    </script>

    <asp:UpdatePanel ID="pnnn1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="statuslable" ForeColor="Red" runat="server" Text="" Visible="False"></asp:Label>
                </div>
                <div style="clear: both;">
                    <br />
                </div>
                <asp:Panel ID="pnladd" runat="server" Visible="true">
                    <fieldset>
                        <legend>A - General Rules</legend>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblBusiness" runat="server" Text="Select Business"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlwarehouse"
                                            ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                    </label>
                                    <%--</td>
                                <td>--%>
                                    <label>
                                        <asp:DropDownList ID="ddlwarehouse" runat="server" ValidationGroup="1" Width="250px"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="Attend">
                                        <asp:Label ID="Label1" runat="server" Text="Select the employee which will have rights of Payroll Admin"></asp:Label>
                                    </label>
                                    <%-- </td>
                                <td>--%>
                                    <label>
                                        <asp:DropDownList ID="ddlemp" Visible="true" runat="server" Width="250px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <div style="clear: both;">
                        </div>
                    </fieldset>
                    <asp:Panel ID="pnlenb" runat="server">
                        <fieldset>
                            <legend>B - Rules Regarding Arrivals and Departures</legend>
                            <table width="100%">
                                <tr>
                                    <td valign="top">
                                        <label>
                                            1
                                        </label>
                                    </td>
                                    <td valign="top">
                                        <label class="Attend">
                                            <asp:Label ID="Label2" runat="server" Text="Payroll Calculation based on attendance."></asp:Label>
                                            <br />
                                            <asp:Label ID="Label4" runat="server" Text="you can set number of instances of entry or exit time deviation."></asp:Label>
                                            <br />
                                            <asp:Label ID="Label5" runat="server" Text="you can set a rule that when employee's no of deviation exceeds your set rule it may automatically consider any new deviation as half day leave or full day leave."></asp:Label>
                                            <br />
                                            <asp:Label ID="Label10" runat="server" Text="(If no of hours worked on the day of deviation is less than half day it would be considered full day leave. If no of hours worked on the day of deviation is more than half day it would be considered as half day leave.)"></asp:Label>
                                            <br />
                                            <asp:Label ID="Label6" runat="server" Text="In Your "></asp:Label>
                                            <asp:HyperLink ID="lblhp1" runat="server" Text="attendance rule," Target="_blank"
                                                ForeColor="Black" NavigateUrl="~/ShoppingCart/Admin/AttendenceRule.aspx"></asp:HyperLink>
                                            <asp:Label ID="Label7" runat="server" Text=" you have allowed employees to come late by "></asp:Label>
                                            <asp:Label ID="lbllateen" runat="server" ForeColor="Black" Text=""></asp:Label>
                                            <asp:Label ID="Label8" runat="server" Text="  minutes or go early by "></asp:Label>
                                            <asp:Label ID="lblearltmin" runat="server" ForeColor="Black" Text=""></asp:Label>
                                            <asp:Label ID="Label9" runat="server" Text=" minutes (Grace Period)"></asp:Label>
                                        </label>
                                        <label>
                                        </label>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td valign="top">
                                        <label>
                                        </label>
                                    </td>
                                    <td valign="top">
                                        <label class="Attend">
                                                 </label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td valign="top">
                                    </td>
                                    <td valign="top">
                                        <label>
                                            <%--   <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Visible="false" OnCheckedChanged="CheckBox1_CheckedChanged" />--%>
                                            <asp:Label ID="bbn" runat="server" Text="Would you like to use this half day type of rule when employees attendance deviation exceeds set no of instances in a pay period?"></asp:Label>
                                        </label>
                                        <div style="clear: both;">
                                        </div>
                                        <asp:RadioButtonList ID="rdru2" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                            OnSelectedIndexChanged="rdru2_SelectedIndexChanged">
                                            <asp:ListItem Text="Yes" Value="2"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="No" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <div style="clear: both;">
                                        </div>
                                        <asp:Panel ID="pnlconsider" runat="server" Visible="true">
                                            <div style="padding-left: 2%">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="gh" runat="server" Text="Allowed instances of deviations for employee as per payperiod"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtconsi"
                                                                    ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtconsi" runat="server" Width="45px" MaxLength="3" Visible="False">3</asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                                    FilterType="Custom, Numbers" TargetControlID="txtconsi" ValidChars="">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Panel ID="pnlpayperiod" runat="server" Visible="true">
                                                                <asp:GridView ID="grdpay" runat="server" EmptyDataText="Record Not Found." AllowSorting="false"
                                                                    AutoGenerateColumns="False" DataKeyNames="Id" GridLines="Both" CssClass="mGrid"
                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="220px">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Employees pay period" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblpname" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="No of deviation instances allowed">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtpaytype" runat="server" Width="45px" MaxLength="3" Text='<%# Eval("aspayruletime") %>'></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                                                    FilterType="Custom, Numbers" TargetControlID="txtpaytype" ValidChars="">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                                <asp:RequiredFieldValidator ID="Requirtor13" runat="server" ControlToValidate="txtpaytype"
                                                                                    ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                            </div>
                                        </asp:Panel>
                                        <div style="clear: both;">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <label>
                                            2
                                        </label>
                                    </td>
                                    <td colspan="2">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <label class="Attend">
                                                        Do you wish to pay your employee just for regular scheduled hours when his actual
                                                        timings falls within grace period allowed ? Yes
                                                        <br />
                                                        For eg. If your employees regular scheduled time is 9am to 5pm but on certain days he arrives at 9:05am and exist at 5:02pm,<br />
                                                         the attendance deviation here is within the grace period so, payroll will be calculated for full 8 hours.
                                                        <br />
                                                        It will ignore few minutes of deviation in arrival and departure.

                                                    </label>
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:CheckBox ID="checkbox" runat="server" Checked="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <label>
                                            3
                                        </label>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <label class="Attend">
                                                        Do you wish to require that all attendance records must be approved on daily basis
                                                        <br />
                                                        either by attendance admin or payoll admin for them to be included in payroll calculation
                                                        ? Yes
                                                    </label>
                                                </td>
                                                <td valign="bottom">
                                                    <label class="Attend">
                                                        <asp:CheckBox ID="chkgenralrule" TextAlign="Left" runat="server" AutoPostBack="false"
                                                            Text="" /></label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <div style="clear: both;">
                        </div>
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="C - Overtime Rules"></asp:Label></legend>
                            <div style="clear: both;">
                            </div>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <label class="Attend">
                                            How would you like to pay your employees for overtime ?
                                        </label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td valign="top">
                                                    <label>
                                                        1
                                                    </label>
                                                </td>
                                                <td>
                                                    <label class="Attend">
                                                        Would you like to set a rule to pay for this extra time/overtime only in the cases
                                                        where the overtime hours are approved by payroll admin?
                                                    </label>
                                                    <label class="Attend">
                                                    </label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkappsuper" runat="server" Checked="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            2
                                        </label>
                                    </td>
                                    <td>
                                        <label class="Attend">
                                            How do you pay overtime?
                                        </label>
                                        <label class="Attend">
                                            <asp:CheckBox ID="chkoverhowdo" runat="server" Checked="true" AutoPostBack="True"
                                                Visible="false" OnCheckedChanged="chkoverhowdo_CheckedChanged" />
                                        </label>
                                        <label>
                                            <asp:CheckBox ID="chkoverA" runat="server" Checked="true" Visible="false" /></label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td colspan="2">
                                        <asp:Panel ID="pnloverop2radio" runat="server">
                                            <table>
                                                <tr>
                                                    <td colspan="1" valign="top">
                                                        <asp:RadioButton ID="rd1" runat="server" AutoPostBack="true" GroupName="abc" OnCheckedChanged="rd1_CheckedChanged"
                                                            Text="" />
                                                    </td>
                                                    <td>
                                                        <label>
                                                            No extra remuneration paid for employee's working extra time. Employees paid ony
                                                            for the scheduled hours although he might have work for more hours on some days.</label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:RadioButton ID="rd2" runat="server" AutoPostBack="True" Checked="True" GroupName="abc"
                                                            OnCheckedChanged="rd2_CheckedChanged" Text="" />
                                                    </td>
                                                    <td>
                                                        <label>
                                                            Employees would be paid at the regular rate of the remuneration for extra time worked.</label>
                                                        <label>
                                                            <asp:DropDownList ID="ddlrempt2" runat="server" Visible="true" Width="150px">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="" valign="top">
                                                        <asp:RadioButton ID="rd3" runat="server" AutoPostBack="True" Text="" GroupName="abc"
                                                            OnCheckedChanged="rd3_CheckedChanged" />
                                                    </td>
                                                    <td>
                                                        <label>
                                                            Employees would be paid at special rate for the extra time worked during the pay
                                                                            period on the following basis :</label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="pnloverA" runat="server" Visible="false">
                                                            <table width="100%">
                                                               
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                    </td>
                                                                    <td>
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:Label ID="Label19" runat="server" Text="When extra time/overtime exceeds"></asp:Label>
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:TextBox ID="txthour" runat="server" Text="0" Width="40px" MaxLength="3"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="txtaddress_FilteredTextBoxExtender" runat="server"
                                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="txthour" ValidChars="">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txthour"
                                                                                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:Label ID="Label23" runat="server" Text="hour(s), the overtime hours will be paid at"></asp:Label>
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:TextBox ID="txtRate" runat="server" Text="100" Width="40px" MaxLength="3"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                                            FilterType="Custom, Numbers" TargetControlID="txtRate" ValidChars="-.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtRate"
                                                                                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                    </label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <label>
                                                                                                    <asp:Label ID="Label25" runat="server" Text="% of the remuneration"></asp:Label>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlramu"
                                                                                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                                </label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <label class="Attend">
                                                                                                    <asp:DropDownList ID="ddlramu" runat="server" Width="150px">
                                                                                                    </asp:DropDownList>
                                                                                                </label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
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
                        </fieldset></asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <div style="padding-left: 1%;" align="center">
                        <asp:Button ID="btnUpdate" CssClass="btnSubmit" runat="server" Visible="false" Text="Update"
                            OnClick="btnUpdate_Click" ValidationGroup="1" />
                        <asp:Button ID="btnEdit" CssClass="btnSubmit" runat="server" Visible="false" Text="Edit"
                            OnClick="btnEdit_Click" />
                        <asp:Button ID="Button5" CssClass="btnSubmit" runat="server" Text="Cancel" OnClick="Button5_Click" />
                    </div>
                    <div style="clear: both;">
                        <input style="width: 1px" id="Hidden1" type="hidden" name="hdnsortExp" runat="Server" />
                        <input style="width: 1px" id="Hidden2" type="hidden" name="hdnsortDir" runat="Server" />
                        <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                        <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                        <input style="width: 1px" id="hdnti" type="hidden" name="ti" runat="Server" />
                        <input style="width: 1px" id="hdnarithmatic" type="hidden" name="arithmatic" runat="Server" />
                    </div>
                </asp:Panel>
                <div style="clear: both;">
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlwarehouse" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click" />
            <%--  <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
