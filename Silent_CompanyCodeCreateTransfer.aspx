<%@ Page Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true"
    CodeFile="Silent_CompanyCodeCreateTransfer.aspx.cs" Inherits="AccessRight" Title="Page Access Right" %>

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box" style="min-height:400px;">
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td align="center" >
                                <h2 style="text-align: center">
                                    <asp:Label ID="Label1a" runat="server" Text="Notice"></asp:Label>
                                </h2>
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                            </td>
                        </tr>
                        <tr>
                        <td><br />
                                <asp:Label ID="lblmsg" runat="server" Text="" style="font-size:14px"></asp:Label>

                        </td>
                        </tr>
                        <tr>
                        <td style="color: #FFFFFF; background-color: #FFFFFF">
                               lbl_ClientServerID  <asp:label id="lbl_ClientServerID" runat="server"></asp:label><br />
                 lbl_CompServerID   <asp:label id="lbl_CompServerID" runat="server"></asp:label><br />
                  lbl_compid  <asp:label id="lbl_compid" runat="server" ></asp:label><br />
                  lbl_ProdMasCodeId  <asp:label id="lbl_ProdMasCodeId" runat="server" ></asp:label><br />
                  lbl_versionid  <asp:label id="lbl_versionid" runat="server" ></asp:label><br />
                 lbl_codetypeid   <asp:label id="lbl_codetypeid" runat="server" ></asp:label><br />
                 lbl_codeversionno   <asp:label id="lbl_codeversionno" runat="server" ></asp:label><br /> 
                              
                   
                   
                      lbl_serverconnnstr        <asp:label id="lbl_serverconnnstr" runat="server" ></asp:label><br />
                         lbl_TOMasterServer_TemppathFor_Comp     <asp:label id="lbl_TOMasterServer_TemppathFor_Comp" runat="server" ></asp:label><br />
                           lbl_TOMasterServer_PublishFor_Comp   <asp:label id="lbl_TOMasterServer_PublishFor_Comp" runat="server" ></asp:label><br />
                          lbl_Client_latestcodeZipFilePath    <asp:label id="lbl_Client_latestcodeZipFilePath" runat="server" ></asp:label><br />
                         lbl_Client_CodePath     <asp:label id="lbl_Client_CodePath" runat="server" ></asp:label><br />


                              <asp:label id="lbl_CodeFolderNameZIP" runat="server" ></asp:label><br />
                              <asp:label id="lbl_CodeFolderName" runat="server" ></asp:label><br />
                              <asp:label id="lbl_AppcodefolderPath" runat="server" ></asp:label><br />
                              <asp:label id="lbl_PublishfolderPath" runat="server" ></asp:label><br />
                              
                              
                        </td>
                        </tr>
                    </table>                   
                   <table>
                   <tr>
                   <td>
                   
                   </td>
                   </tr>
                   </table> 
                    
                        
                   
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
