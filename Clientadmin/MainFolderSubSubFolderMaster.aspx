<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MainFolderSubSubFolderMaster.aspx.cs" Inherits="MainFolderSubSubFolderMaster"
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
                    <asp:Label ID="statuslable" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Add/Manage Sub-Subfolder "
                            Font-Bold="True"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add Sub-Subfolder" CssClass="btnSubmit"
                            OnClick="btnadd_Click" Height="25px" Width="130px" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                    <label>
                            <asp:Label ID="Label16" runat="server" Text="Product/Version"></asp:Label>
                            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList3_SelectedIndexChanged" Height="25px" 
                            Width="225px">
                            </asp:DropDownList>
                        </label>
                    <label>
                            <asp:Label ID="Label15" runat="server" Text="Main Folder"></asp:Label>
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged" Height="25px" 
                            Width="225px">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label5" runat="server" Text="Subfolder"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlInventorySubCatID"
                                ErrorMessage="*" InitialValue="0" ValidationGroup="6"></asp:RequiredFieldValidator>
                            <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"> </asp:Label>
                            <asp:DropDownList ID="ddlInventorySubCatID" runat="server" AppendDataBoundItems="false"
                                OnSelectedIndexChanged="ddlInventorySubCatID_SelectedIndexChanged" 
                            Height="25px" Width="225px">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <br />
                            
                                 <asp:ImageButton ID="imgdeptrefresh" runat="server" 
                            AlternateText="Refresh" Height="20px"
                                ImageUrl="~/images/DataRefresh.jpg" OnClick="LinkButton13_Click"
                                ToolTip="Refresh" Width="20px" ImageAlign="Bottom" Visible="False" />
                        </label>
                        <label>
                            <asp:Label ID="Label6" runat="server" Text="Sub-Subfolder Name"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="*" CssClass="labelstar"> </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtInventorySubSubName"
                                ErrorMessage="*" ValidationGroup="6"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                ControlToValidate="txtInventorySubSubName" ValidationGroup="6"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtInventorySubSubName" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                
                            onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',20)" 
                            Height="20px" Width="225px"></asp:TextBox>
                            <asp:Label ID="Label17" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="div1" class="labelcount">20</span>
                            <asp:Label ID="lblsj" Text="(A-Z 0-9 _)" CssClass="labelcount" runat="server"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label7" runat="server" Text="Status"></asp:Label>
                            <asp:DropDownList ID="ddlstatus" runat="server" Width="225px" Height="25px" 
                            onselectedindexchanged="ddlstatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                            </asp:DropDownList>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <asp:Button ID="ImageButton2" CssClass="btnSubmit" Text="Submit" runat="server" ValidationGroup="6"
                            OnClick="btnsubmit_Click" Height="25px" Width="60px" />
                        <asp:Button ID="Button8" runat="server" onclick="Button8_Click" Text="Update" 
                            Height="25px" Width="60px" />
                        <asp:Button ID="ImageButton7" CssClass="btnSubmit" Text="Cancel" runat="server" 
                            OnClick="ImageButton7_Click" Height="25px" Width="60px" />
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label9" runat="server" Text="List of Sub-Subfolders"
                            Font-Bold="True"></asp:Label>
                    </legend>
                    <div style="clear: both;">
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button1_Click" Visible="False" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label18" runat="server" Text=" Filter by Product/Version"></asp:Label>
                        <asp:DropDownList ID="DropDownList4" runat="server" OnSelectedIndexChanged="ddlInventorySubCatID_SelectedIndexChanged"
                            AutoPostBack="True" Width="225px" Height="25px">
                        </asp:DropDownList>
                    </label>
                  
                    <label style="width:330px;">
                        <asp:Label ID="Label8" runat="server" 
                        Text=" Filter by Main Folder and Subfolder"></asp:Label>
                        <asp:DropDownList ID="ddlSubcat" runat="server" OnSelectedIndexChanged="ddlSubcat_SelectedIndexChanged"
                            AutoPostBack="True" Width="225px" Height="25px">
                        </asp:DropDownList>
                    </label>

                    <label>
                        <asp:Label ID="Label10" runat="server" Text="Filter by Status"></asp:Label>
                        <asp:DropDownList ID="ddlActive" runat="server" Width="225px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlActive_SelectedIndexChanged" Height="25px">
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table id="Table1" cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Size="20px" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center">
                                                    <asp:Label ID="Label3" runat="server" Font-Size="18px" Text="List of Item Sub Sub Categories "></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Font-Size="16px" Font-Bold="false" Text="Items Main Category and Sub Category : "></asp:Label>
                                                    <asp:Label ID="lblSubCat" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>,
                                                    <asp:Label ID="Label2" runat="server" Font-Size="16px" Font-Bold="false" Text="Status : "></asp:Label>
                                                    <asp:Label ID="lblStatus" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                        Width="100%" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt"  EmptyDataText="No Record Found."
                                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                        OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                                        OnRowUpdating="GridView1_RowUpdating" OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
                                        OnSorting="GridView1_Sorting">
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Product/Version" 
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSubCategoryName" runat="server" Text='<%# Bind("productversion") %>'  ></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Main Folder Name" 
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsubcategory123" runat="server" Text='<%# Bind("FolderCatName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subfolder Name" 
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInventorySubSubName" runat="server" Text='<%# Bind("FolderSubName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub-Subfolder Name" 
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsunf" runat="server" Text='<%# Bind("FolderSubSubName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%"
                                                SortExpression="Statuslabel">
                                                <ItemTemplate>
                                                    
                                                    <asp:Label ID="lblstatuslabel" runat="server" Text='<%# Bind("Active") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="3%" HeaderText="Action" HeaderStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                   
                                                    <asp:ImageButton ID="llinedit" runat="server" 
                                                        CommandArgument='<%#Eval("FolderSubSubId") %>' CommandName="Edit" 
                                                        ImageUrl="~/images/edit.gif" ToolTip="Edit" />
                                                         <asp:ImageButton ID="Btn" runat="server" 
                                                        CommandArgument='<%#Eval("FolderSubSubId") %>' CommandName="Delete" 
                                                        ImageUrl="~/images/delete.gif" onclick="Btn_Click2" ToolTip="Delete" />
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
                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                </fieldset>
            </div>
            <!--end of right content-->
            <div style="clear: both;">
            </div>
            <asp:Panel ID="Panel3" runat="server" Height="310px" Width="60%" ScrollBars="None">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="Panel4" runat="server" Width="60%" Height="310px" ScrollBars="None">
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
                                                <asp:Label ID="Label14" runat="server" Text="Attention:"></asp:Label><br />
                                                <asp:Label ID="Label11" runat="server" Text="Move Sub Sub Category"></asp:Label><br />
                                                <asp:Label ID="Label12" runat="server" Text="This sub sub category contains"></asp:Label>
                                                <asp:Label ID="lblTotal" runat="server" Font-Bold="True"></asp:Label>
                                                <asp:Label ID="Label13" runat="server" Text="inventories. Please move them before deleting."></asp:Label>
                                            </label>
                                            <asp:Panel ID="Panel2" runat="server" Height="300px" ScrollBars="Both" Width="100%">
                                                <asp:GridView ID="GridView2" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    CssClass="mGrid" DataKeyNames="InventorySubSubId" PagerStyle-CssClass="pgr" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="InventoryCatName" ItemStyle-Width="20%" HeaderText="Inventory Main Category"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="InventorySubCatName" ItemStyle-Width="20%" HeaderText="Sub Category "
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="InventorySubSubName" ItemStyle-Width="20%" HeaderText="Sub Sub Category"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Name" ItemStyle-Width="20%" HeaderText="Inventory Name"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:TemplateField HeaderText="Change Sub Sub Category To:" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("InventorySubCatName") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="DropDownList2" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="insubid" runat="server" Text='<%# Bind("InventorySubCatId") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="InvMid" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblinvMid" runat="server" Text='<%# Bind("InventoryMasterId") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblinvMid" runat="server" Text='<%# Bind("InventoryMasterId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                            <div style="clear: both">
                                            </div>
                                            <div style="text-align: center">
                                                <asp:Button ID="ImgBtnMove" CssClass="btnSubmit" Text=" Move " runat="server" OnClick="ImgBtnMove_Click" />
                                                <asp:Button ID="ImageButton4" CssClass="btnSubmit" Text="Cancel" runat="server" />
                                            </div>
                                        </fieldset></asp:Panel>
                                </div>
                                <asp:Button ID="HiddenButton" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="ImageButton4"
                                    BackgroundCssClass="modalBackground" PopupControlID="Panel4" TargetControlID="HiddenButton">
                                </cc1:ModalPopupExtender>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
