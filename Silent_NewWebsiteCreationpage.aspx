<%@ Page Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true"
    CodeFile="Silent_NewWebsiteCreationpage.aspx.cs" Inherits="AccessRight" Title="Page Access Right" %>

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
                                    <asp:Label ID="Label1a" runat="server" Text=""></asp:Label>
                                </h2>
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                            </td>
                        </tr>
                        <tr>
                             <td><br />
                                <asp:Label ID="lblmsg" runat="server" Text="" style="font-size:14px"></asp:Label>
                                <asp:Label ID="lbl_serverurl" runat="server" Visible="false"  Text="" style="font-size:1px"></asp:Label>
                                <asp:Button ID="Button1" runat="server" Visible="false"  onclick="Button1_Click" Text="Submit " Height="25px" Width="60px" />
                        </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnl_downloadstatus" runat="server" Visible="false"  >
                                                  <table style="vertical-align:top;" width="100%">
                                                    <tr>
                                                                <td>
                                                                <label>
                                                                      <%-- Please wait following files are being downloaded to your server--%>
                                                                </label> 
                                                                </td>
                                                             </tr>
                                                    <tr>
                                                                <td align="center">
                                                                <%--AllowPaging="True" PageSize="20"--%>
                                                                        <asp:GridView ID="Gv_Com_Cre_Need_Code" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
                                                                                    EmptyDataText="No Record Found."  Width="1%" CssClass="mGrid"
                                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  OnPageIndexChanging="Gv_Com_Cre_Need_Code_PageIndexChanging"
                                                                                    OnRowCommand="Gv_Com_Cre_Need_Code_RowCommand" OnRowDeleting="Gv_Com_Cre_Need_Code_RowDeleting" OnRowEditing="Gv_Com_Cre_Need_Code_RowEditing1"
                                                                                    OnRowDataBound="Gv_Com_Cre_Need_Code_RowDataBound">
                                                                                    <Columns>                                                                                       
                                                                                        <asp:TemplateField HeaderText="Code Name" SortExpression="Name" ItemStyle-Width="65%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_Name" runat="server" Text='<%# Bind("Name")%>'></asp:Label>
                                                                                                <asp:Label ID="lbl_id" runat="server" Text='<%# Bind("Id")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_CompanyLoginId" runat="server" Text='<%# Bind("CompanyLoginId")%>' Visible="false"></asp:Label>
                                                                                                
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                            <ItemStyle Width="65%" />
                                                                                        </asp:TemplateField>                                                                                 
                                                                                        <asp:TemplateField HeaderText="Status" SortExpression="Successfullyuploadedtoserver" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_status" runat="server" Text='<%# Bind("Successfullyuploadedtoserver")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                            <ItemStyle Width="25%" />
                                                                                        </asp:TemplateField>                                                                                                                                                                                         
                                                                                         <asp:TemplateField HeaderText="Go" HeaderImageUrl="~/images/Btn_go.jpg" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" Visible="false">
                                                                                                <ItemTemplate>
                                                                                                        <asp:ImageButton ID="imgbtnedit" runat="server" CommandArgument='<%# Eval("Id") %>' ToolTip="Go" CommandName="Go" ImageUrl="~/images/Btn_go.jpg" />
                                                                                                         </ItemTemplate>
                                                                                                     <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                                                                 <ItemStyle HorizontalAlign="Left" />
                                                                                          </asp:TemplateField>                                          
                                                                                    </Columns>
                                                                                   <PagerStyle CssClass="pgr" />
                                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                 </asp:GridView>
                                                                </td>
                                                              </tr>
                                                               <tr>
                                                                <td>
                                                                <label>
                                                                    <%--  This may take upto 20 -25 minutes depending on the speed of your server.--%>
                                                                      <br />
                                                                   <%--   Please do not close this browser. --%>
                                                                </label> 
                                                                </td>
                                                             </tr>
                                                   </table>
                                            </asp:Panel>
                            </td>
                        </tr>


                        <tr>

                             <td style="color: #FFFFFF; background-color: #FFFFFF">
                               <asp:label id="lbl_compid" runat="server" Visible="false" ></asp:label>
                             <%--   lbl_ClientServerID  <asp:label id="lbl_ClientServerID" runat="server"></asp:label><br />
                             lbl_CompServerID   <asp:label id="lbl_CompServerID" runat="server"></asp:label><br />
                              <br />
                              lbl_ProdMasCodeId  <asp:label id="lbl_ProdMasCodeId" runat="server" ></asp:label><br />--%>
                             <%-- lbl_versionid  <asp:label id="lbl_versionid" runat="server" ></asp:label><br />--%>
                             <%-- lbl_codetypeid   <asp:label id="lbl_codetypeid" runat="server" ></asp:label><br />--%>
                             <%-- lbl_codeversionno   <asp:label id="lbl_codeversionno" runat="server" ></asp:label><br /> --%>
                             <%-- lbl_serverconnnstr        <asp:label id="lbl_serverconnnstr" runat="server" ></asp:label><br />
                             lbl_TOMasterServer_TemppathFor_Comp     <asp:label id="lbl_TOMasterServer_TemppathFor_Comp" runat="server" ></asp:label><br />
                               lbl_TOMasterServer_PublishFor_Comp   <asp:label id="lbl_TOMasterServer_PublishFor_Comp" runat="server" ></asp:label><br />
                              lbl_Client_latestcodeZipFilePath    <asp:label id="lbl_Client_latestcodeZipFilePath" runat="server" ></asp:label><br />
                             lbl_Client_CodePath     <asp:label id="lbl_Client_CodePath" runat="server" ></asp:label><br />--%>
                             <%-- <asp:label id="lbl_CodeFolderNameZIP" runat="server" ></asp:label><br />
                              <asp:label id="lbl_CodeFolderName" runat="server" ></asp:label><br />
                              <asp:label id="lbl_AppcodefolderPath" runat="server" ></asp:label><br />
                              <asp:label id="lbl_PublishfolderPath" runat="server" ></asp:label><br />--%>
                              
                              
                        </td>
                        </tr>
                    </table>                   
                    
                    
                        
                   
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
