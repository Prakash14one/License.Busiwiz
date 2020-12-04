<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="Productportalmaster.aspx.cs" Inherits="SetupWizardDetail1" Title="Add Manage Product portal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
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
        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
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
        .pnlBackGround
{
 position:fixed;
    top:10%;
    left:10px;
    width:300px;
    height:125px;
    text-align:center;
    background-color:White;
    border:solid 3px black;
}
    </style>
     <script type="text/javascript" language="javascript">
         function ShowMyModalPopup() {
             var modal = $find("<%=ModalPopupExtender9.ClientID%>");
             modal.show();

         }                
        
    </script>
    <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel1" runat="server">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </legend>
                        <div style="float: right;">
                            <asp:Button ID="addnewpanel" runat="server" Text="Add Product Portal Details" CssClass="btnSubmit" OnClick="addnewpanel_Click" />
                             <asp:Button ID="btndosyncro" runat="server" CssClass="btnSubmit" Visible="false"   OnClick="btndosyncro_Clickpop" Text="Do Synchronise" />
                        </div>
                    </fieldset></asp:Panel>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                    <fieldset>
                        <legend>
                            <asp:Label ID="lbladd" runat="server" Text=" Product Portal Details "></asp:Label>
                        </legend>
                        <div style="left">
                            <table style="width: 100%">
                               
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            Product Name
                                            <asp:Label ID="Label69" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvname0" runat="server" ControlToValidate="ddpname"
                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddpname" runat="server" Width="230px" AutoPostBack="True" OnSelectedIndexChanged="ddpname_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            Portal Name
                                            <asp:Label ID="Label63" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator178" runat="server" ControlToValidate="txtportname"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtportname"
                                                ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-zA-Z.0-9\s]*)"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtportname" runat="server" MaxLength="50" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@#$%^']&amp;*()&gt;_0-9+:;={}[]|\/]/g,/^[\a-zA-Z.0-9\s]+$/,'div3',50)"
                                                ValidationGroup="1" Width="224px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label61" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                            <span id="div3" class="labelcount">50</span>
                                            <asp:Label ID="Label62" runat="server" CssClass="labelcount" Text="(A-Z 0-9 .)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                 <%--<tr>
                                    <td colspan="2">
                                    <asp:CheckBox ID="Chk_portalIsforServer" runat="server" AutoPostBack="True" Checked="false" oncheckedchanged="Chk_portalIsforServer_CheckedChanged" Text="  Is this the portal regarding providing server hosting services ?  " />                                   
                                    <label>
                                   (For Eg. Safestserver.com which provides hosting services to the subscribers of any other portal like OnlineAccounts.net)
                                    </label> 
                                    
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            Default Home Page
                                            <asp:Label ID="Label48" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtaspxpagename"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtaspxpagename" runat="server" MaxLength="50" ValidationGroup="1"
                                                Width="224px"></asp:TextBox>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            Browse Logo
                                            <br>
                                            (Recommended size: 176 x 106 pixels)
                                            <asp:Label ID="Label79" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                            &nbsp;<asp:Button ID="btnUpload" runat="server" CssClass="btnSubmit" Text="Upload"
                                                OnClick="btnUpload_Click1" />
                                        </label>
                                        <label>
                                            <asp:Image ID="imglogo" runat="server" Height="106px" Width="176px" />
                                        </label>
                                    </td>
                                    <td rowspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            Color number to be used
                                            <asp:Label ID="Label54" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcolour"
                                                ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtcolour" runat="server" MaxLength="50" ValidationGroup="1" Width="224px"></asp:TextBox>
                                        </label>
                                   <%-- </td>
                                    <td>--%>
                                    <label>
                                    <asp:Button ID="btntest" runat="server" Text="Test" onclick="btntest_Click" />
                                    </label>
                                    <label> <asp:TextBox ID="lbltestdata" runat="server" Width="50px" Enabled="False" ></asp:TextBox></label>
                                    </td>
                                </tr>
                            </table>
                    </fieldset>
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label2" runat="server" Text="Out Going Mail Server"></asp:Label>
                        </legend>
                        <asp:Panel ID="Panel7" runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label26" runat="server" Text="E-mail Display Name"></asp:Label>
                                            <asp:Label ID="Label27" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" SetFocusOnError="true"
                                                ControlToValidate="txtmemailname" ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtmemailname"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtmemailname" MaxLength="50" Width="224px" runat="server" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@#$?%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'Span8',50)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label35" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                            <span id="Span8" class="labelcount">50</span>
                                            <asp:Label ID="Label51" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label3" runat="server" Text="Email ID"></asp:Label>
                                            <asp:Label ID="Label73" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvSubSubCat0" runat="server" ControlToValidate="txtemail"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtemail"
                                                Display="Dynamic" ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtemail" runat="server" MaxLength="50" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!#$%^'&amp;*()&gt;+:;={}[]|\/]/g,/^[\._@a-zA-Z0-9\s]+$/,'Span10',50)"
                                                ValidationGroup="1" Width="224px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label4" Text="Max " runat="server" CssClass="labelcount"></asp:Label>
                                            <span id="Span10" class="labelcount">50</span>
                                            <asp:Label ID="Label9" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ . @)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label14" Text="Out Going Mail Server User ID" runat="server"></asp:Label>
                                            <asp:Label ID="Label74" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtuserid"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                ControlToValidate="txtuserid" ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtuserid" runat="server" Width="224px" ValidationGroup="1" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!#$%^'&amp;*()&gt;+:;={}[]|\/]/g,/^[\._@a-zA-Z0-9\s]+$/,'Span9',50)"
                                                MaxLength="50"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label15" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span9" class="labelcount">50</span>
                                            <asp:Label ID="Label36" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label10" runat="server" Text=" Out Going Mail Server URL"></asp:Label>
                                            <asp:Label ID="Label34" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="TextIncomingMailServer"
                                                ErrorMessage="*" ValidationGroup="2" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                SetFocusOnError="true" ErrorMessage="Invalid Character" ValidationExpression="^([.-_@a-zA-Z0-9\s]*)"
                                                ControlToValidate="TextIncomingMailServer" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="TextIncomingMailServer" Width="224px" MaxLength="50" runat="server"
                                                onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%?^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z.@-_0-9\s]+$/,'Span5',50)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label59" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                            <span id="Span5" class="labelcount">50</span>
                                            <asp:Label ID="Label24" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @ - )"></asp:Label>
                                        </label>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label55" runat="server" Text=" Out Going Mail Server Port"></asp:Label>
                                            <asp:Label ID="Label58" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextIncomingMailServer"
                                                ErrorMessage="*" ValidationGroup="2" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                                SetFocusOnError="true" ErrorMessage="Invalid Character" ValidationExpression="^([.-_@a-zA-Z0-9\s]*)"
                                                ControlToValidate="TextIncomingMailServerport" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="TextIncomingMailServerport" Width="224px" MaxLength="50" runat="server"
                                                onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%?^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z.@-_0-9\s]+$/,'Span5',50)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label66" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                            <span id="Span14" class="labelcount">50</span>
                                            <asp:Label ID="Label67" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @ - )"></asp:Label>
                                        </label>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label37" runat="server" Text="Password"></asp:Label>
                                            <asp:Label ID="Label33" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="TextEmailMasterLoginPassword"
                                                ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="TextEmailMasterLoginPassword" runat="server" TextMode="Password" Width="224px"></asp:TextBox>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label43" runat="server" Text="Confirm Password"></asp:Label>
                                            <asp:Label ID="Label72" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator180" runat="server" ControlToValidate="TextBox3" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="TextEmailMasterLoginPassword" ControlToValidate="TextBox3" ErrorMessage="Password does not match" SetFocusOnError="true" ValidationGroup="1"></asp:CompareValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="TextBox3" runat="server" TextMode="Password" Width="224px"></asp:TextBox>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label50" runat="server" Text="Support Team Details"></asp:Label>
                        </legend>
                        <asp:Panel ID="Panel4" runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label12" Text="Support Team EmailID" runat="server"></asp:Label>
                                            <asp:Label ID="Label75" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtsupptememail"
                                                ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtsupptememail" runat="server" Width="224px" ValidationGroup="1"
                                                onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%^'&amp;*()&gt;+:;={}[]|\/]/g,/^[\._@a-zA-Z0-9\s]+$/,'Span1',50)"
                                                MaxLength="50"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label13" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span1" class="labelcount">30</span>
                                            <asp:Label ID="Label16" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label40" Text="Support Team Phone Number " runat="server"></asp:Label>
                                            <asp:Label ID="Label42" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtspno"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="txtspno" ValidChars="0147852369+-">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtspno" Width="224px" runat="server" ValidationGroup="1" onkeyup="return mak('Span11', 15, this)"
                                                MaxLength="15"></asp:TextBox>
                                        </label>
                                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                                            <label>
                                                <asp:Label ID="Label44" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                <span id="Span11" class="labelcount">15</span>
                                                <asp:Label ID="Label45" CssClass="labelcount" runat="server" Text="(0-9 + -)"></asp:Label>
                                            </label>
                                        </asp:Panel>
                                        <label>
                                            <asp:Label ID="Label82" runat="server" Text="Ext"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:TextBox ID="txtextension" runat="server" MaxLength="15" onkeyup="return mak('Span14', 15, this)"
                                                Width="80Px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtextension_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" TargetControlID="txtextension" ValidChars="0147852369+-">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="lblsstmanagname" Text="Support Team Manager Name" runat="server"></asp:Label>
                                            <asp:Label ID="Label25" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtmanagername"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_.\s]*)" ControlToValidate="txtmanagername"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtmanagername" runat="server" Width="224px" ValidationGroup="1"
                                                MaxLength="50" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^']&amp;*()&gt;_0-9+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.\s]+$/,'div1',50)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="div1" class="labelcount">50</span>
                                            <asp:Label ID="Label80" CssClass="labelcount" runat="server" Text="(A-Z 0-9 .)"></asp:Label></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label17" runat="server" Text="Portal Marketing Website Name"></asp:Label>
                                            <asp:Label ID="Label57" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txtSiteName"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REG1dsd" runat="server" ControlToValidate="txtSiteName"
                                                Display="Dynamic" ErrorMessage="invalid Character" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_.\s]*)"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtSiteName" runat="server" MaxLength="50" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@#$%^'&amp;*()&gt;+:;={}[]|\/]/g,/^[\a-zA-Z0-9._\s]+$/,'Span12',50)"
                                                ValidationGroup="1" Width="224px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label46" runat="server" CssClass="labelcount" Text="Max "></asp:Label>
                                            <span id="Span12" class="labelcount">50</span>
                                            <asp:Label ID="Label47" runat="server" CssClass="labelcount" Text="(A-Z 0-9 ._)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label49" runat="server" Text="Contact Details"></asp:Label>
                        </legend>
                        <asp:Panel ID="Panel3" runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="lblStreetAddressL2" runat="server" Text="Address"></asp:Label>
                                            <asp:Label ID="Label68" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator179" runat="server" ControlToValidate="txtAddress1"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAddress1"
                                                ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([._a-z,-/A-Z0-9\s]*)"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtAddress1" runat="server" MaxLength="150" onKeydown="return mask(event)"
                                                onkeypress="return checktextboxmaxlength(this,150)" onkeyup="return check(this,/[\\/!@#$%^'&amp;*()&gt;+:;={}[]|\/]/g,/^[\a-z,-/A-Z._0-9\s]+$/,'Span3',150)"
                                                Width="400px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label38" runat="server" CssClass="labelcount" Text="Max "></asp:Label>
                                            <span id="Span3" class="labelcount">150</span>
                                            <asp:Label ID="Label5" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ . , - /)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label21" runat="server" Text="Address 2"></asp:Label>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtAddress2"
                                                ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([._a-z,-/A-Z0-9\s]*)"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtaddress2" runat="server" MaxLength="150" onKeydown="return mask(event)"
                                                onkeypress="return checktextboxmaxlength(this,150)" onkeyup="return check(this,/[\\/!@#$%^'&amp;*()&gt;+:;={}[]|\/]/g,/^[\a-z,-/A-Z._0-9\s]+$/,'Span2',150)"
                                                Width="400px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label19" runat="server" CssClass="labelcount" Text="Max "></asp:Label>
                                            <span id="Span2" class="labelcount">150</span>
                                            <asp:Label ID="Label20" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ . , - /)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="lblCountry" Text="Country" runat="server"></asp:Label>
                                            <asp:Label ID="Label28" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlcountry"
                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList Width="224px" ID="ddlcountry" runat="server" AutoPostBack="True"
                                                ValidationGroup="1" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="lblState" Text="State" runat="server"></asp:Label>
                                            <asp:Label ID="Label29" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlstate"
                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList Width="224px" ID="ddlstate" runat="server" ValidationGroup="1"
                                                OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label76" Text="City" runat="server"></asp:Label>
                                            <asp:Label ID="lbllbl" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator118" runat="server" ControlToValidate="txtcity"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                ControlToValidate="txtcity" Display="Dynamic" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ValidationGroup="1"> </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtcity" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@#$%^'.&amp;*()&gt;+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span13',30)"
                                                Width="224px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label77" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                            <span id="Span13" cssclass="labelcount">30</span>
                                            <asp:Label ID="Label78" runat="server" CssClass="labelcount" Text="(A-Z,0-9,_)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="lblZip" Text="ZIP Code" runat="server"></asp:Label>
                                            <asp:Label ID="Label31" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TextZip"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="TextZip"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="TextZip" runat="server" Width="224px" ValidationGroup="1" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@#$%^'._&amp;*()&gt;+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span4',10)"
                                                MaxLength="10"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label39" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span4" class="labelcount">10</span>
                                            <asp:Label ID="Label11" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="lblPhone1" Text="Phone " runat="server"></asp:Label>
                                            <asp:Label ID="Label32" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator177" runat="server" ControlToValidate="TextPhone1"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" TargetControlID="TextPhone1" ValidChars="0147852369+-">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="TextPhone1" Width="224px" runat="server" ValidationGroup="1" onkeyup="return mak('Span6', 15, this)"
                                                MaxLength="15"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label41" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span6" class="labelcount">15</span>
                                            <asp:Label ID="Label6" CssClass="labelcount" runat="server" Text="(0-9 + -)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="lblTollFree1" Text="Toll Free" runat="server"></asp:Label>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                TargetControlID="txttollfree" ValidChars="0147852369+-">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txttollfree" Width="224px" runat="server" ValidationGroup="1" onkeyup="return mak('Span15', 15, this)"
                                                MaxLength="15"></asp:TextBox>
                                        </label>
                                        <asp:Panel ID="Panel5" runat="server" Visible="false">
                                            <label>
                                                <asp:Label ID="Label64" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                                <span id="Span15" class="labelcount">15</span>
                                                <asp:Label ID="Label65" CssClass="labelcount" runat="server" Text="(0-9 + -)"></asp:Label>
                                            </label>
                                        </asp:Panel>
                                        <label>
                                            <asp:Label ID="Label56" Text="Ext" runat="server"></asp:Label>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" TargetControlID="txtex" ValidChars="0147852369+-">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:TextBox ID="txtex" Width="80Px" runat="server" onkeyup="return mak('Span16', 15, this)"
                                                MaxLength="15"></asp:TextBox>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label7" Text="Fax" runat="server"></asp:Label>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                TargetControlID="txtfax" ValidChars="0147852369+-">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtfax" runat="server" Width="224px" ValidationGroup="1" onkeyup="return mak('Span7', 15, this)"
                                                MaxLength="15"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label22" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span7" class="labelcount">15</span>
                                            <asp:Label ID="Label23" CssClass="labelcount" runat="server" Text="(0-9 + -)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label81" runat="server" Text="Status"></asp:Label>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="txtfax" ValidChars="0147852369+-">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlstatus" runat="server" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" Width="224px">
                                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                   
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label52" runat="server" Text="Portal Type Details"></asp:Label>
                        </legend>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                            <td colspan="2">
                                <asp:RadioButtonList ID="RbselectPortalUse" runat="server" RepeatDirection="Vertical" AutoPostBack="true" OnSelectedIndexChanged="RbselectPortalUse_SelectedIndexChanged" Width="100%">
                                            <asp:ListItem Selected="True" Value="1">This portal allows download of software for e.g. Windows application/software</asp:ListItem>
                                             <asp:ListItem  Value="2">This portal provides server hosting service to other portals of the company <br />(For E.g. Safestserver.com which provides hosting services to the subscribers of any other portal like OnlineAccounts.net)</asp:ListItem>
                                            <asp:ListItem Value="3">This is the normal web based applications offering portal</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:CheckBox ID="ChkDoenlodeblesw" Visible="false"  runat="server" AutoPostBack="True" Checked="false" oncheckedchanged="Chk_portalIsforServer_CheckedChanged" Text="IsDownloadableSoftware" />                                   
                                        <asp:CheckBox ID="Chk_portalIsforServer" Visible="false"  runat="server" AutoPostBack="True" Checked="false" oncheckedchanged="Chk_portalIsforServer_CheckedChanged" Text="IsHostingServer" />                                   
                                         <asp:CheckBox ID="Chk_IsWebBasedApplications" Visible="false"  runat="server" AutoPostBack="True" Checked="false" oncheckedchanged="Chk_portalIsforServer_CheckedChanged" Text="IsWebBasedApplications" />                                   
                            </td>
                            </tr>
                        </table>
                        
                    </fieldset>
                   
                     <asp:Panel ID="pnlwebsitecretion" runat="server" Visible="false">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label8" runat="server" Text="Select there the Portal Subscriber's website will be created"></asp:Label>
                        </legend>                       
                        <asp:Panel ID="Panel10" runat="server" Visible="true">
                        <table width="100%" cellpadding="0" cellspacing="0">                           
                            <tr>
                                <td align="left">
                                     <table>
                                     <tr>
                                     <td colspan="3">
                                     <label style="width:800px;">
                                     Whether  the site of the protal subscriber will be created ?
                                     </label> 
                                     </td>                                                                        
                                     </tr>                                    
                                     <tr>
                                     <td colspan="4">
                                     <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Vertical" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" Width="100%">
                                            <asp:ListItem Selected="True" Value="1">Yes, The separate site will be created for each subscriber</asp:ListItem>
                                            <asp:ListItem Value="0">No, Seprate site will be created for the subscriber , the new record will be created in existing site for the protal for
                                            <br />  eg. Each company subscriber of ijobcenter.com is a part o the same site and not separate site for each company of ijob </asp:ListItem>
                                        </asp:RadioButtonList>                           
                                     </td>                                     
                                     </tr>
                                     <tr>
                                     <td colspan="4">
                                            <asp:Panel ID="Panel6" runat="server" Visible="False">
                                                     <table width="100%">
                                                        <tr>
                                                            <td style="width: 30%">
                                                                <label style="width:380px;">
                                                                    This portal is run by the name of which Company ID
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:DropDownList ID="DropDownList1" runat="server" 
                                                                    onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                                                    </asp:DropDownList>
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
                              <asp:Panel ID="Panel11" runat="server" Visible="false">
                               </asp:Panel>    
                           <tr>
                                <td>
                                    <label>
                                         <asp:Label ID="lbl_websiteoption" runat="server" Text="The product < Show selected product from above ddl> has below of websites available, which website / would you like to create for subscribers"></asp:Label>                                                                            
                                    </label> 
                                  <asp:Panel ID="pnlServerForPortal" runat="server" Visible="false">
                                     <table>                               
                                         <tr>
                                            <td colspan="2">
                                                    <asp:Panel ID="pnl_websiter1" runat="server" Visible="true">                          
                                                    <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                                        <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                                         <input runat="server" id="hdnProductDetailId" type="hidden" name="hdnProductDetailId" style="width: 3px" />
                                                               <asp:Panel ID="Panel12" runat="server" Visible="false"  ScrollBars="Horizontal" Height="200px">
                                                               <asp:Panel ID="pnlpr" runat="server" Width="800px"  ScrollBars="Horizontal" Height="200px">
                                                                    <asp:GridView ID="GridView3" runat="server" DataKeyNames="WebsiteID" AutoGenerateColumns="False"
                                                                        OnRowCommand="GridView3_RowCommand" EmptyDataText="There is no data." AllowSorting="True"
                                                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                     OnRowDataBound="GridView3_RowDataBound"   Width="100%" OnPageIndexChanging="GridView3_PageIndexChanging" OnSorting="GridView3_Sorting"
                                                                         OnSelectedIndexChanged="GridView3_SelectedIndexChanged">
                                                                        <Columns>
                                                                            <asp:TemplateField ControlStyle-Width="5%" FooterStyle-Width="5%" HeaderStyle-Width="5%"  >
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="cbHeader" runat="server" Visible="false"  OnCheckedChanged="ch1_chachedChanged" AutoPostBack="true" />
                                                                                    <asp:Label ID="check" runat="server" ForeColor="White" Text="" HeaderStyle-Width="30px" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="cbItem" Checked='<%# Bind("chk") %>'  runat="server" Enabled="true"  />
                                                                                    <asp:CheckBox ID="chkdef" Checked='<%# Bind("chk") %>' runat="server" Visible="false" />                                                                                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Website URL" SortExpression="ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("WebsiteUrl") %>'></asp:Label>                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>   
                                                                            <asp:TemplateField HeaderText="Website Name" SortExpression="ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblp" runat="server" Text='<%# Bind("WebsiteName") %>'></asp:Label>
                                                                                    <asp:Label ID="lblwebid" runat="server" Text='<%# Bind("WebsiteID") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_codetypeid" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Suggested IIS website Name" SortExpression="ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label53" runat="server" Text="Demo."></asp:Label>                                                                                                                                                                   
                                                                                    <asp:Label ID="lblsugges" runat="server" Text='<%# Bind("WebsiteUrl") %>'></asp:Label>                                                                                                                                                                   
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                                                                                                     
                                                       
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="GridView4" runat="server" DataKeyNames="ProductCodeDatabasDetailID" AutoGenerateColumns="False"  Width="100%" 
                                                                         EmptyDataText="There is no data." AllowSorting="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"                                                                        
                                                                     OnRowDataBound="GridView4_RowDataBound"  OnPageIndexChanging="GridView4_PageIndexChanging" OnSorting="GridView4_Sorting"
                                                                       OnRowCommand="GridView4_RowCommand"  OnSelectedIndexChanged="GridView4_SelectedIndexChanged">
                                                                        <Columns>
                                                                            <asp:TemplateField ControlStyle-Width="5%" FooterStyle-Width="5%" HeaderStyle-Width="5%"  >
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="cbHeader" runat="server" Visible="false"  OnCheckedChanged="ch4_chachedChanged" AutoPostBack="true" />
                                                                                    <asp:Label ID="check" runat="server" ForeColor="White" Text="" HeaderStyle-Width="30px" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="cbItem" Checked='<%# Bind("chk") %>'  runat="server" Enabled="true"  />
                                                                                    <asp:CheckBox ID="chkdef" Checked='<%# Bind("chk") %>' runat="server" Visible="false" />                                                                                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Database" SortExpression="ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("CodeTypeName") %>'></asp:Label>                                                                                    
                                                                                     <asp:Label ID="lbl_codedetailid" runat="server" Text='<%# Bind("ProductCodeDatabasDetailID") %>' Visible="false"></asp:Label>       
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField> 
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                                </asp:Panel>
                                                            </asp:Panel>
                                            </td>  
                                         </tr> 
                                         <tr>
                                             <td colspan="2" align="left">
                                   <table width="98%" style="text-align:left;"> 
                                    <tr>
                                        <td colspan="2">
                                        <asp:CheckBox ID="chk_OwnServer" runat="server" AutoPostBack="True" Checked="false" oncheckedchanged="chk_OwnServer_CheckedChanged" Text="Allowed to be hosted at customer's option on  Customer's own server at its location " />                                                                                                
                                        </td>
                                    </tr>  
                                                                     
                                    <tr>
                                        <td colspan="2">
                                            ( This will give a link to customer on successful subscription of portal to download the satellite server software and the custoemr will have to setup the satellite server at his location )
                                        </td>
                                    </tr>
                                     <tr>
                                            <td colspan="2" id="td4">
                                                     <asp:CheckBox ID="chkBusiwizServer" runat="server" AutoPostBack="True" Checked="false" oncheckedchanged="chkBusiwizServer_CheckedChanged" Text="Allowed to be hosted on Clients's ( for eg Busiwiz) server " />                                                       
                                            </td>
                                                                                              
                                    </tr>
                                    <tr>
                                    <td colspan="2">
                                    <asp:Panel ID="pnlbusiwizserver" runat="server" Visible="false">
                                            <%--/Allo To Host on Client Server/--%>
                                    <table width="98%" style="text-align:left;">    
                                        <tr>
                                         <td style="width:5%">
                                        
                                        </td>
                                        <td>
                                        <asp:CheckBox ID="chbbusiServer" runat="server" AutoPostBack="True" Checked="false" oncheckedchanged="chbbusiServer_CheckedChanged" Text="Option to host on secured common servershared with multiple other companies." />
                                        </td>
                                                                                              
                                        </tr>
                                        <tr>
                                        <td style="width:5%">                                        
                                        </td>
                                            <td>                                            
                                            <asp:Panel ID="pnlcommon" runat="server" Visible="false">  
                                                                        <table style="vertical-align: baseline; border: thin groove #FFFFFF">
                                                                            <tr>
                                                                                <td>                                                                                 
                                                                                <label>
                                                                                List of servers and the priority
                                                                                </label> 
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                            <td>
                                                                                <asp:Button ID="Button3" runat="server" onclick="Btnchangerank_Click" Text="Change Rank" UseSubmitBehavior="False" Visible="false"  />
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            </tr>
                                                                            <tr>
                                                                            <td colspan="2">
                                                                                            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mGrid"  onrowdatabound="GridView2_RowDataBound" PagerStyle-CssClass="prg"  Width="100%" onpageindexchanging="GridView2_PageIndexChanging" PageSize="10">
                                                                                                     <Columns>
                                                                                                    <asp:TemplateField HeaderText="pageid">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblserverid" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Rank">                                                                                                                
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtgrid_Priority" runat="server" AutoPostBack="True" 
                                                                                                                        ontextchanged="txtgrid_Priority_TextChanged"  Text='<%#Eval("Priority") %>'  Width="70px"></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Server Name">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="Label19" runat="server" Text='<%#Eval("ServerName") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Total Allow Company">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:LinkButton ID="LinkButton9" runat="server"  CommandArgument='<%# Eval("Id") %>' CommandName="View1" ForeColor="Gray" 
                                                                                                                        Text='<%# Eval("MaxCompSharing") %>' Enabled="false"  ToolTip="View Page"></asp:LinkButton>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                          
                                                                                                            <asp:TemplateField Visible="false" >
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:ImageButton ID="ImageButton3" runat="server" 
                                                                                                                        ImageUrl="~/images/delete.gif" onclick="ImageButton3_Click" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                             <PagerStyle CssClass="pgr" />
                                                                                             </asp:GridView>
                                                                            </td>
                                                                            </tr>                                                                          
                                                                        </table> 
                                             </asp:Panel>                                            
                                            </td>
                                        </tr>
                                       
                                      
                                        <tr>
                                         <td>
                                        
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkLeaseServer" runat="server" AutoPostBack="True" Checked="false" oncheckedchanged="chkLeaseServer_CheckedChanged" Text="Host on your exclusive separate secured ( physical not virtual) server in server farm of busiwiz .  " />
                                                                                                  
                                        </td>
                                    </tr>
                                        <tr>
                                         <td>
                                        
                                        </td>
                                      
                                        <td>
                                                (Only sites required for your company would be hosted on server. It will be considerably higher in performance and security compared to other shared server or common server options)
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                     <td>
                                        
                                        </td>
                                        <td>
                                        </td>
                                        </tr>
                                    <tr>
                                     <td>
                                        
                                        </td>
                                        <td>
                                        <asp:CheckBox ID="chkSharedServer" runat="server" AutoPostBack="True" Checked="false" oncheckedchanged="chkSharedServer_CheckedChanged" Text="Host on your Limited shared secured ( physical not virtual) server in server farm of busiwiz . " />
                                                                                                
                                        </td>
                                    </tr>                                    
                                    <tr>
                                     <td>
                                        
                                        </td>
                                        <td>
                                            (Only sites required for all sharing companies would be hosted on server. It will be considerably higher in performance and security compared to common server options) 
                                        </td>
                                    </tr>
                                     <tr>
                                      <td>
                                        
                                        </td>
                                            <td>
                                             
                                            </td>                                                                                              
                                    </tr>
                                    <tr>
                                     <td>
                                        
                                        </td>
                                        <td>
                                        <asp:CheckBox ID="chksale" runat="server" AutoPostBack="True" Checked="false" oncheckedchanged="chksale_CheckedChanged" Text="Hosted on Clients Own server in Busiwiz Server farm. " />
                                                                                                
                                        </td>
                                    </tr>   
                                     <tr>
                                              <td>                                        
                                              </td>
                                              <td>
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
                                                   
                               </table> 
                            </td>
                            </tr>                             
                        </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel9" runat="server" Visible="False">
                            <table width="100%">
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            This portal is run by the name of which Company ID
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="DropDownList2" runat="server" 
                                            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                      
                    </asp:Panel>
                    <fieldset>
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btnSubmit" Text="Submit" ValidationGroup="1" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="BtnUpdate" runat="server" CssClass="btnSubmit" OnClick="BtnUpdate_Click"
                                        Text="Update" ValidationGroup="1" Visible="False" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                  
                    </asp:Panel>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label18" runat="server" Text="List of Product Portal Details "></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            OnClick="Button1_Click1" />
                        <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                               
                                   <label>                                   
                                   Filter by Product Name
                                   <asp:DropDownList ID="ddlProductname" runat="server" DataTextField="ProductName" DataValueField="ProductId" AutoPostBack="True" OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                  
                                   </label>
                                    
                                  
                                    
                               
                                
                               <label  style="width:220px;">
                                 <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Checked="True" oncheckedchanged="CheckBox1_CheckedChanged" Text="Show only active " style="width:100%;" />                                       
                                </label>
                                <label style="width:200px;">
                                        Search
                                <asp:TextBox ID="TextBox1" runat="server" Width="180px"></asp:TextBox>                                       
                                </label>
                               <label style="width:10px;">
                               
                               <asp:Label ID="Label70" runat="server" Text="Filter by Product Name" Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddlsrechportal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsrechportal_SelectedIndexChanged" Width="160px" Visible="false">
                                    </asp:DropDownList>
                                        <asp:DropDownList ID="ddlversion" runat="server" DataTextField="ProductName" DataValueField="ProductId" AutoPostBack="True" OnSelectedIndexChanged="ddlversion_SelectedIndexChanged" Visible="false">
                                    </asp:DropDownList>
                                    
                                </label>
                                   <label style="width:25px;">
                                   <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Go" ValidationGroup="1" OnClick="btnSubmit_ClickGo" />
                                   </label> 
                                </td>
                        </tr>
                        
                    </table>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="850Px">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label30" runat="server" Font-Italic="true" Text="List of Product Portal Details"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
                                        EmptyDataText="No Record Found." AllowPaging="True" Width="100%" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing1"
                                        OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName" ItemStyle-Width="15%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("ProductName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Portal Name" SortExpression="VersionInfoName" ItemStyle-Width="15%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblportalnameName" runat="server" Text='<%# Bind("PortalName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email Display Name" SortExpression="WebsiteName" ItemStyle-Width="15%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemaildisplaynameName" runat="server" Text='<%# Bind("EmailDisplayname")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Support Team EmailID" SortExpression="WebsiteUrl"
                                                ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsupportemailid" runat="server" Text='<%# Bind("Supportteamemailid")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Support Team Phone Number" SortExpression="WebsiteUrl"
                                                ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsuupotphoneno" runat="server" Text='<%# Bind("Supportteamphoneno")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Support Team Manager Name" SortExpression="WebsiteUrl"
                                                ItemStyle-Width="12%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsuupotteammanagername" runat="server" Text='<%# Bind("Supportteammanagername")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="12%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Portal Marketing Website Name" SortExpression="WebsiteUrl"
                                                ItemStyle-Width="18%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblportalmarketingwebsitename" runat="server" Text='<%# Bind("Portalmarketingwebsitename")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="18%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                    <asp:Label ID="lblproductid2" Visible="false" runat="server" Text="Active"></asp:Label>
                                                    <asp:Label ID="lblproductid3" Visible="false" runat="server" Text="Inactive"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <%--   <asp:ButtonField CommandName="edit" Text="Edit" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" ImageUrl="~/Account/images/edit.gif"
                                            ButtonType="Image" ItemStyle-Width="3%">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="3%" />
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/trash.jpg"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                             </asp:ButtonField>--%>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnedit" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                        ToolTip="Edit" CommandName="edit" ImageUrl="~/Account/images/edit.gif" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="0.5%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <%--     <asp:ButtonField CommandName="delete" Text="Delete" HeaderText="Delete" HeaderStyle-HorizontalAlign="Left"
                                        HeaderImageUrl="~/ShoppingCart/images/trash.jpg" ImageUrl="~/images/delete.gif"
                                        ButtonType="Image" ItemStyle-Width="3%">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="3%" />
                                    </asp:ButtonField>--%>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtndelete" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                        ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="0.5%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="Panel8" runat="server">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlMainTypeAdd" runat="server" BackColor="White" BorderColor="#999999"
                                        Width="80%"  ScrollBars="None" BorderStyle="Solid" BorderWidth="2px">
                                        <asp:UpdatePanel ID="UpdatePanelMainTypeAdd" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                               
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 95%; font-size: large; font-weight: bold; text-align: center">
                                                                <label>
                                                                <asp:DropDownList ID="ddlserchstatus" runat="server" AutoPostBack="True" 
                                                                    OnSelectedIndexChanged="ddlserchstatus_SelectedIndexChanged" Visible="False" 
                                                                    Width="150px">
                                                                    <asp:ListItem Selected="True" Value="2">All</asp:ListItem>
                                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                                                </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                            <td style="width: 5%">
                                                                <div style="text-align: right; border-style: none">
                                                                    <asp:ImageButton ID="ImageButton8" Height="15px" Width="15px" BorderStyle="None"
                                                                        runat="server" ImageUrl="~/images/closeicon.png" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <label>
                                                                    For each new portal, It is advisable you to create a new company in any of the portal
                                                                    of the above mentioned "Product" and remember the name of the database it created
                                                                    for that company and then you create this new portal and select in the drop down
                                                                    the database of that newly created company.
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <label>
                                                                <asp:Label ID="Label83" runat="server" Text="Filter by Status" Visible="False"></asp:Label>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                               
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender9" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="pnlMainTypeAdd" TargetControlID="hdnMaintypeAdd" CancelControlID="ImageButton8"
                                        Drag="true">
                                    </cc1:ModalPopupExtender>
                                    <input id="hdnMaintypeAdd" runat="Server" name="hdnMaintypeAdd" type="hidden" style="width: 4px" />
                                </td>
                            </tr>

                             <tr>
                            <td>
                              <asp:Panel ID="Paneldoc" runat="server" Width="55%" CssClass="modalPopup">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="Label142" runat="server" Text=""></asp:Label>
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 95%;">
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <label>
                                                        <asp:Label ID="Label143" runat="server" Text="Was this the last record you are going to add right now to this table?"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdsync" runat="server">
                                                                    <asp:ListItem Value="1" Text="Yes, this is the last record in the series of records I am inserting/editing to this table right now"></asp:ListItem>
                                                                    <asp:ListItem Value="0" Text="No, I am still going to add/edit records to this table right now"
                                                                        Selected="True"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="btnok" runat="server" CssClass="btnSubmit" Text="OK" OnClick="btndosyncro_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Paneldoc" TargetControlID="btnreh" CancelControlID="ImageButton10">
                                </cc1:ModalPopupExtender>
                            </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
    <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server"  ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>

</asp:Content>
