<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="WizardCompanyWebsitMaster.aspx.cs" Inherits="Add_Wizard_Company_Website_Master" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
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

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 ||  evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) 
        {
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
    <asp:UpdatePanel ID="update" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server" Text=""></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="btnadd" CssClass="btnSubmit" runat="server" Text="Add New Business"
                            OnClick="btnadd_Click" />
                            <asp:Label ID="lblV" runat="server" Text="V6" ForeColor="#FFF" Font-Size="14px"></asp:Label>
                    </div>
                    <asp:Panel ID="pnladd" Visible="false" runat="server">
                        <table width="100%">
                            <tr>
                                <td width="25%">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Company Name"></asp:Label>
                                    </label>
                                </td>
                                <td width="75%">
                                    <label>
                                        <asp:Label ID="ddlCompanyName" CssClass="lblSuggestion" runat="server">
                                        </asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="New Business"></asp:Label>
                                        <asp:Label ID="Label28" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextSiteName"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Character"
                                            SetFocusOnError="True" ValidationExpression="^(['-_a-z)(A-Z0-9\s]*)" ControlToValidate="TextSiteName"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="TextSiteName" runat="server" Width="250px" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z'_-()0-9\s]+$/,'div1',80)"
                                            ValidationGroup="1" MaxLength="80"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label41" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                        <span id="div1" class="labelcount">80</span>
                                        <asp:Label ID="lblbf" CssClass="labelcount" runat="server" Text="(A-Z 0-9 ) - ( ' _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td width="80%">
                                                    <label>
                                                        <asp:Label ID="Label39" runat="server" Text="Do you want to set up a master e-mail account? (Optional but recommended)"></asp:Label>
                                                        <div style="clear: both;">
                                                        </div>
                                                        <asp:Label ID="Label40" runat="server" Text="This e-mail address will be used as the primary account to send out notifications and e-mails to all required users."></asp:Label>
                                                    </label>
                                                </td>
                                                <td valign="top" width="20%">
                                                    <asp:CheckBox ID="chkmasteremail" runat="server" TextAlign="Left" AutoPostBack="True"
                                                        Text="Yes" OnCheckedChanged="chkmasteremail_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="pnlmasteremail" runat="server" Visible="false">
                                                        <table width="100%">
                                                            <tr valign="top">
                                                                <td width="30%">
                                                                    <label>
                                                                        <asp:Label ID="Label7" runat="server" Text="E-mail Display Name"></asp:Label>
                                                                        <asp:Label ID="Label30" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" SetFocusOnError="true"
                                                                            ControlToValidate="txtmemailname" ValidationGroup="2" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Character"
                                                                            SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtmemailname"
                                                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                    </label>
                                                                </td>
                                                                <td width="70%">
                                                                    <label>
                                                                        <asp:TextBox ID="txtmemailname" MaxLength="45" Width="150px" runat="server" onKeydown="return mask(event)"
                                                                            onkeyup="return check(this,/[\\/!@#$?%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'Span1',45)"></asp:TextBox>
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="Label21" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                                                        <span id="Span1" class="labelcount">45</span>
                                                                        <asp:Label ID="Label51" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr valign="top">
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label8" runat="server" Text="Master E-mail ID"></asp:Label>
                                                                        <asp:Label ID="Label31" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtmemailid"
                                                                            ValidationGroup="2" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                                        <div style="clear: both;">
                                                                        </div>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Email ID"
                                                                            ControlToValidate="txtmemailid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                            ValidationGroup="1" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:TextBox ID="txtmemailid" MaxLength="45" Width="150px" runat="server" onKeydown="return mask(event)"
                                                                            onkeyup="return check(this,/[\\/!#$%^?'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z.@-_0-9\s]+$/,'Span2',45)"></asp:TextBox>
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="Label52" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                                                        <span id="Span2" class="labelcount">45</span>
                                                                        <asp:Label ID="Label53" CssClass="labelcount" runat="server" Text="(A-Z 0-9 - . @ _)"></asp:Label>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr valign="top">
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label10" runat="server" Text="User Name"></asp:Label>
                                                                        <asp:Label ID="Label32" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtusmail"
                                                                            ValidationGroup="2" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                                        <div style="clear: both;">
                                                                        </div>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtusmail"
                                                                            ErrorMessage="Invalid Email ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                            ValidationGroup="1" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:TextBox ID="txtusmail" MaxLength="45" Width="150px" runat="server" onKeydown="return mask(event)"
                                                                            onkeyup="return check(this,/[\\/!#$%^'?&*()>+:;={}[]|\/]/g,/^[\a-zA-Z@.-_0-9\s]+$/,'Span3',45)"></asp:TextBox>
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="Label54" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                                                        <span id="Span3" class="labelcount">45</span>
                                                                        <asp:Label ID="Label55" CssClass="labelcount" runat="server" Text="(A-Z 0-9 - . @ _)"></asp:Label>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr valign="top">
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label42" runat="server" Text="Confirm User Name"></asp:Label>
                                                                        <asp:Label ID="Label58" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" SetFocusOnError="true"
                                                                            ControlToValidate="TextBox2" ValidationGroup="2">*</asp:RequiredFieldValidator>
                                                                        <div style="clear: both;">
                                                                        </div>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                                            ControlToValidate="TextBox2" ErrorMessage="Invalid Email ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                        <div style="clear: both;">
                                                                        </div>
                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox2"
                                                                            ControlToValidate="txtusmail" ValidationGroup="1" SetFocusOnError="true" ErrorMessage="User Name does not match"></asp:CompareValidator>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:TextBox ID="TextBox2" runat="server" Width="150px" MaxLength="45" onKeydown="return mask(event)"
                                                                            onkeyup="return check(this,/[\\/!#$?%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z@.-_0-9\s]+$/,'Span4',45)"></asp:TextBox>
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="Label56" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                                                        <span id="Span4" class="labelcount">45</span>
                                                                        <asp:Label ID="Label57" CssClass="labelcount" runat="server" Text="(A-Z 0-9 - . @ _)"></asp:Label>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr valign="top">
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label12" runat="server" Text="Password"></asp:Label>
                                                                        <asp:Label ID="Label33" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="TextEmailMasterLoginPassword"
                                                                            ErrorMessage="*" ValidationGroup="2" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:TextBox ID="TextEmailMasterLoginPassword" Width="150px" runat="server" TextMode="Password"></asp:TextBox>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr valign="top">
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label43" runat="server" Text="Confirm Password"></asp:Label>
                                                                        <asp:Label ID="Label63" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="TextBox3"
                                                                            ErrorMessage="*" ValidationGroup="2" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                        <div style="clear: both;">
                                                                        </div>
                                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="TextBox3"
                                                                            ControlToValidate="TextEmailMasterLoginPassword" SetFocusOnError="true" ValidationGroup="1"
                                                                            ErrorMessage="Password does not match"></asp:CompareValidator>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:TextBox ID="TextBox3" TextMode="Password" Width="150px" runat="server"></asp:TextBox>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr valign="top">
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label14" runat="server" Text="Incoming Mail Server"></asp:Label>
                                                                        <asp:Label ID="Label34" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="TextIncomingMailServer"
                                                                            ErrorMessage="*" ValidationGroup="2" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                        <div style="clear: both;">
                                                                        </div>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                                            SetFocusOnError="true" ErrorMessage="Invalid Character" ValidationExpression="^([.-_@a-zA-Z0-9\s]*)"
                                                                            ControlToValidate="TextIncomingMailServer" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:TextBox ID="TextIncomingMailServer" Width="150px" MaxLength="45" runat="server"
                                                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%?^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z.@-_0-9\s]+$/,'Span5',45)"></asp:TextBox>
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="Label59" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                                                        <span id="Span5" class="labelcount">45</span>
                                                                        <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="(A-Z 0-9 - . @ _)"></asp:Label>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr valign="top">
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label15" runat="server" Text="Outgoing Mail Server"></asp:Label>
                                                                        <asp:Label ID="Label35" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="TextOutGoingMailServer"
                                                                            ErrorMessage="*" ValidationGroup="2" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                        <div style="clear: both;">
                                                                        </div>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                                            ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_.-@a-zA-Z0-9\s]*)"
                                                                            ControlToValidate="TextOutGoingMailServer" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:TextBox ID="TextOutGoingMailServer" Width="150px" MaxLength="45" runat="server"
                                                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%^'?&*()>+:;={}[]|\/]/g,/^[\a-zA-Z.@-_0-9\s]+$/,'Span6',45)"></asp:TextBox>
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="Label61" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                                                        <span id="Span6" class="labelcount">45</span>
                                                                        <asp:Label ID="Label62" CssClass="labelcount" runat="server" Text="(A-Z 0-9 - . @ _)"></asp:Label>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Panel ID="Panel1" Visible="false" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:Label ID="Label9" runat="server" Text="Admin Email"></asp:Label>
                                                                                        <asp:Label ID="Label36" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextAdminEmail"
                                                                                            ErrorMessage="*" ValidationGroup="1" Visible="true" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*"
                                                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextAdminEmail"
                                                                                            ValidationGroup="1" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                                                        <asp:TextBox ID="TextAdminEmail" runat="server"></asp:TextBox><%--<input type="text" />--%>
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:Label ID="Label11" runat="server" Text="Support Email"></asp:Label>
                                                                                        <asp:Label ID="Label37" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextSupportEmail"
                                                                                            ErrorMessage="*" ValidationGroup="1" Visible="False"></asp:RequiredFieldValidator>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextSupportEmail"
                                                                                            ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                                            ValidationGroup="1" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                                                        <asp:TextBox ID="TextSupportEmail" runat="server" ValidationGroup="1"></asp:TextBox><%--<input type="text" />--%>
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:Label ID="Label13" runat="server" Text="Sales Email"></asp:Label>
                                                                                        <asp:Label ID="Label38" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextWebMasterEmail"
                                                                                            ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="*"
                                                                                            ControlToValidate="TextWebMasterEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                                            ValidationGroup="1" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                                                        <asp:TextBox ID="TextWebMasterEmail" runat="server" ValidationGroup="1"></asp:TextBox><%--<input type="text" />--%>
                                                                                    </label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" CssClass="btnSubmit"
                                                                        Text="Test e-mail" ValidationGroup="2" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label44" runat="server" Text="Do you want to give administrative accesss for your new business to the administrative users of your current existing business?"></asp:Label>
                                                        <asp:Label ID="lblddd" runat="server" Text=""></asp:Label>
                                                    </label>
                                                </td>
                                                <td valign="top">
                                                    <asp:CheckBox ID="chkad" runat="server" AutoPostBack="True" Text="Yes" TextAlign="Left"
                                                        OnCheckedChanged="chkad_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="pnladmin" runat="server" Width="100%" Visible="false">
                                                        <fieldset>
                                                            <legend>List of Admin for Current Business </legend>
                                                            <asp:GridView ID="gridAccess" runat="server" DataKeyNames="EmployeeMasterId" Width="50%"
                                                                AutoGenerateColumns="False" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                AlternatingRowStyle-CssClass="alt">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Business-Employee Name" HeaderStyle-HorizontalAlign="Left"
                                                                        SortExpression="Business" ItemStyle-Width="30%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempname" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label>
                                                                            <asp:Label ID="lblwid" runat="server" Text='<%#Eval("WareHouseId")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblEmployeeMasterId" runat="server" Text='<%#Eval("EmployeeMasterId")%>'
                                                                                Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Select Employee to give admin rights for this new business"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="ChkAess" runat="server" />
                                                                            <asp:CheckBox ID="chka" runat="server" Visible="false" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    No Record Found.
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </fieldset>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label45" runat="server" Text="Your fist accounting year is set as "></asp:Label>
                                                        <asp:Label ID="lblfirstyear" ForeColor="#457cec" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="Label46" ForeColor="#457cec" runat="server" Text="-"></asp:Label>
                                                        <asp:Label ID="lblendyear" ForeColor="#457cec" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="Label47" runat="server" Text=". Do you want to change this date?"></asp:Label>
                                                        <div style="clear: both;">
                                                        </div>
                                                        <asp:Label ID="Label64" runat="server" Text="Note: If you plan to do any accounting entry for this business, for any prior year, please change the dates of the first accounting year."></asp:Label>
                                                    </label>
                                                </td>
                                                <td valign="top">
                                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="Yes" TextAlign="Left"
                                                        OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <label>
                                                        <asp:Label ID="Label48" runat="server" Text="Note: You will be able to put the opening balances only for the first accounting year. For other years, they are carried forward from one year to the next year."></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="pnlaccy" runat="server" Visible="false" Width="100%">
                                                        <fieldset>
                                                            <legend>
                                                                <asp:Label ID="Label16" runat="server" Text="Accounting year setup"></asp:Label>
                                                            </legend>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Label ID="Label17" runat="server" Text="A.  What is the first day and month of your regular accounting year?"></asp:Label>
                                                                            <div style="clear: both;">
                                                                            </div>
                                                                            <asp:Label ID="Label49" runat="server" Text="(ex. January 1st)"></asp:Label>
                                                                        </label>
                                                                        <label>
                                                                            <asp:Label ID="Label19" runat="server" Text="Month"></asp:Label>
                                                                            <asp:DropDownList ID="ddlacmonth" runat="server" OnSelectedIndexChanged="ddlacmonth_SelectedIndexChanged"
                                                                                AutoPostBack="True" Width="60px">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                        <label>
                                                                            <asp:Label ID="Label18" runat="server" Text="Day"></asp:Label>
                                                                            <asp:DropDownList ID="ddlacday" runat="server" AutoPostBack="True" Width="60px" OnSelectedIndexChanged="ddlacday_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Label ID="Label50" runat="server" Text="B.  Select the year that your business will begin using the service provided on this site."></asp:Label>
                                                                        </label>
                                                                        <label>
                                                                            <asp:Label ID="Label20" runat="server" Text="Year"></asp:Label>
                                                                            <asp:DropDownList ID="ddlacyear" runat="server" AutoPostBack="True" Width="100px"
                                                                                OnSelectedIndexChanged="ddlacyear_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Label ID="Label22" runat="server" Text="First Accounting Year Start Date :"></asp:Label>
                                                                        </label>
                                                                        <label>
                                                                            <asp:Label CssClass="lblSuggestion" ID="lblfysdate" Text="" runat="server"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Label ID="Label1" runat="server" Text="First Accounting Year End Date :"></asp:Label>
                                                                        </label>
                                                                        <label>
                                                                            <asp:Label CssClass="lblSuggestion" ID="lblfyedate" Text="" runat="server"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button CssClass="btnSubmit" ID="btnconf" runat="server" Text="Confirm" OnClick="btnconf_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="clear: both;">
                                                            </div>
                                                            <label>
                                                                <asp:CheckBox ID="chkbusisetup" runat="server" Visible="false" Checked="True" />
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label24" runat="server" Text="Government Account Type Name" Visible="false"></asp:Label>
                                                                <asp:TextBox ID="txt1" runat="server" Visible="false"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label25" runat="server" Text="Government Account Number" Visible="false"></asp:Label>
                                                                <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                                            </label>
                                                            <div style="clear: both;">
                                                            </div>
                                                        </fieldset>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlcurrency" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td width="25%">
                                                    <label>
                                                        <asp:Label ID="Label6" runat="server" Text="Currency "></asp:Label><asp:Label ID="Label29"
                                                            runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlcurrency" InitialValue="0"
                                                                ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                    </label>
                                                </td>
                                                <td width="75%">
                                                    <label>
                                                        <asp:DropDownList ID="ddlcurrency" runat="server" Width="100px" ValidationGroup="1">
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
                                    <asp:Panel ID="Panel5" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td width="25%">
                                                    <label>
                                                        <asp:Label ID="Label67" runat="server" Text="Time Zone"></asp:Label>
                                                        <asp:Label ID="Label68" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="ddlwztimezone"
                                                            InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                    </label>
                                                </td>
                                                <td width="75%">
                                                    <label>
                                                        <asp:DropDownList ID="ddlwztimezone" runat="server" ValidationGroup="1">
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
                                    <asp:Panel ID="Panel4" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td width="25%">
                                                    <label>
                                                        <asp:Label ID="Label27" runat="server" Text=" Status"></asp:Label>
                                                    </label>
                                                </td>
                                                <td width="75%">
                                                    <label>
                                                        <asp:DropDownList ID="ddlstatus" runat="server" Width="100px">
                                                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:CheckBox ID="chkstatus" runat="server" Visible="false" Checked="True" />--%>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        <asp:Label ID="lblurrr" runat="server" Text="Website URL :" Visible="False"></asp:Label>
                                        <asp:Label ID="lblhttp" runat="server" Text="http://" Visible="False"></asp:Label>
                                        <asp:Label ID="lblsiteurl" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblshop" runat="server" Visible="False"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="ImgBtnSubmit" Text="Submit" CssClass="btnSubmit" OnClick="ImgBtnSubmit_Click"
                                        runat="server" ValidationGroup="1" />
                                    <asp:Button ID="imgbtnedit" runat="server" CssClass="btnSubmit" Text="Edit" OnClick="imgbtnedit_Click"
                                        Visible="False" />
                                    <asp:Button ID="imgbtnupdate" runat="server" Text="Update" CssClass="btnSubmit" OnClick="imgbtnupdate_Click"
                                        Visible="False" ValidationGroup="1" />
                                    <asp:Button ID="imgBtnCancelMainUpdate" runat="server" Text="Cancel" CssClass="btnSubmit"
                                        OnClick="imgBtnCancelMainUpdate_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label2" runat="server" Text="List Of Business Address"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button1_Click2" />
                        <input type="button" value="Print" id="Button3" runat="server" onclick="javascript:CallPrint('divPrint')"
                            visible="false" class="btnSubmit" />
                    </div>
                    <div style="float: left;">
                        <label>
                            <asp:Label ID="Label69" runat="server" Text="Filter by status"></asp:Label>
                        </label>
                        <label>
                            <asp:DropDownList ID="ddlactfil" runat="server" Width="100px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlactfil_SelectedIndexChanged">
                                <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="Active" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblcomname" runat="server" Font-Size="20px" Visible="false">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblhead" runat="server" Font-Size="18px" Text=" List of Company Businesses"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" DataKeyNames="CompanyWebsiteMasterId"
                                        EmptyDataText="No Records Found" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
                                        Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="True"
                                        OnSorting="GridView1_Sorting" PageSize="20">
                                        <Columns>
                                            <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblid" runat="server" Text='<%# Bind("CompanyWebsiteMasterId")%>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company Name" Visible="false" SortExpression="CompanyName"
                                                ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcompanyname" runat="server" Text='<%# Bind("CompanyName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="Sitename" ItemStyle-Width="15%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsitename" runat="server" Text='<%# Bind("Sitename")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Website URL" SortExpression="WebSite" ItemStyle-Width="50%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblurl" runat="server" Text='<%#Bind("WebSite")%>'></asp:Label>
                                                    <asp:Label ID="Label3" runat="server" Text="/Shoppingcart/default.aspx?WHid="></asp:Label>
                                                    <asp:Label ID="Label4" runat="server" Text='<%#Bind("Whid")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="50%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Currency" SortExpression="CurrencyName" ItemStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcurrency" runat="server" Text='<%# Bind("CurrencyName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="Active" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="chkstatus" runat="server" Text='<%# Bind("Active")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/edit.gif"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btndele" runat="server" CommandArgument='<%#Eval("CompanyWebsiteMasterId")%>'
                                                        CommandName="editview" ImageUrl="~/Account/images/edit.gif" ToolTip="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            <%--<asp:ButtonField CommandName="editview" HeaderStyle-HorizontalAlign="Left" ButtonType="Image"
                                                ImageUrl="~/Account/images/edit.gif" HeaderImageUrl="~/Account/images/edit.gif"
                                                Text="Edit" HeaderText="Edit" ItemStyle-Width="3%" />--%>
                                            <asp:TemplateField HeaderText="View" HeaderImageUrl="~/Account/images/viewprofile.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton48" runat="server" CommandName="View" CommandArgument='<%#Eval("CompanyWebsiteMasterId")%>'
                                                        ToolTip="View" ImageUrl="~/Account/images/viewprofile.jpg" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </cc11:PagingGridView>
                                    <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                    <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
            <asp:Panel ID="pnconfor" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA"
                BorderStyle="Outset" Width="300px">
                <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="lblddffg1" runat="server" Text="Confirmation...."></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="lblddffg" runat="server" Text="Notice : Please note that you will be able to change this information in future.You will be able to feed Opening balances of all accounts for this accounting year only."></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnapd" runat="server" CssClass="btnSubmit" Text="Confirm" OnClick="btnapd_Click" />
                            <asp:Button ID="btns" runat="server" CssClass="btnSubmit" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="Button4" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                ID="ModapupExtende22" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btns"
                PopupControlID="pnconfor" TargetControlID="Button4">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" Width="300px">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label26" runat="server" ForeColor="Black" Text="Confirmation"></asp:Label>
                    </legend>
                    <table id="subinnertbl" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label23" runat="server" Text="Your first accounting year will be :"></asp:Label>
                                    <asp:Label ID="lblfinal" runat="server" Text="Confirmation"></asp:Label>
                                    <asp:Label ID="Label65" runat="server" Text=" You will be able to add opening balances of all accounts for this accounting year only."></asp:Label>
                                    <div style="clear: both;">
                                    </div>
                                    <asp:Label ID="Label66" runat="server" Text="Please also note that once a business has been created, you will not be able to delete it, you will only be able to make it inactive."></asp:Label>
                                    <%-- <asp:Label ID="Label23" runat="server">"You are about to create new location for the company, Please note you will not be able to delete it, You will be able to make it inactive only,Are you sure you wish to create new location/Website"</asp:Label>--%>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="ImageButton5" runat="server" CssClass="btnSubmit" OnClick="ImageButton5_Click"
                                    Text="Confirm" />
                                <asp:Button ID="ImageButton6" runat="server" CssClass="btnSubmit" OnClick="ImageButton6_Click"
                                    Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <asp:Button ID="HiddenButton222" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel3" TargetControlID="HiddenButton222">
            </cc1:ModalPopupExtender>
            <!--end of right content-->
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="clear: both;">
    </div>

     <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" Text="V5" ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>
</asp:Content>
