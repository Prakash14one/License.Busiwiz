<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="ExpenseInvoice.aspx.cs" Inherits="ExpenseInvoice"
    Title="Expense Invoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        function Test() {
            alert("In");
        }
        function MadeAddition(myfield, e) {
            var qty = document.getElementById("ctl00$ContentPlaceHolder1$txtqty").value;
            var sprice = document.getElementById("ctl00$ContentPlaceHolder1$txtsalesprice").value;
            var TotalC;
            TotalC = qty * sprice;
            document.getElementById("ctl00_ContentPlaceHolder1_lblAmount").innerHTML = TotalC;
        }
        function MadeDeduction(myfield, e) {
            //alert("IN") ;
            var Tax1 = document.getElementById("ctl00$ContentPlaceHolder1$txttax1").value;
            var Tax2 = document.getElementById("ctl00$ContentPlaceHolder1$txttax2").value;
            var Tax3 = document.getElementById("ctl00$ContentPlaceHolder1$txttax3").value;
            var Discount = document.getElementById("ctl00_ContentPlaceHolder1_txtdiscount").value;
            var ShippingCharge = document.getElementById("ctl00$ContentPlaceHolder1$txtshippingcharges").value;
            var HandlingCharge = document.getElementById("ctl00$ContentPlaceHolder1$txthandlingcharges").value;
            var OtherCharge = document.getElementById("ctl00$ContentPlaceHolder1$txtothercharges").value;
            var SubTotal = document.getElementById("ctl00_ContentPlaceHolder1_lblSubTotal").innerHTML;
            //ctl00_ContentPlaceHolder1_lblGrossAmount 
            //alert(qty) ;
            //alert(sprice) ;
            var TotalC;
            // alert("Total 1 " + TotalC); 
            var ttt = (Tax1) + (Tax2) + (Tax3) + (Discount) + (ShippingCharge) + (HandlingCharge) + (OtherCharge);
            alert(ttt);
            TotalC = SubTotal - ttt;
            alert("Total " + TotalC);
            //document.getElementById("ctl00_ContentPlaceHolder1_lblAmount").value  = TotalC   ;
            document.getElementById("ctl00_ContentPlaceHolder1_lblGrossAmount").innerHTML = TotalC;
            alert("out");
            //TotalAmt.value = TotalC ;
            //alert(TotalC); 

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
        function mask(evt, max_len) {



            if (evt.keyCode == 13) {

                return true;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }


        }      
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 1%">
                <asp:Label ID="lblmmssgg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                <asp:Label ID="Label8" runat="server" ForeColor="Red"></asp:Label>
                <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label45" runat="server" Text="Make Expense/Asset Purchase Invoice Entry"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel3" runat="server">
                                    <table >
                                        <tr>
                                            <td colspan="3" align="right" style="height: 34px">
                                                <asp:Button ID="btnnewinv" runat="server" Text="Make New Entry" CssClass="btnSubmit"
                                                    OnClick="btnnewinv_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:20%;">
                                                <label>
                                                    <asp:Label ID="Label38" runat="server" Text=" Business Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td colspan="2">
                                                <label>
                                                    <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged"
                                                        Style="margin-left: 0px">
                                                    </asp:DropDownList>
                                                    <input id="hdnWHid" runat="server" style="width: 2px" type="hidden" />
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label39" runat="server" Text=" Date"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txtDate" runat="server" Width="75px"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calender.jpg" />
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton1"
                                                        TargetControlID="txtDate">
                                                    </cc1:CalendarExtender>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label41" runat="server" Text="Entry No."></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblEntryNo" runat="server"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label40" runat="server" Text="Type of Expense Invoice"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                    <asp:ListItem Selected="True" Value="1">On Credit</asp:ListItem>
                                                    <asp:ListItem Value="2">With Cash</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:Panel ID="pnlcash" runat="server">
                                                    <label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcashtype"
                                                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="11"></asp:RequiredFieldValidator>
                                                        <asp:DropDownList ID="ddlcashtype" runat="server" AutoPostBack="True" Style="margin-left: 0px"
                                                            Width="151px">
                                                        </asp:DropDownList>
                                                    </label>
                                                </asp:Panel>
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table>
                                                    <tr>
                                                        <td align="left" valign="top">
                                                            <label>
                                                                <asp:Label ID="lblpartname" Text="Vendor Name" runat="server"></asp:Label>
                                                                <asp:Label ID="Label63" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlpartyName"
                                                                    Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="true" ValidationGroup="11"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td align="left" valign="top">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlpartyName" runat="server" Width="151px">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                    <label>
                                                                        <asp:ImageButton ID="imgadddivision" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                            OnClick="imgadddivision_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                                                    </label>
                                                                    <label>
                                                                        <asp:ImageButton ID="LinkButton3" runat="server" AlternateText="Refresh" Height="20px"
                                                                            ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton3_Click"
                                                                            ToolTip="Refresh" Width="20px" ImageAlign="Bottom" />
                                                                    </label>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="imgadddivision" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="LinkButton3" EventName="Click" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td align="left" valign="top">
                                                            <label>
                                                                <asp:Label ID="Label42" runat="server" Text="Invoice No."></asp:Label>
                                                                <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([().a-zA-Z0-9_\s]*)"
                                                                    ControlToValidate="txtInvNo" ValidationGroup="11"></asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td align="left" valign="top">
                                                            <label>
                                                                <asp:TextBox ID="txtInvNo" MaxLength="15" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_().\s]+$/,'div1',15)"
                                                                    runat="server" Text="0" Width="70px"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label29" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="div1" class="labelcount">15</span>
                                                                <asp:Label ID="lblkjsadfgh" runat="server" Text="(A-Z 0-9 _ . ())" CssClass="labelcount"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td align="left" valign="top">
                                                            <asp:Panel ID="pnlpaydue" runat="server">
                                                                <label>
                                                                    <asp:Label ID="Label1" runat="server" Text="Payment Due Date"> </asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpayduedate"
                                                                        ErrorMessage="*" ValidationGroup="11"> </asp:RequiredFieldValidator>
                                                                </label>
                                                                <label>
                                                                    <asp:TextBox ID="txtpayduedate" runat="server" Width="100px"></asp:TextBox>
                                                                </label>
                                                                <label>
                                                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/calender.jpg" />
                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton4"
                                                                        TargetControlID="txtpayduedate">
                                                                    </cc1:CalendarExtender>
                                                                </label>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chksinfo" runat="server" Text="Shipping Info" AutoPostBack="True"
                                                                OnCheckedChanged="chksinfo_CheckedChanged" />
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:Panel ID="pnlshipin" runat="server" Visible="false">
                                                                <table>
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <label>
                                                                                <asp:Label ID="Label44" runat="server" Text="Tracking No."></asp:Label>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                                                                    ControlToValidate="txtTrackingNo" ValidationGroup="11"></asp:RegularExpressionValidator>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:TextBox ID="txtTrackingNo" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'div2',15)"
                                                                                    runat="server" Text="0" MaxLength="15" Width="70px"></asp:TextBox>
                                                                            </label>
                                                                            <label>
                                                                                <asp:Label ID="Label37" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                                <span id="div2" class="labelcount">15</span>
                                                                                <asp:Label ID="Label47" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td valign="top">
                                                                            <label>
                                                                                <asp:Label ID="Label43" runat="server" Text="Transporter Name"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td valign="top">
                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>
                                                                                    <label>
                                                                                        <asp:DropDownList ID="ddlTransporter" runat="server" Width="151px">
                                                                                        </asp:DropDownList>
                                                                                    </label>
                                                                                    <label>
                                                                                        <asp:ImageButton ID="ImageButton2" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                                            OnClick="imgadddivision_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                                                                    </label>
                                                                                    <label>
                                                                                        <asp:ImageButton ID="ImageButton3" runat="server" AlternateText="Refresh" Height="20px"
                                                                                            ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                                                                            ImageAlign="Bottom" OnClick="ImageButton3_Click" />
                                                                                    </label>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="ImageButton2" EventName="Click" />
                                                                                    <asp:AsyncPostBackTrigger ControlID="ImageButton3" EventName="Click" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="lbld" runat="server" Text="Do you wish to seperately account for taxes paid on this Invoice?"></asp:Label></label>
                                            <label>
                                                <asp:LinkButton ID="lnkmore" runat="server" Text="More Info" OnClick="lnkmore_Click"></asp:LinkButton>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rdl1" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                OnSelectedIndexChanged="rdl1_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlitem" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="vb" runat="server" Text="Are items taxed at different rates?"></asp:Label></label>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="CheckBox2" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                                OnSelectedIndexChanged="CheckBox2_SelectedIndexChanged">
                                                                <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlmain" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlconf" runat="server" Visible="true">
                                                    <table width="100%">
                                                        <tr>
                                                            <td valign="top" >
                                                                <label>
                                                                    <asp:Label ID="lblDescription" runat="server" Text="Memo"></asp:Label>
                                                                    <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="Invalid Character"
                                                                        SetFocusOnError="True" Display="Dynamic" ValidationExpression="^([_.a-zA-Z0-9 \s]*)"
                                                                        ControlToValidate="txtdesc" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                </label>
                                                            </td>
                                                            <td valign="top">
                                                                <label>
                                                                    <asp:TextBox ID="txtdesc" runat="server" ValidationGroup="1" Height="66px" TextMode="MultiLine"
                                                                        onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*>+:;={}()[]|\/]/g,/^[\a-zA-Z.0-9_\s]+$/,'Span1',500)"
                                                                        MaxLength="500" onkeypress="return checktextboxmaxlength(this,500,event)" Width="370px"></asp:TextBox>
                                                                </label>
                                                                <label>
                                                                    <asp:Label ID="Label28" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                    <span id="Span1" class="labelcount">500</span>
                                                                    <asp:Label ID="Label7" runat="server" Text="(A-Z 0-9 _ .)" CssClass="labelcount"></asp:Label>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <fieldset>
                                                                    <legend>
                                                                        <asp:Label ID="lblsadd" runat="server" Text="Enter the details of the invoice"></asp:Label>
                                                                    </legend>
                                                                    <div style="float: right;">
                                                                        <label>
                                                                            <asp:Label ID="lblvvv" runat="server" Text="No of Items in Invoice"></asp:Label>
                                                                        </label>
                                                                        <label>
                                                                            <asp:DropDownList ID="ddlitemnoinv" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlitemnoinv_SelectedIndexChanged"
                                                                                Width="50px">
                                                                                <asp:ListItem Selected="True" Value="1" Text="05"></asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="10"></asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="15"></asp:ListItem>
                                                                                <asp:ListItem Value="4" Text="20"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </div>
                                                                    <div style="clear: both;">
                                                                        <asp:GridView ID="grdinvoice" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                                            PagerStyle-CssClass="pgr" DataKeyNames="Id" AlternatingRowStyle-CssClass="alt"
                                                                            Width="100%" Visible="true">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Asset/Expense" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlexasset" runat="server" AutoPostBack="True" Width="95%"
                                                                                            OnSelectedIndexChanged="ddlexasset_SelectedIndexChanged">
                                                                                            <asp:ListItem Selected="True" Value="15" Text="Expense"></asp:ListItem>
                                                                                            <asp:ListItem Value="1" Text="Asset"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Account Name : Group Name" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlaccgroup" runat="server" Width="95%">
                                                                                        </asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="tdfvf" runat="server" ControlToValidate="ddlaccgroup"
                                                                                            Display="Dynamic" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="11"></asp:RequiredFieldValidator>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="lblheadtax1" runat="server" Text="Tax1"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtgt1" runat="server" Width="40px"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="txtaess_FilteredTextBoxExtender" runat="server"
                                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtgt1" ValidChars=".">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="lblheadtax2" runat="server" Text="Tax2"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtgt2" runat="server" Width="40px" Enabled="true"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="txtaess_FteredTextBoxExtender" runat="server" Enabled="True"
                                                                                            FilterType="Custom, Numbers" TargetControlID="txtgt2" ValidChars=".">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Tax3" HeaderStyle-HorizontalAlign="Left">
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="lblheadtax3" runat="server" Text="Tax3"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtgt3" runat="server" Width="40px" Enabled="true"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxtender1" runat="server" Enabled="True"
                                                                                            FilterType="Custom, Numbers" TargetControlID="txtgt3" ValidChars=".">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtamount" Width="120px" runat="server" MaxLength="15"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="txtaddress_FilteredTextBoxExtender" runat="server"
                                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtamount" ValidChars=".">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                        <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtamount"
                                                                                            ErrorMessage="*" SetFocusOnError="true" ValidationGroup="2" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                        <asp:RegularExpressionValidator ID="RegularEssionValior3" ControlToValidate="txtamount"
                                                                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid Digits"
                                                                                            ValidationGroup="2" Display="Dynamic"></asp:RegularExpressionValidator>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Memo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="25%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtmemo" runat="server" Width="95%" MaxLength="300"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <PagerStyle CssClass="pgr" />
                                                                            <AlternatingRowStyle CssClass="alt" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </fieldset>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <label>
                                                                    <asp:Label ID="Label20" runat="server" Text="Invoice Amount"></asp:Label><asp:Label
                                                                        ID="Label62" runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RegularExpressionValidator
                                                                            ID="RegularExpressionValidator19" runat="server" ErrorMessage="*" Display="Dynamic"
                                                                            SetFocusOnError="True" ValidationExpression="^([0-9.]*)" ControlToValidate="txtValurRecd"
                                                                            ValidationGroup="11"></asp:RegularExpressionValidator></label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="txtValurRecd" runat="server" ReadOnly="True" Enabled="False">0</asp:TextBox><%-- <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" TargetControlID="txtValurRecd" ValidChars="0147852369.">
                                        </cc1:FilteredTextBoxExtender>--%>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <label>
                                                                    <asp:Label ID="Label3" runat="server" Text="Discount or Rebate Received"></asp:Label>
                                                                    <asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequirFieldValidator3" runat="server" ControlToValidate="txtincoperation"
                                                                        ErrorMessage="*" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressioidator2" runat="server" ErrorMessage="*"
                                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([0-9.]*)" ControlToValidate="txtincoperation"
                                                                        ValidationGroup="11"></asp:RegularExpressionValidator>
                                                                        </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="txtincoperation" runat="server" Enabled="true">0</asp:TextBox>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <asp:Panel ID="pnltax1" runat="server" Visible="false">
                                                                <td align="right">
                                                                    <label>
                                                                        <asp:Label ID="Label23" runat="server" Text="Tax1"></asp:Label><asp:Label ID="lbltaxper1"
                                                                            runat="server" Visible="false"></asp:Label><asp:Label ID="Label55" runat="server"
                                                                                Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                                                                    runat="server" ControlToValidate="txtTax1" ErrorMessage="*"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                                        ID="RegularExpressionValidator8" ControlToValidate="txtTax1" ValidationExpression="^(-)?\d+(\.\d\d)?$"
                                                                                        runat="server" ErrorMessage="Invalid"> </asp:RegularExpressionValidator>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:TextBox ID="txtTax1" MaxLength="15" runat="server">0</asp:TextBox></label>
                                                                </td>
                                                            </asp:Panel>
                                                        </tr>
                                                        <tr>
                                                            <asp:Panel ID="pnltax2" runat="server" Visible="false" Style="margin-bottom: 0px">
                                                                <td align="right">
                                                                    <label>
                                                                        <asp:Label ID="Label24" runat="server" Text="Tax2"></asp:Label><asp:Label ID="lbltaxper2"
                                                                            runat="server" Visible="false"></asp:Label><asp:Label ID="Label56" runat="server"
                                                                                Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                                                                                    runat="server" ControlToValidate="txtTax2" ErrorMessage="*"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                                        ID="RegularExpressionValidator9" ControlToValidate="txtTax2" ValidationExpression="^(-)?\d+(\.\d\d)?$"
                                                                                        runat="server" ErrorMessage="Invalid"> </asp:RegularExpressionValidator>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:TextBox ID="txtTax2" MaxLength="15" runat="server">0</asp:TextBox></label>
                                                                </td>
                                                            </asp:Panel>
                                                        </tr>
                                                        <tr>
                                                            <asp:Panel ID="pnltax3" runat="server" Visible="false">
                                                                <td align="right">
                                                                    <label>
                                                                        <asp:Label ID="Label60" runat="server" Text="Tax3"></asp:Label><asp:Label ID="lbltaxper3"
                                                                            runat="server" Visible="false"></asp:Label><asp:Label ID="lbltxtop" runat="server"
                                                                                Visible="False">0</asp:Label><asp:Label ID="Label61" runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                                                                    ID="RequiredFieldValidapotor16" runat="server" ControlToValidate="txtTax3" ErrorMessage="*"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                                        ID="RegularExpressioalidator22" ControlToValidate="txtTax3" ValidationExpression="^(-)?\d+(\.\d\d)?$"
                                                                                        runat="server" ErrorMessage="Invalid"> </asp:RegularExpressionValidator>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:TextBox ID="txtTax3" MaxLength="15" runat="server">0</asp:TextBox></label>
                                                                </td>
                                                            </asp:Panel>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width:20%;">
                                                                <label>
                                                                    <asp:Label ID="Label25" runat="server" Text="Shipping and Other Charges"></asp:Label><asp:Label
                                                                        ID="Label57" runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                                                            ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtShippCharge"
                                                                            ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                                ID="RegularExpressionValidator10" ControlToValidate="txtShippCharge" ValidationExpression="^(-)?\d+(\.\d\d)?$"
                                                                                runat="server" ErrorMessage="Invalid" Display="Dynamic"></asp:RegularExpressionValidator></label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="txtShippCharge" MaxLength="15" runat="server">0</asp:TextBox></label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <label>
                                                                    <asp:Label ID="Label26" runat="server" Text="Total Invoice Amount"></asp:Label><asp:Label
                                                                        ID="Label58" runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                                                            ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtNetAmount"
                                                                            ErrorMessage="*"></asp:RequiredFieldValidator><%--<asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="txtNetAmount"
                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"> </asp:RegularExpressionValidator>--%>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="txtNetAmount" MaxLength="15" runat="server" ReadOnly="True" Enabled="False">0</asp:TextBox></label>
                                                            </td>
                                                        </tr>
                                                      
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                                           <td style="width:20%;"></td>
                                                            <td>
                                                                <asp:CheckBox ID="chkdoc" runat="server" Text="Do you wish to attach or upload any documents related to this entry?" />
                                                                <br />
                                                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Add Inventory received qty to inventory on hand for shopping cart.?"
                                                                    Visible="False" />
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                           <td style="width:20%;"></td>
                                                            <td>
                                                               <asp:CheckBox ID="chkappentry" runat="server" Text="Approved for this entry" 
                                         Visible="False" />
                                                            </td>
                                                        </tr>
                                        <tr align="center" >
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btCal" runat="server" OnClick="btCal_Click" Text="Recalculate and Confirm"
                                                    CssClass="btnSubmit" ValidationGroup="11" />
                                                <%-- <asp:Button ID="btConfirm" runat="server" OnClick="btConfirm_Click" Text="Confirm"
                                        ValidationGroup="11" Visible="False" CssClass="btnSubmit" />--%>
                                                <asp:Button ID="btSubmit" runat="server" OnClick="btSubmit_Click" Text="Submit" ValidationGroup="11"
                                                    Visible="False" CssClass="btnSubmit" />
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td align="left"  colspan="2">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="txttinfo1" runat="server"></asp:Label></label><label><asp:Label ID="txttinfo2"
                                                                    runat="server"></asp:Label></label><label><asp:Label ID="txttinfo3" runat="server"></asp:Label></label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                           
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Panel ID="Panel5" runat="server" CssClass="modalPopup" Width="300px">
                                    <fieldset>
                                        <legend></legend>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="height: 18px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="lblm0" runat="server">There is no tax account set to account 
                                                        tax on purchases. Would you like to create a new tax account? </asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnyes" CssClass="btnSubmit" runat="server" Text="Yes" OnClick="btnyes_Click" />
                                                    &nbsp;<asp:Button ID="btncan" CssClass="btnSubmit" runat="server" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Panel>
                                <asp:Button ID="HiddenButton222" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                                    ID="ModalPopupExtender12222" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel5" TargetControlID="HiddenButton222" CancelControlID="btncan">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" Width="75%">
                                    <fieldset>
                                        <legend></legend>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        For example if your Invoice amount is $1000/- and paid $100/- as Tax
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        If you are allowed input tax credit in your business, we recommend using this option.
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="Button1" CssClass="btnSubmit" runat="server" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Panel>
                                <asp:Button ID="Button3" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                                    ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel4" TargetControlID="Button3" CancelControlID="Button1">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Panel ID="Panel6" runat="server" CssClass="modalPopup" Width="70%">
                                    <fieldset>
                                        <legend></legend>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Please note that, you cannot change this option selection once you've made it. If
                                                        you wish to change your selection,<br />
                                                        you will have to create a new invoice by clicking the button at the top of the page.
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <label>
                                                        By clicking yes on this option , your expense/asset account will be debited by $1000
                                                        and $100 would be debited as input tax credit, which you can later offset against
                                                        your liability to pay tax on sales you make.<br />
                                                        If you click no on this option, please note that the complete $1100 would be debited
                                                        to your expense/asset account.
                                                    </label>
                                                    <asp:Button ID="btnk" CssClass="btnSubmit" runat="server" Text=" OK " OnClick="btnk_Click" />
                                                    <asp:Button ID="btnccc" CssClass="btnSubmit" runat="server" Text="Cancel" OnClick="btnccc_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    &nbsp;&nbsp;&nbsp;</asp:Panel>
                                <asp:Button ID="Button4" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                                    ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel6" TargetControlID="Button4">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
