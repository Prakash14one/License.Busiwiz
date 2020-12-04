<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="PolicyProcedureRuleCategory.aspx.cs" Inherits="ShoppingCart_Admin_PolicyProcedureRuleCategory"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
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
    <script type="text/javascript" language="javascript">
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
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
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
        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
            }
        }
        function mask(evt, max_len) {



            if (evt.keyCode == 13) {

                return true;
            }


            if (evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {

                alert("You have entered an invalid character");
                return false;
            }




        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="False" />
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllegend" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" CssClass="btnSubmit" Text="Add Policy/Procedure/Rule"
                            OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Pnladdnew" runat="server" Visible="false">
                        <table id="innertbl" width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="lblwname" runat="server" Text="Business Name "></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" ValidationGroup="1"
                                            Width="250px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Policy/Procedure/Rule"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtpolicycat"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-z().A-Z-,0-9\s]*)"
                                            ControlToValidate="txtpolicycat" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtpolicycat" MaxLength="150" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%^'_+@&*>:;={}[]|\/]/g,/^[\a-z().A-Z0-9,-\s]+$/,'div1',150)"
                                            runat="server" Width="350px" ValidationGroup="1"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label6" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1">150</span>
                                        <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 ) ( , . -)" CssClass="labelcount"></asp:Label>
                                    </label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                    <asp:Button ID="btnsubmit" runat="server" CssClass="btnSubmit" Text="Submit" ValidationGroup="1"
                                        OnClick="btnsubmit_Click" />
                                    <asp:Button ID="btnupdate" runat="server" CssClass="btnSubmit" Text="Update" OnClick="Update_Click"
                                        ValidationGroup="1" Visible="False" />
                                    <asp:Button ID="btncancel" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="btncancel_Click" />
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllist" Text="List of Policy/Procedure/Rule"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnprintableversion" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            OnClick="btnprintableversion_Click" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" class="btnSubmit" value="Print" visible="False" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label3" runat="server" Text="Business Name"></asp:Label>
                    </label>
                    <label>
                        <asp:DropDownList ID="ddlfilterbybusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbybusiness_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td colspan="2">
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblcmpny" Font-Italic="true" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="Label23" Font-Italic="true" Text="Business:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Italic="true" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label11" runat="server" Font-Italic="true" Text="List of Policy/Procedure/Rule"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <cc11:PagingGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="Id"
                                        Width="100%" EmptyDataText="No Record Found." OnRowCommand="GridView1_RowCommand"
                                        OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" AllowSorting="True"
                                        OnSorting="GridView1_Sorting" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging1">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Category ID" SortExpression="Id" ItemStyle-Width="8%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblid" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="WName" ItemStyle-Width="30%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbusinessname123" runat="server" Text='<%# Eval("WName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="30%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Policy/Procedure/Rule" SortExpression="Policyprocedurecategory"
                                                ItemStyle-Width="65%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpolicyprocedure123" runat="server" Text='<%# Eval("Policyprocedurecategory") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="65%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id") %>'
                                                        ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ToolTip="Delete"
                                                        CommandArgument='<%# Eval("Id") %>' ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <%-- <asp:ButtonField CommandName="Edit" HeaderText="Edit" Text="Edit" ButtonType="Image"
                                                HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif"
                                                ImageUrl="~/Account/images/edit.gif" ItemStyle-Width="2%"></asp:ButtonField>--%>
                                            <%--<asp:ButtonField CommandName="Delete" HeaderText="Delete" Text="Delete" ButtonType="Image"
                                                HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                ImageUrl="~/Account/images/delete.gif" ItemStyle-Width="3%"></asp:ButtonField>--%>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </cc11:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
