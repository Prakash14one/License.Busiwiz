<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="UploadCodeforexeforCustomer.aspx.cs" Inherits="UploadCodeforexeforCustomer" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" width="645px">
        <tr>
            <td colspan="3">
                <strong>Upload Client's project code&nbsp; to create Exe on behalf of Client</strong></td>
        </tr>
        <tr>
            <td class="column2" colspan="2" style="height: 52px">
            </td>
            <td style="width: 3px; height: 52px">
            </td>
        </tr>
        <tr>
            <td class="column2" colspan="2" style="height: 52px">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                    RepeatDirection="Horizontal" Width="457px" Visible="False">
                    <asp:ListItem Selected="True" Value="0">New code of Existing Price Plan</asp:ListItem>
                    <asp:ListItem Value="1">Update code of Existing Price Plan</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="width: 3px; height: 52px;">
            </td>
        </tr>
        <tr>
            <td class="column1" style="width: 208px">
                Client Name :
            </td>
            <td class="column2" style="width: 3px">
                <asp:DropDownList ID="ddlClientList" runat="server" AutoPostBack="True" DataTextField="CompanyName"
                    DataValueField="ClientMasterId" OnSelectedIndexChanged="ddlClientList_SelectedIndexChanged"
                    Width="222px">
                </asp:DropDownList></td>
            <td style="width: 3px">
            </td>
        </tr>
        <tr>
            <td class="column1" style="width: 208px">
                Product Name : &nbsp;
            </td>
            <td style="width: 3px" class="column2">
                <asp:DropDownList ID="ddlProductname" runat="server" Width="218px" DataTextField="ProductName"
                    DataValueField="ProductId" OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlProductname"
                    ErrorMessage="*" InitialValue="0" ValidationGroup="1" Width="1px"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 3px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlProductname"
                    ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="column1" style="width: 208px">
                Select Version Name :
            </td>
            <td class="column2" style="width: 3px">
                <asp:DropDownList ID="ddlVersion" runat="server" AutoPostBack="True" DataTextField="VersionNo"
                    DataValueField="ProductDetailId" OnSelectedIndexChanged="ddlVersion_SelectedIndexChanged"
                    Width="218px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlVersion"
                    ErrorMessage="*" InitialValue="0" ValidationGroup="1" Width="1px"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 3px">
            </td>
        </tr>
        <tr>
            <td class="column1" style="height: 38px; width: 208px;">
                Price Plan Name :
            </td>
            <td class="column2" style="width: 3px; height: 38px;">
                <asp:DropDownList ID="ddlPricePlanName" runat="server" DataTextField="PricePlanName"
                    DataValueField="PricePlanId" Width="218px" AutoPostBack="True" OnSelectedIndexChanged="ddlPricePlanName_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width: 3px; height: 38px;">
            </td>
        </tr>
        <tr>
            <td class="column1" style="height: 38px; width: 208px;">
                Upload zip file of your project's Code :
            </td>
            <td class="column2" style="width: 3px; height: 38px">
                <asp:FileUpload ID="fuploadProjectSetup" runat="server" Width="191px" />
            </td>
            <td style="width: 3px; height: 38px">
            </td>
        </tr>
        <tr>
            <td style="height: 16px; width: 208px;">
            </td>
            <td style="width: 3px; height: 16px">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                    ValidationGroup="1" />
            </td>
            <td style="height: 16px">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 16px">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </td>
            <td style="height: 16px">
            </td>
        </tr>
        <tr>
            <td class="column2" colspan="2" style="height: 16px">
                If you want to upload your zip file again which you have already uploaded and it's
                setup file not to be created
                <br />
                &nbsp;then you select product name,version name and price plan name and then from
                the list below just delete that.
            </td>
            <td style="height: 16px">
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" Width="645px" ScrollBars="Auto">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="GridBack"
            DataKeyNames="productDetailExeId" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand"
            AllowPaging="True" EmptyDataText="There is no data." OnPageIndexChanging="GridView1_PageIndexChanging"
            PageSize="5">
            <RowStyle CssClass="GridRowStyle" />
            <Columns>
                <asp:ButtonField ButtonType="Button" CommandName="Delete1" HeaderText="Delete" Text="Delete" />
                <asp:BoundField DataField="productName" HeaderText="Product Name" />
                <%-- <asp:TemplateField HeaderText="Path to Open Code" >
<ItemTemplate>
  <asp:Label  ID="lblLocation" runat="server" BackColor="Transparent"  
                                                  /> 
</ItemTemplate>--%>
                <%--   <HeaderStyle Font-Bold="True" HorizontalAlign="Left"   />
</asp:TemplateField>--%>
                <asp:BoundField DataField="UploadDate" DataFormatString="{0:d-MMM-yyyy}" HeaderText="Upload Date" />
                <asp:BoundField DataField="productURL" HeaderText="Product URL" />
                <asp:BoundField DataField="PricePlanName" HeaderText="Price Plan Name" />
                <asp:BoundField DataField="PricePlanAmount" HeaderText="Price Plan Amount in ($)" />
                <asp:BoundField DataField="VersionNo" HeaderText="Version No" />
                <%--<asp:BoundField DataField="Active" HeaderText="Active" />--%>
            </Columns>
            <FooterStyle CssClass="GridFooter" />
            <PagerStyle CssClass="GridPager" />
            <HeaderStyle CssClass="GridHeader" />
            <AlternatingRowStyle CssClass="GridAlternateRow" />
        </asp:GridView>
    </asp:Panel>
    &nbsp;<%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="GridBack"
        DataKeyNames="productDetailId"  > 
        <RowStyle CssClass="GridRowStyle" />
        <Columns>
           
            <asp:BoundField DataField="productName" HeaderText="Product Name" />
            
            <asp:BoundField DataField="productURL" HeaderText="Product URL" />
            <asp:BoundField DataField="PricePlanURL" HeaderText="Price Plan URL" />
            <asp:BoundField DataField="VersionNo" HeaderText="Version No" />
            <asp:BoundField DataField="Active" HeaderText="Active" />
            <asp:BoundField DataField="StartDate" DataFormatString="{0:d-MMM-yyyy}" HeaderText="Start Date" />
            <asp:BoundField DataField="EndDate" DataFormatString="{0:d-MMM-yyyy}" HeaderText="End Date" />
        </Columns>
        <FooterStyle CssClass="GridFooter" />
        <PagerStyle CssClass="GridPager" />
        <HeaderStyle CssClass="GridHeader" />
        <AlternatingRowStyle CssClass="GridAlternateRow" />
    </asp:GridView>--%>
    <input id="hdnPricePlanId" name="hdnPricePlanId" runat="server" type="hidden" style="width: 1px" />
    <input runat="server" id="hdnProductDetailId" type="hidden" name="hdnProductDetailId"
        style="width: 3px" />
    <input runat="server" id="hdnProductId" type="hidden" name="hdnProductId" style="width: 3px" />
</asp:Content>
