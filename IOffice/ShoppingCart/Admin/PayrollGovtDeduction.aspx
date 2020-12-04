<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="PayrollGovtDeduction.aspx.cs" Inherits="PayrollGovtDeduction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=724,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }  
    </script>

    <div class="products_box">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text="" Visible="False" />
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label4" runat="server" Text="List of Payroll Government Deduction"></asp:Label></legend>
                    <asp:Panel ID="pnledit" runat="server">
                        <div style="float: right;">
                            <label>
                                <asp:Button ID="Button1" CssClass="btnSubmit" runat="server" Text="Printable Version"
                                    OnClick="Button1_Click" />
                            </label>
                            <label>
                                <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    type="button" value="Print" visible="False" />
                            </label>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label9" runat="server" Text="Business Name"></asp:Label></label>
                        <label>
                            <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfilterbus_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label1" runat="server" Text="Country : "></asp:Label></label>
                        <label>
                            <asp:Label ID="lblcountry" runat="server" Text=""></asp:Label></label>
                        <label>
                            <asp:Label ID="Label2" runat="server" Text="State : "></asp:Label></label>
                        <label>
                            <asp:Label ID="lblstate" runat="server" Text=""></asp:Label></label>
                        <div style="clear: both;">
                            <br />
                        </div>
                        <div style="clear: both;">
                            <table width="100%">
                                <tr><td align="right">
                                </td>
                                <td  width="150px">
                                <label>Create New Account</label>
                                </td>
                                <td align="right" width="20px">
                                <label>
                                   <asp:ImageButton ID="imgAdd2" runat="server" AlternateText="Add New" Height="20px"
                                                                                            ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="imgAdd2_Click" ToolTip="Add New "
                                                                                            Width="20px" /></label>
                                </td>
                                    <td align="right" width="20px"><label>
                                   
                                                                                        <asp:ImageButton ID="imgRefresh2" runat="server" AlternateText="Refresh" Height="20px"
                                                                                            ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="imgRefresh2_Click"
                                                                                            ToolTip="Refresh" Width="20px" /></label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                            <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                                <tr align="center">
                                    <td>
                                        <div id="mydiv" class="closed">
                                            <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblCompany" runat="server" Font-Size="20px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label7" runat="server" Font-Size="18px" Text="List of Payroll Government Deduction"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lblcst" runat="server" Font-Size="20px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Payrolltax_id"
                                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Deduction Name" ItemStyle-Width="20%" SortExpression="Deduction Name"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldedname" runat="server" Text='<%# Eval("Dedname") %>'></asp:Label>
                                                        <asp:Label ID="lblgovId" runat="server" Text='<%# Eval("GovId") %>' Visible="false"></asp:Label>
                                                         <asp:Label ID="lblempcont" runat="server" Text='<%# Eval("MatchingEmployee") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Account to credit for Employee Deduction(Current Liabilities)" ItemStyle-Width="25%"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlreditac" runat="server" Width="98%">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblcracc" runat="server" Text='<%# Eval("CrAccId") %>' Visible="false"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="tdfvf" runat="server" ControlToValidate="ddlreditac"
                                                            Display="Dynamic" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Account to debit for<br>Employer contribution(Expense Group)"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddldebitac" runat="server" Width="98%">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lbldbacc" runat="server" Text='<%# Eval("DrAccId") %>' Visible="false"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="tdfvf1" runat="server" ControlToValidate="ddldebitac"
                                                            Display="Dynamic" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Account to credit for accumulating Current Liability<br>regarding employer contribution"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlaccumulateliab" runat="server" Width="98%">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblcraccum" runat="server" Text='<%# Eval("CrAccumulateLiab") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="tdfvf2" runat="server" ControlToValidate="ddlaccumulateliab"
                                                            Display="Dynamic" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btnSubmit" ValidationGroup="1"
                                        OnClick="btnsave_Click" Visible="False" />
                                    <asp:Button ID="btnEdit" runat="server" CssClass="btnSubmit" Text="Edit" ValidationGroup="1"
                                        Visible="False" OnClick="btnEdit_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
                <asp:HiddenField ID="hfidwhid" runat="server" />
                <asp:HiddenField ID="hfempid" runat="server" />
                <asp:HiddenField ID="hfbmid" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
