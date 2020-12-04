<%@ Page Language="C#" MasterPageFile="CandidateMain.master" AutoEventWireup="true"
    CodeFile="Vacancy.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
        <title>Vacancy</title>
    </head>
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

        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

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
    <table width="100%">
        <tr>
            <td valign="top">
                <asp:Image ID="Img1" runat="server" Width="245px" Height="106px" />
            </td>
            <td align="right">
                <div id="headerInfo">
                    <asp:Label runat="server" ID="lblcompanyname" Font-Size="18px"></asp:Label>
                    <div style="clear: both; height: 0px">
                    </div>
                    <asp:Label runat="server" ID="lbladdress1" Font-Size="14px" Font-Bold="false"></asp:Label>
                    <div style="clear: both; height: 0px">
                    </div>
                    <asp:Label runat="server" ID="lblphoneno" Font-Size="14px" Font-Bold="false"></asp:Label>
                    <div style="clear: both; height: 0px">
                    </div>
                    <asp:Label runat="server" ID="lblwebsite" Font-Size="12px" Font-Bold="false" Visible="false"></asp:Label>
                    <div style="clear: both;">
                    </div>
                    <asp:Label runat="server" ID="lblcs" Font-Size="12px" Font-Bold="false"></asp:Label>
                    <div style="clear: both; height: 0px">
                    </div>
                    <asp:Label runat="server" ID="lbltollfreeno" Font-Size="12px" Font-Bold="false"></asp:Label>
                    <div style="clear: both; height: 0px">
                    </div>
                    <asp:Label runat="server" ID="lblemail" Font-Size="12px" Font-Bold="false"></asp:Label>
                    <div style="clear: both; height: 0px">
                    </div>
                </div>
            </td>
            <%-- <td>
                <div id="headerInfo">
                  
                    <div style="clear: both;">
                    </div>
                </div>
                <div id="site_title" style="vertical-align: top">
                    <h1>
                        
                    </h1>
                </div>
            </td>--%>
        </tr>
    </table>
    <div class="products_box">
        <%--    <div style="padding-left: 2%">
            <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div style="clear: both;">
        </div>--%>
        <div style="float: right;">
            <asp:Button ID="btnadd" runat="server" CssClass="btnSubmit" Text="Add Vacancy" OnClick="btnadd_Click"
                Visible="False" />
        </div>
        <div style="clear: both;">
        </div>
        <div>
            <asp:Panel ID="Pnladdnew" runat="server" Visible="false" Width="100%">
                <table width="100%">
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="lblwname" runat="server" Text="Business Name">
                                </asp:Label>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:DropDownList ID="ddlStore" runat="server" Width="250px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlStore_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label12" runat="server" Text="Department - Designation"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:DropDownList ID="ddldeptdesg" runat="server" Width="250px">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label39" runat="server" Text="Vacancy Type"></asp:Label>
                                <%--<asp:Label ID="Label42" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlvacancytype"
                                    ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:DropDownList ID="ddlvacancytype" runat="server" Width="250px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlvacancytype_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label40" runat="server" Text="Vacancy Position Title"></asp:Label>
                                <%--<asp:Label ID="Label41" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:DropDownList ID="ddlvacancyposition" runat="server" Width="250px">
                                </asp:DropDownList>
                            </label>
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="Other" TextAlign="Left" AutoPostBack="True"
                                OnCheckedChanged="CheckBox2_CheckedChanged" />
                            <asp:Panel ID="panel11" runat="server" Visible="false">
                                <label>
                                    <asp:TextBox ID="txtnewvacancy" runat="server" MaxLength="60"></asp:TextBox>
                                </label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label1" runat="server" Text="Vacancy Location"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:Label ID="lblCountrylbl" runat="server" Text="Country"></asp:Label>
                                <%--<asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="rdg3" runat="server" ControlToValidate="ddlCountry"
                                    ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                    ValidationGroup="1" Width="151px">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="lblStatelbl" runat="server" Text="State"></asp:Label>
                                <%--<asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="rd1n1" runat="server" ControlToValidate="ddlState"
                                    ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                    ValidationGroup="1" Width="151px">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="lblCitylbl" runat="server" Text="City"></asp:Label>
                                <%--<asp:Label ID="Label43" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="RequiredFielgdValidator12" runat="server" ControlToValidate="ddlCity"
                                    ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlCity" runat="server" ValidationGroup="1" Width="151px">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label13" runat="server" Text="Number of Vacancies"></asp:Label>
                                <%--<asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldVadaslidator2" runat="server" ControlToValidate="txtvacancy"
                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                    TargetControlID="txtvacancy" ValidChars="0123456789">
                                </cc1:FilteredTextBoxExtender>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:TextBox ID="txtvacancy" runat="server" MaxLength="3" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z.A-Z0-9]+$/,'Span2',3)"></asp:TextBox>
                            </label>
                            <label>
                                <asp:Label runat="server" ID="Label26" Text="Max " CssClass="labelcount" Visible="false"></asp:Label>
                                <span id="Span2"></span>
                                <asp:Label ID="Label23" runat="server" CssClass="labelcount" Text="(0-9)" Visible="false"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label7" runat="server" Text="Vacancy Advertisement Start Date"></asp:Label>
                                <%--<asp:Label ID="Label18" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtestartdate"
                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:TextBox ID="txtestartdate" runat="server" Width="70px"></asp:TextBox>
                            </label>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton2"
                                TargetControlID="txtestartdate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtestartdate">
                            </cc1:MaskedEditExtender>
                            <label>
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label8" runat="server" Text="Vacancy Advertisement End Date"></asp:Label>
                                <%--<asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txteenddate"
                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:TextBox ID="txteenddate" runat="server" Width="70px"></asp:TextBox>
                            </label>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                                TargetControlID="txteenddate">
                            </cc1:CalendarExtender>
                            <label>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                            </label>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txteenddate">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <%--<tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label17" runat="server" Text="salary"></asp:Label>
                                    <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFielddsdValidator2" runat="server" ControlToValidate="txtsalary"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBdsoxExtender2" runat="server" Enabled="True"
                                        TargetControlID="txtsalary" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </label>
                            </td>
                            <td style="width: 70%">
                                <label>
                                    <asp:TextBox ID="txtsalary" runat="server" MaxLength="10" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z.A-Z0-9]+$/,'Span1',10)"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label runat="server" ID="Label1" Text="Max " CssClass="labelcount"></asp:Label>
                                    <span id="Span1" cssclass="labelcount">10</span>
                                    <asp:Label ID="Label2" runat="server" CssClass="labelcount" Text="(0-9)"></asp:Label>
                                </label>
                            </td>
                        </tr>--%>
                    <%--<tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="lbldsad" runat="server" Text="Hours"></asp:Label>
                                    <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txthours"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txthours"
                                        ErrorMessage="*" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td style="width: 70%">
                                <label>
                                    <asp:TextBox ID="txthours" runat="server" Width="80px" MaxLength="5"></asp:TextBox>
                                </label>
                                <label>
                                    <span class="lblInfoMsg">(24Hour(HH:MM))</span>
                                </label>
                            </td>
                        </tr>--%>
                    <%--<tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Position"></asp:Label>
                                    <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtposition"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                        ValidationExpression="^([a-z?.,A-Z0-9_\s]*)" ControlToValidate="txtposition"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td style="width: 70%">
                                <label>
                                    <asp:TextBox ID="txtposition" runat="server" MaxLength="60" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z.A-Z0-9]+$/,'Span1',60)"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label runat="server" ID="Label5" Text="Max " CssClass="labelcount"></asp:Label>
                                    <span id="Span1" cssclass="labelcount">60</span>
                                    <asp:Label ID="Label6" runat="server" CssClass="labelcount" Text="(A-Z 0-9 . , ? _)"></asp:Label>
                                </label>
                            </td>
                        </tr>--%>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label9" runat="server" Text="Job Function"></asp:Label>
                                <%--<asp:Label ID="Label10" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtposition"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                    ValidationExpression="^([a-z?.,A-Z0-9_\s]*)" ControlToValidate="txtjobfunction"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Please enter maximum 1500 chars"
                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1500})$"
                                    ControlToValidate="txtjobfunction" ValidationGroup="1"></asp:RegularExpressionValidator>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:TextBox ID="txtjobfunction" Width="400px" Height="60px" runat="server" TextMode="MultiLine"
                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div2',1500)"
                                    MaxLength="1500" onKeydown="return mask(event)" onkeypress="return checktextboxmaxlength(this,1500,event)"></asp:TextBox>
                                <asp:Label runat="server" ID="Label25" Text="Max " CssClass="labelcount" Visible="false"></asp:Label>
                                <span id="div2"></span>
                                <asp:Label ID="Label22" runat="server" CssClass="labelcount" Text="(A-Z 0-9 . , ? _)"
                                    Visible="false"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label10" runat="server" Text="Qualification Requirements"></asp:Label>
                                <%--<asp:Label ID="Label10" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtposition"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Character"
                                    ValidationExpression="^([a-z?.,A-Z0-9_\s]*)" ControlToValidate="txtqualireq"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Please enter maximum 1500 chars"
                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1500})$"
                                    ControlToValidate="txtqualireq" ValidationGroup="1"></asp:RegularExpressionValidator>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:TextBox ID="txtqualireq" Width="400px" Height="60px" runat="server" TextMode="MultiLine"
                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'Span3',1500)"
                                    MaxLength="1500" onKeydown="return mask(event)" onkeypress="return checktextboxmaxlength(this,1500,event)"></asp:TextBox>
                                <asp:Label runat="server" ID="Label11" Text="Max " CssClass="labelcount" Visible="false"></asp:Label>
                                <span id="Span3"></span>
                                <asp:Label ID="Label14" runat="server" CssClass="labelcount" Text="(A-Z 0-9 . , ? _)"
                                    Visible="false"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label15" runat="server" Text="Currency"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:DropDownList ID="ddlcurrency" runat="server" Width="70px">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="Label16" runat="server" Text="Salary Amount"></asp:Label>
                            </label>
                            <label>
                                <asp:TextBox runat="server" ID="txtmysalary" MaxLength="10"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                    TargetControlID="txtmysalary" ValidChars="0123456789">
                                </cc1:FilteredTextBoxExtender>
                            </label>
                            <label>
                                <asp:Label ID="Label38" runat="server" Text="per"></asp:Label>
                            </label>
                            <label>
                                <asp:DropDownList ID="ddlsalperiod" runat="server" Width="80px">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Labedsfl14" runat="server" Text="Other Terms and Conditions"></asp:Label>
                                <asp:RegularExpressionValidator ID="RegularExpressionVdfdsfalidator1" runat="server"
                                    ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                    ControlToValidate="txttermcond" ValidationGroup="1"></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionVasdfdsflidator2" runat="server"
                                    ErrorMessage="Please enter maximum 1500 chars" Display="Dynamic" SetFocusOnError="True"
                                    ValidationExpression="^([\S\s]{0,1500})$" ControlToValidate="txttermcond" ValidationGroup="1"></asp:RegularExpressionValidator>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:TextBox ID="txttermcond" Width="400px" Height="60px" runat="server" TextMode="MultiLine"
                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div22',1500)"
                                    MaxLength="1500" onKeydown="return mask(event)" onkeypress="return checktextboxmaxlength(this,1500,event)"></asp:TextBox>
                                <asp:Label runat="server" ID="Labefdsdl25" Text="Max " CssClass="labelcount" Visible="false"></asp:Label>
                                <span id="div22"></span>
                                <asp:Label ID="Labdfsel22" runat="server" CssClass="labelcount" Text="(A-Z 0-9 . , ? _)"
                                    Visible="false"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label17" runat="server" Text="Scheduled Hours"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:TextBox ID="txtworktimings" runat="server"></asp:TextBox>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label20" runat="server" Text="Number of Hours"></asp:Label>
                                <%--<asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="RequiredFielasdasdValidator3" runat="server" ControlToValidate="txtnoofhours"
                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtnoofhours"
                                    ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid Digits"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                <%--<asp:RegularExpressionValidator ID="RegularExpressisadsadonValidator3" runat="server"
                                        ControlToValidate="txtnoofhours" ErrorMessage="*" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:TextBox ID="txtnoofhours" runat="server" MaxLength="5" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'Span100',5)"></asp:TextBox>
                            </label>
                            <label>
                                <asp:Label runat="server" ID="Label48" Text="Max " CssClass="labelcount" Visible="false"></asp:Label>
                                <span id="Span100"></span>
                                <asp:Label ID="Label49" runat="server" Text="(0-9 .)" CssClass="labelcount" Visible="false"></asp:Label>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                    TargetControlID="txtnoofhours" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                            </label>
                            <label>
                                <asp:Label ID="Label21" runat="server" Text="per"></asp:Label>
                            </label>
                            <label>
                                <asp:DropDownList ID="ddlhourperiod" runat="server">
                                    <asp:ListItem Selected="True" Text="Day" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Week" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label24" runat="server" Text="Terms of Employment"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:DropDownList ID="ddlduration" runat="server">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label28" runat="server" Text="Contact Email ID"></asp:Label>
                                <%--<asp:Label ID="Label33" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlemail"
                                    runat="server" ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                <%--    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Email ID"
                                        ControlToValidate="txtemailid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:DropDownList ID="ddlemail" runat="server" Width="300px">
                                </asp:DropDownList>
                                <%-- <asp:TextBox ID="txtemailid" runat="server" MaxLength="100" AutoPostBack="true" OnTextChanged="txtemailid_TextChanged"></asp:TextBox>
                                    <asp:Label ID="duplicatetbemail" runat="server" ForeColor="Red"></asp:Label>--%>
                            </label>
                            <label>
                                <asp:ImageButton ID="imgempadd" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                    ToolTip="AddNew" Width="20px" ImageAlign="Bottom" OnClick="imgempadd_Click" />
                            </label>
                            <label>
                                <asp:ImageButton ID="imgemprefresh" runat="server" AlternateText="Refresh" ImageAlign="Bottom"
                                    Height="20px" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh"
                                    Width="20px" OnClick="imgemprefresh_Click" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label27" runat="server" Text="Contact Person Name"></asp:Label>
                                <%--<asp:Label ID="Label32" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtpername"
                                    SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                    ErrorMessage="Invalid Character" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                    ControlToValidate="txtpername" ValidationGroup="1"></asp:RegularExpressionValidator>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:TextBox ID="txtpername" runat="server" MaxLength="60" onKeydown="return mask(event)"
                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div12',60)"></asp:TextBox>
                            </label>
                            <label>
                                <asp:Label runat="server" ID="sadasd" Text="Max " CssClass="labelcount" Visible="false"></asp:Label>
                                <span id="div12"></span>
                                <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 . , ? _)" CssClass="labelcount"
                                    Visible="false"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label29" runat="server" Text="Contact Phone Number"></asp:Label>
                                <%--<asp:Label ID="Label34" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtphoneno"
                                    SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                    TargetControlID="txtphoneno" ValidChars="0123456789">
                                </cc1:FilteredTextBoxExtender>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:TextBox ID="txtphoneno" runat="server" MaxLength="20"></asp:TextBox>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label30" runat="server" Text="Contact Address"></asp:Label>
                                <%--<asp:Label ID="Label35" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtconaddress"
                                    SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Invalid Character"
                                    SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)" ControlToValidate="txtconaddress"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Please enter maximum 1500 chars"
                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1500})$"
                                    ControlToValidate="txtconaddress" ValidationGroup="1"></asp:RegularExpressionValidator>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:TextBox ID="txtconaddress" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'Span4',1500)"
                                    runat="server" Height="60px" onkeypress="return checktextboxmaxlength(this,1500,event)"
                                    Width="400px" TextMode="MultiLine"></asp:TextBox>
                                <asp:Label runat="server" ID="Label36" Text="Max " CssClass="labelcount" Visible="false"></asp:Label>
                                <span id="Span4"></span>
                                <asp:Label ID="Label37" CssClass="labelcount" runat="server" Text="(A-Z 0-9 . , ? _)"
                                    Visible="false"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label31" runat="server" Text="Candidate should apply by"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:CheckBox ID="chkemail" runat="server" Text="Email" />
                                <asp:CheckBox ID="chkphone" runat="server" Text="Phone" />
                                <asp:CheckBox ID="chkvisit" runat="server" Text="Personal Visit" />
                                <asp:CheckBox ID="chkonline" runat="server" Text="Online Form" />
                                <%--<asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Email"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Phone"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Personal Visit"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Online Form Fillup"></asp:ListItem>
                                    </asp:CheckBoxList>--%>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <label>
                                <asp:Label ID="Label44" runat="server" Text="Status"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 70%">
                            <label>
                                <asp:DropDownList ID="ddlstatus" runat="server">
                                    <asp:ListItem Selected="True" Text="Active" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:CheckBox ID="CheckBox1" runat="server" />--%>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                        </td>
                        <td style="width: 70%">
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btnsubmit_Click"
                                ValidationGroup="1" />
                            <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btnSubmit" Visible="false"
                                OnClick="btnupdate_Click" ValidationGroup="1" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="btncancel_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%--</fieldset>--%>
        </div>
        <div style="clear: both;">
        </div>
        <div style="padding-left: 2%">
            <asp:Label ID="Label18" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div style="clear: both;">
        </div>
        <asp:Panel ID="paneltop" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="Panel3" runat="server" Visible="false">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label5" runat="server" Text="Business Name"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Button ID="Button2" runat="server" Text="Go" OnClick="Button2_Click" CssClass="btnSubmit" />
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlbus" runat="server" Visible="true">
                            <fieldset>
                                <legend>Search Vacancies </legend>
                                <table>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label6" runat="server" Text="Business"></asp:Label>
                                                <asp:DropDownList ID="ddlstore1" runat="server">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                Country
                                                <asp:DropDownList ID="ddlcountry1" runat="server" Width="130px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlcountry1_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                State
                                                <asp:DropDownList ID="ddlstate1" runat="server" Width="130px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlstate1_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                City
                                                <asp:DropDownList ID="ddlcity1" runat="server" Width="130px">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                Type
                                                <asp:DropDownList ID="DropDownList1" runat="server" Width="130px" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                Term
                                                <asp:DropDownList ID="DropDownList2" runat="server" Width="130px" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td valign="bottom">
                                            <asp:Button ID="Button3" runat="server" Text="Go" OnClick="Button3_Click" CssClass="btnSubmit"
                                                ValidationGroup="13" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="panelreg" runat="server" Visible="false">
            <fieldset>
                <legend>
                    <asp:Label ID="Label45" runat="server" Text="List of Vacancies"></asp:Label>
                </legend>
                <div style="clear: both;">
                </div>
                <div style="padding-left: 2%">
                    <asp:Label ID="Label4" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <table width="100%">
                    <%--<tr>
                        <td>
                            <div style="float: right;">
                                <asp:Button ID="btncancel0" runat="server" CausesValidation="false" OnClick="btncancel0_Click"
                                    Text="Printable Version" CssClass="btnSubmit" />
                                <input id="Button7" runat="server" visible="false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    type="button" value="Print" class="btnSubmit" />
                            </div>
                        </td>
                    </tr>--%>
                    <tr>
                        <td align="right">
                            <asp:Button ID="Button5" runat="server" Text="Login" CssClass="btnSubmit" OnClick="Button5_Click" />
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
                                                    <tr align="center">
                                                        <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                            <asp:Label ID="lblCompany" runat="server" Font-Italic="true" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <%--<tr align="center">
                                                        <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                            <asp:Label ID="Label1" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                            <asp:Label ID="lblBusiness" runat="server" Font-Italic="true"></asp:Label>
                                                        </td>
                                                    </tr>--%>
                                                    <tr align="center">
                                                        <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                            <asp:Label ID="Label2" runat="server" Font-Italic="true" Text="List of Vacancies"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                CssClass="mGrid" EmptyDataText="No Record Found." OnRowCommand="GridView1_RowCommand"
                                                OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" PagerStyle-CssClass="pgr"
                                                Width="100%" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                PageSize="15">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%" HeaderText="Business Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label46" runat="server" Text='<%# Eval("wname") %>'></asp:Label>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("BusinessID") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" HeaderText="Type"
                                                        Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelLogin" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="Label47q" runat="server" Text='<%# Eval("vname") %>'></asp:Label>
                                                            <asp:Label ID="lblvacpositiontype" runat="server" Text='<%# Eval("vacancypositiontypeid") %>'
                                                                Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" HeaderText="Term"
                                                        Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label47112" runat="server" Text='<%# Eval("vvv")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="18%" HeaderText="Vacancy Position">
                                                        <ItemTemplate>
                                                            <%-- <a href="VacancyProfile.aspx?Id=<%# Eval("ID")%>"><b><font color="black">
                                                                <%#  Eval ("vtitle")  %></b></font></a>--%>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                CommandName="link" ForeColor="Black" Text='<%# Eval("vtitle")%>'></asp:LinkButton>
                                                            <asp:Label ID="lblvacpositiontitle" runat="server" Text='<%# Eval("vacancypositiontitleid") %>'
                                                                Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="9%" HeaderText="Schedule Hours"
                                                        Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label471as" runat="server" Text='<%# Eval("worktimings")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="9%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="7%" HeaderText="Start Date"
                                                        Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label471" runat="server" Text='<%# Eval("startdate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="7%" HeaderText="End Date"
                                                        Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label472" runat="server" Text='<%# Eval("enddate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%" HeaderText="Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label47111" runat="server" Text='<%# Eval("CityName")%>'></asp:Label>
                                                            ,<br />
                                                            <asp:Label ID="Label34" runat="server" Text='<%# Eval("StateName")%>'></asp:Label>
                                                            <asp:Label ID="Label50" runat="server" Text='<%# Eval("cityid")%>' Visible="false"></asp:Label>
                                                            ,<br />
                                                            <asp:Label ID="Label35" runat="server" Text='<%# Eval("CountryName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" HeaderText="Qualification Requirements">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="Label4721" runat="server" Text='<%# Eval("QualificationRequirements") %>'></asp:Label>--%>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                CommandName="link1" ForeColor="Black" Text='<%# Eval("QualificationRequirements")%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" HeaderText="Job Functions">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="Label47132" runat="server" Text='<%# Eval("JobFunction") %>'></asp:Label>--%>
                                                            <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                CommandName="link2" ForeColor="Black" Text='<%# Eval("JobFunction")%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" HeaderText="Terms and Conditions">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="Label19" runat="server" Text='<%# Eval("TermsandConditions")%>'></asp:Label>--%>
                                                            <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                CommandName="link3" ForeColor="Black" Text='<%# Eval("TermsandConditions")%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="2%" HeaderText="Edit" ItemStyle-HorizontalAlign="Left" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                CommandName="Edit" ImageUrl="~/Account/images/edit.gif" ToolTip="Edit" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderImageUrl="~/ShoppingCart/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="2%" HeaderText="Delete" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="llinkbb" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                ToolTip="Delete" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="2%" HeaderText="View" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                CommandName="View1" Height="20px" ImageUrl="~/Account/images/viewprofile.jpg"
                                                                ToolTip="View Vacancy" Width="20px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="1%" HeaderText="Refer to a friend"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkMsg11" runat="server" AutoPostBack="True" OnCheckedChanged="chkMsg11_CheckedChanged" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:Button ID="Button6" runat="server" CssClass="btnSubmit" OnClick="Button6_Click"
                                                                ToolTip="Please select atleast one job to send mail" Enabled="false" Height="40px"
                                                                Text="Send mail 
to a friend" />
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="1%" HeaderText="Apply"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkMsg" runat="server" AutoPostBack="True" OnCheckedChanged="chkMsg_CheckedChanged" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" OnClick="Button1_Click"
                                                                ToolTip="Please select atleast one job to apply" Enabled="false" Text="Apply" />
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <%-- <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" OnClick="Button1_Click"
                                                Text="Apply" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="Panel16" runat="server" BorderWidth="2px" BackColor="White" Width="450px"
                            Height="210px">
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="Label83" runat="server" Text="" ForeColor="Red" Font-Names="Verdana"
                                            Font-Size="12px" Font-Bold="false"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                            Width="15px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="panel2" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" TextAlign="Right" RepeatDirection="Horizontal"
                                                            AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                            <asp:ListItem Text="Are you a new candidate  OR" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Existing candidate ? " Selected="True" Value="1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td valign="bottom">
                                                        <asp:Button ID="Button4" runat="server" Text="Go" OnClick="Button4_Click" CssClass="btnSubmit"
                                                            Visible="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="panelpopo" runat="server" Visible="false">
                                            <table width="100%">
                                                <tr>
                                                    <td align="left">
                                                        <label>
                                                            <asp:Label ID="Label80" runat="server" Text="User ID" Font-Size="12px" Font-Bold="false"
                                                                Font-Names="Verdana"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                                ControlToValidate="TextBox1" ValidationGroup="12"></asp:RequiredFieldValidator>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:TextBox ID="TextBox1" runat="server" Width="150px"></asp:TextBox>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <label>
                                                            <asp:Label ID="Label82" runat="server" Font-Size="12px" Text="Password" Font-Bold="false"
                                                                Font-Names="Verdana"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                                ControlToValidate="TextBox4" ValidationGroup="12"></asp:RequiredFieldValidator>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:TextBox ID="TextBox4" runat="server" Width="150px" TextMode="Password"></asp:TextBox>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button7" runat="server" Text="Submit" ValidationGroup="12" CssClass="btnSubmit"
                                                            OnClick="Button7_Click" />
                                                        <asp:Button ID="Button8" runat="server" Text="Cancel" OnClick="Button8_Click" CssClass="btnSubmit" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="panel1" runat="server" Visible="false">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Thanks for Applying for the following Positions. We will get back to you , if we
                                                            need more information.
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label32" runat="server" Text="Position Name - " Font-Size="Large"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="Label33" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label41" runat="server" Text="If you need more information , Please contact us by email at"></asp:Label>
                                                            <asp:Label ID="lblemails" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="HiddenButtonresd" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender12" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel16" TargetControlID="HiddenButtonresd" CancelControlID="ImageButton4">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
            </table>
        </div>
        <div style="clear: both;">
        </div>
        <div>
            <asp:Panel ID="Panel21" runat="server" BackColor="White" BorderColor="#999999" Width="550px"
                Height="370px" BorderStyle="Solid" BorderWidth="10px">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%; font-weight: bold; color: #000000;" bgcolor="#CCCCCC">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label42" runat="server" Text="Send this Vacancy information to your friend by email."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlof" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="Label43" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%">
                                        </td>
                                        <td>
                                            <label>
                                                Sender
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%">
                                            <label>
                                                Your Name
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="TextBox2"
                                                    ForeColor="Red" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="11"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%">
                                            <label>
                                                Your Email ID
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TextBox3"
                                                    ForeColor="Red" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="11"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox3"
                                                    ForeColor="Red" ErrorMessage="Invalid Email ID" Font-Size="14px" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ValidationGroup="11"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%">
                                        </td>
                                        <td>
                                            <label>
                                                Recepient
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%">
                                            <label>
                                                Recepient Name
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="TextBox6"
                                                    ForeColor="Red" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="11"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%">
                                            <label>
                                                Recepient Email ID
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TextBox7"
                                                    ForeColor="Red" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="11"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TextBox7"
                                                    ForeColor="Red" ErrorMessage="Invalid Email ID" Font-Size="14px" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ValidationGroup="11"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%">
                                        </td>
                                        <td>
                                            <asp:Button ID="Button9" runat="server" Text="Send" CssClass="btnSubmit" OnClick="Button9_Click"
                                                ValidationGroup="11" />
                                            <asp:Button ID="Button10" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="Button10_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="ModalPopupExtenderAddnew" runat="server" BackgroundCssClass="modalBackground"
                Enabled="True" PopupControlID="Panel21" TargetControlID="HiddenButton222">
            </cc1:ModalPopupExtender>
        </div>
    </div>
</asp:Content>
