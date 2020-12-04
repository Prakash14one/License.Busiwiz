<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="PayrollDeductionOther.aspx.cs" Inherits="Add_Payroll_Deduction_Other" %>

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

    <script type="text/javascript" language="javascript">

        function RealNumWithDecimal(myfield, e, dec) {
            //myfield=document.getElementById(FindName(myfield)).value
            //alert(myfield);
            var key;
            var keychar;
            if (window.event)
                key = window.event.keyCode;
            else if (e)
                key = e.which;
            else
                return true;
            keychar = String.fromCharCode(key);
            if (key == 13) {
                return false;
            }
            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 27)) {
                return true;
            }
            else if ((("0123456789.").indexOf(keychar) > -1)) {
                return true;
            }
            // decimal point jump
            else if (dec && (keychar == ".")) {
                myfield.form.elements[dec].focus();
                myfield.value = "";
                return false;
            }
            else {
                myfield.value = "";
                return false;
            }
        }


        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        function mask(evt) {


            if (evt.keyCode == 13) {

                return false;
            }




            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) {
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
                    <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" />
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" Text="Add New Deduction" runat="server" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <asp:Panel ID="pnlddlfill" runat="server" Width="100%" Visible="false">
                        <table width="100%">
                            <tr>
                                <td width="25%">
                                    <label>
                                        <asp:Label ID="lblBusinessName" Text="Business Name" runat="server"></asp:Label>
                                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    </label>
                                </td>
                                <td width="75%">
                                    <label>
                                        <asp:DropDownList ID="ddlstrname" runat="server" AutoPostBack="True" Width="210px"
                                            OnSelectedIndexChanged="ddlstrname_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblDeductionName" Text="Deduction Name" runat="server"></asp:Label>
                                        <asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdeduction"
                                            ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtdeduction" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtdeduction" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@.#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',30)"></asp:TextBox><br />
                                    </label>
                                    <label>
                                        <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="div1" class="labelcount">30</span>
                                        <asp:Label ID="Label34" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblMappedToAccount" Text="Mapped to account name" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Panel3" runat="server">
                                        <table>
                                            <tr>
                                                <td rowspan="2">
                                                    <asp:RadioButtonList ID="rblmappacc" runat="server" RepeatDirection="Vertical" AutoPostBack="True"
                                                        OnSelectedIndexChanged="rblmappacc_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Create a New Account and Map with this account (Recommended)</asp:ListItem>
                                                        <asp:ListItem Value="1" Selected="True">Select Account</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:Panel ID="pnlnewacc" runat="server" Visible="False">
                                                        <label>
                                                            <asp:Label ID="lblGroupName" Text="Group Name" runat="server"></asp:Label>
                                                            <asp:DropDownList ID="ddlgrup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlgrup_SelectedIndexChanged"
                                                                Width="250px">
                                                            </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="lblAccountName" Text="Account Name" runat="server"></asp:Label>
                                                            <asp:Label ID="Label16" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtnewacc"
                                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                                SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtnewacc"
                                                                ValidationGroup="1">
                                                            </asp:RegularExpressionValidator>
                                                            <div style="clear: both;">
                                                            </div>
                                                            <asp:TextBox ID="txtnewacc" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                                                Width="250px" onkeyup="return check(this,/[\\/!@.#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span1',30)"></asp:TextBox>
                                                            <asp:Label ID="Label21" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                            <span id="Span1" class="labelcount">30</span>
                                                            <asp:Label ID="Label17" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                                        </label>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="bottom">
                                                    <asp:Panel ID="pnlacc" runat="server" Visible="False">
                                                        <label>
                                                            <asp:Label ID="lblList" Text="List of Accounts" runat="server"></asp:Label>
                                                            <asp:DropDownList ID="ddlallacc" runat="server" AutoPostBack="True" CssClass="ddList">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        <asp:Label ID="lblDeductionAmount" Text="Deduction Amount" runat="server"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Panel4" runat="server">
                                        <table>
                                            <tr>
                                                <td rowspan="2">
                                                    <asp:RadioButtonList ID="rblamount" runat="server" RepeatDirection="Vertical" AutoPostBack="True"
                                                        OnSelectedIndexChanged="rblamount_SelectedIndexChanged">
                                                        <asp:ListItem Value="0" Selected="True">Fixed Amount</asp:ListItem>
                                                        <asp:ListItem Value="1">Percent of Selected Remuneration Name</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td valign="top" align="left">
                                                    <asp:Panel ID="pnlfixamount" runat="server" Visible="False" Width="100%">
                                                        <label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtfixamount"
                                                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtfixamount"
                                                                ValidationGroup="1" ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server"
                                                                Display="Dynamic" ErrorMessage="Invalid Digits"> </asp:RegularExpressionValidator>
                                                            <asp:Label ID="Label12" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:TextBox ID="txtfixamount" runat="server" Width="50px" MaxLength="10" onkeypress="return RealNumWithDecimal(this,event,2);"></asp:TextBox>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="lblAmount" Text="Amount per " runat="server"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:DropDownList ID="ddlpayperiodtype" runat="server" Width="100px">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="bottom">
                                                    <asp:Panel ID="pnlpercentage" runat="server" Visible="False">
                                                        <label>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtpercent"
                                                                Display="Dynamic" ValidationGroup="1" ValidationExpression="^(-)?\d+(\.\d\d)?$"
                                                                runat="server" ErrorMessage="Invalid Digits"> </asp:RegularExpressionValidator>
                                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Up to 100%"
                                                                ValidationGroup="1" Display="Dynamic" ControlToValidate="txtpercent" MaximumValue="100"
                                                                MinimumValue="0" Type="Double"></asp:RangeValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtpercent"
                                                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:Label ID="Label13" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:TextBox ID="txtpercent" runat="server" Width="50px" MaxLength="6" onkeypress="return RealNumWithDecimal(this,event,2);"></asp:TextBox>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="lblof" Text="% of Remuneration Name" runat="server"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:DropDownList ID="ddlremutype" runat="server" Width="90px">
                                                            </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="Label19" Text=" per " runat="server"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:DropDownList ID="ddlpayper" runat="server" Width="90px">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        <asp:Label ID="lblApplicabletoEmployees" Text="Is this deduction applicable to "
                                            runat="server"></asp:Label>
                                    </label>
                                    <asp:RadioButtonList ID="rblemployee" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Selected="True">All Employees</asp:ListItem>
                                        <asp:ListItem Value="1">Some Employees</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlemplist" Width="100%" runat="server" Visible="False">
                                        <table width="100%">
                                            <tr>
                                                <td align="left" style="width: 60%;">
                                                    <asp:GridView ID="grdemplist" Width="30%" runat="server" AutoGenerateColumns="false"
                                                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="7%">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckAdd" Checked="true" runat="server" OnCheckedChanged="checkemp_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="List of Employees" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%#Bind("EmployeeName")%>'></asp:Label>
                                                                    <asp:Label ID="lblempid" runat="server" Text='<%#Bind("EmployeeID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        <asp:Label ID="Label20" Text="Effective Start Date" runat="server"></asp:Label>
                                        <asp:Label ID="Label14" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtestartdate"
                                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rghjk" runat="server" ErrorMessage="*" ControlToValidate="txtestartdate"
                                            ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                    </label>
                                    <label>
                                        <asp:TextBox ID="txtestartdate" runat="server" Width="70px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <cc1:CalendarExtender runat="server" ID="cal1" TargetControlID="txtestartdate" PopupButtonID="txtestartdate">
                                        </cc1:CalendarExtender>
                                    </label>
                                    <label>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txtestartdate"></cc1:MaskedEditExtender>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblEffectiveEndDate" Text="Effective End Date" runat="server"></asp:Label>
                                        <asp:Label ID="Label15" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txteenddate"
                                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*"
                                            ControlToValidate="txteenddate" ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                    </label>
                                    <label>
                                        <asp:TextBox ID="txteenddate" runat="server" Width="70px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <cc1:CalendarExtender runat="server" ID="cal2" TargetControlID="txteenddate" PopupButtonID="txteenddate">
                                        </cc1:CalendarExtender>
                                    </label>
                                    <label>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txteenddate">
                                        </cc1:MaskedEditExtender>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        <asp:Label ID="lblStatus" Text="Status of this deduction" runat="server"></asp:Label>
                                    </label>
                                    <%-- <asp:RadioButtonList ID="rblstatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:RadioButtonList>--%>
                                    <label>
                                        <asp:DropDownList ID="rblstatus" runat="server" Width="120px">
                                            <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <div style="clear: both;">
                        </div>
                        <div style="clear: both;">
                        </div>
                        <br />
                        <asp:Button ID="Btn_Submit" runat="server" Text="Submit" ValidationGroup="1" OnClick="Btn_Submit_Click"
                            CssClass="btnSubmit" />
                        &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                            CssClass="btnSubmit" />
                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                            ValidationGroup="1" Visible="false" CssClass="btnSubmit" />
                        &nbsp;<asp:Button ID="btncancel1" runat="server" Visible="false" OnClick="btncancel1_Click"
                            Text="Cancel" CssClass="btnSubmit" />
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblPayrolldeduction" Text="List of Non-Government Payroll Deductions"
                            runat="server"></asp:Label></legend>
                    <label>
                        <asp:Label ID="lblBusinessNameselect" Text="Filter by Business Name" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlstrFilter" runat="server" AutoPostBack="True" Width="210px"
                            OnSelectedIndexChanged="ddlstrFilter_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <div style="float: right;">
                        <asp:Button ID="btncancel0" runat="server" CausesValidation="false" Text="Printable Version"
                            OnClick="btncancel0_Click" CssClass="btnSubmit" />
                        <input id="Button7" runat="server" visible="false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" type="button" value="Print" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td style="text-align: center; font-size: 20px; font-weight: bold; font-style: italic;">
                                                    <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td style="text-align: center; font-size: 20px; font-weight: bold; font-style: italic;">
                                                    <asp:Label ID="Label18" runat="server" ForeColor="Black" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" ForeColor="Black"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td style="text-align: center; font-size: 18px; font-weight: bold; font-style: italic;">
                                                    <asp:Label ID="Label3" runat="server" Text="List of Non-Government Payroll Deductions"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                        AllowPaging="true" CssClass="mGrid" PageSize="20" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        DataKeyNames="Id" Width="100%" EmptyDataText="No Record Found." OnRowCommand="GridView1_RowCommand"
                                        OnRowEditing="GridView1_RowEditing" OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
                                        OnRowDeleting="GridView1_RowDeleting" OnPageIndexChanging="GridView1_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="ID" Visible="false" />
                                            <asp:TemplateField HeaderText="Business Name" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblid" Visible="false" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                    <asp:Label ID="lbldefaultid" Visible="false" runat="server" Text='<%# Eval("DefaultId") %>'></asp:Label>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deduction Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("DeductionName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Period Type" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("paypername") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Account Name" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("AccountName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="17%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblempname" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fixed Amount" ItemStyle-Width="9%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("FixedAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Percentage of Remuneration" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("remper") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="% of Remuniration" SortExpression="RemunerationName"
                                                ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("RemunerationName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Start Date" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("StartDate","{0:MM/dd/yyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Date" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("EndDate","{0:MM/dd/yyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("active") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Button3" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="vi"
                                                        ImageUrl="~/Account/images/edit.gif" ToolTip="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:CommandField HeaderText="Edit" ShowEditButton="True" ValidationGroup="2" ButtonType="Image"
                                                EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/updategrid.jpg"
                                                CancelImageUrl="~/images/delete.gif" HeaderImageUrl="~/Account/images/edit.gif"
                                                ItemStyle-Width="4%" />--%>
                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
            <div style="clear: both;">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
