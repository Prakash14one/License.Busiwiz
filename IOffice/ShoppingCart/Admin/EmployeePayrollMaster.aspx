<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="EmployeePayrollMaster.aspx.cs" Inherits="Add_Employee_Payroll_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
        function mask(evt) {

            counter = document.getElementById(id);
            alert(counter);
            if (evt.srcElement.value.length > max_len && evt.keyCode != 8) {
                return false;
            }
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="padding-left: 1%">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <fieldset>
                 <legend><asp:Label ID="lbladd" runat="server" Text=""></asp:Label></legend>
                    <div style="float: right">
                        <asp:Button ID="btnshowparty" runat="server" Text="Add Employee Payroll" CssClass="btnSubmit"
                            OnClick="btnshowparty_Click" />
                    </div>
                    <asp:Panel ID="pnlparty" runat="server" Visible="false">
                       
                        <label>
                            <asp:Label ID="Label2" runat="server" Text="Business Name"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStore"
                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlStore" runat="server" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged"
                                AutoPostBack="True" >
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Employee Name"></asp:Label>
                            <asp:Label ID="Label52" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlemployee"
                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlemployee" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <br />
                                <asp:ImageButton ID="ImageButton50" runat="server" Height="20px" ImageAlign="Bottom"
                                    ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                    ToolTip="AddNew" Width="20px" onclick="ImageButton50_Click" />
                             </label>
                             <label>
                             <br />
                                <asp:ImageButton ID="ImageButton51" runat="server" Height="20px" ImageAlign="Bottom"
                                    ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" 
                                    ToolTip="Refresh" Width="20px" onclick="ImageButton51_Click"  />
                           </label>
                        <label>
                            <asp:Label ID="Label4" runat="server" Text="Status" Width="100px"></asp:Label>
                            <asp:DropDownList ID="ddlstatus" runat="server">
                                <asp:ListItem Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                            </asp:DropDownList>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label class="first">
                            <asp:Label ID="Label5" runat="server" Text="Remuneration calculation method"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">
                                        <a href="RemunerationByDesignation.aspx" target="_blank" >
                                        Employee paid as per Master Designation 
                                        Salary
                                        </a>
                            </asp:ListItem>
                            <asp:ListItem Value="0">
                                         <a href="EmployeeSalaryMaster.aspx" target="_blank">
                                        Employee paid as per own Separate Setup
                                         </a>
                            </asp:ListItem>
                        </asp:RadioButtonList>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label6" runat="server" Text="Last Name"></asp:Label>
                            <asp:Label ID="Label20" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtlastname"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtlastname"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                            <br />
                            <asp:TextBox ID="txtlastname" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@.#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'div1',30)"></asp:TextBox>
                            <asp:Label ID="Label40" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="div1" class="labelcount">30</span>
                            <asp:Label ID="Label34" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                        </label>
                        <label class="first">
                            <asp:Label ID="Label7" runat="server" Text="First Name"></asp:Label>
                            <asp:Label ID="Label36" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtfirstname"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtfirstname"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                            <br />
                            <asp:TextBox ID="txtfirstname" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@.#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'Span1',30)"></asp:TextBox>
                            <asp:Label ID="Label31" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span1" class="labelcount">30</span>
                            <asp:Label ID="Label21" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                        </label>
                        <label class="first">
                            <asp:Label ID="Label8" runat="server" Text="Initials"></asp:Label>
                            <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtintialis"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                SetFocusOnError="True" ValidationExpression="^([._a-zA-Z0-9\s]*)" ControlToValidate="txtintialis"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                            <br />
                            <asp:TextBox ID="txtintialis" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\.a-zA-Z_0-9\s]+$/,'Span2',15)"></asp:TextBox>
                            <asp:Label ID="Label32" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span2" class="labelcount">15</span>
                            <asp:Label ID="Label22" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                        </label>
                        <label class="first">
                            <asp:Label ID="Label9" runat="server" Text="Employee Number"></asp:Label>
                            <asp:Label ID="Label24" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtemployeeno"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Character"
                                SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtemployeeno"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                            <br />
                            <asp:TextBox ID="txtemployeeno" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!.@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'Span3',20)"></asp:TextBox>
                            <asp:Label ID="Label33" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span3" class="labelcount">20</span>
                            <asp:Label ID="Label23" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label10" runat="server" Text="Date Of Birth"></asp:Label>
                            <asp:Label ID="Label25" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtdateofbirth"
                             Display="Dynamic"    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rghjk" runat="server" ErrorMessage="*" ControlToValidate="txtdateofbirth"
                             Display="Dynamic"    ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtdateofbirth" runat="server" Width="70px"></asp:TextBox>
                            
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtdateofbirth"
                                TargetControlID="txtdateofbirth">
                            </cc1:CalendarExtender>
                        </label>
                        
                        <label>
                            <asp:Label ID="Label11" runat="server" Text=" Social Insurance Number /Social Security Number/Pan Card Number/ Other Government
                    Issued ID"></asp:Label>
                            <asp:Label ID="Label26" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtsecurityno"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Character"
                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtsecurityno"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtsecurityno" runat="server" MaxLength="40" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!._@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span4',40)"></asp:TextBox>
                            <asp:Label ID="Label35" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span4" class="labelcount">40</span>
                            <asp:Label ID="Label27" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label12" runat="server" Text="Payment Method"></asp:Label>
                            <asp:Label ID="Label28" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPaymentMethod"
                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlPaymentMethod" runat="server" OnSelectedIndexChanged="ddlPaymentMethod_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label13" runat="server" Text="Payment Cycle"></asp:Label>
                            <asp:Label ID="Label29" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPaymentCycle"
                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlPaymentCycle" runat="server">
                            </asp:DropDownList>
                        </label>
                         <div style="clear: both;">
                        </div>
                        <asp:Panel ID="pnlreceivepayment" runat="server" Visible="false">
                        
                        
                        <label>
                            <asp:Label ID="lblPaymentReceivedNameOf" runat="server"  Text="Payment Received Name Of"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14"  runat="server" ErrorMessage="Invalid Character"
                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z_0-9\s]*)" ControlToValidate="txtPaymentReceivedNameOf"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtPaymentReceivedNameOf" runat="server" MaxLength="50" onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!.@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z_A-Z0-9\s]+$/,'Span12',50)"></asp:TextBox>
                             <asp:Label ID="Label53" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                    <span id="Span12" class="labelcount">50</span>
                                    <asp:Label ID="Label54" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                        </label>
                        </asp:Panel>
                         <div style="clear: both;">
                        </div>
                        <asp:Panel ID="pnlpaypalid" runat="server" Visible="false">
                        <label>
                            <asp:Label ID="lblPaypalId" runat="server"  Text="Paypal Id"></asp:Label>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator15"  runat="server" ErrorMessage="Invalid Character"
                                        SetFocusOnError="True" ValidationExpression="^([0-9\s]*)" ControlToValidate="txtPaypalId"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                     <cc1:FilteredTextBoxExtender ID="txtEndzip_FilteredTextBoxExtender" runat="server"
                Enabled="True" TargetControlID="txtPaypalId" ValidChars="0123456789">
            </cc1:FilteredTextBoxExtender>
                            <asp:TextBox ID="txtPaypalId" runat="server"  MaxLength="15" onkeyup="return mak('Span13',15,this)"
                                        ></asp:TextBox>
                                    <asp:Label ID="Label55" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                    <span id="Span13" class="labelcount">15</span>
                                    <asp:Label ID="Label56" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>   
                        </label>
                        </asp:Panel>
                         <div style="clear: both;">
                        </div>
                        <asp:Panel ID="pnlpaymentemail" runat="server" Visible="false">
                        <label>
                            <asp:Label ID="lblPaymentEmailId" runat="server"  Text="Payment Email Id"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPaymentEmailId"
                                ErrorMessage="Invalid E-mail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtPaymentEmailId" runat="server" MaxLength="40"  ></asp:TextBox>
                        </label>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                        <br />
                        <asp:Panel ID="ddpnl" Width="100%" Visible="false" runat="server">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="Label17" runat="server" Text="Direct Deposited Bank Details"></asp:Label>
                                </legend>
                                <table>
                                    <tr>
                                        <td>
                                             <label>
                                    <asp:Label ID="lblDirectDepositBankName" runat="server" Text="Bank Name"></asp:Label>
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Character"
                                        SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtDirectDepositBankName"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtDirectDepositBankName" runat="server" MaxLength="50" onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!.@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z_A-Z0-9\s]+$/,'Span5',50)"
                                        ></asp:TextBox>
                                    <asp:Label ID="Label37" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                    <span id="Span5" class="labelcount">50</span>
                                    <asp:Label ID="Label38" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                </label>
                                        </td>
                                        <td>
                                        <label>
                                    <asp:Label ID="lblDirectDepositBankCode" runat="server" Text="Bank Code"></asp:Label>
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Invalid Character"
                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtDirectDepositBankCode"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtDirectDepositBankCode" runat="server" MaxLength="40" 
                                    onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!._@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span6',40)" ></asp:TextBox>
                                    <asp:Label ID="Label39" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                    <span id="Span6" class="labelcount">40</span>
                                    <asp:Label ID="Label41" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                                </label>
                                        </td>
                                        <td>
                                        <label>
                                    <asp:Label ID="lblDirectDepositBankBranchName" runat="server"  Text="Bank Branch Name"></asp:Label>
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9"  runat="server" ErrorMessage="Invalid Character"
                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z_0-9\s]*)" ControlToValidate="txtDirectDepositBankBranchName"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                    
                                    <asp:TextBox ID="txtDirectDepositBankBranchName" runat="server" MaxLength="50"
                                    onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!.@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z_A-Z0-9\s]+$/,'Span7',50)" ></asp:TextBox>
                                     <asp:Label ID="Label42" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                    <span id="Span7" class="labelcount">50</span>
                                    <asp:Label ID="Label43" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                </label>
                                        </td>
                                        <td>
                                         <label>
                                    <asp:Label ID="lblDirectDepositBankBranchNumber" runat="server" Text="Bank Branch Number"></asp:Label>
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ErrorMessage="Invalid Character"
                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtDirectDepositBankBranchNumber"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtDirectDepositBankBranchNumber" runat="server" MaxLength="40"
                                    onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!._@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span8',40)" ></asp:TextBox>
                                    <asp:Label ID="Label44" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                    <span id="Span8" class="labelcount">40</span>
                                    <asp:Label ID="Label45" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                                </label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                        <label>
                                    <asp:Label ID="lblDirectDepositTransitNumber" runat="server"  Text="Transit Number"></asp:Label>
                                    
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11"  runat="server" ErrorMessage="Invalid Character"
                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtDirectDepositTransitNumber"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtDirectDepositTransitNumber" runat="server" MaxLength="40" 
                                    onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!._@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span9',40)" ></asp:TextBox>
                                    <asp:Label ID="Label46" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                    <span id="Span9" class="labelcount">40</span>
                                    <asp:Label ID="Label47" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                                </label>
                                        </td>
                                        <td>
                                        <label>
                                    <asp:Label ID="lblDirectDepositAccountHolderName" runat="server" 
                                        Text="Account Holder Name"></asp:Label>
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12"  runat="server" ErrorMessage="Invalid Character"
                                        SetFocusOnError="True" ValidationExpression="^([a-z_A-Z0-9\s]*)" ControlToValidate="txtDirectDepositAccountHolderName"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtDirectDepositAccountHolderName" runat="server" MaxLength="50"
                                    onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!.@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'Span10',50)" 
                                        ></asp:TextBox>
                                    <asp:Label ID="Label48" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                    <span id="Span10" class="labelcount">50</span>
                                    <asp:Label ID="Label49" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                </label>
                                        </td>
                                        <td>
                                         <label>
                                    <asp:Label ID="lblDirectDepositBankAccountType" runat="server" Text="Bank Account Type"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:DropDownList ID="ddlDirectDepositBankAccountType" runat="server" >
                                        <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Bussiness"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Checking"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Current"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Payroll"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Savings"></asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                </label>
                                        </td>
                                          <td>
                                        <label>
                                    <asp:Label ID="lblDirectDepositBankAccountNumber" runat="server" 
                                        Text="Bank Account Number"></asp:Label>
                                     <br />
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ErrorMessage="Invalid Character"
                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtDirectDepositBankAccountNumber"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtDirectDepositBankAccountNumber" MaxLength="50" runat="server"
                                     onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!._@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span11',50)" 
                                       ></asp:TextBox>
                                    <asp:Label ID="Label50" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                    <span id="Span11" class="labelcount">50</span>
                                    <asp:Label ID="Label51" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                                </label>
                                        </td>
                                    </tr>
                                </table>
                               
                                
                                
                               
                                
                                
                               
                                
                            </fieldset></asp:Panel>
                            <div style="clear: both;">
                        </div>
                        <asp:Button ID="btnsubmit" runat="server" CssClass="btnSubmit" OnClick="Button1_Click"
                            Text="Submit" ValidationGroup="1" />
                        <asp:Button ID="btnupdate" CssClass="btnSubmit" Text="Update" runat="server" OnClick="btnupdate_Click"
                            ValidationGroup='1' Visible="False" />
                        <asp:Button ID="Button4" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="Button2_Click" />
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" type="hidden" />
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label18" runat="server" Text="List of Employee Payroll"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button5" CssClass="btnSubmit" runat="server" Text="Printable Version"
                            OnClick="Button5_Click" />
                        <input id="Button3" class="btnSubmit" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label14" runat="server" Text="Filter by Business Name"></asp:Label>
                        <asp:DropDownList ID="ddlfilterbusiness" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlfilterbusiness_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label15" runat="server" Text="Select by Employee Name"></asp:Label>
                        <asp:DropDownList ID="ddlfilteremployee" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlfilteremployee_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%" Height="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblCompany" Font-Size="20px" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label30" Font-Size="20px" runat="server" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblbusiness" Font-Size="20px" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                        <td align="center" >
                                            <asp:Label ID="Label14" runat="server" Text="Status Category :"></asp:Label>
                                            <asp:Label ID="lblStatusCat" runat="server"></asp:Label>
                                        </td>
                                    </tr>--%>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label16" runat="server" Font-Size="18px" Text="List of Employee Payroll"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl" runat="server" Font-Bold="false" Font-Size="16px" Text="Employee Name : "></asp:Label>
                                                    <asp:Label ID="lblfilteremp" runat="server" Font-Bold="false" Font-Size="16px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" Width="100%"
                                        AutoGenerateColumns="False" GridLines="Both" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" DataKeyNames="EmployeePayrollMaster_Id" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                                        OnSorting="GridView1_Sorting" EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="StoreName"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" >
                                               <ItemTemplate>
                                               <asp:Label ID="lblstore" runat="server" Text='<%#Bind("StoreName") %>'></asp:Label>
                                               </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Employee Name" SortExpression="EmployeeName"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="17%">
                                                <ItemTemplate>
                                                <asp:Label ID="lblemp" runat="server" Text='<%#Bind("EmployeeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Employee No." SortExpression="EmployeeNo"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblempno" runat="server" Text='<%#Bind("EmployeeNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment Cycle" SortExpression="PaymentCycle" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpaymentcycle123" runat="server" Text='<%#Bind("PaymentCycle") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Payment Method" SortExpression="PaymentMethodName"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                 <asp:Label ID="lblpayemntmethd" runat="server" Text='<%#Bind("PaymentMethodName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Paid as Per" SortExpression="EmployeePaidAsPerDesignation"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemppaidasperdesignation" runat="server" Text='<%#Bind("EmployeePaidAsPerDesignation") %>'
                                                        Visible="false"></asp:Label>
                                                    <asp:RadioButtonList Enabled="false" ID="RadioButtonList123" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Designation</asp:ListItem>
                                                        <asp:ListItem Value="0">Own Setup</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnedit" runat="server" CommandName="Edit" CommandArgument='<%#Bind("EmployeePayrollMaster_Id") %>'
                                                        ImageUrl="~/Account/images/edit.gif" ToolTip="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:ButtonField CommandName="Edit" HeaderText="Edit" ShowHeader="True" Text="Edit"
                                                ButtonType="Image" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif"
                                                ImageUrl="~/Account/images/edit.gif" ItemStyle-Width="3%" />--%>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Button1" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:ButtonField CommandName="del" HeaderText="Delete" Text="Delete" ButtonType="Image" HeaderStyle-HorizontalAlign="Left"
                              HeaderImageUrl="~/ShoppingCart/images/trash.jpg" ImageUrl="~/Account/images/delete.gif" ItemStyle-Width="15px" />--%>
                                            <asp:TemplateField HeaderText="View" HeaderImageUrl="~/Account/images/viewprofile.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnview" runat="server" CommandName="View" CommandArgument='<%#Bind("EmployeePayrollMaster_Id") %>'
                                                        ImageUrl="~/Account/images/viewprofile.jpg" ToolTip="View"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:ButtonField CommandName="View" HeaderText="View" ShowHeader="True" Text="Edit"
                                                ButtonType="Image" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/viewprofile.jpg"
                                                ImageUrl="~/Account/images/viewprofile.jpg" ItemStyle-Width="3%" />--%>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--end of right content-->
    <div style="clear: both;">
    </div>
</asp:Content>
