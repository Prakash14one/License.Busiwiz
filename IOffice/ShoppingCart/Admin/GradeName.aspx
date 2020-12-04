<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="GradeName.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
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
    </script>
    <script language="javascript" type="text/javascript">
        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                //  alert("You have entered an invalid character");
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');
                //   alert("You have entered an invalid character");
            }

            counter = document.getElementById(id);

            if (txt1.value.length <= max_len) {
                remaining_characters = max_len - txt1.value.length;
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
        .style1
        {
            height: 53px;
        }
    </style>
    <div class="products_box">
        <div style="padding-left: 1%">
            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
        <div style="clear: both;">
        </div>
        <fieldset>
            <legend>
                <asp:Label ID="lbllegend" runat="server" Text=""></asp:Label>
            </legend>
            <div style="float: right; margin: 5px;">
                <asp:Button ID="btnadddiv" runat="server" Text="Add Grade" Width="73px" Height="26px"
                    OnClick="btnadddiv_Click" />
            </div>
            <asp:Panel runat="server" ID="pnladdd" Visible="false">
                <table id="content" border="0" runat="server" width="96%" style="margin: 10px;">
                    <tr align="left">
                        <td width="25%">
                            <label>
                                School Name
                                <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            </label>
                        </td>
                        <td valign="bottom">
                            <asp:DropDownList ID="ddlschool" runat="server" 
                                OnSelectedIndexChanged="ddlschool_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr align="left">
                        <td width="25%">
                            <label>
                                Grade Name
                                <asp:Label ID="Label1" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="redsd" runat="server" ControlToValidate="txtGrade"
                                    SetFocusOnError="true" ValidationGroup="save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                    SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtGrade"
                                    ValidationGroup="save"></asp:RegularExpressionValidator>
                                <%--        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtGrade"
                                                ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"  ValidationGroup="save"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:TextBox ID="txtGrade" runat="server" onKeydown="return mask(event)" Width="200px"
                                    onkeyup="return check(this,/[\\/!._@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span2',25)"
                                    TabIndex="5" OnTextChanged="txtGrade_TextChanged" MaxLength="25"></asp:TextBox>
                            </label>
                            <label>
                                <asp:Label ID="Label5" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                <span id="Span2" cssclass="labelcount">25</span>
                                <asp:Label ID="Label6" runat="server" CssClass="labelcount" Text="(A-Z 0-9)"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <label>
                                Status</label>
                        </td>
                        <td valign="bottom">
                            <asp:DropDownList ID="ddlActiveInactive" runat="server" 
                                OnSelectedIndexChanged="ddlActiveInactive_SelectedIndexChanged">
                                <asp:ListItem Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <label style="width: 350px;">
                                Take me to add sections after adding this grade</label>
                        </td>
                        <td>
                            <%--        <asp:CheckBox ID="chkNavigation" runat="server" Text="" 
        ForeColor="#006699" oncheckedchanged="chkNavigation_CheckedChanged" />--%>
                            <asp:CheckBox ID="chkNavigation" runat="server" Text="" ForeColor="#006699" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Submit" Width="73px" ForeColor="Black"
                                ValidationGroup="save" OnClick="btnSave_Click" Height="26px" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="73px" ForeColor="Black"
                                Height="26px" OnClick="btnUpdate_Click" Visible="False" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="73px" ForeColor="Black"
                                Height="26px" OnClick="btnCancel_Click" Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
        <fieldset>
            <legend>
                <asp:Label ID="Label4" runat="server" Text="Grade List"></asp:Label>
            </legend>
            <div id="divgrid" runat="server" visible="true" style="margin: 10px;">
                <div style="float: right">
                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                        OnClick="Button1_Click1" />
                    <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                        style="width: 51px;" type="button" value="Print" visible="false" />
                </div>
                <table>
                    <tr>
                        <td>
                            <label>
                                Filter by School
                            </label>
                            <label>
                                <asp:DropDownList ID="filterschool" runat="server" Width="180px" AutoPostBack="true"
                                    OnSelectedIndexChanged="filterschool_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                        <td>
                            <label>
                                Filter by Status
                            </label>
                            <label>
                                <asp:DropDownList ID="ddlfilter" runat="server" AutoPostBack="true" 
                                OnSelectedIndexChanged="ddlfilter_SelectedIndexChanged" Width="120px">
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <div id="mydiv" class="closed">
                                                <table width="850Px">
                                                    <tr align="center">
                                                        <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                            <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Grades"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                                                    <td align="left" style="text-align: left; font-size: 14px;">
                                                        <asp:Label ID="Label2" runat="server" Font-Italic="true" Text="Status : "></asp:Label>
                                                        <asp:Label ID="lblstat" runat="server" Font-Italic="true"></asp:Label>
                                                    </td>
                                                </tr>--%>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" EnableModelValidation="True"
                                                Width="100%" ForeColor="#333333" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                AllowPaging="True" AllowSorting="True" DataKeyNames="Id" EmptyDataText="No Record Found"
                                                AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting"
                                                OnRowEditing="GridView1_RowEditing" OnPageIndexChanging="GridView1_PageIndexChanging">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="School Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="80%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Labelid" runat="server" Text='<%# Bind("school") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Grade Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Labelg" runat="server" Text='<%# Bind("GradeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelS" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/UserControl/images/edit.gif"
                                                        HeaderStyle-HorizontalAlign="left" HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton49" ImageUrl="~/Account/UserControl/images/edit.gif"
                                                                runat="server" ToolTip="Edit" CommandArgument='<%# Eval("Id") %>' CommandName="Edit" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="left" Width="2%"></HeaderStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                        HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                                ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/UserControl/images/delete.gif"
                                                                OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="left" Width="2%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</asp:Content>
