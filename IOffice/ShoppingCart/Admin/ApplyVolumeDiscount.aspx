<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="ApplyVolumeDiscount.aspx.cs" Inherits="ApplyVolumeDiscount"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }  
    </script>

    <script language="javascript" type="text/javascript">
        function toggle(ctrl, chk) {
            alert(ctrl);
            alert(chk);

            $('#<%=GridView1.ClientID %>:checkbox[id$=' + chk + ']').attr('checked', ctrl.checked);
        }
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
        }
    </script>

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
        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
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
        .style2
        {
            height: 38px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                </div>
                  <fieldset>
                <legend>  <asp:Label ID="lblleg" runat="server" Text=""></asp:Label></legend>
                 <div style="float: right;">
                    <asp:Button ID="btnadd" runat="server" Text="Add Volume Discount" CssClass="btnSubmit" onclick="btnadd_Click" 
                        />
                </div>
                 <div style="clear: both;">
                </div>
                 
                <asp:Panel ID="pnladd" runat="server" Visible="false">
                 <asp:Panel ID="pnlv" runat="server" Visible="true">
                <table >
                     <tr>
                        <td  >
                            <label>
                                <asp:Label ID="Label23" runat="server" Text="Discount Name"></asp:Label>
                                   <asp:Label ID="Label24" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="Requiredalidator9" runat="server" SetFocusOnError="true"  ControlToValidate="txtdosname"
                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="Rs21" runat="server" ErrorMessage="Invalid Character" Display="Dynamic"
                                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" 
                                                        ControlToValidate="txtdosname" ValidationGroup="1">
                                                        </asp:RegularExpressionValidator>
                            </label>
                         
                      </td>
                <td>
                            <label>
                                <asp:TextBox ID="txtdosname" runat="server" MaxLength="20"  
                                onKeydown="return mask(event)" Width="180px" 
                                                
                                
                                onkeyup="return check(this,/[\\/!@#$%^'&_.*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9 ]+$/,'Span3',20)" ></asp:TextBox>
                                            </label>  
                                 
                                <label>
                                                 <asp:Label ID="Label30" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                
                                 <span id="Span3"  class="labelcount">20</span>
                            <asp:Label ID="Label122" runat="server" Text="(A-Z 0-9)"  CssClass="labelcount"></asp:Label> 
                            </label>
                              </td>
              
                       
                          <td>
                          <label>  <asp:Label ID="Label31" runat="server" Text="Min Discount Qty"></asp:Label>
                          <asp:Label ID="Label8" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                          <asp:RegularExpressionValidator ID="RegularExpressinValidator3" ControlToValidate="txtFooterMinQty"
                                                ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"
                                                ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="requiredfieldvalidator12" runat="server" Display="Dynamic"
                                                ControlToValidate="txtFooterMinQty" Text="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            
                                  
                          </label>
                          </td>               
                          <td>
                             <label>
                                            <asp:TextBox ID="txtFooterMinQty" MaxLength="10" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'_&*()>+:;={}[]|\/]/g,/^[\0-9.\s]+$/,'div1242',10)"
                                                runat="server" Width="50px"></asp:TextBox>
                                                  </label>
                                        <label><asp:Label ID="Labgbel22" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="div1242" class="labelcount">10</span>
                                            <asp:Label ID="Lbel2z" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label></label>
                          </td>  
                             <td><label>  <asp:Label ID="Label28" runat="server" Text="Max Discount Qty"></asp:Label>
                                   <asp:Label ID="Label29" runat="server" Text="*" CssClass="labelstar"></asp:Label> <asp:RegularExpressionValidator ID="RegularExressionValidator3" ControlToValidate="txtFooterMaxQty"
                                                ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"
                                                ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="requiredfieldvalidator13" runat="server" ControlToValidate="txtFooterMaxQty"
                                                Text="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                
                                                </label></td>
                          <td>
                            <label>
                                            <asp:TextBox ID="txtFooterMaxQty" MaxLength="10" runat="server" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@#$%^'_&*()>+:;={}[]|\/]/g,/^[\0-9.\s]+$/,'div1241',10)"
                                                Width="50px"></asp:TextBox>
                                           
                                           
                                        </label>
                                        <label> <asp:Label ID="Lagbe922" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="div1241" class="labelcount">10</span>
                                            <asp:Label ID="Lbel2a" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label></label>
                          </td>  
                               
                    </tr>
                   
                    
                      <tr>
                      <td colspan="6">
                      <table width="100%">
                         <tr>
                        <td  >
                                  <label>
                       <asp:Label ID="Labtel26" runat="server" Text="Discount"></asp:Label>
                                   <asp:Label ID="Label127" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                      <asp:RegularExpressionValidator ID="RegularExpenValidator3" ControlToValidate="txtFooterDescount"
                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid" ValidationGroup="gpft1" Display="Dynamic"></asp:RegularExpressionValidator>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2223222" runat="server" Display="Dynamic" ControlToValidate="txtFooterDescount"
                                        ErrorMessage="*"  ValidationGroup="1"></asp:RequiredFieldValidator>
                                          <cc1:FilteredTextBoxExtender ID="txtSchemaVhhfalueDiscount" runat="server" Enabled="True"
                                        FilterType="Custom, Numbers" TargetControlID="txtFooterDescount" ValidChars="0.123456789">
                                    </cc1:FilteredTextBoxExtender>
                     </label>
                     
                       </td>
                <td >
                            <label>
                             <asp:TextBox ID="txtFooterDescount" runat="server" Width="100px"  MaxLength="10" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'_&*()>+:;={}[]|\/]/g,/^[\0-9.\s]+$/,'div124',10)">
                                  
                                    </asp:TextBox>
                                      </label>   <label>
                                  <asp:Label ID="Lagbel22" runat="server" Text="Max " CssClass="labelcount" ></asp:Label><span id="div124" class="labelcount">10</span>
                                     <asp:Label ID="Lbel2" runat="server" Text="(0-9 .)"  CssClass="labelcount"></asp:Label>
                       </label>
                      </td> 
                  <td  >
                                 <label>
                                    <asp:DropDownList ID="rdperamt" runat="server" Width="100px" 
                                >
                                        <asp:ListItem Selected="True" Text="Percentage" Value="1"></asp:ListItem>
                                          <asp:ListItem Text="Amount" Value="2"></asp:ListItem>
                                       </asp:DropDownList>
                                     </label>
                          
                           
                    
                    
                                                   
                                                    </td>
                        <td >
                           <table width="100%">
                           <tr>
                           <td>
                                <label>
                                                        <asp:Label ID="Label171" runat="server" Text="Start Date"></asp:Label>
                                                           <asp:Label ID="Label25" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequireldValidator4" runat="server" ControlToValidate="txtStartDate"
                                                            ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                              <cc1:CalendarExtender ID="txtdatelendarExtender"  runat="server" PopupButtonID="ImageButton3"
                                                            TargetControlID="txtStartDate">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="TextBox1EditExtender" runat="server" CultureName="en-AU"
                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtStartDate" />
                                                             <asp:RegularExpressionValidator ID="rghccccccjk1" runat="server" ErrorMessage="*" ControlToValidate="txtStartDate"
                                                ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                     
                                                    </label>
                           </td>
                           <td>
                                     <label>
                                            <asp:TextBox ID="txtStartDate" runat="server" Width="75px"></asp:TextBox>
                                           
                                        
                                        </label>
                                               
                                              
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                    </label>
                                              
                           </td>
                           <td>
                                   <label>
                                                        <asp:Label ID="Label26" runat="server" Text="End Date"></asp:Label>
                                                           <asp:Label ID="Label22" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFiealidator3" runat="server" ControlToValidate="txtEndDate"
                                                            ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton4"
                                                            TargetControlID="txtEndDate">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtEndDate" />
                                                            <asp:RegularExpressionValidator ID="rghjxk3" runat="server" ErrorMessage="*" ControlToValidate="txtEndDate"
                                                ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                 
                                                    </label>
                           </td>
                           <td>
                             <label>
                                                        <asp:TextBox ID="txtEndDate" runat="server" Width="75px"></asp:TextBox>
                                                           
                                                    </label>
                                               
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                    </label>
                           </td>
                           <td>
                             <label>
                                                    <asp:Label ID="Label27" runat="server" Text="Status"></asp:Label>
                                                
                                                    </label>
                           </td>
                           <td>
                             <label>
                                                   <asp:DropDownList ID="ddlstatus" runat="server" Width="75px" >
                                                   <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                                                       <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                                   </asp:DropDownList>
                                                
                                                    </label>
                           </td>
                           </tr>
                           </table>
                                       
                                                   
                        </td>
                                           
                                         
                    </tr>
                      
                      </table>
                      </td>
                      </tr>
                     
                   
                   
                </table>
                </asp:Panel>
                
                  <div style="clear: both;">
                  <table width="100%">
                  <tr>
                  <td align="center">
                   <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Submit" 
                          OnClick="Button2_Click" ValidationGroup="1" /> 
                            &nbsp;<asp:Button ID="imgupdate" runat="server" CssClass="btnSubmit" Text="Update" onclick="imgupdate_Click"  ValidationGroup="1" Visible="False" /> 
                            &nbsp;<asp:Button ID="Button3" runat="server" CssClass="btnSubmit" 
                          Text="Cancel" OnClick="Button3_Click" /> 
                        <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                    <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
      
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
                        <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="List of volume discounts"></asp:Label>
                    </legend>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Auto">
                   
                  
                                                 
                                                  
                        <div style="clear: both;">
                        
                        <table >
                        <tr>
                        <td> <label> <asp:Label ID="Label7" runat="server"  Text="Filter by Status"></asp:Label></label></td>
                        <td>  <label><asp:DropDownList ID="ddlfilst" runat="server" Width="75px" >
                        <asp:ListItem Value="2" Text="All" Selected="True"></asp:ListItem>
                                                   <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                       <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                                   </asp:DropDownList></label></td>
                                                   <td>  <label>
                                                   <asp:Label ID="Label9" runat="server"  Text="Filter By Effective Date"></asp:Label>
                                                   </label></td>
                           <td>
                                <label>
                                                        <asp:Label ID="Label11" runat="server" Text="Start Date"></asp:Label>
                                                           <asp:Label ID="Label12" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rd1" runat="server" ControlToValidate="txtfilsdate"
                                                            ErrorMessage="*" ValidationGroup="10" Display="Dynamic"></asp:RequiredFieldValidator>
                                                              <cc1:CalendarExtender ID="CalendarExtender2"  runat="server" PopupButtonID="ImageButton5"
                                                            TargetControlID="txtfilsdate">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureName="en-AU"
                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfilsdate" />
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtfilsdate"
                                                ValidationGroup="10" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                     
                                                    </label>
                           </td>
                           <td>
                                     <label>
                                            <asp:TextBox ID="txtfilsdate" runat="server" Width="75px"></asp:TextBox>
                                           
                                        
                                        </label>
                                               
                                              
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                    </label>
                                              
                           </td>
                           <td>
                                   <label>
                                                        <asp:Label ID="Label13" runat="server" Text="End Date"></asp:Label>
                                                           <asp:Label ID="Label32" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfiledate"
                                                            ErrorMessage="*" ValidationGroup="10" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="ImageButton6"
                                                            TargetControlID="txtfiledate">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureName="en-AU"
                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfiledate" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtfiledate"
                                                ValidationGroup="10" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                 
                                                    </label>
                           </td>
                           <td>
                             <label>
                                                        <asp:TextBox ID="txtfiledate" runat="server" Width="75px"></asp:TextBox>
                                                           
                                                    </label>
                                               
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                    </label>
                           </td>
                           <td>
                          <label> <asp:Button ID="btng" runat="server" CssClass="btnSubmit" ValidationGroup="10"
                          Text="Go" onclick="btng_Click"/></label></td>
                        </tr>
                        </table>
                </div>
                        <asp:GridView ID="txtFooterSchemaName" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                            AlternatingRowStyle-CssClass="alt" Width="100%" OnRowEditing="GrdSchemaVolume_RowEditing"
                            OnRowCommand="GrdSchemaVolume_RowCommand" DataKeyNames="SchemeID"
                            AllowPaging="true" AutoGenerateColumns="False" PageSize="5" OnPageIndexChanging="GrdSchemaVolume_PageIndexChanging"
                            AllowSorting="True" OnSorting="GrdSchemaVolume_Sorting" EmptyDataText="No Record Found."
                            OnRowDeleting="GrdSchemaVolume_RowDeleting" 
                            onrowdatabound="txtFooterSchemaName_RowDataBound">
                            <Columns>
                                <asp:TemplateField  HeaderText="Discount Name" SortExpression="SchemeName"
                                    HeaderStyle-HorizontalAlign="Left" FooterStyle-VerticalAlign="Top">
                                 
                                  
                                    <ItemTemplate>
                                       
                                            <asp:Label ID="lblScemaName" runat="server" Text='<%#Bind("SchemeName") %>'
                                                ></asp:Label>
                                     
                                   </ItemTemplate>
                                    <FooterStyle VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Min Discount Qty" SortExpression="MinDiscountQty"
                                    HeaderStyle-HorizontalAlign="Left" FooterStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                  
                                   
                                    <ItemTemplate>
                                      
                                            <asp:Label ID="lblMinDiscountQty"  runat="server" Text='<%#Bind("MinDiscountQty") %>'
                                            ></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                </asp:TemplateField>
                           <asp:TemplateField HeaderText="Max Discount Qty" SortExpression="MinDiscountQty"
                                    HeaderStyle-HorizontalAlign="Left" FooterStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                
                                  
                                    <ItemTemplate>
                                      
                                            <asp:Label ID="lblMaxDiscountQty" runat="server" Text='<%#Bind("MaxDiscountQty") %>'
                                              ></asp:Label>
                                        
                                    </ItemTemplate>
                                    <FooterStyle VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Discount Percent" SortExpression="SchemeDiscount"
                                    HeaderStyle-HorizontalAlign="Left" FooterStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                 
                                    <ItemTemplate>
                                      
                                            <asp:Label ID="lblsign" runat="server" Text="$" Visible="false"></asp:Label>
                                            <asp:Label ID="lblSchemeDiscount" ForeColor="#416271" runat="server" Text='<%#Bind("SchemeDiscount") %>'></asp:Label>
                                            <asp:Label ID="Label22" runat="server" Text="%" Visible="false"></asp:Label>
                                      
                                        <asp:CheckBox ID="chkIsprs1" runat="server" Text=" " Visible="false" Checked='<%#Eval("IsPercentage") %>'
                                            Enabled="false" />
                                        <asp:Label ID="lblIsPercent" runat="server" Text='<%#Eval("IsPercentage") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Start Date" HeaderStyle-Width="80px" SortExpression="EffectiveStartDate"
                                    HeaderStyle-HorizontalAlign="Left" FooterStyle-VerticalAlign="Top">
                                
                                   
                                    <ItemTemplate>
                                     
                                            <asp:Label ID="lblEffectiveStartDate"  runat="server" Text='<%#Bind("EffectiveStartDate") %>'></asp:Label>
                                             
                                      
                                    </ItemTemplate>
                                    <FooterStyle VerticalAlign="Top" />
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="End Date" HeaderStyle-Width="80px" SortExpression="EndDate"
                                    HeaderStyle-HorizontalAlign="Left" FooterStyle-VerticalAlign="Top">
                                 
                              
                                    <ItemTemplate>
                                     
                                            <asp:Label ID="lblEndDate" runat="server" Text='<%#Bind("EndDate") %>'
                                               ></asp:Label>
                                    
                                    </ItemTemplate>
                                    <FooterStyle VerticalAlign="Top" />
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Status" SortExpression="Active" HeaderStyle-HorizontalAlign="Left"
                                    FooterStyle-VerticalAlign="Top" HeaderStyle-Width="6%">
                                <ItemTemplate>
                                            <asp:Label ID="lblch" runat="server" Text='<%#Eval("status") %>'></asp:Label>
                                            <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%#Eval("Active") %>' Visible="false" ></asp:CheckBox>
                                       
                                  </ItemTemplate>
                                       
                                 <FooterStyle VerticalAlign="Top" />
                                 <HeaderStyle HorizontalAlign="Left" Width="6%" />
                                       
                                </asp:TemplateField>
                       <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif"  HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Width="3%" >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="img11" runat="server" CausesValidation="true" ToolTip="Edit" ImageUrl="~/Account/images/edit.gif"
                                                          CommandArgument='<%#Bind("SchemeID") %>'
                                                        CommandName="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField> 
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
                                
                         <asp:TemplateField  HeaderText="Apply Discount"  HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%"  ItemStyle-ForeColor="#416271" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="img11a" runat="server" CausesValidation="true" ToolTip="Apply Discount"  ForeColor="#416271"
                                                          CommandArgument='<%#Bind("SchemeID") %>' Text="Apply Discount"
                                                        CommandName="Select"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle ForeColor="#416271" />
                                            </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr" />
                            <AlternatingRowStyle CssClass="alt" />
                        </asp:GridView>
                    </asp:Panel>
                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                </fieldset>
                <asp:Panel ID="Panel1" runat="server" Visible="False" Width="100%">
                    <fieldset>
                        <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="Label5" runat="server" Text="Select Inventory To Apply Volume Discount"></asp:Label>
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="4">
                                                    <label>
                                                        <asp:Label ID="Label6" runat="server" Text="Apply Volume Discount Scheme "></asp:Label>
                                                        <asp:Label ID="lblScemaNamefromGrd" runat="server"></asp:Label>
                                                        <asp:Label ID="lblmd" runat="server" Text=""></asp:Label>
                                                           <%-- <asp:Label ID="Label7" runat="server" Text="(Min Disc" Qty=""></asp:Label>
                                                        <asp:Label ID="lblminqty" runat="server"></asp:Label>
                                                        <asp:Label ID="Label8" runat="server" Text="),(Max Disc" Qty=""></asp:Label>
                                                        <asp:Label ID="lblmaxqty" runat="server"></asp:Label>
                                                        <asp:Label ID="Label9" runat="server" Text="),(Disc = "></asp:Label>
                                                        <asp:Label ID="lbldis" runat="server"></asp:Label>
                                                        <asp:Label ID="Label10" runat="server" Text="),( Is" Pers=""></asp:Label>
                                                        <asp:Label ID="lblisper" runat="server"></asp:Label>
                                                        <asp:Label ID="Label11" runat="server" Text="),(Start" Date=""></asp:Label>
                                                        <asp:Label ID="lblsdate" runat="server"></asp:Label>
                                                        <asp:Label ID="Label12" runat="server" Text="),(End" Date=""></asp:Label>
                                                        <asp:Label ID="lblenddate" runat="server"></asp:Label>
                                                        <asp:Label ID="Label13" runat="server" Text=")"></asp:Label>--%>
                                                        <asp:Label ID="Labe" runat="server" Text="to the following inventory items:"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:CheckBox ID="chkisper" runat="server" Visible="false"></asp:CheckBox>
                                                        <asp:Label ID="lblsdate" runat="server" Visible="false"></asp:Label>
                                                          <asp:Label ID="lblenddate" runat="server" Visible="false"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            </td>
                                  </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label14" runat="server" Text="Business Name"></asp:Label>
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
                                                        <asp:Label ID="Label15" runat="server" Text="Category"></asp:Label>
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
                                                        <asp:Label ID="Label16" runat="server" Text="Sub Category"></asp:Label>
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
                                                        <asp:Label ID="Label17" runat="server" Text="Sub-Sub Category"></asp:Label>
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
                                                        <asp:Label ID="Label18" runat="server" Text="Inventory Name"></asp:Label>
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
                                                <td colspan="4" align="center">
                                                    <asp:Button ID="imgbtnGo" runat="server" Text="  Go  " CssClass="btnSubmit" OnClick="imgbtnGo_Click"
                                                        ValidationGroup="6" />
                                                    <asp:Button ID="btncan" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="btncan_Click1" />
                                                </td>
                                            </tr>
                                        </table>
                                    
                             <table width="100%">
                            <tr>
                                <td align="right" class="style2">
                                    <asp:Button ID="Buttonp1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                                        OnClick="Button1_Click1" />
                                    <input id="Button2" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        type="button" class="btnSubmit" visible="false" value="Print" />
                                </td>
                            </tr>
                            <tr>
                            
                            <td align="left">
                                <asp:Label ID="lblerrormsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                            <tr>
                                <td >
                                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                        <table id="GridTbl" width="100%">
                                            <tr align="center">
                                                <td>
                                                    <div id="mydiv" class="closed">
                                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Label ID="lblcomname" runat="server" Font-Size="20px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Label ID="lblBusiness" runat="server" Font-Size="20px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Label ID="lblmaa" runat="server" Font-Size="18px" Text="Apply Volume Discount Scheme"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="font-size: 16px; font-weight: normal">
                                                                    <asp:Label ID="Label4" runat="server" Text=" Main Category : " Font-Bold="true"></asp:Label>
                                                                    <asp:Label ID="lblMainCat" runat="server"></asp:Label>
                                                                    <asp:Label ID="Label19" runat="server" Text=", Sub Category : " Font-Bold="true"></asp:Label>
                                                                    <asp:Label ID="lblSubCat" runat="server"></asp:Label>
                                                                    <asp:Label ID="Label20" runat="server" Text=", Sub Sub Category : " Font-Bold="true"></asp:Label>
                                                                    <asp:Label ID="lblSubSub" runat="server"></asp:Label>
                                                                    <asp:Label ID="Label21" runat="server" Text=", Inventory : " Font-Bold="true"></asp:Label>
                                                                    <asp:Label ID="lblInv" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnp" runat="server" ScrollBars="Both" Width="100%">
                                                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                            CssClass="mGrid" PagerStyle-CssClass="pgr" GridLines="Both" AlternatingRowStyle-CssClass="alt"
                                                            DataKeyNames="InventoryWarehouseMasterId" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                            OnSorting="GridView1_Sorting" Width="100%" EmptyDataText="No Record Found.">
                                                            <Columns>
                                                                <asp:BoundField DataField="catandname" HeaderStyle-HorizontalAlign="Left" HeaderText="Name"
                                                                    SortExpression="catandname" />
                                                                    
                                                                      <asp:TemplateField  HeaderText="Weight/Unit" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="nr" runat="server" Text='<%#Bind("Weight") %>' />
                                                                          <asp:Label ID="Labentrl7" runat="server" Text='<%#Bind("UnitName") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                              <%--  <asp:BoundField DataField="" HeaderText="Weight" HeaderStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="UnitName" HeaderText="Unit" HeaderStyle-HorizontalAlign="Left" />
                                                          --%>      <asp:TemplateField HeaderText="Sales Rate" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsalesrate" runat="server" Text='<%#Bind("Rate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-Width="5%" HeaderText="Avg. Cost rate" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblavgcost" runat="server" Text="" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Promo Discount (amt)" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblpromp" runat="server" />
                                                                        <asp:Label ID="lblpromdetailid" runat="server" Visible="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-Width="2%" HeaderText="Remove Promo Discount" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkpromoremove" runat="server" Checked="false" />
                                                                        <asp:CheckBox ID="chkpromoh" runat="server" Checked="false" Visible="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Volume Discount (amt)" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblvald" runat="server" />
                                                                        <asp:Label ID="lblvoldesdetail" runat="server" Visible="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Max Order Value Discount (amt)" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblmaxorder" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Discounts (amt)" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltotdiscamt" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Net Sales Rate" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblnetsales" runat="server" />
                                                                         <asp:Label ID="lblsalerror" runat="server"  Text="*" Visible="false" ForeColor="Red" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Mark Up %<br>After Discount" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblmarkup" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- <asp:TemplateField HeaderStyle-Width="2%" HeaderText="Remove Volume Discount" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkvoldis" runat="server" Checked="false" />
                                                                    </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lbl1" runat="server" Text="Apply to<br> this Product"></asp:Label>
                                                                        <br />
                                                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_chachedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="chktemp_chachedChanged" />
                                                                        <asp:CheckBox ID="chkvoldish" runat="server" Checked="false" Visible="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-Width="2%" HeaderText="Online Sales" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkonapp" runat="server" Checked="false" Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-Width="2%" HeaderText="Retail Sales" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkretailap" runat="server" Enabled="false" Checked="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="ImageButton1" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="Button1_Click" />
                                    <%--<asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Apply" ImageUrl="~/ShoppingCart/images/submit.png"
                                OnClick="Button1_Click" />--%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    </fieldset>
                <table id="subinnertbl" cellspacing="0" cellpadding="0" width="100%">
                    <tbody>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" BorderStyle="Outset"
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
                                                    <asp:Label ID="Label2" runat="server" Text="Start Date of the Year is "></asp:Label>
                                                    <asp:Label ID="lblstartdate" runat="server"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lblm0" runat="server">You can not select any date earlier 
                                                than that. </asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="height: 26px">
                                                <asp:Button ID="ImageButton2" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="ImageButton2_Click" />
                                                <%--<asp:ImageButton ID="ImageButton2" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/cancel.png"
                                                                OnClick="ImageButton2_Click" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                    &nbsp;</asp:Panel>
                                <asp:Button ID="HiddenButton222" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                                    ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel5" TargetControlID="HiddenButton222">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="col2">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
