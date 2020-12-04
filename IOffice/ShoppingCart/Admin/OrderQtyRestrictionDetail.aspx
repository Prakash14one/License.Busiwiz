<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="OrderQtyRestrictionDetail.aspx.cs" Inherits="ShoppingCart_Admin_OrderQtyRestrictionDetail"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

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
    </script>

    <script type="text/javascript" language="javascript">
function ChangeCheckBoxState(id, checkState)
        {
            var cb = document.getElementById(id);
            if (cb != null)
               cb.checked = checkState;
        }
        // For Document
        function ChangeAllCheckBoxStates(checkState)
        {
            if (CheckBoxIDs != null)
            {
               for (var i = 0; i < CheckBoxIDs.length; i++)
               ChangeCheckBoxState(CheckBoxIDs[i], checkState);
            }
        }        
        function ChangeHeaderAsNeeded()
        {
            if (CheckBoxIDs != null)
            {
                for (var i = 1; i < CheckBoxIDs.length; i++)
                {
                    var cb = document.getElementById(CheckBoxIDs[i]);
                    if (!cb.checked)
                    {
                       ChangeCheckBoxState(CheckBoxIDs[0], false);
                       return;
                    }
                }        
               ChangeCheckBoxState(CheckBoxIDs[0], true);
            }
        }
        function SelectAllCheckboxes(spanChk){

   // Added as ASPX uses SPAN for checkbox
   var oItem = spanChk.children;
   var theBox= (spanChk.type=="checkbox") ? 
        spanChk : spanChk.children.item[0];
   xState=theBox.checked;
   elm=theBox.form.elements;

   for(i=0;i<elm.length;i++)
     if(elm[i].type=="checkbox" && 
              elm[i].id!=theBox.id)
     {
       //elm[i].click();
       if(elm[i].checked!=xState)
         elm[i].click();
       //elm[i].checked=xState;
     }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div class="products_box">
                <div style="padding-left:1%">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                
                <fieldset>
                    <legend>
                       <asp:Label ID="Label7" runat="server" Font-Bold="true"  Text="List of Order Quantity Restriction Rules"></asp:Label> 
                    </legend>
                     <div style="padding-left:1%">
                     </div>
                     <label>  <asp:Label ID="Label18" runat="server"  Text="Filter by Business:"></asp:Label> 
        </label>
        <label>
        <asp:DropDownList ID="ddlfilterbusness" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlfilterbusness_SelectedIndexChanged" >
                                        </asp:DropDownList>
        </label>
                      <div style="padding-left:1%">
                     </div>
            <label>
                          <asp:Panel ID="Panel3" runat="server" Width="100%">
                            <asp:GridView ID="GrdOrdrQtyMaster" runat="server" AutoGenerateColumns="False" 
                                CssClass="mGrid" PagerStyle-CssClass="pgr" 
                    AlternatingRowStyle-CssClass="alt" 
                                DataKeyNames="OrderQtyRestrictionMasterId" OnPageIndexChanging="GrdOrdrQtyMaster_PageIndexChanging"
                                ShowFooter="True" OnRowCancelingEdit="GrdOrdrQtyMaster_RowCancelingEdit"
                                OnRowCommand="GrdOrdrQtyMaster_RowCommand" OnRowEditing="GrdOrdrQtyMaster_RowEditing"
                                OnRowUpdating="GrdOrdrQtyMaster_RowUpdating" Width="100%" 
                                onrowdeleting="GrdOrdrQtyMaster_RowDeleting" >
                                
                                <Columns>
                                  <asp:TemplateField HeaderText="Business Name" FooterStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left">
                                         <EditItemTemplate>
                                                                         
                                        <asp:Label ID="lblwid1" Text='<%#Bind("Whid") %>' Visible="false" runat="server"></asp:Label>
                                  
                                        <asp:DropDownList ID="ddlwarename" runat="server" Width="130px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlwarename" Text="*" 
                                            ValidationGroup="gp1" ID="requiredfieldvwae" runat="server"></asp:RequiredFieldValidator>                                            
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlware" runat="server" Width="130px" >                                           
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlware" Text="*"
                                            ValidationGroup="gp1" ID="requiredfieldvwaee" runat="server"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                         <ItemTemplate>
                                      <asp:Label ID="lblwid" Text='<%#Bind("Whid") %>' runat="server" Visible="false"></asp:Label>
                                      <asp:Label ID="lblwname" Text='<%#Bind("Wname") %>' runat="server"></asp:Label>
                                      </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity Restriction" FooterStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left">
                                         <ItemTemplate>
                                            <asp:Label ID="lblSchemaame" runat="server" Text='<%#Bind("Name") %>'></asp:Label></ItemTemplate>
                                            <EditItemTemplate>
                                            
                                            <asp:TextBox ID="txtGridInScmName" Text='<%#Bind("Name") %>' MaxLength="30" runat="server" Width="140px"></asp:TextBox><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGridInScmName"
                                                ErrorMessage="*" ValidationGroup="gp1"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RG1gh4889" runat="server" 
                                                    ErrorMessage="Invalid Character" Display="Dynamic"
                                                    SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"  
                                                    ControlToValidate="txtGridInScmName" ValidationGroup="gp1"></asp:RegularExpressionValidator></EditItemTemplate>
                                                    <FooterTemplate>
                                           
                                            <asp:TextBox ID="txtFooterScmName" MaxLength="30" runat="server" Width="140px"></asp:TextBox><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFooterScmName"
                                                ErrorMessage="*" ValidationGroup="gpft1"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="REG1gh4889" runat="server" 
                                                    ErrorMessage="Invalid Character" Display="Dynamic"
                                                    SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"  
                                                    ControlToValidate="txtFooterScmName" ValidationGroup="gpft1"></asp:RegularExpressionValidator>
                                                    <asp:Button ID="Button1" runat="server" Text="Add" CssClass="btnSubmit" ValidationGroup="gpft1" OnClick="Button1_Click1" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    
                                       <asp:TemplateField HeaderText="Rule Applies to" FooterStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrulapp" runat="server" Text='<%#Bind("Ruleapp") %>'></asp:Label></ItemTemplate><EditItemTemplate>
                                              <asp:Label ID="lblrulapp1" runat="server" Text='<%#Bind("Ruleapp") %>' Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddleditrule" runat="server" Width="100px">
                                            <asp:ListItem Selected="True" Value="1" Text="Online Sales"></asp:ListItem><asp:ListItem Value="2" Text="Retail Sales"></asp:ListItem></asp:DropDownList></EditItemTemplate>
                                            <FooterTemplate>
                                            <asp:DropDownList ID="ddlrule" runat="server" Width="100px">
                                            <asp:ListItem Selected="True" Value="1" Text="Online Sales"></asp:ListItem><asp:ListItem Value="2" Text="Retail Sales">
                                            </asp:ListItem>
                                            </asp:DropDownList>
                                            </FooterTemplate>
                                            </asp:TemplateField>
                                            
                                                <asp:TemplateField HeaderText="Min Order Value" SortExpression="Minorder"  HeaderStyle-HorizontalAlign="Left" FooterStyle-VerticalAlign="Top">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtGrdInMinQty" MaxLength="10" runat="server" Width="50px" onkeypress="return RealNumWithDecimal(this,event,2);"
                                        Text='<%#Bind("Minorder") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidaor3" ControlToValidate="txtGrdInMinQty"
                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid" ValidationGroup="qq" Display="Dynamic"></asp:RegularExpressionValidator>
                      
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator227" runat="server" ValidationGroup="qq"
                                        ControlToValidate="txtGrdInMinQty" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    
                                    
                                   
                                    <asp:TextBox ID="txtFooterMinQty" MaxLength="10" onkeypress="return RealNumWithDecimal(this,event,2);"
                                        runat="server" Width="50px"></asp:TextBox>
                                          <asp:RegularExpressionValidator ID="RegularExpressinValidator3" ControlToValidate="txtFooterMinQty"
                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid" ValidationGroup="add1" Display="Dynamic"></asp:RegularExpressionValidator>
                      
                                    <asp:RequiredFieldValidator ID="requirieldvalidator12" runat="server" ControlToValidate="txtFooterMinQty"
                                        Text="*" ValidationGroup="qq"></asp:RequiredFieldValidator>
                                    
                                </FooterTemplate>
                                <ItemTemplate>
                                   
                                         <asp:Label ID="lblMinDiscountQty" runat="server" Text='<%#Bind("Minorder") %>'
                                       ></asp:Label>
                                
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Max Order Value" SortExpression="Maxorder"  HeaderStyle-HorizontalAlign="Left" FooterStyle-VerticalAlign="Top">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtGrdInMaxQty" MaxLength="10" runat="server" Width="50px" onkeypress="return RealNumWithDecimal(this,event,2);"
                                        Text='<%#Bind("Maxorder") %>'></asp:TextBox>
                                          <asp:RegularExpressionValidator ID="RegularExpessionValidator3" ControlToValidate="txtGrdInMaxQty"
                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid" ValidationGroup="qq" Display="Dynamic"></asp:RegularExpressionValidator>
                      
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1111117" ValidationGroup="qq"
                                        runat="server" ControlToValidate="txtGrdInMaxQty" ErrorMessage="*"></asp:RequiredFieldValidator>
                                 <asp:CompareValidator ID="com2" runat="server" Operator="GreaterThan" Type="Double"  ControlToCompare="txtGrdInMinQty" ControlToValidate="txtGrdInMaxQty" Display="Dynamic" ValidationGroup="qq" SetFocusOnError="true" ErrorMessage="Must Greater then Min Order"></asp:CompareValidator>
                      
                                </EditItemTemplate>
                                <FooterTemplate>
                                   
                                    <asp:TextBox ID="txtFooterMaxQty" MaxLength="10" runat="server" onkeypress="return RealNumWithDecimal(this,event,2);"
                                        Width="50px"></asp:TextBox>
                                          <asp:RegularExpressionValidator ID="RegularExressionValidator3" ControlToValidate="txtFooterMaxQty"
                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid" ValidationGroup="add1" Display="Dynamic"></asp:RegularExpressionValidator>
                      
                                        <asp:RequiredFieldValidator ID="requiredfieldvalidator13"
                                            runat="server" ControlToValidate="txtFooterMaxQty" Text="*" ValidationGroup="add1"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="com1" runat="server"  ControlToCompare="txtFooterMinQty" ControlToValidate="txtFooterMaxQty" Operator="GreaterThan" Type="Double" Display="Dynamic" ValidationGroup="add1" SetFocusOnError="true" ErrorMessage="Must Greater then Min Order"></asp:CompareValidator>
                                </FooterTemplate>
                                <ItemTemplate>
                                 
                                         <asp:Label ID="lblMaxDiscountQty"  runat="server" Text='<%#Bind("Maxorder") %>'
                                       ></asp:Label>
                                  
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Effective Start Date" FooterStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrdStartDate" runat="server" Text='<%#Bind("StartDate") %>'></asp:Label></ItemTemplate>
                                            <EditItemTemplate>
                                            <asp:TextBox ID="txtGrdStartDate" Text='<%#Bind("StartDate") %>' runat="server" Width="75px" MaxLength="10"> </asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtGrdStartDate" />
                                            <cc1:CalendarExtender  ID="CalendarExtender27" runat="server"
                                                PopupButtonID="txtGrdStartDate" TargetControlID="txtGrdStartDate">
                                            </cc1:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator111111111111" runat="server"
                                                ControlToValidate="txtGrdStartDate" ErrorMessage="*" ValidationGroup="gp1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rghjfffkgd" runat="server" 
                                        ErrorMessage="*" ControlToValidate="txtGrdStartDate"
                                       validationgroup="gp1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>   
                                  
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                            <asp:TextBox ID="txtFooterStartDate" runat="server" MaxLength="10" Width="75px"> </asp:TextBox>
                                            <cc1:CalendarExtender  ID="CalendarExtender28"
                                                runat="server" PopupButtonID="txtFooterStartDate" TargetControlID="txtFooterStartDate">
                                            </cc1:CalendarExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender12" runat="server" CultureName="en-AU"
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFooterStartDate" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator222222" runat="server" ControlToValidate="txtFooterStartDate"
                                                ErrorMessage="*" ValidationGroup="gpft1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rgxxhjkaa" runat="server" 
                                        ErrorMessage="*" ControlToValidate="txtFooterStartDate"
                                       validationgroup="gpft1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>   
                                  
                                                </FooterTemplate></asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="Effective End Date" FooterStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrdEndDate" runat="server" Text='<%#Bind("EndDate") %>'></asp:Label></ItemTemplate>
                                            <EditItemTemplate>
                                            <asp:TextBox ID="txtGrdEndDate" Text='<%#Bind("EndDate") %>' MaxLength="10" runat="server" Width="75px"> </asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender123" runat="server" CultureName="en-AU"
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtGrdEndDate" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11133111111111" runat="server"
                                                ControlToValidate="txtGrdEndDate" ErrorMessage="*" ValidationGroup="gp1"></asp:RequiredFieldValidator><cc1:CalendarExtender ID="CalendarExtender26" runat="server" PopupButtonID="txtGrdEndDate"
                                                TargetControlID="txtGrdEndDate">
                                            </cc1:CalendarExtender>
                                              <asp:RegularExpressionValidator ID="rgaA1fgfhjk" runat="server" 
                                        ErrorMessage="*" ControlToValidate="txtGrdEndDate"
                                       validationgroup="gp1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>   
                                  
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFooterEndDate" runat="server" MaxLength="10" Width="75px"> </asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2121212" runat="server" PopupButtonID="txtFooterEndDate"
                                                TargetControlID="txtFooterEndDate">
                                            </cc1:CalendarExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender1234" runat="server" CultureName="en-AU"
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFooterEndDate" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2322" runat="server" ControlToValidate="txtFooterEndDate"
                                                ErrorMessage="*" ValidationGroup="gpft1"></asp:RequiredFieldValidator>
                                                   <asp:RegularExpressionValidator ID="ghhhxxhh" runat="server" 
                                        ErrorMessage="*" ControlToValidate="txtFooterEndDate"
                                       validationgroup="gpft1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>   
                                                
                                                </FooterTemplate>
                                         </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Status" FooterStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                         <asp:Label ID="lblch1" runat="server" Text='<%#Eval("status") %>' Visible="false" />
                                          <asp:DropDownList ID="ddleditstatus" runat="server" Width="70">
                                            <asp:ListItem Selected="True" Value="1" Text="Active"></asp:ListItem><asp:ListItem Value="0" Text="Inactive"></asp:ListItem></asp:DropDownList></EditItemTemplate>
                                            <ItemTemplate> <asp:Label ID="lblch" runat="server" Text='<%#Eval("status") %>' />
                                          </ItemTemplate>
                                          <FooterTemplate>
                                         <asp:DropDownList ID="ddlstatus" runat="server" Width="70">
                                            <asp:ListItem Selected="True" Value="1" Text="Active"></asp:ListItem><asp:ListItem Value="0" Text="Inactive"></asp:ListItem></asp:DropDownList>
                                            </FooterTemplate>
                                       </asp:TemplateField>
                                       <asp:CommandField ShowEditButton="True" ValidationGroup="gp1" CausesValidation="true" HeaderText="Edit" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" ButtonType="Image" HeaderImageUrl="~/Account/images/edit.gif" EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/UpdateGrid.JPG" CancelImageUrl="~/images/delete.gif" />
                                 
                                     <asp:TemplateField HeaderText="Delete" Visible="true" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"   HeaderStyle-HorizontalAlign="Left"  ItemStyle-Width="3%">
                                <ItemTemplate>
                                     <asp:ImageButton ID="Btndele" runat="server"  
                                     CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                     </asp:ImageButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="3%" />
                         </asp:TemplateField>
                           <asp:TemplateField  HeaderStyle-Width="2%" >
                           <HeaderTemplate>
                           <asp:ImageButton ID="btnmanage" runat="server"  Enabled="false"
                                    ImageUrl="~/Account/images/ManageImg.jpg" Height="20px" Width="20px" ToolTip="Manage">
                                     </asp:ImageButton>
                           </HeaderTemplate>
                          <ItemTemplate>
                          <asp:ImageButton ID="btnmae" runat="server"  
                                     CommandName="Manage" ImageUrl="~/Account/images/ManageImg.jpg" Height="20px" Width="20px" ToolTip="Manage"  OnClick="lblScemaName_Click">
                                     </asp:ImageButton>
                                     </ItemTemplate>
                                        </asp:TemplateField>
                                 
                                </Columns>
                                
                            </asp:GridView>
                        </asp:Panel>
                        </label>
                </fieldset>
                
            
                   
                       <div style="padding-left:1%">
                     </div>
              <asp:Panel ID="pnlApplySchema" runat="server" Width="100%" Visible="False">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label8" runat="server" Text="This rule is already applied to :" Visible="false"></asp:Label><asp:Label ID="lblSchemaName" runat="server" Visible="false"></asp:Label>
                            </legend>
                              <label>
                                <asp:Label ID="Label19" runat="server" Text="Apply restriction rule to ("></asp:Label>
                                   <asp:Label ID="lblap" runat="server"></asp:Label>
                                 <asp:Label ID="Label22" runat="server" Text="),(Business Name = "></asp:Label>
                                  <asp:Label ID="lblbname" runat="server"></asp:Label>
                                  <asp:Label ID="Label9" runat="server" Text="),"></asp:Label>
                                 <asp:Label ID="lblScemaNamefromGrd" runat="server"></asp:Label>
                                
                               <asp:Label ID="Label20" runat="server" Text="(Rule Applies to = "></asp:Label>
                             
                               <asp:Label ID="lblrulea" runat="server"></asp:Label>
                              
                              
                              <asp:Label ID="Label3" runat="server" Text="),(Min Order Value = "></asp:Label>
                                  <asp:Label ID="lblminorder" runat="server"></asp:Label>
                                  
                                  <asp:Label ID="Label15" runat="server" Text="),(Max Order Value = "></asp:Label>
                                  <asp:Label ID="lblmaxorder" runat="server"></asp:Label>
                                                
                                  <asp:Label ID="Label24" runat="server" Text="),(Start Date = "></asp:Label>
                                  <asp:Label ID="lblsdate" runat="server"></asp:Label>
                                  <asp:Label ID="Label25" runat="server" Text="),(End Date = "></asp:Label>
                                  <asp:Label ID="lblenddate" runat="server"></asp:Label>
                                  <asp:Label ID="Label21" runat="server" Text="),(Status = "></asp:Label>
                                  <asp:Label ID="lblst" runat="server"></asp:Label>
                                
                            </label>
                            <table id="Table1" cellpadding="0" cellspacing="0" width="100%">                                                              
                               <%-- <tr>
                                    <td width="30%">
                                        <label>
                                        <asp:Label ID="Label10" runat="server" Text="Business Name"></asp:Label></label></td><td width="70%">
                                        <label>
                                        <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                        <asp:Label ID="Label11" runat="server" Text="This rule is already applied to:"></asp:Label></label></td>
                                      
                                            </tr><tr>
                                    <td colspan="2">
                                    <asp:Panel ID="pnlqty" runat="server" Width="100%" Visible="False">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td style="width:10%;">
                                                    <label>
                                                    <asp:Label ID="Label4" runat="server" Text="Min Qty"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2323" runat="server" ControlToValidate="txtMinQtyOut"
                                                        ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="tbPhone1_FilteredTextBoxExtender111113" 
                                                            runat="server" Enabled="True" TargetControlID="txtMinQtyOut" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                    <asp:TextBox ID="txtMinQtyOut" runat="server" 
                                                         MaxLength="10"></asp:TextBox>
                                                        
                                                       
                                                        <%--<asp:Label ID="Label6" runat="server" Text="(Max 10 Digits,0-9)">
                                                        </asp:Label>--%>
                                                        </label>
                                                       </td>
                                                 <td style="width:10%;">
                                                    <label>
                                                        <asp:Label ID="Label5" runat="server" Text="Max Qty"></asp:Label>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2324" runat="server" ControlToValidate="txtMaxQtyOut"
                                                        ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                                            runat="server" Enabled="True" TargetControlID="txtMaxQtyOut" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>                                                   
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                         <asp:TextBox ID="txtMaxQtyOut" runat="server" 
                                                       MaxLength="10"></asp:TextBox><%--<asp:Label ID="Label12" runat="server" Text="(Max 10 Digits,0-9)"></asp:Label>--%>
                                                          <asp:CompareValidator ID="com2" runat="server" Operator="GreaterThan" Type="Integer"  ControlToCompare="txtMinQtyOut" ControlToValidate="txtMaxQtyOut" Display="Dynamic" ValidationGroup="1" SetFocusOnError="true" ErrorMessage="Must Greater then Min Qty"></asp:CompareValidator>
                      </label>
                                                        </td></tr></table></asp:Panel></td></tr>
                                                        <tr>
                                    <td colspan="2">
                                      
                                                            <asp:Panel ID="pnlQtyRestriction" runat="server" Width="100%" Visible="False">
                                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                                                Width="100%" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" AutoPostBack="True"
                                                ValidationGroup="1">
                                                <asp:ListItem Value="0">Full Category</asp:ListItem><asp:ListItem Value="1">Full Sub Category</asp:ListItem><asp:ListItem Value="2">Full Sub Sub Category</asp:ListItem><asp:ListItem Value="3">Inventory Items</asp:ListItem></asp:RadioButtonList></asp:Panel>
                                                </td></tr>
                                          <tr>
                                    <td colspan="2">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                     <asp:Panel ID="Panel2" runat="server"  
                                                        ScrollBars="None" Width="100%" Visible="False">
                                        <table Width="100%">
                                            <tr>
                                                <td >
                                                   
                                                        <asp:GridView ID="GridView2" runat="server" AlternatingRowStyle-CssClass="alt" 
                                                            AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="Id" 
                                                            PagerStyle-CssClass="pgr" Width="97%">
                                                            <Columns>
                                                                <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" 
                                                                    HeaderText="Category" />
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" HeaderText="Apply">
                                                                  
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkApplyto" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="HeaderChkbox" runat="server" AutoPostBack="True" 
                                                                            OnCheckedChanged="HeaderChkbox11_CheckedChanged1" />
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Id" 
                                                                    Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                   
                                                </td>
                                            </tr>
                                          
                                        </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                  <tr>
                                            <td align="center">
                                            <asp:Button ID="ImgBtnMove" runat="server" Text="Go" CssClass="btnSubmit" 
                                                    OnClick="ImgBtnMove_Click" ValidationGroup="gp123" Visible="False"  />
 
                                            </td>
                                            </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                            DataKeyNames="InventoryWarehouseMasterId" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                                                    AlternatingRowStyle-CssClass="alt"
                                            Width="100%" AllowSorting="True" >
                                            <Columns>
                                                <asp:BoundField DataField="CScSScName" HeaderText="Category: Sub Category: Sub Sub Category: Inventory Name" SortExpression="CScSScName" HeaderStyle-HorizontalAlign="Left" />
                                                  <asp:TemplateField HeaderText="Sales Rate" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsalesrate" runat="server"  Text='<%#Bind("Rate") %>'/>
                                                        </ItemTemplate>
                                                     
                                                    </asp:TemplateField>
                                                    
                                              
                                                     
                                                    <asp:TemplateField HeaderStyle-Width="8%" HeaderText="Avg. Cost rate" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblavgcost" runat="server" Text ="" />
                                                        </ItemTemplate>
                                                     
                                                    </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Warehouse" HeaderText="WarehouesName" />--%>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Bind("chk") %>' />
                                                         
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></EditItemTemplate>
                                                        <HeaderTemplate>
                                                          <asp:Label ID="lblappl" runat="server" Text ="Apply to All" />
                                                        <asp:CheckBox ID="HeaderChkbox1" runat="server" AutoPostBack="True" OnCheckedChanged="HeaderChkbox1_CheckedChanged1" />
                                                    </HeaderTemplate>
                                                   
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblOrderQtyRestrictionDetailID" runat="server" Visible="false" Text='<%#Bind("OrderQtyRestrictionDetailID") %>' />
                             
                                                        <asp:Label ID="lblInvWHMid" runat="server" Visible="false" Text='<%#Bind("InventoryWarehouseMasterId") %>' />
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                            </Columns>
                                            
                                        </asp:GridView>
                                    </td>
                                </tr> 
                                <tr>
                                    <td colspan="2">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        
                                        <asp:Button ID="ImgeButt1" CssClass="btnSubmit" runat="server" Text=" Apply "  Visible="false"
                                            OnClick="Button1_Click" ValidationGroup="1" />
                                            
                                    </td>
                                </tr>                                                                                          
                            </table>
                    </fieldset></asp:Panel>
            </div>
             <div style="clear: both;">
            </div>
           
             <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" 
                            Width="300px">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="height: 18px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                        <asp:Label ID="lblm" runat="server">Please check the date.</asp:Label></label></td></tr><tr>
                                    <td>
                                        <label>
                                        <asp:Label ID="Label2" runat="server"  Text="Start Date of the Year is "></asp:Label><asp:Label ID="lblstartdate" runat="server"></asp:Label></label></td></tr><tr>
                                    <td>
                                        <label>
                                        <asp:Label ID="lblm0" runat="server" >You can not select any date earlier than that. </asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="ImageButton2" CssClass="btnSubmit" runat="server" Text="Cancel" />
                                        
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="HiddenButton222" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                            ID="ModalPopupExtender12222" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel5" TargetControlID="HiddenButton222" CancelControlID="ImageButton2">
                        </cc1:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
