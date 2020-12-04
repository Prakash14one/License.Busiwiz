<%@ Page Language="C#" MasterPageFile="~/Party/Master/Party_Admin.master"
    AutoEventWireup="true" CodeFile="CustSCUpdatePage2.aspx.cs" Inherits="customer_CustSCUpdatePage2"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="customercss/forms.css" media="screen" />');
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <legend>Customer Complaint Status Review </legend>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click1" />
                            <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" visible="false" />
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <asp:Panel ID="Panel1" runat="server" Enabled="true">
                        </asp:Panel>
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblcustomername1" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <fieldset>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label1" runat="server" Text="Support Request ID "></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="lblRef" runat="server" ReadOnly="True" Text="lblRef"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label5" runat="server" Text="Request Date"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="lblEnttydate" runat="server" ReadOnly="True" Text="lblEnttydate"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label4" runat="server" Text="Status"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="lblCompStatus" runat="server"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label9" runat="server" Text="Customer ID"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="lblcustomerid" runat="server"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label7" runat="server" Text="Customer Name"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label8" runat="server" Text="Support Request Title"></asp:Label>
                                                    </label>
                                                </td>
                                                <td colspan="5">
                                                    <label>
                                                        <asp:Label ID="lblproblemtitle" runat="server"></asp:Label>
                                                    </label>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label2" runat="server" Text="Support Request Description"></asp:Label>
                                                    </label>
                                                </td>
                                                <td colspan="5">
                                                    <label>
                                                        <asp:Label ID="lblProblemdiscription" runat="server"></asp:Label>
                                                    </label>
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label6" runat="server" Text="Service Date" Visible="false"> </asp:Label>
                                                    </label>
                                                </td>
                                                <td colspan="5">
                                                    <label>
                                                        <asp:Label ID="lblServicedate" runat="server" ReadOnly="True" Text="lblServicedate"
                                                            Visible="false"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label3" runat="server" Text="List of service provided" Visible="false"></asp:Label>
                                                    </label>
                                                </td>
                                                <td colspan="5">
                                                    <label>
                                                        <asp:Label ID="lblAdminNote" runat="server" Visible="false"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="Label12" runat="server" Text="List of Support Provided"></asp:Label>
                                        </legend>
                                        <div style="clear: both;">
                                        </div>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel3" runat="server" Width="100%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <div id="Div1" class="closed">
                                                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                                                            <tr>
                                                                                <td align="center">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="GridView1" runat="server" Width="100%" PagerStyle-CssClass="pgr" AllowSorting="true"
                                                                        AlternatingRowStyle-CssClass="alt" CssClass="mGrid" DataKeyNames="Id" AutoGenerateColumns="False"
                                                                        EmptyDataText="No Record Found." onsorting="GridView1_Sorting">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Service ID" HeaderStyle-HorizontalAlign="Left" SortExpression="Id" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblservicecalllogid" runat="server" Text='<%#Bind("Id") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Service Date" HeaderStyle-HorizontalAlign="Left" SortExpression="ServiceProvidedDate" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblserviceprovideddate" runat="server" Text='<%#Bind("ServiceProvidedDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                             
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Service Provider ID" ItemStyle-Width="15%" SortExpression="EmployeeNo" HeaderStyle-HorizontalAlign="Left"
                                                                                ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblemployeename" runat="server" Text='<%#Bind("EmployeeNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                              
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Service Notes" ItemStyle-Width="60%" SortExpression="ServiceDoneNote" HeaderStyle-HorizontalAlign="Left"
                                                                                ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblservicedonenote" runat="server" Text='<%#Bind("ServiceDoneNote") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                               
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="pgr" />
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel2" runat="server" Width="100%">
                        
                            <label>
                                <asp:Button ID="Button2" runat="server" Visible="false" CssClass="btnSubmit" OnClick="Button2_Click"
                                    Text="Back" />
                                     <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            </label>
                        
                        </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
