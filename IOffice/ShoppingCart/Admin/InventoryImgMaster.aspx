<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="InventoryImgMaster.aspx.cs" Inherits="InventoryImgMaster"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    <asp:UpdatePanel ID="UpdatePanel3" ChildrenAsTriggers="true" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" Text="Upload Images for Multiple Products" runat="server"
                            CssClass="btnSubmit" Visible="false" OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="btnmassentry" Text="Mass Entry of Inventory Images by Importing Image Files"
                            CssClass="btnSubmit" runat="server" OnClick="btnmassentry_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server">
                        <label>
                            <asp:Label ID="Label2" runat="server" Text="Select Business Name"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlWarehouse"
                                Display="Dynamic" ErrorMessage="*" InitialValue="0" ValidationGroup="12"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label11" runat="server" Text=" Select Inventory"></asp:Label>
                            <asp:DropDownList ID="ddlimageavaibility" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlimageavaibility_SelectedIndexChanged">
                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                <asp:ListItem Text="With All Images " Value="1"></asp:ListItem>
                                <%-- <asp:ListItem Text="With Any One Image Availabel" Value="2"></asp:ListItem>--%>
                                <asp:ListItem Text="Without Images " Selected="True" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Select Inventory Item"></asp:Label>
                            <asp:Label ID="Label10" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="DropDownList1"
                                ErrorMessage="*" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="12"></asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownList1"
                                Display="Dynamic" ErrorMessage="*" InitialValue="0" ValidationGroup="12"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="DropDownList1" runat="server" ValidationGroup="12" Width="450px">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <br />
                            <asp:Button ID="btndars" runat="server" Text="Go" OnClick="imgBtnSearchGo_Click"
                                ValidationGroup="12" />
                        </label>
                        <div style="clear: both;">
                        </div>
                        <div style="text-align: center">
                        </div>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label4" runat="server" Text="List of Images of the Selected Item"
                            Font-Bold="true"></asp:Label>
                    </legend>
                    <div style="text-align: right">
                        <asp:Button ID="btncancel0" runat="server" CssClass="btnSubmit" CausesValidation="false"
                            OnClick="btncancel0_Click" Text="Printable Version" />
                        <input id="Button2" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" type="button" value="Print" visible="false" />
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table id="GridTbl" width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblcomname" runat="server" Font-Size="20px" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label8" runat="server" Text="Business : " Font-Size="18px"></asp:Label>
                                                    <asp:Label ID="lblbusiness" runat="server" Font-Size="18px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblhead" runat="server" Font-Size="18px" Text="List of Images of the Selected Item"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="font-size: 16px; font-weight: normal;">
                                                    <asp:Label ID="lblitemdd" Visible="false" runat="server" Text="Item Name :"></asp:Label>
                                                    <asp:Label ID="lblitemname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnl" Width="100%" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                                    OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
                                                    OnRowEditing="GridView1_RowEditing" EmptyDataText="No Record Found." OnRowUpdating="GridView1_RowUpdating"
                                                    OnRowCommand="GridView1_RowCommand" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Thumbnail Image" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgsmall" runat="server" Height="50px" Visible="false" Width="50px" />
                                                                <br />
                                                                <asp:Label ID="lblSmallImageText" runat="server" Text="No Image Available" Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:FileUpload ID="FileUploadSmallImage" runat="server" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                                    ControlToValidate="FileUploadSmallImage" ValidationGroup="qq1"></asp:RequiredFieldValidator>
                                                                <asp:Button ID="btnsmall" runat="server" Text="Add" CssClass="btnSubmit" CommandName="addsmall"
                                                                    ValidationGroup="qq1" />
                                                                <asp:Image ID="imgsmall" runat="server" Height="50px" Visible="false" Width="50px" />
                                                                <br />
                                                                <asp:Label ID="lblSmallImageText" runat="server" Text="No Image Available" Visible="false"></asp:Label>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Large Image" HeaderStyle-HorizontalAlign="Left">
                                                            <EditItemTemplate>
                                                                <asp:FileUpload ID="FileUploadLargeImage" runat="server" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4555" runat="server" ErrorMessage="*"
                                                                    ControlToValidate="FileUploadLargeImage" ValidationGroup="qq2"></asp:RequiredFieldValidator>
                                                                <asp:Button ID="btnlarge" runat="server" Text="Add" CssClass="btnSubmit" CommandName="addLarge"
                                                                    ValidationGroup="qq2" />
                                                                <asp:Image ID="imglarge" runat="server" Height="50px" Visible="false" Width="80px" />
                                                                <br />
                                                                <asp:Label ID="lblLargeImageText" runat="server" Text="No Image Available" Visible="false"></asp:Label>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Image ID="imglarge" runat="server" Height="50px" Visible="false" Width="80px" />
                                                                <br />
                                                                <asp:Label ID="lblLargeImageText" runat="server" Text="No Image Available" Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnedit" runat="server" CommandName="Edit" ImageUrl="~/Account/images/edit.gif"
                                                                    ToolTip="Edit"></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:ImageButton ID="btnedit" runat="server" CommandName="Update" ImageUrl="~/Account/images/UpdateGrid.JPG"
                                                                    ToolTip="Update"></asp:ImageButton>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Cancel" ImageUrl="~/images/delete.gif"
                                                                    ToolTip="Cancel"></asp:ImageButton>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:CommandField CausesValidation="false" ShowEditButton="True" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Edit" ItemStyle-Width="5%" ButtonType="Image" EditImageUrl="~/Account/images/edit.gif"
                                                            HeaderImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/UpdateGrid.JPG"
                                                            CancelImageUrl="~/images/delete.gif" ItemStyle-HorizontalAlign="Left" />--%>
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
                                                        <%--<asp:CommandField ShowDeleteButton="True" CausesValidation="false" ValidationGroup="qq1" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Delete" />--%>
                                                        <asp:TemplateField HeaderText="HdnInvMid" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvMid" runat="server" Text='<%#Bind("InventoryMasterId") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblInvImgId" runat="server" Text='<%#Bind("InventoryImgMasterID") %>'
                                                                    Visible="false" Width="100Px" Height="50Px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblInvMid" runat="server" Text='<%#Bind("InventoryMasterId") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblInvImgId" runat="server" Text='<%#Bind("InventoryImgMasterID") %>'
                                                                    Visible="false" Width="100Px" Height="50Px"></asp:Label>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="GridView1" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <asp:Panel ID="Panel2" runat="server" Visible="False" Width="100%">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label5" runat="server" Text="Insert New Image"></asp:Label>
                        </legend>
                        <label>
                            <asp:Label ID="Label6" runat="server" Text="Thumbnail Image"></asp:Label>
                            <asp:Label ID="req" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </label>
                        <label>
                            <asp:Label ID="Label7" runat="server" Text="Large Image "></asp:Label>
                            <asp:Label ID="Label9" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FileUpload2"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:FileUpload ID="FileUpload2" runat="server" />
                        </label>
                        <div style="clear: both;">
                        </div>
                        <div>
                            <asp:Button ID="ImageButton1" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btnsubmit_Click"
                                ValidationGroup="1" />
                            <asp:Button ID="ImageButton2" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="Button2_Click" />
                        </div>
                    </fieldset>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ImageButton1" />
            <asp:PostBackTrigger ControlID="btndars" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
