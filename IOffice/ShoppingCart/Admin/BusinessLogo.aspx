<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="BusinessLogo.aspx.cs" Inherits="ShoppingCart_Admin_TimeKeeperCompanyLogo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlpvt" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="lblmsg" ForeColor="Red" runat="server" Text=""></asp:Label>
            </div>
            <fieldset>
                <%--<legend>
                    <asp:Label ID="Label19" runat="server" Text="2. Please change your company logo if required"></asp:Label></legend>--%>
                <label>
                    <asp:Label ID="lblLogo" runat="server" Text="Existing Logo " Visible="false"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Visible="false" ForeColor="Black"></asp:Label>
                </label>
                <div style="clear: both;">
                </div>
                <br />
                <asp:Panel ID="pnllogo" runat="server" Width="180px">
                    <table>
                        <tr>
                            <td>
                                <asp:Image ID="imgLogo" runat="server" Height="106px" Width="176px" 
                                    Visible="False" />
                            </td>
                            <td valign="bottom">
                                <asp:Button ID="btnChange" Text="Change" runat="server" CssClass="btnSubmit" OnClick="btnChange_Click"
                                    Visible="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div style="clear: both;">
                </div>
                <br />
                <asp:Panel ID="Panel1" runat="server" Visible="false">
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="Select the file you wish to upload. (Recommended image size: 176 x 106 pixels)"></asp:Label>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <br />
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="FileUpload1"
                        ErrorMessage="*" ValidationGroup="7412">
                    </asp:RequiredFieldValidator>
                    <asp:Button ID="imgBtnImageUpdate" Text="Update Image" runat="server" CssClass="btnSubmit"
                        OnClick="imgBtnImageUpdate_Click" />
                    <asp:Button ID="ImgBtncancel" Text="Cancel" runat="server" OnClick="ImgBtncancel_Click"
                        CssClass="btnSubmit" />
                </asp:Panel>
                <div style="clear: both;">
                </div>
                <asp:GridView ID="GridView1" runat="server" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                    CssClass="mGrid" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Record Found."
                    OnRowDataBound="GridView1_RowDataBound" 
                    OnRowCommand="GridView1_RowCommand" onrowediting="GridView1_RowEditing">
                    <Columns>
                        <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left"
                            HeaderStyle-Width="85%">
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Logo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" Height="70px" Width="120px" ImageUrl='<%# Eval("LogoUrl") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Button ID="change" Text="Change" runat="server" CssClass="btnSubmit" CommandName="Edit"
                                    CommandArgument='<%# Eval("whid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>
            </fieldset>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnChange" />
            <asp:PostBackTrigger ControlID="imgBtnImageUpdate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

