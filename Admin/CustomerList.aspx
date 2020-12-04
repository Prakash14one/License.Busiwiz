<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="CustomerList.aspx.cs" Inherits="CustomerListAdmin" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" border="0" cellspacing="0" width="645px">
        <tr>
            <td class="hdng" colspan="4">
                Customer List
            </td>
        </tr>
        <tr>
            <td width="75px">
                Client Name :
            </td>
            <td class="column2">
                <asp:DropDownList ID="ddlClientList" runat="server" AutoPostBack="True" DataTextField="CompanyName"
                    DataValueField="ClientMasterId" OnSelectedIndexChanged="ddlClientList_SelectedIndexChanged"
                    Width="222px">
                </asp:DropDownList>
            </td>
            <td class="column1">
            </td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column2" colspan="4">
                <asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Auto" Width="645px">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="CompanyId" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                        EmptyDataText="There is no data." PageSize="15" CssClass="GridBack">
                        <Columns>
                            <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderText="Edit" ImageUrl="~/images/edit.gif"
                                Text="Button" />
                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                            <asp:BoundField DataField="HostName" HeaderText="Server Name" />
                            <asp:BoundField DataField="phone" HeaderText="phone" />
                            <asp:BoundField DataField="ProductURL" HeaderText="Domain" />
                            <%-- <asp:BoundField DataField="active" HeaderText="Active" />--%>
                            <asp:BoundField DataField="PricePlanName" HeaderText="Plan" />
                            <asp:BoundField DataField="PricePlanAmount" HeaderText="Price/Month" />
                            <asp:BoundField DataField="LicenseKey" HeaderText="LicenseKey" />
                            <asp:BoundField DataField="AllowIPTrack" HeaderText="Allow IP Track" />
                            <asp:BoundField DataField="GBUsage" HeaderText="Usage in GB" />
                            <asp:BoundField DataField="MaxUser" HeaderText="Maximun User Allow" />
                            <asp:BoundField DataField="TrafficinGB" HeaderText="Traffic in GB" />
                            <asp:BoundField DataField="TotalMail" HeaderText="Total no of Mail Allow" />
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
        <tr>
            <td class="column2" colspan="4">
            </td>
        </tr>
    </table>
</asp:Content>
