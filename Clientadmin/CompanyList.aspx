<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="CompanyList.aspx.cs" Inherits="Admin_CompanyList" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table id="subPage" cellpadding="0" cellspacing="0">
        <tr>
            <th>
                Company List&nbsp;</th>
        </tr>
    </table>
    <table id="innertbl1" cellpadding="0" cellspacing="0">
        <tr>
            <td class="column1">
            </td>
            <td class="column2">
            </td>
            <td class="column1" style="color: #333333">
            </td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column2" style="height: 450px" colspan="4">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"   PageSize="15" DataKeyNames="CompanyId">
                   <%-- OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"--%>
                  
                    <Columns>
                        <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderText="Edit" ImageUrl="~/images/edit.gif"
                            Text="Button" />
                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                        <asp:BoundField DataField="phone" HeaderText="phone" />
                        <asp:BoundField DataField="url" HeaderText="Domain" />
                        <asp:BoundField DataField="active" HeaderText="Active" />
                        <asp:BoundField DataField="PlanName" HeaderText="Plan" />
                        <asp:BoundField DataField="Amount" HeaderText="Price/Month" />
                    </Columns>
                    <HeaderStyle BackColor="DodgerBlue" Font-Bold="True" Font-Size="11px" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="column2" colspan="4" style="height: 15px">
            </td>
        </tr>
    </table>
</asp:Content>

