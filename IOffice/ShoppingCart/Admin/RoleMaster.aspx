<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="RoleMaster.aspx.cs" Inherits="Add_Role_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= Panel6.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');

            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        function mask(evt) {
            counter = document.getElementById(id);
            alert(counter);
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
    <div class="products_box">
        <asp:UpdatePanel ID="Updaeelrt1" runat="server">
            <ContentTemplate>
                <div style="padding-left: 1%">
                    <asp:Label ID="Label2" ForeColor="Red" runat="server">
                    </asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" Text="Add New Website Access Rights Role" runat="server"
                            CssClass="btnSubmit" OnClick="btnadd_Click" />
                    </div>
                    <asp:Panel ID="pnladd" Visible="false" runat="server">
                        <label>
                            <asp:Label ID="Label1" runat="server" Text="Role Name"></asp:Label>
                            <asp:Label ID="Label8" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRole"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                ControlToValidate="txtRole" ValidationGroup="1"></asp:RegularExpressionValidator>
                            
                            <asp:TextBox ID="txtRole" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'div1',20)"></asp:TextBox>
                            <asp:Label ID="lblrole_id" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="div1" class="labelcount">20</span>
                            <asp:Label ID="Label7" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Status"></asp:Label>
                            <asp:DropDownList ID="ddlstatus" runat="server" Width="100px">
                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <%-- <asp:RadioButton ID="raActive" runat="server" Checked="True" GroupName="r1" Text="Active" />
                    <asp:RadioButton ID="raDeactive" runat="server" GroupName="r1" Text="InActive"
                        ValidationGroup="r" />--%>
                            <%--<asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True">Active</asp:ListItem>
                <asp:ListItem>InActive</asp:ListItem>
            </asp:RadioButtonList>--%>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Set up access rights for various parts and pages of this website for role (Recommended)"
                            TextAlign="Left" Checked="true" />
                        <div style="clear: both;">
                        </div>
                        <br />
                        <asp:Button ID="butSubmit" runat="server" OnClick="butSubmit_Click" Text="Submit"
                            CssClass="btnSubmit" ValidationGroup="1" />
                        <%--<asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button2"
                CssClass="btnSubmit" Text="Submit" />--%>
                        <asp:Button ID="butCancel" runat="server" CssClass="btnSubmit" OnClick="butCancel_Click"
                            Text="Cancel" />
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                        <%--<asp:Button runat="server" ID="Button3" CssClass="btnSubmit" Text="Cancel" />--%></asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label4" runat="server" Text="List of Role Names"></asp:Label></legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button1_Click1" />
                        <input id="Button2" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" type="button" value="Print" visible="false" />
                        <%--<asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button1"
                    CssClass="btnSubmit" Text="Print Version" />--%>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel6" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label6" runat="server" Text="List of Role Names"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" GridLines="Both" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" runat="server" AutoGenerateColumns="False"
                                        PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowEditing="GridView1_RowEditing"
                                        AllowPaging="True" AllowSorting="True" EmptyDataText="No Record Found." OnRowDeleting="GridView1_RowDeleting"
                                        Width="100%" OnSorting="GridView1_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Role Name" SortExpression="Role_name" ItemStyle-Width="85%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Role_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="ActiveDeactive" ItemStyle-Width="6%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="CheckBox1" runat="server" Text='<%# Eval("ActiveDeactive") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Manage Access Rights" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnmanage" Text="Manage Access Rights" CssClass="btnSubmit" runat="server"
                                                        CommandArgument='<%# Eval("Role_id") %>' CommandName="Manage" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-Width="3%" HeaderImageUrl="~/Account/images/edit.gif"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Eval("Role_id") %>'
                                                        CommandName="Edit" ImageUrl="~/Account/images/edit.gif" ToolTip="Edit" />
                                                    <%--<asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Role_id") %>'
                                                CommandName="Edit">Edit</asp:LinkButton>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="3%" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument='<%# Eval("Role_id") %>'
                                                        CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                        ToolTip="Delete" />
                                                    <%--<asp:LinkButton ID="llinkbb" runat="server" CommandArgument='<%# Eval("Role_id") %>'
                                            CommandName="Delete">Delete</asp:LinkButton>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Solid"
                                        Width="300px">
                                        <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" ForeColor="Black">You Sure , You want to Delete this Record?</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="ImageButton5" runat="server" Text="Yes" OnClick="yes_Click" />
                                                    <%--<asp:ImageButton ID="ImageButton5" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/Yes.png"
                                            OnClick="yes_Click" />--%>
                                                    <asp:Button ID="ImageButton6" runat="server" Text="No" OnClick="Button1_Click" />
                                                    <%--<asp:ImageButton ID="ImageButton6" runat="server" AlternateText="cancel" ImageUrl="~/ShoppingCart/images/No.png"
                                            OnClick="Button1_Click" />--%>
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
                    </asp:Panel>
                    <%--<asp:GridView ID="gvCustomres" runat="server" DataSourceID="customresDataSource"
            AutoGenerateColumns="False" GridLines="None" AllowPaging="true" CssClass="mGrid"
            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
            <Columns>
                <asp:BoundField DataField="ContactName" HeaderText="Role Name" />
                <asp:CheckBoxField DataField="Checkbox" HeaderText="Status" />
                <asp:HyperLinkField Text="Edit" HeaderText="Edit" />
                <asp:HyperLinkField Text="Delete" HeaderText="Delete" />
            </Columns>
        </asp:GridView>--%>
                    <%--<asp:XmlDataSource ID="customresDataSource" runat="server" DataFile="~/App_Data/data1.xml">
        </asp:XmlDataSource>--%>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--end of right content-->
    <div style="clear: both;">
    </div>
</asp:Content>
