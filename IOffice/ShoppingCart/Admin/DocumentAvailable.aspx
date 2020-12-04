<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentAvailable.aspx.cs" Inherits="DocumentAvailable"
    Title="Untitled Page" %>

<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
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

    <div style="float: left;">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div style="clear: both;">
    </div>
    <fieldset>
        <table id="innertbl1" cellpadding="0" cellspacing="3" width="100%">
            <tr>
                <td style="width: 35%; text-align: right;" align="right">
                    <label>
                        <asp:Label ID="Label4" runat="server" Text="Business Name"></asp:Label>
                    </label>
                </td>
                <td style="width: 15%">
                    <label>
                        <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                            Width="150px">
                        </asp:DropDownList>
                    </label>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_doc_appvd_by_deo"
                ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                </td>
                <td style="width: 35%">
                </td>
                <td style="width: 15%">
                </td>
            </tr>
            <tr>
                <td style="width: 35%" align="right">
                    <label>
                        <asp:Label ID="Label6" runat="server" Text="Document Approved by Office Clerk"></asp:Label>
                    </label>
                </td>
                <td style="width: 15%">
                    <label>
                        <asp:DropDownList ID="ddl_doc_appvd_by_deo" runat="server" DataTextField="EmployeeName"
                            DataValueField="EmployeeID" ValidationGroup="1" OnSelectedIndexChanged="ddl_doc_appvd_by_deo_SelectedIndexChanged"
                            Width="150px">
                        </asp:DropDownList>
                    </label>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_doc_appvd_by_deo"
                ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                </td>
                <td align="right" style="width: 35%">
                    <label>
                        <asp:Label ID="Label7" runat="server" Text="Document Approved by Supervisor"></asp:Label>
                    </label>
                </td>
                <td style="width: 15%">
                    <label>
                        <asp:DropDownList ID="ddl_doc_not_appvd_by_sup" runat="server" DataTextField="EmployeeName"
                            DataValueField="EmployeeID" ValidationGroup="1" OnSelectedIndexChanged="ddl_doc_not_appvd_by_sup_SelectedIndexChanged"
                            Width="150px">
                        </asp:DropDownList>
                    </label>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_doc_not_appvd_by_sup"
                ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td style="width: 35%" align="right">
                    <label>
                        <asp:Label ID="Label8" runat="server" Text="Select Office Clerk's Approval Status"></asp:Label>
                    </label>
                </td>
                <td style="width: 15%">
                    <label>
                        <asp:DropDownList ID="ddldeostatus" runat="server" DataTextField="EmployeeName" DataValueField="EmployeeID"
                            ValidationGroup="1" OnSelectedIndexChanged="ddldeostatus_SelectedIndexChanged"
                            Width="150px">
                            <asp:ListItem>-Select-</asp:ListItem>
                            <asp:ListItem Value="True">Approved</asp:ListItem>
                            <asp:ListItem Value="False">Not Approved</asp:ListItem>
                        </asp:DropDownList>
                    </label>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddl_doc_appvd_by_deo"
                ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                </td>
                <td align="right" style="width: 35%">
                    <label>
                        <asp:Label ID="Label9" runat="server" Text="Select Supervisor's Approval Status"></asp:Label>
                    </label>
                </td>
                <td style="width: 15%">
                    <label>
                        <asp:DropDownList ID="ddlsupstatus" runat="server" DataTextField="EmployeeName" DataValueField="EmployeeID"
                            ValidationGroup="1" Width="150px">
                            <asp:ListItem>-Select-</asp:ListItem>
                            <asp:ListItem Value="True">Approved</asp:ListItem>
                            <asp:ListItem Value="False">Not Approved</asp:ListItem>
                        </asp:DropDownList>
                    </label>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddl_doc_not_appvd_by_sup"
                ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 28%">
                </td>
                <td colspan="2" align="left">
                    <asp:Button ID="imgbtngo" runat="server" Text="Go" ValidationGroup="1" OnClick="imgbtngo_Click"
                        Width="40px" CssClass="btnSubmit" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>
            <asp:Label ID="Label10" runat="server" Text="List of Document Approval Status by Office Clerk/Supervisor"
                Font-Bold="True"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td align="right" colspan="4">
                    <asp:Button ID="btnprintableversion" runat="server" Text="Printable Version" OnClick="btnprintableversion_Click"
                        CssClass="btnSubmit" />
                    <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" visible="False" class="btnSubmit" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Italic="true" ForeColor="Black"></asp:Label>
                                                </td>    
                                             </tr>
                                             <tr>   
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">   
                                                    <asp:Label ID="lblBddf" runat="server" Font-Italic="true" Text="Business : "></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Italic="true" ForeColor="Black"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;"> 
                                                    <asp:Label ID="Label11" runat="server" Text="List of Document Approval Status by Office Clerk/Supervisor"
                                                                Font-Italic="True"></asp:Label>
                                                </td>  
                                            </tr>
                                            <tr>
                                            <td></td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="text-align: left; font-size: 13px; font-weight: bold">
                                                    <asp:Label ID="Label1" runat="server" Text="Office Cleark :" ForeColor="Black" Font-Italic="True"></asp:Label>
                                                    <asp:Label ID="lblDEO" runat="server" Font-Italic="True"></asp:Label>,
                                                
                                                    <asp:Label ID="Label2" runat="server" Text="Supervisor :" ForeColor="Black" Font-Italic="True"></asp:Label>
                                                    <asp:Label ID="lblSuper" runat="server" Font-Italic="True"></asp:Label>,
                                                
                                                    <asp:Label ID="Label3" runat="server" Text="Office Cleark Status :" ForeColor="Black" Font-Italic="True"></asp:Label>
                                                    <asp:Label ID="lblOS" runat="server" Font-Italic="True"></asp:Label>,
                                                
                                                    <asp:Label ID="Label5" runat="server" Text="Supervisor Status :" ForeColor="Black" Font-Italic="True"></asp:Label>
                                                    <asp:Label ID="lblSS" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:GridView ID="grid_doc_available" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" DataKeyNames="DocumentProcessingId"
                                        EmptyDataText="No Record Found." OnPageIndexChanging="grid_doc_available_PageIndexChanging"
                                        AllowSorting="True" OnSorting="grid_doc_available_Sorting" Width="100%">
                                        <Columns>
                                            <%--<asp:CommandField ButtonType="Image" UpdateImageUrl ="~/Account/images/UpdateGrid.jpg" CancelImageUrl="~/Account/images/CancelGrid.jpg" EditImageUrl="~/Account/images/edit.gif" ShowEditButton="True" ShowHeader="True"
                                            HeaderText="Edit" ValidationGroup="qq"  EditText="UPDATE"
                                            CancelText="CANCEL" UpdateText="UPDATE">
                                            
                                                <ItemStyle CssClass="theHeader" />
                                               <HeaderStyle CssClass="theHeader" />
                                        </asp:CommandField>--%>
                                            <%--<asp:BoundField DataField="DocumentProcessingId" HeaderText="Doc.Proc.Id" />--%>
                                            <%--<asp:TemplateField HeaderText="Doc.Proc.Id" SortExpression="DocumentProcessingId">
                                        <ItemTemplate>
                                        <A onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" href="javascript:void(0)">
                                          <%#DataBinder.Eval(Container.DataItem, "DocumentProcessingId")%>
                                          </A>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                            <%--<asp:BoundField HeaderText="Doc. Id" DataField="DocumentId" />--%>
                                            <asp:TemplateField HeaderText="Doc Id" SortExpression="DocumentId" ItemStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                        href="javascript:void(0)" style="color: Black">
                                                        <%#DataBinder.Eval(Container.DataItem, "DocumentId")%>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField HeaderText="Title" DataField="DocumentTitle" />--%>
                                            <asp:TemplateField HeaderText="Title" SortExpression="DocumentTitle" ItemStyle-Width="16%"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                        href="javascript:void(0)" style="color: Black">
                                                        <%#DataBinder.Eval(Container.DataItem, "DocumentTitle")%>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField HeaderText="Date" DataField="Date" />--%>
                                            <asp:TemplateField HeaderText=" Upload Date" SortExpression="Date" ItemStyle-Width="12%"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                        href="javascript:void(0)" style="color: Black">
                                                        <%#DataBinder.Eval(Container.DataItem, "Date")%>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approve Date" SortExpression="ApproveDate" ItemStyle-Width="12%"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Labedate" runat="server" Text='<%# Bind("ApproveDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:BoundField HeaderText="Allocated Date" DataField="AllocatedDate,{0:dd/MM/yyy}" SortExpression="AllocatedDate"  DataFormatString="{0:MM/dd/yyyy-HH:mm}"/>--%>
                                            <asp:BoundField HeaderText="Party" DataField="PartyName" SortExpression="PartyName"
                                                ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField HeaderText="Operator Name" DataField="OperatorName" SortExpression="OperatorName"
                                                ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <%--<asp:TemplateField HeaderText="View" ShowHeader="False">
                                        <ItemTemplate>
                                        <A onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" href="javascript:void(0)">View</A>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                    <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                    <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
