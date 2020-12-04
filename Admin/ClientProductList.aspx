<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="ClientProductList.aspx.cs" Inherits="ClientProductList" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="645px">
        <tr>
            <td class="hdng">
                Client's Product List :<br />
            </td>
        </tr>
        <tr>
            <td class="hdng">
                Client Name :
                <asp:DropDownList ID="ddlClientList" runat="server" AutoPostBack="True" DataTextField="CompanyName"
                    DataValueField="ClientMasterId" OnSelectedIndexChanged="ddlClientList_SelectedIndexChanged"
                    Width="222px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" Width="645px" Height="500px" ScrollBars="Auto">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="GridBack"
                        DataKeyNames="productDetailId" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderText="Edit" ImageUrl="~/images/edit.gif"
                                Text="Button" />
                            <asp:BoundField DataField="CompanyName" HeaderText="Client Name" />
                            <asp:BoundField DataField="productName" HeaderText="Product Name" />
                            <asp:BoundField DataField="productURL" HeaderText="Product URL" />
                            <asp:BoundField DataField="PricePlanURL" HeaderText="Price Plan URL" />
                            <asp:BoundField DataField="VersionNo" HeaderText="Version No" />
                            <asp:BoundField DataField="Active" HeaderText="Active" />
                            <asp:BoundField DataField="StartDate" DataFormatString="{0:d-MMM-yyyy}" HeaderText="Start Date" />
                            <asp:BoundField DataField="EndDate" DataFormatString="{0:d-MMM-yyyy}" HeaderText="End Date" />
                        </Columns>
                        <PagerStyle CssClass="GridPager" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                        <RowStyle CssClass="GridRowStyle" />
                        <FooterStyle CssClass="GridFooter" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
