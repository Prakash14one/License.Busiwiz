<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="PartyTypeCategoryMasterTbl.aspx.cs" Inherits="PartyTypeCategoryMasterTbl"
    Title="Untitled Page" %>

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
        function Button7_onclick() {

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Add New Category for Customer Discount"
                            Font-Bold="True" Visible="False"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add New Category for Customer Discount"
                            CssClass="btnSubmit" OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <table cellpadding="0" cellspacing="3" width="100%">
                            <tr>
                                <td style="width: 50%">
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Business Name"></asp:Label>
                                        <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="rd1" runat="server" ValidationGroup="1" InitialValue="0"
                                            ControlToValidate="ddlwarehouse" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 50%">
                                    <label>
                                        <asp:DropDownList ID="ddlwarehouse" runat="server" ValidationGroup="1">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Discount Category Name"></asp:Label>
                                        <asp:Label ID="Label14" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox1"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="*" Display="Dynamic"
                                            SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9'(),\s]*)" ControlToValidate="TextBox1"
                                            ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="TextBox1" runat="server" ValidationGroup="1" MaxLength="25" onkeyup="return mak('div1',25,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" class="labelcount">25</span>
                                        <asp:Label ID="lblcategorynnname" runat="server" class="labelcount" Text="(A-Z 0-9 _ () ' ,)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label9" runat="server" Text="Discount in %"></asp:Label>
                                        <asp:Label ID="Label15" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox2"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="TextBox2"
                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid Digits"
                                            ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="TextBox2" runat="server" onkeypress="return RealNumWithDecimal(this,event,2);"
                                            MaxLength="15" onkeyup="return mak('Span1',6,this)" Width="100px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label17" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" class="labelcount">6</span>
                                        <asp:Label ID="Label18" runat="server" class="labelcount" Text="(0-9)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="lbldatefrom" runat="server" Text="Effective Start Date "></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromDate"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtFromDate" runat="server" Width="100px"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="TextBox1_MaskedEditExtender2" runat="server" CultureName="en-AU"
                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate" />
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton3"
                                            TargetControlID="txtFromDate">
                                        </cc1:CalendarExtender>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label19" runat="server" Text="Effective End Date "></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtToDate"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtToDate" runat="server" Width="100px"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate" />
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgbtncal2"
                                            TargetControlID="txtToDate">
                                        </cc1:CalendarExtender>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgbtncal2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                    </label>
                                </td>
                            </tr>
                            <asp:Panel ID="Panel1" runat="server" Visible="false">
                                <tr>
                                    <td valign="top">
                                        <label>
                                            <asp:Label ID="Label10" runat="server" Text="Discount in % ?"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="2" Text="%" Checked="True" />
                                        <asp:RadioButton ID="RadioButton2" runat="server" GroupName="2" Text="Amount" />
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label11" runat="server" Text="Status"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlstatus" runat="server" Width="100px">
                                            <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label20" runat="server" Text="Select and apply this new discount category to existing customers after adding this record"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                </td>
                                <td>
                                    <asp:Button ID="ImageButton1" runat="server" CssClass="btnSubmit" OnClick="ImageButton1_Click"
                                        Text="Submit" ValidationGroup="1" />
                                    &nbsp;<asp:Button ID="ImageButton2" runat="server" CssClass="btnSubmit" OnClick="Button3_Click1"
                                        Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label13" runat="server" Text="List of Categories for Customer Discounts"
                            Font-Bold="true"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="Button3" runat="server" Text="Printable Version" OnClick="Button3_Click2"
                                    CssClass="btnSubmit" />
                                <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    type="button" value="Print" visible="false" class="btnSubmit" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    <asp:Label ID="Label12" runat="server" Text="Select by Business"></asp:Label>
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label21" runat="server" Text="Filter by Status"></asp:Label>
                                    <asp:DropDownList ID="ddlfilterstatus" runat="server" Width="100px">
                                        <asp:ListItem Selected="True" Value="2">All</asp:ListItem>
                                        <asp:ListItem Value="1">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">Currently Effective</asp:ListItem>
                                        <asp:ListItem Value="1">Start Date/End Date</asp:ListItem>
                                    </asp:RadioButtonList>
                                </label>
                                <label>
                                    <br />
                                    <asp:Button ID="Button4" runat="server" Text="Go" ValidationGroup="2" OnClick="Button4_Click1" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel2" runat="server" Visible="False">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label22" runat="server" Text="From "></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox3"
                                                        ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="TextBox3" runat="server" Width="100px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureName="en-AU"
                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TextBox3" />
                                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="ImageButton4"
                                                        TargetControlID="TextBox3">
                                                    </cc1:CalendarExtender>
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                </label>
                                                <label>
                                                    <asp:Label ID="lbldateto" runat="server" Text="To "></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox4"
                                                        ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="TextBox4" runat="server" Width="100px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureName="en-AU"
                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TextBox4" />
                                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="ImageButton5"
                                                        TargetControlID="TextBox4">
                                                    </cc1:CalendarExtender>
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
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
                                                                        <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                            <asp:Label ID="lblCompany" runat="server" Font-Italic="true" Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                            <asp:Label ID="llll" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                                            <asp:Label ID="lblBusiness" runat="server" Font-Italic="true"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                            <asp:Label ID="Label5" runat="server" Font-Italic="true" Text="List of Categories for Customer Discounts"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label23" runat="server" Text="Status : " Font-Italic="true"></asp:Label>
                                                                            <asp:Label ID="lblstatusprint" runat="server" Font-Italic="true"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PartyTypeCategoryMasterId"
                                                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                AllowSorting="True" OnRowCommand="GridView1_RowCommand" Width="100%" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                                OnSorting="GridView1_Sorting" EmptyDataText="No Record Found." PageSize="50">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Id" InsertVisible="False" Visible="false" SortExpression="PartyTypeCategoryMasterId"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("PartyTypeCategoryMasterId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Business Name" SortExpression="Name" ItemStyle-Width="20%"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblb" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Customer Discount Category" SortExpression="PartyCategoryName"
                                                                        ItemStyle-Width="35%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("PartyCategoryName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Discount in %" SortExpression="PartyCategoryDiscount"
                                                                        ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("PartyCategoryDiscount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CheckBoxField DataField="IsPercentage" HeaderText="Discount in %" SortExpression="IsPercentage"
                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                                    </asp:CheckBoxField>
                                                                    <asp:CheckBoxField DataField="Active" Visible="false" HeaderText="Status" SortExpression="Active"
                                                                        ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Left"></asp:CheckBoxField>
                                                                    <asp:TemplateField HeaderText="Status" SortExpression="Statuslabel" ItemStyle-Width="5%"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblstatus123" runat="server" Text='<%# Bind("Statuslabel") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Effective Date" SortExpression="EntryDate" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("EntryDate", "{0:MM/dd/yyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Effective Start Date" SortExpression="StartDate" ItemStyle-Width="10%"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbleffectivestartdate" runat="server" Text='<%# Bind("StartDate", "{0:MM/dd/yyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Effective End Date" SortExpression="EndDate" ItemStyle-Width="10%"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbleffectiveenddate" runat="server" Text='<%# Bind("EndDate", "{0:MM/dd/yyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderImageUrl="~/Account/images/edit.gif">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="llinedit" runat="server" CommandName="editview" CommandArgument='<%#Eval("PartyTypeCategoryMasterId") %>'
                                                                                ImageUrl="~/Account/images/edit.gif" ToolTip="Edit"></asp:ImageButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="3%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderImageUrl="~/ShoppingCart/images/trash.jpg" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="llinkbb" runat="server" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                                CommandArgument='<%# Eval("PartyTypeCategoryMasterId") %>' CommandName="del"
                                                                                ImageUrl="~/Account/images/delete.gif" ToolTip="Delete"></asp:ImageButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                                <AlternatingRowStyle CssClass="alt" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Outset"
                                Width="300px">
                                <table id="Table1" cellpadding="2" cellspacing="4" width="100%">
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label6" runat="server" ForeColor="Black">
                  Are you sure you wish to delete this record?  
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="Button1" runat="server" Text="Yes" OnClick="Button1_Click" Width="40px" />
                                            &nbsp;<asp:Button ID="Button2" runat="server" Text="No" Width="40px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                                PopupControlID="Panel3" TargetControlID="HiddenButton222">
                            </cc1:ModalPopupExtender>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
