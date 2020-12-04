<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="EmployeeMaster.aspx.cs" Inherits="EmployeeMaster" Title="Untitled Page" %>

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllgng" runat="server" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="addnewpanel" runat="server" Text="Add Employee" CssClass="btnSubmit"
                            OnClick="addnewpanel_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text="Employee Name"></asp:Label>
                                        <asp:Label ID="Label1" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator117" runat="server" ControlToValidate="txtempname"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtempname" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtempname" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',30)"
                                            Width="180px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="max1" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" cssclass="labelcount">30</span>
                                        <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="User Name (preferably Email Id given by your company)"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtusername"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtusername" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtusername" runat="server" onKeydown="return mask(event)" MaxLength="30"
                                            onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span1',30)"
                                            Width="180px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label35" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" cssclass="labelcount">30</span>
                                        <asp:Label ID="Label5" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                    <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" OnClick="Button4_Click"
                                        Text="CheckAvailability" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label17" runat="server" Text="Password"></asp:Label>
                                        <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpassword"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([.@+-_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtpassword" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtpassword" runat="server" onkeyup="return mak('Span2',20,this)"
                                            MaxLength="20" Width="180px" TextMode="Password"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label36" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span2" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label6" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ . @ + -)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label18" runat="server" Text="Supervisor Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlsupervisor" runat="server" Width="184px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label19" runat="server" Text="Designation Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddldesignation" runat="server" Width="184px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label20" runat="server" Text="FTP Server URL"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtftpserverurl" onkeyup="return mak('Span3',50,this)" MaxLength="50"
                                            runat="server" Width="180px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label37" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span3" cssclass="labelcount">50</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label21" runat="server" Text="FTP Port"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtftpport" onkeyup="return mak('Span4',10,this)" MaxLength="10"
                                            runat="server" Width="180px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtftpport" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label38" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span4" cssclass="labelcount">10</span>
                                        <asp:Label ID="asdasd" runat="server" Text="(0-9)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label22" runat="server" Text="FTP UserId"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtftpuserid" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtftpuserid" onKeydown="return mask(event)" MaxLength="30" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span5',30)"
                                            runat="server" Width="180px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label39" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span5" cssclass="labelcount">30</span>
                                        <asp:Label ID="lblid" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label23" runat="server" Text="FTP Password"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([.@+-_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtftppassword" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtftppassword" runat="server" onkeyup="return mak('Span6',20,this)"
                                            MaxLength="20" Width="180px" TextMode="Password"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label40" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span6" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label7" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ . @ + -)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label24" runat="server" Text="Phone No."></asp:Label>
                                        <asp:Label ID="lb" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator120" runat="server" ControlToValidate="txtphoneno"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtphoneno" onkeyup="return mak('Span7',20,this)" MaxLength="20"
                                            runat="server" Width="180px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FiltereffdTextBoxExtender1" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtphoneno" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label41" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span7" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label8" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label25" runat="server" Text="Phone Extension"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtphoneextension" runat="server" onkeyup="return mak('Span8',10,this)"
                                            MaxLength="10" Width="180px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtphoneextension" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label42" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span8" cssclass="labelcount">10</span>
                                        <asp:Label ID="Label10" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label26" runat="server" Text="Mobile No."></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtmobileno" onkeyup="return mak('Span9',10,this)" runat="server"
                                            Width="180px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtmobileno" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label43" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span9" cssclass="labelcount">10</span>
                                        <asp:Label ID="Label11" runat="server" CssClass="labelcount" Text="(0-9)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label27" runat="server" Text="Country"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlcountry" runat="server" Width="184px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label34" runat="server" Text="State"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlstate" runat="server" Width="184px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label28" runat="server" Text="City"></asp:Label>
                                        <asp:Label ID="lbllbl" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator118" runat="server" ControlToValidate="txtcity"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtcity"
                                            Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtcity" onKeydown="return mask(event)" MaxLength="30" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span10',30)"
                                            runat="server" Width="180px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label44" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span10" cssclass="labelcount">30</span>
                                        <asp:Label ID="Label12" CssClass="labelcount" runat="server" Text="(A-Z,0-9,_)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label29" runat="server" Text="Email"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator119" runat="server" ControlToValidate="txtemail"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtemail"
                                            Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtemail" MaxLength="30" onKeydown="return mak('Span11',30,this)"
                                            runat="server" Width="180px">
                                        </asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label45" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span11" cssclass="labelcount">30</span>
                                        <%--<asp:Label ID="Label13" CssClass="labelcount" runat="server" Text="(A-Z,0-9,_,@,.,-)"></asp:Label>--%>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label30" runat="server" Text="Zipcode"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtzipcode" runat="server" onKeydown="return mak('Span12',15,this)"
                                            Width="180px" MaxLength="15"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtzipcode" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label46" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span12" cssclass="labelcount">15</span>
                                        <asp:Label ID="Label14" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label31" runat="server" Text="Role Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlrolename" runat="server" Width="184px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Effective Rate"></asp:Label>
                                        <asp:Label ID="Label48" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="1" ControlToValidate="txteffectiverate"
                                            runat="server" ErrorMessage="*">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([0-9.\s]*)"
                                            ControlToValidate="txteffectiverate" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txteffectiverate" MaxLength="30" Width="184px" onKeydown="return mak('Span14',30,this)"
                                            runat="server"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label49" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span14" cssclass="labelcount">30</span>
                                        <asp:Label ID="Label50" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label32" runat="server" Text="Active"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label33" runat="server" Text="Allowed Ip Address for login"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="TextBox1" onkeyup="return mak('Span13',20,this)" MaxLength="20"
                                            runat="server" Width="180px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="TextBox1" ValidChars="012.3456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label47" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span13" cssclass="labelcount">20</span>
                                    </label>
                                    <label>
                                        <asp:Button ID="Button5"  runat="server" Text="Add" OnClick="Button5_Click" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:ListBox ID="ListBox1" runat="server" Width="184px"></asp:ListBox>
                                    </label>
                                    <label>
                                     <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnSubmit" OnClick="btnEdit_Click"
                                        Visible="False" />
                                    </label>
                                   <label>
                                   <asp:Button ID="Button6" runat="server" Text="Remove" CssClass="btnSubmit" OnClick="Button6_Click" />
                                   </label>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="Button1_Click"
                                        ValidationGroup="1" />
                                    <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" OnClick="Button3_Click"
                                        Text="Update" Visible="False" ValidationGroup="1" />
                                    <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="Button2_Click" />
                                    &nbsp;<asp:Button ID="Btn_sycronce" runat="server" CssClass="btnSubmit" 
                                        OnClick="Button1_ClickSyncronice" Text="Synchronize With Busiwiz Master" ValidationGroup="1" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="List Of Employee"></asp:Label>
                    </legend>
                    <table>
                   
                      <tr>
                        <td>
                            <label>
                                <asp:Label ID="lbldes" runat="server" Text="Designation"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddldes" runat="server" AppendDataBoundItems="True" 
                                onselectedindexchanged="ddldes_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>All</asp:ListItem>
                                    
                                </asp:DropDownList>
                            </label>
                        </td>
                        </tr>
<tr>
                        <td>
                            <label>
                                <asp:Label ID="lblsup" runat="server" Text="Supervisor"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlsup" runat="server" AppendDataBoundItems="True" 
                                AutoPostBack="True" onselectedindexchanged="ddlsup_SelectedIndexChanged">
                                    
                                    <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                        </td>
                        </tr>
<tr>
                        <td>
                            <label>
                                <asp:Label ID="lblactive" runat="server" Text="Active"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlactive" runat="server" AppendDataBoundItems="True" 
                                AutoPostBack="True" onselectedindexchanged="ddlactive_SelectedIndexChanged">
                                    <asp:ListItem>All</asp:ListItem>
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Inctive</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                        </td>
                        </tr>
                         <tr>
                        <td>
                            <label>
                                <asp:Label ID="lblsearch" runat="server" Text="Search"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:TextBox ID="txtsearch" runat="server" AutoPostBack="True" 
                                ontextchanged="txtsearch_TextChanged"></asp:TextBox>
                            </label>
                        </td>
                        </tr>

                    </table>
                    <div style="float: right;">
                        <asp:Button ID="btnprint" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            CausesValidation="False" OnClick="btnprint_Click" />
                        <input id="btnin" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label9" runat="server" Text="List Of Employee" Font-Italic="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                 CssClass="mGrid"  PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                                                 Width="100%" DataKeyNames="Id"
                                                    OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                                                    OnRowCommand="GridView1_RowCommand" AllowSorting="True" OnSorting="GridView1_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Employee Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblemployeename123" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                                <asp:Label ID="lblempid123" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" SortExpression="UserId">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblusername123" runat="server" Text='<%#Bind("UserId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supervisor Name" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="SupervisorName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblusernxzcame123" runat="server" Text='<%#Bind("SupervisorId") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblsupervisorname123" runat="server" Text='<%#Bind("SupervisorName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Left" SortExpression="DesignationName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldesignation123" runat="server" Text='<%#Bind("DesignationId") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbldesignationname" runat="server" Text='<%#Bind("DesignationName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FTPServerURL" HeaderStyle-HorizontalAlign="Left" SortExpression="FTPServerURL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblftpserverurl123" runat="server" Text='<%#Bind("FTPServerURL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mobile No" HeaderStyle-HorizontalAlign="Left" SortExpression="MobileNo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmbono" runat="server" Text='<%#Bind("MobileNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PhoneExtension" HeaderStyle-HorizontalAlign="Left" SortExpression="PhoneExtension">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblphnext" runat="server" Text='<%#Bind("PhoneExtension") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Role" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="Role_name">
                                                            <ItemTemplate>
                                                            <asp:Label ID="lblrole1" runat="server" Text='<%#Bind("RoleId") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblrolename" runat="server" Text='<%#Bind("Role_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:ButtonField CommandName="vi" HeaderImageUrl="~/Account/images/edit.gif" ButtonType="Image"
                                                            ImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" Text="Edit"
                                                            HeaderText="Edit" ValidationGroup="2">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:ButtonField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgdelete" ToolTip="Delete" runat="server" CommandName="Delete"
                                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                    CommandArgument='<%# Eval("Id") %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <%-- <asp:CommandField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" ShowDeleteButton="True" />--%>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </td>
                                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                        </tr>
                                    </table>
                                </asp:Panel>

                                 <asp:Panel ID="Pnl_EmployeeMasterForIjon" Visible="false"  runat="server" Width="100%">
                                  <asp:TextBox ID="tbUserName"  runat="server" Width="180px"></asp:TextBox>
                                   <asp:TextBox ID="tbPassword"  runat="server" Width="180px"></asp:TextBox>
                                 </asp:Panel>
                            </td>
                        </tr>
                    </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
