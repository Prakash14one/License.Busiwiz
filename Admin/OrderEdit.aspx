<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master"   AutoEventWireup="true" CodeFile="OrderEdit.aspx.cs" Inherits="OrderEdit" Title="Untitled Page" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

    <table id="adminsubcontentbg" cellpadding="0" cellspacing="0">
        <tr>
            <td class="adminhdngbg">
                Order Setup Edit<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            <asp:Label id="lblmsg" runat="server" Visible="False" Width="100%" BackColor="#FF8080"></asp:Label></td>
        </tr>
        <tr>
            <td class="admintxtbg">
    <table id="adminorderformtbl" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2">
                <table id="adminorderformtbl" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="subhdng" colspan="2">
                            &nbsp;</td>
                        <td class="subhdng" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="subhdng" colspan="2">Company Information</td>
                        <td class="subhdng" colspan="2">Account Information</td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 50%">
                            Company Name :</td>
                        <td class="column2" style="width: 165px"><asp:Label ID="txtcompanyname" runat="server"></asp:Label></td>
                        <td class="column1" style="width: 114px">
                            Date :</td>
                        <td class="column2">
                            <asp:TextBox ID="txtdate" runat="server" ValidationGroup="1" ReadOnly="True"></asp:TextBox>
                            <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/images/calender.jpg" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtdate"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <cc1:CalendarExtender runat="server" ID="CalendarExtender1"  TargetControlID="txtdate" PopupButtonID="imgbtncal"  ></cc1:CalendarExtender></td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 50%">
                            Contact Person Name :</td>
                        <td class="column2" style="width: 165px">
                            <asp:TextBox ID="txtcontactprsn" runat="server" ValidationGroup="1" Width="133px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcontactprsn"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
                        <td class="column1" style="width: 114px">
                            Subscription Plan :</td>
                        <td class="column2">
                            <asp:DropDownList ID="ddlsubscriptionplan" runat="server" Enabled="False" Width="145px">
                                <asp:ListItem>--Select--</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 50%">
                            Contact Person Designation :</td>
                        <td class="column2" style="width: 165px">
                            <asp:TextBox ID="txtcontactprsndsg" runat="server" ValidationGroup="1" Width="133px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcontactprsndsg"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
                        <td class="column1" style="width: 114px">
                        </td>
                        <td class="column2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="subhdng">Address Information</td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 50%">
                            Address :</td>
                        <td class="column2" colspan="2">
                            <asp:TextBox ID="txtadd" runat="server" ValidationGroup="1" TextMode="MultiLine" Width="133px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtadd"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
                        <td class="column2">
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 50%">
                            Email :</td>
                        <td class="column2" colspan="2">
                            <asp:TextBox ID="txtemail" runat="server" ValidationGroup="1" Width="133px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtemail"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail"
                                ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="1"></asp:RegularExpressionValidator></td>
                        <td class="column2">
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 50%">
                            Phone :</td>
                        <td class="column2" style="width: 165px">
                            <asp:TextBox ID="txtphn"  onkeypress="return RealNumWithDecimal(this,event,2);" runat="server" ValidationGroup="1" Width="133px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtphn"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
                        <td class="col1" style="width: 114px">
                        </td>
                        <td class="column2">
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 50%">
                            Fax :</td>
                        <td class="column2" style="width: 165px">
                            <asp:TextBox ID="txtfax"  onkeypress="return RealNumWithDecimal(this,event,2);" runat="server" Width="133px"></asp:TextBox></td>
                        <td class="col1" style="width: 114px">
                        </td>
                        <td class="column2">
                        </td>
                    </tr>
                    <tr>
                        <td class="subhdng" colspan="4">Admin Login Information</td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 50%">
                            Company ID :</td>
                        <td class="column2" style="width: 165px">
                            <asp:Label ID="lblCompanyId" runat="server" Font-Bold="True"></asp:Label>
                                                                                        </td>
                        <td class="col1" style="width: 114px">
                        </td>
                        <td class="column2">
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 50%">
                            Admin User Id : </td>
                        <td class="column2" style="width: 165px">
                            <asp:Label ID="lblAUserId" runat="server" Font-Bold="True"></asp:Label>
                                                                                        </td>
                        <td class="col1" style="width: 114px"></td>
                        <td class="column2"></td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 50%">
                            Admin Password :</td>
                        <td class="column2" style="width: 165px">
                            <asp:Label ID="lblAPassword" runat="server" Font-Bold="True"></asp:Label>
                                                                                        </td>
                        <td class="col1" style="width: 114px"></td>
                        <td class="column2"></td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 50%">
                            Redirect URL :</td>
                        <td class="column2" colspan="2">
                            http://<asp:Label ID="lblRedirectUrl" runat="server" Font-Bold="True"></asp:Label>
                            .ifilecabinet.com </td>
                        <td class="column2">
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 50%">
                            Status:</td>
                        <td class="column2" style="width: 165px">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="True">Active</asp:ListItem>
                                <asp:ListItem Value="False">Deactive</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td class="col1" style="width: 114px">
                        </td>
                        <td class="column2">
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 50%">
                        </td>
                        <td class="column2" style="width: 165px">
                            <asp:Button ID="btnUpdate" runat="server"  Text="Update"
                                ValidationGroup="1" OnClick="btnUpdate_Click" />&nbsp;&nbsp;<asp:Button ID="btnreset" runat="server"
                                    Text="Reset" OnClick="btnreset_Click" /></td>
                        <td class="col1" style="width: 114px">
                        </td>
                        <td class="column2">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
            </td>
        </tr>
    </table>
</asp:Content>

