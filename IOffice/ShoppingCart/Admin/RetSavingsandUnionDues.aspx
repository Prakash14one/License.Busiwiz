<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="RetSavingsandUnionDues.aspx.cs" Inherits="RetSavingsandUnionDues" %>

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
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
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
                        <asp:Label ID="Label5" runat="server" Text="Request for withholding for Retirement Savings and Union Dues"></asp:Label></legend>
                    <label>
                        <asp:Label ID="Label2" runat="server" Text="Business Name"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlstrname"
                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlstrname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstrname_SelectedIndexChanged1">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label3" runat="server" Text="Employee Name"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlemp"
                            ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="true"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlemp" runat="server" OnSelectedIndexChanged="ddlemp_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label8" runat="server" Text="Year"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlyear"
                            ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="true"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlyear" runat="server">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label6" runat="server" Text="Pay Cycle"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlpaycycle"
                            ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlpaycycle" runat="server" Width="130px">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <%--   <label>
             <asp:Label ID="Label1" runat="server" Text="Dedction Name"></asp:Label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlmname"
                                                        ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
             <asp:DropDownList ID="ddlmname" runat="server" Width="500px" Visible="false">
                  
                        
                </asp:DropDownList> 
                                                  
             
            </label>--%>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label13" runat="server" Text="Please withhold following amount from the payroll for the paycycle mentioned above"></asp:Label></legend>
                    <label>
                        <asp:Label ID="Label17" runat="server" Text="Request Date"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtreqdate"
                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtreqdate" runat="server" Width="87px" Enabled="False"></asp:TextBox>
                    </label>
                    <label>
                        <br />
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton2"
                            TargetControlID="txtreqdate">
                        </cc1:CalendarExtender>
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg"
                            Enabled="False" /></label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="lblrssc" runat="server" Text="RRSP contribution Amount"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequireldValidator9" runat="server" ControlToValidate="txtrssc"
                                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtrssc" runat="server" 
                        OnTextChanged="txtrssc_TextChanged" ></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                            FilterType="Custom, Numbers" TargetControlID="txtrssc" ValidChars="-.">
                        </cc1:FilteredTextBoxExtender>
                    </label>
                    <label class="Attend">
                        <br />
                        <asp:CheckBox ID="chkrss" runat="server" />
                    </label>
                    <label>
                        <br />
                        <asp:Label ID="lblmsd" runat="server" Text="Make this withholding for every paycycle for the year" />
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label15" runat="server" Text="Union Dues Amount"></asp:Label>
                         <asp:RequiredFieldValidator ID="RequiredValidator10" runat="server" ControlToValidate="txtUnionDues"
                                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtUnionDues" runat="server"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txtaddress_FilteredTextBoxExtender" runat="server"
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtUnionDues" ValidChars="-.">
                        </cc1:FilteredTextBoxExtender>
                    </label>
                    <label class="Attend">
                        <br />
                        <asp:CheckBox ID="chkUnionDues" runat="server" />
                    </label>
                    <label>
                        <br />
                        <asp:Label ID="Label14" runat="server" Text="Make this withholding for every paycycle for the year" />
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btnsubmit_Click"
                            ValidationGroup="1" />
                        <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btnSubmit" OnClick="btnupdate_Click"
                            Visible="False" ValidationGroup="1" />
                    </label>
                    <label>
                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="btncancel_Click" />
                    </label>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label4" runat="server" Text="List of Request for withholding for Retirement Savings and Union Dues"></asp:Label></legend>
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
                        <asp:Label ID="Label9" runat="server" Text="Business Name"></asp:Label>
                        <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlstrname"
                                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                        <asp:DropDownList ID="ddlfilterbus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfilterbus_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label10" runat="server" Text="Employee Name"></asp:Label>
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                        <asp:DropDownList ID="ddlfilteremp" runat="server" OnSelectedIndexChanged="ddlfilteremp_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label11" runat="server" Text="Year"></asp:Label>
                        <asp:DropDownList ID="ddlfilteryear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfilteryear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label12" runat="server" Text="Pay Cycle"></asp:Label>
                        <asp:DropDownList ID="ddlfilterpaycycle" runat="server" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterpaycycle_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <%-- <label>
                <asp:Label ID="Label16" runat="server" Text="Dedction Name"></asp:Label>
             
                <asp:DropDownList ID="ddlfilterdedctionname" runat="server" 
                AutoPostBack="true" Width="500px" 
                onselectedindexchanged="ddlfilterdedctionname_SelectedIndexChanged">
                  
                        
                </asp:DropDownList>
                                                  
             
            </label>--%>
                    <div style="clear: both;">
                    <br />
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
                                                    <asp:Label ID="lblemp" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblpay" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblyea" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblcy" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label7" runat="server" Font-Size="18px" Text="List of Request for withholding for Retirement Savings and Union Dues"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                        CellPadding="4" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        Width="100%" OnRowEditing="GridView1_RowEditing" AllowSorting="True" OnSorting="GridView1_Sorting"
                                        OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Request Date" ItemStyle-Width="4%" SortExpression="Date"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Business Name" ItemStyle-Width="15%" SortExpression="Wname" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblbname" runat="server" Text='<%# Eval("Wname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Year" ItemStyle-Width="5%" SortExpression="TaxYear_Name"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblno" runat="server" Text='<%# Eval("TaxYear_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Pay Cycle" ItemStyle-Width="10%" SortExpression="Paycycle"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRRSPpay" runat="server" Text='<%# Eval("Paycycle") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RRSP Contribution Amount" HeaderStyle-Width="130px" SortExpression="RRSPCotributionREcurringAMT"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrrsp" runat="server" Text='<%# Eval("RRSPCotributionREcurringAMT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Recurring ?" ItemStyle-Width="10%" SortExpression="RRSPRecurring"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrrsprec" runat="server" Text='<%# Eval("RRSPRecurring") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Union Dues Amount" HeaderStyle-Width="85px" SortExpression="UnionDuesRecurringAMT"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluni" runat="server" Text='<%# Eval("UnionDuesRecurringAMT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Recurring ?" ItemStyle-Width="10%" SortExpression="UnionDuesRecurring"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblunionrec" runat="server" Text='<%# Eval("UnionDuesRecurring") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:CommandField ShowSelectButton="true" HeaderImageUrl="~/Account/images/edit.gif"
                                                HeaderText="Edit" ButtonType="Image" SelectImageUrl="~/Account/images/edit.gif"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" Width="2%"></ItemStyle>
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <%--<asp:GridView ID="gvCustomres" runat="server" DataSourceID="customresDataSource"
                AutoGenerateColumns="False" GridLines="None" AllowPaging="true" CssClass="mGrid"
                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                <Columns>
                    <asp:BoundField DataField="ContactName" HeaderText="Department" />
                    <asp:BoundField DataField="PricePlan" HeaderText="Designation" />
                    <asp:BoundField DataField="PricePlan" HeaderText="Employee Id" />
                    <asp:BoundField DataField="PricePlan" HeaderText="Employee Name" />
                    <asp:BoundField DataField="PricePlan" HeaderText="Employee No" />
                    <asp:BoundField DataField="PricePlan" HeaderText="Status" />
                    <asp:BoundField DataField="PricePlan" HeaderText="Batch Name" />
                    <asp:HyperLinkField Text="Edit" HeaderText="Edit" />
                </Columns>
            </asp:GridView>--%>
                    <%--<asp:XmlDataSource ID="customresDataSource" runat="server" DataFile="~/App_Data/data1.xml">
            </asp:XmlDataSource>--%>
                </fieldset>
                <asp:HiddenField ID="hfidwhid" runat="server" />
                <asp:HiddenField ID="hfempid" runat="server" />
                <asp:HiddenField ID="hfbmid" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--end of right content-->
    <div style="clear: both;">
    </div>
</asp:Content>
