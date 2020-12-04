<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="UpdateImageOfItem.aspx.cs" Inherits="ShoppingCart_Admin_UpdateImageOfItem"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="uppae" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" ForeColor="Red" runat="server"></asp:Label>
                </div>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td style="width: 25%">
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Business Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="gg" runat="server" ControlToValidate="ddlwarehouse"
                                        SetFocusOnError="true" InitialValue="0" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td style="width: 25%">
                                <label>
                                    <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td style="width: 25%">
                            </td>
                            <td style="width: 25%">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Category"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlcategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Sub Category"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlSubCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="Sub Sub Category"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlSubSubCategory" runat="server">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="Image Availability"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlImgAvai" runat="server" OnSelectedIndexChanged="ddlImgAvai_SelectedIndexChanged">
                                        <asp:ListItem>Neither Image available</asp:ListItem>
                                        <asp:ListItem Selected="True">All Image available</asp:ListItem>
                                       
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="Sort By"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlSort" runat="server">
                                        <asp:ListItem>Name</asp:ListItem>
                                        <asp:ListItem>Product Number</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label9" runat="server" Text="Sort Type"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlSortType" runat="server">
                                        <asp:ListItem>Ascending</asp:ListItem>
                                        <asp:ListItem>Descending</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" Text=" Go "
                                    CssClass="btnSubmit" ValidationGroup="1" />
                                 <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/go.png"
                                OnClick="ImageButton1_Click" ValidationGroup="1" />--%>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label10" runat="server" Text="List of Products"></asp:Label>
                    </legend>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" 
                            AlternatingRowStyle-CssClass="alt" AllowPaging="true" PageSize="15"
                            GridLines="Both" DataKeyNames="InventoryMasterId" AllowSorting="True" OnSorting="GridView1_Sorting"
                            OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                            OnRowUpdating="GridView1_RowUpdating" Width="100%" 
                            EmptyDataText="No Record Found." onpageindexchanging="GridView1_PageIndexChanging" 
                           >
                            <Columns>
                                <asp:TemplateField HeaderText="Product No." SortExpression="ProductNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductNo") %>'
                                            ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category/Sub Category/Sub Sub Category" SortExpression="cat"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"cat") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <a href='Inventoryprofile.aspx?Invmid=<%# Eval("InventoryMasterId")%>' target="_blank">
                                        <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' ForeColor="#416271"></asp:Label>
                                         </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Thumbnail" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblImgMasterIdTN" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "InventoryImgMasterID")%>'> </asp:Label>
                                        <%--<img height="50" alt="" hspace="0" src='~/Account/<%#DataBinder.Eval(Container.DataItem, "Thumbnail")%>'
                                width="50" border="0" />--%>
                                        <asp:Image ID="imgsmall" runat="server" Height="50px" Width="50px" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Image ID="imgsmall" runat="server" Height="50px" Width="50px" />
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                            ControlToValidate="FileUploadThumbNail" ValidationGroup="q1"></asp:RequiredFieldValidator>
                                        <asp:FileUpload ID="FileUploadThumbNail" runat="server" />
                                        
                                        
                                        <asp:Button ID="btnsmall" runat="server" Text="Add" CssClass="btnSubmit" CommandName="addsmall"
                                            ValidationGroup="q1" />
                                        <asp:Label ID="lblImgMasterIdTN" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "InventoryImgMasterID")%>'> </asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Large Image" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <%--  <img height="50" hspace="0" alt="" 
                                src='../.../Account/<%#DataBinder.Eval(Container.DataItem, "LargeImg")%>' border="0" />--%>
                                        <%--<img height="50" hspace="0" alt="" 
                                src='~/Account/<%#DataBinder.Eval(Container.DataItem, "LargeImg")%>' border="0" />--%>
                                        <asp:Image ID="imglarge" runat="server" Height="50px" Width="80px" />
                                        <asp:Label ID="lblImgMasterIdLI" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "InventoryImgMasterID")%>'> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Image ID="imglarge" runat="server" Height="50px" Width="80px" />
                                        <br />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                            ControlToValidate="FileUploadLargeImage" ValidationGroup="q2"></asp:RequiredFieldValidator>
                                        <asp:FileUpload ID="FileUploadLargeImage" runat="server" />
                                         <asp:Button ID="btnlarge" runat="server" Text="Add" CssClass="btnSubmit" CommandName="addLarge"
                                            ValidationGroup="q2" />
                                       
                                        <asp:Label ID="lblImgMasterIdLI" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "InventoryImgMasterID")%>'> </asp:Label>
                                      
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" HeaderText="Edit" ShowHeader="True" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="5%" ButtonType="Image" HeaderImageUrl="~/Account/images/edit.gif"
                                    EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/UpdateGrid.JPG"
                                    CancelImageUrl="~/images/delete.gif" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GridView1" />
            <asp:AsyncPostBackTrigger ControlID="ImageButton1" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
