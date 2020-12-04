<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="ManageCustomerDiscountCategory.aspx.cs" Inherits="ManageCutomerDiscountCategory"
    Title="Customer Discount Categories: Manage " %>

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
         function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
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
                <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <table width="100%">
                        <fieldset>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                            <asp:ListItem Value="0">View Customer Discount Category</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="1">Apply Customer Discount Category</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <asp:Panel ID="Panel1" runat="server">
                            <tr>
                                <td colspan="2">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="lblstep1" runat="server" Text="Step 1:"></asp:Label>
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label11" runat="server" Text="Select Business"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:DropDownList ID="ddlSearchByStore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchByStore_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="lblstep2" runat="server" Text="Step 2:"></asp:Label>
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label1" runat="server" Text="Change the customer discount category of the selected customers to "></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:DropDownList ID="ddldiscountcategory" runat="server">
                                                        </asp:DropDownList>
                                                    </label>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <label>
                                                                <asp:ImageButton ID="LinkButton97666667" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                    OnClick="LinkButton97666667_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                                                &nbsp;
                                                                <asp:ImageButton ID="LinkButton13" runat="server" AlternateText="Refresh" Height="20px"
                                                                    ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton13_Click"
                                                                    ToolTip="Refresh" Width="20px" ImageAlign="Bottom" />
                                                            </label>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="LinkButton13" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </asp:Panel>
                    </table>
                    <fieldset>
                        <legend>
                            <asp:Label ID="lblstep3" runat="server" Text="Step 3:"></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <fieldset>
                                        <legend>Select customers to apply discount category </legend>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label9" runat="server" Text="Filter Customer List "></asp:Label>
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Panel ID="pnlfilter" runat="server" Width="100%" Visible="false">
                                                <table width="100%">
                                                    <tr>
                                                        <td width="20%">
                                                            <label>
                                                                <asp:Label ID="Label2" runat="server" Text="Filter by Business"></asp:Label>
                                                                <asp:DropDownList ID="ddlfilterbybusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbybusiness_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td width="20%">
                                                            <label>
                                                                <asp:Label ID="Label3" runat="server" Text="Filter by Customer Discount Category"></asp:Label>
                                                                <asp:DropDownList ID="ddlfilterbydiscountcategory" runat="server" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td colspan="3">
                                                            <label>
                                                                <asp:Label ID="Label4" runat="server" Text="Search by (customer name, contact name, phone number, email address)"></asp:Label>
                                                                <asp:TextBox ID="txtsearchby" runat="server" MaxLength="30" onkeyup="return mak('div1',30,this)">
                                                                </asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="Invalid Character"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9@\s]*)"
                                                                    ControlToValidate="txtsearchby">
                                                                </asp:RegularExpressionValidator>
                                                                <asp:Label ID="Label19" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="div1" class="labelcount">30</span>
                                                                <asp:Label ID="lblsadjk" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount">
                                                                </asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="lblcontry" runat="server" Text="Filter by Country"></asp:Label>
                                                                <asp:DropDownList ID="ddlfiltercountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfiltercountry_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label10" runat="server" Text="Filter by State"></asp:Label>
                                                                <asp:DropDownList ID="ddlflterstate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlflterstate_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label24" runat="server" Text="Filter by City"></asp:Label>
                                                                <asp:DropDownList ID="ddlfiltercity" runat="server">
                                                                </asp:DropDownList>
                                                            </label>
                                                            <label>
                                                                <br />
                                                                <asp:Button ID="Button3" runat="server" Text="Go" OnClick="Button3_Click" />
                                                            </label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <div>
                                        </div>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <fieldset>
                                        <legend>Customer List by Discount Category</legend>
                                        <div style="float: right;">
                                            <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                                OnClick="Button1_Click" CausesValidation="False" />
                                            <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                                type="button" value="Print" visible="false" />
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr align="center">
                                                    <td>
                                                        <div id="mydiv" class="closed">
                                                            <table width="100%" style="font-style: italic">
                                                                <tr>
                                                                    <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                        <asp:Label ID="lblCompany" runat="server" Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                        <asp:Label ID="Label6" runat="server" Text="Business : " Font-Italic="true" Font-Size="20px"></asp:Label>
                                                                        <asp:Label ID="lblBusiness" runat="server" Font-Size="20px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                        <asp:Label ID="Label7" runat="server" Text="Customer List by Discount Category" ForeColor="Black"
                                                                            Font-Size="18px" Font-Italic="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label21" runat="server" Text="Customer Discount Category : " Font-Size="18px"
                                                                            Font-Italic="true">
                                                        
                                                                        </asp:Label>
                                                                        <asp:Label ID="lblcutomercategoryprint" runat="server" Font-Size="18px" Font-Italic="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GridView1" runat="server" AllowSorting="false" AutoGenerateColumns="False"
                                                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                            DataKeyNames="PartyID" Width="100%" EmptyDataText="No Record Found." OnRowEditing="GridView1_RowEditing"
                                                            OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Business Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblbusinessname" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Customer Name" SortExpression="Compname" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcustomername" runat="server" Text='<%# Eval("Compname") %>'></asp:Label>
                                                                        <asp:Label ID="lblpartyid" runat="server" Text='<%# Eval("PartyID") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Contact Name" SortExpression="Contactperson" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcontactname" runat="server" Text='<%# Eval("Contactperson") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Phone No." SortExpression="Phoneno" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPhoneno" runat="server" Text='<%# Eval("Phoneno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="City" SortExpression="CityName" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcityname" runat="server" Text='<%# Eval("CityName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="State" SortExpression="StateName" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblstatename" runat="server" Text='<%# Eval("StateName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Country" SortExpression="CountryName" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcountryname" runat="server" Text='<%# Eval("CountryName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Discount Category" SortExpression="PartyCategoryName"
                                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldiscountname" runat="server" Text='<%# Bind("PartyCategoryName")%>'></asp:Label>
                                                                        <asp:Label ID="lblpartydetailmasterid" runat="server" Text='<%# Bind("PartyTypeDetailId")%>'
                                                                            Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblpartytypemasterid" runat="server" Text='<%# Bind("PartyTypeCategoryMasterId")%>'
                                                                            Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblwhid" runat="server" Text='<%# Bind("WarehouseId")%>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList ID="ddlgrdcategory" DataTextField="PartyCategoryName" DataValueField="PartyTypeCategoryMasterId"
                                                                            runat="server">
                                                                        </asp:DropDownList>
                                                                        <asp:Label ID="lblwhid123" runat="server" Text='<%# Bind("WarehouseId")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblpartydetailmasterid123" runat="server" Text='<%# Bind("PartyTypeDetailId")%>'
                                                                            Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblpartytypemasterid123" runat="server" Text='<%# Bind("PartyTypeCategoryMasterId")%>'
                                                                            Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblpartyid123" runat="server" Text='<%# Eval("PartyID") %>' Visible="false"></asp:Label>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkpartyid" runat="server" AutoPostBack="True" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblwhid123" runat="server" Text="Select"></asp:Label>
                                                                        <asp:CheckBox ID="chkmasterid" runat="server" AutoPostBack="True" OnCheckedChanged="chkboxcopy_CheckedChanged" />
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:CommandField HeaderText="Change" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="10%" EditText="Change" ButtonType="Button" ShowEditButton="True" />
                                                            </Columns>
                                                            <PagerStyle CssClass="pgr" />
                                                            <AlternatingRowStyle CssClass="alt" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel2" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 50%">
                                                    </td>
                                                    <td style="width: 50%">
                                                        <asp:Button ID="Button4" runat="server" Text="Update" CssClass="btnSubmit" OnClick="Button4_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
