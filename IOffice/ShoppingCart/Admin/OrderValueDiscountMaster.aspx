<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="OrderValueDiscountMaster.aspx.cs" Inherits="Admin_OrderValueDiscountMaster"
    Title="Untitled Page" %>

<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UControlWizardpanel.ascx" TagName="pnl"
    TagPrefix="pnl" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function RealNumWithDecimal(myfield, e, dec)
         {

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
            var prtContent = document.getElementById('<%= Panel2.ClientID %>');
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
    <asp:UpdatePanel ID="pbd" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="btnaddroom" runat="server" Text="Add Order Value Discount" CssClass="btnSubmit"
                                OnClick="btnaddroom_Click" />
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="addinventoryroom" Visible="false" runat="server">
                        <table id="subinnertbl" width="100%">
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        <asp:Label ID="Label9" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 25%">
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    <label>
                                        <asp:DropDownList ID="ddlWarehouse" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td style="width: 25%">
                                    <label>
                                        <asp:Label ID="Label11" runat="server" Text="Value Discount"></asp:Label>
                                        <asp:Label ID="Label8" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSchemaValueDiscount"
                                            ErrorMessage="*" ValidationGroup="1">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtSchemaValueDiscount"
                                            ValidationGroup="1" ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server"
                                            ErrorMessage="Invalid Digits "> </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 25%">
                                    <label>
                                        <cc1:FilteredTextBoxExtender ID="txtSchemaVhhfalueDiscount" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtSchemaValueDiscount" ValidChars="0.123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtSchemaValueDiscount" onkeyup="return mak('Span1',15,this)" runat="server"
                                            MaxLength="15" TabIndex="4" ValidationGroup="1">
                                        </asp:TextBox>
                                        <asp:Label ID="Label24" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" class="labelcount">15</span>
                                        <asp:Label ID="Label23" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label10" runat="server" Text="Discount Name"></asp:Label>
                                        <asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSchemaName"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="R14554545" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtSchemaName" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtSchemaName" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',30)"
                                            MaxLength="30" TabIndex="1" ValidationGroup="1">
                                        </asp:TextBox>
                                        <asp:Label ID="Label25" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" class="labelcount">30</span>
                                        <asp:Label ID="Label7" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label17" runat="server" Text="Discount Type"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="2" TabIndex="10" Text="Amount" />
                                    <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" GroupName="2" TabIndex="9"
                                        Text="%" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text="Eligible Min Order Value"></asp:Label>
                                        <asp:Label ID="Label20" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMinValQuantity"
                                            ErrorMessage="*" ValidationGroup="1">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularEsionValidator1" ControlToValidate="txtMinValQuantity"
                                            ValidationGroup="1" ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server"
                                            ErrorMessage="Invalid Digits"> </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <cc1:FilteredTextBoxExtender ID="txtbalance_FilteredTextBoxExtender1" runat="server"
                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtMinValQuantity"
                                            ValidChars="0.123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtMinValQuantity" runat="server" MaxLength="10" onkeyup="return mak('Span2',10,this)"
                                            TabIndex="2" ValidationGroup="1">
                                        </asp:TextBox>
                                        <asp:Label ID="Label26" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span2" class="labelcount">10</span>
                                        <asp:Label ID="Label27" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Eligible Max Order Value"></asp:Label>
                                        <asp:RegularExpressionValidator ID="ularExpressionValidator1" ControlToValidate="txtMaxValQuantity"
                                            ValidationGroup="1" ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server"
                                            ErrorMessage="Invalid Digits">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtMaxValQuantity" ValidChars="0.123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtMaxValQuantity" runat="server" MaxLength="10" onkeyup="return mak('Span3',10,this)"
                                            TabIndex="3" ValidationGroup="1"></asp:TextBox>
                                        <asp:Label ID="Label28" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span3" class="labelcount">10</span>
                                        <asp:Label ID="Label29" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Start Date"></asp:Label>
                                        <asp:Label ID="Label21" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tXtEffectiveStartdate"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="tXtEffectiveStartdate" runat="server" Width="80px" MaxLength="10">
                                        </asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/cal_btn.jpg"
                                            TabIndex="5" OnClick="ImageButton1_Click" />
                                    </label>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="tXtEffectiveStartdate" />
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                                        TargetControlID="tXtEffectiveStartdate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text="End date "></asp:Label>
                                        <asp:Label ID="Label22" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtenddate"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtenddate" runat="server" ValidationGroup="1" Width="80px" MaxLength="10">
                                        </asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/cal_btn.jpg"
                                            TabIndex="6" />
                                    </label>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureName="en-AU"
                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtenddate" />
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton2"
                                        TargetControlID="txtenddate">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="nn" Text="Apply to Online Sales" runat="server"></asp:Label>
                                    </label>
                                    <asp:CheckBox ID="chkonline" runat="server" />
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label2" Text="Apply to Retail Sales" runat="server"></asp:Label>
                                    </label>
                                    <asp:CheckBox ID="chkretail" runat="server" />
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Status "></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlstatus" runat="server" Width="120px">
                                            <asp:ListItem Selected="True" Value="1" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="ImageButton3" runat="server" ValidationGroup="1" CssClass="btnSubmit"
                                        OnClick="Button1_Click" Text="Submit" />
                                    <asp:Button ID="imgupdate" runat="server" ValidationGroup="1" OnClick="imgupdate_Click"
                                        Visible="False" Text="Update" CssClass="btnSubmit" />
                                    <asp:Button ID="ImageButton4" runat="server" OnClick="Button2_Click" Text="Cancel"
                                        CssClass="btnSubmit" CausesValidation="false" />
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text=" List of Order Value Discount Details" runat="server"
                            Font-Bold="true"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="Button2" runat="server" Text="Printable Version" OnClick="Button1_Click1"
                                CssClass="btnSubmit" CausesValidation="False" />
                            <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" visible="false" class="btnSubmit" />
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label18" Text=" Filter by Business Name" runat="server">
                        </asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="250px"
                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="600px" ScrollBars="Vertical">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Italic="True" ForeColor="Black" Visible="false">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label19" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Italic="True" ForeColor="Black">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label3" runat="server" Font-Italic="True" Text="List of Order Value Discount Details">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                        DataKeyNames="OrderValueDiscountMasterId" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        Width="100%" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting"
                                        AllowSorting="True" OnSorting="GridView1_Sorting" EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwh" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                    <asp:Label ID="lblwhid" runat="server" Visible="false" Text='<%#Bind("Whid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SchemeName" HeaderText="Discount Name" SortExpression="SchemeName"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Eligible Min <br/> Order value" SortExpression="MinValue"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("MinValue") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eligible Max <br/> Order value" SortExpression="MaxValue"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("MaxValue") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount" SortExpression="ValueDiscount" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsign" runat="server" Visible="false" Text="$"></asp:Label>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ValueDiscount") %>'></asp:Label>
                                                    <asp:Label ID="lblpersign" runat="server" Visible="false" Text="%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Date" SortExpression="StartDate" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEffectiveStartDate" runat="server" Text='<%# Bind("StartDate", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Date" SortExpression="EndDate" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox2" runat="server" Text='<%# Bind("EndDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="Status" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtstat" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CheckBoxField DataField="ApplyOnlineSales" HeaderText="Online" SortExpression="ApplyOnlineSales"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                            </asp:CheckBoxField>
                                            <asp:CheckBoxField DataField="ApplyRetailSales" HeaderText="Retail" SortExpression="ApplyRetailSales"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                            </asp:CheckBoxField>
                                            <asp:TemplateField HeaderText="%" SortExpression="IsPercentage" Visible="false" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="txtchisper" runat="server" Checked='<%# Bind("IsPercentage") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:CommandField ShowSelectButton="True" SelectText="Edit" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Edit" ButtonType="Image" HeaderImageUrl="~/Account/images/edit.gif"
                                                SelectImageUrl="~/Account/images/edit.gif">
                                                <ItemStyle Width="2%" />
                                            </asp:CommandField>--%>
                                            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("OrderValueDiscountMasterId") %>'
                                                        CommandName="Select" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderImageUrl="~/ShoppingCart/images/trash.jpg" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Button1" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');"></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="2%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" BorderStyle="Outset"
                        Width="300px">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="subinnertblfc" style="height: 18px">
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblm" runat="server" ForeColor="Black">Please check the date.</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 26px">
                                    <asp:Label ID="Label5" runat="server" ForeColor="Black" Text="Start Date of the Year is "></asp:Label>
                                    <asp:Label ID="lblstartdate1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 26px">
                                    <asp:Label ID="lblm0" runat="server" ForeColor="Black">You can not select 
                            anydate earlier than that. </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 26px">
                                    <asp:ImageButton ID="ImageButton5" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/cancel.png"
                                        OnClick="ImageButton2_Click" />
                                </td>
                            </tr>
                        </table>
                        &nbsp;</asp:Panel>
                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                        ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel5" TargetControlID="HiddenButton222">
                    </cc1:ModalPopupExtender>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
