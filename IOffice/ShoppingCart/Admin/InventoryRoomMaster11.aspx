<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="InventoryRoomMaster11.aspx.cs" Inherits="ShoppingCart_Admin_InventoryRoomMaster11"
    Title="Admin" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Src="~/IOffice/ShoppingCart/Admin/UserControl/UControlWizardMaster1.ascx" TagName="pnlM"
    TagPrefix="pnlM" %>
<%@ Register Src="~/IOffice/ShoppingCart/Admin/UserControl/UControlWizardMaster4Inv.ascx"
    TagName="pnl4" TagPrefix="pnl4" %>
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
        function Button7_onclick() {

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
                    <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Add Inventory Room" Font-Bold="True"
                            Visible="False"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnaddroom" runat="server" CssClass="btnSubmit" Text="Add Inventory Room"
                            OnClick="btnaddroom_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="addinventoryroom" Visible="false" runat="server">
                        <label>
                            <asp:Label ID="Label5" runat="server" Text="Select Business Name "></asp:Label>
                            <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar">
                            </asp:Label>
                            <asp:DropDownList ID="ddlwarehouse0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse0_SelectedIndexChanged"
                                ValidationGroup="1">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label6" runat="server" Text="Select by Inventory Location Name "></asp:Label>
                            <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlinvsiteid0"
                                ErrorMessage="*" InitialValue="0" ValidationGroup="1">
                            </asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlinvsiteid0" runat="server" ValidationGroup="1">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label7" runat="server" Text="Add Room Name "></asp:Label>
                            <asp:Label ID="Label12" runat="server" Text="*" CssClass="labelstar">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtinvroomname"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtinvroomname"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtinvroomname" runat="server" ValidationGroup="1" Width="300px"
                                MaxLength="25" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',25)"></asp:TextBox>
                            <asp:Label ID="Label17" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="div1" class="labelcount">25</span>
                            <asp:Label ID="lblsdgha" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <asp:Button ID="ImageButton1" runat="server" Text="Submit" OnClick="Button1_Click"
                            CssClass="btnSubmit" ValidationGroup="1" />
                        <asp:Button ID="btnupdate" runat="server" Text="Update" ValidationGroup="1" CssClass="btnSubmit"
                            Visible="false" OnClick="btnupdate_Click" />
                        <asp:Button ID="ImageButton6" runat="server" Text="Cancel" OnClick="ImageButton6_Click"
                            CssClass="btnSubmit" ValidationGroup="1" CausesValidation="False" />
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                        <div style="clear: both;">
                        </div>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Inventory Rooms" Font-Bold="true" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" OnClick="Button1_Click1"
                            CssClass="btnSubmit" CausesValidation="False" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" class="btnSubmit" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label14" Text=" Select Business Name" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged"
                            ValidationGroup="1">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label8" Text="Filter by Inventory Location " runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlinvsiteid" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlinvsiteid_SelectedIndexChanged"
                            ValidationGroup="1">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Italic="True" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="Label28" runat="server" Text="Business-Inventory Location : " Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Italic="True"></asp:Label>-
                                                    <asp:Label ID="Label10" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label4" runat="server" Font-Italic="true" ForeColor="Black" Text="List of Inventory Rooms "></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Font-Italic="True" ForeColor="Black" Text="Inventory Location : "
                                                        Visible="false"></asp:Label>
                                                    <asp:Label ID="lblSite" runat="server" Font-Italic="True" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        AllowSorting="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        DataKeyNames="InventoryRoomID" EmptyDataText="No Record Found." OnPageIndexChanging="GridView1_PageIndexChanging"
                                        OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowCommand="GridView1_RowCommand"
                                        OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                                        OnSorting="GridView1_Sorting" Width="100%" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField DataField="InventoryRoomID" HeaderText="Inventory Room ID" InsertVisible="False"
                                                ReadOnly="True" SortExpression="InventoryRoomID" Visible="False" />
                                            <asp:TemplateField HeaderText="Business and Inventory Location Name" SortExpression="InventorySiteName"
                                                ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("InventorySiteName") %>'></asp:Label>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("InventorySiteID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="50%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Room Name" SortExpression="InventoryRoomName" ItemStyle-Width="45%"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label32" runat="server" Text='<%# Bind("InventoryRoomName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="45%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton48" ToolTip="Edit" runat="server" CommandArgument='<%# Eval("InventoryRoomID") %>'
                                                        CommandName="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Button1" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </cc11:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Height="400px" Width="60%">
                        <div>
                            <fieldset style="border: 1px solid white;">
                                <legend style="color: Black">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/ShoppingCart/images/Error.png" />
                                </legend>
                                <div>
                                    <table width="100%" style="color: #000000">
                                        <tbody>
                                            <tr>
                                                <td class="subinnertblfc" width="90%">
                                                    <label>
                                                        <asp:Label ID="Label9" runat="server" Text="Attention:"></asp:Label>
                                                    </label>
                                                </td>
                                                <td class="subinnertblfc" width="5%">
                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" OnClick="ImageButton3_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="90%">
                                                    <label>
                                                        <asp:Label ID="Label13" runat="server" Text="Move Room"></asp:Label>
                                                    </label>
                                                </td>
                                                <td width="5%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label16" runat="server" Text="This room contains"></asp:Label>
                                                        <asp:Label ID="lblTotal" runat="server" Font-Bold="True"></asp:Label>
                                                        <asp:Label ID="Label15" runat="server" Text=" racks. Please move them before deleting.">
                                                        </asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="height: 158px">
                                                    <asp:Panel ID="Panel2" BackColor="#CCCCCC" runat="server" Height="160px" ScrollBars="Both"
                                                        Width="100%">
                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" PagerStyle-CssClass="pgr"
                                                            AlternatingRowStyle-CssClass="alt" CssClass="mGrid" GridLines="Both" Width="100%">
                                                            <Columns>
                                                                <asp:BoundField DataField="InventoryRoomName" ItemStyle-Width="30%" HeaderText="Room Name"
                                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="InventortyRackName" ItemStyle-Width="30%" HeaderText="Rack Name"
                                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:TemplateField HeaderText="Change Room to:" ItemStyle-Width="40%" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("InventoryRoomName") %>'></asp:TextBox><asp:Label
                                                                            ID="lblrackid" runat="server" Visible="false" Text='<%#Bind("InventortyRackID") %>'></asp:Label></EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="DropDownList3" runat="server" Width="350px">
                                                                        </asp:DropDownList>
                                                                        <asp:Label ID="lblrackid" runat="server" Visible="false" Text='<%#Bind("InventortyRackID") %>'></asp:Label></ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Button ID="ImgBtnMove" runat="server" CssClass="btnSubmit" Text="Move" OnClick="ImgBtnMove_Click" />
                                                    <asp:Button ID="ImageButton4" runat="server" CssClass="btnSubmit" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </fieldset>
                            &nbsp;&nbsp;</div>
                    </asp:Panel>
                    <asp:Button ID="HiddenButton" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel1" CancelControlID="ImageButton4" TargetControlID="HiddenButton">
                    </cc1:ModalPopupExtender>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Outset"
                        Width="300px">
                        <table id="subinnertbl" cellpadding="0" cellspacing="3" width="100%">
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Font-Size="14px" ForeColor="Black">You 
                                    Sure ,You Want to Delete a Record?</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="ImageButton2" CssClass="btnSubmit" runat="server" Text="Yes" OnClick="yes_Click" />
                                    <%--<asp:ImageButton ID="ImageButton2" runat="server" AlternateText="submit" 
                                ImageUrl="~/ShoppingCart/images/Yes.png" OnClick="yes_Click" />--%>
                                    &nbsp;<asp:Button ID="ImageButton5" CssClass="btnSubmit" runat="server" Text="No"
                                        OnClick="ImageButton5_Click" />
                                    <%--<asp:ImageButton ID="ImageButton5" runat="server" AlternateText="cancel" 
                                ImageUrl="~/ShoppingCart/images/No.png" OnClick="ImageButton5_Click" />--%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel3" TargetControlID="HiddenButton222">
                    </cc1:ModalPopupExtender>
                </fieldset>
                &nbsp;&nbsp;</div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
