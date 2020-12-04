<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="InventoryReportView.aspx.cs" Inherits="ShoppingCart_Admin_InventoryReportView"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Src="~/IOffice/ShoppingCart/Admin/UserControl/UControlWizardpanel.ascx" TagName="pnl"
    TagPrefix="pnl" %>
<%@ Register Src="~/IOffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
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
            <div class="products_box">
                <fieldset>
                    <asp:Panel ID="addinventoryroom" runat="server">
                        <table id="subinnertbl" cellpadding="0" cellspacing="3" style="width: 100%">
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"> </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text="Search By"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                        RepeatDirection="Horizontal" Width="100%">
                                        <asp:ListItem Selected="True" Value="0">Category</asp:ListItem>
                                        <asp:ListItem Value="1">Name / Barcode / Product No.</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 25%">
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Business Name"></asp:Label>
                                        <asp:RequiredFieldValidator ID="ddb" runat="server" ControlToValidate="ddlStoreLocationDe"
                                            InitialValue="0" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 75%">
                                    <asp:DropDownList ID="ddlStoreLocationDe" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStoreLocationDe_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlInvCat" runat="server" Width="100%">
                                        <table id="Table1" width="100%">
                                            <tr>
                                                <td style="width: 25%">
                                                    <label>
                                                        <asp:Label ID="Label6" runat="server" Text="Category "> </asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:DropDownList ID="ddlInvCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvCat_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 15%">
                                                    <label>
                                                        <asp:Label ID="Label7" runat="server" Text=" Sub Category"> </asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:DropDownList ID="ddlInvSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubCat_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%">
                                                    <label>
                                                        <asp:Label ID="Label8" runat="server" Text="Sub Sub Category"> </asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:DropDownList ID="ddlInvSubSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubSubCat_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 15%">
                                                    <label>
                                                        <asp:Label ID="Label9" runat="server" Text="Inventory Name"> </asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:DropDownList ID="ddlInvName" runat="server" OnSelectedIndexChanged="ddlInvName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlInvName" runat="server" Width="100%">
                                        <table id="lftpnl" width="100%">
                                            <tr>
                                                <td style="width: 25%">
                                                    <label>
                                                        <asp:Label ID="Label10" runat="server" Text="Inventory Name"> </asp:Label>
                                                        <asp:Label ID="Label17" runat="server" Text="*" CssClass="labelstar"> </asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearchInvName"
                                                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                                            SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtSearchInvName">
                                                        </asp:RegularExpressionValidator>
                                                    </label>
                                                </td>
                                                <td style="width: 75%">
                                                    <label>
                                                        <asp:TextBox ID="txtSearchInvName" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z.0-9_\s]+$/,'div1',20)">
                                                        </asp:TextBox>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label19" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                        <span id="div1" class="labelcount">20</span>
                                                        <asp:Label ID="lblsadjk" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount">
                                                        </asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="lblInvName" runat="server" Font-Bold="True" ForeColor="Green" Visible="False"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 25%">
                                    <label>
                                        <asp:Label ID="Label11" runat="server" Text="Search by Status"> </asp:Label>
                                    </label>
                                </td>
                                <td align="left" style="width: 75%">
                                    <label>
                                        <asp:DropDownList ID="ddlstatus" runat="server">
                                            <asp:ListItem>All</asp:ListItem>
                                            <asp:ListItem>Active</asp:ListItem>
                                            <asp:ListItem>Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25%">
                                </td>
                                <td align="left" style="width: 75%">
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    <asp:Button ID="btnSearchGo" CssClass="btnSubmit" runat="server" OnClick="btnSearchGo_Click"
                                        Text="Go" ValidationGroup="1" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Panel2" runat="server" Width="100%" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtBarcode" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtProductNo" runat="server" Visible="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Inventory Items" Font-Bold="true" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click" />
                            <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" visible="false" />
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table id="GridTbl" width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblcomname" runat="server" Font-Italic="True" Visible="false"> </asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label16" runat="server" Font-Italic="True" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label14" runat="server" ForeColor="Black" Text="List of Inventory Items "
                                                        Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel3" runat="server" Width="100%">
                                                        <table width="100%" style="font-style: italic">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblclaat" runat="server" Text="Inventory Category :" Font-Size="14px"></asp:Label>
                                                                    <asp:Label ID="lblinvcateg" runat="server" Font-Size="14px" Font-Italic="true"></asp:Label>,
                                                                    <asp:Label ID="lblcma" runat="server" Text="Inventory Sub Category :" Font-Size="14px"> </asp:Label>
                                                                    <asp:Label ID="lblinvsubcate" runat="server" Font-Size="14px" Font-Italic="true"></asp:Label>,
                                                                    <asp:Label ID="Label2" runat="server" Font-Size="14px" Text="Inventory Sub Sub Category :"></asp:Label>
                                                                    <asp:Label ID="lblinvsubsubcate" runat="server" Font-Size="14px" Font-Italic="true"></asp:Label>,
                                                                    <asp:Label ID="Label4" runat="server" Font-Size="14px" Text="Inventory Name :"></asp:Label>
                                                                    <asp:Label ID="lblinvprod" runat="server" Font-Size="14px" Font-Italic="true"></asp:Label>,
                                                                    <asp:Label ID="Label3" runat="server" Font-Size="14px" Text="Inventory Status :"></asp:Label>
                                                                    <asp:Label ID="lblinvstat" runat="server" Font-Size="14px" Font-Italic="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel4" runat="server" Width="100%" Visible="false">
                                                        <table width="100%" style="font-style: italic">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="13px" Text="Name :"></asp:Label>
                                                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="13px"></asp:Label>
                                                                    &nbsp;
                                                                    <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="Smaller" Text="Inventory Status :"></asp:Label>
                                                                    <asp:Label ID="lblinvstatus123" runat="server" Font-Bold="True" Font-Size="Smaller"
                                                                        Font-Italic="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="InventoryMasterId"
                                            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                            AllowSorting="True" OnSorting="GridView1_Sorting" EmptyDataText="No Record Found."
                                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            GridLines="Both" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Business Name" SortExpression="wname" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStore" runat="server" Text='<%#Bind("wname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category : Sub Category : Sub Sub Category" SortExpression="CateAndName"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCategory" runat="server" Text='<%#Bind("CateAndName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inventory Name" SortExpression="InventoryMasterName"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <a href='Inventoryprofile.aspx?Invmid=<%# Eval("InventoryMasterId")%>' target="_blank">
                                                            <asp:Label ID="lblInvName" runat="server" Text='<%#Bind("InventoryMasterName") %>'
                                                                ForeColor="Black"></asp:Label>
                                                        </a>
                                                        <asp:Label ID="lblinvMasterId" runat="server" Text='<%#Bind("InventoryMasterId") %>'
                                                            Visible="false">
                                                        </asp:Label>
                                                        <asp:Label ID="lblInvWHMasterId" runat="server" Text='<%#Bind("InventoryWarehouseMasterId") %>'
                                                            Visible="false">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Barcode" SortExpression="Barcode" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbarcode" runat="server" Text='<%# Bind("Barcode")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product No." SortExpression="ProductNo" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductNo" runat="server" Text='<%#Bind("ProductNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Weight/Unit" SortExpression="Weight" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblweight" runat="server" Text='<%# Bind("Weight")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit" Visible="false" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblunit" runat="server" Text='<%# Bind("Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQtyOnHand" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" SortExpression="Statuslabel"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%-- <asp:CheckBox Enabled="false" ID="chkActive" runat="server" Checked='<%# Bind("MasterActiveStatus") %>' />--%>
                                                        <asp:Label ID="lblstatusoflabel" runat="server" Text='<%# Bind("Statuslabel")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="InventoryMasterId" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInventoryWarehouseMasterId" runat="server" Text='<%#Bind("InventoryMasterId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:ButtonField CommandName="ed" Text="Edit" HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif"
                                                    ButtonType="Image" ImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:ButtonField>
                                                <%--  <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("InventoryMasterId") %>'
                                                        CommandName="ed" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
