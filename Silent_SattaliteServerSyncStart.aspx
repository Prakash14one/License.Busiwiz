<%@ Page Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true"
    CodeFile="Silent_SattaliteServerSyncStart.aspx.cs" Inherits="AccessRight" Title="Page Access Right" %>

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
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                         
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                    <asp:GridView ID="grdserver" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"
                                                   Visible="false"     EmptyDataText="No data required to be synchronise right now" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" Width="100%" 
                                                         AllowSorting="True" >
                                                        <%--PageSize="15" AllowPaging="True" OnPageIndexChanging="grdserver_PageIndexChanging" OnSorting="grdserver_Sorting" --%>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Satellite Server Name" SortExpression="ServerName" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("ServerName") %>'></asp:Label>  
                                                                    <asp:Label ID="lbl_TableId" runat="server" Text='<%# Eval("TableId") %>' Visible="false"></asp:Label>  
                                                                    <asp:Label ID="lbl_jobdetail" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>  
                                                                    <asp:Label ID="lbl_typeoperation" runat="server" Text='<%# Eval("TypeOfOperationDone") %>' Visible="false"></asp:Label>  
                                                                    <asp:Label ID="lbl_JobReordTableID" runat="server" Text='<%# Eval("JobReordTableID") %>' Visible="false"></asp:Label>  
                                                                    <asp:Label ID="lbl_RecordID" runat="server" Text='<%# Eval("RecordID") %>' Visible="false"></asp:Label>                                                                      
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderText="Sync requiring table name" SortExpression="TableName"  HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltabname" runat="server" Text='<%# Eval("TableName") %>' Visible="true"></asp:Label>
                                                                    <asp:Label ID="lblsyncreq" runat="server" Text='<%# Eval("TableName") %>' Visible="false" ></asp:Label>
                                                                    <asp:Label ID="lblseid" runat="server" Text='<%# Eval("SatelliteServerID") %>' Visible="false"></asp:Label>                                                                   
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Sync requiring RecordId" SortExpression="RecordID"  HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrecordid" runat="server" Text='<%# Eval("RecordID") %>' Visible="true"></asp:Label>
                                                                                                     
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Action" SortExpression="ActionName" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblACTION" runat="server" Text='<%# Eval("ActionName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sync requested date" SortExpression="JobDateTime" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldt" runat="server" Text='<%# Eval("JobDateTime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            
                                                           <%--  <asp:TemplateField HeaderText="Failed Attempt" SortExpression="DateandTime" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmsgatt" runat="server" Text='<%# Eval("Msg") %>'></asp:Label>
                                                                     <asp:Label ID="lblattempt" runat="server" Text='<%# Eval("Attempt") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="cbHeader" runat="server" Checked="true" Visible="false"  />
                                                                    <asp:Label ID="check" runat="server" ForeColor="White" Text="Sync" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbItem" runat="server" Checked="true" /></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>                           
                                       <asp:Timer ID="Timer1" runat="server" Interval="1000"  Enabled="false" ontick="Timer1_Tick">
                                                                    </asp:Timer> 
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>                                                                         
                                                                        <asp:Label ID="Label6" runat="server" Visible="false" Text="1"></asp:Label>
                                                                          <asp:Label ID="lbl_che5time" runat="server" Visible="false" Text="1"></asp:Label>
                                                              <asp:Panel ID="Paneldoc" runat="server"  Width="40%" BackColor="White"   CssClass="modalPopup" Visible="false">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td colspan="2" align="center">
                                                                                <asp:Image ID="Image11234" runat="server"  ImageUrl="~/Images/loading.gif" />
                                                                            </td>                                                
                                                                        </tr>                                           
                                                                        <tr>
                                                                            <td colspan="2">
                                                                            <label>
                                                                            <asp:Label ID="lblsid" runat="server" Visible="false"  Text=""></asp:Label>
                                                                              <asp:Label ID="lblcid" runat="server" Visible="false"  Text=""></asp:Label>
                                                                            <asp:Label ID="lblportmsg" runat="server" Text="Please waite for some moment we opening TCP port"></asp:Label>
                                                                            <%--<asp:LinkButton ID="link_openport" OnClick="btnsynTables"    runat="server" Text="" ></asp:LinkButton>--%>                                                           
                                                                                   </label> 
                                                                            </td>
                                                                        </tr>
                                                                    </table>                                   
                                                              </asp:Panel>
                                                               <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick">
                                                                        </asp:AsyncPostBackTrigger>
                                                                    </Triggers>                                                        
                                                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
               
                
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
