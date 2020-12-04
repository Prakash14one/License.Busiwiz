<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="Incidence_Profile.aspx.cs" Inherits="ShoppingCart_Admin_Incidence_Profile" %>

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
            <fieldset>
                <table id="temp1" cellpadding="0" cellspacing="3" width="100%">
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel8" runat="server" Width="100%">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 25%" valign="top">
                                            <label>
                                                <asp:Label ID="lblwname" runat="server" Text="Business Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 25%" valign="top">
                                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                            <label>
                                                <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstore_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td style="width: 25%" align="right">
                                            <label>
                                                <asp:Label ID="Label3" runat="server" Text="Employee Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:DropDownList ID="ddlemployee" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%">
                                <tr valign="top">
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label4" runat="server" Text="From"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:TextBox ID="txtfromdt" runat="server" Width="70px" ValidationGroup="1" 
                                            AutoPostBack="True" ontextchanged="txtfromdt_TextChanged"></asp:TextBox>
                                        </label>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromdt"
                                            PopupButtonID="ImageButton2">
                                        </cc1:CalendarExtender>
                                        <label>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label5" runat="server" Text="To Date"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:TextBox ID="txttodt" runat="server" Width="70px" ValidationGroup="1" AutoPostBack="True"
                                                OnTextChanged="txttodt_TextChanged"></asp:TextBox>
                                        </label>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txttodt"
                                            PopupButtonID="ImageButton3">
                                        </cc1:CalendarExtender>
                                        <label>
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label6" runat="server" Text="Incident No."></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:DropDownList ID="ddlincidence" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlincidence_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                        <label>
                                            <asp:Button Text="Go" runat="server" CssClass="btnSubmit" Visible="false" ID="btngo"
                                                OnClick="btngo_Click" />
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <asp:Panel ID="panelhide" runat="server" Visible="false">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label14" runat="server" Text="List of Incident Report"></asp:Label>
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
                                <asp:Panel ID="Panel12" runat="server" Width="100%">
                                    <table width="100%">
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td colspan="2">
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
                                                                <asp:Label ID="Label15" runat="server" Text="List of Incident Report" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; font-weight: bold">
                                                <label>
                                                    <asp:Label ID="Label7" runat="server" Text="Incident No."></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lblincidenceno" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; font-weight: bold">
                                                <label>
                                                    <asp:Label ID="Label8" runat="server" Text="Employee Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lblemp" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; font-weight: bold">
                                                <label>
                                                    <asp:Label ID="Label9" runat="server" Text="Date"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lbldate" runat="server" Text=""></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lbltime" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; font-weight: bold">
                                                <label>
                                                    <asp:Label ID="Label11" runat="server" Text="Penalty Points for Incident"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lblpoint" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; font-weight: bold">
                                                <label>
                                                    <asp:Label ID="lblname" runat="server" Text="" Visible="false"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lbltitle" runat="server" Text="" Visible="false"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; font-weight: bold">
                                                <label>
                                                    <asp:Label ID="Label12" runat="server" Text="Incident Note"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lblnote" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; font-weight: bold">
                                                <label>
                                                    <asp:Label ID="Label13" runat="server" Text="Employee Answer"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lblempans" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; font-weight: bold">
                                                <label>
                                                    <asp:Label ID="Label1" runat="server" Text="Penalty points for month to date"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lblptm" runat="server"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; font-weight: bold">
                                                <label>
                                                    <asp:Label ID="Label2" runat="server" Text="Penalty points for Year to date"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lblpty" runat="server"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td colspan="2">
                                                <%--<asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                    
                                    PageSize="20" Width="100%" AllowSorting="True" 
                                     CellPadding="4" CssClass="GridBack"
                                    
                                    EmptyDataText="No Record Found.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Insidence No." SortExpression="Id" ItemStyle-Width="3%" Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinsidenceno" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="EmployeeName" ItemStyle-Width="12%" HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left"
                                                                     SortExpression="EmployeeName">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>
                                                 <asp:TemplateField HeaderText="Date" SortExpression="Date" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldate" runat="server"  Text='<%# Eval("Date","{0:MM/dd/yyyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Time" SortExpression="Time" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltime" runat="server"  Text='<%# Eval("Time","{0:HH:mm}")%>'></asp:Label><asp:Label ID="lbltimezone" runat="server"  Text='<%# Eval("Timezone")%>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Penalty Point" SortExpression="Penaltypoint" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpenaltypoint" runat="server" Text='<%# Eval("Penaltypoint")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                               
                                                <asp:TemplateField HeaderText="Related Policy" SortExpression="PolicyTitle" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpolicy" runat="server" Text='<%# Eval("PolicyTitle")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Related Procedure" SortExpression="ProcedureTitle" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblprocedure" runat="server" Text='<%# Eval("ProcedureTitle")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Related Rule" SortExpression="RuleTitle" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrule" runat="server" Text='<%# Eval("RuleTitle")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Insidence Note" SortExpression="IncidenceNote" ItemStyle-Width="17%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinsidencenote" runat="server" Text='<%# Eval("IncidenceNote")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Ans" SortExpression="IncidenceEmpAnsNote" ItemStyle-Width="17%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblempans" runat="server" Text='<%# Eval("IncidenceEmpAnsNote")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                                                                                                                                                             
                                               
                                               
                                               
                                            </Columns>
                                            <PagerStyle CssClass="GridPager" />
                                            <HeaderStyle CssClass="GridHeader" />
                                            <AlternatingRowStyle CssClass="GridAlternateRow" />
                                            <FooterStyle CssClass="GridFooter" />
                                            <RowStyle CssClass="GridRowStyle" />
                                        </asp:GridView>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset></asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
