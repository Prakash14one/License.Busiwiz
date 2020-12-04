<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageList.ascx.cs" Inherits="Account_UserControl_MessageListacc" %>
  <table id="lftpnl" cellspacing="0">
  <tr>
                        <td class="col1">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Account/images/inbox.jpg" AlternateText="Inbox" /></td>
                        <td class="col2" style="width: 40px">
                            <asp:LinkButton ID="lbtnCompose" runat="server" PostBackUrl="~/ShoppingCart/Admin/MessageCompose.aspx">Compose</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td class="col1">
                            <asp:Image ID="imginbox" runat="server" ImageUrl="~/Account/images/inbox.jpg" AlternateText="Inbox" /></td>
                        <td class="col2" style="width: 40px">
                            <asp:LinkButton ID="lbtninbox" runat="server" PostBackUrl="~/ShoppingCart/Admin/MessageInbox.aspx">Inbox</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td class="col1">
                            <asp:Image ID="imgsent" runat="server" ImageUrl="~/Account/images/sent.jpg" AlternateText="Sent" /></td>
                        <td class="col2" style="width: 40px">
                            <asp:LinkButton ID="lbtnsent" runat="server" PostBackUrl="~/ShoppingCart/Admin/MessageSent.aspx">Sent</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td class="col1">
                            <asp:Image ID="imgdrafts" runat="server" ImageUrl="~/Account/images/drafts.jpg" AlternateText="Drafts" /></td>
                        <td class="col2" style="width: 40px">
                            <asp:LinkButton ID="lbtndrafts" runat="server" PostBackUrl="~/ShoppingCart/Admin/MessageDrafts.aspx">Drafts</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td class="col1" style="height: 21px">
                            <asp:Image ID="imgtrash" runat="server" ImageUrl="~/Account/images/trash.jpg" AlternateText="Trash" /></td>
                        <td class="col2" style="height: 21px; width: 40px;">
                            <asp:LinkButton ID="lbtntrash" runat="server" PostBackUrl="~/ShoppingCart/Admin/MessageDeletedItems.aspx">Trash</asp:LinkButton></td>
                    </tr>
                </table>
