<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentAllocate.aspx.cs" Inherits="Account_DocumentAllocate"
    Title="Untitled Page" %>

<%@ Register Src="~/ioffice/Account/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        function ChangeCheckBoxState(id, checkState) {
            var cb = document.getElementById(id);
            if (cb != null)
                cb.checked = checkState;
        }

        function ChangeAllCheckBoxStates(checkState) {
            if (CheckBoxIDs != null) {
                for (var i = 0; i < CheckBoxIDs.length; i++)
                    ChangeCheckBoxState(CheckBoxIDs[i], checkState);
            }
        }
        function ChangeHeaderAsNeeded() {
            if (CheckBoxIDs != null) {
                for (var i = 1; i < CheckBoxIDs.length; i++) {
                    var cb = document.getElementById(CheckBoxIDs[i]);
                    if (!cb.checked) {
                        ChangeCheckBoxState(CheckBoxIDs[0], false);
                        return;
                    }
                }
                ChangeCheckBoxState(CheckBoxIDs[0], true);
            }
        }

        function ChangeAllCheckBoxStatesDes(checkState) {
            if (CheckBoxIDsDes != null) {
                for (var i = 0; i < CheckBoxIDsDes.length; i++)
                    ChangeCheckBoxState(CheckBoxIDsDes[i], checkState);
            }
        }
        function ChangeHeaderAsNeededDes() {
            if (CheckBoxIDsDes != null) {
                for (var i = 1; i < CheckBoxIDsDes.length; i++) {
                    var cb = document.getElementById(CheckBoxIDsDes[i]);
                    if (!cb.checked) {
                        ChangeCheckBoxState(CheckBoxIDsDes[0], false);
                        return;
                    }
                }
                ChangeCheckBoxState(CheckBoxIDsDes[0], true);
            }
        }
        // For employee
        function ChangeAllCheckBoxStatesEmp(checkState) {
            if (CheckBoxIDsEmp != null) {
                for (var i = 0; i < CheckBoxIDsEmp.length; i++)
                    ChangeCheckBoxState(CheckBoxIDsEmp[i], checkState);
            }
        }
        function ChangeHeaderAsNeededEmp() {
            if (CheckBoxIDsEmp != null) {
                for (var i = 1; i < CheckBoxIDsEmp.length; i++) {
                    var cb = document.getElementById(CheckBoxIDsEmp[i]);
                    if (!cb.checked) {
                        ChangeCheckBoxState(CheckBoxIDsEmp[0], false);
                        return;
                    }
                }
                ChangeCheckBoxState(CheckBoxIDsEmp[0], true);
            }
        } 
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <table id="secondtbl" cellpadding="0" cellspacing="5" width="100%">
                        <tr>
                            <td align="left" colspan="2">
                            <label></label>
                                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 3%;">
                                <label>
                                    1</label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Do you wish to allocate document to the "></asp:Label>
                                </label>
                                <asp:RadioButtonList ID="rdsetrul" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                    OnSelectedIndexChanged="rdsetrul_SelectedIndexChanged">
                                    <asp:ListItem Value="True" Selected="True" Text="Selected Employee of All Business"></asp:ListItem>
                                    <asp:ListItem Value="False" Text="Selected Employee of One Business"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                        <td style="width: 3%;">
                             <label>
                                    2</label>
                            </td>
                            <td>
                               
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="Select the document for selected employee for document processing"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr> <td style="width: 3%;">
                             <label></label>
                            </td>
                            <td>
                              
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlbusiness" runat="server" Width="205px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr> <td style="width: 3%;">
                             <label></label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Step 1 : Select Documents to Allocate"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr> <td style="width: 3%;">
                             <label></label>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pbln" runat="server">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnldocument" runat="server" Width="100%">
                                                                <asp:GridView ID="grdDocList" DataKeyNames="DocumentId" runat="server" CssClass="mGrid"
                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True"
                                                                    AllowPaging="true" AutoGenerateColumns="False" EmptyDataText="There are no documents available to allocate"
                                                                    OnPageIndexChanging="grdDocList_PageIndexChanging" OnRowDataBound="grdDocList_RowDataBound"
                                                                    OnRowCommand="grdDocList_RowCommand" OnSorting="grdDocList_Sorting" Width="100%"
                                                                    PageSize="20">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkDoc" runat="server" />
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="HeaderChkbox" runat="server" AutoPostBack="True" OnCheckedChanged="HeaderChkbox_CheckedChanged" />
                                                                            </HeaderTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Date" DataField="DocumentUploadDate" DataFormatString="{0:MM/dd/yyyy-HH:mm}"
                                                                            SortExpression="DocumentUploadDate" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Title" SortExpression="DocumentTitle" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <a onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','width=1000,height=650,menubar=no,status=no')"
                                                                                    href="javascript:void(0)" style="color: Black">
                                                                                    <%#DataBinder.Eval(Container.DataItem, "DocumentTitle")%>
                                                                                </a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Folder" DataField="DocumentType" SortExpression="DocumentType"
                                                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="Party Name" DataField="PartyName" SortExpression="PartyName"
                                                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="Uploaded By" DataField="EmployeeName" SortExpression="EmployeeName"
                                                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Edit" Visible="false" ShowHeader="False" HeaderStyle-HorizontalAlign="Left"
                                                                            ItemStyle-Width="3%" HeaderImageUrl="~/Account/images/edit.gif">
                                                                            <ItemTemplate>
                                                                                <a onclick="window.open('DocumentEditAndView.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                                                    href="javascript:void(0)" target="_blank">
                                                                                    <asp:Image ImageUrl="~/Account/images/edit.gif" ID="Image1" runat="server" />
                                                                                </a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delete" Visible="false" ShowHeader="False" HeaderStyle-HorizontalAlign="Left"
                                                                            ItemStyle-Width="3%" HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImageButton2" CommandName="delete1" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>'
                                                                                    runat="server" CausesValidation="false" ImageUrl="~/Account/images/delete.gif"
                                                                                    Text="" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                                    ToolTip="Delete" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                                <input id="hdncnfm" type="hidden" name="hdncnfm" runat="Server" />
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr> <td style="width: 3%;">
                             <label></label>
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnldesignation" Visible="false" runat="server" Width="100%">
                                                <asp:GridView ID="grdDesignation" runat="server" DataKeyNames="DesignationId" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True"
                                                    AutoGenerateColumns="False" EmptyDataText="No one Desigantion avilable in Document Departmant"
                                                    OnRowDataBound="grdDesignation_RowDataBound" Width="100%" OnSorting="grdDesignation_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField ShowHeader="False" HeaderStyle-Width="10px" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="HeaderChkboxDes" runat="server" AutoPostBack="True" OnCheckedChanged="HeaderChkboxDes_CheckedChanged" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkDesignation" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Designation" DataField="DesignationName" SortExpression="DesignationName"
                                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Button ID="imgbShowEmp" runat="server" CssClass="btnSubmit" Text="Show Employees"
                                                    OnClick="imgbShowEmp_Click" />
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr> <td style="width: 3%;">
                             <label></label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Step 2 : Select Employee for Allocate"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr> <td style="width: 5%;">
                             <label></label>
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlEmpoylee" runat="server">
                                                <asp:GridView ID="grdEmployeeList" runat="server" DataKeyNames="EmployeeId" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True"
                                                    AutoGenerateColumns="False" EmptyDataText="Select the designation you wish to allocate to"
                                                    OnPageIndexChanging="grdEmployeeList_PageIndexChanging" OnRowDataBound="grdEmployeeList_RowDataBound"
                                                    Width="100%" OnSorting="grdEmployeeList_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField ShowHeader="False" HeaderStyle-Width="10px">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="HeaderChkboxEmp" runat="server" AutoPostBack="True" OnCheckedChanged="HeaderChkboxEmp_CheckedChanged" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkEmployee" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Designation" DataField="DesignationName" SortExpression="DesignationName"
                                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                        <asp:BoundField HeaderText="Business:Employee Name" DataField="EmployeeName" SortExpression="EmployeeName"
                                                            HeaderStyle-HorizontalAlign="Left" />
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr> <td style="width: 5%;">
                             <label></label>
                            </td>
                            <td align="center">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="imgbtnallocate" CssClass="btnSubmit" runat="server" Text="Allocate Now"
                                                OnClick="imgbtnallocate_Click" />
                                        </td>
                                        <input style="width: 1px" id="Hidden1" type="hidden" name="hdnsortExp" runat="Server" />
                                        <input style="width: 1px" id="Hidden2" type="hidden" name="hdnsortDir" runat="Server" />
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td colspan="4">
                                <asp:Panel ID="pnlconfirmmsg" runat="server" BorderStyle="Outset" Height="100px"
                                    Width="300px" BackColor="#CCCCCC" BorderColor="#666666">
                                    <table id="innertbl1">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelMainTypeAdd" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table cellspacing="0" cellpadding="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <%--<td class="secondtblfc3" colspan="1">
                                            <asp:ImageButton ID="ibtnCancelCabinetAdd" runat="server"
                                                ImageUrl="~/Account/images/closeicon.png" AlternateText="Close" CausesValidation="False">
                                            </asp:ImageButton>
                                        </td>--%>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="pnlconfirmmsgub" runat="server" Width="100%" Height="75px">
                                                                            <table cellspacing="3" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td colspan="2" style="font-weight: bold; padding-left: 10px; font-size: 12px; font-family: Arial;
                                                                                            text-align: left; vertical-align: top;">
                                                                                            <br />
                                                                                            Are you sure, You want to Delete a Record?
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <br />
                                                                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:Button ID="imgconfirmok" runat="server" Text=" Ok " CausesValidation="False"
                                                                                                        OnClick="imgconfirmok_Click" />
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                        <td>
                                                                                            <br />
                                                                                            <asp:Button ID="imgconfirmcalcel" runat="server" Text="Close" CausesValidation="False" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <cc1:ModalPopupExtender ID="mdlpopupconfirm" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="pnlconfirmmsg" TargetControlID="hdnconfirm" CancelControlID="imgconfirmcalcel"
                                    X="250" Y="-200" Drag="true">
                                </cc1:ModalPopupExtender>
                                <input id="hdnconfirm" runat="Server" name="hdnconfirm" type="hidden" style="width: 4px" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
