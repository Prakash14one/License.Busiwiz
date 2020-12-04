<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="PackingHandlingChargesMaster.aspx.cs" Inherits="PackingHandlingChargesMaster"
    Title="Untitled Page" %>

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
   
    <script type="text/javascript" language="javascript">
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
            else if ((("0123456789").indexOf(keychar) > -1)) {
                return true;
            }
            // decimal point jump
//            else if (dec && (keychar == ".")) {

//                myfield.form.elements[dec].focus();
//                myfield.value = "";

//                return false;
//            }
            else {
                myfield.value = "";
                return false;
            }
        }
        
    </script>

    <asp:UpdatePanel ID="up1" runat="server" >
        <ContentTemplate>
         <div style="padding-left:1%">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                </div>
                  <div style="clear: both;">
            </div>
            <div class="products_box">
               
                <fieldset>
                <legend>  <asp:Label ID="lblleg" runat="server" Text=""></asp:Label></legend>
                 <div style="float: right;">
                    <asp:Button ID="btnadd" runat="server" Text="Add Packing Handling Charges" CssClass="btnSubmit" 
                        onclick="btnadd_Click" />
                </div>
                 <div style="clear: both;">
                </div>
                 
                <asp:Panel ID="pnladd" runat="server" Visible="false">
                 <asp:Panel ID="pnlv" runat="server" Visible="true">
                <table width="100%">
                     <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label4" runat="server" Text="Business Name"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" InitialValue="0" ControlToValidate="ddlWarehouse"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </label>
                         
                        </td>
                        <td>
                            <label>
                                 <asp:DropDownList ID="ddlWarehouse" runat="server"                                                         
                                    OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" AutoPostBack="True" >
                                </asp:DropDownList>
                            </label>
                           
                        </td>
                        <td>
                            
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width:25%" valign="top">
                            <label>
                                <asp:Label ID="Label5" runat="server" Text="Name of Handling Charge"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" 
                                ErrorMessage="Invalid Character" Display="Dynamic"
                                SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"  
                                ControlToValidate="txtName" ValidationGroup="1"></asp:RegularExpressionValidator>
                            </label>                             
                        </td>
                        <td  colspan="3" valign="top">
                            <label>
                                <asp:TextBox ID="txtName" runat="server" ValidationGroup="1" onKeydown="return mask(event)"  onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',20)" MaxLength="20" ></asp:TextBox> 
                                 <asp:Label ID="Label27" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                 <span id="div1" class="labelcount">20</span>
                            <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                               
                            </label>
                                                                                  
                        </td>
                        
                    </tr>
                    <tr>
                        <td valign="top">
                             <label>
                                <asp:Label ID="Label6" runat="server" Text="Amount"></asp:Label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAmount"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                                 ControlToValidate="txtAmount" Display="Dynamic"
                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" 
                                 ValidationGroup="1" ErrorMessage="Invalid Digits"></asp:RegularExpressionValidator>
                                <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                Enabled="True" TargetControlID="txtAmount" ValidChars="0147852369.">
                            </cc1:FilteredTextBoxExtender>
                            </label>
                         
                        </td>
                        <td colspan="3" valign="top">
                            <label>
                                <asp:TextBox ID="txtAmount" MaxLength="15" onkeyup="return mak('Span1',15,this)"  runat="server" ValidationGroup="1" Width="145px" ></asp:TextBox>                           
                               <asp:Label ID="Label28" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                 <span id="Span1" class="labelcount">15</span>
                                 <asp:Label ID="Label3" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label>
                            </label>
                      <%--  </td>
                        <td colspan="2">--%>
                              <asp:RadioButton ID="rbPercentage" runat="server" Checked="True" GroupName="2" Text="%" />
                              <asp:RadioButton ID="rbPerUnit" runat="server" GroupName="2" Text="$" />
                              <asp:RadioButton ID="rbPerFlat" runat="server" GroupName="2" Text="Per Order" />
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="lb" runat="server" Text="Apply to Online Sales" ></asp:Label>    
                            </label>
                            
                            <asp:CheckBox ID="chkonline" runat="server" Text="" />
                        </td>
                        <td>
                            <label>
                                <asp:Label ID="Label2" runat="server" Text="Apply to Retail Sales"></asp:Label>     
                            </label>
                           
                            <asp:CheckBox ID="chkretail" runat="server" Text="" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                     <tr>                        
                        <td colspan="4">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="Label18" runat="server" Text="1. Criteria for apply handling charges" ></asp:Label>     
                                </legend>
                                <table width="100%">
                                    <tr>                        
                                        <td colspan="4">
                                            <label>
                                            <asp:Label ID="Label19" runat="server" Text="Apply handling charges to sales order with:" ></asp:Label>  
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            
                                        </td>
                                        <td width="25%">
                                            <label>
                                            <asp:Label ID="Label7" runat="server" Text="Min Value"></asp:Label>
                                            </label>
                                        </td>
                                        <td width="25%">
                                            <label>
                                            <asp:Label ID="Label9" runat="server" Text="Max Value"></asp:Label>
                                            </label>
                                        </td>
                                        <td width="30%">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <label>
                                                <asp:Label ID="Label8" runat="server" Text="Order Value"></asp:Label>
                                            </label>
                                        </td>
                                        <td  valign="top">
                                            <label>
                                                <asp:TextBox ID="TxtMinOrderValue"  MaxLength="10" 
                                                onKeydown="return mask(event)"  
                                                onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_. ]+$/,'Span2',10)" 
                                                  runat="server"  
                                                ValidationGroup="1" Width="80px"></asp:TextBox>
                                            
                                            </label> 
                                            <label>
                                
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtMinOrderValue"
                                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                 ControlToValidate="TxtMinOrderValue" Display="Dynamic"
                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" 
                                 ValidationGroup="1" ErrorMessage="Invalid"></asp:RegularExpressionValidator>
                                                 <asp:Label ID="Label29" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                 <span id="Span2" class="labelcount">10</span>
                            <asp:Label ID="Label21" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label>
                             <cc1:FilteredTextBoxExtender ID="FilteredTextoxExtender1" runat="server"
                                Enabled="True" TargetControlID="TxtMinOrderValue" ValidChars="0147852369.">
                            </cc1:FilteredTextBoxExtender>   
                                            </label> 
                                        </td>
                                        <td  valign="top"> 
                                            <label>
                                                <asp:TextBox  ID="TxtMaxOrderValue"  Width="80px"
                                                onKeydown="return mask(event)"  
                                                onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9._ ]+$/,'Span3',10)"  
                                                MaxLength="10"   
                                                runat="server"  ValidationGroup="1"></asp:TextBox>
                                                
                                            </label>  
                                            <label>                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtMaxOrderValue"
                                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                 <asp:RegularExpressionValidator ID="ReguessionValidator1" 
                                 ControlToValidate="TxtMaxOrderValue" Display="Dynamic"
                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" 
                                 ValidationGroup="1" ErrorMessage="Invalid"></asp:RegularExpressionValidator>
                                                 <asp:Label ID="Label30" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                 <span id="Span3"  class="labelcount">10</span>
                            <asp:Label ID="Label22" runat="server" Text="(0-9 .)"  CssClass="labelcount"></asp:Label> 
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExender1" runat="server"
                                Enabled="True" TargetControlID="TxtMaxOrderValue" ValidChars="0147852369.">
                            </cc1:FilteredTextBoxExtender>   
                                            </label>
                                        </td>
                                        <td>
                                             
                                        </td>
                                    </tr>
                                     <tr>
                                        <td valign="top">
                                        <label>
                                            <asp:Label ID="Label10" runat="server" Text="Weight in"></asp:Label>  
                                          </label>  
                                          <label>
                                                <asp:DropDownList ID="ddllbs" runat="server" Width="59px" 
                                                >
                                            </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtMinOrderWeight" Width="80px" onKeydown="return mask(event)"  onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_]+$/,'Span4',10)" runat="server"  MaxLength="10"     ValidationGroup="1"></asp:TextBox>
                                               
                                            </label>
                                             <label>                                
                                                 <asp:RequiredFieldValidator ID="ReqdFieldValidator5" runat="server" ControlToValidate="txtMinOrderWeight"
                                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                
                                                
                                                 <asp:Label ID="Label31" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                 <span id="Span4"  class="labelcount">10</span>
                            <asp:Label ID="Label23" runat="server" Text="(0-9)"  CssClass="labelcount"></asp:Label> 
                            <cc1:FilteredTextBoxExtender ID="FiltedtBoxExtender1" runat="server"
                                Enabled="True" TargetControlID="txtMinOrderWeight" ValidChars="0147852369">
                            </cc1:FilteredTextBoxExtender>   
                                            </label> 
                                        </td>
                                        <td  valign="top">
                                             <label>
                                                <asp:TextBox ID="txtMaxOrderWeight" runat="server" Width="80px" onKeydown="return mask(event)"  onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span5',10)"  MaxLength="10"    ValidationGroup="1"></asp:TextBox>
                                              
                                            </label>   
                                            <label>                                
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMaxOrderWeight"
                                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                   <asp:Label ID="Label32" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                 <span id="Span5"  class="labelcount">10</span>
                            <asp:Label ID="Label24" runat="server" Text="(0-9)"  CssClass="labelcount"></asp:Label> 
                             <cc1:FilteredTextBoxExtender ID="FilteextBoxExtender1" runat="server"
                                Enabled="True" TargetControlID="txtMaxOrderWeight" ValidChars="0147852369">
                            </cc1:FilteredTextBoxExtender>     
                                            </label>  
                                        </td>
                                        <td valign="top">
                                            
                                        </td>
                                    </tr>
                                     <tr>
                                        <td valign="top">
                                            <label>
                                                <asp:Label ID="Label11" runat="server" Text="No. of Items"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtMinOrederNo" runat="server" Width="80px" onKeydown="return mask(event)"  onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span6',10)" MaxLength="10"     ValidationGroup="1"
                                               ></asp:TextBox>
                                                  
                                            </label>
                                            <label>                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMinOrederNo"
                                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:Label ID="Label33" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                 <span id="Span6"  class="labelcount">10</span>
                            <asp:Label ID="Label25" runat="server" Text="(0-9)"  CssClass="labelcount"></asp:Label> 
                            <cc1:FilteredTextBoxExtender ID="FilteredTeoxExtender1" runat="server"
                                Enabled="True" TargetControlID="txtMinOrederNo" ValidChars="0147852369">
                            </cc1:FilteredTextBoxExtender>      
                                            </label> 
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtMaxOrederNo" runat="server" Width="80px" onKeydown="return mask(event)"  onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span7',10)" MaxLength="10"  ValidationGroup="1"></asp:TextBox>
                                                
                                            </label>
                                            <label>                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtMaxOrederNo"
                                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                 <asp:Label ID="Label34" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                 <span id="Span7"  class="labelcount">10</span>
                            <asp:Label ID="Label26" runat="server" Text="(0-9)"  CssClass="labelcount"></asp:Label> 
                             <cc1:FilteredTextBoxExtender ID="FiltereBoxExtender1" runat="server"
                                Enabled="True" TargetControlID="txtMaxOrederNo" ValidChars="0147852369">
                            </cc1:FilteredTextBoxExtender>      
                                            </label>
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>                           
                        </td>
                    </tr>
                    <tr>                        
                        <td colspan="4">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="Label12" runat="server" Text="2. Apply these handling charges for the following categories of inventory:" ></asp:Label>
                                </legend>
                                <table width="100%">
                                    <tr>
                                        <td>
                                             <label>
                                                <asp:Label ID="Label13" runat="server" Text="Category "></asp:Label>
                                            </label>                           
                                        </td>
                                        <td>    
                                            <label>
                                                <asp:DropDownList ID="ddlcategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged"
                                                Width="150px">
                                            </asp:DropDownList>
                                            </label>                            
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label14" runat="server" Text="Sub Category "></asp:Label>
                                            </label>                           
                                        </td>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlsubcategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label15" runat="server" Text="Sub Sub Category"></asp:Label>
                                            </label>                          
                                        </td>
                                        <td>
                                            <label>
                                                 <asp:DropDownList ID="ddlsubsubcategory" runat="server" Width="150px" 
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        
                        <td colspan="4">
                          
                        </td>
                        
                    </tr>
                    
                   
                </table>
                </asp:Panel>
                
                  <div style="clear: both;">
                  <table width="100%">
                  <tr>
                  <td align="center">
                   <asp:Button ID="ImageButton1" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="Button1_Click" ValidationGroup="1" /> 
                            <asp:Button ID="imgupdate" runat="server" CssClass="btnSubmit" Text="Update" onclick="imgupdate_Click"  ValidationGroup="1" Visible="False" /> 
                            &nbsp;<asp:Button ID="ImageButton2" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="Button2_Click" /> 

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
                        <asp:Label ID="Label16" runat="server" Text="List of Packing and Handling Charges"></asp:Label>
                    </legend>
                    <div style="clear: both;">
                </div>
                    <div style="float: right;">
                      <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />

                        <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit" 
                            onclick="Button1_Click1" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                           type="button" class="btnSubmit" value="Print" visible="false"/>
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%" ScrollBars="Horizontal">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div id="mydiv" class="closed">
                                            <table width="100%" style="color:Black; font-weight:bold; font-style:italic; text-align:center">
                                                <tr>
                                                    <td align="center" >
                                                        <asp:Label ID="lblCompany" runat="server" Font-Size="20px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" >
                                                        <asp:Label ID="Label20" runat="server" Font-Size="20px" Text="Business :"></asp:Label>
                                                         <asp:Label ID="lblbusiness" runat="server" Font-Size="20px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" >
                                                        <asp:Label ID="Label17" runat="server" Text="List of Packing and Handling Charges" Font-Size="18px" ></asp:Label>
                                                    </td>
                                                </tr>
                                               
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                   
                                    
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"  
                                            CssClass="mGrid" PagerStyle-CssClass="pgr" 
                                AlternatingRowStyle-CssClass="alt" Width="100%" DataKeyNames="PackhandMasterId" 
                                            onrowcommand="GridView2_RowCommand1" 
                                            onrowdeleting="GridView2_RowDeleting" 
                                          HeaderStyle-HorizontalAlign="Left" 
                                            AllowSorting="True" onsorting="GridView2_Sorting" 
                                            onrowediting="GridView2_RowEditing">
                                        <Columns>
                                            <asp:BoundField DataField="PackhandMasterId" Visible="False" />
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="Wname" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:Label ID="lblwh" runat="server"  
                                                Text='<%#Bind("Wname") %>'  ></asp:Label>
                                          
                                       
                                        </ItemTemplate>
                                     
                                       
                                                <HeaderStyle HorizontalAlign="Left" />
                                     
                                       
                                    </asp:TemplateField> 
                                    
                                        <asp:TemplateField HeaderText="Entry Date" SortExpression="EntryDate" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:Label ID="lblentrydate" runat="server"  
                                                Text='<%#Bind("EntryDate") %>'  ></asp:Label>
                                          
                                       
                                        </ItemTemplate>
                                     
                                       
                                                <HeaderStyle HorizontalAlign="Left" />
                                     
                                       
                                    </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Handling Charge Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:Label ID="lblhandlingcharge" runat="server"  
                                                Text='<%#Bind("Name") %>'  ></asp:Label>
                                             </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                             </asp:TemplateField> 
                                    
                                                <asp:TemplateField HeaderText="Handling Charge" SortExpression="Amount" HeaderStyle-HorizontalAlign="Left" Visible="true"
                                            ItemStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblsingamt" runat="server" Visible="false" Text="$"></asp:Label>
                                                <asp:Label ID="lblamt" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                                                 <asp:Label ID="lblsignpers" runat="server" Visible="false" Text="%"></asp:Label>
                                                 <asp:Label ID="lblperorder" runat="server" Visible="false" Text="Per Order"></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Left" />
                                           
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Min Order Value" SortExpression="MinOrderValue" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:Label ID="lblminorder" runat="server"  
                                                Text='<%#Bind("MinOrderValue") %>'  ></asp:Label>
                                             </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                             </asp:TemplateField> 
                                            
                                               <asp:TemplateField HeaderText="Max Order Value" SortExpression="MaxOrderValue" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:Label ID="lblmaxorder" runat="server"  
                                                Text='<%#Bind("MaxOrderValue") %>'  ></asp:Label>
                                             </ItemTemplate>
                                      
                                             </asp:TemplateField> 
                                     
                                           <asp:TemplateField HeaderText="Min Order Weight" SortExpression="MinOrderWeight" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:Label ID="lblminorderweight" runat="server"  
                                                Text='<%#Bind("MinOrderWeight") %>'  ></asp:Label>
                                             </ItemTemplate>
                                      
                                             </asp:TemplateField> 
                                             
                                                <asp:TemplateField HeaderText="Max Order Weight" SortExpression="MaxOrderWeight" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:Label ID="lblmaxorderweight" runat="server"  
                                                Text='<%#Bind("MaxOrderWeight") %>'  ></asp:Label>
                                             </ItemTemplate>
                                      
                                             </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Weight Unit" SortExpression="Unitname" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:Label ID="lbluniy" runat="server"  
                                                Text='<%#Bind("Unitname") %>'  ></asp:Label>
                                             </ItemTemplate>
                                      
                                             </asp:TemplateField> 
                                          
                                             <asp:TemplateField HeaderText="Min Item No." SortExpression="MinitemNo" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:Label ID="lblminno" runat="server"  
                                                Text='<%#Bind("MinitemNo") %>'  ></asp:Label>
                                             </ItemTemplate>
                                      
                                             </asp:TemplateField> 
                                          
                                          
                                              <asp:TemplateField HeaderText="Max Item No." SortExpression="MaxItemNo" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:Label ID="lblmaxino" runat="server"  
                                                Text='<%#Bind("MaxItemNo") %>'  ></asp:Label>
                                             </ItemTemplate>
                                      
                                             </asp:TemplateField> 
                                          
                                             <asp:TemplateField HeaderText="Apply Online Sales" SortExpression="ApplyOnlineSales" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:CheckBox ID="lblch" runat="server"  
                                                Checked='<%#Bind("ApplyOnlineSales") %>' Enabled="false"/>
                                             </ItemTemplate>
                                      
                                             </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Apply Retail Sales" SortExpression="ApplyRetailSales" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:CheckBox ID="chkxc" runat="server"  
                                                Checked='<%#Bind("ApplyRetailSales") %>' Enabled="false" />
                                             </ItemTemplate>
                                      
                                             </asp:TemplateField> 
                                  
                                            <asp:TemplateField HeaderText="Is Percent" Visible="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox Enabled="False"  ID="chkpercent" runat="server" HeaderStyle-HorizontalAlign="Left"
                                                        Checked='<%# Eval("IsPercent") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Is perUnit" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox Enabled="false" ID="chkperunit" runat="server" 
                                                        Checked='<%# Eval("IsPerUnit") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Is per" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox Enabled="false" ID="chkperflat" runat="server" 
                                                        Checked='<%# Eval("IsPerFlatOrder") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                         
                                          
                                                <asp:TemplateField HeaderText="Inv Category Name" SortExpression="InventoryCatName"  HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:Label ID="lblcat" runat="server"  
                                                Text='<%#Bind("InventoryCatName") %>'  ></asp:Label>
                                          
                                       
                                        </ItemTemplate>
                                     
                                                    <HeaderStyle HorizontalAlign="Left" />
                                     
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Inv Sub Category Name" SortExpression="InventorySubCatName" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:Label ID="lblsub" runat="server"  
                                                Text='<%#Bind("InventorySubCatName") %>'  ></asp:Label>
                                          
                                       
                                        </ItemTemplate>
                                     
                                        <HeaderStyle HorizontalAlign="Left" />
                                     
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Inv Sub Sub Category Name" SortExpression="InventorySubSubName"  HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                              <asp:Label ID="lblsubsub" runat="server"  
                                                Text='<%#Bind("InventorySubSubName") %>'  ></asp:Label>
                                          
                                       
                                        </ItemTemplate>
                                  
                                        <HeaderStyle HorizontalAlign="Left" />
                                  
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="img1" runat="server" CausesValidation="true" ToolTip="View" ImageUrl="~/Account/images/viewprofile.jpg"
                                                          CommandArgument='<%#Bind("PackhandMasterId") %>'
                                                        CommandName="View"></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle" >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="img11" runat="server" CausesValidation="true" ToolTip="Edit" ImageUrl="~/Account/images/edit.gif"
                                                          CommandArgument='<%#Bind("PackhandMasterId") %>'
                                                        CommandName="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                           
                                             <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"  HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="llinkbb" runat="server" ToolTip="Delete"
                                                     CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                </asp:TemplateField>
                                        </Columns>
                                        
                                           
                                        
                                        <PagerStyle CssClass="pgr" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="alt" />
                                        
                                           
                                        
                                    </asp:GridView>
                                    
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
      
</asp:Content>
