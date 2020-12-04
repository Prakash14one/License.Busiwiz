<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="CandidateTempJobDailyRegistration.aspx.cs" Inherits="BusinessCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script language="javascript" type="text/javascript">

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
    <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <div style="padding-left: 2%">
                    <asp:CheckBox ID="CheckBox6" runat="server" Text="I am available to work today for temporary job."
                        TextAlign="Left" AutoPostBack="True" OnCheckedChanged="CheckBox6_CheckedChanged"
                        Checked="True" />
                    <%--            <asp:Label ID="lbllegend" runat="server" Text="I am available to work today for temporary job."
                        ForeColor="#416271" Font-Size="18px"></asp:Label>--%>
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel4" runat="server" Visible="false">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label4" runat="server" Text="My Preferences for temporary job are"></asp:Label>
                        </legend>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="panelup" runat="server" Visible="true">
                            <div>
                                <label>
                                    Same preferences as last time ?
                                </label>
                                <asp:RadioButtonList ID="RadioButtonList3" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged">
                                    <asp:ListItem Text="Yes" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <asp:Panel ID="Panel5" runat="server" Visible="false">
                                <div>
                                    <label>
                                        You have a previous entry. Do you wish to show the same for your easy update?
                                    </label>
                                    <asp:CheckBox ID="CheckBox5" runat="server" Text="Yes" TextAlign="Left" AutoPostBack="True"
                                        OnCheckedChanged="CheckBox5_CheckedChanged" />
                                </div>
                            </asp:Panel>
                            <div style="clear: both;">
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel6" runat="server" Visible="false">
                            <div>
                                <asp:Label ID="Label3" runat="server" Text="A - Preferred Timings" ForeColor="Black"></asp:Label>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <table width="100%">
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            <asp:Label ID="Label1" runat="server" Text="I am available today to work from (HH:MM) "></asp:Label>
                                        </label>
                                        <label>
                                            <asp:TextBox ID="TextBox1" runat="server" Width="50px" Text="09:00"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                Display="Dynamic" CssClass="labelstar" ControlToValidate="TextBox1" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox1"
                                                CssClass="labelstar" ErrorMessage="*" ValidationExpression="^([0-1][0-2]|[0][0-9]):(([0-5][0-9]))$"
                                                Display="Dynamic" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                        <label>
                                            <asp:DropDownList ID="DropDownList1" runat="server" Width="50px">
                                                <asp:ListItem Text="AM" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </label>
                                        <label>
                                            to
                                        </label>
                                        <label>
                                            <asp:TextBox ID="TextBox2" runat="server" Width="50px" Text="05:00"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                Display="Dynamic" CssClass="labelstar" ControlToValidate="TextBox2" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox2"
                                                CssClass="labelstar" ErrorMessage="*" ValidationExpression="^([0-1][0-2]|[0][0-9]):(([0-5][0-9]))$"
                                                Display="Dynamic" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                        <label>
                                            <asp:DropDownList ID="DropDownList2" runat="server" Width="50px">
                                                <asp:ListItem Text="AM" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="1" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </label>
                                        <label>
                                            <asp:DropDownList ID="ddltimezone" runat="server" Width="200px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            If Employer Requires I will be Available to work
                                        </label>
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="0" Text="Any Day in next two weeks"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="On Specific Days"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div style="padding-left: 1%">
                                            <asp:Panel ID="panel1" runat="server" Visible="false">
                                                <asp:DataList ID="DataList1" runat="server" RepeatColumns="3" RepeatLayout="Table"
                                                    Width="50%" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                    <ItemTemplate>
                                                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    <asp:CheckBox ID="chkMsg11" runat="server" />
                                                                </td>
                                                                <td style="width: 90%">
                                                                    <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                    <asp:Label ID="Label2" runat="server" Visible="false" Text='<%#Eval("Id") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="width: 22%">
                                        <div style="padding-left: 1%">
                                            <asp:CheckBox ID="chkMsg11" runat="server" Text="Add Specific Notes (Optional)" TextAlign="Left"
                                                AutoPostBack="true" OnCheckedChanged="chkMsg11_CheckedChanged" />
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Panel ID="panelimpnotes" runat="server" Visible="false">
                                            <label>
                                                <asp:TextBox ID="txtdescription" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div2',1500)"
                                                    runat="server" Height="60px" onkeypress="return checktextboxmaxlength(this,1500,event)"
                                                    Width="360px" TextMode="MultiLine"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                    CssClass="labelstar" ControlToValidate="txtdescription" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                                    ControlToValidate="txtdescription" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtdescription"
                                                    SetFocusOnError="True" ErrorMessage="*" ValidationExpression="^([\S\s]{0,1500})$"
                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                                <br />
                                                <br />
                                                <asp:Label runat="server" ID="Label12" Text="Max " CssClass="labelcount"></asp:Label>
                                                <span id="div2" cssclass="labelcount">1500</span>
                                                <asp:Label ID="Label24" CssClass="labelcount" runat="server" Text="(A-Z 0-9 . , ? _)"></asp:Label>
                                            </label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <div style="clear: both;">
                            </div>
                            <div>
                                <asp:Label ID="Label5" runat="server" Text="B - Preferred Location" ForeColor="Black"></asp:Label>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <table>
                                <tr>
                                    <td valign="top">
                                        <label>
                                            <asp:Label runat="server" ID="Label6" Text="I am available to work at "></asp:Label>
                                        </label>
                                    </td>
                                    <td valign="top">
                                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="0" Text="Anywhere"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Specific Area"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td valign="bottom">
                                        <asp:Panel runat="server" Visible="false" ID="panelarea">
                                            <label>
                                                <asp:TextBox ID="TextBox3" runat="server" MaxLength="150" onKeydown="return mask(event)"
                                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div1',50)"></asp:TextBox>
                                                <asp:Label runat="server" ID="sadasd" Text="Max " CssClass="labelcount"></asp:Label>
                                                <span id="div1" cssclass="labelcount">50</span>
                                                <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 . , ? _)" CssClass="labelcount"></asp:Label>
                                            </label>
                                        </asp:Panel>
                                    </td>
                                    <td valign="top">
                                        <label>
                                            of City
                                        </label>
                                        <label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                InitialValue="0" CssClass="labelstar" ControlToValidate="ddlcity" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </label>
                                        <label>
                                            <asp:DropDownList ID="ddlcity" runat="server">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                            <div style="clear: both;">
                            </div>
                            <div>
                                <asp:Label ID="Label7" runat="server" Text="C - Prefered Work Position" ForeColor="Black"></asp:Label>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="Label400" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%" valign="top">
                                        <label>
                                            Select Position Type
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                                InitialValue="0" ValidationGroup="1" ControlToValidate="ddlvacancytype"></asp:RequiredFieldValidator>
                                        </label>
                                        <label>
                                            <asp:DropDownList ID="ddlvacancytype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlvacancytype_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label50" runat="server" Text="Select Job Titles" Visible="false"></asp:Label>
                                            <br />
                                            <asp:Button ID="Button7" runat="server" Text="Select" Visible="false" OnClick="Button7_Click" />
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel runat="server" ID="panel2" ScrollBars="Vertical" Height="100px" Visible="false">
                                            <asp:DataList ID="datalist2" runat="server" RepeatColumns="4" DataKeyField="ID" RepeatLayout="Table"
                                                Width="60%" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                <ItemTemplate>
                                                    <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                        <tr>
                                                            <td style="width: 10%">
                                                                <asp:CheckBox ID="chkMsg11" runat="server" />
                                                            </td>
                                                            <td style="width: 90%">
                                                                <asp:Label ID="lblvtitle" runat="server" Text='<%#Eval("vtitle") %>'></asp:Label>
                                                                <asp:Label ID="Label51" Visible="false" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <asp:Panel runat="server" ID="panel3" Visible="false">
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblvtsdsdsitle" runat="server" ForeColor="#416271" Text="My Prefered Position"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="left">
                                            <asp:GridView ID="GridView5" runat="server" EmptyDataText="No Record Found." CssClass="mGrid"
                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="Both"
                                                AutoGenerateColumns="False" Width="60%" OnRowCommand="GridView5_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderText="Position Type" HeaderStyle-Width="35%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvtype" runat="server" Text='<%#Eval("VacancyType") %>'></asp:Label>
                                                            <asp:Label ID="Label511" Visible="false" runat="server" Text='<%#Eval("VacancyTypeID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderText="Position Title" HeaderStyle-Width="45%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvtitle" runat="server" Text='<%#Eval("VacancyTitle") %>'></asp:Label>
                                                            <asp:Label ID="Label521" Visible="false" runat="server" Text='<%#Eval("VacancyTitleID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:ButtonField ButtonType="Image" HeaderImageUrl="~/Account/images/delete.gif"
                                                        ImageUrl="~/Account/images/delete.gif" HeaderText="Delete" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%" CommandName="del"></asp:ButtonField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                            <div style="clear: both;">
                            </div>
                            <div>
                                <asp:Label ID="Label8" runat="server" Text="D - Preferred Remuneration" ForeColor="Black"></asp:Label>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <label>
                                            I expect to be paid in
                                        </label>
                                        <label>
                                            <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged"
                                                Width="90px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="bottom">
                                        <label>
                                            <asp:TextBox ID="TextBox6" runat="server" Width="80px" MaxLength="5"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="TextBox6" ValidChars="0123456789.">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <%--          <label>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="TextBox6"
                                                ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>--%>
                                        <label>
                                            Per Hour OR
                                        </label>
                                        <label>
                                            <asp:TextBox ID="TextBox7" runat="server" Width="80px" MaxLength="6"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                TargetControlID="TextBox7" ValidChars="0123456789.">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <%--           <label>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="TextBox7"
                                                ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>--%>
                                        <label>
                                            Per Day OR
                                        </label>
                                        <label>
                                            <asp:TextBox ID="TextBox8" runat="server" Width="80px" MaxLength="7"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                TargetControlID="TextBox8" ValidChars="0123456789.">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <%--        <label>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="TextBox8"
                                                ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>--%>
                                        <label>
                                            Per Week OR
                                        </label>
                                        <label>
                                            <asp:TextBox ID="TextBox9" runat="server" Width="80px" MaxLength="8"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                TargetControlID="TextBox9" ValidChars="0123456789.">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <%--       <label>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ControlToValidate="TextBox9"
                                                ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>--%>
                                        <label>
                                            Per Month OR
                                        </label>
                                        <label>
                                            <asp:TextBox ID="TextBox11" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                TargetControlID="TextBox11" ValidChars="0123456789.">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <%--    <label>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ControlToValidate="TextBox11"
                                                ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>--%>
                                        <label>
                                            Per Project
                                        </label>
                                    </td>
                                </tr>
                                <asp:Panel ID="pandnswwe" runat="server" Visible="false">
                                    <tr>
                                        <td>
                                            <label>
                                                OR if Paid By year
                                            </label>
                                        </td>
                                        <td>
                                        </td>
                                        <td valign="bottom">
                                            <asp:TextBox ID="TextBox10" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                TargetControlID="TextBox10" ValidChars="0123456789.">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <label>
                                                Per Year
                                            </label>
                                            <label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" ControlToValidate="TextBox10"
                                                    ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid Digits"
                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td>
                                        <label>
                                            Consider my application even for vacancies where remuneration is negotiable or unspecified.
                                        </label>
                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                    </td>
                                </tr>
                                <asp:Panel ID="pansdad" runat="server" Visible="false">
                                    <tr>
                                        <td colspan="4">
                                            <label>
                                                <asp:Label ID="Label42" runat="server" Text="USD" ForeColor="Black"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label43" runat="server" Text="USD" ForeColor="Black"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label44" runat="server" Text="USD" ForeColor="Black"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label45" runat="server" Text="USD" ForeColor="Black"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label46" runat="server" Text="USD" ForeColor="Black"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label47" runat="server" Text="USD" ForeColor="Black"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                            <div style="clear: both;">
                            </div>
                            <div>
                                <asp:Label ID="Label9" runat="server" Text="E - Preferred Contact Method" ForeColor="Black"></asp:Label>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <table>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label10" runat="server" Text="Employer can contact me by"></asp:Label>
                                        </label>
                                    </td>
                                    <td valign="top">
                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Phone" TextAlign="Right" Checked="true" />
                                        <asp:CheckBox ID="CheckBox4" runat="server" Text="Mobile Text" TextAlign="Right" />
                                        <asp:CheckBox ID="CheckBox3" runat="server" Text="Email" TextAlign="Right" Checked="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td valign="bottom">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td valign="top">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="Button2" runat="server" Text="Submit" OnClick="Button1_Click" ValidationGroup="1"
                                            CssClass="btnSubmit" />
                                        <%--   <asp:ImageButton ID="Button1" runat="server" OnClick="Button1_Click" ImageUrl="~/images/availablenow.jpg"
                                            ValidationGroup="1" />--%>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
