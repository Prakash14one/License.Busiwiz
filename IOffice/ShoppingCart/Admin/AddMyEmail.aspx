<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="AddMyEmail.aspx.cs" Inherits="Account_AddCompanyEmail"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Src="~/Ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        #Table1
        {
            height: 418px;
        }
        #Table4
        {
            height: 386px;
            width: 663px;
        }
    </style>
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
        function mask(evt) {

            if (evt.keyCode == 13) {


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
    <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
        <ContentTemplate>
            <div style="padding-left: 1%">
                <asp:Label ID="lblext" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label24" runat="server" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadddd" runat="server" Text="Add New Email" CssClass="btnSubmit"
                            OnClick="btnadddd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel runat="server" ID="pnladdd" Width="100%" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td colspan="2">
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                      Visible="false"  OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="1">Set New Email Account for Company</asp:ListItem>
                                        <asp:ListItem Value="2">Set New Email Account for Employee</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="lblnme" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstore_SelectedIndexChanged" Enabled="false">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <asp:Panel ID="pnle" Width="100%" runat="server" Visible="false">
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label9" runat="server" Text="Employee Name"></asp:Label>
                                            <asp:Label ID="Label37" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFielfdsfdValidator4" runat="server" ValidationGroup="6"
                                                ErrorMessage="*" ControlToValidate="ddlemp" InitialValue="0"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td colspan="2">
                                        <label>
                                            <asp:DropDownList ID="ddlemp" runat="server" AutoPostBack="false" Enabled="false">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label44" runat="server" Text="E-mail Display Name"></asp:Label>
                                        <asp:Label ID="Label45" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" SetFocusOnError="true"
                                            ControlToValidate="txtmemailname" ValidationGroup="2" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                            ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtmemailname" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtmemailname" MaxLength="100" runat="server" onkeyup="return mak('Span9',100,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label46" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                        <span id="Span9" cssclass="labelcount">100</span>
                                        <%--<asp:Label ID="Label51" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>--%>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label10" runat="server" Text="Email ID"></asp:Label>
                                        <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="6"
                                            ErrorMessage="*" ControlToValidate="txtemail"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtemail"
                                            ErrorMessage="Invalid Email ID" ValidationExpression="\w+([_.]\w+)*@\w+([_.]\w+)*\.\w+([_.]\w+)*"
                                            ValidationGroup="6"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtemail" runat="server" ValidationGroup="6" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.@ \s]+$/,'Span3',50)"
                                            MaxLength="50" OnTextChanged="txtemail_TextChanged"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="sadasd" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span3" cssclass="labelcount">50</span>
                                        <asp:Label ID="Label5" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ @ .)"></asp:Label>
                                        <%-- <asp:Label ID="Label29"  runat="server" Text="(A-Z,0-9,_,.)" ></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="(Max 30 Chars)"></asp:Label>--%>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text="Incoming Mail Server(POP3)"></asp:Label>
                                        <asp:Label ID="Label13" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="6"
                                            ErrorMessage="*" ControlToValidate="txtserver"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="23"
                                            ErrorMessage="*" ControlToValidate="txtserver"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Mail Server"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([.@a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtserver" ValidationGroup="6"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtserver" runat="server" MaxLength="40" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.@ \s]+$/,'Span1',40)"
                                            ValidationGroup="6"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label7" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" cssclass="labelcount">40</span>
                                        <asp:Label ID="Label2" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ @ .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Incoming Mail Server User ID"></asp:Label>
                                        <asp:Label ID="Label15" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="6"
                                            ErrorMessage="*" ControlToValidate="txtuser"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="23"
                                            ErrorMessage="*" ControlToValidate="txtuser"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtuser"
                                            ErrorMessage="Invalid User ID" ValidationExpression="\w+([_.]\w+)*@\w+([_.]\w+)*\.\w+([_.]\w+)*"
                                            ValidationGroup="6"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtuser" runat="server" ValidationGroup="6" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.@ \s]+$/,'Span2',50)"
                                            MaxLength="50" ></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label8" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span2" cssclass="labelcount">50</span>
                                        <asp:Label ID="Label4" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ @ .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <asp:Panel ID="upperpanel" runat="server" Visible="false">
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label38" runat="server" Text="Port(POP3)"></asp:Label>
                                            <asp:Label ID="Label47" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        </label>
                                    </td>
                                    <td colspan="2">
                                        <label>
                                            <asp:TextBox ID="txtport1" runat="server" MaxLength="10" onkeyup="return mak('Span55',10,this)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label runat="server" ID="Label39" Text="Max " CssClass="labelcount"></asp:Label>
                                            <span id="Span55">10</span>
                                            <asp:Label ID="Label40" runat="server" Text="(0-9)" CssClass="labelcount"></asp:Label>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                TargetControlID="txtport1" ValidChars="0147852369">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Enter Password"></asp:Label>
                                        <asp:Label ID="Label17" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="6"
                                            ErrorMessage="*" ControlToValidate="txtpass"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="23"
                                            ErrorMessage="*" ControlToValidate="txtpass"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([._!%#$@+a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtpass" ValidationGroup="6"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 35%">
                                    <label>
                                        <asp:TextBox ID="txtpass" runat="server" ValidationGroup="6" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/^'&*()>:;={}[]|\/]/g,/^[\!%#$a-zA-Z0-9_.+@ \s]+$/,'Span4',30)"
                                            MaxLength="30" TextMode="Password" Width="200px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label31" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span4" cssclass="labelcount">30</span>
                                        <asp:Label ID="Label29" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ @ . ! % # $)"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Test Email" OnClick="Button2_Click"
                                        ValidationGroup="23" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:CheckBox ID="CheckBox2" runat="server" Text="Same as Incoming Mail Server" AutoPostBack="True"
                                            OnCheckedChanged="CheckBox2_CheckedChanged" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label18" runat="server" Text="Outgoing Mail Server(SMTP)"></asp:Label>
                                        <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="6"
                                            ErrorMessage="*" ControlToValidate="txtoutserver"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="24"
                                            ErrorMessage="*" ControlToValidate="txtoutserver"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Mail Server"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([.@a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtoutserver" ValidationGroup="6"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtoutserver" runat="server" ValidationGroup="6" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.@ \s]+$/,'Span5',40)"
                                            MaxLength="40"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label32" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span5" cssclass="labelcount">40</span>
                                        <asp:Label ID="Label30" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ @ .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label20" runat="server" Text="Outgoing Mail Server User ID"></asp:Label>
                                        <asp:Label ID="Label21" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2874" runat="server" ValidationGroup="6"
                                            ErrorMessage="*" ControlToValidate="txtoutemail"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="24"
                                            ErrorMessage="*" ControlToValidate="txtoutemail"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtoutemail"
                                            ErrorMessage="Invalid User ID" ValidationExpression="\w+([_.]\w+)*@\w+([_.]\w+)*\.\w+([_.]\w+)*"
                                            ValidationGroup="6"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtoutemail" runat="server" ValidationGroup="6" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.@ \s]+$/,'Span6',50)"
                                            MaxLength="50"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label33" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span6" cssclass="labelcount">50</span>
                                        <asp:Label ID="Label3" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ @ .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <asp:Panel ID="Panel2" runat="server" Visible="false">
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label41" runat="server" Text="Port(SMTP)"></asp:Label>
                                            <asp:Label ID="Label48" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="6"
                                            ErrorMessage="*" ControlToValidate="txtport2"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="23"
                                            ErrorMessage="*" ControlToValidate="txtport2"></asp:RequiredFieldValidator>--%>
                                        </label>
                                    </td>
                                    <td colspan="2">
                                        <label>
                                            <asp:TextBox ID="txtport2" runat="server" MaxLength="10" onkeyup="return mak('Span8',10,this)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label runat="server" ID="Label42" Text="Max " CssClass="labelcount"></asp:Label>
                                            <span id="Span8">10</span>
                                            <asp:Label ID="Label43" runat="server" Text="(0-9)" CssClass="labelcount"></asp:Label>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="txtport2" ValidChars="0147852369">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label22" runat="server" Text="Enter Password"></asp:Label>
                                        <asp:Label ID="Label23" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="6"
                                            ErrorMessage="*" ControlToValidate="txtoutpass"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="24"
                                            ErrorMessage="*" ControlToValidate="txtoutpass"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Invalid"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_!%#$.@+a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtoutpass" ValidationGroup="6"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtoutpass" runat="server" ValidationGroup="6" MaxLength="30" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/^'&*()>:;={}[]|\/]/g,/^[\!#$%a-zA-Z0-9_.+@ \s]+$/,'Span7',30)"
                                            TextMode="Password" Width="200px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label34" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span7" cssclass="labelcount">30</span>
                                        <asp:Label ID="Label6" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ @ . ! % # $)"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:Button ID="Button3" runat="server" Text="Test Email" CssClass="btnSubmit" OnClick="Button3_Click"
                                        ValidationGroup="24" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Set this account as default outgoing Email Server" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:CheckBox ID="CheckBox3" runat="server" Text="Email Signature" Checked="true"
                                            Enabled="False" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td colspan="2">
                                    <asp:Button ID="imgbtnsubmitCabinetAdd" CssClass="btnSubmit" runat="server" Text="Submit"
                                        ValidationGroup="6" OnClick="imgbtnsubmitCabinetAdd_Click"></asp:Button>
                                    <asp:Button ID="imgupdate" CssClass="btnSubmit" runat="server" Text="Update" ValidationGroup="6"
                                        OnClick="imgupdate_Click"></asp:Button>
                                    <asp:Button ID="imgcancel" CssClass="btnSubmit" runat="server" Text="Cancel" OnClick="imgcancel_Click">
                                    </asp:Button>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label25" runat="server" Text="List of Your Email Accounts"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <div style="float: right;">
                                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                        CausesValidation="False" OnClick="Button1_Click" />
                                    <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        type="button" value="Print" visible="false" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label26" runat="server" Text="Filter by Business"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlbusinessfil" runat="server" Width="203px" AutoPostBack="True"
                                     Enabled="false"   OnSelectedIndexChanged="ddlbusinessfil_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label27" runat="server" Text="Select by Employee"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlempfil" runat="server" Width="203px" AutoPostBack="True"
                                    Enabled="false"    OnSelectedIndexChanged="ddlempfil_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="left">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" Font-Italic="True" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label runat="server" ID="name" Font-Italic="true" Text="Business :"></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label28" runat="server" Font-Italic="True" Text="List of Email Accounts of the company"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cc11:PagingGridView AutoGenerateColumns="False" ID="GridEmail" runat="server" Width="100%"
                                                    OnRowCommand="GridEmail_RowCommand" EmptyDataText="No Record Found." CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="ID"
                                                    AllowSorting="True" OnSorting="GridEmail_Sorting" OnRowDataBound="GridEmail_RowDataBound"
                                                    OnRowEditing="GridEmail_RowEditing" OnRowDeleting="GridEmail_RowDeleting" OnRowCancelingEdit="GridEmail_RowCancelingEdit"
                                                    OnSelectedIndexChanged="GridEmail_SelectedIndexChanged" OnRowUpdating="GridEmail_RowUpdating"
                                                    OnPageIndexChanging="GridEmail_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="Wname" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblempname123" runat="server" Text='<%# Bind("Wname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="EmployeeName" HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblempname" runat="server" Text='<%# Bind("EmployeeName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Master Email" HeaderStyle-HorizontalAlign="Left" SortExpression="EmailId"
                                                            HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblemail" runat="server" Text='<%# Bind("EmailId")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Outgoing Email ID" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="OutEmailID" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("OutEmailID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Incoming Email ID" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="InEmailID" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="inLabel1" runat="server" Text='<%# Bind("InEmailID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Default" HeaderStyle-HorizontalAlign="Left" SortExpression="SetforOutgoingemail"
                                                            HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkprint" runat="server" Enabled="false" Checked='<%#Eval("SetforOutgoingemail") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View Signature" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton49" runat="server" ToolTip="View" ImageUrl="~/Account/images/viewprofile.jpg"
                                                                    CommandName="Viewsignature" CommandArgument='<%# Eval("ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" ToolTip="Edit" CommandName="Edit"
                                                                    ImageUrl="~/Account/images/edit.gif" CommandArgument='<%# Eval("ID") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinkbb" ToolTip="Delete" runat="server" CommandName="Delete1"
                                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                    CommandArgument='<%# Eval("ID") %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </cc11:PagingGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="update" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ImageButton1" />
                            <asp:AsyncPostBackTrigger ControlID="imgupdate" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="imgcancel" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="imgbtnsubmitCabinetAdd" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                    <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                    <input id="hdnMaintypeAdd" runat="Server" name="hdnMaintypeAdd" type="hidden" style="width: 4px" />
                    <input id="Hidden1" runat="Server" name="Hidden1" type="hidden" style="width: 4px" />
                    <asp:Panel ID="pnlMainTypeAdd" runat="server" BorderStyle="Outset" CssClass="GridPnl"
                        Height="120px" Width="100%" BackColor="#CCCCCC" BorderColor="#333333">
                        <table width="100%">
                            <tr>
                                <td class="secondtblfc2">
                                    <asp:UpdatePanel ID="UpdatePanelMainTypeAdd" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table id="subinnertbl1" cellspacing="0" cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td class="secondtblfc1">
                                                            Confirm Message
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlMainTypeAdd1" runat="server" Width="100%" Height="95px">
                                                                <table id="subinnertbl1" cellspacing="0" cellpadding="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="2" style="font-weight: bold; padding-left: 10px; font-size: 12px; font-family: Arial;
                                                                                text-align: left; vertical-align: top;">
                                                                                <br />
                                                                                Are you sure you want to delete an Email Account?
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-left: 15px;">
                                                                                <br />
                                                                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:Button ID="ImageButton1" runat="server" Text="Go" CausesValidation="False" OnClick="ImageButton1_Click">
                                                                                        </asp:Button>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </td>
                                                                            <td>
                                                                                <br />
                                                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Account/images/btncancel.jpg"
                                                                                    AlternateText="Close" CausesValidation="False"></asp:ImageButton>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalPopupExtender9" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="pnlMainTypeAdd" TargetControlID="hdnMaintypeAdd" CancelControlID="ImageButton2"
                        X="300" Y="-200" Drag="true">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" Height="130px" Width="500px"
                        BackColor="#CCCCCC" BorderColor="#666666" BorderWidth="5px">
                        <table id="innertbl1">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel3" runat="server" Width="100%">
                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            <label>
                                                                You are trying to delete a default outgoing email account. Please select any other
                                                                email account as default outgoing email account before trying to delete this account
                                                                again.
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Button ID="ImageButton3" Text="Ok" runat="server" CssClass="btnSubmit" CausesValidation="False" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel1" TargetControlID="Hidden1" CancelControlID="ImageButton3"
                        X="250" Y="-100" Drag="true">
                    </cc1:ModalPopupExtender>
                </fieldset>

                <fieldset>
                    <legend>
                        <asp:Label ID="Label49" runat="server" Text="List of Your Panding Email Accounts"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label50" runat="server" Text="Filter by Business"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlbusinessfil1" runat="server" Width="203px" AutoPostBack="True"
                                     Enabled="false"   OnSelectedIndexChanged="ddlbusinessfil_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label51" runat="server" Text="Select by Employee"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlempfil1" runat="server" Width="203px" AutoPostBack="True"
                                    Enabled="false"    OnSelectedIndexChanged="ddlempfil_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Panel ID="Panel6" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="left">
                                            <td>
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cc11:PagingGridView AutoGenerateColumns="False" ID="FillGridPanding1" runat="server" Width="100%"
                                                    OnRowCommand="GridEmail_RowCommand" EmptyDataText="No Record Found." CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="ID"
                                                    AllowSorting="True" OnSorting="GridEmail_Sorting" OnRowDataBound="GridEmail_RowDataBound"
                                                    OnRowEditing="GridEmail_RowEditing" OnRowDeleting="GridEmail_RowDeleting" OnRowCancelingEdit="GridEmail_RowCancelingEdit"
                                                    OnSelectedIndexChanged="GridEmail_SelectedIndexChanged" OnRowUpdating="GridEmail_RowUpdating"
                                                    OnPageIndexChanging="GridEmail_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="Wname" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblempname123" runat="server" Text='<%# Bind("Wname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="EmployeeName" HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblempname" runat="server" Text='<%# Bind("EmployeeName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Master Email" HeaderStyle-HorizontalAlign="Left" SortExpression="EmailId"
                                                            HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblemail" runat="server" Text='<%# Bind("EmailId")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Outgoing Email ID" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="OutEmailID" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("OutEmailID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Incoming Email ID" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="InEmailID" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="inLabel1" runat="server" Text='<%# Bind("InEmailID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Default" HeaderStyle-HorizontalAlign="Left" SortExpression="SetforOutgoingemail"
                                                            HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkprint" runat="server" Enabled="false" Checked='<%#Eval("SetforOutgoingemail") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View Signature" HeaderStyle-HorizontalAlign="Left"
                                                         Visible="false"   HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton49" runat="server" ToolTip="View" ImageUrl="~/Account/images/viewprofile.jpg"
                                                                    CommandName="Viewsignature" CommandArgument='<%# Eval("ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                          Visible="false"  HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" ToolTip="Edit" CommandName="Edit"
                                                                    ImageUrl="~/Account/images/edit.gif" CommandArgument='<%# Eval("ID") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg" Visible="false" 
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinkbb" ToolTip="Delete" runat="server" CommandName="Delete1"
                                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                    CommandArgument='<%# Eval("ID") %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </cc11:PagingGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                           
                        </Triggers>
                    </asp:UpdatePanel>
                    
                    
                    
                    
                </fieldset>
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel4" runat="server" BorderStyle="Solid" BackColor="#CCCCCC" BorderColor="#999999"
                                BorderWidth="10px">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label36" runat="server" Text="Email Signature Manage"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="1">
                                            <asp:ImageButton ID="ibtnCancelCabinetAdd" runat="server" AlternateText="Close" CausesValidation="False"
                                                ImageUrl="~/Account/images/closeicon.png" OnClick="ibtnCancelCabinetAdd_Click"
                                                Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel5" runat="server">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 35%">
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblmmms" runat="server" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <label>
                                                                <asp:Label ID="Label35" runat="server" Text="Enter your Signature"></asp:Label>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="*"
                                                                    ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)" ControlToValidate="TextBox1"
                                                                    ValidationGroup="7"></asp:RegularExpressionValidator>
                                                                <br />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="*"
                                                                    ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)" ControlToValidate="TextBox2"
                                                                    ValidationGroup="7"></asp:RegularExpressionValidator>
                                                                <br />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="*"
                                                                    ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)" ControlToValidate="TextBox3"
                                                                    ValidationGroup="7"></asp:RegularExpressionValidator>
                                                                <br />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                                    ErrorMessage="*" ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)" ControlToValidate="TextBox4"
                                                                    ValidationGroup="7"></asp:RegularExpressionValidator>
                                                                <br />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                                    ErrorMessage="*" ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)" ControlToValidate="TextBox5"
                                                                    ValidationGroup="7"></asp:RegularExpressionValidator>
                                                                <br />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                                    ErrorMessage="*" ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)" ControlToValidate="TextBox6"
                                                                    ValidationGroup="7"></asp:RegularExpressionValidator>
                                                                <br />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                                                    ErrorMessage="*" ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)" ControlToValidate="TextBox7"
                                                                    ValidationGroup="7"></asp:RegularExpressionValidator>
                                                                <br />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                                                    ErrorMessage="*" ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)" ControlToValidate="TextBox8"
                                                                    ValidationGroup="7"></asp:RegularExpressionValidator>
                                                                <br />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                                                                    ErrorMessage="*" ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)" ControlToValidate="TextBox9"
                                                                    ValidationGroup="7"></asp:RegularExpressionValidator>
                                                                <br />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                                                                    ErrorMessage="*" ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)" ControlToValidate="TextBox10"
                                                                    ValidationGroup="7"></asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="TextBox1" runat="server" MaxLength="100" Width="450px" BorderStyle="None"></asp:TextBox>
                                                                <asp:TextBox ID="TextBox2" runat="server" MaxLength="100" Width="450px" BorderStyle="None"></asp:TextBox>
                                                                <asp:TextBox ID="TextBox3" runat="server" MaxLength="100" Width="450px" BorderStyle="None"></asp:TextBox>
                                                                <asp:TextBox ID="TextBox4" runat="server" MaxLength="100" Width="450px" BorderStyle="None"></asp:TextBox>
                                                                <asp:TextBox ID="TextBox5" runat="server" MaxLength="100" Width="450px" BorderStyle="None"></asp:TextBox>
                                                                <asp:TextBox ID="TextBox6" runat="server" MaxLength="100" Width="450px" BorderStyle="None"></asp:TextBox>
                                                                <asp:TextBox ID="TextBox7" runat="server" MaxLength="100" Width="450px" BorderStyle="None"></asp:TextBox>
                                                                <asp:TextBox ID="TextBox8" runat="server" MaxLength="100" Width="450px" BorderStyle="None"></asp:TextBox>
                                                                <asp:TextBox ID="TextBox9" runat="server" MaxLength="100" Width="450px" BorderStyle="None"></asp:TextBox>
                                                                <asp:TextBox ID="TextBox10" runat="server" MaxLength="100" Width="450px" BorderStyle="None"></asp:TextBox>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="ImageButton4" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="ImageButton4_Click"
                                                                ValidationGroup="7" />
                                                            <asp:Button ID="btnsigup" runat="server" Text="Update" OnClick="btnsigup_Click" CssClass="btnSubmit"
                                                                ValidationGroup="7" Visible="False" />
                                                            <asp:Button ID="imgbtncancel" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="imgbtncancel_Click"
                                                                Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                            <%--<input id="hdnMaintypeAdd1" runat="Server" name="hdnMaintypeAdd1" type="hidden" style="width: 4px" />--%>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                PopupControlID="Panel4" TargetControlID="HiddenButton222" X="400" Y="100">
                            </cc1:ModalPopupExtender>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
