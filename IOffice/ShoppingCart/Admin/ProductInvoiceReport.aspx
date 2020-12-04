<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="ProductInvoiceReport.aspx.cs" Inherits="ProductInvoiceReport"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentPlaceHolder2" ContentPlaceHolderID="ContentPlaceHolder2"
    runat="Server">

    <script language="javascript" type="text/javascript">


        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1020,height=760,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');

            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }  


    </script>

   
    <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <asp:Panel ID="pnlgrid" runat="server">
                    <div id="mydiv" class="closed">
                    </div>
                    <table width="100%">
                        <tr>
                            <td align="center" colspan="5" border="0.5">
                                <asp:Label ID="lblHeading" runat="server" Font-Names="Tahoma" Font-Size="8pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <fieldset>
                        <legend></legend>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <label>
                                        Client</label>
                                    <label>
                                        <asp:Label ID="lblclientname" runat="server" Text=""></asp:Label>
                                    </label>
                                    <label>
                                        Project</label>
                                    <label>
                                        <asp:Label ID="lblproject" runat="server" Text=""></asp:Label>
                                    </label>
                                    <label>
                                        Reference No</label>
                                    <label>
                                        <asp:Label ID="lblrefno" runat="server" Text=""></asp:Label>
                                    </label>
                                     <label>
                                        Date</label>
                                    <label>
                                        <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                            <td>
                            <label>
                                <asp:Label ID="Label13" runat="server" Text="Bill To:"></asp:Label>
                            <br />
                           
                            <asp:Label ID="lblBillAddress" runat="server"></asp:Label>
                            </label>
                        </td>
                              <td>
                            <label>
                                <asp:Label ID="Label14" runat="server" Text="Ship To:"></asp:Label>
                           
                        <br />
                         
                            <asp:Label ID="lblShipAddress" runat="server" ></asp:Label>
                        
                        </td>
                            </tr>
                        </table>
                    </fieldset>
                      <asp:Panel ID="pnlmaterial" runat="server" Visible="false">
                    <fieldset>
                        <legend>1. Material Used </legend>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No Record Found." AllowPaging="True" PageSize="6" ShowFooter="True"
                                        DataKeyNames="Mid">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcate" runat="server" Text='<%#Bind("InventoryCatName") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Category" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcates" runat="server" Text='<%#Bind("InventorySubCatName") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Sub Category" HeaderStyle-HorizontalAlign="Left"
                                                Visible="false" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcatess" runat="server" Text='<%#Bind("InventorySubSubName") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product No." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpno" runat="server" Text='<%#Bind("ProductNo") %>'></asp:Label>
                                                      <asp:Label ID="lblserviceId" runat="server" Text='<%#Bind("InventoryWarehouseMasterId") %>'  Visible="false"></asp:Label></ItemTemplate>
                                                     
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpname" runat="server" Text='<%#Bind("Name") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Used<br> Units" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblunit" runat="server" Text='<%#Bind("Qty") %>'></asp:Label><asp:Label
                                                        ID="lblpcid" runat="server" Text='<%#Bind("pcid") %>' Visible="false"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cost" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcost" runat="server" Text='<%#Bind("Rate") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="List Sales Rate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false">
                                            <ItemTemplate>
                                           <asp:Label ID="lblsalesrateor" runat="server" Width="50px" MaxLength="10" Text='<%#Bind("OriginalRate") %>'></asp:Label>
                                                   
                                         </ItemTemplate>  
                                         </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Applied Sales Rate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsalesrate" runat="server" Width="50px" MaxLength="10" Text='<%#Bind("SalesRate") %>'></asp:Label></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgt" runat="server" Font-Bold="true" Text="Grand Total"></asp:Label></FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Cost" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcharges" runat="server" Text='<%#Bind("Totalcost") %>'></asp:Label></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotalfooter" runat="server" Font-Bold="true"></asp:Label></FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total To be<br> Charge" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblchargestobe" runat="server" Text='<%#Bind("Totaltobechange") %>'></asp:Label></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotaltobefooter" runat="server" Font-Bold="true"></asp:Label></FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Margin" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmargin" runat="server" Text='<%#Bind("Margin") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Margin(%)" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmarginper" runat="server" Text='<%#Bind("Marginper") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
                      <asp:Panel ID="pnllabour" runat="server" Visible="false">
                    <fieldset>
                        <legend>2. Labour Used </legend>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No Record Found." ShowFooter="True" DataKeyNames="Id">
                                        <Columns>
                                         <asp:TemplateField HeaderText="Service" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                             <ItemTemplate>
                                           <asp:Label ID="lblservice" runat="server" ></asp:Label>
                                             <asp:Label ID="lblserviceId" runat="server" Visible="false"></asp:Label>
                                         </ItemTemplate>  
                                          </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Id" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="labourId" runat="server" Text='<%#Bind("labourId") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lvlnote" runat="server" Text='<%#Bind("Note") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldate" runat="server" Text='<%#Bind("Date") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfromtime" runat="server" Text='<%#Bind("FromTime") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEnddate" runat="server" Text='<%#Bind("Enddate") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left"
                                                Visible="false" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemp" runat="server" Text='<%#Bind("Employee") %>'></asp:Label><asp:Label
                                                        ID="lblempid" runat="server" Text='<%#Bind("EmployeeId") %>' Visible="false"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hours Worked" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblhor" runat="server" Text='<%#Bind("Hrs") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Rate" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemprare" runat="server" Text='<%#Bind("Rate") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="List Sales Rate" HeaderStyle-HorizontalAlign="Left" Visible="false" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                           <asp:Label ID="lblsalesrateor" runat="server" Width="50px" MaxLength="10" Text='<%#Bind("OriginalRate") %>'></asp:Label>
                                                   
                                         </ItemTemplate>  
                                         </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Applied Sales Rate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsalesrate" runat="server" Width="50px" MaxLength="10" Text='<%#Bind("SalesRate") %>'></asp:Label></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgt" runat="server" Font-Bold="true" Text="Grand Total"></asp:Label></FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Cost" HeaderStyle-HorizontalAlign="Left" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcharges" runat="server" Text='<%#Bind("Totalcost") %>'></asp:Label></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotalfooter" runat="server" Font-Bold="true"></asp:Label></FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total To be<br> Charge" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblchargestobe" runat="server" Text='<%#Bind("Totaltobechange") %>'></asp:Label></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotaltobefooter" runat="server" Font-Bold="true"></asp:Label></FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Margin" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmargin" runat="server" Text='<%#Bind("Margin") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Margin(%)" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmarginper" runat="server" Text='<%#Bind("Marginper") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                       
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
                    <asp:Panel ID="pnlothecharge" runat="server" Visible="false">
                    <fieldset>
                        <legend>3. Other Charges </legend>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="GridView3" runat="server" Width="100%" AutoGenerateColumns="False"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No Record Found." AllowPaging="True" DataKeyNames="Id" ShowFooter="True">
                                        <Columns>
                                          <asp:TemplateField HeaderText="Service" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Left">
                                             <ItemTemplate>
                                           <asp:Label ID="lblservice" runat="server" Text='<%#Bind("category") %>'></asp:Label>
                                             <asp:Label ID="lblserviceId" runat="server" Text='<%#Bind("InvwId") %>' Visible="false"></asp:Label>
                                         </ItemTemplate>  
                                          
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Note" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50%"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgnote" runat="server" Text='<%#Bind("Note") %>'></asp:Label></ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgqty" runat="server" Text='<%#Bind("Qty") %>'></asp:Label><asp:Label
                                                        ID="lblId" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label></ItemTemplate>
                                               
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="List Sales Rate" HeaderStyle-HorizontalAlign="Left" Visible="false" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                           <asp:Label ID="lblsalesrateor" runat="server"   Text='<%#Bind("OriginalRate") %>'></asp:Label>
                                                   
                                         </ItemTemplate>  
                                         </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Applied Sales Rate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="7%"
                                                ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrate" runat="server" Text='<%#Bind("Rate") %>'></asp:Label></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblttt" runat="server" Font-Bold="true" Text="Grand Total"></asp:Label></FooterTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="8%"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgtotal" runat="server" Text='<%#Bind("Total") %>'></asp:Label><asp:Label
                                                        ID="lbljid" runat="server" Text='<%#Bind("jobid") %>' Visible="false"></asp:Label></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotaltobefooter" runat="server" Font-Bold="true"></asp:Label></FooterTemplate>
                                                
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
                    <fieldset>
                        <legend>Net Amount </legend>
                        <table width="100%">
                        
                             <tr>
                             <td></td>
                                <td align="right" >
                               
                                    <asp:Label ID="Label2" runat="server" Text="Gross Total"></asp:Label>
                                   </td>
                                   
                                     <td align="right">
                                    <asp:Label ID="lblGross" runat="server"></asp:Label>
                                   
                                </td>
                            </tr>
                              <tr>
                             <td align="right" colspan="1"  style="width:80%;">
                                <asp:Label ID="lblcusdisname" runat="server"></asp:Label>
                                  </td> 
                                 <td></td>
                                <td align="right" >
                                
                             </td>
                            </tr>
                              <tr>
                             <td align="right" colspan="1"  style="width:80%;">
                              
                                  </td> 
                                <td align="right" valign="bottom">
                                  Customer Discount
                                 </td>
                                   
                                     <td align="right">
                                    <asp:Label ID="lblCustDisc" runat="server">0.00</asp:Label>
                             </td>
                            </tr>
                              <tr>
                             <td align="right" colspan="1"  style="width:80%;">
                                <asp:Label ID="lblorderdiscname" runat="server"></asp:Label>
                                  </td> 
                                  <td>
                                  </td>
                                <td align="right" valign="bottom">
                                
                             </td>
                            </tr>
                              <tr>
                             <td align="right" colspan="1"  style="width:80%;">
                              
                                  </td> 
                                <td align="right" valign="bottom">
                                   Order Value Discount
                                 </td>
                                   
                                     <td align="right">
                                    <asp:Label ID="lblOrderDisc" runat="server">0.00</asp:Label>
                             </td>
                            </tr>
                            <tr>
                             <td align="right" colspan="1"  style="width:80%;">
                                 <asp:Panel ID="pnltax1" runat="server" Visible="false">
                                    <asp:Label ID="lbltax1text" runat="server" Text=""></asp:Label>
                                    <label>
                                     <asp:Label ID="lblt1acc" runat="server" Text="" Visible="false"></asp:Label>
                                    </label>
                                    <asp:Label ID="lbltax1amt" runat="server" Text=""></asp:Label>
                                    </asp:Panel>
                                     <asp:Panel ID="pnltax2" runat="server" Visible="false">
                                    <asp:Label ID="lbltax2text" runat="server" Text=""></asp:Label>
                                    <label>
                                      <asp:Label ID="lblt2acc" runat="server" Text="" Visible="false"></asp:Label>
                                    </label>
                                    <asp:Label ID="lbltax2amt" runat="server" Text=""></asp:Label>
                                    </asp:Panel>
                                      <asp:Panel ID="pnltax3" runat="server" Visible="false">
                                    <asp:Label ID="lbltax3text" runat="server" Text=""></asp:Label>
                                    <label>
                                     <asp:Label ID="lblt3acc" runat="server" Text="" Visible="false"></asp:Label>
                                    </label>
                                    <asp:Label ID="lbltax3amt" runat="server" Text=""></asp:Label>
                                    </asp:Panel> 
                                  </td> 
                                <td align="right" >
                                     <asp:Label ID="Label3" runat="server" Text="Total Tax Amount "></asp:Label>
                                     
                                      </td>
                                   
                                     <td align="right">
                                     <asp:Label ID="lbltotaltaxamt" runat="server" Text="0"></asp:Label>
                             </td>
                            </tr>
                            
                            
                       
                            <tr>
                                <td >
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label1" runat="server" Text="Net Total"></asp:Label>
                                    </td>
                                   
                                     <td align="right">
                                    <asp:Label ID="lblnetamt" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
                <fieldset>
                    <legend></legend>
                    <table width="100%">
                        <tr>
                            <td colspan="2" style="height: 66px">
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Send this invoice by email to customer"
                                    TextAlign="Right" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="Button2" runat="server" Text="Confirm" CssClass="btnSubmit" OnClick="Button2_Click" />
                                <asp:Button ID="Button1" runat="server" Text="Apply" CssClass="btnSubmit" OnClick="Button1_Click"
                                    Enabled="False" />
                                <input id="Button4" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    class="btnSubmit" type="button" name="Print" value="Print" />
                                <asp:Button ID="Button3" runat="server" Text="Change/Go Back" CssClass="btnSubmit"
                                    OnClick="Button3_Click" />
                                &nbsp;
                                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
