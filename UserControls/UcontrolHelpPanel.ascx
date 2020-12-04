<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcontrolHelpPanel.ascx.cs" Inherits="ShoppingCart_Admin_UserControls_UC_Title" %>
<div id="right_content">
    <asp:Panel runat="server" ID="PNLTITLE">
        <div class="divHeaderLeft">
            <div style="float: left; width: 50%;">
                <h2>
                    <asp:Label ID="lbltitle" runat="server"></asp:Label>
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/Information.png" Visible="false" />
                </h2>
            </div>
            <div style="float: right; width: 50%">


                <asp:Panel ID="pnlshow" runat="server">
                </asp:Panel>


            </div>
        </div>
    </asp:Panel>
    <div style="clear: both;">
    </div>
    <asp:Panel runat="server" ID="pnlhelp" Style="border: solid 1px black">
        <h3>
            <asp:Label ID="lblDetail" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="7pt">
            </asp:Label>
        </h3>
    </asp:Panel>
</div>
