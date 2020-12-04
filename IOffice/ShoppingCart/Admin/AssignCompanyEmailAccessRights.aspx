<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="AssignCompanyEmailAccessRights.aspx.cs" Inherits="Account_AssignrightstoEmpforEmail"
    Title="Untitled Page" %>

<%--<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    &nbsp;<script language="javascript" type="text/javascript">
              function CallPrint(strid) {
                  var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
                  var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
                  WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
                  WinPrint.document.close();
                  WinPrint.focus();
                  WinPrint.print();
                  WinPrint.close();

              }
    

    </script><style type="text/css">
                 .open
                 {
                     display: block;
                 }
                 .closed
                 {
                     display: none;
                 }
             </style><%--   <table width="100%">
        <tr>
            <td>
                 <pnlhelp:pnlhelp ID="pnlHlp" runat="server" />
            </td>
        </tr>
    </table>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 1%">
                <asp:Panel ID="pnlmsg" runat="server" Visible="False" Width="100%">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div class="products_box">
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="title" Text="Set Email Access Rights -To Employee"></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            OnClick="Button1_Click1" />
                        <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td width="22%">
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Business Name"></asp:Label>
                                </label>
                            </td>
                            <td width="78%">
                                <label>
                                    <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstore_SelectedIndexChanged"
                                        Width="250px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>

                         <tr>
                            <td width="22%">
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Show All Business Email Accounts"></asp:Label>
                                </label>
                            </td>
                            <td width="78%">
                                <asp:CheckBox ID="checkbox12" runat="server" AutoPostBack="true" OnCheckedChanged="checkbox12_CheckedChanged" />
                            </td>
                        </tr>


                        <tr>
                            <td width="22%">
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Department - Designation"></asp:Label>
                                </label>
                            </td>
                            <td width="78%">
                                <label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="250px"
                                        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td width="22%">
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Select Employee For Allocate"></asp:Label>
                                    <%--     <asp:Label ID="sfdssdf" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                        ControlToValidate="ddlemployee" ValidationGroup="1" InitialValue="0">
                                    </asp:RequiredFieldValidator>--%>
                                </label>
                            </td>
                            <td width="78%">
                                <label>
                                    <%--<asp:TextBox ID="txtdept" runat="server" ValidationGroup="1" CssClass="txtbox"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdept"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                    <asp:DropDownList ID="ddlemployee" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged"
                                        Width="250px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                       
                    </table>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label67" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Allowed Access"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel2" runat="server" Width="100%">
                                                    <asp:GridView ID="gridocaccessright1" runat="server" AutoGenerateColumns="False"
                                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        DataKeyNames="CompanyEmailId" OnRowDataBound="gridocaccessright1_RowDataBound"
                                                        EmptyDataText="No Record Found." Width="100%" AllowSorting="True" OnSorting="gridocaccessright1_Sorting">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Email Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblempname" runat="server"></asp:Label>
                                                                    <asp:Label ID="Label2" runat="server" Visible="false" Text='<%# Bind("EmployeeId")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Labelasd2" runat="server" Text='<%# Bind("Wname")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Email Address" SortExpression="EmailId" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-HorizontalAlign="Left" DataField="EmailId" HeaderStyle-Width="30%">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Print" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkprint" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="HeaderChkboxPrint" runat="server" AutoPostBack="true" OnCheckedChanged="HeaderChkboxPrint_CheckedChanged" />
                                                                    <asp:Label ID="heat" runat="server" Text="View Rights"></asp:Label>
                                                                </HeaderTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Print" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkprint1" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="HeaderChkboxPrint1" runat="server" AutoPostBack="true" OnCheckedChanged="HeaderChkboxPrint1_CheckedChanged" />
                                                                    <%--OnCheckedChanged="HeaderChkboxPrint_CheckedChanged"--%>
                                                                    <asp:Label ID="heat1" runat="server" Text="Delete Rights"></asp:Label>
                                                                </HeaderTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Print" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkprint2" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="HeaderChkboxPrint2" runat="server" AutoPostBack="true" OnCheckedChanged="HeaderChkboxPrint2_CheckedChanged" />
                                                                    <%--OnCheckedChanged="HeaderChkboxPrint_CheckedChanged"--%>
                                                                    <asp:Label ID="heat2" runat="server" Text="Send Rights"></asp:Label>
                                                                </HeaderTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                                    <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                                    <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="CheckBoxIDsArrayPrint" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="imgbtnsubmit" Text="Submit" CssClass="btnSubmit" runat="server" OnClick="imgbtnsubmit_Click"
                                  Visible="false"  ValidationGroup="1" />
                            </td>
                        </tr>
                </fieldset>
            </div>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
