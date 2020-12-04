<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentAvailableByMangr.aspx.cs" Inherits="DocumentAvailableByMangr"
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
        #Table1
        {
            margin-right: 3px;
        }
        #Table4
        {
            width: 99%;
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

    <table id="innertbl1" cellpadding="0" cellspacing="3" width="100%">
        <tr>
            <td colspan="4" align="center">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 35%" align="right">
                <label>
                    <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                </label>
            </td>
            <td colspan="3" align="left">
                <label>
                    <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                        Width="250px">
                    </asp:DropDownList>
                </label>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_doc_appvd_by_deo"
                
                ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <%--  <tr>
    <td colspan="4">
    <asp:Panel ID="pnlemp" runat="server" Visible="false">
    <table >
     <tr>
        <td style="width: 230px">
            Document Approved by Me:</td>
        <td colspan="2">
            <asp:DropDownList ID="ddlemp" runat="server" >
            </asp:DropDownList>
            </td>
        <td colspan="1">
           
            </td>
    </tr>
    </table>
    </asp:Panel>

    </td>
    </tr>--%>
        <tr>
            <td style="width: 35%" align="right">
                <label>
                    <asp:Label ID="Label2" runat="server" Text="Doc Approved by Office Clerk"></asp:Label>
                </label>
            </td>
            <td colspan="3">
                <label>
                    <asp:DropDownList ID="ddl_doc_appvd_by_deo" runat="server" DataTextField="EmployeeName"
                        DataValueField="EmployeeID" ValidationGroup="1" OnSelectedIndexChanged="ddl_doc_appvd_by_deo_SelectedIndexChanged"
                        Width="250px">
                    </asp:DropDownList>
                </label>
            </td>
        </tr>
        <tr>
            <td style="width: 35%" align="right">
                <label>
                    <asp:Label ID="Label3" runat="server" Text="Document Approved by Supervisor"></asp:Label>
                </label>
            </td>
            <td colspan="3">
                <label>
                    <asp:DropDownList ID="ddl_doc_appvd_by_sup" runat="server" DataTextField="EmployeeName"
                        DataValueField="EmployeeID" ValidationGroup="1" OnSelectedIndexChanged="ddl_doc_appvd_by_sup_SelectedIndexChanged"
                        Width="250px">
                    </asp:DropDownList>
                </label>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="imgbtngo" runat="server" Text=" Go " ValidationGroup="2" OnClick="imgbtngo_Click"
                    CausesValidation="False" CssClass="btnSubmit" />
            </td>
        </tr>
    </table>
    <fieldset>
        <legend>
            <asp:Label ID="Label4" runat="server" Text="Document Approve for Filing Desk Manager"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:Button ID="Button1" runat="server" Text="Printable Version" OnClick="Button1_Click"
                        CssClass="btnSubmit" />
                    <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                        style="width: 51px;" type="button" value="Print" visible="false" class="btnSubmit" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table id="GridTbl" width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblcompany" runat="server" Font-Italic="true" Font-Size="20px" ForeColor="Black"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="Label20" runat="server" Font-Italic="true" Text="Business : "></asp:Label>
                                                    <asp:Label ID="lblcomname" runat="server" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="lblhead" runat="server" Font-Italic="true" Text="Document Approve for Filing Desk Manager">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:GridView ID="grid_doc_available" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" DataKeyNames="DocumentProcessingId"
                                            EmptyDataText="No Record Found." AllowPaging="True" OnPageIndexChanging="grid_doc_available_PageIndexChanging"
                                            AllowSorting="True" OnSorting="grid_doc_available_Sorting" Width="100%" PageSize="20">
                                            <Columns>
                                                <%-- <asp:CommandField ButtonType="Image" UpdateImageUrl ="~/Account/images/UpdateGrid.jpg" CancelImageUrl="~/Account/images/CancelGrid.jpg" EditImageUrl="~/Account/images/edit.gif" ShowEditButton="True" ShowHeader="True"
                                                    HeaderText="Edit" ValidationGroup="qq"  EditText="UPDATE"
                                                    CancelText="CANCEL" UpdateText="UPDATE">
                                            
                                                <ItemStyle CssClass="theHeader" />
                                               <HeaderStyle CssClass="theHeader" />
                                                </asp:CommandField>--%>
                                                <%--<asp:BoundField DataField="DocumentProcessingId" HeaderText="Doc.Proc.Id" />--%>
                                                <%-- <asp:TemplateField HeaderText="Doc.Proc.Id" SortExpression="DocumentProcessingId">
                                                    <ItemTemplate>
                                                    <A onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" href="javascript:void(0)">
                                                      <%#DataBinder.Eval(Container.DataItem, "DocumentProcessingId")%>
                                                      </A>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <%--<asp:BoundField HeaderText="Doc. Id" DataField="DocumentId" />--%>
                                                <asp:TemplateField HeaderText="Id" SortExpression="DocumentId" ItemStyle-Width="5%"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <a onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                            style="color: Black" href="javascript:void(0)">
                                                            <%#DataBinder.Eval(Container.DataItem, "DocumentId")%>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Title" DataField="DocumentTitle" SortExpression="DocumentTitle"
                                                    ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                                                <%--<asp:TemplateField HeaderText="Title" SortExpression="DocumentTitle">
                                                                    <ItemTemplate>
                                                                    <A onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" href="javascript:void(0)">
                                                                      <%#DataBinder.Eval(Container.DataItem, "DocumentTitle")%>
                                                                      </A>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                <asp:BoundField HeaderText="Date" DataField="Date" SortExpression="Date" ItemStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Left" />
                                                <%--<asp:TemplateField HeaderText="Date" SortExpression="Date">
                                                                    <ItemTemplate>
                                                                    <A onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" href="javascript:void(0)">
                                                                      <%#DataBinder.Eval(Container.DataItem, "Date")%>
                                                                      </A>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                <asp:BoundField HeaderText="Allocated Date" DataField="AllocatedDate" DataFormatString="{0:dd/MM/yyyy-HH:mm}"
                                                    ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                                <%--<asp:TemplateField HeaderText="Allocated Date" SortExpression="AllocatedDate">
                                                                    <ItemTemplate>
                                                                    <A onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" href="javascript:void(0)">
                                                                      <%#DataBinder.Eval(Container.DataItem, "AllocatedDate")%>
                                                                      </A>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                <asp:BoundField HeaderText="Party" DataField="PartyName" SortExpression="PartyName"
                                                    ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:TemplateField HeaderText="Appove" SortExpression="Approve" ItemStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblapprove" runat="server" Text='<%# Eval("Approve") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Accept/Reject" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%--<asp:RadioButtonList ID="rbtnAcceptReject" runat="server" RepeatDirection="Horizontal"
                                                    Width="75px">
                                                    <asp:ListItem Selected="True" Value="None">None</asp:ListItem>
                                                    <asp:ListItem Value="True">Accept</asp:ListItem>
                                                    <asp:ListItem Value="False">Reject</asp:ListItem>
                                                </asp:RadioButtonList>
                                              --%>
                                                        <asp:DropDownList ID="rbtnAcceptReject" runat="server" Width="80px">
                                                            <asp:ListItem Selected="True" Value="None">None</asp:ListItem>
                                                            <asp:ListItem Value="True">Accept</asp:ListItem>
                                                            <asp:ListItem Value="False">Reject</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Note" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine" Height="48px"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                                            SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtNote"
                                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="View" ShowHeader="False">
                                                <ItemTemplate>
                                                <A onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" href="javascript:void(0)">View</A>                                                  
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                        <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                        <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="imgbtnsubmit" runat="server" CssClass="btnSubmit" Text="Submit" ValidationGroup="1"
                                        OnClick="imgbtnsubmit_Click" CausesValidation="False" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
