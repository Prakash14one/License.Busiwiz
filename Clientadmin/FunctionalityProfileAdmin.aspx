<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="FunctionalityProfileAdmin.aspx.cs" Inherits="FunctionalityProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" GroupingText="Functionality Profile">
                
                <table style="width: 100%">
                    <tr>
                        <td width="25%">
                        <label>
                            <asp:Label ID="Label11" runat="server"  Text="Product/Version"></asp:Label></label>
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
                            <asp:Label ID="Label1" runat="server" Text="Functionality Category"></asp:Label> </label></td>
                        <td width="25%">
                           <asp:DropDownList ID="ddlfunctionalitycategory" runat="server" AutoPostBack="True" 
                                 Width="369px" 
                                onselectedindexchanged="ddlfunctionalitycategory_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <label>
                            <asp:Label ID="Label13" runat="server" Text="Functionality Title"></asp:Label>
                            </label>
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
                        <td width="25%" align="right">
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
                        <td width="25%" align="right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                CssClass="mGrid" Width="50%">
                                <Columns>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label23" runat="server" Text='<%#Eval("FileName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FileTitle" HeaderText="File Title" />
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton10" runat="server" ForeColor="Black" 
                                                onclick="LinkButton10_Click">Download</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td width="16.66%">
                            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td width="16.66%">
                            &nbsp;</td>
                        <td width="16.66%">
                            &nbsp;</td>
                        <td width="16.66%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="16.66%">
                            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
                                Text="Change Rank" UseSubmitBehavior="False" />
                        </td>
                        <td width="16.66%">
                            &nbsp;</td>
                        <td width="16.66%">
                            &nbsp;</td>
                        <td width="16.66%">
                            <asp:Button ID="Button3" runat="server" Text="Add New Page to Functionality" 
                                onclick="Button3_Click" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" CssClass="mGrid" 
                                onrowdatabound="GridView1_RowDataBound" PagerStyle-CssClass="prg" 
                                Width="100%" onpageindexchanging="GridView1_PageIndexChanging" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="pageid">
                                        <ItemTemplate>
                                            <asp:Label ID="Label22" runat="server" Text='<%#Eval("PageId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rank">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBox3" runat="server" AutoPostBack="True" Enabled="False" 
                                                ontextchanged="TextBox3_TextChanged" Text='<%#Eval("OrderNo") %>' Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Page Title">
                                        <ItemTemplate>
                                            <asp:Label ID="Label19" runat="server" Text='<%#Eval("PageTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Page Name">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton9" runat="server" 
                                                CommandArgument='<%# Eval("PageId") %>' CommandName="View1" ForeColor="Gray" 
                                                Text='<%# Eval("PageName") %>' ToolTip="View Page">LinkButton</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Latest Version">
                                        <ItemTemplate>
                                            <asp:Label ID="Label21" runat="server" Text='<%#Eval("VersionNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton3" runat="server" 
                                                ImageUrl="~/images/delete.gif" onclick="ImageButton3_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="Panel4" runat="server" BackColor="White" BorderColor="#999999" Width="1004px"
                                    Height="570px" BorderStyle="Solid" BorderWidth="10px" 
                    ScrollBars="Vertical">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                <table style="width: 100%; font-weight: bold; color: #000000;" bgcolor="#CCCCCC">
                                                    <tr>
                                                        <td bgcolor="#416271" align="left">
                                                            <asp:Label ID="Label32" runat="server" ForeColor="White" 
                                                                Text="Add Page to Functionality"></asp:Label>
                                                        </td>
                                                        <td align="right" bgcolor="#416271">
                                                            <asp:ImageButton ID="ImageButton2" runat="server" Height="15px" 
                                                                ImageUrl="~/images/closeicon.jpeg" Width="15px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel5" ScrollBars="Vertical" runat="server">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <asp:Label ID="lblmsg0" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <asp:Panel ID="Panel6" runat="server" GroupingText="Add Page ">
                                                                                <table style="width: 100%">
                                                                                    <tr>
                                                                                        <td width="25%">
                                                                                            <asp:Label ID="Label24" runat="server" Text="Category"></asp:Label>
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                                                                                                onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            &nbsp;</td>
                                                                                        <td width="25%">
                                                                                            &nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="25%">
                                                                                            <asp:Label ID="Label25" runat="server" Text="Main Menu"></asp:Label>
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                                                                                                onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            &nbsp;</td>
                                                                                        <td width="25%">
                                                                                            &nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="25%">
                                                                                            <asp:Label ID="Label26" runat="server" Text="Sub Menu"></asp:Label>
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            <asp:DropDownList ID="DropDownList3" runat="server">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            &nbsp;</td>
                                                                                        <td width="25%">
                                                                                            &nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="25%">
                                                                                            <asp:Label ID="Label27" runat="server" Text="Page Title"></asp:Label>
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            &nbsp;</td>
                                                                                        <td width="25%">
                                                                                            &nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="25%">
                                                                                            <asp:Label ID="Label29" runat="server" Text="Page Name"></asp:Label>
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            &nbsp;</td>
                                                                                        <td width="25%">
                                                                                            &nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="25%">
                                                                                            &nbsp;</td>
                                                                                        <td width="25%">
                                                                                            <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
                                                                                                Text="Add page " />
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            &nbsp;</td>
                                                                                        <td width="25%">
                                                                                            &nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="20%">
                                                                            &nbsp;</td>
                                                                        <td width="20%">
                                                                            &nbsp;</td>
                                                                        <td width="20%">
                                                                            &nbsp;</td>
                                                                        <td width="20%">
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <asp:Panel ID="Panel7" runat="server" GroupingText="Page List">
                                                                                <table style="width: 100%">
                                                                                    <tr>
                                                                                        <td>
                                                                                          <label> <asp:Label ID="Label30" runat="server" Text="Search"></asp:Label>
                                                                                            </label>
                                                                                            <label><asp:TextBox ID="TextBox6" runat="server" AutoPostBack="True" 
                                                                                                ontextchanged="TextBox6_TextChanged"></asp:TextBox></label> 
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                                                                                                CssClass="mGrid" Width="100%">
                                                                                                <Columns>
                                                                                                    <%--<asp:BoundField HeaderText="Category" DataField=""/>--%>
                                                                                                    <asp:BoundField DataField="MainMenuName" HeaderText="Main Menu" />
                                                                                                    <asp:BoundField DataField="SubMenuName" HeaderText="Submenu" />
                                                                                                    <asp:BoundField DataField="PageTitle" HeaderText="Page Title" />
                                                                                                    <asp:BoundField DataField="PageName" HeaderText="Page Name" />
                                                                                                    <asp:TemplateField HeaderText="Add">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Button ID="Button5" runat="server" onclick="Button5_Click" Text="Add" />
                                                                                                            <asp:Label ID="Label31" runat="server" Text='<%#Eval("PageId")%>' 
                                                                                                                Visible="False"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                   
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                               <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                                    Enabled="True" PopupControlID="Panel4" TargetControlID="HiddenButton2224" CancelControlID="ImageButton2">
                                </cc1:ModalPopupExtender>
                                <asp:Button ID="HiddenButton2224" runat="server" Style="display: none" /></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;</td>
                    </tr>
                </table>
                
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
     <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" ForeColor="#416271" Font-Size="15px"></asp:Label>
              </div>

</asp:Content>

