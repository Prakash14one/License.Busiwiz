<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="EmployeeProfile.aspx.cs" Inherits="ShoppingCart_Admin_EmployeeProfile" %>

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


                alert("You have entered invalid character");
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered invalid character");
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
    <asp:UpdatePanel ID="update" runat="server">
        <ContentTemplate>
            <div class="products_box">               
                
                 <fieldset>
                   <%-- <legend>
                        <asp:Label ID="Label74" runat="server" Text="Print Data"></asp:Label>
                    </legend>--%>
                    <asp:Panel ID="Panel4" runat="server" Width="100%">
                      <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button1_Click" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" type="button" value="Print" visible="false" />
                    </div>
                 
                 <%--   add Div Tag--%>
                    <table width="100%">
                      <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblcompany" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblem" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            </table>
                         </asp:Panel>
                </fieldset>              
               <asp:Panel ID="pnlgrid" runat="server" Width="100%">            
                
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                </div>                               
                
                <fieldset>
                    <legend>
                        <asp:Label ID="Label55" runat="server" Text="A. Basic Information"></asp:Label>
                    </legend>
                    <asp:Panel ID="pnlbasiclbl" runat="server" Width="100%">
                        <table width="100%">                  
                            <tr>
                                <td style="width: 33%">
                                    <label>
                                        <asp:Label ID="Label56" runat="server" Text="Last Name"></asp:Label>
                                    </label>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lbllastnam" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 33%">
                                    <label>
                                        <asp:Label ID="Label57" runat="server" Text="First Name"></asp:Label>
                                    </label>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblfirstnam" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 33%">
                                    <label>
                                        <asp:Label ID="Label58" runat="server" Text="Middle Initial"></asp:Label>
                                    </label>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblmiddl" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td rowspan="3">
                                    <label>
                                        <asp:Label ID="Label71" runat="server" Text="Employee Photo"></asp:Label>
                                    </label>
                                    <asp:Image ID="imgLogo" runat="server" Height="176px" Width="126px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 33%">
                                    <label>
                                        <asp:Label ID="Label60" runat="server" Text="Date of Birth"></asp:Label>
                                    </label>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblDOB" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 33%">
                                    <label>
                                        <asp:Label ID="Label59" runat="server" Text="Employee Number"></asp:Label>
                                    </label>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblempno" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 33%">
                                    <label>
                                        <asp:Label ID="Label70" runat="server" Text="Security Code"></asp:Label>
                                    </label>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lnlseccode" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        <asp:Label ID="Label61" runat="server" Text=" Social Insurance Number/Social Security Number/Other Government Issued ID"></asp:Label>
                                    </label>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblsocsecno" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label73" runat="server" Text="Sex"></asp:Label>
                                    </label>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblsex" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label1" runat="server" Text="B. Contact Information"></asp:Label></legend>
                    <div style="float: right;">
                        <asp:Button ID="btnchange" CssClass="btnSubmit" runat="server" Text="Change" OnClick="btnchange_Click" />
                    </div>
                    <div class="cleaner">
                    </div>
                    <asp:Panel ID="Panel2" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label2" runat="server" Text="Employee Name"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label CssClass="lblSuggestion" ID="lblempname" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label3" runat="server" Text="Phone Number"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label CssClass="lblSuggestion" ID="lblphone" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label4" runat="server" Text="Street Address"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label CssClass="lblSuggestion" ID="lbladdress" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label7" runat="server" Text="Fax"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblfax" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label6" runat="server" Text="Country"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblcountry" CssClass="lblSuggestion" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label10" runat="server" Text="Email"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblemail" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label9" runat="server" Text="State"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblstate" CssClass="lblSuggestion" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label8" runat="server" Text="City"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblcity" CssClass="lblSuggestion" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label5" runat="server" Text="ZIP/Postal Code"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblzipcode" CssClass="lblSuggestion" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="cleaner">
                    </div>
                    <asp:Panel ID="Panel3" runat="server" Visible="false" Width="100%">
                        <table width="100%">
                            <tr>
                                <td width="15%">
                                    <label>
                                        <asp:Label ID="Label26" runat="server" Text="Employee Name"></asp:Label>
                                    </label>
                                </td>
                                <td width="42%">
                                    <label>
                                        <asp:Label CssClass="lblSuggestion" ID="lblempedit" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td width="12%">
                                    <label>
                                        <asp:Label ID="Label28" runat="server" Text="Phone Number"></asp:Label>
                                        <asp:Label ID="Label27" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtphone" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td width="31%">
                                    <label>
                                        <asp:TextBox ID="txtphone" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span3',15)"
                                            MaxLength="15" Width="145px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtphone_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" TargetControlID="txtphone" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label72" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="Span3" cssclass="labelcount">15</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label29" runat="server" Text="Street Address"></asp:Label>
                                        <%--        <asp:Label ID="Label24" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtaddress" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([.,_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtaddress" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtaddress" runat="server" MaxLength="100" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z.,A-Z0-9_ ]+$/,'Span2',100)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label24" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="Span2" cssclass="labelcount">100</span>
                                        <asp:Label ID="Label36" runat="server" Text="(A-Z 0-9 _ .)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label32" runat="server" Text="Fax"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtfax" Width="145px" MaxLength="15" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            TargetControlID="txtfax" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label31" runat="server" Text="Country"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label34" runat="server" Text="Email ID"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtemail" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail"
                                            ErrorMessage="Invalid Email ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtemail" runat="server"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label33" runat="server" Text="State"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlstates" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstates_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label35" runat="server" Text="City"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlcity" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label30" runat="server" Text="ZIP/Postal Code"></asp:Label>
                                        <%--              <asp:Label ID="Label37" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtzipcode" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="*"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtzipcode" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtzipcode" runat="server" MaxLength="15" Width="145px" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span6',15)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label37" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="Span6" cssclass="labelcount">15</span>
                                    </label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="cleaner">
                    </div>
                    <br />
                    <asp:Button ID="btnupdate" runat="server" Visible="false" CssClass="btnSubmit" ValidationGroup="1"
                        Text="Update" OnClick="btnupdate_Click" />
                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                    <asp:Button ID="btncancel" runat="server" Text="Cancel" Visible="false" CssClass="btnSubmit"
                        OnClick="btncancel_Click" />
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label42" runat="server" Text="C. Emergency Contact Information"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnchange1" CssClass="btnSubmit" runat="server" Text="Change" OnClick="btnchange1_Click" />
                    </div>
                    <div class="cleaner">
                    </div>
                    <asp:Panel ID="pnlemerconinfolbl" runat="server">
                        <table width="100%">
                            <tr>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label43" runat="server" Text="First Emergency Contact Name"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblemerconnam1" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label44" runat="server" Text="Second Emergency Contact Name"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblemerconnam2" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label45" runat="server" Text="Relationship to Employee"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblemerrel1" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label46" runat="server" Text="Relationship to Employee"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblemerrel2" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label47" runat="server" Text="Phone Number (Home)"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblemerphhom1" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label48" runat="server" Text="Phone Number (Home)"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblemerphhom2" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label49" runat="server" Text="Phone Number (Cell)"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblemerphcel1" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label50" runat="server" Text="Phone Number (Cell)"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblemerphcel2" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label51" runat="server" Text="Phone Number (Work)"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblemerphwk1" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label52" runat="server" Text="Phone Number (Work)"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblemerphwk2" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label53" runat="server" Text="Email"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblemeremail1" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information">
                                        <asp:Label ID="Label54" runat="server" Text="Email"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label class="cssLabelCompany_Information_Ans">
                                        <asp:Label ID="lblemeremail2" CssClass="lblSuggestion" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="cleaner">
                    </div>
                    <asp:Panel ID="pnlemercontinfo" runat="server" Visible="false">
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label121" runat="server" Text="First Emergency Contact Name"></asp:Label>
                            <asp:TextBox ID="TextBox5" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span11',30)"></asp:TextBox>
                            <asp:Label ID="Label133" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span11" class="labelcount">30</span>
                            <asp:Label ID="Label134" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label122" runat="server" Text="Second Emergency Contact Name"></asp:Label>
                            <asp:TextBox ID="TextBox6" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span25',30)"></asp:TextBox>
                            <asp:Label ID="Label135" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span25" class="labelcount">30</span>
                            <asp:Label ID="Label136" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label123" runat="server" Text="Relationship to Employee"></asp:Label>
                            <asp:TextBox ID="TextBox7" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span26',30)"></asp:TextBox>
                            <asp:Label ID="Label137" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span26" class="labelcount">30</span>
                            <asp:Label ID="Label138" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label124" runat="server" Text="Relationship to Employee"></asp:Label>
                            <asp:TextBox ID="TextBox8" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span27',30)"></asp:TextBox>
                            <asp:Label ID="Label139" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span27" class="labelcount">30</span>
                            <asp:Label ID="Label140" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label125" runat="server" Text="Phone Number (Home)"></asp:Label>
                            <asp:TextBox ID="TextBox9" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span28',15)"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                TargetControlID="TextBox9" ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                            <asp:Label ID="Label141" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span28" class="labelcount">15</span>
                            <asp:Label ID="Label142" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label126" runat="server" Text="Phone Number (Home)"></asp:Label>
                            <asp:TextBox ID="TextBox10" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span29',15)"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                TargetControlID="TextBox10" ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                            <asp:Label ID="Label143" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span29" class="labelcount">15</span>
                            <asp:Label ID="Label144" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label127" runat="server" Text="Phone Number (Cell)"></asp:Label>
                            <asp:TextBox ID="TextBox11" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span30',15)"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                TargetControlID="TextBox11" ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                            <asp:Label ID="Label145" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span30" class="labelcount">15</span>
                            <asp:Label ID="Label146" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label128" runat="server" Text="Phone Number (Cell)"></asp:Label>
                            <asp:TextBox ID="TextBox12" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span31',15)"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                TargetControlID="TextBox12" ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                            <asp:Label ID="Label147" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span31" class="labelcount">15</span>
                            <asp:Label ID="Label148" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label129" runat="server" Text="Phone Number (Work)"></asp:Label>
                            <asp:TextBox ID="TextBox13" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span32',15)"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                TargetControlID="TextBox13" ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                            <asp:Label ID="Label149" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span32" class="labelcount">15</span>
                            <asp:Label ID="Label150" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label130" runat="server" Text="Phone Number (Work)"></asp:Label>
                            <asp:TextBox ID="TextBox14" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@.#$%?^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span33',15)"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                TargetControlID="TextBox14" ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                            <asp:Label ID="Label151" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span33" class="labelcount">15</span>
                            <asp:Label ID="Label152" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label131" runat="server" Text="Email"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator29" runat="server"
                                ControlToValidate="TextBox15" ErrorMessage="Invalid Email ID" Font-Size="14px"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="TextBox15" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:Label ID="Label153" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span34" class="labelcount">50</span>
                        </label>
                        <label>
                            <asp:Label ID="Label132" runat="server" Text="Email"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server"
                                ControlToValidate="TextBox16" ErrorMessage="Invalid Email ID" Font-Size="14px"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="TextBox16" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:Label ID="Label154" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span35" class="labelcount">50</span>
                        </label>
                    </asp:Panel>
                    <div class="cleaner">
                    </div>
                    <br />
                    <asp:Button ID="btnupdate1" runat="server" Visible="false" CssClass="btnSubmit" ValidationGroup="1"
                        Text="Update" OnClick="btnupdate1_Click" />
                    <asp:Button ID="btncancel1" runat="server" Text="Cancel" Visible="false" CssClass="btnSubmit"
                        OnClick="btncancel1_Click" />
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label11" runat="server" Text="D. Security Question"></asp:Label></legend>
                    <div style="float: right;">
                        <asp:Button ID="btnque1" runat="server" Text="Change" CssClass="btnSubmit" OnClick="btnque1_Click" />
                    </div>
                    <div class="cleaner">
                    </div>
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <label>
                            <asp:Label ID="Label12" runat="server" ForeColor="Red"></asp:Label>
                        </label>
                        <div class="cleaner">
                        </div>
                        <asp:Panel ID="Pnlsecurityblank" runat="server" Width="100%" Visible="false">
                            <label>
                                <asp:Label ID="Label62" runat="server" Text="There is no security question set."></asp:Label>
                            </label>
                            <div class="cleaner">
                            </div>
                            <label>
                                <asp:CheckBox ID="CheckBox1" runat="server" TextAlign="Left" Text="Would you like to set one now?"
                                    AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            </label>
                            <div class="cleaner">
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlsecurityque" runat="server" Width="100%" Visible="false">
                            <div class="cleaner">
                            </div>
                            <label class="cssLabelCompany_Information">
                                <asp:Label ID="Label13" runat="server" Text="Question 1"></asp:Label>
                            </label>
                            <label class="cssLabelCompany_Information_Ans">
                                <asp:Label CssClass="lblSuggestion" ID="lblque1" runat="server">No Question 
                            set yet</asp:Label>
                                <asp:DropDownList ID="ddlque1" Visible="false" runat="server">
                                </asp:DropDownList>
                            </label>
                            <label class="cssLabelCompany_Information">
                                <asp:Label ID="Label14" runat="server" Text="Answer 1"></asp:Label>
                                <asp:Label ID="Label40" runat="server" Text="*" CssClass="labelstar" Visible="false"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtans1" ValidationGroup="2"></asp:RequiredFieldValidator>
                            </label>
                            <label class="cssLabelCompany_Information_Ans">
                                <asp:Label CssClass="lblSuggestion" ID="lblans1" runat="server">No Answer 
                            set yet</asp:Label>
                                <asp:TextBox ID="txtans1" Visible="false" runat="server"></asp:TextBox>
                            </label>
                            <div class="cleaner">
                            </div>
                            <label class="cssLabelCompany_Information">
                                <asp:Label ID="Label15" runat="server" Text="Question 2"></asp:Label>
                            </label>
                            <label class="cssLabelCompany_Information_Ans">
                                <asp:Label CssClass="lblSuggestion" ID="lblque2" runat="server">No Question 
                            set yet</asp:Label>
                                <asp:DropDownList ID="ddlque2" Visible="false" runat="server">
                                </asp:DropDownList>
                            </label>
                            <label class="cssLabelCompany_Information">
                                <asp:Label ID="Label16" runat="server" Text="Answer 2"></asp:Label>
                            </label>
                            <label class="cssLabelCompany_Information_Ans">
                                <asp:Label CssClass="lblSuggestion" ID="lblans2" runat="server">No Answer 
                            set yet</asp:Label>
                                <asp:TextBox ID="txtans2" Visible="false" runat="server"></asp:TextBox>
                            </label>
                            <div class="cleaner">
                            </div>
                            <label class="cssLabelCompany_Information">
                                <asp:Label ID="Label17" runat="server" Text="Question 3"></asp:Label>
                            </label>
                            <label class="cssLabelCompany_Information_Ans">
                                <asp:Label CssClass="lblSuggestion" ID="lblque3" runat="server">No Question 
                            set yet</asp:Label>
                                <asp:DropDownList ID="ddlque3" Visible="false" runat="server">
                                </asp:DropDownList>
                            </label>
                            <label class="cssLabelCompany_Information">
                                <asp:Label ID="Label18" runat="server" Text="Answer 3"></asp:Label>
                            </label>
                            <label class="cssLabelCompany_Information_Ans">
                                <asp:Label CssClass="lblSuggestion" ID="lblans3" runat="server">No Answer 
                            set yet</asp:Label>
                                <asp:TextBox ID="txtans3" Visible="false" runat="server"></asp:TextBox>
                            </label>
                            <div class="cleaner">
                            </div>
                        </asp:Panel>
                        <br />
                        <asp:Button ID="btnupque1" ValidationGroup="2" runat="server" CssClass="btnSubmit"
                            Visible="false" Text="Update" OnClick="btnupque1_Click" />
                        <asp:Button ID="btnreset1" runat="server" Text="Cancel" Visible="false" CssClass="btnSubmit"
                            OnClick="btnreset1_Click" />
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label19" runat="server" Text="E. Login Information Change"></asp:Label></legend>
                     <div style="float: right;">
                        <asp:Button ID="Button2" runat="server" Text="Change" CssClass="btnSubmit" 
                             onclick="Button2_Click"/>
                    </div>
                    
                    <div class="cleaner">
                    </div>
                    <asp:Panel runat="server" ID="existinglogininfo" Visible="false">
                        <label>
                            <asp:Label ID="Label68" runat="server" Text="Existing Login Information"></asp:Label>
                        </label>
                        <div class="cleaner">
                        </div>
                        <label>
                            <asp:Label ID="Label66" runat="server" Text="User ID"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblexistuid" runat="server" Text="" ForeColor="Black"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label67" runat="server" Text="Password"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblexistpwd" runat="server" Text="" ForeColor="Black"></asp:Label>
                        </label>
                    </asp:Panel>
                    <div class="cleaner">
                    </div>
                    <div style="float: left;">
                        <table width="100%">
                            <tr>
                                <td style="width: 32%">
                                    <label>
                                        <asp:Label ID="Label69" Visible="false" runat="server" Text="What would you like to change?"></asp:Label>
                                    </label>
                                </td>
                                <td valign="bottom">
                                    <%--<asp:Button ID="btnuidpwd" runat="server" Text="Change" CssClass="btnSubmit" OnClick="btnuidpwd_Click" />--%>
                                    <asp:DropDownList Visible="false" ID="ddluidwd" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddluidwd_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="1">User ID</asp:ListItem>
                                        <asp:ListItem Value="2">Password</asp:ListItem>
                                        <asp:ListItem Value="3">Both</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="cleaner">
                    </div>
                    
                    <asp:Label ID="lbl1234" runat="server" ForeColor="Red"></asp:Label>
                    <br /><asp:Label ID="Label20" runat="server" ForeColor="Red"></asp:Label>
                    
                    <div class="cleaner">
                    </div>
                    <asp:Panel runat="server" ID="panelforuserid" Width="100%" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 33%">
                                    <label>
                                        <asp:Label ID="Label63" Visible="false" runat="server" Text="Old User ID"></asp:Label>
                                        <asp:TextBox ID="TextBox1" Visible="false" runat="server"></asp:TextBox>
                                    </label>
                                </td>
                                <td style="width: 33%">
                                    <label>
                                        <asp:Label ID="Label64" Visible="false" runat="server" Text="New User ID"></asp:Label>
                                        <asp:Label ID="Label65" Visible="false" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidfgddator4" runat="server" ErrorMessage="*"
                                            ControlToValidate="TextBox2" ValidationGroup="3"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="TextBox2" Visible="false" runat="server"></asp:TextBox>
                                    </label>
                                </td>
                                <td style="width: 33%">
                                    <label>
                                        
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="cleaner">
                    </div>
                    <asp:Panel runat="server" ID="panelforpassword" Width="100%">
                        <table width="100%">
                            <tr>
                                <td style="width: 33%">
                                    <label>
                                        <asp:Label ID="Label21" runat="server" Text="Old Password"></asp:Label>
                                        <asp:Label ID="Label38" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtoldpass" ValidationGroup="3"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtoldpass" TextMode="Password" runat="server" Width="200px"></asp:TextBox>
                                    </label>
                                </td>
                                <td style="width: 33%">
                                    <label>
                                        <asp:Label ID="Label22" runat="server" Text="New Password"></asp:Label>
                                        <asp:Label ID="Label39" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtnewpass" ValidationGroup="3"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtnewpass" TextMode="Password" runat="server" Width="200px"></asp:TextBox>
                                    </label>
                                </td>
                                <td style="width: 33%">
                                    <label>
                                        <asp:Label ID="Label23" runat="server" Text="Confirm New Password"></asp:Label>
                                        <asp:TextBox ID="txtconfirmpass" TextMode="Password" runat="server" Width="200px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="cleaner">
                    </div>
                    <br />
                    <asp:Button ID="btnchangepass" runat="server" CssClass="btnSubmit" Visible="false"
                        Text="Change" />
                    <asp:Button ID="btnupdatepass" Visible="false" ValidationGroup="3" runat="server" CssClass="btnSubmit"
                        Text="Update" OnClick="btnupdatepass_Click" />
                    <asp:Button ID="btnresetpass" Visible="false" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="btnresetpass_Click" />
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label41" runat="server" Text="List of Addresses"></asp:Label>
                    </legend>
                    <div class="cleaner">
                    </div>
                   <%-- <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button1_Click" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" type="button" value="Print" visible="false" />
                    </div>--%>
                    <div class="cleaner">
                    </div>
                    <asp:Panel ID="pnlgrid1" runat="server" Width="100%" ScrollBars="None">
                        <table width="100%">
                           <%-- <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblcompany" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblem" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>--%>
                            <tr style="vertical-align: top; height: 100%">
                                <td>
                                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                        Width="100%" OnSorting="grid_Sorting" AllowSorting="True" GridLines="Both" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Address" SortExpression="Address" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("Address")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="City" SortExpression="CityName" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcity" runat="server" Text='<%# Eval("CityName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State" SortExpression="StateName" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstate" runat="server" Text='<%# Eval("StateName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Country" SortExpression="CountryName" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcountry" runat="server" Text='<%# Eval("CountryName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Zip/Postal Code" SortExpression="Zipcode" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblzipcode" runat="server" Text='<%# Eval("zipcode")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active" HeaderStyle-Width="5%" SortExpression="AddressActiveStatus"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkactive" runat="server" Checked='<%# Eval("AddressActiveStatus")%>'
                                                        AutoPostBack="true" OnCheckedChanged="chkactive_chachedChanged"></asp:CheckBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="cleaner">
                    </div>
                    <asp:Panel ID="pnconfor" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA"
                        BorderStyle="Outset" Width="300px">
                        <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="">
                                    <asp:Label ID="Label25" runat="server" Text="Confirmation..."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblddffg" runat="server" Text="Are you sure you wish to change current address ?"
                                        ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnapd" runat="server" CssClass="btnSubmit" Text="Yes" OnClick="btnapd_Click" />
                                    <asp:Button ID="btns" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="btns_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                        ID="ModalPopupExtender122" runat="server" BackgroundCssClass="modalBackground"
                        CancelControlID="btns" PopupControlID="pnconfor" TargetControlID="HiddenButton222">
                    </cc1:ModalPopupExtender>
                </fieldset>
             </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnapd" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
