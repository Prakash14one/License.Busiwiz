<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="StoreTaxmethodtbl.aspx.cs" Inherits="ShoppingCart_Admin_StoreTaxmethodtbl" Title="Store Tax Mathod" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left:1%">
                    <asp:Label ID="Label1" runat="server" BorderColor="Red" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <fieldset>
                    <label>
                        <asp:Label ID="Label5" runat="server" Text="Business Name"></asp:Label>
                        <asp:DropDownList   ID="ddlSearchByStore" 
                                        runat="server" onselectedindexchanged="ddlSearchByStore_SelectedIndexChanged" 
                                        AutoPostBack="True" >
                                        </asp:DropDownList><asp:RequiredFieldValidator ID="rf1" runat="server" ControlToValidate="ddlSearchByStore" ErrorMessage="*">
                                        </asp:RequiredFieldValidator>
                    </label>
                    <label style="float:right">
                        <br />
                        <asp:Button ID="imgchange" runat="server"  CssClass="btnSubmit"
                            onclick="imgchange_Click" Text="Change Method of Taxation" Visible="false"/>
                            
                       
                        
                        
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnllabel" runat="server" Visible="false">
                        <label>
                            <asp:Label ID="lbl" runat="server" Text="The method of taxation of sales is: "></asp:Label>
                            
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="lbltax" runat="server" Text="" ></asp:Label>
                        </label>
                        <label style="float:right">
                            <asp:Button ID="imgrate" onclick="imgrate_Click" Text="Manage Tax Rates" runat="server" Visible="false" CssClass="btnSubmit" />
                            
                        </label>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlchange" runat="server" Visible="false">
                    
                    <label>
                        <asp:RadioButton ID="rd0" runat="server" AutoPostBack="false" GroupName="dd"  Text="Set Fixed  Rate of Tax for Sales" />
                        
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:RadioButton ID="rd1" runat="server" AutoPostBack="false" GroupName="dd" Text="Set Fixed Rate of Tax for Sales Regionally" />
                        
                    </label>
                    <div style="clear: both;">
                </div>
                    <label>
                         <asp:RadioButton ID="rd2" runat="server" AutoPostBack="false" GroupName="dd" Text="Set Tax Rate/Amount for Different Products Individually & Regionally" />
                       
                    </label>
                     <div style="clear: both;">
                </div>
                <asp:Button ID="ImageButton9" runat="server" CssClass="btnSubmit"
                            onclick="ImageButton9_Click" Text="Update" Visible="false"  />
                            
                        <asp:Button ID="ImageButton1" runat="server" CssClass="btnSubmit"
                            onclick="ImageButton1_Click" Text="Submit" Visible="false"  />
                    </asp:Panel>
                    
                     
                  
                </fieldset>
                
            </div>
            
            
            
            
            
            
            
            
            
            
                 
            
           
        </ContentTemplate>
    </asp:UpdatePanel>
 </asp:Content>


