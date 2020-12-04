<%@ Page Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true"
    CodeFile="ordernowTest.aspx.cs" Inherits="New_Account_SignUp_Form" Title="New Account SignUp Form" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script language="javascript" type="text/javascript">

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
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="right_content">
        <%--<h2>
            New Account Sign Up Form</h2>--%>
        <h2>
            New Company Sign Up Form</h2>
        <div class="products_box">
            <fieldset>
                <legend>Plan Detail </legend>
                <table width="100%">
                    <tr>
                        <td style="width: 25%">
                            <label>
                                Portal-Product/Version Name
                            </label>
                        </td>
                        <td style="width: 50%" colspan="2">
                            <label>
                                <asp:Label runat="server" ForeColor="#5badff" ID="Label4" Text="">
                                </asp:Label>
                                <asp:Label runat="server" ForeColor="#5badff" ID="lblproductId" Text="">
                                </asp:Label>
                                <asp:Label ID="ddlVersion" Text="test" ForeColor="#5badff" runat="server"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 25%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <label>
                                Subscription Plan
                            </label>
                        </td>
                        <td style="width: 50%" colspan="2">
                            <label>
                                <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="#5badff" OnClick="LinkButton1_Click"> </asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" ForeColor="#999999" runat="server" Font-Italic="true"
                                    OnClick="LinkButton2_Click">Change Plan</asp:LinkButton>
                            </label>
                        </td>
                        <td style="width: 25%">
                            <label>
                                <asp:Label ID="lblPricePlanId" runat="server" Visible="False"></asp:Label>
                            </label>
                            <label for="lastName1">
                                <a href="" runat="server" visible="false" target="_blank" id="ibtnPlanINfo" style="font-size: 12px">
                                    View more info.</a>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Amount
                            </label>
                        </td>
                        <td colspan="2">
                            <label>
                                <asp:Label ID="Label2" ForeColor="#5badff" runat="server" Text="$">
                                </asp:Label>
                                <asp:Label ID="lblamt" ForeColor="#5badff" runat="server" Text="">
                                </asp:Label>
                            </label>
                        </td>
                        <td>
                        
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Payment Mode
                            </label>
                        </td>
                        <td colspan="2">
                            <label>
                                 <asp:DropDownList ID="drppaymode" runat="server">
                                </asp:DropDownList>
                            </label>
                        </td>
                        <td>
                        
                               
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>Company Information </legend>
                <table width="100%">
                    <tr>
                        <td style="width: 25%">
                            <label>
                                Company Name
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtcompanyname"
                                    ErrorMessage="*" ValidationGroup="1">
                                </asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" ID="txtcompanyname" MaxLength="20" Text=""></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                    TargetControlID="txtcompanyname" ValidChars="0147852369_. abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                </cc1:FilteredTextBoxExtender>
                            </label>
                        </td>
                        <td style="width: 25%">
                            <label>
                                Contact Person Name
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcontactprsn"
                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtcontactprsn" runat="server" MaxLength="50" ValidationGroup="1"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                    TargetControlID="txtcontactprsn" ValidChars="0147852369_. abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                </cc1:FilteredTextBoxExtender>
                            </label>
                        </td>
                        <td style="width: 25%">
                            <label>
                                Contact Person Designation
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcontactprsndsg"
                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtcontactprsndsg" MaxLength="50" runat="server" ValidationGroup="1"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                    TargetControlID="txtcontactprsndsg" ValidChars="0147852369_. abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                </cc1:FilteredTextBoxExtender>
                            </label>
                        </td>
                        <td style="width: 25%">
                            <label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic"
                                    ControlToValidate="txtemail" ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    Style="font-size: 14px"></asp:RegularExpressionValidator>
                                Email
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtemail"
                                    ErrorMessage="*" ValidationGroup="1">
                                </asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtemail" MaxLength="100" runat="server" CausesValidation="True"
                                    AutoPostBack="True" OnTextChanged="txtemail_TextChanged">
                                </asp:TextBox>
                                <asp:Label ID="lblmsg" runat="server" ForeColor="#009900" Visible="False">
                                </asp:Label>
                            </label>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>Contact Information </legend>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="4">
                            <label class="first">
                                Address
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtadd"
                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtadd" MaxLength="500" runat="server" CssClass="txtInputLarge">
                                </asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                    TargetControlID="txtadd" ValidChars="0147852369_.-/ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                </cc1:FilteredTextBoxExtender>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <label>
                                Country
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlcountry"
                                    ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="True" DataTextField="CountryName"
                                    DataValueField="CountryId" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                        <td style="width: 25%">
                            <label for="state1">
                                State
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlstate"
                                    ErrorMessage="*" InitialValue="0" ValidationGroup="1">
                                </asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlstate" runat="server">
                                </asp:DropDownList>
                            </label>
                        </td>
                        <td style="width: 25%">
                            <label>
                                City
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtcity"
                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtcity" MaxLength="100" runat="server">
                                </asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                    TargetControlID="txtcity" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                </cc1:FilteredTextBoxExtender>
                            </label>
                        </td>
                        <td style="width: 25%">
                            <label>
                                Zip Code
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtzipcode"
                                    ErrorMessage="*" ValidationGroup="1">
                                </asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtzipcode" MaxLength="30" Width="150px" runat="server"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                    TargetControlID="txtzipcode" ValidChars="0147852369_ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                </cc1:FilteredTextBoxExtender>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <label>
                                Country
                                <br />
                                Code &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Phone No.
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtphn"
                                    ErrorMessage="*" ValidationGroup="1">
                                </asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtcccode"
                                    ErrorMessage="*" ValidationGroup="1">
                                </asp:RequiredFieldValidator>
                                <div class="formSpan">
                                    <asp:TextBox ID="txtcccode" runat="server" MaxLength="5" CssClass="txtInputSmall">
                                    </asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                        TargetControlID="txtcccode" ValidChars="0147852369+">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtphn" runat="server" MaxLength="15" CssClass="txtInputMed"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                        TargetControlID="txtphn" ValidChars="0147852369">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </label>
                        </td>
                        <td style="width: 25%">
                            <label>
                                Carrier &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Mobile No.
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" Display="Dynamic" runat="server"
                                    ControlToValidate="txtmobileno" ErrorMessage="*" ValidationGroup="1">
                                </asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                    TargetControlID="txtmobileno" ValidChars="0147852369">
                                </cc1:FilteredTextBoxExtender>
                                <div class="formSpan">
                                    <asp:DropDownList ID="ddlcarriername" CssClass="txtInputSmall" runat="server">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtmobileno" CssClass="txtInputMed" runat="server" MaxLength="13"></asp:TextBox>
                                </div>
                            </label>
                        </td>
                        <td style="width: 25%">
                            <label>
                                Fax
                                <asp:TextBox ID="txtfax" runat="server" Width="150px" MaxLength="15">
                                </asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                    TargetControlID="txtfax" ValidChars="0147852369">
                                </cc1:FilteredTextBoxExtender>
                            </label>
                        </td>
                        <td style="width: 25%">
                        </td>
                    </tr>
                </table>
            </fieldset>
            <asp:Panel ID="pnlpayo" runat="server" Width="100%" Visible="false">
                <fieldset>
                    <legend>Deposite Information</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 25%">
                                <label>
                                    <asp:Label Text="Free Initial Orders Worth $" runat="server" ID="Label1">
                                    </asp:Label>
                                    <br />
                                    <asp:Label runat="server" ForeColor="Green" ID="lblfreeiorder">
                                    </asp:Label>
                                </label>
                            </td>
                            <td style="width: 25%">
                                <label>
                                    <asp:Label Text=" Minimum Deposite Required" runat="server" ID="Label3">
                                    </asp:Label>
                                    <asp:TextBox ID="txtdepo" runat="server" AutoPostBack="True" OnTextChanged="txtdepo_TextChanged">
                                    </asp:TextBox>
                                </label>
                            </td>
                            <td style="width: 25%">
                                <label>
                                    <asp:Label ForeColor="Green" ID="lblminidep" runat="server"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 25%">
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <fieldset>
                <legend>Set Your Login Information</legend>
                <table width="100%">
                    <tr>
                        <td style="width: 25%">
                            <label class="first" for="title1">
                                Company ID (Alpha-numeric A-Z, 0-9)
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display="Dynamic" runat="server"
                                    ControlToValidate="txtcompanyid" ErrorMessage="*" ValidationGroup="1">
                                </asp:RequiredFieldValidator>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtcompanyid" runat="server" MaxLength="30" OnTextChanged="txtcompanyid_TextChanged"
                                            AutoPostBack="True">
                                        </asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                            TargetControlID="txtcompanyid" ValidChars="0147852369_abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="lblCompanyIDAVl" runat="server"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </label>
                        </td>
                        <td style="width: 25%">
                            <label for="firstName1">
                                Admin User ID
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" MaxLength="100" runat="server"
                                    ControlToValidate="txtadminuserid" ErrorMessage="*" ValidationGroup="1">
                                </asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtadminuserid" runat="server" ValidationGroup="1">
                                </asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                    TargetControlID="txtadminuserid" ValidChars="0147852369_.@abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                </cc1:FilteredTextBoxExtender>
                            </label>
                        </td>
                        <td style="width: 25%">
                            <label for="firstName1">
                                Password
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtpassword"
                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <br />
                                <asp:TextBox ID="txtpassword" runat="server" Width="200px" MaxLength="100" TextMode="Password"> </asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                    TargetControlID="txtpassword" ValidChars="0147852369_.@$&%-+abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                </cc1:FilteredTextBoxExtender>
                            </label>
                        </td>
                        <td style="width: 25%">
                            <label for="firstName1">
                                Confirm Password
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtcnfrmpassword"
                                    ErrorMessage="*" ValidationGroup="1">
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtpassword"
                                    ControlToValidate="txtcnfrmpassword" ErrorMessage="Enter Same Password" ValidationGroup="1"></asp:CompareValidator>
                                <br />
                                <asp:TextBox ID="txtcnfrmpassword" runat="server" Width="200px" MaxLength="100" TextMode="Password"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" Enabled="True"
                                    TargetControlID="txtcnfrmpassword" ValidChars="0147852369_.@$&%-+abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                </cc1:FilteredTextBoxExtender>
                            </label>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td style="width: 35%">
                            <img src="Captcha.aspx" width="300px" height="50px" alt="Captcha.aspx" />
                        </td>
                        <td style="width: 25%">
                            <label for="captcha">
                                <br />
                                Captcha Code
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCaptcha"
                                    ErrorMessage="*" ValidationGroup="1">
                                </asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtCaptcha" runat="server"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                    TargetControlID="txtcnfrmpassword" ValidChars="0147852369_. abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                </cc1:FilteredTextBoxExtender>
                                <asp:Label ID="lblchapmsg" runat="server" ForeColor="Red" Text="">
                                </asp:Label>
                            </label>
                        </td>
                        <td style="width: 40%">
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>Terms and Condition</legend>
                <label>
                    <asp:Panel runat="server" ScrollBars="Auto" Height="0px" ID="pnlTermsCondition">
                        <asp:Label ID="lbltermsofuse" runat="server"></asp:Label>
                    </asp:Panel>
                </label>
                <div style="clear: both;">
                </div>
                <div class="chkBox">
                    <%-- <asp:CheckBox runat="server" ID="chkAccept" TextAlign="Right" Text=" I Accept the Terms and Condition" />--%>
                    <asp:CheckBox ID="Chk" runat="server" Text="I Accept the Terms and Condition"></asp:CheckBox>
                    <asp:Button ID="btnCheckCompany" runat="server" Text="Check Availability" Visible="false"
                        OnClick="btnCheckCompany_Click"></asp:Button>
                    <asp:Button ID="Button1" runat="server" Text="Availability" ValidationGroup="5" Visible="false"
                        OnClick="Button1_Click" />
                </div>
                <div style="clear: both;">
                </div>
                <label>
                   <%-- On Clicking "Confirm Order" button you will be redirected for payment through paypal--%>
                    Upon clicking "Confirm Order" you will be redirected to PayPal to process your payment
                </label>
                <div style="clear: both;">
                </div>
                <label>
                    <%-- <asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="btnConfirm"
                        CssClass="btnSubmit" Text="Confirm Order" />--%>
                    <asp:Button ID="btncontinue" runat="server" Text="Confirm Order" OnClick="btncontinue_Click"
                        CssClass="btnSubmit" ValidationGroup="1" />
                    <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" CssClass="btnSubmit"
                        Text="Reset" />
                </label>
                <label>
                    <%--<asp:Button runat="server" ID="Button1" CssClass="btnSubmit" Text="Cancel" />--%>
                </label>
                <div style="clear: both;">
                </div>
                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                    <ProgressTemplate>
                        <div style="border-right: #946702 1px solid; border-top: #946702 1px solid; left: 45%;
                            visibility: visible; vertical-align: middle; border-left: #946702 1px solid;
                            width: 50px; border-bottom: #946702 1px solid; position: absolute; top: 65%;
                            height: 20px; background-color: #ffe29f" id="IMGDIV" align="center" runat="server"
                            valign="middle">
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image11" runat="server" ImageUrl="~/images/preloader.gif" Width="50px"
                                                Height="20px" Visible="false"></asp:Image>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblprogress" runat="server" ForeColor="#946702" Text="Please Wait"
                                                Font-Bold="True" Font-Size="16px" Font-Names="Arial" Width="50px" Height="20px"
                                                Visible="false"></asp:Label><br />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <input id="hdnProductId" name="hdnProductId" runat="server" type="hidden" />
            </fieldset>
            <div style="clear: both;">
            </div>
            <asp:TextBox ID="Textpaypal" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="Textcancel" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="Textreturn" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="Textnotify" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtpaymentnotifyurl" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txthostedsite" runat="server" Width="154px" Visible="false"></asp:TextBox>
        </div>
        <!--end of right content-->
    </div>

         <div style="position: fixed;bottom: 0; right:20px;">
              <asp:Label ID="lblVersion" runat="server" Text="V5" ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>
</asp:Content>
