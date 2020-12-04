<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentViewApproveDetail.aspx.cs" Inherits="Account_DocumentMyUploaded"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%--<%@ Register Src="~/Account/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>--%>
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
    </style>

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
          function mask(evt)
        { 
         
           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if(evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }     
            
        }   
         function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered an invalid character");
            }   
        
            counter=document.getElementById(id);
            
            if(txt1.value.length <= max_len)
            {
                remaining_characters=max_len-txt1.value.length;
                counter.innerHTML=remaining_characters;
            }
        } 
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <div style="float: left;">
                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <table width="100%">
                            <tr>
                                <td width="25%">
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td colspan="3" width="75%">
                                    <label>
                                        <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                                            Width="300px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Filter by
                                    </label>
                                </td>
                                <td width="10%">
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Selected="True">Period</asp:ListItem>
                                        <asp:ListItem Value="1">Date</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td colspan="2">
                                    <asp:Panel ID="pnlperiod" runat="server">
                                        <label>
                                            <asp:Label ID="Label6" runat="server" Text="Period"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:DropDownList ID="ddlDuration" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDuration_SelectedIndexChanged">
                                                <asp:ListItem>Today</asp:ListItem>
                                                <asp:ListItem>Yesterday</asp:ListItem>
                                                <asp:ListItem Selected="True">This Week</asp:ListItem>
                                                <asp:ListItem>This Month</asp:ListItem>
                                                <asp:ListItem>This Quarter</asp:ListItem>
                                                <asp:ListItem>This Year</asp:ListItem>
                                                <asp:ListItem>Last Week</asp:ListItem>
                                                <asp:ListItem>Last Month</asp:ListItem>
                                                <asp:ListItem>Last Quarter</asp:ListItem>
                                                <asp:ListItem>Last Year</asp:ListItem>
                                            </asp:DropDownList>
                                        </label>
                                    </asp:Panel>
                                    <asp:Panel ID="pnldate" runat="server" Visible="False">
                                        <label>
                                            <asp:Label ID="Label7" runat="server" Text=" From"></asp:Label>
                                            <asp:RegularExpressionValidator ID="rghjk" runat="server" ErrorMessage="*" ControlToValidate="txtfrom"
                                                ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                        </label>
                                        <label>
                                            <asp:TextBox ID="txtfrom" runat="server" Width="100px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:ImageButton ID="imgbtncalfrom" runat="server" Width="20px" Height="20px" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                        </label>
                                        <label>
                                            <asp:Label ID="Label8" runat="server" Text="To"></asp:Label>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                                                ControlToValidate="txtto" ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                        </label>
                                        <label>
                                            <asp:TextBox ID="txtto" runat="server" Width="100px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:ImageButton ID="imgbtnto" runat="server" Width="20px" Height="20px" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                        </label>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="3">
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Cabinet-Drawer-Folder"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddldoctype"
                                            Display="Dynamic" ErrorMessage="*" InitialValue="0" ValidationGroup="3"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td colspan="3">
                                    <label>
                                        <asp:DropDownList ID="ddldoctype" runat="server" Width="600px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddldoctype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        List of Documents
                                    </label>
                                </td>
                                <td colspan="3">
                                    <label>
                                        <asp:DropDownList ID="ddllistofdocument" runat="server" Width="600px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Filter by Employee
                                        <br />
                                        Deptartment: Designation: Name
                                    </label>
                                </td>
                                <td colspan="3">
                                    <label>
                                        <asp:DropDownList ID="ddlemployee" runat="server" CausesValidation="false" DataTextField="EmployeeName"
                                            DataValueField="EmployeeID" ValidationGroup="1" Width="600px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Filter by Status
                                    </label>
                                </td>
                                <td colspan="3">
                                    <label>
                                        <asp:DropDownList ID="ddlstatus" runat="server" Width="150px">
                                            <asp:ListItem Value="2" Selected="True">All</asp:ListItem>
                                            <asp:ListItem Value="1">Accepted</asp:ListItem>
                                            <asp:ListItem Value="0">Rejected</asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="Search by Title"></asp:Label>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                            SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)" ControlToValidate="txtsearch"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td colspan="3">
                                    <label>
                                        <asp:TextBox ID="txtsearch" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',30)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" class="labelcount">30</span>
                                        <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ )"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                    <label>
                                        <asp:Label ID="Label10" runat="server" Text="Filter by Party"></asp:Label>
                                        <br />
                                        Party Name: Contact Name: Party Type
                                    </label>
                                </td>
                                <td colspan="3">
                                    <asp:Panel ID="pnlfilterbyparty" runat="server" Visible="false">
                                        <label>
                                            <asp:DropDownList ID="ddlfilterbyparty" Width="350px" runat="server">
                                            </asp:DropDownList>
                                        </label>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="3">
                                    <asp:Button ID="ImageButton1" runat="server" Text="Go" OnClick="ImageButton1_Click"
                                        CssClass="btnSubmit" ValidationGroup="1" />
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
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label9" runat="server" Text="General Document Approval History (Without Document Flow Rule)"></asp:Label>
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
                                                                <asp:Label ID="lblcompany" runat="server" Font-Italic="true" Visible="false" Font-Size="20px" ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="sdfdsfdsffdsf" runat="server" Font-Italic="true" Text="Business : "></asp:Label>
                                                                <asp:Label ID="lblcomname" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="lblhead" runat="server" Font-Italic="true" Text="General Document Approval History (Without Document Flow Rule)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblemp" runat="server" Font-Italic="true" Text="Cabinet-Drawer-Folder :"></asp:Label>
                                                                <asp:Label ID="lblcabinetdrawerfolderprint" runat="server" Font-Italic="true"></asp:Label>
                                                                &nbsp;
                                                                <asp:Label ID="lblemptext" runat="server" Font-Italic="true" Text="List of Documents:"></asp:Label>
                                                                <asp:Label ID="lbldocidprint" runat="server" Font-Italic="true"></asp:Label>
                                                                &nbsp;
                                                                <asp:Label ID="Label1" runat="server" Font-Italic="true" Text="Search by Title :"></asp:Label>
                                                                <asp:Label ID="lblsearchbytitleprint" Font-Italic="true" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <cc11:PagingGridView ID="Gridreqinfo" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                        EmptyDataText="No Record Found." DataKeyNames="DocumentTypeId" OnRowCommand="Gridreqinfo_RowCommand"
                                                        OnPageIndexChanging="Gridreqinfo_PageIndexChanging" OnSorting="Gridreqinfo_Sorting"
                                                        Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Doc ID" ShowHeader="true" SortExpression="DocumentId"
                                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <a onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                                        style="color: #426172" href="javascript:void(0)">
                                                                        <asp:Label ID="kkgg" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>'></asp:Label>
                                                                    </a>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="DocumentUploadDate" DataFormatString="{0:dd/MM/yyyy-HH:mm}"
                                                                ItemStyle-Width="5%" SortExpression="DocumentUploadDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Folder" DataField="DocumentType" SortExpression="DocumentType"
                                                                ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DocumentTitle" HeaderText="Title" SortExpression="DocumentTitle"
                                                                ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="15%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PartyName" HeaderText="Party" ItemStyle-Width="10%" SortExpression="PartyName"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ItemStyle-Width="10%"
                                                                SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DocumentApproveType" HeaderText="Approval Type" ItemStyle-Width="10%"
                                                                SortExpression="DocumentApproveType" HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Status" SortExpression="Approvelabel" ItemStyle-Width="5%"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblapprovallabel" runat="server" Text='<%#Bind("Approvelabel")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ApproveDate" ItemStyle-Width="10%" DataFormatString="{0:dd/MM/yyyy-HH:mm}"
                                                                SortExpression="ApproveDate" HeaderStyle-HorizontalAlign="Left" HeaderText="Approval Date/Time">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Note" ItemStyle-Width="20%" SortExpression="Note"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lbltxtnoteapprovalnote" runat="server" Text="Note"></asp:Label>
                                                                    <asp:LinkButton ID="LinkButton3" ForeColor="White" runat="server" CausesValidation="false"
                                                                        AutoPostBack="True" OnClick="LinkButton3_Click" Text="(More Info)">
                                                                    </asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblapprovalnotesmall" runat="server" Text='<%#Bind("Notesmall")%>'></asp:Label>
                                                                    <asp:Label ID="lblapprovalnotebig" runat="server" Text='<%#Bind("Note")%>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="20%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </cc11:PagingGridView>
                                                    <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                                    <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                                </asp:Panel>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
