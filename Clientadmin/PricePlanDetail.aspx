<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="PricePlanDetail.aspx.cs" Inherits="PricePlanDetail" Title="Price Plan Master Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
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
        .style1
        {
            height: 42px;
        }
         .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
    
        TR.updated TD
        {
            background-color:yellow;
        }
        .modalBackground 
        {
            background-color:Gray;
            filter:alpha(opacity=70);
            opacity:0.7;
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

        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


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

    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>    <%-- <asp:UpdatePanel ID="pnlid" runat="server">
        <ContentTemplate>--%>
    <div style="float: left;">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Size="16px"></asp:Label>
    </div>
    <div style="clear: both;">
    </div>
    <div class="products_box">
        <fieldset>
            <legend>
                <asp:Label ID="lbladdlabel" runat="server" Text=""></asp:Label>
            </legend>
            <div style="float: right;">
                <asp:Button ID="addnewpanel" runat="server" Text="Add Price Plan" CssClass="btnSubmit"
                    OnClick="addnewpanel_Click" />
                <asp:Button ID="btndosyncro" runat="server" Visible="false" CssClass="btnSubmit"
                    OnClick="btndosyncro_Click" Text="Do Synchronise" />
            </div>
            <div style="clear: both;">
            </div>
            <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                <table width="100%">
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label1" runat="server" Text="Product Name/Version"></asp:Label>
                                <asp:Label ID="Label22" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValddlpv" runat="server" ControlToValidate="ddlProductname1"
                                    ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlProductname1" runat="server" AutoPostBack="True" Width="600px"
                                    OnSelectedIndexChanged="ddlProductname1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Portal Name<asp:RequiredFieldValidator ID="reqportal" runat="server" ErrorMessage="*"
                                    SetFocusOnError="true" InitialValue="0" ControlToValidate="ddlportal" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                    </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlportal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlportal_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label48" runat="server" Text="Priceplan Category"></asp:Label>
                                <asp:Label ID="Label49" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlpriceplancatagory"
                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlpriceplancatagory" runat="server" >
                                
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdmultip" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                OnSelectedIndexChanged="rdmultip_SelectedIndexChanged" RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Text="Add Single Price Plan" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Add Multiple Price Plan" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <label>
                                <asp:Label ID="lblmd" runat="server" Text=""></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <%-- <fieldset>
                                <legend>Common Price Plan Information </legend>--%>
                            <asp:Panel ID="pnlmult" runat="server" BorderWidth="1">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="pnlprname" runat="server">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="width: 430px;">
                                                            <label>
                                                                <asp:Label ID="Label4" runat="server" Text="Plan Name"></asp:Label>
                                                                <asp:Label ID="Label23" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPlanName"
                                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="Invalid Character"
                                                                    SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9-\s]*)" ControlToValidate="txtPlanName"
                                                                    ValidationGroup="1"> </asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtPlanName" MaxLength="100" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9-_ ]+$/,'div1',100)"
                                                                    runat="server" Width="300px" AutoPostBack="True" OnTextChanged="txtPlanName_TextChanged"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label runat="server" ID="adasdasd" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="div1" class="labelcount">100</span>
                                                                <asp:Label ID="lblinvstiename" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ -)"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label5" runat="server" Text="Price Plan Description"></asp:Label>
                                                        <asp:Label ID="Label25" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPlanDesc"
                                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                            SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_.\s]*)" ControlToValidate="txtPlanDesc"
                                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:TextBox ID="txtPlanDesc" onkeypress="return checktextboxmaxlength(this,300,event)"
                                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_. \s]+$/,'Span3',300)"
                                                            runat="server" Height="60px" Width="360px" TextMode="MultiLine"></asp:TextBox>
                                                    </label>
                                                    <label>
                                                        <asp:Label runat="server" ID="Label40" Text="Max" CssClass="labelcount"></asp:Label>
                                                        <span id="Span3" cssclass="labelcount">300</span>
                                                        <asp:Label ID="Label32" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="pnlperorder" runat="server" Visible="false">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label6" runat="server" Text="Is it a Pay per Order Plan?"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkporder" Visible="true" runat="server" OnCheckedChanged="chkporder_CheckedChanged"
                                                                AutoPostBack="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label7" runat="server" Text=" $ per Order"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtperorder" runat="server" Width="163px"></asp:TextBox>
                                                            </label>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtperorder"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label8" runat="server" Text="Free Initial Orders"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtfreeinitialorder" runat="server" Width="163px"></asp:TextBox></label>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtfreeinitialorder"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label9" runat="server" Text="Minimum Deposit required"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtmindepre" runat="server" Width="163px"></asp:TextBox></label>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtmindepre"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label10" runat="server" Text="Minimum Amount"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtminamount" runat="server" Width="163px"></asp:TextBox>
                                                            </label>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtmindepre"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label13" runat="server" Text="Period when the Price Plan available to subscribe"></asp:Label>
                                                <asp:Label ID="Label18" runat="server" Text="Start Date"></asp:Label>
                                                <asp:Label ID="Label26" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtStartdate"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtStartdate" runat="server" Width="100px"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgbtnEndDate"
                                                    TargetControlID="txtStartdate">
                                                </cc1:CalendarExtender>
                                            </label>
                                            <%-- </td>
                    </tr>
                    <tr>
                        <td>--%>
                                            <label>
                                                <asp:Label ID="Label14" runat="server" Text="End Date"></asp:Label>
                                                <asp:Label ID="Label27" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEndDate"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                            <%--  </td>
                        <td>--%>
                                            <label>
                                                <asp:TextBox ID="txtEndDate" runat="server" Width="100px"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:ImageButton ID="imgbtnCalEnddate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                            </label>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgbtnCalEnddate"
                                                TargetControlID="txtEndDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label16" runat="server" Text="License Duration(In Days)"></asp:Label>
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                                    Visible="false">
                                                    <asp:ListItem Value="True">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:Label ID="Label28" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtMonth"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtMonth" runat="server" MaxLength="5" onKeyup="return mak('Span2',5,this)"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                    FilterType="Custom, Numbers" TargetControlID="txtMonth" ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                            </label>
                                            <label>
                                                <asp:Label runat="server" ID="Label42" Text="Max" CssClass="labelcount"></asp:Label>
                                                <span id="Span2" class="labelcount">5</span>
                                                <asp:Label ID="Label34" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPlanAmount" runat="server" onKeyup="return mak('Span1',15,this)"
                                                MaxLength="15" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtGBUsage" MaxLength="5" runat="server" onKeyup="return mak('Span4',5,this)"
                                                Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtTrafficinGB" MaxLength="5" runat="server" onKeyup="return mak('Span5',5,this)"
                                                Visible="false"></asp:TextBox>
                                        </td>
                                   </tr>
                                    <tr>
                                        <td valign="top">
                                            <label>
                                                <asp:Label ID="lblinter" runat="server" Text="Automatic License check interval days from busicontroller"></asp:Label>
                                                <asp:Label ID="Label29" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtcheintvaldays"
                                                    ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtcheintvaldays"
                                                    ErrorMessage="Only digit Allowed" Display="Dynamic" ValidationExpression="[0-9]*"
                                                    ForeColor="Red"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td valign="top">
                                            <label>
                                                <asp:TextBox ID="txtcheintvaldays" MaxLength="5" onKeyup="return mak('Span7',5,this)"
                                                    runat="server"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                    FilterType="Custom, Numbers" TargetControlID="txtcheintvaldays" ValidChars="0.123456789">
                                                </cc1:FilteredTextBoxExtender>
                                            </label>
                                            <label>
                                                <asp:Label runat="server" ID="Label46" Text="Max" CssClass="labelcount"></asp:Label>
                                                <span id="Span7" class="labelcount">5</span>
                                                <asp:Label ID="Label38" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                                                <asp:TextBox ID="txtTotalMail" MaxLength="5" Visible="false" runat="server"></asp:TextBox>
                                            </label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 430px;" valign="top">
                                            <label>
                                                <asp:Label ID="lblgrace" runat="server" Text="Grace Days when a product would still run in the event when a license validity could not be confirmed.after this grace days the client product would be deleted after taking its data backup"></asp:Label>
                                                <asp:Label ID="Label30" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtgracedays"
                                                    ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtgracedays"
                                                    ErrorMessage="Only digit Allowed" ValidationExpression="[0-9]*" ValidationGroup="1"
                                                    Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td valign="top">
                                            <label>
                                                <asp:TextBox ID="txtgracedays" runat="server" MaxLength="5" onKeyup="return mak('Span8',5,this)"
                                                    Width="125px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                                    FilterType="Custom, Numbers" TargetControlID="txtgracedays" ValidChars="0.123456789">
                                                </cc1:FilteredTextBoxExtender>
                                            </label>
                                            <label>
                                                <asp:Label runat="server" ID="Label47" Text="Max" CssClass="labelcount"></asp:Label>
                                                <span id="Span8" class="labelcount">5</span>
                                                <asp:Label ID="Label39" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label12" runat="server" Text="Active"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkboxActiveDeactive" Checked="true" runat="server" Text="Active" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                           <label> <asp:Label ID="Label50" runat="server" 
                                                Text="Is this the default price plan in the selected priceplan category ?" 
                                                Visible="False"></asp:Label></label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkdeafult" runat="server" Checked="True" Text="Yes" 
                                                Visible="False" />
                                        </td>
                                    </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td colspan="2">
                                                                <label>
                                                                <asp:Label Visible="false"  ID="Label11" runat="server"  Text="Whether the client would be required to host the application on his own server ? OR Would it be hosted on busiwiz server ?"></asp:Label>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList Visible="false"  ID="Rdownclient" runat="server" AutoPostBack="True"  Checked="true" OnSelectedIndexChanged="Rdownclient_SelectedIndexChanged"  RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes, It would be hosted on client's server" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>                                                                    
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                             <asp:Panel ID="pnlserver" runat="server" Visible="false">
                                                                              <label>
                                                                                <asp:DropDownList ID="ddlserverMas" runat="server">
                                                                                </asp:DropDownList>
                                                                                </label>
                                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                      
                                                       
                                                    </table>
                                                </td>
                                            </tr>
                                           
                                           
                                            
                                           
                                           
                                            
                                               
                                </table>
                            </asp:Panel>
                            <%--</fieldset>--%>
                            </td>
                            </tr>
                        <tr>
                            <td colspan="3">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblmultipres" runat="server" Text="Set Price Plan Restrictions"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Button ID="btncalsugprice" runat="server" CssClass="btnSubmit" Text="Calculate Price"
                                                    OnClick="btncalsugprice_Click" ValidationGroup="1" />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:Panel ID="pnlnoofmulti" runat="server" Visible="false">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                I want to add</label>
                                                            <label>
                                                                <asp:TextBox ID="txtnoofalloed" runat="server" MaxLength="5" Width="50px" AutoPostBack="True"
                                                                    Text="2" OnTextChanged="txtnoofalloed_TextChanged"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextr1" runat="server" Enabled="True" FilterType="Custom, Numbers"
                                                                    TargetControlID="txtnoofalloed" ValidChars="1234567890">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </label>
                                                            <label>
                                                                Price Plans
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
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="pnlmultoplan" runat="server" ScrollBars="None">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdmulti" runat="server" DataKeyNames="Id" ShowFooter="false" AutoGenerateColumns="False"
                                                EmptyDataText="No Record Found." CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnRowCreated="grdmulti_RowCreated">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkAll_chackedChanged" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkchield" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="chkchield_chackedChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="240px"
                                                        ItemStyle-Width="240px">
                                                        <HeaderTemplate>
                                                            <label>
                                                                <asp:Label ID="lblhcead1" runat="server" Text="Plan Name Restrictions" ForeColor="White"></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="imgadddept" runat="server" Height="20px" ImageUrl="~/images/AddNewRecord.jpg"
                                                                    OnClick="LinkButton97666667_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="imgdeptrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                                    ImageUrl="~/images/DataRefresh.jpg" OnClick="LinkButton13_Click" ToolTip="Refresh"
                                                                    Width="20px" ImageAlign="Bottom" />
                                                            </label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcb" runat="server" Text='<%# Bind("NameofRest") %>' Width="150px"></asp:Label>
                                                            <asp:Label ID="lblrestgroup" runat="server" Text='<%# Bind("Restingroup") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblpriceaddgroup" runat="server" Text='<%# Bind("Priceaddingroup") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:Label ID="lblupId" runat="server" Text='<%# Bind("DTI") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblfo1vc" runat="server" Text="" ForeColor="Black"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl1" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp1" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp1_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc1" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc1" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice1" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice1" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender1" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice1" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice1" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo1" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender1" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo1" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan1" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan1" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest1" runat="server" Width="40px" Text='<%# Bind("RecordsAllowed") %>'></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender1" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest1" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF1" runat="server" ControlToValidate="lblprprest1"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl2" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp2" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp2_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc2" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc2" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice2" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice2" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender2" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice2" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice2" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo2" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender2" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo2" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan2" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan2" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest2" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender2" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest2" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF2" runat="server" ControlToValidate="lblprprest2"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl3" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp3" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp3_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc3" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc3" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice3" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice3" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender3" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice3" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice3" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo3" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender3" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo3" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan3" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan3" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest3" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender3" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest3" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF3" runat="server" ControlToValidate="lblprprest3"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl4" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp4" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp4_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc4" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc4" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice4" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice4" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender4" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice4" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice4" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo4" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender4" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo4" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan4" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan4" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest4" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender4" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest4" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF4" runat="server" ControlToValidate="lblprprest4"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl5" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp5" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp5_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc5" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc5" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice5" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice5" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender5" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice5" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice5" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo5" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender5" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo5" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan5" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan5" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest5" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender5" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest5" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF5" runat="server" ControlToValidate="lblprprest5"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl6" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp6" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp6_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc6" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc6" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice6" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice6" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender6" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice6" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice6" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo6" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender6" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo6" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan6" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan6" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest6" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender6" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest6" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF6" runat="server" ControlToValidate="lblprprest6"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl7" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp7" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp7_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc7" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc7" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice7" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice7" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender7" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice7" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice7" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo7" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender7" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo7" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan7" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan7" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest7" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender7" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest7" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF7" runat="server" ControlToValidate="lblprprest7"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl8" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp8" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp8_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc8" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc8" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice8" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice8" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender8" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice8" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice8" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo8" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender8" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo8" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan8" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan8" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest8" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender8" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest8" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF8" runat="server" ControlToValidate="lblprprest8"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl9" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp9" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp9_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc9" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc9" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice9" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice9" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender9" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice9" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice9" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo9" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender9" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo9" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan9" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan9" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest9" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender9" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest9" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF9" runat="server" ControlToValidate="lblprprest9"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl10" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp10" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp10_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc10" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc10" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice10" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice10" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender10" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice10" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice10" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo10" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender10" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo10" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan10" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan10" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest10" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender10" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest10" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF10" runat="server" ControlToValidate="lblprprest10"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl11" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp11" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp11_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc11" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc11" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice11" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice11" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender11" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice11" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice11" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo11" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender11" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo11" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan11" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan11" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest11" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender11" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest11" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF11" runat="server" ControlToValidate="lblprprest11"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl12" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp12" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp12_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc12" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc12" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice12" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice12" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender12" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice12" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice12" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo12" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender12" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo12" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan12" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan12" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest12" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender12" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest12" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF12" runat="server" ControlToValidate="lblprprest12"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl13" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp13" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp13_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc13" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc13" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice13" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice13" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender13" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice13" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice13" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo13" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender13" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo13" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan13" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan13" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest13" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender13" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest13" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF13" runat="server" ControlToValidate="lblprprest13"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl14" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp14" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp14_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc14" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc14" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice14" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice14" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender14" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice14" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice14" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo14" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender14" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo14" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan14" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan14" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest14" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender14" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest14" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF14" runat="server" ControlToValidate="lblprprest14"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl15" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp15" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp15_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc15" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc15" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice15" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice15" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender15" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice15" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice15" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo15" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender15" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo15" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan15" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan15" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest15" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender15" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest15" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF15" runat="server" ControlToValidate="lblprprest15"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl16" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp16" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp16_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc16" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc16" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice16" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice16" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender16" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice16" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice16" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo16" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender16" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo16" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan16" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan16" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest16" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender16" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest16" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF16" runat="server" ControlToValidate="lblprprest16"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl17" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp17" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp17_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc17" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc17" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice17" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice17" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender17" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice17" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice17" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo17" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender17" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo17" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan17" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan17" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest17" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender17" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest17" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF17" runat="server" ControlToValidate="lblprprest17"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl18" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp18" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp18_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc18" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc18" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice18" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice18" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender18" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice18" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice18" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo18" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender18" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo18" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan18" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan18" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest18" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender18" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest18" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF18" runat="server" ControlToValidate="lblprprest18"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl19" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp19" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp19_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc19" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc19" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice19" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice19" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender19" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice19" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice19" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo19" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender19" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo19" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan19" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan19" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest19" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender19" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest19" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF19" runat="server" ControlToValidate="lblprprest19"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblpl20" runat="server" Text="Plan Name" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblprp20" runat="server" Text="" Width="100px" AutoPostBack="True"
                                                                            OnTextChanged="lblprp20_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblplc20" runat="server" Text="Default Plan For Catg" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkplc20" runat="server" AutoPostBack="true" OnCheckedChanged="chkplc1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblbaseprice20" runat="server" Text="Base Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtbaseprice20" runat="server" Text="" Width="100px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextbaseBoxExtender20" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="txtbaseprice20" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblsuggprice20" runat="server" Text="Sugg Price" Width="71px" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblfo20" runat="server" ForeColor="Black" Width="71px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredssTexender20" runat="server" Enabled="True"
                                                                            FilterType="Custom, Numbers" TargetControlID="lblfo20" ValidChars=".0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblfreetryplan20" runat="server" Text="Default Free Tryout Plan" ForeColor="White"></asp:Label>
                                                                        <asp:CheckBox ID="chkfreetryplan20" runat="server" AutoPostBack="true" OnCheckedChanged="chkfreetryplan1_chackedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblprprest20" runat="server" Text="" Width="40px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTexender20" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="lblprprest20" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="ReqF20" runat="server" ControlToValidate="lblprprest20"
                                                                Visible="false" ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <%--  <asp:Panel ID="pnlsingleplan" runat="server" Width="100%">--%>
                            <asp:Panel ID="pnlfreetryout" Visible="false" runat="server" Width="100%">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            Free Tryout Plan Details :
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Price Plan
                                                <asp:Label ID="lbldisplanname" runat="server"></asp:Label>
                                                is selected as free plan to be offered to the new clients of other portals for try
                                                out.
                                            </label>
                                            <label>
                                                <asp:LinkButton ID="btnlinkpop" Visible="false" runat="server" Text="More Info" ForeColor="Black"
                                                    OnClick="btnlinkpop_Click"></asp:LinkButton>
                                            </label>
                                        </td>
                                        <td valign="top">
                                            <asp:CheckBox ID="CheckBox1" Visible="false" Text="Yes" AutoPostBack="true" runat="server"
                                                OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td width="700px" colspan="2">
                                            <label>
                                                Prefix to be added to the company ID created for any new client of any other portal
                                                <br />
                                                (For Eg. If xyz is mentioned here; and when new company for any other portal with
                                                <br>
                                                company ID 'capman' is created; the second company ID 'xyzcapman' would be created<br>
                                                for being used for this free try out plan)";
                                            </label>
                                        </td>
                                        <td valign="top">
                                            <label>
                                                <asp:TextBox ID="txtprefix" runat="server"></asp:TextBox>
                                                <asp:LinkButton ID="lnkpop" runat="server" Text="More Info" Visible="false" ForeColor="Black"
                                                    OnClick="lnkpop_Click"></asp:LinkButton>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="700px" colspan="2" valign="top">
                                            <label>
                                                Please mention the text to be displayed to the new client of any other portal while
                                                he is running<br />
                                                "New company configuration wizard" offering him to subscibe to this free try out
                                                plan.<br />
                                                For Eg. You can mention following text would you like to subscribe to 90 days free<br>
                                                   plan of BusiDirectory.com - an online business directory ?
                                            </label>
                                        </td>
                                        <td valign="top">
                                            <label>
                                                <asp:TextBox ID="TextBox2" TextMode="MultiLine" Width="400px" Height="80px" runat="server"></asp:TextBox>
                                                <asp:LinkButton ID="lnkpopmm" runat="server" Text="More Info" Visible="false" ForeColor="Black"
                                                    OnClick="lnkpopmm_Click"></asp:LinkButton>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <label>
                                            </label>
                                        </td>
                                        <td valign="top">
                                            <label>
                                                <asp:TextBox ID="TextBox1" Visible="false" runat="server"></asp:TextBox>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="1" />
                            <asp:Button ID="btncancel" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="btncancel_Click" />
                        </td>                       
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
        <fieldset>
            <legend>
                <asp:Label ID="lbllegendd" runat="server" Text="List of Price Plan for My Product"></asp:Label>
            </legend>
            <div>
                <label style="width:700px">
                    Filter by</label>
                    <label style="width:400px">
                           <asp:CheckBox ID="chkupload" runat="server" AutoPostBack="True" Text="Show only active records in Above Dropdown" OnCheckedChanged="chkupload_CheckedChanged" Checked="true">
                                </asp:CheckBox>
                        </label> 
                <div style="float: right;">
                    <asp:Button ID="btnprint" runat="server" CssClass="btnSubmit" Text="Printable Version"
                        CausesValidation="False" OnClick="btnprint_Click" />
                    <input id="btnin" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                        type="button" value="Print" visible="false" />
                </div>
            </div>
            <table width="100%">
                <tr>
                    <td>
                        <label>
                            <asp:Label ID="Label31" runat="server" Text="Product"></asp:Label>
                        </label>
                        <label>
                            <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="ProductName" DataValueField="ProductId"
                                AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            Portal Name
                        </label>
                        <label>
                            <asp:DropDownList ID="ddlfilterportal" runat="server" AutoPostBack="True" Width="200px"
                                OnSelectedIndexChanged="ddlfilterportal_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Priceplan Category"></asp:Label>
                        </label>
                        <label>
                            <asp:DropDownList ID="ddlfilproductcategory" runat="server" AutoPostBack="True" Width="200px"
                                OnSelectedIndexChanged="ddlfilproductcategory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label15" runat="server" Text="Status"></asp:Label>
                        </label>
                        <label>
                            <asp:DropDownList ID="ddlstfilter" runat="server" AutoPostBack="True" Width="100px"
                                OnSelectedIndexChanged="ddlstfilter_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="1" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                            </asp:DropDownList>
                        </label>
                     
                    </td>
                    
                </tr>
                <tr>
                    <td>
                           

                    </td>
                </tr>
            </table>
            
            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                
                <table width="100%">
                <div id="mydiv" >
                    <tr align="center">
                        <td>
                                <table width="100%">
                                    <tr align="center">
                                        <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                            <asp:Label ID="Labeczcxl19" runat="server" Text="List of Price Plan for My Product"
                                                Font-Italic="True" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                         
                        </td>
                  </tr>
                  
                <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" DataKeyNames="PricePlanId" OnRowCommand="GridView1_RowCommand"
                                AutoGenerateColumns="False" EmptyDataText="No Record Found." CssClass="mGrid"
                                GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                Width="100%" AllowSorting="True" OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="productName" SortExpression="productName" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="Product Name">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PortalName" SortExpression="PortalName" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="Portal Name">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PricePlanName" SortExpression="PricePlanName" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="Price Plan Name" ItemStyle-Width="300px">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PricePlanAmount" SortExpression="PricePlanAmount" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="Price Plan Amount(in $)">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PayperOrderPlan" Visible="false" SortExpression="PayperOrderPlan"
                                        HeaderStyle-HorizontalAlign="Left" HeaderText="PayperOrderPlan">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <%-- <asp:BoundField DataField="amountperOrder" SortExpression="amountperOrder" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="$ perOrder">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="DurationMonth" SortExpression="DurationMonth" HeaderStyle-Width="130px"
                                        HeaderStyle-HorizontalAlign="Left" HeaderText="License Duration (In Days)">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Active1" SortExpression="Active1" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="Status">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" ImageUrl="~/Account/images/edit.gif"
                                        Text="Button">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:ButtonField>
                                    <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="llinkbb" ToolTip="Delete" runat="server" CommandName="delete"
                                                ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                CommandArgument='<%# Eval("PricePlanId") %>'></asp:ImageButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Image" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                        HeaderImageUrl="~/images/edit.gif" CommandName="view" HeaderText="View" ImageUrl="~/images/edit.gif"
                                        Text="Button">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:ButtonField>
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
                        </td>
                   </tr>
                  </div>
                </table>
                  
            </asp:Panel>
          
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnlmd" runat="server" Width="50%" CssClass="modalPopup">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lbldoclab" runat="server" Text=""></asp:Label>
                                </legend>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%;">
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="ImageButton110" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <label>
                                                <asp:Label ID="lbllllv" runat="server" Text=""></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>
                        <asp:Button ID="btnvb" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="pnlmd" TargetControlID="btnvb" CancelControlID="ImageButton110">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Paneldoc" runat="server" Width="55%" CssClass="modalPopup">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
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
                                                <asp:Label ID="Label20" runat="server" Text="Was this the last record you are going to add right now to this table?"></asp:Label>
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
                            </fieldset>
                        </asp:Panel>
                        <asp:Button ID="btnreh" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Paneldoc" TargetControlID="btnreh" CancelControlID="ImageButton10">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
            </table>
            <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
            <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
            <input id="hdnPricePlanId" name="hdnPricePlanId" runat="server" type="hidden" style="width: 1px" />
        </fieldset>
    </div>
     
    <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" ForeColor="#416271" Font-Size="10px"></asp:Label>
              </div>
</asp:Content>
