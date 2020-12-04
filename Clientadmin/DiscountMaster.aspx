<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="DiscountMaster.aspx.cs" Inherits="DiscountMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <asp:UpdatePanel ID="UpdatePanel" runat="server">
     <ContentTemplate>

      <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="statuslable" runat="server" ForeColor="Red" Text=""> </asp:Label>
                </div>
               <%-- <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Discount Master Add/Manage"
                            Font-Bold="True" Visible="False"></asp:Label>
                    </legend>
                   
                    <div style="clear: both;">
                    </div>--%>
                    <asp:Panel ID="pnladd" runat="server" Visible="False" GroupingText="Discount Master Add/Manage">
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="DiscountName"></asp:Label>
                                        </label>
                                </td>
                                <td>
                                    <label>
                                    <asp:TextBox ID="txtDiscountName" runat="server" Height="25px" Width="200px"></asp:TextBox>
                                    </label>
                                    <label>
                                       
                                        
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td><label>       <asp:Label ID="Label13" runat="server" 
                                        Text="DiscountType"></asp:Label></label>
                                </td>
                                <td><label>
                     
                                    <asp:DropDownList ID="DropDownList1" runat="server" Width="209px" Height="25px">
                                    </asp:DropDownList>
                        </label>
                                </td>
                            </tr>
                            <tr>
                                <td><label> <asp:Label ID="Label14" runat="server" Text="DiscountPercentage"></asp:Label></label>
                                </td>
                                <td>  <label>
                           
                                    <asp:TextBox ID="txtDiscountPercentage" runat="server" Width="200px" 
                                        Height="25px"></asp:TextBox>
                        </label>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="DiscoutAmount"></asp:Label></label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtDiscoutAmount" runat="server" Width="200px" 
                                        Height="25px"></asp:TextBox>
                                    </label>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Status"></asp:Label></label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="drpstatus" runat="server" Width="209px" Height="25px">
                                            <asp:ListItem Selected="True" Value="Yes">Yes</asp:ListItem>
                                            <asp:ListItem Value="No">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>

                            </tr>
                            <tr>
                            <td colspan="2">
                              <label>
                            <%--  <asp:Label ID="Label13" runat="server" Text="Category Type"></asp:Label>--%>
                            
                        </label>
                            </td>
                            </tr>
                             <tr>
                                <td>
                                  
                                </td>

                                <td>
                                    <asp:Button ID="btnsave" runat="server" Text="Save" onclick="btnsave_Click" 
                                        Height="25px"/>
                            <asp:Button ID="buttonupdate" runat="server" Text="Update" Visible="False" 
                                        onclick="Update_Click" Height="25px"/>
                         
                                    <asp:Button ID="btnupdate" runat="server" onclick="btnupdate_Click" 
                                        Text="Cancel" Visible="false" Height="25px" />
                         
                                </td>
                            </tr>
                        </table>
                       
                      
                        
                      
                       
                      
                    </asp:Panel>
                </fieldset>
                <div >
             
                     <br />
               
                </div>
                  <div>
                 <div >
                   
                     </div>
                </div>
                   
                    </div>
                    <asp:Panel ID="Panel1" runat="server" 
             GroupingText="List of Discount Master">
               <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add New " onclick="btnadd_Click" 
                            Height="25px" />
                    </div>
        <td colspan="2">
                              <label>
                            <%--  <asp:Label ID="Label13" runat="server" Text="Category Type"></asp:Label>--%>
                            
                        </label>
                            </td>
                     <br />
               
                    <asp:GridView ID="GridView1" runat="server" 
                                        AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" GridLines="Both" 
                                      Width="100%" EmptyDataText="No Record Found." 
             onrowcommand="GridView1_RowCommand" onrowediting="GridView1_RowEditing" 
             onrowupdated="GridView1_RowUpdated" onrowupdating="GridView1_RowUpdating" 
             onselectedindexchanging="GridView1_SelectedIndexChanging" AllowPaging="True" 
                            onpageindexchanging="GridView1_PageIndexChanging">
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Product type name"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblproductName" runat="server" Text='<%#Eval("DiscountName") %>'></asp:Label>
                                                   <%-- <asp:Label ID="lblproductid" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>--%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="DiscountType"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblDiscountType" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblDiscountTypeId" runat="server" Text='<%#Eval("DiscountTypeName") %>' ></asp:Label>
                                                </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
                                                 <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="DiscountPercentage"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDiscountPercentage" runat="server" Text='<%#Eval("DiscountPercentage") %>'></asp:Label>
                                                    <%--<asp:Label ID="lblDiscountPercentageId" runat="server" Text='<%#Eval("InventeroyCatId") %>' Visible="false"></asp:Label>--%>
                                                </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Left" />
                                                  <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="DiscountAmount"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDiscountAmount" runat="server" Text='<%#Eval("DiscoutAmount") %>'></asp:Label>
                                                    <%--<asp:Label ID="lblDiscountPercentageId" runat="server" Text='<%#Eval("InventeroyCatId") %>' Visible="false"></asp:Label>--%>
                                                </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
                                                 <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%" SortExpression="Statuslabel"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <%--    <asp:CheckBox ID="CheckBox2" runat="server" Enabled="false" Checked='<%#Eval("Active") %>'
                                                        Visible="false" />--%>
                                                    <asp:Label ID="lbllabelstatus" runat="server" Text='<%#Eval("Active") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit"
                                                        CommandName="Edit" ImageUrl="~/Account/images/edit.gif" CommandArgument='<%#Eval("ID") %>'
                                                        onclick="llinedit_Click"></asp:ImageButton>
                                                </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
                                                 <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Btn" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" 
                                                        ToolTip="Delete" onclick="Btn_Click">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="2%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>


     </asp:Panel>
         <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" Text="developed by vinod on 1 may 2016" ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>


  </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>

