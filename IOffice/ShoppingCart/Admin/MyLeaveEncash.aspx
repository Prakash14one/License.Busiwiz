<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MyLeaveEncash.aspx.cs" Inherits="ShoppingCart_Admin_MyLeaveEncash"
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

    <asp:UpdatePanel ID="pnnn1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="statuslable" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td style="width: 25%">
                                <label>
                                    <asp:Label ID="dfgsd" runat="server" Text="Business Name"></asp:Label>
                                    <asp:DropDownList ID="ddlwarehouse" runat="server" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged"
                                        Width="250px" AutoPostBack="True">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td style="width: 25%">
                                <label>
                                    <asp:Label ID="sdd" runat="server" Text=" Employee Name"></asp:Label>
                                    <asp:DropDownList ID="ddlemp" runat="server" Width="250px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td valign="bottom">
                                <asp:Button ID="btngo" runat="server" OnClick="btngo_Click" CssClass="btnSubmit"
                                    Text=" Go " ValidationGroup="1" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label4" runat="server" Text="My Leave Availability"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btncancel0" runat="server" CausesValidation="false" OnClick="btncancel0_Click"
                                    Text="Printable Version" CssClass="btnSubmit" />
                                <input id="Button7" runat="server" visible="false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    style="width: 51px;" type="button" value="Print" class="btnSubmit" />
                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%" ScrollBars="None">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="850Px">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="Label7" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="lblheadtext" runat="server" Text="List of My Leave Availability" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="left">
                                                            <td align="left" style="text-align: left; font-size: 14px;">
                                                                <asp:Label ID="lblemp" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr align="left">
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="Id"
                                                    EmptyDataText="No Record Found." Width="100%" OnSorting="GridView1_Sorting" AllowSorting="True"
                                                    OnRowCommand="GridView1_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Leave Type" HeaderStyle-Width="35%" SortExpression="EmployeeLeaveTypeName"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblempleavetype" runat="server" Text='<%# Bind("EmployeeLeaveTypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Number of Leaves Available" HeaderStyle-Width="18%"
                                                            SortExpression="NumberofleaveEarned" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnoofleave" runat="server" Text='<%# Eval("NumberofleaveEarned")%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="18%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Encash Now" HeaderStyle-Width="10%" 
                                                            Visible="False">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="More info" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="view"
                                                                    Text="More info" ForeColor="Black"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%--<asp:ImageButton ID="ImageButton48" runat="server" CommandName="view1" CommandArgument='<%# Eval("Id") %>' />--%>
                                                                <asp:LinkButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="view1"
                                                                    Text="View" ForeColor="Black"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="10%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                     <%--   <tr>
                                            <td align="center">
                                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" ValidationGroup="2" OnClick="btnsubmit_Click"
                                                    Visible="False" CssClass="btnSubmit" />
                                            </td>
                                        </tr>--%>
                                        <tr style="vertical-align: top;">
                                            <td>
                                                <asp:Panel ID="pnlmoreinfo" runat="server" Visible="false">
                                                    <fieldset>
                                                        <legend>
                                                            <asp:Label ID="Label1" runat="server" Text="My Leave Account"></asp:Label>
                                                        </legend>
                                                        <table width="100%">
                                                            <%-- <tr>
                                                            <td style="font-weight: bolder; text-align: left; background-color: #CCCCCC">
                                                                
                                                            </td>
                                                        </tr>--%>
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label2" runat="server" Text="From Date"></asp:Label>
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="lblfdate" runat="server" Text=""></asp:Label>
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="Label3" runat="server" Text="To Date"></asp:Label>
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="lbltodate" runat="server" Text=""></asp:Label>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr style="vertical-align: top; height: 80%">
                                                                <td>
                                                                    <asp:GridView ID="grdleaveaccount" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                                                        EmptyDataText="No Record Found." Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                        AlternatingRowStyle-CssClass="alt">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-Width="20%"
                                                                                HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Leave Type" SortExpression="EmployeeLeaveTypeName"
                                                                                HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblempleave" Visible="true" runat="server" Text='<%# Bind("EmployeeLeaveTypeName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="No of leave Earned" SortExpression="Ernedleave" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblnoofleave" runat="server" Text='<%# Bind("Ernedleave") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="No of leaves Used" SortExpression="Usedeave" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblusedleave" runat="server" Text='<%# Bind("Usedeave") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblbleave" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset></asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <triggers>
      
          <asp:AsyncPostBackTrigger ControlID="ddlwarehouse" EventName="SelectedIndexChanged" />
         
               <asp:AsyncPostBackTrigger ControlID="btncancel0" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="Sorting" />
                <asp:AsyncPostBackTrigger ControlID="btngo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlwarehouse" EventName="SelectedIndexChanged" />
        </triggers>
</asp:Content>
