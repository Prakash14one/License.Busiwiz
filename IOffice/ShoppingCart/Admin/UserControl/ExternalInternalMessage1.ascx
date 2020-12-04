<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExternalInternalMessage1.ascx.cs"
    Inherits="Account_UserControl_ExternalInternalMessage1" %>
<asp:Panel ID="Panel2" runat="server">
    <label>
        <asp:Label ID="Label1" runat="server" Text="Internal Message Center" Font-Size="Large"></asp:Label>
    </label>
</asp:Panel>
<br />
<div style="float: right;">
    <asp:Button ID="btnexternal" runat="server" CssClass="btnSubmit" Text="External Messaging Center"
        OnClick="btnexternal_Click" />
</div>
<table cellpadding="0" cellspacing="0" id="wizPanel">
    <%--<tr>
        <td colspan="2" style="height: 10px">
            </td>
    </tr>--%>
    <tr>
        <%--<td>
            <asp:RadioButton ID="RadioButton2" runat="server" Text="Switch to External Message Center"
                OnCheckedChanged="RadioButton2_CheckedChanged" Checked="false" AutoPostBack="True" />
        </td>--%>
        <td>
            <asp:DropDownList ID="ddlempemail" runat="server" AutoPostBack="True" DataTextField="EmailId"
                DataValueField="CompanyEmailId" Visible="False">
            </asp:DropDownList>
        </td>
        <%-- <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnStep13" runat="server" BackColor="LightGray"  Text="Step 13"  ImageUrl="~/Account/images/BtnStep13.jpg"   ForeColor="Black" OnClick="btnStep13_Click"     />
                </td>
                 <td style="width: 3px; height: 36px">
                    <asp:ImageButton ID="btnStep14" runat="server" BackColor="LightGray"  Text="Step 14"   ForeColor="Black" ImageUrl="~/Account/images/BtnStep14.jpg"  OnClick="btnStep14_Click"    />
                </td>--%>
    </tr>
    <tr>
    </tr>
</table>
