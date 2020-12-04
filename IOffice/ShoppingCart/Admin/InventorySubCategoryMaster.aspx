<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="InventorySubCategoryMaster.aspx.cs" Inherits="InventorySubCategoryMaster"
    Title="Untitled Page" %>

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
                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Text=""></asp:Label>
                    <asp:Label ID="statuslable" runat="server" ForeColor="Red" Text=""></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Add a New Sub Category for Your Inventory"
                            Font-Bold="True" Visible="False"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add Sub Category" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <label>
                            <asp:Label ID="Label6" runat="server" Text="Select Category"></asp:Label>
                            <asp:Label ID="Label20" runat="server" Text="*" CssClass="labelstar">
                            </asp:Label>
                            <asp:DropDownList ID="ddlInventoryCategoryMasterId" runat="server" AppendDataBoundItems="true"
                                OnSelectedIndexChanged="ddlInventoryCategoryMasterId_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <br />
                            <asp:ImageButton ID="imgadddept" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                OnClick="LinkButton97666667_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                        </label>
                        <label>
                            <br />
                            <asp:ImageButton ID="imgdeptrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton13_Click"
                                ToolTip="Refresh" Width="20px" ImageAlign="Bottom" />
                        </label>
                        <label>
                            <asp:Label ID="Label7" runat="server" Text="Sub Category Name"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="*" CssClass="labelstar">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtInventorySubCatName"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                ControlToValidate="txtInventorySubCatName" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtInventorySubCatName" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&*().>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',20)"></asp:TextBox>
                            <asp:Label ID="Label17" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="div1" class="labelcount">20</span>
                            <asp:Label ID="lblkjsadfgh" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label8" runat="server" Text=" Status"></asp:Label>
                            <asp:DropDownList ID="ddlstatus" runat="server" Width="120px">
                                <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                            </asp:DropDownList>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <asp:Button ID="Button3" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btnsubmit_Click"
                            ValidationGroup="1" />
                        <asp:Button ID="btnupdate" runat="server" Text="Update" Visible="false" ValidationGroup="1"
                            CssClass="btnSubmit" OnClick="btnupdate_Click" />
                        <asp:Button ID="Button4" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="ImageButton7_Click" />
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label9" runat="server" Text="List of Inventory Sub Categories" Font-Bold="true"></asp:Label>
                    </legend>
                    <div style="clear: both;">
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button1_Click1" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" class="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label10" runat="server" Text="Filter by Inventory Main Category"></asp:Label>
                        <asp:DropDownList ID="ddlCat" Width="300px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCat_SelectedIndexChanged"
                            AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label11" runat="server" Text="Filter by Status">
                        </asp:Label>
                        <asp:DropDownList ID="ddlActive" runat="server" Width="120px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlActive_SelectedIndexChanged">
                            <asp:ListItem Text="All"></asp:ListItem>
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlsubcat" runat="server" OnSelectedIndexChanged="ddlsubcat_SelectedIndexChanged"
                            Width="147px" AutoPostBack="True" Visible="false">
                        </asp:DropDownList>
                        <asp:Label ID="Label3" runat="server" Font-Bold="true" Text=""></asp:Label>
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
                                                    <asp:Label ID="Label12" runat="server" Text="List of Inventory Sub Categories" Font-Size="18px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label4" runat="server" Font-Size="16px" Font-Bold="false" Text="Inventory Main Category : "></asp:Label>
                                                    <asp:Label ID="lblCate" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>,
                                                    <asp:Label ID="Label5" runat="server" Font-Size="16px" Font-Bold="false" Text="Status : "></asp:Label>
                                                    <asp:Label ID="lblStatus" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="InventorySubCatId" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                        OnRowCommand="GridView1_RowCommand" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                        OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" Width="100%"
                                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting1"
                                        OnSorting="GridView1_Sorting" EmptyDataText="No Record Found.">
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Inventory Main Category" SortExpression="InventoryCatName"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="45%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdcatname" runat="server" Text='<%# Bind("InventoryCatName") %>'></asp:Label>
                                                    <asp:Label ID="lblgrdCatid" Visible="false" runat="server" Text='<%# Bind("InventeroyCatId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Category" SortExpression="InventorySubCatName"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="45%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdSubCatname" runat="server" Text='<%# Bind("InventorySubCatName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%"
                                                SortExpression="Statuslabel" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkinvMasterStatus" runat="server" Checked='<%# Bind("Activestatus") %>'
                                                        Enabled="false" Visible="false" />
                                                    <asp:Label ID="lbllabelstatus" runat="server" Text='<%#Eval("Statuslabel") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("InventorySubCatId") %>'
                                                        CommandName="Edit" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Btn" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="InventorySubCatId" HeaderText="InventorySubCatId" ReadOnly="True"
                                                InsertVisible="False" SortExpression="InventorySubCatId" Visible="False"></asp:BoundField>
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
                </fieldset>
            </div>
            <!--end of right content-->
            <div style="clear: both;">
            </div>
            <asp:Panel ID="Panel3" runat="server" Height="310px" Width="60%" ScrollBars="None">
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel1" runat="server" Height="310px" Width="60%" ScrollBars="None">
                                <div>
                                    <asp:Panel ID="pnlof" runat="server" CssClass="modalPopup" ScrollBars="None">
                                        <fieldset style="border: 1px solid white;">
                                            <div style="float: right;">
                                                <asp:ImageButton ID="ImageButton3" OnClick="ImageButton3_Click" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="16px"></asp:ImageButton>
                                            </div>
                                            <div style="clear: both">
                                            </div>
                                            <legend style="color: Black">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/ShoppingCart/images/Error.png">
                                                </asp:Image>
                                            </legend>
                                            <label>
                                                <asp:Label ID="Label16" runat="server" Text="Attention:"></asp:Label><br />
                                                <asp:Label ID="Label13" runat="server" Text="Move sub category"></asp:Label><br />
                                                <asp:Label ID="Label14" runat="server" Text="This sub category contains"></asp:Label>
                                                <asp:Label ID="lblTotal" runat="server" Font-Bold="True"></asp:Label>
                                                <asp:Label ID="Label15" runat="server" Text=" sub sub categories. Please move them before deleting."></asp:Label>
                                            </label>
                                            <asp:Panel ID="Panel2" runat="server" Height="300px" Width="100%" ScrollBars="Both">
                                                <asp:GridView ID="GridView2" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                    AlternatingRowStyle-CssClass="alt" Width="100%" DataKeyNames="InventorySubSubId"
                                                    AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:BoundField DataField="InventorySubCatName" ItemStyle-Width="30%" HeaderText="Sub Category"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="InventorySubSubName" ItemStyle-Width="30%" HeaderText="Sub Sub Category"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:TemplateField HeaderText="Change Sub Category To:" ItemStyle-Width="40%" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("InventorySubCatName") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="DropDownList2" runat="server">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                            <div style="clear: both">
                                            </div>
                                            <div style="text-align: center">
                                                <asp:Button ID="ImgBtnMove" CssClass="btnSubmit" runat="server" Text="Move" OnClick="ImgBtnMove_Click" />
                                                <asp:Button ID="ImageButton4" CssClass="btnSubmit" runat="server" Text="Cancel" />
                                            </div>
                                            <div style="clear: both">
                                            </div>
                                        </fieldset>
                                    </asp:Panel>
                                </div>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel1" TargetControlID="HiddenButton" CancelControlID="ImageButton4"
                                    runat="server">
                                </cc1:ModalPopupExtender>
                                <asp:Button ID="HiddenButton" runat="server" Style="display: none" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlCat" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
