<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="BusinessProcessRules.aspx.cs" Inherits="Account_BusinessProcessRules"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
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

    <script language="javascript" type="text/javascript">

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

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div style="padding-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                <legend><asp:Label ID="lbllegend" runat="server" Text=""
                            Font-Bold="True" Visible="False"></asp:Label></legend>
                             <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add New Document Approval Rule" CssClass="btnSubmit" 
                                     onclick="btnadd_Click"  />
                    </div>
                    <div style="clear: both;">
                    </div>
                <asp:Panel ID="pnlsh" runat="server" Visible="false">
                    <table width="100%">
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <label>
                                                <asp:Label ID="lblconfirm" runat="server" Text="Do you wish to create a document approval rule for employees, or for external parties"></asp:Label>
                                            </label>
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                                AutoPostBack="True" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="True" Selected="True" Text="Employee">Employee</asp:ListItem>
                                                <asp:ListItem Value="False" Text="Party">Party</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            <label>
                                                <asp:Label ID="Label3" runat="server" Text="Business Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 80%">
                                            <label>
                                                <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                                                    Width="210px">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                <asp:Label ID="Label4" runat="server" Text="Approval Rule Type"></asp:Label>
                                                <asp:Label ID="Label5" runat="server" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlruletype"
                                                    ErrorMessage="*" InitialValue="0" ValidationGroup="11" Width="1px">
                                                </asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td style="width: 80%">
                                            <asp:UpdatePanel ID="Udccsd" runat="server">
                                                <ContentTemplate>
                                                    <label>
                                                        <asp:DropDownList ID="ddlruletype" runat="server" DataTextField="RuleType" DataValueField="RuleTypeId"
                                                            AutoPostBack="True" CausesValidation="True" OnSelectedIndexChanged="ddlruletype_SelectedIndexChanged"
                                                            Width="210px">
                                                        </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                        <asp:ImageButton ID="LinkButton1" runat="server" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                            Height="20px" Width="20px" OnClick="LinkButton1_Click" />
                                                    </label>
                                                    <label>
                                                        <asp:ImageButton ID="lnkadd0" runat="server" Height="20px" Width="20px" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                                            OnClick="lnkadd0_Click" />
                                                    </label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lnkadd0" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                <asp:Label ID="Label6" runat="server" Text=" Rule Date"></asp:Label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*"
                                                    ControlToValidate="txtruledate" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td style="width: 80%">
                                            <label>
                                                <asp:TextBox ID="txtruledate" runat="server"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:ImageButton ID="imgbtncal" runat="server" Visible="false" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                <asp:Label ID="Label7" runat="server" Text="Document Flow Rule Name"></asp:Label>
                                                <asp:Label ID="Label8" runat="server" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtrulename"
                                                    ErrorMessage="*" ValidationGroup="11" Width="1px"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                                    SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtrulename"
                                                    ValidationGroup="11"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td style="width: 80%">
                                            <label>
                                                <asp:TextBox ID="txtrulename" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',30)"
                                                    Width="250px" MaxLength="30"></asp:TextBox>
                                            </label>
                                            <label>
                                                Max <span id="div1">30</span>
                                                <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 _)"></asp:Label></label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label28" runat="server" Text="Status"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 80%">
                                            <label>
                                                <asp:DropDownList ID="ddlstatus" runat="server" Width="120px">
                                                    <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <label>
                                                <asp:Label ID="Label27" runat="server" Text=" Select the cabinet, drawer and folder to which this rule would apply"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;">
                                            <label>
                                                <asp:Label ID="Label9" runat="server" Text="Cabinet "></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 60%">
                                            <label>
                                                <asp:DropDownList ID="ddlcabinet" runat="server" Width="250px" AutoPostBack="True"
                                                    DataTextField="DocumentMainType" DataValueField="DocumentMainTypeId" OnSelectedIndexChanged="ddlcabinet_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;">
                                            <label>
                                                <asp:Label ID="Label10" runat="server" Text="Drawer "></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 80%">
                                            <label>
                                                <asp:DropDownList ID="ddldrower" runat="server" Width="250px" AutoPostBack="True"
                                                    DataTextField="DocumentSubType" DataValueField="DocumentSubTypeId" OnSelectedIndexChanged="ddldrower_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;">
                                            <label>
                                                <asp:Label ID="Label11" runat="server" Text="Folder "></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 80%">
                                            <label>
                                                <asp:DropDownList ID="ddltypeofdoc" runat="server" Width="250px" OnSelectedIndexChanged="ddltypeofdoc_SelectedIndexChanged"
                                                    DataTextField="DocumentType" DataValueField="DocumentTypeId">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <label>
                                                <asp:Label ID="Label1" runat="server" Text="Cabinet - Drawer - Folder" Visible="False"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr id="tr16" runat="server" visible="true">
                                        <td style="width: 20%">
                                            <label>
                                                <asp:Label ID="Label12" runat="server" Text="Flow Type "></asp:Label>
                                                <asp:Label ID="Label13" runat="server" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcondition1"
                                                    ErrorMessage="*" InitialValue="0" ValidationGroup="1" Width="1px"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td style="width: 80%">
                                            <label>
                                                <asp:DropDownList ID="ddlcondition1" runat="server" ValidationGroup="11" Width="250px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlcondition1_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">- Select - </asp:ListItem>
                                                    <asp:ListItem Value="1">Flow from person to person for approval (as set up below)</asp:ListItem>
                                                    <asp:ListItem Value="2" Selected="True">Flow simultaneously to all people for approval</asp:ListItem>
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr id="tr1" runat="server" visible="true">
                                        <td style="width: 20%" align="right">
                                        </td>
                                        <td style="width: 80%">
                                            <label>
                                                <asp:CheckBox ID="chkcheckapp" runat="server" Text="Would you like to send the documents for approval by email as well? " />
                                            </label>
                                            <label>
                                                <br />
                                                <%--<asp:Label ID="Label29" runat="server" Text="Would you like to send the documents for approval by email as well? "> </asp:Label>--%>
                                                <asp:Label ID="Label30" runat="server"> </asp:Label>
                                            </label>
                                            <label>
                                                (Please note, that the system will generate an email when documents have been received
                                                for approval. The user will not have to log in to the system to approve these documents
                                                if this option is selected. They will receive the approval by email).
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <label>
                                                <asp:Label ID="Label25" runat="server" Text="Send to all selected employees simultaneously for approval"
                                                    Visible="False"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtruledate"
                                                TargetControlID="txtruledate">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="tremp" runat="server" visible="true">
                            <td>
                                <fieldset>
                                    <legend>Add employee to the receiver list for document approval </legend>
                                    <asp:Panel ID="pnlstep1" runat="server" Width="100%" BackColor="#CCCCCC" BorderColor="#999999">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Image1" runat="server" Text="Approval #" />
                                                        <asp:Label ID="lblStepNo" runat="server"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label14" runat="server" Text="Business Name"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label15" runat="server" Text="Department - Designation"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label16" runat="server" Text="Employee Name"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label17" runat="server" Text="Approval Type"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlapprovetype1"
                                                            ErrorMessage="*" InitialValue="0" ValidationGroup="1" Width="1px"></asp:RequiredFieldValidator>
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:DropDownList ID="ddlbuemp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbuemp_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:DropDownList ID="ddldeg1" runat="server" AutoPostBack="True" DataTextField="DesignationName"
                                                            DataValueField="DesignationId" OnSelectedIndexChanged="ddldeg1_SelectedIndexChanged">
                                                            <asp:ListItem>Designation</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <label>
                                                                <asp:DropDownList ID="dlstusr1" runat="server" DataValueField="EmployeeId" DataTextField="EmployeeName">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddldeg1"></asp:AsyncPostBackTrigger>
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td>
                                                    <label>
                                                        send for
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:DropDownList ID="ddlapprovetype1" runat="server" CausesValidation="True" AutoPostBack="false"
                                                            OnSelectedIndexChanged="ddlapprovetype1_SelectedIndexChanged" Width="120px">
                                                            <asp:ListItem>Approve type</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        approved within
                                                        <asp:RangeValidator ID="RangeValidator1" Display="Dynamic" runat="server" ErrorMessage="only 1 to 365"
                                                            ControlToValidate="txt1" MaximumValue="365" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt1"
                                                            ErrorMessage="*" ValidationGroup="1" Width="1px"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt1"
                                                            ErrorMessage="*" ValidationGroup="11" Width="1px"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="txt1" onkeypress="return RealNumWithDecimal(this,event,2);" runat="server"
                                                            Width="30px"></asp:TextBox>
                                                    </label>
                                                    <label>
                                                        days
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" ValidationGroup="1"
                                                        Text="View Details" OnClick="Button2_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="7" align="center">
                                                    <asp:Button ID="ibtnstep1" runat="server" Text="Add Employee" OnClick="ibtnstep1_Click"
                                                        ValidationGroup="1" CssClass="btnSubmit" />
                                                    <asp:TextBox ID="txtdes1" runat="server" onkeypress="return RealNumWithDecimal(this,event,2);"
                                                        TextMode="MultiLine" Visible="False" MaxLength="500"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </fieldset>
                            </td>
                        </tr>
                        <tr id="trparty" runat="server" visible="false">
                            <td>
                                <asp:Panel ID="Panelparty" runat="server" Width="100%" BackColor="#CCCCCC" BorderColor="#666666">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Image2" runat="server" Text="Approval # " />
                                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label18" runat="server" Text="Business Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label19" runat="server" Text="Party Type"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlpartytype"
                                                        ErrorMessage="*" InitialValue="0" ValidationGroup="1" Width="1px">
                                                    </asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label20" runat="server" Text="Party Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label21" runat="server" Text=" Approval Type"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlapprovetype1"
                                                        ErrorMessage="*" InitialValue="0" ValidationGroup="1" Width="1px"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*"
                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([+a-zA-Z0-9_\s\-\.]*)"
                                                        ControlToValidate="txtdesc2" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlbusparty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusparty_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlpartytype" runat="server" AutoPostBack="True" DataTextField="PartyTypeName"
                                                        DataValueField="PartyTypeId" OnSelectedIndexChanged="ddlpartytype_SelectedIndexChanged">
                                                        <asp:ListItem>Designation</asp:ListItem>
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlpartyname" runat="server" DataValueField="PartyId" DataTextField="PartyName">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddldeg1"></asp:AsyncPostBackTrigger>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <label>
                                                    for
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlaprvtype" runat="server" ValidationGroup="312" CausesValidation="True"
                                                        AutoPostBack="false" OnSelectedIndexChanged="ddlaprvtype_SelectedIndexChanged"
                                                        Width="120px">
                                                        <asp:ListItem>Approve type</asp:ListItem>
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    approved within
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="TextBox1" onkeypress="return RealNumWithDecimal(this,event,2);"
                                                        Text="1" runat="server" Width="30px" MaxLength="10"></asp:TextBox>
                                                    <asp:RangeValidator ID="RangeValidator2" Display="Dynamic" runat="server" ErrorMessage="only 1 to 365"
                                                        ControlToValidate="TextBox1" MaximumValue="365" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                                </label>
                                                <label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt1"
                                                        ErrorMessage="*" ValidationGroup="11" Width="1px"></asp:RequiredFieldValidator>
                                                    days
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt1"
                                                        ErrorMessage="*" ValidationGroup="1" Width="1px"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td colspan="2">
                                                <asp:Button ID="Button6" runat="server" CssClass="btnSubmit" ValidationGroup="312"
                                                    Text="View Details" OnClick="Button6_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" align="center">
                                                <asp:Button ID="imgbtnsubmit" runat="server" Text="Confirm" ValidationGroup="1" OnClick="imgbtnsubmit_Click"
                                                    CssClass="btnSubmit" />
                                                <asp:TextBox ID="txtdesc2" Enabled="false" runat="server" Height="57px" onkeypress="return RealNumWithDecimal(this,event,2);"
                                                    TextMode="MultiLine" Visible="False" Width="200px" MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="GridTbl" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <fieldset>
                                                <legend>
                                                    <asp:Label ID="Label26" runat="server" Text="Document Flow Type :"></asp:Label>
                                                    <asp:Label ID="lblflowtypelabel" runat="server" Text=""></asp:Label>
                                                </legend>
                                                <asp:Panel ID="Panel2" runat="server" Width="100%" Height="110px" ScrollBars="Vertical">
                                                    <asp:GridView ID="gridRuleDetail" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                                                        CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        Width="100%" OnRowCommand="gridRuleDetail_RowCommand" PageIndex="4">
                                                        <Columns>
                                                            <asp:BoundField DataField="StepNo" HeaderStyle-HorizontalAlign="Left" HeaderText="Approval No." />
                                                            <asp:TemplateField HeaderText="Rule Detail" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblRuleDetail" Text='<%#DataBinder.Eval(Container.DataItem, "RuleDetail")%>'>                                                 
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="DesId" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                <ControlStyle Width="0px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="EmpId" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                <ControlStyle Width="0px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ApprovedTypeId" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                <ControlStyle Width="0px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Days" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                <ControlStyle Width="0px" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left"
                                                                SortExpression="BusinessName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwname" runat="server" Text='<%# Eval("BusinessName")%>'></asp:Label>
                                                                    <asp:Label ID="lblwhid" runat="server" Text='<%# Eval("Whid")%>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:ButtonField CommandName="Edit1" HeaderStyle-HorizontalAlign="Left" HeaderText="Edit"
                                                                        ImageUrl="~/Account/images/edit.gif" Text="Edit"></asp:ButtonField>--%>
                                                            <asp:TemplateField ItemStyle-Width="3%" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderImageUrl="~/Account/images/edit.gif">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandName="Edit1"
                                                                        ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:ButtonField CommandName="Delete1" ButtonType="Image" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                                ImageUrl="~/Account/images/delete.gif" Text="Delete" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div style="float: none; text-align: center">
                        <asp:Button ID="ibtnadd1" runat="server" Text="Confirm" ValidationGroup="11" OnClick="ibtnadd1_Click"
                            CssClass="btnSubmit" />
                        <asp:Button ID="ibtnReset" runat="server" Text="Cancel" OnClick="ibtnReset_Click"
                            CausesValidation="False" CssClass="btnSubmit" />
                        <input id="hdnRuleMaster" runat="server" name="hdnRuleMaster" type="hidden" style="width: 1px" />
                        <input id="hdnRuleDetail" runat="server" name="hdnAddress" style="width: 1px" type="hidden" />
                        <input id="hdnStepNo" runat="server" name="hdnStepNo" type="hidden" style="width: 1px" />
                    </div>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblhead" runat="server" Font-Bold="True" Font-Size="13px" Text="List of Document Flow Rules"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <div style="float: right;">
                                <label>
                                    <asp:Button ID="btnsave" runat="server" Text="Edit Status" CssClass="btnSubmit" onclick="btnsave_Click"
                                         />
                                        </label>
                                <label>
                                    <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                                        OnClick="Button1_Click" />
                                        </label>
                                        <label>
                                    <input type="button" value="Print" id="Button3" runat="server" class="btnSubmit"
                                        onclick="javascript:CallPrint('divPrint')" visible="false" />
                                        </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <label>
                                    <asp:Label ID="Label22" runat="server" Text="Filter By Business Name"></asp:Label>
                                </label>
                            </td>
                            <td width="80%">
                                <label>
                                    <asp:DropDownList ID="ddlbusbyfilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusbyfilter_SelectedIndexChanged"
                                        Width="300px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <label>
                                    <asp:Label ID="Label23" runat="server" Text="Filter By"></asp:Label>
                                </label>
                            </td>
                            <td width="80%">
                                <label>
                                    <asp:RadioButtonList ID="rdfilterpartyemployee" runat="server" AutoPostBack="True"
                                        RepeatDirection="Horizontal" OnSelectedIndexChanged="rdfilterpartyemployee_SelectedIndexChanged">
                                        <asp:ListItem Value="True" Selected="True" Text="Employee">Employee</asp:ListItem>
                                        <asp:ListItem Value="False" Text="Party">Party</asp:ListItem>
                                    </asp:RadioButtonList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%" ScrollBars="Vertical">
                                    <table id="Table4" width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblcomid" runat="server" Font-Bold="True" Font-Italic="true" Font-Size="20px"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="Label24" runat="server" Text="Business:" Font-Bold="True" Font-Italic="true"
                                                                    Font-Size="18px"></asp:Label>
                                                                <asp:Label ID="lblcomname" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="18px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblheaa" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="18px"
                                                                    Text="List of Document Flow Rules"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlemp" runat="server" Width="100%" ScrollBars="Vertical">
                                                    <asp:GridView ID="grid_Rule_master" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        DataKeyNames="RuleId" EmptyDataText="No Record Found." OnPageIndexChanging="grid_Rule_master_PageIndexChanging"
                                                        OnRowCommand="grid_Rule_master_RowCommand" AllowSorting="True" OnSorting="grid_Rule_master_Sorting"
                                                        Visible="true" OnRowDeleting="grid_Rule_master_RowDeleting" OnRowEditing="grid_Rule_master_RowEditing"
                                                        OnRowUpdating="grid_Rule_master_RowUpdating" OnRowCancelingEdit="grid_Rule_master_RowCancelingEdit"
                                                        Width="100%" PageSize="15">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Business Name" ItemStyle-HorizontalAlign="Left" SortExpression="Name"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlwname" DataTextField="Name" DataValueField="Whid" Width="90px"
                                                                        AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlwname_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwname" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                                    <asp:Label ID="lblwhid" runat="server" Text='<%# Eval("Whid")%>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rule Name" ItemStyle-HorizontalAlign="Left" SortExpression="RuleTitle"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrulename" runat="server" Text='<%# Eval("RuleTitle")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtrulename" runat="server" Text='<%# Eval("RuleTitle")%>' Width="80px">
                                                                    </asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtrulename"
                                                                        SetFocusOnError="true" runat="server" ErrorMessage="Not Accepted" ValidationGroup="qq"
                                                                        Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                                        ControlToValidate="txtrulename" ValidationGroup="qq"></asp:RegularExpressionValidator>
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rule Date" ItemStyle-HorizontalAlign="Left" SortExpression="RuleDate"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblruledate" runat="server" Text='<%# Eval("RuleDate","{0:MM/dd/yyyy-HH:mm}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtruledate" runat="server" Text='<%# Eval("RuleDate")%>' Width="60">
                                                                    </asp:TextBox>
                                                                    <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg"
                                                                        Visible="false" />
                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgbtncal"
                                                                        TargetControlID="txtruledate">
                                                                    </cc1:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtruledate"
                                                                        SetFocusOnError="true" runat="server" ErrorMessage="Not Accepted" ValidationGroup="qq"
                                                                        Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rule Type Name" ItemStyle-HorizontalAlign="Left" SortExpression="RuleType"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlruletype" DataTextField="RuleType" DataValueField="RuleTypeId"
                                                                        runat="server" Width="80px">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblruletype" runat="server" Text='<%# Eval("RuleType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cabinet" ItemStyle-HorizontalAlign="Left" SortExpression="DocumentMainType"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlmaindoc" DataTextField="DocumentMainType" DataValueField="DocumentMainId"
                                                                        Width="90px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmaindoc_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmain" runat="server" Text='<%# Eval("DocumentMainType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Drawer" ItemStyle-HorizontalAlign="Left" SortExpression="DocumentSubType"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlsubdoc" DataTextField="DocumentSubType" DataValueField="DocumentSubId"
                                                                        runat="server" Width="90px" AutoPostBack="true" OnSelectedIndexChanged="ddlsubdoc_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsub" runat="server" Text='<%# Eval("DocumentSubType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Folder" ItemStyle-HorizontalAlign="Left" SortExpression="DocumentType"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlDocType" DataTextField="DocumentType" DataValueField="DocumentTypeId"
                                                                        Width="80px" runat="server">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDocType" runat="server" Text='<%# Eval("DocumentType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Flow Type" ItemStyle-HorizontalAlign="Left" SortExpression="ConditionTypeName"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlcondition1" runat="server" ValidationGroup="11" Width="70px">
                                                                        <asp:ListItem Value="0">- Select - </asp:ListItem>
                                                                        <asp:ListItem Value="1">Step Wise in Serial Order</asp:ListItem>
                                                                        <asp:ListItem Value="2">Simultaneous to All</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblConditionTypeName" runat="server" Text='<%# Eval("ConditionTypeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="By Email Approval" ItemStyle-HorizontalAlign="Left"
                                                                SortExpression="Approvemail" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="1%">
                                                                <EditItemTemplate>
                                                                    <asp:CheckBox ID="chkapp" runat="server" Checked='<%# Eval("Approvemail")%>' />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkapp1" runat="server" Visible="false" Enabled="false" Checked='<%# Eval("Approvemail")%>' />
                                                                    <asp:Label ID="lblapprovalmaildetail" runat="server" Text='<%# Eval("ApprovalMaillabel")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="1%" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Status" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                <asp:Label ID="lblstat" runat="server" Text='<%# Eval("Statuslabel")%>'></asp:Label>
                                                                   <asp:CheckBox ID="chksta" runat="server" Visible="false" />
                                                                </ItemTemplate>
                                                                  <HeaderStyle HorizontalAlign="Left" />
                                                                  <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:ButtonField CommandName="detail" ItemStyle-HorizontalAlign="Left" HeaderText="Show Detail"
                                                                Text="Show" ItemStyle-ForeColor="#426172" 
                                                                HeaderStyle-HorizontalAlign="Left" >
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle ForeColor="#426172" HorizontalAlign="Left" />
                                                            </asp:ButtonField>
                                                            <asp:CommandField Visible="false" ButtonType="Image" ShowEditButton="True" ItemStyle-HorizontalAlign="Left"
                                                                ShowHeader="True" HeaderStyle-HorizontalAlign="Left" HeaderText="Edit" ValidationGroup="qq"
                                                                EditText="Edit" HeaderImageUrl="~/Account/images/edit.gif" EditImageUrl="~/Account/images/edit.gif"
                                                                UpdateImageUrl="~/Account/images/UpdateGrid.JPG" CancelImageUrl="~/images/delete.gif"
                                                                CancelText="CANCEL" UpdateText="UPDATE">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:CommandField>
                                             
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton1123" runat="server" ToolTip="Delete" CommandArgument='<%# Eval("RuleId") %>'
                                                                        CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:GridView ID="grdRuleDetail" runat="server" AutoGenerateColumns="False" GridLines="None">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="StepId" ItemStyle-HorizontalAlign="Left" HeaderText="Approval No." />
                                                                            <%--<asp:BoundField DataField="RuleDetail" HeaderText="RuleDetail" />--%>
                                                                            <asp:TemplateField HeaderText="Rule Detail" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblRuleDetail1" Text='<%#DataBinder.Eval(Container.DataItem, "RuleDetail")%>'>
                                     
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlparty" runat="server" Width="100%">
                                                    <asp:GridView ID="grid_Rule_master_party" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        DataKeyNames="RuleId" EmptyDataText="No Record Found." CssClass="mGrid" GridLines="Both"
                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True"
                                                        OnPageIndexChanging="grid_Rule_master_party_PageIndexChanging" OnRowCommand="grid_Rule_master_party_RowCommand"
                                                        OnSorting="grid_Rule_master_party_Sorting" Visible="false" OnRowDeleting="grid_Rule_master_party_RowDeleting"
                                                        OnRowEditing="grid_Rule_master_party_RowEditing" OnRowUpdating="grid_Rule_master_party_RowUpdating"
                                                        OnRowCancelingEdit="grid_Rule_master_party_RowCancelingEdit" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Business Name" ItemStyle-HorizontalAlign="Left" SortExpression="Name"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlwname" DataTextField="Name" DataValueField="Whid" AutoPostBack="true"
                                                                        runat="server" OnSelectedIndexChanged="ddlwname_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwname" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                                    <asp:Label ID="lblwhid" runat="server" Text='<%# Eval("Whid")%>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rule Name" ItemStyle-HorizontalAlign="Left" SortExpression="RuleTitle"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrulename" runat="server" Text='<%# Eval("RuleTitle")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtrulename" runat="server" Text='<%# Eval("RuleTitle")%>' Width="100">
                                                                    </asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtrulename"
                                                                        SetFocusOnError="true" runat="server" ErrorMessage="Not Accepted" ValidationGroup="qq"
                                                                        Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                                        ControlToValidate="txtrulename" ValidationGroup="qq"></asp:RegularExpressionValidator>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rule Date" ItemStyle-HorizontalAlign="Left" SortExpression="RuleDate"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblruledate" runat="server" Text='<%# Eval("RuleDate","{0:MM/dd/yyyy-HH:mm}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtruledate" runat="server" Text='<%# Eval("RuleDate")%>' Width="60px">
                                                                    </asp:TextBox>
                                                                    <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg"
                                                                        Visible="false" />
                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgbtncal"
                                                                        TargetControlID="txtruledate">
                                                                    </cc1:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtruledate"
                                                                        SetFocusOnError="true" runat="server" ErrorMessage="Not Accepted" ValidationGroup="qq"
                                                                        Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rule Type Name" ItemStyle-HorizontalAlign="Left" SortExpression="RuleType"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlruletype" DataTextField="RuleType" DataValueField="RuleTypeId"
                                                                        runat="server">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblruletype" runat="server" Text='<%# Eval("RuleType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cabinet" ItemStyle-HorizontalAlign="Left" SortExpression="DocumentMainType"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlmaindoc" DataTextField="DocumentMainType" DataValueField="DocumentMainId"
                                                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmaindoc_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmain" runat="server" Text='<%# Eval("DocumentMainType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Drawer" ItemStyle-HorizontalAlign="Left" SortExpression="DocumentSubType"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlsubdoc" DataTextField="DocumentSubType" DataValueField="DocumentSubId"
                                                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlsubdoc_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsub" runat="server" Text='<%# Eval("DocumentSubType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Folder" ItemStyle-HorizontalAlign="Left" SortExpression="DocumentType"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlDocType" DataTextField="DocumentType" DataValueField="DocumentTypeId"
                                                                        runat="server">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDocType" runat="server" Text='<%# Eval("DocumentType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Flow Type" ItemStyle-HorizontalAlign="Left" SortExpression="ConditionTypeName"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlcondition1" runat="server" ValidationGroup="11">
                                                                        <asp:ListItem Value="0">- Select - </asp:ListItem>
                                                                        <asp:ListItem Value="1">Step Wise in Serial Order</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblConditionTypeName" runat="server" Text='<%# Eval("ConditionTypeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:ButtonField CommandName="detail1" ItemStyle-HorizontalAlign="Left" HeaderText="Show Detail"
                                                                HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#426172"
                                                                Text="Show">
                                                                <HeaderStyle Width="50px" />
                                                            </asp:ButtonField>
                                                        <asp:CommandField ButtonType="Image" Visible="false" ItemStyle-HorizontalAlign="Left"
                                                                ShowEditButton="True" ShowHeader="True" HeaderStyle-HorizontalAlign="Left" HeaderText="Edit"
                                                                ValidationGroup="qq" EditText="Edit" HeaderImageUrl="~/Account/images/edit.gif"
                                                                EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/UpdateGrid.JPG"
                                                                CancelImageUrl="~/images/delete.gif" CancelText="CANCEL" UpdateText="UPDATE"
                                                                HeaderStyle-Width="50px">
                                                                <HeaderStyle Width="50px" />
                                                            </asp:CommandField>
                                                         
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton1123" runat="server" ToolTip="Delete" CommandArgument='<%# Eval("RuleId") %>'
                                                                        CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:GridView ID="grdRuleDetailParty" runat="server" AutoGenerateColumns="False"
                                                                        GridLines="None">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="StepId" ItemStyle-HorizontalAlign="Left" HeaderText="Approval No." />
                                                                            <%--<asp:BoundField DataField="RuleDetail" HeaderText="RuleDetail" />--%>
                                                                            <asp:TemplateField HeaderText="Rule Detail" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblRuleDetail1" Text='<%#DataBinder.Eval(Container.DataItem, "RuleDetail")%>'>                                                         
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle BackColor="Goldenrod" Font-Bold="True" ForeColor="White" />
                                                                    </asp:GridView>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlconfirmmsg" runat="server" BorderStyle="Outset" CssClass="GridPnl"
                                    Height="100px" Width="300px" BackColor="#CCCCCC" BorderColor="#666666">
                                    <table id="Table1">
                                        <tr>
                                            <td class="secondtblfc2">
                                                <table id="Table2" cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td class="secondtblfc1">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="pnlconfirmmsgub" runat="server" Width="100%" Height="75px">
                                                                    <table id="Table3" cellspacing="0" cellpadding="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td colspan="2" style="font-weight: bold; padding-left: 10px; font-size: 12px; font-family: Arial;
                                                                                    text-align: left; vertical-align: top;">
                                                                                    Are you sure, You want to Delete a Record?
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Button ID="imgconfirmok" runat="server" Text=" Ok " CausesValidation="False"
                                                                                        OnClick="imgconfirmok_Click" BackColor="#CCCCCC" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="imgconfirmcalcel" runat="server" Text="Close" CausesValidation="False"
                                                                                        OnClick="imgconfirmcalcel_Click" BackColor="#CCCCCC" />
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
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
                    <table>
                        <tr>
                            <td align="left">
                                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Width="600px">
                                    <fieldset>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <div style="float: right;">
                                                <label>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" />
                                                </label>
                                            </div>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Approval type description :
                                                        <asp:Label ID="lblapprovetypedescription" runat="server" Text="Label"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </asp:Panel>
                                <asp:Button ID="Button4" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel1" TargetControlID="Button4" CancelControlID="ImageButton2">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td align="left">
                                <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" Width="600px">
                                    <fieldset>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <div style="float: right;">
                                                <label>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" />
                                                </label>
                                            </div>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Approval type description :
                                                        <asp:Label ID="Label31" runat="server" Text="Label"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </asp:Panel>
                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel3" TargetControlID="Button5" CancelControlID="ImageButton1">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
