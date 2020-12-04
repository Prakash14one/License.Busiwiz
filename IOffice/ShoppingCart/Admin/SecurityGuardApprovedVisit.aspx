<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="SecurityGuardApprovedVisit.aspx.cs" Inherits="Add_frmgatepass_approval" %>

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
    <div class="products_box">
        <div style="padding-left: 1%">
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="Record submitted successfully"
                Visible="False"></asp:Label>
        </div>
        <asp:Panel ID="panelfdf" runat="server" Visible="false">
            <fieldset>
                <label>
                    <asp:Label ID="lblBusinessName" Text="Business Name" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
                <label>
                    <asp:Label ID="lblStatus" Text="Status" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                        <asp:ListItem Value="1">Pending</asp:ListItem>
                        <asp:ListItem Value="2">Approved</asp:ListItem>
                        <asp:ListItem Value="3">Rejected</asp:ListItem>
                    </asp:DropDownList>
                </label>
                <label>
                    <asp:Label ID="lblDepartment" Text="Department" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </label>
                <label>
                    <asp:Label ID="lblDesignation" Text="Designation" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlDesignation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
                <label>
                    <asp:Label ID="lblEmployeeName" Text="Employee Name" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlEmployeeName" runat="server" OnSelectedIndexChanged="ddlEmployeeName_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </label>
                <div style="clear: both;">
                </div>
                <label>
                    <asp:Label ID="Label1" Text="Project Name" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlproject" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlproject_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
                <label>
                    <asp:Label ID="Label2" Text="Task Name" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddltask" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltask_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
                <label>
                    <asp:Label ID="Label10" runat="server" Text="From"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfromdate"
                        ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtfromdate" runat="server" Width="70px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtfromdate"
                        TargetControlID="txtfromdate">
                    </cc1:CalendarExtender>
                </label>
                <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtfromdate">
                </cc1:MaskedEditExtender>
                <label>
                    <asp:Label ID="Label11" runat="server" Text="To"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttodate"
                        ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txttodate" runat="server" Width="70px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txttodate"
                        TargetControlID="txttodate">
                    </cc1:CalendarExtender>
                </label>
                <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txttodate">
                </cc1:MaskedEditExtender>
                <div style="clear: both;">
                </div>
                <label>
                    <asp:Button ID="Button1" runat="server" Text="Go" CssClass="btnSubmit" OnClick="Button1_Click"
                        ValidationGroup="1" />
                </label>
            </fieldset>
        </asp:Panel>
        <div style="clear: both;">
        </div>
        <fieldset>
            <legend>
                <asp:Label ID="lblListDetail" Text="Security Guard - Approved External Visits" runat="server"
                    Style="font-weight: 700"></asp:Label>
            </legend>
            <div style="clear: both;">
            </div>
            <div style="float: right">
                <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Printable Version"
                    OnClick="Button1_Click1" />
                <input id="Button3" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                    style="width: 51px;" type="button" value="Print" visible="false" />
            </div>
            <div style="clear: both;">
            </div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div id="mydiv" class="closed">
                                            <table width="850Px">
                                                <tr align="center">
                                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                        <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="Security Guard Approved External Visits"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:GridView ID="GridView1" runat="server" DataKeyNames="Id" GridLines="None" AllowPaging="true"
                                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            AutoGenerateColumns="False" EmptyDataText="No Record Found." Width="100%" OnRowCommand="GridView1_RowCommand"
                                            OnRowEditing="GridView1_RowEditing">
                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" HeaderText="Business Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBusiness" Text='<%#Bind("Wname") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblmasterid123" Visible="false" Text='<%#Bind("Id") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmployeeName" Text='<%#Bind("EmployeeName") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="Label3" Text='<%#Bind("EmployeeID") %>' runat="server" Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" HeaderStyle-Width="7%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" Text='<%#Bind("Date","{0:MM/dd/yyyy}") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Expected Out Time" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOuttime" Text='<%#Bind("ExpectedOutTime") %>' runat="server"></asp:Label>
                                                        <%--<asp:TextBox ID="txtOuttime" Width="80px" Text='<%#Bind("ExpectedOutTime") %>' runat="server"></asp:TextBox>--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Expected In Time" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtInTime" Text='<%#Bind("ExpectedInTime") %>' runat="server"></asp:Label>
                                                        <%--<asp:TextBox runat="server" Width="80px" ID="txtInTime" Text='<%#Bind("ExpectedInTime") %>'></asp:TextBox>--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actual Out Time" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ActualOuttime" Text='<%#Bind("TimeReached") %>' runat="server"></asp:Label>
                                                        <%--<asp:TextBox ID="txtOuttime" Width="80px" Text='<%#Bind("ExpectedOutTime") %>' runat="server"></asp:TextBox>--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Return Back To Office"
                                                    HeaderStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Return" ForeColor="Black" CommandArgument='<%# Eval("Id") %>'
                                                            CommandName="Return"></asp:LinkButton>
                                                        <%--<asp:CheckBox ID="chkParty" runat="server" OnClick="LinkButton1_Click"  OnCheckedChanged="chkParty_CheckedChanged" AutoPostBack="true" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <div style="clear: both;">
            </div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="Panel21" runat="server" BackColor="White" BorderColor="#999999" Width="400px"
                            Height="240px" BorderStyle="Solid" BorderWidth="10px">
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td>
                                        <table style="width: 100%; font-weight: bold; color: #000000;" bgcolor="#CCCCCC">
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label17" runat="server" Text="Return back Report"></asp:Label>
                                                    </label>
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                        OnClick="ImageButton3_Click" Width="15px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="Label5" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Company ID
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        User ID
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Password
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button4" runat="server" Text="Submit" CssClass="btnSubmit" 
                                                        onclick="Button4_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalPopupExtenderAddnew" runat="server" BackgroundCssClass="modalBackground"
                            Enabled="True" PopupControlID="Panel21" TargetControlID="HiddenButton222">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <!--end of right content-->
    <div style="clear: both;">
    </div>
</asp:Content>
