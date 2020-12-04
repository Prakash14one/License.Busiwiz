<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="WizardAutoAllocation.aspx.cs" Inherits="WizardAutoAllocation"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ioffice/Account/UserControl/UControlWizardpanel.ascx" TagName="pnl" TagPrefix="pnl" %>
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

    <script language="javascript">

 
 
 function RealNumWithDecimal(myfield, e, dec)
{

//myfield=document.getElementById(FindName(myfield)).value
//alert(myfield);
var key;
var keychar;
if (window.event)
key = window.event.keyCode;
else if (e)
key = e.which;
else
return true;
keychar = String.fromCharCode(key);
   if(key==13)
{
return false;
}
if ((key==null) || (key==0) || (key==8) || (key==9) || (key==27) )
{
return true;
}
else if ((("0123456789.").indexOf(keychar) > -1))
{
return true;
}
// decimal point jump
else if (dec && (keychar == "."))
{

 myfield.form.elements[dec].focus();
  myfield.value="";
 
return false;
}
else
{
  myfield.value="";
  return false;
}
}
    </script>

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }  
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <div style="margin-left: 1%">
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add Filing Desk Employee for Auto Allocation of Documents"
                            CssClass="btnSubmit" OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <table cellpadding="0" cellspacing="3" width="100%">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 15px">
                                    <label>
                                        <asp:Label ID="Label1" Visible="false" runat="server" Text="Do you wish to Auto Allocate documents to different employees for processing ?"></asp:Label>
                                    </label>
                                    <asp:RadioButtonList ID="RadioButtonList1" Visible="false" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                        AutoPostBack="True" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="True" Selected="True">Yes</asp:ListItem>
                                        <asp:ListItem Value="False">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            
                             <tr>
                             
                                <td>
                                <label>1</label>
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Do you wish to set "></asp:Label>
                                    </label>
                                   <asp:RadioButtonList ID="rdsetrul"  runat="server" 
                                        RepeatDirection="Horizontal" AutoPostBack="True" 
                                        onselectedindexchanged="rdsetrul_SelectedIndexChanged">
                                        <asp:ListItem Value="True" Selected="True" Text="common rule for all businesses "></asp:ListItem>
                                        <asp:ListItem Value="False" Text="Separate Rule for each business ?"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                             <tr>
                             
                                <td>
                                <label>2</label>
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Select the employee of the business for document processing"></asp:Label>
                                    </label>
                                
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <label></label>
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="pnlIP" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="height: 18px">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="pnldesignation" runat="server" Visible="false">
                                                                    <asp:GridView ID="grdDesignation" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                        DataKeyNames="DesignationId" EmptyDataText="No Designation available in Document Department"
                                                                        OnRowDataBound="grdDesignation_RowDataBound" Width="100%" OnSorting="grdDesignation_Sorting"
                                                                        PageSize="20">
                                                                        <Columns>
                                                                            <asp:TemplateField ShowHeader="False" ItemStyle-Width="7%">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="HeaderChkboxDes" runat="server" AutoPostBack="True" OnCheckedChanged="HeaderChkboxDes_CheckedChanged" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkDesignation" runat="server" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="10px" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="DesignationName" HeaderText="Designation" SortExpression="DesignationName"
                                                                                HeaderStyle-HorizontalAlign="Left" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:Button ID="imgbShowEmp" runat="server" CssClass="btnSubmit" Visible="false"
                                                                        Text="Add Employees" OnClick="imgbShowEmp_Click" />
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                                                <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 18px">
                                                 <label></label>
                                                    <label>
                                                        <asp:Label ID="Label4" runat="server" Text="Select Filing Desk Employees for Auto Allocation"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 18px">
                                               
                                                    <asp:GridView ID="grdEmployeeList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        DataKeyNames="EmployeeId" EmptyDataText="Select designation for auto allocation"
                                                        OnPageIndexChanging="grdEmployeeList_PageIndexChanging" OnRowDataBound="grdEmployeeList_RowDataBound"
                                                        Width="100%" PageSize="20" OnSelectedIndexChanged="grdEmployeeList_SelectedIndexChanged"
                                                        OnSorting="grdEmployeeList_Sorting">
                                                        <Columns>
                                                            <asp:TemplateField ShowHeader="False" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="HeaderChkboxEmp" runat="server" AutoPostBack="True" OnCheckedChanged="HeaderChkboxEmp_CheckedChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkEmployee" runat="server" />
                                                                </ItemTemplate>
                                                               
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="DesignationName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                HeaderText="Designation" SortExpression="DesignationName" ItemStyle-Width="25%">
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="EmployeeName" HeaderText="Business:Employee Name" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="68%" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="imgbtnsubmit" runat="server" Text="Save" ValidationGroup="1" OnClick="imgbtnsubmit_Click"
                                        CssClass="btnSubmit" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label5" runat="server" Text="List of Employees of Filing Desk Department for Auto Allocation of Documents"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="Button1" runat="server" Text="Printable Version" OnClick="Button1_Click"
                                    CssClass="btnSubmit" />
                                <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    style="width: 51px;" type="button" value="Print" visible="false" class="btnSubmit" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="Filter by Business Name"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlbusfilter" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlbusfilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                            <table id="GridTbl" width="100%">
                                                <tr align="center">
                                                    <td>
                                                        <div id="mydiv" class="closed">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                        <asp:Label ID="lblcompany" runat="server" Font-Italic="true" Font-Size="20px" ForeColor="Black"
                                                                            Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                        <asp:Label ID="dfdfdf" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                                        <asp:Label ID="lblbusiness" runat="server" Font-Italic="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                        <asp:Label ID="lblhead" runat="server" Font-Italic="true" Text="List of Employees of Filing Desk Department for Auto Allocation of Documents"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel2" runat="server" Width="100%">
                                                            <cc11:PagingGridView AutoGenerateColumns="False" ID="gridIPAddress" runat="server"
                                                                Width="100%" DataKeyNames="EmployeeMasterId" EmptyDataText="No Record Found."
                                                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                OnPageIndexChanging="gridIPAddress_PageIndexChanging" OnRowCommand="gridIPAddress_RowCommand"
                                                                OnRowDeleting="gridIPAddress_RowDeleting" PageSize="20" 
                                                                AllowSorting="True" OnSorting="gridIPAddress_Sorting">
                                                                <PagerSettings Mode="NumericFirstLast" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Business Name" SortExpression="Wname" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-Width="30%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblwname" runat="server" Text='<%# Eval("Wname")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="30%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Designation Name" SortExpression="DesignationName"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDesName" runat="server" Text='<%# Eval("DesignationName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="30%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bisiness:Employee Name" SortExpression="Ename" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-Width="35%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDesemp" runat="server" Text='<%# Eval("Ename")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="35%" />
                                                                    </asp:TemplateField>
                                                                    <%--<asp:ButtonField CommandName="Delete1" ButtonType="Image" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                                        ImageUrl="~/Account/images/delete.gif" HeaderText="Delete" Text="Delete" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-Width="3%">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="3%" />
                                                                    </asp:ButtonField>--%>
                                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Remove Employee From Auto Allocation"
                                                                                CommandName="Delete1" CommandArgument='<%# Eval("EmployeeMasterId")%>' ImageUrl="~/Account/images/delete.gif"
                                                                                OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                                <AlternatingRowStyle CssClass="alt" />
                                                            </cc11:PagingGridView>
                                                            <input id="hdncnfm" type="hidden" name="hdncnfm" runat="Server" />
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="imgconfirmok" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlconfirmmsg" runat="server" BorderStyle="Outset" CssClass="GridPnl"
                                    Height="100px" Width="300px" BackColor="#CCCCCC" BorderColor="#666666">
                                    <table id="innertbl1">
                                        <tr>
                                            <td class="secondtblfc2">
                                                <asp:UpdatePanel ID="UpdatePanelMainTypeAdd" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table id="subinnertbl1" cellspacing="0" cellpadding="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="secondtblfc1">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="pnlconfirmmsgub" runat="server" Width="100%" Height="75px">
                                                                            <table id="subinnertbl1" cellspacing="3" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td colspan="2" style="font-weight: bold; padding-left: 10px; font-size: 12px; font-family: Arial;
                                                                                            text-align: left; vertical-align: top;">
                                                                                            <br />
                                                                                            Are you sure, You want to Delete a Record?
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <br />
                                                                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:Button ID="imgconfirmok" runat="server" Text="Ok" CausesValidation="False" OnClick="imgconfirmok_Click" />
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                        <td>
                                                                                            <br />
                                                                                            <asp:Button ID="imgconfirmcalcel" runat="server" Text="Close" CausesValidation="False" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <cc1:ModalPopupExtender ID="mdlpopupconfirm" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="pnlconfirmmsg" TargetControlID="hdnconfirm" CancelControlID="imgconfirmcalcel"
                                    X="250" Y="-200" Drag="true">
                                </cc1:ModalPopupExtender>
                                <input id="hdnconfirm" runat="Server" name="hdnconfirm" type="hidden" style="width: 4px" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <asp:Literal ID="CheckBoxIDsArray" runat="server"></asp:Literal><asp:Literal ID="CheckBoxIDsArrayDes"
                    runat="server"></asp:Literal><asp:Literal ID="CheckBoxIDsArrayEmp" runat="server"></asp:Literal>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
