<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="CustomerDCEdit.aspx.cs" Inherits="CustomerDCEdit"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

      <script language="javascript" type="text/javascript">
          function checktextboxmaxlength(txt, maxLen, evt) {
              try {
                  if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                      return false;
              }
              catch (e) {

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
          function mak(id, max_len, myele) {
              counter = document.getElementById(id);

              if (myele.value.length <= max_len) {
                  remaining_characters = max_len - myele.value.length;
                  counter.innerHTML = remaining_characters;
              }
          }
          function mask(evt, max_len) {



              if (evt.keyCode == 13) {

                  return true;
              }


              if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                  alert("You have entered an invalid character");
                  return false;
              }




          }
          </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           <div class="products_box">
            <fieldset>
                 <div style="clear: both;">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
           
           <fieldset>
           <legend>
           Select your sales order to prepare a packing slip
           </legend>
            <label>
                                <asp:Label ID="Label5" runat="server" Text="Business Name"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlWarehouse"
                                ErrorMessage="*" InitialValue="0" ValidationGroup="3"></asp:RequiredFieldValidator>
                              <asp:DropDownList ID="ddlWarehouse" runat="server" 
                   AutoPostBack="True" 
                   OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" Enabled="False"
                                >
                                </asp:DropDownList>
                                <input id="hdnWHid" runat="server" type="hidden" />
             
                            
                            </label>    
                             <label>
                                <asp:Label ID="Label23" runat="server" Text="Select Sales Order Type" ></asp:Label>
                             <asp:DropDownList ID="ddlSalesordertype" runat="server" 
                   AutoPostBack="True" 
                   onselectedindexchanged="ddlSalesordertype_SelectedIndexChanged" 
                   Width="150px" Enabled="False" >
                              <asp:ListItem Value="1" Text="All"></asp:ListItem>
                               <asp:ListItem Value="2" Text="Online Orders"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Retail Orders"></asp:ListItem>
                            </asp:DropDownList>
                            </label>
                             <label>
                                <asp:Label ID="Label9" runat="server" Text="Sales Order Status"></asp:Label>
                             <asp:DropDownList ID="ddlSalesOrdStatus" runat="server" 
                   AutoPostBack="True" 
                   OnSelectedIndexChanged="ddlSalesOrdStatus_SelectedIndexChanged" Enabled="False"
                                >
                            </asp:DropDownList>
                            </label>
                             <label>
                                <asp:Label ID="Label11" runat="server" Text="Sales Order No." ></asp:Label>
                            
                            <asp:DropDownList ID="ddlRefSellno" runat="server" 
                   AutoPostBack="True" OnSelectedIndexChanged="ddlRefSellno_SelectedIndexChanged"
                                Width="280px" Enabled="False">
                            </asp:DropDownList>
                            </label> 
                             <label>
                                <asp:Label ID="Label10" runat="server" Text="Date"></asp:Label>
                                <br />
                                  <asp:Label ID="lblDate" runat="server"></asp:Label>
                            </label>  
                              <div style="clear: both;">
            </div>
            
            
           
           </fieldset>
            
                      <div style="clear: both;">
            </div>
            <asp:Panel ID="pnlsh" runat="server" Visible="false">
            <fieldset>
               <table id="Table5" width="100%">
                 <tr>
                 <td width="50%">
                    <label>
                                <asp:Label ID="Label12" runat="server" Text="Party Name: "></asp:Label>
                               
                               <asp:Label ID="lblPartyName" runat="server" ></asp:Label>
                                <asp:DropDownList ID="ddlParty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlParty_SelectedIndexChanged"
                                Width="147px" Visible="false">
                            </asp:DropDownList>
                              <asp:Label ID="lblDelNo" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSalesInvoice" runat="server" Visible="False"></asp:Label>
                            </label>                             
                       </td>
                       <td width="50%">
                     
                            <label>
                            <asp:Label ID="Label4" runat="server" Text="Contact Name: "></asp:Label>
                               <asp:Label ID="lblcpers" runat="server" ></asp:Label>
                           <asp:CheckBox ID="checkGnrInvoice" runat="server" Checked="True"
                                Text="Generate Invoice ?" Visible="False" OnCheckedChanged="checkGnrInvoice_CheckedChanged" />                          
             
                           </label>
                           </td>
                            
                 </tr>
                     <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label13" runat="server" Text="Bill To:"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlBillTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBillTo_SelectedIndexChanged"
                                Visible="False" Width="147px">
                            </asp:DropDownList>
                          
                            <asp:Label ID="lblBillAddress" runat="server"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:Label ID="Label14" runat="server" Text="Ship To:"></asp:Label>
                           
                        <br />
                            <asp:DropDownList ID="ddlShipTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlShipTo_SelectedIndexChanged"
                                Visible="False" Width="147px">
                            </asp:DropDownList>
                          
                            <asp:Label ID="lblShipAddress" runat="server" ></asp:Label>
                        
                        </td>
                    </tr>
                    </table>
            </fieldset>
            <fieldset>
          <legend>
        Shipping Information
          </legend>
             <table >
                        <tr>
                      <td>
                             <label>
                                <asp:Label ID="Label15" runat="server" Text="Order Shipped By"></asp:Label>
                              
                             <asp:DropDownList ID="ddlShippers" runat="server" AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlShippers_SelectedIndexChanged">
                            </asp:DropDownList>
                            </label>
                            </td>
                       <td>
                           <label>
                                <asp:Label ID="Label16" runat="server" Text="Shippers Tracking No." 
                               ></asp:Label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTrckNo"
                                ErrorMessage="*" ValidationGroup="4"></asp:RequiredFieldValidator>
                             
                                 <asp:TextBox ID="txtTrckNo" runat="server" Width="100px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                TargetControlID="txtTrckNo" ValidChars="0147852369.">
                            </cc1:FilteredTextBoxExtender>
                            </label>
                      
                        </td>
                          <td  >
                        <asp:Panel ID="pnlcustprefer" runat="server" Visible="false">
                           <label>
                          
                            <asp:Label ID="Label3" runat="server" Text="Customer's Preferred Shipping Company" ></asp:Label>
                         
                          <asp:Label ID="lblshippingType" runat="server"></asp:Label>
                            </label>
                            </asp:Panel>
                            </td>
                        </tr>
                        
                        <tr>
                        <td >
                          <asp:Panel ID="pnlcustpresh" runat="server" Visible="false">
                          <label>
                                <asp:Label ID="Label17" runat="server" Text="Customers Preferred Shipping Option" ></asp:Label>
                            
                            <asp:DropDownList ID="ddlshipoption" runat="server" 
                                OnSelectedIndexChanged="ddlshipoption_SelectedIndexChanged">
                            </asp:DropDownList>
                            </label>
                        </asp:Panel>
                        </td><td>
                            <label>
                                <asp:Label ID="Label19" runat="server" Text="Shipping Date" ></asp:Label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtGoodsDate"
                                ErrorMessage="*" ValidationGroup="4"></asp:RequiredFieldValidator>
                               
                            <asp:TextBox ID="txtGoodsDate" runat="server" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                PopupButtonID="imgCal" TargetControlID="txtGoodsDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1534" runat="server" CultureName="en-AU"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtGoodsDate" />
                            </label>
                            <label>
                           <br />
                            <asp:ImageButton ID="imgCal" runat="server" ImageUrl="~/images/calender.jpg" 
                                OnClick="imgCal_Click" Height="16px" />
                           </label>
                           </td><td>
                            <label>
                                <asp:Label ID="Label20" runat="server" Text="Receipt No." ></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtRecieptNo"
                                ErrorMessage="*" ValidationGroup="4"></asp:RequiredFieldValidator>
                              
                              <asp:TextBox ID="txtRecieptNo" runat="server" Width="100px"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                TargetControlID="txtRecieptNo" ValidChars="0147852369.">
                            </cc1:FilteredTextBoxExtender>
                            </label>     
                        </td>
                      
                        </tr>
                     
                    
                    <tr>
                        <td valign="top">
                         <label>
                                <asp:Label ID="Label22" runat="server" Text="Shipping Person" ></asp:Label>
                            <%--  </label>  <label>--%>
                            <asp:DropDownList ID="ddlShippingPerson" runat="server" 
                                OnSelectedIndexChanged="ddlShippingPerson_SelectedIndexChanged">
                            </asp:DropDownList>
                            </label>
                            </td><td colspan="2" >
                            <label>
                                <asp:Label ID="Label21" runat="server" Text="Note" ></asp:Label>
                           <br />
                           <asp:TextBox ID="txtNote" runat="server" Height="72px" TextMode="MultiLine" 
                                Width="520px"  onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*>+:;={}()[]|\/]/g,/^[\a-zA-Z.0-9_\s]+$/,'div2',500)" 
                  MaxLength="500" 
                onkeypress="return checktextboxmaxlength(this,500,event)" ></asp:TextBox>
                         </label><label>
                         <br />
                        <asp:Label ID="Label28" runat="server" Text="Max " CssClass="labelcount" ></asp:Label><span id="div2" class="labelcount">500</span>
                                                                                <asp:Label ID="Label49" runat="server" Text="(A-Z 0-9 _ .)" CssClass="labelcount"></asp:Label>
                            </label>
                            
                        </td>
                      
                      
                    </tr>
                      <tr>
                        <td colspan="3">
                             <label>
                                <asp:Label ID="Label18" runat="server" Text="Shippers Document No." ></asp:Label>
                              <%--  </label><label>--%>
                              <asp:TextBox ID="txtShippersDocNo" runat="server" Width="100px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                TargetControlID="txtShippersDocNo" ValidChars="0147852369.">
                            </cc1:FilteredTextBoxExtender>
                            </label>
                            
                       
                            <label>
                            <asp:TextBox ID="txtShipCost" runat="server" Width="145px" OnTextChanged="txtShipCost_TextChanged"
                                Visible="False">0</asp:TextBox>
                            </label>
                        </td>
                    </tr>
                      <tr>                       
                       <td colspan="2">
                            <asp:CheckBox ID="chkdoc" runat="server" 
                                Text="Would you like to add any documents to this packing slip?" />
                        </td>
                    </tr>
                        </table> 
            </fieldset>
                     
                   <fieldset>
                     <legend></legend>
                <table id="Table1" width="100%">
                 
             
                    <tr>
                        <td>
                            <asp:Panel ID="pnlSearch" runat="server"  Visible="False" Width="100%">
                                <table id="Table2"  width="100%">
                                  <tr>
                                        <td>
                                        <label>
                                        <asp:Label ID="lblprod" runat="server" Text="List of Products Added to Packing Slip/Service Completion Note"></asp:Label>
                                        </label>
                                        </td>
                                        </tr>
                                   
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                                                        Width="100%" HeaderStyle-HorizontalAlign="Left">
                                                        <Columns>
                                                            <asp:BoundField DataField="InventoryWarehouseMasterId" HeaderText="ID" HeaderStyle-HorizontalAlign="Left"/>
                                                          
                                                            <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"/>
                                                              <asp:BoundField DataField="ProductNo" HeaderText="Product No." HeaderStyle-HorizontalAlign="Left"/>
                                                            <asp:BoundField DataField="Unit" HeaderText="Weight/Unit" HeaderStyle-HorizontalAlign="Left"/>
                                                            <asp:TemplateField  HeaderText="Qty On Hand" HeaderStyle-HorizontalAlign="Left">
                                                          <ItemTemplate>
                                                                 
                                                                    <asp:Label ID="lblqtyonhand" runat="server" ></asp:Label>
                                                                      <asp:Label ID="lblavgrate" runat="server"  Visible="false"></asp:Label>
                                                                     <asp:Label ID="lblinvW" runat="server" Text='<%# Bind("InventoryWarehouseMasterId") %>' Visible="false" ></asp:Label>
                                                                </ItemTemplate>
                                                         
                                                         </asp:TemplateField>
                                                         <asp:TemplateField  HeaderText="Ordered Qty" HeaderStyle-HorizontalAlign="Left">
                                                          <ItemTemplate>
                                                                 
                                                                    <asp:Label ID="lblqty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                         
                                                         </asp:TemplateField>
                                                            <asp:BoundField DataField="UnshipQty" HeaderText="Unshipped Qty" HeaderStyle-HorizontalAlign="Left"/>
                                                            <asp:TemplateField HeaderText="Shipped Qty" HeaderStyle-HorizontalAlign="Left">
                                                               
                                                                <ItemTemplate>
                                                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="TextBox4" ValidChars="0147852369.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("OderedQty") %>' Width="60px" AutoPostBack="true"  OnTextChanged="textqty_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty Shortage" HeaderStyle-HorizontalAlign="Left">
                                                               
                                                                <ItemTemplate>
                                                                   <%-- <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender76" runat="server"
                                                                        Enabled="True" TargetControlID="TextBox5" ValidChars="0147852369.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:TextBox ID="TextBox5" runat="server" Width="60px" AutoPostBack="true"  OnTextChanged="textqty1_TextChanged">0</asp:TextBox>
                                                               --%> 
                                                                 <asp:Label ID="TextBox5" runat="server" Text='<%# Bind("QtyShort") %>'></asp:Label>
                                                         </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Note" HeaderStyle-HorizontalAlign="Left">
                                                             
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TextBox6" runat="server" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="iwhmid" runat="server" Text='<%# Bind("InventoryWarehouseMasterId")%>'></asp:Label>
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
                        <td>
                            <asp:Panel ID="pnlservices" runat="server"  Visible="true" Width="100%">
                                <table id="Table3"  width="100%">
                                  <tr>
                                        <td>
                                        <label>
                                        <asp:Label ID="Label24" runat="server" Text="List of Services Added to Packing Slip/Service Completion Note"></asp:Label>
                                        </label>
                                        </td>
                                        </tr>
                                   
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"  CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                                                        Width="100%" HeaderStyle-HorizontalAlign="Left">
                                                        <Columns>
                                                            <asp:BoundField DataField="InventoryWarehouseMasterId" HeaderText="ID" HeaderStyle-HorizontalAlign="Left"/>
                                                         
                                                            <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"/>
                                                               <asp:BoundField DataField="ProductNo" HeaderText="Sevices No." HeaderStyle-HorizontalAlign="Left"/>
                                                            <asp:BoundField DataField="Unit" HeaderText="Weight/Unit" HeaderStyle-HorizontalAlign="Left" Visible="false"/>
                                                            <asp:TemplateField  HeaderText="Qty On Hand" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                          <ItemTemplate>
                                                                 
                                                                    <asp:Label ID="lblqtyonhand" runat="server" ></asp:Label>
                                                                     <asp:Label ID="lblinvW" runat="server" Text='<%# Bind("InventoryWarehouseMasterId") %>' Visible="false" ></asp:Label>
                                                                </ItemTemplate>
                                                         
                                                         </asp:TemplateField>
                                                         <asp:TemplateField  HeaderText="Number of Services/Hours Ordered" HeaderStyle-HorizontalAlign="Left">
                                                          <ItemTemplate>
                                                                 
                                                                    <asp:Label ID="lblqty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                         
                                                         </asp:TemplateField>
                                                            <asp:BoundField DataField="UnshipQty" HeaderText="Number of Services/Hours Yet to be Provided" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120px" />
                                                            <asp:TemplateField HeaderText="Number of Services/Hours Provided Now" HeaderStyle-HorizontalAlign="Left">
                                                               
                                                                <ItemTemplate>
                                                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="TextBox4" ValidChars="0147852369.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("OderedQty") %>' Width="60px" AutoPostBack="true"  OnTextChanged="textqty2_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Number of Services/Hours Provided Shortage" HeaderStyle-HorizontalAlign="Left">
                                                               
                                                                <ItemTemplate>
                                                                   <%-- <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender76" runat="server"
                                                                        Enabled="True" TargetControlID="TextBox5" ValidChars="0147852369.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:TextBox ID="TextBox5" runat="server" Width="60px" AutoPostBack="true"  OnTextChanged="textqty1_TextChanged">0</asp:TextBox>
                                                               --%> 
                                                                 <asp:Label ID="TextBox5" runat="server"  Text='<%# Bind("QtyShort") %>'></asp:Label>
                                                         </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Note" HeaderStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TextBox6" runat="server" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="iwhmid" runat="server" Text='<%# Bind("InventoryWarehouseMasterId")%>'></asp:Label>
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
                        <td align="left">
                        <label>Order Status</label>
                        <label>
                            <asp:DropDownList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                <asp:ListItem Value="1">Order Partially Shipped but considered Complete</asp:ListItem>
                                <asp:ListItem Value="2">Partially Shipped and on Backorder</asp:ListItem>
                            
                                <asp:ListItem Value="3">Partially Shipped</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="4">Fully Shipped</asp:ListItem>
                          </asp:DropDownList>
                          </label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Update" CssClass="btnSubmit" 
                                OnClick="btnSubmit_Click" />
                          
                            <asp:Button ID="btnPrintCustomer" runat="server" Text="Print Customer" 
                                CssClass="btnSubmit" OnClick="btnPrintCustomer_Click" Visible="False" />
                           
                        </td>
                    </tr>
                   
                    <tr>
                        <td>                                                                                                               
                            <asp:Label ID="lblSubTotal" runat="server" 
                                Visible="False" Font-Size="10pt"></asp:Label>
                            <asp:Label ID="lblCDisc" runat="server" 
                                Visible="False" Font-Size="10pt"></asp:Label>
                            <asp:Label ID="lblVDis" runat="server" 
                                Visible="False" Font-Size="10pt"></asp:Label>
                            <asp:Label ID="lblOVHCharge" runat="server" 
                                Visible="False" Font-Size="10pt"></asp:Label>
                            <asp:Label ID="lblShipCharge" runat="server" 
                                Visible="False" Font-Size="10pt"></asp:Label>
                            <asp:Label ID="lblTax" runat="server" 
                                Visible="False" Font-Size="10pt"></asp:Label>
                            <asp:Label ID="lblTotal" runat="server" 
                                Visible="False" Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel2" runat="server" CssClass="panelpro" Visible="False" Width="100%">
                                <table cellpadding="0" cellspacing="0" style="width: 750px" id="Table7">
                                    <tr>
                                        <td colspan="2" class="subinnertblfc">
                                            <label>
                                                <asp:Label ID="Label33" runat="server" Text="Sales Order Amts"></asp:Label>
                                            </label>
                                           
                                        </td>
                                        <td colspan="2" class="subinnertblfc">
                                            <label>
                                                <asp:Label ID="Label34" runat="server" Text="Invoice Amt"></asp:Label>
                                            </label>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label35" runat="server" Text="Sub Total"></asp:Label>
                                            </label>
                                            
                                        </td>
                                        <td>
                                            <label>                                            
                                            <asp:Label ID="lblSubTotalSO" runat="server" Text="0.00"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label36" runat="server" Text="Sub Total"></asp:Label>
                                            </label>
                                             
                                        </td>
                                        <td >
                                            <asp:Label ID="lblSubTotalSI" runat="server" Text="0.00"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                             <label>
                                                <asp:Label ID="Label37" runat="server" Text="Customter Discount"></asp:Label>
                                            </label>                                            
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblCustDisSO" runat="server" Text="0.00"></asp:Label>
                                            </label>                                            
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label38" runat="server" Text="Customter Discount"></asp:Label>
                                            </label>                                            
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblCustDisSI" runat="server" Text="0.00"></asp:Label>
                                            </label>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label39" runat="server" Text=" Order Value Discount "></asp:Label>
                                            </label>                                          
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblValueDisSO" runat="server" Text="0.00"></asp:Label>
                                            </label>                                            
                                        </td>
                                        <td>
                                             <label>
                                                <asp:Label ID="Label40" runat="server" Text=" Order Value Discount "></asp:Label>
                                            </label> 
                                           
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblValueDisSI" runat="server" Text="0.00"></asp:Label>
                                            </label>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label41" runat="server" Text="Handling Charge"></asp:Label>
                                            </label> 
                                            
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblHandChrgSO" runat="server" Text="0.00"></asp:Label>
                                            </label>
                                            
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label42" runat="server" Text="Handling Charge"></asp:Label>
                                            </label> 
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblHandChrgSI" runat="server" Text="0.00"></asp:Label>
                                            </label>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label43" runat="server" Text="Shipping Charge"></asp:Label>
                                            </label> 
                                            
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblShipChrgSO" runat="server" Text="0.00"></asp:Label>
                                            </label>
                                            
                                        </td>
                                        <td>
                                             <label>
                                                <asp:Label ID="Label44" runat="server" Text="Shipping Charge"></asp:Label>
                                            </label> 
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblShipChrgSI" runat="server" Text="0.00"></asp:Label>
                                            </label>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label45" runat="server" Text="Tax"></asp:Label>
                                            </label>
                                            
                                        </td>
                                        <td>
                                            <label>
                                            <asp:Label ID="lblTaxSO" runat="server" Text="0.00"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label46" runat="server" Text="Tax"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                            <asp:Label ID="lblTaxSI" runat="server" Text="0.00"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label47" runat="server" Text="Total"></asp:Label>
                                            </label>
                                             
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblTotalSO" runat="server" Text="0.00"></asp:Label>
                                            </label>
                                            
                                        </td>
                                        <td>
                                           <label>
                                                <asp:Label ID="Label48" runat="server" Text="Total"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblTotalSI" runat="server" Text="0.00"></asp:Label>
                                            </label>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Width="150px"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Width="150px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                           <label>
                                            <asp:Label ID="lblSalesOrderType" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="2">
                                            <label>
                                            <asp:Label ID="Label6" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                </fieldset></asp:Panel>
            </fieldset>
           </div> 
            <div style="clear: both;">
            </div> 
             <asp:Panel ID="pnllast2" runat="server"  CssClass="modalPopup"  >
             <fieldset>
             <legend></legend>
            
                <table  width="100%">
                    <tr>
                        <td colspan="2">
                            <label>
                                <asp:Label ID="Label30" runat="server" Text="You have successfully created your packing slip."></asp:Label>
                            </label>
                           
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <label>
                            <asp:Label ID="Label1" runat="server" Text="Would you like to print your invoice or packing slip?"></asp:Label>
                            </label>
                        </td>
                    </tr>
                     <tr>
                        <td colspan="2">
                           <asp:Button ID="imgbtnPrintCreditInv" runat="server" Text="Print Invoice" CssClass="btnSubmit"
                                OnClick="imgbtnPrintCreditInv_Click" />
                            <asp:Button ID="imgbtnPrintPackingSlip" runat="server" Text="Print Packing Slip" CssClass="btnSubmit" OnClick="imgbtnPrintPackingSlip_Click" />
                               
                        </td>
                        </tr>
                          <tr>
                        <td colspan="2">
                            <label>
                            <asp:Label ID="Label25" runat="server" Text="Would you like to email the invoice or packing slip to the customer?"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="2">
                            
                              
                                <asp:Button ID="btnprintcreditinv" runat="server" 
                                Text="Email Invoice" CssClass="btnSubmit" onclick="btnprintcreditinv_Click"
                               />
                               <asp:Button ID="btprintpak" runat="server" 
                                Text="Email Packing Slip" CssClass="btnSubmit" onclick="btprintpak_Click"
                                />
                        </td>
                    </tr>
                     <tr>
                        <td colspan="2">
                          
                           <asp:Button ID="imgbtnCancel" runat="server" Text="Close" CssClass="btnSubmit" ImageUrl="~/ShoppingCart/images/cancel.png"
                                OnClick="imgbtnCancel_Click" />         
                        </td>
                    </tr>
                </table>
                 </fieldset></asp:Panel>
            <asp:HiddenField ID="hdnbtn" runat="server" />
             <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="hdnbtn2"
                                PopupControlID="pnllast2" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
             <div style="clear: both;">
            </div> 
            <asp:Panel ID="pnllast" runat="server" BackColor="#CCCCCC"  Width="300px">
                    <table id="Table4" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label32" runat="server" Text="All The Items ordered are not being shipped"></asp:Label>
                                </label>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                <asp:Label ID="Label2" runat="server" Text="Are you sure you want to mark this order as fully Shipped?"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnYes" runat="server" Text="Submit" CssClass="btnSubmit"
                                    OnClick="btnYes_Click" />
                                <asp:Button ID="btnNo" runat="server" Text="Cancel" CssClass="btnSubmit"
                                    OnClick="btnNo_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:HiddenField ID="hdnbtn2" runat="server" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hdnbtn"
                    PopupControlID="pnllast" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
