<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/LicenseMaster.master"
    CodeFile="DocumentViewAccount.aspx.cs" Inherits="DocumentViewAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
  
    var message="You may not right mouse click this page.";
      if (navigator.appName == 'Microsoft Internet Explorer')
        {
        function NOclickIE(e) 
        {
            if (event.button == 2 || event.button == 3) 
            {
                alert(message);
                return false;
            }
    return true;
    }
    document.onmousedown=NOclickIE;
    document.onmouseup=NOclickIE;
    window.onmousedown=NOclickIE;
    window.onmouseup=NOclickIE;
    }
    else {
    function NOclickNN(e){
    if (document.layers||document.getElementById&&!document.all){
    if (e.which==2||e.which==1){
    alert(message);
    return false;
    }}}
    if (document.layers){
    document.captureEvents(Event.MOUSEDOWN);
    document.onmousedown=NOclickNN; }
    document.oncontextmenu=new Function("alert(message);return false")
    }
 
 
 function temp()
            {
                
                //var abc = document.getElementById('txtAmount').value; 
                //alert("hello");  
            }
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
function pnlVisible()
{
alert("in");
var e = document.getElementById("ctl00_ContentPlaceHolder1_Panel1").style.visibility = "hidden";
var e = document.getElementById("ctl00_ContentPlaceHolder1_Panel2").style.visibility = "visible";


}
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Panel ID="pnlmsg" runat="server" >
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                </asp:Panel>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td>
                                <div style="float: right;">
                                    <label>
                                        <asp:Label ID="lbldcla" runat="server" Text="Document Title: "></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblddcname" runat="server" Text=""></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblnototal" runat="server"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="false" OnClick="LinkButton6_Click"></asp:LinkButton>
                                    </label>
                                    <label>
                                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" OnClick="LinkButton4_Click"></asp:LinkButton>
                                    </label>
                                </div>
                                <div style="clear: both;">
                                </div>
                                <div>
                                    <asp:Panel ID="lblim" runat="server" Height="520px" Width="100%" ScrollBars="Vertical">
                                        <asp:DataList ID="DataList1" runat="server">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Image ID="Image2" runat="server" ImageUrl='<%#Eval("image")%>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text=" Business Name"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Cabinet-Drawer-Folder"></asp:Label>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgbtncalfrom"
                                        TargetControlID="txtfrom">
                                    </cc1:CalendarExtender>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgbtnto"
                                        TargetControlID="txtto">
                                    </cc1:CalendarExtender>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddldoctype" runat="server" ValidationGroup="1" DataTextField="doctype"
                                        DataValueField="DocumentTypeId" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddldoctype_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <asp:CheckBox ID="CheckBox1" AutoPostBack="true" Checked="true" Text="Filter by Period"
                                    runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />
                               <label>
                               <asp:Label ID="Label3" runat="server" Text="Top Record"></asp:Label>
                               </label>
                               <label>
                                   <asp:DropDownList ID="DropDownList1" runat="server" Width="100px">
                                   
                                   <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                   <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                   <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                   <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                   <asp:ListItem Text="200" Value="200"></asp:ListItem>
                                   <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                   <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                   </asp:DropDownList>
                               </label>
                            </td>
                        </tr>
                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                            <tr>
                                <td colspan="6">
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="Filter by Period"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="RadioButtonList1" runat="server">
                                            <asp:ListItem Selected="True">Document Date</asp:ListItem>
                                            <asp:ListItem>Document Upload Date</asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label18" runat="server" Text="Start Date"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfrom"
                                            ErrorMessage="*" ValidationGroup="3" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </label>
                                    <label>
                                        <asp:TextBox ID="txtfrom" Width="100px" runat="server"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgbtncalfrom" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                    </label>
                                    <label>
                                        <asp:Label ID="Label19" runat="server" Text="End Date"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtto"
                                            ErrorMessage="*" ValidationGroup="3" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </label>
                                    <label>
                                        <asp:TextBox ID="txtto" Width="100px" runat="server"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgbtnto" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                    </label>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="List of documents for edit"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddllistofdoc" Width="500px" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddllistofdoc_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <input id="hdnDocId" name="HdnDocId" runat="server" type="hidden" />
                                </label>
                                 <label>
                                    <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Go" OnClick="Button2_Click" />
                                </label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <asp:Panel ID="pnlupdatedoc" runat="server" Width="100%">
                    <fieldset>
                        <table width="100%">
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label11" runat="server" Text="Document ID "></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lbldocidmaster" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label43" runat="server" Text="Upload Date/Time "></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lbldocuploaddate" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
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
                                        <asp:TextBox ID="TxtDocDate" runat="server" TabIndex="1" Width="75px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgbtncal"
                                            TargetControlID="TxtDocDate">
                                        </cc1:CalendarExtender>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label44" runat="server" Text="User Type"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlPartyType" TabIndex="2" AutoPostBack="true" runat="server"
                                            OnSelectedIndexChanged="ddlPartyType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label10" runat="server" Text="User Name"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlpartyname" TabIndex="3" runat="server" ValidationGroup="1">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgAdd2" runat="server" AlternateText="Add New" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                            Height="20px" ToolTip="Add New " Width="20px" ImageAlign="Bottom" OnClick="imgAdd2_Click" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgRefresh2" runat="server" AlternateText="Refresh" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                            Height="20px" Width="20px" ToolTip="Refresh" ImageAlign="Bottom" OnClick="imgRefresh2_Click" />
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label35" runat="server" Text="Ref No. "></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtdocrefnmbr" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtdocrefnmbr" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'sp11',100)"
                                            Width="100px" MaxLength="100" ValidationGroup="1"></asp:TextBox>
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
                                        <asp:Label ID="lbldoyou" runat="server" Text="Cabinet"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddldocmaintype" TabIndex="5" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddldocmaintype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgAdd" runat="server" AlternateText="Add New" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                            Height="20px" ToolTip="Add New " Width="20px" ImageAlign="Bottom" OnClick="imgAdd_Click" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgRefresh" runat="server" AlternateText="Refresh" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                            Height="20px" Width="20px" ToolTip="Refresh" ImageAlign="Bottom" OnClick="imgRefresh_Click" />
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label runat="server" ID="Label45" Text="Drawer-Folder "></asp:Label>
                                    </label>
                                </td>
                                <td colspan="3">
                                    <label>
                                        <asp:DropDownList ID="ddldoctypeup" TabIndex="7" Width="500px" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Add New" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                            Height="20px" ToolTip="Add New " Width="20px" ImageAlign="Bottom" OnClick="ImageButton1_Click" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton2" runat="server" AlternateText="Refresh" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                            Height="20px" Width="20px" ToolTip="Refresh" ImageAlign="Bottom" OnClick="ImageButton2_Click" />
                                    </label>
                                </td>
                            </tr>
                                <tr>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label61" runat="server" Text="Document Type"></asp:Label>
                                                                        <asp:Label ID="Labelxc" runat="server" Text="*"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RiredFievbalidator2" runat="server" ControlToValidate="ddldt" Display="Dynamic"
                                                                            ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddldt" runat="server" ValidationGroup="1" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddldt_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                    <label>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                    Width="20px" AlternateText="Add New" Height="20px" ToolTip="AddNew" 
                                                                        onclick="ImageButton3_Click"  />
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                                                    AlternateText="Refresh" Height="20px" Width="20px" ToolTip="Refresh" 
                                                                        onclick="ImageButton4_Click"  />
                                                            </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label62" runat="server" Text="Party Doc Ref.No"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequicmnredFieljgdValidator2" runat="server" Display="Dynamic"
                                                                            ControlToValidate="txtpartdocrefno" ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegulValidator2" runat="server" ErrorMessage="Invalid Character"
                                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                                            ControlToValidate="txtpartdocrefno" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                    </label>
                                                                </td>
                                                                <td colspan="2">
                                                                    <label>
                                                                        <asp:TextBox ID="txtpartdocrefno" runat="server" 
                                                                        onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span3',100)"
                                                                            MaxLength="100" ValidationGroup="1" Width="150px" TabIndex="5"></asp:TextBox>
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="Label63" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                        <span id="Span3" class="labelcount">100</span>
                                                                        <asp:Label ID="Label64" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
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
                                <td colspan="5">
                                    <label>
                                        <asp:TextBox ID="txtdoctitle" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'div1',150)"
                                            runat="server" ValidationGroup="1" Width="500px" MaxLength="150" TabIndex="8"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label17" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" class="labelcount">150</span>
                                        <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label38" runat="server" Text="Net Amount"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:TextBox ID="txtnetamount" runat="server" MaxLength="10" onKeydown="return mak('Span1',10,this)"
                                            onkeypress="return RealNumWithDecimal(this,event,2);" Width="100px" TabIndex="6"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label39" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span2" class="labelcount">10</span>
                                        <asp:Label ID="Label40" runat="server" CssClass="labelcount" Text="(0-9 .)"></asp:Label>
                                    </label>
                                   <%--<label>
                                        <asp:Button ID="btncon" runat="server" Text="Confirm " onclick="btncon_Click" 
                                         />
                                    </label>--%>
                                    <label>
                                        <asp:Button ID="Button1" runat="server" Text="Save " Visible="true"
                                        OnClick="Button1_Click" />
                                    </label>
                                  
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
                <table width="100px">
                    <tr>
                        <td class="text">
                            <asp:Panel ID="pnloa" runat="server" Width="645px" BorderColor="Black" BorderStyle="Outset"
                                Height="380px" BackColor="#CCCCCC">
                                <table id="Table6" cellpadding="0" cellspacing="5">
                                    <tr>
                                        <td class="hdr" colspan="2">
                                            Accounting Entries done for following document
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Account/images/closeicon.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="column1">
                                            DocId :<asp:Label ID="lbldid" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            DocTitle :<asp:Label ID="lbldtitle" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="column2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                           <%-- <asp:UpdatePanel ID="pvb" runat="server" UpdateMode="Always">
                                                <ContentTemplate>--%>
                                                    <asp:RadioButtonList ID="rdradio" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                        OnSelectedIndexChanged="rdradio_SelectedIndexChanged">
                                                        <asp:ListItem Value="0" Text="Make new entry"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Add to Existing Entry" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
<%--                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="rdradio" />
                                                </Triggers>
                                            </asp:UpdatePanel>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Panel ID="pvlnewentry" runat="server" Visible="false">
                                            <label>
                                                <asp:DropDownList ID="ddloa" runat="server" Width="200px">
                                                </asp:DropDownList>
                                                </label>
                                                <label>
                                                <asp:Button ID="ImageButton5" runat="server" Text=" Go " CssClass="btnSubmit" OnClick="ImageButton5_Click" />
                                                <asp:HyperLink ID="hypost" Visible="false" runat="server" Target="_blank" />
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Panel ID="pnlexist" runat="server" Visible="true">
                                            <label>
                                                <asp:DropDownList ID="ddldo" runat="server" Width="200px">
                                                    <asp:ListItem Text="Cash Register"></asp:ListItem>
                                                    <asp:ListItem Text="Journal Register"></asp:ListItem>
                                                    <asp:ListItem Text="Credit/Debit Note Register"></asp:ListItem>
                                                    <asp:ListItem Text="Packing Slip Register"></asp:ListItem>
                                                    <asp:ListItem Text="Purchase Register"></asp:ListItem>
                                                    <asp:ListItem Text="Sales Register"></asp:ListItem>
                                                    <asp:ListItem Text="Sales Order Register"></asp:ListItem>
                                                    <asp:ListItem Text="Expense Register"></asp:ListItem>
                                                </asp:DropDownList>
                                                 </label>
                                                <label>
                                                <asp:Button ID="img2" runat="server" OnClick="Img2_Click" Text=" Go " CssClass="btnSubmit" />
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            List of accounting entries done based on this document.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Panel ID="Panel5" runat="server" ScrollBars="Both" Height="150px" Width="630px">
                                                <asp:GridView ID="gridpopup" runat="server" CssClass="mGrid" AutoGenerateColumns="False"
                                                    OnSelectedIndexChanged="gridpopup_SelectedIndexChanged" Width="612px">
                                                    <Columns>
                                                      <asp:BoundField DataField="Datetime" HeaderStyle-HorizontalAlign="Left" HeaderText="Date" />
                                                    <asp:BoundField DataField="Entry_Type_Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Entry Type" />
                                                    <asp:BoundField DataField="EntryNumber" HeaderText="Entry Number" HeaderStyle-HorizontalAlign="Left" />
                                     
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="mdloa" BackgroundCssClass="modalBackground" PopupControlID="pnloa"
                                TargetControlID="Hidden1" CancelControlID="ImageButton6" runat="server">
                            </cc1:ModalPopupExtender>
                            <input id="Hidden1" name="Hidden1" runat="Server" type="hidden" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
