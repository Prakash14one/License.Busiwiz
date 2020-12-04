<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_List_Pages.ascx.cs"
    Inherits="UserContorls_UC_List_Pages" %>
<div id="right_content">
    <h2>
        List Of Pages / Web Client</h2>
    <div class="products_box">
        <fieldset>
            <legend>List Of Pages</legend>
            <label for="lastName1">
                <asp:HyperLink runat="server" ID="lbtnChange" Text="New Account Sign Up Form" NavigateUrl="~/New_Account_SignUp_Form.aspx"></asp:HyperLink>
            </label>
            <div style="clear: both;">
            </div>
            <label for="lastName1">
                <asp:HyperLink runat="server" ID="HyperLink1" Text="Account Master Configuration Form"
                    NavigateUrl="~/Account_Master_Configuration_Form.aspx"></asp:HyperLink>
            </label>
            <div style="clear: both;">
            </div>
            <label for="lastName1">
                <asp:HyperLink runat="server" ID="HyperLink2" Text="Price Plan Form" NavigateUrl="~/Price_Plan.aspx"></asp:HyperLink>
            </label>
            <div style="clear: both;">
            </div>
            <label for="lastName1">
                <asp:HyperLink runat="server" ID="HyperLink3" Text="License" NavigateUrl="~/License.aspx"></asp:HyperLink>
            </label>
            <div style="clear: both;">
            </div>
            <label for="lastName1">
                <asp:HyperLink runat="server" ID="HyperLink4" Text="Client Search" NavigateUrl="~/Client_Search.aspx"></asp:HyperLink>
            </label>
            <div style="clear: both;">
            </div>
        </fieldset>
        <fieldset>
            <legend>List Of Pages / Web Admin</legend>
            <label for="lastName1">
                <asp:HyperLink runat="server" ID="HyperLink13" Text="Main Page" NavigateUrl="http://weballyadmin.v2infotech.in/SampleForm.aspx"></asp:HyperLink>
            </label>
            <div style="clear: both;">
            </div>
            <label for="lastName1">
                <asp:HyperLink runat="server" ID="HyperLink14" Text="Form Page" NavigateUrl="http://weballyadmin.v2infotech.in/Default.aspx"></asp:HyperLink>
            </label>
            <div style="clear: both;">
            </div>
        </fieldset>
        <fieldset>
            <legend>Source Code</legend>
            <label for="lastName1">
                <asp:HyperLink runat="server" ID="HyperLink11" Text="Click Here to Download new Source Code, SEPT 19 7:05PM"
                    NavigateUrl="~/SourceCode/SourceCodeV1.0.rar"></asp:HyperLink>
            </label>
            <div style="clear: both;">
            </div>
            <label for="lastName1">
                <asp:HyperLink runat="server" ID="HyperLink12" Text="Click Here to Download new Source Code, SEPT 20 6:37PM"
                    NavigateUrl="~/SourceCode/SourceCodeV1.1.rar"></asp:HyperLink>
            </label>
        </fieldset>
    </div>
    <!--end of right content-->
    <div style="clear: both;">
    </div>
</div>
