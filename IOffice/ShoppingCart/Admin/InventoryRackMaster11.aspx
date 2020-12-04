<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="InventoryRackMaster11.aspx.cs" Inherits="ShoppingCart_Admin_InventoryRackMaster11"
    Title="Inventory Rack Master" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        function RealNumWithDecimal(myfield, e, dec)
         {

            //myfield=document.getElementById(FindName(myfield)).value
            //alert(myfield);
            var key;
            var keychar;
            if (window.event)
                key = window.event.keyCode;
            else if (e)
                key = e.which;
            else
                return true;
            keychar = String.fromCharCode(key);
            if (key == 13) {
                return false;
            }
            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 27)) {
                return true;
            }
            else if ((("0123456789.").indexOf(keychar) > -1)) {
                return true;
            }
            // decimal point jump
            else if (dec && (keychar == ".")) {

                myfield.form.elements[dec].focus();
                myfield.value = "";

                return false;
            }
            else {
                myfield.value = "";
                return false;
            }
        }

        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }

        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
            }
        }  
    </script>

    <script language="javascript" type="text/javascript"> 
    function RealNumWithDecimal(myfield, e, dec) 
    { //myfield=document.getElementById(FindName(myfield)).value //alert(myfield);
        var key; 
        var keychar; 
        if (window.event) key = window.event.keyCode; 
        else if (e)
            key = e.which; 
        else return true; 
            keychar = String.fromCharCode(key); 
        if (key == 13) 
            { return false; } 
        if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 27)) 
            { return true; } 
        else if ((("0123456789.").indexOf(keychar) > -1))
            { return true; } 
        // decimal point jump else if (dec && (keychar == ".")) { myfield.form.elements[dec].focus();}
        myfield.value = ""; return false; 
        else { myfield.value = ""; return false; } 
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
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Add New Inventory Rack" Font-Bold="True"
                            Visible="False">
                        </asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnaddroom" runat="server" CssClass="btnSubmit" Text="Add Inventory Rack"
                            OnClick="btnaddroom_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="addinventoryroom" Visible="false" runat="server">
                        <label>
                            <asp:Label ID="Label5" runat="server" Text="Business Name ">
                            </asp:Label>
                            <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlwarehouse"
                                ErrorMessage="*" InitialValue="-Select-" ValidationGroup="1">
                       
                            </asp:RequiredFieldValidator><asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged" ValidationGroup="1"
                                Width="250px">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label11" runat="server" Text="Location Name ">
                            </asp:Label>
                            <asp:Label ID="Label20" runat="server" Text="*" CssClass="labelstar">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlsitename"
                                ErrorMessage="*" InitialValue="-Select-" ValidationGroup="1"></asp:RequiredFieldValidator><asp:DropDownList
                                    ID="ddlsitename" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsitename_SelectedIndexChanged"
                                    ValidationGroup="1" Width="250px">
                                </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label12" runat="server" Text="Room Name "></asp:Label><asp:Label ID="Label21"
                                runat="server" Text="*" CssClass="labelstar">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlinvroomid"
                                ErrorMessage="*" ValidationGroup="1" InitialValue="0">
                            </asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlinvroomid" runat="server" Width="250px" ValidationGroup="1">
                            </asp:DropDownList>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label13" runat="server" Text="Rack No."></asp:Label>
                            <asp:Label ID="Label22" runat="server" Text="*" CssClass="labelstar">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="txtinvrackname" ErrorMessage="*" ValidationGroup="1">
                            </asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtinvrackname" runat="server" Width="244px" ValidationGroup="1"
                                MaxLength="15" onkeyup="return mak('Span1',15,this)">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                TargetControlID="txtinvrackname" ValidChars="0147852369">
                            </cc1:FilteredTextBoxExtender>
                            <asp:Label ID="Label29" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span1" class="labelcount">15</span>
                            <asp:Label ID="Label6" Text="(0-9)" CssClass="labelcount" runat="server">
                            </asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label16" runat="server" Text="No. of Shelves "></asp:Label><asp:Label
                                ID="Label23" runat="server" Text="*" CssClass="labelstar">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtshelves"
                                ErrorMessage="*" ValidationGroup="1">
                            </asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                TargetControlID="txtshelves" ValidChars="0147852369">
                            </cc1:FilteredTextBoxExtender>
                            <asp:TextBox ID="txtshelves" runat="server" Width="244px" ValidationGroup="1" MaxLength="15"
                                onkeyup="return mak('Span2',15,this)">
                            </asp:TextBox>
                            <asp:Label ID="Label30" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span2" class="labelcount">15</span>
                            <asp:Label ID="Label25" Text="(0-9)" runat="server" CssClass="labelcount"></asp:Label></label>
                        <label>
                            <asp:Label ID="Label15" runat="server" Text="SQFT of each Shelf ">
                            </asp:Label><asp:Label ID="Label24" runat="server" Text="*" CssClass="labelstar">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                ControlToValidate="txtsizeofrack" ErrorMessage="*" ValidationGroup="1">
                            </asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                TargetControlID="txtsizeofrack" ValidChars="0147852369">
                            </cc1:FilteredTextBoxExtender>
                            <asp:TextBox ID="txtsizeofrack" runat="server" Width="244px" ValidationGroup="1"
                                MaxLength="15" onkeyup="return mak('Span3',15,this)">
                            </asp:TextBox>
                            <asp:Label ID="Label31" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span3" class="labelcount">15</span>
                            <asp:Label ID="Label26" Text="(0-9)" runat="server" CssClass="labelcount"></asp:Label></label>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="Panel1" runat="server" Width="100%" Visible="false">
                            <label>
                                <asp:Label ID="Label14" runat="server" Text="No. of positions on shelf " Visible="False"></asp:Label><asp:TextBox
                                    ID="txtpositionshelf" runat="server" Width="244px" ValidationGroup="1" MaxLength="15"
                                    Visible="false"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                        runat="server" ControlToValidate="txtpositionshelf" ErrorMessage="*" ValidationGroup="1">
                                    </asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                    TargetControlID="txtpositionshelf" ValidChars="0147852369">
                                </cc1:FilteredTextBoxExtender>
                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            </label>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                        <asp:Button ID="ImageButton1" runat="server" Text="Submit" OnClick="Button1_Click"
                            CssClass="btnSubmit" ValidationGroup="1" />
                        <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btnSubmit" ValidationGroup="1"
                            OnClick="btnupdate_Click" Visible="False" />
                        <asp:Button ID="ImageButton7" runat="server" Text="Cancel" OnClick="ImageButton7_Click"
                            CssClass="btnSubmit" />
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Inventory Racks" Font-Bold="true" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            OnClick="Button1_Click1" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" class="btnSubmit" visible="False" />
                        <%-- <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                style="width: 51px;" type="button" value="Print" visible="false" />--%>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label17" Text="Select by Business Name" runat="server">
                        </asp:Label>
                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                            ValidationGroup="1" Width="200px">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label8" Text="Select by Inventory Location" runat="server">
                        </asp:Label>
                        <asp:DropDownList ID="ddlinvsiteid" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlinvsiteid_SelectedIndexChanged"
                            ValidationGroup="1" Width="200px">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label27" Text="Select by Inventory Room" runat="server">
                        </asp:Label>
                        <asp:DropDownList ID="ddlfilterinventoryroom" runat="server" AutoPostBack="True"
                            ValidationGroup="1" Width="200px" OnSelectedIndexChanged="ddlfilterinventoryroom_SelectedIndexChanged">
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
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label18" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="lblBus" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label7" runat="server" Font-Italic="True" Text="List of Inventory Racks "></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" ForeColor="Black" Font-Italic="True" Text="Inventory Location Name : "
                                                        Style="font-size: 16px;">
                                                    </asp:Label>
                                                    <asp:Label ID="lblSite" runat="server" Font-Italic="True"></asp:Label>,
                                                    <asp:Label ID="Label28" runat="server" ForeColor="Black" Font-Italic="True" Text="Inventory Room Name : "
                                                        Style="font-size: 16px;">
                                                    </asp:Label>
                                                    <asp:Label ID="lblroomprint" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowCommand="GridView1_RowCommand"
                                        OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdated="GridView1_RowUpdated"
                                        OnRowUpdating="GridView1_RowUpdating" OnSorting="GridView1_Sorting" DataKeyNames="InventortyRackID"
                                        Width="100%" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No Record Found.">
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="Name" ItemStyle-Width="20%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsiteware" runat="server" Text='<%# Bind("Name1") %>' Visible="false"> </asp:Label>
                                                    <asp:Label ID="lblwarehousename" runat="server" Text='<%# Bind("Name") %>'> </asp:Label>
                                                    <asp:Label ID="lblsiteid" runat="server" Visible="false" Text='<%# Bind("InventorySiteID") %>'> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location Name" SortExpression="InventorySiteName"
                                                ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbllocationname" runat="server" Text='<%# Bind("InventorySiteName") %>'> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Room Name" SortExpression="InventoryRoomName" ItemStyle-Width="12%"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("InventoryRoomID") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("InventoryRoomName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rack No." SortExpression="InventortyRackName" ItemStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("InventortyRackName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Expr1" HeaderText="Expr1" SortExpression="Expr1" Visible="False">
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="No. of positions on shelf" SortExpression="Numberofpositionsonshelf"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Numberofpositionsonshelf") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No. of Shelves" SortExpression="NumberofShelves" ItemStyle-Width="13%"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("NumberofShelves") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SQFT of each Shelf" SortExpression="SizeofRack" ItemStyle-Width="20%"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("SizeofRack") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="True" HeaderText="Edit" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ButtonType="Image" HeaderImageUrl="~/Account/images/edit.gif"
                                                EditImageUrl="~/Account/images/edit.gif">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
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
                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Outset"
                        Width="300px">
                        <table cellpadding="0" cellspacing="0" width="100%" id="Table1">
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" ForeColor="Black">You Sure , You want to 
                            Delete this Record?</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="ImageButton5" runat="server" CssClass="btnSubmit" Text="Yes" OnClick="yes_Click" />
                                    <%--<asp:ImageButton ID="ImageButton5" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/Yes.png"
                                    OnClick="yes_Click" />--%>
                                    <asp:Button ID="ImageButton6" runat="server" CssClass="btnSubmit" Text="No" OnClick="Buttonc1_Click" />
                                    <%--<asp:ImageButton ID="ImageButton6" runat="server" AlternateText="cancel" ImageUrl="~/ShoppingCart/images/No.png"
                                    OnClick="Buttonc1_Click" />--%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="HiddenButton" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel3" TargetControlID="HiddenButton">
                    </cc1:ModalPopupExtender>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
