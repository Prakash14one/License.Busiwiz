<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MessageViewSent.aspx.cs" Inherits="Account_MessageViewSent"
    Title="Untitled Page" %>

<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UControlWizardpanel.ascx" TagName="pnl"
    TagPrefix="pnl" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Src="~/Account/UserControl/MessageList1.ascx" TagName="MsgList" TagPrefix="MsgList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="products_box">
        <table width="100%">
            <tr>
                <td colspan="2">
                    <asp:Panel ID="Panel1" runat="server" Visible="False" Width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="width: 12%">
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="View Message" Font-Size="Large"></asp:Label>
                    </label>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Send" CssClass="btnSubmit" OnClick="Button1_Click"
                        Visible="false" />
                    <asp:Button ID="Button2" runat="server" Text="Save As Draft" CssClass="btnSubmit"
                        OnClick="Button2_Click" Visible="false" />
                    <asp:Button ID="Button3" runat="server" Text="Discard" CssClass="btnSubmit" OnClick="Button3_Click"
                        Visible="false" />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 12%">
                    <MsgList:MsgList runat="server" ID="MsgListView" />
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 20%">
                                <label>
                                    From
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="lblFrom" runat="server">Me</asp:Label>
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
                                    <asp:Label ID="lblto" runat="server"></asp:Label>
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
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GrdFileList" runat="server" AutoGenerateColumns="False" OnRowDataBound="GrdFileList_RowDataBound"
                                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    Width="100%" PageSize="5" ShowFooter="false" ShowHeader="false" AllowPaging="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="No">
                                                            <ItemTemplate>
                                                                <asp:Image ID="ImgFIleHeader" runat="server" ImageUrl="~/Account/images/attach.png" />
                                                                <asp:Label ID="TxtNo" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="FileOpen" runat="server" NavigateUrl='<%# Eval("FileName") %>'
                                                                    Target="_blank" Text='<%# Eval("FileName") %>' ForeColor="Black"></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Label ID="lblAttachment" runat="server">None</asp:Label>
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
                            </td>
                            <td>
                                <asp:Button ID="Button4" runat="server" Text="Reply" CssClass="btnSubmit" OnClick="Button4_Click"
                                    Visible="false" />
                                <asp:Button ID="imgbtnfw" runat="server" Text="Forward" CssClass="btnSubmit" OnClick="imgbtnfw_Click1"
                                    Visible="false" />
                                <asp:Button ID="Button5" runat="server" Text="Save As Draft" CssClass="btnSubmit"
                                    Visible="false" OnClick="Button5_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
