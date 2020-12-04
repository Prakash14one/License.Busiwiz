<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="Incidence_Report.aspx.cs" Inherits="ShoppingCart_Admin_Incidence_Report" %>

<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
        
        function RealNumWithDecimal(myfield, e, dec) {

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
            if (key == 13) {
                return false;
            }
            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 27)) {
                return true;
            }
            else if ((("0123456789.").indexOf(keychar) > -1)) {
                return true;
            }
            // decimal point jump
            else if (dec && (keychar == ".")) {

                myfield.form.elements[dec].focus();
                myfield.value = "";

                return false;
            }
            else {
                myfield.value = "";
                return false;
            }
        }
        
         function checktextboxmaxlength(txt, maxLen,evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
        
         function mask(evt)
        { 
         
           if(evt.keyCode==13 )
            { 
         
                  
             }
            
           
            if(evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered invalid character");
                  return false;
             }     
            
        }   
         function check(txt1, regex, reg,id,max_len)
            {
             if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
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

    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllegend" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add Incident" CssClass="btnSubmit" OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Pnladdnew" runat="server" Visible="false">
                        <table cellpadding="0" cellspacing="3" style="width: 100%">
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Panel8" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                    <label>
                                                        <asp:Label ID="lblwname" runat="server" Text="Business Name"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                    <label>
                                                        <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstore_SelectedIndexChanged"
                                                            Width="250px">
                                                        </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                        <asp:ImageButton ID="imgadddept" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                            ToolTip="AddNew" Visible="False" Width="20px" ImageAlign="Bottom" />
                                                        <asp:ImageButton ID="imgdeptrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                            ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Visible="False"
                                                            Width="20px" ImageAlign="Bottom" />
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Panel2" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                    <label>
                                                        <asp:Label ID="Label1" runat="server" Text="Incident No."></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:Label ID="lblincidence" runat="server" Text="1"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlemployee" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                    <label>
                                                        <asp:Label ID="Label2" runat="server" Text="Employee Name"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:DropDownList ID="ddlemployee" runat="server" AutoPostBack="True" Width="250px">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Panel3" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                    <label>
                                                        <asp:Label ID="Label3" runat="server" Text="Date"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:TextBox ID="txteenddate" runat="server" Width="70px"></asp:TextBox>
                                                    </label>
                                                    <cc1:CalendarExtender runat="server" ID="cal2" TargetControlID="txteenddate" PopupButtonID="ImageButton1">
                                                    </cc1:CalendarExtender>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txteenddate">
                                                    </cc1:MaskedEditExtender>
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                    <label>
                                                        <asp:Label ID="Label4" runat="server" Text="Time"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:TextBox ID="txttime" runat="server" Width="50px"></asp:TextBox>
                                                    </label>
                                                    <label>
                                                        <asp:DropDownList ID="ddltime" runat="server" Width="180px">
                                                            <asp:ListItem Value="0" Text="AM"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="PM"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Panel16" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                    <label>
                                                        <asp:Label ID="Label5" runat="server" Text="Select For"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                                            AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                            <asp:ListItem Text="Related to Policy" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Related to Procedure" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Related to Rule" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="None" Value="0" Selected="True"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlpolicy" runat="server" Width="100%" Visible="False">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                    <label>
                                                        <asp:Label ID="Label6" runat="server" Text="Policy Name"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:DropDownList ID="ddlpolicytitle" runat="server">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Pnlprocedure" runat="server" Width="100%" Visible="False">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                    <label>
                                                        <asp:Label ID="Label7" runat="server" Text="Procedure Name"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:DropDownList ID="ddlproceduretitle" runat="server">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Pnlrule" runat="server" Width="100%" Visible="False">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                    <label>
                                                        <asp:Label ID="Label8" runat="server" Text="Rule Name"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:DropDownList ID="ddlruletitle" runat="server">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Panel9" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                    <label>
                                                        <asp:Label ID="Label9" runat="server" Text="Incident Note"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <br />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                            SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)" ControlToValidate="txtdescription"
                                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtdescription"
                                                            SetFocusOnError="True" ErrorMessage="Please enter maximum 1500 chars" ValidationExpression="^([\S\s]{0,1500})$"
                                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                                    </label>
                                                </td>
                                                <td style="width: 70%" valign="top">
                                                    <label>
                                                        <asp:TextBox ID="txtdescription" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div2',1500)"
                                                            runat="server" Height="60px" onkeypress="return checktextboxmaxlength(this,1500,event)"
                                                            Width="360px" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:Label runat="server" ID="Label21" Text="Max " CssClass="labelcount"></asp:Label>
                                                        <span id="div2" cssclass="labelcount">1500</span>
                                                        <asp:Label ID="Label27" CssClass="labelcount" runat="server" Text="(A-Z 0-9 . , ? _)"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                </td>
                                                <td style="width: 70%">
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click"
                                                        ValidationGroup="1" CssClass="btnSubmit" />
                                                    <asp:Button ID="btnreset" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnreset_Click"
                                                        CssClass="btnSubmit" />
                                                    <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click"
                                                        ValidationGroup="1" CssClass="btnSubmit" Visible="False" />
                                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CausesValidation="false"
                                                        OnClick="btncancel_Click" CssClass="btnSubmit" Style="height: 26px" Visible="False" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label10" runat="server" Text="List of Incident Report"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="btncancel0" runat="server" CssClass="btnSubmit" CausesValidation="false"
                                    OnClick="btncancel0_Click" Text="Printable Version" />
                                <input id="Button7" runat="server" visible="false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    class="btnSubmit" style="width: 51px;" type="button" value="Print" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel6" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 20%; text-align: right">
                                                <label>
                                                    <asp:Label ID="Label12" runat="server" Text="Business Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 30%;">
                                                <label>
                                                    <asp:DropDownList Width="210px" ID="ddlsearchByStore" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlsearchByStore_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td style="width: 20%; text-align: right">
                                                <label>
                                                    <asp:Label ID="Label13" runat="server" Text="Employee Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 30%;">
                                                <label>
                                                    <asp:DropDownList Width="210px" ID="ddlfilteremployee" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlfilteremployee_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel15" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" style="width: 20%;" valign="top" rowspan="4">
                                                <label>
                                                    <asp:Label ID="Label14" runat="server" Text="Filter by"></asp:Label>
                                                </label>
                                            </td>
                                            <td align="left" style="width: 30%;" valign="top" rowspan="4">
                                                <label>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" Width="210px"
                                                        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                        <asp:ListItem Text="Policy" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Procedure" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Rules" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="None" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="Pnlfilterpolicy" runat="server" Width="100%" Visible="False">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right" style="width: 22%" valign="top">
                                                                <label>
                                                                    <asp:Label ID="Label15" runat="server" Text="Related to Policy"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td align="left" style="width: 30%" valign="top">
                                                                <label>
                                                                    <asp:DropDownList ID="ddlfilterpolicy" runat="server" AutoPostBack="true" Width="210px"
                                                                        OnSelectedIndexChanged="ddlfilterpolicy_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="Pnlfilterprocedure" runat="server" Width="100%" Visible="False">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right" style="width: 22%" valign="top">
                                                                <label>
                                                                    <asp:Label ID="Label16" runat="server" Text="Related to Procedure"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td align="left" style="width: 30%" valign="top">
                                                                <label>
                                                                    <asp:DropDownList ID="ddlfilterprocedure" runat="server" AutoPostBack="true" Width="210px"
                                                                        OnSelectedIndexChanged="ddlfilterprocedure_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="Pnlfilterrule" runat="server" Width="100%" Visible="False">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right" style="width: 22%" valign="top">
                                                                <label>
                                                                    <asp:Label ID="Label17" runat="server" Text="Related to Rules"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td align="left" style="width: 30%" valign="top">
                                                                <label>
                                                                    <asp:DropDownList ID="ddlfilterrule" runat="server" AutoPostBack="true" Width="210px"
                                                                        OnSelectedIndexChanged="ddlfilterrule_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel12" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" style="width: 20%">
                                                <label>
                                                    <asp:Label ID="Label23" runat="server" Text="Department Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td align="left" style="width: 30%">
                                                <label>
                                                    <asp:DropDownList ID="ddlfilterdepartment" runat="server" Width="210px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlfilterdepartment_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td align="right" style="width: 20%">
                                                <label>
                                                    <asp:Label ID="Label24" runat="server" Text="From"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txtfromdt" runat="server" Width="70px" ValidationGroup="1" AutoPostBack="True"></asp:TextBox>
                                                </label>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromdt"
                                                    PopupButtonID="ImageButton2">
                                                </cc1:CalendarExtender>
                                                <label>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                </label>
                                            </td>
                                            <td align="left" style="width: 30%">
                                                <label>
                                                    <asp:Label ID="Label25" runat="server" Text="To Date"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txttodt" runat="server" Width="70px" ValidationGroup="1" AutoPostBack="True"
                                                        OnTextChanged="txttodt_TextChanged"></asp:TextBox>
                                                </label>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txttodt"
                                                    PopupButtonID="ImageButton3">
                                                </cc1:CalendarExtender>
                                                <label>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel11" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td rowspan="3" style="width: 50%" align="right" valign="top">
                                                <label>
                                                    <asp:Label ID="Label18" runat="server" Text="Show Employee with Penalty points Exceeding"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txtfilterpoint" runat="server" MaxLength="10" Width="30px" onkeypress="return RealNumWithDecimal(this,event,2);"
                                                        AutoPostBack="True" OnTextChanged="txtfilterpoint_TextChanged"></asp:TextBox>
                                                </label>
                                            </td>
                                            <td rowspan="3" style="width: 22%" valign="top">
                                                <asp:Panel ID="Panel7" Width="100%" runat="server" Visible="False">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label19" runat="server" Text="For"></asp:Label>
                                                                </label>
                                                                <label>
                                                                    <asp:DropDownList ID="ddlperiod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlperiod_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Current Month" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Current Year" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%">
                                                <asp:Panel ID="Panel4" runat="server" Visible="False">
                                                    <table width="100%">
                                                        <tr valign="top">
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label20" runat="server" Text="Penalty points for month to date"></asp:Label>
                                                                </label>
                                                                <label>
                                                                    <asp:Label ID="lblptm" runat="server"></asp:Label>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%" valign="top">
                                                <asp:Panel ID="Panel5" runat="server" Visible="False">
                                                    <table width="100%">
                                                        <tr valign="top">
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label22" runat="server" Text="Penalty points for Year to date"></asp:Label>
                                                                </label>
                                                                <label>
                                                                    <asp:Label ID="lblpty" runat="server"></asp:Label>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td colspan="2">
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" style="font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblcmpny" runat="server" Font-Italic="true" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label26" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label11" runat="server" Text="List of Incident Report" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr style="vertical-align: top; height: 80%">
                                            <td colspan="2">
                                                <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                                    PageSize="20" Width="100%" AllowSorting="True" CellPadding="4" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found."
                                                    OnRowEditing="grid_RowEditing" OnSorting="grid_Sorting" OnRowDeleting="grid_RowDeleting"
                                                    OnRowCommand="grid_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Incidence No." SortExpression="Id" Visible="true"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblinsidenceno" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="EmployeeName" HeaderStyle-Width="12%">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-Width="4%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Date","{0:MM/dd/yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Time" SortExpression="Time" HeaderStyle-Width="7%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltime" runat="server" Text='<%# Eval("Time")%>'></asp:Label>
                                                                <asp:Label ID="lbltimezone" runat="server" Text='<%# Eval("Timezone")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Penalty Points" SortExpression="Penaltypoint" Visible="true"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpenaltypoint" runat="server" Text='<%# Eval("Penaltypoint")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="policyname" HeaderStyle-HorizontalAlign="Left" HeaderText="Related Policy/Procedure/Rule Broken"
                                                            SortExpression="policyname"></asp:BoundField>--%>
                                                        <asp:TemplateField HeaderText="Related Policy/Procedure/Rule Broken" SortExpression="policyname"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpolicy" runat="server" Text='<%# Eval("policyname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Incident Note" SortExpression="IncidenceNote" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="27%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblinsidencenote" runat="server" Text='<%# Eval("IncidenceNote")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Answer" SortExpression="IncidenceEmpAnsNote"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="27%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblempans" runat="server" Text='<%# Eval("IncidenceEmpAnsNote")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id") %>'
                                                                    ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ToolTip="Delete"
                                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                                </asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                                    CommandName="view" ToolTip="View Profile" Height="20px" ImageUrl="~/Account/images/viewprofile.jpg"
                                                                    Width="20px"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
