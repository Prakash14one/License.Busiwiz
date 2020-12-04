<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" 
    AutoEventWireup="true" CodeFile="PartyDocApproveDetail.aspx.cs" Inherits="Account_PartyDocApproveDetail"
    Title="Untitled Page" %>

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
            var prtContent = document.getElementById('<%= pnlgridd.ClientID %>');
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

    <table style="width: 100%">
        <%-- <a href="BusinessRuleManagementreport2.aspx?DocId=<%# Eval("DocId")%>">
                                                                                        <%# Eval("DocId")%>
                                                                                  </a>  --%>
        <tr>
            <td>
                <asp:Panel ID="pnlmsg" runat="server" Width="100%">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <%# Eval("DocId")%>
            </td>
        </tr>
    </table>
    <%-- <a href="BusinessRuleManagementreport2.aspx?DocId=<%# Eval("DocId")%>">
                                                                                        <%# Eval("DocId")%>
                                                                                    </a>  --%>
    <table id="secondtbl" cellpadding="0" cellspacing="3" width="100%">
        <tr>
            <td>
                <%# Eval("DocId")%>
                <table id="innertbl1" cellpadding="0" cellspacing="3" width="100%">
                    <tr>
                        <td style="width: 13%" align="right">
                            <label>
                                <asp:Label ID="Label4" runat="server" Text="Business Name"></asp:Label>
                            </label>
                        </td>
                        <td colspan="3">
                            <label>
                                <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                                    Width="313px">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <label>
                                <asp:Label ID="Label5" runat="server" Text="Cabinet-Drawer-Folder"></asp:Label>
                            </label>
                        </td>
                        <td colspan="3">
                            <label>
                                <asp:DropDownList ID="ddltypeofdoc" runat="server" Width="313px" OnSelectedIndexChanged="ddltypeofdoc_SelectedIndexChanged1">
                                </asp:DropDownList>
                            </label>
                            <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                            <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" align="right">
                            <label>
                                <asp:Label ID="Label6" runat="server" Text="Rule Type-Rule Name"></asp:Label>
                            </label>
                        </td>
                        <td colspan="3">
                            <label>
                                <asp:DropDownList ID="DdlRuleName" DataTextField="RuleTitle" DataValueField="RuleId"
                                    runat="server" Width="313px" OnSelectedIndexChanged="DdlRuleName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 13%" align="right">
                            <label>
                                <asp:Label ID="Label7" runat="server" Text="Select Status"></asp:Label>
                            </label>
                        </td>
                        <td colspan="3">
                            <label>
                                <asp:DropDownList ID="ddlstatus" runat="server" DataTextField="EmployeeName" DataValueField="EmployeeID"
                                    ValidationGroup="1" Width="313px" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                    <asp:ListItem>-All-</asp:ListItem>
                                    <asp:ListItem Value="True">Approved</asp:ListItem>
                                    <asp:ListItem Value="False">Not Approved</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 13%" align="right">
                            <label>
                                <asp:Label ID="Label8" runat="server" Text="Party Type-Party Name"></asp:Label>
                            </label>
                        </td>
                        <td colspan="3">
                            <label>
                                <asp:DropDownList ID="ddlparty" runat="server" DataTextField="PartyName" DataValueField="PartyId"
                                    Width="313px" OnSelectedIndexChanged="ddlparty_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" align="right">
                            <label>
                                <asp:Label ID="Label9" runat="server" Text="Search"></asp:Label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Inavalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s_.]*)"
                                            ControlToValidate="TextBox1" ValidationGroup="1"></asp:RegularExpressionValidator>
                            </label>
                        </td>
                        <td style="width: 25%">
                            <label>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="180px" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0"> By Id</asp:ListItem>
                                    <asp:ListItem Value="1"> By Title</asp:ListItem>
                                </asp:RadioButtonList>
                            </label>
                        </td>
                        <td colspan="2" style="width: 50%" valign="bottom">
                            <label>
                                <asp:TextBox ID="TextBox1" runat="server" Width="150px" MaxLength="20" onKeydown="return mask(event)"
                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',20)"></asp:TextBox>
                                Char Rem <span id="div1">20</span>
                                <asp:Label ID="Label25" runat="server" Text="(A-Z,0-9,_)"></asp:Label>
                            </label>
                            <asp:Button ID="ImageButton1" runat="server" Text="Show" ValidationGroup="1" OnClick="ImageButton1_Click1"
                                CssClass="btnSubmit" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="3">
                            <asp:Button ID="btngo" runat="server" Text=" Go "  CssClass="btnSubmit" OnClick="btngo_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            <label>
                                <asp:Panel BackColor="Red" ID="Panel2" runat="server" Width="50px" Height="10px">
                                </asp:Panel>
                                <asp:Label ID="Label10" runat="server" Text="Approved after approval due date"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <label>
                                <asp:Image runat="server" ID="imgfinalstatus" ImageUrl="~/Account/images/closeicon.png" />
                                <asp:Label ID="Label11" runat="server" Text="Document is not approved by Party."></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <label>
                                <asp:Image runat="server" ID="Image1" ImageUrl="~/Account/images/Right.jpg" />
                                <asp:Label ID="Label12" runat="server" Text="Document is approved by Party."></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <label>
                                <asp:Image runat="server" ID="Image2" ImageUrl="~/Account/images/Datapending.jpg" />
                                <asp:Label ID="Label13" runat="server" Text="Document is Pending for Party Approval."></asp:Label>
                            </label>
                        </td>
                    </tr>
                </table>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label14" runat="server" Text="Approval History - Party"></asp:Label>
                    </legend>
                    <table cellpadding="0" cellspacing="3" width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                    OnClick="Button1_Click" />
                                <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    style="width: 51px;" type="button" value="Print" visible="false" class="btnSubmit" />
                                <%--   <input type="button" value="Print" id="Button3" runat="server" onclick="javascript:CallPrint('divPrint')"
                                style="background-color: #CCCCCC; width: 51px;" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlgridd" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblcompany" runat="server" Font-Italic="true" Font-Size="20px" ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label runat="server" ID="bus" Text="Business : " Font-Italic="true"></asp:Label>
                                                                <asp:Label ID="lblcomname" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="lblhead" runat="server" Font-Italic="true" Text="List of Approval History - Party"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="text-align: left; font-size: 13px; font-weight: bold;">
                                                                <asp:Label ID="Label1" runat="server" Font-Italic="true" Text="Cabinet - Drower - Folder :"></asp:Label>
                                                                <asp:Label ID="lblcabddff" runat="server" Font-Italic="true"></asp:Label>,
                                                                <asp:Label ID="Label2" runat="server" Font-Italic="true" Text="Approval Type -Rule Name :"></asp:Label>
                                                                <asp:Label ID="lblApp" runat="server" Font-Italic="true" Text=""></asp:Label>,
                                                                <asp:Label ID="Label3" runat="server" Font-Italic="true" Text="Status :"></asp:Label>
                                                                <asp:Label ID="lblst" runat="server" Font-Italic="true" Text=""></asp:Label>,
                                                                <asp:Label ID="lblser0" runat="server" Font-Italic="true">Party 
                                                            Type - Party Name :</asp:Label>
                                                                <asp:Label ID="lblpartyname" runat="server" Font-Italic="true"></asp:Label>
                                                                <asp:Label ID="lblser" runat="server" Font-Italic="true" Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                                                            OnSorting="GridView1_Sorting1" EmptyDataText="No Record Found." OnRowCommand="GridView1_RowCommand"
                                                            Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="View Full History" SortExpression="DocId" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <a href="BusinessRuleManagementreport2.aspx?DocId=<%# Eval("DocId")%>" style="color: Black">
                                                                            View</a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Doc ID" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <%-- <a href="BusinessRuleManagementreport2.aspx?DocId=<%# Eval("DocId")%>">
                                                                                        <%# Eval("DocId")%>
                                                                                    </a>  --%>
                                                                        <a onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                                            style="color: Black" href="javascript:void(0)">
                                                                            <%# Eval("DocId")%></a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- <asp:TemplateField HeaderText="View" ShowHeader="False">
                                                        <ItemTemplate>
                                                        <A onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" href="javascript:void(0)">View</A>
                                                          
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Doc Title" SortExpression="DocumentTitle" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <a onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                                            style="color: Black" href="javascript:void(0)">
                                                                            <%# Eval("DocumentTitle")%></a>
                                                                        <%-- <a href="BusinessRuleManagementreport2.aspx?DocId=<%# Eval("DocId")%>">
                                                                                        <%# Eval("DocumentTitle")%>
                                                                                    </a> --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Rule Type" DataField="RuleTypeName" SortExpression="RuleTypeName"
                                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Rule Name" DataField="RuleTitle" SortExpression="RuleTitle"
                                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Flow of Document" DataField="ConditionTypeName" SortExpression="ConditionTypeName"
                                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                <asp:TemplateField HeaderText="Final Status" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <%--  <asp:Label runat="server" ID="lblFinalStatus" ></asp:Label>--%>
                                                                        <asp:Image runat="server" ID="imgfinalstatus" ImageUrl="~/Account/images/closeicon.png" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Approval Info" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:DataList RepeatColumns="4" runat="server" ID="DRuleDetail" OnItemDataBound="DRuleDetail_ItemDataBound"
                                                                            RepeatDirection="Horizontal" OnItemCommand="DRuleDetail_ItemCommand">
                                                                            <ItemTemplate>
                                                                                <asp:Panel runat="server" ID="pnlDetail">
                                                                                    <table cellpadding="0" cellspacing="0" id="subinnertbl1">
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                                <asp:Panel ID="pnlhd" runat="server" Width="100%">
                                                                                                    &nbsp;<asp:Label runat="server" ID="lblStep"></asp:Label>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label runat="server" ID="lblRuleDetail1"></asp:Label>Status
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label runat="server" ID="lblStatus"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                Approval Required Date
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label runat="server" ID="lblReqDate"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                Approved Date
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label runat="server" ID="lblApprovedDate"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                Approval Type :
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label runat="server" ID="lblApprovalType"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:LinkButton runat="server" ID="msglink" Text="Message"> </asp:LinkButton>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:LinkButton runat="server" ID="lbtnViewNote" Text="View Note" CommandName="notes"
                                                                                                    CommandArgument='<%# Eval("PartyId")%>' ToolTip='<%# Eval("DocId")%>'>  </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btngo" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="ImageButton1" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
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
    <asp:Panel ID="pnlReminder" runat="server" BorderStyle="Outset" Width="254px" BackColor="#CCCCCC"
        BorderColor="#666666">
        <table>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanelReminderShow" UpdateMode="Always" runat="server">
                        <ContentTemplate>
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label15" runat="server" Text="View Note"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:Label ID="lblReminderTitle" runat="server"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label16" runat="server" Text="Close"></asp:Label>
                                        </label>
                                        <asp:ImageButton ID="ibtnReminderClose" OnClick="ibtnReminderClose_Click" runat="server"
                                            ImageUrl="~/Account/images/closeicon.png" Width="16px"></asp:ImageButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel14" runat="server" Width="97%" Height="100px">
                                            <table id="subinnertbl1" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <label>
                                                            <asp:Label ID="lblReminderDetail" runat="server" __designer:wfdid="w13"></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <%--   <asp:AsyncPostBackTrigger  ControlID="lbtnViewNote"/>--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="Modalpopupextender2" BackgroundCssClass="modalBackground"
        PopupControlID="pnlReminder" TargetControlID="Hidden1" X="365" Y="-200" runat="server">
    </cc1:ModalPopupExtender>
    <input id="Hidden1" name="Hidden1" runat="Server" type="hidden" />
</asp:Content>
