<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="Product_PartyMaster.aspx.cs" Inherits="Add_Party_Master"
    Title="Add Party Master" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
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
        #Table4
        {
            width: 99%;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
        }

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
            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 189 || evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


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
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnshowparty" runat="server" Text="Add New User" OnClick="btnshowparty_Click"
                            CssClass="btnSubmit" />
                    </div>
                    <asp:Panel ID="pnlparty" runat="server" Visible="false" Width="100%">
                        <fieldset>
                            <table width="100%">
                                <tr valign="top">
                                    <td style="width: 18%">
                                        <label>
                                            <asp:Label ID="lblBusinessName" runat="server" Text="Business Name: "></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 32%">
                                        <label>
                                            <asp:DropDownList ID="ddwarehouse" runat="server" AutoPostBack="True" Width="150px"
                                                OnSelectedIndexChanged="ddwarehouse_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td style="width: 18%">
                                        <label>
                                            <asp:Label ID="lblContactPerson" Text="Contact Person: " runat="server"></asp:Label>
                                            <asp:Label ID="Label38" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbName"
                                                ErrorMessage="RequiredFieldValidator" ValidationGroup="1">*</asp:RequiredFieldValidator><br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                ControlToValidate="tbName" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td style="width: 32%">
                                        <label>
                                            <asp:TextBox ID="tbName" MaxLength="30" runat="server" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@.#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'div1',30)"
                                                ValidationGroup="1" Width="145px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="div1" class="labelcount">30</span>
                                            <asp:Label ID="Label34" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="lblAddress" Text="Address:" runat="server"></asp:Label>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                ControlToValidate="tbAddress" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="tbAddress" MaxLength="100" runat="server" Width="145px" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@#$%^'&*.()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'Span2',100)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label21" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span2" class="labelcount">100</span>
                                            <asp:Label ID="Label36" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblEmail" Text="Email: " runat="server"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="tbEmail" MaxLength="30" Width="145px" runat="server"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbEmail"
                                                Display="Dynamic" ErrorMessage="Enter Valid Email Address" ValidationExpression="\w+([_.]\w+)*@\w+([_.]\w+)*\.\w+([_.]\w+)*"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="lblCountry" Text="Country: " runat="server"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" Width="150px"
                                                OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" ValidationGroup="1">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblPhoneNo" Text="Phone No." runat="server"></asp:Label><br />
                                            <%--<input id="Text7" class="txtInputSmall" name="postcode1" type="text" value="Code" />--%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([0-9\s]*)" ControlToValidate="tbPhone"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="tbPhone" MaxLength="15" runat="server" onKeyup="return mak('Span3', 15, this)"
                                                Width="145px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label17" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span3" class="labelcount">15</span>
                                            <asp:Label ID="Label40" runat="server" CssClass="labelcount" Text="(0-9)"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label25" Text="(No dashes required. I.e. 1234567890)" runat="server"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="lblState" Text="State: " runat="server"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlState" Width="150px" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlState_SelectedIndexChanged" ValidationGroup="1">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lvlExtension" runat="server" Text="Extension: "></asp:Label><br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                ErrorMessage="Invalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([0-9\s]*)"
                                                ControlToValidate="tbExtension" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="tbExtension" MaxLength="10" runat="server" onKeyup="return mak('Span4', 10, this)"
                                                Width="145px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label49" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span4" class="labelcount">10</span>
                                            <asp:Label ID="Label16" runat="server" Text="(0-9)"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label28" Text="(No dashes required. I.e. 1234567890)" runat="server"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="lblCity" Text="City: " runat="server"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlCity" Width="150px" runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblFax" runat="server" Text="Fax No.: "></asp:Label><br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                ControlToValidate="tbFax" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="tbFax" MaxLength="15" onKeyup="return mak('Span5', 15, this)" runat="server"
                                                Width="145px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label18" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span5" class="labelcount">15</span>
                                            <asp:Label ID="Label19" runat="server" Text="(0-9)"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label26" Text="(No dashes required. I.e. 1234567890)" runat="server"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="lblZipCode" Text="ZIP/Postal Code: " runat="server"></asp:Label><br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                ControlToValidate="tbZipCode" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="tbZipCode" MaxLength="15" runat="server" Width="145px" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@#.,$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span6',15)"
                                                ValidationGroup="1"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label22" CssClass="labelcount" runat="server" Text="Max"></asp:Label>
                                            <span id="Span6" class="labelcount">15</span>
                                            <asp:Label ID="Label23" runat="server" CssClass="labelcount" Text="(A-Z 0-9)"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset>
                            <table width="100%">
                                <tr valign="top">
                                    <td style="width: 18%">
                                        <label>
                                            <asp:Label ID="lblPartyMasterCategory" runat="server" Text="User Category: "></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 32%">
                                        <label>
                                            <asp:DropDownList ID="ddlpartycate" runat="server" AutoPostBack="True" Width="150px"
                                                OnSelectedIndexChanged="ddlpartycate_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblpno" runat="server" Visible="false"></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 18%">
                                        <label>
                                            <asp:Label ID="lblPartyRole" runat="server" Text="User Role: "></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 32%">
                                        <label>
                                            <asp:DropDownList ID="ddlemprole" runat="server" Width="150px" ValidationGroup="1">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="lblPartyType" runat="server" Text="User Type: "></asp:Label>
                                            <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPartyType"
                                                InitialValue="0" ValidationGroup="1" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlPartyType" runat="server" AutoPostBack="True" Width="150px"
                                                OnSelectedIndexChanged="ddlPartyType_SelectedIndexChanged" ValidationGroup="1">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblPartyName" runat="server" Text="User Name: "></asp:Label>
                                            <asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="tbCompanyName"
                                                ErrorMessage="RequiredFieldValidator" ValidationGroup="1" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator><br />
                                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                ControlToValidate="tbCompanyName" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="tbCompanyName" MaxLength="30" runat="server" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@.,#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'Span1',30)"
                                                Width="145px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label20" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span1" class="labelcount">30</span>
                                            <asp:Label ID="Label2" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="lblWebsite" Text="Website: " runat="server"></asp:Label>
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtpercentage"
                                                        ValidationExpression="^(-)?\w+(\/\w\w)?$" runat="server" 
                                                        ErrorMessage="Invalid" ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                                            <br />
                                            <asp:Label ID="Label37" runat="server" Text="(i.e. http://xyz.com)"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="tbWebsite" MaxLength="50" runat="server" Width="145px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbWebsite"
                                                Display="Dynamic" ErrorMessage="Enter valid URL" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset>
                            <table width="100%">
                                <tr valign="top">
                                    <td style="width: 18%">
                                        <label>
                                            <asp:Label ID="lblBalanceLimitType" runat="server" Text="Balance Limit Type: "></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="dddlbalance"
                                                InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td style="width: 32%">
                                        <label>
                                            <asp:DropDownList ID="dddlbalance" runat="server" Width="150px" ValidationGroup="1"
                                                OnSelectedIndexChanged="dddlbalance_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td style="width: 18%">
                                    </td>
                                    <td style="width: 32%">
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="lblBalanceLimit" runat="server" Text="Balance Limit: "></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtbal"
                                                ErrorMessage="RequiredFieldValidator" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="txtaddress_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtbal" ValidChars=".">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtbal" MaxLength="10" runat="server" Width="145px" ValidationGroup="1">0</asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" CssClass="btnSubmit" />
                                        </label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:ListBox ID="listip" runat="server" AppendDataBoundItems="True" Height="47px"
                                                Width="149px" Visible="False"></asp:ListBox>
                                        </label>
                                        <label>
                                            <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click"
                                                Visible="False" CssClass="btnSubmit" />
                                        </label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="lblBalanceDate" Text="Balance Date: " runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtdate"
                                                ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtdate" runat="server" Width="70px" ValidationGroup="1"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" PopupButtonID="ImageButton4"
                                                runat="server" TargetControlID="txtdate">
                                            </cc1:CalendarExtender>
                                            <cc1:MaskedEditExtender ID="TextBox1_MaskedEditExtender" runat="server" CultureName="en-AU"
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate" />
                                        </label>
                                        <label>
                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                        </label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset>
                            <table width="100%">
                                <tr valign="top">
                                    <td style="width: 18%">
                                        <label>
                                            <asp:Label ID="lblITNumber" Text="IT Number: " runat="server"></asp:Label><br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                ControlToValidate="tbITNumber" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td style="width: 32%">
                                        <label>
                                            <asp:TextBox ID="tbITNumber" MaxLength="15" runat="server" ValidationGroup="1" Width="145px"
                                                onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*.,_()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9 ]+$/,'Span8',15)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label54" CssClass="labelcount" runat="server" Text="Max"></asp:Label>
                                            <span id="Span8" class="labelcount">15</span>
                                            <asp:Label ID="Label45" runat="server" CssClass="labelcount" Text="(A-Z 0-9)"></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 18%">
                                        <label>
                                            <asp:Label ID="lblStatus" Text="Status: " runat="server"></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 32%">
                                        <label>
                                            <asp:DropDownList ID="ddlActive" runat="server" Width="150px">
                                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="lblGSTNumber" Text="GST Number: " runat="server"></asp:Label><br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                ControlToValidate="tbGSTNumber" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="tbGSTNumber" MaxLength="15" runat="server" ValidationGroup="1" Width="145px"
                                                onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*.,_()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9 ]+$/,'Span7',15)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label53" CssClass="labelcount" runat="server" Text="Max"></asp:Label>
                                            <span id="Span7" class="labelcount">15</span>
                                            <asp:Label ID="Label44" runat="server" CssClass="labelcount" Text="(A-Z 0-9)"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblCategory" Text="Category: " runat="server"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlstatus" runat="server" ValidationGroup="1" Width="150px">
                                            </asp:DropDownList>
                                        </label>
                                        <label>
                                            <asp:Button ID="btnaddstatus" CssClass="btnSubmit" Text="Add" OnClick="btnaddstatus_Click"
                                                runat="server" />
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="lblcustdisc" Visible="False" Text="Customer Category Discount: " runat="server"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlcustomerdis" runat="server" Visible="False" Width="150px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:ListBox ID="listb" runat="server" AppendDataBoundItems="True" Height="47px"
                                                Width="150px" Visible="False"></asp:ListBox>
                                        </label>
                                        <label>
                                            <asp:Button ID="btnremove" CssClass="btnSubmit" Text="remove" OnClick="btnremove_Click"
                                                runat="server" Visible="False" />
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="lblusername123" runat="server" Text="User Name: " Visible="False"></asp:Label>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                ErrorMessage="Invalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.@a-zA-Z0-9\s]*)"
                                                ControlToValidate="tbUserName" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="tbUserName" MaxLength="20" runat="server" Visible="false" Width="145px"></asp:TextBox>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblpassword123" runat="server" Text="Password" Visible="False"></asp:Label>
                                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" 
                                            ErrorMessage="*" Display="Dynamic"
                                            SetFocusOnError="True" ValidationExpression="^([_.@a-zA-Z0-9\s]*)"  
                                            ControlToValidate="tbPassword" ValidationGroup="1"></asp:RegularExpressionValidator>     --%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="tbPassword" MaxLength="20" runat="server" TextMode="Password" Visible="false"
                                                Width="145px"></asp:TextBox>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="lbldept" runat="server" Text="Related Department: " Visible="false"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddldept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldept_SelectedIndexChanged"
                                                ValidationGroup="1" Width="150px" Visible="False">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lbldes" runat="server" Text="Designation: " Visible="False"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddldesignation" runat="server" ValidationGroup="1" Width="150px"
                                                OnSelectedIndexChanged="ddldesignation_SelectedIndexChanged" Visible="False">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:CheckBox ID="CheckBox2" runat="server" Text="Would you like to send a confirmation Email to the Party ? " TextAlign="Right" />
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <asp:Label ID="Label114" runat="server" Text="Assigned Purchase Dept ID " Visible="false"></asp:Label>
                                        <asp:Label ID="Label116" runat="server" Text="Assigned Sales Dept ID" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAssSalDeptId" runat="server" Enabled="False" Width="150px"
                                            Visible="false">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlAssPurDeptId" runat="server" Enabled="False" Width="150px"
                                            Visible="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label113" runat="server" Text="Assigned Recieving Dept ID" Visible="false"></asp:Label>
                                        <asp:Label ID="Label115" runat="server" Text="Assigned Shipping Dept ID" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAssRecieveDeptId" runat="server" Enabled="False" Width="150px"
                                            Visible="False">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlAssShipDeptId" runat="server" Enabled="False" Width="150px"
                                            Visible="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:FileUpload ID="FileUploadPhoto" runat="server" Visible="False" />
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlGroup" runat="server" Width="150px" ValidationGroup="1"
                                                Visible="False" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlGroup"
                                                ErrorMessage="RequiredFieldValidator" Display="Dynamic" Visible="False">*</asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="tbConPassword" runat="server" TextMode="Password" Visible="false"> </asp:TextBox>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <div style="clear: both;">
                        </div>
                        <table width="100%">
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Button ID="btnSubmit" CssClass="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"
                                        runat="server" ValidationGroup="1" />
                                    <asp:Button ID="imgbtnedit" runat="server" CssClass="btnSubmit" Text="Edit" OnClick="imgbtnedit_Click"
                                        Visible="False" />
                                    <asp:Button ID="imgbtnupdate" runat="server" CssClass="btnSubmit" Text="Update" OnClick="imgbtnupdate_Click"
                                        ValidationGroup="1" Visible="false" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblParties" Text="List of Users" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="Printable Version"
                            CssClass="btnSubmit" />
                        <input id="Button5" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label24" runat="server" Text="Filter by:"></asp:Label>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                        <asp:DropDownList ID="ddshorting" Width="150px" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddshorting_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label8" runat="server" Text="User Category"></asp:Label>
                        <asp:DropDownList ID="ddlpartycategory" runat="server" AutoPostBack="True" Width="150px"
                            OnSelectedIndexChanged="ddlpartycategory_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlfilterstatuscategory" runat="server" AutoPostBack="True"
                            Width="150px" Visible="false" OnSelectedIndexChanged="ddlfilterstatuscategory_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label6" runat="server" Width="149px" Text="User Type"></asp:Label>
                        <asp:DropDownList ID="ddlfilterbypartytype" runat="server" AutoPostBack="True" Width="150px"
                            OnSelectedIndexChanged="ddlfilterbypartytype_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label9" runat="server" Width="149px" Text="Status"></asp:Label>
                        <asp:DropDownList ID="ddlfilterbyactive" runat="server" Width="150px">
                            <asp:ListItem Text="All" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label12" runat="server" Width="149px" Text="Search"></asp:Label>
                        <asp:TextBox ID="txtsearch" runat="server" Width="145px"></asp:TextBox>
                    </label>
                    <label>
                        <br />
                        <asp:Button ID="btngo" runat="server" Text=" Go " CssClass="btnSubmit" OnClick="btngo_Click" />
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="lblfiltercustdiscategory" runat="server" Visible="False" Text="Customer Category Discount"></asp:Label>
                        <asp:DropDownList ID="ddlfiltercusdis" runat="server" Width="150px" Visible="False">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td colspan="4">
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: center; color: Black; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Italic="True" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; color: Black; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="Label10" runat="server" Font-Italic="True" Font-Size="20px" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Italic="True" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; color: Black; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label11" runat="server" Font-Italic="True" Text="List of Users" ForeColor="Black"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="color: Black; font-size: 16px;">
                                                    <asp:Label ID="lblsc" runat="server" Font-Italic="True" Text="User Category :"></asp:Label>
                                                    <asp:Label ID="lblstcategory" runat="server" Font-Italic="True"></asp:Label>,
                                                    <asp:Label ID="lblpartytypetext" runat="server" Font-Italic="True" Text="User Type :"></asp:Label>
                                                    <asp:Label ID="lblptype" runat="server" Font-Italic="True"></asp:Label>,
                                                    <asp:Label ID="lblact" runat="server" Font-Italic="True" Text="Status :"></asp:Label>
                                                    <asp:Label ID="lblactive" runat="server" Font-Italic="True"></asp:Label>
                                                    <asp:Label ID="lblterc" runat="server" Font-Italic="True" Text="Customer Discount Category :"
                                                        Visible="false"></asp:Label>
                                                    <asp:Label ID="lblfilterc" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <%--<asp:Panel ID="Panel1" runat="server"  ScrollBars="Both" Width="960px">--%>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PartyID"
                                        GridLines="Both" AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" AllowSorting="true" OnRowCommand="GridView1_RowCommand"
                                        Width="100%" HorizontalAlign="Center" OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting"
                                        OnPageIndexChanging="GridView1_PageIndexChanging1" EmptyDataText="No Record Found."
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField Visible="true" HeaderText="User ID" SortExpression="PartyID" ItemStyle-Width="6%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpartyid" runat="server" Text='<% #Bind("PartyID") %>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" SortExpression="Account">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblaccid" runat="server" Text='<% #Bind("Account") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Type" SortExpression="PartType" ItemStyle-Width="8%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblptype" runat="server" Text='<% #Bind("PartType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name" SortExpression="Compname" ItemStyle-Width="14%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpartyname" runat="server" Text='<% #Bind("Compname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Person" SortExpression="Contactperson" ItemStyle-Width="16%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcontper" runat="server" Text='<% #Bind("Contactperson") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Website" SortExpression="Website">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwebsite" runat="server" Text='<% #Bind("Website") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Email" SortExpression="Email" ItemStyle-Wrap="true"
                                                ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href="MessageCompose.aspx?pid=<%#DataBinder.Eval(Container.DataItem,"PartyID") %>"
                                                        target="_blank">
                                                        <asp:Label ID="lblpartyid1" runat="server" ForeColor="Black" Text='<% #Bind("Email") %>'></asp:Label>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone No." SortExpression="Phoneno" ItemStyle-Width="11%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpartyid2" runat="server" Text='<% #Bind("Phoneno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="City:State:Country:Zip" SortExpression="country" ItemStyle-Width="22%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcitys" runat="server" Text='<% #Bind("country") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Status" SortExpression="StatusName" ItemStyle-Width="3%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<% #Bind("StatusName") %>' Enabled="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left"
                                                HeaderImageUrl="~/Account/images/edit.gif">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="edit" CommandName="editview" CommandArgument='<% #Bind("PartyID") %>'
                                                        ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left"
                                                HeaderImageUrl="~/Account/images/trash.jpg">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="delete" CommandName="del" CommandArgument='<% #Bind("PartyID") %>'
                                                        ToolTip="Delete" ImageUrl="~/Account/images/delete.gif" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left"
                                                HeaderImageUrl="~/Account/images/viewprofile.jpg">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="view" CommandName="View" CommandArgument='<% #Bind("PartyID") %>'
                                                        ToolTip="View" ImageUrl="~/Account/images/viewprofile.jpg" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc11:PagingGridView>
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    <%--</asp:Panel>--%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnlconfirm" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA"
                    BorderStyle="Outset" Width="300px">
                    <table cellpadding="0" cellspacing="3" width="100%">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="lblconmsg" runat="server">Do you wish to delete this record?</asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="msg" runat="server" Text="All Information for this account will be lost."></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="ImageButton3" CssClass="btnSubmit" Text="Confirm" OnClick="ImageButton3_Click"
                                    runat="server" />
                                &nbsp;
                                <asp:Button ID="ImageButton5" CssClass="btnSubmit" Text="Cancel" OnClick="ImageButton5_Click"
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="hdbtn" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender145" runat="server" BackgroundCssClass="modalBackground"
                    Enabled="True" PopupControlID="pnlconfirm" TargetControlID="hdbtn">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel6" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA" BorderStyle="Outset"
                    Width="300px">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="subinnertblfc">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <label>
                                    <asp:Label ID="lbleditmsg" runat="server">Sorry, Edit is not 
                                                    allowed!</asp:Label></label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 26px">
                                &nbsp;
                                <asp:Button ID="ImageButton10" CssClass="btnSubmit" Text="OK" OnClick="ImageButton10_Click"
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button3" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel6" TargetControlID="Button3">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA" BorderStyle="Outset"
                    Width="300px">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="subinnertblfc">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <label>
                                    <asp:Label ID="lbldeletemsg" runat="server">you cannot delete this party record because it is having opening balance kindly,change that then you can delete it!</asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 26px">
                                &nbsp;
                                <asp:Button ID="ImageButton8" CssClass="btnSubmit" Text="OK" OnClick="ImageButton8_Click"
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button2" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel5" TargetControlID="Button2">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA" BorderStyle="Outset"
                    Width="300px">
                    <table cellpadding="0" cellspacing="0" width="100%" id="Table1">
                        <tr>
                            <td class="subinnertblfc">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <label>
                                    <asp:Label ID="lblm" runat="server">Please check the date.</asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 26px">
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Start Date of the Year is "></asp:Label>
                                    <asp:Label ID="lblstartdate" runat="server"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 26px">
                                <label>
                                    <asp:Label ID="lblm0" runat="server">You can not select 
                                                            anydate earlier than that. </asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 26px">
                                <asp:Button ID="ImageButton2" runat="server" Text="Cancel" OnClick="ImageButton2_Click"
                                    CssClass="btnSubmit" />
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="HiddenButton2221" runat="server" Style="display: none" />
                </asp:Panel>
                <asp:Button ID="Button4" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel3" TargetControlID="HiddenButton2221">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel4" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA" BorderStyle="Outset"
                    Width="300px">
                    <table cellpadding="0" cellspacing="0" width="100%" id="Table2">
                        <tr>
                            <td class="subinnertblfc">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <label>
                                    <asp:Label ID="Label7" runat="server">Please check the date.</asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 26px">
                                <label>
                                    <asp:Label ID="Label13" runat="server" Text="End Date of the Year is "></asp:Label>
                                    <asp:Label ID="lblenddate" runat="server"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 26px">
                                <label>
                                    <asp:Label ID="Label14" runat="server">You can not select 
                                                            anydate after that date. </asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 26px">
                                <asp:Button ID="Button7" runat="server" CssClass="btnSubmit" OnClick="ImageButton4_Click"
                                    Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="HiddenButton22212" runat="server" Style="display: none" />
                </asp:Panel>
                <asp:Button ID="Button6" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel4" TargetControlID="HiddenButton22212">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel2" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA" BorderStyle="Outset"
                    Width="300px">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="subinnertblfc">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label15" runat="server">You are not 
                                                    allowed to DELETE ADMIN Account!</asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 26px">
                                &nbsp;
                                <asp:Button ID="ImageButton1" CssClass="btnSubmit" Text="OK" OnClick="ImageButton8_Click"
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button1" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender112121" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel2" TargetControlID="Button2">
                </cc1:ModalPopupExtender>
                <!--end of right content-->
                <div style="clear: both;">
                </div>
                <cc1:FilteredTextBoxExtender ID="txtEndzip_FilteredTextBoxExtender" runat="server"
                    Enabled="True" TargetControlID="tbPhone" ValidChars="0123456789">
                </cc1:FilteredTextBoxExtender>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                    TargetControlID="tbExtension" ValidChars="0123456789">
                </cc1:FilteredTextBoxExtender>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                    TargetControlID="tbFax" ValidChars="0123456789">
                </cc1:FilteredTextBoxExtender>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                    TargetControlID="txtbal" ValidChars="0123456789.">
                </cc1:FilteredTextBoxExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
