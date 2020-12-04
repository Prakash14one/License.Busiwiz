<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="partyautoallocationofmanagers.aspx.cs" Inherits="ShoppingCart_Admin_partyautoallocationnew"
    Title="partyautoallocationofmanagers" %>

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
            <div style="padding-left: 1%">
                <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladdlabel" runat="server" Text="Add Support Team" Visible="False"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" CssClass="btnSubmit" Text="Add Support Team"
                            OnClick="btnadd_Click"></asp:Button>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false" Width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <label>
                                        <asp:Label ID="Label11" runat="server" Text="Select Business"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="ddlSearchByStore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchByStore_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text="Select Support Team For"></asp:Label>
                                    </label>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Selected="True">City</asp:ListItem>
                                        <asp:ListItem Value="2">State</asp:ListItem>
                                        <asp:ListItem Value="3">Country</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    <asp:Panel ID="Panel1" runat="server" Width="100%" Visible="true">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 10%">
                                                    <label>
                                                        <asp:Label ID="lblCountry" runat="server" Text="Country " Visible="False"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 23%">
                                                    <label>
                                                        <asp:DropDownList ID="ddlselectcountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlselectcountry_SelectedIndexChanged"
                                                            Visible="False">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                                <td style="width: 10%">
                                                    <label>
                                                        <asp:Label ID="lblstate" runat="server" Text="State  " Visible="False"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 23%">
                                                    <label>
                                                        <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"
                                                            Visible="False">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                                <td style="width: 10%">
                                                    <label>
                                                        <asp:Label ID="lblcity" runat="server" Text="City  " Visible="False"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 23%">
                                                    <label>
                                                        <asp:DropDownList ID="ddlcity" runat="server" Visible="False">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label13" runat="server" Text="Assign Account Manager"></asp:Label>
                                                    <asp:Label ID="Label14" runat="server" Text="*"></asp:Label>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAssignedAccountManager"
                                                        ErrorMessage="*" InitialValue="0" ValidationGroup="2"></asp:RequiredFieldValidator>--%>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlAssignedAccountManager" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label15" runat="server" Text="Assign Purchase Dept. In Charge"></asp:Label>
                                                    <asp:Label ID="Label23" runat="server" Text="*"></asp:Label>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAssignedPurchseDept"
                                                        ErrorMessage="*" InitialValue="0" ValidationGroup="2"></asp:RequiredFieldValidator>--%>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlAssignedPurchseDept" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label16" runat="server" Text="Assign Sales Dept. In Charge"></asp:Label>
                                                    <asp:Label ID="Label17" runat="server" Text="*"></asp:Label>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAssignedSalesDept"
                                                        ErrorMessage="*" InitialValue="0" ValidationGroup="2"></asp:RequiredFieldValidator>--%>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlAssignedSalesDept" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%" valign="top">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label18" runat="server" Text="Assign Receiving Dept. In Charge"></asp:Label>
                                                    <asp:Label ID="Label19" runat="server" Text="*"></asp:Label>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlAssignedRecievingDept"
                                                        ErrorMessage="*" InitialValue="0" ValidationGroup="2"></asp:RequiredFieldValidator>--%>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlAssignedRecievingDept" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label20" runat="server" Text="Assign Shipping Dept. In Charge"></asp:Label>
                                                    <asp:Label ID="Label22" runat="server" Text="*"></asp:Label>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAssignedShippingDept"
                                                        ErrorMessage="*" InitialValue="0" ValidationGroup="2"></asp:RequiredFieldValidator>--%>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlAssignedShippingDept" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="2">
                                    <asp:Button ID="ImageButton1" Text="Submit" runat="server" CssClass="btnSubmit "
                                        OnClick="ImageButton1_Click" ValidationGroup="2" />
                                    <asp:Button ID="Button3" Text="Update" runat="server" CssClass="btnSubmit " ValidationGroup="2"
                                        OnClick="Button3_Click" Visible="False" />
                                    <asp:Button ID="Button2" Text="Cancel" runat="server" CssClass="btnSubmit " CausesValidation="false"
                                        OnClick="Button2_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="List of Support Teams " Font-Bold="true"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            OnClick="Button1_Click" CausesValidation="False" />
                        <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <label>
                            <asp:Label ID="Label2" runat="server" Text="Filter by Business"></asp:Label>
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="lblcontry" runat="server" Text="Filter by Country"></asp:Label>
                            <asp:DropDownList ID="ddlcountry1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcountry1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label10" runat="server" Text="Filter by State"></asp:Label>
                            <asp:DropDownList ID="dllstate1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dllstate1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label24" runat="server" Text="Filter by City"></asp:Label>
                            <asp:DropDownList ID="dllcity1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dllcity1_SelectedIndexChanged1">
                            </asp:DropDownList>
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Italic="True" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label29" runat="server" Text="Business :" Font-Italic="True"></asp:Label>
                                                    <asp:Label ID="lblbusinessprint" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label25" runat="server" Text="List of Support Teams " Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td align="left" style="text-align: left; font-size: 16px;">
                                                    <asp:Label ID="Label26" runat="server" Text="Country :" Font-Italic="True"></asp:Label>
                                                    <asp:Label ID="lblcon" runat="server" Font-Italic="True"></asp:Label>
                                                    &nbsp;,
                                                    <asp:Label ID="Label27" runat="server" Text="State :" Font-Italic="True"></asp:Label>
                                                    <asp:Label ID="lblst" runat="server" Font-Italic="True"></asp:Label>
                                                    &nbsp;,
                                                    <asp:Label ID="Label28" runat="server" Text="City :" Font-Italic="True"></asp:Label>
                                                    <asp:Label ID="lblcityyyy" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        DataKeyNames="PartyAutoAllocationID" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                        OnRowCommand="GridView1_RowCommand" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" OnSorting="GridView1_Sorting" Width="100%"
                                        OnRowCreated="GridView1_RowCreated" EmptyDataText="No Record Found." OnPageIndexChanging="GridView1_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="Wname">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbusinessname123" runat="server" Text='<%# Bind("Wname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Country" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                SortExpression="Country">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("CountryName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="State" ItemStyle-HorizontalAlign="Left"
                                                SortExpression="State">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label21" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="City" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                SortExpression="City">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("CityName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Account Manager" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" SortExpression="AccountMgr">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("AccountMgr") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receiving Dept." ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" SortExpression="RcvDpt">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("RcvDpt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purchase Dept." ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="PrcDpt">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("PrcDpt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shipping Dept." ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="ShipDpt">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("ShipDpt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales Dept." ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="SalesDpt">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("SalesDpt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("PartyAutoAllocationID") %>'
                                                        CommandName="Editgrd" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgdelete" runat="server" CommandName="Delete1" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" CommandArgument='<%# Eval("PartyAutoAllocationID") %>'>
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc11:PagingGridView>
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
