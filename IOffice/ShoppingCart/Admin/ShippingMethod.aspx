<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="ShippingMethod.aspx.cs" Inherits="ShoppingCart_Admin_ShippingMethod"
    Title="Untitled Page" %>

<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UControlWizardpanel.ascx" TagName="pnl"
    TagPrefix="pnl" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
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

        function CallPrint1(strid) {
            var prtContent = document.getElementById('<%= pnlreal.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

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
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 1%">
                <label>
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Label"></asp:Label>
                </label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend></legend>
                    <table>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Business Name"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged"
                                        Width="150px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Shipping Method"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged2">
                                        <asp:ListItem Value="0" Selected="True">Ship through shipping companies 
                                providing online quotes for the region (real time quote).</asp:ListItem>
                                        <asp:ListItem Value="1">Flat shipping rate based on order destination and weight 
                                (not a real time quote).</asp:ListItem>
                                    </asp:RadioButtonList>
                                </label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <asp:Panel ID="pnl1" runat="server" Visible="false" Width="100%">
                    <fieldset>
                        <legend></legend>
                        <div style="float: right;">
                            <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button4_Click" />
                            <input id="Button2" runat="server" onclick="document.getElementById('Div2').className='open';javascript:CallPrint1('divPrint');document.getElementById('Div2').className='closed';"
                                type="button" value="Print" class="btnSubmit" visible="False" />
                        </div>
                        <asp:Panel ID="pnlreal" runat="server" Width="100%">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div id="Div2" class="closed">
                                            <table width="100%">
                                                <tr align="center">
                                                    <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                        <asp:Label ID="lblCn" runat="server" Font-Italic="True" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                        <asp:Label ID="Label43" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                        <asp:Label ID="lblbn" runat="server" Font-Italic="True" ForeColor="Black">
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="LSS" runat="server" Text="List of Companies providing real time online quotes for your business in "></asp:Label><asp:Label
                                                ID="lblco" runat="server"></asp:Label><asp:Label ID="lblst" runat="server"></asp:Label>
                                            <%--  <asp:Label ID="lbl" runat="server" Text=" of your business."></asp:Label>--%>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdTempData" runat="server" AutoGenerateColumns="False" OnRowCommand="grdTempData_RowCommand"
                                            OnRowDeleting="grdTempData_RowDeleting" DataKeyNames="ShippingManagerID" OnSorting="grdTempData_Sorting"
                                            EmptyDataText="No Record Found." Width="100%" CssClass="mGrid" HeaderStyle-HorizontalAlign="Left"
                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ShippingAccountInfo" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Text='<%#Bind("ShippingAccountInfo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shipping Company Name" HeaderStyle-Width="16%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblshipmethodname" runat="server" Text='<%#Bind("CarrierMethod") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Select for your business " HeaderStyle-Width="12%"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkbus" runat="server" Checked='<%#Bind("chk") %>' AutoPostBack="true"
                                                            OnCheckedChanged="chkedit_chachedChanged" />
                                                        <%--  <asp:Label ID="lblwhid" runat="server" Visible="false" 
                                                Text='<%#Bind("Whid") %>'  ></asp:Label>
                                                        --%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Name" HeaderStyle-Width="22%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%-- <asp:Label ID="txtUsername" runat="server"  Text='<%#Bind("UserID") %>'  ></asp:Label>--%>
                                                        <asp:TextBox ID="lblUsername" runat="server" Text='<%#Bind("UserID") %>' MaxLength="30"
                                                            Enabled="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="Reqfieldusername" runat="server" ControlToValidate="lblUsername"
                                                            Visible="false" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="ab"
                                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s_.]*)"
                                                            ControlToValidate="lblUsername" ValidationGroup="ab"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Password" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="11%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblppp" runat="server" Text='<%#Bind("Password") %>' Visible="false"></asp:Label>
                                                        <asp:TextBox ID="lblpassword" runat="server" TextMode="Password" Width="90px" MaxLength="15"
                                                            Enabled="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="Reqfieldpass" runat="server" ControlToValidate="lblpassword"
                                                            Visible="false" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="ab"
                                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="REG13" runat="server" ErrorMessage="Invalid Character"
                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                            ControlToValidate="lblpassword" ValidationGroup="ab"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Merchant Key" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="11%">
                                                    <ItemTemplate>
                                                        <%-- <asp:Label ID="txtlblmerchant" runat="server"  Text='<%#Bind("MerchantId") %>'></asp:Label>
                                                        --%>
                                                        <asp:TextBox ID="lblmerchant" runat="server" Width="90px" Text='<%#Bind("MerchantId") %>'
                                                            MaxLength="20" Enabled="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="ReqfieldMerc" runat="server" ControlToValidate="lblmerchant"
                                                            Visible="false" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="ab"
                                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="REG12" runat="server" ErrorMessage="Invalid Character"
                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s_.]*)"
                                                            ControlToValidate="lblmerchant" ValidationGroup="ab"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Account No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="11%">
                                                    <ItemTemplate>
                                                        <%-- <asp:Label ID="txtaccname" runat="server"  Text='<%#Bind("AccountNo") %>'></asp:Label>--%>
                                                        <asp:TextBox ID="lblaccname" runat="server" Text='<%#Bind("AccountNo") %>' Width="90px"
                                                            onkeypress="return RealNumWithDecimal(this,event,2);" MaxLength="20" Enabled="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="ReqfieldAcc" runat="server" ControlToValidate="lblaccname"
                                                            Visible="false" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="ab"
                                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularEionValior3" ControlToValidate="lblaccname"
                                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"
                                                            ValidationGroup="ab" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Access Key" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="11%">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="txtaccesskey" runat="server"  Text='<%#Bind("AccessKey") %>'></asp:Label>--%>
                                                        <asp:TextBox ID="lblaccesskey" runat="server" Text='<%#Bind("AccessKey") %>' Width="90px"
                                                            MaxLength="20" Enabled="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="ReqfieldAccesskey" runat="server" Visible="false"
                                                            ControlToValidate="lblaccesskey" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="ab"
                                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RE4G12" runat="server" ErrorMessage="Invalid Character"
                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s_.]*)"
                                                            ControlToValidate="lblaccesskey" ValidationGroup="ab"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnadd" Text="Save" runat="server" Enabled="false" ValidationGroup="ab"
                                                            OnClick="btnAddFreeShippingRule_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowEditButton="True" Visible="false" HeaderText="Edit" ItemStyle-ForeColor="#416271">
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle ForeColor="#416271" />
                                                </asp:CommandField>
                                                <asp:CommandField ShowDeleteButton="True" Visible="false" HeaderText="Delete" ItemStyle-ForeColor="#416271">
                                                </asp:CommandField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset></asp:Panel>
                <asp:Panel ID="pnlop2" runat="server" Visible="false">
                    <asp:Panel ID="masterpnl" runat="server" Visible="false">
                        <fieldset>
                            <legend>
                                <asp:Label ID="lbllegend" runat="server" Text="" Font-Bold="True" Visible="False"></asp:Label>
                            </legend>
                            <%--  <div style="float: right;">
                            <asp:Button ID="btnnew" runat="server" Text="Add New Fixed Shipping Rate" CssClass="btnSubmit"
                                OnClick="btnnew_Click" />
                        </div>--%>
                            <div style="clear: both;">
                            </div>
                            <legend>
                                <asp:Label ID="Label5" runat="server" Text="A. Choose a criteria for setting fixed rates. This can either be by city, or by range of zip/postal codes. "></asp:Label>
                            </legend>
                            <div style="clear: both;">
                            </div>
                            <label>
                                <asp:RadioButton ID="rdcountry" runat="server" GroupName="AA" Text="Set fixed rate by country/state/city"
                                    AutoPostBack="True" Checked="true" OnCheckedChanged="rdcountry_CheckedChanged" />
                            </label>
                            <label>
                                <asp:RadioButton ID="rdzippostal" runat="server" GroupName="AA" Text="Set fixed rate by country/Zip/Postal Codes"
                                    AutoPostBack="True" OnCheckedChanged="rdcountry_CheckedChanged" />
                            </label>
                            <div style="clear: both;">
                            </div>
                            <asp:Panel ID="pnlcountry" runat="server" Visible="true">
                                <label>
                                    <asp:Label ID="Label11" runat="server" Text="Select Country"></asp:Label>
                                    <%--   </label>
                      <label>--%>
                                    <asp:DropDownList ID="ddlcountry" runat="server" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label12" runat="server" Text="Select State"></asp:Label>
                                    <%-- </label>
                    
                    <label>--%>
                                    <asp:DropDownList ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label13" runat="server" Text="Select City"></asp:Label>
                                    <%--  </label>
                     <label>--%>
                                    <asp:DropDownList ID="ddlcity" runat="server">
                                    </asp:DropDownList>
                                </label>
                            </asp:Panel>
                            <div style="clear: both;">
                            </div>
                            <asp:Panel ID="pnlcounpostal" runat="server" Visible="false">
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="Select Country"></asp:Label>
                                    <%-- </label>
                    <label>--%>
                                    <asp:DropDownList ID="ddlpostalcountry" runat="server">
                                    </asp:DropDownList>
                                </label>
                                <div style="clear: both;">
                                </div>
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="For Zip/Postal Codes"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="From"></asp:Label><asp:Label ID="Label14"
                                        runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartzip"
                                        ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                        ControlToValidate="txtStartzip" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtStartzip" runat="server" MaxLength="10" Text="0" onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s ]+$/,'div1',10)"
                                        Width="100px"></asp:TextBox>
                                    <asp:Label ID="Label40" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="div1" class="labelcount">10</span>
                                    <asp:Label ID="lbljshg" runat="server" Text="(A-Z 0-9)" CssClass="labelcount"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="Label15" runat="server" Text="To"></asp:Label><asp:Label ID="Label10"
                                        runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEndzip"
                                        ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                        ControlToValidate="txtEndzip" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtEndzip" runat="server" MaxLength="10" Text="0" onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s ]+$/,'div11',10)"
                                        Width="100px"></asp:TextBox>
                                    <asp:Label ID="Label41" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="div11" class="labelcount">10</span>
                                    <asp:Label ID="Label9" runat="server" Text="(A-Z 0-9)" CssClass="labelcount"></asp:Label>
                                </label>
                            </asp:Panel>
                            <div style="clear: both;">
                                <br />
                            </div>
                            <legend>
                                <asp:Label ID="Label47" runat="server" Text="B. Set rates by weight, volume, or by order"></asp:Label>
                            </legend>
                            <div style="clear: both;">
                            </div>
                            <label>
                                <asp:RadioButton ID="rdrate" runat="server" GroupName="AC" Text="For Weight" AutoPostBack="True"
                                    OnCheckedChanged="RadioButton1_CheckedChanged" Checked="true" />
                            </label>
                            <label>
                                <asp:RadioButton ID="rdratevalume" runat="server" GroupName="AC" Text="For Volume"
                                    AutoPostBack="True" OnCheckedChanged="RadioButton1_CheckedChanged" />
                            </label>
                            <label>
                                <asp:RadioButton ID="rdperorder" runat="server" GroupName="AC" Text="By Order" AutoPostBack="True"
                                    OnCheckedChanged="RadioButton1_CheckedChanged" />
                            </label>
                            <div style="clear: both;">
                            </div>
                            <asp:Panel ID="pnlforweight" runat="server" Visible="true">
                                <label>
                                    <asp:Label ID="Label28" runat="server" Text="Minimum Weight"></asp:Label>
                                    <asp:Label ID="Label26" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMinWeight"
                                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <%--</label>
                      <label>--%>
                                    <asp:TextBox ID="txtMinWeight" runat="server" MaxLength="10" Width="100px" Text="0"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender8" runat="server"
                                        Enabled="True" TargetControlID="txtMinWeight" ValidChars="0147852369">
                                    </cc1:FilteredTextBoxExtender>
                                </label>
                                <label>
                                    <asp:Label ID="Label17" runat="server" Text="Maximum Weight"></asp:Label>
                                    <asp:Label ID="Label16" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldVali" runat="server" ControlToValidate="txtMaxWeight"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <%-- </label>
                       
                        <label>--%>
                                    <asp:TextBox ID="txtMaxWeight" runat="server" MaxLength="10" Width="100px" Text="0"></asp:TextBox>
                                    <asp:CompareValidator ID="com1" runat="server" Display="Dynamic" SetFocusOnError="true"
                                        ControlToCompare="txtMinWeight" ControlToValidate="txtMaxWeight" Type="Integer"
                                        Operator="GreaterThan" ValidationGroup="1" ErrorMessage="Larger then Min Weight"></asp:CompareValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" Enabled="True"
                                        TargetControlID="txtMaxWeight" ValidChars="0147852369">
                                    </cc1:FilteredTextBoxExtender>
                                </label>
                                <label>
                                    <asp:Label ID="Label46" runat="server" Text="Per"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="Label18" runat="server" Text="Unit"></asp:Label>
                                    <%--</label> 
                        <label>--%>
                                    <asp:DropDownList ID="ddlUnitType" runat="server" Width="100px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlUnitType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <div style="clear: both;">
                                </div>
                                <label>
                                    <asp:Label ID="Label29" runat="server" Text="Rate"></asp:Label>
                                    <asp:Label ID="Label30" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFi" runat="server" ControlToValidate="txtWeightRate"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <%-- </label>
                     <label>--%>
                                    <asp:TextBox ID="txtWeightRate" runat="server" Width="100px" Text="0" MaxLength="10"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server" Enabled="True"
                                        TargetControlID="txtWeightRate" ValidChars="0147852369.">
                                    </cc1:FilteredTextBoxExtender>
                                </label>
                                <label>
                                    <asp:Label ID="Label42" runat="server" Text="Per"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="Label31" runat="server" Text="Unit"></asp:Label>
                                    <%-- </label>
                    <label>--%>
                                    <asp:DropDownList ID="ddlunittype1" runat="server" Width="100px">
                                    </asp:DropDownList>
                                </label>
                            </asp:Panel>
                            <div style="clear: both;">
                            </div>
                            <asp:Panel ID="pnlvolume" runat="server" Visible="false">
                                <label>
                                    <asp:Label ID="Label20" runat="server" Text="Minimum Volume"></asp:Label>
                                    <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFiel" runat="server" ControlToValidate="txtMinVolume"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <%--  </label>
                      
                            <label>--%>
                                    <asp:TextBox ID="txtMinVolume" runat="server" MaxLength="10" Text="0" Width="100px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender35" runat="server" Enabled="True"
                                        TargetControlID="txtMinVolume" ValidChars="0147852369">
                                    </cc1:FilteredTextBoxExtender>
                                </label>
                                <label>
                                    <asp:Label ID="Label21" runat="server" Text="Maximum Volume"></asp:Label>
                                    <asp:Label ID="Label32" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="Requi" runat="server" ControlToValidate="txtMaxVolume"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <%-- </label>
                       
                            <label>--%>
                                    <asp:TextBox ID="txtMaxVolume" runat="server" Width="100px" MaxLength="10" Text="0"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" SetFocusOnError="true"
                                        ControlToCompare="txtMinVolume" ControlToValidate="txtMaxVolume" Type="Integer"
                                        Operator="GreaterThan" ValidationGroup="1" ErrorMessage="Larger then Min Volume"></asp:CompareValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender44" runat="server" Enabled="True"
                                        TargetControlID="txtMaxVolume" ValidChars="0147852369">
                                    </cc1:FilteredTextBoxExtender>
                                </label>
                                <label>
                                    <asp:Label ID="Label44" runat="server" Text="Per"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="Label22" runat="server" Text="Unit"></asp:Label>
                                    <%-- </label>
                       
                        <label>--%>
                                    <asp:DropDownList ID="ddlVolumeUnit" runat="server" Width="100px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlVolumeUnit_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <div style="clear: both;">
                                </div>
                                <label>
                                    <asp:Label ID="Label23" runat="server" Text="Rate"></asp:Label>
                                    <asp:Label ID="Label34" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="Requ" runat="server" ControlToValidate="txtVolumerate"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <%--   </label>
                       
                            <label>--%>
                                    <asp:TextBox ID="txtVolumerate" runat="server" MaxLength="10" Text="0" Width="100px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender53" runat="server" Enabled="True"
                                        TargetControlID="txtVolumerate" ValidChars="0147852369">
                                    </cc1:FilteredTextBoxExtender>
                                </label>
                                <label>
                                    <asp:Label ID="Label45" runat="server" Text="Per"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="Label33" runat="server" Text="Unit"></asp:Label>
                                    <%--   </label>
                       
                        <label>--%>
                                    <asp:DropDownList ID="ddlvolunit" runat="server" Width="100px">
                                    </asp:DropDownList>
                                </label>
                            </asp:Panel>
                            <div style="clear: both;">
                            </div>
                            <asp:Panel ID="pnlperorder" runat="server" Visible="false">
                                <label>
                                    <asp:Label ID="Label24" runat="server" Text="Flat Rate"></asp:Label>
                                    <asp:Label ID="Label35" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="Re" runat="server" ControlToValidate="txtFlatRate"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpenValidator3" ControlToValidate="txtFlatRate"
                                        ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"
                                        ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator>
                                    <%--</label>
                       
                           <label>--%>
                                    <asp:TextBox ID="txtFlatRate" runat="server" Width="100px" MaxLength="10" Text="0"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender62" runat="server" Enabled="True"
                                        TargetControlID="txtFlatRate" ValidChars="0147852369.">
                                    </cc1:FilteredTextBoxExtender>
                                </label>
                                <br />
                                <asp:RadioButtonList ID="rdflatamt" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0" Text="$"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="%"></asp:ListItem>
                                </asp:RadioButtonList>
                                <div style="clear: both;">
                                </div>
                                <%-- </asp:Panel>--%>
                                <div style="clear: both;">
                                    <br />
                                </div>
                            </asp:Panel>
                    </asp:Panel>
                    <div style="padding-left: 1%">
                        <asp:Panel ID="pnlbtnshow" runat="server" Visible="false">
                            <asp:Button ID="ImageButton1" CssClass="btnSubmit" runat="server" ValidationGroup="1"
                                Text="Submit" OnClick="Button1_Click" />
                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            <asp:Button ID="imgupdate" runat="server" CssClass="btnSubmit" OnClick="imgupdate_Click"
                                Text="Update" ValidationGroup="1" Visible="False" />
                            <asp:Button ID="ImageButton2" runat="server" CssClass="btnSubmit" OnClick="Button2_Click"
                                Text="Cancel" ValidationGroup="13" />
                        </asp:Panel>
                    </div>
                    </fieldset>
                    <div style="clear: both;">
                    </div>
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label36" runat="server" Text="List of Fixed Shipping Rates Based on Weight/Volume/Order"></asp:Label>
                        </legend>
                        <div style="float: right;">
                            <label>
                                <asp:Button ID="btnnew" runat="server" Text="Add New Fixed Shipping Rate" CssClass="btnSubmit"
                                    OnClick="btnnew_Click" /></label>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label2" runat="server" Text="Filter by Country"></asp:Label>
                            <%-- </label>
                      <label>--%>
                            <asp:DropDownList ID="ddlfiltercountory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfiltercountory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label25" runat="server" Text="Filter by State"></asp:Label>
                            <%--  </label>
                    
                    <label>--%>
                            <asp:DropDownList ID="ddlfilterstate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterstate_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label39" runat="server" Text="Filter by City"></asp:Label>
                            <%--</label>
                     <label>--%>
                            <asp:DropDownList ID="ddlfiltercity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfiltercity_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <%--   <label style="float: right;">
                          
                                 </label>--%>
                        <label style="float: right;">
                            <asp:Button ID="btnprinta" runat="server" Text="Printable Version" CssClass="btnSubmit"
                                OnClick="btnprinta_Click" />
                            <input id="Button75" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" class="btnSubmit" value="Print" visible="false" />
                        </label>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div id="mydiv" class="closed">
                                            <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblCompany" runat="server" Font-Size="20px" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label37" runat="server" Font-Size="20px" Text="Business :"></asp:Label>
                                                        <asp:Label ID="lblbusiness" runat="server" Font-Size="20px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label38" runat="server" Text="List of Fixed Shipping Rates Based on Weight/Volume/Order"
                                                            Font-Size="18px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowCommand="GridView2_RowCommand"
                                            OnRowDeleting="GridView2_RowDeleting" DataKeyNames="Id" OnRowEditing="GridView2_RowEditing"
                                            OnSorting="GridView2_Sorting" EmptyDataText="No Record Found." Width="100%" HeaderStyle-HorizontalAlign="Left">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Text='<%#Bind("Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCntid" runat="server" Text='<%#Bind("CountryName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstid" runat="server" Text='<%#Bind("StateName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="City" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblctid" runat="server" Text='<%#Bind("CityName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Start Zip/Postal" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstrzp" runat="server" Text='<%#Bind("StartZip") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End Zip/Postal" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblendzp" runat="server" Text='<%#Bind("EndZip") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Weight Rate" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWhtRate" runat="server" Text='<%#Bind("WeightRate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shipping Volume Rate" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVlmRate" runat="server" Text='<%#Bind("Volumerate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Flate Rate" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFlateRate" runat="server" Text='<%#Bind("FlatRate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="img1" runat="server" CausesValidation="true" ToolTip="View"
                                                            ImageUrl="~/Account/images/viewprofile.jpg" CommandArgument='<%#Bind("Id") %>'
                                                            CommandName="View"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="img11" runat="server" CausesValidation="true" ToolTip="Edit"
                                                            ImageUrl="~/Account/images/edit.gif" CommandArgument='<%#Bind("Id") %>' CommandName="Edit">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                    HeaderStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="llinkbb" runat="server" ToolTip="Delete" CommandName="Delete"
                                                            ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="2%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset></asp:Panel>
                <asp:Panel ID="Panel1" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" BorderStyle="Outset"
                    Width="300px">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <label>
                                    <%--                                            <asp:Label ID="Label26" runat="server" Text="Confirm Delete"></asp:Label>
                                    --%>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label27" runat="server" Text="Do you really wish to stop using this shipping company?"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnchkcon" runat="server" Text="Confirm" OnClick="btnchkcon_Click" />
                                <asp:Button ID="btnchkcan" runat="server" Text="Cancel" OnClick="btnchkcan_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button1" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="mod1" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel1" TargetControlID="Button1">
                </cc1:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
