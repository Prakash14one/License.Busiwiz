<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MessageView.aspx.cs" Inherits="Account_MessageView"
    Title="Untitled Page" %>

<%@ Register Src="~/Account/UserControl/MessageList.ascx" TagName="MsgList" TagPrefix="MsgList" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/ExternalInternalMessage1.ascx"
    TagName="extmsg" TagPrefix="extmsg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <table width="100%">
        <tr>
            <td>
                <pnlhelp:pnlhelp ID="pnlHlp" runat="server" />
            </td>
        </tr>
    </table>--%>
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
                <td colspan="3">
                    <extmsg:extmsg ID="msgwzd" runat="server" />
                    <%--View Messages--%>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 12%">
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="View Message" Font-Size="Large"></asp:Label>
                    </label>
                </td>
                <td>
                    <asp:Button ID="imgbtnreply" runat="server" Text="Reply" OnClick="imgbtnreply_Click"
                        CssClass="btnSubmit" />
                    <asp:Button ID="imgbtnfw" runat="server" Text="Forward" OnClick="imgbtnfw_Click"
                        CssClass="btnSubmit" />
                    <asp:Button ID="ImgBtnDelete" runat="server" Text="Delete" OnClick="ImgBtnDelete_Click"
                        CssClass="btnSubmit" />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 12%">
                    <MsgList:MsgList runat="server" ID="MsgListView" />
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                <asp:Label ID="Label2" runat="server" Text="From"></asp:Label>
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
                                                <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
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
                                                <asp:Label ID="Label4" runat="server" Text="Date"></asp:Label>
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
                                                <asp:Label ID="Label5" runat="server" Text="Subject"></asp:Label>
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
                                                <asp:Label ID="Label6" runat="server" Text="Attachment"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlgrid" runat="server">
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="left">
                                                            <asp:GridView GridLines="None" ShowHeader="false" ShowFooter="false" ID="GrdFileList"
                                                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdFileList_RowDataBound"
                                                                AllowPaging="True" OnPageIndexChanging="GrdFileList_PageIndexChanging" PageSize="5">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="No" ShowHeader="true" HeaderStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png" />
                                                                            <asp:Label runat="server" ID="TxtNo" Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ShowHeader="true" InsertVisible="False">
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
                                                <asp:Label ID="Label7" runat="server" Text="Message"></asp:Label>
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
                                                <%--<asp:ImageButton ID="imgbtnreply" runat="server" AlternateText="Reply" ImageUrl="~/Account/images/reply.jpg" OnClick="imgbtnreply_Click" />
                                        <asp:ImageButton ID="imgbtnfw" runat="server" AlternateText="Forward" ImageUrl="~/Account/images/forward.jpg" OnClick="imgbtnfw_Click" />
                                        <asp:ImageButton ID="ImgBtnDelete" runat="server" AlternateText="Delete"
                                            ImageUrl="~/Account/images/Delete_Msg.jpg" OnClick="ImgBtnDelete_Click"   />--%>
                                            </asp:Panel>
                                            <asp:Panel Width="100%" runat="server" ID="PnlDeletedMsg" Visible="false">
                                                <%--<asp:ImageButton ID="ImgBtnMovetoInbox" runat="server" AlternateText="Move to Inbox"
                                            ImageUrl="~/Account/images/MovetoInbox.jpg" OnClick="ImgBtnMovetoInbox_Click"     />--%>
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
