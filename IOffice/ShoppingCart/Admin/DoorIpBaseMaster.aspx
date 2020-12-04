<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DoorIpBaseMaster.aspx.cs" Inherits="DoorIpBaseMaster"
    Title="Door IpBase Master " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">


        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }

        function checktextboxmaxlength1(txt, maxLen, evt) {
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
        function checktextboxmaxlength(txt, maxLen, evt) {
            try {

                if (evt.srcElement.value > 255) {
                    txt.value = txt.value.substring(0, maxLen - 1);
                    return false;
                }

            }
            catch (e) {

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
        .style1
        {
            width: 25%;
            height: 42px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="float: left; padding: 1%;">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblms" runat="server" Text=""></asp:Label></legend>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="btnaddacc" runat="server" Text="Add New Door/IP Based Device" CssClass="btnSubmit"
                                OnClick="btnaddacc_Click" />
                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="addinventoryroom" runat="server" Visible="False">
                        <asp:Panel ID="pnlattdev" runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label5" runat="server" Text="Business Name"> </asp:Label>
                                            <asp:Label ID="Label40" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlwarehouse"
                                                ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlwarehouse" runat="server">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <label>
                                            <asp:Label ID="Label6" runat="server" Text="Door Number"></asp:Label>
                                            <asp:Label ID="Label8" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdoornumber"
                                                ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtdoornumber" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'_.&*a-zA-Z()>+:;={}[]|\/]/g,/^[\0-9\s]+$/,'div1',10)"
                                                MaxLength="30"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTetender4" runat="server" Enabled="True"
                                                TargetControlID="txtdoornumber" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label20" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="div1" class="labelcount">10</span>
                                            <asp:Label ID="lblinvstiename" runat="server" Text="(0-9)" CssClass="labelcount"></asp:Label></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label4" runat="server" Text="Door Name"></asp:Label>
                                            <asp:Label ID="Label7" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RegularExpressionValidator ID="larExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtdoorname" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequdFieldValidator2" runat="server" ControlToValidate="txtdoorname"
                                                ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtdoorname" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span1',90)"
                                                MaxLength="90"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label10" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="Span1" class="labelcount">90</span>
                                            <asp:Label ID="Label11" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label12" runat="server" Text="Serial Number"></asp:Label>
                                            <%-- <asp:Label ID="Label13" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtserialno" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtserialno"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtserialno" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span2',30)"
                                                MaxLength="30"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label14" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="Span2" class="labelcount">30</span>
                                            <asp:Label ID="Label15" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label34" runat="server" Text="IP Address"></asp:Label>
                                            <asp:Label ID="Label35" runat="server" Text="*" CssClass="labelstar"></asp:Label></label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtIpAddress" runat="server" MaxLength="3" Width="40px" onkeypress="return mask(event)"
                                                onkeyup="return checktextboxmaxlength(this,3,event)"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtEndzip_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" TargetControlID="txtIpAddress" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtIpAddress"
                                                ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:Label ID="Label36" runat="server" Text="(0-255)" CssClass="labelcount"></asp:Label></label>
                                        <label>
                                            <asp:TextBox ID="txtip1" runat="server" MaxLength="3" Width="40px" onkeypress="return mask(event)"
                                                onkeyup="return checktextboxmaxlength(this,3,event)"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="txtip1" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtip1"
                                                ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:Label ID="Label37" runat="server" Text="(0-255)" CssClass="labelcount"></asp:Label></label>
                                        <label>
                                            <asp:TextBox ID="txtip2" runat="server" MaxLength="3" Width="40px" onkeypress="return mask(event)"
                                                onkeyup="return checktextboxmaxlength(this,3,event)"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                TargetControlID="txtip2" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:RequiredFieldValidator ID="RequFieldValidator4" runat="server" ControlToValidate="txtip2"
                                                ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:Label ID="Label38" runat="server" Text="(0-255)" CssClass="labelcount"></asp:Label></label>
                                        <label>
                                            <asp:TextBox ID="txtip3" runat="server" MaxLength="3" Width="40px" onkeypress="return mask(event)"
                                                onkeyup="return checktextboxmaxlength(this,3,event)"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                TargetControlID="txtip3" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtip3"
                                                ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:Label ID="Label39" runat="server" Text="(0-255)" CssClass="labelcount"></asp:Label></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label2" runat="server" Text="Select the Attendance Device Linked to this Door"> </asp:Label><%-- <asp:Label ID="Label43" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                            <%--<asp:RequiredFieldValidator ID="RedFieldValidator2" runat="server" ControlToValidate="ddlattendancedevice"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlattendancedevice" runat="server">
                                            </asp:DropDownList>
                                        </label>
                                        <label>
                                            <asp:ImageButton ID="imgAdd2" runat="server" AlternateText="Add New" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                Height="20px" ToolTip="Add New " Width="20px" OnClick="imgAdd2_Click" ImageAlign="Bottom" />
                                        </label>
                                        <label>
                                            <asp:ImageButton ID="LinkButton97666667" runat="server" Height="20px" ToolTip="Refresh "
                                                Width="20px" AlternateText="Refresh" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                                OnClick="LinkButton97666667_Click" ImageAlign="Bottom"></asp:ImageButton>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <label>
                                            <asp:Label ID="Label44" runat="server" Text="Admin Access Port "></asp:Label>
                                            <%-- <asp:Label ID="Label45" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtadminaccessport"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                             --%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtadminaccessport" runat="server" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@#$%^'_.&*a-zA-Z()>+:;={}[]|\/]/g,/^[\0-9\s]+$/,'Span6',8)"
                                                MaxLength="8"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilterxExtender4" runat="server" Enabled="True"
                                                TargetControlID="txtadminaccessport" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label46" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="Span6" class="labelcount">8</span>
                                            <asp:Label ID="Label47" runat="server" Text="(0-9)" CssClass="labelcount"></asp:Label></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <label>
                                            <asp:Label ID="Label48" runat="server" Text="Control Port"></asp:Label>
                                            <%--<asp:Label ID="Label49" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtadminaccessport"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                --%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtcontrolport" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'_.&*a-zA-Z()>+:;={}[]|\/]/g,/^[\0-9\s]+$/,'Span7',8)"
                                                MaxLength="8"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                TargetControlID="txtcontrolport" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label50" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="Span7" class="labelcount">8</span>
                                            <asp:Label ID="Label51" runat="server" Text="(0-9)" CssClass="labelcount"></asp:Label></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label52" runat="server" Text="Command Line to Open"></asp:Label>
                                            <%--<asp:Label ID="Label53" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                        --%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtcopen" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtcopen"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                          --%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtcopen" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9._\s]+$/,'Span8',100)"
                                                MaxLength="100"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label54" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="Span8" class="labelcount">100</span>
                                            <asp:Label ID="Label55" runat="server" Text="(A-Z 0-9 . _)" CssClass="labelcount"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label56" runat="server" Text="Command Line to Start"></asp:Label>
                                            <%-- <asp:Label ID="Label57" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                 --%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtcstart" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtcstart"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                         --%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtcstart" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9._\s]+$/,'Span9',100)"
                                                MaxLength="100"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label58" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="Span9" class="labelcount">100</span>
                                            <asp:Label ID="Label59" runat="server" Text="(A-Z 0-9 . _)" CssClass="labelcount"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label60" runat="server" Text="Command Line to Stop"></asp:Label>
                                            <%-- <asp:Label ID="Label61" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtcstop" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtcstop"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                             --%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtcstop" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9._\s]+$/,'Span10',100)"
                                                MaxLength="100"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label62" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="Span10" class="labelcount">100</span>
                                            <asp:Label ID="Label63" runat="server" Text="(A-Z 0-9 . _)" CssClass="labelcount"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label64" runat="server" Text="Security Key"></asp:Label>
                                            <%--  <asp:Label ID="Label65" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtsecuritykey" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtsecuritykey"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                          --%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtsecuritykey" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span11',30)"
                                                MaxLength="30"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label66" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="Span11" class="labelcount">30</span>
                                            <asp:Label ID="Label67" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label16" runat="server" Text="Version Number"></asp:Label>
                                            <%--    <asp:Label ID="Label17" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtverno" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtverno"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                             --%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtverno" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span3',30)"
                                                MaxLength="30"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label18" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="Span3" class="labelcount">30</span>
                                            <asp:Label ID="Label21" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label22" runat="server" Text="Make"></asp:Label>
                                            <%--  <asp:Label ID="Label23" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtmake" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtmake"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                             --%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtmake" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span4',30)"
                                                MaxLength="30"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label24" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="Span4" class="labelcount">30</span>
                                            <asp:Label ID="Label25" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label26" runat="server" Text="Model"></asp:Label>
                                            <%--  <asp:Label ID="Label27" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtmodel" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtmodel"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                             --%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtmodel" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span5',30)"
                                                MaxLength="30"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label28" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="Span5" class="labelcount">30</span>
                                            <asp:Label ID="Label29" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label30" runat="server" Text="Location"></asp:Label>
                                            <%-- <asp:Label ID="Label31" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtlocation" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtlocation"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                     --%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtlocation" runat="server" ValidationGroup="1" Height="73px" TextMode="MultiLine"
                                                onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*>+.:;={}()[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div2',150)"
                                                MaxLength="150" onkeypress="return checktextboxmaxlength1(this,150,event)" Width="370px"></asp:TextBox>
                                            <asp:Label ID="Label32" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="div2" class="labelcount">150</span>
                                            <asp:Label ID="Label33" runat="server" Text="(A-Z 0-9 _ )" CssClass="labelcount"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <tr>
                            <td colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 30%">
                                            <label>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Button ID="ImageButton9" runat="server" CssClass="btnSubmit" Text="Update" OnClick="ImageButton9_Click"
                                                Visible="false" ValidationGroup="1" />
                                            <asp:Button ID="ImageButton1" CssClass="btnSubmit" runat="server" Text="Submit" OnClick="Button1_Click"
                                                ValidationGroup="1" />
                                            <asp:Button ID="ImageButton2" CssClass="btnSubmit" runat="server" Text="Cancel" OnClick="ImageButton2_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <div style="clear: both;">
                                </div>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Door/IP Based Device" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click1" />
                            <input id="Button3" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" class="btnSubmit" visible="False" />
                        </label>
                    </div>
                    <%--  <div style="clear: both;">
                    </div>--%>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label41" runat="server" Text="Filter by Business"> </asp:Label></label>
                                <label>
                                    <asp:DropDownList ID="ddlbussnessfilter" runat="server" OnSelectedIndexChanged="ddlbussnessfilter_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                </label>
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Search"> </asp:Label></label>
                                <label>
                                    <asp:TextBox ID="txtserach" runat="server" AutoPostBack="True" OnTextChanged="txtserach_TextChanged"></asp:TextBox></label>
                            </td>
                        </tr>
                    </table>
                    <label>
                    </label>
                    <div style="clear: both;">
                        <br />
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <tr align="center">
                            <td>
                                <div id="mydiv" class="closed">
                                    <table width="100%">
                                        <tr align="center">
                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                <asp:Label ID="lblCompany" runat="server" Font-Italic="True" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                <asp:Label ID="Label19" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="True" ForeColor="Black"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                <asp:Label ID="Label9" runat="server" Font-Italic="True" Text="List of Door/IP Based Device"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="left">
                                            <td align="left" style="text-align: left; font-size: 18px; font-weight: bold;">
                                                <asp:Label ID="lblse" runat="server" Text="Search by : " Font-Italic="true"></asp:Label>
                                                <asp:Label ID="lblserttex" runat="server" Font-Italic="True" ForeColor="Black"> </asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    EmptyDataText="No Record Found." CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt" DataKeyNames="Id" HorizontalAlign="Left" Width="100%"
                                    OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Business" SortExpression="Wname" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblwname" runat="server" Text='<%#Bind("Wname") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Door/IP Based Device Name" SortExpression="DoorName"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblattdevi" runat="server" Text='<%#Bind("DoorName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Door/IP Based<br> Device Number" SortExpression="DoorNo"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcous" runat="server" Text='<%#Bind("DoorNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location" SortExpression="Location" HeaderStyle-HorizontalAlign="Left"
                                            Visible="true" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblloca" runat="server" Text='<%#Bind("Location") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IP Address" SortExpression="IpAddress" HeaderStyle-HorizontalAlign="Left"
                                            Visible="true" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblipaddress" runat="server" Text='<%#Bind("IpAddress") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Control Port" SortExpression="ControlPort" HeaderStyle-HorizontalAlign="Left"
                                            Visible="true" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcport" runat="server" Text='<%#Bind("ControlPort") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View" ItemStyle-Width="2%" HeaderImageUrl="~/Account/images/viewprofile.jpg"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="edit1" runat="server" ToolTip="View" CommandName="View" CommandArgument='<%# Bind("Id") %>'
                                                    OnClick="edit_Click" ImageUrl="~/Account/images/viewprofile.jpg" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-Width="2%" HeaderImageUrl="~/Account/images/edit.gif"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="edit" runat="server" ToolTip="Edit" CommandArgument='<%# Bind("Id") %>'
                                                    CommandName="Edit" OnClick="edit_Click" ImageUrl="~/Account/images/edit.gif" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                            HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="Button1" runat="server" ToolTip="Delete" CommandName="Delete"
                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                </asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
