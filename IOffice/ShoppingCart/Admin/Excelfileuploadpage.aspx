<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="Excelfileuploadpage.aspx.cs" Inherits="Admin_Excelfileuploadpage"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Excel File Upload"></asp:Label>
                    </legend>
                    <asp:Panel ID="Panel11" runat="server">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label26" runat="server" Text="Please click here before proceeding with data upload. "></asp:Label>
                                    </label>
                                </td>
                                <td valign="bottom">
                                    <asp:Button ID="btlnote" runat="server" Text="Note" OnClick="btlnote_Click" CssClass="btnSubmit" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        Address Type
                                        <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAddType"
                                            ErrorMessage="*" InitialValue="0" ValidationGroup="2"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td valign="bottom">
                                    <label>
                                        <asp:DropDownList ID="ddlAddType" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        Category - Subcategory - Subsubcategory
                                        <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCat"
                                            ErrorMessage="*" InitialValue="0" ValidationGroup="2"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td valign="bottom">
                                    <label>
                                        <asp:DropDownList ID="ddlCat" runat="server" Width="300px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        Select File
                                        <asp:Label ID="Label32" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator177" runat="server" ControlToValidate="File12"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                    <label>
                                        <asp:FileUpload ID="File12" runat="server" CssClass="btnSubmit" />
                                    </label>
                                </td>
                                <td valign="bottom">
                                    <asp:Button ID="btnfileupload" runat="server" OnClick="btnfileupload_Click" Text="Upload File"
                                        CssClass="btnSubmit" ValidationGroup="1" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        Select Sheet
                                        <asp:Label ID="Label2" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSheets"
                                            ErrorMessage="*" ValidationGroup="2" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="ddlSheets" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td valign="bottom">
                                    <asp:Button ID="bnt123" runat="server" OnClick="bnt123_Click" Text="Transfer Data"
                                        CssClass="btnSubmit" ValidationGroup="2" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel Visible="false" runat="server" ID="pnlme">
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        <asp:GridView ID="GVC" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            DataKeyNames="Id" PageSize="10" Width="100%" OnPageIndexChanging="GVC_PageIndexChanging"
                                            lternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" CssClass="mGrid"
                                            EmptyDataText="No Record Found.">
                                            <Columns>
                                                <asp:BoundField DataField="Filename" HeaderText="File Name" HeaderStyle-HorizontalAlign="Left"
                                                    SortExpression="Filename"></asp:BoundField>
                                                <asp:BoundField DataField="Uploaddate" HeaderText="Uploaded Date" HeaderStyle-HorizontalAlign="Left"
                                                    SortExpression="Uploaddate"></asp:BoundField>
                                                <asp:BoundField DataField="Noofrecordupload" HeaderText="Uploaded Records" HeaderStyle-HorizontalAlign="Left"
                                                    SortExpression="Noofrecordupload"></asp:BoundField>
                                                <asp:BoundField DataField="Noofrecordreject" HeaderText="Rejected Records" HeaderStyle-HorizontalAlign="Left"
                                                    SortExpression="Noofrecordreject"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>
                    <table width="100%">
                        <tr>
                            <td colspan="4">
                                <asp:Panel ID="pnlsucedata" runat="server" Visible="false">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    <asp:Label ID="lblvvv" runat="server" Text="You have successfully uploaded "></asp:Label>
                                                    <asp:Label ID="lblsucmsg" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    <asp:Label ID="lblnoofrecord" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="Label24" runat="server" Text=" records have been added to your Business. "></asp:Label>
                                                </label>
                                                <label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    <asp:Label ID="lblnotimport" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="Label25" runat="server" Text=" records had errors in importing. "></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Button ID="Button2" Text="View the List of Records with Errors" runat="server"
                                                        CssClass="btnSubmit" OnClick="Button2_Click" Visible="False" /></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlgrd" runat="server" Visible="false">
                                                    <asp:GridView ID="grderrorlist" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                        GridLines="Both" AllowPaging="True" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" DataKeyNames="No" EmptyDataText="No Record Found."
                                                        OnPageIndexChanging="grderrorlist_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Row No." Visible="true" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbusness" runat="server" Text='<%# Bind("BusinessName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                        <%--    <asp:TemplateField HeaderText="Sub Sub Category" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprodname" runat="server" Text='<%# Bind("subsubcategory") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Adress Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="l1" runat="server" Text='<%# Bind("addresstype") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                            <%--                <asp:TemplateField HeaderText="Contact Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpno" runat="server" Text='<%# Bind("contactname") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="barn" runat="server" Text='<%# Bind("designation") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblssname" runat="server" Text='<%# Bind("address1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Country" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("country") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblst" runat="server" Text='<%# Bind("state") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="City" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwight" runat="server" Text='<%# Bind("city") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Phone" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("phone1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Zipcode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRate" runat="server" Text='<%# Bind("zip") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRatdfe" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                            <%--                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRaffte" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="Button1" Text="Close" runat="server" CssClass="btnSubmit" OnClick="Button1_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlMain" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Panel ID="Paneldoc" runat="server" BackColor="GhostWhite" BorderColor="#416271"
                                        ScrollBars="Auto" BorderStyle="Solid" Height="270px" Width="616px" BorderWidth="5px">
                                        <fieldset>
                                            <legend>Rules for a valid excel file for data upload</legend>
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="3">
                                                        <label>
                                                            1) Please ensure that you are using a Microsoft excel version from 2003-2010.
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <label>
                                                            2) In order to successfully transfer your data, you must have the columns in your
                                                            spread sheet appear exactly as they do below.<br />
                                                            If they are not the same, the data will not transfer properly.
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 50%">
                                                        Column Name
                                                    </td>
                                                    <td align="center" style="width: 50%">
                                                        Field Name
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        A
                                                    </td>
                                                    <td align="center">
                                                        Business Name
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        B
                                                    </td>
                                                    <td align="center">
                                                        Address
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        C
                                                    </td>
                                                    <td align="center">
                                                        City
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        D
                                                    </td>
                                                    <td align="center">
                                                        State
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        E
                                                    </td>
                                                    <td align="center">
                                                        Country
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        F
                                                    </td>
                                                    <td align="center">
                                                        ZipCode
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        G
                                                    </td>
                                                    <td align="center">
                                                        Phone No
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        H
                                                    </td>
                                                    <td align="center">
                                                        Email
                                                    </td>
                                                </tr>
                                           <%--     <tr>
                                                    <td align="center">
                                                        I
                                                    </td>
                                                    <td align="center">
                                                        City
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        J
                                                    </td>
                                                    <td align="center">
                                                        Phone No.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        K
                                                    </td>
                                                    <td align="center">
                                                        Zipcode
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        L
                                                    </td>
                                                    <td align="center">
                                                        Email
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        M
                                                    </td>
                                                    <td align="center">
                                                        Status
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td align="center" class="btn" colspan="3">
                                                        <asp:Button ID="ImageButton4" runat="server" OnClick="ImageButton4123_Click" Text="Cancel" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </asp:Panel>
                                    <asp:Button ID="Button4" runat="server" Style="display: none" />
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="Paneldoc" TargetControlID="Button4">
                                    </cc1:ModalPopupExtender>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnfileupload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
