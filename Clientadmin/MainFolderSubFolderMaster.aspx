<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MainFolderSubFolderMaster.aspx.cs" Inherits=" MainFolderSubFolderMaster"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

   
</script>
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
    <asp:Panel ID="Panel2" runat="server">
  
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Text=""></asp:Label>
                    <asp:Label ID="statuslable" runat="server" ForeColor="Red" Text=""></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Add/Manage Subfolder "
                            Font-Bold="True"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add Subfolder" CssClass="btnSubmit"
                            OnClick="btnadd_Click" Width="120px" Height="25px" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                       <label> 
                           <asp:Label ID="Label18" runat="server" Text="Product/Version"></asp:Label>
                           <asp:DropDownList ID="drpFillProduct" runat="server" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                            AutoPostBack="True" Height="25px" Width="200px">
                           </asp:DropDownList>
                           </label>

                      

                        <label>
                            <asp:Label ID="Label6" runat="server" Text="Main Folder Name"></asp:Label>
                           
                            <asp:DropDownList ID="DrpMainFolder" runat="server" AutoPostBack="True" 
                            Height="25px" Width="200px" 
                            onselectedindexchanged="DrpMainFolder_SelectedIndexChanged"></asp:DropDownList>
                        </label>
                        <label>
                        <asp:Label ID="LabelSubFolder" runat="server" Text="Subfolder Name"></asp:Label>
                            <asp:TextBox ID="TextSubFolder" runat="server" AutoPostBack="True" 
                            Height="20px" Width="200px"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="Label8" runat="server" Text=" Status"></asp:Label>
                            <asp:DropDownList ID="ddlstatus" runat="server" Width="200px" Height="25px">
                                <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                            </asp:DropDownList>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <asp:Button ID="Button3" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btnsubmit_Click"
                            ValidationGroup="1" Width="57px" Height="25px" />
                                                    <asp:Button ID="btnupdate" runat="server" Text="Update" 
                                              Visible="false" ValidationGroup="1"
                            CssClass="btnSubmit" OnClick="btnupdate_Click" Height="25px" />
                        
                        <asp:Button ID="Button4" runat="server" Text="Cancel" CssClass="btnSubmit" 
                            OnClick="ImageButton7_Click" Width="57px" Height="25px" />
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label9" runat="server" Text="List of Subfolders" 
                            Font-Bold="True"></asp:Label>
                    </legend>





                 

                    <div style="clear: both;">
                    </div>
                 
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label19" runat="server" Text="Filter by Product/Version"></asp:Label>
                        <asp:DropDownList ID="DrpFilterProduct" runat="server" Height="25px" 
                        Width="200px" AutoPostBack="True" 
                        onselectedindexchanged="DrpFilterProduct_SelectedIndexChanged"></asp:DropDownList>
                    </label>
                    <label>
                    <asp:Label ID="Label1" runat="server" Text="Filter by Main Folder"></asp:Label>
                     <asp:DropDownList ID="drpmainfolderFilter" runat="server" Height="25px" 
                        Width="200px" AutoPostBack="True" 
                        onselectedindexchanged="drpmainfolderFilter_SelectedIndexChanged"></asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label11" runat="server" Text="Filter by Status"></asp:Label>
                        <asp:DropDownList ID="DrpStatus" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="DrpStatus_SelectedIndexChanged" Width="200px" 
                        Height="25px"> 
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
                                                    <asp:Label ID="Label12" runat="server" Text="List of Item Sub Categories" Font-Size="18px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label4" runat="server" Font-Size="16px" Font-Bold="false" Text="Item Main Category : "></asp:Label>
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


                                <asp:GridView ID="GridMainSUbFolder" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" onrowcommand="GridMainSUbFolder_RowCommand" 
                                        onrowediting="GridMainSUbFolder_RowEditing" 
                                        onrowupdating="GridMainSUbFolder_RowUpdating" CssClass="mGrid" 
                                        Width="100%">
                                   
                                        <Columns>
                                           <asp:TemplateField Visible="false" HeaderText="Id">


                                    <ItemTemplate>
                                        <asp:Label ID="Label31" runat="server"  Text='<%#Eval("FolderSubId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product/Version" SortExpression="InventoryCatName"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdcatname1" runat="server" Text='<%# Bind("productversion") %>'></asp:Label>
                                                    <asp:Label ID="lblgrdCatid1" Visible="false" runat="server" Text='<%# Bind("productversion") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="40%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Main Folder Name" SortExpression="InventoryCatName"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdcatname" runat="server" Text='<%# Bind("FolderCatName") %>'></asp:Label>
                                                    <asp:Label ID="lblgrdCatid" Visible="false" runat="server" Text='<%# Bind("FolderCatName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="40%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subfolder Name" SortExpression="InventorySubCatName"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdSubCatname" runat="server" Text='<%# Bind("FolderSubName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%"
                                                SortExpression="Statuslabel" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    
                                                    <asp:Label ID="lbllabelstatus" runat="server" Text='<%#Eval("Activestatus") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("FolderSubId") %>'
                                            Height="20px" ImageUrl="~/Images/edit.gif" Width="15px" 
                                            onclick="imgbtnEdit_Click" />
                                        <asp:ImageButton ID="imgbtnDelete" runat="server" 
                                            CommandArgument='<%# Eval("FolderSubId") %>' CommandName="Delete" 
                                            ImageUrl="~/images/delete.gif" Height="20px" Width="15px" 
                                            onclick="imgbtnDelete_Click" />
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>

                                          
                                            <asp:BoundField DataField="InventorySubCatId" HeaderText="InventorySubCatId" ReadOnly="True"
                                                InsertVisible="False" SortExpression="InventorySubCatId" Visible="False"></asp:BoundField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                   </asp:GridView>

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
    </asp:Panel>
</asp:Content>
