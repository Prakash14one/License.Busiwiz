<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DesignationWisePanelRights.aspx.cs" Inherits="DesignationWisePanelRights"
    Title="Panel Rights" %>

<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td style="width: 20%">
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Page Name"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 80%">
                                <label>
                                    <asp:DropDownList ID="ddlpagename" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagename_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Accounting</asp:ListItem>
                                        <asp:ListItem Value="2">Filling Cabinet</asp:ListItem>
                                        <asp:ListItem Value="3">office Management</asp:ListItem>
                                        <asp:ListItem Value="4">Communication</asp:ListItem>
                                        <asp:ListItem Value="5">Time Keeping</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Business Name"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlbusinessname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusinessname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView AutoGenerateColumns="False" ID="GridView1" runat="server" Width="100%"
                                    EmptyDataText="No Record Found." CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt" OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Department-Designation Name" ItemStyle-Width="35%" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrddeptname123" runat="server" Text='<%# Eval("Departmentname")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbldesignation123" runat="server" Text='<%# Eval("DesignationName")%>'></asp:Label>
                                                <asp:Label ID="lbldesignationid" runat="server" Text='<%# Eval("DesignationMasterId")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Panel 1" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label3panel1" runat="server" Text="Panel 1"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblpanel1hdr" runat="server"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxpanel1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Panel 2" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label3panel2" runat="server" Text="Panel 2"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblpanel2hdr" runat="server"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxpanel2" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Panel 3" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label3panel3" runat="server" Text="Panel 3"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblpanel3hdr" runat="server"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxpanel3" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Panel 4" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                         <HeaderTemplate>
                                                <asp:Label ID="Label3panel4" runat="server" Text="Panel 4"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblpanel4hdr" runat="server"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxpanel4" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Panel 5" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                         <HeaderTemplate>
                                                <asp:Label ID="Label3panel5" runat="server" Text="Panel 5"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblpanel5hdr" runat="server"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxpanel5" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Panel 6" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                         <HeaderTemplate>
                                                <asp:Label ID="Label3panel6" runat="server" Text="Panel 6"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblpanel6hdr" runat="server"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxpanel6" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Panel 7" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                                <asp:Label ID="Label3panel7" runat="server" Text="Panel 7"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblpanel7hdr" runat="server"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxpanel7" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Panel 8" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                         <HeaderTemplate>
                                                <asp:Label ID="Label3panel8" runat="server" Text="Panel 8"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblpanel8hdr" runat="server"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxpanel8" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="Button1_Click" />
                                <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Update" Visible="false"
                                    OnClick="Button2_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
