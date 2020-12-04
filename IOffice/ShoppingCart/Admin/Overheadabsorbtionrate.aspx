<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="Overheadabsorbtionrate.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend></legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="View Calculation Reports" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
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
                                    <asp:DropDownList ID="ddlStore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Select Accounting Year"></asp:Label>
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
                            <td colspan="2">
                                <asp:Panel ID="panel1" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 20%">
                                                <label>
                                                    <asp:Label ID="Label3" runat="server" Text="Overhead Absorbtion Rate"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 20%">
                                                <label>
                                                    <asp:Label ID="lblabsorbrate" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 60%">
                                                <asp:Button ID="Button1" runat="server" Text="Recalculate" CssClass="btnSubmit" OnClick="Button1_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                <label>
                                                    <asp:Label ID="Label4" runat="server" Text="Last Recalculated Date"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 80%" colspan="2">
                                                <label>
                                                    <asp:Label ID="lbldate" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Date of Calculation"></asp:Label>
                                    <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtestartdate"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td style="width: 80%">
                                <label>
                                    <asp:TextBox ID="txtestartdate" runat="server" Width="80px"></asp:TextBox>
                                </label>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton2"
                                    TargetControlID="txtestartdate">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="txtestartdate">
                                </cc1:MaskedEditExtender>
                                <label>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                </label>
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label6" runat="server" Text="Total Expected Business Overheads"></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="panelhd" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    <label>
                                                        <asp:Label ID="Label7" runat="server" Text="Overhead Name"></asp:Label>
                                                        <asp:Label ID="Label20" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtovername"
                                                            ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtovername" runat="server" MaxLength="50"></asp:TextBox>
                                                    </label>
                                                </td>
                                                <td style="width: 15%">
                                                    <label>
                                                        <asp:Label ID="Label8" runat="server" Text="Amount"></asp:Label>
                                                        <asp:Label ID="Label157" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldVali432edator13" runat="server" ControlToValidate="txtamount"
                                                            ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtamount" runat="server" Width="150px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredresdfsdfTextBoxExtender9" runat="server"
                                                            Enabled="True" TargetControlID="txtamount" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </label>
                                                </td>
                                                <td style="width: 65%" valign="bottom">
                                                    <label>
                                                        <asp:Button ID="Button2" runat="server" Text="Add" CssClass="btnSubmit" ValidationGroup="1"
                                                            OnClick="Button2_Click" />
                                                        <asp:Button ID="Button4" runat="server" Text="Update" CssClass="btnSubmit" Visible="false"
                                                            OnClick="Button4_Click" />
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        <asp:Label ID="Label22" runat="server" Text="Total Overheads"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 60%">
                                    <asp:GridView ID="GridView1" runat="server" Width="700px" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found." AutoGenerateColumns="False"
                                        OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing">
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
                                            <%--<asp:ButtonField ButtonType="Image" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderImageUrl="~/Account/images/delete.gif" ImageUrl="~/Account/images/delete.gif"
                                                HeaderText="Delete" ItemStyle-Width="2%" CommandName="del">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="50px" />
                                            </asp:ButtonField>--%>
                                            <%-- <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" 
                                                HeaderStyle-HorizontalAlign="Left" Visible="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton49" ImageUrl="~/Account/images/edit.gif" runat="server"
                                                        ToolTip="Edit" CommandArgument='<%# Eval("ID") %>' CommandName="Edit" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" 
                                                ItemStyle-Width="3%" Visible="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                        ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                                <td style="width: 40%">
                                    <asp:Button ID="Button5" runat="server" Text="Add New Overhead" CssClass="btnSubmit"
                                        OnClick="Button5_Click" />
                                    <asp:Button ID="Button6" runat="server" Text="Calculate Now" CssClass="btnSubmit"
                                        OnClick="Button6_Click" Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 45%">
                                                <label>
                                                    <asp:Label ID="Label12" runat="server" Text="Total Expected Business Overheads"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 55%" colspan="2">
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
                    <asp:Panel ID="pannnn" runat="server" Visible="false"  Width="100%">
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label9" runat="server" Text="Calculation of Employee Average Cost Per Hour"></asp:Label>
                            </legend>
                            <div style="height:5px;">
                            </div>
                            <div>
                                Note: Overhead absorbtion rate is calculated by dividing total expected business
                                overheads by total number of expected employee hours for the year.
                            </div>
                            <div style="clear: both;">
                            </div>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <label>
                                            Total Expected Business Overheads:
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lbl1rate" runat="server" Text=""></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Total Number of Expected Employee Hours for the Year:
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lbl2rate" runat="server" Text=""></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border-style: dotted none none none"  >
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label25" runat="server" Text="Overhead Absorbtion Rate:" Font-Size="Large"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lbl3rate" runat="server" Text="" Font-Size="Large"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                            <div style="height: 5px">
                            </div>
                            <fieldset>
                                <legend>
                                    <asp:Label ID="Label24" runat="server" Text="Employee Average Cost Per Hour"></asp:Label>
                                </legend>
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="Panel2" runat="server" Width="100%" Visible="false">
                                                <%--<table width="50%" >
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                Employee Name
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                Average Daily Hours
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                Total Working Days Per Year
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                Total Yearly Hours
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                Employee Average Wage Rate
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                Overhead Absorption Rate
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                Average Cost Per Hour
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>--%>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="pnlhidden" runat="server" ScrollBars="Both" Height="200px" Width="100%">
                                                <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                   AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found." AutoGenerateColumns="False"
                                                    OnRowDataBound="GridView2_RowDataBound" >
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="12%" ItemStyle-Width="14%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Labesdfsdl158" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("EmployeeMasterID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Average Daily Hours" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="14%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Labelsdfsdd159" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Working Days Per Year" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Labxxx158" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Yearly Hours" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="14%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Labtttt59" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Average Wage Rate" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label159" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Overhead Absorption Rate" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="14%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Labelsdf158" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Average Cost Per Hour" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="14%">
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
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%">
                                            <label>
                                                <asp:Label ID="Label14" runat="server" Text="Total Number of Expected Employee Hours for the Year"></asp:Label>
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
                            <asp:Panel ID="pannnnneeeee" runat="server" Visible="false" width="100%">
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
                                                    <%--<asp:Label ID="lbl1rate" runat="server" Text=""></asp:Label>--%>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <%--<asp:Label ID="lbl2rate" runat="server" Text=""></asp:Label>--%>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label23" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <%--<asp:Label ID="lbl3rate" runat="server" Text=""></asp:Label>--%>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                        </fieldset>
                        <div style="clear: both;">
                        </div>
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="Button3" runat="server" Text="Submit" CssClass="btnSubmit" Visible="false"
                                        OnClick="Button3_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
