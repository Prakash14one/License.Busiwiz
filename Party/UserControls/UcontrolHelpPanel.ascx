<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcontrolHelpPanel.ascx.cs" Inherits="ShoppingCart_Admin_UserControls_UC_Title" %>
<div id="right_content">
<asp:Panel runat="server" ID="PNLTITLE">
    <div class="divHeaderLeft">
        <div style="float: left; width: 50%;">
            <h2>
                <asp:Label ID="lbltitle" runat="server"   ></asp:Label>
                <asp:Image ID="Image2" runat="server" ImageUrl="~/ShoppingCart/images/HelpIconHHH.png" Visible="false" />
                </h2>
        </div>
        <div style="float: right; width:50%" >
          
        
                                  <asp:Panel ID="pnlshow" runat="server" Visible=""  >
            <%--    <span style="float: left; margin-right: 10px;">--%>
        
               <asp:Label ID="lblcay"   runat="server" ForeColor="White" Text="Current Accounting year" Visible="false" Font-Bold="True" >
               </asp:Label>
              
            
                
                <asp:Label ID="lblb" ForeColor="White"   runat="server" Text="Business " Font-Bold="True" >
                                 </asp:Label>
                                  <asp:DropDownList ID="ddlwarehouse" runat="server"  onselectedindexchanged="ddlbus_SelectedIndexChanged" AutoPostBack="true" Font-Size="10px" >
                                 </asp:DropDownList>
                                 
                <asp:Label ID="lblacdate" ForeColor="White" runat="server" Font-Bold="True" Text="year ending on "></asp:Label>
                <asp:Label ID="lblopenaccy" ForeColor="White" runat="server" Font-Bold="True"></asp:Label>
             
                                                           
                                
                                    
                                     
                                          
                                      
                                      &nbsp;
                                      <asp:HyperLink ID="lblop" runat="server" ForeColor="White" Font-Bold="True" Target="_blank" 
                                          Text="Change"></asp:HyperLink>
                                          
                                        
                                     
              <%--   </span> --%>
             

                </asp:Panel>
                
       
        </div>
    </div>
     </asp:Panel>
    <div style="clear: both;">
    </div>
    <asp:Panel runat="server" ID="pnlhelp" style="border:solid 1px black" >
    <h3>
        <asp:Label ID="lblDetail" runat="server"  Font-Bold="True" Font-Names="Tahoma" Font-Size="7pt" >
                </asp:Label>
                </h3>
                </asp:Panel>
</div>
