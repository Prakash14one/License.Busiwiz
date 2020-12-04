<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="InventoryCategoryMaster.aspx.cs" Inherits="InventoryCategoryMaster"
    EnableEventValidation="true" Title="Untitled Page" %>

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
   
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="statuslable" runat="server" ForeColor="Red" Text="">                            
                    </asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Add a New Category For Your Inventory"
                            Font-Bold="True" Visible="False"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add Category" CssClass="btnSubmit" OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Add Category"></asp:Label>
                            <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtInventoryCatName"
                                ErrorMessage="*" ValidationGroup="1">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                ControlToValidate="txtInventoryCatName" ValidationGroup="1">
                            </asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtInventoryCatName" runat="server" MaxLength="25" Width="225px"
                                onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',25)">
                            </asp:TextBox>
                            <asp:Label ID="Label12" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="div1" class="labelcount">25</span>
                            <asp:Label ID="lbljshg" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label13" runat="server" Text="Category Type"></asp:Label>
                            <asp:DropDownList ID="ddlcattype" runat="server" Width="120px">
                                <asp:ListItem Selected="True" Value="1">Inventory</asp:ListItem>
                                <asp:ListItem Value="0">Service</asp:ListItem>
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label4" runat="server" Text="Status"></asp:Label>
                            <asp:DropDownList ID="ddlstatus" runat="server" Width="120px">
                                <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                            </asp:DropDownList>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <asp:Button ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" Text="Submit"
                            ValidationGroup="1" CssClass="btnSubmit" />
                        <asp:Button ID="btnupdate" runat="server" Text="Update" Visible="false" ValidationGroup="1"
                            CssClass="btnSubmit" OnClick="btnupdate_Click" />
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label5" runat="server" Text="List of Inventory Main Categories" Font-Bold="true"></asp:Label>
                    </legend>
                    <div style="clear: both;">
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button1_Click1" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" class="btnSubmit" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label6" runat="server" Text="Filter by status"></asp:Label>
                        <asp:DropDownList ID="ddlActive" runat="server" Width="120px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlActive_SelectedIndexChanged">
                            <asp:ListItem Text="All"></asp:ListItem>
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Size="20px" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label2" runat="server" Text="List of Inventory Main Categories" Font-Size="18px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label7" runat="server" Text="Status :" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                    <asp:Label ID="lblstatus" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" DataKeyNames="InventeroyCatId" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" GridLines="Both" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowCommand="GridView1_RowCommand"
                                        OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                                        OnSorting="GridView1_Sorting" Width="100%" EmptyDataText="No Record Found.">
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Inventory Main Category" SortExpression="InventoryCatName"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="90%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCatName" runat="server" Text='<%#Eval("InventoryCatName") %>'></asp:Label>
                                                    <asp:Label ID="lblCatId" runat="server" Text='<%#Eval("InventeroyCatId") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category Type" SortExpression="InventoryCatName" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="90%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcattype" runat="server" Text='<%#Eval("CatType1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%" SortExpression="Statuslabel"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" Enabled="false" Checked='<%#Eval("Activestatus") %>'
                                                        Visible="false" />
                                                    <asp:Label ID="lbllabelstatus" runat="server" Text='<%#Eval("Statuslabel") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("InventeroyCatId") %>'
                                                        CommandName="Edit" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Btn" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="2%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </cc11:PagingGridView>
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                </fieldset>
            </div>
            <div style="clear: both;">
            </div>
            <asp:Panel ID="Panel3" runat="server" Height="310px" Width="60%" ScrollBars="None">
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel1" runat="server" Height="310px" Width="60%" ScrollBars="None">
                                <div style="background-color: White">
                                    <asp:Panel ID="pnlof" runat="server" CssClass="modalPopup" ScrollBars="None">
                                        <fieldset style="border: 1px solid white;">
                                            <div style="float: right;">
                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                    OnClick="ImageButton3_Click" Width="16px" />
                                            </div>
                                            <div style="clear: both">
                                            </div>
                                            <legend style="color: Black">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/ShoppingCart/images/Error.png" />
                                            </legend>
                                            <label>
                                                <asp:Label ID="Label1" runat="server" Text="Attention:"></asp:Label>
                                                <br />
                                                <asp:Label ID="Label8" runat="server" Text="Move Category"></asp:Label>
                                                <br />
                                                <asp:Label ID="Label9" runat="server" Text="This category contains"></asp:Label>
                                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                <asp:Label ID="Label10" runat="server" Text="sub categories. Please move them before deleting.">
                                
                                
                                                </asp:Label>
                                            </label>
                                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Width="100%" Height="300px">
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="InventorySubCatId"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="InventoryCatName" ItemStyle-Width="30%" HeaderText="Category"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="InventorySubCatName" ItemStyle-Width="30%" HeaderText="Sub Category"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                        <asp:TemplateField HeaderText="Change Category To:" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("InventorySubCatName") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="DropDownList2" runat="server" Width="300px">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                            <div style="clear: both">
                                            </div>
                                            <div style="text-align: center">
                                                <asp:Button ID="Button3" runat="server" OnClick="ImgBtnMove_Click" Text="Move" CssClass="btnSubmit" />
                                                <asp:Button ID="Button4" runat="server" Text="Cancel" CssClass="btnSubmit" />
                                            </div>
                                        </fieldset>
                                    </asp:Panel>
                                </div>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="Button4"
                                    BackgroundCssClass="modalBackground" PopupControlID="Panel1" TargetControlID="HiddenButton">
                                </cc1:ModalPopupExtender>
                                <asp:Button ID="HiddenButton" runat="server" OnClick="HiddenButton_Click" Style="display: none" />
                                <input id="DeleteStatus" runat="server" style="width: 16px" type="hidden" value="0" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="clear: both;">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
