<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="OfficePhysicalfileView.aspx.cs" Inherits="ShoppingCart_Admin_DocumentSubSubType"
    Title="Office Physical file: Add Manage" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
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
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= Panel2.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1000px,height=1000px,toolbar=1,scrollbars=0,status=0');
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
    </script>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
             <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
            </div>
            <div class="products_box">              
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="List of Office Document Type"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <div style="float: right;">
                                    <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                        OnClick="Button2_Click" />
                                    <input id="Button1" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        class="btnSubmit" type="button" value="Print" visible="false" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 15%">
                                            <label>
                                                <asp:Label ID="Label12" runat="server" Text="Business Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 15%">
                                            <label>
                                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td style="width: 15%">
                                            <label>
                                                <asp:Label ID="Label22" runat="server" Text="Party Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 15%">
                                            <label>
                                                <asp:DropDownList ID="ddlpartyfilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpartyfilter_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%">
                                            <label>
                                                <asp:Label ID="Label13" runat="server" Text="Cabinet Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 15%">
                                            <label>
                                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                                                    CausesValidation="True">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td style="width: 15%">
                                            <label>
                                                <asp:Label ID="Label14" runat="server" Text="Drawer Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 15%">
                                            <label>
                                                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                                                    CausesValidation="True">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%">
                                            <label>
                                                <asp:Label ID="Label27" runat="server" Text="Type"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 15%">
                                            <label>
                                                <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                                                    <asp:ListItem Text="All" Selected="True" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="Books" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Manual" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Pen Drive" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Periodicals/Megazines" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Physical File" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Software CD/DVD" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td style="width: 15%">
                                            <label>
                                                <asp:Label ID="Label28" runat="server" Text="Search"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 15%">
                                            <label>
                                                <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel2" runat="server">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblBusiness0" runat="server" Text="" Font-Italic="True" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="Label15" runat="server" Text="Business : " Font-Italic="True"></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="True" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label1" runat="server" Font-Italic="True" Text="List of Office Document Files"
                                                                    ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label25" runat="server" Text="Party Name : "></asp:Label>
                                                                <asp:Label ID="lblpartyy" runat="server"></asp:Label>
                                                                ,
                                                                <asp:Label ID="Label2" runat="server" Text="Cabinet : "></asp:Label>
                                                                <asp:Label ID="lblCabinet" runat="server"></asp:Label>
                                                                ,
                                                                <asp:Label ID="Label3" runat="server" Text="Drawer : "></asp:Label>
                                                                <asp:Label ID="lblDrawer" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cc11:PagingGridView ID="gridocsubsubtype" runat="server" BorderWidth="1px" Width="100%"
                                                    AutoGenerateColumns="False" DataKeyNames="DocumentTypeId" OnRowCancelingEdit="gridocsubsubtype_RowCancelingEdit"
                                                    OnRowCommand="gridocsubsubtype_RowCommand" OnRowDeleting="gridocsubsubtype_RowDeleting"
                                                    OnRowEditing="gridocsubsubtype_RowEditing" EmptyDataText="No Record Found." CssClass="mGrid"
                                                    GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    AllowSorting="True" OnPageIndexChanging="gridocsubsubtype_PageIndexChanging"
                                                    OnSorting="gridocsubsubtype_Sorting" PageSize="20">
                                                    <PagerSettings Mode="NumericFirstLast" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblwname" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cabinet" SortExpression="DocumentMainType" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocumentMainType" runat="server" Text='<%# Eval("DocumentMainType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Drawer" SortExpression="DocumentSubType" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocumentSubType" runat="server" Text='<%# Eval("DocumentSubType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="File/Book/Electronic Media" SortExpression="DocumentType"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="35%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocumentType" runat="server" Text='<%# Eval("DocumentType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldatesdsd" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" HeaderStyle-Width="8%" SortExpression="Status"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Labessddsdl6" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("DocumentTypeId") %>'
                                                                    CommandName="Edit" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" CausesValidation="false"
                                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </cc11:PagingGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
