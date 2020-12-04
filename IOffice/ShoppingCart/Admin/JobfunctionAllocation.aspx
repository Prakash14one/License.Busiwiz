<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="JobfunctionAllocation.aspx.cs" Inherits="ShoppingCart_Admin_JobfunctionAllocation"
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
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }  
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <fieldset>
                <legend>
                    <asp:Label ID="Label1" runat="server" Text="Job Function Responsibility Allocation"></asp:Label>
                </legend>
                <div style="clear: both;">
                </div>
                <table width="100%">
                    <tr>
                        <td style="width: 25%">
                            <label>
                                Business Name
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlwarehouse" runat="server" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged"
                                    Width="250px" AutoPostBack="True">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <label>
                                Department-Designation
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddldesi" runat="server" Width="250px" OnSelectedIndexChanged="ddldesi_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <label>
                                Job Function Category
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddljobcate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddljobcate_SelectedIndexChanged"
                                    Width="250px">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <label>
                                Job Function Sub Category
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddljobsubcat" runat="server" Width="250px" OnSelectedIndexChanged="ddljobsubcat_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>
                    <asp:Label ID="Label2" runat="server" Text="List of Job Function Allocation"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <input id="Button7" runat="server" visible="False" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" class="btnSubmit" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlgrid" runat="server" Width="100%" ScrollBars="None">
                                <table width="100%">
                                    <tr align="center">
                                        <td colspan="2">
                                            <div id="mydiv" class="closed">
                                                <table width="850Px">
                                                    <tr>
                                                        <td align="center" style="font-size: 22px; font-family: Calibri; font-weight: bold;
                                                            color: #000000">
                                                            <asp:Label ID="lblBusiness" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="text-align: center; font-family: Calibri; font-size: 18px;
                                                            font-weight: bold;">
                                                            <asp:Label ID="lblbatch" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="text-align: center; font-family: Calibri; font-size: 18px;
                                                            font-weight: bold;">
                                                            <asp:Label ID="lblemp" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="text-align: center; font-family: Calibri; font-size: 18px;
                                                            font-weight: bold;">
                                                            <asp:Label ID="lblheadtext" runat="server" Text="List of Attendence Approval"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                                EmptyDataText="No Record Founds." Width="100%" OnSorting="GridView1_Sorting"
                                                AllowSorting="True" Enabled="False" lternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                                CssClass="mGrid">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Category" HeaderStyle-Width="10%" SortExpression="CategoryName"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcate" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sub Category" HeaderStyle-Width="10%" SortExpression="SubCategoryName"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsubcate" runat="server" Text='<%# Eval("SubCategoryName")%>'> </asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Function Title" HeaderStyle-Width="10%" SortExpression="Jobfunctiontitle"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljobtitle" runat="server" Text='<%# Eval("Jobfunctiontitle")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="First Level Responsibility" HeaderStyle-Width="15%"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="pnlp1" runat="server" ScrollBars="Both" Height="100px">
                                                                <asp:GridView ID="grdf1" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"
                                                                    GridLines="Both" Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Employee Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblemp" Text='<%#DataBinder.Eval(Container.DataItem, "EmployeeName")%>'>
                                                                                </asp:Label>
                                                                                <asp:Label runat="server" ID="lnlno" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "No")%>'>
                                                                                </asp:Label>
                                                                                <asp:Label runat="server" ID="lblbid" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "BatchId")%>'>
                                                                                </asp:Label>
                                                                                <asp:Label runat="server" ID="lblempid" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "EmpId")%>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" ID="chkf" Checked='<%#DataBinder.Eval(Container.DataItem, "chk")%>'
                                                                                    OnCheckedChanged="chkSendMail_CheckedChanged" AutoPostBack="true" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="Goldenrod" Font-Bold="True" ForeColor="White" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Second Level Responsibility" HeaderStyle-Width="15%"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="pnlp2" runat="server" ScrollBars="Both" Height="100px">
                                                                <asp:GridView ID="grdf2" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"
                                                                    GridLines="Both" Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Employee Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblemp" Text='<%#DataBinder.Eval(Container.DataItem, "EmployeeName")%>'>
                                                                                </asp:Label>
                                                                                <asp:Label runat="server" ID="lnlno" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "No")%>'>
                                                                                </asp:Label>
                                                                                <asp:Label runat="server" ID="lblbid" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "BatchId")%>'>
                                                                                </asp:Label>
                                                                                <asp:Label runat="server" ID="lblempid" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "EmpId")%>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" ID="chkf" Checked='<%#DataBinder.Eval(Container.DataItem, "chk")%>'
                                                                                    OnCheckedChanged="chkSendMail3_CheckedChanged" AutoPostBack="true" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="Goldenrod" Font-Bold="True" ForeColor="White" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Third Level Responsibility" HeaderStyle-Width="15%"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="pnlp3" runat="server" ScrollBars="Both" Height="100px">
                                                                <asp:GridView ID="grdf3" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                                                    GridLines="Both" Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Employee Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblemp" Text='<%#DataBinder.Eval(Container.DataItem, "EmployeeName")%>'>
                                                                                </asp:Label>
                                                                                <asp:Label runat="server" ID="lnlno" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "No")%>'>
                                                                                </asp:Label>
                                                                                <asp:Label runat="server" ID="lblbid" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "BatchId")%>'>
                                                                                </asp:Label>
                                                                                <asp:Label runat="server" ID="lblempid" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "EmpId")%>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" ID="chkf" Checked='<%#DataBinder.Eval(Container.DataItem, "chk")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="Goldenrod" Font-Bold="True" ForeColor="White" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <label>
                                <asp:Label ID="lblg" runat="server" Text="Effective Active Start Date and Time : "
                                    Visible="False"></asp:Label>
                            </label>
                            <label>
                                <asp:TextBox ID="txtdatetime" runat="server" Visible="False" Enabled="false"></asp:TextBox>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" ValidationGroup="2" OnClick="btnsubmit_Click"
                                Visible="False" CssClass="btnSubmit" />
                            <asp:Button ID="btnedit" runat="server" OnClick="btnedit_Click" Text="Edit" Visible="False"
                                CssClass="btnSubmit" />
                            <asp:Button ID="btncancel" runat="server" OnClick="btncancel_Click" Text="Cancel"
                                CssClass="btnSubmit" Visible="False" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlwarehouse" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="Sorting" />
            <asp:AsyncPostBackTrigger ControlID="btnsubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
