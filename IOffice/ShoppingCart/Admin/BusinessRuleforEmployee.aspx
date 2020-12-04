<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="BusinessRuleforEmployee.aspx.cs" Inherits="BusinessRuleforEmployee"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%--<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML,'<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
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
            
           
            if( evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
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
            function mak(id,max_len,myele)
        {
            counter=document.getElementById(id);
            
            if(myele.value.length <= max_len)
            {
                remaining_characters=max_len-myele.value.length;
                counter.innerHTML=remaining_characters;
            }
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
        <ContentTemplate>
            <div style="padding-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend></legend>
                    <table width="100%">
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Business Name"></asp:Label>
                                </label>
                            </td>
                            <td>
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
                                    <asp:Label ID="Label5" runat="server" Text="Related Approval Type"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlapprule" runat="server"
                                        Width="300px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="Approval Status"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlstatus" runat="server"
                                        RepeatDirection="Horizontal" Width="100px">
                                        <asp:ListItem Text="Pending" Value="Pending">Pending</asp:ListItem>
                                        <asp:ListItem Text="Accept" Value="True">Accepted</asp:ListItem>
                                        <asp:ListItem Text="Reject" Value="False">Rejected</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label9" runat="server" Text="Filter by Date"></asp:Label>
                                </label>
                            </td>
                            <td>
                                  <asp:Panel ID="Panel2" runat="server" Visible="true">
                                                                   <label>
                                                                        <asp:Label ID="Label10" runat="server" Text="From "></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true" 
                                                                            ControlToValidate="txtdatefrom" ErrorMessage="*" 
                                                                            ValidationGroup="3" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </label>
                                                                    <label>
                                                                        <asp:TextBox ID="txtdatefrom" runat="server" Width="75px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                                                            CultureName= "en-AU"  Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom"/>
                                                                        
                                                                        <cc1:CalendarExtender  ID="CalendarExtender3" runat="server" PopupButtonID="imgbtn1"
                                                                                TargetControlID="txtdatefrom">
                                                                        </cc1:CalendarExtender>
                                                                    </label>
                                                                    <label>
                                                                        <asp:ImageButton ID="imgbtn1" runat="server" ImageUrl="~/images/cal_btn.jpg" />
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="Label11" runat="server" Text="To "></asp:Label>
                                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="true"
                                                                                ControlToValidate="txtdateto" ErrorMessage="*" 
                                                                            ValidationGroup="3" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </label>
                                                                    <label>
                                                                        <asp:TextBox ID="txtdateto" runat="server" Width="75px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                                                            CultureName= "en-AU"  Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdateto"/>
                                                                                     
                                                                        <cc1:CalendarExtender   ID="CalendarExtender4" runat="server" PopupButtonID="ImageButton5"
                                                                                TargetControlID="txtdateto">
                                                                        </cc1:CalendarExtender>
                                                                    </label>
                                                                    <label>
                                                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/images/cal_btn.jpg" />
                                                                    <input style="width: 1px" id="Hidden1" type="hidden" name="hdnsortExp" runat="Server" />
                                <input style="width: 1px" id="Hidden2" type="hidden" name="hdnsortDir" runat="Server" />
                          
                                                                    </label>
                                                                    </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="Search By"></asp:Label>
                                    
                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ControlToValidate="txtSearch"
                                        Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                        ValidationGroup="3"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">ID</asp:ListItem>
                                    <asp:ListItem Value="1">Title</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtSearch" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',30)"
                                        Width="250px"></asp:TextBox>
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
                            </td>
                            <td>
                                <asp:Button ID="ImageButton1" runat="server" CssClass="btnSubmit" OnClick="ImageButton1_Click1"
                                    Text="Show" ValidationGroup="3" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="List of Documents For My Approval "></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="6">
                                <div style="float: right;">
                                 <label>
                                    <asp:Button ID="imgbtnSubmit" CssClass="btnSubmit" runat="server" OnClick="imgbtnSubmit_Click"
                                        Text="Save" ValidationGroup="2" />
                                       </label>  <label>
                                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" OnClick="Button1_Click"
                                        Text="Print and Export" />
                                        </label> <label>
                                    <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        type="button" value="Print" visible="false" />
                                        </label>  <label>
                   <asp:DropDownList ID="ddlExport" runat="server" 
                            onselectedindexchanged="ddlExport_SelectedIndexChanged" 
                            AutoPostBack="True" Width="130px" Visible="False">
                            
                    
                   </asp:DropDownList>
                   </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td colspan="7">
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" colspan="7">
                                                                <asp:Label ID="lblcompny" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="20px"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="7">
                                                                <asp:Label ID="Label8" runat="server" Font-Italic="true" Font-Bold="True" Text="Business:"
                                                                    Font-Size="18px"></asp:Label>
                                                                <asp:Label ID="lblcomname" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="18px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="7">
                                                                <asp:Label ID="lblhead" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="18px"
                                                                    Text="List of Documents for My Approval (With Document Approval Flow Rule)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="left">
                                                            <td align="left" colspan="7">
                                                                <asp:Label ID="Label2" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="16px"
                                                                    Text="Approval Type :"></asp:Label>
                                                                <asp:Label ID="lblApp" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="16px"
                                                                    Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="left">
                                                            <td align="left" colspan="7">
                                                                <asp:Label ID="Label3" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="16px"
                                                                    Text="Status :"></asp:Label>
                                                                <asp:Label ID="lblst" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="16px"
                                                                    Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                         <tr align="left">
                                                            <td align="left" colspan="7">
                                                                <asp:Label ID="lbldate" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="16px"
                                                                    Visible="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="left">
                                                            <td align="left" colspan="7">
                                                                <asp:Label ID="lblser" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="16px"
                                                                    Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:GridView ID="grid_Rule_master" runat="server" AllowSorting="true" AllowPaging="false"
                                                    PageSize="25" AutoGenerateColumns="False" CssClass="mGrid" 
                                                    GridLines="Both" PagerStyle-CssClass="pgr"
                                                    AlternatingRowStyle-CssClass="alt" DataKeyNames="RuleDetailId" EmptyDataText="No Record Found."
                                                    OnRowCommand="grid_Rule_master_RowCommand" OnSorting="grid_Rule_master_Sorting"
                                                    Width="100%">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ID" ItemStyle-Width="5%"
                                                            SortExpression="DocId">
                                                            <ItemTemplate>
                                                                <a href="javascript:void(0)" onclick='window.open(&#039;ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocId")%>&amp;Did=<%= DesignationId %>&#039;, &#039;welcome&#039;,&#039;fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no&#039;)'>
                                                                    <asp:Label ID="Label1" ForeColor="#426172" runat="server" Text='<%# Eval("DocId") %>'></asp:Label>
                                                                </a>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approval <br/> by Date"
                                                            SortExpression="ProcessDate" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblapprovetobedate" runat="server" Text='<%# Bind("ProcessDate","{0:MM/dd/yyyy-HH:mm}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                           
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="DocumentTitle" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%"
                                                            HeaderText="Title" SortExpression="DocumentTitle">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="20%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approval Rule"
                                                            SortExpression="RuleTitle" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <a href='BusinessProcessRules.aspx?Id=<%#DataBinder.Eval(Container.DataItem, "RuleDetailId")%>'
                                                                    target="_blank">
                                                                    <asp:Label ID="lblruletitle123" ForeColor="#426172" runat="server" Text='<%# Bind("RuleTitle") %>'></asp:Label>
                                                                </a>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approval Type"
                                                            ItemStyle-Width="15%" SortExpression="RuleApproveTypeName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtDescraa" runat="server" Text='<%# Bind("RuleApproveTypeName") %>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblruleid" runat="server" Text='<%# Bind("RuleId") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton1" ForeColor="#426172" runat="server" Text='<%# Bind("RuleApproveTypeName") %>'
                                                                    OnClick="linkdow1_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="15%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approval Type Description"
                                                            SortExpression="Description" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDescr" runat="server" Height="48px" Text='<%# Bind("Description") %>'
                                                                    TextMode="MultiLine" Width="110px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approval History"
                                                            >
                                                            <ItemTemplate>
                                                                <%--  <a href='BusinessRuleManagementreport2.aspx?DocId=<%#DataBinder.Eval(Container.DataItem, "DocId")%>'
                                                                target="_blank">--%>
                                                                <a onclick="window.open('BusinessRuleManagementreport2.aspx?DocId=<%#DataBinder.Eval(Container.DataItem, "DocId")%>')"
                                                                    href="javascript:void(0)">
                                                                  <%--  <asp:Label ID="lblapprovalhistory" ForeColor="#426172" runat="server" Visible="false"
                                                                        Text="Approval History"></asp:Label>--%>
                                                                    <asp:ImageButton ID="ImageButton2approval" Height="18px" Width="18px" runat="server"
                                                                        CausesValidation="false" ImageUrl="~/images/ApprovalHistoryDoc.png" AlternateText="Approval History" ToolTip="Approval History" />
                                                                </a>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                                                            HeaderText="Accept/Reject" ItemStyle-Width="7%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="rbtnAcceptReject" runat="server" RepeatDirection="Horizontal"
                                                                    Width="100px">
                                                                    <asp:ListItem Selected="True" Value="Pending">Pending</asp:ListItem>
                                                                    <asp:ListItem Value="True">Accepted</asp:ListItem>
                                                                    <asp:ListItem Value="False">Rejected</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle VerticalAlign="Top" Width="7%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" 
                                                            HeaderText="Approval Notes" ItemStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lbltxtnoteapprovalnote" runat="server" Text="Approval Notes"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton3" ForeColor="White" runat="server" CausesValidation="false"
                                                                    AutoPostBack="True" OnClick="LinkButton3_Click" Text="Add/View">

                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtNote" Visible="false" runat="server" Height="48px" MaxLength="100"
                                                                    Width="400px" TextMode="MultiLine"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                                                    SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_.\s]*)" ControlToValidate="txtNote"
                                                                    ValidationGroup="2"></asp:RegularExpressionValidator>
                                                                <asp:Label ID="lbltxtnote" runat="server" Text=""></asp:Label>
                                                                <asp:Button ID="btnaddnotes" runat="server" Text="Add Notes" Visible="false" OnClick="btnaddnotes_Click" />
                                                            </ItemTemplate>
                                                          
                                                            <ItemStyle Width="20%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="View &amp; Approve"
                                                            HeaderImageUrl="~/images/ViewandApprove.png" ItemStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <%-- <a href='ViewbyApprove.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocId")%>&amp;Rd=<%#DataBinder.Eval(Container.DataItem,"RuleDetailId") %>'
                                                                target="_blank">--%>
                                                                <a onclick="window.open('ViewByapprove.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocId")%> &amp;Rd=<%#DataBinder.Eval(Container.DataItem,"RuleDetailId") %>')"
                                                                    href="javascript:void(0)">
                                                                    <asp:ImageButton ID="ImageButton2viewandapproval" Height="18px" Width="18px" runat="server" CausesValidation="false" ImageUrl="~/images/ViewandApprove.png" ToolTip="View & Approve" />
                                                                    <asp:Label ID="Labl1" runat="server" ForeColor="#426172" Text="View & Approve" Visible="false"></asp:Label>
                                                                </a>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Send Message" ItemStyle-Width="2%"
                                                            HeaderImageUrl="~/images/SendMessage.png">
                                                            <ItemTemplate>
                                                                <%-- <a href='MessageCompose.aspx?apd=<%#DataBinder.Eval(Container.DataItem, "DocId")%>&amp;Rd=<%#DataBinder.Eval(Container.DataItem,"RuleDetailId") %>'
                                                                target="_blank">--%>
                                                                <a onclick="window.open('MessageCompose.aspx?apd=<%#DataBinder.Eval(Container.DataItem,"DocId")%>&Rd=<%#DataBinder.Eval(Container.DataItem,"RuleDetailId") %>')"
                                                                    href="javascript:void(0)">
                                                                    <asp:Label ID="Lab1" runat="server" ForeColor="#426172" Text="Send Message" Visible="false"></asp:Label>
                                                                    <asp:ImageButton ID="ImageButton2sendmessage" Height="18px" Width="18px" runat="server"
                                                                        CausesValidation="false" ImageUrl="~/images/SendMessage.png" ToolTip="Send Message" />
                                                                </a>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="2%" />
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
                        <tr>
                            <td colspan="3">
                            </td>
                            <td align="left" colspan="3">
                                <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="server" />
                                <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="server" />
                            </td>
                        </tr>
                        </tr>
                </table>
                    <div style="clear: both;">
                    </div>
                    <table>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Panel ID="Panel5" runat="server" CssClass="modalPopup" Width="600px">
                                    <fieldset>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <div style="float: right;">
                                                <label>
                                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" />
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Approval Type
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="lblapprovaltype123456" runat="server" Text=""></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Approval Description
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="lblapprovaldescription123456" runat="server" Text=""></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    &nbsp;&nbsp;&nbsp;</asp:Panel>
                                <asp:Button ID="Button9" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel5" TargetControlID="Button9" CancelControlID="ImageButton4">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <table>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Width="600px">
                                    <fieldset>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <div style="float: right;">
                                                <label>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" />
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Approval Note
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:TextBox ID="TextBox1" TextMode="MultiLine" Height="200px" Width="500px" runat="server"></asp:TextBox>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnsubmitnote" runat="server" Text="Submit" OnClick="btnsubmitnote_Click" />
                                                    <asp:Button ID="btnupdatenote" runat="server" Text="Update" OnClick="btnupdatenote_Click" />
                                                    <asp:Button ID="btncancelnote" runat="server" Text="Cancel" OnClick="btncancelnote_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    &nbsp;&nbsp;&nbsp;</asp:Panel>
                                <asp:Button ID="Button3" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel1" TargetControlID="Button3" CancelControlID="ImageButton2">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
                <asp:PostBackTrigger ControlID="ddlExport"  />
                </Triggers>
    </asp:UpdatePanel>
</asp:Content>
