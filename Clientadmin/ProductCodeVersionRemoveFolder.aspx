<%@ Page Title="" Language="C#"  MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="ProductCodeVersionRemoveFolder.aspx.cs" Inherits="productcode_databaseaddmanage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"> 
<style type="text/css">
        .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
    
        TR.updated TD
        {
            background-color:yellow;
        }
        .modalBackground 
        {
            background-color:Gray;
            filter:alpha(opacity=70);
            opacity:0.7;
        }
    .pnlBackGround
{
 position:fixed;
    top:10%;
    left:10px;
    width:300px;
    height:125px;
    text-align:center;
    background-color:White;
    border:solid 3px black;
}
    </style>



    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <div style="clear: both;">
    
                </div> 
     <fieldset>
     <div style="float: left;">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
                         <legend>
                        <asp:Label ID="Label19" runat="server" Text="Product Code Add Manage"></asp:Label>
                 </legend>         
                 <div style="float: right;">                    
                    <asp:Button ID="btn_addnew" runat="server" CssClass="btnSubmit" onclick="btnAddNewDAta_Click"  Text="Add New Codes" />                    
                </div>
     
                    <asp:Panel ID="Panel1" runat="server" Visible="false">

                        <table style="width: 100%">
                            <tr>
                                <td width="20%">
                                  <label>
                                     <asp:Label ID="lbl_serverid" runat="server" Text="" Visible="false"></asp:Label>
                                     <asp:Label ID="Label1" runat="server" Text="Select Products"></asp:Label>
                                    <asp:Label ID="Label51" runat="server" CssClass="labelstar" Text="*"></asp:Label>                                    
                                    <asp:RequiredFieldValidator ID="RequiredFsdfsdfieldValidator34" runat="server" ControlToValidate="ddlproductversion" ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>                                     
                                    </label>
                                </td>
                                <td width="50%">
                                <label>
                                    <asp:DropDownList ID="ddlproductversion" runat="server" AutoPostBack="True" onselectedindexchanged="ddlproductversion_SelectedIndexChanged" Width="200px" >
                                    </asp:DropDownList>                                   
                                    </label> 
                                    <label>
                                             <asp:CheckBox ID="Chk_addactive" runat="server" Checked="true" Text="Active" Visible="false" />
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
                                        <asp:TextBox ID="txt_prod_desc" TextMode="MultiLine" Width="600px" 
                                        Height="45px" runat="server" Enabled="False" BackColor="White" 
                                        BorderColor="White" BorderStyle="None" ForeColor="Black"></asp:TextBox>
                                        <asp:DropDownList ID="ddlcodetypecategory" runat="server" Visible="false"  Enabled="false" onselectedindexchanged="ddlcodetypecategory_SelectedIndexChanged"  AutoPostBack="True" Width="200px">
                                         </asp:DropDownList>
                                    </label> 
                                </td>
                            </tr>                            
                           <tr>
                                                        <td style="width:30%">
                                                            <label>
                                                                 <asp:Label ID="Label17111awebsite" runat="server" Text="Select websites"></asp:Label>
                                                                 <asp:Label ID="Label1811awebstar" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DDLWebsiteC" ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                                             </label>
                                                            </td>
                                                            <td style="width: 70%">
                                                                <label>
                                                                    <asp:DropDownList ID="DDLWebsiteC" runat="server" Width="250px" AutoPostBack="True" onselectedindexchanged="DDLWebsiteC_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                          <tr>
                                                            <td width="30%">
                                                            <label>
                                                                <asp:Label ID="Label4" runat="server" Text="Enter Folder Path"></asp:Label>
                                                                <asp:Label ID="Label5" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcodetypename" ValidationGroup="1"  ErrorMessage="*" ></asp:RequiredFieldValidator>
                                                                </label> 
                                                            </td>
                                                            <td width="70%">                              
                                                             <asp:TextBox ID="txtcodetypename" Width="500px"  runat="server"></asp:TextBox>
                                                            </td>                                
                                                            
                                                        </tr>
                            <tr>
                                <td width="22%">
                                    &nbsp;</td>
                                <td width="25%">
                                    <asp:Button ID="btn_submitCode" runat="server"  CssClass="btnSubmit" onclick="btn_submitCode_Click" Text="Submit" ValidationGroup="1" />
                                    <asp:Button ID="btn_updateCode" runat="server"  CssClass="btnSubmit" onclick="btn_updateCode_Click" Text="Update" ValidationGroup="1" Visible="False" />
                                    <asp:Button ID="BtnCancel" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="BtnCancel_Click" />
                                </td>
                               
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                           <%-- <cc1:ModalPopupExtender ID="modal11"   BackgroundCssClass="modalBackground"  CancelControlID="btnCancelPopup1" runat="server" TargetControlID="btn_submitCode" PopupControlID="pnlPopup1"></cc1:ModalPopupExtender>
                                        <asp:Panel ID="pnlPopup1" Height="150px" Width="600px" runat="server" CssClass="pnlBackGround" Visible="false">
                                        <asp:Label ID="lblMsgPopup1" runat="server" Text="The physical path of the website is different than the recommended path for the website. It will be moved to recommended path"></asp:Label><br /><br />
                                        <center>
                                        <label>
                                        <asp:Button ID="btnCancelPopup1" runat="server" Text="Confirm"  />    
                                        </label> 
                                        <label>
                                         <asp:Button ID="Button1" runat="server" Text="Cancel" onclick="btn_pnlclose_Click"  />                                       
                                         </label> 
                                        </center>
                                    </asp:Panel>--%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
              </fieldset>
              <fieldset>
                  

                    <asp:Panel ID="Panel4" runat="server" GroupingText="List of delete folder when publish website code">
                        <table style="width: 100%">   
                        <tr>
                                <td width="25%">
                                     <label>
                                        <asp:CheckBox ID="chk_Active" runat="server" Checked="true" Text="Show active filters only" AutoPostBack="True" oncheckedchanged="chk_Active_CheckedChanged"/>
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
                                    <asp:DropDownList ID="ddlcodetypecategory0" runat="server" Enabled="false"  onselectedindexchanged="ddlcodetypecategory0_SelectedIndexChanged"  AutoPostBack="True">
                                    </asp:DropDownList>
                                    </label> 
                                   
                                    <label>
                                           <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" Width="181px" Visible="false"  OnSelectedIndexChanged="ddlcodetypecategory0_SelectedIndexChanged">
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
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="DelFolID" EmptyDataText="No Record Found." PagerStyle-CssClass="pgr" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Product Name" ItemStyle-Width="20%" SortExpression="VersionInfoName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpversion" runat="server" Text='<%#Bind("ProductName") %>'></asp:Label>
                                                      <asp:Label ID="Label7" Visible="false"  runat="server" Text='<%#Bind("vv") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Website Name" ItemStyle-Width="20%" SortExpression="WebsiteName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWebsiteName" runat="server" Text='<%#Bind("WebsiteName") %>'></asp:Label>                                                      
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>                                            
                                          
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code Name" ItemStyle-Width="20%" SortExpression="CodeTypeName" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldefaultcodename" runat="server" Text='<%#Bind("CodeTypeName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Version Delete Folder Path" ItemStyle-Width="40%" SortExpression="VersionDeleteFolderPath" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpaths" runat="server" Text='<%#Bind("VersionDeleteFolderPath") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="40%" />
                                            </asp:TemplateField>
                                         
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Account/images/edit.gif" onclick="ImgBtn_EditGrig"/>
                                                </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                         <asp:Label ID="Label19" runat="server"  Text='<%#Eval("ID") %>' Visible="False"></asp:Label>
                                                            <asp:ImageButton ID="imgdelete" runat="server" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete" onclick="imgdelete_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="4">
                                  
                                     
                                    
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
               
                    
                             
    </ContentTemplate>
     <Triggers>          
          <%--  <asp:PostBackTrigger ControlID="btn_upload_mastercode" />--%>
            
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

