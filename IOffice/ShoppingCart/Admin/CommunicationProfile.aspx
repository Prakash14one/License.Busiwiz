<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="CommunicationProfile.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="uppanel" runat="server">
        <ContentTemplate>
            <fieldset>
                <legend>
                    <asp:Label ID="Label1" runat="server" Text="Communication Profile"></asp:Label>
                </legend>
                <div style="clear: both;">
                </div>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%">
                            <label>
                                <asp:Label ID="Label2" runat="server" Text="Communication Date"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 50%">
                            <label>
                                <asp:Label ID="lbldate" runat="server"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 30%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <label>
                                <asp:Label ID="Label3" runat="server" Text="User Type"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 50%">
                            <label>
                                <asp:Label ID="lblutype" runat="server"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 30%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <label>
                                <asp:Label ID="Label4" runat="server" Text="User Name"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 50%">
                            <label>
                                <asp:Label ID="lbluname" runat="server"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 30%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <label>
                                <asp:Label ID="Label5" runat="server" Text="Communication Type"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 50%">
                            <label>
                                <asp:Label ID="lblcomtype" runat="server"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 30%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <label>
                                <asp:Label ID="Label6" runat="server" Text="Communication By"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 50%">
                            <label>
                                <asp:Label ID="lblcomname" runat="server"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 30%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <label>
                                <asp:Label ID="Label8" runat="server" Text="Description"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 50%">
                            <label>
                                <asp:Label ID="lbldesc" runat="server"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 30%">
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
