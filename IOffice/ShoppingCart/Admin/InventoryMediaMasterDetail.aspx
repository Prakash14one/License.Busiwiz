<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="InventoryMediaMasterDetail.aspx.cs" Inherits="ShoppingCart_Admin_InventoryMediaMasterDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
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
          function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
    </script>

    <asp:UpdatePanel ID="uppae" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label10" runat="server" Text="Search for inventory for which you wish to create a media file"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td width="25%">
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Select Business Name"></asp:Label>
                                </label>
                            </td>
                            <td width="25%">
                                <label>
                                    <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td width="25%">
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="Search by Name"></asp:Label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                        SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtSearchInvName"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td width="25%">
                                <label>
                                    <asp:TextBox ID="txtSearchInvName" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!@.#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'Span2',30)">
                                    </asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label ID="Label40" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                    <span id="Span2" class="labelcount">30</span>
                                    <asp:Label ID="Label19" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Category"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlInvCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvCat_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Sub Category"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlInvSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubCat_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Sub Sub Category"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlInvSubSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubSubCat_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="Select Product"></asp:Label>
                                </label>
                            </td>
                            <td align="left">
                                <label>
                                    <asp:DropDownList ID="ddlInvName" runat="server">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnSearchGo" runat="server" OnClick="imgBtnSearchGo_Click" Text="Go  "
                                    ValidationGroup="1" CssClass="btnSubmit" />
                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        
                    </table>
                </fieldset>
                <asp:Panel ID="Panel1" runat="server" Height="140px" ScrollBars="Both" Visible="False"
                    Width="100%">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label11" runat="server" Text="List of Inventory for which you wish to create a media file"></asp:Label>
                        </legend>
                        <asp:GridView ID="grdInvMasters" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            PagerStyle-CssClass="pgr" GridLines="Both" AlternatingRowStyle-CssClass="alt"
                            DataKeyNames="InventoryMasterId" EmptyDataText="No Record Found." OnRowCommand="grdInvMasters_RowCommand"
                            Width="100%" AllowSorting="True" OnSorting="grdInvMasters_Sorting">
                            <Columns>
                                <asp:TemplateField HeaderText="Category : Sub Category : Sub Sub Category" SortExpression="CatScSsc"
                                    HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%#Bind("CatScSsc") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle VerticalAlign="Top" Width="35%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventory Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="InvName" runat="server" Text='<%#Bind("Name") %>' ForeColor="#416271"
                                            CommandName="name" CommandArgument='<%#Bind("InventoryMasterId") %>' OnClick="linkinvetory_click"></asp:LinkButton>
                                        <asp:Label ID="lblInvName" runat="server" Text='<%#Bind("Name") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle VerticalAlign="Top" Width="25%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product No." SortExpression="ProductNo" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductNo" runat="server" Text='<%#Bind("ProductNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle VerticalAlign="Top" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight" SortExpression="Unit" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvweight" runat="server" Text='<%#Bind("Unit") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle VerticalAlign="Top" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit" SortExpression="unittype" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvunit" runat="server" Text='<%#Bind("unittype") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle VerticalAlign="Top" Width="6%" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Barcode" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBarcode" runat="server" Text='<%#Bind("Barcode") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" Width="12%" />
                                </asp:TemplateField>--%>
                                <asp:ButtonField CommandName="Select1" ItemStyle-ForeColor="#416271" HeaderStyle-HorizontalAlign="Left"
                                    Text="Select" HeaderText="View" ItemStyle-Width="5%">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle ForeColor="#416271" Width="5%" />
                                </asp:ButtonField>
                                <asp:TemplateField HeaderText="InvMasterId" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvMasterId" runat="server" Text='<%#Bind("InventoryMasterId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr" />
                            <AlternatingRowStyle CssClass="alt" />
                        </asp:GridView>
                    </fieldset></asp:Panel>
                <asp:Panel ID="pnlViewFill" runat="server" Visible="False" Width="100%">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label8" runat="server" Text="The following media files are available for: "></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblInvCScSScName" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td align="right">
                                    <asp:Button ID="btntest" runat="server" Text=" Add New " CssClass="btnSubmit" OnClick="btntest_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" DataKeyNames="InventoryMediaMasterID" AlternatingRowStyle-CssClass="alt"
                                        Width="100%" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound"
                                        EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="File Title" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfiletitle" runat="server" Text='<%# Eval("MediaTitle")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfilename" runat="server"></asp:Label>
                                                    <asp:Label ID="lblfullname" Visible="false" runat="server" Text='<%# Eval("FileName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldescription" runat="server" Text='<%# Eval("MediaFileDesc")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Media Type" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblview" runat="server" Text='<%# Eval("MediaFileType")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Test" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#416271"
                                                ItemStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:Button ID="btntest" OnClick="linktest_Click" runat="server" Text=" Test " CommandArgument='<%# Eval("MediaFileType") %>'
                                                        CssClass="btnSubmit" />
                                                    <%-- <asp:LinkButton ID="linktest" OnClick="linktest_Click" runat="server" Text="Test" CommandArgument='<%# Eval("MediaFileType") %>' ForeColor="#416271"></asp:LinkButton>--%>
                                                    <%--<a id="playaudio" visible="false"  onclick="window.open('../PlayAudio.aspx?id=<%#DataBinder.Eval(Container.DataItem, "InventoryMasterId")%>', 'welcome','width=200,height=220,menubar=no,status=no')" href="javascript:void(0)" style="color:#416271">Test</a>--%>
                                                    <%--<a id="playvideo" visible="false"  onclick="window.open('../PlayVideo.aspx?id=<%#DataBinder.Eval(Container.DataItem, "InventoryMasterId")%>', 'welcome','width=230,height=250,menubar=no,status=no')" href="javascript:void(0)" style="color:#416271"></a>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:CommandField  CausesValidation="false" ShowEditButton="True"  HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Edit"    ButtonType="Image" EditImageUrl="~/Account/images/edit.gif" HeaderImageUrl="~/Account/images/edit.gif" 
                                          UpdateImageUrl="~/Account/images/UpdateGrid.JPG" CancelImageUrl="~/images/delete.gif" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%" />--%>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Btn" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
                <asp:Panel ID="pnladdnew" runat="server" Visible="false" Width="100%">
                    <fieldset>
                        <table width="100%">
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text="Upload Media File "></asp:Label>
                                        <asp:Label ID="Label17" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FileUpload1"
                                            ErrorMessage="*" ValidationGroup="11"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td colspan="3">
                                   
                                    <label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </label>
                                     <label>
                                        <asp:Label ID="Label13" runat="server" Text="Media Type "></asp:Label>
                                        <asp:Label ID="Label16" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldVdator3" runat="server" ControlToValidate="DropDownList2"
                                            ErrorMessage="*" InitialValue="0" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="DropDownList2" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                   
                                    <label>
                                        <asp:Button ID="Button1" runat="server" Text="Upload " ValidationGroup="11"
                                            OnClick="Button1_Click1" />
                                    </label>
                                     <label>
                                        <asp:Label ID="lblfilen" runat="server" Text=""></asp:Label>
                                    </label>
                                    
                                </td>
                            </tr>
                           <tr>
                                <td style="width: 25%" valign="top">
                                    <label>
                                        <asp:Label ID="Label9" runat="server" Text="File Title "></asp:Label>
                                        <asp:Label ID="Label18" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfiletitle"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtfiletitle" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 80%" valign="top" colspan="3">
                                   
                                    <label>
                                        <asp:TextBox ID="txtfiletitle" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-z_A-Z0-9\s]+$/,'div1',20)"
                                            runat="server" MaxLength="20">
                                        </asp:TextBox>
                                    </label>
                                     <label>
                                        <asp:Label ID="Label20" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="div1" class="labelcount">20</span>
                                        <asp:Label ID="lblbf" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="File Description"></asp:Label>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                        runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([._a-zA-Z0-9\s]*)"
                                            ControlToValidate="txttaskinstruction" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                        runat="server" ErrorMessage="*"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,300})$"
                                            ControlToValidate="txttaskinstruction" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td colspan="2" align="left">
                                    <label>
                                        <asp:TextBox ID="txttaskinstruction" runat="server" Height="128px" onkeypress="return checktextboxmaxlength(this,300,event)"
                                            Width="400px" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z._A-Z0-9\s]+$/,'Span1',300)"
                                            TextMode="MultiLine"></asp:TextBox>
                                        <asp:Label ID="Label21" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span1" class="labelcount">300</span>
                                        <asp:Label ID="Label15" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                                    </label>
                                </td>
                                <td valign="bottom">
                                </td>
                            </tr>
                           
                          
                            <tr>
                                <td>
                                    
                                </td>
                                 <td colspan="2">
                                <asp:Button ID="ImageButton1" runat="server" OnClick="Button1_Click" ValidationGroup="1"
                                        CssClass="btnSubmit" Text="Upload File" />
                                    <asp:Button ID="ImageButton3" runat="server" Text="Cancel" OnClick="ImageButton3_Click"
                                        CssClass="btnSubmit" />
                                </td>
                                 <td>
                                </td>
                               
                               
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel8" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA" BorderStyle="Outset"
                    Width="300px">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="lblm" runat="server" Text="By adding this media file, you will delete your previously added media file. Do you wish to proceed ?"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="Button3" runat="server" Text=" Yes " CssClass="btnSubmit" OnClick="Button3_Click" />
                                <asp:Button ID="Button4" runat="server" Text=" No " CssClass="btnSubmit" OnClick="Button4_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button2" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel8" TargetControlID="Button2">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btntest" />
            <asp:PostBackTrigger ControlID="Button1" />
            <asp:PostBackTrigger ControlID="ImageButton1" />
            <asp:PostBackTrigger ControlID="Button3" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
