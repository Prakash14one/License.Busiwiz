<%@ Page Language="C#"  MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="TransferImages.aspx.cs" Inherits="ShoppingCart_Admin_TransferImages" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div class="products_box">
        
          <div style="padding-left:1%">
                  <asp:Label ID="Label2" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                <br /> <asp:Label ID="lblmessage" runat="server" ForeColor="Red" Text="Alway image alllowed in (Productname_m_l.jpg) this formate."></asp:Label>
                      
                </div>
                <fieldset>
                
                    <legend>
                  
                      <asp:Label ID="Labelp5" runat="server" Font-Bold="true"  Text="Transfer Image to FTP Server"></asp:Label> 
                    </legend>
                       <div style="clear: both;">
                     
                </div>
                <table>
            <%--    <tr>
                <td>
                  <label>
                 <asp:Label ID="lblid1" Text="Enter FTP Name" runat="server"></asp:Label>
                          <asp:RequiredFieldValidator ID="rd1" runat="server" ValidationGroup="1" ErrorMessage="*"
                   Display="Dynamic" ControlToValidate="txtftp"></asp:RequiredFieldValidator>
                 
                </label>
                </td>
                <td>
                 <label>
                   <asp:TextBox ID="txtftp" runat= "server"></asp:TextBox>
                    </label>
                </td>
                </tr>--%>
                <tr>
                <td valign="top"> <label>
                 <asp:Label ID="Label1" Text="Enter FTP Name with Port No" runat="server"></asp:Label>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1" ErrorMessage="*"
                   Display="Dynamic" ControlToValidate="txtportNo"></asp:RequiredFieldValidator>
                </label>
                </td>
                <td> <label>
                 <asp:TextBox ID="txtportNo" runat= "server" ></asp:TextBox>
                               Ex:192.168.6.49:21
                </label></td>
                </tr>
                <tr>
                <td>
                  <label>
                 <asp:Label ID="lblusername" Text="Enter User Name" runat="server"></asp:Label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1" ErrorMessage="*"
                   Display="Dynamic" ControlToValidate="txtusername"></asp:RequiredFieldValidator>
         
                 </label>
                </td>
                 <td>
                   <label>
                 <asp:TextBox ID="txtusername" runat= "server"></asp:TextBox>
          </label>
                </td>
                </tr>
                <tr>
                <td>
                <label>
                  <asp:Label ID="Label3" Text="Enter Passward" runat="server"></asp:Label>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1" ErrorMessage="*"
                   Display="Dynamic" ControlToValidate="txtpassword"></asp:RequiredFieldValidator>
         
                </label>
                </td>
                  <td>
                  <label>
                   <asp:TextBox ID="txtpassword" runat= "server" TextMode="Password"></asp:TextBox>
                    </label></td>
                      
               
                </td>
                </tr>
                <tr>
                <td><label>
                  <asp:Label ID="Label4" Text="Enter Folder Name" runat="server"></asp:Label>
                         
                </label>
                </td>
                 <td>
                  <label>
                 <asp:TextBox ID="txtfoldername" runat= "server"></asp:TextBox>  </label></td>
                            
              
                </td>
                </tr>
                <tr>
               <td>  
               </td>  
                <td><label>
                    <asp:Button  id="Btnimages" Text="Transfer Image" runat="server" 
                                    onclick="Btnimages_Click" ValidationGroup="1" /> </label> <label>
                 <asp:Label ID="lblno" ForeColor="Red" runat="server" ></asp:Label>
                                <asp:Label ID="Label6" ForeColor="Red" runat="server" ></asp:Label>
                        
                </label>  <label>
                 <asp:Label ID="lblcountimages" runat="server" Visible="false"></asp:Label>
                            
                </label>
               </td>
                </tr>
                <tr>
                <td>
                
                </td>
                <td>
                  <asp:GridView ID="grd" runat="server" AutoGenerateColumns="false" 
                                    AllowPaging="true"  onpageindexchanging="grd_PageIndexChanging" 
                                    onselectedindexchanging="grd_SelectedIndexChanging" CssClass="GridBack">
                                
                                <Columns>
                                
                                <asp:BoundField DataField="Name" HeaderText="Product Name" />
                                </Columns>
                                <Columns>
                                <asp:BoundField DataField="ProductNo" HeaderText="Product No" />
                                </Columns>
                                
                                
                                </asp:GridView>
                </td>
                </tr>
                </table>
             
                </fieldset>
                </div>
 

</asp:Content>

