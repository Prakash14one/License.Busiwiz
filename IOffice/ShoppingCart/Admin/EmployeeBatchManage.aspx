<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="EmployeeBatchManage.aspx.cs" Inherits="Add_Employee_Batch" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
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
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
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
                <asp:Panel ID="pnladd" runat="server" Visible="false">
                    <fieldset>
                        <label>
                            <asp:Label ID="Label11" runat="server" Text="You have selected to update the batch information of: "></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <br />
                        </label>
                        <label>
                            <asp:Label ID="lblempno" runat="server" CssClass="lblSuggestion" Text=""></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblemp" runat="server" CssClass="lblSuggestion" Text=""></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lbldesig" runat="server" CssClass="lblSuggestion" Text=""></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lbldept" runat="server" CssClass="lblSuggestion" Text=""></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label15" runat="server" Text="Would you like to change the batch?"></asp:Label>
                        </label>
                        <asp:CheckBox ID="ChkYes" runat="server" Text="Yes" TextAlign="Left" AutoPostBack="true"
                            OnCheckedChanged="ChkYes_CheckedChanged" />
                        <%--<asp:RadioButtonList ID="rblchange" AutoPostBack="true" runat="server" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="rblchange_SelectedIndexChanged">
                            <asp:ListItem Text="Business" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Batch" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Both" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>--%>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="pnlbusiness" runat="server" Visible="false">
                            <table width="100%">
                                <tr valign="top">
                                    <td width="15%">
                                        <label>
                                            <asp:Label ID="Label2" runat="server" Text="Business Name"></asp:Label>
                                            <asp:Label ID="Label13" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlstrname"
                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td width="85%">
                                        <label>
                                            <asp:DropDownList ID="ddlstrname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstrname_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="pnlbatch" runat="server" Visible="false">
                            <table width="100%">
                                <tr valign="top">
                                    <td width="15%">
                                        <label>
                                            <asp:Label ID="Label1" runat="server" Text="Batch List"></asp:Label>
                                            <asp:Label ID="Label14" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlstrbatchname"
                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                            <asp:CheckBox ID="CheckBox1" Visible="false" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged1"
                                                AutoPostBack="True" />
                                        </label>
                                    </td>
                                    <td width="85%">
                                        <label>
                                            <asp:DropDownList ID="ddlBatchName" Visible="false" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlstrbatchname" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DropDownList3" runat="server" Visible="false">
                                            </asp:DropDownList>
                                        </label>
                                        <label>
                                            <%--<asp:Label ID="Label3" runat="server" Text="Employee List" Visible="false"></asp:Label>--%>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownList3"
                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                        <br />
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btnsubmit_Click"
                            Visible="False" />
                        <asp:Button ID="btnreset" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="btnreset_Click"
                            Visible="False" />
                        <asp:Button ID="btnupdate" runat="server" Text="Update" ValidationGroup="1" CssClass="btnSubmit"
                            OnClick="btnupdate_Click" />
                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="btncancel_Click" />
                    </fieldset></asp:Panel>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label4" runat="server" Text="List of Employee Batches"></asp:Label></legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" CssClass="btnSubmit" runat="server" Text="Printable Version"
                            OnClick="Button1_Click" />
                        <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="False" />
                    </div>
                    <label>
                        <asp:Label ID="Label16" runat="server" Text="Filter By:"></asp:Label>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label8" runat="server" Text="Business Name"></asp:Label>
                        <asp:DropDownList ID="ddlfilterstore" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfilterstore_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label10" runat="server" Text="Batch Name"></asp:Label>
                        <asp:DropDownList ID="ddlfilterbatch" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlfilterbatch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label9" runat="server" Text="Employee Name"></asp:Label>
                        <asp:DropDownList ID="ddlfilteremployee" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilteremployee_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label6" runat="server" Text="Status"></asp:Label>
                        <asp:DropDownList ID="ddlstatus" runat="server" Width="100px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                            <asp:ListItem Text="All" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Size="20px" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label5" runat="server" Font-Size="20px" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusines" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label7" runat="server" Font-Size="18px" Text="List of Employee Batches"></asp:Label>
                                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                        AllowSorting="true" CellPadding="4" GridLines="Both" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" Width="100%" OnRowEditing="GridView1_RowEditing"
                                        EmptyDataText="No Record Found." OnSorting="GridView1_Sorting" 
                                        onpageindexchanging="GridView1_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName" ItemStyle-Width="15%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee No." SortExpression="EmployeeNo" ItemStyle-Width="8%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblno" runat="server" Text='<%# Eval("EmployeeNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" SortExpression="DesignationName" ItemStyle-Width="12%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("DesignationName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department" ItemStyle-Width="12%" SortExpression="Departmentname"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldept" runat="server" Text='<%# Eval("Departmentname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%" SortExpression="Active"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblactive" runat="server" Text='<%# Eval("Active") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Id" Visible="false" SortExpression="EmployeeMasterID"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblempid" runat="server" Text='<%# Eval("EmployeeMasterID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch Name / Normal Time" ItemStyle-Width="27%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatch" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                    <asp:Label ID="Label12" runat="server"></asp:Label>
                                                    <asp:Label ID="lblbatchid" Visible="false" runat="server" Text='<%# Eval("Batchmasterid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left"
                                                HeaderImageUrl="~/Account/images/edit.gif">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="edit" CommandName="Edit" ToolTip="Edit" ImageUrl="~/Account/images/edit.gif"
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc11:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
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
