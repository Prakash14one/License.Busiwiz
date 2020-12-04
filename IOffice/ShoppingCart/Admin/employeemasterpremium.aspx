<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="employeemasterpremium.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <table width="100%">
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label1" runat="server" Text="Quick Entry of Employee Information" Font-Size="16px"></asp:Label>
                            </label>
                        </td>
                        <td valign="bottom">
                            <asp:Button ID="Button1" runat="server" Text="Quick Entry" CssClass="btnSubmit" 
                                onclick="Button1_Click" />
                        </td>
                        <td>
                            <label>
                                <asp:Label ID="Label2" runat="server" Text="Detailed Entry of Employee Information" Font-Size="16px"></asp:Label>
                            </label>
                        </td>
                        <td valign="bottom">
                            <asp:Button ID="Button2" runat="server" Text="Detailed Entry" 
                                CssClass="btnSubmit" onclick="Button2_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <label>
                                <asp:Label ID="Label3" runat="server" Text="Please note that when you use the quick entry option, you will fill in all basic information for your employee. However, more detailed requirements (i.e. employee, designation, department etc.) will be set to a default account. This information can be changed at any time through the necessary pages."></asp:Label>
                            </label>
                        </td>
                        <td colspan="2">
                            <label>
                                <asp:Label ID="Label4" runat="server" Text="Please note that when you use the detailed entry option, you will be required to fill out all necessary information for the employee you are adding. This process may take some time, as you are filling out all basic employee information, as well as all information pertinent to their position within your business (i.e. payroll, department, designation, role, page access ect.)"></asp:Label>
                            </label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
