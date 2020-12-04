<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="AbsenceNoteninaddemo.aspx.cs" Inherits="BusinessCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script language="javascript" type="text/javascript">

        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


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
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Fill Absense Report"></asp:Label>
                    </legend>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td style="width: 20%">
                                <label>
                                    Business Name
                                    <asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlbusiness"
                                        InitialValue="0" SetFocusOnError="true" ValidationGroup="save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <label>
                                    Employee Name
                                    <asp:Label ID="Label1" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlemployee"
                                        InitialValue="0" SetFocusOnError="true" ValidationGroup="save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlemployee" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <label>
                                    Date
                                    <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtstartdate"
                                        SetFocusOnError="true" ValidationGroup="save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtstartdate" runat="server" Width="75px" MaxLength="10"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton2"
                                        Format="MM/dd/yyyy" TargetControlID="txtstartdate">
                                    </cc1:CalendarExtender>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <label>
                                    Reason
                                    <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlreason"
                                        InitialValue="0" SetFocusOnError="true" ValidationGroup="save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlreason" runat="server">
                                        <asp:ListItem Selected="True" Text="Sick" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Family Emergency" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Out of Town" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Personal Work" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Others" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <label>
                                    Note
                                    <asp:Label ID="asdsadsad" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="redsd" runat="server" ControlToValidate="txtdescription"
                                        SetFocusOnError="true" ValidationGroup="save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                        ControlToValidate="txtdescription" ValidationGroup="save"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtdescription" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div2',1500)"
                                        runat="server" Height="60px" onkeypress="return checktextboxmaxlength(this,1500,event)"
                                        Width="360px" TextMode="MultiLine"></asp:TextBox>
                                    <asp:Label runat="server" ID="Label12" Text="Max " CssClass="labelcount"></asp:Label>
                                    <span id="div2" cssclass="labelcount">1500</span>
                                    <asp:Label ID="Label24" CssClass="labelcount" runat="server" Text="(A-Z 0-9 . , ? _)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                            </td>
                            <td>
                                <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Submit" ValidationGroup="save"
                                    CssClass="btnSubmit" />
                                <asp:Button ID="btncancel" runat="server" OnClick="btncancel_Click" Text="Cancel"
                                    CssClass="btnSubmit" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
