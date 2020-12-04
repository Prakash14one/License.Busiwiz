<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="InventoryAdjustMaster.aspx.cs" Inherits="account_InventoryAdjustMaster"
    EnableEventValidation="true" Title="Inventory Adjust Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

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


       
        function mask(evt)
        { 

           if(evt.keyCode==13 )
            { 
         
                  return true;
             }
            
           
            if( evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }
             
                        
        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
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
        
        
            function mak(id,max_len,myele)
        {
            counter=document.getElementById(id);
            
            if(myele.value.length <= max_len)
            {
                remaining_characters=max_len-myele.value.length;
                counter.innerHTML=remaining_characters;
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

    <script type="text/javascript" language="javascript">
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label6" runat="server" Text="Make New Inventory Adjustment " Visible="False">
                        </asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Create New Adjustment" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <fieldset>
                            <table width="100%">
                                <tr>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label9" runat="server" Text="User Making Change"></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="lblUserFromSession" runat="server"></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label10" runat="server" Text="Date of Adjustment"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDate"
                                                ErrorMessage="*" InitialValue="0" ValidationGroup="gp1"> </asp:RequiredFieldValidator>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                                                TargetControlID="txtDate">
                                            </cc1:CalendarExtender>
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:TextBox ID="txtDate" runat="server" ValidationGroup="1" Width="100px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/cal_btn.jpg" />
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label11" runat="server" Text="Business Name "></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label12" runat="server" Text="Select Adjustment Reason"></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:DropDownList ID="ddlInventoryAdjustReasonName" runat="server">
                                            </asp:DropDownList>
                                        </label>
                                        <label>
                                            <asp:Button ID="imgbtnAddNewReason" runat="server" Text="Add New" OnClick="imgbtnAddNewReason_Click" />
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label13" runat="server" Text="Inventory Adjust Title"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtInvAdjustTitle"
                                                ErrorMessage="*" ValidationGroup="gpsss5"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtInvAdjustTitle"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:TextBox ID="txtInvAdjustTitle" runat="server" ValidationGroup="1" MaxLength="30"
                                                onkeypress="return checktextboxmaxlength(this,30,event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.,\s]+$/,'Span1',30)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label29" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="Span1" class="labelcount">30</span>
                                            <asp:Label ID="lbljshg" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label23" runat="server" Text="Select Type of Adjustment"></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:DropDownList ID="ddltypeofadjustment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltypeofadjustment_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="0">Change in Quantity</asp:ListItem>
                                            <asp:ListItem Value="1">Increase in Rate</asp:ListItem>
                                            <asp:ListItem Value="2">Decrease in Rate</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label24" runat="server" Text="Inventory Adjustment Details "></asp:Label>
                                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtadjustmentdetail"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td colspan="3">
                                        <label>
                                            <asp:TextBox ID="txtadjustmentdetail" runat="server" Width="400px" MaxLength="50"
                                                onkeypress="return checktextboxmaxlength(this,50,event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.,\s]+$/,'Span2',50)"
                                                ValidationGroup="1"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label25" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="Span2" class="labelcount">50</span>
                                            <asp:Label ID="Label26" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset>
                            <legend>Select the Inventory to be Adjusted </legend>
                            <table width="100%">
                                <tr>
                                    <td width="100%">
                                        <label>
                                            Select By
                                        </label>
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">  Category</asp:ListItem>
                                            <asp:ListItem Value="1">  Name / Barcode / Product No.</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnlInvCat" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 25%">
                                                        <label>
                                                            <asp:Label ID="Label14" runat="server" Text="Category"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td style="width: 25%">
                                                        <label>
                                                            <asp:DropDownList ID="ddlInvCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvCat_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                    <td style="width: 25%">
                                                        <label>
                                                            <asp:Label ID="Label15" runat="server" Text="Sub Category"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td style="width: 25%">
                                                        <label>
                                                            <asp:DropDownList ID="ddlInvSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubCat_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                        <label>
                                                            <asp:Label ID="Label16" runat="server" Text="Sub Sub Category"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td style="width: 25%">
                                                        <label>
                                                            <asp:DropDownList ID="ddlInvSubSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubSubCat_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                    <td style="width: 25%">
                                                        <label>
                                                            <asp:Label ID="Label17" runat="server" Text="Inventory Name"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td style="width: 25%">
                                                        <label>
                                                            <asp:DropDownList ID="ddlInvName" runat="server">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlInvName" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <label>
                                                            <asp:Label ID="Label18" runat="server" Text="Search by Keyword( product name, barcode, product number)"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:TextBox ID="txtSearchInvName" MaxLength="30" runat="server" onkeypress="return checktextboxmaxlength(this,30,event)"
                                                                onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.,\s]+$/,'Span6',30)"></asp:TextBox>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="Label40" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                            <span id="Span6" class="labelcount">30</span>
                                                            <asp:Label ID="Label42" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel11" runat="server" Visible="false">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Is this increase in rate of inventory because of reversing previous decrease in inventory rate ?"
                                                            OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel8" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 25%">
                                                    </td>
                                                    <td style="width: 25%">
                                                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                        <asp:Button ID="btnSearchGo" runat="server" CssClass="btnSubmit" OnClick="btnSearchGo_Click"
                                                            Text="Go" ValidationGroup="gp1" />
                                                    </td>
                                                    <td style="width: 25%">
                                                        <label>
                                                            <asp:Label ID="lblhdnAddQtyAdjust" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblhdnQtyOnHand" runat="server" Visible="false"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td style="width: 25%">
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel6" runat="server" Width="100%" Visible="False">
                                            <fieldset>
                                                <legend>List of Selected Inventory Items </legend>
                                                <table width="100%">
                                                    <tr align="center">
                                                        <td>
                                                            <div id="mydiv" class="closed">
                                                                <table width="100%" visible="false">
                                                                    <tr align="center">
                                                                        <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                            <asp:Label ID="lblCompany" runat="server" Font-Italic="True"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                            <asp:Label ID="lblBusiness" runat="server" Font-Italic="True"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                            <asp:Label ID="Label8" runat="server" Font-Italic="True" Text="List of Inventory "></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="left">
                                                                        <td>
                                                                            <asp:Label ID="Label41" runat="server" ForeColor="Black" Text="Main Category : "></asp:Label>
                                                                            <asp:Label ID="lblMainCat" runat="server" Font-Italic="True"></asp:Label>
                                                                            ,
                                                                            <asp:Label ID="Label51" runat="server" ForeColor="Black" Text="SubCategory : "></asp:Label>
                                                                            <asp:Label ID="lblSubCat" runat="server" Font-Italic="True"></asp:Label>
                                                                            ,
                                                                            <asp:Label ID="Label4" runat="server" ForeColor="Black" Text="Sub Sub Category : "></asp:Label>
                                                                            <asp:Label ID="lblSubsubCat" runat="server" Font-Italic="True"></asp:Label>
                                                                            ,
                                                                            <asp:Label ID="Label7" runat="server" ForeColor="Black" Text="Inventory Item : "></asp:Label>
                                                                            <asp:Label ID="lblInv" runat="server" Font-Italic="True"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div align="right">
                                                                <asp:Button ID="Button12" Visible="false" runat="server" Text="More Info: Normal and Abnormal Losses"
                                                                    CssClass="btnSubmit" CausesValidation="False" OnClick="Button12_Click" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="grdInvMasters" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                OnRowCommand="grdInvMasters_RowCommand" OnRowDataBound="grdInvMasters_RowDataBound"
                                                                OnRowDeleting="grdInvMasters_RowDeleting" OnRowEditing="grdInvMasters_RowEditing"
                                                                OnSorted="grdInvMasters_Sorted" OnSorting="grdInvMasters_Sorting" EmptyDataText="No Record Found.">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Business" Visible="false" SortExpression="Warehouse"
                                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIWarehouse" runat="server" Text='<%#Bind("Warehouse") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Category" SortExpression="CateAndName" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCategory" runat="server" Text='<%#Bind("CateAndName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name" SortExpression="Name" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvName" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Product No." Visible="false" SortExpression="ProductNo"
                                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblProductNo" runat="server" Text='<%#Bind("ProductNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Barcode" Visible="false" SortExpression="Barcode"
                                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBarcode" runat="server" Text='<%#Bind("Barcode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Weight/Unit" Visible="false" SortExpression="Weight"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblweight" runat="server" Text='<%# Bind("Weight")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quantity in Storage" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblQtyOnHand" Text="0" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Avg Rate" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblavgrate" Text="0" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Normal Adjusted Quantity" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnormalstar" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                                                            <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtendertxtnormaladjustqty"
                                                                                runat="server" Enabled="True" TargetControlID="txtnormaladjustqty" ValidChars="+-0147852369">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <asp:TextBox ID="txtnormaladjustqty" runat="server" MaxLength="30" Width="80px">
                                                                            </asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Abnormal Adjusted Quantity" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblabnormalstar" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                                                            <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtendertxtabnormaladjustqty"
                                                                                runat="server" Enabled="True" TargetControlID="txtabnormaladjustqty" ValidChars="+-0147852369">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <asp:TextBox ID="txtabnormaladjustqty" runat="server" Width="80px">
                                                                            </asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="New Quantity" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblINewQty" runat="server" Text=""> 
                                    
                                                                            </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblvalue" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Previous Balance of Reduction" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblpreviousbalanceofreduction" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="New Rate" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnewratestar" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                                                            <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtendertxtnewrate" runat="server"
                                                                                Enabled="True" TargetControlID="txtnewrate" ValidChars="0147852369.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <asp:TextBox ID="txtnewrate" runat="server" MaxLength="30" Width="80px">
                                                                            </asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="New Value" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnewvalue" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total New Incresed in Value" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltotalnewincreaseinvalue" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Note" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtNote1" runat="server" MaxLength="200" Width="120px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="InvMasterId" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvWHMasterId" runat="server" Text='<%#Bind("InventoryWarehouseMasterId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="InvDetailId" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvDetailId" runat="server" Text='<%#Bind("Inventory_Details_Id") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="WareHouseId" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblWareHouseId" runat="server" Text='<%#Bind("WareHouseId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Adjusted Qty" Visible="false" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender11111111" runat="server"
                                                                                Enabled="True" TargetControlID="txtAdjustQty" ValidChars="+-0147852369">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <asp:TextBox ID="txtAdjustQty" runat="server" OnTextChanged="txtAdjustQty_TextChanged"
                                                                                Width="80px">
                                                                            </asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset></asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </fieldset></asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlforview" runat="server" Width="100%" Visible="False">
                        <fieldset>
                            <legend>List of Selected Inventory Items </legend>
                            <asp:GridView ID="GridView2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                EmptyDataText="No Records found">
                                <Columns>
                                    <asp:TemplateField HeaderText="Business" SortExpression="Warehouse" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIWarehouseedit" runat="server" Text='<%#Bind("Warehouse") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category" SortExpression="CateAndName" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryedit" runat="server" Text='<%#Bind("CateAndName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="Name" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvNameedit" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product No." SortExpression="ProductNo" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductNoedit" runat="server" Text='<%#Bind("ProductNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Barcode" SortExpression="Barcode" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBarcodeedit" runat="server" Text='<%#Bind("Barcode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight/Unit" SortExpression="Weight" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblweightedit" runat="server" Text='<%# Bind("Weight")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity in Storage" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQtyOnHandedit" runat="server" Text='<%#Bind("QtyOnHand") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Avg Rate" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblavgrateedit" runat="server" Text='<%#Bind("AvgRate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Normal adjust qty" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnormaladjustedit" runat="server" Text='<%#Bind("NormalAdjust") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Abnormal adjust qty" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblabnormaladjustedit" runat="server" Text='<%#Bind("AbnormalAdjust") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="New Qty" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblINewQtyedit" runat="server" Text='<%#Bind("NewQty") %>'> 
                                    
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblvalueedit" runat="server" Text='<%#Bind("OldValue") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="New Rate" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnewrateedit" runat="server" Text='<%#Bind("NewRate") %>'> </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="New Value" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnewvalueedit" runat="server" Text='<%#Bind("NewValue") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Note" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnoteedit" runat="server" Text='<%#Bind("Notes") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </fieldset></asp:Panel>
                    <div>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" OnClick="Button3_Click"
                                        Text="Calculate" Visible="False" />
                                    <asp:Button ID="ImageButton2" runat="server" OnClick="Button1_Click" Text="Submit"
                                        CssClass="btnSubmit" ValidationGroup="gpsss5" Visible="False" />
                                    <asp:Button ID="ImageButton3" runat="server" OnClick="Button2_Click" Text="Cancel"
                                        CssClass="btnSubmit" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Previous Inventory Adjustments " runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click1" />
                            <input id="Button7" runat="server" onclick="document.getElementById('Div1').className='open';javascript:CallPrint('divPrint');document.getElementById('Div1').className='closed';"
                                type="button" value="Print" class="btnSubmit" visible="False" />
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <label>
                            <asp:Label ID="Label27" Text="Filter by Business" runat="server"></asp:Label>
                        </label>
                        <label>
                            <asp:DropDownList ID="ddlfilterbybusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbybusiness_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="Div1" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="Label5" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label34" runat="server" Font-Italic="True" Font-Size="18px" Text="Business :"></asp:Label>
                                                    <asp:Label ID="Label39" runat="server" Font-Italic="True" Font-Size="18px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label22" runat="server" Font-Italic="True" Text="List of Previous Inventory Adjustments "></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gridviewadjust" runat="server" AutoGenerateColumns="false" DataKeyNames="InventoryAdjustMasterId"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                        EmptyDataText="No Record Found." GridLines="Both" OnRowCommand="gridviewadjust_RowCommand"
                                        OnRowDeleting="gridviewadjust_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business Name" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladjid" runat="server" Text='<% #Bind("InventoryAdjustMasterId") %>'
                                                        Visible="false"></asp:Label>
                                                    <asp:Label ID="lblbusinessname" runat="server" Text='<% #Bind("Wname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Adjustment Date" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladjdate" runat="server" Text='<% #Bind("Datetime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblusername" runat="server" Text='<% #Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Adjustment Title" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladjtitle" runat="server" Text='<% #Bind("InventoryAdjustTitle") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Adjustment Reason" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladjreason" runat="server" Text='<% #Bind("InventoryAdjustReasonName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Adjustment Details" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladjustdetail" runat="server" Text='<% #Bind("AdjustDetail") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/images/view.png">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinedit" runat="server" ToolTip="View" CommandArgument='<%# Eval("InventoryAdjustMasterId") %>'
                                                        CommandName="Edit1" ImageUrl="~/images/view.png"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Button1" runat="server" ToolTip="Delete" CommandArgument='<% #Bind("InventoryAdjustMasterId") %>'
                                                        CommandName="Delete1" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Width="60%">
                                    <div>
                                        <fieldset style="border: 1px solid white;">
                                            <legend style="color: Black">Insert Adjustment Reason </legend>
                                            <div style="background-color: White;">
                                                <div style="float: right;">
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                            OnClick="ImageButton3_Click" Width="16px" />
                                                    </label>
                                                </div>
                                                <div style="clear: both;">
                                                </div>
                                                <div>
                                                    <label>
                                                        <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                                                    </label>
                                                    <label>
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        <asp:Label ID="Label19" runat="server" Text="Inventory Adjustment Reason"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtreason"
                                                            ErrorMessage="*" ValidationGroup="45"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="*"
                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                            ControlToValidate="txtreason" ValidationGroup="45">
                                                        </asp:RegularExpressionValidator>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="txtreason" runat="server" MaxLength="40" onkeypress="return checktextboxmaxlength(this,40,event)"
                                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.,\s]+$/,'Span5',40)"></asp:TextBox>
                                                        <asp:Label ID="Label20" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                        <span id="Span5" class="labelcount">40</span>
                                                        <asp:Label ID="Label32" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Button ID="ImgBtnInsertReason" runat="server" Text="Submit" CssClass="btnSubmit"
                                                            OnClick="ImgBtnInsertReason_Click" ValidationGroup="45" />
                                                    </label>
                                                    <label>
                                                        <asp:Button ID="Button11" OnClick="Button11_Click" runat="server" Text="Update" ValidationGroup="45"
                                                            Visible="false" CssClass="btnSubmit" />
                                                    </label>
                                                    <label>
                                                        <asp:Button ID="imgbtncancel" OnClick="imgbtncancel_Click" runat="server" CssClass="btnSubmit"
                                                            Text="Cancel" />
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <asp:Panel ID="Panel3" runat="server" Height="200px" ScrollBars="Vertical">
                                                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                            OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowCommand="GridView1_RowCommand"
                                                            GridLines="Both" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting"
                                                            DataKeyNames="InventoryAdjustReasonId">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Adjust Reason" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="90%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbladjustreason" runat="server" Text='<%#Bind("InventoryAdjustReasonName") %>'>
                                                                    

                                                                        </asp:Label>
                                                                        <asp:Label ID="lblAdjustResonId" runat="server" Text='<%#Bind("InventoryAdjustReasonId") %>'
                                                                            Visible="false">
                                                                  

                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtadjustreason" MaxLength="40" runat="server" Text='<%#Bind("InventoryAdjustReasonName") %>'>
                                                                    

                                                                        </asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4456464" runat="server"
                                                                            ErrorMessage="*" Display="Dynamic" ValidationGroup="455" SetFocusOnError="True"
                                                                            ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtadjustreason">
                                                                        </asp:RegularExpressionValidator>
                                                                        <asp:RequiredFieldValidator ID="ddfg" runat="server" ControlToValidate="txtadjustreason"
                                                                            ValidationGroup="455" SetFocusOnError="true" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                        <asp:Label ID="lblAdjustResonId" runat="server" Text='<%#Bind("InventoryAdjustReasonId") %>'
                                                                            Visible="false">
                                                                    

                                                                        </asp:Label>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="llinedit1123" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("InventoryAdjustReasonId") %>'
                                                                            CommandName="Edit1" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete1" CommandArgument='<%# Eval("InventoryAdjustReasonId") %>'
                                                                            ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                                        </asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        <asp:Button ID="imgbtnGotoAdjustInv" runat="server" CssClass="btnSubmit" Visible="false"
                                                            Text="Back" OnClick="imgbtnGotoAdjustInv_Click" />
                                                    </label>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </asp:Panel>
                                <asp:Button ID="Button10" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1333" runat="server" PopupControlID="Panel2"
                                    TargetControlID="Button10" CancelControlID="ImageButton4">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Width="70%">
                                    <fieldset>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <div align="right">
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Please note that you are recording an abnormal loss of inventory. This will 
                                                    not adjust the average cost of the remaining inventory in stock.
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        The following journal entry will be recorded in your records.
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        <asp:Label ID="lblabnormalinventoryloss" runat="server"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="lblreductionininventory" runat="server"></asp:Label>
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        <asp:DropDownList ID="ddlacc1" Enabled="false" runat="server" Width="400px">
                                                        </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="lblabnormalinventorylossdebitcerdit" runat="server"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label33" runat="server" Text="Amount"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="lblabnormalinventorylossamount" runat="server"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label28" runat="server" Text="Memo"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="txtmemo1" MaxLength="500" Width="400px" runat="server" TextMode="MultiLine"
                                                            onkeypress="return checktextboxmaxlength(this,500,event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.,\s]+$/,'Span4',500)"></asp:TextBox>
                                                        <asp:Label ID="Label37" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                        <span id="Span4" class="labelcount">500</span>
                                                        <asp:Label ID="Label38" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        <asp:DropDownList ID="ddlacc2" Enabled="false" runat="server" Width="400px">
                                                        </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="lblreductionininventorydebitcredit" runat="server"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label31" runat="server" Text="Amount"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="lblreductionininventoryamount" runat="server"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label30" runat="server" Text="Memo"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="txtmemo2" MaxLength="500" Width="400px" TextMode="MultiLine" runat="server"
                                                            onkeypress="return checktextboxmaxlength(this,500,event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.,\s]+$/,'Span3',500)"></asp:TextBox>
                                                        <asp:Label ID="Label35" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                        <span id="Span3" class="labelcount">500</span>
                                                        <asp:Label ID="Label36" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="Button2" CssClass="btnSubmit" runat="server" Text="Confirm" OnClick="Button2_Click1" />
                                                    <asp:Button ID="Button5" CssClass="btnSubmit" runat="server" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="Button4" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel1" TargetControlID="Button4" CancelControlID="Button5">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <table>
                        <tr>
                            <td align="left">
                                <asp:Panel ID="Panel5" runat="server" CssClass="modalPopup" Width="600px">
                                    <fieldset>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <div align="right">
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <tr>
                                                <td>
                                                    <label>
                                                        You are increasing the value of inventory , this is against the 
                                                    International Accounting Standard - 2. Are you sure you wish to increase the 
                                                    value of inventory ?
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="Button6" CssClass="btnSubmit" runat="server" Text="Confirm" OnClick="Button6_Click" />
                                                    <asp:Button ID="Button8" CssClass="btnSubmit" runat="server" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    &nbsp;&nbsp;&nbsp;</asp:Panel>
                                <asp:Button ID="Button9" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel5" TargetControlID="Button9" CancelControlID="Button8">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <asp:Panel ID="Panel9" runat="server" CssClass="modalPopup" Width="70%">
                                    <fieldset>
                                        <div align="right">
                                            <asp:ImageButton ID="ImageButton8" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                Width="15px" />
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <asp:Panel ID="Panel10" runat="server" Width="100%">
                                            <div>
                                                <label>
                                                    Normal loss or gain in inventory quantity reflects excess or shortages in 
                                                inventory levels during the regular course of business. For example, there may 
                                                be breakage or spoilage of inventory items, which happens regularly in the 
                                                normal course of business.
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <label>
                                                    When you apply certain changes in inventory quantity as normal loss or gain, 
                                                the system will adjust the actual quantity to match your stated quantity. 
                                                However, it will keep the value of the inventory at the same level of what it 
                                                was before your changes by increasing the average cost of items adjusted.
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <label>
                                                    Abnormal loss or gain in inventory quantity refelcts excess or shortages in 
                                                inventory levels because of abnormal events or circumstances. For example, there 
                                                may be natural disasters such as flooding, hurricanes or tornedors which are not 
                                                regular for your business.
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <label>
                                                    When you select certain changes in inventory quantity as abnormal loss or 
                                                gain the system will adjust the actual quantity to match with your stated 
                                                quantity. However, it will not change the average cost of the items so the value 
                                                of the inventory will not remain at the same level of what it was before your 
                                                changes. The abnormal loss of inventory will not affect the value of the cost of 
                                                goods sold in your financial statement but it will be shown as a seperate line 
                                                in the income statement.
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <label>
                                                    The following journal entry will be recorded on the date of the changes you 
                                                made to reflect the abnormal loss.
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <label>
                                                    Debit
                                                </label>
                                                <label>
                                                    Abnormal Inventory Loss
                                                </label>
                                                <label>
                                                    A/C in
                                                </label>
                                                <label>
                                                    (Group: Other Operating Expenses and Losses)
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <label>
                                                    Credit
                                                </label>
                                                <label>
                                                    Inventory
                                                </label>
                                                <label>
                                                    A/C in
                                                </label>
                                                <label>
                                                    (Group: Inventory)
                                                </label>
                                            </div>
                                        </asp:Panel>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="Button15" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel9" TargetControlID="Button15" CancelControlID="ImageButton8">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel4" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Outset"
                                    Width="300px">
                                    <table id="subinnertbl" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="subinnertblfc">
                                                Confirm Delete
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label3" runat="server" ForeColor="Black">You Sure , You Want to 
                                                Delete !</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:ImageButton ID="ImageButton5" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/Yes.png"
                                                    OnClick="ImageButton5_Click" />
                                                <asp:ImageButton ID="ImageButton6" runat="server" AlternateText="cancel" ImageUrl="~/ShoppingCart/images/No.png"
                                                    OnClick="ImageButton6_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1444" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel4" TargetControlID="HiddenButton222">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel7" runat="server" Width="80%" CssClass="modalPopup" Height="400px"
                                    ScrollBars="Vertical">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="Label21" Text="List of Inventory Adjusted " runat="server"></asp:Label>
                                        </legend>
                                        <div style="background-color: White;">
                                            <div style="float: right;">
                                                <label>
                                                    <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" OnClick="ImageButton7_Click1" />
                                                </label>
                                            </div>
                                            <asp:GridView ID="gridvi" runat="server" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                CssClass="mGrid" GridLines="Both" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Business Name" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblwarehouse" runat="server" Text='<% #Bind("Warehouse") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ProductNo" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblproductno" runat="server" Text='<% #Bind("ProductNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category:Sub Category:Sub Sub Category" ItemStyle-Width="50%"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcatandname" runat="server" Text='<% #Bind("CateAndName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinvname" runat="server" Text='<% #Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity Adjusted" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblproductno" runat="server" Text='<% #Bind("QuantityAdjusted") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="Button212321" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1455454" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel7" TargetControlID="Button212321">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
