<%@ Page Title="" Language="C#" MasterPageFile="~/BeforeLogin/BeforLoginMasterPage.master" AutoEventWireup="true" CodeFile="HelpWizard.aspx.cs" Inherits="HelpWizard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
  <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        <table style="width: 100%">
         
            <tr>
                <td>
                    <asp:Label ID="Label18" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                    
                    <fieldset>
                    <legend>
                    
                        <asp:Label ID="mainmenu" runat="server" Font-Bold="True" Font-Size="17px" ></asp:Label></legend>
                        <asp:Panel ID="Panel1" runat="server" Width="100%">

                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                      <asp:DataList ID="DataList3" runat="server" Width="100%">
                  
                       <ItemTemplate>
                           <table width="100%">
                               <tr>
                                   <td>
                                        <div>
                    <asp:ImageButton ID="ImageButton1" runat="server" style="    margin-bottom: -12px;"
                        ImageUrl="~/images/minus.png" onclick="ImageButton1_Click" />
                    <asp:Label ID="Label1" runat="server"
                        Text='<%#Eval("SubMenuName")%>'  ForeColor="Black" Font-Bold="True" style="font-size: 15px;"></asp:Label>
                        <asp:Label ID="Label17" runat="server" Visible="False"
                        Text='<%# Eval("SubMenuId") %>' Font-Bold="False" ForeColor="White"></asp:Label>
                </div> 
                 </td>
                               </tr>
                               <tr>
                                   <td>
                                       <div >
                    <asp:Panel ID="Panel1" runat="server" >
                  
                   
                        <asp:DataList ID="DataList4" runat="server">
                            <ItemTemplate>
                              &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  <a href="http://help.busiwiz.com/Help_View.aspx?PageID=<%# Eval("PageId")%>">
                                                        </font color="black">
                                                        <b><%#Eval("PageTitle")%></b></a>
                                &nbsp;
                            </ItemTemplate>
                        </asp:DataList>
                  
                   
                    </asp:Panel>

                </div></td>
                               </tr>
                           </table>
                       </ItemTemplate>
                  
                   </asp:DataList></td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>

                        </asp:Panel>
                    </fieldset>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

