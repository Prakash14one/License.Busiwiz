<%@ Page Language="C#" AutoEventWireup="true" CodeFile="verification.aspx.cs" MasterPageFile="~/Master/Login.master"
    Inherits="verification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <form id="form1" runat="server">
        <div style="height: 768px; margin-left:20px; " >
            <asp:Panel ID="pnlconfirmationmessage" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%" align="right">
                            <asp:Label ID="Label3" Font-Bold="true" Font-Size="Large" runat="server" Text="Security Center"> </asp:Label>
                            <br />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label4" runat="server" Text=""> </asp:Label>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="User Name :"> </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server"> </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="User Type :"> </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server"> </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="User ID :"> </asp:Label>
                        </td>
                        <td style="height: 23px">
                            <asp:Label ID="Label8" runat="server"> </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="IP address:"> </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server"> </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td  align="right">
                            <asp:Button ID="Button1" runat="server" Font-Bold="True" Height="30px" Width="100px"
                                Text="Close & Login" OnClick="Button1_Click" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        </form>

</asp:Content>
