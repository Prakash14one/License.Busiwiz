<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" 
    AutoEventWireup="true" CodeFile="BusinessRuleManagementReport1.aspx.cs" Inherits="BusinessRuleManagementReport1"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
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
            var prtContent = document.getElementById('<%= pnlgriddd.ClientID %>');
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
         
                  return true;
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
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 25%">
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Business Name"></asp:Label>
                                </label>
                                <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                            </td>
                            <td colspan="3" style="width: 75%">
                                <label>
                                    <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                                        Width="330px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 25%">
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Cabinet-Drawer-Folder"></asp:Label>
                                </label>
                                <td colspan="3" style="width: 75%">
                                    <label>
                                        <asp:DropDownList ID="ddltypeofdoc" runat="server" Width="600px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddltypeofdoc_SelectedIndexChanged1">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 25%">
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Rule Type-Rule Name"></asp:Label>
                                </label>
                            </td>
                            <td colspan="3" style="width: 75%">
                                <label>
                                    <asp:DropDownList ID="DdlRuleName" DataTextField="RuleTitle" DataValueField="RuleId"
                                        runat="server" Width="600px" OnSelectedIndexChanged="DdlRuleName_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 25%">
                                <label>
                                    <asp:Label ID="Label17" runat="server" Text="Filter By"></asp:Label>
                                </label>
                            </td>
                            <td colspan="3" style="width: 75%">
                                <label>
                                    <asp:Label ID="lbldatefrom" runat="server" Text="From "></asp:Label>
                                </label>
                                <label>
                                    <asp:TextBox ID="txtFromDate" runat="server" Width="100px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="TextBox1_MaskedEditExtender2" runat="server" CultureName="en-AU"
                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfromdate" />
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton2"
                                        TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                </label>
                                <label>
                                    <asp:Label ID="lbldateto" runat="server" Text="To "></asp:Label>
                                </label>
                                <label>
                                    <asp:TextBox ID="txtToDate" runat="server" Width="100px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txttodate" />
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgbtncal2"
                                        TargetControlID="txttodate">
                                    </cc1:CalendarExtender>
                                </label>
                                <label>
                                    <asp:ImageButton ID="imgbtncal2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 25%">
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="Search"></asp:Label>
                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="TextBox1"
                                        ValidationGroup="12"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td colspan="3" style="width: 75%">
                                <label>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="0"> By ID</asp:ListItem>
                                        <asp:ListItem Value="1"> By Title</asp:ListItem>
                                    </asp:RadioButtonList>
                                </label>
                                <asp:Panel ID="pnlsearchbytext" runat="server" Visible="false">
                                    <label>
                                        <br />
                                        <asp:TextBox ID="TextBox1" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'Span2',30)"
                                            MaxLength="30" runat="server"></asp:TextBox>
                                    </label>
                                    <label>
                                        <br />
                                        <asp:Label ID="Label16" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span2" class="labelcount">30</span>
                                        <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ )"></asp:Label>
                                    </label>
                                </asp:Panel>
                                <asp:Panel ID="pnlsearchbyid" runat="server">
                                    <label>
                                        <br />
                                        <asp:TextBox ID="TextBox2" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'Span1',30)"
                                            MaxLength="30" runat="server"></asp:TextBox>
                                    </label>
                                    <label>
                                        <br />
                                        <asp:Label ID="Label7" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" class="labelcount">30</span>
                                        <asp:Label ID="Label8" runat="server" CssClass="labelcount" Text="(0-9)"></asp:Label>
                                        <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender323" runat="server"
                                            Enabled="True" TargetControlID="TextBox2" ValidChars="0147852369">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="ImageButton1" runat="server" Text="Go" OnClick="ImageButton1_Click1"
                                    CssClass="btnSubmit" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label11" runat="server" Text="Documet Approval Details " Font-Bold="True"></asp:Label>
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
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlgriddd" runat="server" Width="100%">
                                                <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
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
                                                                            <asp:Label runat="server" ID="ffff" Text="Business : " Font-Italic="true"></asp:Label>
                                                                            <asp:Label ID="lblcomname" runat="server" Font-Italic="true"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                            <asp:Label runat="server" ID="Label15" Text="Documet Approval Details " Font-Italic="true"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="text-align: left;">
                                                                            <asp:Label ID="Label1" runat="server" Font-Italic="true" Text="Cabinet - Drawer - Folder :"></asp:Label>
                                                                            <asp:Label ID="lblcabddff" runat="server" Font-Italic="true"></asp:Label>,
                                                                            <asp:Label ID="Label2" runat="server" Font-Italic="true" Text="Rule Type -Rule Name :"></asp:Label>
                                                                            <asp:Label ID="lblApp" runat="server" Font-Italic="true" Text=""></asp:Label>,
                                                                            <asp:Label ID="lblser" runat="server" Font-Italic="true" Visible="False"></asp:Label>
                            <%--                                                <asp:Label ID="lblhead" runat="server" Font-Italic="true" Text="Approval History - Party"></asp:Label>--%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <cc11:PagingGridView ID="GridView1" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                                                                AllowSorting="true" OnSorting="GridView1_Sorting1" AllowPaging="true" PageSize="25"
                                                                EmptyDataText="No Record Found." OnRowCommand="GridView1_RowCommand" Width="100%"
                                                                OnPageIndexChanging="GridView1_PageIndexChanging">
                                                                <PagerSettings Mode="NumericFirstLast" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" SortExpression="DocId" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <a onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                                                style="color: Black" href="javascript:void(0)">
                                                                                <%# Eval("DocId")%></a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Title" SortExpression="DocumentTitle" ItemStyle-Width="15%"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <a onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                                                style="color: Black" href="javascript:void(0)">
                                                                                <%# Eval("DocumentTitle")%></a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Upload Date" SortExpression="DocumentUploadDate" ItemStyle-Width="12%"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldocumentuploaddate" runat="server" Text='<%# Eval("DocumentUploadDate")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Approval Rule Type" DataField="RuleTypeName" SortExpression="RuleTypeName"
                                                                        ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Approval Rule Name" DataField="RuleTitle" SortExpression="RuleTitle"
                                                                        ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Document Flow Type" DataField="ConditionTypeName" SortExpression="ConditionTypeName"
                                                                        ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Final Status" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Image runat="server" ID="imgfinalstatus" ImageUrl="~/Account/images/closeicon.png"
                                                                                Visible="false" />
                                                                            <asp:Label ID="lblfinalstatusname" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Approval Info" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:DataList RepeatColumns="2" runat="server" ID="DRuleDetail" OnItemDataBound="DRuleDetail_ItemDataBound"
                                                                                RepeatDirection="Horizontal" OnItemCommand="DRuleDetail_ItemCommand" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Panel runat="server" ID="pnlDetail">
                                                                                        <table cellpadding="0" cellspacing="0" id="subinnertbl1">
                                                                                            <tr>
                                                                                                <td colspan="2">
                                                                                                    <asp:Panel ID="pnlhd" runat="server">
                                                                                                        <asp:Label runat="server" ID="lblStep"></asp:Label>
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
                                                                                                    Approval Type
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label runat="server" ID="lblApprovalType"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:HyperLink runat="server" ID="HyperLink1" CommandArgument='<%# Eval("RuledetailId")%>'
                                                                                                        Text="Message" Target="_blank"> </asp:HyperLink>
                                                                                                    <asp:LinkButton runat="server" ID="msglink" CommandArgument='<%# Eval("RuledetailId")%>'
                                                                                                        Visible="false" Text="Message"> </asp:LinkButton><%--<asp:LinkButton  runat="server" id="msglink" Text="Message" > </asp:LinkButton>--%>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:LinkButton runat="server" ID="lbtnViewNote" Text="View Note" CommandName="notes"
                                                                                                        CommandArgument='<%# Eval("EmployeeId")%>' ToolTip='<%# Eval("DocId")%>'>  </asp:LinkButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </ItemTemplate>
                                                                            </asp:DataList>
                                                                            <asp:LinkButton ID="LinkButton1" ForeColor="#426172" runat="server" OnClick="linkdow1_Click"
                                                                                Text="More Info"></asp:LinkButton>
                                                                            <asp:Label ID="lblruleidmaster" runat="server" Text='<%# Eval("RuleId")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbldocid" runat="server" Text='<%# Eval("DocId")%>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="View Full History" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <a href="BusinessRuleManagementreport2.aspx?DocId=<%# Eval("DocId")%>" style="color: Black">
                                                                                View</a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                                <AlternatingRowStyle CssClass="alt" />
                                                            </cc11:PagingGridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <asp:Panel ID="pnlReminder" runat="server" BorderStyle="Outset" Width="254px" BackColor="#CCCCCC"
                    BorderColor="#666666">
                    <table id="innertbl1">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelReminderShow" UpdateMode="Always" runat="server">
                                    <ContentTemplate>
                                        <table cellspacing="0" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label12" runat="server" Text="View Note"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="lblReminderTitle" runat="server"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td align="right">
                                                        <label>
                                                            <asp:Label ID="Label13" runat="server" Text="Close"></asp:Label>
                                                        </label>
                                                        <asp:ImageButton ID="ibtnReminderClose" OnClick="ibtnReminderClose_Click" runat="server"
                                                            ImageUrl="~/Account/images/closeicon.png" Width="16px"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel14" runat="server" Width="97%" Height="100px">
                                                            <table id="subinnertbl1" cellspacing="0" cellpadding="0">
                                                                <tbody>
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
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
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
                <table>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" Width="70%">
                                <fieldset>
                                    <table width="100%">
                                        <div style="float: right;">
                                            <label>
                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="16px" />
                                            </label>
                                        </div>
                                        <div>
                                            <asp:GridView ID="GridView2" runat="server" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" EmptyDataText="No Record Found."
                                                Width="100%" AllowSorting="false" OnRowDataBound="GridView2_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Approval No." ItemStyle-ForeColor="Black" ItemStyle-Width="10%"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldetailstepno" runat="server" Text='<%# Eval("StepNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name of Employee" ItemStyle-ForeColor="Black" ItemStyle-Width="25%"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnameofemployee" runat="server" Text='<%# Eval("EmployeeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-ForeColor="Black" ItemStyle-Width="10%"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatusdetail" runat="server" Text='<%# Eval("ApprovalStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approval Type" ItemStyle-ForeColor="Black" ItemStyle-Width="20%"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapprovaltype" runat="server" Text='<%# Eval("ApprovalType")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approval By Date" ItemStyle-ForeColor="Black" ItemStyle-Width="15%"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapprovalbydate" runat="server" Text='<%# Eval("ApprovalReqDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actual Approval Date" ItemStyle-ForeColor="Black"
                                                        ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblactualapprovaldate" runat="server" Text='<%# Eval("ApprovedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </table>
                                </fieldset></asp:Panel>
                            <asp:Button ID="Button5" runat="server" Style="display: none" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                PopupControlID="Panel3" TargetControlID="Button5" CancelControlID="ImageButton3">
                            </cc1:ModalPopupExtender>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
