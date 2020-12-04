<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    ValidateRequest="false" AutoEventWireup="true" CodeFile="FrmMMaster.aspx.cs"
    Inherits="Admin_FrmMMaster" Title="OnlineMis" %>

<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= divPrint.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1000px,height=1000px,toolbar=1,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                    <asp:Button ID="btnadd" runat="server" Text="Add Monthly Goal" OnClick="btnadd_Click"
                        CssClass="btnSubmit" />
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Pnladdnew" runat="server" Visible="false">
                    <table cellspacing="3" width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel1" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%" align="right">
                                                <label>
                                                    <asp:Label ID="Label4" runat="server" Text="Add Monthly Goal For"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
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
                                <td colspan="2">
                                    <asp:Panel ID="pnlEmployeeBusiness" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                    <label>
                                                        <asp:Label ID="Label40ghghgfhfg" runat="server" Text="Business Name "></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>                                                                                                              
                                                          <asp:DropDownList ID="DDLonlyBusiness" runat="server"  Width="250px" OnSelectedIndexChanged="DDLonlyBusiness_SelectedIndexChanged" AutoPostBack="True">
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
                                <asp:Panel ID="pnlbusiness" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" style="width: 30%;">
                                                <label>
                                                    <asp:Label ID="lblwname0" runat="server" Text="Business Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <label>
                                                    <asp:DropDownList ID="ddlStore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged"
                                                        Width="455px">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="imgadddept" runat="server" Height="20px" ImageAlign="Bottom"
                                                        ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="LinkButton97666667_Click"
                                                        ToolTip="AddNew" Visible="False" Width="20px" />
                                                    <asp:ImageButton ID="imgdeptrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                        ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton13_Click"
                                                        ToolTip="Refresh" Visible="False" Width="20px" />
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="imgadddivision" runat="server" Height="20px" ImageAlign="Bottom"
                                                        ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="imgadddivision_Click"
                                                        ToolTip="AddNew" Width="20px" />
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
                            <td colspan="2">
                                <asp:Panel ID="pnldivision" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label5" runat="server" Text="Division Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="text-align: left; width: 70%;">
                                                <label>
                                                    <asp:DropDownList ID="ddlbusiness" Width="250px" runat="server" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                                                        Height="22px">
                                                    </asp:DropDownList>
                                                </label>
                                                <%-- <label>
                                                    <asp:ImageButton ID="imgadddivision" runat="server" Height="20px" ImageAlign="Bottom"
                                                        ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="imgadddivision_Click"
                                                        ToolTip="AddNew" Width="20px" />
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
                            <td colspan="2">
                                <asp:Panel ID="pnlemployee" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label6" runat="server" Text="Employee Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="text-align: left; width: 70%;">
                                                <label>
                                                    <asp:DropDownList ID="ddlemployee" runat="server" AppendDataBoundItems="true" Height="22px"
                                                        Width="250px" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="imgempadd" runat="server" Height="20px" ImageAlign="Bottom"
                                                        ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="imgempadd_Click" ToolTip="AddNew"
                                                        Width="20px" />
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
                                <asp:Panel ID="Panel6" runat="server" Visible="true">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label9" runat="server" Text="Select Year"></asp:Label>
                                                    <%--<asp:Label ID="Label21" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RiredFieldValidator1" ControlToValidate="ddlmonth"
                                                        InitialValue="0" SetFocusOnError="true" runat="server" ErrorMessage="*" Display="Dynamic"
                                                        ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                                </label>
                                            </td>
                                            <td style="text-align: left; width: 70%;">
                                              <%--  <label>
                                                    <asp:Label ID="Label3" runat="server" Text="Year"></asp:Label>
                                                </label>--%>
                                                <label>
                                                    <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true" 
                                                    Width="170px" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged1">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label21" runat="server" Text="Month"></asp:Label>
                                                    <asp:Label ID="Label29" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="repodsfdsf" runat="server" ValidationGroup="1" ControlToValidate="ddlmonth"
                                                        InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </label>
                                                <label>
                                                    <asp:DropDownList ID="ddlmonth" AppendDataBoundItems="true" AutoPostBack="true" runat="server"
                                                        Height="22px" Width="170px" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
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
                                <asp:Panel ID="pnlradio" runat="server" Visible="False">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label23" runat="server" Text="Set Goals Related To"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="text-align: right; width: 70%;">
                                                <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Selected="True" Text=" Yearly Goals of Department" Value="0"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Monthly Goals of Business"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlradio1" runat="server" Visible="False">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label27" runat="server" Text="Set Goals Related To"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="text-align: right; width: 70%;">
                                                <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList4_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Text=" Yearly Goals of Division" Value="0"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Monthly Goals of Business/Department"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlradio2" runat="server" Visible="False">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label28" runat="server" Text="Set Goals Related To"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="text-align: right; width: 70%;">
                                                <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList5_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Text=" Yearly Goals of Employee" Value="0"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Monthly Goals of Business/Department/Division"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel15" runat="server" Visible="False">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label35" runat="server" Text="Link this goal to monthly goals of"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="text-align: right; width: 70%;">
                                                <label>
                                                    <asp:DropDownList ID="DropDownList6" AutoPostBack="true" runat="server" Width="250px"
                                                        OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="0" Text="Business Monthly Goal"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Department Monthly Goal"></asp:ListItem>
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
                                <asp:Panel ID="Panel16" runat="server" Visible="False">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label36" runat="server" Text="Link this goal to monthly goals of"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="text-align: right; width: 70%;">
                                                <label>
                                                    <asp:DropDownList ID="DropDownList7" AutoPostBack="true" runat="server" Width="250px"
                                                        OnSelectedIndexChanged="DropDownList7_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="0" Text="Business Monthly Goal"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Department Monthly Goal"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Division Monthly Goal"></asp:ListItem>
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
                                <asp:Panel ID="Panel5" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label7" runat="server" Text="Yearly Goal Name"></asp:Label>
                                                    <asp:Label ID="Label51df" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddly"
                                                        InitialValue="0" SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td style="text-align: left; width: 70%;">
                                                <label>
                                                    <asp:DropDownList ID="ddly" Width="455px" runat="server" Height="22px" OnSelectedIndexChanged="ddly_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="imgobjadd" runat="server" Height="20px" ImageAlign="Bottom"
                                                        ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="imgobjadd_Click" ToolTip="AddNew"
                                                        Width="20px" />
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
                            <td colspan="2">
                                <asp:Panel ID="pnlmonthly1" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label26" runat="server" Text="Business Monthly Goal"></asp:Label>
                                                    <asp:Label ID="Label43" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlbusimonthly"
                                                        InitialValue="0" SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td style="text-align: right; width: 70%;">
                                                <label>
                                                    <asp:DropDownList ID="ddlbusimonthly" runat="server" Width="455px">
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
                                <asp:Panel ID="pnlmonthly2" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label37" runat="server" Text="Department Monthly Goal"></asp:Label>
                                                    <asp:Label ID="Label44" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddldeptmonthly"
                                                        InitialValue="0" SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td style="text-align: right; width: 70%;">
                                                <label>
                                                    <asp:DropDownList ID="ddldeptmonthly" runat="server" Width="455px">
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
                                <asp:Panel ID="pnlmonthly3" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label40" runat="server" Text="Division Monthly Goal"></asp:Label>
                                                    <asp:Label ID="Label45" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddldivimonthly"
                                                        InitialValue="0" SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td style="text-align: right; width: 70%;">
                                                <label>
                                                    <asp:DropDownList ID="ddldivimonthly" runat="server" Width="455px">
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
                                <asp:Panel ID="Panel7" runat="server" Visible="true">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label8" runat="server" Text="Monthly Goal Title"></asp:Label>
                                                    <asp:Label ID="Label20" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txttitle"
                                                        SetFocusOnError="true" runat="server" ErrorMessage="*" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                                        ControlToValidate="txttitle" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td style="text-align: left; width: 70%;">
                                                <label>
                                                    <asp:TextBox ID="txttitle" runat="server" MaxLength="150" Width="450px" onKeydown="return mask(event)"
                                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div1',150)"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:Label runat="server" ID="sadasd" Text="Max " CssClass="labelcount"></asp:Label>
                                                    <span id="div1" cssclass="labelcount">150</span>
                                                    <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 . , ? _)" CssClass="labelcount"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel13" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%" align="right">
                                                <label>
                                                    <asp:Label ID="Label10" runat="server" Text="Show Description"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="text-align: left; width: 70%;">
                                                <asp:CheckBox ID="Button2" runat="server" AutoPostBack="True" OnCheckedChanged="Button2_CheckedChanged" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel runat="server" ID="Pnl1" Visible="false" Width="100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 30%;">
                                                <asp:RegularExpressionValidator ID="RegularExpresssdfsdionValidator4" runat="server"
                                                    ErrorMessage="Invalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                                    ControlToValidate="txtdescription" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpresssqqqqdfionValidator5" runat="server"
                                                    ControlToValidate="txtdescription" SetFocusOnError="True" ErrorMessage="Please enter maximum 1500 chars"
                                                    ValidationExpression="^([\S\s]{0,1500})$" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            </td>
                                            <td style="width: 70%;">
                                                <label>
                                                    <asp:TextBox ID="txtdescription" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div2',1500)"
                                                        runat="server" Height="60px" onkeypress="return checktextboxmaxlength(this,1500,event)"
                                                        Width="450px" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:Label runat="server" ID="Label1" Text="Max " CssClass="labelcount"></asp:Label>
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
                            <td colspan="2">
                                <asp:Panel ID="Panel8" runat="server" Visible="true">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label12" runat="server" Text="Budgeted Cost"></asp:Label>
                                                    <asp:Label ID="Label55" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtbudgetedamount"
                                                        SetFocusOnError="true" runat="server" ErrorMessage="*" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtbudgetedamount"
                                                        ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid Digits"
                                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td style="text-align: left; width: 70%;">
                                                <label>
                                                    <asp:TextBox ID="txtbudgetedamount" runat="server" Width="70px" MaxLength="15" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z.0-9]+$/,'Span2',15)">
                                                    </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                        TargetControlID="txtbudgetedamount" ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </label>
                                                <label>
                                                    <asp:Label runat="server" ID="Label25" Text="Max " CssClass="labelcount"></asp:Label>
                                                    <span id="Span2" cssclass="labelcount">15</span>
                                                    <asp:Label ID="Label2" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel9" runat="server" Visible="true">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label13" runat="server" Text="Add Attachment"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="text-align: left; width: 70%;">
                                                <asp:CheckBox ID="chk" runat="server" Checked="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                       

                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel19" runat="server" Visible="true">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                                <label>
                                                    <asp:Label ID="Label3" runat="server" Text="Create a new project also for same title and details ?"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="text-align: left; width: 70%;">
                                                <asp:CheckBox ID="CheckBox1" runat="server" Checked="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>



                          <tr>
                            <td colspan="2">
                               
                            </td>
                        </tr>




                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel10" runat="server" Visible="true">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right; width: 30%;">
                                            </td>
                                            <td style="text-align: left; width: 70%;">
                                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click"
                                                    ValidationGroup="1" CssClass="btnSubmit" />
                                                <asp:Button ID="btnreset" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnreset_Click"
                                                    Visible="False" CssClass="btnSubmit" />
                                                <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click"
                                                    Visible="False" ValidationGroup="1" CssClass="btnSubmit" />
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
                    <asp:Label ID="Label19" runat="server" Text="List of Monthly Goals"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdnid" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btncancel0" runat="server" CssClass="btnSubmit" CausesValidation="false"
                              Visible="false"  OnClick="btncancel0_Click" Text="Printable Version" />
                            <input id="Button7" runat="server" visible=" false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                class="btnSubmit" style="width: 51px;" type="button" value="Print" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel11" runat="server" Width="100%" Visible="true">
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: right; width: 30%;">
                                            <label>
                                                <asp:Label ID="Label14" runat="server" Text="Select Monthly Goal of"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="text-align: left; width: 70%;" valign="top">
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
                                <td colspan="2">
                                    <asp:Panel ID="pnlEmpbusinessfilter" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                    <label>
                                                        <asp:Label ID="Label31" runat="server" Text="Business Name: "></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>                                                                                                              
                                                          <asp:DropDownList ID="DDLonlyBusinessFilter" runat="server"  Width="250px" OnSelectedIndexChanged="DDLonlyBusinessFilter_SelectedIndexChanged" AutoPostBack="True">
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
                            <asp:Panel ID="Panel2" runat="server" Width="100%" Visible="true">
                                <table width="100%">
                                    <tr>
                                        <td align="right" style="width: 30%" valign="top">
                                            <label>
                                                <asp:Label ID="lblwnamefilter" runat="server" Text="Business Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td align="left" valign="top">
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
                            <asp:Panel ID="Panel3" runat="server" Width="100%" Visible="False">
                                <table width="100%">
                                    <tr>
                                        <td align="right" style="width: 30%">
                                            <label>
                                                <asp:Label ID="Label15" runat="server" Text="Division Name"></asp:Label>
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
                            <asp:Panel ID="Panel4" runat="server" Width="100%" Visible="False">
                                <table width="100%">
                                    <tr>
                                        <td align="right" style="width: 30%">
                                            <label>
                                                <asp:Label ID="Label16" runat="server" Text="Employee Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td align="left">
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
                            <asp:Panel ID="panelyrmon" runat="server" Width="100%" Visible="true">
                                <table width="100%">
                                    <tr>
                                        <td align="right" style="width: 30%">
                                            <label>
                                                <asp:Label ID="Label30" runat="server" Text="Select Year"></asp:Label>
                                            </label>
                                        </td>
                                        <td align="left">
                                           <%-- <label>
                                                <asp:Label ID="Label31" runat="server" Text="Year"></asp:Label>
                                            </label>--%>
                                            <label>
                                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="130px"
                                                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
                                                </asp:DropDownList>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label32" runat="server" Text="Month"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Width="130px"
                                                    OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
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
                            <asp:Panel ID="paneldepartment" runat="server" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td align="right" style="width: 30%">
                                            <label>
                                                <asp:Label ID="Label33" runat="server" Text="Filter by Related Goals"></asp:Label>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:DropDownList ID="dropdowndepartment" runat="server" AutoPostBack="true" Width="250px"
                                                    OnSelectedIndexChanged="dropdowndepartment_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Selected="True" Text="-Select-"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Yearly Goals of Department"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Monthly Goals of Business"></asp:ListItem>
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
                            <asp:Panel ID="paneldivision" runat="server" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td align="right" style="width: 30%">
                                            <label>
                                                <asp:Label ID="Label38" runat="server" Text="Filter by Related Goals"></asp:Label>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:DropDownList ID="dropdowndivision" runat="server" AutoPostBack="true" Width="250px"
                                                    OnSelectedIndexChanged="dropdowndivision_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Selected="True" Text="-Select-"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Yearly Goals of Division"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Monthly Goals of Business"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Monthly Goals of Department"></asp:ListItem>
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
                            <asp:Panel ID="panelemployee" runat="server" Width="100%" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td align="right" style="width: 30%">
                                            <label>
                                                <asp:Label ID="Label41" runat="server" Text="Filter by Related Goals"></asp:Label>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:DropDownList ID="dropdownemployee" runat="server" AutoPostBack="true" Width="250px"
                                                    OnSelectedIndexChanged="dropdownemployee_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Selected="True" Text="-Select-"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Yearly Goals of Employee"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Monthly Goals of Business"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Monthly Goals of Department"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Monthly Goals of Division"></asp:ListItem>
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
                            <asp:Panel ID="Panel12" runat="server" Width="100%" Visible="true">
                                <table width="100%">
                                    <tr>
                                        <td align="right" style="width: 30%">
                                            <label>
                                                <asp:Label ID="Label17" runat="server" Text="Yearly Goal Name"></asp:Label>
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
                        <td colspan="2">
                            <asp:Panel ID="Panel14" runat="server" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: right; width: 30%;">
                                            <label>
                                                <asp:Label ID="Label34" runat="server" Text="Business Monthly Goal"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="text-align: right; width: 70%;">
                                            <label>
                                                <asp:DropDownList ID="DropDownList5" runat="server" Width="250px" AutoPostBack="True"
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
                        <td colspan="2">
                            <asp:Panel ID="Panel17" runat="server" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: right; width: 30%;">
                                            <label>
                                                <asp:Label ID="Label39" runat="server" Text="Department Monthly Goal"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="text-align: right; width: 70%;">
                                            <label>
                                                <asp:DropDownList ID="DropDownList8" runat="server" Width="250px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DropDownList8_SelectedIndexChanged">
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
                            <asp:Panel ID="Panel18" runat="server" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: right; width: 30%;">
                                            <label>
                                                <asp:Label ID="Label42" runat="server" Text="Division Monthly Goal"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="text-align: right; width: 70%;">
                                            <label>
                                                <asp:DropDownList ID="DropDownList9" runat="server" Width="250px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DropDownList9_SelectedIndexChanged">
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
                            <asp:Panel ID="divPrint" runat="server" Width="100%">
                                <table width="100%">
                                    <tr align="center">
                                        <td>
                                            <div id="mydiv" class="closed">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="center" style="font-size: 20px; font-weight: bold; color: #000000">
                                                            <asp:Label ID="lblcompany" runat="server" Font-Italic="true" Font-Size="20px" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                            <asp:Label ID="Label22" runat="server" Font-Italic="true" Text="Business : "></asp:Label>
                                                            <asp:Label ID="lblBusiness" runat="server" Font-Italic="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                            <asp:Label ID="Label11" runat="server" Text=" List of Monthly Goals" ForeColor="Black"
                                                                Font-Size="18px" Font-Italic="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="text-align: left; font-size: 14px;">
                                                            <asp:Label ID="lblBusiness1" runat="server" Font-Italic="true"></asp:Label>
                                                            <asp:Label ID="lblDepartmemnt" runat="server" Font-Italic="true"></asp:Label>
                                                            <asp:Label ID="lblDivision" runat="server" Font-Italic="true"></asp:Label>
                                                            <asp:Label ID="lblEmp" runat="server" Font-Italic="true"></asp:Label>,
                                                            <asp:Label ID="lblyeartext" runat="server" Font-Italic="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top; height: 80%">
                                        <td>
                                            <cc11:PagingGridView ID="grid" runat="server" AutoGenerateColumns="False" DataKeyNames="masterid"
                                                OnRowDeleting="grid_RowDeleting" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                AllowPaging="True" OnPageIndexChanging="grid_PageIndexChanging" Width="100%"
                                                OnSorting="grid_Sorting" AllowSorting="True" OnSelectedIndexChanging="grid_SelectedIndexChanging"
                                                OnRowCommand="grid_RowCommand" OnRowEditing="grid_RowEditing" ShowFooter="true">
                                                <%--<HeaderStyle BackColor="gray" />--%>
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
                                                        <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Division" HeaderStyle-Width="12%" SortExpression="BusinessName"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldeptnsddfame" runat="server" Text='<%#Bind("BusinessName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="BusinessName" HeaderText="Division" HeaderStyle-HorizontalAlign="Left"
                                                        SortExpression="BusinessName"></asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="Employee" SortExpression="EmployeeName" HeaderStyle-Width="12%"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldeptempnsddfame" runat="server" Text='<%#Bind("EmployeeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="EmployeeName" HeaderStyle-HorizontalAlign="Left" HeaderText="Employee"
                                                        SortExpression="EmployeeName"></asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="Month" SortExpression="Month" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbfrgtrikookortame" runat="server" Text='<%#Bind("Month")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="Yearname" HeaderStyle-HorizontalAlign="Left" HeaderText=" Yearly Goal Name"
                                                        SortExpression="Yearname"></asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="Monthly Goal Title" HeaderStyle-HorizontalAlign="Left"
                                                        SortExpression="title">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltaskstatus" runat="server" Text='<%# Eval("title")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Related Goal Name" SortExpression="Yearname" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbertypnsddfame" runat="server" Text='<%#Bind("Yearname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotalsum" runat="server" Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Budgeted Cost Planned" HeaderStyle-Width="10%" SortExpression="budgetedcost"
                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbudcost" runat="server" Text='<%# Eval("budgetedcost","{0:###,###.##}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblfooter" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Budgeted Cost Allocated" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Right" SortExpression="bdcost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ldsfsdsfblbudcost" runat="server" Text='<%# Eval("bdcost","{0:###,###.##}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblfooter1" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actual Cost" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Right" SortExpression="ActualCost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ldsfsgd134444udfgfgfst" runat="server" Text='<%# Eval("ActualCost","{0:###,###.##}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblfooter2" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <%--  <asp:BoundField DataField="Month" HeaderStyle-HorizontalAlign="Left" HeaderText="Month "
                                                        HeaderStyle-Width="7%" SortExpression="Month" />--%>
                                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbfrgttotothkotame" runat="server" Text='<%#Bind("StatusName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="StatusName" HeaderText="Status" HeaderStyle-Width="7%"
                                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="Docs" SortExpression="DocumentId" HeaderStyle-Width="2%"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("DocumentId") %>' CommandName="Send"
                                                                CommandArgument='<%#Eval("MasterId") %>' ForeColor="Black"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit" CommandArgument='<%# Eval("MasterId") %>'
                                                                ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <%--<asp:CommandField ShowSelectButton="true" HeaderImageUrl="~/Account/images/edit.gif"
                                                        HeaderText="Edit" ButtonType="Image" SelectImageUrl="~/Account/images/edit.gif"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                        <ItemStyle HorizontalAlign="Left" Width="2%"></ItemStyle>
                                                    </asp:CommandField>--%>
                                                    <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                        HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ToolTip="Delete"
                                                                ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                            </asp:ImageButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" />
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
                                                <PagerSettings FirstPageImageUrl="~/images/firstpg.gif" FirstPageText="" LastPageImageUrl="~/images/lastpg.gif"
                                                    LastPageText="" NextPageImageUrl="~/images/nextpg.gif" NextPageText="" PreviousPageImageUrl="~/images/prevpg.gif"
                                                    PreviousPageText="" />
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
                </table>
            </fieldset>
            <table width="100%">
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
                                                    <label>
                                                        <asp:Label ID="Label18" runat="server" Text="Office Documents"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="text-align: right" valign="top">
                                                    <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                        OnClick="ImageButton3_Click" Width="15px" CssClass="btnSubmit" />
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
                                                            Width="470Px" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                            OnRowCommand="GridView1_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Doc ID" SortExpression="DocumentId" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("DocumentId") %>' CommandName="View"
                                                                            CommandArgument='<%#Eval("DocumentId") %>' ForeColor="Black"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="DocType" HeaderText="Cabinet-Drawer-Folder" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" />
                                                                <%-- <asp:BoundField DataField="DocumentId" HeaderText="Doc Id" />--%>
                                                                <asp:BoundField DataField="DocumentTitle" HeaderText="Title" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="DocumentDate" HeaderText="Date" HeaderStyle-Width="2%"
                                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                No Record Found.
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
            <asp:AsyncPostBackTrigger ControlID="Button2" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="imgdeptrefresh" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgadddivision" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgempadd" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgobjadd" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddly" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
