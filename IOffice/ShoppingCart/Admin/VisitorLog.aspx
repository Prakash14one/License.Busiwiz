<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="VisitorLog.aspx.cs"
    Inherits="ShoppingCart_Admin_Master_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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


        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
            }
        }
        function mask(evt) {

            if (evt.keyCode == 13) {


            }


            if (evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


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

        function Button3_onclick() {

        }

    </script>
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div>
            </div>
            <asp:Panel ID="paneluper" runat="server">
                <fieldset>
                    <legend>Visitor Form</legend>
                    <div style="clear: both;">
                    </div>
                    <div class="products_box">
                        <div style="padding-left: 2%">
                            <table>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblrequest" runat="server" Text="Request For "></asp:Label>
                                        </label>
                                        <label>
                                            <asp:DropDownList ID="drop_request" runat="server" OnSelectedIndexChanged="drop_request_SelectedIndexChanged"
                                                Height="21px" Width="150px" AutoPostBack="True">
                                                <asp:ListItem Value="1" Selected="True">Entry</asp:ListItem>
                                                <asp:ListItem Value="2">Exit</asp:ListItem>
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel4" runat="server">
                                            <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                                                OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" ValidationGroup="1"
                                                                AutoPostBack="True" EnableTheming="True">
                                                                <asp:ListItem Selected="True" Value="1">New Visitor</asp:ListItem>
                                                                <asp:ListItem Value="2">Existing Visitor</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="Panel10" runat="server">
                            <fieldset>
                                <asp:Panel ID="Panel9" runat="server">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" EnableTheming="True" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Selected="True">New User of Existing Company</asp:ListItem>
                                                    <asp:ListItem Value="1">New User of New Company</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </fieldset>
                        </asp:Panel>
                        <asp:Panel ID="Panel6" runat="server">
                            <fieldset>
                                <asp:Panel ID="Panel7" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td width="25%">
                                                <label>
                                                    <asp:Label ID="Label1" runat="server" Text=" Select Visitor By"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddselectvisitor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddselectvisitor_SelectedIndexChanged"
                                                        Width="150px">
                                                        <asp:ListItem Value="1" Selected="True">Visitor ID</asp:ListItem>
                                                        <asp:ListItem Value="2">Visitor Name</asp:ListItem>
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%">
                                        <tr>
                                            <td width="25%">
                                                <label>
                                                    <asp:Label ID="Label13" runat="server" Text="Visitor ID"></asp:Label>
                                                    <asp:Label ID="Label22" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                                        ControlToValidate="txtvisiotrID" SetFocusOnError="True" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td style="width: 282px">
                                                <label>
                                                    <asp:TextBox ID="txtvisiotrID" runat="server" MaxLength="6" onKeydown="return mask(event)"
                                                        onkeyup="return check(this,/[\\/!@#$%^'&amp;*()&gt;+;={}[]|\/]/g,/^[\_,?a-zA-Z.0-9\s]+$/,'Span4',6)"
                                                        Width="151px" AutoPostBack="True" ValidationGroup="5"></asp:TextBox>
                                                </label>
                                                <label id="Label14" runat="server">
                                                    <asp:Label ID="Label15" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                    <span id="Span4" cssclass="labelcount">6</span>
                                                    <asp:Label ID="Label16" runat="server" CssClass="labelcount">(0-9)</asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnGo" runat="server" Text="GO" ValidationGroup="5" OnClick="btnGo_Click"
                                                    CssClass="btnSubmit" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%">
                                                <label>
                                                    <asp:Label ID="Label6" runat="server" Text="Visitor Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 282px">
                                                <label>
                                                    <asp:TextBox ID="txtname" runat="server" MaxLength="60" onKeydown="return mask(event)"
                                                        onkeyup="return check(this,/[\\/!@#$%^'&amp;*()&gt;+;={}[]|\/]/g,/^[\_,?a-zA-Z.0-9\s]+$/,'Span5',60)"
                                                        Width="151px" AutoPostBack="True" ValidationGroup="3" OnTextChanged="txtname_TextChanged"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:Label runat="server" ID="Label11" Text="Max " CssClass="labelcount"></asp:Label>
                                                    <span id="Span5" cssclass="labelcount">60</span>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel8" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td width="20%">
                                                <label>
                                                    <asp:Label ID="Label18" runat="server" Text="Party Name"></asp:Label>
                                                    <asp:Label ID="Label23" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                        ControlToValidate="ddname" SetFocusOnError="True" ValidationGroup="9" InitialValue="0"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td width="25%">
                                                <label>
                                                    <asp:DropDownList ID="ddname" runat="server" OnSelectedIndexChanged="ddname_SelectedIndexChanged"
                                                        ValidationGroup="9" AutoPostBack="True" Width="250px">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Button ID="btngoo" runat="server" Text="Go" ValidationGroup="9" OnClick="btngoo_Click"
                                                        CssClass="btnSubmit" />
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </fieldset>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                            <fieldset>
                                <table width="100%">
                                    <tr>
                                        <td align="right" colspan="4">
                                            <asp:Button ID="btnUpdate" runat="server" CssClass="btnSubmit" Text="Update" OnClick="btnUpdate_Click"
                                                ValidationGroup="6" />
                                            <asp:Button ID="btnEdit" runat="server" CssClass="btnSubmit" OnClick="btnupdate_Click"
                                                Text="Edit" />
                                        </td>
                                    </tr>
                                    <asp:Panel ID="Panel18" runat="server" Visible="false">
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lbluserid" runat="server" Text="Visitor ID"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="userid" runat="server" Text="User ID"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <asp:Panel ID="Panel15" runat="server" Visible="false">
                                            <td style="width: 20%">
                                                <label>
                                                    <asp:Label ID="Label31" runat="server" Text="Party Name"></asp:Label>
                                                    <asp:Label ID="Label35" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="TextBox1"
                                                        ErrorMessage="*" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="TextBox1"
                                                        ErrorMessage="*" ValidationGroup="6">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                        ControlToValidate="TextBox1" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:TextBox ID="TextBox1" runat="server" Width="220px" MaxLength="60" onKeydown="return mask(event)"
                                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span7',60)"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label32" runat="server" CssClass="labelcount" Text="Max "></asp:Label>
                                                    <span id="Span7" cssclass="labelcount">60</span>
                                                    <asp:Label ID="Label33" runat="server" CssClass="labelcount" Text="(A-Z 0-9)"></asp:Label>
                                                </label>
                                            </td>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel16" runat="server" Visible="false">
                                            <td style="width: 20%">
                                                <label>
                                                    <asp:Label ID="Label34" runat="server" Text="Select Party"></asp:Label>
                                                    <asp:Label ID="Label36" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddparty"
                                                        InitialValue="0" ErrorMessage="*" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddparty"
                                                        InitialValue="0" ErrorMessage="*" ValidationGroup="6">*</asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:DropDownList runat="server" ID="ddparty" Width="220px">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </asp:Panel>
                                        <td style="width: 20%">
                                            <label>
                                                <asp:Label ID="lblEmailid" runat="server" Text="Email ID "></asp:Label>
                                                <%--<asp:Label ID="Label28" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtemailid"
                                                    ErrorMessage="RequiredFieldValidator" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtemailid"
                                                    ErrorMessage="RequiredFieldValidator" ValidationGroup="6">*</asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtemailid"
                                                    Display="Dynamic" ErrorMessage="Invalid Email ID" ValidationExpression="\w+([_.]\w+)*@\w+([_.]\w+)*\.\w+([_.]\w+)*"
                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td style="width: 30%">
                                            <asp:Panel runat="server" ID="panel14">
                                                <label>
                                                    <asp:TextBox ID="txtemailid" runat="server" Width="220px" onKeydown="return mask(event)"
                                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\@ _,?a-zA-Z.0-9\s]+$/,'Span6',50)"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label44" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                    <span id="Span6" cssclass="labelcount">50</span>
                                                </label>
                                            </asp:Panel>
                                            <label>
                                                <asp:Label ID="eemailid" runat="server" Text="Label4"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                <asp:Label ID="lblfname" runat="server" Text="First Name "></asp:Label>
                                                <asp:Label ID="Label25" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtfname"
                                                    ErrorMessage="RequiredFieldValidator" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtfname"
                                                    ErrorMessage="RequiredFieldValidator" ValidationGroup="6">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                    ControlToValidate="txtfname" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td style="width: 30%">
                                            <asp:Panel runat="server" ID="panel11">
                                                <label>
                                                    <asp:TextBox ID="txtfname" runat="server" Width="220px" MaxLength="60" onKeydown="return mask(event)"
                                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span1',60)"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label41" runat="server" CssClass="labelcount" Text="Max "></asp:Label>
                                                    <span id="Span1" cssclass="labelcount">60</span>
                                                    <asp:Label ID="Label5" runat="server" CssClass="labelcount" Text="(A-Z 0-9)"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                            <label>
                                                <asp:Label ID="efname" runat="server" Text="Label1"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 20%">
                                            <label>
                                                <asp:Label ID="lbllastname" runat="server" Text="Last Name"></asp:Label>
                                                <asp:Label ID="Label26" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtlastname"
                                                    ErrorMessage="RequiredFieldValidator" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtlastname"
                                                    ErrorMessage="RequiredFieldValidator" ValidationGroup="6">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                    ControlToValidate="txtlastname" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td style="width: 30%">
                                            <asp:Panel runat="server" ID="panel12">
                                                <label>
                                                    <asp:TextBox ID="txtlastname" runat="server" Width="220px" MaxLength="60" onKeydown="return mask(event)"
                                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span2',60)"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:Label runat="server" ID="Label8" Text="Max " CssClass="labelcount"></asp:Label>
                                                    <span id="Span2" cssclass="labelcount">60</span>
                                                    <asp:Label ID="Label9" runat="server" Text="(A-Z 0-9)" CssClass="labelcount"></asp:Label>
                                                </label>
                                            </asp:Panel>
                                            <label>
                                                <asp:Label ID="elname" runat="server" Text="Label2"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                <asp:Label ID="lblcno" runat="server" Text="Mobile Number"></asp:Label>
                                                <asp:Label ID="Label27" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtphone"
                                                    ErrorMessage="*" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtphone"
                                                    ErrorMessage="*" ValidationGroup="6">*</asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td style="width: 30%">
                                            <asp:Panel runat="server" ID="panel13">
                                                <label>
                                                    <asp:TextBox ID="txtphone" runat="server" MaxLength="15" onkeyup="return mak('Span3',15, this)"
                                                        Width="220px"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                        TargetControlID="txtphone" ValidChars="0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label17" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                    <span id="Span3" cssclass="labelcount">15</span>
                                                    <asp:Label ID="Label40" runat="server" CssClass="labelcount">(0-9)</asp:Label>
                                                </label>
                                            </asp:Panel>
                                            <label>
                                                <asp:Label ID="ephoneno" runat="server" Text="Label3"></asp:Label>
                                            </label>
                                        </td>
                                        <asp:Panel ID="pnlmobcar" runat="server">
                                            <td style="width: 20%">
                                                <label>
                                                    <asp:Label ID="Label37" runat="server" Text="Mobile Carrier"></asp:Label>
                                            <%--        <asp:Label ID="Label38" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlcarrier"
                                                        InitialValue="0" ErrorMessage="RequiredFieldValidator" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlcarrier"
                                                        InitialValue="0" ErrorMessage="RequiredFieldValidator" ValidationGroup="6">*</asp:RequiredFieldValidator>--%>
                                                </label>
                                            </td>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:DropDownList ID="ddlcarrier" runat="server" Width="220px">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel19" runat="server" Visible="false">
                                            <td style="width: 20%">
                                                <label>
                                                    <asp:Label ID="Label49" runat="server" Text="Mobile Carrier"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:Label ID="Label53" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </asp:Panel>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                <asp:Label ID="Label39" runat="server" Text="Phone Number"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 30%">
                                            <asp:Panel runat="server" ID="panel17">
                                                <label>
                                                    <asp:TextBox ID="TextBox2" runat="server" MaxLength="15" onkeyup="return mak('Span8',15, this)"
                                                        Width="220px"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                        TargetControlID="TextBox2" ValidChars="0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label43" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                    <span id="Span8" cssclass="labelcount">15</span>
                                                    <asp:Label ID="Label46" runat="server" CssClass="labelcount">(0-9)</asp:Label>
                                                </label>
                                            </asp:Panel>
                                            <label>
                                                <asp:Label ID="Label47" runat="server" Text="Label3" Visible="false"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 20%">
                                            <label>
                                                <asp:Label ID="Label48" runat="server" Text="Ext"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 30%">
                                            <label>
                                                <asp:TextBox ID="TextBox3" runat="server" MaxLength="15" Width="120px"></asp:TextBox>
                                                <asp:Label ID="Label42" runat="server" Text="Label3" Visible="false"></asp:Label>
                                            </label>
                                            <label>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                    TargetControlID="TextBox3" ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>
                        <asp:Panel ID="Panel3" runat="server" Visible="true">
                            <fieldset>
                                <table width="100%">
                                    <tr>
                                        <td align="25%">
                                            <label>
                                                <asp:Label ID="lblmeeting" runat="server" Text="Meeting with"></asp:Label>
                                                <asp:Label ID="Label54" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label4" runat="server" Text="Business Name"></asp:Label>
                                                <asp:DropDownList ID="dropbusiness" runat="server" Height="22px" Width="220px" OnSelectedIndexChanged="dropbusiness_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label7" runat="server" Text="Employee Name"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                                                    ControlToValidate="dropdepartment" InitialValue="0" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="dropdepartment" runat="server" Width="250px" OnSelectedIndexChanged="dropdepartment_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                            <asp:Panel ID="panelwdddffdf" runat="server" Visible="false">
                                                <label>
                                                    <asp:Label ID="Label10" runat="server" Text="Employee Name"></asp:Label>
                                                    <asp:DropDownList ID="dropemployee" runat="server" Height="22px" Width="220px" OnSelectedIndexChanged="dropemployee_SelectedIndexChanged"
                                                        ValidationGroup="1">
                                                    </asp:DropDownList>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="25%" valign="top">
                                            <label>
                                                <asp:Label ID="lblpurpose" runat="server" Text="Purpose of Meeting"></asp:Label>
                                             <%--   <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                                    ControlToValidate="txtpurpose" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtpurpose" runat="server" TextMode="MultiLine" Width="360px" Height="60px"
                                                    nKeydown="return mask(event)" onkeypress="return checktextboxmaxlength(this,1500,event)"
                                                    onkeyup="return check(this,/[\\/!@#$%^'&amp;*()&gt;+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div2',1500)"
                                                    ValidationGroup="1"></asp:TextBox>
                                                <asp:Label runat="server" ID="Label12" Text="Max " CssClass="labelcount"></asp:Label>
                                                <span id="div2" cssclass="labelcount">1500</span>
                                                <asp:Label ID="Label24" CssClass="labelcount" runat="server" Text="(A-Z 0-9 . , ? _)"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblentrytime" runat="server" Text="Entry Time"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label50" runat="server" Text=""></asp:Label>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label2" runat="server" Text="Label2"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Would you like to print your Visitor Pass ?
                                            </label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btnsubmit_Click1"
                                                ValidationGroup="1" />
                                        </td>
                                    </tr>
                                </table>
                        </asp:Panel>
                </fieldset>
                <asp:Panel ID="Panel5" runat="server" Visible="false">
                    <fieldset>
                        <table width="100%">
                            <tr>
                                <td width="25%">
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Select Visitor"></asp:Label>
                                        <asp:Label ID="Label29" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="dropvisitor"
                                            ErrorMessage="*" InitialValue="0" ValidationGroup="4">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="dropvisitor" runat="server" Height="22px" Width="220px" OnSelectedIndexChanged="dropvisitor_SelectedIndexChanged"
                                            ValidationGroup="4" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <asp:Panel ID="pnlsds" runat="server" Visible="false">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label21" runat="server" Text="Your Intime is"></asp:Label></label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label52" runat="server" Text=""></asp:Label>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label45" runat="server" Text=""></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblexittime" runat="server" Text="Exit Time"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label51" runat="server" Text=""></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblsystemtime" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:Button ID="btn_exitsubmit" runat="server" Text="Submit" OnClick="btn_exitsubmit_Click"
                                            ValidationGroup="4" CssClass="btnSubmit" />
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    </fieldset>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="printpass" runat="server" Visible="false">
                <fieldset>
                    <div style="float: right">
                        <input id="Button3" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print Visitor Pass" />
                        <asp:Button ID="Button1" runat="server" Text="Print Visitor Pass" CssClass="btnSubmit"
                            Visible="false" OnClick="Button1_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div align="center">
                        <asp:Panel ID="pnlgrid" runat="server" Width="559.370079px" Height="396.850394px">
                            <table width="100%" align="center">
                                <tr>
                                    <td>
                                        <div id="mydiv" class="closed">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 25%" valign="top" align="left">
                                                        <asp:Image ID="Img1" runat="server" Height="80px" Width="100px" Visible="false"  />
                                                    </td>
                                                    <td align="right">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblcompanyname" Font-Size="14px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lbladdress1" Font-Size="14px" Font-Bold="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <%--    <tr>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lbltollfreeno" Font-Size="14px" Font-Bold="false"></asp:Label>
                                                                </td>
                                                            </tr>--%>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblcs" Font-Size="14px" Font-Bold="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblphoneno" Font-Size="14px" Font-Bold="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblemail" Font-Size="14px" Font-Bold="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="clear: both;">
                                            </div>
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="3" align="center">
                                                        <asp:Label ID="Label20" runat="server" Text="Visitor Pass" Font-Size="22px" ForeColor="#416271"
                                                            Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                    </td>
                                                    <td style="width: 25%" align="left">
                                                        Entry Time
                                                    </td>
                                                    <td align="left" style="width: 50%">
                                                        :
                                                        <asp:Label ID="lblentrytt" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                    </td>
                                                    <td style="width: 25%" align="left">
                                                        First Name
                                                    </td>
                                                    <td align="left" style="width: 50%">
                                                        :
                                                        <asp:Label ID="lblfirsttt" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                    </td>
                                                    <td style="width: 25%" align="left">
                                                        Last Name
                                                    </td>
                                                    <td align="left" style="width: 50%">
                                                        :
                                                        <asp:Label ID="lbllasttt" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                    </td>
                                                    <td style="width: 25%" align="left">
                                                        Contact No
                                                    </td>
                                                    <td align="left" style="width: 50%">
                                                        :
                                                        <asp:Label ID="lblcontactnoo" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                    </td>
                                                    <td style="width: 25%" align="left">
                                                        Meeting with
                                                    </td>
                                                    <td align="left" style="width: 50%">
                                                        :
                                                        <asp:Label ID="lblmeetingwith" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                    </td>
                                                    <td style="width: 25%" align="left">
                                                        Purpose of Meeting
                                                    </td>
                                                    <td align="left" style="width: 50%">
                                                        :
                                                        <asp:Label ID="Label30" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div style="padding-left: 50%">
                        <asp:Button ID="Button2" runat="server" Text="Close" CssClass="btnSubmit" OnClick="Button2_Click" />
                    </div>
                </fieldset>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
