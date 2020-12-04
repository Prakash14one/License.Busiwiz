<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="OverheadAbsorbtionRateReport.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <legend></legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Calculate New Employee Average Cost"
                            CssClass="btnSubmit" OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td style="width: 20%">
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Select Business"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 80%">
                                <label>
                                    <asp:DropDownList ID="ddlStore" runat="server" 
                                    OnSelectedIndexChanged="ddlStore_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Select Accounting Year"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 80%">
                                <label>
                                    <asp:DropDownList ID="ddlaccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlaccount_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Date of Calculation Report"></asp:Label>
                                    <asp:Label ID="Label25" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                        InitialValue="0" ControlToValidate="DropDownList1" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td style="width: 70%">
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                            </td>
                            <td style="width: 80%">
                                <asp:Button ID="Button1" runat="server" Text="Go" CssClass="btnSubmit" OnClick="Button1_Click"
                                    ValidationGroup="1" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnlprint" runat="server" Visible="false">
                    <table width="100%">
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnprintableversion" runat="server" Text="Printable Version" OnClick="btnprintableversion_Click"
                                    CssClass="btnSubmit" />
                                <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    type="button" value="Print" visible="False" class="btnSubmit" />
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
                                                        <tr>
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label20" runat="server" Font-Italic="true" Text="Business : "></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label3" runat="server" Text="Calculation Report of Emplyee Average Cost"
                                                                    Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="panelreport" runat="server" Visible="false">
                                                    <fieldset>
                                                        <legend>
                                                            <asp:Label ID="Label6" runat="server" Text="Part A - Total Expected Yearly Business Overheads"></asp:Label>
                                                        </legend>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 60%">
                                                                    <asp:GridView ID="GridView1" runat="server" Width="700px" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                        AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Overhead Name" HeaderStyle-HorizontalAlign="Left"
                                                                                HeaderStyle-Width="80%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label158" runat="server" Text='<%# Eval("Overheadname") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" Width="80%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label159" runat="server" Text='<%# Eval("Amount","{0:###,###.##}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="pgr" />
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                    </asp:GridView>
                                                                </td>
                                                                <td style="width: 40%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td style="width: 44%">
                                                                                <label>
                                                                                    <asp:Label ID="Label12" runat="server" Text="Total Expected Yearly Business Overheads"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td style="width: 56%" colspan="2">
                                                                                <label>
                                                                                    <asp:Label ID="lbltotal1" runat="server" Text="0"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <asp:Panel ID="pannnn" runat="server" Visible="false">
                                                        <fieldset>
                                                            <legend>
                                                                <asp:Label ID="Label9" runat="server" Text="Part B - Calculation of Employee Average Cost Per Hour"></asp:Label>
                                                            </legend>
                                                            <div style="clear: both;">
                                                            </div>
                                                            <label>
                                                                Overhead Absorption Rate (By Labour Hours) = Total Expected overheads /Total Annual
                                                                Employee Hours =
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="Label7" runat="server" Text="/"></asp:Label>
                                                                <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                                                            </label>
                                                            <label>
                                                                =
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                                            </label>
                                                            <div style="clear: both;">
                                                            </div>
                                                            <fieldset>
                                                                <legend>
                                                                    <asp:Label ID="Label24" runat="server" Text="Employee Average Cost Per Hour"></asp:Label>
                                                                </legend>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td colspan="7">
                                                                            <asp:Panel ID="pnlhidden" runat="server" Width="100%">
                                                                                <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                                    AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found." AutoGenerateColumns="False">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left"
                                                                                            HeaderStyle-Width="14%">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Labesdfsdl158" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                                                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("EmployeeMasterID") %>' Visible="false"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Average Daily Hours" HeaderStyle-HorizontalAlign="Left"
                                                                                            HeaderStyle-Width="14%" ItemStyle-HorizontalAlign="Right">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Labelsdfsdd159" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Total Working Days Per Year" HeaderStyle-HorizontalAlign="Left"
                                                                                            HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Labxxx158" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Total Yearly Hours" HeaderStyle-HorizontalAlign="Left"
                                                                                            HeaderStyle-Width="14%" ItemStyle-HorizontalAlign="Right">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Labtttt59" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Employee Average Wage Rate" HeaderStyle-HorizontalAlign="Left"
                                                                                            HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label159" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Overhead Absorption Rate" HeaderStyle-HorizontalAlign="Left"
                                                                                            HeaderStyle-Width="14%" ItemStyle-HorizontalAlign="Right">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Labelsdf158" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Average Cost Per Hour" HeaderStyle-HorizontalAlign="Left"
                                                                                            HeaderStyle-Width="14%" ItemStyle-HorizontalAlign="Right">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Laasdasbel159" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%">
                                                                            <label>
                                                                                <asp:Label ID="Label14" runat="server" Text="Total Number of Expected Employe Hours for the year"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td style="width: 50%">
                                                                            <label>
                                                                                <asp:Label ID="Label15" runat="server" Text="0"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                            <asp:Panel ID="rammmma" runat="server" Visible="false">
                                                                <fieldset>
                                                                    <legend>
                                                                        <asp:Label ID="Label11" runat="server" Text="Overhead Absorbtion Rate (By Labour Hours)"></asp:Label>
                                                                    </legend>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                    <asp:Label ID="Label13" runat="server" Text="Total Expected Business Overheads"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    <asp:Label ID="Label16" runat="server" Text="Divided by"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    <asp:Label ID="Label17" runat="server" Text="Total Number of Expected Emplyee Hours for the Year"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    <asp:Label ID="Label19" runat="server" Text="Equals to"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    <asp:Label ID="Label18" runat="server" Text="Overhead Absorbtion Rate"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                    <asp:Label ID="lbl1rate" runat="server" Text=""></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    <asp:Label ID="lbl2rate" runat="server" Text=""></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    <asp:Label ID="Label23" runat="server" Text=""></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    <asp:Label ID="lbl3rate" runat="server" Text=""></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset></asp:Panel>
                                                        </fieldset></asp:Panel>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
