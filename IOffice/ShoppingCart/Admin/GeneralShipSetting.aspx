<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="GeneralShipSetting.aspx.cs" Inherits="GeneralShipSetting"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function CallPrint(strid) 
        {
            var prtContent = document.getElementById('<%= Panel1.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
      
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="btnadd" CssClass="btnSubmit" runat="server" Text="Add Handling Charge and General Shipping Option"
                            OnClick="btnadd_Click" />
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <fieldset>
                            <div>
                                <label>
                                    <asp:Label ID="Label11" runat="server" Text="Select Business"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFielasdfdValidatorasdf1" runat="server" ControlToValidate="ddlware"
                                        InitialValue="0" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlware" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlware_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </div>
                        </fieldset>
                        <div style="clear: both;">
                        </div>
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label3" runat="server" Text="Shipping Origin Address"></asp:Label>
                            </legend>
                            <table id="Table2"  width="100%">
                                <tr>
                                    <td width="20%">
                                        <label>
                                            <asp:Label ID="Label7" runat="server" Text="City"></asp:Label>
                                        </label>
                                    </td>
                                    <td width="20%">
                                        <label>
                                            <asp:Label ID="Label6" runat="server" Text="State"></asp:Label>
                                        </label>
                                    </td>
                                    <td width="20%">
                                        <label>
                                            <asp:Label ID="Label4" runat="server" Text="Country"></asp:Label>
                                        </label>
                                    </td>
                                    <td width="40%">
                                        <label>
                                            <asp:Label ID="Label9" runat="server" Text="ZIP"></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEndzip"
                                                ErrorMessage="*" ValidationGroup="10"></asp:RequiredFieldValidator>
                                                
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="10"
                                                ErrorMessage="Invalid Character"  SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtEndzip"></asp:RegularExpressionValidator>
                                                
                                        </label>
                                        <div style="text-align: right">
                                            <asp:Button ID="Button2" CssClass="btnSubmit" runat="server" ValidationGroup="10" Text="Change" OnClick="Button2_Click1" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Panel ID="Panel2" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td width="20%">
                                                        <label>
                                                            <asp:Label ID="lblcity" runat="server" ></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td width="20%">
                                                        <label>
                                                            <asp:Label ID="lblstate" runat="server" ></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td width="20%">
                                                        <label>
                                                            <asp:Label ID="lblcountry" runat="server" ></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td width="40%">
                                                        <label>
                                                            <asp:Label ID="lblzip" runat="server" ></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Panel ID="pnlchangeaddress" runat="server" Visible="false" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td width="20%">
                                                        <label>
                                                            <asp:DropDownList ID="ddlcity"    runat="server" 
                                                             AutoPostBack="True" 
                                                            onselectedindexchanged="ddlcity_SelectedIndexChanged" Width="149px">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                    <td width="20%">
                                                        <label>
                                                            <asp:DropDownList ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"
                                                                AutoPostBack="True" Width="149px">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                    <td width="20%">
                                                        <label>
                                                            <asp:DropDownList ID="ddlcountry" runat="server" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged"
                                                                AutoPostBack="True" Width="149px" >
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                    <td width="40%">
                                                        <label>
                                                            <asp:TextBox ID="txtEndzip" runat="server" Width="150px"  MaxLength="15" onKeydown="return mask(event)"
                                                                onkeyup="return check(this,/[\\/!@#$%^'&.,_*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9]+$/,'Span6',15)"></asp:TextBox>
                                                                
                                                                
                                                        </label>
                                                        <label>
                                                         <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                            <span id="Span6" class="labelcount">15</span>
                                                            <asp:Label ID="Label19" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                                                        </label>
                                                       
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
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label12" runat="server" Text="Handling Charge Option"></asp:Label>
                            </legend>
                            <label>
                                <asp:Label ID="Label13" runat="server" Text="A) Do you wish to add a custom name for your handling charges ?"></asp:Label>
                            </label>
                            <asp:CheckBox ID="chkhandlingcharge" runat="server" AutoPostBack="true" TextAlign="Left"
                                Text="Yes" OnCheckedChanged="chkhandlingcharge_CheckedChanged" />
                            <div style="clear: both;">
                            </div>
                            <asp:Panel ID="pnlhandlingtext" runat="server" Visible="false">
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="Custom Name for your Handling Charge"></asp:Label>
                                    <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHndlingText"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="1"
                                        ErrorMessage="Invalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                        ControlToValidate="txtHndlingText"></asp:RegularExpressionValidator>
                                </label>
                                <label>
                                    <asp:TextBox ID="txtHndlingText" runat="server"  MaxLength="30" onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!@.,#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'div1',30)"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label ID="Label20" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                    <span id="div1" class="labelcount">30</span>
                                    <asp:Label ID="Label17" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                </label>
                            </asp:Panel>
                            <div style="clear: both;">
                            </div>
                            <label>
                                <asp:Label ID="Label10" runat="server" Text="B) Handling Charge per order "></asp:Label>
                                <asp:Label ID="Label15" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtHndlingText0"
                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" ControlToValidate="txtHndlingText0"
                                    ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid Digits"
                                    ValidationGroup="1"> </asp:RegularExpressionValidator>
                            </label>
                            <label>
                            <asp:Label ID="Label23" runat="server" Text="$" ></asp:Label>
                               
                                     
                            </label>
                            <label>
                             <asp:TextBox ID="txtHndlingText0"  MaxLength="15" runat="server"
                                    onkeypress="return RealNumWithDecimal(this,event,2);" onkeyup="return mak('Span1', 15, this)"
                                    Text="0"></asp:TextBox>
                            </label>
                            <label>
                                <asp:Label ID="Label21" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                <span id="Span1" class="labelcount">15</span>
                                <asp:Label ID="Label22" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                            </label>
                        </fieldset>
                        <div style="clear: both;">
                        </div>
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label14" runat="server" Text="General Shipping Options for Online Orders"></asp:Label>
                            </legend>
                            <asp:CheckBox ID="CheckBox1" runat="server" TextAlign="Right" Text=" Allow Different Shipping and Billing Address" />
                            <div style="clear: both;">
                            </div>
                            <asp:CheckBox ID="CheckBox2" runat="server" TextAlign="Right" Text=" Allow International Shipping Address" />
                        </fieldset>
                        <fieldset>
                            <div>
                                <asp:Button ID="ImageButton1" runat="server" OnClick="Button1_Click" CssClass="btnSubmit"
                                    ValidationGroup="1" Text="Submit" />
                                <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" ValidationGroup="1"
                                    Text="Update" OnClick="Button1_Click1" Visible="false" />
                                <asp:Button ID="ImageButton2" runat="server" OnClick="Button2_Click" CssClass="btnSubmit"
                                    Text="Cancel" />
                            </div>
                        </fieldset></asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label16" runat="server" Text="List of Handling Charges and General Shipping Options" Font-Bold="true"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btncancel0" runat="server" CssClass="btnSubmit" CausesValidation="false"
                            Text="Printable Version" OnClick="btncancel0_Click" />
                        <input id="Button7" runat="server" visible="false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" type="button" value="Print" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign="Left">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblcompany" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label38" runat="server" Font-Size="20px" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblbusiness" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label18" runat="server" Text="List of Handling Charges and General Shipping Options"
                                                        Font-Size="18px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="grdTempData" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowDeleting="grdTempData_RowDeleting"
                                        OnRowEditing="grdTempData_RowEditing" OnSorting="grdTempData_Sorting" EmptyDataText="No Record Found."
                                        Width="100%" DataKeyNames="general_ship_settings_id"  AllowPaging="false"
                                        OnRowCreated="grdTempData_RowCreated" AllowSorting="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblid" runat="server" Text='<%#Bind("general_ship_settings_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Business Name" ItemStyle-Width="13%" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwname" Text='<%#Bind("Name") %>' runat="server"> </asp:Label>
                                                    <asp:Label ID="lblwid" Text='<%#Bind("Whid") %>' Visible="false" runat="server"></asp:Label>
                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Country" ItemStyle-Width="10%" SortExpression="CountryName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCountry" Text='<%#Bind("CountryName") %>' runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State" ItemStyle-Width="10%" SortExpression="StateName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblState" Text='<%#Bind("StateName") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="City" ItemStyle-Width="10%" SortExpression="CityName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCity" Text='<%#Bind("CityName") %>' runat="server">                                
                                                    </asp:Label>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ZIP" ItemStyle-Width="8%" SortExpression="shipping_orgin_zip_code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstrzp" runat="server" Text='<%#Bind("shipping_orgin_zip_code") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Custome Name <br/>Handling Charges" ItemStyle-Width="15%"
                                                SortExpression="handling_text" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMinWht" runat="server" Text='<%#Bind("handling_text") %>'></asp:Label>
                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shipping Option" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMaxWht" runat="server" Text='<%#Bind("select_shipping_option") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Charge Per Order" ItemStyle-Width="10%" SortExpression="charge_per_order" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="ddlWhtUnit15" runat="server" Enabled="false" Text='<%#Bind("charge_per_order") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow Different <br/> Shipping Address" ItemStyle-Width="12%"
                                                SortExpression="allow_different_shipping_addr" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ddlWhtUnit5" Visible="false" runat="server" Enabled="false" Checked='<%#Eval("allow_different_shipping_addr") %>'>
                                                    </asp:CheckBox>
                                                    <asp:Label ID="lblshippaddress" Text='<%#Bind("Shipaddresslbl") %>' runat="server"> </asp:Label>
                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow International  <br/> Shipping Address" ItemStyle-Width="22%"
                                                SortExpression="allow_international_shipping_addr" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ddlWhtUnit3" Visible="false" runat="server" Enabled="false" Checked='<%#Eval("allow_international_shipping_addr") %>'>
                                                    </asp:CheckBox>
                                                    <asp:Label ID="lblinternationaddress" Text='<%#Bind("Internationaddresslbl") %>' runat="server"> </asp:Label>
                                                </ItemTemplate>
                                            
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Show Hanling Text" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ddlWhtUnit2"  runat="server" Enabled="false" Checked='<%#Eval("show_handling_text") %>'>
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnedit" runat="server" CommandName="Edit" ImageUrl="~/Account/images/edit.gif"
                                                        ToolTip="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Btn" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                                <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                            </tr>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
