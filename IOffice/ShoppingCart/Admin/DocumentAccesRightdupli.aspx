<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" 
    AutoEventWireup="true" CodeFile="DocumentAccesRightdupli.aspx.cs" Inherits="Account_DocumentAccesRightdupli"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/Account/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
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
        .style1
        {
            width: 141px;
        }
        .mGridcss
        {
            width: 100%;
            background-color: #fff;
            margin: 2px 0 2px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-size: 13px !important;
        }
        .mGridcss a
        {
            font-size: 15px !important;
            color: White;
        }
        .mGridcss a:hover
        {
            font-size: 15px !important;
            color: White;
            text-decoration: underline;
        }
        .mGridcss td
        {
            padding: 0px;
            color: #717171;
        }
        .mGridcss th
        {
            padding: 0px 0px;
            color: #fff;
            background-color: #416271;
            font-size: 14px !important;
        }
        .mGridcss .alt
        {
            background: #fcfcfc url(grd_alt.png) repeat-x top;
        }
        .mGridcss .pgr
        {
            background-color: #416271;
        }
        .mGridcss .ftr
        {
            background-color: #416271;
            font-size: 15px !important;
            color: White;
        }
        .mGridcss .pgr table
        {
            margin: 5px 0;
        }
        .mGridcss .pgr td
        {
            border-width: 0;
            padding: 0 2px;
            font-weight: bold;
            color: #fff;
            line-height: 12px;
        }
        .mGridcss .pgr a
        {
            color: Gray;
            text-decoration: none;
        }
        .mGridcss .pgr a:hover
        {
            color: #000;
            text-decoration: none;
        }
        .mGridcss input[type="checkbox"]
        {
            margin-top: 5px !important;
            width: 10px !important;
            float: left !important;
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
        function ChangeCheckBoxState(id, checkState) {
            var cb = document.getElementById(id);
            if (cb != null)
                cb.checked = checkState;
        }
        // For Print
        function ChangeAllCheckBoxStatesPrint(checkState) {
            if (CheckBoxIDsPrint != null) {
                for (var i = 0; i < CheckBoxIDsPrint.length; i++)
                    ChangeCheckBoxState(CheckBoxIDsPrint[i], checkState);
            }
        }
        function ChangeHeaderAsNeededPrint() {
            if (CheckBoxIDsPrint != null) {
                for (var i = 1; i < CheckBoxIDsPrint.length; i++) {
                    var cb = document.getElementById(CheckBoxIDsPrint[i]);
                    if (!cb.checked) {
                        ChangeCheckBoxState(CheckBoxIDsPrint[0], false);
                        return;
                    }
                }
                ChangeCheckBoxState(CheckBoxIDsPrint[0], true);
            }
        }
        // For View
        function ChangeAllCheckBoxStatesView(checkState) {
            if (CheckBoxIDsView != null) {
                for (var i = 0; i < CheckBoxIDsView.length; i++)
                    ChangeCheckBoxState(CheckBoxIDsView[i], checkState);
            }
        }
        function ChangeHeaderAsNeededView() {
            if (CheckBoxIDsView != null) {
                for (var i = 1; i < CheckBoxIDsView.length; i++) {
                    var cb = document.getElementById(CheckBoxIDsView[i]);
                    if (!cb.checked) {
                        ChangeCheckBoxState(CheckBoxIDsView[0], false);
                        return;
                    }
                }
                ChangeCheckBoxState(CheckBoxIDsView[0], true);
            }
        }
        // For Delete
        function ChangeAllCheckBoxStatesDelete(checkState) {
            if (CheckBoxIDsDelete != null) {
                for (var i = 0; i < CheckBoxIDsDelete.length; i++)
                    ChangeCheckBoxState(CheckBoxIDsDelete[i], checkState);
            }
        }
        function ChangeHeaderAsNeededDelete() {
            if (CheckBoxIDsDelete != null) {
                for (var i = 1; i < CheckBoxIDsDelete.length; i++) {
                    var cb = document.getElementById(CheckBoxIDsDelete[i]);
                    if (!cb.checked) {
                        ChangeCheckBoxState(CheckBoxIDsDelete[0], false);
                        return;
                    }
                }
                ChangeCheckBoxState(CheckBoxIDsDelete[0], true);
            }
        }
        // For Save
        function ChangeAllCheckBoxStatesSave(checkState) {
            if (CheckBoxIDsSave != null) {
                for (var i = 0; i < CheckBoxIDsSave.length; i++)
                    ChangeCheckBoxState(CheckBoxIDsSave[i], checkState);
            }
        }
        function ChangeHeaderAsNeededSave() {
            if (CheckBoxIDsSave != null) {
                for (var i = 1; i < CheckBoxIDsSave.length; i++) {
                    var cb = document.getElementById(CheckBoxIDsSave[i]);
                    if (!cb.checked) {
                        ChangeCheckBoxState(CheckBoxIDsSave[0], false);
                        return;
                    }
                }
                ChangeCheckBoxState(CheckBoxIDsSave[0], true);
            }
        }

        // For Edit
        function ChangeAllCheckBoxStatesEdit(checkState) {
            if (CheckBoxIDsEdit != null) {
                for (var i = 0; i < CheckBoxIDsEdit.length; i++)
                    ChangeCheckBoxState(CheckBoxIDsEdit[i], checkState);
            }
        }
        function ChangeHeaderAsNeededEdit() {
            if (CheckBoxIDsEdit != null) {
                for (var i = 1; i < CheckBoxIDsEdit.length; i++) {
                    var cb = document.getElementById(CheckBoxIDsEdit[i]);
                    if (!cb.checked) {
                        ChangeCheckBoxState(CheckBoxIDsEdit[0], false);
                        return;
                    }
                }
                ChangeCheckBoxState(CheckBoxIDsEdit[0], true);
            }
        }
        // For Email
        function ChangeAllCheckBoxStatesEmail(checkState) {
            if (CheckBoxIDsEmail != null) {
                for (var i = 0; i < CheckBoxIDsEmail.length; i++)
                    ChangeCheckBoxState(CheckBoxIDsEmail[i], checkState);
            }
        }
        function ChangeHeaderAsNeededEmail() {
            if (CheckBoxIDsEmail != null) {
                for (var i = 1; i < CheckBoxIDsEmail.length; i++) {
                    var cb = document.getElementById(CheckBoxIDsEmail[i]);
                    if (!cb.checked) {
                        ChangeCheckBoxState(CheckBoxIDsEmail[0], false);
                        return;
                    }
                }
                ChangeCheckBoxState(CheckBoxIDsEmail[0], true);
            }
        }
        // For Fax
        function ChangeAllCheckBoxStatesFax(checkState) {
            if (CheckBoxIDsFax != null) {
                for (var i = 0; i < CheckBoxIDsFax.length; i++)
                    ChangeCheckBoxState(CheckBoxIDsFax[i], checkState);
            }
        }
        function ChangeHeaderAsNeededFax() {
            if (CheckBoxIDsFax != null) {
                for (var i = 1; i < CheckBoxIDsFax.length; i++) {
                    var cb = document.getElementById(CheckBoxIDsFax[i]);
                    if (!cb.checked) {
                        ChangeCheckBoxState(CheckBoxIDsFax[0], false);
                        return;
                    }
                }
                ChangeCheckBoxState(CheckBoxIDsFax[0], true);
            }
        }
        // For Mesage
        function ChangeAllCheckBoxStateMsg(checkState) {
            if (CheckBoxIDsMsg != null) {
                for (var i = 0; i < CheckBoxIDsMsg.length; i++)
                    ChangeCheckBoxState(CheckBoxIDsMsg[i], checkState);
            }
        }
        function ChangeHeaderAsNeededMsg() {
            if (CheckBoxIDsMsg != null) {
                for (var i = 1; i < CheckBoxIDsMsg.length; i++) {
                    var cb = document.getElementById(CheckBoxIDsMsg[i]);
                    if (!cb.checked) {
                        ChangeCheckBoxState(CheckBoxIDsMsg[0], false);
                        return;
                    }
                }
                ChangeCheckBoxState(CheckBoxIDsMsg[0], true);
            }
        }
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
        }
        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186 || evt.keyCode == 59) {


                alert("You have entered an invalid character");
                return false;
            }




        }
        function check(txt1, regex, reg, id, max_len) {
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

  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div  style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend></legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 2%;">
                                <label>
                                    1)</label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Select the designation for which you wish to set access rights"></asp:Label></label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="lbldoyou" runat="server" Text="Business Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label10" runat="server" Text="Select Department:designation"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddldeptname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldeptname_SelectedIndexChanged"
                                                    ValidationGroup="1">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlrule2" runat="server" Visible="false">
                                    <table width="100%" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td style="width: 2%;">
                                                <label>
                                                    2)
                                                </label>
                                            </td>
                                            <td valign="top">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="lbldocset1" runat="server" Text="Do you wish to give Full Access rights for "></asp:Label>
                                                                <asp:Label ID="lblallcab" runat="server" Text=" ALL cabinets " ForeColor="Red"></asp:Label>
                                                                <asp:Label ID="lblfor" runat="server" Text="for "></asp:Label>
                                                                <asp:Label ID="lblallbus" runat="server" Text=" ALL businesses " ForeColor="Red"></asp:Label>
                                                                <asp:Label ID="lblsel" runat="server" Text="to the "></asp:Label>
                                                                 <asp:Label ID="lblseldes" runat="server" Text=" selected designation" ForeColor="Red"></asp:Label>
                                                            </label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rdbus" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                                OnSelectedIndexChanged="rdbus_SelectedIndexChanged">
                                                                <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                                <asp:ListItem Value="0" Selected="True" Text="No, I wish to grant access  rights for selected business only."></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlallbus" runat="server" Visible="false">
                                                    <fieldset>
                                                        <legend>
                                                            <asp:Label ID="lblr1edit" runat="server" Text="Select the level of access rights for all the documents in all cabinets of all businesses"></asp:Label>
                                                        </legend>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="width: 10%;">
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkfaxabus" runat="server" Checked="true" Visible="false" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkviewabus" runat="server" Visible="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkdeleteabus" runat="server" Visible="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chksaveabus" runat="server" Visible="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkeditabus" runat="server" Visible="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkemailabus" runat="server" Visible="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkMessageabus" runat="server" Visible="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%;">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label14" runat="server" Text="View"></asp:Label></label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label15" runat="server" Text="Delete"></asp:Label></label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label16" runat="server" Text="Save"></asp:Label></label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label17" runat="server" Text="Edit"></asp:Label></label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label18" runat="server" Text="Attach to E-Mail"></asp:Label></label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label19" runat="server" Text="Attach to Message"></asp:Label>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset></asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnl1dis" runat="server" Visible="false">
                                    <table width="100%" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td style="width: 2%;">
                                                <label>
                                                    3)</label>
                                            </td>
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td colspan="1">
                                                                        <label>
                                                                            <asp:Label ID="Label7" runat="server" Text="Select business to set access right "></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:DropDownList ID="ddlbussele" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbussele_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Label ID="Label9" runat="server" Text="Access To" Visible="false"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:DropDownList ID="ddlselection" runat="server" AutoPostBack="True" Visible="false"
                                                                                OnSelectedIndexChanged="ddlbussele_SelectedIndexChanged" Width="100px">
                                                                                <asp:ListItem Value="0" Text="Unselected"></asp:ListItem>
                                                                                <asp:ListItem Value="1" Selected="True" Text="Entire"></asp:ListItem>
                                                                                <%-- <asp:ListItem Value="2" Text="Partial"></asp:ListItem>--%>
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <fieldset>
                                                    <legend>
                                                        <asp:Label ID="lbllegend" runat="server" Text="Set following rights to "></asp:Label>
                                                        <asp:Label ID="lbldeshead" runat="server" Text=" selected designation" ForeColor="Red"></asp:Label>
                                                    </legend>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <table cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label3" runat="server" Text="1) Do you wish to give Full Access rights for "></asp:Label>
                                                                                <asp:Label ID="Label21" runat="server" Text=" All cabinets " ForeColor="Red"></asp:Label>
                                                                                <asp:Label ID="Label22" runat="server" Text="for "></asp:Label>
                                                                                <asp:Label ID="Label23" runat="server" Text=" selected business " ForeColor="Red"></asp:Label>
                                                                                <asp:Label ID="Label24" runat="server" Text="to the "></asp:Label>
                                                                                 <asp:Label ID="Label25" runat="server" Text=" selected designation" ForeColor="Red"></asp:Label>
                                                                            </label>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rdsiglebus" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                                                OnSelectedIndexChanged="rdsiglebus_SelectedIndexChanged">
                                                                                <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                                                <asp:ListItem Value="0" Text="No, I wish to grant access  rights for selected cabinet only." Selected="True"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td colspan="2">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                          <tr>
                                                                                        <td>
                                                                                            <label>
                                                                                                <asp:Label ID="lblserd" runat="server" Text="  Full Access Rights for Entire Filing System ( All existing and Future Filing Cabinets of the selected Business)"></asp:Label>
                                                                                            </label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <label>
                                                                                                <asp:DropDownList ID="rdallcab" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                                                                    OnSelectedIndexChanged="rdallcab_SelectedIndexChanged" Visible="False">
                                                                                                    <%--<asp:ListItem Value="1" Selected="True" Text="For All Cabinet"></asp:ListItem>--%>
                                                                                                    <asp:ListItem Value="2" Text="For Selected Cabinet "></asp:ListItem>
                                                                                                    <asp:ListItem Value="0" Text="No cabinets"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div style="float: right;">
                                                                                                <label>
                                                                                                    <asp:Button ID="btnedit" runat="server" CssClass="btnSubmit" Text="View" OnClick="btnedit_Click"
                                                                                                        Visible="false" />
                                                                                                </label>
                                                                                                <label>
                                                                                                    <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" OnClick="Button3_Click"
                                                                                                        Visible="false" Text="Printable Version" />
                                                                                                </label>
                                                                                                <label>
                                                                                                    <input id="Button1" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                                                                                        type="button" value="Print" visible="false" /></label>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                        </table>
                                                                        </td>
                                                                        </tr>
                                                                     
                                                                </table>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                                            <td>
                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Panel ID="pnlallcab" runat="server">
                                                                                                <fieldset>
                                                                                                    <legend>
                                                                                                        <asp:Label ID="lblrulca" runat="server" Text="Select the level of access rights for all the documents in selected business"></asp:Label>
                                                                                                    </legend>
                                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td style="width: 10%;">
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:CheckBox ID="chkfaxcab" runat="server" Checked="true" Text="Fax" Visible="false" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:CheckBox ID="chkviewcab" runat="server" Text="" Visible="true" Checked="True" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:CheckBox ID="chkdeletecab" runat="server" Text="" Visible="true" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:CheckBox ID="chksavecab" runat="server" Text="" Visible="true" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:CheckBox ID="chkeditcab" runat="server" Text="" Visible="true" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:CheckBox ID="chkmailcab" runat="server" Text="" Visible="true" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:CheckBox ID="chkmessagecab" runat="server" Text="" Visible="true" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="width: 10%;">
                                                                                                            </td>
                                                                                                            <td>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <label>
                                                                                                                    <asp:Label ID="Label4" runat="server" Text="View"></asp:Label></label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <label>
                                                                                                                    <asp:Label ID="Label5" runat="server" Text="Delete"></asp:Label></label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <label>
                                                                                                                    <asp:Label ID="Label8" runat="server" Text="Save"></asp:Label></label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <label>
                                                                                                                    <asp:Label ID="Label11" runat="server" Text="Edit"></asp:Label></label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <label>
                                                                                                                    <asp:Label ID="Label13" runat="server" Text="Attach to E-Mail"></asp:Label></label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <label>
                                                                                                                    <asp:Label ID="Label20" runat="server" Text="Attach to Message"></asp:Label>
                                                                                                                </label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </fieldset></asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="pnlrightd" runat="server" Visible="False">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                       
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Panel ID="pnlgrid" runat="server" Width="100%" Visible="true">
                                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                                        <tr align="center">
                                                                                            <td>
                                                                                                <div id="mydiv" class="closed">
                                                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                                                                <asp:Label ID="lblcomname" runat="server" Font-Bold="True" Font-Italic="true" Font-Size="20px"
                                                                                                                    Visible="false"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                                                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Italic="true" Font-Size="20px"
                                                                                                                    Text="Business:"></asp:Label>
                                                                                                                <asp:Label ID="lblBusiness" runat="server" Font-Bold="True" Font-Italic="true" Font-Size="20px"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <asp:Label ID="lblmaa" runat="server" Font-Bold="True" Font-Italic="true" Font-Size="16px"
                                                                                                                    Text="List of Documnet Access Rights "></asp:Label>
                                                                                                                <asp:Label ID="lblvssh" runat="server" Font-Bold="True" Font-Italic="true" Font-Size="16px"
                                                                                                                    Text=""></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left">
                                                                                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Italic="true" Font-Size="16px"
                                                                                                                    Text="Department : Designation -"></asp:Label>
                                                                                                                <asp:Label ID="lblDepartment" runat="server" Font-Bold="True" Font-Italic="true"
                                                                                                                    Font-Size="16px"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left">
                                                                                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="true" Font-Size="16px"
                                                                                                                    Text="Handling Rights of Following Cabinet Drawer Folder for "></asp:Label>
                                                                                                                <asp:Label ID="lblappbus" runat="server" Font-Bold="True" Font-Italic="true" Font-Size="16px"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <br />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <asp:Panel ID="pnlfilec" runat="server" Visible="true">
                                                                                                    <asp:GridView ID="grdfillcab" runat="server" AllowPaging="false" AllowSorting="True"
                                                                                                       AutoGenerateColumns="False" CssClass="mGrid"
                                                                                                        DataKeyNames="DocumentMainTypeId" EmptyDataRowStyle-HorizontalAlign="Left" EmptyDataText="No Record Found."
                                                                                                        PagerStyle-CssClass="pgr" ShowHeader="false" Visible="True" Width="100%" BackColor="#f1f8f8" >
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Cabinet" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="25%" ItemStyle-Width="25%"
                                                                                                                Visible="true">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblcab" runat="server" Text='<%# DataBinder.Eval( Container.DataItem, "DocumentMainType")%>' ForeColor="Black"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="All" ItemStyle-HorizontalAlign="Left" 
                                                                                                                Visible="true">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:DropDownList ID="rdcab" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                                                                                        OnSelectedIndexChanged="rdcab_SelectedIndexChanged" Width="375px">
                                                                                                                        <asp:ListItem Value="1" Text="Set same rights on all drawers in this cabinet"></asp:ListItem>
                                                                                                                        <asp:ListItem Value="2" Text="Set separate rights for selected drawers in this cabinet"></asp:ListItem>
                                                                                                                        <asp:ListItem Value="0"  Selected="True" Text="Select"></asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </ItemTemplate>
                                                                                                           </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="View" ItemStyle-HorizontalAlign="Left"
                                                                                                                ItemStyle-Width="7%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblviw" runat="server" Text="View" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                    <asp:CheckBox ID="chkview" runat="server" Checked="true" Visible="false" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Delete" ItemStyle-HorizontalAlign="Left" 
                                                                                                                ItemStyle-Width="8%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lbldel" runat="server" Text="Delete" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                    <asp:CheckBox ID="chkdelete" runat="server" Visible="false" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Save" ItemStyle-HorizontalAlign="Left"
                                                                                                                ItemStyle-Width="11%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblsave" runat="server" Text="Copy/Move" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                    <asp:CheckBox ID="chksave" runat="server" Visible="false" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Edit" ItemStyle-HorizontalAlign="Left"
                                                                                                                ItemStyle-Width="7%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lbledit" runat="server" Text="Edit" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                    <asp:CheckBox ID="chkedit" runat="server" Visible="false" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Email" ItemStyle-HorizontalAlign="Left"
                                                                                                                ItemStyle-Width="8%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblema" runat="server" Text="Mail" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                    <asp:CheckBox ID="chkemail" runat="server" Visible="false" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Message" ItemStyle-HorizontalAlign="Left"
                                                                                                                ItemStyle-Width="8%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblmess" runat="server" Text="Message" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                    <asp:CheckBox ID="chkMessage" runat="server" Visible="false" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField Visible="true">
                                                                                                                <ItemTemplate>
                                                                                                                    <tr>
                                                                                                                        <td colspan="8">
                                                                                                                            <table border="0" width="100%">
                                                                                                                                <tr>
                                                                                                                                    <td style="width: 1%;">
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <asp:GridView ID="grddrower" runat="server" AllowPaging="false" 
                                                                                                                                            PagerStyle-CssClass="pgr" AutoGenerateColumns="False" BorderWidth="0" CssClass="mGrid"
                                                                                                                                            DataKeyNames="DocumentSubTypeId" EmptyDataRowStyle-HorizontalAlign="Left" ShowHeader="false" BackColor="#e2f1f1">
                                                                                                                                            <Columns>
                                                                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Drawer" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%" ItemStyle-Width="30%"
                                                                                                                                                    Visible="true">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="pnlcab" runat="server" Text='<%# Bind("DocumentSubType") %>' ForeColor="Black"></asp:Label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="All" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                    Visible="true" >
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:DropDownList ID="rddrow" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                                                                                                                            OnSelectedIndexChanged="rddrow_SelectedIndexChanged" Width="375px">
                                                                                                                                                            <asp:ListItem Value="1" Text="Set same rights on all folders in this drawers"></asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="2" Text="Set separate rights for selected folders in this drawers"></asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="0" Selected="True"  Text="Select"></asp:ListItem>
                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="All" ItemStyle-HorizontalAlign="Left"
                                                                                                                                    Visible="true">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblcabAll" runat="server" Text="Full Drawer"></asp:Label>
                                                                                                                                        <asp:CheckBox ID="chkfax" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkAll2_chachedChanged" />
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>--%>
                                                                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="View" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                    ItemStyle-Width="7%">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lblviw" runat="server" Text="View" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                                                        <asp:CheckBox ID="chkview" runat="server" Checked="true" Visible="false" />
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Delete" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                    ItemStyle-Width="8%">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lbldel" runat="server" Text="Delete" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                                                        <asp:CheckBox ID="chkdelete" runat="server" Visible="false" />
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Save" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                    ItemStyle-Width="11%">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lblsave" runat="server" Text="Copy/Move" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                                                        <asp:CheckBox ID="chksave" runat="server" Visible="false" />
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Edit" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                    ItemStyle-Width="7%">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lbledit" runat="server" Text="Edit" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                                                        <asp:CheckBox ID="chkedit" runat="server" Visible="false" />
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Email" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                    ItemStyle-Width="8%">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lblema" runat="server" Text="Mail" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                                                        <asp:CheckBox ID="chkemail" runat="server" Visible="false" />
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Message" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                    ItemStyle-Width="8%">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lblmess" runat="server" Text="Message" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                                                        <asp:CheckBox ID="chkMessage" runat="server" Visible="false" />
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField>
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <tr>
                                                                                                                                                            <td colspan="8">
                                                                                                                                                                <table border="0" width="100%">
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td style="width: 1%;">
                                                                                                                                                                        </td>
                                                                                                                                                                        <td>
                                                                                                                                                                            <asp:GridView ID="grddfolder" runat="server" AllowPaging="false"
                                                                                                                                                                                PagerStyle-CssClass="pgr" AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="DocumentTypeId" BackColor="#e9edea"
                                                                                                                                                                                EmptyDataRowStyle-HorizontalAlign="Left" GridLines="Both" ShowHeader="false">
                                                                                                                                                                                <Columns>
                                                                                                                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Folder" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="35%" ItemStyle-Width="35%"
                                                                                                                                                                                        Visible="true">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <asp:Label ID="pnlcab" runat="server" Text='<%# Bind("DocumentType") %>' ForeColor="Black"></asp:Label>
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="All" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                                                        Visible="true" >
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <asp:DropDownList ID="rdfolder" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                                                                                                                                                                OnSelectedIndexChanged="rdfolder_SelectedIndexChanged">
                                                                                                                                                                                                <asp:ListItem Value="1"  Text="Set rights"></asp:ListItem>
                                                                                                                                                                                                <asp:ListItem Value="0" Selected="True" Text="Select"></asp:ListItem>
                                                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                   
                                                                                                                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="View" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                                                        ItemStyle-Width="7%">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <asp:Label ID="lblviw" runat="server" Text="View" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                                                                                            <asp:CheckBox ID="chkview" runat="server" Checked="false" Visible="false" />
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Delete" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                                                        ItemStyle-Width="8%">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <asp:Label ID="lbldel" runat="server" Text="Delete" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                                                                                            <asp:CheckBox ID="chkdelete" runat="server" Checked="false" Visible="false" />
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Save" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                                                        ItemStyle-Width="11%">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <asp:Label ID="lblsave" runat="server" Text="Copy/Move" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                                                                                            <asp:CheckBox ID="chksave" runat="server" Checked="false" Visible="false" />
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Edit" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                                                        ItemStyle-Width="7%">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <asp:Label ID="lbledit" runat="server" Text="Edit" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                                                                                            <asp:CheckBox ID="chkedit" runat="server" Checked="false" Visible="false" />
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Email" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                                                        ItemStyle-Width="8%">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <asp:Label ID="lblema" runat="server" Text="Mail" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                                                                                            <asp:CheckBox ID="chkemail" runat="server" Checked="false" Visible="false" />
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Message" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                                                        ItemStyle-Width="8%">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <asp:Label ID="lblmess" runat="server" Text="Message" Visible="false" ForeColor="Black"></asp:Label>
                                                                                                                                                                                            <asp:CheckBox ID="chkMessage" runat="server" Checked="false" Visible="false" />
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                </Columns>
                                                                                                                                                                            </asp:GridView>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                            </Columns>
                                                                                                                                        </asp:GridView>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
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
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="imgbtnsubmit" runat="server" CssClass="btnSubmit" OnClick="imgbtnsubmit_Click"
                                    Text="Save" ValidationGroup="1" Visible="False" />
                                <asp:Button ID="imgbtnreset" runat="server" CausesValidation="False" CssClass="btnSubmit"
                                    OnClick="imgbtnreset_Click" Text="Edit" Visible="false" />
                                <asp:Button ID="btnup" runat="server" CausesValidation="False" CssClass="btnSubmit"
                                    Text="Update" Visible="false"/>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td style="width: 20%">
                            </td>
                            <td style="width: 80%" align="left">
                                <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
