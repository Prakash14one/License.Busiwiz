<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="frmGatePassProfile.aspx.cs" Inherits="Add_Page_Role_Access" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
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
                <asp:Label ID="lblBusinessName" Text="Business Name" runat="server"></asp:Label>
                <asp:DropDownList ID="ddBusiness" runat="server" AutoPostBack="True" Width="170px"
                    OnSelectedIndexChanged="ddBusiness_SelectedIndexChanged" Enabled="False">
                </asp:DropDownList>
            </label>
            <label>
                <asp:Label ID="lblDepartment" Text="Department" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" Width="170px"
                    Enabled="False">
                </asp:DropDownList>
            </label>
            <label>
                <asp:Label ID="lblDesignation" Text="Designation" runat="server"></asp:Label>
                <asp:DropDownList ID="ddDesignation" runat="server" AutoPostBack="True" Width="170px"
                    OnSelectedIndexChanged="ddDesignation_SelectedIndexChanged" Enabled="False">
                </asp:DropDownList>
            </label>
            <div style="clear: both;">
            </div>
            <label>
                <asp:Label ID="lblEmployeeName" Text="Employee Name" runat="server"></asp:Label>
                <asp:DropDownList ID="ddEmployee" runat="server" AutoPostBack="True" Width="170px"
                    OnSelectedIndexChanged="ddEmployee_SelectedIndexChanged" Enabled="False">
                </asp:DropDownList>
            </label>
            <label>
                <asp:Label ID="lblStatus" Text="Status" runat="server"></asp:Label>
                <asp:DropDownList ID="ddStatus" runat="server" AutoPostBack="True" Width="170px"
                    OnSelectedIndexChanged="ddStatus_SelectedIndexChanged">
                    <asp:ListItem Value="1">Pending</asp:ListItem>
                    <asp:ListItem Value="2">Approved</asp:ListItem>
                    <asp:ListItem Value="3">Rejected</asp:ListItem>
                </asp:DropDownList>
            </label>
            <label>
                <asp:Label ID="lblGateReqNolbl" Text="Gate Requision" runat="server"></asp:Label>
                <asp:DropDownList ID="ddGateReq" runat="server" AutoPostBack="True" Width="170px"
                    OnSelectedIndexChanged="ddGateReq_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
            <div style="clear: both;">
            </div>
            <div style="float: right;">
                <label>
                    <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                        OnClick="Button1_Click" />
                    <input type="button" value="Print" id="Button5" runat="server" onclick="javascript:CallPrint('divPrint')"
                        visible="False" class="btnSubmit" />
                </label>
            </div>
            <div style="clear: both;">
            </div>
            <label class="cssLabelCompany_Information">
                <asp:Label ID="lblreqlbl" Text="Gate Pass Req. No." runat="server"></asp:Label>
            </label>
            <label class="cssLabelCompany_Information_Ans">
                <asp:Label ID="lblREQ" CssClass="lblSuggestion" runat="server"></asp:Label>
            </label>
            <div style="clear: both;">
            </div>
            <label class="cssLabelCompany_Information">
                <asp:Label ID="lblEmplbl" Text="Employee Name" runat="server"></asp:Label>
            </label>
            <label class="cssLabelCompany_Information_Ans">
                <asp:Label ID="lblEmpName" CssClass="lblSuggestion" runat="server"></asp:Label>
            </label>
            <div style="clear: both;">
            </div>
            <label class="cssLabelCompany_Information">
                <asp:Label ID="lblGatepassNlbl" Text="Gate Pass Number" runat="server"></asp:Label>
            </label>
            <label class="cssLabelCompany_Information_Ans">
                <asp:Label ID="lblGatePassNo" CssClass="lblSuggestion" runat="server"></asp:Label>
            </label>
            <div style="clear: both;">
            </div>
            <label class="cssLabelCompany_Information">
                <asp:Label ID="lbldatetimelbl" Text="Date" runat="server"></asp:Label>
            </label>
            <label class="cssLabelCompany_Information_Ans">
                <asp:Label ID="lblDate" CssClass="lblSuggestion" runat="server" Style="font-weight: 700"></asp:Label>
            </label>
            <div style="clear: both;">
            </div>
            <label class="cssLabelCompany_Information">
                <asp:Label ID="lblExpGoingTimelbl" Text="Expected Exit Time" runat="server"></asp:Label>
            </label>
            <label class="cssLabelCompany_Information_Ans">
                <asp:Label ID="lblExpTime" CssClass="lblSuggestion" runat="server"></asp:Label>
            </label>
            <div style="clear: both;">
            </div>
            <label class="cssLabelCompany_Information">
                <asp:Label ID="lblExpReturnTime" Text="Expected Return Time" runat="server"></asp:Label>
            </label>
            <label class="cssLabelCompany_Information_Ans">
                <asp:Label ID="lblReturnTime" CssClass="lblSuggestion" runat="server"></asp:Label>
            </label>
            <div style="clear: both;">
            </div>
            <label class="cssLabelCompany_Information">
                <asp:Label ID="lblApprovalStatuslbl" Text="Status" runat="server"></asp:Label>
            </label>
            <label class="cssLabelCompany_Information_Ans">
                <asp:Label ID="lblApprovalStatus" CssClass="lblSuggestion" runat="server" Style="font-weight: 700"></asp:Label>
            </label>
            <div style="clear: both;">
            </div>
            <label class="cssLabelCompany_Information">
                <asp:Label ID="lblApprovalBylbl" Text="Approval By" runat="server"></asp:Label>
            </label>
            <label class="cssLabelCompany_Information_Ans">
                <asp:Label ID="lblApprovalBy" CssClass="lblSuggestion" runat="server" Style="font-weight: 700"></asp:Label>
            </label>
            <div style="clear: both;">
            </div>
            <label class="cssLabelCompany_Information">
                <asp:Label ID="lblApprovalTimelbl" Text="Approval Date" runat="server"></asp:Label>
            </label>
            <label class="cssLabelCompany_Information_Ans">
                <asp:Label ID="lblApprovalTime" CssClass="lblSuggestion" runat="server" Style="font-weight: 700"></asp:Label>
            </label>
            <div style="clear: both;">
            </div>
            <label class="cssLabelCompany_Information">
                <asp:Label ID="lblApprovalNotelbl" Text="Approval Note" runat="server"></asp:Label>
            </label>
            <label class="cssLabelCompany_Information_Ans">
                <asp:Label ID="lblApprovalNote" CssClass="lblSuggestion" runat="server" Style="font-weight: 700"></asp:Label>
            </label>
            <div style="clear: both;">
            </div>
        </fieldset>
        <fieldset>
            <legend>
                <asp:Label ID="lblAssignedTask" Text="List of Visits" runat="server" Style="font-weight: 700"></asp:Label>
            </legend>
            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                <div style="clear: both;">
                </div>
                <div style="clear: both;">
                </div>
                <div id="mydiv" class="closed">
                    <table width="100%">
                        <tr>
                            <td align="center" style="color: Black; font-style: italic">
                                <asp:Label ID="lblCompany" Font-Size="20px" runat="server" Font-Bold="True" Font-Italic="True"
                                    Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="color: Black; font-style: italic">
                                <asp:Label ID="Label1" Font-Size="18px" runat="server" Font-Italic="True" Text="Business :"
                                    Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="True" Font-Size="18px" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblhead" runat="server" Font-Bold="True" Font-Size="18px" Text=" List of Visits"
                                    Font-Italic="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblapprovalDatelbl" runat="server" Font-Bold="True" Font-Italic="true"
                                    Font-Size="16px" Text="Approval Date :"></asp:Label>
                                <asp:Label ID="lblappdate" runat="server" Font-Bold="True" Font-Size="16px" Font-Italic="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblDepartmentlbl" runat="server" Font-Size="14px" Text="Department Name :"
                                    Font-Italic="true"></asp:Label>
                                <asp:Label ID="lblDepartment1" runat="server" Font-Size="14px" Font-Italic="true"></asp:Label>,
                                <%--<asp:Label ID="lblDesignationlbl" runat="server" Font-Size="14px" Text="Party Name :"
                                    Font-Italic="true"></asp:Label>
                                <asp:Label ID="lblDesignation1" runat="server" Font-Size="14px" Font-Italic="true"></asp:Label>,--%>
                                <asp:Label ID="lblEmployeeNamelbl" runat="server" Font-Size="14px" Text="Employee Name :"
                                    Font-Italic="true"></asp:Label>
                                <asp:Label ID="lblEmp1" runat="server" Font-Size="14px" Font-Italic="true"></asp:Label>,
                                <asp:Label ID="lblstatuslbl" runat="server" Font-Size="14px" Text="Status :" Font-Italic="true"></asp:Label>
                                <asp:Label ID="lblstatus1" runat="server" Font-Size="14px" Font-Italic="true"></asp:Label>,
                                <asp:Label ID="lblGatepassREQlbl" runat="server" Font-Size="14px" Text="Gatepass Req :"
                                    Font-Italic="true"></asp:Label>
                                <asp:Label ID="lblGatepassREQ1" runat="server" Font-Size="14px" Font-Italic="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:GridView ID="grdparty" runat="server" AutoGenerateColumns="False" GridLines="Both"
                    Width="100%" AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                    <Columns>
                        <asp:TemplateField HeaderText="GatePass" HeaderStyle-HorizontalAlign="Left" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%#Bind("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%"
                            ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lblVisitParty" runat="server" Text='<%#Bind("VisitParty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Purpose" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40%"
                            ItemStyle-Width="40%">
                            <ItemTemplate>
                                <asp:Label ID="lblPurpose" runat="server" Text='<%#Bind("PurposeofVisit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Related Task" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40%"
                            ItemStyle-Width="40%">
                            <ItemTemplate>
                                <asp:Label ID="lblRelatedTask1" runat="server" Text='<%#Bind("RelatedTaskName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </fieldset>
    </div>
    <!--end of right content-->
    <div style="clear: both;">
    </div>
</asp:Content>
