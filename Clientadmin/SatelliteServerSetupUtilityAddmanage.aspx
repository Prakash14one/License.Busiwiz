<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="SatelliteServerSetupUtilityAddmanage.aspx.cs" Inherits="IOffice_ShoppingCart_Admin_SatelliteServerSetupUtilityAddmanage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">




  <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
        <ContentTemplate>

        
            <table width="100%">
        <tr>

        <%-- <asp:Panel ID="Panel3" runat="server">
                        <fieldset>
                        <legend>Operating Systems Add Manage </legend>
--%>
            <td width="33.33%">
               
                
                </td>
                          <td width="33.33%"> 

                &nbsp;</td>
                           <td width="33.33%" align="right"> 

                             
                </td>
        </tr>
              



                <tr>
                     <td colspan="3" style="width: 66%"> 


                      <fieldset>
               
                   <legend>Satellite Server Setup Utility Add manage</legend>
                    <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                
                <div style="float: right;">
                    
                
                    <asp:Button ID="Button4" runat="server" Text="Add New" 
                        onclick="Button4_Click2" />

                </div>
                <div style="clear: both;">
                </div>



                        <asp:Panel ID="Panel3" runat="server" Visible="false">
      
                 <table width="100%">

      
        <tr>
                           <td width="33.33%"> 

                               <asp:Label ID="Label40" runat="server" Text=" Select Operating System Name"></asp:Label>
                           </td>
                           <td width="25%"> 

                           <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Width="180px"
                                   onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                                 </asp:DropDownList>
                           </td>
                             <td width="43.33%">

                &nbsp;</td>
        </tr>



           <tr>
                           <td width="33.33%"> 

                               <asp:Label ID="Label6" runat="server" Text=" Select Type"></asp:Label>
                           </td>
                           <td width="25%"> 

                           <asp:DropDownList ID="DropDownList1" runat="server" Width="180px">


                                <asp:ListItem Value="0" Text="32 bit"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="64 bit"></asp:ListItem>

                                 </asp:DropDownList>
                           </td>
                             <td width="43.33%">

                &nbsp;</td>
        </tr>



                <tr>
                                   <td width="33.33%"> 

                                       <asp:Label ID="Label1" runat="server" Text="Upload Setup Utility"></asp:Label>
                                       </td>
                                   <td Width="25%"> 
                                       <asp:FileUpload ID="FileUpload1" runat="server" />
                                       <asp:Button ID="Button1" runat="server" Text="upload" onclick="Button1_Click" 
                                           style="height: 26px" />

                                   </td>
                                    <td width="43.33%">

                                        <asp:Label ID="Label41" runat="server" ></asp:Label>
                                   </td>
                </tr>

                




                     <tr>
                                   <td width="33.33%"> 

                                      <asp:Label ID="Label7" runat="server" Text="Folder Name"></asp:Label>
                                       </td>
                                   <td width="25%"> 
                                         <asp:TextBox ID="TextBox5" runat="server" Width="180px"></asp:TextBox>

                                   </td>
                                    <td width="43.33%">

                        &nbsp;</td>
                </tr>



                <tr>
                                   <td width="33.33%"> 

                                       <asp:Label ID="Label2" runat="server" Text="FTPURL/IP"></asp:Label>
                       </td>
                                   <td width="25%"> 
                                       <asp:TextBox ID="TextBox1" runat="server" Width="180px"></asp:TextBox>

                       </td>
                                   <td width="43.33%"> 

                        &nbsp;</td>
                </tr>
                 
                    <tr>
                                   <td width="33.33%"> 

                                       <asp:Label ID="Label4" runat="server" Text="Port"></asp:Label>
                       </td>
                                   <td width="25%"> 
                                       <asp:TextBox ID="TextBox3" runat="server" Width="180px"></asp:TextBox>

                      </td>
                                   <td width="43.33%"> 

                       </td>
                </tr>
                     <tr>
                                   <td width="33.33%"> 

                                       <asp:Label ID="Label5" runat="server" Text="Userid"></asp:Label>
                    </td>
                                   <td width="25%"> 
                                       <asp:TextBox ID="TextBox4" runat="server" Width="180px"></asp:TextBox>

                       </td>
                                   <td width="43.33%"> 

                      </td>
                </tr>


                    <tr>
                                   <td width="33.33%"> 

                                       <asp:Label ID="Label3" runat="server" Text="Password"></asp:Label>
                        </td>
                                   <td width="25%"> 
                                       <asp:TextBox ID="TextBox2" runat="server" Width="180px"></asp:TextBox>

                       </td>
                                   <td width="43.33%"> 

                                       <asp:Button ID="Button10" runat="server" onclick="Button10_Click" 
                                           Text="Test FTP" />
                                       <asp:Label ID="lblmsg0" runat="server" ForeColor="Red" Text=""></asp:Label>
                                   </td>
                </tr>



                <tr>
                   <td width="33.33%"> 
                        &nbsp;</td>
                   <td width="25%" style="margin-left: 40px"> 
                        <asp:Button ID="Button9" runat="server" Text="Submit" onclick="Button9_Click" />

                       <asp:Button ID="Button2" runat="server" Text="Update" onclick="Button2_Click" Visible="false" />


                       <asp:Button ID="Button3" runat="server" Text="Cancel" onclick="Button3_Click" />

                    </td>
                    <td width="43.33%"> 
                        &nbsp;</td>
                </tr>
                </table>

                  </asp:Panel>
                  
                  </fieldset>
                  
                  </td>
                </tr>

                
                <tr>
                    <td width="33.33%"> 
                        &nbsp;</td>
                    <td width="33.33%"> 
                        &nbsp;</td>
                   <td width="33.33%"> 
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="width: 66%">


                        <asp:Panel ID="Panel2" runat="server">
                          <fieldset>
                        <legend>List of Satellite Server Setup Utility </legend>
              
                 <table width="100%">

        <tr>
                            <td width="33.33%"> 

                            </td>
                            <td width="33.33%"> 

                          
                            </td>
                             <td width="33.33%"> 

                &nbsp;</td>
        </tr>
        <tr>
                           <td colspan="3" style="width: 66%"> 




                           <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
                                        EmptyDataText="No Record Found." AllowPaging="True" Width="100%" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" onrowcommand="GridView1_RowCommand"
                                        >
                                        <Columns>
                                       
                                         
                                             <asp:TemplateField HeaderText="Operating System Name" SortExpression=" Operating System Name " ItemStyle-Width="25%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsectiona1" Visible="true" runat="server" Text='<%# Bind("Operatingsystemname")%>'></asp:Label>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("id")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>                                               
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="File Name " SortExpression=" Id" ItemStyle-Width="25%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemaildisplayname" runat="server" Text='<%# Bind("FileName")%>'></asp:Label>
                                                </ItemTemplate>                                               
                                            </asp:TemplateField>
                                            
                                            
                                            <asp:TemplateField HeaderText="FTPURL " SortExpression="FTPURL" ItemStyle-Width="25%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcountry" runat="server" Text='<%# Bind("FTPURL")%>'></asp:Label>
                                                 
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>
                                    
                                       
                                            <asp:TemplateField HeaderText="Port " SortExpression="Port" ItemStyle-Width="25%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcountry1" runat="server" Text='<%# Bind("Port")%>'></asp:Label>
                                                 
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>
                                    
                                               
                                            <asp:TemplateField HeaderText="Userid " SortExpression="Userid" ItemStyle-Width="25%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcountry2" runat="server" Text='<%# Bind("Userid")%>'></asp:Label>
                                                 
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>
                                    

                                       <asp:TemplateField HeaderText="Password " SortExpression="Password" ItemStyle-Width="25%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcountry4" runat="server" Text='<%# Bind("Password")%>'></asp:Label>
                                                 
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>
                                                                             
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" Visible="true"   >
                                                <ItemTemplate>
                                                  <asp:ImageButton ID="imgbtnedit" runat="server" 
                                                        ToolTip="Edit"  ImageUrl="~/Account/images/edit.gif" 
                                                        onclick="imgbtnedit_Click" />
                                                </ItemTemplate>                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" Visible="true">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtndelete" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                        ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>

                            
                           </td>
        </tr>
                <tr>
                                   <td width="33.33%"> 

                                  
                                   </td>
                                   <td width="33.33%"> 

                               

                                     
                                   </td>
                                    <td width="33.33%">

                        &nbsp;</td>
                </tr>
                <tr>
                                   <td width="33.33%"> 

                        &nbsp;</td>
                                   <td width="33.33%"> 

                        &nbsp;</td>
                                   <td width="33.33%"> 

                        &nbsp;</td>
                </tr>
                <tr>
                   <td width="33.33%"> 
                        &nbsp;</td>
                   <td width="33.33%"> 
                      
                    </td>
                    <td width="33.33%"> 
                        &nbsp;</td>
                </tr>
                </table>
                </fieldset>
                  </asp:Panel>









                       </td>
                </tr>
                <tr>
                    <td width="33.33%">
                        &nbsp;</td>
                    <td width="33.33%">
                        &nbsp;</td>
                    <td width="33.33%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="33.33%">
                        &nbsp;</td>
                    <td width="33.33%">
                        &nbsp;</td>
                    <td width="33.33%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="33.33%">
                        &nbsp;</td>
                    <td width="33.33%">
                        &nbsp;</td>
                    <td width="33.33%">
                        &nbsp;</td>
                </tr>
    </table>
        
                
        </ContentTemplate>
        <Triggers>
          
            <asp:PostBackTrigger ControlID="Button1" />
            <asp:PostBackTrigger ControlID="Button4" />
            
        </Triggers>

        </asp:UpdatePanel>
         
</asp:Content>

