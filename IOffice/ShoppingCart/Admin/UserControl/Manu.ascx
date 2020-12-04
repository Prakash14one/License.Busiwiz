<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Manu.ascx.cs" Inherits="ShoppingCart_Admin_UserControl_Manu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
&nbsp;<cc1:Accordion ID="Accordion1" runat="server"
            FadeTransitions="true"
              SelectedIndex="3"
            TransitionDuration="300">
            <Panes>
            <cc1:AccordionPane ID="AccordionPane1"  runat="server">
            <Header>
                <asp:Image ID="Image1" runat="server" AlternateText="Master"  ImageUrl="~/images1/sendmsg.jpg" /> </Header>
            <Content>
                <table>
                    
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/ShoppingCart/Admin/FaqCategoryMaster.aspx" >Faq Category</asp:LinkButton>
                            
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                        <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/ShoppingCart/Admin/CountryMaster.aspx"  >Country Master</asp:LinkButton>
                           
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton3" PostBackUrl="~/ShoppingCart/Admin/StateMaster.aspx" runat="server">State Master</asp:LinkButton>
                        
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/ShoppingCart/Admin/AddressTypeMaster.aspx">Address Type</asp:LinkButton>
                        
                        </td>
                        
                    </tr>
                   
                </table>
            </Content>
            
            </cc1:AccordionPane>
            <cc1:AccordionPane ID="AccordionPane2" runat="server">
            <Header> <asp:Image ID="Image2" runat="server" AlternateText="Customer" ImageUrl="~/images1/asendmsg.jpg" /></Header>
            <Content>
             <table>
                    
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl="~/ShoppingCart/Admin/FaqCategoryMaster.aspx">New Customer</asp:LinkButton>
                            
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton6" runat="server" PostBackUrl="~/ShoppingCart/Admin/Memberprofile.aspx">Customer Profile</asp:LinkButton>
                            
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton7" runat="server" PostBackUrl="~/ShoppingCart/Admin/PartyTypeCategoryMasterTbl.aspx" >Customer Type Category</asp:LinkButton>
                        
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton8" runat="server" PostBackUrl="~/ShoppingCart/Admin/FaqCategoryMaster.aspx">Apply Customer Type</asp:LinkButton>
                        
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton9" runat="server">Send Mails</asp:LinkButton>
                        
                        </td>
                        
                    </tr>
                  
                </table>
            
            </Content>
            </cc1:AccordionPane>
             <cc1:AccordionPane ID="AccordionPane3" runat="server">
            <Header> <asp:Image ID="Image3" runat="server" AlternateText="Inventory" ImageUrl="~/images1/asendmsg.jpg" /></Header>
            <Content>
             <table>
                    
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton10" runat="server">Inventory Main Category</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton11" runat="server">Inventory  Sub Category</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton12" runat="server">Inventory Sub Sub Category</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton13" runat="server">Inventory Site Master</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton14" runat="server">Inventory Location Master</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton15" runat="server">Inventory Room Master</asp:LinkButton>
                        </td>
                       
                    </tr>
                     <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton16" runat="server">Inventory Rack Master</asp:LinkButton>
                        </td>
                       
                    </tr>
                     <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton17" runat="server">Inventory Position Master</asp:LinkButton>
                        </td>
                       
                    </tr>
                     <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton18" runat="server">Inventory Count</asp:LinkButton>
                        </td>
                       
                    </tr>
                     <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton19" runat="server">Inventory Adjust</asp:LinkButton>
                        </td>
                       
                    </tr>
                     <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton20" runat="server">Find Inventory Location</asp:LinkButton>
                        </td>
                       
                    </tr>
                     <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton21" runat="server">List Inventory Location</asp:LinkButton>
                        </td>
                       
                    </tr>
                     <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton22" runat="server">Inventory Image master</asp:LinkButton>
                        </td>
                       
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton23" runat="server">Stock Adjus</asp:LinkButton>
                        </td>
                       
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton24" runat="server">Manage Inventory</asp:LinkButton>
                        </td>
                       
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton25" runat="server">Inventory Media Master</asp:LinkButton>
                        </td>
                       
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton26" runat="server">Update Inventory Master</asp:LinkButton>
                        </td>
                       
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton27" runat="server">Active Deactive Inventory</asp:LinkButton>
                        </td>
                       
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton28" runat="server">Update Inventory Image</asp:LinkButton>
                        </td>
                       
                    </tr>
                     <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton29" runat="server">Inventory Statement</asp:LinkButton>
                        </td>
                       
                    </tr>
                     
                </table>
            
            </Content>
            </cc1:AccordionPane>
             <cc1:AccordionPane ID="AccordionPane4" runat="server">
            <Header> <asp:Image ID="Image4" runat="server" AlternateText="Sales Management" ImageUrl="~/images1/asendmsg.jpg" /></Header>
            <Content>
             <table>
                    
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton30" runat="server">All Sales Order</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton31" runat="server">Sales Order By Customer</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton32" runat="server">Purchase Order</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton33" runat="server">Edit Purchase Invoice</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton34" runat="server">Sales Delivery Cahllan</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton35" runat="server">Edit Delivety Challan</asp:LinkButton>
                        </td>
                       
                    </tr>
                </table>
            
            </Content>
            </cc1:AccordionPane>
             <cc1:AccordionPane ID="AccordionPane5" runat="server">
            <Header> <asp:Image ID="Image5" runat="server" AlternateText="Sales Price" ImageUrl="~/images1/asendmsg.jpg" /></Header>
            <Content>
             <table>
                    
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton36" runat="server">Inventory Volume Discount</asp:LinkButton>
                            
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton37" runat="server">Apply Volume Discount</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton38" runat="server">Order Value Discount</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton39" runat="server">Packing Handling Charges</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton40" runat="server">Promotion Discount</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton41" runat="server">Apply Promotion Discount</asp:LinkButton>
                        </td>
                       
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton42" runat="server">Sales Rate Determination</asp:LinkButton>
                        </td>
                       
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton43" runat="server">Purchase Rate History</asp:LinkButton>
                        </td>
                       
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton44" runat="server">Inventory Price List</asp:LinkButton>
                        </td>
                       
                    </tr>
                     <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton45" runat="server">Price Determination</asp:LinkButton>
                        </td>
                       
                    </tr>
                </table>
            
            </Content>
            </cc1:AccordionPane>
             <cc1:AccordionPane ID="AccordionPane6" runat="server">
            <Header> <asp:Image ID="Image6" runat="server" AlternateText="Content Management" ImageUrl="~/images1/asendmsg.jpg" /></Header>
            <Content>
             <table>
                    
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton46" runat="server">Faq</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton47" runat="server">Manu Url Master</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton48" runat="server">Web Content Entry</asp:LinkButton>
                        </td>
                        
                    </tr>
                    
                </table>
            
            </Content>
            </cc1:AccordionPane>
             <cc1:AccordionPane ID="AccordionPane7" runat="server">
            <Header> <asp:Image ID="Image7" runat="server" AlternateText="Configuration" ImageUrl="~/images1/asendmsg.jpg" /></Header>
            <Content>
             <table>
                    
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton49" runat="server">Order Qty Restriction</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton50" runat="server">Apply Order Qty Restriction</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton51" runat="server">Company Website</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton52" runat="server">Company Website Address</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton53" runat="server">Email Type</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton54" runat="server">Email Content</asp:LinkButton>
                        </td>
                       
                    </tr>
                </table>
            
            </Content>
            </cc1:AccordionPane>
                <cc1:AccordionPane ID="AccordionPane8" runat="server">
            <Header> <asp:Image ID="Image8" runat="server" AlternateText="Shipping & Payments" ImageUrl="~/images1/asendmsg.jpg" /></Header>
            <Content>
             <table>
                    
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton55" runat="server">Shipping Master</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton56" runat="server">Free Shipping Master</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton57" runat="server">Free Shipping Rule</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton58" runat="server">Shipping Manages</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton59" runat="server">Shipping Methods</asp:LinkButton>
                        </td>
                        
                    </tr>
                  
                </table>
            
            </Content>
            </cc1:AccordionPane>
                  <cc1:AccordionPane ID="AccordionPane9" runat="server">
            <Header> <asp:Image ID="Image9" runat="server" AlternateText="Messages" ImageUrl="~/images1/asendmsg.jpg" /></Header>
            <Content>
             <table>
                    
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton60" runat="server">Compose</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton61" runat="server">Inbox</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton62" runat="server">Sent</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton63" PostBackUrl="~/ShoppingCart/Admin/test1.aspx" runat="server">Drafts</asp:LinkButton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton64" PostBackUrl="~/ShoppingCart/Admin/test2.aspx" runat="server">Trash</asp:LinkButton>
                        </td>
                        
                    </tr>
                  
                </table>
            
            </Content>
            </cc1:AccordionPane>
                  <cc1:AccordionPane ID="AccordionPane10" runat="server">
            <Header> <asp:Image ID="Image10" runat="server" AlternateText="Reports" ImageUrl="~/images1/asendmsg.jpg" /></Header>
            <Content>
         
            
            </Content>
            </cc1:AccordionPane>
            </Panes>
            
            </cc1:Accordion>
