<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExternalInternalMessage.ascx.cs"
    Inherits="Account_UserControl_ExternalInternalMessageacc" %>
<%--<link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />--%>
<asp:Panel ID="Panel2" runat="server">
    <label>
        <asp:Label ID="Label1" runat="server" Text="External Message Center" Font-Size="Large"></asp:Label>
    </label>
</asp:Panel>
<br />
<div style="float: right;">
    <asp:Button ID="btnexternal" runat="server" CssClass="btnSubmit" Text="Internal Messaging Center"
        OnClick="btnexternal_Click" />
</div>
<table cellpadding="0" cellspacing="0" id="wizPanel">
    <tr>
        <td colspan="2" style="height: 10px">
            <%--<asp:RadioButton ID="RadioButton1" runat="server" Text="Switch to Internal Message Center"
                OnCheckedChanged="RadioButton1_CheckedChanged" Checked="false" AutoPostBack="True" />--%>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="column2" id="td1" runat="server" visible="false">
            <asp:RadioButtonList ID="rbtnlistsetrules" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                OnSelectedIndexChanged="rbtnlistsetrules_SelectedIndexChanged" OnTextChanged="rbtnlistsetrules_TextChanged">
                <asp:ListItem>Internal Message</asp:ListItem>
                <asp:ListItem>External Message</asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <%-- <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnStep13" runat="server" BackColor="LightGray"  Text="Step 13"  ImageUrl="~/Account/images/BtnStep13.jpg"   ForeColor="Black" OnClick="btnStep13_Click"     />
                </td>
                 <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnStep14" runat="server" BackColor="LightGray"  Text="Step 14"   ForeColor="Black" ImageUrl="~/Account/images/BtnStep14.jpg"  OnClick="btnStep14_Click"    />
                </td>--%>
    </tr>
    <tr id="tr1" runat="server" visible="false">
        <td>
            Select EmailId :
        </td>
        <td>
            <asp:DropDownList ID="ddlempemail" runat="server" AutoPostBack="True" DataTextField="EmailId"
                DataValueField="CompanyEmailId" Visible="false" OnSelectedIndexChanged="ddlempemail_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
</table>
