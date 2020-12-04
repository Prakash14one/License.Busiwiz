<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MyFrmProjectMaster.aspx.cs" Inherits="Admin_Admin_files_FrmProjectMaster"
    Title="OnlineMis" %>

<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        .style1
        {
            width: 30%;
            height: 42px;
        }
        .style2
        {
            width: 70%;
            height: 42px;
        }
        .style3
        {
            width: 30%;
            height: 46px;
        }
        .style4
        {
            width: 70%;
            height: 46px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML,'<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
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

    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <legend>
                    <asp:Label ID="lbllegend" runat="server" Text="" Visible="false"></asp:Label>
                </legend>
                <div style="float: right;">
                    <asp:Button ID="btnadd" runat="server" Text="Add New Project" OnClick="btnadd_Click"
                        CssClass="btnSubmit" Visible="False" />
                </div>
                <div style="clear: both;">
                </div>
              
                    <table cellspacing="3" width="100%" visible="false">
                         <asp:Panel ID="Pnladdnew" runat="server" Visible="false" Width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel8" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%" align="right">
                                                <label>
                                                    <asp:Label ID="Label3" runat="server" Text=" Add Project For"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                                        AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                        <asp:ListItem Text="Business" Value="0" Selected="True"></asp:ListItem>
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
                                <asp:Panel ID="Panel16" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%" align="left">
                                                <label>
                                                    <asp:Label ID="lblwname" runat="server" Text="Business Name: "></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                    <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstore_SelectedIndexChanged"
                                                        Width="250px">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="imgadddept" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                        OnClick="LinkButton97666667_Click" ToolTip="AddNew" Visible="False" Width="20px"
                                                        ImageAlign="Bottom" />
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="imgdeptrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                        ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton13_Click"
                                                        ToolTip="Refresh" Visible="False" Width="20px" ImageAlign="Bottom" />
                                                </label>
                                                <%-- <label>
                                                        <asp:ImageButton ID="imgadddivision" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                            OnClick="imgadddivision_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                                    </label>
                                                    <label>
                                                        <asp:ImageButton ID="imgdivisionrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                            ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="imgdivisionrefresh_Click"
                                                            ToolTip="Refresh" Width="20px" ImageAlign="Bottom" />
                                                    </label>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel5" runat="server" Width="100%" Visible="False">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:Label ID="Label1" runat="server" Text="Division Name: "></asp:Label>
                                                    <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlbusiness"
                                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:DropDownList ID="ddlbusiness" runat="server" AppendDataBoundItems="true" Width="250px"
                                                        OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="imgadddivision" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                        OnClick="imgadddivision_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="imgdivisionrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                        ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="imgdivisionrefresh_Click"
                                                        ToolTip="Refresh" Width="20px" ImageAlign="Bottom" />
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel6" runat="server" Width="100%" Visible="False">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:Label ID="Label4" runat="server" Text="Employee Name: "></asp:Label>
                                                    <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlemployee"
                                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:DropDownList ID="ddlemployee" runat="server" AppendDataBoundItems="true" Width="250px"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="imgempadd" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                        OnClick="imgempadd_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="imgemprefresh" runat="server" AlternateText="Refresh" ImageAlign="Bottom"
                                                        Height="20px" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="imgemprefresh_Click"
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
                                <asp:Panel ID="Panel4" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%" align="">
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:CheckBox ID="chkgoal" runat="server" Text="Select Goal" AutoPostBack="True"
                                                        TextAlign="Left" OnCheckedChanged="chkgoal_CheckedChanged" />
                                                </label>
                                                <label>
                                                    <asp:CheckBox ID="chkparty" runat="server" Text="Select Party" AutoPostBack="True"
                                                        TextAlign="Left" OnCheckedChanged="chkparty_CheckedChanged" />
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlgoal" runat="server" Width="100%" Visible="False">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:Label ID="Label6" runat="server" Text="Goal Type: "></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged"
                                                        Width="250px">
                                                        <asp:ListItem Value="1">Monthly Goal</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">Weekly Goal</asp:ListItem>
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
                                <asp:Panel ID="Panelempgoal" runat="server" Width="100%" Visible="False">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:Label ID="Label34" runat="server" Text="Goal Type: "></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:DropDownList ID="ddlempgoaltype" runat="server" AutoPostBack="true" Width="250px"
                                                        OnSelectedIndexChanged="ddlempgoaltype_SelectedIndexChanged">
                                                        <asp:ListItem Value="2">Business Monthly Goal</asp:ListItem>
                                                        <asp:ListItem Value="1" Selected="True">Business Weekly Goal</asp:ListItem>
                                                        <asp:ListItem Value="4">Department Monthly Goal</asp:ListItem>
                                                        <asp:ListItem Value="3">Department Weekly Goal</asp:ListItem>
                                                        <asp:ListItem Value="6">Division Monthly Goal</asp:ListItem>
                                                        <asp:ListItem Value="5">Division Weekly Goal</asp:ListItem>
                                                        <asp:ListItem Value="8">Employee Monthly Goal</asp:ListItem>
                                                        <asp:ListItem Value="7">Employee Weekly Goal</asp:ListItem>
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
                                <asp:Panel ID="pnlmonth" runat="server" Visible="false" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:Label ID="Label7" runat="server" Text="Goal Name: "></asp:Label>
                                                    <asp:Label ID="Label33" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlm"
                                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:DropDownList ID="ddlm" runat="server" AppendDataBoundItems="true" Width="455px">
                                                        <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
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
                                <asp:Panel ID="pnlweek" runat="server" Visible="false" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:Label ID="Label8" runat="server" Text="Goal Name: "></asp:Label>
                                                    <asp:Label ID="Label35" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlw"
                                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:DropDownList ID="ddlw" runat="server" AppendDataBoundItems="true" Width="455px">
                                                        <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
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
                                <asp:Panel ID="pnlparty" runat="server" Visible="false" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td class="style1">
                                                <label>
                                                    <asp:Label ID="Label9" runat="server" Text="Party Name: "></asp:Label>
                                                    <asp:Label ID="Label36" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlparty"
                                                        runat="server" ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td class="style2">
                                                <label>
                                                    <asp:DropDownList ID="ddlparty" runat="server" AppendDataBoundItems="true" Width="455px"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlparty_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
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
                                <asp:Panel ID="Panel7" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td class="style3">
                                                <label>
                                                    <asp:Label ID="Label10" runat="server" Text="Project Title: "></asp:Label>
                                                    <asp:Label ID="Label12" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtprojectname"
                                                        runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                        ValidationExpression="^([a-z?.,A-Z0-9_\s]*)" ControlToValidate="txtprojectname"
                                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td class="style4">
                                                <label>
                                                    <asp:TextBox ID="txtprojectname" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div1',150)"
                                                        runat="server" Width="450px" MaxLength="150"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:Label runat="server" ID="sadasd" Text="Max " CssClass="labelcount"></asp:Label>
                                                    <span id="div1">150</span>
                                                    <asp:Label ID="Label30" runat="server" Text="(A-Z 0-9 . , ? _)" CssClass="labelcount"></asp:Label>
                                                </label>
                                                <asp:HiddenField ID="hdnid" runat="server" />
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel runat="server" ID="Pnl1" Width="100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:Label ID="Label13" runat="server" Text="Add Description: "></asp:Label>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                                        SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)" ControlToValidate="txtdescription"
                                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter maximum 1500 chars"
                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1500})$"
                                                        ControlToValidate="txtdescription" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:TextBox ID="txtdescription" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'Span3',1500)"
                                                        runat="server" Height="60px" onkeypress="return checktextboxmaxlength(this,1500,event)"
                                                        Width="450px" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:Label runat="server" ID="Label24" Text="Max " CssClass="labelcount"></asp:Label>
                                                    <span id="Span3">1500</span>
                                                    <asp:Label ID="Label31" CssClass="labelcount" runat="server" Text="(A-Z 0-9 . , ? _)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel17" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%" align="right">
                                                <label>
                                                    <asp:Label ID="Label14" runat="server" Text="Status"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:DropDownList ID="ddlstatus" runat="server" CssClass="dropdowntxtsmall1" Width="250px">
                                                        <asp:ListItem Selected="True" Value="Pending">Pending</asp:ListItem>
                                                        <asp:ListItem Value="Completed">Completed</asp:ListItem>
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
                                <asp:Panel ID="Panel10" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:Label ID="Label15" runat="server" Text="Start Date: "></asp:Label>
                                                    <asp:Label ID="Label16" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtestartdate"
                                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:TextBox ID="txtestartdate" runat="server" Width="70px"></asp:TextBox>
                                                    <cc1:CalendarExtender runat="server" ID="cal1" TargetControlID="txtestartdate" PopupButtonID="ImageButton2">
                                                    </cc1:CalendarExtender>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtestartdate">
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
                            <td colspan="2">
                                <asp:Panel ID="Panel12" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%" align="left">
                                                <label>
                                                    <asp:Label ID="Label17" runat="server" Text="Target End Date: "></asp:Label>
                                                    <asp:Label ID="Label18" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txteenddate"
                                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:TextBox ID="txteenddate" runat="server" Width="70px"></asp:TextBox>
                                                    <cc1:CalendarExtender runat="server" ID="cal2" TargetControlID="txteenddate" PopupButtonID="ImageButton1">
                                                    </cc1:CalendarExtender>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txteenddate">
                                                    </cc1:MaskedEditExtender>
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel18" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:Label ID="Label19" runat="server" Text="Add as a new job for Job Costing: "></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:CheckBox ID="chkjob" runat="server" AutoPostBack="True" OnCheckedChanged="chkjob_CheckedChanged" />
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
                                            <td style="width: 30%" align="left">
                                                <label>
                                                    <asp:Label ID="Label20" runat="server" Text="Budgeted Amount: "></asp:Label>
                                                    <asp:Label ID="Label21" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtbudgetedamount"
                                                        SetFocusOnError="true" runat="server" ErrorMessage="*" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtbudgetedamount"
                                                        ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid Digits"
                                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:TextBox ID="txtbudgetedamount" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z.0-9]+$/,'Span2',15)"
                                                        runat="server" Width="70px" MaxLength="15"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:Label runat="server" ID="Label29" Text="Max " CssClass="labelcount"></asp:Label>
                                                    <span id="Span2" cssclass="labelcount">15</span>
                                                    <asp:Label ID="Label32" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label>
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
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:Label ID="Label22" runat="server" Text="Add Attachment: "></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
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
                            <td colspan="2">
                                <asp:Panel ID="Panel13" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%">
                                                <asp:Label ID="lblmain" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblsubcat" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblsubsub" runat="server" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 70%">
                                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click"
                                                    ValidationGroup="1" CssClass="btnSubmit" />
                                                <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click"
                                                    CssClass="btnSubmit" ValidationGroup="1" />
                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                    CssClass="btnSubmit" CausesValidation="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                         </asp:Panel>
                    </table>
               
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllist" Text=" List of Projects"> </asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td align="right" colspan="2">
                                <div style="float: right;">
                                    <asp:Button ID="btncancel0" runat="server" CssClass="btnSubmit" CausesValidation="false"
                                      Visible="false"  OnClick="btncancel0_Click" Text="Printable Version" />
                                    <input id="Button7" runat="server" visible="false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        class="btnSubmit" type="button" value="Print" />
                                    <%--<label>
                                        <asp:DropDownList ID="ddlExport" runat="server" OnSelectedIndexChanged="ddlExport_SelectedIndexChanged"
                                            AutoPostBack="True" Width="130px">
                                        </asp:DropDownList>
                                    </label>--%>
                                </div>
                                <div style="clear: both;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel15" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%;">
                                                <label>
                                                    <asp:Label runat="server" ID="Label23" Text=" Filter Project by: " Visible="False"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 70%;">
                                                <label>
                                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                                                        AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged"
                                                        Visible="False">
                                                        <asp:ListItem Text="Business" Selected="True" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="Department" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Division" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Employee" Value="2"></asp:ListItem>
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
                                <asp:Panel ID="Panel14" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td align="left" style="width: 30%;">
                                                <label>
                                                    <asp:Label ID="lblwnamefilter" runat="server" Text="Business Name: "></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 70%;">
                                                <label>
                                                    <asp:DropDownList Width="250px" ID="ddlsearchByStore" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlsearchByStore_SelectedIndexChanged" Enabled="False">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" width="100%">
                                <asp:Panel ID="Panel2" runat="server" Width="100%" Visible="False">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:Label ID="Label25" runat="server" Text="Division Name: "></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
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
                            <td colspan="2" width="100%">
                                <asp:Panel ID="Panel3" runat="server" Width="100%" Visible="False">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%">
                                                <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                <label>
                                                    <asp:Label ID="Label26" runat="server" Text="Employee Name: "></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                                                        Width="250px" Enabled="False">
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
                                <asp:Panel ID="Panel19" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 30%">
                                                <label>
                                                    <asp:Label ID="Label27" runat="server" Text=" Status: "></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <label>
                                                    <asp:DropDownList ID="ddlstatusfilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstatusfilter_SelectedIndexChanged"
                                                        Width="250px">
                                                        <asp:ListItem Selected="True" Value="0" Text="All"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Pending"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Completed"></asp:ListItem>
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
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%" ScrollBars="Both">
                                    <table width="100%">
                                        <tr align="center">
                                            <td colspan="2">
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" style="font-size: 22px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="lblcompany" Font-Italic="true" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label37" Font-Italic="true" runat="server" Text="Business:"></asp:Label>
                                                                <asp:Label ID="lblBusiness" Font-Italic="true" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label11" runat="server" Font-Italic="true" Text="List of Projects"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="font-size: 14px; color: #000000">
                                                                <asp:Label ID="lblBusiness1" runat="server" Font-Italic="true"></asp:Label>
                                                                <asp:Label ID="lblDepartmemnt" Font-Italic="true" runat="server"></asp:Label>
                                                                ,
                                                                <asp:Label ID="lblDivision" Font-Italic="true" runat="server"></asp:Label>
                                                                <asp:Label ID="lblEmp" Font-Italic="true" runat="server"></asp:Label>
                                                                ,
                                                                <asp:Label ID="lblst" Font-Italic="true" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr style="vertical-align: top; height: 80%">
                                            <td colspan="2">
                                                <cc11:PagingGridView ID="grid" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectId"
                                                    OnRowDeleting="grid_RowDeleting" 
                                                    OnPageIndexChanging="grid_PageIndexChanging" Width="100%" 
                                                    OnSorting="grid_Sorting" AllowSorting="True" OnSelectedIndexChanging="grid_SelectedIndexChanging"
                                                    OnRowCommand="grid_RowCommand" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                    AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found." OnRowEditing="grid_RowEditing"
                                                    ShowFooter="True">
                                                    <PagerSettings Mode="NumericFirstLast" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business" SortExpression="Wname" Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblwname" runat="server" Text='<%# Eval("Wname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Department" SortExpression="Departmentname" Visible="true"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldepartment" runat="server" Text='<%# Eval("Departmentname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Division" SortExpression="businessname" Visible="true"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldesdfdspartment" runat="server" Text='<%# Eval("businessname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="businessname" HeaderText="Division" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="businessname" HeaderStyle-Width="15%">
                                                            <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>--%>
                                                        <asp:TemplateField HeaderText="Employee" SortExpression="EmployeeName" Visible="false"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldesdEmployeeNamepartment" runat="server" Text='<%# Eval("EmployeeName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:BoundField DataField="EmployeeName" HeaderText="Employee" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="EmployeeName" HeaderStyle-Width="15%">
                                                            <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>--%>
                                                        <asp:TemplateField HeaderText="Project Name" HeaderStyle-Width="45%" SortExpression="projectname"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmissionname" runat="server" Text='<%# Eval("projectname")%>'></asp:Label>
                                                                <asp:Label ID="Label38" runat="server" Text='<%# Eval("ProjectId")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" SortExpression="status" Visible="true" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="6%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("status")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Target Date" SortExpression="EEndDate" HeaderStyle-Width="8%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltargetdate" runat="server" Text='<%# Eval("EEndDate","{0:mm/dd/yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbltotalsum" runat="server" Text="Total"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Budgeted Cost Planned" SortExpression="budgetedcost"
                                                            Visible="true" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbudcost" runat="server" Text='<%# Eval("budgetedcost")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblfooter" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Budgeted Cost Allocated" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Right" SortExpression="bdcost">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ldsfsdsfblbudcost" runat="server" Text='<%# Eval("bdcost")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblfooter1" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actual Cost" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Right" SortExpression="ActualCost">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ldsfsgd134444udfgfgfst" runat="server" Text='<%# Eval("ActualCost")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblfooter2" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <%-- <asp:TemplateField HeaderText="Actual Cost" SortExpression="Actual Cost" HeaderStyle-Width="2%"
                                                            Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblexc" runat="server" Text='<%# Eval("ActualCost")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Job Cost" HeaderStyle-Width="7%" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkjob" runat="server" Checked='<%# Eval("Addjob")%>' Enabled="false" />
                                                                <asp:LinkButton ID="lnklob" runat="server" ForeColor="Black" OnClick="lnklob_Click"
                                                                    CommandName="vie" CommandArgument='<%#Eval("ProjectId") %>' Text="Go"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Docs" SortExpression="DocumentId" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="4%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" Text='<%#Eval("DocumentId") %>'
                                                                    CommandName="Send" CommandArgument='<%#Eval("ProjectId") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="4%" />
                                                        </asp:TemplateField>
                                                        <%--<asp:CommandField ShowSelectButton="true" HeaderImageUrl="~/Account/images/edit.gif"
                                                            HeaderText="Edit" ButtonType="Image" SelectImageUrl="~/Account/images/edit.gif"
                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                            <ItemStyle HorizontalAlign="Left" Width="2%"></ItemStyle>
                                                        </asp:CommandField>--%>
                                                        <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ProjectId") %>'
                                                                    ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="15px" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                            HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Left" Visible="false">
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
                                                                <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("ProjectId") %>'
                                                                    CommandName="view" ToolTip="View Profile" Height="20px" ImageUrl="~/Account/images/viewprofile.jpg"
                                                                    Width="20px"></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </cc11:PagingGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="Panel21" runat="server" BackColor="#CCCCCC" BorderColor="Black" Width="500px"
                                                    BorderStyle="Solid">
                                                    <table bgcolor="#CCCCCC" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="center" bgcolor="#CCCCCC">
                                                                <table bgcolor="#CCCCCC" cellpadding="0" cellspacing="0" style="width: 500Px">
                                                                    <tr>
                                                                        <td style="text-align: left; font-weight: bolder;">
                                                                            <label>
                                                                                <asp:Label ID="Label28" runat="server" Text="Office Documents"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td style="text-align: right">
                                                                            <label>
                                                                                <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                                                    Width="15px" />
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Panel ID="pnlof" Height="220px" ScrollBars="Both" runat="server">
                                                                    <table cellpadding="0" cellspacing="0" width="480Px">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="10"
                                                                                    Width="470" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                                    OnRowCommand="GridView1_RowCommand">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Doc ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                            SortExpression="DocumentId">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" Text='<%#Eval("DocumentId") %>'
                                                                                                    CommandName="View" HeaderStyle-HorizontalAlign="Left" CommandArgument='<%#Eval("DocumentId") %>'></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="DocType" HeaderText="Cabinet-Drawer-Folder"
                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                        <%-- <asp:BoundField DataField="DocumentId" HeaderText="Doc Id" />--%>
                                                                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="DocumentTitle" HeaderText="Title"
                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="DocumentDate" HeaderText="Date"
                                                                                            HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Left" />
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
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <asp:Panel ID="panelhide" runat="server" Visible="false">
                    <asp:GridView ID="grdMaterialIssue" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        CssClass="mGrid" GridLines="Both" EmptyDataText="No Record Found." Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Material Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="30%">
                                <ItemTemplate>
                                    <asp:Label ID="lblitemnameid" runat="server" Text='<%# Eval("InvWMasterId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbljobname124" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    <asp:Label ID="lblmaterialmasterid" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lbldate124" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="18%">
                                <ItemTemplate>
                                    <asp:Label ID="lblqty" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="17%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Cost" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="18%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCost" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ViewDetail" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="7%">
                                <ItemTemplate>
                                    <a href='Materialissueform.aspx?jobid=<%# Eval("JobMasterId")%>&amp;materilInvId=<%#Eval("InvWMasterId") %>'
                                        target="_blank">
                                        <asp:Label ID="lblmateriliss" runat="server" Text="View Detail" ForeColor="#0066FF"></asp:Label>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdoverhead" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        CssClass="mGrid" GridLines="Both" DataKeyNames="Id" Width="100%" EmptyDataText="No Record Found.">
                        <Columns>
                            <asp:TemplateField HeaderText="Overhead Name" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lbloverheadname" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                    <asp:Label ID="lbloverheadmasterid" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="8%">
                                <ItemTemplate>
                                    <asp:Label ID="lblstartdate789" runat="server" Text='<%#Bind("StartDate","{0:MM/dd/yyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="8%">
                                <ItemTemplate>
                                    <asp:Label ID="lblenddate789" runat="server" Text='<%#Bind("EndDate","{0:MM/dd/yyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Overhead By Material" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblohbymaterial789" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Overhead By Labour Cost" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lbldirectlabour789" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Overhead By Project Duration " HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblnoofdays789" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Overhead By Equal" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="ohbyequal789" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblOhAllocationtotal789" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grddailywork" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        CssClass="mGrid" GridLines="Both" DataKeyNames="Id" Width="100%" EmptyDataText="No Record Found.">
                        <Columns>
                            <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployee" runat="server" Text='<%#Bind("EmployeeName") %>'></asp:Label>
                                    <asp:Label ID="lblmasterid" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hours" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblhours" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cost" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="18%">
                                <ItemTemplate>
                                    <asp:Label ID="lblcost" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ViewDetail" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="7%">
                                <ItemTemplate>
                                    <a href='DailyWorkSheet.aspx?EmployeeId=<%# Eval("EmployeeMasterID")%>&amp;JobId=<%#Eval("JobMasterId") %>'
                                        target="_blank">
                                        <asp:Label ID="lblentrytyp2e" runat="server" Text="View Detail" ForeColor="#0066FF"></asp:Label></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblTotalSum" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lbltotaloverheadbyall" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lbldailyworktotal" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblMyfinal" runat="server" Text=""></asp:Label>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlstore" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlbusiness" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="RadioButtonList2" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlsearchByStore" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DropDownList2" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DropDownList3" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="imgdeptrefresh" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgadddivision" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgempadd" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btncancel0" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnadd" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="chkgoal" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="chkparty" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlstatusfilter" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
