<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="JobVacancyIJ.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
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
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <fieldset>
                <legend>
                    <asp:Label ID="lbllegend" runat="server" Text=""></asp:Label>
                </legend>
                <div style="float: right;">
                </div>
                <asp:Panel ID="panellink" runat="server">
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    Public web URL for the vacancies of your business -
                                    <asp:Label ID="Label59" runat="server" Text="" ForeColor="Black"></asp:Label>
                                    is :
                                    <asp:Label ID="Label53" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                    <br />
                                    Any candidate can visit this URL and post his/her resume from that URL
                                </label>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnadd" runat="server" CssClass="btnSubmit" Text="Add Vacancy" OnClick="btnadd_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div style="clear: both;">
                </div>
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
                                    <asp:DropDownList ID="ddldeptdesg" runat="server" Width="250px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddldeptdesg_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label39" runat="server" Text="Vacancy Type"></asp:Label>
                                    <asp:Label ID="Label42" runat="server" Text="*" CssClass="labelstar"></asp:Label>
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
                                    <asp:Label ID="Label40" runat="server" Text="Vacancy Position Title"></asp:Label>
                                    <asp:Label ID="Label41" runat="server" Text="*" CssClass="labelstar"></asp:Label>
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
                                <label>
                                    <asp:ImageButton ID="ImageButton6" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                        Visible="false" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" OnClick="ImageButton6_Click" />
                                    <asp:ImageButton ID="ImageButton7" runat="server" AlternateText="Refresh" Height="20px"
                                        Visible="false" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh"
                                        Width="20px" ImageAlign="Bottom" OnClick="ImageButton7_Click" />
                                </label>
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
                                    <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rdg3" runat="server" ControlToValidate="ddlCountry"
                                        ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                        ValidationGroup="1" Width="151px">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="lblStatelbl" runat="server" Text="State"></asp:Label>
                                    <asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rd1n1" runat="server" ControlToValidate="ddlState"
                                        ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                        ValidationGroup="1" Width="151px">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="lblCitylbl" runat="server" Text="City"></asp:Label>
                                    <asp:Label ID="Label43" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFielgdValidator12" runat="server" ControlToValidate="ddlCity"
                                        ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlCity"
                                        ErrorMessage="*" InitialValue="0" ValidationGroup="2"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCity" runat="server" ValidationGroup="1" Width="151px">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <br />
                                    <asp:Button ID="Button2" runat="server" Text="Add" CssClass="btnSubmit" ValidationGroup="2"
                                        OnClick="Button2_Click" />
                                </label>
                            </td>
                        </tr>
                        <asp:Panel ID="panelgr" runat="server" Visible="false">
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                    <asp:Panel ID="panel1" runat="server" Height="150px" ScrollBars="Vertical" Width="60%">
                                        <asp:GridView ID="GridView5" runat="server" EmptyDataText="No Record Found." CssClass="mGrid"
                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="Both"
                                            AutoGenerateColumns="False" Width="100%" OnRowCommand="GridView5_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="Country" HeaderStyle-Width="25%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labelcountry" runat="server" Text='<%#Eval("Country") %>'></asp:Label>
                                                        <asp:Label ID="labelcountryid" Visible="false" runat="server" Text='<%#Eval("CountryID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="State" HeaderStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labelstate" runat="server" Text='<%#Eval("State") %>'></asp:Label>
                                                        <asp:Label ID="labelstateid" Visible="false" runat="server" Text='<%#Eval("StateID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="City" HeaderStyle-Width="35%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labelcity" runat="server" Text='<%#Eval("City") %>'></asp:Label>
                                                        <asp:Label ID="labelcityid" Visible="false" runat="server" Text='<%#Eval("CityID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:ButtonField ButtonType="Image" HeaderImageUrl="~/Account/images/delete.gif"
                                                    ImageUrl="~/Account/images/delete.gif" HeaderText="Delete" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%" CommandName="del"></asp:ButtonField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label55" runat="server" Text="Vacancy Duration"></asp:Label>
                                    <asp:Label ID="Label56" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="DropDownList3"
                                        InitialValue="0" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td style="width: 70%">
                                <label>
                                    <asp:DropDownList ID="DropDownList3" runat="server">
                                    </asp:DropDownList>
                                </label>
                        </tr>
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label13" runat="server" Text="Number of Vacancies"></asp:Label>
                                    <asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>
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
                                    <asp:Label runat="server" ID="Label26" Text="Max " CssClass="labelcount"></asp:Label>
                                    <span id="Span2">3</span>
                                    <asp:Label ID="Label23" runat="server" CssClass="labelcount" Text="(0-9)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="Vacancy Advertisement Start and End Date"></asp:Label>
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
                                <label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtestartdate"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                                <label>
                                    to
                                </label>
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
                                <label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txteenddate"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
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
                            <td style="width: 30%" valign="top">
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
                                    <asp:TextBox ID="txtjobfunction" Width="450px" Height="60px" runat="server" TextMode="MultiLine"
                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div2',1500)"
                                        MaxLength="1500" onKeydown="return mask(event)" onkeypress="return checktextboxmaxlength(this,1500,event)"></asp:TextBox>
                                    <asp:Label runat="server" ID="Label25" Text="Max " CssClass="labelcount"></asp:Label>
                                    <span id="div2">1500</span>
                                    <asp:Label ID="Label22" runat="server" CssClass="labelcount" Text="(A-Z 0-9 . , ? _)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%" valign="top">
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
                                    <asp:TextBox ID="txtqualireq" Width="450px" Height="60px" runat="server" TextMode="MultiLine"
                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'Span3',1500)"
                                        MaxLength="1500" onKeydown="return mask(event)" onkeypress="return checktextboxmaxlength(this,1500,event)"></asp:TextBox>
                                    <asp:Label runat="server" ID="Label11" Text="Max " CssClass="labelcount"></asp:Label>
                                    <span id="Span3">1500</span>
                                    <asp:Label ID="Label14" runat="server" CssClass="labelcount" Text="(A-Z 0-9 . , ? _)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label15" runat="server" Text="Salary"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 70%">
                                <label>
                                    <asp:Label ID="Label50" runat="server" Text="Currrency"></asp:Label>
                                    <asp:DropDownList ID="ddlcurrency" runat="server" Width="70px">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label16" runat="server" Text="Amount"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtmysalary" MaxLength="10" Width="80px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                        TargetControlID="txtmysalary" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </label>
                                <label>
                                    <asp:Label ID="Label38" runat="server" Text="per"></asp:Label>
                                    <asp:DropDownList ID="ddlsalperiod" runat="server" Width="90px">
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
                                    <asp:TextBox ID="txttermcond" Width="450px" Height="60px" runat="server" TextMode="MultiLine"
                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div22',1500)"
                                        MaxLength="1500" onKeydown="return mask(event)" onkeypress="return checktextboxmaxlength(this,1500,event)"></asp:TextBox>
                                    <asp:Label runat="server" ID="Labefdsdl25" Text="Max " CssClass="labelcount"></asp:Label>
                                    <span id="div22">1500</span>
                                    <asp:Label ID="Labdfsel22" runat="server" CssClass="labelcount" Text="(A-Z 0-9 . , ? _)"></asp:Label>
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
                                    <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
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
                                    <asp:TextBox ID="txtnoofhours" Width="55px" runat="server" MaxLength="5" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'Span100',5)"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                        TargetControlID="txtnoofhours" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </label>
                                <label>
                                    <asp:Label ID="Label21" runat="server" Text="per"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlhourperiod" runat="server" Width="95px">
                                        <asp:ListItem Selected="True" Text="Day" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Week" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                                <asp:Panel ID="panewsdsdsd" runat="server" Visible="false">
                                    <label>
                                        <asp:Label runat="server" ID="Label48" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="Span100">5</span>
                                        <asp:Label ID="Label49" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label28" runat="server" Text="Contact Email ID"></asp:Label>
                                    <asp:Label ID="Label33" runat="server" Text="*" CssClass="labelstar"></asp:Label>
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
                                    <asp:Label ID="Label31" runat="server" Text="Candidate should apply by"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 70%">
                                <asp:CheckBox ID="chkemail" runat="server" Text="Email" />
                                <asp:CheckBox ID="chkphone" runat="server" Text="Phone" AutoPostBack="True" OnCheckedChanged="chkphone_CheckedChanged" />
                                <asp:CheckBox ID="chkvisit" runat="server" Text="Personal Visit" AutoPostBack="True"
                                    OnCheckedChanged="chkvisit_CheckedChanged" />
                                <asp:CheckBox ID="chkonline" runat="server" Text="Online Form" />
                            </td>
                        </tr>
                        <asp:Panel ID="panelcontact" Visible="false" runat="server">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label27" runat="server" Text="Contact Person Name"></asp:Label>
                                        <asp:Label ID="Label32" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtpername"
                                            SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                            ErrorMessage="Invalid Character" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                            ControlToValidate="txtpername" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%" colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtpername" runat="server" MaxLength="60" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div12',60)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="sadasd" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="div12">60</span>
                                        <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 . , ? _)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label29" runat="server" Text="Contact Phone Number"></asp:Label>
                                        <asp:Label ID="Label34" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtphoneno"
                                            SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                            TargetControlID="txtphoneno" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                </td>
                                <td style="width: 70%" colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtphoneno" runat="server" MaxLength="20"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="panel1address" Visible="false" runat="server">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label30" runat="server" Text="Contact Address"></asp:Label>
                                        <asp:Label ID="Label35" runat="server" Text="*" CssClass="labelstar"></asp:Label>
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
                                <td style="width: 70%" colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtconaddress" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'Span4',1500)"
                                            runat="server" Height="60px" onkeypress="return checktextboxmaxlength(this,1500,event)"
                                            Width="450px" TextMode="MultiLine"></asp:TextBox>
                                        <asp:Label runat="server" ID="Label36" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="Span4">1500</span>
                                        <asp:Label ID="Label37" CssClass="labelcount" runat="server" Text="(A-Z 0-9 . , ? _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                        </asp:Panel>
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
                                <a href=""  style="color:Brown" target=>
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
            </fieldset>
            <fieldset>
                <legend>
                    <asp:Label ID="Label45" runat="server" Text="List of Vacancies"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <div style="float: right;">
                                <asp:Button ID="btncancel0" runat="server" CssClass="btnSubmit " CausesValidation="false"
                                    OnClick="btncancel0_Click" Text="Printable Version" />
                                <input id="Button7" runat="server" visible="false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    class="btnSubmit" type="button" value="Print" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <label>
                                Business Name
                            </label>
                            <label>
                                <asp:DropDownList ID="DropDownList1" Width="180px" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                            <label>
                                Position Title
                            </label>
                            <label>
                                <asp:DropDownList ID="DropDownList4" Width="180px" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </label>
                            <asp:Panel ID="noneed" runat="server" Visible="false">
                                <label>
                                    Dept - Desi
                                </label>
                                <label>
                                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </label>
                            </asp:Panel>
                            <label>
                                Status
                            </label>
                            <label>
                                <asp:DropDownList ID="ddlstatus_search" runat="server" Width="100px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlstatus_search_SelectedIndexChanged">
                                    <asp:ListItem>All</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="Label8" runat="server" Text="From"></asp:Label>
                                <asp:Label ID="Label18" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtestartdate"
                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </label>
                            <label>
                                <asp:TextBox ID="TextBox1" runat="server" Width="70px"></asp:TextBox>
                            </label>
                            <label>
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                            </label>
                            </label>
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="ImageButton3"
                                TargetControlID="TextBox1">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="TextBox1">
                            </cc1:MaskedEditExtender>
                            <label>
                                <asp:Label ID="Label18q" runat="server" Text="To"></asp:Label>
                                <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txteenddate"
                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </label>
                            <label>
                                <asp:TextBox ID="TextBox2" runat="server" Width="70px"></asp:TextBox>
                            </label>
                            <label>
                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                            </label>
                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="ImageButton4"
                                TargetControlID="TextBox2">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="TextBox2">
                            </cc1:MaskedEditExtender>
                            <label>
                                <asp:Button ID="Button1" runat="server" Text="Go" CssClass="btnSubmit" OnClick="Button1_Click" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 35%">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
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
                                            <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" EmptyDataText="No Record Found."
                                                OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                                                AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="15">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label46" runat="server" Text='<%# Eval("wname") %>'></asp:Label>
                                                            <asp:Label ID="Label51" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department: Designation" HeaderStyle-HorizontalAlign="Left"
                                                        Visible="false" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label47" runat="server" Text='<%# Eval("dname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Position Title" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Eval("vtitle") %>' CommandArgument='<%# Eval("ID") %>'
                                                                ForeColor="Black" CommandName="View1" ToolTip="View Profile"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label47q" runat="server" Text='<%# Eval("vname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Term" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label47qfsdsd" runat="server" Text='<%# Eval("Term") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%" HeaderText="Qualification Requirements"
                                                        SortExpression="QualificationRequirements">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label35Qualification" runat="server" Text='<%# Eval("QualificationRequirements")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%" HeaderText="Terms and Conditions"
                                                        SortExpression="TermsandConditions">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label35Qualidsdsdfication" runat="server" Text='<%# Eval("TermsandConditions")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" HeaderText="Salary"
                                                        SortExpression="salary">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label34" runat="server" Text='<%# Eval("ccc")%>' ForeColor="Black"></asp:Label>
                                                            <asp:Label ID="LinkButton88" runat="server" Text='<%# Eval("salary","{0:###,###.##}")%>'></asp:Label>
                                                            <asp:Label ID="Label57" runat="server" Text="/"></asp:Label>
                                                            <asp:Label ID="Label58" runat="server" Text='<%# Eval("sss")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="8%"
                                                        Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label471" runat="server" Text='<%# Eval("startdate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Open till date" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label472" runat="server" Text='<%# Eval("enddate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="4%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label473" runat="server" Text='<%# Eval("Statuslabel") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Candidates Applied" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("candidate") %>' CommandName="Vii"
                                                                ForeColor="Black" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                                            <%--<asp:Label ID="Label473asdasd" runat="server" Text='<%# Eval("candidate") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ID") %>'
                                                                ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%"
                                                        HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ToolTip="Delete"
                                                                CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                            </asp:ImageButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View" HeaderImageUrl="~/Account/images/viewprofile.jpg"
                                                        Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                CommandName="View" ToolTip="View Profile" Height="20px" ImageUrl="~/Account/images/viewprofile.jpg"
                                                                Width="20px"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" />
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
                </table>
            </fieldset>
            <div>
                <asp:Panel ID="Panel16" runat="server" BackColor="White" BorderColor="#416271" Width="470px"
                    Height="220px" BorderStyle="Solid" BorderWidth="3px">
                    <table id="Table6" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="ImageButton5" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                    Width="15px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="padding-left: 2%">
                                    <label>
                                        Please use the following link to easily access the vacancy list of your business.
                                        You can use this link also for inserting in your newspaper advt.
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                &nbsp;
                                <asp:Label ID="Label52" runat="server" Text="" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <%--      <tr>
                            <td>
                                <label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    OR
                                    <br />
                                    &nbsp; you can use the general vacancy URL for your entire company.
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                &nbsp;
                                <asp:Label ID="Label54" runat="server" Text="" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>--%>
                    </table>
                </asp:Panel>
                <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel16" TargetControlID="HiddenButton222" CancelControlID="ImageButton5">
                </cc1:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
