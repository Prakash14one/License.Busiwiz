<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="MessageViewExt.aspx.cs" Inherits="Account_MessageViewExt"
    Title="Untitled Page" %>

<%@ Register Src="~/Account/UserControl/MessageList1.ascx" TagName="MsgList" TagPrefix="MsgList" %>
<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Src="~/ShoppingCart/Admin/UserControl/ExternalInternalMessage1.ascx"
    TagName="extmsg" TagPrefix="extmsg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="products_box">
        <table width="100%">
            <tr>
                <td colspan="3">
                    <asp:Panel ID="Panel1" runat="server" Visible="False" Width="100%">
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="hdr" colspan="3">
                    <extmsg:extmsg ID="msgwzd" runat="server" />
                </td>
            </tr>
        </table>
        <%--  <table width="100%">
            <tr>
                <td>
                    <pnlhelp:pnlhelp ID="pnlHlp" runat="server" />
                </td>
            </tr>
        </table>--%>
        <table width="100%">
            <tr>
                <td style="width: 12%">
                    <label>
                        View Message
                    </label>
                </td>
                <td>
                    <asp:Button ID="imgbtnreply" runat="server" Text="Reply" CssClass="btnSubmit" OnClick="imgbtnreply_Click" />
                    <asp:Button ID="imgbtnfw" runat="server" Text="Forward" CssClass="btnSubmit" OnClick="imgbtnfw_Click" />
                    <asp:Button ID="ImgBtnDelete" runat="server" Text="Delete" CssClass="btnSubmit" OnClick="ImgBtnDelete_Click" />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 12%">
                    <MsgList:MsgList runat="server" ID="MsgListView" />
                </td>
                <td>
                    <table id="msgboxcontent" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="2">
                                <table id="tomsg" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                From
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblfrom" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                To
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblto" runat="server">Me</asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                Date
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lbldate" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                Subject
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblsubject" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                Attachment
                                            </label>
                                        </td>
                                        <td valign="bottom">
                                            <asp:Panel ID="pnlgrid" runat="server">
                                                <table id="msgridpnl" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:GridView GridLines="None" ShowHeader="false" ShowFooter="false" ID="GrdFileList"
                                                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                runat="server" AutoGenerateColumns="False" OnRowDataBound="GrdFileList_RowDataBound"
                                                                AllowPaging="True" OnPageIndexChanging="GrdFileList_PageIndexChanging" PageSize="5">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="No">
                                                                        <ItemTemplate>
                                                                            <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png" />
                                                                            <asp:Label runat="server" ID="TxtNo"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ShowHeader="False" InsertVisible="False">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="FileOpen" runat="server" Target="_blank" Text='<%# Eval("FileName") %>'
                                                                                NavigateUrl='<%# Eval("FileName") %>' ForeColor="Black"></asp:HyperLink>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                Message
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                <asp:Label ID="Label8" runat="server" Text="Signature"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblsignature" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                <asp:Label ID="Label9" runat="server" Text="Photo"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Image ID="image1" runat="server" Height="70px" Width="70px" />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                        </td>
                                        <td>
                                            <asp:Panel runat="server" Width="100%" ID="PnlInboxMsg">
                                            </asp:Panel>
                                            <asp:Panel Width="100%" runat="server" ID="PnlDeletedMsg" Visible="false">
                                                <asp:Button ID="ImgBtnMovetoInbox" runat="server" Text="Move to Inbox" CssClass="btnSubmit"
                                                    OnClick="ImgBtnMovetoInbox_Click" />
                                            </asp:Panel>
                                            <input id="hdnFromPartyId" runat="server" name="hdnFromPartyId" style="width: 1px"
                                                type="hidden" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
