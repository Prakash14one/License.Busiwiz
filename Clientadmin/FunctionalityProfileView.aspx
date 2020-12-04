<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="FunctionalityProfileView.aspx.cs" Inherits="FunctionalityProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" GroupingText="Functionality Profile">
                
                <table style="width: 100%">
                    <tr>
                        <td width="25%">
                         <label>
                            <asp:Label ID="Label11" runat="server"  Text="Product/Version"></asp:Label> </label>
                        </td>
                        <td width="25%">
                            <asp:DropDownList ID="ddlversion" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlversion_SelectedIndexChanged" Width="369px">
                            </asp:DropDownList>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                         <label>
                            <asp:Label ID="Label13" runat="server" Text="Functionality Title"></asp:Label> </label>
                        </td>
                        <td width="25%">
                            <asp:DropDownList ID="ddlfuncti" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlfuncti_SelectedIndexChanged" Width="369px">
                            </asp:DropDownList>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%" align="left">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                         <label>
                            <asp:Label ID="Label14" runat="server" Text="Functionality Title"></asp:Label> </label>
                        </td>
                        <td align="left" width="25%">
                            <asp:TextBox ID="txtfuncti" runat="server" Width="369px"></asp:TextBox>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                         <label>
                            <asp:Label ID="Label15" runat="server"  Text="Functionality Description"></asp:Label> </label>
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="txtfundesc" runat="server" Height="50px" TextMode="MultiLine" 
                                Width="369px"></asp:TextBox>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                         <label>
                            <asp:Label ID="Label16" runat="server"  Text="Attachments"></asp:Label> </label>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                CssClass="mGrid" Width="50%">
                                <Columns>
                                    <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                    <asp:BoundField DataField="FileTitle" HeaderText="File Title" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td width="16.66%">
                            &nbsp;</td>
                        <td width="16.66%">
                            &nbsp;</td>
                        <td width="16.66%">
                            &nbsp;</td>
                        <td width="16.66%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                AllowPaging="True" PagerStyle-CssClass="prg" PageSize="10"  
                                CssClass="mGrid" Width="100%" DataKeyNames="PageId"
                                onpageindexchanging="GridView1_PageIndexChanging" 
                                onrowcommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Rank">
                                        <ItemTemplate>
                                             <asp:Label ID="lbl27" runat="server" Text='<%#Eval("OrderNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Page Title">
                                        <ItemTemplate>
                                        
                                           <asp:Label ID="Label19" runat="server" Text='<%#Eval("PageTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Page Name">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="Gray" CommandArgument='<%# Eval("PageId") %>'
                                                                                            CommandName="View1"  Text='<%# Eval("PageName") %>'
                                                                    ToolTip="View Page">LinkButton</asp:LinkButton>
                                           <%-- <asp:Label ID="Label20" runat="server" Text='<%#Eval("PageName") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField HeaderText="Latest Version">
                                        <ItemTemplate>
                                            <asp:Label ID="Label21" runat="server" Text='<%#Eval("VersionNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    
                                </Columns>
                                 <PagerStyle CssClass="pgr" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" ForeColor="#416271" Font-Size="15px"></asp:Label>
              </div>
</asp:Content>

