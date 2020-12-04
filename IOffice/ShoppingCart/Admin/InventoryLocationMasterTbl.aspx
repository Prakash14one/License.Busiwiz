<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="InventoryLocationMasterTbl.aspx.cs" Inherits="InventoryLocationMasterTbl"
    Title="Inventory Location Master" %>

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
            var prtContent = document.getElementById('<%= Panel1.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
        function mask(evt) {
            counter = document.getElementById(id);
            alert(counter);
            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 189 || evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


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
        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
            }
        } 
        
    </script>

    <asp:UpdatePanel ID="Updaeelrt1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td width="25%">
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Select Business"></asp:Label>
                                    <asp:RequiredFieldValidator ID="ddc" runat="server" ControlToValidate="ddlWarehouse"
                                        SetFocusOnError="true" ValidationGroup="1" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td width="75%">
                                <label>
                                    <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <input id="hdninvExist" runat="server" style="width: 4px" type="hidden" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="Search Location by"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                    RepeatDirection="Horizontal" CausesValidation="True" ValidationGroup="1" Width="80%">
                                    <asp:ListItem Selected="True" Value="0">By Category</asp:ListItem>
                                    <asp:ListItem Value="1">By Name/Barcode/Product No.</asp:ListItem>
                                    <%--<asp:ListItem Value="2">ByName</asp:ListItem>--%>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlInvCat" runat="server" Width="100%">
                                    <table id="Table1" cellpadding="0" cellspacing="3" width="100%">
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:Label ID="Label7" runat="server" Text="Category"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 25%;">
                                                <label>
                                                    <asp:DropDownList ID="ddlInvCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvCat_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td style="width: 20%">
                                                <label>
                                                    <asp:Label ID="Label8" runat="server" Text="Sub Category"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:DropDownList ID="ddlInvSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubCat_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:Label ID="Label9" runat="server" Text="Sub Sub Category"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:DropDownList ID="ddlInvSubSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubSubCat_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td style="width: 20%">
                                                <label>
                                                    <asp:Label ID="Label10" runat="server" Text="Inventory Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:DropDownList ID="ddlInvName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlInvName" runat="server" Width="100%">
                                    <table id="lftpnl" cellpadding="0" cellspacing="3" width="100%">
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:Label ID="Label11" runat="server" Text="Search by"></asp:Label>
                                                    <br />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.,a-zA-Z0-9\s]*)"
                                                        ControlToValidate="txtSearchInvName" ValidationGroup="gp1"></asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td width="75%">
                                                <label>
                                                    <asp:TextBox ID="txtSearchInvName" runat="server" OnTextChanged="txtSearchInvName_TextChanged"
                                                        AutoPostBack="True" MaxLength="20" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z.,A-Z_0-9\s]+$/,'Span2',20)"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label20" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                    <span id="Span2" class="labelcount">20</span>
                                                    <asp:Label ID="lbvljyh" CssClass="labelcount" Text="(A-Z 0-9 _ . ,)" runat="server"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="text-align: left" valign="top">
                                                <label>
                                                    <asp:Label ID="lblBarcod" runat="server" Text="Barcode" Visible="False"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txtBarcode" runat="server" Visible="False" MaxLength="20"></asp:TextBox>
                                                </label>
                                            </td>
                                            <td valign="top">
                                                <label>
                                                    <asp:Label ID="lblProductno" runat="server" Text="Product No" Visible="False"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="text-align: left" valign="top">
                                                <label>
                                                    <asp:TextBox ID="txtProductNo" runat="server" Visible="False" MaxLength="15"></asp:TextBox>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%" align="right">
                                            </td>
                                            <td colspan="4" style="text-align: left">
                                                <asp:Label ID="lblInvName" runat="server" Font-Bold="True" ForeColor="Black" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlInvDDLname" runat="server" Width="100%">
                                    <table cellpadding="0" cellspacing="3" style="width: 100%">
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:Label ID="Label12" runat="server" Text="Search by"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlCatScSscNameofInv" runat="server" OnSelectedIndexChanged="ddlCatScSscNameofInv_SelectedIndexChanged"
                                                        Width="500px" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSearchGo" runat="server" OnClick="btnSearchGo_Click" Text="Go"
                                    CssClass="btnSubmit" ValidationGroup="1" Visible="false" />
                                <asp:ImageButton ID="ImgBtnSearchGo" runat="server" ImageUrl="~/Shoppingcart/images/go.png"
                                    OnClick="ImgBtnSearchGo_Click" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label13" runat="server" Text="List of Inventory Location"></asp:Label>
                    </legend>
                    <div style="clear: both;">
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="Button4" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button4_Click" />
                        <input id="Button2" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <asp:Panel ID="Panel2" runat="server" Width="100%">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div id="mydiv" class="closed">
                                            <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblCompany" runat="server" Font-Size="20px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td align="center">
                                                        <asp:Label ID="Label22" runat="server" Text="Business : " Font-Size="20px"></asp:Label>
                                                        <asp:Label ID="lblBusiness" runat="server" Font-Size="20px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="List of Inventory Location" Font-Size="18px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 16px; font-weight: normal;" align="left">
                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                        <asp:Label ID="lbltimain" runat="server" Text="Inventory Category : "></asp:Label>
                                                        <asp:Label ID="lblMainCat" runat="server"></asp:Label>
                                                        <asp:Label ID="lbltisub" runat="server" Text="Inventory Sub Category : "></asp:Label>
                                                        <asp:Label ID="lblSubCat" runat="server"></asp:Label>
                                                        <asp:Label ID="lbltisubsub" runat="server" Text="Inventory Sub Sub Category : "></asp:Label>
                                                        <asp:Label ID="lblSubSubCat" runat="server"></asp:Label>
                                                        <asp:Label ID="lbltiname" runat="server" Text="Inventory Name : "></asp:Label>
                                                        <asp:Label ID="lbliname" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdInvMasters" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                                            GridLines="Both" DataKeyNames="InventoryMasterId"
                                            AllowPaging="true" PageSize="100" OnRowCommand="grdInvMasters_RowCommand" Width="100%"
                                            EmptyDataText="No Record Found." 
                                            OnSelectedIndexChanging="grdInvMasters_SelectedIndexChanging" 
                                            onpageindexchanging="grdInvMasters_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Product No." ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductNo" runat="server" Text='<%#Bind("ProductNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category: Sub Category: Sub Sub Category" ItemStyle-Width="17%"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCategory" runat="server" Text='<%#Bind("CateAndName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <a href='Inventoryprofile.aspx?Invmid=<%# Eval("InventoryMasterId")%>' target="_blank">
                                                            <asp:Label ID="lblInvName" runat="server" Text='<%#Bind("InvName") %>' ForeColor="Black"></asp:Label>
                                                        </a>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Site" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSite" runat="server" Text=""></asp:Label>
                                                        <%--<asp:Label ID="lblSite" runat="server" Text='<%#Bind("InventorySiteName") %>'></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Room" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRoom" runat="server" Text=""></asp:Label>
                                                        <%-- <asp:Label ID="lblRoom" runat="server" Text='<%#Bind("InventoryRoomName") %>'></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rack" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRack" runat="server" Text=""></asp:Label>
                                                        <%--<asp:Label ID="lblRack" runat="server" Text='<%#Bind("InventortyRackName") %>'></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Location" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblLocation" runat="server" Text='<%#Bind("InventoryLocationName") %>'></asp:Label>--%>
                                                        <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shelf No." ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblShelf" runat="server" Text='<%#Bind("ShelfNumber") %>'></asp:Label>--%>
                                                        <asp:Label ID="lblShelf" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Position" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblPosition" runat="server" Text='<%#Bind("Position") %>'></asp:Label>--%>
                                                        <asp:Label ID="lblPosition" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Active Status">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkActiveStatusOfInv" runat="server" Checked='<%#Bind("ActiveStatusOfInv") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                <%--<asp:ButtonField CommandName="Select1" Text="Select" HeaderText="Select" ItemStyle-Width="4%"
                                                    HeaderStyle-HorizontalAlign="Left" ButtonType="Image" ImageUrl="~/Account/images/edit.gif"
                                                    HeaderImageUrl="~/Account/images/edit.gif" />--%>
                                                <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnedit" runat="server" CommandName="Select1" CommandArgument='<%#Bind("InventoryMasterId") %>'  ImageUrl="~/Account/images/edit.gif"
                                            ToolTip="Edit"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="InvMasterId" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvMasterId" runat="server" Text='<%#Bind("InventoryMasterId") %>'></asp:Label>
                                                        <asp:Label ID="lblInvWMasterId" runat="server" Text='<%#Bind("InventoryWarehouseMasterId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="InvDetailId" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvDetailId" runat="server" Text='<%#Bind("InventoryDetailsId") %>'></asp:Label>
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
                    </asp:Panel>
                </fieldset>
            </div>
            <div style="clear: both;">
            </div>
            <div style="background-color: White">
                <asp:Panel ID="Panel4" runat="server" Width="750px" Height="400px">
                    <asp:Panel ID="pnlof" runat="server" BackColor="#CCCCCC">
                        <div style="float: right; vertical-align: top;">
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                OnClick="ImageButtondsfdsfdsf123_Click" Width="16px" />
                        </div>
                        <div style="clear: both">
                        </div>
                        <fieldset style="border: 1px solid white;">
                            <table cellpadding="0" style="width: 100%">
                                <tr>
                                    <td colspan="4" style="text-align: center">
                                        <asp:Label ID="lblInvDetail" runat="server" ForeColor="Red"></asp:Label>
                                        <input id="hdninvExist0" runat="server" style="width: 4px" type="hidden" />
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label14" runat="server" Text="Select Site"></asp:Label>
                                            <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlsite"
                                                ErrorMessage="*" ValidationGroup="2" InitialValue="0"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td style="width: 22%">
                                        <label>
                                            <asp:DropDownList ID="ddlsite" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsite_SelectedIndexChanged"
                                                Width="120px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label15" runat="server" Text="Select Room"></asp:Label>
                                          <%--  
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlroomId"
                                                ErrorMessage="*" InitialValue="0" ValidationGroup="2"></asp:RequiredFieldValidator>--%>
                                        </label>
                                    </td>
                                    <td style="width: 20%">
                                        <label>
                                            <asp:DropDownList ID="ddlroomId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlroomId_SelectedIndexChanged"
                                                Width="120px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="Label16" runat="server" Text="Select Rack"></asp:Label>
                                            
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlrackid"
                                                ErrorMessage="*" InitialValue="0" ValidationGroup="2"></asp:RequiredFieldValidator>--%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlrackid" runat="server" OnSelectedIndexChanged="ddlrackid_SelectedIndexChanged"
                                                Width="120px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label17" runat="server" Text="Add Location Name"></asp:Label>
                                            
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtinvlocname"
                                                ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>--%>
                                            <br />
                                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtinvlocname"
                                                ValidationGroup="2"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtinvlocname" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@.#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'div1',20)"
                                                Width="125px"></asp:TextBox>
                                            <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="div1" class="labelcount">20</span>
                                            <asp:Label ID="Label26" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="Label18" runat="server" Text="Shelf No."></asp:Label>
                                            
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtsherlnumber"
                                                ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>--%>
                                            <br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtsherlnumber"
                                                ValidationGroup="2"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtsherlnumber" runat="server" MaxLength="10" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@.#$%_^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span1',10)"
                                                Width="125px"></asp:TextBox>
                                            <asp:Label ID="Label27" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span1" class="labelcount">10</span>
                                            <asp:Label ID="Label28" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label19" runat="server" Text="Position"></asp:Label>
                                            
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtposition"
                                                ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>--%>
                                            <br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtposition"
                                                ValidationGroup="2"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtposition" runat="server" MaxLength="10" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@.#$%_^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span3',10)"
                                                Width="125px"></asp:TextBox>
                                            <asp:Label ID="Label29" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span3" class="labelcount">10</span>
                                            <asp:Label ID="Label30" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                            CssClass="btnSubmit" ValidationGroup="2" />
                                        <asp:Button ID="btnupdate" runat="server" Text="Update" Visible="false" CssClass="btnSubmit"
                                            ValidationGroup="2" OnClick="btnupdate_Click" />
                                        &nbsp;<asp:Button ID="Button3" runat="server" Text="Cancel" CssClass="btnSubmit"
                                            OnClick="Button3_Click" />
                                        <%--<asp:ImageButton ID="btnSubmit" runat="server" AlternateText="Submit" ImageUrl="~/ShoppingCart/images/submit.png"
                                            OnClick="btnSubmit_Click" ValidationGroup="2" />--%>
                                        <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="Panel3" runat="server" Height="300px" ScrollBars="Both">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lbladd" runat="server" Text="The Inventory is located at the following places: "></asp:Label>
                                </legend>
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="true" AllowSorting="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid"
                                    DataKeyNames="InventoryLocationID" EmptyDataText="No Record Found." GridLines="Both"
                                    OnRowCommand="GridView1_RowCommand" OnSorting="GridView1_Sorting" PagerStyle-CssClass="pgr"
                                    PageSize="20" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Site Name" ItemStyle-Width="15%"
                                            SortExpression="InventorySiteName">
                                            <ItemTemplate>
                                                <asp:Label ID="invlocationid" runat="server" Text='<%#Bind("InventoryLocationID") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblproSite" runat="server" Text='<%#Bind("InventorySiteName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Room Name" ItemStyle-Width="15%"
                                            SortExpression="InventoryRoomName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblproRoom" runat="server" Text='<%#Bind("InventoryRoomName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Rack Name" ItemStyle-Width="15%"
                                            SortExpression="InventortyRackName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblproRack" runat="server" Text='<%#Bind("InventortyRackName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Shelf No." ItemStyle-Width="15%"
                                            SortExpression="ShelfNumber">
                                            <ItemTemplate>
                                                <asp:Label ID="lblproShelf" runat="server" Text='<%#Bind("ShelfNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Position" ItemStyle-Width="15%"
                                            SortExpression="Position">
                                            <ItemTemplate>
                                                <asp:Label ID="lblproPosition" runat="server" Text='<%#Bind("Position") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Specific Inventory Placement Name"
                                            ItemStyle-Width="20%" SortExpression="InventoryLocationName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblproLocation" runat="server" Text='<%#Bind("InventoryLocationName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Left"
                                            HeaderImageUrl="~/Account/images/edit.gif">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%#Bind("InventoryLocationID")%>'
                                                    CommandName="Selectlocation" ImageUrl="~/Account/images/edit.gif" ToolTip="Edit" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        <%--<asp:ButtonField ButtonType="Image" CommandName="Selectlocation" 
                                                        HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" 
                                                        HeaderText="Select" ImageUrl="~/Account/images/edit.gif" ItemStyle-Width="8%" 
                                                        Text="Select" />--%>
                                        <asp:TemplateField HeaderText="InvMasterId" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblproInvMasterId" runat="server" Text='<%#Bind("InventoryMasterId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </fieldset></asp:Panel>
                        <div style="clear: both;">
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </div>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel4"
                TargetControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearchGo" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
