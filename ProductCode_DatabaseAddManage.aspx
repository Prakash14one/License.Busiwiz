<%@ Page Title="" Language="C#"  MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="ProductCode_DatabaseAddManage.aspx.cs" Inherits="productcode_databaseaddmanage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <div style="clear: both;">
                </div> 
     <fieldset>
     <div style="float: left;">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
                         <legend>
                        <asp:Label ID="Label19" runat="server" Text="Product Code / Database / Script Add Manage"></asp:Label>
                 </legend>         
                 <div style="float: right;">                    
                    <asp:Button ID="btn_addnew" runat="server" CssClass="btnSubmit" onclick="btnAddNewDAta_Click"  Text="Add New Code / Database" />                    
                </div>
     
                    <asp:Panel ID="Panel1" runat="server" Visible="false">

                        <table style="width: 100%">
                            <tr>
                                <td width="20%">
                                  <label>
                                     <asp:Label ID="Label1" runat="server" Text="Select Products"></asp:Label>
                                    <asp:Label ID="Label51" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlproductversion" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td width="50%">
                                <label>
                                    <asp:DropDownList ID="ddlproductversion" runat="server" AutoPostBack="True" onselectedindexchanged="ddlproductversion_SelectedIndexChanged" Width="200px" >
                                    </asp:DropDownList>
                                    </label> 
                                </td>
                               
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                         <asp:Label ID="Label11" runat="server" Text="Products Description"></asp:Label>
                                     </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                                                  
                                 <asp:TextBox ID="txt_prod_desc" TextMode="MultiLine" Width="600px" Height="45px"  
                                        runat="server" Enabled="False"></asp:TextBox>
                                
                                        
                                    </label> 
                                </td>
                            </tr>
                            <tr>
                                <td width="20%">
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Code Category"></asp:Label>
                                    <asp:Label ID="Label3" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlproductversion" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label> 
                                </td>
                                <td width="25%">
                                    <asp:DropDownList ID="ddlcodetypecategory" runat="server" onselectedindexchanged="ddlcodetypecategory_SelectedIndexChanged"  AutoPostBack="True" Width="200px">
                                    </asp:DropDownList>
                                </td>
                                
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="20%">
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Code Name"></asp:Label>
                                    <asp:Label ID="Label5" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcodetypename" ValidationGroup="1"  ErrorMessage="*" ></asp:RequiredFieldValidator>
                                    </label> 
                                </td>
                                <td width="25%">
                              
                                 <asp:TextBox ID="txtcodetypename" runat="server"></asp:TextBox>
                                </td>
                                
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="20%">
                                    &nbsp;</td>
                                <td colspan="2" style="width: 50%">
                                    <asp:Panel ID="Panel2" runat="server" >
                                     <asp:CheckBox ID="CheckBox1" runat="server" Text="Is it Product's Default Code?"  AutoPostBack="True"/>
                                     <br />
                                    <label>  <asp:Label ID="Label9" runat="server" Text="(Is this the code where the unique subscriber files will be inserted in copy of master code to make the code unique for the subscriber?)"></asp:Label>
                                     </label>
                                    </asp:Panel> 
                                    <asp:Panel ID="Panel3" runat="server" Visible="false">
                                       
                                         <asp:CheckBox ID="CheckBox11" runat="server" Text="Is it Busicontroller Database?" AutoPostBack="True" oncheckedchanged="CheckBox11_CheckedChanged"/>
                                           
                                     <asp:CheckBox ID="CheckBox12" runat="server" Text="Is it Company Default Database?" AutoPostBack="True" oncheckedchanged="CheckBox12_CheckedChanged"/>
                                                                      
                                    </asp:Panel> 
                                   </td>
                               
                            </tr>
                            <tr>
                                <td width="20%">
                               <label>
                                 <asp:Label ID="Label10" runat="server" Text="Active"></asp:Label>
                               </label>
                                    &nbsp;</td>
                                <td width="25%">
                                          <asp:CheckBox ID="Chk_addactive" runat="server" Checked="true" Text="Active" />
                                    &nbsp;</td>
                               
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="22%">
                                    &nbsp;</td>
                                <td width="25%">
                                    <asp:Button ID="btn_submit" runat="server"  CssClass="btnSubmit" onclick="Button1_Click" Text="Submit" ValidationGroup="1" />
                                    <asp:Button ID="btn_update" runat="server"  CssClass="btnSubmit" onclick="Button5_Click" Text="Update" ValidationGroup="1" Visible="False" />
                                    <asp:Button ID="BtnCancel" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="BtnCancel_Click" />
                                </td>
                               
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                        </table>

                    </asp:Panel>
              </fieldset>
              <fieldset>
            
                    <asp:Panel ID="Panel4" runat="server" GroupingText="List Of Product Code / Database / Script">
                        <table style="width: 100%">   
                        <tr>
                                <td width="25%">
                                     <label>
                                        <asp:CheckBox ID="chk_Active" runat="server" Checked="true" Text="Show Active Filters Only" AutoPostBack="True" oncheckedchanged="chk_Active_CheckedChanged"/>
                                    </label> 
                                    </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>                        
                            <tr>
                                <td colspan="4">
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="Product Name"></asp:Label>
                                    </label> 
                              <label>
                                    <asp:DropDownList ID="ddlProductname" runat="server" AutoPostBack="True" 
                                        DataTextField="ProductName" DataValueField="ProductId" 
                                        OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged">
                                    </asp:DropDownList>
                               </label> 
                                  <label>
                                     <asp:Label ID="Label52" runat="server" Text="Category Type "></asp:Label>
                                     </label>
                                     <label>                               
                                    <asp:DropDownList ID="ddlcodetypecategory0" runat="server"  onselectedindexchanged="ddlcodetypecategory0_SelectedIndexChanged"  AutoPostBack="True">
                                    </asp:DropDownList>
                                    </label> 
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text="Status"></asp:Label>
                                    </label>  
                                    <label>
                                           <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" Width="181px" OnSelectedIndexChanged="ddlcodetypecategory0_SelectedIndexChanged">
                                            <asp:ListItem Text="All" ></asp:ListItem>
                                            <asp:ListItem Text="Active" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Inactive"></asp:ListItem>
                                         </asp:DropDownList>
                                    </label>                                   
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="GridView1" runat="server" 
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="Id" EmptyDataText="No Record Found." PagerStyle-CssClass="pgr" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Product Name" ItemStyle-Width="20%" SortExpression="VersionInfoName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpversion" runat="server" Text='<%#Bind("ProductName") %>'></asp:Label>
                                                      <asp:Label ID="Label7" Visible="false"  runat="server" Text='<%#Bind("vv") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code Category" ItemStyle-Width="12%" SortExpression="CodeTypeCategory" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcodetypecategory" runat="server" Text='<%#Bind("CodeTypeCategory") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="12%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code Name" ItemStyle-Width="20%" SortExpression="CodeTypeName" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldefaultcodename" runat="server" Text='<%#Bind("CodeTypeName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                           <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code Name" ItemStyle-Width="25%" SortExpression="Name" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcodetype" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="25%" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Default Application Code?">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label53" runat="server" Text='<%#Bind("AdditionalPageInserted") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Busiwiz Database?">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label54" runat="server" Text='<%#Bind("BusiwizSynchronization") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Default Application Database?">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label55" runat="server" Text='<%#Bind("CompanyDefaultData") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Active">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActive" runat="server" Text='<%#Bind("Active") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Account/images/edit.gif" onclick="ImgBtn_EditGrig"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                         <asp:Label ID="Label19" runat="server"  Text='<%#Eval("ID") %>' Visible="False"></asp:Label>
                                                            <asp:ImageButton ID="imgdelete" runat="server" ImageUrl="~/Account/images/delete.gif" ToolTip="Delete" onclick="imgdelete_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
               
                     <asp:Panel ID="Paneldoc" runat="server" Width="55%" CssClass="modalPopup">
                    <fieldset>
                     <div>
                         <asp:Label ID="Label6" runat="server" ForeColor="Black"></asp:Label>
                     </div>
                     <div>
                     </div>
                     <div align="center">
                         <asp:Button ID="Button3" runat="server" Text="Yes" onclick="Button3_Click" />
                         <asp:Button ID="Button4" runat="server" Text="Cancel" onclick="Button4_Click" />
                     </div>
                     </fieldset>
                     </asp:Panel>
                       <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Paneldoc" TargetControlID="btnreh" >
                                </cc1:ModalPopupExtender> 
                                </fieldset> 
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

