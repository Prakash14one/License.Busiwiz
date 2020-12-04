<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="FrmBusinessMaster.aspx.cs" Inherits="ShoppingCart_Admin_FrmBusinessMaster"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllegend" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadddd" runat="server" Text="Add Division" Width="180px" CssClass="btnSubmit"
                            OnClick="btnadddd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel runat="server" ID="pnladdd" Width="100%" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%" align="right">
                                    <label>
                                        Business Name
                                    </label>
                                </td>
                                <td style="width: 75%" colspan="2">
                                    <label>
                                        <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstore_SelectedIndexChanged"
                                            Width="250px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%" align="right">
                                    <label>
                                        Department Name
                                        <asp:Label ID="Label1" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                            InitialValue="0" ControlToValidate="ddldepartment" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 20%">
                                    <label>
                                        <asp:DropDownList ID="ddldepartment" runat="server" Width="250px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td style="width: 55%" valign="bottom">
                                    <asp:ImageButton ID="imgadddept" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                        OnClick="LinkButton97666667_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                    <asp:ImageButton ID="imgdeptrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                        ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton13_Click"
                                        ToolTip="Refresh" Width="20px" ImageAlign="Bottom" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%" align="right">
                                    <label>
                                        Division Name
                                        <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 75%" colspan="2">
                                    <label>
                                        <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                </td>
                                <td style="width: 75%" colspan="2">
                                    <asp:Button ID="Button26" runat="server" OnClick="Button26_Click" Text="Submit" ValidationGroup="1"
                                        CssClass="btnSubmit" />
                                    <asp:Button ID="Button28" runat="server" OnClick="Button28_Click" Text="Update" CssClass="btnSubmit"
                                        Visible="False" ValidationGroup="1" />
                                    <asp:Button ID="Button27" runat="server" Text="Cancel" OnClick="Button27_Click" CssClass="btnSubmit" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label18" runat="server" Text="List of Divisions"></asp:Label>
                    </legend>
                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                    <div style="float: right">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" OnClick="Button1_Click"
                            CssClass="btnSubmit" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" class="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td colspan="4">
                                            <label>
                                                Business Name
                                            </label>
                                            <label>
                                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="200px"
                                                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                            <label>
                                                Department Name
                                            </label>
                                            <label>
                                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Width="200px"
                                                    OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="font-size: 20px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label20" runat="server" Font-Italic="true" Text="Business : "></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label4" runat="server" ForeColor="Black" Text="List of Divisions "
                                                                    Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="text-align: left; font-size: 14px;">
                                                                <asp:Label ID="Label11" runat="server" ForeColor="Black" Text="Department :" Font-Italic="true"></asp:Label>
                                                                <asp:Label ID="lblDepartment" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <cc11:PagingGridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    DataKeyNames="BusinessID" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                                                    OnRowCommand="GridView1_RowCommand" AllowSorting="True" OnSorting="GridView1_Sorting"
                                                    EmptyDataText="No Record Found." AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business" SortExpression="Wname" ItemStyle-Width="20%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstorename123" runat="server" Text='<%#Bind("Wname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="20%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Department" SortExpression="Dname" ItemStyle-Width="35%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldepatmentname123" runat="server" Text='<%#Bind("Dname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="35%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Division" SortExpression="BusinessName" ItemStyle-Width="35%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbussinessname123" runat="server" Text='<%#Bind("BusinessName") %>'></asp:Label>
                                                                <asp:Label ID="lblbusinessid123" runat="server" Text='<%#Bind("BusinessID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="35%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton49" ImageUrl="~/Account/images/edit.gif" runat="server"
                                                                    ToolTip="Edit" CommandArgument='<%# Eval("BusinessID") %>' CommandName="vi" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <%-- <asp:ButtonField CommandName="vi" Text="Edit" HeaderText="Edit" ValidationGroup="2"
                                                            ButtonType="Image" HeaderImageUrl="~/Account/images/edit.gif" ImageUrl="~/Account/images/edit.gif"
                                                            ItemStyle-Width="2%" />--%>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                                    OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
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
