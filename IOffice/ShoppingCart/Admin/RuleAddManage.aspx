<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="RuleAddManage.aspx.cs" Inherits="ShoppingCart_Admin_RuleAddManage" %>

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
    </style>
    <script type="text/javascript" language="javascript">
        function RealNumWithDecimal(myfield, e) {
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

            else {
                myfield.value = "";
                return false;
            }
        }
        function mask(evt) {
            if (evt.keyCode == 13) {

            }
            if (evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {
                alert("You have entered an invalid character");
                return false;
            }
        }

        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered an invalid character");
            }

            counter = document.getElementById(id);

            if (txt1.value.length <= max_len) {
                remaining_characters = max_len - txt1.value.length;
                counter.innerHTML = remaining_characters;
            }
        }


        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
            }
        }      
    </script>
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
    </script>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="Label21" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" CssClass="btnSubmit" Text="Add Rule" OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Pnladdnew" runat="server" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="lblwname" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstore_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Rule Title"></asp:Label>
                                        <asp:Label ID="Label28" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                            InitialValue="0" ControlToValidate="ddlruletitle" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlruletitle" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlruletitle_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgadd" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                            ToolTip="AddNew" Width="20px" ImageAlign="Bottom" OnClick="imgadd_Click" /></label>
                                    <label>
                                        <asp:ImageButton ID="imgrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                            ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                            ImageAlign="Bottom" OnClick="imgrefresh_Click" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Version No."></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <asp:TextBox ID="lblversion" runat="server" Text="1" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Rule Name "></asp:Label>
                                        <asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtrulename"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            SetFocusOnError="True" ValidationExpression="^([a-z().A-Z-,0-9\s]*)" ControlToValidate="txtrulename"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtrulename" MaxLength="150" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%^'_+@&*>:;={}[]|\/]/g,/^[\a-z().A-Z0-9,-\s]+$/,'Span2',150)"
                                            runat="server" ValidationGroup="1" Width="350px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label25" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span2">150</span>
                                        <asp:Label ID="Label26" runat="server" Text="(A-Z 0-9 ) ( , . -)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="Rule Description"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <asp:CheckBox ID="Button2" runat="server" OnCheckedChanged="Button2_CheckedChanged"
                                        AutoPostBack="True" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel runat="server" ID="Pnl1" Visible="false" Width="100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 30%">
                                                    <label>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-z().A-Z-,0-9\s]*)"
                                                            ControlToValidate="txtdescription" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*"
                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1500})$"
                                                            ControlToValidate="txtdescription" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:TextBox ID="txtdescription" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%^'_+@&*>:;={}[]|\/]/g,/^[\a-z().A-Z0-9,-\s]+$/,'Span3',1500)"
                                                            runat="server" onkeypress="return checktextboxmaxlength(this,1500,event)" Height="60px"
                                                            Width="360px" TextMode="MultiLine" MaxLength="1500"></asp:TextBox>
                                                    </label>
                                                    <label>
                                                        <asp:Label runat="server" ID="Label24" Text="Max" CssClass="labelcount"></asp:Label>
                                                        <span id="Span3">1500</span>
                                                        <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 ) ( , . -)" CssClass="labelcount"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%;">
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="Penalty Points"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%;">
                                    <label>
                                        <asp:TextBox ID="txtpenaltypoint" onkeyup="return mak('Span1',5,this)" onkeypress="return RealNumWithDecimal(this,event);"
                                            runat="server" Width="100px" ValidationGroup="1" MaxLength="5"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label27" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="Span1">5</span>
                                        <asp:Label ID="lblkjsadfgh" runat="server" Text="(0-9)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%;">
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Applicable to Department"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%;">
                                    <asp:CheckBox ID="chkdepartment" runat="server" Text="All" OnCheckedChanged="chkdepartment_CheckedChanged"
                                        AutoPostBack="True" />
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Enforced by"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%;">
                                    <label>
                                        <asp:RadioButtonList ID="rblemforcedby" RepeatDirection="Horizontal" AutoPostBack="true"
                                            runat="server" OnSelectedIndexChanged="rblemforcedby_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Designation</asp:ListItem>
                                            <asp:ListItem Value="1">Employee</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnldesignation" runat="server" Width="100%" Visible="False">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%">
                                                    <label>
                                                        <asp:Label ID="Label9" runat="server" Text="Select Enforcing Designation"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:DropDownList ID="ddldesignation" runat="server" AutoPostBack="True">
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
                                    <asp:Panel ID="pnlemployee" runat="server" Width="100%" Visible="False">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%">
                                                    <label>
                                                        <asp:Label ID="Label10" runat="server" Text="Select Enforcing Employee"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>
                                                        <asp:DropDownList ID="ddlemployee" runat="server" AutoPostBack="True">
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
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text="  Effective Start Date"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtestartdate"
                                            Display="Dynamic" ErrorMessage="Invalid" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
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
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Effective End Date"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txteenddate"
                                            Display="Dynamic" ErrorMessage="Invalid" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
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
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Status"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%;">
                                    <asp:CheckBox ID="chkstatus" runat="server" Text="Active/Inactive" Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                    <asp:Button ID="btnsubmit" CssClass="btnSubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click"
                                        ValidationGroup="1" />
                                    <asp:Button ID="btnreset" runat="server" CssClass="btnSubmit" Text="Cancel" CausesValidation="false"
                                        OnClick="btnreset_Click" Visible="False" />
                                    <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btnSubmit" OnClick="btnupdate_Click"
                                        ValidationGroup="1" Visible="False" />
                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btnSubmit" CausesValidation="false"
                                        OnClick="btncancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllegend" Text="List of Rules"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btncancel0" CssClass="btnSubmit" runat="server" CausesValidation="false"
                            OnClick="btncancel0_Click" Text="Printable Version" />
                        <input id="Button7" runat="server" visible="false" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td colspan="4">
                                <label>
                                    <asp:Label runat="server" ID="Label15" Text="Business Name" Visible="true"></asp:Label>
                                    <asp:DropDownList ID="ddlfilterstore" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfilterstore_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label runat="server" ID="Label17" Text="Policy Category" Visible="true"></asp:Label>
                                    <asp:DropDownList ID="ddlfilterpolicyprocedure" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlfilterpolicyprocedure_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label runat="server" ID="Label16" Text="Policy Title" Visible="true"></asp:Label>
                                    <asp:DropDownList ID="ddlfilterpolicy" runat="server" OnSelectedIndexChanged="ddlfilterpolicy_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label runat="server" ID="Label18" Text="  Rule Title" Visible="true"></asp:Label>
                                    <asp:DropDownList ID="ddlfilterruletitle" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterruletitle_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 13%">
                                <label>
                                    <asp:Label runat="server" ID="Label19" Text="Show Latest Version" Visible="true"></asp:Label>
                                </label>
                            </td>
                            <td colspan="3" valign="top" style="width: 87%" align="left">
                                <asp:CheckBox ID="showlatest" runat="server" OnCheckedChanged="showlatest_CheckedChanged"
                                    AutoPostBack="True" Checked="True" />
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td colspan="2">
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblcmpny" Font-Italic="true" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label runat="server" ID="name" Font-Italic="true" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusiness" Font-Italic="true" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label11" Font-Italic="true" runat="server" Text="List of Rules"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="text-align: center; font-family: Calibri; font-size: 18px;">
                                                    <asp:Label ID="lblDepartmemnt" runat="server"></asp:Label>
                                                    <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                                    <asp:Label ID="lblEmp" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr style="vertical-align: top; height: 80%">
                                <td colspan="2">
                                    <cc11:PagingGridView ID="grid" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                        Width="100%" AllowSorting="True" CellPadding="4" CssClass="mGrid" GridLines="Both"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found."
                                        OnRowCommand="grid_RowCommand" OnRowEditing="grid_RowEditing" OnSelectedIndexChanging="grid_SelectedIndexChanging"
                                        OnRowDeleting="grid_RowDeleting" OnSorting="grid_Sorting" OnPageIndexChanging="grid_PageIndexChanging">
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Category" ItemStyle-Width="15%" SortExpression="Policyprocedurecategory"
                                                Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcategory" runat="server" Text='<%# Eval("Policyprocedurecategory")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rule Title" ItemStyle-Width="21%" SortExpression="RuleTitle"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblruletitle" runat="server" Text='<%# Eval("RuleTitle")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rule Name" ItemStyle-Width="30%" SortExpression="RuleName"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrRuleNamee" runat="server" Text='<%# Eval("RuleName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="RuleName" ItemStyle-Width="30%" HeaderText="Rule Name"
                                                HeaderStyle-HorizontalAlign="Left" SortExpression="RuleName">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="Penalty Point" ItemStyle-Width="10%" SortExpression="Penaltypoint"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbPenaltyPointee" runat="server" Text='<%# Eval("Penaltypoint")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="Penaltypoint" ItemStyle-Width="10%" HeaderText="Penalty Point"
                                                HeaderStyle-HorizontalAlign="Left" SortExpression="Penaltypoint">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="Version" ItemStyle-Width="10%" SortExpression="Version"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lVersionintee" runat="server" Text='<%# Eval("Version")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Version" HeaderText="Version" ItemStyle-Width="6%" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="Version">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="Status" SortExpression="Status" ItemStyle-Width="6%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id") %>'
                                                        ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:CommandField ShowEditButton="true" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif"
                                                HeaderText="Edit" ButtonType="Image" EditImageUrl="~/Account/images/edit.gif"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="4%">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:CommandField>--%>
                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="4%" Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                        CommandName="view" ToolTip="View Profile" Height="20px" ImageUrl="~/Account/images/viewprofile.jpg"
                                                        Width="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </cc11:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    </td> </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel21" runat="server" BackColor="White" BorderColor="#999999" Width="320px"
                                Height="160px" BorderStyle="Solid" BorderWidth="10px" ScrollBars="Vertical">
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                            <table style="width: 100%; font-weight: bold; color: #000000;" bgcolor="#CCCCCC">
                                                <tr>
                                                    <td>
                                                        Select Department
                                                    </td>
                                                    <td align="right">
                                                        <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                            OnClick="ImageButton3_Click" Width="15px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlof" ScrollBars="Vertical" runat="server" Height="250px">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="grddepartment" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                                AlternatingRowStyle-CssClass="alt" runat="server" AutoGenerateColumns="False"
                                                                DataKeyNames="id" Width="100%" EmptyDataText="No Record Found.">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Deparment Name" SortExpression="Departmentname" ItemStyle-Width="90%"
                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldepartmentname123" runat="server" Text='<%# Eval("Departmentname") %>'></asp:Label>
                                                                            <asp:Label ID="lbldepartmasterid" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="90%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Active" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkactive123" runat="server" Checked="true" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <asp:Button ID="btnsubmitpop1" CssClass="btnSubmit" runat="server" Text="Submit"
                                                OnClick="btnsubmitpop1_Click" />
                                            <asp:Button ID="btnupdatepop1" CssClass="btnSubmit" runat="server" Text="Update"
                                                Visible="False" ValidationGroup="1" OnClick="btnupdatepop1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtenderAddnew" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="Panel21" TargetControlID="HiddenButton222" X="700"
                                Y="270">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                        </td>
                    </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
