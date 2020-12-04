<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" 
    AutoEventWireup="true" CodeFile="BusinessRuleManagementreport2.aspx.cs" Inherits="BusinessRuleManagementreport2"
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
        var prtContent = document.getElementById('<%= pnlgr.ClientID %>');
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
            
           
            if( evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186 ||evt.keyCode==59  )
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td style="width: 40%">
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Business Name"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 60%">
                                <label>
                                    <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                                        Width="300px">
                                    </asp:DropDownList>
                                </label>
                                <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%">
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Search Document"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDocTitle"
                                        ErrorMessage="*" ValidationGroup="1">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                        SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtDocTitle"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td style="width: 60%">
                                <label>
                                    <asp:RadioButton ID="RadioButton1" runat="server" Text="By ID" Checked="true" GroupName="1"
                                        ValidationGroup="1"></asp:RadioButton>
                                </label>
                                <label>
                                    <asp:RadioButton ID="RadioButton4" runat="server" GroupName="1" Text="By Title" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtDocTitle" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%^'&*@.()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span2',60)"
                                        runat="server" Width="198px" ValidationGroup="1" MaxLength="60"></asp:TextBox></label>
                                <label>
                                    Max <span id="Span2">60</span>
                                    <asp:Label ID="Label21" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%">
                                <br />
                            </td>
                            <td style="width: 40%">
                                <asp:Button ID="ibtnSearchShow" CssClass="btnSubmit" runat="server" Text="Show" OnClick="ibtnSearchShow_Click"
                                    ValidationGroup="1" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    <asp:Label ID="lblmsgDocUploadValidation" runat="server" Font-Bold="true"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel3" runat="server" Width="100%">
                                <fieldset>
                                <legend>
                                Select the Document for which you wish to view approval history
                                </legend>
                               
                                    <asp:GridView ID="GridView3" runat="server" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" DataKeyNames="DocumentId" AutoGenerateColumns="False"
                                        EmptyDataText="No Record Found." Width="100%" OnRowCommand="GridView3_RowCommand1"
                                        AllowSorting="True" OnSorting="GridView3_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="Wname">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwname" runat="server" Text='<%# Eval("Wname")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cabinet-Drawer-Folder" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="Dos">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcabi" runat="server" Text='<%# Eval("Dos")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="Left" SortExpression="PartyName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpartyname" runat="server" Text='<%# Eval("PartyName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Document Title" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="DocumentTitle">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbltt" runat="server" ForeColor="#426172" Text='<%# Eval("DocumentTitle")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField CommandName="Select"  ItemStyle-ForeColor="#426172"   HeaderStyle-HorizontalAlign="Left" HeaderText="Select"
                                                Text="Select" />
                                        </Columns>
                                    </asp:GridView>
                                     </fieldset>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Single Document Approval History"></asp:Label>
                    </legend>
                     <asp:Panel  ID="Panel4" runat="server" >
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="right">
                                <div style="float: right;">
                                 <asp:Button ID="Button2" runat="server" Text="Printable Version" CssClass="btnSubmit" OnClick="Button1_Click"/>
                                    <input type="button" value="Print" visible="false" id="Button3" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        class="btnSubmit" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlgr" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnldd" runat="server" Visible="true">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td>
                                                                <div id="mydiv" class="closed">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Panel ID="lblim" runat="server" Height="1130px" Width="100%" ScrollBars="Both">
                                                                                    <asp:DataList ID="DataList1" runat="server">
                                                                                        <ItemTemplate>
                                                                                            <table cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Image ID="Image2" runat="server" ImageUrl='<%#Eval("image")%>' />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td bgcolor="silver" style="height: 9px">
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" colspan="4">
                                                                                <asp:Label ID="lblcom" runat="server" Font-Bold="True" Font-Italic="true" Font-Size="20px"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" colspan="4">
                                                                                <asp:Label ID="Label2" runat="server" Font-Italic="true" Text="Business:" Font-Bold="True"
                                                                                    Font-Size="20px"></asp:Label>
                                                                                <asp:Label ID="lblcomname" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="20px"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" colspan="4">
                                                                                <asp:Label ID="lbl" runat="server" Font-Bold="True" Font-Italic="true" Font-Size="16px"
                                                                                    Text="Document Id"></asp:Label>&nbsp;-
                                                                                <asp:Label ID="lblDocID" Font-Bold="True" Font-Size="16px" Font-Italic="true" runat="server"
                                                                                    Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" colspan="4">
                                                                                <asp:Label ID="lbslbb" Font-Bold="True" Font-Size="16px" Font-Italic="true" runat="server"
                                                                                    Text="Document Title"></asp:Label>&nbsp;-
                                                                                <asp:Label ID="lblDocName" Font-Bold="True" Font-Size="16px" Font-Italic="true" runat="server"
                                                                                    Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" colspan="4">
                                                                                <asp:Label ID="lblbdb" Font-Bold="True" Font-Size="16px" Font-Italic="true" runat="server"
                                                                                    Text="Party Name"></asp:Label>&nbsp;-
                                                                                <asp:Label ID="lblPartyName" Font-Bold="True" Font-Size="16px" Font-Italic="true"
                                                                                    runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" colspan="4">
                                                                                <asp:Label ID="lblbfb" Font-Bold="True" Font-Size="16px" Font-Italic="true" runat="server"
                                                                                    Text="Document Date"></asp:Label>&nbsp;-
                                                                                <asp:Label ID="lblDocDate" Font-Bold="True" Font-Size="16px" Font-Italic="true" runat="server"
                                                                                    Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    <asp:Label ID="lbldoc" runat="server" Text="Document Approval History (As part of any document flow rule)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right">
                                                <asp:CheckBox ID="chkapp" runat="server" AutoPostBack="true" Checked="true" Text="View approval note"
                                                    OnCheckedChanged="chkapp_CheckedChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="Panel2" runat="server" Width="100%">
                                                    <asp:GridView ID="GridView2" runat="server" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound"
                                                        EmptyDataText="No Record Found." Width="100%" AllowSorting="True" OnSorting="GridView2_Sorting">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Approval Date/Time" HeaderStyle-HorizontalAlign="Left"
                                                                DataField="RuleProcessDate" SortExpression="RuleProcessDate" />
                                                            <asp:BoundField HeaderText="Department" HeaderStyle-HorizontalAlign="Left" DataField="Departmentname"
                                                                SortExpression="Departmentname" />
                                                            <asp:BoundField HeaderText="Designation" HeaderStyle-HorizontalAlign="Left" DataField="DesignationName"
                                                                SortExpression="DesignationName" />
                                                            <asp:BoundField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" DataField="EmployeeName"
                                                                SortExpression="EmployeeName" />
                                                            <asp:BoundField HeaderText="Approve Type" HeaderStyle-HorizontalAlign="Left" DataField="RuleApproveTypeName"
                                                                SortExpression="RuleApproveTypeName"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Rule Type" HeaderStyle-HorizontalAlign="Left" DataField="RuleTypeName"
                                                                SortExpression="RuleTypeName"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Rule Name" HeaderStyle-HorizontalAlign="Left" DataField="RuleTitle"
                                                                SortExpression="RuleTitle"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Flow of Document" HeaderStyle-HorizontalAlign="Left"
                                                                DataField="ConditionTypeName" SortExpression="ConditionTypeName"></asp:BoundField>
                                                                
                                                            <asp:TemplateField HeaderText="Final Status" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    
                                                                    <asp:Image runat="server" ID="imgfinalstatus" ImageUrl="~/Account/images/closeicon.png" Visible="false" />
                                                                    
                                                                      <asp:Label runat="server" ID="lblfinalstatusword" ></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Approval Note" HeaderStyle-HorizontalAlign="Left" DataField="Note"
                                                                SortExpression="Note" />
                                                            <asp:TemplateField HeaderText="Approval Note" HeaderStyle-HorizontalAlign="Left"
                                                                Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:DataList RepeatColumns="4" runat="server" ID="DRuleDetail" OnItemDataBound="DRuleDetail_ItemDataBound"
                                                                        RepeatDirection="Horizontal" Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:Panel runat="server" ID="pnlDetail" Width="100%">
                                                                                <table cellpadding="0" cellspacing="0" id="subinnertbl1">
                                                                                    <tr>
                                                                                        <td colspan="2" class="c2">
                                                                                            <asp:Panel ID="pnlhd" runat="server" Width="100%">
                                                                                                &nbsp;<asp:Label runat="server" ID="lblStep"></asp:Label>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="c2" style="width: 28%">
                                                                                            <asp:Label runat="server" ID="lblRuleDetail1"></asp:Label>
                                                                                            Status
                                                                                        </td>
                                                                                        <td class="c2">
                                                                                            <asp:Label runat="server" ID="lblStatus"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="c2" style="width: 28%">
                                                                                            Approval Required Date
                                                                                        </td>
                                                                                        <td class="c2">
                                                                                            <asp:Label runat="server" ID="lblReqDate"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="c2" style="width: 28%">
                                                                                            Approved Date
                                                                                        </td>
                                                                                        <td class="c2">
                                                                                            <asp:Label runat="server" ID="lblApprovedDate"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="c2" style="width: 28%">
                                                                                            Approval Type :
                                                                                        </td>
                                                                                        <td class="c2">
                                                                                            <asp:Label runat="server" ID="lblApprovalType"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="2" class="c2">
                                                                                            <a href="AfterLoginforAdmin.aspx">Message</a>
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
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="left">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    <asp:Label ID="lbllegend1" runat="server" Text="Document Approval History (Not part of any document flow rule)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlgene" runat="server" Width="100%">
                                                    <asp:GridView ID="grd_general" runat="server" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" EmptyDataText="No Record Found."
                                                        Width="100%" AllowSorting="True" OnSorting="grd_general_Sorting">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Approval Date/Time" HeaderStyle-HorizontalAlign="Left"
                                                                DataField="ApproveDate" SortExpression="ApproveDate" />
                                                            <asp:BoundField HeaderText="Department" HeaderStyle-HorizontalAlign="Left" DataField="Departmentname"
                                                                SortExpression="Departmentname" />
                                                            <asp:BoundField HeaderText="Designation" HeaderStyle-HorizontalAlign="Left" DataField="DesignationName"
                                                                SortExpression="DesignationName" />
                                                            <asp:BoundField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" DataField="EmployeeName"
                                                                SortExpression="EmployeeName" />
                                                            <asp:BoundField HeaderText="Approve Type" HeaderStyle-HorizontalAlign="Left" DataField="RuleApproveTypeName"
                                                                SortExpression="RuleApproveTypeName"></asp:BoundField>
                                                            <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" SortExpression="Approve">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblst" Text='<%# Bind("Approve") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Approval Note" HeaderStyle-HorizontalAlign="Left" DataField="Note"
                                                                SortExpression="Note" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    <asp:Label ID="lblbdesk" runat="server" Text="Filing Desk Approval History"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="Panel1" runat="server" Width="100%">
                                                    <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" EmptyDataText="No Record Found."
                                                        Width="100%" AllowSorting="True">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Employee" HeaderStyle-HorizontalAlign="Left" DataField="EmployeeName">
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Allocated Date" HeaderStyle-HorizontalAlign="Left" DataField="DocAllocateDate">
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Approved Date" DataField="ApproveDate" HeaderStyle-HorizontalAlign="Left">
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                    <asp:Panel  ID="Panel5" Visible="false" runat="server" >
                    <label>
                    <asp:Label ID="Label3" runat="server" Text="None" ForeColor="Red"></asp:Label>
                    </label>
                    </asp:Panel>
                    
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
