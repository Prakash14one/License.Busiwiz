<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    ValidateRequest="false" AutoEventWireup="true" CodeFile="FrmWDetail.aspx.cs"
    Inherits="Admin_FrmWDetail" Title="OnlineMis" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
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
        #txttitle
        {
            width: 244px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= divPrint.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1000px,height=1000px,toolbar=1,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML,'<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
        
        function box(me)
        {
           boxValue=me.value.length;
           boxSixe=me.size;
           minNum=0;
           MaxNum=60;
           if(boxValue==0)
           {
           me.size=36;
           }
           
          if(boxValue>MaxNum)
          {
          }
          else
          {
           if(boxValue>minNum)
           {
             me.size=boxValue;
           }
           else
           if(boxValue<minNum || boxValue!=minNum)
           {
           me.size=minNum;
           }
          }
        }
        function box(me)
        {
           boxValue=me.value.length;
           boxSixe=me.size;
           minNum=0;
           MaxNum=60;
           if(boxValue==0)
           {
           me.size=36;
           }
           
          if(boxValue>MaxNum)
          {
          }
          else
          {
           if(boxValue>minNum)
           {
             me.size=boxValue;
           }
           else
           if(boxValue<minNum || boxValue!=minNum)
           {
           me.size=minNum;
           }
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
            
           
            if(evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219||evt.keyCode==59||evt.keyCode==186)
            { 
                
            
              alert("You have entered an invalid character");
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
    <asp:UpdatePanel ID="upo1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div class="products_box">
                    <div style="padding-left: 2%">
                        <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <fieldset>
                        <legend>
                            <asp:Label runat="server" ID="lbllegend" Text=""></asp:Label>
                        </legend>
                        <div style="float: right;">
                            <asp:Button ID="btnadd" runat="server" CssClass="btnSubmit" Text="Add Instruction"
                                OnClick="btnadd_Click" />
                        </div>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="Pnladdnew" runat="server" Width="100%" Visible="false">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel13" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 30%;">
                                                        <label>
                                                            <asp:Label ID="Label3" runat="server" Text=" Select Weekly Goal Instruction of"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td style="width: 70%;">
                                                        <label>
                                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                                                RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Text="Business" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Department" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Division" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Employee" Value="3"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlbusiness" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 30%;">
                                                        <label>
                                                            <asp:Label ID="lblwname0" runat="server" Text="Business Name "></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td style="width: 70%;">
                                                        <label>
                                                            <asp:DropDownList ID="ddlStore" runat="server" Width="450px" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlStore_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgadddept" runat="server" Height="20px" ImageAlign="Bottom"
                                                                ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="LinkButton97666667_Click"
                                                                ToolTip="AddNew" Visible="False" Width="20px" />
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgdeptrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                                ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton13_Click"
                                                                ToolTip="Refresh" Visible="False" Width="20px" />
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgadddivision" runat="server" Height="20px" ImageAlign="Bottom"
                                                                ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="imgadddivision_Click"
                                                                ToolTip="AddNew" Width="20px" />
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgdivisionrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                                ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="imgdivisionrefresh_Click"
                                                                ToolTip="Refresh" Width="20px" />
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="PnlDivision" runat="server" Width="100%" Visible="False">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 30%;">
                                                        <label>
                                                            <asp:Label ID="Label1" runat="server" Text=" Division Name  "></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td style="width: 70%;">
                                                        <label>
                                                            <asp:DropDownList ID="ddlbusiness" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                                Height="22px" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged" Width="250px">
                                                            </asp:DropDownList>
                                                        </label>
                                                        <%--<label>
                                                            <asp:ImageButton ID="imgadddivision" runat="server" Height="20px" ImageAlign="Bottom"
                                                                ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="imgadddivision_Click"
                                                                ToolTip="AddNew" Width="20px" />
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgdivisionrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                                ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="imgdivisionrefresh_Click"
                                                                ToolTip="Refresh" Width="20px" />
                                                        </label>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="PnlEmployee" Width="100%" runat="server" Visible="false">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 30%;">
                                                        <label>
                                                            <asp:Label ID="Label4" runat="server" Text="Employee Name"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td style="width: 70%;">
                                                        <label>
                                                            <asp:DropDownList ID="ddlemployee" runat="server" Height="23px" Width="250px" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged"
                                                                AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgempadd" runat="server" Height="20px" ImageAlign="Bottom"
                                                                ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="imgempadd_Click" ToolTip="AddNew"
                                                                Width="20px" />
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgemprefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                                ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="imgemprefresh_Click"
                                                                ToolTip="Refresh" Width="20px" />
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel16" runat="server" Width="100%" Visible="true">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 30%">
                                                        <label>
                                                            <asp:Label ID="Label25" runat="server" Text="Select Year"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td align="left">
                                                       <%-- <label>
                                                            <asp:Label ID="Label26" runat="server" Text="Year"></asp:Label>
                                                        </label>--%> 
                                                        <label>
                                                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="70px"
                                                                OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
                                                            </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="Label27" runat="server" Text="Month"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Width="130px"
                                                                OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="Label28" runat="server" Text="Weekly"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="true" Width="200px"
                                                                OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel5" runat="server" Visible="true" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 30%">
                                                        <label>
                                                            <asp:Label ID="Label5" runat="server" Text=" Monthly Goal Name "></asp:Label>
                                                            <asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddly"
                                                                SetFocusOnError="true" runat="server" ErrorMessage="*" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                        </label>
                                                    </td>
                                                    <td style="width: 70%">
                                                        <label>
                                                            <asp:DropDownList ID="ddly" Width="450px" runat="server" Height="22px" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddly_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgobjadd" runat="server" Height="20px" ImageAlign="Bottom"
                                                                ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="imgobjadd_Click" ToolTip="AddNew"
                                                                Width="20px" />
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgobjreshresh" runat="server" AlternateText="Refresh" Height="20px"
                                                                ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="imgobjreshresh_Click"
                                                                ToolTip="Refresh" Width="20px" />
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel1" runat="server" Visible="true" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 30%;">
                                                        <label>
                                                            <asp:Label ID="Label7" runat="server" Text="Weekly Goal Name"></asp:Label>
                                                            <asp:Label ID="Label8" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlm"
                                                                SetFocusOnError="true" runat="server" ErrorMessage="*" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                        </label>
                                                    </td>
                                                    <td style="width: 70%;">
                                                        <label>
                                                            <asp:DropDownList ID="ddlm" runat="server" AppendDataBoundItems="true" Height="23px"
                                                                Width="450px">
                                                                <asp:ListItem Selected="True" Value="">Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgmonthadd" runat="server" Height="20px" ImageAlign="Bottom"
                                                                ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" ToolTip="AddNew" Width="20px"
                                                                OnClick="imgmonthadd_Click" />
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgmonthrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                                ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh"
                                                                Width="20px" OnClick="imgmonthrefresh_Click" />
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel2" runat="server" Width="100%" Visible="true">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 30%">
                                                        <label>
                                                            <asp:Label ID="Label9" runat="server" Text="Weekly Goal Instruction"></asp:Label>
                                                            <asp:Label ID="Label10" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtdetail"
                                                                runat="server" ErrorMessage="*" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                        </label>
                                                        <br />
                                                        <label>
                                                            <asp:RegularExpressionValidator ID="RegularExpresssdfsdionValidator4" runat="server"
                                                                ErrorMessage="Invalid Character" Display="Dynamic" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                                                ControlToValidate="txtdetail" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpresssqqqqdfionValidator5" runat="server"
                                                                ControlToValidate="txtdetail" SetFocusOnError="True" ErrorMessage="Please enter maximum 1500 chars"
                                                                ValidationExpression="^([\S\s]{0,1500})$" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                        </label>
                                                    </td>
                                                    <td style="width: 70%">
                                                        <label>
                                                            <asp:TextBox ID="txtdetail" Width="450px" Height="60px" runat="server" TextMode="MultiLine"
                                                                onkeypress="return checktextboxmaxlength(this,1500,event)" MaxLength="1500" onKeydown="return mask(event)"
                                                                onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div2',1500)"></asp:TextBox>
                                                            <asp:Label runat="server" ID="Label20" Text="Max " CssClass="labelcount"></asp:Label>
                                                            <span id="div2" cssclass="labelcount">1500</span>
                                                            <asp:Label ID="Label24" CssClass="labelcount" runat="server" Text="(A-Z 0-9 . , ? _)"></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel10" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 30%">
                                                        <label>
                                                            <asp:Label ID="Label12" runat="server" Text="Instruction Date"></asp:Label>
                                                            <asp:Label ID="Label13" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtincdate"
                                                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:TextBox ID="txtincdate" runat="server" Width="70px" ValidationGroup="1"></asp:TextBox>
                                                            <cc1:CalendarExtender runat="server" ID="cal1" TargetControlID="txtincdate" PopupButtonID="ImageButton2">
                                                            </cc1:CalendarExtender>
                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                                MaskType="Date" TargetControlID="txtincdate">
                                                            </cc1:MaskedEditExtender>
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel9" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 30%">
                                                        <label>
                                                            <asp:Label ID="Label14" runat="server" Text="Add Attachment"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:CheckBox ID="chk" runat="server" Checked="false" />
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel3" Width="100%" runat="server" Visible="true">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 30%">
                                                    </td>
                                                    <td style="width: 70%">
                                                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click"
                                                            CssClass="btnSubmit" ValidationGroup="1" />
                                                        <asp:Button ID="btnreset" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnreset_Click"
                                                            CssClass="btnSubmit" Visible="False" />
                                                        <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click"
                                                            CssClass="btnSubmit" ValidationGroup="1" Visible="False" />
                                                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CausesValidation="false"
                                                            OnClick="btncancel_Click" CssClass="btnSubmit" />
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
                            <asp:Label ID="lblmscc" runat="Server" Text="List of Weekly Goal Instructions" Font-Bold="False"></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <div style="float: right;">
                                        <asp:Button ID="btncancel0" runat="server" CssClass="btnSubmit" CausesValidation="false"
                                            OnClick="btncancel0_Click" Text="Printable Version" />
                                        <input id="Button7" runat="server" visible=" false" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                            type="button" value="Print" />
                                        <div style="float: right;">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel11" runat="server" Width="100%" Visible="true">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%;">
                                                    <label>
                                                        <asp:Label ID="Label15" runat="server" Text="Select Weekly Goal Instruction by"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged"
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Selected="True" Text="Business" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="Department" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Division" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Employee" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </label>
                                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel4" runat="server" Width="100%" Visible="true">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 30%" valign="top">
                                                    <label>
                                                        <asp:Label ID="lblwnamefilter" runat="server" Text="Business Name "></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:DropDownList ID="ddlsearchByStore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsearchByStore_SelectedIndexChanged"
                                                            Width="250px">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel6" runat="server" Width="100%" Visible="False">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 30%">
                                                    <label>
                                                        <asp:Label ID="Label16" runat="server" Text=" Division Name"></asp:Label>
                                                    </label>
                                                </td>
                                                <td align="left">
                                                    <label>
                                                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" Width="250px"
                                                            OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel7" runat="server" Width="100%" Visible="False">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 30%">
                                                    <label>
                                                        <asp:Label ID="Label17" runat="server" Text=" Employee Name"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="true" Width="250px"
                                                            OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel12" runat="server" Width="100%" Visible="False">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 30%">
                                                    <label>
                                                        <asp:Label ID="Label18" runat="server" Text=" Division Name"></asp:Label>
                                                    </label>
                                                </td>
                                                <td align="left">
                                                    <label>
                                                        <asp:DropDownList ID="ddlyfilter" runat="server" AutoPostBack="true" Width="250px"
                                                            OnSelectedIndexChanged="ddlyfilter_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel14" runat="server" Visible="false" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%">
                                                    <label>
                                                        <asp:Label ID="Label21" runat="server" Text=" Monthly Goal Name "></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:DropDownList ID="ddlmonthfilter" Width="250px" runat="server" Height="22px"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlmonthfilter_SelectedIndexChanged">
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
                                    <asp:Panel ID="Panel15" runat="server" Width="100%" Visible="true">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 30%">
                                                    <label>
                                                        <asp:Label ID="Label22" runat="server" Text="Select Year"></asp:Label>
                                                    </label>
                                                </td>
                                                <td align="left">
                                                   <%--  <label>
                                                        <asp:Label ID="Label23sdf" runat="server" Text="Year"></asp:Label>
                                                    </label>--%>
                                                    <label>
                                                        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged"
                                                            Width="70px">
                                                        </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label23sdfdf" runat="server" Text="Month"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged"
                                                            Width="130px">
                                                        </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label23" runat="server" Text="Weekly"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:DropDownList ID="ddlweek" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlweek_SelectedIndexChanged"
                                                            Width="200px">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel8" runat="server" Width="100%" Visible="true">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 30%">
                                                    <label>
                                                        <asp:Label ID="Label19" runat="server" Text="Filter by Weekly Goal"></asp:Label>
                                                    </label>
                                                </td>
                                                <td align="left">
                                                    <label>
                                                        <asp:DropDownList ID="ddlmfilter" runat="server" AutoPostBack="true" Width="250px"
                                                            OnSelectedIndexChanged="ddlmfilter_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="divPrint" runat="server">
                                        <table width="100%">
                                            <tr align="center">
                                                <td colspan="4">
                                                    <div id="mydiv" class="closed">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center" style="font-size: 20px; font-weight: bold; color: #000000">
                                                                    <asp:Label ID="lblcompany" runat="server" Font-Italic="true" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                    <asp:Label ID="Label2" runat="server" Font-Italic="true" Font-Size="20px" Text="Business : "></asp:Label>
                                                                    <asp:Label ID="lblBusiness" runat="server" Font-Italic="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                    <asp:Label ID="Label11" runat="server" Font-Italic="true" Text="List of Weekly Goal Instructions"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="text-align: left; font-size: 14px;">
                                                                    <asp:Label ID="lblBusiness1" runat="server" Font-Italic="true"></asp:Label>
                                                                    <asp:Label ID="lblDepartmemnt" runat="server" Font-Italic="true"></asp:Label>
                                                                    <asp:Label ID="lblDivision" runat="server" Font-Italic="true"></asp:Label>
                                                                    <asp:Label ID="lblEmp" runat="server" Font-Italic="true"></asp:Label>
                                                                    ,
                                                                    <asp:Label ID="lblmonthtext" runat="server" Font-Italic="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr style="vertical-align: top; height: 80%">
                                                <td colspan="2">
                                                    <cc11:PagingGridView ID="grid" runat="server" AutoGenerateColumns="False" DataKeyNames="DetailId"
                                                        OnRowDeleting="grid_RowDeleting" AllowPaging="True" 
                                                        OnPageIndexChanging="grid_PageIndexChanging" Width="100%" CssClass="mGrid" 
                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        OnSorting="grid_Sorting" AllowSorting="True" 
                                                        OnRowCommand="grid_RowCommand" OnRowEditing="grid_RowEditing">
                                                        <PagerSettings Mode="NumericFirstLast" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Business" SortExpression="Wname" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwname" runat="server" Text='<%#Bind("Wname")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department" HeaderStyle-Width="12%" SortExpression="Departmentname"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptname" runat="server" Text='<%#Bind("Departmentname")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Division" HeaderStyle-Width="12%" SortExpression="BusinessName"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptnsddfame" runat="server" Text='<%#Bind("BusinessName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Employee" SortExpression="EmployeeName" HeaderStyle-Width="12%"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptempnsddfame" runat="server" Text='<%#Bind("EmployeeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-Width="2%"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltasksewrewtatus" runat="server" Text='<%# Eval("Date")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Related Monthly Goal" HeaderStyle-HorizontalAlign="Left"
                                                                SortExpression="mtitle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmonthlygoal" runat="server" Text='<%# Eval("mtitle")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Weekly Goal Name" HeaderStyle-HorizontalAlign="Left"
                                                                SortExpression="title">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltaskstatuscc" runat="server" Text='<%# Eval("title")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Weekly Goal Instruction" SortExpression="detail" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltaskstatus" runat="server" Text='<%# Eval("detail")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Docs" SortExpression="DocumentId" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="2%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("DocumentId") %>' CommandName="Send"
                                                                        CommandArgument='<%#Eval("DetailId") %>' ForeColor="Black"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="2%">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit" CommandArgument='<%# Eval("DetailId") %>'
                                                                        ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                                HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="llinkbb" runat="server" ToolTip="Delete" CommandName="Delete"
                                                                        ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                                    </asp:ImageButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-Width="2%"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("masterid") %>'
                                                                        CommandName="view" ToolTip="View Profile" Height="20px" ImageUrl="~/Account/images/viewprofile.jpg"
                                                                        Width="20px"></asp:ImageButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <EmptyDataTemplate>
                                                            No Record Found.
                                                        </EmptyDataTemplate>
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </cc11:PagingGridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel21" runat="server" BackColor="#CCCCCC" BorderColor="Black" Width="500px"
                                        BorderStyle="Solid">
                                        <table style="background-color: #CCCCCC;" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="center" style="background-color: #CCCCCC;">
                                                    <table style="background-color: #CCCCCC; width: 500px;" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="text-align: left; font-weight: bolder;" valign="top">
                                                                Office Documents :
                                                            </td>
                                                            <td style="text-align: right" valign="top">
                                                                <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                                    OnClick="ImageButton3_Click" Width="15px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left; font-weight: bolder;" valign="top" colspan="2">
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Panel ID="pnlof" Height="220px" ScrollBars="Both" runat="server">
                                                        <table cellpadding="0" cellspacing="0" width="480Px">
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="10"
                                                                        Width="470Px" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                        OnRowCommand="GridView1_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                HeaderText="Document ID" SortExpression="DocumentId">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("DocumentId") %>' CommandName="View"
                                                                                        CommandArgument='<%#Eval("DocumentId") %>' ForeColor="Black"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="theHeader" />
                                                                                <ItemStyle CssClass="theHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="DocType" ItemStyle-HorizontalAlign="Left" HeaderText="Cabinet-Drawer-Folder"
                                                                                HeaderStyle-HorizontalAlign="Left" />
                                                                            <%-- <asp:BoundField DataField="DocumentId" HeaderText="Doc Id" />--%>
                                                                            <asp:BoundField DataField="DocumentTitle" ItemStyle-HorizontalAlign="Left" HeaderText="Title"
                                                                                HeaderStyle-HorizontalAlign="Left" />
                                                                            <asp:BoundField DataField="DocumentDate" ItemStyle-HorizontalAlign="Left" HeaderText="Date"
                                                                                HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Left" />
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                            <b>No Record Found.</b>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtenderAddnew" runat="server" BackgroundCssClass="modalBackground"
                                        Enabled="True" PopupControlID="Panel21" TargetControlID="HiddenButton222">
                                    </cc1:ModalPopupExtender>
                                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlStore" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlbusiness" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlyfilter" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="RadioButtonList2" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlsearchByStore" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DropDownList2" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DropDownList3" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="imgdeptrefresh" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgadddivision" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgempadd" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgobjadd" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddly" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlmfilter" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
