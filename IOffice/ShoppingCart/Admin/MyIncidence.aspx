<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MyIncidence.aspx.cs" Inherits="ShoppingCart_Admin_MyIncidence" %>

<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <fieldset>
                <table id="temp1" cellpadding="0" cellspacing="3" style="width: 100%">
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label1" runat="server" Text="Penalty Points for Month to Date"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 20%">
                            <label>
                                <asp:Label ID="lblptm" runat="server"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 30%">
                        </td>
                        <td style="width: 20%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label2" runat="server" Text="Penalty Points for Year to Date"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 20%">
                            <label>
                                <asp:Label ID="lblpty" runat="server"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 30%">
                        </td>
                        <td style="width: 20%">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="Panel2" runat="server" Width="100%">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 10%">
                                            <label>
                                                <asp:Label ID="Label3" runat="server" Text="From"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfromdt"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rghjk" runat="server" ErrorMessage="*" ControlToValidate="txtfromdt"
                                                    ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$">
                                                </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td style="width: 19%">
                                            <label>
                                                <asp:TextBox ID="txtfromdt" runat="server" Width="90px" ValidationGroup="1" AutoPostBack="True"></asp:TextBox>
                                            </label>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromdt"
                                                PopupButtonID="ImageButton2">
                                            </cc1:CalendarExtender>
                                            <label>
                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                            </label>
                                        </td>
                                        <td align="right" style="width: 10%">
                                            <label>
                                                <asp:Label ID="Label4" runat="server" Text="To Date"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttodt"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                                                    ControlToValidate="txttodt" ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td align="right" style="width: 61%">
                                            <label>
                                                <asp:TextBox ID="txttodt" runat="server" Width="90px" ValidationGroup="1" AutoPostBack="True"
                                                    OnTextChanged="txttodt_TextChanged"></asp:TextBox>
                                            </label>
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txttodt"
                                                PopupButtonID="ImageButton3">
                                            </cc1:CalendarExtender>
                                            <label>
                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel9" runat="server" Width="100%" Visible="False">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label5" runat="server" Text="Incidence Note"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 70%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel runat="server" ID="Pnl1" Visible="false" Width="100%">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 30%" align="right">
                                                        </td>
                                                        <td style="width: 70%">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%" align="right">
                                        </td>
                                        <td style="width: 70%">
                                            <asp:Button ID="btnsubmit" runat="server" Text="Update" OnClick="btnsubmit_Click"
                                                ValidationGroup="1" CssClass="btnSubmit" />
                                            <asp:Button ID="btnreset" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnreset_Click"
                                                CssClass="btnSubmit" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>
                    <asp:Label ID="Label6" runat="server" Text="List of My Incident Report"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <asp:Button ID="btncancel0" runat="server" CssClass="btnSubmit" CausesValidation="false"
                                OnClick="btncancel0_Click" Text="Printable Version" />
                            <input id="Button7" runat="server" visible="false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                class="btnSubmit" style="width: 51px;" type="button" value="Print" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                <table width="100%">
                                    <tr align="center">
                                        <td>
                                            <div id="mydiv" class="closed">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="center" style="font-size: 20px; font-weight: bold;">
                                                            <asp:Label ID="lblcmpny" runat="server" Font-Italic="true" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                            <asp:Label ID="Label26" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                            <asp:Label ID="lblBusiness" runat="server" Font-Italic="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                            <asp:Label ID="Label11" runat="server" Text="List of My Incident Report" Font-Italic="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="text-align: left; font-size: 14px;">
                                                            <asp:Label ID="lblempname" runat="server" Font-Italic="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top; height: 80%">
                                        <td>
                                            <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                                PageSize="20" Width="100%" AllowSorting="True" CellPadding="4" CssClass="mGrid"
                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found."
                                                OnRowEditing="grid_RowEditing" OnRowCommand="grid_RowCommand" OnRowUpdating="grid_RowUpdating"
                                                OnRowCancelingEdit="grid_RowCancelingEdit">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" SortExpression="Date" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Date","{0:MM/dd/yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Time" SortExpression="Time" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltime" runat="server" Text='<%# Eval("Time")%>'></asp:Label>
                                                            <asp:Label ID="lbltimezone" runat="server" Text='<%# Eval("Timezone")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Penalty Points" SortExpression="Penaltypoint" ItemStyle-Width="8%"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpenaltypoint" runat="server" Text='<%# Eval("Penaltypoint")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Related Policy" SortExpression="PolicyTitle" ItemStyle-Width="9%"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpolicy" runat="server" Text='<%# Eval("PolicyTitle")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Related Procedure" SortExpression="ProcedureTitle"
                                                        ItemStyle-Width="13%" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblprocedure" runat="server" Text='<%# Eval("ProcedureTitle")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Related Rule" SortExpression="RuleTitle" ItemStyle-Width="13%"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrule" runat="server" Text='<%# Eval("RuleTitle")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Incident Note" SortExpression="IncidenceNote" ItemStyle-Width="20%"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinsidencenote" runat="server" Text='<%# Eval("IncidenceNote")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Answer" SortExpression="IncidenceEmpAnsNote"
                                                        ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempans" runat="server" Text='<%# Eval("IncidenceEmpAnsNote")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtempans" Height="40px" runat="server" Text='<%# Eval("IncidenceEmpAnsNote")%>'></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtempans"
                                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="rdroom" runat="server" ControlToValidate="txtempans"
                                                                ForeColor="red" Text="*"></asp:RequiredFieldValidator>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowEditButton="true" HeaderImageUrl="~/Account/images/edit.gif"
                                                        HeaderText="Edit" ButtonType="Image" EditImageUrl="~/Account/images/edit.gif"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%" UpdateImageUrl="~/Account/images/UpdateGrid.JPG"
                                                        CancelImageUrl="~/images/delete.gif" HeaderStyle-HorizontalAlign="Left"></asp:CommandField>
                                                    <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-Width="2%"
                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                                CommandName="view" ToolTip="View Profile" Height="20px" ImageUrl="~/Account/images/viewprofile.jpg"
                                                                Width="20px"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
