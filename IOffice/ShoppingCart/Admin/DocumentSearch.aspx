<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="DocumentSearch.aspx.cs"
    Inherits="Account_DocumentMyUploaded" Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%--<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
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
    </script>

    <script language="javascript" type="text/javascript">
        function RealNumWithDecimal(myfield, e, dec) {


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

            
    </script>

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

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlentry" runat="server" Visible="false" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lbldoyou" runat="server" Text=" Entry No"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lbleno" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label3" runat="server" Text=" Entry Type"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lbletype" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label4" runat="server" Text="Transaction MasterId"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lbltid" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Business Name"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                 <label>
                                    <asp:Label ID="Label14" runat="server" Text="Document Type"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddldoctypem" runat="server" AutoPostBack="True" >
                                    </asp:DropDownList>
                                </label>
                                 <label>
                                    <asp:Label ID="Label35" runat="server" Text="Document Flag"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlflag" runat="server" AutoPostBack="True" >
                                    </asp:DropDownList>
                                </label>
                                <label>
                                
                                </label>
                                <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text=" Search By"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlSearchby" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchby_SelectedIndexChanged">
                                                    <asp:ListItem Text="Document ID, Title, Party or Contact Name" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Cabinet, Drawer, Folder" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="User Category: User Name: Contact Person Name" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Panel Width="100%" ID="pnlParty" runat="server" Visible="False">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <%--   <asp:Label ID="Label14" runat="server" Text="User category: user name: contact person name"></asp:Label>--%>
                                                                <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlParty"
                                                                    Display="Dynamic" ErrorMessage="*" InitialValue="0" ValidationGroup="3" SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                                            </label>
                                                            <label>
                                                                <asp:DropDownList ID="ddlParty" runat="server" Width="400px">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlID" runat="server" Visible="False">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label8" runat="server" Text=" Search By"></asp:Label>
                                                                <asp:Label ID="Label9" runat="server" Text="*"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDocId"
                                                                    ErrorMessage="*" ValidationGroup="3" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="Invalid Character"
                                                                    SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtDocId"
                                                                    ValidationGroup="3" Display="Dynamic"></asp:RegularExpressionValidator>
                                                            </label>
                                                            <label>
                                                                <asp:TextBox ID="txtDocId" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',25)"
                                                                    MaxLength="25"> </asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label6" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="div1" class="labelcount">25</span>
                                                                <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel Width="100%" ID="pnlType" runat="server" Visible="False">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <table width="100%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label10" runat="server" Text="Cabinet"></asp:Label>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlmaindotype" runat="server" AutoPostBack="True" DataTextField="DocumentMainType"
                                                                            DataValueField="DocumentMainTypeId" OnSelectedIndexChanged="ddlmaindotype_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label12" runat="server" Text="Drawer "></asp:Label>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlsubdoctype" runat="server" AutoPostBack="True" DataTextField="DocumentSubType"
                                                                            DataValueField="DocumentSubTypeId" OnSelectedIndexChanged="ddlsubdoctype_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label13" runat="server" Text="Folder "></asp:Label>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddldoctype" runat="server" DataTextField="DocumentType" DataValueField="DocumentTypeId">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlmaindotype" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlsubdoctype" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 185px">
                                <label>
                                    <asp:Label ID="Label16" runat="server" Text="Filter by Accounting Entry"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="rdlistaccentry" runat="server">
                                                    <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                                    <asp:ListItem Value="1">With No Accounting Entry</asp:ListItem>
                                                    <asp:ListItem Value="2">With Accounting Entries</asp:ListItem>
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnldate" runat="server" Visible="False">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label17" runat="server" Text="Filter by Period"></asp:Label>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:DropDownList ID="RadioButtonList1" runat="server">
                                                                            <asp:ListItem Selected="True">Document Date</asp:ListItem>
                                                                            <asp:ListItem>Document Upload Date</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="Label18" runat="server" Text="Start Date"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfrom"
                                                                            ErrorMessage="*" ValidationGroup="3" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </label>
                                                                    <label>
                                                                        <asp:TextBox ID="txtfrom" Width="80px" runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgbtncalfrom"
                                                                            TargetControlID="txtfrom">
                                                                        </cc1:CalendarExtender>
                                                                    </label>
                                                                    <label>
                                                                        <asp:ImageButton ID="imgbtncalfrom" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="Label19" runat="server" Text="End Date"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtto"
                                                                            ErrorMessage="*" ValidationGroup="3" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </label>
                                                                    <label>
                                                                        <asp:TextBox ID="txtto" Width="80px" runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgbtnto"
                                                                            TargetControlID="txtto">
                                                                        </cc1:CalendarExtender>
                                                                    </label>
                                                                    <label>
                                                                        <asp:ImageButton ID="imgbtnto" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="CheckBox1" EventName="CheckedChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <asp:Panel Width="100%" ID="Panel4" runat="server" Visible="False">
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" AutoPostBack="True" Text="Select Period"
                                            OnCheckedChanged="CheckBox1_CheckedChanged" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="imgbtnsubmit" CssClass="btnSubmit" runat="server" Text=" Go " OnClick="imgbtnsubmit_Click"
                                    ValidationGroup="3" />
                                <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" OnClick="Button1_Click"
                                    Text="Go Back" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllege" runat="server" Text="List of Documents" Font-Bold="true"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" style="width: 60%;">
                                            <label>
                                                <asp:Button ID="Button3" CssClass="btnSubmit" runat="server" Text="Select Display Columns"
                                                    OnClick="Button3_Click" />
                                            </label>
                                            <label>
                                                <asp:Button ID="Button5" CssClass="btnSubmit" runat="server" Text="Refresh" OnClick="Button5_Click" />
                                            </label>
                                        </td>
                                         <td align="right">
                                         <label>
                                           <asp:Label ID="Label36" runat="server" Text="Set Flag" Font-Bold="true"></asp:Label>
                                           </label>
                                              <asp:CheckBox ID="chkflag" runat="server" AutoPostBack="true" 
                                                oncheckedchanged="chkflag_CheckedChanged" />
                                        </td>
                                      
                                        <td align="right" style="width:05px;">
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btnflagstatus" CssClass="btnSubmit" runat="server" 
                                                Text="Update Flag" onclick="btnflagstatus_Click"
                                               />
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="Button4" CssClass="btnSubmit" runat="server" Text="Upload Documents"
                                                OnClick="Button4_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btncopydocument" CssClass="btnSubmit" runat="server" Text="Copy/Move the Selected Document"
                                                OnClick="btncopydocument_Click" Visible="False" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnsendemail" CssClass="btnSubmit" runat="server" Text="Attach the Selected Documents to Email"
                                                OnClick="btnsendemail_Click" Visible="False" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnsendmsg" CssClass="btnSubmit" runat="server" Text="Attach the Selected Documents to a Message"
                                                OnClick="btnsendmsg_Click" Visible="False" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnmyfolder" CssClass="btnSubmit" runat="server" Text="Add the Selected Documents to My Folder"
                                                OnClick="btnmyfolder_Click" Visible="False" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Print and Export"
                                                OnClick="Button2_Click" />
                                        </td>
                                        <td>
                                            <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                                class="btnSubmit" type="button" value="Print" visible="false" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlExport" runat="server" OnSelectedIndexChanged="ddlExport_SelectedIndexChanged"
                                                AutoPostBack="True" Width="130px" Visible="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="float: left;">
                                    <asp:Panel Width="100%" ID="Panel6" runat="server" Visible="False">
                                        <div>
                                            <asp:CheckBox ID="chkidcolumn" runat="server" Checked="true" />
                                            <label>
                                                <asp:Label ID="Label29" runat="server" Text="ID"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chktitlecolumn" runat="server" Checked="true" />
                                            <label>
                                                <asp:Label ID="Label30" runat="server" Text="Title"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chkfileextsion" runat="server" Checked="true" />
                                            <label>
                                                <asp:Label ID="Label31" runat="server" Text="File Extension"></asp:Label>
                                            </label>
                                            
                                            <asp:CheckBox ID="chkdoctm" runat="server" Checked="true" />
                                            <label>
                                                <asp:Label ID="lbldct" runat="server" Text="Document Type"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chkfoldername" runat="server" Checked="true" />
                                            <label>
                                                <asp:Label ID="Label32" runat="server" Text="Folder Name"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chkpartycolumn" runat="server" Checked="false" />
                                            <label>
                                                <asp:Label ID="Label15" runat="server" Text="Party"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chkdocumentdate" runat="server" Checked="false" />
                                            <label>
                                                <asp:Label ID="Label33" runat="server" Text="Doc Date"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chkuploaddate" runat="server" Checked="true" />
                                            <label>
                                                <asp:Label ID="Label34" runat="server" Text="Upload Date"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chkmyfoldercolumn" runat="server" Checked="false" />
                                            <label>
                                                <asp:Label ID="Label22" runat="server" Text="My Folder"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chkaddtomyfoldercolumn" runat="server" Checked="false" />
                                            <label>
                                                <asp:Label ID="Label23" runat="server" Text="Add to My Folder"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chkaccountentrycolumn" runat="server" Checked="false" />
                                            <label>
                                                <asp:Label ID="Label25" runat="server" Text="Accounting Entry"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chksendmessagecolumn" runat="server" Checked="false" />
                                            <label>
                                                <asp:Label ID="Label26" runat="server" Text="Send Message"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chksendmailcolumn" runat="server" Checked="true" />
                                            <label>
                                                <asp:Label ID="Label27" runat="server" Text="Send Mail"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chkcopycolumn" runat="server" Checked="false" />
                                            <label>
                                                <asp:Label ID="Label28" runat="server" Text="Copy/Move"></asp:Label>
                                            </label>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblcompny" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="20px"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="Label21" runat="server" Text="Business :" Font-Italic="true" Font-Bold="True"
                                                                    Font-Size="18px"></asp:Label>
                                                                <asp:Label ID="lblcomname" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="18px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblhead" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="18px"
                                                                    Text="List of Documents"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblserchcabi" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="16px"
                                                                    Text="Cabinet - Drawer - Folder :"></asp:Label>
                                                                <asp:Label ID="lblserchdrowe" runat="server" Font-Italic="true" Font-Bold="True"
                                                                    Font-Size="16px"></asp:Label>
                                                                <asp:Label ID="lblserfolder" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="16px"
                                                                    Visible="False"></asp:Label>
                                                                <asp:Label ID="lbldate" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="16px"
                                                                    Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel2" runat="server" Width="100%">
                                                    <cc11:PagingGridView ID="Gridreqinfo" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" DataKeyNames="DocumentTypeId" EmptyDataText="No Record Found."
                                                        OnPageIndexChanging="Gridreqinfo_PageIndexChanging" OnRowCommand="Gridreqinfo_RowCommand"
                                                        Width="100%" PageSize="25" OnSorting="Gridreqinfo_Sorting">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Left" SortExpression="DocumentMaster.DocumentId"
                                                                HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" ForeColor="Black" runat="server" Text='<%#Eval("DocumentId") %>'
                                                                        CommandName="Send" CommandArgument='<%#Eval("DocumentId") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="DocumentTitle" HeaderStyle-HorizontalAlign="Left" HeaderText="Title"
                                                                SortExpression="DocumentTitle"></asp:BoundField>
                                                            <asp:BoundField HeaderText="File Extension" DataField="FileExtensionType" HeaderStyle-HorizontalAlign="Left"
                                                                SortExpression="FileExtensionType"></asp:BoundField>
                                                                 <asp:TemplateField HeaderText="Document Type" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldoctype" runat="server" Text='<%#Eval("Doccname") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="DocumentType" HeaderText="Folder Name" SortExpression="DocumentType"
                                                                HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                            <asp:BoundField DataField="PartyName" HeaderText="Party" SortExpression="PartyName"
                                                                HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Doc Date" HeaderStyle-HorizontalAlign="Left" DataField="DocumentDate"
                                                                SortExpression="DocumentDate" DataFormatString="{0:MM-dd-yyyy}"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Upload Date" HeaderStyle-HorizontalAlign="Left" DataField="DocumentUploadDate"
                                                                SortExpression="DocumentUploadDate" DataFormatString="{0:MM-dd-yyyy}"></asp:BoundField>
                                                            <asp:TemplateField HeaderText="My Folder" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Black" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>'
                                                                        CausesValidation="false" CommandName="openfolder" Text="My Folder"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="LinkButton3" ForeColor="White" runat="server" CausesValidation="false"
                                                                        AutoPostBack="True" OnClick="LinkButton3_Click" Text="Add to My Folder" Visible="false"> </asp:LinkButton>
                                                                    <asp:Label ID="Label24" runat="server" Text="Add to My Folder" ForeColor="White"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lbladdfoldertext" runat="server" Text="All" ForeColor="White"></asp:Label>
                                                                    <asp:CheckBox ID="chkbox" runat="server" AutoPostBack="True" OnCheckedChanged="chkbox_CheckedChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chksubbox" AutoPostBack="True" OnCheckedChanged="chksubbox_checkedChanged"
                                                                        runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Accounting Entry" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="Black" CausesValidation="false"
                                                                        CommandName="associate"> </asp:LinkButton>
                                                                    <br />
                                                                    <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="Black" CausesValidation="false"
                                                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>' CommandName="more"> </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="linkmsg" runat="server" ForeColor="White" CausesValidation="false"
                                                                        AutoPostBack="True" OnClick="linkmsg_Click" Text="Send Message" Visible="false"></asp:LinkButton>
                                                                    <asp:Label ID="Label244654" runat="server" Text="Send Message" ForeColor="White"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lbladdfoldertext456" runat="server" Text="All" ForeColor="White"></asp:Label>
                                                                    <asp:CheckBox ID="chkboxmsg" runat="server" AutoPostBack="True" OnCheckedChanged="chkboxmsg_CheckedChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkmsg" runat="server" AutoPostBack="True" OnCheckedChanged="chkmsg_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="linkmail" runat="server" ForeColor="White" CausesValidation="false"
                                                                        AutoPostBack="True" OnClick="linkmail_Click" Text="Send Mail" Visible="false"></asp:LinkButton>
                                                                    <asp:Label ID="Label244654uuh87" runat="server" Text="Send Mail" ForeColor="White"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lbladdfoldertext123" runat="server" Text="All" ForeColor="White"></asp:Label>
                                                                    <asp:CheckBox ID="chkboxmail" runat="server" AutoPostBack="True" OnCheckedChanged="chkboxmail_CheckedChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkmail" runat="server" AutoPostBack="True" OnCheckedChanged="chkmail_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="linkcopy" ForeColor="White" runat="server" CausesValidation="false"
                                                                        AutoPostBack="True" OnClick="linkcopy_Click" Text="Copy/Move" Visible="false"></asp:LinkButton>
                                                                    <asp:Label ID="Label244888" runat="server" Text="Copy/Move" ForeColor="White"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lbladdfoldertext456888" runat="server" Text="All" ForeColor="White"></asp:Label>
                                                                    <asp:CheckBox ID="chkboxcopy" runat="server" AutoPostBack="True" OnCheckedChanged="chkboxcopy_CheckedChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkcopy" runat="server" AutoPostBack="True" OnCheckedChanged="chkcopy_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             
                                                             <asp:TemplateField HeaderText="Flag" HeaderStyle-HorizontalAlign="Left" 
                                                               >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblflag"  runat="server" ></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Flag type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="75px"
                                                              >
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlflag"  runat="server" Width="72px" ></asp:DropDownList>
                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif"
                                                                ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <%--<a onclick="window.open('DocumentEditAndView.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                                        href="javascript:void(0)">--%>
                                                                        <asp:ImageButton ID="ImageButton1"  runat="server" CommandName="Editview" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>' ImageUrl="~/Account/images/edit.gif"
                                                                            ToolTip="Edit" />
                                                                  <%--  </a>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                                ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>'
                                                                        CommandName="delete1" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                        ToolTip="Delete" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                                                HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkaccentry" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </cc11:PagingGridView>
                                                </asp:Panel>
                                                <input id="hdncnfm" type="hidden" name="hdncnfm" runat="Server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="Accdocadd" runat="server" CssClass="btnSubmit" Text=" Add " Visible="false"
                                    OnClick="Accdocadd_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input id="hdnconfirm" runat="Server" name="hdnconfirm" type="hidden" style="width: 4px" />
                                <input id="Hidden3" name="Hidden3" runat="Server" type="hidden" />
                                <asp:Panel ID="pnlSearchShow" runat="server" Width="700px" BorderColor="#666666"
                                    BorderStyle="Outset" Height="550px" BackColor="#CCCCCC">
                                    <table id="innertbl1">
                                        <tr>
                                            <td>
                                                <table id="subinnertbl1" cellpadding="0" cellspacing="">
                                                    <tr>
                                                        <td>
                                                            &nbsp;<asp:Label ID="lblmsgDocSearch" runat="server" Text="List of selected documents  to copy to my folders"
                                                                Font-Bold="True"> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ibtnSearchClose" runat="server" ImageUrl="~/Account/images/closeicon.png">
                                                            </asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table id="subinnertbl" cellpadding="0" cellspacing="0" style="height: 500px;" width="690px">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="asdvv" runat="server" Height="220px" ScrollBars="Both">
                                                                            <asp:GridView ID="GridView2" runat="server" Width="673px" AutoGenerateColumns="False"
                                                                                CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                                DataKeyNames="DocumentId" EmptyDataText="No one Folder Created .." AllowPaging="false">
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="DocumentId" HeaderText="Document ID" />
                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="DocumentTitle" HeaderText="Document Title" />
                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="PartyName" HeaderText="Party" />
                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="DocumentType" HeaderText="Folder Name" />
                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="DocumentUploadDate"
                                                                                        HeaderText="Date" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="LinkButton5" runat="server" Font-Bold="False" OnClick="LinkButton5_Click">Add New Personal Folder</asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel Visible="false" Width="100%" ID="Panel1" runat="server">
                                                                            <table cellpadding="0" cellspacing="0" id="Table4">
                                                                                <tr>
                                                                                    <td>
                                                                                        <label>
                                                                                            <asp:Label ID="lblflder" runat="server" Text="Folder Name"></asp:Label>
                                                                                            <asp:Label ID="Label20" runat="server" Text="*"></asp:Label>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldVator3" runat="server" ControlToValidate="txtFoldername"
                                                                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*"
                                                                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                                                                                ControlToValidate="txtFoldername" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                                        </label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <label>
                                                                                            <asp:TextBox ID="txtFoldername" runat="server" ValidationGroup="1" MaxLength="100"></asp:TextBox>
                                                                                        </label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="ImageButton3" CssClass="btnSubmit" runat="server" Text="Submit" ValidationGroup="1"
                                                                                            OnClick="ImageButton3_Click" />
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Label ID="Label1" runat="server" Text="Add the Selected Documents to the Following Folders"
                                                                                Font-Bold="True"> </asp:Label>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table id="GridTbl">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Panel ID="Panel3" runat="server" Height="200px" ScrollBars="Both">
                                                                                        <asp:GridView ID="Gridreqinfos" runat="server" CssClass="mGrid" GridLines="Both"
                                                                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                                            DataKeyNames="FolderID" OnRowCommand="Gridreqinfos_RowCommand" EmptyDataText="No one Folder Created .."
                                                                                            AllowPaging="True" OnPageIndexChanging="Gridreqinfos_PageIndexChanging" Width="673px">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                                    <EditItemTemplate>
                                                                                                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                                                                                    </EditItemTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="FolderName" HeaderStyle-HorizontalAlign="Left" HeaderText="Folder Name" />
                                                                                                <asp:ButtonField CommandName="detail" ItemStyle-ForeColor="#416271" HeaderStyle-HorizontalAlign="Left"
                                                                                                    HeaderText="Open" Text="Open" />
                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                                    <EditItemTemplate>
                                                                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FolderName") %>'></asp:TextBox>
                                                                                                        <asp:RegularExpressionValidator ID="REG5" runat="server" ErrorMessage="*" Display="Dynamic"
                                                                                                            SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)" ControlToValidate="TextBox1"
                                                                                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                                                    </EditItemTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                                                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="DocumentId" HeaderStyle-HorizontalAlign="Left" HeaderText="Doc ID" />
                                                                                                                <asp:BoundField DataField="DocumentTitle" HeaderStyle-HorizontalAlign="Left" HeaderText="Doc Title" />
                                                                                                                <asp:BoundField DataField="PartyName" HeaderStyle-HorizontalAlign="Left" HeaderText="Party" />
                                                                                                                <asp:BoundField DataField="DocumentType" HeaderStyle-HorizontalAlign="Left" HeaderText="Doc Type" />
                                                                                                                <asp:BoundField DataField="DocumentAddedDate" HeaderStyle-HorizontalAlign="Left"
                                                                                                                    DataFormatString="{00:MM/dd/yyyy}" HeaderText="Add Date" />
                                                                                                                <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Left" ShowHeader="False">
                                                                                                                    <ItemTemplate>
                                                                                                                        <a href="javascript:void(0)" onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,top=0,left=0,menubar=no,status=no')">
                                                                                                                            <asp:Label ID="Label2122" runat="server" ForeColor="#416271" Text="View"></asp:Label>
                                                                                                                        </a>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                            </Columns>
                                                                                                            <HeaderStyle BackColor="Goldenrod" Font-Bold="True" ForeColor="White" />
                                                                                                        </asp:GridView>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button ID="imagebtnSubmit" CssClass="btnSubmit" runat="server" Text="Add" OnClick="imagebtnSubmit_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <input id="Hidden2x" name="Hidden2x" runat="Server" type="hidden" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender3" BackgroundCssClass="modalBackground"
                                    PopupControlID="pnlSearchShow" TargetControlID="Hidden2x" CancelControlID="ibtnSearchClose"
                                    runat="server">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlattachdoc" runat="server" Width="50%" CssClass="modalPopup">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    Accounting Entries done for following document
                                                </label>
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Account/images/closeicon.png" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    DocId :<asp:Label ID="lbldid" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    DocTitle :<asp:Label ID="lbldtitle" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:RadioButtonList ID="rdradio" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                    OnSelectedIndexChanged="rdradio_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Make new entry"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Add to Existing Entry" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Panel ID="pvlnewentry" runat="server" Visible="false">
                                                    <label>
                                                    </label>
                                                    <label>
                                                        <asp:DropDownList ID="ddloa" runat="server">
                                                        </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                        <asp:Button ID="ImageButton5" runat="server" Text=" Go " CssClass="btnSubmit" OnClick="ImageButton5_Click" />
                                                    </label>
                                                    <asp:HyperLink ID="hypost" Visible="false" runat="server" Target="_blank" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Panel ID="pnlexist" runat="server" Visible="true">
                                                    <label>
                                                    </label>
                                                    <label>
                                                        <asp:DropDownList ID="ddldo" runat="server" Width="200px">
                                                            <asp:ListItem Value="1" Text="Cash Register"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Journal Register"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Cr/Dr Note Register"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="Packing Slip Register"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="Purchase Register"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="Sales Register"></asp:ListItem>
                                                           <%-- <asp:ListItem Value="7" Text="Sales Order Register"></asp:ListItem>--%>
                                                            <asp:ListItem Value="8" Text="Expense Register"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                        <asp:Button ID="img2" runat="server" OnClick="Img2_Click" CssClass="btnSubmit" Text=" Go " />
                                                    </label>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <label>
                                                    List of accounting entries done based on this document.</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Panel ID="Panel5" runat="server" ScrollBars="Both" Height="230px" Width="100%">
                                                    <asp:GridView ID="gridpopup" runat="server" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" OnSelectedIndexChanged="gridpopup_SelectedIndexChanged"
                                                        Width="95%">
                                                        <Columns>
                                                            <asp:BoundField DataField="Datetime" HeaderStyle-HorizontalAlign="Left" HeaderText="Date" />
                                                            <asp:BoundField DataField="Entry_Type_Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Entry Type" />
                                                            <asp:BoundField DataField="EntryNumber" HeaderText="Entry Number" HeaderStyle-HorizontalAlign="Left" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <input id="Hidden1" name="Hidden1" runat="Server" type="hidden" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender4" BackgroundCssClass="modalBackground"
                                    PopupControlID="pnlattachdoc" TargetControlID="Hidden1" CancelControlID="ImageButton6"
                                    runat="server">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <asp:Panel ID="pnlconfirmmsg" runat="server" BorderStyle="Outset" Height="100px"
                    Width="300px" BackColor="#CCCCCC" BorderColor="#666666">
                    <table id="Table1">
                        <tr>
                            <td>
                                <table id="Table2" cellspacing="3" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlconfirmmsgub" runat="server" Width="300px" Height="75px">
                                                <table id="Table3" cellspacing="3" cellpadding="0">
                                                    <tr>
                                                        <td colspan="2" style="font-weight: bold; padding-left: 10px; font-size: 12px; font-family: Arial;
                                                            text-align: left; vertical-align: top;">
                                                            <br />
                                                            Are you sure, You want to Delete a Record ?
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <br />
                                                            <asp:Button ID="imgconfirmok" runat="server" Text=" Yes " CausesValidation="False"
                                                                CssClass="btnSubmit" OnClick="imgconfirmok_Click" />
                                                        </td>
                                                        <td>
                                                            <br />
                                                            <asp:Button ID="imgconfirmcalcel" CssClass="btnSubmit" runat="server" Text="No" CausesValidation="False" />
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
                <cc1:ModalPopupExtender ID="mdlpopupconfirm" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="pnlconfirmmsg" TargetControlID="hdnconfirm" CancelControlID="imgconfirmcalcel"
                    X="250" Y="-200" Drag="true">
                </cc1:ModalPopupExtender>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlExport" />
            <%--   <asp:PostBackTrigger ControlID="Gridreqinfo" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
