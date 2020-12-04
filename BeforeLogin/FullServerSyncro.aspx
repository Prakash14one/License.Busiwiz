<%@ Page Title="Syncronice Functionality" Language="C#" MasterPageFile="~/BeforeLogin/BeforLoginMasterPage.master" AutoEventWireup="true" CodeFile="FullServerSyncro.aspx.cs" Inherits="BeforeLogin_Syncro" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .panel {
            border: 1px solid red;
            width: 100%;
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphMain">  
    <h3>Message</h3>


    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
    <ContentTemplate>
    <div class="panel">
         <p>
             <asp:Label ID="lbl_jo1ID" runat="server" Text="" Visible="false"></asp:Label>
             <asp:Label ID="lbl_Msg" runat="server" Text=""></asp:Label>
             <br />    
              <asp:Image ID="img_loading" runat="server" Width="95px" ImageUrl="~/Images/loading.gif" />   
            <%-- <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false"></asp:HyperLink>
              <asp:Label ID="Label1" runat="server" Text="if you don't have an account."></asp:Label>  --%>
         </p>
        <br/>
        <asp:Button runat="server" ID="btnRefresh1" Text="Refresh Content two" OnClick="btnRefresh1_OnClick" Visible="false" />  
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    
     
 <asp:Timer ID="Timer1" runat="server" Interval="5000"  Enabled="false" ontick="Timer1_Tick">
  </asp:Timer>
 
    


   

   <asp:Panel ID="pnl" runat="server" Visible="false">
    <h3>Content three</h3>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
    <ContentTemplate>
    <div class="panel">
        <asp:Label runat="server" ID="lblContentThreeDate"></asp:Label><br/>
        <asp:Button runat="server" ID="btnRefresh2" Text="Refresh Content three" OnClick="btnRefresh2_OnClick"/>   
    </div>
     </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel> 
</asp:Content>

