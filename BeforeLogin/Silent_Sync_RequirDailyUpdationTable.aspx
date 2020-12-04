<%@ Page Language="C#" MasterPageFile="~/BeforeLogin/BeforLoginMasterPage.master" AutoEventWireup="true"
    CodeFile="Silent_Sync_RequirDailyUpdationTable.aspx.cs" Inherits="AccessRight" Title="Page Access Right" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%--<style type="text/css">
.Labelcss {
    background-color: #f1f1f1;
    font-family: Times New Roman; 
     color: #666; 
     font-size:18px; 
     letter-spacing:inherit; 
}

h1 { color: #ff4411; font-size: 18px; font-family: 'Signika', sans-serif; padding-bottom: 10px; }
</style>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
            <h3>Message :  <asp:Label ID="lbl_Hmsg" runat="server" Text=""></asp:Label></h3>
                
                
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
                <div class="panel">
                     <p>
                         <asp:Label ID="lbl_jo1ID" runat="server" Text="" Visible="false"></asp:Label>
                         <asp:Label ID="lbl_Msg" runat="server" Text=""></asp:Label>
                         <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                         <br />    
                         <asp:Image ID="img_loading" runat="server" Width="95px" ImageUrl="~/Images/loading.gif" /> 
                     </p>
                    <br/>
                    <asp:Button runat="server" ID="btnRefresh1" Text="Refresh Content two" OnClick="btnRefresh1_OnClick" Visible="false" />   
                </div>
                </ContentTemplate>
                </asp:UpdatePanel> 


             <asp:Timer ID="Timer1" runat="server" Interval="1000"  Enabled="false" ontick="Timer1_Tick">
             </asp:Timer>
</asp:Content>
