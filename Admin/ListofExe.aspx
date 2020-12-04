<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="ListofExe.aspx.cs" Inherits="Admin_ListofExe" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" border="0" cellspacing="0" width="645px">
        <tr>
            <td colspan="2">
                <strong>List of&nbsp; Projects to create exe </strong>
            </td>
            <td style="width: 3px">
                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Admin/ExeHelp.aspx">Help</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td style="width: 8px">
                Client Name :
            </td>
            <td>
                <asp:DropDownList ID="ddlClientList" runat="server" AutoPostBack="True" DataTextField="CompanyName"
                    DataValueField="ClientMasterId" OnSelectedIndexChanged="ddlClientList_SelectedIndexChanged"
                    Width="222px">
                </asp:DropDownList>
            </td>
            <td style="width: 3px">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Auto" Width="645px">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="GridBack"
                        DataKeyNames="productDetailExeId" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand"
                        AllowPaging="True" EmptyDataText="There is no data." OnPageIndexChanging="GridView1_PageIndexChanging"
                        PageSize="5">
                        <RowStyle CssClass="GridRowStyle" />
                        <Columns>
                            <asp:ButtonField ButtonType="Button" CommandName="edit1" HeaderText="Done" Text="Done" />
                            <asp:BoundField DataField="productName" HeaderText="Product Name" />
                            <asp:TemplateField HeaderText="Path to Open Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation" runat="server" BackColor="Transparent" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="UploadDate" DataFormatString="{0:d-MMM-yyyy}" HeaderText="Upload Date" />
                            <asp:BoundField DataField="productURL" HeaderText="Product URL" />
                            <asp:BoundField DataField="PricePlanName" HeaderText="Price Plan Name" />
                            <asp:BoundField DataField="VersionNo" HeaderText="Version No" />
                            <%--<asp:BoundField DataField="Active" HeaderText="Active" />--%>
                        </Columns>
                        <FooterStyle CssClass="GridFooter" />
                        <PagerStyle CssClass="GridPager" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </td>
            <td style="width: 3px">
            </td>
        </tr>
        <tr>
            <td style="width: 8px">
            </td>
            <td style="width: 3px">
            </td>
            <td style="width: 3px">
            </td>
        </tr>
    </table>
</asp:Content>
