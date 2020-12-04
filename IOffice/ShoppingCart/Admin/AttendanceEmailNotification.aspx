<%@ Page Title="Attendance Rule Master" Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="AttendanceEmailNotification.aspx.cs" Inherits="AttendanceEmailNotification" %>

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
        function mask(evt, max_len) {



            if (evt.keyCode == 13) {

                return true;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }




        }
        //        function Setlbl() {
        //            if (document.getElementById('<%= chkemailnotif.ClientID %>').checked == true) {
        //                document.getElementById('<%=pnlemnot.ClientID%>').disabled = false;
        //            }
        //            else {
        //                document.getElementById('<%=pnlemnot.ClientID%>').disabled = true;

        //            }
        //        }
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
                <asp:Panel ID="pnladd" runat="server" Visible="false">
                    <fieldset>
                        <legend>Attendance Email Rules </legend>
                        <asp:Panel ID="pnlenb" runat="server">
                            <fieldset>
                                <label>
                                    <asp:Label ID="lblBusiness" runat="server" Text="Business Name : "></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlwarehouse"
                                        ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlwarehouse" runat="server" ValidationGroup="1" Width="250px"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <%-- <label>
                                <asp:Label ID="Label20" runat="server" Text="Senior Employee/Supervisor/Admin for all Attendance Matter "></asp:Label>
                            </label>--%>
                                <div style="clear: both;">
                                </div>
                            </fieldset>
                            <asp:Panel ID="pnlrulea" runat="server" Visible="false">
                                <fieldset>
                                    <legend>A - General Rules</legend>
                                    <table width="100%">
                                        <tr>
                                            <td valign="top">
                                                <label>
                                                    1</label>
                                            </td>
                                            <td valign="top">
                                                <label class="Attend">
                                                    <asp:Label ID="Label20" runat="server" Text="All the Popups for user Feedback,user Informataion,unique attendance process will remain open for "></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="TextBox3" runat="server" Visible="true" Width="45px" Text="15" MaxLength="3"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="TextBox3"
                                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                                        FilterType="Custom, Numbers" TargetControlID="TextBox3" ValidChars="">
                                                    </cc1:FilteredTextBoxExtender>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label20as" runat="server" Text="seconds."></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <label>
                                                    2
                                                </label>
                                            </td>
                                            <td valign="top">
                                                <label>
                                                    <asp:Label ID="Label56" runat="server" Text="Select the Employee which will have rights of Attendance Admin" />
                                                </label>
                                                <%--                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlemp"
                                                ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                <label>
                                                    <asp:DropDownList ID="ddlemp" runat="server" Width="250px">
                                                    </asp:DropDownList>
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <label>
                                                    <asp:Label ID="Label59" runat="server" Text="(The Attendance Admin will have rights to approve the rules for attendance and approve all attendance deviations)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <label>
                                                    3
                                                </label>
                                            </td>
                                            <td valign="top">
                                                <label class="Attend">
                                                    <asp:Label ID="Label15" runat="server" Text="The system allows IN and OUT entry using 'Attendance by click for entry/Exit' without any further verification like asking for password of the attendee."></asp:Label>
                                                </label>
                                                <br />
                                                <label class="Attend">
                                                    <asp:Label ID="Label44" runat="server" Text="Would you like to set rule that system asks for password when attendee uses that form ?"></asp:Label>
                                                </label>
                                                <label class="Attend">
                                                    <asp:CheckBox ID="chkallowedempname" Checked="true" runat="server" Text="" />
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <label>
                                                    4
                                                </label>
                                            </td>
                                            <td valign="top">
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="3">
                                                            <label class="Attend">
                                                                <asp:Label ID="Label16" runat="server" Text="Do you wish to allow multiple entry and exit of the employee in Attendance by Employee Number , Attendance by Employee UserID and Attendance by click of entry exit"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label4" runat="server" Text="The System by default takes the first entry as 'IN Entry' for the day and it takes 2nd entry as 'OUT Entry' for the day."></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label29" runat="server" Text="However you can set the rule that system allows multiple entries and the latest entry done in a day as 'OUT Entry' for the day"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" style="width: 250px;" colspan="3">
                                                            <asp:CheckBox ID="chkinoutday" Checked="true" runat="server" TextAlign="Left" Text="Do you wish to set this rule for latest entry as OUT Entry ?" />
                                                            <%--<asp:Label ID="Label32" runat="server" Text="" Width="500px"></asp:Label>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <label>
                                                    5
                                                </label>
                                            </td>
                                            <td valign="top">
                                                <label class="Attend">
                                                    <asp:Label ID="Label34" runat="server" Text="Do you wish to block a user from making another attendance entry for "></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txtblackin" runat="server" Visible="true" Width="45px" Text="20"
                                                        MaxLength="2"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValator14" runat="server" ControlToValidate="txtblackin"
                                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="FilteextBoxExtender11" runat="server" Enabled="True"
                                                        FilterType="Custom, Numbers" TargetControlID="txtblackin" ValidChars="">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:RangeValidator ID="rds" MinimumValue="0" MaximumValue="59" Type="Integer" ControlToValidate="txtblackin"
                                                        Display="Dynamic" runat="server" ErrorMessage="Only 0 to 59 minits" SetFocusOnError="true"></asp:RangeValidator>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label35" runat="server" Text=" minutes after he has checked in / checked out?"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label31" runat="server" Text="Yes"></asp:Label>
                                                </label>
                                                <label class="Attend">
                                                    <asp:CheckBox ID="chkblachinout" Checked="true" runat="server" Text="" />
                                                </label>
                                            </td>
                                        </tr>
                                        <asp:Panel ID="panelmulti" runat="server" Visible="false">
                                            <tr>
                                                <td valign="top">
                                                    <label>
                                                        9
                                                    </label>
                                                </td>
                                                <td valign="top">
                                                    <label class="Attend">
                                                        <asp:Label ID="Label2" runat="server" Text="The system requires that payroll admin approves daily the attendance records of everyone in the business. Do you wish to override the process of daily attendance verification by admin?"></asp:Label>
                                                        <asp:CheckBox ID="chkgenralrule" TextAlign="Left" runat="server" AutoPostBack="True"
                                                            Text="" OnCheckedChanged="chkgenralrule_CheckedChanged" />
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                </td>
                                                <td>
                                                    <asp:Panel ID="pnlattapmail" runat="server" Visible="false">
                                                        <label class="Attend">
                                                            <asp:CheckBox ID="chkadminmailapprove" TextAlign="Left" runat="server" AutoPostBack="false"
                                                                Text="" />
                                                            <asp:Label ID="Label53" runat="server" Text="Send daily attendance report to payroll admin for verification by email"></asp:Label>
                                                        </label>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                            <asp:Panel ID="pnlruleb" runat="server" Visible="false">
                                <fieldset>
                                    <legend>B - Deviation Rules</legend>
                                    <table width="100%">
                                        <tr>
                                            <td valign="top">
                                                <label>
                                                    1
                                                </label>
                                            </td>
                                            <td valign="top">
                                                <label class="Attend">
                                                    <asp:Label ID="lblmmx" runat="server" Text="Acceptable grace period for in / out time (minutes):" />
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        <asp:Label ID="Label6" runat="server" Text="For In Time:"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                                            ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="TextBox1" runat="server" Width="45px" MaxLength="2">15</asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender111" runat="server"
                                                            Enabled="True" TargetControlID="TextBox1" ValidChars="0147852369">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label7" runat="server" Text="For Out Time:"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox2"
                                                            ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="TextBox2" runat="server" Width="45px" MaxLength="2">15</asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                            TargetControlID="TextBox2" ValidChars="0147852369">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <label>
                                                    2
                                                </label>
                                            </td>
                                            <td valign="top">
                                                <label class="Attend">
                                                    <%--   <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Visible="false" OnCheckedChanged="CheckBox1_CheckedChanged" />--%>
                                                    <asp:Label ID="bbn" runat="server" Text="Do you wish to consider any in time/out time entries within the grace period as acceptable timings?"></asp:Label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <asp:RadioButtonList ID="rdru2" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                        OnSelectedIndexChanged="rdru2_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Text="yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="yes, but only for certain instances per set period" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </label>
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
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Panel ID="pnlpayperiod" runat="server" Visible="true">
                                                                        <asp:GridView ID="grdpay" runat="server" EmptyDataText="Record Not Found." AllowSorting="false"
                                                                            AutoGenerateColumns="False" DataKeyNames="Id" GridLines="Both" CssClass="mGrid"
                                                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="220px">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Type of Pay Period" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblpname" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="60px" HeaderText="Deviations Allowed">
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
                                                        <%-- <label>
                                                    <asp:DropDownList ID="ddlpertype" runat="server" Width="120px">
                                                        <asp:ListItem Text="Week" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Semi-month" Value="2"></asp:ListItem>
                                                        <asp:ListItem  Selected="True" Text="Month" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Quarter-year" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="Semi-year" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="Year" Value="6"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </label>--%>
                                                        <div style="clear: both;">
                                                        </div>
                                                        <label>
                                                            <asp:Label ID="Label5" runat="server" Text="If exceeded the above limit:"></asp:Label>
                                                        </label>
                                                        <asp:Panel ID="pnlkhhh" runat="server" Visible="false">
                                                            <div style="clear: both;">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <label class="Attend">
                                                                                <asp:CheckBox ID="chkexedlimit2" runat="server" Checked="true" />
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label36" runat="server" Text="In case of employee, pay the employee only for the actual hours worked during the batch hours"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </asp:Panel>
                                                        <div style="clear: both;">
                                                            <asp:Panel ID="pasdasdas" runat="server" Visible="false">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <label class="Attend">
                                                                                <asp:CheckBox ID="chkpenaliseemp" runat="server" />
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label42" runat="server" Text="Penalize employee"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label49" runat="server" Text="$"></asp:Label>
                                                                            </label>
                                                                            <label>
                                                                                <asp:TextBox ID="txtpanliseemp" runat="server" MaxLength="3" Width="45px">0</asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFiidator5" runat="server" ControlToValidate="txtpanliseemp"
                                                                                    ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                                                    FilterType="Custom, Numbers" TargetControlID="txtpanliseemp" ValidChars="">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label43" runat="server" Text=" for each exceeded instance over the set limit."></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <div style="clear: both;">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <label>
                                                    3
                                                </label>
                                            </td>
                                            <td valign="top">
                                                <label class="Attend">
                                                    Attendance deviations (Coming late or exiting early)
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <label class="Attend">
                                                    A)
                                                    <asp:CheckBox ID="chkdeviationreason" runat="server" AutoPostBack="false" Text="" />
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label10" runat="server" Text="Ask the employee for the reason for attendance deviation when number of instance exceeds "></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txtreason" runat="server" Width="45px" MaxLength="3">5</asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtreason"
                                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                        FilterType="Custom, Numbers" TargetControlID="txtreason" ValidChars="">
                                                    </cc1:FilteredTextBoxExtender>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label17" runat="server" Text="per pay period." Visible="true"></asp:Label>
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <label class="Attend">
                                                    B)
                                                    <asp:CheckBox ID="chkempinstance" runat="server" AutoPostBack="false" />
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label11" runat="server" Text="Require employee to obtain approval of Attendance Admin when attendance deviation exceeds "></asp:Label>
                                                    <br />
                                                    (Please Note : You have to enter the number which is more than subpoint A above.)
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txtempapproal" runat="server" Visible="true" Width="45px" MaxLength="3">5</asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtempapproal"
                                                        ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtempapproal"
                                                        SetFocusOnError="true" Operator="GreaterThan" ControlToCompare="txtreason" ValidationGroup="1"></asp:CompareValidator>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                        FilterType="Custom, Numbers" TargetControlID="txtempapproal" ValidChars="">
                                                    </cc1:FilteredTextBoxExtender>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label18" runat="server" Text="Instance in a given pay period." Visible="true"></asp:Label>
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <label class="Attend">
                                                    C)
                                                    <asp:CheckBox ID="chkMakeEntry" runat="server" AutoPostBack="false" Text="" />
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label12" runat="server" Text="Restrict the employee from recording any attendance when the attendance deviations exceeds "></asp:Label>
                                                    <br />
                                                    (Please Note : You have to enter the number which is more than subpoint B above.)
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txtEntry" runat="server" Visible="true" Width="45px" MaxLength="3">5</asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEntry"
                                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtEntry"
                                                        SetFocusOnError="true" Operator="GreaterThan" ControlToCompare="txtempapproal"
                                                        ValidationGroup="1"></asp:CompareValidator>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                        FilterType="Custom, Numbers" TargetControlID="txtEntry" ValidChars="">
                                                    </cc1:FilteredTextBoxExtender>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label21" runat="server" Text=" instances per pay period." Visible="true"></asp:Label>
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <label class="Attend">
                                                    D)
                                                    <asp:CheckBox ID="chkgreatertime" Checked="true" runat="server" Text="" />
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label9" runat="server" Text="Require Attendance Admin approval if out time is greater than "> </asp:Label>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txtouttimegreater" runat="server" Visible="true" Width="45px" Text="0"
                                                        MaxLength="3"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtouttimegreater"
                                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </label>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                    FilterType="Custom, Numbers" TargetControlID="txtouttimegreater" ValidChars="">
                                                </cc1:FilteredTextBoxExtender>
                                                <label>
                                                    <asp:Label ID="Label14" runat="server" Text=" hours past the schedule exit time."></asp:Label>
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <label class="Attend">
                                                    E)
                                                    <asp:CheckBox ID="chkerlateentry" Checked="true" runat="server" Text="" />
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label8" runat="server" Text="Restrict the employee from recording any early check in or late check out by "></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txtallowusereg" runat="server" Visible="true" Width="45px" Text="6"
                                                        MaxLength="3"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequireieldValidator5" runat="server" ControlToValidate="txtallowusereg"
                                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                                        FilterType="Custom, Numbers" TargetControlID="txtallowusereg" ValidChars="">
                                                    </cc1:FilteredTextBoxExtender>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label22" runat="server" Text=" Hours."></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label13" runat="server" Text="(i.e. if any employee's schedule time is 9 AM to 3 PM, and rule is set for 2 hours here, He can not make any checkin entry prior to 7 AM and check out entry past 5 PM)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                            <fieldset>
                                <legend>Email Notifications</legend>
                                <table width="100%">
                                    <tr>
                                        <td valign="top">
                                            <label>
                                                1
                                            </label>
                                        </td>
                                        <td valign="top">
                                            <table>
                                                <tr>
                                                    <td colspan="5">
                                                        <label class="Attend">
                                                            <asp:Label ID="Label26" runat="server" Text="Do you wish to set a rule that the system generates a notification e-mail when an attendee checks in/out earlier/later than the standard check in/out time by"></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5">
                                                        <label>
                                                            <asp:TextBox ID="txtemailnotific" runat="server" Visible="true" Width="45px" Text="6"
                                                                MaxLength="3"></asp:TextBox>
                                                        </label>
                                                        <label>
                                                            <asp:RequiredFieldValidator ID="RequiredFielidator5" runat="server" ControlToValidate="txtemailnotific"
                                                                ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="txtemailnotific" ValidChars="">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="Label27" runat="server" Text=" minitues ? "></asp:Label>
                                                        </label>
                                                        <asp:CheckBox ID="chkemailnotif" Checked="true" runat="server" Text="yes" AutoPostBack="true"
                                                            OnCheckedChanged="chkemailnotif_CheckedChanged" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5">
                                                        <asp:Panel ID="pnlemnot" Visible="false" runat="server">
                                                            <label>
                                                                Please send Notification Email to
                                                            </label>
                                                            <asp:CheckBox ID="chlemailsuper" Checked="true" runat="server" Text="" />
                                                            <label>
                                                                <asp:Label ID="Label28" runat="server" Text=" supervisor"></asp:Label>
                                                            </label>
                                                            <asp:CheckBox ID="chkemaileppadd" Checked="true" runat="server" Text="" />
                                                            <label>
                                                                <asp:Label ID="Label33" runat="server" Text=" attendance admin"></asp:Label>
                                                            </label>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <label>
                                                2
                                            </label>
                                        </td>
                                        <td valign="top">
                                            <label class="Attend">
                                                <asp:Label ID="Label46" runat="server" Text=" The system allows to automatically send daily attendance report to selected employees."></asp:Label>
                                                <br />
                                                <asp:Label ID="Label30" runat="server" Text="  Do you wish to set such rule to  auto email Daily attendance reports ?"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label47" runat="server" Text="Yes"></asp:Label>
                                            </label>
                                            <label class="Attend">
                                                <asp:CheckBox ID="chkattreport" Checked="true" runat="server" Text="" OnCheckedChanged="chkattreport_CheckedChanged"
                                                    AutoPostBack="True" />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" colspan="2">
                                            <asp:Panel ID="pnlattrepo" runat="server" Visible="false">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 3%;">
                                                        </td>
                                                        <td valign="top" style="width: 3%;">
                                                            <label>
                                                                A.
                                                            </label>
                                                        </td>
                                                        <td valign="top" style="width: 20%;" colspan="2">
                                                            <label class="Attend">
                                                                Select when you wish to send this report.
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 3%;">
                                                        </td>
                                                        <td valign="top" style="width: 3%;">
                                                        </td>
                                                        <td valign="top" style="width: 20%;">
                                                            <label>
                                                                Send
                                                            </label>
                                                            <label class="Attend">
                                                                <asp:TextBox ID="txtdrreport" runat="server" Visible="true" Width="45px" Text="0"
                                                                    MaxLength="2"></asp:TextBox>
                                                                <asp:RangeValidator ID="RangeValidator1" MinimumValue="0" MaximumValue="59" ErrorMessage="Only 0 to 59 minits"
                                                                    Display="Dynamic" Type="Integer" ControlToValidate="txtdrreport" runat="server"
                                                                    SetFocusOnError="true"></asp:RangeValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtdrreport"
                                                                    ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                                                    FilterType="Custom, Numbers" TargetControlID="txtdrreport" ValidChars="">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </label>
                                                            <label class="Attend">
                                                                Minutes after
                                                            </label>
                                                        </td>
                                                        <td style="width: 74%;">
                                                            <label class="radiobuttonlabel">
                                                                <asp:RadioButtonList ID="rddreport" runat="server">
                                                                    <asp:ListItem Text="Start and end of each batch" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="End of each batch" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="End of last batch " Value="3" Selected="True"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 3%;">
                                                        </td>
                                                        <td>
                                                            <label>
                                                                B.
                                                            </label>
                                                        </td>
                                                        <td valign="top" colspan="3">
                                                            <label class="Attend">
                                                                Select recipients of this report
                                                            </label>
                                                            <label class="Attend">
                                                                <asp:CheckBox ID="chkrecreportsuper" runat="server" Checked="true" />
                                                                <asp:Label ID="dfsg" runat="server" Text="Supervisor of any Employee"></asp:Label>
                                                            </label>
                                                            <label class="Attend">
                                                                <asp:CheckBox ID="chkrecreportatteadmin" runat="server" Checked="true" />
                                                                <asp:Label ID="Label50" runat="server" Text="Attendance Admin"></asp:Label>
                                                            </label>
                                                            <label class="Attend">
                                                                <asp:CheckBox ID="chkrecreportadmin" runat="server" Checked="false" />
                                                                <asp:Label ID="Label51" runat="server" Text="Admin"></asp:Label>
                                                            </label>
                                                            <label class="Attend">
                                                                <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" />
                                                                <asp:Label ID="Label32" runat="server" Text="CEO"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 3%;">
                                                        </td>
                                                        <td>
                                                            <label>
                                                                C.
                                                            </label>
                                                        </td>
                                                        <td colspan="3" valign="top">
                                                            <label>
                                                                Select any other employee
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td colspan="3" valign="top">
                                                            <%--<label class="radiobuttonlabel">--%>
                                                            <asp:RadioButtonList ID="rdreportemprec" runat="server" RepeatDirection="Horizontal"
                                                                AutoPostBack="True" OnSelectedIndexChanged="rdreportemprec_SelectedIndexChanged">
                                                                <asp:ListItem Text="All Employees of Selected Designation" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="To selected employee(s)" Value="2"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <%--</label>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 3%;">
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Panel ID="pnlcf" runat="server" Visible="false" Width="53%">
                                                                <asp:GridView ID="GridView2" runat="server" EmptyDataText="Record Not Found." AllowSorting="false"
                                                                    AutoGenerateColumns="False" DataKeyNames="EmpId" GridLines="Both" CssClass="mGrid"
                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="95%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Select Employee(s)" SortExpression="Empname" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblemp" runat="server" Text='<%# Eval("Empname") %>'></asp:Label>
                                                                                <asp:Label ID="Label67" runat="server" Visible="false" Text='<%# Eval("EmpId") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="3%">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkemp" runat="server" Checked='<%# Eval("chapp") %>' AutoPostBack="True"
                                                                                    OnCheckedChanged="chkemp_CheckedChanged" />
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
                                    <asp:Panel ID="panelsdhhide" runat="server" Visible="false">
                                        <tr>
                                            <td valign="top">
                                                <label>
                                                    3
                                                </label>
                                            </td>
                                            <td valign="top">
                                                <label>
                                                    When Employee attendance deviations in any pay period exceeds limit set in <a href="AttendenceRule.aspx"
                                                        target="_blank">Rule</a> 2 A of Attendance Rules
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                </label>
                                                <label>
                                                </label>
                                                <label>
                                                </label>
                                                <label>
                                                </label>
                                                <asp:CheckBox ID="chkwaeningemail2" runat="server" AutoPostBack="true" Checked="false"
                                                    OnCheckedChanged="chkwaeningemail2_CheckedChanged" />
                                                <label>
                                                    <asp:Label ID="Label37" runat="server" Text="Generate warning e-mail to:"></asp:Label>
                                                </label>
                                                <asp:Panel ID="pnlemailgene" runat="server" Visible="false">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label class="Attend">
                                                                    <asp:CheckBox ID="chkemailatt" runat="server" Checked="true" />
                                                                    <asp:Label ID="Label38" runat="server" Text="Attendee"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label class="Attend">
                                                                    <asp:CheckBox ID="chkmailsuper" runat="server" Checked="true" />
                                                                    <asp:Label ID="Label39" runat="server" Text="Supervisor"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label class="Attend">
                                                                    <asp:CheckBox ID="chkmailappadmin" runat="server" Checked="true" />
                                                                    <asp:Label ID="Label40" runat="server" Text="Attendance Admin"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label class="Attend">
                                                                    <asp:CheckBox ID="chkmailparent" runat="server" Checked="false" Visible="false" />
                                                                    <asp:Label ID="Label41" runat="server" Text="Parents (In Case of School)" Visible="false"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnemaiview" runat="server" Text="View Warning E-mail" CssClass="btnSubmit"
                                                                    OnClick="btnemaiview_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </table>
                                <table>
                                </table>
                            </fieldset>
                            <div style="clear: both;">
                            </div>
                            <asp:Panel ID="Panel2" runat="server" Visible="false">
                                <asp:Panel ID="paneldffd" runat="server" Visible="false">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="B - Overtime Rules"></asp:Label></legend>
                                        <div style="clear: both;">
                                        </div>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <label>
                                                        1
                                                    </label>
                                                </td>
                                                <td>
                                                    <label class="Attend">
                                                        No overtime is paid for time fluctuations in grace period.
                                                    </label>
                                                    <label class="Attend">
                                                        <asp:CheckBox ID="checkbox" runat="server" Checked="false" />
                                                    </label>
                                                </td>
                                                <td>
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
                                                        <asp:CheckBox ID="chkoverhowdo" runat="server" Checked="false" AutoPostBack="True"
                                                            OnCheckedChanged="chkoverhowdo_CheckedChanged" />
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Panel ID="pnloverop2radio" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="2" valign="top">
                                                                    <label class="radiobuttonlabel">
                                                                        <asp:RadioButton ID="rd1" runat="server" AutoPostBack="true" Checked="True" GroupName="abc"
                                                                            OnCheckedChanged="rd1_CheckedChanged" Text="No overtime is paid" />
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <label class="radiobuttonlabel">
                                                                        <asp:RadioButton ID="rd2" runat="server" AutoPostBack="True" GroupName="abc" OnCheckedChanged="rd2_CheckedChanged"
                                                                            Text="Overtime is paid at regular rate of the remuneration type" />
                                                                    </label>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlrempt2" runat="server" Visible="False" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" valign="top">
                                                                    <label class="radiobuttonlabel">
                                                                        <asp:RadioButton ID="rd3" runat="server" AutoPostBack="True" Text="Overtime is paid at a special rate"
                                                                            GroupName="abc" OnCheckedChanged="rd3_CheckedChanged" />
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
                                                                                    <label>
                                                                                        <asp:Label ID="lbloverA0" runat="server" Text="A"></asp:Label>
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        No overtime paid for timings within the acceptable grace period defined in Step
                                                                                        A1
                                                                                    </label>
                                                                                    <label class="Attend">
                                                                                        <asp:CheckBox ID="chkoverA" runat="server" Checked="false" />
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 5%;">
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:Label ID="lbloverB0" runat="server" Text="B"></asp:Label>
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:Label ID="Label19" runat="server" Text="Paid overtime when overtime hours exeeds "></asp:Label>
                                                                                    </label>
                                                                                    <label>
                                                                                        <asp:TextBox ID="txthour" runat="server" Text="0" Width="45px" MaxLength="3"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="txtaddress_FilteredTextBoxExtender" runat="server"
                                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="txthour" ValidChars="">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txthour"
                                                                                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                    </label>
                                                                                    <label>
                                                                                        <asp:Label ID="Label23" runat="server" Text="hour(s) per pay period"></asp:Label>
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 5%;">
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:Label ID="lbloverc0" runat="server" Text="C"></asp:Label>
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:Label ID="Label24" runat="server" Text="Overtime is paid at "></asp:Label>
                                                                                    </label>
                                                                                    <label>
                                                                                        <asp:TextBox ID="txtRate" runat="server" Text="100" Width="45px" MaxLength="3"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                                            FilterType="Custom, Numbers" TargetControlID="txtRate" ValidChars="-.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtRate"
                                                                                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                    </label>
                                                                                    <label>
                                                                                        <asp:Label ID="Label25" runat="server" Text="percentage of the average rate of the following remuneration type"></asp:Label>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlramu"
                                                                                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                    </label>
                                                                                    <label>
                                                                                        <asp:DropDownList ID="ddlramu" runat="server" Width="150px">
                                                                                        </asp:DropDownList>
                                                                                    </label>
                                                                                </td>
                                                                                <td>
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
                                                <td>
                                                    <label>
                                                        3
                                                    </label>
                                                </td>
                                                <td>
                                                    <label class="Attend">
                                                        Overtime is to be paid only if approved by attendance admin.
                                                    </label>
                                                    <label class="Attend">
                                                        <asp:CheckBox ID="chkappsuper" runat="server" Checked="false" />
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </asp:Panel>
                            </asp:Panel>
                        </asp:Panel>
                    </fieldset>
                    <div style="clear: both;">
                    </div>
                    <div style="padding-left: 1%">
                        <asp:Button ID="btnSubmit" CssClass="btnSubmit" runat="server" Text="Submit" Visible="false"
                            OnClick="Button4_Click" ValidationGroup="1" />
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
                <fieldset>
                    <legend>List of Attendance Email Notification Rules</legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button1_Click" Visible="False" />
                        <input type="button" value="Print" id="Button3" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" visible="False" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblcompname" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label1" runat="server" Font-Size="18px" Text="List of Attendance and Overtime Rules"> </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="Attendence_Rule_Id" EmptyDataText="Record Not Found."
                                        OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        OnRowEditing="GridView1_RowEditing" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                        GridLines="Both" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        Width="100%" OnSorting="GridView1_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="StoreName" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="92%">
                                                <ItemTemplate>
                                                    <asp:Label ID="storeNameId" runat="server" Text='<%# Eval("StoreName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Acceptable In Time Deviation Minutes" SortExpression="AcceptableInTimeDeviationMinutes"
                                                HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="AcceptInId" runat="server" Text='<%# Eval("AcceptableInTimeDeviationMinutes") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Acceptable Out Time Deviation Minutes" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="AcceptableOutTimeDeviationMinutes" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="AcceptOutId" runat="server" Text='<%# Eval("AcceptableOutTimeDeviationMinutes") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="General Rules" HeaderStyle-HorizontalAlign="Left"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="lblgenrules" Enabled="false" runat="server" Checked='<%# Eval("Generalapprovalrule") %>'>
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField CommandName="Edit" HeaderText="Set/Edit" ShowHeader="True" ControlStyle-ForeColor="#416271"
                                                HeaderStyle-HorizontalAlign="Left" Text="Set/Edit" HeaderStyle-Width="8%" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <!--end of right content-->
                <div>
                    <asp:Panel ID="Panel13" runat="server" BorderWidth="7px" BackColor="#d1cec5" Width="590px"
                        Height="130px">
                        <table id="Table8" cellpadding="0" cellspacing="0" width="100%" align="center">
                            <tr>
                                <td align="right">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                        Width="16px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label48" runat="server" Text=" You can not use this rule at this time as there is no master email account set up for sending out emails. "></asp:Label>
                                        <br />
                                        <asp:Label ID="Label45" runat="server" Text="Please go to the "></asp:Label>
                                        <a href="WizardCompanyWebsitMaster.aspx" style="color: Blue" target="_blank">Business
                                            Information Set Up: Manage</a>
                                        <asp:Label ID="vcncvfd" runat="server" Text=" page to set up a master Email."></asp:Label>
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="HiddenButton22as1" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender11" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel13" TargetControlID="HiddenButton22as1" CancelControlID="ImageButton1">
                    </cc1:ModalPopupExtender>
                </div>
                <div>
                    <asp:Panel ID="Panel1" runat="server" BorderWidth="7px" BackColor="#d1cec5" Width="340px"
                        Height="130px">
                        <table id="Table1" cellpadding="0" cellspacing="0" width="100%" align="center">
                            <tr>
                                <td align="right">
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                        Width="16px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label75" runat="server" Text="You can not select this option as there is no Work email set for "></asp:Label>
                                        <asp:Label ID="Label89" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="Label88" runat="server" Text=" To set work email for the employees click"></asp:Label>
                                        &nbsp;<a href="AddCompanyEmail.aspx" style="color: Blue;" target="_blank">here </a>
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="HiddenButtonzzzzz" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel1" TargetControlID="HiddenButtonzzzzz" CancelControlID="ImageButton2">
                    </cc1:ModalPopupExtender>
                </div>
                <div style="clear: both;">
                    <asp:Panel ID="Paneldoc" runat="server" Width="95%" CssClass="modalPopup" ScrollBars="Both">
                        <fieldset>
                            <legend>
                                <asp:Label ID="lbldoclab" runat="server" Text="Email Format Layout:"></asp:Label>
                            </legend>
                            <table width="100%">
                                <tr>
                                    <td style="width: 95%;">
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                            Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            <asp:Label ID="Label52" runat="server" Text="Subject:	You are receiving this email as you are on the send list: Regarding lateness at work for Employee Name										
"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            <asp:Label ID="Label54" runat="server" Text="Current Date"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            <asp:Label ID="Label55" runat="server" Text="Dear (employee name),"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            <asp:Label ID="Label57" runat="server" Text="Promptness is essential at work. We make allowances and grace periods for the unexpected circumstances that cause tardiness. However, consistently being tardy is not acceptable."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            <asp:Label ID="Label58" runat="server" Text="According to our records, you have been late (dynamic number) times in the past (dynamic period based on Rule 2). Please see the below chart for specific examples."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label60" runat="server" Text="Date"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label61" runat="server" Text="Scheduled In Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label62" runat="server" Text="Recorded In Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label63" runat="server" Text="Scheduled Out Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label64" runat="server" Text="Recorded Out Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label65" runat="server" Text="Deviation in In Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label66" runat="server" Text="Deviation in Out Time"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label68" runat="server" Text="Date"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label69" runat="server" Text="Scheduled In Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label70" runat="server" Text="Recorded In Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label71" runat="server" Text="Scheduled Out Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label72" runat="server" Text="Recorded Out Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label73" runat="server" Text="Deviation in In Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label74" runat="server" Text="Deviation in Out Time"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label76" runat="server" Text="Date"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label77" runat="server" Text="Scheduled In Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label78" runat="server" Text="Recorded In Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label79" runat="server" Text="Scheduled Out Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label80" runat="server" Text="Recorded Out Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label81" runat="server" Text="Deviation in In Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label82" runat="server" Text="Deviation in Out Time"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            <asp:Label ID="Label83" runat="server" Text="If there is an unexpected circumstance that prevents you from arriving to work on time, please contact your supervisor to address the problem accordingly."></asp:Label>
                                            <asp:Label ID="Label84" runat="server" Text=" Any further attendance issues in the future may result in disciplinary action."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            <asp:Label ID="Label85" runat="server" Text="I hope that you will improve your punctuality when arriving at work."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left">
                                        <label>
                                            <asp:Label ID="Label86" runat="server" Text="Sincerely,"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left">
                                        <label>
                                            <asp:Label ID="Label87" runat="server" Text="(Company Name)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                    <asp:Button ID="Button4" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Paneldoc" TargetControlID="Button4" CancelControlID="ImageButton3">
                    </cc1:ModalPopupExtender>
                </div>
                <%--    </div>--%>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlwarehouse" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="chkdeviationreason" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="chkempinstance" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="chkMakeEntry" EventName="CheckedChanged" />
            <%--    <asp:AsyncPostBackTrigger ControlID="CheckBox1" EventName="CheckedChanged" />--%>
            <%--  <asp:AsyncPostBackTrigger ControlID="rd1" EventName="CheckedChanged" />
                              <asp:AsyncPostBackTrigger ControlID="rd2" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="rd3" EventName="CheckedChanged" />--%>
            <%--                              <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" EventName="SelectedIndexChanged" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click" />
            <%--  <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
