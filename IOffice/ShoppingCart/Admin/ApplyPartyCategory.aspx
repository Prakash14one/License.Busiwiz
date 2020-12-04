<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="ApplyPartyCategory.aspx.cs" Inherits="PartyTypeDetail"
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
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <asp:Panel ID="Panel1" runat="server" Width="100%">
                    <div style="padding-left: 1%">
                        <label>
                            <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <fieldset>
                        <legend>
                            <asp:Label ID="lbllegend" runat="server" Text="Add New Customer Discount Category"
                                Font-Bold="True" Visible="False"></asp:Label>
                        </legend>
                        <div style="float: right;">
                            <asp:Button ID="btnadd" runat="server" Text="Add Customer Discount Category" CssClass="btnSubmit"
                                OnClick="btnadd_Click" />
                        </div>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="pnladd" runat="server" Visible="false">
                            <div>
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Select Business"></asp:Label>
                                    <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Select Customer Category"></asp:Label>
                                    <asp:DropDownList ID="ddlPartyType" runat="server" OnSelectedIndexChanged="ddlPartyType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="Select Party"></asp:Label>
                                    <asp:DropDownList ID="ddlpartyname" runat="server">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <br />
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:ImageButton ID="LinkButton97666667" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                OnClick="LinkButton97666667_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                            &nbsp;<asp:ImageButton ID="LinkButton13" runat="server" AlternateText="Refresh" Height="20px"
                                                ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton13_Click"
                                                ToolTip="Refresh" Width="20px" ImageAlign="Bottom" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="LinkButton13" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </label>
                                <br />
                                <asp:Button ID="imgbtnapp" runat="server" CssClass="btnSubmit" Text="Apply Discount"
                                    OnClick="imgbtnapp_Click" />
                                <asp:Button ID="Button5" runat="server" CssClass="btnSubmit" Text="Update Discount"
                                    OnClick="Button5_Click" Visible="False" />
                                <asp:Button ID="Button6" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="Button6_Click" />
                            </div>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label9" runat="server" Text="List of Customer Discount Categories"
                                Font-Bold="True"></asp:Label>
                        </legend>
                        <div style="clear: both;">
                        </div>
                        <div style="float: right;">
                            <asp:Button ID="imgBtnAddNewCat" runat="server" CssClass="btnSubmit" Text="Add New Category"
                                OnClick="imgBtnAddNewCat_Click" Visible="false" />
                            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click2" />
                            <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                style="width: 51px;" type="button" value="Print" visible="false" />
                        </div>
                        <div style="clear: both;">
                        </div>
                        <div>
                            <label>
                                <asp:Label ID="Label19" runat="server" Text="Filter by Business"></asp:Label>
                                <asp:DropDownList ID="ddlfilterbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbusiness_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="Label7" runat="server" Text="Filter by Customer Category"></asp:Label>
                                <asp:DropDownList ID="ddlcustomercat" runat="server" OnSelectedIndexChanged="ddlcustomercat_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                            <label>
                              <asp:Label ID="Label20" runat="server" Text="Filter by Party Name"></asp:Label>
                                <asp:DropDownList ID="ddlParty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlParty_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                            <br />
                            <asp:Button ID="imgbtngo" runat="server" CssClass="btnSubmit" Text="Go" OnClick="imgbtngo_Click"
                                ValidationGroup="10" />
                            <label>
                                <asp:Label ID="Label8" runat="server" Text="Search By Name/Phone No." Visible="false"></asp:Label>
                                <asp:TextBox ID="txtsearch" MaxLength="20" runat="server" Width="194px" onKeydown="return mask(event)"
                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',20)"
                                    Visible="false">
                                </asp:TextBox>
                                <%-- Characters Remaining--%>
                                <span id="div1" visible="false"></span>
                                <asp:Label ID="Label3" runat="server" Text="(A-Z,0-9,_)" Visible="false"></asp:Label>
                            </label>
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
                                                        <asp:Label ID="lblCompany" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                        <asp:Label ID="Label10" runat="server" Text="Business : " Font-Italic="true" Font-Size="20px"></asp:Label>
                                                        <asp:Label ID="lblBusiness" runat="server" Font-Size="20px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                        <asp:Label ID="Label11" runat="server" Text="List of Customer Discount Categories"
                                                            ForeColor="Black" Font-Size="18px" Font-Italic="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="text-align: center; font-size: 16px; ">
                                                        <asp:Label ID="Label21" runat="server" Text="Customer Category"
                                                             Font-Size="18px" Font-Italic="true">
                                                             </asp:Label>
                                                             <asp:Label ID="lblcutomercategoryprint" runat="server" 
                                                             Font-Size="18px" Font-Italic="true"></asp:Label>
                                                             &nbsp;
                                                             <asp:Label ID="Label22" runat="server" Text="Party Name"
                                                             Font-Size="18px" Font-Italic="true">
                                                             </asp:Label>
                                                             <asp:Label ID="lblpartynameprint" runat="server" 
                                                             Font-Size="18px" Font-Italic="true"></asp:Label>
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
                                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            DataKeyNames="PartyTypeDetailId" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting"
                                            OnRowEditing="GridView1_RowEditing" OnSorting="GridView1_Sorting" Width="100%" EmptyDataText="No Record Found.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Business Name" SortExpression="Partyname" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbusinessname" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Party Name" SortExpression="Partyname" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="35%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblparid" runat="server" Text='<%# Eval("PartyID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblpartypecatid" runat="server" Text='<%# Eval("partytypecatid") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblpartyname" runat="server" Text='<%# Eval("Partyname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Category" SortExpression="per" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldiscatid" runat="server" Text='<%# Bind("PartyTypeCategoryMasterId")%>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lbldiscountname" runat="server" Text='<%# Bind("per")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField HeaderText="Edit" ShowEditButton="True" HeaderStyle-HorizontalAlign="Left"
                                                    ButtonType="Image" ValidationGroup="qq" ItemStyle-HorizontalAlign="Left" EditImageUrl="~/Account/images/edit.gif"
                                                    HeaderImageUrl="~/Account/images/edit.gif" ItemStyle-Width="3%"></asp:CommandField>
                                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                    ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("PartyTypeDetailId") %>'
                                                            CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                            ToolTip="Delete" />
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
                    </fieldset>
                    <asp:Panel ID="Panel2" runat="server" BorderColor="Black" Width="296px" BorderStyle="Solid"
                        BackColor="#CCCCCC" Visible="false">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label12" runat="server" Text="Add New Party type Category"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                    OnClick="ImageButton3_Click" Width="15px" />
                                            </td>
                                        </tr>
                                    </table>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lblm" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label13" runat="server" Text="Business Name"></asp:Label>
                                                    <asp:Label ID="Label16" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFdValidator3" runat="server" ControlToValidate="ddlware"
                                                        ErrorMessage="*" InitialValue="0" SetFocusOnError="true" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlware" runat="server" Width="126px">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label14" runat="server" Text="Party Type Category"></asp:Label>
                                                    <asp:Label ID="Label17" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpartycategory"
                                                        ErrorMessage="*" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="txtpartycategory" runat="server" ValidationGroup="5" Width="120px"></asp:TextBox>
                                                </label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label15" runat="server" Text="Party Category Discount"></asp:Label>
                                                    <asp:Label ID="Label18" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtpartycategorydiscount"
                                                        ErrorMessage="*" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="txtpartycategorydiscount" runat="server" ValidationGroup="5" Width="120px"></asp:TextBox>
                                                </label>
                                            </td>
                                            <td>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                    TargetControlID="txtpartycategorydiscount" ValidChars="0123456789.">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="txtentrydate" runat="server" Visible="False" Width="120px"></asp:TextBox>
                                                </label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:CheckBox ID="chkIsper" runat="server" Text="IsPercentage" />
                                        <asp:CheckBox ID="chkActive" runat="server" Text="Active" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="Button2" CssClass="btnSubmit" runat="server" OnClick="Button2_Click1"
                                        Text="Submit" ValidationGroup="5" />
                                    <asp:Button ID="Button3" CssClass="btnSubmit" runat="server" OnClick="Button3_Click1"
                                        Text="Cancel" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <%-- <cc1:ModalPopupExtender ID="ModalPopupExtenderAddnew" runat="server" BackgroundCssClass="modalBackground"
                                                Enabled="True" PopupControlID="Panel2" TargetControlID="imgBtnAddNewCat">
                                            </cc1:ModalPopupExtender>--%>
                    <div style="clear: both;">
                    </div>
                    <cc1:CalendarExtender ID="txtpartycategory_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtentrydate">
                    </cc1:CalendarExtender>
                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Outset"
                        Width="300px">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="subinnertblfc">
                                    Confirm Delete
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" ForeColor="Black">You Sure , You Want to Delete !</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:ImageButton ID="Imabtn" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/Yes.png"
                                        OnClick="Imabtn_Click" />
                                    <asp:ImageButton ID="Imagee" runat="server" AlternateText="cancel" ImageUrl="~/ShoppingCart/images/No.png"
                                        OnClick="Imagee_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel3" TargetControlID="HiddenButton222">
                    </cc1:ModalPopupExtender>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
