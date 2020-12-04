<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="BusinessMoreDetail.aspx.cs" Inherits="BusinessDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
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
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
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
                    <div style="float: right;">
                        <asp:Button ID="btnadddd" runat="server" Text="Add New Business Profile" OnClick="btnadddd_Click"
                            Width="180px" CssClass="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel runat="server" ID="pnladdd" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        Select Business
                                        <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStore"
                                            SetFocusOnError="true" ValidationGroup="save" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:DropDownList ID="ddlStore" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        Business Category
                                    </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:DropDownList ID="ddlcategory" runat="server" AutoPostBack="true" Width="200px"
                                            OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="ddlsubcategory" runat="server" AutoPostBack="true" Width="200px"
                                            OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="ddlsubsubcategory" runat="server" AutoPostBack="true" Width="200px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        In Business Since
                                        <asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtnoofhours"
                                            ValidationGroup="save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtnoofhours" Width="55px" runat="server" MaxLength="4"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                            TargetControlID="txtnoofhours" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                    <label>
                                        (eg : year 1999)
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        No of Employees
                                        <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtnoofhours"
                                            ValidationGroup="save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtnoemp" Width="55px" runat="server" MaxLength="4"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            TargetControlID="txtnoemp" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" valign="top">
                                    <label>
                                        <asp:Label ID="lblMission" runat="server" Text="Title "></asp:Label>
                                        <br />
                                        <asp:Label ID="Label60" runat="server" Font-Bold="false" Text="(Here give title to your profile in 200 chracters or less.)"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label67" runat="server" Font-Bold="false" ForeColor="Red"></asp:Label>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <cc2:HtmlEditor ID="txtmission" runat="server" Height="300px" Width="500px" />
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" valign="top">
                                    <label>
                                        <asp:Label ID="lblkeyhighlight" runat="server" Text="Key Highlights"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label5" runat="server" Font-Bold="false" Text="(Here write major attractive point about your business ideally as a bullet line  in 1000 characters or less.)"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label7" runat="server" Font-Bold="false" ForeColor="Red"></asp:Label>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <cc2:HtmlEditor ID="txthighlights" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" valign="top">
                                    <label>
                                        <asp:Label ID="lblcorporate" runat="server" Text="Corporate info"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label8" runat="server" Font-Bold="false" Text="(Here give information about your business in 1000 characters or less.)"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label9" runat="server" Font-Bold="false" ForeColor="Red"></asp:Label>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <cc2:HtmlEditor ID="txtcorporate" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" valign="top">
                                    <label>
                                        <asp:Label ID="lblfacts" runat="server" Text="Facts"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label10" runat="server" Font-Bold="false" Text="(Here write about the major facts about your business like market share, rise of business etc. in 1000 characters or less.)"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label11" runat="server" Font-Bold="false" ForeColor="Red"></asp:Label>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <cc2:HtmlEditor ID="txtfacts" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" valign="top">
                                    <label>
                                        Upload Images
                                    </label>
                                </td>
                                <td valign="middle" width="35%">
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <asp:Button ID="imgBtnImageUpdate" Text="Upload" runat="server" OnClick="imgBtnImageUpdate_Click" />
                                </td>
                                <td align="left" valign="top">
                                    <asp:Panel ID="panel1" runat="server" Height="120px" ScrollBars="Vertical" Width="150px"
                                        Visible="false">
                                        <asp:GridView ID="GridView5" runat="server" EmptyDataText="No Record Found." CssClass="mGrid"
                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="Both"
                                            AutoGenerateColumns="False" Width="100%" OnRowCommand="GridView5_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="Image " HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" Height="60px" Width="70px" ImageUrl='<%# Eval("image1") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:ButtonField ButtonType="Image" HeaderImageUrl="~/Account/images/delete.gif"
                                                    ImageUrl="~/Account/images/delete.gif" HeaderText="Delete" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%" CommandName="del"></asp:ButtonField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                    <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                </td>
                                <td>
                                    <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Submit" ValidationGroup="save"
                                        CssClass="btnSubmit" />
                                    <asp:Button ID="Button3" runat="server" Text="Update" ValidationGroup="save" CssClass="btnSubmit"
                                        Visible="false" OnClick="Button3_Click" />
                                    <asp:Button ID="btncancel" runat="server" OnClick="btncancel_Click" Text="Cancel"
                                        CssClass="btnSubmit" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label4" runat="server" Text="List of Business Profile"></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            OnClick="Button1_Click1" />
                        <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel runat="server" Visible="false">
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        Business Category
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlstatus_search" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstatus_search_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
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
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Business Profile"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="GVC" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                    Width="100%" EmptyDataText="No Record Found." GridLines="Both" PageSize="10"
                                                    OnRowEditing="GVC_RowEditing" OnPageIndexChanging="GVC_PageIndexChanging" OnRowDeleting="GVC_RowDeleting"
                                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    OnRowCommand="GVC_RowCommand" onsorting="GVC_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business Name" SortExpression="Wname" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbusname" runat="server" Text='<%# Bind("Wname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Business Category" SortExpression="Category" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="20%" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbuscate" runat="server" Text='<%# Bind("Category")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Since" HeaderStyle-Width="7%" SortExpression="year"
                                                            HeaderStyle-HorizontalAlign="left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsince" runat="server" Text='<%# Eval("year") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Title" HeaderStyle-Width="20%" SortExpression="Title"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldetails" runat="server" Text='<%# Eval("Title") %>' ToolTip='<%# Eval("Title1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Key Highlights" HeaderStyle-Width="20%" SortExpression="kee"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblkey" runat="server" Text='<%# Eval("kee") %>' ToolTip='<%# Eval("kee1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Corporate info" HeaderStyle-Width="20%" SortExpression="Corporate"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcorporate" runat="server" Text='<%# Eval("Corporate") %>' ToolTip='<%# Eval("Corporate1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Facts" HeaderStyle-Width="20%" SortExpression="Facts"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfacts" runat="server" Text='<%# Eval("Facts") %>' ToolTip='<%# Eval("Facts1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton49" ImageUrl="~/Account/images/edit.gif" runat="server"
                                                                    ToolTip="Edit" CommandArgument='<%# Eval("ID") %>' CommandName="Edit" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                    ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                                    OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                            </ItemTemplate>
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
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnImageUpdate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
