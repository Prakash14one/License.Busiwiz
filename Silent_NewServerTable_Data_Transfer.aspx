<%@ Page Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true"
    CodeFile="Silent_NewServerTable_Data_Transfer.aspx.cs" Inherits="AccessRight" Title="Page Access Right" %>

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
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Pending synchronisation with client databases."></asp:Label>
                    </legend>
                    <table width="100%">
                        
                        
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnltransst" runat="server" Visible="false">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="lbllegendd" runat="server" Text="List of Server Syncronisation Required"></asp:Label>
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="grdserver" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"
                                                        EmptyDataText="No data required to be synchronise right now" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" Width="100%" 
                                                         AllowSorting="True" >
                                                        <%--PageSize="15" AllowPaging="True" OnPageIndexChanging="grdserver_PageIndexChanging" OnSorting="grdserver_Sorting" --%>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Satellite Server Name" SortExpression="ServerName" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("ServerName") %>'></asp:Label>  
                                                                    <asp:Label ID="lbl_TableId" runat="server" Text='<%# Eval("TableId") %>'></asp:Label>  
                                                                                                                                      
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Server Location" SortExpression="serverloction" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblserverloction" runat="server" Text='<%# Eval("serverloction") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sync requiring table name" SortExpression="tabdesname"  HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltabname" runat="server" Text='<%# Eval("TableName") %>' Visible="true"></asp:Label>
                                                                    <asp:Label ID="lblsyncreq" runat="server" Text='<%# Eval("tabdesname") %>' Visible="false" ></asp:Label>
                                                                    <asp:Label ID="lblseid" runat="server" Text='<%# Eval("servermasterID") %>' Visible="false"></asp:Label>
                                                                    <%--  <asp:Label ID="lblpoid" runat="server" Text='<%# Eval("PortalId") %>' Visible="false"></asp:Label>--%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sync requested date" SortExpression="DateandTime"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldt" runat="server" Text='<%# Eval("DateandTime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Failed Attempt" SortExpression="DateandTime"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmsgatt" runat="server" Text='<%# Eval("Msg") %>'></asp:Label>
                                                                     <asp:Label ID="lblattempt" runat="server" Text='<%# Eval("Attempt") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="cbHeader" runat="server" Checked="true" />
                                                                    <asp:Label ID="check" runat="server" ForeColor="White" Text="Sync All" />
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
                                                <td align="center">
                                                    <asp:Button ID="btnsync" runat="server" Text="Syncronise" CssClass="btnSubmit" 
                                                        OnClick="btnsync_Click" Visible="False" />
                                                      <input id="hdnsortExp1" type="hidden" name="hdnsortExp1" style="width: 3px" runat="Server" />
                                                <input id="hdnsortDir1" type="hidden" name="hdnsortDir1" style="width: 3px" runat="Server" />
                                                <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                                <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                                <input id="hdnPricePlanId" name="hdnPricePlanId" runat="server" type="hidden" style="width: 1px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
