<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="DatacollectionReport.aspx.cs" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    Title="Search for Businesses" Inherits="ListOfBusinessess" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script language="javascript" type="text/javascript">

        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }         
    </script>
    <style type="text/css">
        .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>Data Collection Report</legend>
                    <div style="float: right;">
                        <asp:Button ID="btnPrintVersion" runat="server" Text="Printable Version" OnClick="btnPrintVersion_Click"
                            CausesValidation="False" CssClass="btnSubmit" />
                        <input id="btnPrint" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" class="btnSubmit" />
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    Employee Name
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlemployee" runat="server" Width="250px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    Category
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="filterCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="filterCat_SelectedIndexChanged"
                                        Width="250px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Country
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="filtercountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="filtercountry_SelectedIndexChanged"
                                        Width="250px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    Sub Category
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="filterSub" runat="server" AutoPostBack="True" Width="250px"
                                        OnSelectedIndexChanged="filterSub_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    State
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="filterState" runat="server" AutoPostBack="True" Width="250px"
                                        OnSelectedIndexChanged="filterState_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    Sub Sub Category
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="filtersubsub" runat="server" Width="250px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    City
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="filterCity" runat="server" Width="250px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    Search By
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtSearch" runat="server" OnTextChanged="txtSearch_TextChanged"
                                        Width="250px"></asp:TextBox>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <asp:Panel ID="panelgoom" runat="server" Visible="false">
                                <td>
                                    <label>
                                        Status
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlstatus_search" runat="server" Width="180px">
                                            <asp:ListItem Value="2">All</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </asp:Panel>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="From"></asp:Label>
                                    <asp:Label ID="Label18" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtestartdate"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                                <label>
                                    <asp:TextBox ID="txtestartdate" runat="server" Width="70px"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                </label>
                                </label>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton2"
                                    TargetControlID="txtestartdate">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="txtestartdate">
                                </cc1:MaskedEditExtender>
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="To"></asp:Label>
                                    <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txteenddate"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                                <label>
                                    <asp:TextBox ID="txteenddate" runat="server" Width="70px"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                </label>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                                    TargetControlID="txteenddate">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="txteenddate">
                                </cc1:MaskedEditExtender>
                            </td>
                            <td>
                                <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" CssClass="btnSubmit"
                                    ValidationGroup="1" />
                            </td>
                            <td align="right">
                                No. of Recodrs :
                                <asp:Label ID="Label1" runat="server" Text="" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label14" runat="server" Font-Italic="true" Text="Data Collection Report"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <cc11:PagingGridView ID="GvBlisting" runat="server" AllowPaging="True" EmptyDataText="No Record Found."
                                                    DataKeyNames="ID" AutoGenerateColumns="False" Width="100%" GridLines="Both" OnPageIndexChanging="GvBlisting_PageIndexChanging"
                                                    lternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" CssClass="mGrid"
                                                    PageSize="50" OnRowCancelingEdit="GvBlisting_RowCancelingEdit" OnRowEditing="GvBlisting_RowEditing"
                                                    OnRowUpdating="GvBlisting_RowUpdating">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="22%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBusinessCategory123" runat="server" Text='<%#Bind("BusinessName") %>'></asp:Label>
                                                                <asp:Label ID="lblcandiID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBusinessCategory" runat="server" Text='<%#Bind("B_Category") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub Category" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBSubCategory" runat="server" Text='<%#Bind("B_SubCategory") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub Sub Category" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblB_SubSubCategory" runat="server" Text='<%#Bind("B_SubSubCategory") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="8%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCityName" runat="server" Text='<%#Bind("CityName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="8%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSName" runat="server" Text='<%#Bind("StateName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Country" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="8%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCName" runat="server" Text='<%#Bind("CountryName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Phone" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPhone" runat="server" Text='<%#Bind("Phone") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Email" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmailid" runat="server" Text='<%#Bind("Email") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblActive" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="DropDownList1" runat="server">
                                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowEditButton="True" HeaderText="Edit" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" ButtonType="Image" HeaderImageUrl="~/Account/images/edit.gif"
                                                            EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/UpdateGrid.JPG"
                                                            CancelImageUrl="~/images/delete.gif" Visible="false">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                </cc11:PagingGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
