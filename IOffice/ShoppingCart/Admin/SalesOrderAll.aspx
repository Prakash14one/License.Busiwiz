<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="SalesOrderAll.aspx.cs" Inherits="ShoppingCart_Admin_SalesOrderAll"
    Title="SalesOrderAll" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ShoppingCart/Admin/UserControl/UControlWizardpanel.ascx" TagName="pnl"
    TagPrefix="pnl" %>
<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
    
    
    
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
function Button1_onclick() {

}

function Button2_onclick() {

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
         
         <fieldset>
         
         
         <legend>
             <asp:Label ID="Label36" runat="server" Text="Sales Order Report Between Dates">
             </asp:Label>  
         </legend>
         
        
         
         
         
<%--         
  <asp:Panel ID="pnlgrid" runat="server" Width="100%" ScrollBars="Vertical"> 
         
    <table width="100%">
        <tr>
            <td>
            <div id="mydiv" class="closed">
            </div>--%>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                    <asp:Panel runat="Server" ID="panel1" Width="100%">
                        <table cellpadding="0" cellspacing="0" width="100%">                      
                         
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label33" runat="server" Text="From"></asp:Label>                                
                                    </label>
                                </td>
                                <td>         
                                    <label>
                                         <asp:TextBox ID="txtfromdate" runat="server" Width="153px"></asp:TextBox>
                                    </label>                    
                                    <label>     
                                         <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/ShoppingCart/images/cal_btn.jpg" />
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="imgbtncal"
                                                        TargetControlID="txtfromdate">
                                                    </cc1:CalendarExtender>
                                                    <cc1:MaskedEditExtender ID="TextBox1_MaskedEditExtender4" runat="server"
                                                    CultureName= "en-AU"  Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfromdate"/> 
                                                    
                                    </label>                                      
                                                  
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label34" runat="server" Text="To"></asp:Label>                                
                                    </label>                                
                                </td>
                                <td>
                                    <label>
                                      <asp:TextBox ID="txtTodate" runat="server" Width="153px"></asp:TextBox>
                                     </label> 
                                     <label> 
                                      <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_btn.jpg"  />
      
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                                                    TargetControlID="txtTodate">
                                                </cc1:CalendarExtender>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                CultureName= "en-AU"  Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtTodate"/> 
                                     </label>           
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label35" runat="server" Text="Store Name"></asp:Label>                               
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlwarehouse" runat="server" Width="157px" ValidationGroup="1">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                               </td>
                                <td>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                
                                    <asp:Button ID="ImageButton3" CssClass="btnSubmit" runat="server" Text="Go" ValidationGroup="1" OnClick="Button101_Click" />
                                   </td>
                            </tr>
                        </table>
                </asp:Panel>
                    <fieldset>
                    <legend>
                        <asp:Label ID="Label37" runat="server" Text="List of Sales Order Report Between Dates"></asp:Label>
                    </legend>
                
             
                    <table width="100%">    
                       
                        <tr>
                            <td colspan="4">
                                
                                     
                              <div style="float:right">
                                  
                                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version" OnClick="Button1_Click1" />
                                        <input id="Button2" runat="server" class="btnSubmit"
                                                                onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';" 
                                                                style="width: 51px;" type="button" value="Print" visible="false" />
                                   
                                    </div>
                                     <div style="clear: both;">
                                </div>
                             </td>
                        </tr>
                        <tr>
                        <td colspan="4">
                         <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                            <table width="100%">                              
                                <tr>
                                    <td>
                                        <div id="mydiv" class="closed">
                                            <table width="850Px">
                                                <tr align="center">
                                                    <td align="center" style="text-align: center; font-size: 20px;
                                                        font-weight: bold;">
                                                        <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                    </td>
                                                    </tr>
                                                    <tr align="center">
                                                    <td align="center" style="text-align: center; font-size: 20px;
                                                        font-weight: bold;">
                                                        <asp:Label ID="lblBusiness" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td align="center" 
                                                        style="text-align: center; font-size: 18px; font-weight: bold;">
                                                        <asp:Label ID="Label38" runat="server" Font-Italic="true" Text="List of Sales Order Report Between Dates"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr> 
                        <tr>
                            <td colspan="4" style="width:100%" align="left">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="SalesOrderId" Width="100%"
                                    OnRowCommand="GridView1_RowCommand" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="mGrid" 
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found.">
                                    <Columns>
                                        <asp:BoundField DataField="SalesOrderId" HeaderText="Order No" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                        <asp:BoundField DataField="SalesOrderDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GrossAmount" HeaderText="Total Amount" HeaderStyle-HorizontalAlign="Left">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Compname" HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Left">
                                          
                                        </asp:BoundField>
                                        <asp:ButtonField CommandName="detail" Text="Detail" HeaderText="Detail" ItemStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Left">                                           
                                           
                                        </asp:ButtonField>
                                    </Columns>
                    
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                        </td>
                        </tr>
                     </table>   
                 </fieldset>
                        </td>
                    </tr>
                    <tr>
                      <td>
                        <asp:Panel runat="Server" ID="panel2" Width="100%">
                            <table width="100%">
                                <tr>
                                    <td colspan="4">
                                    <label>
                                        <asp:Label ID="lblHeading" runat="server" Visible="False"></asp:Label>
                                    </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                    <label>
                                        <asp:Label ID="Label30" runat="server" Text="Sales Order" Font-Size="X-Large"></asp:Label>                               
                                    </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    <label>
                                        <asp:Label ID="Label31" runat="server" Text="Order Detail" Font-Size="Large"></asp:Label>                               
                                    </label>
                                       
                                    </td>
                                    <td colspan="2">
                                    <label>
                                        <asp:Label ID="Label32" runat="server" Text="Customer Information" Font-Size="Large"></asp:Label>                               
                                    </label>
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <label>
                                        <asp:Label ID="Label29" runat="server" Text="Order"></asp:Label>                               
                                    </label>   
                                    </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                                   </label>
                                   </td>
                                    <td>
                                    </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblemail" runat="server"></asp:Label>
                                   </label>
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                    <label>
                                        <asp:Label ID="Label28" runat="server" Text="Date"></asp:Label>
                                       
                                     </label>
                                      
                                   </td>
                                    <td>
                                    <label>
                                       <asp:Label ID="lblDate" runat="server"></asp:Label>
                                    </label>
                                    </td>
                                    
                                    <td>
                                      </td>
                                    <td>
                                       </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    <label>
                                        <asp:Label ID="Label26" runat="server" Text="Billing Address" Font-Size="Large"></asp:Label>
                                       
                                     </label>
                                       
                                    </td>
                                    <td colspan="2">
                                    <label>
                                        <asp:Label ID="Label27" runat="server" Text="Shipping Address" Font-Size="Large"></asp:Label>
                                       
                                     </label>
                                       
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <label>
                                        <asp:Label ID="Label24" runat="server" Text="Name"></asp:Label>
                                       
                                     </label>
                                    </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblBName" runat="server"></asp:Label>
                                    </label>
                                    </td>
                                    
                                    <td>
                                     <label>
                                        <asp:Label ID="Label25" runat="server" Text="Name"></asp:Label>
                                       
                                     </label>
                                     </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblSName" runat="server"></asp:Label>
                                     </label> 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <label>
                                        <asp:Label ID="Label22" runat="server" Text="Address"></asp:Label>
                                       
                                     </label>
                                    </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblBAddress" runat="server" Font-Bold="False"></asp:Label>
                                    </label>
                                    </td>
                                    <td>
                                       <label>
                                        <asp:Label ID="Label23" runat="server" Text="Address"></asp:Label>
                                       
                                     </label>
                                    </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblSAddress" runat="server" Font-Bold="False"></asp:Label>
                                    </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                     <label>
                                        <asp:Label ID="Label20" runat="server" Text="Country"></asp:Label>
                                       
                                     </label>
                                    
                                    </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblBCountry" runat="server"></asp:Label>
                                    </label>
                                    </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="Label21" runat="server" Text="Country"></asp:Label>
                                       
                                     </label>
                                      </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblSCountry" runat="server"></asp:Label>
                                    </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       <label>
                                        <asp:Label ID="Label18" runat="server" Text="State"></asp:Label>
                                       
                                     </label>
                                        </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblBState" runat="server"></asp:Label>
                                     </label> 
                                    </td>
                                    <td>
                                       <label>
                                        <asp:Label ID="Label19" runat="server" Text="State"></asp:Label>
                                       
                                     </label>
                                      </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblSState" runat="server" Font-Bold="False"></asp:Label>
                                    </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="City"></asp:Label>
                                       
                                     </label>
                                     </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="LblBCity" runat="server"></asp:Label>
                                    </label>
                                    </td>
                                    <td>
                                      <label>
                                        <asp:Label ID="Label17" runat="server" Text="City"></asp:Label>
                                       
                                     </label>
                                     </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="LblSCity" runat="server"></asp:Label>
                                    </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Phone"></asp:Label>
                                     
                                    </label>
                                     </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblBPhone" runat="server" Font-Bold="False"></asp:Label>
                                    </label>
                                    </td>
                                    <td>
                                      <label>
                                        <asp:Label ID="Label15" runat="server" Text="Phone"></asp:Label>
                                     
                                    </label>
                                      </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblSPhone" runat="server" ></asp:Label>
                                    </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text="Zip"></asp:Label>
                                     
                                    </label>
                                    </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblBZip" runat="server"></asp:Label>
                                    </label>
                                    </td>
                                    <td>
                                      <label>
                                        <asp:Label ID="Label13" runat="server" Text="Zip"></asp:Label>
                                     
                                    </label></td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblSZip" runat="server"></asp:Label>
                                    </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                      </td>
                                    <td>
                                     </td>
                                    <td>
                                        </td>
                                    <td>
                                      </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                    <label>
                                        <asp:Label ID="Label11" runat="server" Text="Payment Detail" Font-Size="Large"></asp:Label>
                                      
                                    </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                     <label>
                                        <asp:Label ID="Label10" runat="server" Text="Payment By"></asp:Label>
                                      
                                     </label>
                           
                                    </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblPayBy" runat="server"></asp:Label></td>
                                    </label>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <label>
                                        <asp:Label ID="Label9" runat="server" Text="Pyment Status"></asp:Label>
                                      
                                     </label>
                                     </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblPayStatus" runat="server"></asp:Label>
                                    </label>
                                    </td>
                                    <td>
                                      </td>
                                    <td>
                                       </td>
                                </tr>
                                <tr>
                                    <td>
                                     </td>
                                    <td>
                                       </td>
                                    <td>
                                      </td>
                                    <td>
                                       </td>
                                </tr>
                                <tr>
                                    <td>
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Item Details" Font-Size="Large"></asp:Label>
                                    
                                    </label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                           
                                            <Columns>
                                                <asp:BoundField DataField="Name" HeaderText="Item">
                                                   
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Qty" HeaderText="Qty">
                                                   
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Rate" HeaderText="Rate">
                                                  
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PromotionDiscountAmount" HeaderText="Promotion Discount">
                                                   
                                                </asp:BoundField>
                                                <asp:BoundField DataField="InventoryVolumeDiscountAmount" HeaderText="Volume Discount">
                                             
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackHandlingAmount" HeaderText="Handling Charges">
                                                   
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Amount" HeaderText="Total">
                                                  
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WarehouseName" HeaderText="Warehouse" />
                                            </Columns>
                             
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       </td>
                                    <td>
                                       </td>
                                    <td>
                                      </td>
                                    <td>
                                      </td>
                                </tr>
                                <tr >
                                    <td colspan="4">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Totals" Font-Size="Large"></asp:Label>                               
                                    </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                      </td>
                                    <td colspan="2">
                                     </td>
                                    <td>
                                      </td>
                                </tr>
                                <tr>
                                    <td>
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Sub Total"></asp:Label>                               
                                    </label>                      
                                </td>
                                <td>
                                <label>
                                    <asp:Label ID="lblSubTotal" runat="server"></asp:Label></td>
                                </label>
                                <td>
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Discount"></asp:Label>                               
                                </label>
                                   
                                </td>
                                <td>
                                <label>
                                    <asp:Label ID="lblCDisc" runat="server"></asp:Label>
                                </label>
                                </td>
                            </tr>
                                <tr>
                                    <td>
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Handling Charges"></asp:Label>
                                    </label>
                                    </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblOVHCharge" runat="server"></asp:Label>
                                    </label>
                                    </td>
                                    <td>
                                    <label>
                                    <asp:Label ID="Label5" runat="server" Text="Shipping Charges"></asp:Label>                                
                                    </label>
                                    </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblShipCharge" runat="server"></asp:Label>
                                    </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                     <label>
                                    <asp:Label ID="Label6" runat="server" Text="Tax"></asp:Label>                                
                                    </label>                               
                                    </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblTax" runat="server"></asp:Label></td>
                                     </label>
                                    <td>
                                     <label>
                                    <asp:Label ID="Label7" runat="server" Text="Totals"></asp:Label>                                
                                    </label>
                                    </td>
                                    <td>
                                    <label>
                                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                     </label>
                                     </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                    
                                        <asp:Button ID="ImageButton4" runat="server" CssClass="btnSubmit" Text="Back" OnClick="ImageButton4_Click" />
                                    </td>
                                </tr>
                        </table>
                    </asp:Panel>
                  </td>
              </tr>
          </table>
            
      
    
       </fieldset>
     </div>
   </ContentTemplate>
  </asp:UpdatePanel>
    
</asp:Content>
