<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentViewApproveByEmpDetail.aspx.cs" Inherits="DocumentViewToUser"
    Title="Untitled Page" %>

<%@ Register Src="~/Account/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
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
        .style1
        {
            height: 28px;
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
        
          function mask(evt)
        { 
         
           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if(evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered invalid character");
                  return false;
             }     
            
        }   
         function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered invalid character");
            }   
        
            counter=document.getElementById(id);
            
            if(txt1.value.length <= max_len)
            {
                remaining_characters=max_len-txt1.value.length;
                counter.innerHTML=remaining_characters;
            }
        } 
    </script>

    <div style="float: left;">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div style="clear: both;">
    </div>
    <fieldset>
        <table id="innertbl1" cellpadding="0" cellspacing="3" width="100%">
            <tr>
                <td align="right">
                    <label>
                        <asp:Label ID="Label3" runat="server" Text="Business Name"></asp:Label>
                    </label>
                </td>
                <td>
                    <label>
                        <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                            Width="268px">
                        </asp:DropDownList>
                    </label>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <label>
                        <asp:Label ID="Label4" runat="server" Text="Employee"></asp:Label>
                    </label>
                </td>
                <td>
                    <label>
                        <asp:DropDownList ID="ddlemployee" runat="server" CausesValidation="false" DataTextField="EmployeeName"
                            DataValueField="EmployeeID" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged"
                            ValidationGroup="1" Width="268px">
                        </asp:DropDownList>
                    </label>
                </td>
                <td align="right">
                    <label>
                        <asp:Label ID="Label5" runat="server" Text="Status"></asp:Label>
                    </label>
                </td>
                <td>
                    <label>
                        <asp:DropDownList ID="ddlaprv" runat="server" Width="150px">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem Value="True">Approve</asp:ListItem>
                            <asp:ListItem Value="False">Reject</asp:ListItem>
                        </asp:DropDownList>
                    </label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <label>
                        <asp:Label ID="Label6" runat="server" Text="Search by Title"></asp:Label>
                        <%--<asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsearch"
                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9._\s]*)"
                            ControlToValidate="txtsearch" ValidationGroup="1"></asp:RegularExpressionValidator>
                    </label>
                </td>
                <td>
                    <label>
                        <asp:TextBox ID="txtsearch" runat="server" Width="262px" MaxLength="20" onKeydown="return mask(event)"
                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9._\s]+$/,'div2',20)"></asp:TextBox>
                        Characters Remaining <span id="div2">20</span>
                        <asp:Label ID="Label26" runat="server" Text="(A-Z,0-9,_,.)"></asp:Label>
                    </label>
                </td>
                <td align="right">
                    <label>
                        <asp:Label ID="Label7" runat="server" Text="Time Duration"></asp:Label>
                    </label>
                </td>
                <td>
                    <label>
                        <asp:DropDownList ID="ddlDuration" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDuration_SelectedIndexChanged"
                            Width="150px">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>Today</asp:ListItem>
                            <asp:ListItem>Yesterday</asp:ListItem>
                            <asp:ListItem>This Week</asp:ListItem>
                            <asp:ListItem>This Month</asp:ListItem>
                            <asp:ListItem>This Quarter</asp:ListItem>
                            <asp:ListItem>This Year</asp:ListItem>
                            <asp:ListItem>Last Week</asp:ListItem>
                            <asp:ListItem>Last Month</asp:ListItem>
                            <asp:ListItem>Last Quarter</asp:ListItem>
                            <asp:ListItem>Last Year</asp:ListItem>
                        </asp:DropDownList>
                    </label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <label>
                        <asp:Label ID="Label8" runat="server" Text="Document From"></asp:Label>
                        <asp:RegularExpressionValidator ID="rghjk" runat="server" ErrorMessage="*" ControlToValidate="txtfrom"
                            ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                    </label>
                </td>
                <td>
                    <label>
                        <asp:TextBox ID="txtfrom" runat="server" Width="144px"></asp:TextBox>
                    </label>
                    <label>
                        <asp:ImageButton ID="imgbtncalfrom" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg"
                            OnClick="imgbtncalfrom_Click" />
                    </label>
                </td>
                <td align="right">
                    <label>
                        <asp:Label ID="Label9" runat="server" Text="To"></asp:Label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                            ControlToValidate="txtto" ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                    </label>
                </td>
                <td>
                    <label>
                        <asp:TextBox ID="txtto" runat="server" Width="144px"></asp:TextBox>
                    </label>
                    <label>
                        <asp:ImageButton ID="imgbtnto" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="center" colspan="2">
                    <asp:Button ID="ImageButton1" runat="server" Text="Go" OnClick="ImageButton1_Click"
                        ValidationGroup="1" CssClass="btnSubmit" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <cc1:CalendarExtender ID="CalendarExtenderfrom" runat="server" PopupButtonID="imgbtncalfrom"
                        TargetControlID="txtfrom">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CalendarExtenderto" runat="server" PopupButtonID="imgbtnto"
                        TargetControlID="txtto">
                    </cc1:CalendarExtender>
                    <%--<asp:ScriptManager  id="sdf" runat="server">
                </asp:ScriptManager>--%>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>
            <asp:Label ID="Label10" runat="server" Text="Approved Documents by Employees (Without Document Approval Flow Rule)"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td colspan="4" align="right">
                    <asp:Button ID="Button1" runat="server" Text="Printable Version" OnClick="Button1_Click"
                        CssClass="btnSubmit" />
                    <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                        style="width: 51px;" type="button" value="Print" visible="false" class="btnSubmit" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
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
                                                    <asp:Label ID="labdddfdfdf" runat="server" Font-Italic="true" Text="Business : "></asp:Label>
                                                    <asp:Label ID="lblcomname" runat="server" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="lblhead" runat="server" Font-Italic="true" Text="Approved Documents by Employees"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblemp" runat="server" Font-Bold="True" Font-Size="14px" Text="Employee Name :"></asp:Label>
                                                    <asp:Label ID="lblemptext" runat="server" Font-Bold="True" Font-Size="14px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="text-align: left; font-size: 13px; font-weight: bold;">
                                                    <asp:Label ID="lbl" runat="server" Font-Italic="true" Text="Status :"></asp:Label>
                                                    <asp:Label ID="lblstatus" runat="server" Font-Italic="true" Text=""></asp:Label>,
                                                    <asp:Label ID="lblserchtitle" runat="server" Font-Italic="true" Visible="false"></asp:Label>,
                                                    <asp:Label ID="Label1" runat="server" Font-Italic="true" Visible="true" Text="From :"></asp:Label>
                                                    <asp:Label ID="lblsdate" runat="server" Font-Italic="true" Visible="true"></asp:Label>,
                                                    <asp:Label ID="Label2" runat="server" Font-Italic="true" Visible="true" Text="To :"></asp:Label>
                                                    <asp:Label ID="lblenddate" runat="server" Font-Italic="true" Visible="true"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel2" runat="server" Width="100%">
                                        <asp:GridView ID="griddocviewapprvbyemp" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            DataKeyNames="EmployeeID" OnPageIndexChanging="griddocviewapprvbyemp_PageIndexChanging"
                                            OnRowCommand="griddocviewapprvbyemp_RowCommand" EmptyDataText="No Record Found."
                                            Width="100%" OnSorting="griddocviewapprvbyemp_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Business" ShowHeader="true" SortExpression="Wname"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="kkggcgh" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Wname")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Doc Id" ShowHeader="true" SortExpression="DocumentId"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <a onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                            style="color: Black" href="javascript:void(0)">
                                                            <asp:Label ID="kkgg" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>'></asp:Label>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DocumentUploadDate" DataFormatString="{0:dd/MM/yyyy-HH:mm}"
                                                    SortExpression="DocumentUploadDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DocumentTitle" HeaderText="Title" SortExpression="DocumentTitle"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="PartyName" HeaderText="Party" SortExpression="PartyName"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="EmployeeName" HeaderStyle-HorizontalAlign="Left" HeaderText="Employee Name"
                                                    SortExpression="EmployeeName" />
                                                <asp:BoundField DataField="DocumentApproveType" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Approve Type" SortExpression="DocumentApproveType" />
                                                <asp:BoundField DataField="Approve" HeaderStyle-HorizontalAlign="Left" HeaderText="Approve"
                                                    SortExpression="Approve" />
                                                <asp:BoundField DataField="ApproveDate" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:dd/MM/yyyy-HH:mm}"
                                                    SortExpression="ApproveDate" HeaderText="Approval Date"></asp:BoundField>
                                                <asp:BoundField DataField="Note" HeaderText="Note" HeaderStyle-HorizontalAlign="Left" />
                                                <%-- <asp:TemplateField HeaderText="View" ShowHeader="False">
                                            <ItemTemplate>
                                                 <A onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" href="javascript:void(0)">View</A>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                    <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    &nbsp;
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
