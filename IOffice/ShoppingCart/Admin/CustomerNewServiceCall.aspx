<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="CustomerNewServiceCall.aspx.cs" Inherits="ShoppingCart_Admin_CustomerNewServiceCall"
    Title="CustomerNewServiceCall" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        function mask(evt) {

            if (evt.keyCode == 13) {

                return true;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
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
        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }

    </script>

    <script type="text/javascript" language="javascript">

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
            function ChangeCheckBoxState(id, checkState) {
                var cb = document.getElementById(id);
                if (cb != null)
                    cb.checked = checkState;
            }
            // For Document
            function ChangeAllCheckBoxStates(checkState) {
                if (CheckBoxIDs != null) {
                    for (var i = 0; i < CheckBoxIDs.length; i++)
                        ChangeCheckBoxState(CheckBoxIDs[i], checkState);
                }
            }
            function ChangeHeaderAsNeeded() {
                if (CheckBoxIDs != null) {
                    for (var i = 1; i < CheckBoxIDs.length; i++) {
                        var cb = document.getElementById(CheckBoxIDs[i]);
                        if (!cb.checked) {
                            ChangeCheckBoxState(CheckBoxIDs[0], false);
                            return;
                        }
                    }
                    ChangeCheckBoxState(CheckBoxIDs[0], true);
                }
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <div style="padding-left: 0%">
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="btnviewservicelog" runat="server" CssClass="btnSubmit" Text="View Service Call Log"
                                OnClick="btnviewservicelog_Click" />
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="addinventorysite" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 20%" >
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="Reference ID"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 80%">
                                    <label>
                                       
                                        <asp:Label ID="TextBox1" runat="server"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" >
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Entry Date"></asp:Label>
                                        <asp:Label ID="Label15" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtEntryDate"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 80%">
                                    <label>
                                        <asp:TextBox ID="TxtEntryDate" runat="server" Width="80px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/cal_btn.jpg" />
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                                            TargetControlID="TxtEntryDate">
                                        </cc1:CalendarExtender>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" >
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 80%">
                                    <label>
                                        <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" >
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Customer Name"></asp:Label>
                                        <asp:Label ID="Label16" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUserMaster"
                                            Display="Dynamic" ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 80%">
                                    <label>
                                        <asp:DropDownList ID="ddlUserMaster" runat="server">
                                        </asp:DropDownList>
                                       
                                        <asp:LinkButton ID="LinkButton2" runat="server" Text="Add New" Visible="false"></asp:LinkButton>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgadddept" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                            OnClick="LinkButton97666667_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                    </label>
                                    <label>
                                        &nbsp;<asp:ImageButton ID="imgdeptrefresh" runat="server" AlternateText="Refresh"
                                            Height="20px" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton13_Click"
                                            ToolTip="Refresh" Width="20px" ImageAlign="Bottom" />
                                    </label>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="pnlpopup" TargetControlID="LinkButton2">
                                    </cc1:ModalPopupExtender>
                                </td>
                            </tr>
                              <tr>
                                <td style="width: 20%" >
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text="Problem Type"></asp:Label>
                                        <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlProbType"
                                            Display="Dynamic" ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 80%">
                                    <label>
                                        <asp:DropDownList ID="ddlProbType" runat="server" OnSelectedIndexChanged="ddlProbType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                     <label>
                                        <asp:ImageButton ID="imgproblemtypeaddnew" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                            OnClick="LinkButtonproblemtypeaddnew_Click" ToolTip="Add New" Width="20px" ImageAlign="Bottom" />
                                    </label>
                                    <label>
                                        &nbsp;<asp:ImageButton ID="imgproblemtyperefresh" runat="server" AlternateText="Refresh"
                                            Height="20px" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButtonproblemtyperefreshs_Click"
                                            ToolTip="Refresh" Width="20px" ImageAlign="Bottom" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" >
                                    <label>
                                        <asp:Label ID="Label10" runat="server" Text="Problem Title"></asp:Label>
                                        <asp:Label ID="Label17" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtProTitle"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            
                                        <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="Invalid Character"
                                             SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtProTitle" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 80%">
                                    <label>
                                        <asp:TextBox ID="txtProTitle" runat="server" ValidationGroup="1" MaxLength="60" Width="490px"
                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',60)">
                                        </asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label20" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" class="labelcount">60</span>
                                        <asp:Label ID="Label14" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" >
                                    <label>
                                        <asp:Label ID="Label11" runat="server" Text="Problem Description"></asp:Label>
                                        <asp:Label ID="Label18" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtProbDesc"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                             SetFocusOnError="True" ValidationExpression="^([_a-zA-Z.0-9\s]*)"
                                            ControlToValidate="txtProbDesc" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                        
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*"
                                             SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1000})$"
                                            ControlToValidate="txtProbDesc" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 80%">
                                    <label>
                                        <asp:TextBox ID="txtProbDesc" runat="server"  MaxLength="1000" TextMode="MultiLine"
                                         Height="69px"   Width="490px" onkeypress="return checktextboxmaxlength(this,1000,event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z.A-Z0-9_\s]+$/,'Span1',1000)">
                                         </asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label21" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" class="labelcount">1000</span>
                                        <asp:Label ID="Label9" runat="server" Text="(A-Z 0-9 _ .)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                          
                            <tr>
                                <td style="width: 20%" align="right">
                                    <label>
                                        <asp:ImageButton ID="imgbtnAddNewUser" runat="server" ImageUrl="~/ShoppingCart/images/addnew.png"
                                            OnClick="imgbtnAddNewUser_Click" Visible="False" />
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                                            PopupControlID="pnlpopup" TargetControlID="imgbtnAddNewUser">
                                        </cc1:ModalPopupExtender>
                                    </label>
                                </td>
                                <td style="width: 80%">
                                    <label>
                                        <asp:Button ID="ImageButton48" runat="server" CssClass="btnSubmit" Text="Submit"
                                            OnClick="ImageButton48_Click" ValidationGroup="1" />
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlpopup" Visible="false" runat="server" BorderColor="#C0C0FF" Width="500px"
                        BorderStyle="Outset">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <table width="500" cellpadding="0" cellspacing="0" id="subinnertbl">
                                    <tr>
                                        <td style="font-size: x-small; color: black; font-family: Verdana, Arial; background-color: white">
                                        </td>
                                        <td style="color: black; font-family: Verdana, Arial; background-color: white; text-align: right">
                                        </td>
                                        <td style="color: black; font-family: Verdana, Arial; background-color: white; text-align: right">
                                            <asp:ImageButton ID="imgbtn4" OnClick="imgbtn4_Click" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; color: black; font-family: Verdana, Arial; background-color: white">
                                        </td>
                                        <td style="color: black; font-family: Verdana, Arial; background-color: white">
                                        </td>
                                        <td style="color: black; font-family: Verdana, Arial; background-color: white">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="subinnertblfc" colspan="2">
                                            New Customer
                                        </td>
                                        <td class="subinnertblfc" colspan="1">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" colspan="3">
                                            <asp:Label ID="Label3" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col1">
                                            Email:
                                        </td>
                                        <td style="color: black; font-family: Verdana, Arial; background-color: white" class="col2"
                                            colspan="2">
                                            <asp:TextBox ID="txtemail" runat="server" CssClass="TextBox"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Shoppingcart/images/CheckAvailability1.png"
                                                OnClick="ImageButton10_Click" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtemail"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail"
                                                ErrorMessage="Invalid Email" Font-Bold="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col1">
                                        </td>
                                        <td style="font-weight: bold; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col2" colspan="2">
                                            <asp:Label ID="Label4" runat="server"></asp:Label>
                                            <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="This email id is already registered with us as a member if you have forgotten your password    "
                                                Visible="False"></asp:Label><asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/ShoppingCart/ForgotPassword.aspx"
                                                    Visible="False" OnClick="LinkButton4_Click">Click Here</asp:LinkButton>
                                            <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="  or you may try your 10 digit phone no. as password (Ex-1234567890)"
                                                Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col1">
                                            Password:
                                        </td>
                                        <td style="font-weight: bold; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col2">
                                            <asp:TextBox ID="txtpassword" runat="server" CssClass="TextBox" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtpassword"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="col2" style="font-weight: bold; color: black; font-family: Verdana, Arial;
                                            background-color: white">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col1">
                                            Confirm Password:
                                        </td>
                                        <td style="font-weight: bold; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col2" colspan="2">
                                            <asp:TextBox ID="txtconfirmpwd" runat="server" CssClass="TextBox" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtconfirmpwd"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtpassword"
                                                ControlToValidate="txtconfirmpwd" ErrorMessage="Please Enter the Same Password."
                                                ValidationGroup="1"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col1">
                                            Store Name:
                                        </td>
                                        <td style="font-weight: bold; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col2" colspan="2">
                                            <asp:DropDownList ID="ddlstorename" runat="server" Width="151px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlstorename"
                                                ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col1">
                                            First Name:&nbsp;
                                        </td>
                                        <td style="font-weight: bold; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col2">
                                            <asp:TextBox ID="txtfirstname" runat="server" CssClass="TextBox"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="txtfirstname"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="col2" style="font-weight: bold; color: black; font-family: Verdana, Arial;
                                            background-color: white">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col1">
                                            Last Name:&nbsp;
                                        </td>
                                        <td style="font-weight: bold; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col2" colspan="2">
                                            <asp:TextBox ID="txtlastname" runat="server" CssClass="TextBox"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ControlToValidate="txtlastname"
                                                Display="None" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="font-size: x-small; color: black; font-family: Verdana, Arial;
                                            background-color: white" valign="top" class="col1">
                                            Address:<br />
                                        </td>
                                        <td style="font-weight: bold; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col2">
                                            <asp:TextBox ID="txtaddress" runat="server" CssClass="TextBox" Height="50px" TextMode="MultiLine"
                                                Width="126px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ControlToValidate="txtaddress"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="col2" style="font-weight: bold; color: black; font-family: Verdana, Arial;
                                            background-color: white">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="font-size: x-small; color: black; font-family: Verdana, Arial;
                                            background-color: white" valign="top" class="col1">
                                            Country:
                                        </td>
                                        <td style="font-weight: bold; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col2">
                                            <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="True" Height="22px"
                                                OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged1" Style="font-weight: normal;
                                                font-family: Arial" Width="151px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ControlToValidate="ddlcountry"
                                                ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="col2" style="font-weight: bold; color: black; font-family: Verdana, Arial;
                                            background-color: white">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="font-size: x-small; color: black; font-family: Verdana, Arial;
                                            background-color: white" valign="top" class="col1">
                                            State:
                                        </td>
                                        <td style="font-weight: bold; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col2">
                                            <asp:DropDownList ID="ddlstate" runat="server" Height="24px" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"
                                                Style="font-weight: normal; font-family: Arial" Width="152px" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ControlToValidate="ddlstate"
                                                ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="col2" style="font-weight: bold; color: black; font-family: Verdana, Arial;
                                            background-color: white">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="font-size: x-small; color: black; font-family: Verdana, Arial;
                                            background-color: white" valign="top" class="col1">
                                            City:
                                        </td>
                                        <td style="font-weight: bold; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col2">
                                            <asp:DropDownList ID="ddlcity" runat="server" Height="26px" Style="font-weight: normal;
                                                font-size: 8pt; font-family: Arial" Width="152px" OnSelectedIndexChanged="ddlcity_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="ddlcity"
                                                ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtcity" runat="server" CssClass="TextBox" Visible="False"></asp:TextBox>
                                        </td>
                                        <td class="col2" style="font-weight: bold; color: black; font-family: Verdana, Arial;
                                            background-color: white">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="font-size: x-small; color: black; font-family: Verdana, Arial;
                                            background-color: white" valign="top" class="col1">
                                            Phone:
                                        </td>
                                        <td style="font-weight: bold; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col2" colspan="2">
                                            <asp:TextBox ID="txtphoneno" runat="server" CssClass="TextBox"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtphoneno"
                                                ErrorMessage="*" ValidationGroup="1" Width="7px"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtphoneno"
                                                ErrorMessage="Enter only Number" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="font-size: x-small; color: black; font-family: Verdana, Arial;
                                            background-color: white" valign="top" class="col1">
                                            Fax:
                                        </td>
                                        <td style="font-weight: bold; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col2" colspan="2">
                                            <asp:TextBox ID="txtfax" runat="server" CssClass="TextBox"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtfax"
                                                ErrorMessage="Enter only Number" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="font-size: x-small; color: black; font-family: Verdana, Arial;
                                            background-color: white" valign="top" class="col1">
                                            Zip Code:
                                        </td>
                                        <td style="font-weight: bold; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col2">
                                            <asp:TextBox ID="txtzip" runat="server" CssClass="TextBox"></asp:TextBox>
                                        </td>
                                        <td class="col2" style="font-weight: bold; color: black; font-family: Verdana, Arial;
                                            background-color: white">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="font-size: x-small; color: black; font-family: Verdana, Arial;
                                            background-color: white" valign="top" class="col1">
                                            Barcode No.:
                                        </td>
                                        <td style="font-weight: bold; color: black; font-family: Verdana, Arial; background-color: white"
                                            class="col2">
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="TextBox"></asp:TextBox>
                                        </td>
                                        <td class="col2" style="font-weight: bold; color: black; font-family: Verdana, Arial;
                                            background-color: white">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="font-weight: bold; color: black; font-family: Verdana, Arial;
                                            background-color: white" valign="top">
                                            <asp:CheckBox ID="CheckBox1" runat="server" ForeColor="Red" Text="I Accept " OnCheckedChanged="CheckBox1_CheckedChanged" />
                                            <asp:LinkButton ID="LinkButton10" runat="server" ForeColor="Blue">Terms & Condition</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <%--<td align="center" colspan="3" style="font-weight: bold; color: black; font-family: Verdana, Arial;
                            background-color: white" valign="top">--%>
                                        <%--<asp:ImageButton ID="ImageButton20" runat="server" ImageUrl="~/Shoppingcart/images/Register1.png"
                                OnClick="RegisterBtn_Click" ValidationGroup="1" />--%>
                                        <%--<asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Shoppingcart/images/Register1.png"
                                 ValidationGroup="1" CausesValidation="False" 
                                onclick="ImageButton6_Click" />
                        </td>--%>
                                        <td align="center" colspan="3" style="font-weight: bold; color: black; font-family: Verdana, Arial;
                                            background-color: white" valign="top">
                                            <asp:ImageButton ID="ImageButton20" runat="server" ImageUrl="~/ShoppingCart/images/Register1.png"
                                                OnClick="RegisterBtn_Click" ValidationGroup="1" CausesValidation="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="font-weight: bold; color: black; font-family: Verdana, Arial;
                                            background-color: white" valign="top">
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
