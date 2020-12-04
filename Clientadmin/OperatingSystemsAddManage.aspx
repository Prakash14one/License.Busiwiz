<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="OperatingSystemsAddManage.aspx.cs" Inherits="IOffice_ShoppingCart_Admin_OperatingSystemsAddManage" %>

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
               
                   <legend>Operating Systems Add Manage </legend>
                    <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                
                <div style="float: right;">
                   <asp:Button ID="Button4" runat="server" Text="Add new" 
                                   onclick="Button1_Click" />
                </div>
               



                        <asp:Panel ID="Panel1" runat="server" Visible="false">
             
                 <table width="100%">

        <tr>
                            <td width="33.33%"> 

                                <asp:Label ID="Label39" runat="server" Text="Id"></asp:Label>
                            </td>
                            <td width="33.33%"> 

                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                            </td>
                             <td width="33.33%"> 

                &nbsp;</td>
        </tr>
        <tr>
                           <td width="33.33%"> 

                               <asp:Label ID="Label40" runat="server" Text="Operating System Name"></asp:Label>
                           </td>
                           <td width="33.33%"> 

                               <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                           </td>
                             <td width="33.33%">

                &nbsp;</td>
        </tr>
                <tr>
                                   <td width="33.33%"> 

                                       <asp:Label ID="Label41" runat="server" Text="Type"></asp:Label>
                                   </td>
                                   <td width="33.33%"> 

                                       <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                                           Width="204px">

                                         <asp:ListItem Value="0" Text="32 bit"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="64 bit"></asp:ListItem>

                                       </asp:DropDownList>
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
                        <asp:Button ID="Button9" runat="server" Text="Submit" onclick="Button9_Click" />

                       <asp:Button ID="Button2" runat="server" Text="Update" onclick="Button2_Click" Visible="false" />


                       <asp:Button ID="Button3" runat="server" Text="Cancel" onclick="Button3_Click" />

                    </td>
                    <td width="33.33%"> 
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
                        <legend>List Of Operating Systems</legend>
              
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




                           <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="Operatingid"
                                        EmptyDataText="No Record Found." AllowPaging="True" Width="100%" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" onrowcommand="GridView1_RowCommand"
                                        >
                                        <Columns>
                                         <asp:TemplateField HeaderText="Id " SortExpression=" Id" ItemStyle-Width="10 %"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemaildisplayname" runat="server" Text='<%# Bind("Id")%>'></asp:Label>
                                                     <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Bind("Operatingid")%>'></asp:Label>
                                                </ItemTemplate>                                               
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="Operating System Name" SortExpression=" Operating System Name " ItemStyle-Width="50%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsectiona1" Visible="true" runat="server" Text='<%# Bind("Operatingsystemname")%>'></asp:Label>
                                                    
                                                </ItemTemplate>                                               
                                                 <HeaderStyle HorizontalAlign="Left" />
                                                 <ItemStyle Width="50%" />
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Type " SortExpression="Type" ItemStyle-Width="25%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcountry" runat="server" Text='<%# Bind("Type")%>'></asp:Label>
                                                 
                                                </ItemTemplate>                                                
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="25%" />
                                            </asp:TemplateField>
                                    
                                            
                                                                             
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnedit" runat="server" 
                                                        ToolTip="Edit"  ImageUrl="~/Account/images/edit.gif" 
                                                        onclick="imgbtnedit_Click" />
                                                </ItemTemplate>                                               
                                                <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtndelete" runat="server" CommandArgument='<%# Eval("Operatingid") %>'
                                                        ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                </ItemTemplate>                                                
                                                <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                <ItemStyle HorizontalAlign="Left" />
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
        </asp:UpdatePanel>





</asp:Content>

