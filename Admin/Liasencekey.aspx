<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Liasencekey.aspx.cs" Inherits="Admin_Liasencekey" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            <td colspan="3" style="text-align: center">
                <strong>Issue Licence To Customer</strong></td>
        </tr>
        <tr>
            <td style="width: 114px; text-align: right; height: 39px;" valign="bottom">
                Company Name:</td>
            <td style="width: 100px; height: 39px;" valign="middle">
                <asp:DropDownList ID="ddlcompanyid" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcompanyid_SelectedIndexChanged"
                    Width="144px">
                </asp:DropDownList></td>
            <td style="width: 100px; height: 39px;" valign="top">
                <asp:RadioButtonList ID="rbcompanylicense" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbcompanylicense_SelectedIndexChanged"
                    RepeatDirection="Horizontal">
                    <asp:ListItem>ALL</asp:ListItem>
                    <asp:ListItem>LicenceKeyRemaing</asp:ListItem>
                    <asp:ListItem>LicencekeyGiven</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td style="width: 114px; text-align: right">
                Address:</td>
            <td colspan="2">
                <asp:Label ID="labcompadd" runat="server" Text="Label" Width="101px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 114px; text-align: right">
                Phone No.</td>
            <td colspan="2">
                <asp:Label ID="labcompphoneno" runat="server" Text="Label" Width="101px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 114px; text-align: right">
                Product Name:</td>
            <td colspan="2">
                <asp:Label ID="lbproductname" runat="server" Text="Label" Width="101px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 114px; text-align: right">
                Product Prize Plan:</td>
            <td colspan="2">
                <asp:Label ID="lbproductprizeplan" runat="server" Text="Label" Width="104px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 114px; text-align: right">
                Liacense Key:</td>
            <td colspan="2">
                <asp:Label ID="lbliacencekey" runat="server" Text="Label" Width="210px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 114px; text-align: right">
                URL:</td>
            <td colspan="2">
                <asp:Label ID="lburl" runat="server" Text="Label"></asp:Label>
                <asp:HyperLink ID="hlurl" runat="server" Visible="False">[hlurl]</asp:HyperLink></td>
        </tr>
        <tr>
            <td style="width: 114px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td style="width: 114px">
            </td>
            <td colspan="2">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
    </table>
    <asp:Panel ID="panforlickey" runat="server" Width ="550px" Visible="False">
        <table>
            <tr>
                <td style="width: 111px; text-align: right;">
                    Liacense Key:</td>
                <td colspan="2">
                    <asp:DropDownList ID="ddllickey" runat="server" Width="213px">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 111px">
                </td>
                <td style="width: 100px; text-align: center;">
                    <asp:Button ID="btnkeyissued" runat="server" Text="Issued Key" OnClick="btnkeyissued_Click" /></td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 111px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

