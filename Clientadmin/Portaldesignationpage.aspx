<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="Portaldesignationpage.aspx.cs" Inherits="Portaldesignationpage" Title="Portal designation afterlogin page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server">
    <style type="text/css">
        .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
    <div style="padding-left: 2%">
        <div style="clear: both;">
            <asp:Panel ID="Panel2" runat="server">
            </asp:Panel>
        </div>
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div class="products_box">
        <fieldset>
            <legend>
                <asp:Label runat="server" ID="lbllegend" Text=""></asp:Label>
            </legend>
            <div style="clear: both;">
            </div>
            <asp:Panel ID="pnladdd" runat="server" Visible="true">
                <table width="100%">
                    <tr>
                        <td>
                            <table>
                                <tr>                                  
                                    <td>
                                        <label>
                                         <asp:Label ID="lblasd" Text="Product" runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatora2" runat="server" ControlToValidate="ddlproduct" InitialValue="0" SetFocusOnError="true" ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>                                          
                                        </label>
                                    </td>   
                                    <td>
                                        <label>
                                          <asp:DropDownList ID="ddlproduct" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged" Width="150px">
                                            </asp:DropDownList>
                                            </label> 
                                    </td>  
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label1" Text="Portal name" runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlproduct" InitialValue="0" SetFocusOnError="true" ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </label> 
                                    </td>
                                     <td>
                                        <label>                                              
                                            <asp:DropDownList ID="ddlportal" runat="server" AutoPostBack="True" Width="150px" onselectedindexchanged="ddlportal_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>   
                                    </td>
                                </tr>
                                <tr>
                                     <td>
                                        <label>
                                              Select Website  
                                        </label> 
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="DDLWebsiteC" runat="server" Width="200px" AutoPostBack="True" onselectedindexchanged="DDLWebsiteC_SelectedIndexChanged">
                                              </asp:DropDownList>
                                        </label> 
                                    </td>                                   
                                </tr>                               
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <fieldset>
                                <legend>
                                    <asp:Label ID="Label6" Text="Set default after login page for selected portal" runat="server"></asp:Label>
                                </legend>
                                <label>
                                            <asp:Label ID="Label2" Text="Default after login page for selected portal" runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlproduct" SetFocusOnError="true" ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </label> 
                                         <label>                                              
                                            <asp:DropDownList ID="ddlpage" runat="server" Width="300px">
                                            </asp:DropDownList>
                                        </label>


                                </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset>
                                <legend>
                                    <asp:Label ID="Label3" Text="Set designation wise after login page for selected portal"
                                        runat="server"></asp:Label>
                                </legend>
                                <asp:Panel ID="PNLAA" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                               
                                                            <label>
                                                                <asp:Label ID="Label4" Text="Designation" runat="server"></asp:Label>
                                                                 <asp:DropDownList ID="ddldes" runat="server">
                                                                </asp:DropDownList>
                                                            </label>                                                       
                                                            <label>
                                                                <asp:Label ID="Label5" Text="After login page" runat="server"></asp:Label>
                                                                 <asp:DropDownList ID="ddlafterloginpage" runat="server" Width="300px">
                                                                </asp:DropDownList>
                                                            </label>
                                                            <label><br />
                                                                  <asp:Button ID="btntempadd" runat="server" Text="Add" CssClass="btnSubmit" OnClick="btntempadd_Click" />
                                                             </label>
                                                       
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlmultoplan" runat="server" >
                                                    <asp:GridView ID="GRDfa" runat="server" DataKeyNames="PortalId" ShowFooter="false"
                                                        AutoGenerateColumns="False" EmptyDataText="No Record Found." CssClass="mGrid"
                                                        GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        Width="100%" AllowSorting="false" onrowcommand="GRDfa_RowCommand" >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Designation Name" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcb" runat="server" Text='<%# Bind("DesignationName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Page Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpname" runat="server" Text='<%# Bind("PageName") %>'></asp:Label>
                                                                    <asp:Label ID="lbldefpage" runat="server" Text='<%# Bind("DPN") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblpnSave" runat="server" Text='<%# Bind("PageNameSave") %>' Visible="false"></asp:Label>
                                                                     <asp:Label ID="lblportalname" runat="server" Text='<%# Bind("PortalName") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:ButtonField ButtonType="Image" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                HeaderImageUrl="~/Account/images/delete.gif" ImageUrl="~/Account/images/delete.gif"
                                                                HeaderText="Delete" ItemStyle-Width="2%" CommandName="del">
                                                                <ItemStyle Width="50px" />
                                                            </asp:ButtonField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnsave" runat="server" Text="Save" Visible="true" CssClass="btnSubmit"
                                OnClick="btnsave_Click" />
                                <asp:Button ID="btnedit" runat="server" Text="Edit" Visible="false" 
                                CssClass="btnSubmit" onclick="btnedit_Click"
                               />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
    </div>
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
