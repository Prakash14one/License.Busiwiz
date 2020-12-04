<%@ Page Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true"
    CodeFile="Silent_Sync_RequirDailyUpdationTable.aspx.cs" Inherits="AccessRight" Title="Page Access Right" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
<style type="text/css">
.Labelcss {
    background-color: #f1f1f1;
    font-family: Times New Roman; 
     color: #666; 
     font-size:18px; 
     letter-spacing:inherit; 
}

h1 { color: #ff4411; font-size: 18px; font-family: 'Signika', sans-serif; padding-bottom: 10px; }
</style>
 <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
  
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label><br />
                <asp:Label ID="lblmsg1" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
               
                
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
