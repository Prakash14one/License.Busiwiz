<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="WizardCompanyWebsiteAddressMaster.aspx.cs" Inherits="Add_Address_Master"
    Title="Business Address-Add,Manage" %>

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

        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
        function mask(evt) {
            counter = document.getElementById(id);
            alert(counter);
            if (evt.srcElement.value.length > max_len && evt.keyCode != 8) {
                return false;
            }
            if (evt.keyCode == 13 ) {

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
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

    <div class="products_box">
        <asp:UpdatePanel ID="update1" runat="server">
            <ContentTemplate>
                <div style="padding-left: 1%">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" Text="" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add New Address" OnClick="btnadd_Click"
                            CssClass="btnSubmit" />
                    </div>
                    <asp:Panel ID="Pnladdnew" Visible="false" runat="server">
                        <table width="100%">
                            <tr>
                                <td width="25%">
                                    <label>
                                        <asp:Label ID="lblBusinessname" Text="Business Name" runat="server"></asp:Label>
                                        <asp:Label ID="Label23" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator176" runat="server" ControlToValidate="ddlCompanyWebsiteMasterName"
                                            InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator></label>
                                </td>
                                <td colspan="3" width="75%">
                                    <label>
                                        <asp:DropDownList ID="ddlCompanyWebsiteMasterName" runat="server" Width="200px" OnSelectedIndexChanged="ddlCompanyWebsiteMasterName_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblAddressType" Text="Address Type Name" runat="server"></asp:Label>
                                        <asp:Label ID="Label24" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlAddressTypeMasterName"
                                            InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator></label>
                                </td>
                                <td valign="top">
                                    <label>
                                        <asp:DropDownList ID="ddlAddressTypeMasterName" runat="server" Width="200px" ValidationGroup="1">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton50" runat="server" Height="20px" ImageAlign="Bottom"
                                            ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" ToolTip="AddNew" OnClick="ImageButton50_Click" />
                                        <asp:ImageButton ID="ImageButton51" runat="server" Height="20px" ImageAlign="Bottom"
                                            ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                            OnClick="ImageButton51_Click1" />
                                    </label>
                                </td>
                                <td valign="middle" align="left" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblContact" Text="Contact Name" runat="server"></asp:Label>
                                        <asp:Label ID="Label25" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="TextContactPersonName"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                            SetFocusOnError="True" ValidationExpression="^([a-zA-Z\s]*)" ControlToValidate="TextContactPersonName"
                                            ValidationGroup="1"></asp:RegularExpressionValidator></label>
                                </td>
                                <td colspan="3">
                                    <label>
                                        <asp:TextBox ID="TextContactPersonName" runat="server" Width="250px" ValidationGroup="1"
                                            MaxLength="50" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'].&amp;*()&gt;_0-9+:;={}[]|\/]/g,/^[\a-zA-Z\s]+$/,'div1',50)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="div1" class="labelcount">50</span>
                                        <asp:Label ID="Label12" CssClass="labelcount" runat="server" Text="(A-Z)"></asp:Label></label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblContactPerson" Text="Contact Designation" runat="server"></asp:Label>
                                        <asp:Label ID="Label26" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="TextContactPersonDesignation"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                            SetFocusOnError="True" ValidationExpression="^([a-zA-Z\s]*)" ControlToValidate="TextContactPersonDesignation"
                                            ValidationGroup="1"></asp:RegularExpressionValidator></label>
                                </td>
                                <td colspan="3" valign="top">
                                    <label>
                                        <asp:TextBox ID="TextContactPersonDesignation" runat="server" ValidationGroup="1"
                                            Width="250px" MaxLength="50" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'].&amp;*()&gt;_0-9+:;={}[]|\/]/g,/^[\a-zA-Z\s]+$/,'Span1',50)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label36" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span1" class="labelcount">50</span>
                                        <asp:Label ID="Label13" CssClass="labelcount" runat="server" Text="(A-Z)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td colspan="4">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table width="100%">
                                            <tr valign="top">
                                                <td width="25%">
                                                    <label>
                                                        <asp:Label ID="lblStreetAddressL1" Text="Street Address (line 1)" runat="server"></asp:Label>
                                                        <asp:Label ID="Label27" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress1"
                                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                ID="REG1" runat="server" ErrorMessage="Invalid Character" SetFocusOnError="True"
                                                                ValidationExpression="^([._a-z,-/A-Z0-9\s]*)" ControlToValidate="txtAddress1"
                                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                                    </label>
                                                </td>
                                                <td width="75%">
                                                    <label>
                                                        <asp:TextBox ID="txtAddress1" Width="400px" runat="server" ValidationGroup="1" onkeypress="return checktextboxmaxlength(this,150)"
                                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&amp;*()&gt;+:;={}[]|\/]/g,/^[\a-z,-/A-Z._0-9\s]+$/,'Span2',150)"
                                                            MaxLength="150"></asp:TextBox>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label37" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                        <span id="Span2" class="labelcount">150</span>
                                                        <asp:Label ID="lblAdd" CssClass="labelcount" Text="(A-Z 0-9 _ . , - /)" runat="server"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td>
                                                    <label>
                                                        <asp:Label ID="lblStreetAddressL2" Text="Street Address (line 2)" runat="server"></asp:Label>
                                                        <br />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                                            SetFocusOnError="True" ValidationExpression="^([._a-z,-/A-Z0-9\s]*)" ControlToValidate="txtAddress2"
                                                            ValidationGroup="1"></asp:RegularExpressionValidator></label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:TextBox ID="txtAddress2" Width="400px" runat="server" onkeypress="return checktextboxmaxlength(this,150)"
                                                            MaxLength="150" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&amp;*()&gt;+:;={}[]|\/]/g,/^[\a-z,-/A-Z._0-9\s]+$/,'Span3',150)"></asp:TextBox>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label38" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                        <span id="Span3" class="labelcount">150</span>
                                                        <asp:Label ID="Label5" CssClass="labelcount" Text="(A-Z 0-9 _ . , - /)" runat="server"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table width="100%">
                                        <tr valign="top">
                                            <td width="18%">
                                                <label>
                                                    <asp:Label ID="lblCountry" Text="Country" runat="server"></asp:Label>
                                                    <asp:Label ID="Label28" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlcountry"
                                                        ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                                </label>
                                                <label>
                                                    <asp:DropDownList Width="110px" ID="ddlcountry" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" ValidationGroup="1">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblState" Text="State" runat="server"></asp:Label>
                                                    <asp:Label ID="Label29" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlstate"
                                                        ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                                </label>
                                                <label>
                                                    <asp:DropDownList Width="110px" ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"
                                                        ValidationGroup="1" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblCity" Text="City" runat="server"></asp:Label>
                                                    <asp:Label ID="Label30" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlcity"
                                                        ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                                </label>
                                                <label>
                                                    <asp:DropDownList Width="110px" ID="ddlcity" runat="server" ValidationGroup="1">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblZip" Text="Postal Code" runat="server"></asp:Label>
                                                    <asp:Label ID="Label31" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TextZip"
                                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <br />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Character"
                                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" Width="100px"
                                                        ControlToValidate="TextZip" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="TextZip" runat="server" Width="100px" ValidationGroup="1" onKeydown="return mask(event)"
                                                        onkeyup="return check(this,/[\\/!@#$%^'._&amp;*()&gt;+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span4',10)"
                                                        MaxLength="10"></asp:TextBox>
                                                    <asp:Label ID="Label39" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                    <span id="Span4" class="labelcount">10</span>
                                                    <asp:Label ID="Label11" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Panel ID="pnl" runat="server">
                                        <table width="100%">
                                            <tr valign="top">
                                                <td width="18%">
                                                    <label>
                                                        <asp:Label ID="lblEmail" Text="Email" runat="server"></asp:Label>
                                                        <asp:Label ID="Label33" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                                       
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server"
                                                        ControlToValidate="TextEmail" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                        <br />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextEmail"
                                                            ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator>
                                                        <br />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Character"
                                                            SetFocusOnError="True" ValidationExpression="^([_.@a-zA-Z0-9]*)" ControlToValidate="TextEmail"
                                                            ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator></label>
                                                    
                                                    <label>
                                                        <asp:TextBox ID="TextEmail" runat="server" Width="143px" ValidationGroup="1" onKeydown="return mask(event)"
                                                            onkeyup="return check(this,/[\\/!#$%^'&amp;*()&gt;+:;={}[]|\/]/g,/^[\._@a-zA-Z0-9\s]+$/,'Span5',30)"
                                                            MaxLength="30"></asp:TextBox>
                                                        <asp:Label ID="Label40" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                        <span id="Span5" class="labelcount">30</span>
                                                        <asp:Label ID="lblas" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @)"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="lblPhone1" Text="Phone 1" runat="server"></asp:Label>
                                                        <asp:Label ID="Label32" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator177" runat="server" ControlToValidate="TextPhone1"
                                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                        <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                                            Enabled="True" TargetControlID="TextPhone1" ValidChars="0147852369+-">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="TextPhone1" Width="143px" runat="server" ValidationGroup="1" onkeyup="return mak('Span6', 15, this)"
                                                            MaxLength="15"></asp:TextBox>
                                                        <asp:Label ID="Label41" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                        <span id="Span6" class="labelcount">15</span>
                                                        <asp:Label ID="Label6" CssClass="labelcount" runat="server" Text="(0-9 + -)"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="lblPhone2" runat="server" Text="Phone 2"></asp:Label><cc1:FilteredTextBoxExtender
                                                            ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="TextPhone2"
                                                            ValidChars="0147852369+-">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="TextPhone2" runat="server" Width="143px" ValidationGroup="1" onkeyup="return mak('Span7', 15, this)"
                                                            MaxLength="15"></asp:TextBox>
                                                        <asp:Label ID="Label42" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                        <span id="Span7" class="labelcount">15</span>
                                                        <asp:Label ID="Label7" CssClass="labelcount" runat="server" Text="(0-9 + -)"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td width="18%">
                                                    <label>
                                                        <asp:Label ID="lblFax" Text="Fax" runat="server"></asp:Label>
                                                        <%--<asp:Label ID="Label33" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="TextFax"
                                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                            TargetControlID="TextFax" ValidChars="0147852369+-">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="TextFax" runat="server" Width="143px" ValidationGroup="1" onkeyup="return mak('Span8', 15, this)"
                                                            MaxLength="15"></asp:TextBox>
                                                        <asp:Label ID="Label43" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                        <span id="Span8" class="labelcount">15</span>
                                                        <asp:Label ID="Label10" CssClass="labelcount" runat="server" Text="(0-9 + -)"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="lblTollFree1" Text="Toll Free 1" runat="server"></asp:Label>
                                                        <%--<asp:Label ID="Label34" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextTollFree1"
                                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                            TargetControlID="TextTollFree1" ValidChars="0147852369+-">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="TextTollFree1" runat="server" Width="143px" ValidationGroup="1"
                                                            onkeyup="return mak('Span9', 15, this)" MaxLength="15"></asp:TextBox>
                                                        <asp:Label ID="Label44" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                        <span id="Span9" class="labelcount">15</span>
                                                        <asp:Label ID="Label8" CssClass="labelcount" runat="server" Text="(0-9 + -)"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="lblTollFree2" Text="Toll Free 2" runat="server"></asp:Label><cc1:FilteredTextBoxExtender
                                                            ID="FilteredTextBoxExtender3" runat="server" Enabled="True" TargetControlID="TextTollFree2"
                                                            ValidChars="0147852369+-">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="TextTollFree2" runat="server" Width="143px" MaxLength="15" onkeyup="return mak('Span10', 15, this)"
                                                            ValidationGroup="1"></asp:TextBox>
                                                        <asp:Label ID="Label45" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                        <span id="Span10" class="labelcount">15</span>
                                                        <asp:Label ID="Label22" CssClass="labelcount" runat="server" Text="(0-9 + -)"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:CheckBox runat="server" ID="chkboldchat" AutoPostBack="true" TextAlign="Left"
                                        Text="Would you like to add Live Chat feature for your shopping cart for this business?"
                                        OnCheckedChanged="chkboldchat_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Panel ID="Panel4" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label9" Text="Our sofrware enables you to monitor and chat with visitors of the site."
                                                            runat="server"></asp:Label><br />
                                                        <asp:Label ID="Label14" Text="Our website has been tested to work well with boldchat.com - an online provider for chat software."
                                                            runat="server"></asp:Label><br />
                                                        <asp:Label ID="Label15" Text="In order to enable monitoring and chat for site visitors, you will have to register with boldchat.com."
                                                            runat="server"></asp:Label><br />
                                                        <asp:Label ID="Label16" Text="They have free software that is available for you to use."
                                                            runat="server"></asp:Label><br />
                                                        <div style="clear: both;">
                                                        </div>
                                                        <asp:Label ID="Label18" Text="After you have registered with boldchat.com and have downloaded their chat client to your pc, you will be able to generate code that you can upload here. "
                                                            runat="server"></asp:Label><br />
                                                        <asp:Label ID="Label17" Text="Once you have uploaded your code here, you will be able to monitor any visitor that comes to your shopping cart right from your pc.  If they have any questions for you, they will be able to initiate chat and it will notify you on your pc where you can respond."
                                                            runat="server"></asp:Label><br />
                                                        <div style="clear: both;">
                                                        </div>
                                                        <asp:Label ID="Label19" Text="Visit boldchat.com and click 'LIVE CHAT' in the top right corner to see how this works."
                                                            runat="server"></asp:Label><br />
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="lblUploadLive" Text="Upload Live chat Code (Code for live chat)" runat="server"></asp:Label>
                                                        <br />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Character"
                                                            SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)" ControlToValidate="TextLiveChatUrl"
                                                            ValidationGroup="1"></asp:RegularExpressionValidator><!--<input name="street1" type="text" value="Street" />-->
                                                        <br />
                                                        <asp:TextBox TextMode="MultiLine" ID="TextLiveChatUrl" runat="server" Width="300px"
                                                            onkeypress="return checktextboxmaxlength(this,300,event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\.a-zA-Z_0-9\s]+$/,'Span11',300)"
                                                            Height="105px" MaxLength="300"></asp:TextBox>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label20" Text="Visit BoldChat.com for more information." runat="server"></asp:Label><br />
                                                        <asp:Label ID="Label21" Text="Register for free!" runat="server"></asp:Label>
                                                        <div style="clear: both;">
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <asp:Label ID="Label69" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                        <span id="Span11" class="labelcount">300</span>
                                                        <asp:Label ID="Label2" runat="server" CssClass="labelcount" Text="(A-Z 0-9 . _)"></asp:Label>
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
                                <td colspan="3">
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" ValidationGroup="1"
                                        Text="Submit" CssClass="btnSubmit" />
                                    <asp:Button ID="imgbtnedit" runat="server" Text="Edit" OnClick="imgbtnedit_Click"
                                        Visible="False" CssClass="btnSubmit" />
                                    <asp:Button ID="imgbtnupdate" runat="server" OnClick="imgbtnupdate_Click" Visible="False"
                                        ValidationGroup="1" Text="Update" CssClass="btnSubmit" />
                                    <asp:Button ID="imgBtnCancelMainUpdate" runat="server" Text="Cancel" OnClick="imgBtnCancelMainUpdate_Click"
                                        CssClass="btnSubmit" />
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblListBusinessAdd" Text="List of Business Addresses" runat="server"></asp:Label></legend>
                    <label>
                        <asp:Label ID="lblSelectbyBusiness" Text="Select by Business" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlfilterstore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterstore_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <div style="float: right;">
                        <asp:Button ID="btncancel0" runat="server" CssClass="btnSubmit" CausesValidation="false"
                            OnClick="btncancel0_Click" Text="Printable Version" />
                        <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:GridView ID="GridView1" AutoGenerateColumns="False" GridLines="None" AllowPaging="true"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        DataKeyNames="CompanyWebsiteAddressMasterId" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                        OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                        runat="server" AllowSorting="True" OnSorting="GridView1_Sorting">
                        <Columns>
                            <asp:BoundField DataField="CompanyWebsiteAddressMasterId" HeaderText="CompanyWebsiteMasterId"
                                InsertVisible="False" ReadOnly="True" SortExpression="CompanyWebsiteMasterId"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true"
                                Visible="False" />
                            <asp:TemplateField HeaderText="Website" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" SortExpression="CompanyWebsiteMasterId">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownList2" runat="server">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AddressType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" SortExpression="AddressTypeMasterId">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownList4" runat="server">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropDownList3" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="Address1" SortExpression="Address1">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtadd1" runat="server" Text='<%# Bind("Address1") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblad1" runat="server" Text='<%# Bind("Address1") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="Address2" SortExpression="Address2">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtadd2" runat="server" Text='<%# Bind("Address2") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblad2" runat="server" Text='<%# Bind("Address2") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="City" SortExpression="City">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCity" runat="server" Text='<%# Bind("City") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="State" SortExpression="State">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtState" runat="server" Text='<%# Bind("State") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="Country" SortExpression="Country">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCountry" runat="server" Text='<%# Bind("Country") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCountry" runat="server" Text='<%# Bind("Country") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="Phone1" SortExpression="Phone1">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPhone1" runat="server" Text='<%# Bind("Phone1") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPhone1" runat="server" Text='<%# Bind("Phone1") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="Phone2" SortExpression="Phone2">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPhone2" runat="server" Text='<%# Bind("Phone2") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPhone2" runat="server" Text='<%# Bind("Phone2") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="TollFree1" SortExpression="TollFree1">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTollFree1" runat="server" Text='<%# Bind("TollFree1") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTollFree1" runat="server" Text='<%# Bind("TollFree1") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="TollFree2" SortExpression="TollFree2">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTollFree2" runat="server" Text='<%# Bind("TollFree2") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTollFree2" runat="server" Text='<%# Bind("TollFree2") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="Fax" SortExpression="Fax">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFax" runat="server" Text='<%# Bind("Fax") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFax" runat="server" Text='<%# Bind("Fax") %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="Zip" SortExpression="Zip">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtZip" runat="server" Text='<%# Bind("Zip") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblZip" runat="server" Text='<%# Bind("Zip") %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="Email" SortExpression="Email">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="LiveChatUrl" SortExpression="LiveChatUrl">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLiveChatUrl" runat="server" Text='<%# Bind("LiveChatUrl") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLiveChatUrl" runat="server" Text='<%# Bind("LiveChatUrl") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="LogoUrl" SortExpression="LogoUrl">
                                <EditItemTemplate>
                                    <asp:FileUpload ID="fileupload" runat="server"></asp:FileUpload>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLogoUrl" runat="server" Text='<%# Bind("LogoUrl") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="ContactPersonName" SortExpression="ContactPersonName">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtContactPersonName" runat="server" Text='<%# Bind("ContactPersonName") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblContactPersonName" runat="server" Text='<%# Bind("ContactPersonName") %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Font-Bold="true" HeaderText="ContactPersonDesignation" SortExpression="ContactPersonDesignation">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtContactPersonDesignation" runat="server" Text='<%# Bind("ContactPersonDesignation") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblContactPersonDesignation" runat="server" Text='<%# Bind("ContactPersonDesignation") %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" HeaderText="Edit" />
                            <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" />
                        </Columns>
                    </asp:GridView>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="text-align: center; font-weight: bold; font-style: italic">
                                            <tr align="center">
                                                <td align="center">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Size="20px" ForeColor="Black" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center">
                                                    <asp:Label ID="Label46" Font-Size="20px" runat="server" ForeColor="Black" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusiness" Font-Size="20px" runat="server" ForeColor="Black"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label4" runat="server" ForeColor="Black" Text="List of Business Addresses "></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" OnRowCommand="GridView2_RowCommand"
                                        DataKeyNames="CompanyWebsiteAddressMasterId" CssClass="mGrid" Width="100%" AllowSorting="True"
                                        OnSorting="GridView2_Sorting" EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblid" runat="server" Text='<%# Bind("CompanyWebsiteAddressMasterId") %>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblid1" runat="server" Text='<%# Bind("CompanyWebsiteAddressMasterId") %>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="13%" SortExpression="Sitename">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwebsite" Text='<%# Bind("Sitename") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address Type" HeaderStyle-HorizontalAlign="Left" SortExpression="Name"
                                                ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladdtype" Text='<%# Bind("Name") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address1" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="true"
                                                SortExpression="Address1" ItemStyle-Width="18%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladd1" Text='<%# Bind("Address1") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address2" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="true"
                                                SortExpression="Address2" ItemStyle-Width="18%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladd2" Text='<%# Bind("Address2") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="City" HeaderStyle-HorizontalAlign="Left" SortExpression="CityName"
                                                ItemStyle-Width="9%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcity" Text='<%# Bind("CityName") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Left" SortExpression="StateName"
                                                ItemStyle-Width="9%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstate" Text='<%# Bind("StateName") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Country" HeaderStyle-HorizontalAlign="Left" SortExpression="CountryName"
                                                ItemStyle-Width="9%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcountry" Text='<%# Bind("CountryName") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnedit" runat="server" CommandName="editview" CommandArgument='<%#Bind("CompanyWebsiteAddressMasterId") %>'
                                                        ImageUrl="~/Account/images/edit.gif" ToolTip="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:ButtonField CommandName="editview" ButtonType="Image" ImageUrl="~/Account/images/edit.gif"
                                        HeaderStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif"
                                        Text="Edit" HeaderText="Edit" />--%>
                                            <asp:ButtonField CommandName="del" Text="Delete" Visible="false" HeaderText="Delete" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel2" runat="server" BackColor="#CCCCCC" BorderStyle="Outset" Width="300px">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label35" runat="server">Confirmation....</asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label3" runat="server">&quot;You are about to create new location for the company, Please note that you will note be able to delete it, Are you sure you wish to create new Company Address? &quot;</asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="ImageButton4" CssClass="btnSubmit" runat="server" Text="Submit" OnClick="ImageButton51_Click" />
                                <asp:Button ID="ImageButton7" CssClass="btnSubmit" runat="server" Text="Cancel" OnClick="ImageButton6_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button11" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                    ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel2" TargetControlID="Button11">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--end of right content-->
    <%--<td width="10%" >
                                        <label>
                                            <asp:DropDownList Width="120px" ID="ddlcountry" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" ValidationGroup="1">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td width="8%">
                                        <label>
                                            <asp:Label ID="lblState" Text="State" runat="server"></asp:Label>
                                            <asp:Label ID="Label29" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ControlToValidate="ddlstate" ErrorMessage="*"
                                                ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td width="10%">
                                        <label>
                                            <asp:DropDownList Width="120px" ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"
                                                ValidationGroup="1" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td width="7%">
                                        <label>
                                            <asp:Label ID="lblCity" Text="City" runat="server"></asp:Label>
                                            <asp:Label ID="Label30" Text="*"  CssClass="labelstar" runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ControlToValidate="ddlcity" ErrorMessage="*"
                                                ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td width="10%">
                                        <label>
                                            <asp:DropDownList Width="120px" ID="ddlcity" runat="server" ValidationGroup="1">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td width="35%" >
                                        <label>
                                            <asp:Label ID="lblZip" Text="Postal Code" runat="server"></asp:Label>
                                            <asp:Label ID="Label31" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator11" runat="server" ControlToValidate="TextZip" ErrorMessage="*"
                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Character"
                                                     SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                    ControlToValidate="TextZip" ValidationGroup="1"></asp:RegularExpressionValidator>
                                               
                                        </label>
                                        <label>
                                            <asp:TextBox ID="TextZip" runat="server" Width="110px" ValidationGroup="1"
                                            onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'._&amp;*()&gt;+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span4',10)" MaxLength="10"></asp:TextBox>
                                            <asp:Label ID="Label39"  CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                           <span id="Span4" class="labelcount">10</span>
                                            <asp:Label ID="Label11" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                                        </label>
                                      
                                    </td>--%>
    <div style="clear: both;">
    </div>
    <div style="clear: both;">
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
