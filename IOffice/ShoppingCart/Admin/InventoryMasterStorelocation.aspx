<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" EnableEventValidation="true" CodeFile="InventoryMasterStorelocation.aspx.cs"
    Inherits="ShoppingCart_Admin_InventoryMasterStorelocation" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 173px;
        }
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
            var prtContent = document.getElementById('<%= Panel2.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        function RealNumWithDecimal(myfield, e, dec) {

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
        function mask(evt)
        { 

           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if( evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered invalid character");
                  return false;
             }
             
             
               
            
        }  
        function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered invalid character");
            }
        
        
      
        
            counter=document.getElementById(id);
            
            if(txt1.value.length <= max_len)
            {
                remaining_characters=max_len-txt1.value.length;
                counter.innerHTML=remaining_characters;
            }
        }    
            function mak(id,max_len,myele)
        {
            counter=document.getElementById(id);
            
            if(myele.value.length <= max_len)
            {
                remaining_characters=max_len-myele.value.length;
                counter.innerHTML=remaining_characters;
            }
        }      
          
    </script>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    <table id="Table2" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel4" runat="server" Width="100%">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="20%">
                                                <label>
                                                    <asp:Label ID="Label14" runat="server" Text="  Select Business"></asp:Label>
                                                </label>
                                            </td>
                                            <td width="80%">
                                                <label>
                                                    <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Center" Width="100%">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="right" width="20%">
                                                <label>
                                                    <asp:Label ID="Label15" runat="server" Text="Select"></asp:Label>
                                                </label>
                                            </td>
                                            <td align="left" width="80%">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal" Width="75%">
                                                    <asp:ListItem Value="0" Selected="True">By Category</asp:ListItem>
                                                    <asp:ListItem Value="1">By Name / Barcode / Product No.</asp:ListItem>
                                                    <asp:ListItem Value="2">By Name</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlInvCat" runat="server" Width="100%">
                                    <table id="Table1" align="center" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="20%">
                                                <label>
                                                    <asp:Label ID="Label16" runat="server" Text=" Category"></asp:Label>
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
                                                    <asp:Label ID="Label17" runat="server" Text=" Sub Category"></asp:Label>
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
                                                    <asp:Label ID="Label18" runat="server" Text=" Sub Sub Category "></asp:Label>
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
                                                    <asp:Label ID="Label19" runat="server" Text=" Inventory Name "></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlInvName" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlInvName" runat="server" Width="100%" Visible="False">
                                    <table id="Table3" align="center" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="20%">
                                                <label>
                                                    <asp:Label ID="lbln" runat="server" Text="Name "></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSearchInvName"
                                                        ErrorMessage="*" InitialValue="0" ValidationGroup="50"></asp:RequiredFieldValidator>
                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                                            ErrorMessage="*" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"  
                                                            ControlToValidate="txtSearchInvName" ValidationGroup="50"></asp:RegularExpressionValidator>--%>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="txtSearchInvName" MaxLength="20" runat="server" Width="150px"></asp:TextBox>
                                                </label>
                                                <%-- <br />
                                                    <label>
                                                        <asp:Label ID="Label9" runat="server" Text="(Max. 20 digits,A-Z,0-9,_)" ></asp:Label>
                                                    </label>     --%>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lblBarcod" runat="server" Text="Barcode "></asp:Label>
                                                    <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                            ErrorMessage="*" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"  
                                                            ControlToValidate="txtBarcode" ValidationGroup="50"></asp:RegularExpressionValidator>--%>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="txtBarcode" runat="server" MaxLength="20" Width="150px"></asp:TextBox>
                                                </label>
                                                <%--<br />
                                                    <label>
                                                        <asp:Label ID="Label12" runat="server" Text="(Max. 20 digits,A-Z,0-9)" ></asp:Label>
                                                    </label>    --%>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lblProductno" runat="server" Text="Product No."></asp:Label>
                                                    <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                            ErrorMessage="*" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"  
                                                            ControlToValidate="txtProductNo" ValidationGroup="50"></asp:RegularExpressionValidator>--%>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="txtProductNo" runat="server" Visible="true" MaxLength="20" Width="150px"></asp:TextBox>
                                                </label>
                                                <%-- <br />
                                                    <label>
                                                         <asp:Label ID="Label13" runat="server" Text="(Max. 20 digits,A-Z,0-9)" ></asp:Label>
                                                    </label>    --%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="3" style="text-align: left">
                                                <asp:Label ID="lblInvName" runat="server" Font-Bold="True" ForeColor="Green" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="text-align: left">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlInvDDLname" runat="server" Width="100%">
                                    <table id="lftpnl" cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td width="20%">
                                                <label>
                                                    <asp:Label ID="Label20" runat="server" Text=" By Name"></asp:Label>
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
                                    Visible="False" ValidationGroup="50" />
                                <asp:Button ID="buttongo" runat="server" OnClick="ImgBtnSearchGo_Click" Text="  Go  "
                                    CssClass="btnSubmit" ValidationGroup="50" />
                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btncancel0" runat="server" CssClass="btnSubmit" CausesValidation="false"
                                OnClick="btncancel0_Click" Text="Printable Version" />
                            <input id="Button2" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                class="btnSubmit" type="button" value="Print" visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server" Visible="False" Width="100%">
                                        <fieldset>
                                            <legend>
                                                <asp:Label ID="Label5" runat="server" Text="Select the Inventory Item to Manage"></asp:Label>
                                            </legend>
                                            <asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Both" Visible="False"
                                                Width="100%">
                                                <table width="100%">
                                                    <tr align="center">
                                                        <td>
                                                            <div id="mydiv" class="closed">
                                                                <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblCompany" runat="server" Font-Size="20px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="Label8" runat="server" Font-Size="20px" Text="Business :"></asp:Label>
                                                                            <asp:Label ID="lblBusiness" runat="server" Font-Size="20px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="Label4" runat="server" Font-Size="18px" Text="List of Inventory "></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="font-size: 16px; font-weight: normal;">
                                                                            <asp:Label ID="lblMainCat" runat="server" Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblSubCat" runat="server" Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblSubsubCat" runat="server" Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblname" runat="server" Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblbarcode" runat="server" Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblproductnum" runat="server" Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblddlname" runat="server" Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="grdInvMasters" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowCommand="grdInvMasters_RowCommand"
                                                                Width="100%" AllowSorting="True" OnSorting="grdInvMasters_Sorting" OnSelectedIndexChanged="grdInvMasters_SelectedIndexChanged"
                                                                EmptyDataText="No Record Found.">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Category" SortExpression="CateAndName" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-Width="25%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCategory" runat="server" Text='<%#Bind("CateAndName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Product No." SortExpression="ProductNo" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblProductNo" runat="server" Text='<%#Bind("ProductNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-Width="25%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvName" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Description" Visible="false" SortExpression="Description"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Panel ID="PnlDesc" runat="Server" Height="50px" ScrollBars="Vertical">
                                                                                <asp:Label ID="lblDesc" runat="server" Text='<%#Bind("Description") %>'></asp:Label>
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Barcode" SortExpression="Barcode" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-Width="8%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBarcode" runat="server" Text='<%#Bind("Barcode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Weight" SortExpression="Weight" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <%--<asp:Label ID="lblWeight" runat="server" Text='<%#Bind("Weight") %>' ></asp:Label>--%>
                                                                            <asp:Label ID="lblWeightUnitType" runat="server" Text='<%#Bind("UnitTypeName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:ButtonField CommandName="Select1" ItemStyle-VerticalAlign="Top" ItemStyle-ForeColor="#416271"
                                                                        Text="Manage" HeaderText="Manage" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle ForeColor="#416271" VerticalAlign="Top" Width="5%" />
                                                                    </asp:ButtonField>
                                                                    <asp:TemplateField HeaderText="InvMasterId" Visible="False" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvMasterId" runat="server" Text='<%#Bind("InventoryMasterId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="InvDetailId" Visible="False" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvDetailId" runat="server" Text='<%#Bind("Inventory_Details_Id") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                                <AlternatingRowStyle CssClass="alt" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </fieldset>
                                        <asp:Panel ID="updatepnl" runat="server" Visible="False" Width="100%">
                                            <fieldset>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                   
                                                    <tr>
                                                        <td colspan="4" style="text-align: center">
                                                            <asp:Label ID="lblInvDetail" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                                            <input id="hdninvExist" runat="server" style="width: 4px" type="hidden" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20%">
                                                            <label>
                                                                <asp:Label ID="Label26" runat="server" Text="Inventory Start Date"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtqtyondatestarted"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td style="width: 30%">
                                                            <label>
                                                                <asp:TextBox ID="txtqtyondatestarted" runat="server" Width="120px" Enabled="False"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtqtyondatestarted"
                                                                    TargetControlID="txtqtyondatestarted">
                                                                </cc1:CalendarExtender>
                                                                <cc1:MaskedEditExtender ID="TextBox1_MaskedEditExtender" runat="server" CultureName="en-AU"
                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtqtyondatestarted" />
                                                            </label>
                                                        </td>
                                                        <td style="height: 5px; width: 173px;">
                                                            <label>
                                                                <asp:Label ID="Label28" runat="server" Text="Initial Quantity"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtOpeingQty"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3fsd" runat="server" Enabled="True"
                                                                    TargetControlID="txtOpeingQty" ValidChars="0147852369." />
                                                            </label>
                                                        </td>
                                                        <td style="height: 5px">
                                                            <label>
                                                                <asp:TextBox ID="txtOpeingQty" runat="server" Width="120px" Enabled="False" MaxLength="10">0</asp:TextBox>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label25" runat="server" Text="Quantity On Hand"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtotyonhand"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtotyonhand" runat="server" Width="120px" OnTextChanged="txtotyonhand_TextChanged"
                                                                    Enabled="False" MaxLength="10">0</asp:TextBox>
                                                                <asp:Label ID="lblqtyonhand" runat="server"></asp:Label>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1terf" runat="server" Enabled="True"
                                                                    TargetControlID="txtotyonhand" ValidChars="0147852369." />
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label30" runat="server" Text="Initial Rate"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Text="$"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtOpeingRAte"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtOpeingRAte" runat="server" Width="120px" Enabled="False" MaxLength="10">0</asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                                    TargetControlID="txtOpeingRAte" ValidChars="0147852369." />
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label21" runat="server" Text="Sales Rate"></asp:Label>
                                                                <asp:Label ID="curry" runat="server" Text="$"></asp:Label>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtRate"
                                                                    ValidationGroup="1" ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server"
                                                                    ErrorMessage="Invalid"> </asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRate"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtRate" runat="server" onkeyup="return mak('Span1',20,this)" Width="110px"
                                                                    MaxLength="20"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                                                    Enabled="True" TargetControlID="txtRate" ValidChars="0147852369.">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </label>
                                                            <label>
                                                                Chars Rem <span id="Span1">20</span>
                                                                <asp:Label ID="Label1" runat="server" Text="(0-9)"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td style="width: 20%">
                                                            <label>
                                                                <asp:Label ID="Label22" runat="server" Text="Preferred Vendor"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldVar2" runat="server" ControlToValidate="ddlPreferedVendor"
                                                                    ErrorMessage="*" InitialValue="0" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td style="width: 30%">
                                                            <label>
                                                                <asp:DropDownList ID="ddlPreferedVendor" runat="server" Width="126px">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label10" runat="server" Text="Safety Stock"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtreorderquantiy"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtsafetystock" runat="server" Width="120px" MaxLength="10"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                    TargetControlID="txtsafetystock" ValidChars="0147852369" />
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label11" runat="server" Text="Lead Days"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtreorderlevel"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtleaddays" runat="server" Width="120px" MaxLength="10"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" 
                                                                    TargetControlID="txtleaddays" ValidChars="0147852369" />
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label12" runat="server" Text="Lead Days Usage"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtreorderlevel"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtleaddaysusage" runat="server" Width="120px" MaxLength="10">0</asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" 
                                                                    TargetControlID="txtleaddaysusage" ValidChars="0123456789" />
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label24" runat="server" Text="Re-Order Level"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtreorderlevel"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtreorderlevel" runat="server" Width="120px" Enabled="False" MaxLength="10">0</asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22edgd" runat="server"  Enabled="True"
                                                                    TargetControlID="txtreorderlevel" ValidChars="0147852369." />
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label6" runat="server" Text="Economic Order Quantity"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="lbleoq" runat="server" Text="0"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label23" runat="server" Text="Re-Order Quantity"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3terd" runat="server" ControlToValidate="txtreorderquantiy"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtreorderquantiy" runat="server" Width="120px" 
                                                                    MaxLength="10">0</asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12gfgdf" runat="server" Enabled="True"
                                                                    TargetControlID="txtreorderquantiy" ValidChars="0147852369." />
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label29" runat="server" Text="Inventory Status"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlstatus" runat="server" Width="100px">
                                                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td valign="top">
                                                            <label>
                                                                <asp:Label ID="Label9" runat="server" Text="Weight"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td valign="top">
                                                            <label>
                                                                <asp:TextBox ID="txtWeight" runat="server" AutoPostBack="true" MaxLength="15" onkeypress="return RealNumWithDecimal(this,event,2);"
                                                                    Width="90px" onkeyup="return mak('Span5',15,this)">0</asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:DropDownList ID="ddllbs" runat="server" Width="59px">
                                                                </asp:DropDownList>
                                                            </label>
                                                            <label>
                                                                Chars Rem <span id="Span5">15</span>
                                                                <asp:Label ID="Label62" runat="server" Text="(0-9)"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label27" runat="server" Text="Quantity Last Purchased"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                             <asp:Label ID="lblqtylastpurchased" runat="server" Text="Quantity Last Purchased"></asp:Label>
                                                                <asp:TextBox ID="txtnormalorderquantity" runat="server" Width="120px" Visible="false" Enabled="False"
                                                                    MaxLength="10"></asp:TextBox>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label13" runat="server" Text="Rate of Quantity Last Purchased"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                         <label>
                                                                <asp:Label ID="lblrateqtylastpurchased" runat="server" ></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="text-align: center">
                                                        <asp:Button ID="Button3" runat="server" Text="Calculate" CssClass="btnSubmit" 
                                                                ValidationGroup="2" onclick="Button3_Click" />
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btnSubmit_Click"
                                                                ValidationGroup="2" />
                                                            <asp:Button ID="ImageButton3" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="ImageButton3_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset></asp:Panel>
                                        <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Outset"
                                            Width="300px">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="lblm" runat="server">Please check the date.</asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label3" runat="server" Text="Start Date of the Year is "></asp:Label>
                                                            <asp:Label ID="lblstartdate0" runat="server"></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="lblm0" runat="server" Text="You can not select any date earlier than that. "></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="height: 26px">
                                                        <asp:Button ID="ImageButton1" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="ImageButton2_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            &nbsp;</asp:Panel>
                                        <asp:Button ID="Button1" runat="server" Style="display: none" />
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                            PopupControlID="Panel5" TargetControlID="Button1">
                                        </cc1:ModalPopupExtender>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSearchGo" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
