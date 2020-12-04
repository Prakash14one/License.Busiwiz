<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="ExternalVisitRequestLog.aspx.cs" Inherits="Add_frmgatepass_approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="products_box">
        <div style="padding-left: 1%">
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="Record submitted successfully"
                Visible="False"></asp:Label>
        </div>
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
                <asp:Button ID="Button1" runat="server" Text="Go" CssClass="btnSubmit" OnClick="Button1_Click" ValidationGroup="1" />
            </label>
        </fieldset>
        <div style="clear: both;">
        </div>
        <fieldset>
            <legend>
                <asp:Label ID="lblListDetail" Text="List of External Visit Requests" runat="server"
                    Style="font-weight: 700"></asp:Label>
            </legend>
            <div style="clear: both;">
            </div>
            <asp:GridView ID="GridView1" runat="server" DataKeyNames="Id" GridLines="None" AllowPaging="true"
                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                AutoGenerateColumns="False" EmptyDataText="No Record Found." Width="100%" OnRowCommand="GridView1_RowCommand"
                OnRowEditing="GridView1_RowEditing">
                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderText="Req. No.">
                        <ItemTemplate>
                            <asp:Label ID="lblGatePass" Text='<%#Bind("GatepassREQNo") %>' runat="server"></asp:Label>
                            <asp:Label ID="lblmasterid123" Visible="false" Text='<%#Bind("Id") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Name" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblEmployeeName" Text='<%#Bind("EmployeeName") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblDate" Text='<%#Bind("Date","{0:MM/dd/yyyy}") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expected Out Time" HeaderStyle-Width="3%" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtOuttime" Text='<%#Bind("ExpectedOutTime") %>' runat="server"></asp:Label>
                            <%--<asp:TextBox ID="txtOuttime" Width="80px" Text='<%#Bind("ExpectedOutTime") %>' runat="server"></asp:TextBox>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expected In Time" HeaderStyle-Width="3%" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtInTime" Text='<%#Bind("ExpectedInTime") %>' runat="server"></asp:Label>
                            <%--<asp:TextBox runat="server" Width="80px" ID="txtInTime" Text='<%#Bind("ExpectedInTime") %>'></asp:TextBox>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PartyName : Project : Task : Purpose" HeaderStyle-Width="22%"
                        HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblPartyName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approval Status" HeaderStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="ddlApprovalStatus" Text='<%#Bind("Approved") %>' runat="server"></asp:Label>
                            <%--  <asp:DropDownList ID="ddlApprovalStatus" Width="100px" runat="server">
                                <asp:ListItem Value="1">Pending</asp:ListItem>
                                <asp:ListItem Value="2">Approved</asp:ListItem>
                                <asp:ListItem Value="3">Rejected</asp:ListItem>
                            </asp:DropDownList>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approval Note" ItemStyle-Width="25%" HeaderStyle-Width="25%"
                        HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtApprovalNote" Text='<%#Bind("GatePassApprovalnote") %>' runat="server"></asp:Label>
                            <%--   <asp:TextBox Width="300px" TextMode="MultiLine" ID="txtApprovalNote" runat="server"
                                Text='<%#Bind("GatePassApprovalnote") %>' MaxLength="50"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                SetFocusOnError="True" ValidationExpression="^([.a-zA-Z0-9\s]*)" ControlToValidate="txtApprovalNote"
                                ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                        </ItemTemplate>
                        <HeaderStyle Width="25%"></HeaderStyle>
                        <ItemStyle Width="25%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Details of Meeting" ItemStyle-Width="25%" HeaderStyle-Width="25%"
                        HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbldetmweeting" runat="server" Text='<%#Bind("PurposeofVisit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="4%"
                        Visible="false">
                        <ItemTemplate>
                            <asp:Button ID="btnedit" runat="server" HeaderImageUrl="~/Account/images/Edit.gif"
                                CssClass="btnSubmit" CommandArgument='<%# Eval("Id") %>' ButtonType="Image" ImageUrl="~/Account/images/Edit.gif"
                                CommandName="Edit" Text="Edit" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pgr"></PagerStyle>
            </asp:GridView>
            <div style="clear: both;">
            </div>
            <label>
                <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="btnSubmit" OnClick="btnSubmit_Click"
                    Visible="False" ValidationGroup="1" />
            </label>
            <label>
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btnSubmit" Visible="False" />
            </label>
        </fieldset>
    </div>
    <!--end of right content-->
    <div style="clear: both;">
    </div>
</asp:Content>
