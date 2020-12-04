<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="LevelofEducationTBL.aspx.cs" Inherits="ShoppingCart_Admin_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="lblmsg" runat="server" Visible="False" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllegend" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnaddnew" runat="server" Text="Add New Level of Education" CssClass="btnSubmit"
                            OnClick="btnaddnew_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel4" runat="server" Visible="False">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        Area of Study
                                        <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlareaofstudy"
                                            ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlareaofstudy" runat="server" Width="250px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        Level of Education
                                        <asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtName"
                                            SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtName"
                                            ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtName" runat="server" MaxLength="60" onKeydown="return mask(event)"
                                            Width="250px" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'Span4',60)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label5" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="Span4" cssclass="labelcount">60</span>
                                        <asp:Label ID="Label8" runat="server" Text="(A-Z 0-9 . , ? _)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        Status
                                    </label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Active" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                </td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btnSubmit"
                                        Text="Submit" ValidationGroup="1" />
                                    <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btnSubmit" ValidationGroup="1"
                                        Visible="false" OnClick="btnupdate_Click" />
                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="btnSubmit"
                                        Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label18" runat="server" Text="List of Level of Education"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnprintv" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="btnprintv_Click" />
                        <input id="btnPrint" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" class="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    Area of Study
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlareaaaa" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlareaaaa_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
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
                            <td colspan="4" style="width: 1700px">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr align="center">
                                            <td colspan="5">
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="lblCompany" runat="server" Font-Italic="True" Text="Area of Study : "></asp:Label>
                                                                <asp:Label ID="Label3" runat="server" Font-Italic="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label1" runat="server" Font-Italic="true" Text="List of Level of Education"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="text-align: left; font-size: 14px;">
                                                                <asp:Label ID="Label7" runat="server" Font-Italic="true" Text="Status : "></asp:Label>
                                                                <asp:Label ID="lblstat" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    DataKeyNames="Id" EmptyDataText="No Record Found." OnRowEditing="GridView1_RowEditing"
                                                    OnSorting="GridView1_Sorting" Width="100%" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                    OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" AlternatingRowStyle-CssClass="alt"
                                                    PagerStyle-CssClass="pgr" CssClass="mGrid">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Area of Study" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="30%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Labedsal6" runat="server" Text='<%# Eval("AName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Level of Education" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="30%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("LName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Labesdsdl6" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id") %>'
                                                                    ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/ShoppingCart/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ToolTip="Delete"
                                                                    CommandArgument='<%# Eval("Id") %>' ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                                </asp:ImageButton>
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
