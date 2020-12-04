<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="TaxablebenifitMaster.aspx.cs" Inherits="TaxablebenifitMaster" %>

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
        .style2
        {
            height: 24px;
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


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }




        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text="" Visible="False" />
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label5" runat="server" Text="Taxable benefits Master Setup"></asp:Label></legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Business Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlstrname"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlstrname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstrname_SelectedIndexChanged1">
                                    </asp:DropDownList>
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Taxable benefits name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttaxablename"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpionValidator2" runat="server" ErrorMessage="*"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9 \s]*)"
                                        ControlToValidate="txttaxablename" ValidationGroup="1"> </asp:RegularExpressionValidator></label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txttaxablename" runat="server" Width="300px" MaxLength="50" onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!@#$%^'&*>+:;={}[]()|\/]/g,/^[\a-zA-Z.0-9_\s]+$/,'div3',50)"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="div3" class="labelcount">50</span>
                                    <asp:Label ID="Label10" runat="server" Text="(A-Z 0-9 _ .)" CssClass="labelcount"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="Linked to which account for debit"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlaccdebit"
                                        ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="true"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:LinkButton ID="lbllnk" runat="server" Text="Help" ForeColor="Black" OnClick="lbllnk_Click"></asp:LinkButton>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlaccdebit" runat="server" Width="400px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="Linked to which account for credit"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlacccredit"
                                        ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="true"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:LinkButton ID="lnk2" runat="server" Text="Help" ForeColor="Black" OnClick="lnk2_Click"></asp:LinkButton>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlacccredit" runat="server" Width="400px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label12" runat="server" Text="Status"></asp:Label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlstatus" runat="server" Width="100px">
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
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btnsubmit_Click"
                                    ValidationGroup="1" />
                                <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btnSubmit" OnClick="btnupdate_Click"
                                    Visible="False" ValidationGroup="1" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="btncancel_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label4" runat="server" Text="List of Taxable benefits"></asp:Label></legend>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="Button1" CssClass="btnSubmit" runat="server" Text="Printable Version"
                                OnClick="Button1_Click" />
                        </label>
                        <label>
                            <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" visible="False" />
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label9" runat="server" Text="Filter Business Name"></asp:Label>
                    </label>
                    <label>
                        <asp:DropDownList ID="ddlfilterbus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfilterbus_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label13" runat="server" Text="Status"></asp:Label>
                    </label>
                    <label>
                        <asp:DropDownList ID="ddlfilterstatus" runat="server" Width="100px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlfilterstatus_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="1" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr align="center">
                                <td class="style2">
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label7" runat="server" Font-Size="18px" Text="List of Taxable benefits"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                        CellPadding="4" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        Width="100%" OnRowEditing="GridView1_RowEditing" AllowSorting="True" OnSorting="GridView1_Sorting"
                                        OnRowCommand="GridView1_RowCommand" EmptyDataText="No Record Founds" OnRowDeleting="GridView1_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business Name" ItemStyle-Width="15%" SortExpression="Wname"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbname" runat="server" Text='<%# Eval("Wname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Taxable benefits Name" ItemStyle-Width="20%" SortExpression="Taxablebenifitname"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltaxbname" runat="server" Text='<%# Eval("Taxablebenifitname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Debit Account" ItemStyle-Width="20%" SortExpression="accdebit"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluni" runat="server" Text='<%# Eval("accdebit") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit Account" ItemStyle-Width="20%" SortExpression="acccredit"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblunionrec" runat="server" Text='<%# Eval("acccredit") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-Width="4%" SortExpression="Status"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblst" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowSelectButton="true" HeaderImageUrl="~/Account/images/edit.gif"
                                                HeaderText="Edit" ButtonType="Image" SelectImageUrl="~/Account/images/edit.gif"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left">
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                        <asp:Panel ID="pnlvend" runat="server" Width="60%" CssClass="modalPopup">
                            <fieldset>
                                <legend></legend>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="lbladdventext" runat="server" Text="For eg. when company is allowing free use of company vehicle for personal use of employee, the account to be debited is Employee benefits A/c and the account to be credited is Vehicle expense. Because when the actual expense is incurred the company would debit Vehicle expense and credit cash/bank. Now it is being reallocated to Employees benefits exp so account for the portion of the expense which is attributable to employee benefits is taken out of Vehicle exp. A/c."></asp:Label></label>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnvc" CssClass="btnSubmit" runat="server" Text="Close" />
                                        </td>
                                    </tr>
                                </table>
                        </asp:Panel>
                        <asp:Button ID="btnven" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="pnlvend" TargetControlID="btnven" CancelControlID="btnvc">
                        </cc1:ModalPopupExtender>
                    </div>
                </fieldset>
                <asp:HiddenField ID="hfidwhid" runat="server" />
                <asp:HiddenField ID="hfempid" runat="server" />
                <asp:HiddenField ID="hfbmid" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
