<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="AccountReport.aspx.cs" Inherits="ShoppingCart_Admin_AccountReport"
    Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }  

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <asp:Panel ID="addinventoryroom" runat="server">
                        <table id="innertbl" style="width: 100%" cellspacing="3" cellpadding="2">
                            <tr>
                                <td colspan="4">
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%" align="right">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 25%">
                                    <label>
                                        <asp:DropDownList ID="ddlsearchByStore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsearchByStore_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td style="width: 25%" align="right">
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text=" Select Account Class Type"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 25%">
                                    <label>
                                        <asp:DropDownList ID="ddlSearchByClassType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchByClassType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%" align="right">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Select Account Class Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 25%">
                                    <label>
                                        <asp:DropDownList ID="ddlSearchByClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchByClass_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td style="width: 25%" align="right">
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="Select Account Group Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 25%">
                                    <label>
                                        <asp:DropDownList ID="ddlSearchByGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchByGroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%" align="right">
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text=" Select by Status"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 25%">
                                    <label>
                                        <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="2">All</asp:ListItem>
                                            <asp:ListItem Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td style="width: 25%" align="left">
                                    <asp:Button ID="Button3" runat="server" Text="Go" CssClass="btnSubmit" OnClick="Button3_Click" />
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Accounts" Font-Bold="True" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click" />
                            <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" visible="false" />
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table id="GridTbl" width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblcomname" runat="server" Font-Italic="True" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label8" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label14" runat="server" Font-Italic="True" Text="List of Accounts"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel3" runat="server" Width="100%">
                                                        <table width="100%" style="font-style: italic">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblclaat" runat="server" Font-Size="14px" Text="Class Type :"></asp:Label>
                                                                    <asp:Label ID="lblclasstypeprint" runat="server" Font-Size="14px" Font-Italic="true"></asp:Label>,
                                                                    <asp:Label ID="lblcma" runat="server" Font-Size="14px" Text="Class Name :"> </asp:Label>
                                                                    <asp:Label ID="lblclassnameprint" runat="server" Font-Size="14px" Font-Italic="true"></asp:Label>,
                                                                    <asp:Label ID="Label2" runat="server" Font-Size="14px" Text="Group Name :"></asp:Label>
                                                                    <asp:Label ID="lblgroupnameprint" runat="server" Font-Size="14px" Font-Italic="true"></asp:Label>,
                                                                    <asp:Label ID="Label7" runat="server" Font-Size="14px" Text="Status :"></asp:Label>
                                                                    <asp:Label ID="lblstatusprint" runat="server" Font-Size="14px" Font-Italic="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" DataKeyNames="id" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        CssClass="mGrid" Width="100%" PageSize="100" OnSorting="GridView1_Sorting" EmptyDataText="No Record Found."
                                        OnPageIndexChanging="GridView1_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="id" SortExpression="id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgroupid" runat="server" Text='<%#Bind("id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsname" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class Type" SortExpression="Classtypename" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblclasstypename" runat="server" Text='<%# Bind("Classtypename") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class Name" SortExpression="displayname" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldisname" runat="server" Text='<%# Bind("displayname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Group Name" SortExpression="groupdisplayname" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcountrystate" runat="server" Text='<%# Bind("groupdisplayname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Account ID" SortExpression="AccountId" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label21" runat="server" Text='<%# Bind("AccountId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Account Name" SortExpression="AccountName" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("AccountName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Status") %>' Visible="false"
                                                        Enabled="false" />
                                                    <asp:Label ID="Label24564" runat="server" Text='<%# Bind("Statuslabel") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
