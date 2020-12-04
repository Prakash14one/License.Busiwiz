<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="EmpProjectStatusDetailsfinal.aspx.cs" Inherits="EmpProjectStatusDetailsfinal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="javascript" type="text/javascript">
    function CallPrint(strid) {
        var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
        var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
        WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="" media="screen" />');
        WinPrint.document.close();
        WinPrint.focus();
        WinPrint.print();
        WinPrint.close();

    }
    </script>
    <%--<div id="right_content">
        <div id="ctl00_ucTitle_PNLTITLE">
            <div class="divHeaderLeft">
                <div style="float: left; width: 50%;">
                    <h2>
                        <span id="ctl00_ucTitle_lbltitle">Project Status- Add,Manage</span>
                    </h2>
                </div>
                <div style="float: right; width: 50%">
                    <div id="ctl00_ucTitle_pnlshow">
                    </div>
                </div>
            </div>
        </div>
        <div style="clear: both;">
        </div>
        <div id="ctl00_ucTitle_pnlhelp" style="border: solid 1px black">
            <h3>
                <span id="ctl00_ucTitle_lblDetail" style="font-family: Tahoma; font-size: 7pt; font-weight: bold;">
                    Project Status Add,Manage</span>
            </h3>
        </div>
    </div>--%>
    <div style="margin-left: 1%">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div style="clear: both;">
    </div>
    <div class="products_box">
        <fieldset>
            <legend id="5">
                <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
            </legend>
            <div style="float: right;">
                <asp:Button ID="addnewpanel" runat="server" Text="Add Project Status" CssClass="btnSubmit"
                    OnClick="addnewpanel_Click" />
            </div>
            <div style="clear: both;">
            </div>
            <asp:Panel ID="pnlmonthgoal" runat="server" Width="100%" Visible="false">
                <table id="pagetbl" width="100%">
                    <tr>
                        <td>
                            <label>
                               Select Department
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlDeptName" runat="server" Width="369px" AutoPostBack="true"  >
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Employee Name
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlemployee" runat="server" Width="369px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Project Name
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlproname" runat="server" Width="369px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlproname_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Project Description
                            </label>
                            <asp:CheckBox ID="Chkprodesc" AutoPostBack="true" runat="server" OnCheckedChanged="Chkprodesc_CheckedChanged" />
                        </td>
                        <td colspan="5">
                            <label>
                                <asp:TextBox ID="txtprodescription" runat="server" Enabled="false" Height="75px"
                                    TextMode="MultiLine" Visible="false" Width="369px"></asp:TextBox>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Date
                            </label>
                        </td>
                        <td colspan="5">
                            <label>
                                <asp:TextBox ID="txtstartdate" runat="server" Width="100px" DataFormatString="{0:dd.MM.yyyy}">
                                </asp:TextBox>
                                <cc1:CalendarExtender ID="calstartdate" runat="server" TargetControlID="txtstartdate"
                                    PopupButtonID="ImageButton2">
                                </cc1:CalendarExtender>
                            </label>
                            <label>
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Status
                            </label>
                        </td>
                        <td colspan="5">
                            <label>
                                <asp:DropDownList ID="ddlselectstatus" runat="server" Width="369px">
                                    <asp:ListItem>---Select Status---</asp:ListItem>
                                    <asp:ListItem>Complete</asp:ListItem>
                                    <asp:ListItem>Pending</asp:ListItem>
                                    <asp:ListItem>Overdue</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtstatus" runat="server" Height="25px" Width="369px"></asp:TextBox>--%>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label5" runat="server" Text="Status Description">
                                </asp:Label>
                            </label>
                            <asp:CheckBox ID="ChkStatus" AutoPostBack="true" runat="server" OnCheckedChanged="ChkStatus_CheckedChanged" />
                        </td>
                        <td colspan="5">
                            <label>
                                <asp:TextBox ID="txtstatusDesc" runat="server" Height="75px" TextMode="MultiLine"
                                    Visible="false" Width="369"></asp:TextBox>
                            </label>
                        </td>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <label>
                                    <asp:CheckBox ID="ChkActive" runat="server" Text="Active" />
                                </label>
                            </td>
                        </tr>
                    </tr>
                    <%--  <tr>
                                <td>
                                    <label>
                                        Weekly Goal Description
                                    </label>
                                </td>
                                <td colspan="5">
                                    <label>
                                        <asp:TextBox ID="txtDescription" runat="server" Height="75px" TextMode="MultiLine"
                                            Width="369px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>--%>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="BtnSubmit_Click" />
                            <asp:Button ID="Button5" runat="server" CssClass="btnSubmit" Text="Update" Visible="false"
                                ValidationGroup="1" OnClick="Button5_Click" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="btncancel_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
        <fieldset>
            <legend>
                <asp:Label ID="Label30" runat="server" Text="List of ProjectStatus"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" 
                            Text="Printable Version" onclick="Button3_Click" />
                        <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className = 'open'; javascript: CallPrint('divPrint'); document.getElementById('mydiv').className = 'closed';"
                            style="width: 51px;" type="button" value="Print" visible="false"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                       
                        <label>
                            <asp:Label ID="Label1" runat="server" Text="Filter By Employee :"></asp:Label>
                        </label>
                        <label>
                            <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" Width="180px"
                                OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                         <label>
                            <asp:Label ID="Label6" runat="server" Text="Filter By Department :" ></asp:Label>
                        </label>
                        <label>
                            <asp:DropDownList ID="ddlsortdept" runat="server" AutoPostBack="True" Width="180px"
                                OnSelectedIndexChanged="ddlsortdept_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="lblDate" runat="server" Text="Filter By Date :"></asp:Label>
                        </label>
                         <label>
                             <asp:Table ID="Table1" runat="server">
                                 <asp:TableRow>
                                     <asp:TableCell>
                                         <asp:TextBox ID="txtFromDT" OnTextChanged="txtFromDT_TextChanged" AutoPostBack="true" runat="server" Width="75px"></asp:TextBox>
                                     </asp:TableCell>
                                     <asp:TableCell>
                                         <asp:ImageButton ID="imgbtnFromDT" runat="server" ImageAlign="AbsMiddle" AlternateText="FromDate" ImageUrl="~/images/calender.jpg" />
                                         <cc1:CalendarExtender ID="cal1" runat="server" TargetControlID="txtFromDT" PopupButtonID="imgbtnFromDT" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                     </asp:TableCell>
                                     <asp:TableCell>
                                         <asp:Label ID="lblTo" runat="server" Text="To"></asp:Label>
                                     </asp:TableCell>
                                     <asp:TableCell>
                                         <asp:TextBox ID="txtToDT" AutoPostBack="true" OnTextChanged="txtFromDT_TextChanged" runat="server" Width="75px"></asp:TextBox>
                                     </asp:TableCell>
                                     <asp:TableCell>
                                         <asp:ImageButton ID="imgbtnToDT" runat="server" ImageAlign="AbsMiddle" AlternateText="ToDate" ImageUrl="~/images/calender.jpg" />
                                         <cc1:CalendarExtender ID="cal2" runat="server" TargetControlID="txtToDT" PopupButtonID="imgbtnToDT" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                     </asp:TableCell>
                                 </asp:TableRow>
                             </asp:Table>      
                        </label>
                       
                    </td>
                </tr>
                <tr>
                    <td style="width: 610px">
                        <label>
                            <asp:Label ID="Label8" runat="server" Text="Filter By ProjectStatus:"></asp:Label>
                        </label>
                        <asp:CheckBox ID="Chksortstatus" Text="Show All ProjectStatus" runat="server" AutoPostBack="true"
                            OnCheckedChanged="Chksortstatus_CheckedChanged" />
                        <label>
                            <asp:DropDownList ID="ddlsortmonth" runat="server" Width="180px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlsortmonth_SelectedIndexChanged">
                                <asp:ListItem>---Select All---</asp:ListItem>
                                <asp:ListItem>Pending</asp:ListItem>
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="lblProjectName" runat="server" Text="Filter by Project Name"></asp:Label>
                        </label>
                        <label>
                            <asp:DropDownList ID="ddlProjectName" runat="server" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true" Width="200px"></asp:DropDownList>
                        </label>
                    </td>
                    
                    <%-- <label>
                                    <asp:Label ID="lblemp" runat="server" Text="Filter By Employee :"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlstatus1" runat="server" AutoPostBack="True" Width="180px">
                                  
                                    </asp:DropDownList>
                                </label>--%>
                    <%-- <asp:Button ID="btngo" runat="server" Text="Go" CausesValidation="false" /> --%>
                    <%-- onclick="btngo_Click" />--%>
                    <%--</label>--%>
                </tr>
                <tr>
                    <td style="width: 610px">
                         <label>
                            <asp:Label ID="Label2" runat="server" Visible="false" Text="ProjectTitle :"></asp:Label>
                        </label>
                          <label>
                         <asp:Label ID="lbltitle" runat="server"  Visible="false"></asp:Label>
                          </label></td>
                   
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="Gray" Width="600"
                            BorderStyle="Solid" BorderWidth="5px">
                            <table style="width: 100%">
                                <tr>
                                    <td style="height: 9px">
                                        &nbsp;
                                    </td>
                                    <td align="right" style="height: 9px">
                                        <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                            Width="16px" Height="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            You will need to add our Monthlygoal which allows download of Busicontroller for
                                            your Product. Busicontroller will allow to regulate your license terms with your
                                            customer.</label>
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td colspan="2">
                                        <asp:Button ID="Button1" runat="server" Text="OK" CssClass="btnSubmit" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="Button11" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel2" TargetControlID="Button11">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                            <table width="100%">
                                <tr>
                                    <td>
                                            <div id="mydiv" class="closed">
                                                    <table width="850Px">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                               
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <%--<label>
                                                                <asp:Label ID="Label31" runat="server" Text="Filter By Department :"></asp:Label>
                                                                </label>--%>
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Project Status" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdmonthlygoal" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True"
                                            OnRowCommand="grdmonthlygoal_RowCommand" OnRowDeleting="grdmonthlygoal_RowDeleting"
                                            OnRowEditing="grdmonthlygoal_RowEditing" OnRowUpdating="grdmonthlygoal_RowUpdating">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" HeaderText="DeptName"
                                                    ItemStyle-Width="8%" SortExpression="Name">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="1%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name1" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%"
                                                    HeaderText="Employee Name" SortExpression="Name1">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="1%" />
                                                </asp:BoundField>
                                             <%--   <asp:BoundField DataField="ProjectMaster_ProjectTitle" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Project Title" ItemStyle-Width="20%" SortExpression="MonthlyGoalMaster_MonthlyGoalTitle">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="20%" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="ProjectStatus_Date" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Date" ItemStyle-Width="15%" SortExpression="ProjectStatus_Date"
                                                    DataFormatString="{0:dd.MM.yyyy}">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="1%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectMaster_ProjectTitle" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="ProjectTitle" ItemStyle-Width="15%" SortExpression="ProjectMaster_ProjectTitle">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="15%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectStatus_Status_Description" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="15%" HeaderText="Status Description" SortExpression="ProjectStatus_Status_Description">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="30%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectStatus_Status" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="15%" HeaderText="Status" SortExpression="ProjectStatus_Status">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="2%" />
                                                </asp:BoundField>
                                                <%--<asp:BoundField DataField="ProjectMaster_ProjectStatus" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="10%" HeaderText="Project Status" SortExpression="ProjectMaster_ProjectStatus">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectMaster_ProjectStatus_Description" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Project Status Description" ItemStyle-Width="15%" SortExpression="ProjectMaster_ProjectStatus_Description">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="15%" />
                                                        </asp:BoundField>--%>
                                                <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Account/images/edit.gif"
                                                            CommandName="Edit" CommandArgument='<%# Eval("ProjectStatus_Id") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="3%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgdelete" ToolTip="Delete" runat="server" ImageUrl="~/Account/images/delete.gif"
                                                            CommandName="Delete" CommandArgument='<%# Eval("ProjectStatus_Id") %>'></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                        </asp:GridView>
                                    </td>
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>

