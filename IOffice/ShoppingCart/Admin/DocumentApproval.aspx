<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentApproval.aspx.cs" Inherits="Account_DocumentApproval"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>--%>
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

    <script type="text/javascript" language="javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnl_grid_priceplan.ClientID %>');
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
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Business Name"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 70%">
                                <label>
                                    <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Designation"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="lbldesignation" runat="server" Text=""></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Employee Name"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlemp" runat="server" 
                                    OnSelectedIndexChanged="ddlemp_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Approval Status"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlapproval" runat="server" 
                                    OnSelectedIndexChanged="ddlapproval_SelectedIndexChanged">
                                        <asp:ListItem Value="5" Text="Pending-All"></asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0" Text="Pending-New"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Pending-Returned"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Rejected"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Approved"></asp:ListItem>
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
                            <td>
                               <table>
                              <tr>
                              <td>  <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Selected="True">Period</asp:ListItem>
                                        <asp:ListItem Value="1">Date</asp:ListItem>
                                    </asp:RadioButtonList></td>
                               <td>  
                                    <asp:Panel ID="pnlperiod" runat="server">
                                        <label>
                                            <asp:Label ID="Label12" runat="server" Text="Period"></asp:Label>
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
                                            <asp:Label ID="Label14" runat="server" Text="From Date"></asp:Label>
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
                                            <asp:Label ID="Label16" runat="server" Text="To Date"></asp:Label>
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
                              </table>
                                  
                               
                              
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>
                                    <asp:Label ID="Label13" runat="server" Text="Search by Title"></asp:Label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                        ControlToValidate="txtsearch" ValidationGroup="1"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtsearch" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span1',30)"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label ID="Label18" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="Span1" class="labelcount">30</span>
                                    <asp:Label ID="Label19" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ )"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label20" runat="server" Text=" Document Approved by Office Clerk"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlfilterofficeclerk" runat="server">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label24" runat="server" Text="Office Clerk's Approval Status"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlofficestatus" runat="server">
                                        <asp:ListItem Value="5" Selected="True" Text="Any"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Pending-New"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Pending-Returned"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Rejected"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Approved"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label21" runat="server" Text="Document Approved by Supervisor"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlsupervisorfilter" runat="server">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label27" runat="server" Text="Supervisor's Approval Status"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlsupervisorstatus" runat="server">
                                        <asp:ListItem Value="5" Selected="True" Text="Any"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Pending-New"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Pending-Returned"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Rejected"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Approved"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="ImageButton1" runat="server" Text="Go" CssClass="btnSubmit" ValidationGroup="1"
                                    OnClick="ImageButton1_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label6" runat="server" Text="Documents For Filing"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <input type="hidden" id="Hidden1" runat="server" value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <div style="float: left;">
                                    <asp:Button ID="Button4" CssClass="btnSubmit" runat="server" Text="Select Display Columns"
                                        OnClick="Button4_Click" />
                                    <asp:Button ID="Button5" CssClass="btnSubmit" runat="server" Text="Refresh" OnClick="Button5_Click" />
                                </div>
                                <div style="float: right;">
                                    <asp:Button ID="imgbtnSubmit" Text="Save" runat="server" OnClick="imgbtnSubmit_Click"
                                        Visible="False" CssClass="btnSubmit" />
                                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                        OnClick="Button1_Click" />
                                    <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        style="width: 51px;" type="button" value="Print" visible="false" class="btnSubmit" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel Width="100%" ID="Panel6" runat="server" Visible="False">
                                    <div>
                                        <asp:CheckBox ID="chkidcolumn" runat="server" Checked="true" />
                                        <label>
                                            <asp:Label ID="Label29" runat="server" Text="ID"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chktitlecolumn" runat="server" Checked="true" />
                                        <label>
                                            <asp:Label ID="Label30" runat="server" Text="Thumbnail"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkfileextsion" runat="server" Checked="true" />
                                        <label>
                                            <asp:Label ID="Label31" runat="server" Text="Upload Date"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkfoldername" runat="server" Checked="true" />
                                        <label>
                                            <asp:Label ID="Label32" runat="server" Text="Title"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkpartycolumn" runat="server" Checked="true" />
                                        <label>
                                            <asp:Label ID="Label15" runat="server" Text="Folder"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkdocumentdate" runat="server" Checked="true" />
                                        <label>
                                            <asp:Label ID="Label33" runat="server" Text="Party Name"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkrefno" runat="server" Checked="false" />
                                        <label>
                                            <asp:Label ID="Label41" runat="server" Text="Document Ref No."></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkdocamount" runat="server" Checked="false" />
                                        <label>
                                            <asp:Label ID="Label42" runat="server" Text="Document Amount"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkuploaddate" runat="server" Checked="true" />
                                        <label>
                                            <asp:Label ID="Label34" runat="server" Text="Document Date"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkmyfoldercolumn" runat="server" Checked="true" />
                                        <label>
                                            <asp:Label ID="Label22" runat="server" Text="Office Clerk Approval"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkaddtomyfoldercolumn" runat="server" Checked="true" />
                                        <label>
                                            <asp:Label ID="Label23" runat="server" Text="Supervisor Approval"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkaccountentrycolumn" runat="server" Checked="true" />
                                        <label>
                                            <asp:Label ID="Label25" runat="server" Text="Approval Status"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chksendmessagecolumn" runat="server" Checked="false" />
                                        <label>
                                            <asp:Label ID="Label26" runat="server" Text="Approval Note"></asp:Label>
                                        </label>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlupdatedoc" runat="server" Visible="false" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 35%">
                                                <label>
                                                    <asp:Label ID="Label11" runat="server" Text="Document ID"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 65%">
                                                <label>
                                                    <asp:Label ID="lbldocidmaster" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label2" runat="server" Text="Document Type"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlDocType" runat="server" ValidationGroup="1" Width="600px">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label8" runat="server" Text="Document Title"></asp:Label>
                                                    <asp:Label ID="Label9" runat="server" Text="*"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdoctitle"
                                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                        ControlToValidate="txtdoctitle" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="txtdoctitle" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'div1',30)"
                                                        runat="server" ValidationGroup="1" Width="250px" MaxLength="30" TabIndex="2"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label17" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                    <span id="div1" class="labelcount">30</span>
                                                    <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label10" runat="server" Text="User Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlpartyname" runat="server" ValidationGroup="1" Width="400px"
                                                        TabIndex="3">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label35" runat="server" Text="Document Ref. Number"></asp:Label>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                        ControlToValidate="txtdocrefnmbr" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="txtdocrefnmbr" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'sp11',100)"
                                                        MaxLength="100" ValidationGroup="1"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label36" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                    <span id="sp11" class="labelcount">100</span>
                                                    <asp:Label ID="Label37" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label38" runat="server" Text="Net Amount"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="txtnetamount" runat="server" MaxLength="10" onKeydown="return mak('Span1',10,this)"
                                                        onkeypress="return RealNumWithDecimal(this,event,2);" Width="180px" TabIndex="6"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label39" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                    <span id="Span2" class="labelcount">10</span>
                                                    <asp:Label ID="Label40" runat="server" CssClass="labelcount" Text="(0-9 .)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label28" runat="server" Text="Document Date"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TxtDocDate"
                                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rgfgfhjk" runat="server" ErrorMessage="*" ControlToValidate="TxtDocDate"
                                                        ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="TxtDocDate" runat="server" Width="70px"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="Button2" ValidationGroup="1" CssClass="btnSubmit" runat="server"
                                                    Text="Update" OnClick="Button2_Click" />
                                                <asp:Button ID="Button3" ValidationGroup="1" CssClass="btnSubmit" runat="server"
                                                    Text="Cancel" OnClick="Button3_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnl_grid_priceplan" runat="server" Width="100%">
                                    <table id="gridpanel" width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" Font-Italic="true" Visible="false" ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblbbbs" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="true" ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label7" runat="server" Text="List of Documents For Filing" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblA" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <table id="GridTbl" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel2" runat="server" Width="100%">
                                                                <asp:GridView ID="gridocapproval" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                    AlternatingRowStyle-CssClass="alt" AllowPaging="True" AllowSorting="True" DataKeyNames="DocumentProcessingId"
                                                                    AutoGenerateColumns="False" EmptyDataText="No Record Found." OnPageIndexChanging="gridocapproval_PageIndexChanging"
                                                                    PageSize="30" OnRowCommand="gridocapproval_RowCommand" OnSorting="gridocapproval_Sorting"
                                                                    Width="100%" OnRowDataBound="gridocapproval_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="DocumentId"
                                                                            HeaderText="ID" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <a id="docviewmasterid" href='ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>'
                                                                                    target="_blank">
                                                                                    <asp:Label ID="lbldocid" runat="server" ForeColor="#426172" Text='<%#Bind("DocumentId")%>'></asp:Label>
                                                                                </a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Thumbnail" ItemStyle-HorizontalAlign="Left"
                                                                            ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <asp:Image ID="Image2" Width="60px" Height="25px" runat="server" />
                                                                                <asp:LinkButton ID="LinkButton1" ForeColor="Blue" runat="server" OnClick="linkdow1_Click"
                                                                                    Text="View" Font-Size="8px"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="DocumentUploadDate" DataFormatString="{0:MM/dd/yyyy}"
                                                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Upload Date" ItemStyle-HorizontalAlign="Left"
                                                                            ItemStyle-Width="8%" SortExpression="DocumentUploadDate"></asp:BoundField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Title" ItemStyle-HorizontalAlign="Left"
                                                                            ItemStyle-Width="13%" SortExpression="DocumentTitle">
                                                                            <ItemTemplate>
                                                                               <%-- <a id="masterhr" href='DocumentEditAndView.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&amp;Did=<%= DesignationId %>'
                                                                                    target="_blank">--%>
                                                                                    <asp:Label ID="titillabel" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "DocumentTitle")%>'></asp:Label>
                                                                              <%--  </a>--%>
                                                                                <asp:Label ID="titlemaster" runat="server" ForeColor="#426172" Text='<%#DataBinder.Eval(Container.DataItem, "DocumentTitle")%>'
                                                                                    Visible="false"> </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="DocumentType" HeaderStyle-HorizontalAlign="Left" HeaderText="Folder"
                                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%" SortExpression="DocumentType">
                                                                        </asp:BoundField>
                                                                        <%-- <asp:BoundField DataField="PartyName" HeaderStyle-HorizontalAlign="Left" HeaderText="Party Name <br/>(User Name)"
                                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" SortExpression="PartyName">
                                                                        </asp:BoundField>--%>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Party Name <br/>(User Name)"
                                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" SortExpression="PartyName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblpartyname" runat="server" Text='<%# Eval("PartyName")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Document Ref No."
                                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%" SortExpression="DocumentRefNo">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldocrefno" runat="server" Text='<%# Eval("DocumentRefNo")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Document Amount"
                                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%" SortExpression="DocumentAmount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldocamount" runat="server" Text='<%# Eval("DocumentAmount")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Document Date" SortExpression="DocumentDate" HeaderStyle-HorizontalAlign="Left"
                                                                            ItemStyle-Width="8%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldocumentdate" runat="server" Text='<%# Eval("DocumentDate","{0:MM/dd/yyy}")%>'></asp:Label>
                                                                                <asp:Label ID="lbllevelofaccess" runat="server" Text='<%# Eval("Levelofaccess")%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblemployeeid" runat="server" Text='<%# Eval("EmployeeIDNew")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblstatusid" runat="server" Text='<%# Eval("StatusId")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblapprovalstatus" runat="server" Text='<%# Eval("Approve")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbldocumentid" runat="server" Text='<%# Eval("DocumentId")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblmasterid" runat="server" Text='<%# Eval("ProcessingId")%>' Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Office Clerk <br/> Approval" HeaderStyle-HorizontalAlign="Left"
                                                                            ItemStyle-Width="15%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblofficeclarkapproval" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Supervisor <br/> Approval" HeaderStyle-HorizontalAlign="Left"
                                                                            ItemStyle-Width="15%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsupervisorapproval" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approval Status"
                                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="rbtnAcceptReject" runat="server" Width="130px">
                                                                                    <asp:ListItem Selected="True" Value="0" Text="Pending-New"></asp:ListItem>
                                                                                    <asp:ListItem Value="1" Text="Pending-Returned"></asp:ListItem>
                                                                                    <asp:ListItem Value="2" Text="Rejected"></asp:ListItem>
                                                                                    <asp:ListItem Value="3" Text="Approved"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approval Note"
                                                                            ItemStyle-HorizontalAlign="Left" Visible="false" SortExpression="Note">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNote" runat="server" Height="30px" MaxLength="100" Text='<%#DataBinder.Eval(Container.DataItem, "Note")%>'
                                                                                    TextMode="MultiLine" Width="150px"></asp:TextBox>
                                                                                <asp:Label ID="lbltxtnote" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Note")%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:RegularExpressionValidator ID="REG1" runat="server" ControlToValidate="txtNote"
                                                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                                                    ValidationGroup="1">
                                                                                </asp:RegularExpressionValidator>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="EmployeeName" HeaderStyle-HorizontalAlign="Left" HeaderText="UploadedBy"
                                                                            ItemStyle-HorizontalAlign="Left" Visible="false" SortExpression="EmployeeName">
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("DocumentId") %>'
                                                                                    CommandName="Edit1" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                                                <asp:ImageButton ID="ImageButton2" Visible="false" runat="server" ImageUrl="~/Account/images/AD.png">
                                                                                </asp:ImageButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-HorizontalAlign="Left"
                                                                            HeaderText="Edit" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                                            <ItemTemplate>
                                                                                <%-- <a href="javascript:void(0)" onclick='window.open(FilingDeskViewApprove.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&amp;Eid=<%#DataBinder.Eval(Container.DataItem, "EmployeeId")%>&amp;Pid=<%#DataBinder.Eval(Container.DataItem, "ProcessingId")%>'>
                                                                                 --%>
                                                                                <a href="javascript:void(0)" onclick='window.open(&#039;FilingDeskViewApprove.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&amp;Eid=<%#DataBinder.Eval(Container.DataItem, "EmployeeIDNew")%>&amp;Pid=<%#DataBinder.Eval(Container.DataItem, "ProcessingId")%>&#039;, &#039;welcome&#039;,&#039;fullscreen=no,status=yes,top=0,left=0,menubar=yes,status=yes&#039;)'>
                                                                                    <asp:Image ID="Image1" runat="server" ToolTip="View/Edit" ImageUrl="~/Account/images/viewprofile.jpg" />
                                                                                </a>
                                                                                <asp:ImageButton ID="ImageButton3" Visible="false" runat="server" ImageUrl="~/Account/images/AD.png">
                                                                                </asp:ImageButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle CssClass="pgr" />
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                </asp:GridView>
                                                                <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                                                <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgbtncal"
                                                    TargetControlID="TxtDocDate">
                                                </cc1:CalendarExtender>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgbtncalfrom"
                                                    TargetControlID="txtfrom">
                                                </cc1:CalendarExtender>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgbtnto"
                                                    TargetControlID="txtto">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Panel ID="Panel9" runat="server" CssClass="modalPopup" Width="85%" Height="250px"
                                    ScrollBars="Both">
                                    <fieldset>
                                        <div align="right">
                                            <asp:ImageButton ID="ImageButton8" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                Width="15px" />
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <div>
                                            <%--<asp:Image ID="Image2DocView" AlternateText="No Image Found." runat="server" />--%>
                                            <asp:DataList ID="DataList1" runat="server">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Image ID="Image2DocView" runat="server" Height="565px" Width="100%" ImageUrl='<%#Eval("image")%>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="Button15" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender3" X="80" Y="25" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel9" TargetControlID="Button15" CancelControlID="ImageButton8">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
