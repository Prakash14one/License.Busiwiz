<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="EmployeeMaster.aspx.cs" Inherits="ShoppingCart_Admin_Default" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <asp:Panel ID="Panel6" runat="server" Visible="false" Width="100%">
                    <div>
                        <fieldset>
                            <legend></legend>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:ImageButton ImageUrl="~/images/o1.jpg" ID="ImageButton9" runat="server" OnClick="ImageButton9_Click"
                                            Visible="false" />
                                        <asp:ImageButton ImageUrl="~/images/h1.jpg" ID="ImageButton22" runat="server" OnClick="ImageButton22_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ImageUrl="~/images/o2.jpg" ID="ImageButton10" runat="server" OnClick="ImageButton10_Click" />
                                        <asp:ImageButton ImageUrl="~/images/h2.jpg" ID="ImageButton23" runat="server" Visible="false"
                                            OnClick="ImageButton23_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ImageUrl="~/images/o3.jpg" ID="ImageButton11" runat="server" OnClick="ImageButton11_Click" />
                                        <asp:ImageButton ImageUrl="~/images/h3.jpg" ID="ImageButton24" runat="server" Visible="false"
                                            OnClick="ImageButton24_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ImageUrl="~/images/o4.jpg" ID="ImageButton12" runat="server" OnClick="ImageButton12_Click" />
                                        <asp:ImageButton ImageUrl="~/images/h4.jpg" ID="ImageButton25" runat="server" Visible="false"
                                            OnClick="ImageButton25_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ImageUrl="~/images/o5.jpg" ID="ImageButton13" runat="server" OnClick="ImageButton13_Click" />
                                        <asp:ImageButton ImageUrl="~/images/h5.jpg" ID="ImageButton26" runat="server" Visible="false"
                                            OnClick="ImageButton26_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ImageUrl="~/images/o6.jpg" ID="ImageButton18" runat="server" OnClick="ImageButton18_Click" />
                                        <asp:ImageButton ImageUrl="~/images/h6.jpg" ID="ImageButton27" runat="server" Visible="false"
                                            OnClick="ImageButton27_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ImageUrl="~/images/o7.jpg" ID="ImageButton19" runat="server" OnClick="ImageButton19_Click" />
                                        <asp:ImageButton ImageUrl="~/images/h7.jpg" ID="ImageButton28" runat="server" Visible="false"
                                            OnClick="ImageButton28_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ImageUrl="~/images/o8.jpg" ID="ImageButton29" runat="server" OnClick="ImageButton29_Click" />
                                        <asp:ImageButton ImageUrl="~/images/h8.jpg" ID="ImageButton30" runat="server" Visible="false"
                                            OnClick="ImageButton30_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </asp:Panel>
                <%--<fieldset>--%>
                <%--  <legend>
                        <asp:Label ID="lbladd" runat="server"></asp:Label>
                    </legend>--%>
                <div style="float: right;">
                    <asp:Button ID="btnshowparty" runat="server" Text="Add Employee" OnClick="btnshowparty_Click"
                        Visible="true" CssClass="btnSubmit" />
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnlparty" runat="server" Visible="false" Width="100%">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div style="float: right">
                                <asp:ImageButton ImageUrl="~/images/next1.jpg" ID="ImageButton14" runat="server"
                                    OnClick="ImageButton14_Click" ValidationGroup="1" />
                            </div>
                            <div style="clear: both;">
                            </div>
                            <fieldset>
                                <legend>Basic Information</legend>
                                <div style="clear: both;">
                                </div>
                                <div>
                                    <label>
                                        Are you hiring existing candidate and want to import his information ? Yes
                                    </label>
                                    <asp:CheckBox ID="CheckBox6" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox6_CheckedChanged" />
                                    <asp:Panel ID="panelexistingcandidate" runat="server" Visible="false">
                                        <label>
                                            Candidate
                                        </label>
                                        <label>
                                            <asp:DropDownList ID="ddlcandidate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcandidate_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </asp:Panel>
                                </div>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label6" runat="server" Text="Last Name"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label20" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtlastname"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                                    SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtlastname"
                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label7" runat="server" Text="First Name"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label36" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtfirstname"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                                    SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtfirstname"
                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label8" runat="server" Text="Middle Initial"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtintialis"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                                    SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtintialis"
                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td valign="top" style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label9" runat="server" Text="Employee Number"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtlastname" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                                    Width="160px" onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'div1',30)"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label40" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                <span id="div1" class="labelcount">30</span><asp:Label ID="Label34" CssClass="labelcount"
                                                    runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtfirstname" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                                    Width="160px" onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'Span1',30)"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label31" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                <span id="Span1" class="labelcount">30</span><asp:Label ID="Label21" CssClass="labelcount"
                                                    runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtintialis" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                                    Width="160px" onkeyup="return check(this,/[\\/!.@#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'Span2',15)"
                                                    AutoPostBack="True" OnTextChanged="txtintialis_TextChanged"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label32" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                <span id="Span2" class="labelcount">15</span><asp:Label ID="Label22" CssClass="labelcount"
                                                    runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                            </label>
                                        </td>
                                        <td valign="top">
                                            <label>
                                                <asp:TextBox ID="TextBox1" MaxLength="4" Enabled="false" runat="server"></asp:TextBox>
                                            </label>
                                            <%--     <label>
                                            <asp:TextBox ID="TextBox2" Width="45px" MaxLength="4" Enabled="false" runat="server"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:TextBox ID="TextBox3" Width="45px" MaxLength="4" Enabled="false" runat="server"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:TextBox ID="TextBox4" Width="45px" MaxLength="4" Enabled="false" runat="server"></asp:TextBox>
                                        </label>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label167" runat="server" Text="Sex"></asp:Label>
                                            </label>
                                            <asp:RadioButtonList ID="Radiogender" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Text="Male" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label11" runat="server" Text="Social Insurance No./Security No./"></asp:Label>
                                                <br />
                                                <asp:Label ID="Label173" runat="server" Text="Other Government Issued ID"></asp:Label>
                                                <asp:Label ID="Label26" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtsecurityno"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid"
                                                    SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtsecurityno"
                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lbllogo" runat="server" Text="Employee Photo"></asp:Label>
                                            </label>
                                        </td>
                                        <td valign="top" rowspan="3">
                                            <asp:Image ID="imgLogo" runat="server" Height="170px" Width="126px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <label>
                                                <asp:Label ID="Label10" runat="server" Text="Date of Birth"></asp:Label>
                                                <asp:Label ID="Label25" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtdateofbirth"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rghjk" runat="server" ErrorMessage="*" ControlToValidate="txtdateofbirth"
                                                    ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                            </label>
                                            <label>
                                                <asp:TextBox ID="txtdateofbirth" runat="server" Width="70px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtdateofbirth"
                                                    TargetControlID="txtdateofbirth">
                                                </cc1:CalendarExtender>
                                            </label>
                                        </td>
                                        <td valign="top" align="left">
                                            <label>
                                                <asp:TextBox ID="txtsecurityno" runat="server" MaxLength="40" onKeydown="return mask(event)"
                                                    onkeyup="return check(this,/[\\/!._@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span4',40)"
                                                    Height="22px"></asp:TextBox>
                                                <asp:Label ID="Label35" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                <span id="Span4" class="labelcount">40</span><asp:Label ID="Label27" CssClass="labelcount"
                                                    runat="server" Text="(A-Z 0-9)"></asp:Label>
                                            </label>
                                        </td>
                                        <td valign="top">
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btnSubmit" />
                                            <asp:Button ID="imgBtnImageUpdate" Text="Upload" runat="server" CssClass="btnSubmit"
                                                OnClick="imgBtnImageUpdate_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td valign="middle">
                                        </td>
                                        <td valign="middle">
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>Contact Information</legend>
                                <div style="float: right">
                                    <asp:Button ID="Button5" Visible="false" CssClass="btnSubmit" runat="server" Text="Update"
                                        OnClick="Button5_Click" />
                                    <asp:Button ID="Button6" Visible="false" CssClass="btnSubmit" runat="server" Text="Delete"
                                        OnClick="Button6_Click" />
                                    <asp:Button ID="addnewaddr" Visible="false" CssClass="btnSubmit" runat="server" Text="Add New Address"
                                        OnClick="addnewaddr_Click" />
                                </div>
                                <asp:Panel ID="addnewaddrpnl" runat="server">
                                    <label>
                                        <asp:Label ID="Label97" runat="server" Text="Effective from: "></asp:Label>
                                    </label>
                                    <label>
                                        <asp:TextBox ID="txtstartdate" runat="server" Width="75px" MaxLength="10"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton2"
                                            Format="MM/dd/yyyy" TargetControlID="txtstartdate">
                                        </cc1:CalendarExtender>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                    </label>
                                    <label>
                                        <asp:Label ID="lblAddresslbl" runat="server" Text="Street Address"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:TextBox ID="tbAddress" runat="server" Width="300px" MaxLength="100" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z.,A-Z0-9_ ]+$/,'Span3',100)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label2" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span3" class="labelcount">100</span><asp:Label ID="Label1" CssClass="labelcount"
                                            runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Character"
                                            SetFocusOnError="True" ValidationExpression="^([.,_a-zA-Z0-9\s]*)" ControlToValidate="tbAddress"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="lblCountrylbl" runat="server" Text="Country"></asp:Label>
                                        <asp:Label ID="Label16" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="rdg3" runat="server" ControlToValidate="ddlCountry"
                                            ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                            ValidationGroup="1" Width="151px">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblStatelbl" runat="server" Text="State"></asp:Label>
                                        <asp:Label ID="Label17" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="rd1n1" runat="server" ControlToValidate="ddlState"
                                            ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                            ValidationGroup="1" Width="151px">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblCitylbl" runat="server" Text="City"></asp:Label>
                                        <asp:Label ID="Label18" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFielgdValidator12" runat="server" ControlToValidate="ddlCity"
                                            ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCity" runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"
                                            ValidationGroup="1" Width="151px">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblZipcodelbl" runat="server" Text="Zip/Postal Code"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                                            SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="tbZipCode"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="tbZipCode" runat="server" ValidationGroup="1" Width="145px" MaxLength="15"
                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9]+$/,'Span6',15)"></asp:TextBox>
                                        <asp:Label ID="Label3" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span6" class="labelcount">15</span><asp:Label ID="Label4" CssClass="labelcount"
                                            runat="server" Text="(A-Z 0-9)"></asp:Label>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="lblPhonelbl" runat="server" Text="Home Phone"></asp:Label>
                                        <asp:Label ID="Label23" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="tbPhone"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="*"
                                            SetFocusOnError="True" ValidationExpression="^([0-9\s]*)" ControlToValidate="tbPhone"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="tbPhone" runat="server" Width="125px" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span5',15)"
                                            MaxLength="15"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="tbPhone_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" TargetControlID="tbPhone" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="Label13" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span5" class="labelcount">15</span><asp:Label ID="Label15" CssClass="labelcount"
                                            runat="server" Text="(0-9)"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblExtensionlbl" runat="server" Text="Mobile Phone"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                            ControlToValidate="tbExtension" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="tbExtension" runat="server" Width="125px" MaxLength="15" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span7',15)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="tbExtension_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" TargetControlID="tbExtension" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="Label24" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span7" class="labelcount">15</span></label><label><asp:Label ID="lblEmaillbl0"
                                            runat="server" Text="Personal Email"></asp:Label>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="tbEmail"
                                                ErrorMessage="Invalid Email ID" Font-Size="14px" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                            <asp:TextBox ID="tbEmail" MaxLength="40" runat="server" Width="145px" AutoPostBack="True"
                                                OnTextChanged="tbEmail_TextChanged"></asp:TextBox>
                                            <asp:Label ID="duplicatetbemail" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </label>
                                </asp:Panel>
                                <div style="clear: both;">
                                </div>
                                <asp:Panel ID="pnllabeladdress" Visible="false" runat="server">
                                    <label>
                                        <asp:Label ID="Label98" runat="server" Text="Effective from: "></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lbldate" runat="server" Text="" ForeColor="#457cec"></asp:Label>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label99" runat="server" Text="Street Address"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblstreetadd" runat="server" Text="" ForeColor="#457cec"></asp:Label>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label102" runat="server" Text="City"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblcity" runat="server" Text="" ForeColor="#457cec"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label104" runat="server" Text="State"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblstate" runat="server" Text="" ForeColor="#457cec"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label106" runat="server" Text="Country"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblcountry" runat="server" Text="" ForeColor="#457cec"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label108" runat="server" Text="Zip Code"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblzipcode" runat="server" Text="" ForeColor="#457cec"></asp:Label>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label111" runat="server" Text="Home Phone"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblphone" runat="server" Text="" ForeColor="#457cec"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label115" runat="server" Text="Mobile Phone"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblext" runat="server" Text="" ForeColor="#457cec"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label117" runat="server" Text="Personal Email"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblemail" runat="server" Text="" ForeColor="#457cec"></asp:Label>
                                    </label>
                                </asp:Panel>
                                <div style="clear: both;">
                                </div>
                                <asp:Panel ID="pnllastaddress" Visible="false" runat="server">
                                    <label>
                                        <asp:Label ID="Label100" runat="server" Text="Previous Addresses"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="ddlpreviousaddress" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlpreviousaddress_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </asp:Panel>
                            </fieldset>
                        </asp:View>
                        <div style="clear: both;">
                        </div>
                        <asp:View ID="View2" runat="server">
                            <div style="float: right">
                                <asp:ImageButton ImageUrl="~/images/previous1.jpg" ID="ImageButton32" runat="server"
                                    OnClick="ImageButton32_Click" />
                                <asp:ImageButton ImageUrl="~/images/next1.jpg" ID="ImageButton15" runat="server"
                                    OnClick="ImageButton15_Click" ValidationGroup="1" />
                            </div>
                            <div style="clear: both; padding-bottom: 0px;">
                            </div>
                            <fieldset>
                                <legend>Work Information</legend>
                                <div style="clear: both;">
                                </div>
                                <table>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblbusinessnamelbl" Text="Business Name" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlwarehouse" runat="server" Width="151px" ValidationGroup="1"
                                                    OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblPartyType" Text="User Category" runat="server"></asp:Label>
                                                <asp:Label ID="Label14" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPartyType"
                                                    InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlPartyType" runat="server" Width="151px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlPartyType_SelectedIndexChanged" ValidationGroup="1">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td valign="top">
                                            <label>
                                                <asp:Label ID="lblDepartmentlbl" Text="Department " runat="server"></asp:Label>
                                                <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddldept"
                                                    ErrorMessage="RequiredFieldValidator" InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddldept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldept_SelectedIndexChanged"
                                                    Width="151px" ValidationGroup="1">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td valign="bottom">
                                            
                                        </td>
                                        <td valign="top">
                                            <label>
                                                <asp:Label ID="lblDesihdg" Text="Designation" runat="server"></asp:Label>
                                                <asp:Label ID="Label12" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="ddldesignation"
                                                    ErrorMessage="RequiredFieldValidator" InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddldesignation" runat="server" ValidationGroup="1" Width="151px"
                                                    OnSelectedIndexChanged="ddldesignation_SelectedIndexChanged1" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td valign="bottom">
                                            <label>
                                           <asp:Button ID="btnvwaccessright" runat="server" CssClass="btnSubmit" Text="View Rights For Designation" OnClick="btnvwaccessright_Click" />
                                           </label>
                                        </td>
                                        <td colspan="2" valign="bottom">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblSupervisorNamelbl" Text="Supervisor Name" runat="server"></asp:Label>
                                                <asp:Label ID="Label172" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlemp"
                                                    ErrorMessage="RequiredFieldValidator" InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlemp" runat="server" Width="151px">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblStatuslbl" Text="Status" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlActive" runat="server" Width="148px">
                                                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td valign="top">
                                            <label>
                                                <asp:Label ID="lblBatchnamelbl" Text="Batch Name" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlbatch" runat="server" Width="151px">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td valign="bottom" align="left">
                                            
                                        </td>
                                        <td valign="top">
                                            <label>
                                                <asp:Label ID="lblEmployeeTypelbl" Text="Employee Type" runat="server"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="ddlemptype"
                                                    ErrorMessage="RequiredFieldValidator" InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlemptype" runat="server" Width="151px">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td valign="bottom">
                                        
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label87" runat="server" Text="Employee Status"></asp:Label>
                                                <asp:Label ID="Label89" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlstatusname"
                                                    ErrorMessage="RequiredFieldValidator" InitialValue="0" ValidationGroup="st">*</asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlstatusname" runat="server" Width="151px">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td valign="bottom">
                                            <asp:ImageButton ID="imgadddstaname" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                             Visible="false"   ToolTip="AddNew" Width="20px" ImageAlign="Bottom" OnClick="imgadddstaname_Click" />
                                        </td>
                                        <td valign="bottom">
                                            <asp:ImageButton ID="imgrefreshstaname" runat="server" AlternateText="Refresh" Height="20px"
                                             Visible="false"   ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                                ImageAlign="Bottom" OnClick="imgrefreshstaname_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label90" runat="server" Text="Work Phone"></asp:Label>
                                                <asp:Label ID="Label91" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="workphone"
                                                    ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server"
                                                    ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^([0-9\s]*)" ControlToValidate="workphone"
                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="workphone" runat="server" Width="146px" onKeydown="return mask(event)"
                                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span20',15)"
                                                    MaxLength="15"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                    TargetControlID="workphone" ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:Label ID="Label92" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                <span id="Span20" class="labelcount">15</span><asp:Label ID="Label93" CssClass="labelcount"
                                                    runat="server" Text="(0-9)"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label94" runat="server" Text="Ext."></asp:Label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator24" runat="server"
                                                    ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                    ControlToValidate="workext" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="workext" runat="server" Width="146px" MaxLength="10" onKeydown="return mask(event)"
                                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span21',10)"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                    TargetControlID="workext" ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:Label ID="Label95" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                <span id="Span21" class="labelcount">10</span></label>
                                        </td>
                                        <td colspan="2" valign="top">
                                            <label>
                                                <asp:Label ID="Label96" runat="server" Text="Work Email"></asp:Label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server"
                                                    ControlToValidate="txtworkemail" ErrorMessage="Invalid Email ID" Font-Size="14px"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="txtworkemail" runat="server" MaxLength="40" Width="145px" AutoPostBack="True"
                                                    OnTextChanged="txtworkemail_TextChanged"></asp:TextBox>
                                                <asp:Label ID="duplicateworkemail" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            </label>
                                        </td>
                                        <td valign="top">
                                            <label>
                                                <asp:Label ID="lblDatejoininglbl" Text="Date of Joining" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtjoindate" runat="server" ValidationGroup="1" Width="75px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtjoindate">
                                                </cc1:CalendarExtender>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" TargetControlID="txtjoindate">
                                                </cc1:MaskedEditExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtjoindate"
                                                    ErrorMessage="RequiredFieldValidator" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pandsds" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="lblEmployeeRolelbl" runat="server" Text="Page Access Rights Role"></asp:Label>
                                                    <asp:DropDownList ID="ddlemprole" runat="server" AutoPostBack="True" ValidationGroup="1"
                                                        Width="151px">
                                                    </asp:DropDownList>
                                                </label>
                                                <asp:ImageButton ID="imgadddrolema" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                    ToolTip="AddNew" Width="20px" ImageAlign="Bottom" OnClick="imgadddrolema_Click" />
                                                <asp:ImageButton ID="imgrefreshrolema" runat="server" AlternateText="Refresh" Height="20px"
                                                    ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                                    ImageAlign="Bottom" OnClick="imgrefreshrolema_Click" />
                                            </asp:Panel>
                                        </td>
                                        <td valign="bottom">
                                        </td>
                                    </tr>
                                </table>
                                <div style="clear: both;">
                                </div>
                                <asp:Panel ID="Panel7" Visible="false" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td width="55%">
                                                <label>
                                                    <asp:Label ID="Label28" Text="Manage Employee Status Category" runat="server"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chksunday" runat="server" AutoPostBack="True" Checked="true" OnCheckedChanged="chksunday_CheckedChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel3" Visible="true" runat="server" Width="350px">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label86" runat="server" Text="Status Category"></asp:Label>
                                                                    <asp:Label ID="Label88" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlstatuscategory"
                                                                        ErrorMessage="RequiredFieldValidator" InitialValue="0" ValidationGroup="st">*</asp:RequiredFieldValidator>
                                                                    <asp:DropDownList ID="ddlstatuscategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstatuscategory_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                            <td valign="bottom">
                                                                <asp:ImageButton ID="imgadddstacat" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                    Visible="false" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" OnClick="imgadddstacat_Click" />
                                                            </td>
                                                            <td valign="bottom">
                                                                <asp:ImageButton ID="imgrefreshstacat" runat="server" AlternateText="Refresh" Height="20px"
                                                                    Visible="false" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh"
                                                                    Width="20px" ImageAlign="Bottom" OnClick="imgrefreshstacat_Click" />
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <br />
                                                                    <asp:Button ID="btnadd" runat="server" Text="Add" ValidationGroup="st" CssClass="btnSubmit"
                                                                        OnClick="btnadd_Click" />
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5">
                                                                <asp:GridView ID="grdstatuscategory" runat="server" PageSize="5" DataKeyNames="StatusId"
                                                                    Width="100%" AutoGenerateColumns="False" Visible="false" GridLines="None" CssClass="mGrid"
                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowCommand="grdstatuscategory_RowCommand"
                                                                    OnRowDeleting="grdstatuscategory_RowDeleting" OnRowEditing="grdstatuscategory_RowEditing"
                                                                    EmptyDataText="No Record Found.">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Status Category" HeaderStyle-HorizontalAlign="Left"
                                                                            ItemStyle-Width="40%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblstatuscategory" runat="server" Text='<%#Eval("StatusCategory")%>'></asp:Label>
                                                                                <asp:Label ID="lblstatuscategoryid" runat="server" Text='<%#Eval("StatusCategoryMasterId")%>'
                                                                                    Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("StatusName")%>'></asp:Label>
                                                                                <asp:Label ID="lblstatusid" runat="server" Text='<%#Eval("StatusId")%>' Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnedit" CommandName="Edit" runat="server" ImageUrl="~/Account/images/edit.gif"
                                                                    ToolTip="Edit"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%><asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnremove" CommandName="Delete" runat="server" ImageUrl="~/Account/images/delete.gif"
                                                                    ToolTip="Delete"></asp:ImageButton>
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
                                    </table>
                                </asp:Panel>
                                <asp:Panel runat="server" Visible="false" ID="Pandsdkdkdwww">
                                    <table>
                                        <tr>
                                            <td align="right">
                                                <label>
                                                    <asp:Label ID="Label80" runat="server" Text="Assigned Account Manager Id" Visible="False"></asp:Label></label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlAssAccManagerId" runat="server" Enabled="False" Visible="false"
                                                    Width="151px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <label>
                                                    <asp:Label ID="Label81" runat="server" Text=" Assigned Sales Deptt Id" Visible="False"></asp:Label></label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlAssSalDeptId" runat="server" Enabled="False" Width="151px"
                                                    Visible="false">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <label>
                                                    <asp:Label ID="Label84" runat="server" Text="Assigned Shipping Deptt Id" Visible="False"></asp:Label></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <label>
                                                    <asp:Label ID="Label82" runat="server" Text="Assigned Receiving Deptt Id" Visible="False"></asp:Label></label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlAssRecieveDeptId" runat="server" Enabled="False" Width="151px"
                                                    Visible="false">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <label>
                                                    <asp:Label ID="Label83" runat="server" Text=" Assigned Purchase Deptt Id" Visible="False"></asp:Label></label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlAssPurDeptId" runat="server" Enabled="False" Width="151px"
                                                    Visible="false">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlAssShipDeptId" runat="server" Enabled="False" Visible="false"
                                                    Width="151px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <label>
                                                    <asp:Label ID="lblPartyMasterCategory" Visible="false" Text="Party Master Category"
                                                        runat="server"></asp:Label></label>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="ddlpartycate" Visible="false" runat="server" Width="150px">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblpno" runat="server" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlGroup" runat="server" Width="151px" ValidationGroup="1"
                                                    Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </fieldset>
                        </asp:View>
                        <div style="clear: both;">
                        </div>
                        <asp:View ID="View3" runat="server">
                            <div style="float: right">
                                <asp:ImageButton ImageUrl="~/images/previous1.jpg" ID="ImageButton33" runat="server"
                                    ValidationGroup="1" onclick="ImageButton33_Click" />
                                <asp:ImageButton ImageUrl="~/images/next1.jpg" ID="ImageButton16" runat="server"
                                    OnClick="ImageButton16_Click" ValidationGroup="1" />
                            </div>
                            <div style="clear: both;">
                            </div>
                            
                            <asp:Panel ID="panelympanel" runat="server" Visible="false">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label37" Text="Add employee ID information (barcode, biometric scanner etc.)"
                                                runat="server"></asp:Label></label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkempbarcode" runat="server" AutoPostBack="True" OnCheckedChanged="chkempbarcode_CheckedChanged" />
                                    </td>
                                </tr>
                            </asp:Panel>
                            <fieldset>
                                <legend>Access Rights </legend>
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <label class="Attend">
                                                <label>
                                                    <asp:Label ID="Label30" Text="This employee belongs to - " runat="server"></asp:Label>
                                                    <asp:Label ID="Label59" Text="" runat="server" ForeColor="Black"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label178" Text="" runat="server"></asp:Label>
                                                    <asp:Label ID="Label174" Text=" has inherent access right for the designation - "
                                                        runat="server"></asp:Label>
                                                    <asp:Label ID="Label176" Text="" runat="server" ForeColor="Black"></asp:Label>
                                                    <asp:Label ID="Label175" runat="server" Text=" for business - "></asp:Label>
                                                    <asp:Label ID="Label177" Text="" runat="server" ForeColor="Black"></asp:Label>
                                                    <br />
                                                    <asp:Panel runat="server" Visible="false" ID="panelsdshhffg">
                                                        You can give similar access rights to this employee for your other businesses also.
                                                        <br />
                                                        Would you like to give similar access rights for other businesses ?
                                                    </asp:Panel>
                                                </label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40%">
                                            <label>
                                                <asp:Label ID="Label29" Text="Select business to give access rights to this employee"
                                                    runat="server"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkaccessright" runat="server" Visible="false" AutoPostBack="True"
                                                OnCheckedChanged="chkaccessright_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="pnlaccess" Visible="false" runat="server" Width="350px" ScrollBars="Vertical"
                                                Height="200px">
                                                <asp:GridView ID="gridAccess" runat="server" PageSize="5" DataKeyNames="WareHouseId"
                                                    Width="100%" AutoGenerateColumns="False" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                    AlternatingRowStyle-CssClass="alt" OnRowDataBound="gridAccess_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblid1" runat="server" Text='<%#Eval("WareHouseId")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblwaid" runat="server" Text='<%#Eval("Id")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Business" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBusiness" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                                <asp:Label ID="lblWh" runat="server" Text='<%#Eval("WareHouseId")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Access Allowed" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="30%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkAess" runat="server" Checked='<%#Eval("AccessAllowed")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        No Record Found. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <asp:Panel ID="panelaccuracy" runat="server" Visible="false">
                                        <tr>
                                            <td style="width: 40%">
                                                <label>
                                                    <asp:Label ID="Label162" Text="Upload documents related to this entry" runat="server"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk111" runat="server" />
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </table>
                            </fieldset>
                        </asp:View>
                        <asp:View ID="View4" runat="server">
                            <div style="float: right">
                                <asp:ImageButton ImageUrl="~/images/previous1.jpg" ID="ImageButton34" runat="server"
                                    ValidationGroup="1" onclick="ImageButton34_Click" />
                                <asp:ImageButton ImageUrl="~/images/next1.jpg" ID="ImageButton17" runat="server"
                                    OnClick="ImageButton17_Click" ValidationGroup="1" />
                            </div>
                            <div style="margin: 0px 0px 0px 0px; clear: both; padding-bottom: 0px;">
                            </div>
                            <asp:Panel ID="pnlempbarcode" Width="100%" Visible="true" runat="server">
                                <fieldset>
                                    <legend>Employee Attendance Information </legend>
                                    <table width="100%">
                                        <tr valign="top">
                                            <td width="30%">
                                                <label>
                                                    <asp:Label ID="Label66" runat="server" Text="Barcode"></asp:Label>
                                                    <asp:Label ID="Label67" runat="server" Text="*" CssClass="labelstar" Visible="false"></asp:Label>
                                                    <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtbarcode"
                                                            ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="Invalid Character"
                                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtbarcode"
                                                        ValidationGroup="1">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td width="20%">
                                                <label>
                                                    <asp:TextBox ID="txtbarcode" runat="server" MaxLength="50" onKeydown="return mask(event)"
                                                        Width="145px" onkeyup="return check(this,/[\\/!@#$%^'_.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span17',50)"></asp:TextBox></label>
                                            </td>
                                            <td width="50%">
                                                <label>
                                                    <asp:Label ID="Label68" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span
                                                        id="Span17" class="labelcount">50</span>
                                                    <asp:Label ID="Label69" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label></label>
                                            </td>
                                        </tr>
                                        <%-- <tr>
                                                        <td colspan="3">
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                                                                ErrorMessage="Enter Valid Barcode, E.g: 1 char and 1 digit with minimum 15 Characters are must"
                                                                ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{15,20})$" ControlToValidate="txtbarcode"
                                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>--%>
                                        <tr valign="top">
                                            <td>
                                                <label class="first">
                                                    <asp:Label ID="Label70" runat="server" Text="Security Code"></asp:Label>
                                                    <asp:Label ID="Label13271" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldV123alidator12" runat="server" ControlToValidate="txtsecuritycode"
                                                        ErrorMessage="*" ValidationGroup="1">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpre213ssionValidator21" runat="server"
                                                        ErrorMessage="Invalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([@#+_a-zA-Z0-9\s]*)"
                                                        ControlToValidate="txtsecuritycode" ValidationGroup="1">
                                                    </asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="txtsecuritycode" runat="server" MaxLength="16" onKeydown="return mask(event)"
                                                        Width="145px" onkeyup="return check(this,/[\\/!$%^'.&*()>:;={}[]|\/]/g,/^[\@#+_a-zA-Z0-9\s]+$/,'Span1e',16)"></asp:TextBox></label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Lab234el20" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                    <span id="Span1e" class="labelcount">16</span>
                                                    <asp:Label ID="Labe234l15" CssClass="labelcount" runat="server" Text="(A-Z 0-9 + _ @ #)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                <label class="first">
                                                    <asp:Label ID="Label72" runat="server" Text="Biometric Scanner ID"></asp:Label>
                                                    <%-- <asp:Label ID="Label73" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtbiometricid"
                                                                    ErrorMessage="*" ValidationGroup="1">
                                                                </asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server"
                                                        ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                        ControlToValidate="txtbiometricid" ValidationGroup="1">
                                                    </asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="txtbiometricid" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                                        Width="145px" onkeyup="return check(this,/[\\/!@#$%^'_.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span18',20)">
                                                    </asp:TextBox>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label74" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span
                                                        id="Span18" class="labelcount">20</span>
                                                    <asp:Label ID="Label75" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label></label>
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                <label class="first">
                                                    <asp:Label ID="Label76" runat="server" Text="Bluetooth ID"></asp:Label>
                                                    <%-- <asp:Label ID="Label77" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtbiometricid"
                                                                    ErrorMessage="*" ValidationGroup="1">
                                                                </asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server"
                                                        ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                        ControlToValidate="txtbluetoothid" ValidationGroup="1">
                                                    </asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="txtbluetoothid" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                                        Width="145px" onkeyup="return check(this,/[\\/!@#$%^'_.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span19',20)">
                                                    </asp:TextBox>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label78" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span
                                                        id="Span19" class="labelcount">20</span>
                                                    <asp:Label ID="Label79" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label></label>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                        </asp:View>
                        <asp:View ID="View5" runat="server">
                            <div style="float: right">
                                <asp:ImageButton ImageUrl="~/images/previous1.jpg" ID="ImageButton35" runat="server"
                                    ValidationGroup="1" onclick="ImageButton35_Click" />
                                <asp:ImageButton ImageUrl="~/images/next1.jpg" ID="ImageButton21" runat="server"
                                    ValidationGroup="1" OnClick="ImageButton21_Click" />
                            </div>
                            <div style="margin: 0px 0px 0px 0px; clear: both; padding-bottom: 0px;">
                            </div>
                            <table width="100%">
                                <asp:Panel ID="paneldfrrww" runat="server" Visible="false">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label33" Text="Add Payroll Information" runat="server"></asp:Label><asp:Label
                                                    ID="Label120" runat="server" Text="*" CssClass="labelstar"></asp:Label></label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkemppayroll" runat="server" Checked="true" AutoPostBack="True"
                                                OnCheckedChanged="chkemppayroll_CheckedChanged" Visible="false" />
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Pnlemppayroll" Visible="false" runat="server" Width="100%">
                                            <fieldset>
                                                <legend>Employee Remuneration Information</legend>
                                                <label>
                                                    How this employee would be paid ?
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                    <asp:ListItem Value="1" Text="Employee will be paid same as other employees of same designation is paid"
                                                        Enabled="false">
                                                    
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="0" Selected="True" Text="Employee will be paid as per own separate remuneration terms">
                                                   
                                                    </asp:ListItem>
                                                </asp:RadioButtonList>
                                                <div style="clear: both;">
                                                </div>
                                                <label>
                                                    There is not any rule set fixing specific remuneration terms for designation -
                                                    <asp:Label ID="Label180" Text="" runat="server" ForeColor="Black"></asp:Label>
                                                </label>
                                                <asp:CheckBox ID="CheckBox5" runat="server" Text="Would you like to set one now ?"
                                                    TextAlign="Left" AutoPostBack="True" OnCheckedChanged="CheckBox5_CheckedChanged" />
                                                <div style="clear: both;">
                                                </div>
                                                <asp:Panel ID="panelgetready" runat="server" Visible="false">
                                                    <asp:Button ID="Button8" runat="server" Text="Add New Designation Payroll Setup"
                                                        CssClass="btnSubmit" OnClick="Button8_Click" />
                                                    <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Refresh" Height="20px"
                                                        ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                                        ImageAlign="Bottom" OnClick="ImageButton1_Click" />
                                                </asp:Panel>
                                                <div style="clear: both;">
                                                </div>
                                                <asp:Panel ID="Panel2" runat="server" Width="100%" Visible="false">
                                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        DataKeyNames="Id" Width="100%" EmptyDataText="No Record Found.">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left"
                                                                SortExpression="Name" ItemStyle-Width="12%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbusi" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Left" SortExpression="DesignationName"
                                                                ItemStyle-Width="12%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblempname123" runat="server" Text='<%# Eval("DesignationName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remuneration Name" HeaderStyle-HorizontalAlign="Left"
                                                                SortExpression="RemunerationName" ItemStyle-Width="12%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblremuneration123" runat="server" Text='<%# Eval("RemunerationName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField SortExpression="Amountpay" HeaderText="Remuneration Amount Per Period"
                                                                ItemStyle-Width="12%" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("Amountpay") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Percentage of other Remuneration" SortExpression="Percentofsale"
                                                                ItemStyle-Width="12%" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpercent" runat="server" Text='<%# Eval("Percentofsale") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Percentage of Sales" SortExpression="PercentageOfSalesId"
                                                                ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblispercentofsales" runat="server" Text='<%# Eval("PercentageOfSalesemp") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Start Date" SortExpression="EffectiveStartDate" ItemStyle-Width="7%"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstartdate" runat="server" Text='<%# Eval("EffectiveStartDate","{0:MM/dd/yyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date" SortExpression="EffectiveEndDate" ItemStyle-Width="7%"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblenddate" runat="server" Text='<%# Eval("EffectiveEndDate","{0:MM/dd/yyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <div style="clear: both;">
                                                </div>
                                                <asp:Panel ID="panlam" runat="server">
                                                    <asp:RadioButtonList ID="RadioButtonList3" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                        OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged">
                                                        <asp:ListItem Value="0" Selected="True" Text="Quick Setup"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Detailed Setup" Enabled="false"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </asp:Panel>
                                                <div style="clear: both;">
                                                </div>
                                                <label>
                                                    you can make detailed setup after you have created this employee. (by editing this
                                                    employee)
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <asp:Panel ID="panel2nd" runat="server">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label73" runat="server" Text="Remuneration Name"></asp:Label>
                                                                    <asp:DropDownList ID="ddlremuneration" runat="server">
                                                                    </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                            <td valign="bottom">
                                                                <label>
                                                                    <asp:ImageButton ID="ImageButton6" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                        ToolTip="AddNew" Width="20px" ImageAlign="Bottom" OnClick="ImageButton6_Click" />
                                                                    <asp:ImageButton ID="ImageButton7" runat="server" AlternateText="Refresh" Height="20px"
                                                                        ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                                                        ImageAlign="Bottom" OnClick="ImageButton7_Click" />
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label77" runat="server" Text="Amount"></asp:Label>
                                                                    <asp:Label ID="Label156" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldVali432edator13" runat="server" ControlToValidate="txtamount"
                                                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtamount" runat="server" Width="100px" MaxLength="15"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTertertextBoxExtender9" runat="server" Enabled="True"
                                                                        TargetControlID="txtamount" ValidChars="0123456789">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label155" runat="server" Text="Payable Per"></asp:Label>
                                                                    <asp:DropDownList ID="ddlpaybleper" Width="120px" runat="server">
                                                                    </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <div style="clear: both;">
                                                </div>
                                                <asp:Panel ID="panel3rd" runat="server">
                                                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        DataKeyNames="Id" Width="100%" EmptyDataText="No Record Found." OnRowCommand="GridView4_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left"
                                                                SortExpression="Name" ItemStyle-Width="12%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbusi" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName" ItemStyle-Width="18%"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblempname123" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remuneration Name" HeaderStyle-HorizontalAlign="Left"
                                                                SortExpression="RemunerationName" ItemStyle-Width="12%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblremuneration123" runat="server" Text='<%# Eval("RemunerationName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField SortExpression="Amountpay" HeaderText="Remuneration Amount Per Period"
                                                                ItemStyle-Width="12%" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("Amountpay") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Percentage of other Remuneration" SortExpression="Percentofsale"
                                                                ItemStyle-Width="12%" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpercent" runat="server" Text='<%# Eval("Percentofsale") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Percentage of Sales" SortExpression="PercentageOfSalesId"
                                                                ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblispercentofsales" runat="server" Text='<%# Eval("PercentageOfSalesemp") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Effective <br/> Start Date" SortExpression="EffectiveStartDate"
                                                                ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstartdate" runat="server" Text='<%# Eval("EffectiveStartDate","{0:MM/dd/yyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Effective <br/> End Date" SortExpression="EffectiveEndDate"
                                                                ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblenddate" runat="server" Text='<%# Eval("EffectiveEndDate","{0:MM/dd/yyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Views" ForeColor="Black">Make changes</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <div style="clear: both;">
                                                </div>
                                                <fieldset>
                                                    <legend>
                                                        <asp:Label ID="Label179" runat="server" Text="Remuneration Payment Information" ForeColor="Black"
                                                            Font-Size="16px" Font-Bold="false"></asp:Label>
                                                    </legend>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" Text="Please select when employee would be paid ?"
                                                        AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged" TextAlign="Left" />
                                                    <asp:Panel ID="Panel9" runat="server" Visible="false">
                                                        <label>
                                                            <asp:Label ID="Label45" runat="server" Text="    Payment Cycle"></asp:Label>
                                                            <asp:Label ID="Label46" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlPaymentCycle"
                                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </label>
                                                        <label>
                                                            <asp:DropDownList ID="ddlPaymentCycle" runat="server">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </asp:Panel>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <asp:CheckBox ID="CheckBox3" runat="server" Text="Please select the payment method for the employee"
                                                        AutoPostBack="True" OnCheckedChanged="CheckBox3_CheckedChanged" TextAlign="Left" />
                                                    <asp:Panel ID="Panel8" runat="server" Visible="false">
                                                        <label>
                                                            <asp:Label ID="Label43" runat="server" Text="    Payment Method"></asp:Label>
                                                            <asp:Label ID="Label44" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPaymentMethod"
                                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </label>
                                                        <label>
                                                            <asp:DropDownList ID="ddlPaymentMethod" runat="server" OnSelectedIndexChanged="ddlPaymentMethod_SelectedIndexChanged"
                                                                AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </asp:Panel>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <asp:CheckBox ID="CheckBox4" runat="server" Text="Please write the exact name the payment should be made to"
                                                        AutoPostBack="True" OnCheckedChanged="CheckBox4_CheckedChanged" TextAlign="Left" />
                                                    <div style="clear: both;">
                                                    </div>
                                                    <asp:Panel ID="panelmainpanel" runat="server" Visible="false">
                                                        <asp:Panel ID="pnlreceivepayment" runat="server" Visible="false">
                                                            <label>
                                                                <asp:Label ID="lblPaymentReceivedNameOf" runat="server" Text="Payment Received Name Of"></asp:Label><asp:RegularExpressionValidator
                                                                    ID="RegularExpressionValidator14" runat="server" ErrorMessage="Invalid Character"
                                                                    SetFocusOnError="True" ValidationExpression="^([a-zA-Z_0-9\s]*)" ControlToValidate="txtPaymentReceivedNameOf"
                                                                    ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox ID="txtPaymentReceivedNameOf"
                                                                        runat="server" MaxLength="50" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!.@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z_A-Z0-9\s]+$/,'Span12',50)"></asp:TextBox><asp:Label
                                                                            ID="Label53" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span id="Span12"
                                                                                class="labelcount">50</span>
                                                                <asp:Label ID="Label54" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label></label></asp:Panel>
                                                        <div style="clear: both;">
                                                        </div>
                                                        <asp:Panel ID="pnlpaypalid" runat="server" Visible="false">
                                                            <label>
                                                                <asp:Label ID="lblPaypalId" runat="server" Text="Paypal Id"></asp:Label><asp:RegularExpressionValidator
                                                                    ID="RegularExpressionValidator15" runat="server" ErrorMessage="Invalid Character"
                                                                    SetFocusOnError="True" ValidationExpression="^([0-9\s]*)" ControlToValidate="txtPaypalId"
                                                                    ValidationGroup="1"></asp:RegularExpressionValidator><cc1:FilteredTextBoxExtender
                                                                        ID="txtEndzip_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtPaypalId"
                                                                        ValidChars="0123456789">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                <asp:TextBox ID="txtPaypalId" runat="server" MaxLength="15" onkeyup="return mak('Span13',15,this)"></asp:TextBox><asp:Label
                                                                    ID="Label55" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span id="Span13"
                                                                        class="labelcount">15</span>
                                                                <asp:Label ID="Label56" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label></label></asp:Panel>
                                                        <div style="clear: both;">
                                                        </div>
                                                        <asp:Panel ID="pnlpaymentemail" runat="server" Visible="false">
                                                            <label>
                                                                <asp:Label ID="lblPaymentEmailId" runat="server" Text="Payment Email Id"></asp:Label><asp:RegularExpressionValidator
                                                                    ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtPaymentEmailId"
                                                                    ErrorMessage="Invalid Email ID" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                    ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox ID="txtPaymentEmailId"
                                                                        runat="server" MaxLength="40"></asp:TextBox></label></asp:Panel>
                                                        <div style="clear: both;">
                                                        </div>
                                                        <asp:Panel ID="ddpnl" Width="100%" Visible="false" runat="server">
                                                            <fieldset>
                                                                <legend>
                                                                    <asp:Label ID="Label47" runat="server" Text="Direct Deposited Bank Details"></asp:Label></legend>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="lblDirectDepositBankName" runat="server" Text="Bank Name"></asp:Label><br />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                                                    ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                                                    ControlToValidate="txtDirectDepositBankName" ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox
                                                                                        ID="txtDirectDepositBankName" runat="server" MaxLength="50" onKeydown="return mask(event)"
                                                                                        onkeyup="return check(this,/[\\/!.@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z_A-Z0-9\s]+$/,'Span8',50)"></asp:TextBox><asp:Label
                                                                                            ID="Label48" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span id="Span8"
                                                                                                class="labelcount">50</span>
                                                                                <asp:Label ID="Label49" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label></label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="lblDirectDepositBankBranchName" runat="server" Text="Branch Name"></asp:Label><br />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                                                    ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-zA-Z_0-9\s]*)"
                                                                                    ControlToValidate="txtDirectDepositBankBranchName" ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox
                                                                                        ID="txtDirectDepositBankBranchName" runat="server" MaxLength="50" onKeydown="return mask(event)"
                                                                                        onkeyup="return check(this,/[\\/!.@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z_A-Z0-9\s]+$/,'Span10',50)"></asp:TextBox><asp:Label
                                                                                            ID="Label52" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span id="Span10"
                                                                                                class="labelcount">50</span>
                                                                                <asp:Label ID="Label57" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label></label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="lblDirectDepositBranchAddress" runat="server" Text="Branch Address"></asp:Label><br />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                                                    ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                                                    ControlToValidate="txtDirectDepositBranchAddress" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                                <asp:TextBox ID="txtDirectDepositBranchAddress" runat="server" MaxLength="50" onKeydown="return mask(event)"
                                                                                    onkeyup="return check(this,/[\\/!._@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span9',50)"></asp:TextBox>
                                                                                <asp:Label ID="Label50" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span
                                                                                    id="Span9" class="labelcount">50</span>
                                                                                <asp:Label ID="Label51" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label></label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label103" runat="server" Text="IFC Number"></asp:Label><br />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server"
                                                                                    ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                                                    ControlToValidate="txtDirectDepositifscnumber" ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox
                                                                                        ID="txtDirectDepositifscnumber" MaxLength="50" runat="server" onKeydown="return mask(event)"
                                                                                        onkeyup="return check(this,/[\\/!._@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span23',50)"></asp:TextBox><asp:Label
                                                                                            ID="Label107" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span
                                                                                                id="Span23" class="labelcount">50</span>
                                                                                <asp:Label ID="Label110" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label></label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label109" runat="server" Text="Branch Country"></asp:Label><asp:DropDownList
                                                                                    ID="ddlDirectDepositBankBranchcountry" runat="server" AutoPostBack="True" ValidationGroup="1"
                                                                                    Width="151px" OnSelectedIndexChanged="ddlDirectDepositBankBranchcountry_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label105" runat="server" Text="Branch State"></asp:Label><asp:DropDownList
                                                                                    ID="ddlDirectDepositBankBranchstate" runat="server" AutoPostBack="True" ValidationGroup="1"
                                                                                    Width="151px" OnSelectedIndexChanged="ddlDirectDepositBankBranchstate_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label101" runat="server" Text="Branch City"></asp:Label><asp:DropDownList
                                                                                    ID="ddlDirectDepositBankBranchcity" runat="server" ValidationGroup="1" Width="151px">
                                                                                </asp:DropDownList>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label112" runat="server" Text="ZIP Code"></asp:Label><asp:RegularExpressionValidator
                                                                                    ID="RegularExpressionValidator26" runat="server" ErrorMessage="Invalid Character"
                                                                                    SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="ddlDirectDepositBankBranchzipcode"
                                                                                    ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox ID="ddlDirectDepositBankBranchzipcode"
                                                                                        runat="server" ValidationGroup="1" Width="145px" MaxLength="10" onKeydown="return mask(event)"
                                                                                        onkeyup="return check(this,/[\\/!@#.$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9]+$/,'Span22',10)"></asp:TextBox><asp:Label
                                                                                            ID="Label113" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span
                                                                                                id="Span22" class="labelcount">10</span>
                                                                                <asp:Label ID="Label114" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label></label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="lblDirectDepositTransitNumber" runat="server" Text="Bank Transit Number"></asp:Label><br />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                                                                                    ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                                                    ControlToValidate="txtDirectDepositTransitNumber" ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox
                                                                                        ID="txtDirectDepositTransitNumber" runat="server" MaxLength="40" onKeydown="return mask(event)"
                                                                                        onkeyup="return check(this,/[\\/!._@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span14',40)"></asp:TextBox><asp:Label
                                                                                            ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span id="Span14"
                                                                                                class="labelcount">40</span>
                                                                                <asp:Label ID="Label61" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label></label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="lblDirectDepositBankAccountNumber" runat="server" Text="Bank Account Number"></asp:Label><br />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server"
                                                                                    ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                                                    ControlToValidate="txtDirectDepositBankAccountNumber" ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox
                                                                                        ID="txtDirectDepositBankAccountNumber" MaxLength="50" runat="server" onKeydown="return mask(event)"
                                                                                        onkeyup="return check(this,/[\\/!._@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span16',50)"></asp:TextBox><asp:Label
                                                                                            ID="Label64" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span id="Span16"
                                                                                                class="labelcount">50</span>
                                                                                <asp:Label ID="Label65" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label></label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="lblDirectDepositBankAccountType" runat="server" Text="Bank Account Type"></asp:Label><br />
                                                                                <br />
                                                                                <asp:DropDownList ID="ddlDirectDepositBankAccountType" runat="server">
                                                                                    <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="1" Text="Bussiness"></asp:ListItem>
                                                                                    <asp:ListItem Value="2" Text="Checking"></asp:ListItem>
                                                                                    <asp:ListItem Value="3" Text="Current"></asp:ListItem>
                                                                                    <asp:ListItem Value="4" Text="Payroll"></asp:ListItem>
                                                                                    <asp:ListItem Value="5" Text="Savings"></asp:ListItem>
                                                                                    <asp:ListItem Value="6" Text="Line of Credit"></asp:ListItem>
                                                                                    <asp:ListItem Value="7" Text="Cash Credit"></asp:ListItem>
                                                                                    <asp:ListItem Value="8" Text="Overdraft"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <br />
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="lblDirectDepositAccountHolderName" runat="server" Text="Transit Bank Account Name"></asp:Label><br />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server"
                                                                                    ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-z_A-Z0-9\s]*)"
                                                                                    ControlToValidate="txtDirectDepositAccountHolderName" ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox
                                                                                        ID="txtDirectDepositAccountHolderName" runat="server" MaxLength="50" onKeydown="return mask(event)"
                                                                                        onkeyup="return check(this,/[\\/!.@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'Span15',50)"></asp:TextBox><asp:Label
                                                                                            ID="Label62" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span id="Span15"
                                                                                                class="labelcount">50</span>
                                                                                <asp:Label ID="Label63" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label></label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label116" runat="server" Text="SWIFT Number"></asp:Label><br />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator28" runat="server"
                                                                                    ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                                                    ControlToValidate="txtDirectDepositswiftnumber" ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox
                                                                                        ID="txtDirectDepositswiftnumber" MaxLength="50" runat="server" onKeydown="return mask(event)"
                                                                                        onkeyup="return check(this,/[\\/!._@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span24',50)"></asp:TextBox><asp:Label
                                                                                            ID="Label118" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span
                                                                                                id="Span24" class="labelcount">50</span>
                                                                                <asp:Label ID="Label119" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label></label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label58" runat="server" Text="Employee Email registered with the bank"></asp:Label><br />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                                                                                    ControlToValidate="txtdirectdipositemployeeemail" ErrorMessage="Invalid Email ID"
                                                                                    Font-Size="14px" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                                    ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox ID="txtdirectdipositemployeeemail"
                                                                                        runat="server" MaxLength="40" Width="200px"></asp:TextBox><br />
                                                                            </label>
                                                                        </td>
                                                                        <td colspan="2">
                                                                            <label>
                                                                                <asp:Label ID="Label182" runat="server" Text="Employee Address registered with the bank"></asp:Label>
                                                                                <br />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                                                                                    ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                                                    ControlToValidate="TextBox2" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                                <asp:TextBox ID="TextBox2" runat="server" MaxLength="50" onKeydown="return mask(event)"
                                                                                    Width="300px" onkeyup="return check(this,/[\\/!._@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span36',50)"></asp:TextBox>
                                                                                <asp:Label ID="Label183" CssClass="labelcount" runat="server" Text="Max "></asp:Label><span
                                                                                    id="Span36" class="labelcount">50</span>
                                                                                <asp:Label ID="Label184" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label></label>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                        </asp:Panel>
                                                    </asp:Panel>
                                                </fieldset>
                                            </fieldset>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <asp:Panel ID="panelondemand" runat="server" Visible="false">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label168" Text="Add Overhead Information" runat="server" Visible="false"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkoverhead" runat="server" AutoPostBack="true" Checked="true"
                                                OnCheckedChanged="chkoverhead_CheckedChanged" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="pnloverhead" Width="100%" runat="server" Visible="false">
                                                <fieldset>
                                                    <legend>Overhead Information</legend>
                                                    <table width="100%">
                                                        <tr valign="top">
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label169" runat="server" Text="Overhead Amount"></asp:Label>
                                                                    <asp:Label ID="Label170" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="TextBox17"
                                                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="TextBox17" runat="server" MaxLength="15" Width="130px"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                                        TargetControlID="TextBox17" ValidChars="0123456789">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </label>
                                                                <label>
                                                                    <asp:Label ID="Label171" runat="server" Text="Payable Per"></asp:Label>
                                                                    <asp:DropDownList ID="DropDownList1" Width="120px" runat="server">
                                                                    </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                        </asp:View>
                        <asp:View ID="View6" runat="server">
                            <div style="float: right">
                                <asp:ImageButton ImageUrl="~/images/previous1.jpg" ID="ImageButton36" runat="server"
                                    ValidationGroup="1" onclick="ImageButton36_Click" />
                                <asp:ImageButton ImageUrl="~/images/next1.jpg" ID="ImageButton20" runat="server"
                                    ValidationGroup="1" OnClick="ImageButton20_Click" />
                            </div>
                            <div style="margin: 0px 0px 0px 0px; clear: both; padding-bottom: 0px;">
                            </div>
                            <fieldset>
                                <legend>Employment and Educational Background </legend>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 50%" colspan="2">
                                            <label>
                                                <asp:Label ID="Label160" runat="server" Text="Previous Job Title"></asp:Label>
                                                <asp:DropDownList ID="ddljobposition" runat="server" Width="400px">
                                                </asp:DropDownList>
                                                <asp:Panel ID="pnljobposition" runat="server" Visible="false">
                                                    <label>
                                                        <asp:TextBox ID="txtjobposition" runat="server" MaxLength="60" Width="400px"></asp:TextBox>
                                                    </label>
                                                </asp:Panel>
                                                <asp:CheckBox ID="chkjobposition" runat="server" Text="Other" TextAlign="Left" AutoPostBack="True"
                                                    OnCheckedChanged="chkjobposition_CheckedChanged" />
                                            </label>
                                        </td>
                                        <td style="width: 50%" colspan="2" valign="top">
                                            <label>
                                                <asp:Label ID="Label159" runat="server" Text="Years in Position"></asp:Label>
                                                <asp:TextBox ID="txtyearexpr" runat="server"></asp:TextBox>
                                                <br />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%" colspan="2">
                                            <label>
                                                <asp:Label ID="Label157" runat="server" Text="Highest Education Received"></asp:Label>
                                                <asp:DropDownList ID="ddleduquali" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddleduquali_SelectedIndexChanged"
                                                    Width="400px">
                                                </asp:DropDownList>
                                                <asp:Panel ID="pnleduqauli" runat="server" Visible="false">
                                                    <label>
                                                        <asp:TextBox ID="txteduquali" runat="server" MaxLength="60" Width="400px"></asp:TextBox>
                                                    </label>
                                                </asp:Panel>
                                                <asp:CheckBox ID="chkeduquali" runat="server" Text="Other" TextAlign="Left" AutoPostBack="True"
                                                    OnCheckedChanged="chkeduquali_CheckedChanged" />
                                            </label>
                                        </td>
                                        <td style="width: 50%" colspan="2">
                                            <label>
                                                <asp:Label ID="Label158" runat="server" Text="Subject of Specialization"></asp:Label>
                                                <asp:DropDownList ID="ddlspecialsub" runat="server" Width="400px">
                                                </asp:DropDownList>
                                                <asp:Panel ID="pnlspecialsub" runat="server" Visible="false">
                                                    <label>
                                                        <asp:TextBox ID="txtspecialsub" runat="server" MaxLength="60" Width="400px"></asp:TextBox>
                                                    </label>
                                                </asp:Panel>
                                                <asp:CheckBox ID="chkspecialsub" runat="server" Text="Other" TextAlign="Left" AutoPostBack="True"
                                                    OnCheckedChanged="chkspecialsub_CheckedChanged" />
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:View>
                        <asp:View ID="View7" runat="server">
                            <div style="float: right">
                                <asp:ImageButton ImageUrl="~/images/previous1.jpg" ID="ImageButton37" runat="server"
                                    ValidationGroup="1" onclick="ImageButton37_Click" />
                                <asp:ImageButton ImageUrl="~/images/next1.jpg" ID="ImageButton31" runat="server"
                                    ValidationGroup="1" OnClick="ImageButton31_Click" />
                            </div>
                            <div style="margin: 0px 0px 0px 0px; clear: both; padding-bottom: 0px;">
                            </div>
                            <asp:Panel ID="panelemergency" runat="server" Width="100%" Visible="true">
                                <fieldset>
                                    <legend>Emergency Contact Information</legend>
                                    <label>
                                        <asp:Label ID="Label121" runat="server" Text="First Emergency Contact Name"></asp:Label>
                                        <asp:TextBox ID="TextBox5" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span11',30)"
                                            Width="230px"></asp:TextBox>
                                        <asp:Label ID="Label133" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span11" class="labelcount">30</span><asp:Label ID="Label134" CssClass="labelcount"
                                            runat="server" Text="(A-Z 0-9)"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label122" runat="server" Text="Second Emergency Contact Name"></asp:Label>
                                        <asp:TextBox ID="TextBox6" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span25',30)"
                                            Width="230px"></asp:TextBox>
                                        <asp:Label ID="Label135" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span25" class="labelcount">30</span><asp:Label ID="Label136" CssClass="labelcount"
                                            runat="server" Text="(A-Z 0-9)"></asp:Label>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label123" runat="server" Text="Relationship to Employee"></asp:Label>
                                        <asp:TextBox ID="TextBox7" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                            Width="230px" onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span26',30)"></asp:TextBox>
                                        <asp:Label ID="Label137" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span26" class="labelcount">30</span><asp:Label ID="Label138" CssClass="labelcount"
                                            runat="server" Text="(A-Z 0-9)"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label124" runat="server" Text="Relationship to Employee"></asp:Label>
                                        <asp:TextBox ID="TextBox8" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                            Width="230px" onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span27',30)"></asp:TextBox>
                                        <asp:Label ID="Label139" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span27" class="labelcount">30</span><asp:Label ID="Label140" CssClass="labelcount"
                                            runat="server" Text="(A-Z 0-9)"></asp:Label>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label125" runat="server" Text="Phone Number (Home)"></asp:Label>
                                        <asp:TextBox ID="TextBox9" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                            Width="230px" onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span28',15)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                            TargetControlID="TextBox9" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="Label141" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span28" class="labelcount">15</span><asp:Label ID="Label142" CssClass="labelcount"
                                            runat="server" Text="(0-9)"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label126" runat="server" Text="Phone Number (Home)"></asp:Label>
                                        <asp:TextBox ID="TextBox10" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                            Width="230px" onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span29',15)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                            TargetControlID="TextBox10" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="Label143" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span29" class="labelcount">15</span><asp:Label ID="Label144" CssClass="labelcount"
                                            runat="server" Text="(0-9)"></asp:Label>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label127" runat="server" Text="Phone Number (Cell)"></asp:Label>
                                        <asp:TextBox ID="TextBox11" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                            Width="230px" onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span30',15)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                            TargetControlID="TextBox11" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="Label145" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span30" class="labelcount">15</span><asp:Label ID="Label146" CssClass="labelcount"
                                            runat="server" Text="(0-9)"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label128" runat="server" Text="Phone Number (Cell)"></asp:Label>
                                        <asp:TextBox ID="TextBox12" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                            Width="230px" onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span31',15)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                            TargetControlID="TextBox12" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="Label147" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span31" class="labelcount">15</span><asp:Label ID="Label148" CssClass="labelcount"
                                            runat="server" Text="(0-9)"></asp:Label>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label129" runat="server" Text="Phone Number (Work)"></asp:Label>
                                        <asp:TextBox ID="TextBox13" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                            Width="230px" onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span32',15)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                            TargetControlID="TextBox13" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="Label149" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span32" class="labelcount">15</span><asp:Label ID="Label150" CssClass="labelcount"
                                            runat="server" Text="(0-9)"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label130" runat="server" Text="Phone Number (Work)"></asp:Label>
                                        <asp:TextBox ID="TextBox14" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                            Width="230px" onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span33',15)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                            TargetControlID="TextBox14" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="Label151" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span33" class="labelcount">15</span><asp:Label ID="Label152" CssClass="labelcount"
                                            runat="server" Text="(0-9)"></asp:Label>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label131" runat="server" Text="Email"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator29" runat="server"
                                            ControlToValidate="TextBox15" ErrorMessage="Invalid Email ID" Font-Size="14px"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="TextBox15" runat="server" MaxLength="50" Width="230px"></asp:TextBox>
                                        <asp:Label ID="Label153" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span34" class="labelcount">50</span></label>
                                    <label>
                                        <asp:Label ID="Label132" runat="server" Text="Email"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server"
                                            ControlToValidate="TextBox16" ErrorMessage="Invalid Email ID" Font-Size="14px"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="TextBox16" runat="server" MaxLength="50" Width="230px"></asp:TextBox>
                                        <asp:Label ID="Label154" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span35" class="labelcount">50</span></label>
                                    <div style="clear: both;">
                                    </div>
                                </fieldset>
                            </asp:Panel>
                        </asp:View>
                        <asp:View ID="View8" runat="server">
                            <div style="float: right">
                                <asp:ImageButton ImageUrl="~/images/previous1.jpg" ID="ImageButton38" runat="server"
                                    ValidationGroup="1" onclick="ImageButton38_Click" />
                            </div>
                            <div style="clear: both;">
                            </div>
                            <fieldset>
                                <legend>Attach Documents</legend>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label185" runat="server" Text="Document Title"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtdoctitle"
                                                    ErrorMessage="*" ValidationGroup="500" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                    ControlToValidate="txtdoctitle" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtdoctitle" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'Span37',50)"
                                                    runat="server" ValidationGroup="1" MaxLength="50" TabIndex="2"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label186" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                <span id="Span37" class="labelcount">50</span>
                                                <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label187" runat="server" Text="Document Date"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="TxtDocDate"
                                                    ErrorMessage="*" ValidationGroup="500" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="TxtDocDate" runat="server" Width="70px" TabIndex="4"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="TxtDocDate">
                                                </cc1:CalendarExtender>
                                            </label>
                                        </td>
                                    </tr>
                                    <asp:Panel Visible="false" runat="server" ID="paeledasd">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label181" runat="server" Text="Document Type"></asp:Label>
                                                    <asp:Label ID="Labelxc" runat="server" Text="*"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RiredFievbalidator2" runat="server" ControlToValidate="ddldt"
                                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                </label>
                                                <label>
                                                    <asp:DropDownList ID="ddldt" runat="server" ValidationGroup="1" AutoPostBack="false"
                                                        Enabled="False">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                Upload Document
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:FileUpload ID="FileUpload2" runat="server" />
                                            </label>
                                            <label>
                                                <asp:Button ID="Button9" runat="server" Text="Upload" CssClass="btnSubmit" OnClick="Button9_Click"
                                                    ValidationGroup="500" />
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel runat="server" ID="panelforresume">
                                                <asp:GridView ID="GridView7" runat="server" EmptyDataText="No Record Found." CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="Both"
                                                    AutoGenerateColumns="False" DataKeyNames="documenttype" Width="100%" PageSize="20"
                                                    OnRowCommand="GridView7_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            DataField="Businessname"></asp:BoundField>
                                                        <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpid" runat="server" Text='<%#Eval("PartyId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            HeaderText="Cabinet-Drawer-Folder">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcabinetdrw" runat="server" Text='<%#Eval("DocType") %>'></asp:Label>
                                                                <asp:Label ID="Label2700" runat="server" Text='<%#Eval("documenttype") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            HeaderText="Document Title">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldocumenttitle" runat="server" Text='<%#Eval("DocumentTitle") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Document Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            DataField="documentname"></asp:BoundField>
                                                        <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundField>
                                                        <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldocdate" runat="server" Text='<%#Eval("docdate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldocrefno" runat="server" Text='<%#Eval("docrefno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldocamt" runat="server" Text='<%#Eval("docamt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Type" HeaderStyle-HorizontalAlign="Left"
                                                            Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldt" runat="server" Text='<%#Bind("Docty") %>'></asp:Label>
                                                                <asp:Label ID="lbldoctid" runat="server" Text='<%#Bind("DoctyId") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Party Doc Ref.No." HeaderStyle-HorizontalAlign="Left"
                                                            Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblprn" runat="server" Text='<%#Bind("PRN") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:ButtonField ButtonType="Image" HeaderImageUrl="~/Account/images/delete.gif"
                                                            ImageUrl="~/Account/images/delete.gif" HeaderText="Delete" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%" CommandName="del"></asp:ButtonField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <div>
                            <asp:Panel ID="Pnl_login" Visible="true" runat="server" Width="100%">
                            <fieldset>
                                <legend>Login Information</legend>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label3022" Text="How would you like to set login information for this employee ?"
                                                    runat="server"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%--<asp:RadioButton ID="RadioButton1" runat="server" Text="Send by email an auto generated temporary User ID and password" />
                                        <asp:RadioButton ID="RadioButton2" runat="server" 
                                            Text="Set employee User ID and password" 
                                            oncheckedchanged="RadioButton2_CheckedChanged" AutoPostBack="True" />--%>
                                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="1" Text="Send by email the auto generated temporary user ID and password"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Set employee User ID and password"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlemployeedate" Visible="false" runat="server" Width="100%">
                                                <fieldset>
                                                    <legend>
                                                        <asp:Label ID="lblLogininfo" Text="Employee Login Information" runat="server"></asp:Label></legend>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="lblUserNamelbl" Text="User Name" runat="server"></asp:Label>
                                                                    <asp:Label ID="Label38" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbUserName"
                                                                        ErrorMessage="RequiredFieldValidator" ValidationGroup="1">*</asp:RequiredFieldValidator></label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="tbUserName" runat="server" MaxLength="50" Width="145px" ValidationGroup="1"
                                                                        OnTextChanged="tbUserName_TextChanged" AutoPostBack="True"></asp:TextBox></label>
                                                            </td>
                                                            <td colspan="2">
                                                                <label>
                                                                    <asp:Label ID="lblusernameavailableornot" runat="server" Text="" ForeColor="Red"></asp:Label></label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="lblPasswordlbl" Text="Password" runat="server"></asp:Label>
                                                                    <asp:Label ID="Label39" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbPassword"
                                                                        ErrorMessage="RequiredFieldValidator" ValidationGroup="1">*</asp:RequiredFieldValidator></label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="tbPassword" runat="server"  Width="145px" MaxLength="50" ValidationGroup="1"></asp:TextBox></label>
                                                                        <%--TextMode="Password"--%>
                                                            </td>
                                                            <td style="width: 18%;">
                                                            </td>
                                                            <td style="width: 40%;">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="lblConfirmlbl" Text="Confirm Password" runat="server"></asp:Label>
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tbPassword" ControlToValidate="tbConPassword" ErrorMessage="Password does not match" ValidationGroup="1">
                                                                    </asp:CompareValidator>
                                                                        </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="tbConPassword" runat="server"  MaxLength="50" Width="145px"></asp:TextBox>
                                                                    <%--TextMode="Password"--%>
                                                                </label>
                                                            </td>
                                                            <td style="width: 18%;">
                                                            </td>
                                                            <td style="width: 40%;">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            </asp:Panel>
                            </div>
                            <div style="text-align: center">
                             
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btnSubmit_Click"
                                    ValidationGroup="1" />
                                <asp:Button ID="imgbtnupdate" runat="server" Visible="false" CssClass="btnSubmit"
                                    Text="Update" OnClick="imgbtnupdate_Click" ValidationGroup="2" />
                                <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="Button2_Click" />
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </asp:Panel>
                <%-- </fieldset>--%>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllist" runat="server" Text="List of Employees"></asp:Label>
                    </legend>
                    <div style="clear: both;">
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" OnClick="Button1_Click"
                            CssClass="btnSubmit" />
                        <input id="Button7" runat="server" onclick="javascript:CallPrint('divPrint')" class="btnSubmit"
                            type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="lblBusinessName" Text="Business Name" runat="server"></asp:Label><asp:DropDownList
                                        ID="filterwarehouse" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="filterwarehouse_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="lblDepartmentName" Text="Department Name" runat="server"></asp:Label>
                                    <asp:DropDownList  ID="ddlfilterdept" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterdept_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="lblDesignationName" Text="Designation Name" runat="server"></asp:Label>
                                    <asp:DropDownList ID="ddlfilterdesig" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterdesig_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="lblStatus" Text="Status" runat="server"></asp:Label><asp:DropDownList
                                        ID="ddlfilterbyactive" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbyactive_SelectedIndexChanged">
                                        <asp:ListItem Text="All" Value="2" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                </label>
                            </td>
                        </tr>
                        <div style="clear: both;">
                        </div>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label41" Text="Batch Name" runat="server"></asp:Label><asp:DropDownList
                                        ID="ddlfilterbatch" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbatch_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label42" Text="Supervisor Name" runat="server"></asp:Label><asp:DropDownList
                                        ID="ddlsupervisor" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlsupervisor_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label85" Text="Search by keywords" runat="server"></asp:Label><asp:TextBox
                                        ID="txtsearch" runat="server" AutoPostBack="true" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                                </label>
                            </td>
                        </tr>
                        <div style="clear: both;">
                        </div>
                        <tr>
                            <td>
                                <label>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="More Filters" TextAlign="Left"
                                        AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                </label>
                            </td>
                        </tr>
                        <div style="clear: both;">
                        </div>
                        <tr>
                            <td>
                                <asp:Panel ID="panelforfilters" runat="server" Visible="false" Width="100%">
                                    <label>
                                        <asp:Label ID="Label163" runat="server" Text="Previous Employment Position"></asp:Label>
                                        <asp:DropDownList ID="ddlfilterposition" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfilterposition_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label164" runat="server" Text="Highest Qualification"></asp:Label>
                                        <asp:DropDownList ID="ddlfilterqualification" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlfilterqualification_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label165" runat="server" Text="Subject of Specialisation"></asp:Label>
                                        <asp:DropDownList ID="ddlfiltersubject" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfiltersubject_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label166" runat="server" Text="Minimum Years of Experience"></asp:Label>
                                        <asp:TextBox ID="txtfilterexpr" runat="server" AutoPostBack="true" OnTextChanged="txtfilterexpr_TextChanged"></asp:TextBox>
                                </asp:Panel>
                            </td>
                        </tr>
                        <div style="clear: both;">
                        </div>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" Font-Italic="true" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="Label71" runat="server" Font-Italic="true" Text="Business :"></asp:Label><asp:Label
                                                                    ID="lblBusiness" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="lblemployeemastenote" Text="List of Employees" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="left">
                                                            <td align="left" style="text-align: left; font-size: 16px;">
                                                                <asp:Label ID="lbldepartmentprint" runat="server" Font-Italic="true"></asp:Label>
                                                                &nbsp;
                                                                <asp:Label ID="lbldesignationprint" runat="server" Font-Italic="true"></asp:Label>
                                                                &nbsp;
                                                                <asp:Label ID="lblstatusname" runat="server" Font-Italic="true"></asp:Label>&nbsp;
                                                                <asp:Label ID="lblfilbatchname" runat="server" Font-Italic="true"></asp:Label>&nbsp;
                                                                <asp:Label ID="lblsupervisorname" runat="server" Font-Italic="true"></asp:Label>
                                                                &nbsp;
                                                                <asp:Label ID="lblserchkey" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel1" runat="server" Width="100%">
                                                    <cc11:PagingGridView ID="GridView1" runat="server" Width="100%" DataKeyNames="EmployeeMasterID"
                                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        AllowPaging="True" PageSize="30" AutoGenerateColumns="False" EmptyDataText="No Record Found."
                                                        OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                        OnSorting="GridView1_Sorting" OnPageIndexChanging="GridView1_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblempid" runat="server" Text='<% #Bind("EmployeeMasterID") %>' Visible="false"></asp:Label></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblaccid" runat="server" Text='<% #Bind("AccountNo") %>' Visible="false"></asp:Label></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Business Name" SortExpression="wname" ItemStyle-Width="12%"
                                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsname" runat="server" Text='<%# Bind("wname") %>'></asp:Label></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName" ItemStyle-Width="12%"
                                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblempname" runat="server" Text='<% #Bind("EmployeeName") %>'></asp:Label>
                                                                     (<asp:Label ID="lblempid12" runat="server" Text='<% #Bind("EmployeeMasterID") %>' ></asp:Label>)
                                                                    </ItemTemplate>
                                                                    
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Supervisor Name" SortExpression="supervisor" ItemStyle-Width="12%"
                                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsuper" runat="server" Text='<% #Bind("supervisor") %>'></asp:Label></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department" SortExpression="Departmentname" ItemStyle-Width="12%"
                                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldept" runat="server" Text='<% #Bind("Departmentname") %>'></asp:Label></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation" SortExpression="DesignationName" ItemStyle-Width="12%"
                                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldesignation" runat="server" Text='<% #Bind("DesignationName") %>'></asp:Label></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Batch Name" SortExpression="Name" ItemStyle-Width="12%"
                                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbatchname" runat="server" Text='<% #Bind("Name") %>'></asp:Label></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Personal Email" SortExpression="Email" ItemStyle-Width="15%"
                                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblemail" runat="server" Text='<% #Bind("Email") %>'></asp:Label></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Phone No." SortExpression="ContactNo" ItemStyle-Width="12%"
                                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblphoneno" runat="server" Text='<% #Bind("ContactNo") %>'></asp:Label></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="City" SortExpression="CityName" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcityname123" runat="server" Text='<% #Bind("CityName") %>'></asp:Label></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" SortExpression="Active" ItemStyle-Width="7%"
                                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblActive" runat="server" Text='<% #Bind("Active") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Docs" SortExpression="DocumentId" ItemStyle-Width="5%"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("DocumentId") %>' CommandName="Send"
                                                                        CommandArgument='<%#Eval("EmployeeMasterID") %>' ForeColor="Black"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="3%">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="Button3" runat="server" CommandArgument='<%#Eval("EmployeeMasterID")%>'
                                                                        CommandName="vi" ImageUrl="~/Account/images/edit.gif" ToolTip="Edit"></asp:ImageButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btndele" runat="server" CommandArgument='<%#Eval("EmployeeMasterID")%>'
                                                                        CommandName="del" ImageUrl="~/Account/images/delete.gif" ToolTip="Delete"></asp:ImageButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="3%" />
                                                            </asp:TemplateField>
                                                            <%--<asp:ButtonField CommandName="del" Text="Delete" HeaderText="Delete" ItemStyle-Width="5%"
                                                    HeaderStyle-HorizontalAlign="Left" ButtonType="Image" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                    ImageUrl="~/Account/images/delete.gif" ItemStyle-HorizontalAlign="Left" />--%>
                                                            <%--<asp:ButtonField CommandName="view" Text="view" HeaderImageUrl="~/Account/images/viewprofile.jpg"
                                                    ItemStyle-Width="5%" ImageUrl="~/Account/images/viewprofile.jpg" HeaderText="view"
                                                    ButtonType="Image" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                </asp:ButtonField>--%>
                                                            <asp:TemplateField HeaderText="View" HeaderImageUrl="~/Account/images/viewprofile.jpg">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton48" ToolTip="View" runat="server" CommandName="View"
                                                                        ImageUrl="~/Account/images/viewprofile.jpg" CommandArgument='<%# Eval("EmployeeMasterID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </cc11:PagingGridView>
                                                </asp:Panel>
                                                <asp:Panel ID="panelover1" runat="server" Visible="false">
                                                    <asp:GridView ID="GridView5" runat="server" Width="700px" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found." AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Overhead Name" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="80%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label15811" runat="server" Text='<%# Eval("Overheadname") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="80%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label15911" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:Label ID="lbl1rate" runat="server"></asp:Label>
                                                </asp:Panel>
                                                <asp:Panel ID="panel4" runat="server" Visible="false">
                                                    <asp:GridView ID="GridView6" runat="server" Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found." AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="25%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Labesdfsdl158" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                                                    <asp:Label ID="Label10var" runat="server" Text='<%# Eval("EmployeeMasterID") %>'
                                                                        Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Average Daily Hours" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="25%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Labelsdfsdd159" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Working Days" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="25%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Labxxx158" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Yearly Hours" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="25%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Labtttt59" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:Label ID="lbl2rate" runat="server"></asp:Label>
                                                    <asp:Label ID="lbl3rate" runat="server"></asp:Label>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel21" runat="server" BackColor="#CCCCCC" BorderColor="Black" Width="500px"
                                                    BorderStyle="Solid">
                                                    <table bgcolor="#CCCCCC" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="center" bgcolor="#CCCCCC">
                                                                <table bgcolor="#CCCCCC" cellpadding="0" cellspacing="0" style="width: 500Px">
                                                                    <tr>
                                                                        <td style="text-align: left; font-weight: bolder;">
                                                                            <label>
                                                                                <asp:Label ID="Label161" runat="server" Text="Office Documents"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td style="text-align: right">
                                                                            <label>
                                                                                <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                                                    OnClick="ImageButton3_Click1" Width="15px" /></label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Panel ID="pnlof" Height="220px" Width="100%" ScrollBars="Both" runat="server">
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" PageSize="10"
                                                                                    Width="470" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                                    OnRowCommand="GridView2_RowCommand">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Doc ID" SortExpression="DocumentId" ItemStyle-HorizontalAlign="Left"
                                                                                            HeaderStyle-HorizontalAlign="Left">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" Text='<%#Eval("DocumentId") %>'
                                                                                                    CommandName="View" HeaderStyle-HorizontalAlign="Left" CommandArgument='<%#Eval("DocumentId") %>'></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="DocType" HeaderText="Cabinet-Drawer-Folder" ItemStyle-HorizontalAlign="Left"
                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                        <%-- <asp:BoundField DataField="DocumentId" HeaderText="Doc Id" />--%>
                                                                                        <asp:BoundField DataField="DocumentTitle" HeaderText="Title" HeaderStyle-HorizontalAlign="Left"
                                                                                            ItemStyle-HorizontalAlign="Left" />
                                                                                        <asp:BoundField DataField="DocumentDate" HeaderText="Date" HeaderStyle-Width="2%"
                                                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                                                    </Columns>
                                                                                    <EmptyDataTemplate>
                                                                                        No Record Found.
                                                                                    </EmptyDataTemplate>
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
                                                    Enabled="True" PopupControlID="Panel21" TargetControlID="HiddenButton222">
                                                </cc1:ModalPopupExtender>
                                                <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <div style="clear: both;">
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnlconfirm" runat="server" CssClass="modalPopup" Width="300px">
                    <fieldset>
                        <table cellpadding="0" cellspacing="0" width="100%" id="subinnertbl">
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblconmsg" runat="server">Are you sure you wish to delete 
                                    this account?</asp:Label></label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="msg" runat="server" Text="All information relating to this employee will be lost."></asp:Label></label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="ImageButton3" runat="server" Text="Confirm" CssClass="btnSubmit"
                                        OnClick="ImageButton3_Click" />
                                    <%--<asp:ImageButton ID="ImageButton3" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/Yes.png"
                                OnClick="ImageButton3_Click" />--%>
                                    &nbsp;
                                    <asp:Button ID="ImageButton5" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="ImageButton5_Click" />
                                    <%--<asp:ImageButton ID="ImageButton5" runat="server" AlternateText="cancel" ImageUrl="~/ShoppingCart/images/No.png"
                                OnClick="ImageButton5_Click" />--%>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
                <asp:Button ID="hdbtn" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender145" runat="server" BackgroundCssClass="modalBackground"
                    Enabled="true" PopupControlID="pnlconfirm" TargetControlID="hdbtn">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel5" runat="server" CssClass="modalPopup" Width="300px">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="subinnertblfc">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <label>
                                    <asp:Label ID="lbldeletemsg" runat="server">You are not allowed to DELETE 
                                this record!</asp:Label></label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 26px">
                                <asp:Button ID="ImageButton8" runat="server" Text="Ok" OnClick="ImageButton8_Click" />
                                <%--<asp:ImageButton ID="ImageButton8" runat="server" AlternateText="OK" ImageUrl="~/ShoppingCart/images/cancel.png"
                                OnClick="ImageButton8_Click" />--%>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button4" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel5" TargetControlID="Button4">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgbtnupdate" />
            <asp:PostBackTrigger ControlID="btnshowparty" />
            <asp:PostBackTrigger ControlID="imgBtnImageUpdate" />
            <asp:PostBackTrigger ControlID="Button9" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
