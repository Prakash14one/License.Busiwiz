<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/BeforeLogin/Site.Master" CodeBehind="NoUpdatePanels.aspx.cs"
    Inherits="UpdatePanelExamples.NoUpdatePanels" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .panel {
            border: 1px solid red;
            width: 150px;
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
   <h3>Content one</h3>
   <div class="panel">
       <asp:Label runat="server" ID="lblContentOneDate"></asp:Label>
   </div>
   
    <h3>Content two</h3>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
    <ContentTemplate>
    <div class="panel">
        <asp:Label runat="server" ID="lblContentTwoDate"></asp:Label><br/>
        <asp:Button runat="server" ID="btnRefresh1" Text="Refresh Content two" 
        OnClick="btnRefresh1_OnClick"/>   
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    <h3>Content three</h3>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
    <ContentTemplate>
    <div class="panel">
        <asp:Label runat="server" ID="lblContentThreeDate"></asp:Label><br/>
        <asp:Button runat="server" ID="btnRefresh2" 
        Text="Refresh Content three" OnClick="btnRefresh2_OnClick"/>   
    </div>
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
