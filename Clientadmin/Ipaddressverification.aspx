<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true"
    CodeFile="Ipaddressverification.aspx.cs" Inherits="ShoppingCart_Admin_Ipaddressverification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <head>
        <title>Security Verification</title>
    </head>
    <div id="headerInfo">
        <span>
            <asp:Label runat="server" ID="lblcompanyname"></asp:Label></span>
        <div style="clear: both;">
        </div>
        <span>
            <asp:Label runat="server" ID="lbladdress1"></asp:Label>
        </span>
        <div style="clear: both;">
        </div>
        <span>
            <asp:Label runat="server" ID="lbltollfreeno"></asp:Label>
        </span>
        <div style="clear: both;">
        </div>
        <span>
            <asp:Label runat="server" ID="lblphoneno"></asp:Label>
            <asp:Label runat="server" ID="lblphone2"></asp:Label>
        </span>
        <div style="clear: both;">
        </div>
        <span>
            <asp:Label runat="server" ID="lblemail"></asp:Label>
        </span>
        <div style="clear: both;">
        </div>
        <span>
            <asp:Label runat="server" ID="lblwebsite"></asp:Label>
        </span>
        <div style="clear: both;">
        </div>
    </div>
    <div id="logo">
        <a href="#">
            <img id="ImageButton1" runat="server" alt="" title="" border="0" width="200" height="100" />
        </a>
    </div>
    <div id="right_content">
        <h2>
            Security Verification for Adding IP Addresses to the List of Allowed IP Addresses
        </h2>
        <div class="products_box">
            <fieldset>
                <asp:Panel ID="pnlradiooption" runat="server">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Vertical"
                        AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                        <asp:ListItem Value="0">Would you like to add the IP address  to the list of allowed IP addresses for the user name ?</asp:ListItem>
                        <asp:ListItem Value="1" Selected="True">Would you like to add the IP address  to the list of allowed IP addresses for the all the users of the company?</asp:ListItem>
                    </asp:RadioButtonList>
                </asp:Panel>
            </fieldset>
            <asp:Panel ID="pnloption2" runat="server" Visible="false">
                <fieldset>
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Vertical"
                        AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                        <asp:ListItem Value="0">Send an email to admin to add this IP address to the list of allowed IP addresses for the whole company.</asp:ListItem>
                        <asp:ListItem Value="1">Verify yourself by answering security questions to add this IP address to the list of allowed IP addresses for the entire company.</asp:ListItem>
                        <asp:ListItem Value="2">Verify yourself by providing the license key to add this IP address to the list of allowed IP addresses for the whole company. </asp:ListItem>
                    </asp:RadioButtonList>
                </fieldset>
            </asp:Panel>
            <asp:Panel ID="pnlradio2option1" runat="server" Visible="false">
                <fieldset>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 25%">
                            </td>
                            <td style="width: 25%">
                                <asp:Button ID="Button8" runat="server" CssClass="button" Text="Send An Email to Admin"
                                    OnClick="Button8_Click" />
                            </td>
                            <td style="width: 25%">
                            </td>
                            <td style="width: 25%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                            </td>
                            <td style="width: 75%" colspan="3">
                                <label>
                                    <asp:Label ID="lblemailconfirmation" runat="server" ForeColor="Red"> </asp:Label>
                                </label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" Visible="False">
                <fieldset>
                    <label>
                        <asp:Label ID="Label20" runat="server" Text="Attempt"> </asp:Label>
                        <asp:Label ID="lblzerocounter" runat="server"> </asp:Label>
                        <asp:Label ID="lblcount1" runat="server" Text="of"> </asp:Label>
                        <asp:Label ID="Label23" runat="server" Text="5"> </asp:Label>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                    <asp:Label ID="Label21" runat="server" ForeColor="Red">
                                            </asp:Label>
                    </label>
                </fieldset>
            </asp:Panel>
            <asp:Panel ID="pnlradio2option2" runat="server" Visible="false">
                <fieldset>
                    <asp:Panel ID="pnluseridpassword" Width="100%" runat="server">
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label1" Text="Please verify yourself as an admin user of the company in order to add the IP address "
                                    runat="server"> </asp:Label>
                                <asp:Label ID="lblsecondoptindynamicipaddress" runat="server"> </asp:Label>
                                <asp:Label ID="Label2" Text=" to the list of allowed IP addresses for the whole company."
                                    runat="server"></asp:Label>
                            </legend>
                            <table style="width: 100%">
                                
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            <asp:Label ID="lblmsg" runat="server" ForeColor="Red">
                                            </asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label3" runat="server" Text="Company ID">
                                            </asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcompanyid"
                                                ErrorMessage="*" ValidationGroup="1">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtcompanyid"
                                                ValidationGroup="1">
                                            </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td style="width: 75%">
                                        <asp:TextBox ID="txtcompanyid" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label4" runat="server" Text="User ID">
                                            </asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtuserid"
                                                ErrorMessage="*" ValidationGroup="1">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([_@.a-zA-Z0-9\s]*)" ControlToValidate="txtuserid"
                                                ValidationGroup="1">
                                            </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtuserid" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label5" runat="server" Text="Password">
                                            </asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtpassword"
                                                ErrorMessage="*" ValidationGroup="1">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([_#$&*-+=@.a-zA-Z0-9\s]*)" ControlToValidate="txtpassword"
                                                ValidationGroup="1">
                                            </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpassword" runat="server" MaxLength="50" TextMode="Password" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" CssClass="button" ValidationGroup="1" Text="Confirm"
                                            OnClick="Button1_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="pnlsecurityquestion" Width="100%" runat="server" Visible="false">
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label19" Text="Please answer the below security questions. " runat="server"> </asp:Label>
                            </legend>
                            <asp:Panel ID="Panel1" Width="100%" runat="server">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblsecuritylblmsg" ForeColor="Red" runat="server">
                                                </asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label6" Text="Question 1" runat="server">
                                                </asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 75%">
                                            <label>
                                                <asp:Label ID="lblquestion1" runat="server">
                                                </asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label8" Text="Answer 1" runat="server">
                                                </asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtanswer1"
                                                    ErrorMessage="*" ValidationGroup="2">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                                    SetFocusOnError="True" ValidationExpression="^([_@.a-zA-Z0-9\s]*)" ControlToValidate="txtanswer1"
                                                    ValidationGroup="2">
                                                </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtanswer1" runat="server" MaxLength="60" ValidationGroup="2"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:Button ID="Button3" runat="server" ValidationGroup="2" CssClass="button" Text="Go"
                                                    OnClick="Button3_Click" />
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlsecurityquestion2" Width="100%" runat="server" Visible="false">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label9" Text="Question 2" runat="server">
                                                </asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 75%">
                                            <label>
                                                <asp:Label ID="lblquestion2" runat="server">
                                                </asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label11" Text="Answer 2" runat="server">
                                                </asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtanswer2"
                                                    ErrorMessage="*" ValidationGroup="3">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                                    SetFocusOnError="True" ValidationExpression="^([_@.a-zA-Z0-9\s]*)" ControlToValidate="txtanswer2"
                                                    ValidationGroup="3">
                                                </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtanswer2" runat="server" MaxLength="60" ValidationGroup="3"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:Button ID="Button4" runat="server" ValidationGroup="3" CssClass="button" Text="Go"
                                                    OnClick="Button4_Click" />
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlsecurityquestion3" Width="100%" runat="server" Visible="false">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label12" Text="Question 3" runat="server">
                                                </asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 75%">
                                            <label>
                                                <asp:Label ID="lblquestion3" runat="server">
                                                </asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label14" Text="Answer 3" runat="server">
                                                </asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtanswer3"
                                                    ErrorMessage="*" ValidationGroup="4">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Character"
                                                    SetFocusOnError="True" ValidationExpression="^([_@.a-zA-Z0-9\s]*)" ControlToValidate="txtanswer3"
                                                    ValidationGroup="4">
                                                </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtanswer3" runat="server" MaxLength="60" ValidationGroup="4"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:Button ID="Button5" runat="server" ValidationGroup="4" CssClass="button" Text="Go"
                                                    OnClick="Button5_Click" />
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="pnlfinalsubmit" runat="server" Visible="false">
                        <fieldset>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 25%">
                                    </td>
                                    <td style="width: 25%">
                                        <asp:Button ID="Button2" runat="server" CssClass="button" Text="Submit" OnClick="Button2_Click" />
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="pnlconfirmationmessage" runat="server" Visible="false">
                        <fieldset>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblsecurityquestionconfirmation" runat="server" ForeColor="Red"> </asp:Label>
                                            <asp:Label ID="lblsecurityquestionconfirmationipname" runat="server" ForeColor="Red"> </asp:Label>
                                            <asp:Label ID="lblsecurityquestionconfirmation2" runat="server" ForeColor="Red"> </asp:Label>
                                            <asp:LinkButton ID="LinkButton2" runat="server" Text="Please try to login again."
                                                OnClick="LinkButton2_Click" Visible="false"> </asp:LinkButton>
                                            <a href="http://onlineaccounts.net/Shoppingcart/Admin/Shoppingcartlogin.aspx">Please
                                                try to login again.</a>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </fieldset>
            </asp:Panel>
            <asp:Panel ID="pnlradio2option3" runat="server" Visible="false">
                <fieldset>
                    <asp:Panel ID="pnllicensekeyverificationuserid" runat="server">
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label16" runat="server" Text="Please verify yourself as an admin user of the company in order to add the IP address">
                                </asp:Label>
                                <asp:Label ID="lbllicensekeyipaddress" runat="server">
                                </asp:Label>
                                <asp:Label ID="Label18" runat="server" Text="to the list of allowed IP addresses for the whole company.">
                                </asp:Label>
                            </legend>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="4">
                                        <label>
                                            <asp:Label ID="lbllicensekeyverification" runat="server" ForeColor="Red">
                                            </asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label10" runat="server" Text="Company ID">
                                            </asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtlicensekeyverificationcid"
                                                ErrorMessage="*" ValidationGroup="5">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtlicensekeyverificationcid"
                                                ValidationGroup="5">
                                            </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td style="width: 75%" colspan="3">
                                        <asp:TextBox ID="txtlicensekeyverificationcid" MaxLength="50" Width="250px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label13" runat="server" Text="User ID">
                                            </asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtlicensekeyverificationuid"
                                                ErrorMessage="*" ValidationGroup="5">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([_@.a-zA-Z0-9\s]*)" ControlToValidate="txtlicensekeyverificationuid"
                                                ValidationGroup="5">
                                            </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtlicensekeyverificationuid" Width="250px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label15" runat="server" Text="Password">
                                            </asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtlicensekeyverificationpwd"
                                                ErrorMessage="*" ValidationGroup="5">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([_#$&*-+=@.a-zA-Z0-9\s]*)" ControlToValidate="txtlicensekeyverificationpwd"
                                                ValidationGroup="5">
                                            </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtlicensekeyverificationpwd" runat="server" Width="250px" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button6" runat="server" CssClass="button" ValidationGroup="5" Text="Confirm"
                                            OnClick="Button6_Click" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="pnllicensekeynameoption" runat="server" Visible="false">
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label17" runat="server" Text=" Please provide the license key you received at the time of registration of your company. ">
                                </asp:Label>
                            </legend>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label7" runat="server" Text="License Key">
                                            </asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtlicensekey"
                                                ErrorMessage="*" ValidationGroup="6">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([_#$&*-+=@.a-zA-Z0-9\s]*)" ControlToValidate="txtlicensekeyverificationpwd"
                                                ValidationGroup="6">
                                            </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td style="width: 75%" colspan="3">
                                        <asp:TextBox ID="txtlicensekey" ValidationGroup="6" runat="server" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button7" runat="server" ValidationGroup="6" CssClass="button" Text="Submit"
                                            OnClick="Button7_Click" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="pnlconfirmationmessagelicense" runat="server" Visible="false">
                        <fieldset>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="lbllicenseconfirmation123" runat="server" ForeColor="Red"> </asp:Label>
                                            <asp:Label ID="lbllicenseconfirmation123456" runat="server" ForeColor="Red"> </asp:Label>
                                            <asp:Label ID="lbllicenseconfirmation123456789" runat="server" ForeColor="Red"> </asp:Label>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text="Please try to login again."
                                                OnClick="LinkButton1_Click" Visible="false"> </asp:LinkButton>
                                            <a href="http://onlineaccounts.net/Shoppingcart/Admin/Shoppingcartlogin.aspx">Please
                                                try to login again.</a>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </fieldset>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
