<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="StoreProceduresAutofetch.aspx.cs" Inherits="storedprocedueautofetch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



<asp:UpdatePanel ID="UpdatePanel1" runat="server">

 <ContentTemplate>
         <div style="margin-left:1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
               
            </div>
            

     <asp:Panel ID="Panel2" runat="server">
    

            <fieldset><legend>Sync Stored Procedure and  Detail From SQl Database</legend>


                    <table style="width: 100%">
                         <tr>
                          <td style=" width="25%">
                        

                         <label>
                                <asp:Label ID="Label1" runat="server" Text="Product Name:"></asp:Label>
                                   <asp:Label ID="Label69" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="rfvname0" runat="server" ControlToValidate="ddlProductname"
                                            ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </label>
                            </td>  

                            <td style=" width="25%">
                           <label>
                                <asp:DropDownList ID="ddlProductname"  runat="server" AutoPostBack="True" 
                                    Width="300px" onselectedindexchanged="ddlProductname_SelectedIndexChanged" 
                                  >
                                </asp:DropDownList>
                            </label>



                                 </td>

                                 </tr>
                                  <tr>
                         <td style=" width="25%">
                        

                        <label>
                                <asp:Label ID="Label2" runat="server" Text="Database:"></asp:Label>
                                 <asp:Label ID="Label4" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList1"
                                            ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>


                            </label>




                                 </td>
                             <td style=" width="25%">
                           <label>
                                <asp:DropDownList ID="DropDownList1"  runat="server" AutoPostBack="True" 
                                    Width="300px" onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                                  >
                                </asp:DropDownList>
                            </label>





                                 </td>
                                 </tr>
                                  <tr>
                          <td style=" width="25%">
                        


                         

                                 </td>
                               <td style=" width="25%">
                        
                      




                                 </td>
                                 </tr>

                                  <tr>
                          <td style=" width="25%">
                        


                         

                                 </td>
                               <td style=" width="25%">
                        
                      




                                   <asp:Button ID="Button1" runat="server" Text="Syncronise Stored Procedure and Code" 
                                       onclick="Button1_Click" ValidationGroup="1" />
                        
                      




                                 </td>
                                 </tr>
                                 </table>

                                 </fieldset>


 </asp:Panel>
                                   <fieldset>
                    <%--<legend>List of Database Information </legend>--%>
                    <br />
                     <asp:Panel ID="Panel1" runat="server">
                       <table style="width: 100%">
                       <tr>
                         <td style="width: 30%; height: 17px;" >
                         </td>
                         <td style="width: 30%; height: 17px;" >
                         </td>
                         <td style="width: 30%; height: 17px;" >
                         </td>
                         <td style="width: 30%; height: 17px;" >
                             <asp:Button ID="Button3" runat="server" Text="Syncronise Now" 
                                 onclick="Button3_Click" />
                         </td>
                         </tr>
                     <tr>
                         <td colspan="4" >
                                 <label>
                             <asp:Label ID="Label22" runat="server" Text="Product:" style="width: 257px"></asp:Label>                             </label>
                          <%--  </td>
                         <td style="width: 30%; height: 28px;">--%>
                            <label>  
                                 <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged" Width="200px"
                                 > 
                               </asp:DropDownList>  
                               </label>   

                                <label>
                             <asp:Label ID="Label3" runat="server" Text="Database:" style="width: 257px"></asp:Label>                             </label>
                          <%--  </td>
                         <td style="width: 30%; height: 28px;">--%>
                            <label>  
                                 <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True"  Width="200px"
                                 > 
                               </asp:DropDownList>  
                               </label>   


                               
                               <label>     <asp:Label ID="Label7" runat="server" Text="Search:"></asp:Label> </label>
                               <label>    <asp:TextBox ID="TextBox6" runat="server" AutoPostBack="True"></asp:TextBox> 
                                  
                                   </label>
                                   <label>
                                   <asp:Button ID="Button2" runat="server" Text="Go" onclick="Button2_Click"  />
                                   </label>
                               
                               
                                                       
                            <%--  <label>   <asp:Label ID="Label23" runat="server" Text="Table Name:"></asp:Label> </label>
                              <label>   <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                                     > 
                                 </asp:DropDownList>
                                 </label>
                                  <label>  <asp:Label ID="Label24" runat="server" Text="Page Name:"></asp:Label> </label>
                                  <label> <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                                      >
                                   </asp:DropDownList> </label>--%></td>
                            
                   <%-- </tr>
                           <tr>
                               <td colspan="4">
                                
                              <label>     <asp:Label ID="Label28" runat="server" Text="Search:"></asp:Label> </label>
                               <label>    <asp:TextBox ID="TextBox5" runat="server" AutoPostBack="True"></asp:TextBox> 
                                  
                                   </label>
                                   <label>
                                   <asp:Button ID="Button15" runat="server" Text="Go"  />
                                   </label>
                               </td>
                           </tr>--%>
                   
                      <tr>
                         <td style="width: 30%;" >
                            </td>
                         <td style="width: 30%;" width="50%">
                            </td>
                                                     <td style="width: 30%;">
                            </td>
                            
                                                     <td style="width: 30%;" width="50%">
                                                   
                                                      
                                                         
                            </td>
                    </tr>


                      <tr>
                         <td style="width: 30%;" >
                            </td>
                         <td style="width: 30%;" width="50%">
                          
                            </td>
                                                     <td style="width: 30%;">
                            </td>
                            
                                                     <td style="width: 30%;" width="50%">
                            </td>
                    </tr>



                    </table>





                     <asp:GridView ID="GridView3" runat="server" Width="100%"
                             AutoGenerateColumns="False" EmptyDataText="No Record Found."
                                CssClass="mGrid" PagerStyle-CssClass="pgr" 
                             AlternatingRowStyle-CssClass="alt"  AllowPaging="True"
                                AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging" 
                             PageSize="10"  DataKeyNames="id" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Product Name/ Version" SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Lael31" runat="server" Text='<%# Eval("productversion") %>'></asp:Label>
                                            <%-- <asp:Label ID="lb_id" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Database Name " SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("DatabaseName") %>' EnableTheming="False"
                                                ForeColor="Black" ></asp:LinkButton>
                                         <asp:Label ID="Lael32" runat="server" Text='<%# Eval("id") %>' Visible="false" ></asp:Label>
                                           <%--  <asp:Label ID="lb_id" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Syncronise Date and Time" SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Lael33" runat="server" Text='<%# Eval("DateandTime") %>'></asp:Label>
                                             <%--<asp:Label ID="lb_id" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Syncronise Result" SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Lael34" runat="server" Text='<%# Eval("Result") %>'></asp:Label>
                                             <%--<asp:Label ID="lb_id" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                          <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="2%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit1" CommandArgument='<%# Eval("id") %>'
                                                                ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
 </asp:Panel>
</fieldset>









                                 </ContentTemplate>
                                 </asp:UpdatePanel>



</asp:Content>

