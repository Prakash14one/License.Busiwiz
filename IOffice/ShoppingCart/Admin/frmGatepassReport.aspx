<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="frmGatepassReport.aspx.cs" Inherits="Add_Employee_Attendance_Report_Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
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
        <fieldset>
            <label>
                <asp:Label ID="lblWarehouseName" Text="Business Name" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" Width="170px"
                    OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
            <label>
                <asp:Label ID="lblEmployeeName" Text="Employee Name" runat="server"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEmployeeName"
                    ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlEmployeeName" runat="server" AutoPostBack="True" Width="170px"
                    OnSelectedIndexChanged="ddEmployee_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
            <label>
                <asp:Label ID="lblPartyName" Text="Party Name" runat="server"></asp:Label>
                <asp:DropDownList ID="ddParty" runat="server" AutoPostBack="True" Width="170px">
                </asp:DropDownList>
            </label>
            <div style="clear: both;">
            </div>
            <label>
                <asp:Label ID="lblFromDate" Text="From Date" runat="server"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txteffectstart"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txteffectstart" runat="server" type="text" Width="80px" />
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txteffectstart"
                    TargetControlID="txteffectstart">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txteffectstart">
                </cc1:MaskedEditExtender>
                <%--<asp:ImageButton ID="ImageButton2" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" runat="server" height="16" width="16" />--%>
            </label>
            <label>
                <asp:Label ID="lblToDate" Text="To Date" runat="server"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txteffectend"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToCompare="txteffectstart"
                    ControlToValidate="txteffectend" ErrorMessage="*" Operator="GreaterThanEqual" Type="Date"
                    ValidationGroup="1"></asp:CompareValidator>
                <asp:TextBox ID="txteffectend" runat="server" Width="80px" type="text" />
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txteffectend"
                    TargetControlID="txteffectend">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txteffectend">
                </cc1:MaskedEditExtender>
                <%--<asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" height="16" width="16" />--%>
            </label>
            <div style="clear: both;">
            </div>
            <label>
                <asp:Button runat="server" CausesValidation="true" ValidationGroup="1" ID="btnGo"
                    CssClass="btnSubmit" Text="Go" OnClick="btnGo_Click" />
            </label>
        </fieldset>
    </div>
    <div style="clear: both;">
    </div>
    <fieldset>
        <legend>
            <asp:Label ID="lblListof" Text="List of Gatepass" runat="server" Style="font-weight: 700"></asp:Label>
        </legend>
        <div style="float: right;">
            <label>
                <asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button1"
                    CssClass="btnSubmit" Text="Printable Version" OnClick="Button1_Click" />
                <input type="button" value="Print" id="Button5" runat="server" onclick="javascript:CallPrint('divPrint')"
                    visible="False" class="btnSubmit" />
            </label>
        </div>
        <br />
        <br />
        <br />
        <div style="clear: both;">
        </div>
        <asp:Panel ID="pnlgrid" runat="server" Width="100%">
            <div id="mydiv" class="closed">
                <table width="100%">
                    <tr>
                        <td align="center" style="color: Black; font-style: italic" colspan="2">
                            <asp:Label ID="lblCompany" Font-Size="20px" runat="server" Font-Bold="True" Font-Italic="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="color: Black; font-style: italic" colspan="2">
                            <asp:Label ID="lblBusiness" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="20px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="lblhead" runat="server" Font-Bold="True" Font-Size="18px" Text=" List of Gatepass"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblEmployeeNamelbl" runat="server" Font-Bold="True" Font-Size="16px"
                                Text="Employee Name :"></asp:Label>
                            <asp:Label ID="lblEmp" runat="server" Font-Bold="True" Font-Size="16px"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:Label ID="lblPartylbl" runat="server" Font-Bold="True" Font-Size="16px" Text="Party Name :"></asp:Label>
                            <asp:Label ID="lblParty" runat="server" Font-Bold="True" Font-Size="16px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="clear: both;">
            </div>
            <asp:GridView ID="grdgatepassreport" runat="server" AutoGenerateColumns="False" GridLines="None"
                AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                DataKeyNames="Id" OnRowCommand="grdgatepassreport_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="5%" HeaderStyle-Width="5%"
                        HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblDate" Text='<%#Bind("Date","{0:MM/dd/yyyy}") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-Width="3%" HeaderText="Req No"
                        HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblGatePass" Text='<%#Bind("GatepassREQNo") %>' runat="server"></asp:Label>
                            <asp:Label ID="lblmasterid123" Width="40px" Visible="false" Text='<%#Bind("Id") %>'
                                runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"></HeaderStyle>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="15%" HeaderStyle-Width="15%"
                        HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblEmployeeName" Text='<%#Bind("EmployeeName") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee ID" ItemStyle-Width="5%" HeaderStyle-Width="5%"
                        HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblEmployeeID" Text='<%#Bind("EmployeeID") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Out Time (Approved)" ItemStyle-Width="6%" HeaderStyle-Width="6%"
                        HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtOuttime" Text='<%#Bind("ExpectedOutTime") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="In Time (Approved)" ItemStyle-Width="6%" HeaderStyle-Width="6%"
                        HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="txtInTime" Text='<%#Bind("ExpectedInTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PartyName" ItemStyle-Width="15%" HeaderStyle-Width="15%"
                        HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblPartyName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-Width="2%"
                        HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("Id") %>'
                                CommandName="view" ToolTip="View Profile" ImageUrl="~/Account/images/viewprofile.jpg">
                            </asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </fieldset>
    <!--end of right content-->
    <div style="clear: both;">
    </div>
</asp:Content>
