<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="BrandMaster.aspx.cs" Inherits="BrandMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="lblmsg" runat="server" ForeColor="#CC3300"></asp:Label>
            </td>
        </tr>
    </table>
     <fieldset>
        <legend>
            <asp:Label ID="Label19" runat="server" Text="Brand Add / Manage "></asp:Label>
        </legend>
        <div style="float: right;">
        <asp:Button ID="Button1" runat="server" Text="Add New Brand" onclick="Button1_Click" />
        </div>
        <div style="clear: both;">
        </div>
    <asp:Panel ID="Panel1" runat="server" >
        <table style="width: 100%">
       
            <tr>
                <td width="20%">
                <label>
                    <asp:Label ID="Label12" runat="server" Text="Name: "></asp:Label>
                     <asp:Label ID="Label63" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator178" runat="server" ControlToValidate="txtname" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                    </label>
                </td>
                <td width="20%">
                    <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                </td>
                <td align="right" colspan="3" style="width: 40%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="20%">
                <label>
                    <asp:Label ID="Label13" runat="server" Text=" Website Name: "></asp:Label>
                    </label>
                </td>
                <td width="20%">
                    <asp:TextBox ID="txtwebsite" runat="server"></asp:TextBox>
                </td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="20%">
                <label>
                    <asp:Label ID="Label14" runat="server" Text="Active: "></asp:Label>
                    </label>
                </td>
                <td width="20%">
                  <asp:CheckBox ID="chkboxActiveDeactive" runat="server" Text="Active" />
                    <asp:Label ID="Label15" runat="server"></asp:Label>
                </td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    <asp:Button ID="Btnsave" runat="server" Text="Save" onclick="Btnsave_Click"  ValidationGroup="1" />
                    <asp:Button ID="Btnupdate" runat="server" Text="Update" 
                        ValidationGroup="1" onclick="Btnupdate_Click"/> 
                    <asp:Button ID="Btncancel" runat="server" Text="Cancel" 
                        onclick="Btncancel_Click" />
                </td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
            </tr>
           
        </table>
    </asp:Panel>
    </fieldset>
    <asp:Panel ID="Panel2" runat="server" GroupingText="List Of Brand Name">
        <table style="width: 100%">
            <tr>
                <td align="left">
                     <label style="width:80px;">
                                <asp:Label ID="Label3" runat="server" Text="Search"></asp:Label>
                            </label>
                           <label style="width:350px;">
                              <asp:TextBox ID="txtsearch" runat="server"   placeholder=" Search here"  Font-Bold="true"   Width="350px"  ></asp:TextBox>
                              </label>
                            <label>
                             <asp:Button ID="BtnGo" CssClass="btnSubmit" runat="server" Text="Go" OnClick="BtnGo_Click"  />
                            </label> 
                </td>
            </tr>
            <tr>
                <td>

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                EmptyDataText="No Record Found."                  CssClass="mGrid" PagerStyle-CssClass="pgr" Width="100%" 
                                                onpageindexchanging="GridView1_PageIndexChanging" PageSize="20" AllowPaging="True">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Brand Name" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="WebsiteName" HeaderText="Website Name" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Active" HeaderText="Active" HeaderStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif"  HeaderStyle-HorizontalAlign="Left" HeaderText="Edit" ItemStyle-Width="3%">
                            <ItemTemplate>
                           <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Account/images/edit.gif" onclick="ImageButton3_Click" />
                           </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Left" />
                           <ItemStyle Width="3%" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderImageUrl="~/Account/images/delete.gif"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="3%" HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                             <asp:Label ID="Label15" runat="server"  Text='<%#Eval("ID") %>' Visible="False"></asp:Label>
                               <asp:ImageButton ID="imgdelete" runat="server" ImageUrl="~/Account/images/delete.gif" ToolTip="Delete" onclick="imgdelete_Click" />
                             </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Left" Width="3%" />
                          <ItemStyle HorizontalAlign="Left" />
                           </asp:TemplateField>
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
              <asp:Label ID="lblVersion" runat="server" ForeColor="#416271" Font-Size="10px"></asp:Label>
              </div>
</asp:Content>

