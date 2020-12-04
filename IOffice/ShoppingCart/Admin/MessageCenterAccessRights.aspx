<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MessageCenterAccessRights.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
        .style1
        {
            height: 28px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 2%">
                    <asp:Label runat="server" ID="lblmsg" Text="" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Select Business"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlStore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstore_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:CheckBox ID="CheckBox3" runat="server" Text="Active" AutoPostBack="true" Checked="true"  OnCheckedChanged="ddlstore_SelectedIndexChanged" />
                                </label> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                                    OnRowCreated="GridView1_RowCreated1">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Department" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                                <asp:Label ID="lbldeptid" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="Label112" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                                                <asp:Label ID="lbldesigid" runat="server" Text='<%# Eval("DesignationMasterId") %>'
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apply to All Business" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkbusiness" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Apply to All Business"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Admin" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkadmin" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Admin"></asp:Label>
                                                <%--<asp:CheckBox ID="HeaderChkboxAdmin" runat="server" Text="Admin" AutoPostBack="True"
                                                    OnCheckedChanged="HeaderChkboxAdmin_CheckedChanged" />--%>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Candidate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCandidate" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label3" runat="server" Text="Candidate"></asp:Label>
                                                <%--<asp:CheckBox ID="HeaderChkboxCandidate" runat="server" Text="Candidate" OnCheckedChanged="HeaderChkboxCandidate_CheckedChanged" />--%>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEmployee" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label4" runat="server" Text="Employee"></asp:Label>
                                                <%--<asp:CheckBox ID="HeaderChkboxEmployee" runat="server" Text="Employee" OnCheckedChanged="HeaderChkboxEmployee_CheckedChanged" />--%>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCustomer" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label5" runat="server" Text="Customer"></asp:Label>
                                                <%--<asp:CheckBox ID="HeaderChkboxCustomer" runat="server" Text="Customer" OnCheckedChanged="HeaderChkboxCustomer_CheckedChanged" />--%>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vendor" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkVendor" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label6" runat="server" Text="Vendor"></asp:Label>
                                                <%--<asp:CheckBox ID="HeaderChkboxVendor" runat="server" Text="Vendor" OnCheckedChanged="HeaderChkboxVendor_CheckedChanged" />--%>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Others" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkOther" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label7" runat="server" Text="Others"></asp:Label>
                                                <%--<asp:CheckBox ID="HeaderChkboxOther" runat="server" Text="Others" OnCheckedChanged="HeaderChkboxOther_CheckedChanged" />--%>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Visitor" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkVisitor" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label7sd" runat="server" Text="Visitor"></asp:Label>
                                                <%--<asp:CheckBox ID="HeaderChkboxOther" runat="server" Text="Others" OnCheckedChanged="HeaderChkboxOther_CheckedChanged" />--%>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
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
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btnsubmit_Click" />
                            </td>
                        </tr>
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
