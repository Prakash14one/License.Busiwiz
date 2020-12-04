<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="frmMMasterReport.aspx.cs"
    Inherits="ShoppingCart_Admin_frmSTGMasterReport" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <fieldset>
                <legend></legend>
                <div style="float: right">
                    <label>
                        <asp:Button ID="Button1" runat="server" Text="Print and Export" OnClick="Button1_Click"
                            CssClass="btnSubmit" />
                    </label>
                    <label>
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="False" class="btnSubmit" />
                    </label>
                    <label>
                        <asp:DropDownList ID="ddlExport" runat="server" OnSelectedIndexChanged="ddlExport_SelectedIndexChanged"
                            AutoPostBack="True" Width="130px" Visible="False">
                        </asp:DropDownList>
                    </label>
                </div>
                <div style="clear: both;">
                </div>
                <label>
                    <asp:Label ID="Label2" runat="server" Text="Monthly Goal For"></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
                        <asp:ListItem Selected="True" Value="0">Business</asp:ListItem>
                        <asp:ListItem Value="1">Department</asp:ListItem>
                        <asp:ListItem Value="2">Division</asp:ListItem>
                        <asp:ListItem Value="3">Employee</asp:ListItem>
                    </asp:DropDownList>
                </label>
                <label>
                    <asp:Label ID="Label3" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlbusunesswithdept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusunesswithdept_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
                <label>
                    <asp:Label ID="Label31" runat="server" Text="Select Related Goal" Visible="false"></asp:Label>
                    <asp:DropDownList ID="ddldeptgoal" runat="server" AutoPostBack="true" Visible="false"
                        OnSelectedIndexChanged="ddldeptgoal_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="Yearly Goals of Department" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Monthly Goals of Business"></asp:ListItem>
                    </asp:DropDownList>
                </label>
                <label>
                    <asp:Label ID="Label9" runat="server" Text="Select Related Goal" Visible="false"></asp:Label>
                    <asp:DropDownList ID="ddldivigoal" runat="server" AutoPostBack="true" Visible="false"
                        OnSelectedIndexChanged="ddldivigoal_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="Yearly Goals of Division" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Monthly Goals of Business"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Monthly Goals of Department"></asp:ListItem>
                    </asp:DropDownList>
                </label>
                <label>
                    <asp:Label ID="Label10" runat="server" Text="Select Related Goal" Visible="false"></asp:Label>
                    <asp:DropDownList ID="ddlempgoal" runat="server" AutoPostBack="true" Visible="false"
                        OnSelectedIndexChanged="ddlempgoal_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="Yearly Goals of Employee" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Monthly Goals of Business"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Monthly Goals of Department"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Monthly Goals of Division"></asp:ListItem>
                    </asp:DropDownList>
                </label>
                <label>
                    <asp:Label ID="Label6" runat="server" Text="Yearly Goal Name"></asp:Label>
                    <asp:DropDownList ID="ddlfilterltg" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbymission_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
                <label>
                    <asp:Label ID="Label1" runat="server" Text="Monthly Goal Name"></asp:Label>
                    <asp:DropDownList ID="ddlstgfilter" runat="server" OnSelectedIndexChanged="ddlstgfilter_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </label>
                <table id="innertbl" cellpadding="0" cellspacing="6" style="width: 100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                <table style="width: 100%">
                                    <tr align="center">
                                        <td colspan="10">
                                            <div id="mydiv" class="closed">
                                                <table width="100%">
                                                    <tr align="center">
                                                        <td align="center" style="font-size: 18px; font-weight: bold; color: #000000" colspan="10">
                                                            <asp:Label ID="Label20" runat="server" Text="Business Name : "></asp:Label>
                                                            <asp:Label ID="lblBusiness" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td align="center" style="font-size: 18px; font-weight: bold; color: #000000" colspan="10">
                                                            <asp:Label ID="Label11" runat="server" Text="Monthly Master Report"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 16%; font-weight: bold;" align="right">
                                            <label>
                                                <asp:Label ID="Label4" runat="server" Text="Title"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 83%" colspan="9">
                                            <label>
                                                <asp:Label ID="lblltgtitile" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 17%; font-weight: bold;" align="right">
                                            <label>
                                                <asp:Label ID="Label5" runat="server" Text="Status"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="9" style="width: 83%">
                                            <label>
                                                <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" style="font-weight: bold">
                                            <label>
                                                <asp:Label ID="Label7" runat="server" Text="Description"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="9" style="width: 83%">
                                            <label>
                                                <asp:Label ID="lbldescription" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-weight: bold">
                                            <label>
                                                <asp:Label ID="Label12" runat="server" Text="Attachments"></asp:Label>
                                                <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" />
                                            </label>
                                        </td>
                                        <td colspan="9" style="width: 83%" align="left">
                                            <label>
                                                <asp:LinkButton ID="LinkButton2" runat="server" Text="" ForeColor="Black" OnClick="LinkButton2_Click"></asp:LinkButton>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-weight: bold">
                                            <label>
                                                <asp:Label ID="Label8" runat="server" Text="Budgeted Cost"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="9" style="width: 83%" align="left">
                                            <label>
                                                <asp:Label ID="lblbudgetedcost" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%; font-weight: bold;" align="right">
                                            <label>
                                                Actual Cost
                                            </label>
                                        </td>
                                        <td style="width: 83%" colspan="9" align="left">
                                            <label>
                                                <asp:Label ID="lblactualcost" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%; font-weight: bold;" align="right">
                                            <label>
                                                Shortage/Excess
                                            </label>
                                        </td>
                                        <td style="width: 83%" colspan="9" align="left">
                                            <label>
                                                <asp:Label ID="lblshortageexcess" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" style="width: 10%; font-weight: bold;">
                                            <label>
                                                <asp:Label ID="lblinstruction" runat="server" Text="Instructions">
                                                </asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="9">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 90%">
                                                        <asp:Panel ID="Panel3" runat="server">
                                                            <cc11:PagingGridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="DetailId"
                                                                Width="100%" EmptyDataText="No Record Found" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                AlternatingRowStyle-CssClass="alt" OnRowCommand="GridView2_RowCommand" OnPageIndexChanging="GridView2_PageIndexChanging"
                                                                PageSize="5">
                                                                <PagerSettings Mode="NumericFirstLast" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Date" SortExpression="Date" ItemStyle-Width="10%"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmasterdate" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Instruction Note" SortExpression="Detail" ItemStyle-Width="80%"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblinstruction123" runat="server" Text='<%# Eval("Detail")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Docs" SortExpression="DocumentId" ItemStyle-Width="10%"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" Text='<%#Eval("DocumentId") %>'
                                                                                CommandName="Send" CommandArgument='<%#Eval("DetailId") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                                <AlternatingRowStyle CssClass="alt" />
                                                            </cc11:PagingGridView>
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 10%" valign="top">
                                                        <label>
                                                            <asp:ImageButton ID="imgadddept" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                           Visible="false"     OnClick="LinkButton97666667_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                                            <asp:ImageButton ID="imgdeptrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                               Visible="false"   ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton13_Click"
                                                                ToolTip="Refresh" Width="20px" ImageAlign="Bottom" />
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="10">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" style="width: 10%; font-weight: bold;">
                                            <label>
                                                <asp:Label ID="lblevaluation" runat="server" Text="Status Notes" Visible="False"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="9">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 90%">
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <cc11:PagingGridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="EvaluationId"
                                                                Width="100%" EmptyDataText="No Record Found" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                AlternatingRowStyle-CssClass="alt" OnRowCommand="GridView1_RowCommand1" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                                PageSize="5">
                                                                <PagerSettings Mode="NumericFirstLast" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Date" SortExpression="Date" ItemStyle-Width="10%"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmasterdate1" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status Note" SortExpression="Detail" ItemStyle-Width="80%"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblevaluationnote123" runat="server" Text='<%# Eval("EvaluationNote")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Docs" SortExpression="DocumentId" ItemStyle-Width="10%"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButton1er" runat="server" ForeColor="Black" Text='<%#Eval("DocumentId") %>'
                                                                                CommandName="Send" CommandArgument='<%#Eval("EvaluationId") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                                <AlternatingRowStyle CssClass="alt" />
                                                            </cc11:PagingGridView>
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 10%" valign="top">
                                                        <label>
                                                            <asp:ImageButton ID="imgadddivision" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                             Visible="false"     OnClick="imgadddivision_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                                            <asp:ImageButton ID="imgdivisionrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                              Visible="false"    ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="imgdivisionrefresh_Click"
                                                                ToolTip="Refresh" Width="20px" ImageAlign="Bottom" />
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="10">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" style="width: 10%; font-weight: bold;">
                                            <label>
                                                <asp:Label ID="Label14" runat="server" Text="Task Done" Visible="False"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="9">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 90%">
                                                        <asp:Panel ID="Panel8" runat="server" Visible="false">
                                                            <cc11:PagingGridView ID="GridView6" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                EmptyDataText="No Record Found" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                                AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="GridView6_PageIndexChanging"
                                                                PageSize="5">
                                                                <PagerSettings Mode="NumericFirstLast" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmasterdate2" runat="server" Text='<%# Eval("TaskAllocationDate","{0:dd/MM/yyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-Width="13%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempname" runat="server" Text='<%# Eval("Employee")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Task" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTask" runat="server" Text='<%# Eval("TaskMasterName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Task Report" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTaskReport" runat="server" Text='<%# Eval("TaskReport")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Budgeted Minute" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblbudgetedminute123" runat="server" Text='<%# Eval("EUnitsAlloted")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Actual Minute" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblunitsused" runat="server" Text='<%# Eval("unitsused")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Actual Cost" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblactempcost" runat="server" Text='<%# Eval("ActualRate","{0:###,###.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Employee Avg Rate" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblavgrate" runat="server" Text='<%# Eval("EmpRate","{0:###,###.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                                <AlternatingRowStyle CssClass="alt" />
                                                            </cc11:PagingGridView>
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 10%" valign="top">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" style="width: 10%; font-weight: bold;">
                                            <label>
                                                <asp:Label ID="lblltgdocs" runat="server" Text="Yearly Documents" Visible="False"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="9" style="width: 90%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" style="width: 10%; font-weight: bold;">
                                            <label>
                                                <asp:Label ID="lblinstructiondocs" runat="server" Text="Instruction Documents" Visible="False"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="9" style="width: 90%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" style="width: 10%; font-weight: bold;">
                                            <label>
                                                <asp:Label ID="lblevaluationdocs" runat="server" Text="Status Note Documents" Visible="False"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="9" style="width: 90%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="9" style="width: 83%">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <asp:Panel ID="Panel6" runat="server" BackColor="#CCCCCC" BorderColor="Black" Width="500px"
                BorderStyle="Solid" Height="180px">
                <table bgcolor="#CCCCCC" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" bgcolor="#CCCCCC">
                            <table bgcolor="#CCCCCC" cellpadding="0" cellspacing="0" style="width: 500Px">
                                <tr>
                                    <td style="text-align: center; font-weight: bolder;">
                                        Status Note Documents
                                    </td>
                                    <td style="text-align: right">
                                        <asp:ImageButton ID="ImageButton5" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                            Width="15px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Panel ID="Panel5" runat="server" ScrollBars="Vertical" Height="100px">
                                <table cellpadding="0" cellspacing="0" width="480Px">
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" PageSize="10"
                                                Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                OnRowCommand="GridView5_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Doc Id" SortExpression="DocumentId" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1122" runat="server" Text='<%#Eval("DocumentId") %>'
                                                                CommandName="View" HeaderStyle-HorizontalAlign="Left" CommandArgument='<%#Eval("DocumentId") %>'
                                                                ForeColor="Black"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DocumentDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="15%"></asp:BoundField>
                                                    <asp:BoundField DataField="DocumentTitle" HeaderText="Title" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="70%"></asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                Enabled="True" PopupControlID="Panel6" TargetControlID="HiddenButton223" CancelControlID="ImageButton5">
            </cc1:ModalPopupExtender>
            <asp:Button ID="HiddenButton223" runat="server" Style="display: none" />
            <asp:Panel ID="Panel4" runat="server" BackColor="#CCCCCC" BorderColor="Black" Width="500px"
                BorderStyle="Solid" Height="180px">
                <table bgcolor="#CCCCCC" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" bgcolor="#CCCCCC">
                            <table bgcolor="#CCCCCC" cellpadding="0" cellspacing="0" style="width: 500Px">
                                <tr>
                                    <td style="text-align: center; font-weight: bolder;">
                                        Instruction Documents
                                    </td>
                                    <td style="text-align: right">
                                        <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                            Width="15px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Panel ID="pnlof" Height="150px" runat="server">
                                <table cellpadding="0" cellspacing="0" width="480Px">
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" PageSize="10"
                                                Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                OnRowCommand="GridView4_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Doc Id" SortExpression="DocumentId" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1121" runat="server" Text='<%#Eval("DocumentId") %>'
                                                                CommandName="View" CommandArgument='<%#Eval("DocumentId") %>' ForeColor="Black"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DocumentDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="15%" />
                                                    <asp:BoundField DataField="DocumentTitle" HeaderText="Title" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="70%" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupExtenderAddnew" runat="server" BackgroundCssClass="modalBackground"
                Enabled="True" PopupControlID="Panel4" TargetControlID="HiddenButton222" CancelControlID="ImageButton4">
            </cc1:ModalPopupExtender>
            <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
            <asp:Panel ID="Panel2" runat="server" BackColor="#CCCCCC" BorderColor="Black" Width="500px"
                BorderStyle="Solid" Height="180px">
                <table bgcolor="#CCCCCC" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" bgcolor="#CCCCCC">
                            <table bgcolor="#CCCCCC" cellpadding="0" cellspacing="0" style="width: 500Px">
                                <tr>
                                    <td style="text-align: center; font-weight: bolder;">
                                        Monthly Documents
                                    </td>
                                    <td style="text-align: right">
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                            Width="15px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Panel ID="Panel7" Height="150px" runat="server">
                                <table cellpadding="0" cellspacing="0" width="480Px">
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" PageSize="10"
                                                Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                OnRowCommand="GridView1_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Doc Id" SortExpression="DocumentId" ItemStyle-Width="15%"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("DocumentId") %>' CommandName="View"
                                                                HeaderStyle-HorizontalAlign="Left" CommandArgument='<%#Eval("DocumentId") %>'
                                                                ForeColor="Black"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DocumentDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="15%" />
                                                    <asp:BoundField DataField="DocumentTitle" HeaderText="Title" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="70%" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                Enabled="True" PopupControlID="Panel2" TargetControlID="Button2" CancelControlID="ImageButton1">
            </cc1:ModalPopupExtender>
            <asp:Button ID="Button2" runat="server" Style="display: none" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
