<%@ Page Language="C#"  MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="xcelltransferdata.aspx.cs" Inherits="ShoppingCart_Admin_xcelltransferdata" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">





    <asp:UpdatePanel ID="uppanel" runat="server" >
        <ContentTemplate>
        
            <div class="products_box">
        
          <div style="padding-left:1%">
                   <asp:Label ID="Label2" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                  
                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                        <asp:Label ID="lblbarcode" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                      
                </div>
                <fieldset>
                
                    <legend>
                  
                      <asp:Label ID="Labelp5" runat="server" Font-Bold="true"  Text="Transfer Data From Excel to Inventory Master"></asp:Label> 
                    </legend>
                       <div style="clear: both;">
                     
                </div>
                <asp:Panel ID="pnldis" runat="server">
                <table>
                 <tr>
                <td colspan="2">
                  <label>
                 <asp:Label ID="Label26" runat="server"   Text="Please click here before proceeding with data upload. "></asp:Label> 
                 
                </label>
                </td>
                <td>
                  <label>
                 <asp:Button ID="btlnote" runat="server" Text="Note" CssClass="btnSubmit"
                                    onclick="btlnote_Click" />
            </label>
                </td>
                </tr>
                <tr>
                <td>
                  <label>
                 <asp:Label ID="Label3" runat="server"   Text="Select File :"></asp:Label> 
                 
                </label>
                </td>
                <td>
                  
                 <asp:FileUpload ID="File12" runat="server" />
            
                </td>
                <td>
                <label>
                <asp:Button ID="btnfileupload" runat="server" onclick="btnfileupload_Click" 
                                    Text="Upload File" CssClass="btnSubmit" />
                        </label>
               
                </td>
                <td> 
                  
               </td>
                </tr>
                <tr>
                <td>  <label>
                   <asp:Label ID="Label4" runat="server"   Text="Select Sheet :"></asp:Label> 
              
                </label>
                </td>
                <td>
                 <label>
                  <asp:DropDownList ID="ddlSheets" runat="server">
                                        </asp:DropDownList>
                </label>
                </td>
                <td>    <label> <asp:Button ID="bnt123" Text="Transfer Data" runat="server" CssClass="btnSubmit" onclick="bnt123_Click"  />
                          </label>
                         </td>
                </tr>
                
               
                </table>
                </asp:Panel>
                <table width="100%">
                 <tr>
                <td colspan="4">
             
                                  <asp:Panel ID="pnlsucedata" runat="server" Visible="false">
                                   <table  style="width:100%;">
                                     <tr>
                                   <td colspan="2">
                                   <label>
                                   <asp:Label ID="lblvvv" runat="server" Text="You have successfully uploaded "></asp:Label>
                                   <asp:Label ID="lblsucmsg" runat="server" Text=""></asp:Label>
                                     <%--<asp:Label ID="Label5" runat="server" Text=" file."></asp:Label>--%>
                                   </label>
                                   </td>
                                   </tr>
                                   <tr>
                                    <td colspan="2">
                                    <label>
                                     <asp:Label ID="lblnoofrecord" runat="server" Text=""></asp:Label>
                                   <asp:Label ID="Label24" runat="server" Text=" records have been added to your opening balance. "></asp:Label>
                                   </label>
                                   <label>
                                  </label>
                                   </td>
                                   </tr>
                                     <tr>
                                    <td colspan="2">
                                    <label>
                                     <asp:Label ID="lblnotimport" runat="server" Text=""></asp:Label>
                                   <asp:Label ID="Label25" runat="server" Text=" records had errors in importing. "></asp:Label>
                                   </label>
                                   <label>
                                     <asp:Button ID="Button2" Text="View the List of Records with Errors" 
                                            runat="server" CssClass="btnSubmit" onclick="Button2_Click" Visible="False"
                                           /></label>
                                   </td>
                                   </tr>
                                   <tr>
                                   <td colspan="2">
                                   <asp:Panel ID="pnlgrd" runat="server" Visible="false">
                                    <asp:GridView ID="grderrorlist" runat="server"  AllowSorting="True" 
                                           AutoGenerateColumns="False" GridLines="Both" AllowPaging="True" CssClass="mGrid"
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                                    DataKeyNames="No"
                                  EmptyDataText="No Record Found." 
                                           onpageindexchanging="grderrorlist_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row No." Visible="true"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server"  Text='<%# Bind("No") %>'></asp:Label>
                                            </ItemTemplate>
                                          
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                          
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbusness" runat="server" Text='<%# Bind("Business") %>'></asp:Label>
                                                     </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Name"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprodname" runat="server" Text='<%# Bind("ProductName") %>'></asp:Label>
                                                          </ItemTemplate>
                                          
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                          
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Datestart"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="l1" runat="server" Text='<%# Bind("Datestart") %>'></asp:Label>
                                            </ItemTemplate>
                                        
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Product No."  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpno" runat="server" Text='<%# Bind("ProductNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Barcode"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="barn" runat="server" Text='<%# Bind("Barcode") %>'></asp:Label>
                                            </ItemTemplate>
                                        
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Sub CatName"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblssname" runat="server" Text='<%# Bind("SubSubCatName") %>'></asp:Label>
                                            </ItemTemplate>
                                        
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                            </ItemTemplate>
                                        
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblst" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Weight"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblwight" runat="server" Text='<%# Bind("Weight") %>'></asp:Label>
                                            </ItemTemplate>
                                        
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                        
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Unit"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("Unit") %>'></asp:Label>
                                            </ItemTemplate>
                                        
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                        
                                        </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Rate"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRate" runat="server" Text='<%# Bind("Rate") %>'></asp:Label>
                                            </ItemTemplate>
                                        
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                        
                                        </asp:TemplateField>
                                    </Columns>                                  
                                    
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    
                                </asp:GridView>
                                </asp:Panel>
                                   </td>
                                   </tr>
                                      <tr>
                                   <td colspan="2" align="center">
                                      <asp:Button ID="Button1" Text="Close" runat="server" 
                                            CssClass="btnSubmit" onclick="Button1_Click"
                                           />
                                   </td>
                                   </tr>
                                   </table>
                                 </asp:Panel>
                                 
                </td>
                </tr>
                </table>
              
             
                   
              
        
             
                
              
               
               
                           
                    </fieldset>
                  
                    </div>
                         <asp:Panel ID="Paneldoc" runat="server" CssClass="modalPopup" Width="70%">
                
                                             <fieldset>
                                             <legend> Rules for a valid excel 
                                                                            file for data upload</legend>
                                                   
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                               
                                                <tr>
                                                    <td >
                                                                   <label>         1) Please ensure that you are using a Microsoft excel version from 2003-2010.
                                                                   </label>     </td>
                                                </tr>
                                                <tr>
                                                    <td>  <label>
                                                                           2) In order to successfully transfer your data, you must have the columns in your spread sheet appear exactly as they do below.<br /> If they are not the same, the data will not transfer properly.	
                                                         </label>

                                                                        </td>
                                                </tr>
                                                 <tr>
                                                    <td><label>
                                                                       3) The date should be listed as MM/DD/YYYY
                                                                       </label> </td>
                                                </tr>
                                                 <tr>
                                                    <td><label>
                                                                       4) Under status, if your inventory is active, it must be listed as True, and if the inventory is inactive, it should be listed as False.
                                                                      </label>  </td>
                                                </tr>
                                                 <tr>
                                               <td><br /></td>
                                                 </tr>
                                                <tr>
                                                
                                                    <td  colspan="2">
                                                        <table>
                                                            <tr>
                                                             <td style="height: 28px">
                                                                <asp:Label ID="Label22" runat="server" ForeColor="Red" Text="Business">
                                                                </asp:Label>
                                                                         </td>
                                                                <td style="height: 28px">
                                                                <asp:Label ID="lblpname" runat="server" ForeColor="Red" Text="ProductName">
                                                                </asp:Label>
                                                                         </td>
                                                                         <td style="height: 28px">
                                                                           <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="Datestart"></asp:Label>
                                                            
                                                                         </td>
                                                                         <td style="height: 28px">
                                                                          <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="ProductNo"></asp:Label>
                                                            
                                                                         </td>
                                                                          <td style="height: 28px">
                                                                          <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="Barcode"></asp:Label>
                                                            
                                                                         </td>
                                                                          <td style="height: 28px">
                                                                          <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="SubSubCatName"></asp:Label>
                                                            
                                                                         </td>
                                                                          <td style="height: 28px">
                                                                          <asp:Label ID="Label9" runat="server" ForeColor="Red" Text="Description"></asp:Label>
                                                            
                                                                         </td>
                                                                           <td style="height: 28px">
                                                                          <asp:Label ID="Label10" runat="server" ForeColor="Red" Text="Status"></asp:Label>
                                                            
                                                                         </td>
                                                                          <td style="height: 28px">
                                                                          <asp:Label ID="Label11" runat="server" ForeColor="Red" Text="Weight"></asp:Label>
                                                            
                                                                         </td>
                                                                          <td style="height: 28px">
                                                                          <asp:Label ID="Label12" runat="server" ForeColor="Red" Text="Unit"></asp:Label>
                                                            
                                                                         </td>
                                                                           <td style="height: 28px">
                                                                          <asp:Label ID="Label27" runat="server" ForeColor="Red" Text="Rate"></asp:Label>
                                                            
                                                                         </td>
                                                      
                                                            </tr>
                                                            <tr>
                                                            <td colspan="6"></td>
                                                            </tr>
                                                             <tr>
                                                               <td>
                                                                <asp:Label ID="Label23" runat="server" ForeColor="Red" Text="Eplaza">
                                                                </asp:Label>
                                                                         </td>
                                                                <td>
                                                                <asp:Label ID="Label13" runat="server" ForeColor="Red" Text="ABC">
                                                                </asp:Label>
                                                                         </td>
                                                                         <td>
                                                                           <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="MM/DD/YYYY"></asp:Label>
                                                            
                                                                         </td>
                                                                         <td>
                                                                          <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="123456"></asp:Label>
                                                            
                                                                         </td>
                                                                          <td>
                                                                          <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="67555"></asp:Label>
                                                            
                                                                         </td>
                                                                          <td>
                                                                          <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="Master ABC"></asp:Label>
                                                            
                                                                         </td>
                                                                          <td>
                                                                          <asp:Label ID="Label18" runat="server" ForeColor="Red" Text="Add this"></asp:Label>
                                                            
                                                                         </td>
                                                                           <td>
                                                                          <asp:Label ID="Label19" runat="server" ForeColor="Red" Text="True"></asp:Label>
                                                            
                                                                         </td>
                                                                          <td>
                                                                          <asp:Label ID="Label20" runat="server" ForeColor="Red" Text="5"></asp:Label>
                                                            
                                                                         </td>
                                                                          <td>
                                                                          <asp:Label ID="Label21" runat="server" ForeColor="Red" Text="gram"></asp:Label>
                                                            
                                                                         </td>
                                                                          <td>
                                                                          <asp:Label ID="Label28" runat="server" ForeColor="Red" Text="2.00"></asp:Label>
                                                            
                                                                         </td>
                                                      
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                  <tr>
                                                 <td align="center"  colspan="2">
                                                 <br />
                                                 </td>
                                                 </tr>
                                                <tr>
                                                 <td align="center"  colspan="2">
                                                 <asp:Button ID="btn" runat="server" Text="Cancel" />
                                                 </td>
                                                </tr>
                                            </table>
                                              </fieldset> 
                                        </asp:Panel>
                                        <asp:Button ID="Button4" runat="server" Style="display: none" />
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
                                            BackgroundCssClass="modalBackground" PopupControlID="Paneldoc" CancelControlID="btn"
                                            TargetControlID="Button4" >
                                        </cc1:ModalPopupExtender>
          
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnfileupload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

