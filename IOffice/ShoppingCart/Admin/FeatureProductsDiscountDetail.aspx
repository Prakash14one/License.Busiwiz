<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="FeatureProductsDiscountDetail.aspx.cs" Inherits="FeatureProductsDiscountDetail"
     Title="Untitle Page"  %>


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
 }

 function CallPrint(strid) {
     var prtContent = document.getElementById('<%= Panel1.ClientID %>');
     var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');

     WinPrint.document.write(prtContent.innerHTML,'<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');     
     WinPrint.document.close();
     WinPrint.focus();
     WinPrint.print();
     WinPrint.close();

 }  
  function mask(evt)
        { 

           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if( evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }
             
             
               
            
        }  
        function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered an invalid character");
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
          <div style="padding-left:1%">
                     <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Label"></asp:Label>
                </div>
                 <div style="clear: both;">
            </div>
            <div class="products_box">
                 <fieldset>
                <legend>  <asp:Label ID="lblleg" runat="server" Text=""></asp:Label></legend>
                 <div style="float: right;">
                    <asp:Button ID="btnadd" runat="server" Text="Add Feature Product Plan" CssClass="btnSubmit" onclick="btnadd_Click" 
                        />
                </div>
                 <div style="clear: both;">
                </div>
                 
                <asp:Panel ID="pnladd" runat="server" Visible="false">
                 <asp:Panel ID="pnlv" runat="server" Visible="true">
                <table width="100%">
                     <tr>
                        <td  valign="top">
                            <label>
                                <asp:Label ID="Label16" runat="server" Text="Feature Product Plan Name"></asp:Label>
                                   <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="Requiredalidator9" runat="server" SetFocusOnError="true"  ControlToValidate="txtfeatureplan"
                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="Rs21" runat="server" ErrorMessage="Invalid Character" Display="Dynamic"
                                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" 
                                                        ControlToValidate="txtfeatureplan" ValidationGroup="1">
                                                        </asp:RegularExpressionValidator>
                            </label>
                         
                        </td>
                        <td valign="top">
                            <label>
                                <asp:TextBox ID="txtfeatureplan" runat="server" MaxLength="20"  
                                onKeydown="return mask(event)" Width="180px" 
                                                
                                onkeyup="return check(this,/[\\/!@#$%^'&_.*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9 ]+$/,'Span3',20)" ></asp:TextBox>
                                               
                                                 <asp:Label ID="Label30" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                
                                 <span id="Span3"  class="labelcount">20</span>
                            <asp:Label ID="Label22" runat="server" Text="(A-Z 0-9)"  CssClass="labelcount"></asp:Label> 
                            </label>
                           
                        </td>
                        <td valign="top"  style="width:105px;">
                                                    <label>
                                                        <asp:Label ID="Label171" runat="server" Text="Start Date"></asp:Label>
                                                           <asp:Label ID="Label20" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequireldValidator4" runat="server" ControlToValidate="txtStartDate"
                                                            ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                              <cc1:CalendarExtender ID="txtdatelendarExtender"  runat="server" PopupButtonID="ImageButton3"
                                                            TargetControlID="txtStartDate">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="TextBox1EditExtender" runat="server" CultureName="en-AU"
                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtStartDate" />
                                                    </label>
                                                </td>
                                                <td valign="top" style="width:75px;">
                                                    <label>
                                                        <asp:TextBox ID="txtStartDate" runat="server" Width="75px"></asp:TextBox>
                                                        
                                                      
                                                    </label>
                                                </td>
                                                <td valign="top" align="left">
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                    </label>
                                                </td>
                                                <td valign="top"  style="width:100px;">
                                                    <label>
                                                        <asp:Label ID="Label17" runat="server" Text="End Date"></asp:Label>
                                                           <asp:Label ID="Label21" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFiealidator3" runat="server" ControlToValidate="txtEndDate"
                                                            ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton4"
                                                            TargetControlID="txtEndDate">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtEndDate" />
                                                    </label>
                                                </td>
                                                <td valign="top" style="width:75px;">
                                                    <label>
                                                        <asp:TextBox ID="txtEndDate" runat="server" Width="75px"></asp:TextBox>
                                                        
                                                    </label>
                                                </td>
                                                <td valign="top" align="left">
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                    </label>
                                                </td>
                                                <td valign="top">
                                                    <label>
                                                    <asp:Label ID="Label18" runat="server" Text="Status"></asp:Label>
                                                
                                                    </label>
                                                    </td>
                                                     <td valign="top">
                                                    <label>
                                                   <asp:DropDownList ID="ddlstatus" runat="server" Width="75px" >
                                                   <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                                                       <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                                   </asp:DropDownList>
                                                
                                                    </label>
                                                    </td>
                    </tr>
                   
                   
                   
                   
                </table>
                </asp:Panel>
                
                  <div style="clear: both;">
                  <table width="100%">
                  <tr>
                  <td align="center">
                   <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Submit" 
                          OnClick="Button2_Click" ValidationGroup="1" /> 
                            &nbsp;<asp:Button ID="imgupdate" runat="server" CssClass="btnSubmit" Text="Update" onclick="imgupdate_Click"  ValidationGroup="1" Visible="False" /> 
                            &nbsp;<asp:Button ID="Button3" runat="server" CssClass="btnSubmit" 
                          Text="Cancel" OnClick="Button3_Click" /> 
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
      
                  </td>
                  </tr>
                  </table>
                                      </div>
                 </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                       <asp:Label ID="Label7" runat="server" Font-Bold="True"  
                            Text="List of Feature Product Plans"></asp:Label> 
                    </legend>
                    <asp:Panel ID="Panel3" runat="server" Width="100%">
                                <asp:GridView ID="GrdOrdrQtyMaster" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                     CssClass="mGrid" PagerStyle-CssClass="pgr" 
                    AlternatingRowStyle-CssClass="alt"  DataKeyNames="FeatureProdDiscountMasterID" OnRowCancelingEdit="GrdOrdrQtyMaster_RowCancelingEdit"
                                    OnRowCommand="GrdOrdrQtyMaster_RowCommand" 
                                    OnRowEditing="GrdOrdrQtyMaster_RowEditing" Width="100%" ShowFooter="false" 
                                    onrowdeleting="GrdOrdrQtyMaster_RowDeleting" 
                                    EmptyDataText="No Record Found" onsorting="GrdOrdrQtyMaster_Sorting" 
                                    AllowSorting="True">
                                    
                                    <Columns>
                                        <asp:TemplateField HeaderText="Feature Product Plan" 
                                           HeaderStyle-HorizontalAlign="Left" SortExpression="FeatureProdDiscountSchemeName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSchemaName" runat="server" Text='<%#Bind("FeatureProdDiscountSchemeName") %>'></asp:Label>
                                            </ItemTemplate>
                                           
                                       
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Date" FooterStyle-VerticalAlign="Top" HeaderStyle-Width="10%" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrdStartDate" runat="server" Text='<%#Bind("StartDate") %>'></asp:Label>
                                            </ItemTemplate>
                                           
                                               
                                           
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Date" FooterStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" SortExpression="EndDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrdEndDate" runat="server" Text='<%#Bind("EndDate") %>'></asp:Label>
                                            </ItemTemplate>
                                          
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" FooterStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="7%" SortExpression="Active">
                                          
                                            <ItemTemplate>
                                                                  <asp:Label ID="Label9" runat="server" Text='<%#Bind("status") %>'></asp:Label>
                                            </ItemTemplate>
                                         
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle VerticalAlign="Top" />
                                          
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif"  HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Width="3%" >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="img11" runat="server" CausesValidation="true" ToolTip="Edit" ImageUrl="~/Account/images/edit.gif"
                                                          CommandArgument='<%#Bind("FeatureProdDiscountMasterID") %>'
                                                        CommandName="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                    
                                       <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"   HeaderStyle-HorizontalAlign="Left"  ItemStyle-Width="3%">
                                <ItemTemplate>
                                     <asp:ImageButton ID="Btndele" runat="server"  
                                     CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                     </asp:ImageButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="3%" />
                         </asp:TemplateField>
                        <asp:TemplateField  HeaderText="Apply"  HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Width="3%" ItemStyle-ForeColor="#416271" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="img11a" runat="server" CausesValidation="true" ToolTip="Apply"  ForeColor="#416271"
                                                          CommandArgument='<%#Bind("FeatureProdDiscountMasterID") %>' Text="Apply"
                                                        CommandName="Select"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                    
                        
                                    <%--  <asp:CommandField ShowSelectButton="True" HeaderText="Apply" SelectText="Apply" ItemStyle-ForeColor="Black" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="40px" />
                                        </asp:CommandField>--%>
                                    </Columns>
                                    
                                   
                                    
                                </asp:GridView>
                            </asp:Panel>
                </fieldset>
                 <asp:Panel ID="Panel2" runat="server" Visible="False" Width="100%">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label8" runat="server" 
                            Text="Please select the product you want to make a feature product for plan:"></asp:Label>
                            <asp:Label ID="lblPromotionScemaName" runat="server"></asp:Label>
                           <%--     <asp:Label ID="Label26" runat="server" Text=", "></asp:Label>--%>
          
                                  <asp:Label ID="lblsdate" runat="server"></asp:Label>
                                  <asp:Label ID="Label24" runat="server" Text=" - "></asp:Label>
                                  <asp:Label ID="lblenddate" runat="server"></asp:Label>
                             <%--    <asp:Label ID="Label12" runat="server" Text=")"></asp:Label>--%>
                                   <asp:Label ID="Label23" runat="server" Text="),(Status=" Visible="false"></asp:Label>
                                  <asp:Label ID="lblsta" runat="server" Visible="false"></asp:Label>
                                  <asp:Label ID="Label28" runat="server" Text=")" Visible="false"></asp:Label>
                    </legend>
                    <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                         <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Select Business"></asp:Label>
                                </label>
                                 
                            </td>
                            <td>
                                <label>
                                <asp:DropDownList ID="ddlWarehouse" runat="server" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged"
                                     AutoPostBack="True">
                                </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                            </td>
                            <td>
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
                                            <asp:DropDownList ID="ddlInvCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvCat_SelectedIndexChanged"
                                               >
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
                                            <asp:DropDownList ID="ddlInvSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubCat_SelectedIndexChanged"
                                                >
                                            </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                    <asp:Label ID="Label6" runat="server" Text="Sub Sub Category "></asp:Label>
                                </label>
                                         
                                        </td>
                                        <td>
                                            <label>
                                            
                                            
                                            <asp:DropDownList ID="ddlInvSubSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubSubCat_SelectedIndexChanged"
                                               >
                                            </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                    <asp:Label ID="Label10" runat="server" Text="Inventory Name"></asp:Label>
                                </label>
                                            
                                        </td>
                                        <td>
                                            <label>
                                            <asp:DropDownList ID="ddlInvName" runat="server">
                                            </asp:DropDownList>
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
                                            <asp:Button ID="imgtnGo" runat="server" Text="  Go  " CssClass="btnSubmit" OnClick="imgbtnGo_Click" ValidationGroup="6" />
                                              &nbsp;<asp:Button ID="btncance" runat="server" Text="Cancel" CssClass="btnSubmit" 
                                                onclick="btncance_Click" />
                                     
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td  align="right" colspan="4">
                                            <asp:Button ID="Button4" runat="server" Text="Printable Version" CssClass="btnSubmit" 
                    onclick="Button4_Click" />
                                            <input id="Button7" runat="server" onclick="javascript:CallPrint('divPrint')" class="btnSubmit" type="button" value="Print" visible="false" />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td  colspan="4">
                                            <asp:Panel ID="Panel1" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr align="center">
                                          
                                                    <td>
                                                        <div id="mydiv" class="closed">
                                                            <table width="850Px" style="color:Black; font-weight:bold; font-style:italic; text-align:center">
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblCompany" runat="server" Visible="false" Font-Size="20px"></asp:Label>
                                                                       
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="Label15" runat="server"  Font-Size="18px" Text="Business : "></asp:Label>
                                                                        <asp:Label ID="lblBusiness" runat="server"  Font-Size="18px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="Label51" runat="server" Font-Size="18px" Text="List of Inventories "></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                         <asp:Label ID="Label11" runat="server" Text=" Main Category : " Font-Bold="true"></asp:Label>
                                                                    <asp:Label ID="lblMainCat" runat="server"></asp:Label>
                                                                    <asp:Label ID="Labelg12" runat="server" Text=", Sub Category : " Font-Bold="true"></asp:Label>
                                                                    <asp:Label ID="lblSubCat" runat="server"></asp:Label>
                                                                    <asp:Label ID="Labelg13" runat="server" Text=", Sub Sub Category : " Font-Bold="true"></asp:Label>
                                                                    <asp:Label ID="lblSubSub" runat="server"></asp:Label>
                                                                    <asp:Label ID="Labegl14" runat="server" Text=", Inventory : " Font-Bold="true"></asp:Label>
                                                                    <asp:Label ID="lblInv" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                               
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                               <tr>
                                                    <td>
                                                    
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="InventoryWarehouseMasterId"
                                                    CssClass="mGrid" PagerStyle-CssClass="pgr" 
                                            AlternatingRowStyle-CssClass="alt" Width="100%" AllowSorting="True" 
                                                        onsorting="GridView1_Sorting" EmptyDataText="No Record Found.">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Left" SortExpression="InventoryCatName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("InventoryCatName") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub Category" HeaderStyle-HorizontalAlign="Left" SortExpression="InventorySubCatName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("InventorySubCatName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub Sub Category" HeaderStyle-HorizontalAlign="Left"  SortExpression="InventorySubSubName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("InventorySubSubName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Name" HeaderText="Name"  SortExpression="Name"
                                                                HeaderStyle-HorizontalAlign="Left" >
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField ControlStyle-Width="40Px" DataField="Rate"  SortExpression="Rate"
                                                                HeaderText="Discount Rate" HeaderStyle-HorizontalAlign="Left" Visible="False">
                                                                <ControlStyle Width="40px" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" >
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="aa" runat="server" Text="Apply"></asp:Label>
                                                                    <input ID="chkAll" runat="server" visible="false"
                                                                    onclick="javascript:SelectAllCheckboxes(this);" type="checkbox" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                </ItemTemplate>
                                                               
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                               
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="False" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="invWHMid" runat="server" 
                                                                    Text='<%#Bind("InventoryWarehouseMasterId") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
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
                                        <td colspan="4" align="center">
                                            <asp:Button ID="ImageButton1" CssClass="btnSubmit" runat="server" Text="Submit" OnClick="Button1_Click" />
                                            
                                        </td>
                                   </tr>
                    </table>
                </fieldset></asp:Panel>
                <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" 
                                                BorderStyle="Outset" Width="300px">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="subinnertblfc" style="height: 18px">
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
                                            <asp:Label ID="Label2" runat="server" 
                                                                Text="Start Date of the Year is "></asp:Label>
                                            <asp:Label ID="lblstartdate" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                            <asp:Label ID="lblm0" runat="server" >You can not select anydate earlier than 
                                            that. </asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="ImageButton2" runat="server" Text="Cancel" OnClick="ImageButton2_Click" />
                                           
                                        </td>
                                    </tr>
                                </table>
                                 
                            </asp:Panel>
                            <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                            <cc1:ModalPopupExtender
                                        ID="ModalPopupExtender12222" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="Panel5" TargetControlID="HiddenButton222">
                            </cc1:ModalPopupExtender>
            </div>                                    
        </ContentTemplate>
    </asp:UpdatePanel>
    
   
</asp:Content>
