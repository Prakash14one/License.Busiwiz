<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="GeneralLedgerSummaryReport.aspx.cs" Inherits="GeneralLedgerSummaryReport"
    Title="Untitled Page" %>

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
        .style2
        {
            width: 213px;
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

    <asp:UpdatePanel ID="updv" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <div style="padding-left: 1%">
                </div>
                <fieldset>
                    <legend></legend>
                    <table width="100%">
                        <tr>
                            <td colspan="4">
                                <table width="100%">
                                    <tr>
                                        <td colspan="4">
                                            <label>
                                                <asp:Label ID="Label7" runat="server" Text="Business Name"></asp:Label>
                                                <asp:DropDownList ID="ddlSearchByStore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchByStore_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label1" runat="server" Text="Class Type/Name"></asp:Label>
                                                <asp:RequiredFieldValidator ID="ReqiredFieldValidator1" runat="server" Display="Dynamic"
                                                    SetFocusOnError="true" ControlToValidate="ddlclass" InitialValue="0" ErrorMessage="*"
                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlclass" runat="server" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label2" runat="server" Text="Group Name"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieValidator1" runat="server" Display="Dynamic"
                                                    SetFocusOnError="true" ControlToValidate="ddlgroup" InitialValue="0" ErrorMessage="*"
                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlgroup" runat="server">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Panel ID="Panel1" runat="server" Visible="False">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="right" style="width: 90px;">
                                                            <label>
                                                                <asp:Label ID="lbldatefrom" runat="server" Text="From Date"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtdatefrom" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                                    SetFocusOnError="true" ControlToValidate="txtdatefrom" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgbtn1"
                                                                    TargetControlID="txtdatefrom">
                                                                </cc1:CalendarExtender>
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureName="en-AU"
                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
                                                            </label>
                                                            <asp:Panel ID="pnlds" runat="server" Visible="false">
                                                                <label>
                                                                    <asp:ImageButton ID="imgbtn1" runat="server" ImageUrl="~/images/cal_btn.jpg" />
                                                                </label>
                                                                <label>
                                                                    <asp:Label ID="lbldateto" runat="server" Text="To Date"></asp:Label>
                                                                </label>
                                                            </asp:Panel>
                                                            <label>
                                                                <asp:TextBox ID="txtdateto" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldVdator2" runat="server" Display="Dynamic"
                                                                    SetFocusOnError="true" ControlToValidate="txtdateto" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgbtn2"
                                                                    TargetControlID="txtdateto">
                                                                </cc1:CalendarExtender>
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdateto" />
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="imgbtn2" runat="server" ImageUrl="~/images/cal_btn.jpg" />
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="center">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="imgbtngo" runat="server" Text="  Go  " CssClass="btnSubmit" ValidationGroup="1"
                                    OnClick="imgbtngo_Click" />
                                <asp:Label ID="lbldate" Visible="false" runat="server"></asp:Label><asp:Label ID="lblcurrentdate"
                                    runat="server" Visible="false"></asp:Label><asp:Label ID="lbldatelast" runat="server"
                                        Visible="false"></asp:Label>
                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table width="100%">
                                    <tr>
                                        <td align="right" colspan="4">
                                            <asp:Button ID="Button2" runat="server" Text="Printable Version" CssClass="btnSubmit"
                                                OnClick="Button2_Click" />
                                            <input type="button" value="Print" id="Button3" runat="server" onclick="javascript:CallPrint('divPrint')"
                                                class="btnSubmit" visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <div id="mydiv" class="closed">
                                                                <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblcompname" runat="server" Font-Size="20px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="Label19" runat="server" Font-Size="20px" Text="Business : "></asp:Label>
                                                                            <asp:Label ID="lblstore" runat="server" Font-Size="20px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="lbltrbal" runat="server" Font-Size="18px" Text="List of Account Group Details">
                                                                            </asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound"
                                                                ShowFooter="True" EmptyDataText="No Record Found." Width="100%" CssClass="mGrid"
                                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowCreated="GridView1_RowCreated"
                                                                OnSorting="GridView1_Sorting" AllowSorting="True">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Store" SortExpression="Name" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblstore" runat="server" Text='<% #Bind("Name")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Class Name" SortExpression="displayname" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblclass" runat="server" Text='<% #Bind("[displayname]")%>'></asp:Label>
                                                                            <asp:Label ID="lblctid" runat="server" Text='<% #Bind("[ClassTypeId]")%>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Group Name" SortExpression="groupdisplayname" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="20%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgroup" runat="server" Text='<% #Bind("[groupdisplayname]")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="18%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ID" SortExpression="AccountId" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblaccid" runat="server" Text='<% #Bind("AccountId")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Account" SortExpression="AccountName" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="31%">
                                                                        <ItemTemplate>
                                                                            <a href='../Admin/GeneralLedger.aspx?Aid=<%# Eval("AccountId")%>&amp;Wid=<%#Eval("WareHouseId") %>'
                                                                                target="_blank">
                                                                                <asp:Label ID="lblaccname" runat="server" Text='<% #Bind("AccountName")%>' ForeColor="Black"></asp:Label>
                                                                            </a>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            Group Total :
                                                                        </FooterTemplate>
                                                                        <FooterStyle ForeColor="Black" HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Op.Balance" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="120px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbllstbaldr" runat="server" Text="0.00"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                        </FooterTemplate>
                                                                        <FooterStyle ForeColor="Black" HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Debit" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="9%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldr" runat="server" Text="0.00"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                        </FooterTemplate>
                                                                        <FooterStyle ForeColor="Black" HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Credit" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="9%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcr" runat="server" Text="0.00"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                        </FooterTemplate>
                                                                        <FooterStyle ForeColor="Black" HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Balance" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="120px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcurbaldr" runat="server" Text="0.00"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                        </FooterTemplate>
                                                                        <FooterStyle ForeColor="Black" HorizontalAlign="Right" Font-Bold="true" />
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
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="lblgptotal" runat="server" Visible="false" Text="0.00"></asp:Label>
                            </td>
                            <td style="width: 100px" class="col2">
                                <asp:Label ID="lblgplsttotal" runat="server" Visible="false" Text="0.00"></asp:Label>
                            </td>
                            <td class="style2">
                                <asp:Label ID="lbllstbalcr" runat="server" Visible="false" Text="0.00"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
