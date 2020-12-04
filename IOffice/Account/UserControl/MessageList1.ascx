<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageList1.ascx.cs" Inherits="Account_UserControl_MessageList1acc" %>
  <table id="lftpnl" cellspacing="0">
  <tr>
                        <td class="col1">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Account/images/inbox.jpg" AlternateText="Inbox" /></td>
                        <td class="col2" style="width: 40px">
                            <asp:LinkButton ID="lbtnCompose" runat="server" PostBackUrl="~/ShoppingCart/Admin/MessageComposeExt.aspx">Compose</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td class="col1">
                            <asp:Image ID="imginbox" runat="server" ImageUrl="~/Account/images/inbox.jpg" AlternateText="Inbox" /></td>
                        <td class="col2" style="width: 40px">
                            <asp:LinkButton ID="lbtninbox" runat="server" PostBackUrl="~/ShoppingCart/Admin/MessageInboxExt.aspx">Inbox</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td class="col1">
                            <asp:Image ID="imgsent" runat="server" ImageUrl="~/Account/images/sent.jpg" AlternateText="Sent" /></td>
                        <td class="col2" style="width: 40px">
                            <asp:LinkButton ID="lbtnsent" runat="server" PostBackUrl="~/ShoppingCart/Admin/MessageSentExt.aspx">Sent</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td class="col1">
                            <asp:Image ID="imgdrafts" runat="server" ImageUrl="~/Account/images/drafts.jpg" AlternateText="Drafts" /></td>
                        <td class="col2" style="width: 40px">
                            <asp:LinkButton ID="lbtndrafts" runat="server" PostBackUrl="~/ShoppingCart/Admin/MessageDraftsExt.aspx">Drafts</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td class="col1" style="height: 21px">
                            <asp:Image ID="imgtrash" runat="server" ImageUrl="~/Account/images/trash.jpg" AlternateText="Trash" /></td>
                        <td class="col2" style="height: 21px; width: 40px;">
                            <asp:LinkButton ID="lbtntrash" runat="server" PostBackUrl="~/ShoppingCart/Admin/MessageDeletedItemsExt.aspx">Trash</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td class="col1">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Account/images/drafts.jpg" AlternateText="Drafts" /></td>
                        <td class="col2" style="width: 40px">
                            <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/ShoppingCart/Admin/EmailSpamlist.aspx">Spam</asp:LinkButton></td>
                    </tr>
                    <%--<tr>
                    <td class="col1"></td>
                    <td>
                      <asp:DataList ID="DataList1" runat="server" RepeatColumns="10"
                                        ShowFooter="False" ShowHeader="False" Height="16px">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lbllink" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Foldername")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        </asp:DataList>
                                        </td>
                    </tr>--%>
                </table>