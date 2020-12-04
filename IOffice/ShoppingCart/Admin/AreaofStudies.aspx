<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="AreaofStudies.aspx.cs" Inherits="ShoppingCart_Admin_Default4"
    Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="lblmessage" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                </asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllegend" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button2" runat="server" Text="Add New Study" CssClass="btnSubmit"
                            OnClick="Button1_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                        <asp:Panel ID="Panel1" runat="server" Visible="False">
                            <table width="100%">
                                <tr>
                                    <td style="width: 25%">
                                    <label>
                                        <asp:Label ID="lblname" runat="server" Text="Study Name"></asp:Label>
                                        <asp:Label ID="Label34" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtname"
                                            SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                            ErrorMessage="Invalid Character" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                            ControlToValidate="txtname" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtname" runat="server" Width="300px" MaxLength="60" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div1',60)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label6" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="div1" cssclass="labelcount">60</span>
                                        <asp:Label ID="Label7" runat="server" Text="(A-Z 0-9 . , ? _)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        <asp:Label ID="lblstatus" runat="server" Text="Status"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkstatus" runat="server" Text="Active" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="Label8" runat="server" Text="Label" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" ValidationGroup="1"
                                            Text="Submit" CssClass="btnSubmit" />
                                        <asp:Button ID="btnupdate" runat="server" OnClick="btnupdate_Click" ValidationGroup="1"
                                            Text="Update" Visible="False" CssClass="btnSubmit" />
                                        <asp:Button ID="btncancel" runat="server" OnClick="btncancel_Click" Text="Cancel"
                                            CssClass="btnSubmit" />
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label18" runat="server" Text="List of Area of Studies"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnPrintVersion" runat="server" Text="Printable Version" OnClick="btnPrintVersion_Click"
                            CausesValidation="False" CssClass="btnSubmit" />
                        <input id="btnPrint" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" class="btnSubmit" />
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    Status
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlstatus_search" runat="server" Width="180px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlstatus_search_SelectedIndexChanged">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label3" runat="server" Font-Italic="true" Text="List of Area of Studies "></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="text-align: left; font-size: 14px;">
                                                                <asp:Label ID="Label1" runat="server" Font-Italic="true" Text="Status : "></asp:Label>
                                                                <asp:Label ID="lblstat" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                                    OnRowEditing="GridView1_RowEditing" AllowSorting="True" OnSorting="GridView1_Sorting"
                                                    Width="100%" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                    PageSize="10" lternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" CssClass="mGrid"
                                                    OnRowDeleting="GridView1_RowDeleting" EmptyDataText="No Record Found.">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Name" HeaderStyle-Width="80%" SortExpression="Name"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" HeaderStyle-Width="5%" SortExpression="Status"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Labesdsdl6" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                <%--<asp:CheckBox ID="CheckBox1" Enabled="false" runat="server" Checked='<%# Eval("Active") %>' />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-Width="2%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="Edit" ImageUrl="~/Account/images/edit.gif" ToolTip="Edit" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/ShoppingCart/images/trash.jpg" HeaderText="Delete"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                    ToolTip="Delete" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
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
