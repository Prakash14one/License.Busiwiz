<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ClientInfo.aspx.cs" Inherits="ClientInfo" Title="Company Registretion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
    <%@ register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
        namespace="System.Web.UI" tagprefix="asp" %>

    <script type="text/javascript" language="javascript">
        function ValidateText(i) {
            if (i.value.length > 0) {
                i.value = i.value.replace(/[^\d]+/g, '');
            }
        }


    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" id="pagetbl" border="0" align="center" style="width: 959px;
        background-color: #EFEFEF;">
        <tr>
            <td class="column1" style="width: 169px">
                &nbsp;
            </td>
            <td class="column2" style="width: 147px">
                &nbsp;
            </td>
            <td class="column1">
                &nbsp;
            </td>
            <td class="column2" style="width: 215px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" class="hdng">
                <asp:Label ID="lblHead" runat="server" Text="New IT Company Registration"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="column1" colspan="4">
                &nbsp; Please enter your company information to start your account with busiwiz.com.&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                 <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
          <tr>
            <td class="column1" colspan="4">
                <br />
            </td>
        </tr>
        <tr>
            <td class="column1" colspan="4" align="center">
                <table bgcolor="#EFEFEF" style="width: 900Px; background-color: #E2E2D8;" align="center">
                    <tr>
                        <td class="column1" style="width: 158px; height: 21px; font-size: 14px; color: #006699;">
                            &nbsp;<span style="text-decoration: underline">Basic Information</span>
                        </td>
                        <td class="column2" style="height: 21px; width: 147px">
                        </td>
                        <td class="column1" style="height: 21px; width: 122px;">
                        </td>
                        <td class="column2" style="height: 21px; width: 164px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 158px; height: 22px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name of Company :
                        </td>
                        <td class="column2" colspan="2" style="color: green; height: 22px;">
                            <asp:TextBox ID="txtCompanyName" runat="server" Width="135px"></asp:TextBox>
                            *
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCompanyName"
                                ErrorMessage="*" ValidationGroup="1">*</asp:RequiredFieldValidator>&nbsp;
                        </td>
                        <td class="column2" style="height: 22px; width: 164px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 158px; height: 20px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Login User Name :
                        </td>
                        <td class="column2" style="width: 147px; height: 20px">
                            <asp:TextBox ID="txtLoginName" runat="server" Width="133px" Height="16px"></asp:TextBox>
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtLoginName"
                                ErrorMessage="*" ValidationGroup="1" Display="None">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="column1" style="height: 20px; width: 122px;">
                        </td>
                        <td class="column2" style="height: 20px; width: 164px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 158px; height: 20px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Login Password :
                        </td>
                        <td class="column2" style="width: 147px; height: 20px">
                            <asp:TextBox ID="txtLoginPassword" runat="server" TextMode="Password" Width="133px"></asp:TextBox>
                            <font color="green">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtLoginPassword"
                                ErrorMessage="*" ValidationGroup="1" Width="4px"></asp:RequiredFieldValidator>
                        </td>
                        <td class="column1" style="height: 20px; width: 122px;">
                        </td>
                        <td class="column2" style="width: 164px; height: 20px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 158px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Confirm Password&nbsp; :
                        </td>
                        <td class="column2" colspan="3" style="height: 24px">
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Width="133px"></asp:TextBox>
                            *
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtConfirmPassword"
                                ErrorMessage="*" ValidationGroup="1">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtLoginPassword"
                                ControlToValidate="txtConfirmPassword" ErrorMessage="Enter Same Password"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 158px; height: 51px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Address1 :
                        </td>
                        <td class="column2" style="height: 51px">
                            <asp:TextBox ID="txtAdrs1" runat="server" TextMode="MultiLine" Width="133px" Height="45px"></asp:TextBox>
                            *
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAdrs1"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                        <td class="column1" style="height: 51px; width: 122px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Address2 :
                        </td>
                        <td class="column2" style="width: 164px; height: 51px;">
                            <asp:TextBox ID="txtAdrs2" runat="server" TextMode="MultiLine" Width="129px" Height="44px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 158px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Country :
                        </td>
                        <td class="column2" style="height: 24px">
                            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                Width="133px" Height="19px">
                            </asp:DropDownList>
                            *
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlCountry"
                                ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                        <td class="column1" style="width: 122px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; State :
                        </td>
                        <td class="column2" style="height: 24px; width: 164px;">
                            <asp:DropDownList ID="ddlState" runat="server" Width="134px" Height="17px">
                            </asp:DropDownList>
                            *
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlState"
                                ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 158px; height: 21px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; City :
                        </td>
                        <td class="column2" style="height: 21px">
                            <asp:TextBox ID="txtCity" runat="server" Width="133px"></asp:TextBox>
                            *
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCity"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                        <td class="column1" style="height: 21px; width: 122px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Zip Code :
                        </td>
                        <td class="column2" style="height: 21px; width: 164px;">
                            <asp:TextBox ID="txtZipcode" runat="server" Width="133px"></asp:TextBox>
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtZipcode"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 158px; height: 9px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name of Contact Person :
                        </td>
                        <td class="column2" style="height: 9px">
                            <asp:TextBox ID="txtContactPerson" runat="server" Width="133px"></asp:TextBox>
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtContactPerson"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                        <td class="column1" style="height: 9px; width: 122px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Company Website :
                        </td>
                        <td class="column2" style="height: 9px; width: 164px;">
                            <asp:TextBox ID="txtClientUrl" runat="server" Width="133px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 158px; height: 21px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Phone1 :
                        </td>
                        <td class="column2" style="height: 21px">
                            <asp:TextBox ID="txtPhone1" runat="server" onkeyup="return ValidateText(this)" Width="133px"></asp:TextBox>
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhone1"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                        <td class="column1" style="height: 21px; width: 122px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Phone2 :
                        </td>
                        <td class="column2" style="height: 21px; width: 164px;">
                            <asp:TextBox ID="txtPhone2" runat="server" onkeyup="return ValidateText(this)" Width="133px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 158px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fax1 :
                        </td>
                        <td class="column2" style="height: 24px">
                            <asp:TextBox ID="txtFax1" runat="server" onkeyup="return ValidateText(this)" Width="133px"></asp:TextBox>
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFax1"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                        <td class="column1" style="width: 122px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fax2 :
                        </td>
                        <td class="column2" style="height: 24px; width: 164px;">
                            <asp:TextBox ID="txtFax2" runat="server" onkeyup="return ValidateText(this)" Width="133px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 158px; height: 18px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Email1 :
                        </td>
                        <td class="column2" style="height: 18px">
                            <asp:TextBox ID="txtEmail1" runat="server" Width="133px"></asp:TextBox>
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEmail1"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                        <td class="column1" style="height: 18px; width: 122px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Email2 :
                        </td>
                        <td class="column2" style="height: 18px; width: 164px;">
                            <asp:TextBox ID="txtEmail2" runat="server" Width="133px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="column1" style="width: 169px">
            </td>
            <td class="column2" style="height: 24px">
            </td>
            <td class="column1">
            </td>
            <td class="column2" style="width: 215px">
            </td>
        </tr>
        <tr>
            <td class="column1" colspan="4" align="center">
                <table bgcolor="#EFEFEF" style="width: 900Px; background-color: #E2E2D8;" align="center">
                    <tr>
                        <td class="column1" style="width: 158px; height: 21px; font-size: 14px; color: #006699;">
                            &nbsp;<span style="text-decoration: underline">Subscription Information</span>
                        </td>
                        <td class="column2" style="height: 21px; width: 147px">
                        </td>
                        <td class="column1" style="height: 21px; width: 122px;">
                        </td>
                        <td class="column2" style="height: 21px; width: 164px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 169px; height: 7px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Subscription Plan :
                        </td>
                        <td class="column2" style="height: 7px">
                            <asp:DropDownList ID="ddlsubscriptionplan" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsubscriptionplan_SelectedIndexChanged"
                                Width="200px" Height="17px">
                                <asp:ListItem>--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="column2" style="height: 7px">
                            *
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlsubscriptionplan"
                                ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <a href="www.ifilecabinet.com"><span style="color: #0000ff; text-decoration: underline">
                                View more info.</span></a>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 169px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Amount :
                        </td>
                        <td class="column2" style="height: 24px">
                            <asp:Label ID="lblamt" runat="server" Font-Bold="False" ForeColor="#C00000"></asp:Label>
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" ForeColor="#C00000" Text="$"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="column1" style="width: 169px">
            </td>
            <td class="column2" style="height: 24px">
            </td>
            <td class="column1">
            </td>
            <td class="column2" style="width: 215px">
            </td>
        </tr>
        <tr>
            <td class="column1" colspan="4" align="center">
                <table bgcolor="#EFEFEF" style="width: 900Px; background-color: #E2E2D8;" align="center">
                    <tr>
                        <td class="column1" style="width: 215px; height: 21px; font-size: 14px; color: #006699;">
                            &nbsp;<span style="text-decoration: underline">Mail Server Information</span>
                        </td>
                        <td class="column2" style="height: 21px; width: 147px">
                        </td>
                        <td class="column1" style="height: 21px; width: 144px;">
                        </td>
                        <td class="column2" style="height: 21px; width: 164px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 215px; height: 24px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Master Email ID :
                        </td>
                        <td class="column2" style="height: 24px">
                            <asp:TextBox ID="Txtmastereid" runat="server" Width="133px"></asp:TextBox>
                        </td>
                        <td class="column1" style="height: 9px; width: 144px;">
                            Outgoing Server(SMTP) :
                        </td>
                        <td class="column2" style="height: 9px; width: 215px;">
                            <asp:TextBox ID="txtoutgoingserver" runat="server" Width="133px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 215px; height: 9px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Incoming Server(POP) :
                        </td>
                        <td class="column2" style="height: 9px">
                            <asp:TextBox ID="txtincomingserver" runat="server" Width="133px"></asp:TextBox>
                        </td>
                        <td class="column1" style="height: 9px; width: 144px;">
                            Outgoing Server Port :
                        </td>
                        <td class="column2" style="height: 9px; width: 215px;">
                            <asp:TextBox ID="txtoutgoingserverport" runat="server" Width="133px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 215px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Incoming Server UserID :
                        </td>
                        <td class="column2" style="height: 24px">
                            <asp:TextBox ID="txtincomingserveruserid" runat="server" Width="133px"></asp:TextBox>
                        </td>
                        <td class="column1" style="width: 144px">
                            Outgoing Server UserID :
                        </td>
                        <td class="column2" style="height: 24px; width: 215px;">
                            <asp:TextBox ID="txtoutgoingserveruserid" runat="server" Width="133px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 215px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Incoming Server Password :
                        </td>
                        <td class="column2" style="height: 24px">
                            <asp:TextBox ID="incomingserverpassword" runat="server" TextMode="Password" Width="133px"></asp:TextBox>
                        </td>
                        <td class="column1" style="width: 144px">
                            Outgoing Server Password :
                        </td>
                        <td class="column2" style="height: 24px; width: 215px;">
                            <asp:TextBox ID="outgoingserverpassword" runat="server" TextMode="Password" Width="133px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="column1" align="center" width="600Px" colspan="4">
                <br />
            </td>
        </tr>
        <tr>
            <td class="column1" align="center" width="600Px" colspan="4">
                <asp:Label ID="lbll1" align="centre" ForeColor="Gray" runat="server" Text="If you wish busiwiz to handle your payments ,Please provide following."
                    Font-Size="Small" Font-Bold="True" Font-Underline="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="column1" align="center" width="600Px" colspan="4">
                <br />
            </td>
        </tr>
        <tr>
            <td class="column1" colspan="4" align="center">
                <table bgcolor="#EFEFEF" style="width: 900Px; background-color: #E2E2D8;" align="center">
                    <tr>
                        <td class="column1" style="height: 21px; font-size: 14px; color: #006699;" colspan="2">
                            &nbsp;<span style="text-decoration: underline">Mail Server Information</span>
                        </td>
                        <td class="column1" style="height: 21px; width: 122px;">
                        </td>
                        <td class="column2" style="height: 21px; width: 152px;">
                        </td>
                    </tr>
                  
                    <tr>
                        <td class="column1" style="width: 210px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Logo URL :
                        </td>
                        <td class="column2" colspan="2" style="height: 24px">
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="164px" />&nbsp;&nbsp;<asp:Button
                                ID="Butsubmit" runat="server" Text="Submit" Width="67px" OnClick="Butsubmit_Click"
                                Height="21px" /><br />
                            <asp:Label ID="lblimg" runat="server" ForeColor="Red">This Logo will be used in your Emails which will be sent to your customers without making payment </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 210px; height: 13px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paypal Email Id :
                        </td>
                        <td class="column2" style="height: 13px" colspan="3">
                            <asp:TextBox ID="Textpaypalid" runat="server" Width="388px"></asp:TextBox>
                             *
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="Textpaypalid"
                                ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 210px">
                            &nbsp;
                        </td>
                        <td class="column2" style="height: 24px" colspan="3">
                            <asp:Label ID="lble" runat="server" ForeColor="#222222" Text="Paypal Email Id where you wish to get the payments which customers deposites."
                                Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 210px; height: 18px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paypal Cancel URL :
                        </td>
                        <td class="column2" style="height: 18px" colspan="3">
                            <asp:TextBox ID="Textcancelurl" runat="server" Width="389px" Style="margin-bottom: 0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 210px; height: 39px;">
                        </td>
                        <td class="column2" style="height: 39px" colspan="3">
                            <asp:Label ID="iblcancel" runat="server" ForeColor="#222222" Text="Ths is the url where the customer would be redirected if he cancels the payment process without making."
                                Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 210px; height: 19px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paypal Notify URL :
                        </td>
                        <td class="column2" style="height: 19px" colspan="3">
                            <asp:TextBox ID="Textnotifyurl" runat="server" Width="391px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 210px; height: 36px;">
                        </td>
                        <td class="column2" style="height: 36px" colspan="3">
                            <asp:Label ID="Labnoti" runat="server" ForeColor="#222222" Text="This is the url where the customers payment information would be sent by Paypal by Instant Payment Notification."
                                Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 210px; height: 18px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paypal Return URL :
                        </td>
                        <td class="column2" style="height: 18px" colspan="3">
                            <asp:TextBox ID="Textreturnurl" runat="server" Width="390px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 210px; height: 38px;">
                        </td>
                        <td class="column2" style="height: 38px" colspan="3">
                            <asp:Label ID="Labrutu" runat="server" ForeColor="#222222" Text="This is the url where the customers would be redirected after he makes successful payment."
                                Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 210px; height: 17px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Your Payment Notify URL :
                        </td>
                        <td class="column2" style="height: 17px" colspan="3">
                            <asp:TextBox ID="Textpaymentnotifyurl" runat="server" Width="391px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column1" style="width: 210px">
                            &nbsp;
                        </td>
                        <td class="column2" style="height: 24px" colspan="3">
                            <asp:Label ID="Labepayre" runat="server" ForeColor="#222222" Text="We will pass complete Paypal  Instant Payment Notification message to this page on successful completion."
                                Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="column1" style="width: 169px">
            </td>
            <td class="column2" style="height: 24px">
            </td>
            <td class="column1">
            </td>
            <td class="column2" style="width: 215px">
            </td>
        </tr>
        <tr>
            <td class="column1" colspan="4" >
                <table bgcolor="#EFEFEF" style="width: 900Px; background-color: #E2E2D8;" align="center">
                    <tr>
                        <td class="column1" style="width: 158px; height: 21px; font-size: 14px; color: #006699;">
                            &nbsp;<span style="text-decoration: underline">SQL Server Information</span>
                        </td>
                        <td class="column2" style="height: 21px; width: 147px">
                        </td>
                        <td class="column1" style="height: 21px; width: 122px;">
                        </td>
                        <td class="column2" style="height: 21px; width: 164px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center" style="height: 33px">
                            &nbsp;&nbsp;<asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            &nbsp;&nbsp;&nbsp;<asp:Label ID="lbllbl" runat="server" ForeColor="#222222" Text="Please give the information about the sql server where the product would be run"></asp:Label>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlservername" runat="server" Visible="false">
                        <tr>
                            <td class="column1" style="height: 26px">
                                Server Name :
                            </td>
                            <td class="column2" style="height: 24px">
                                <asp:TextBox ID="txtservername" runat="server" Width="133px"></asp:TextBox>
                            </td>
                            <td class="column1" style="height: 26px">
                            </td>
                            <td class="column2" style="height: 24px">
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </td>
        </tr>
        <tr>
            <td class="column1" style="width: 169px">
            </td>
            <td class="column2" style="height: 24px">
            </td>
            <td class="column1">
            </td>
            <td class="column2" style="width: 215px">
            </td>
        </tr>
        <tr>
            <td class="column1" style="width: 169px">
            </td>
            <td class="column2" style="height: 24px">
            </td>
            <td class="column1">
            </td>
            <td class="column2" style="width: 215px">
            </td>
        </tr>
        <tr>
          
            <td  align="center" style="height: 24px" colspan="4" >
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btnSubmit" runat="server"
                    OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="1" />
                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                    ValidationGroup="1" />
            </td>
           
        </tr>
        <tr>
            <td class="column1" style="width: 169px">
            <asp:Panel ID="pnl_CompanyReg" runat="server" Visible="false">
            <asp:TextBox ID="txtcompanyid" runat="server" Visible="false"></asp:TextBox>
               <asp:Label ID="lblPricePlanId" runat="server" Text=""></asp:Label>
               </asp:Panel> 
                 <asp:Label runat="server" ForeColor="#5badff" ID="Label4" Text="">
                                </asp:Label>
                                <asp:Label runat="server" ForeColor="#5badff" ID="lblproductId" Text="">
                                </asp:Label>
                                <asp:Label ID="ddlVersion" Text="test" ForeColor="#5badff" runat="server"></asp:Label>
                               <asp:Label runat="server" ForeColor="Green" ID="lblfreeiorder"></asp:Label>
                                 <asp:Label ForeColor="Green" ID="lblminidep" runat="server"></asp:Label>
                                 <input id="hdnProductId" name="hdnProductId" runat="server" type="hidden" />

                                   <asp:TextBox ID="Textpaypal" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="Textcancel" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="Textreturn" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="Textnotify" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtpaymentnotifyurl" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txthostedsite" runat="server" Width="154px" Visible="false"></asp:TextBox>
            </td>
            <td class="column2" style="height: 24px">
              
            </td>
            <td class="column1">
            </td>
            <td class="column2" style="width: 215px">
            </td>
        </tr>
    </table>
</asp:Content>
