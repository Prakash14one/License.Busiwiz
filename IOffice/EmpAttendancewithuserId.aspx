<%@ Page Language="C#" MasterPageFile="~/IOffice/AttendenceMaster/Main.master" AutoEventWireup="true"
    CodeFile="EmpAttendancewithuserId.aspx.cs" Inherits="productprofile" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="right_content" class="columns_contents_Inner">
        <h2>
            Attendance by User ID and Password
        </h2>
        <div class="products_box">
            <fieldset style="padding: 0 0% 0 28%">
                <%--    <fieldset style="padding: 0 20% 0 40%">--%>
                <table style="width: 100%; height: 100%;">
                    <tr>
                        <td>
                            <label class="first" for="title1">
                                <asp:Label ID="Label24" runat="server" Text="Time Zone"></asp:Label>
                            </label>
                            <label>
                                <asp:DropDownList ID="ddltimezone" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltimezone_SelectedIndexChanged"
                                    BackColor="LightGray" CssClass="ddList">
                                </asp:DropDownList>
                                <asp:Label ID="lbldt" runat="server"></asp:Label>
                                <asp:Label ID="lbltime" runat="server"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="lastName1">
                                <asp:Label ID="Label3" runat="server" Text="Current Date"></asp:Label>
                                <asp:Label ID="Label1date" runat="server" Font-Names="Verdana" Font-Size="14px"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="text-align: center; font-size: 50px; border: 2px solid #416271; padding: 10px;
                                width: 80%; vertical-align: text-top; border: solid 2 black !important;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" RenderMode="Block">
                                    <ContentTemplate>
                                        <asp:Timer ID="TimerTime" runat="server" Enabled="false" Interval="1000" OnTick="TimerTime_Tick">
                                        </asp:Timer>
                                        <asp:Label ID="time22" Visible="false" runat="server" Font-Bold="True"></asp:Label>
                                        <asp:Label ID="lblhour" runat="server"></asp:Label>
                                        <asp:Label ID="lblmin" runat="server"></asp:Label>
                                        <asp:Label ID="lblsec" runat="server"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="TimerTime" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="lbluserid" runat="server"></asp:Label>
                                <asp:Label ID="lbldtate" runat="server"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                         <%--   <div style="text-align: left;">
                                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                            </div>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="stylized" class="myform">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 35%">
                                            <label>
                                                <asp:Label ID="Label1" runat="server" Text="Company ID"></asp:Label><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtcompanyid"
                                                    ErrorMessage="*" ValidationGroup="1" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtcompanyid" runat="server" TabIndex="1" BorderWidth="1px" Width="200px"></asp:TextBox>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label2" runat="server" Text="User ID"></asp:Label><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtuname" ErrorMessage="*"
                                                    ValidationGroup="1" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtuname" runat="server" TabIndex="2" BorderWidth="1px" Width="200px"></asp:TextBox>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label5" runat="server" Text="Password"></asp:Label><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtpass" ErrorMessage="*"
                                                    ValidationGroup="1" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtpass" runat="server" TextMode="Password" TabIndex="3" BorderWidth="1px"
                                                    Width="200px"></asp:TextBox>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnempcode" runat="server" Text="Go" ValidationGroup="1" OnClick="btnempcode_Click"
                                                Width="40" BackColor="White" Font-Names="Verdana" ForeColor="Black" Font-Bold="true" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 16px">
                            <div style="text-align: left;">
                                <asp:Label ID="lblentry" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div style="clear: both;">
            </div>
            <table width="850px">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel5" runat="server" CssClass="modalPopup" BackColor="#d1cec5" Width="600px"
                            BorderWidth="10px">
                            <table cellpadding="0" cellspacing="0" style="width: 600px">
                                <tr>
                                    <td align="right">
                                        <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                            Width="16px" OnClick="ImageButton4_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label12" runat="server" Text="Welcome To Office !!!" ForeColor="#336699"
                                            Font-Bold="True" Font-Size="16px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblm" runat="server" ForeColor="#336699"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label11" runat="server" Text="Your Attandance is successfully entered."
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblempnamemsg" runat="server" ForeColor="Black" Text="Employee Name :"></asp:Label>
                                        <asp:Label ID="lblempname" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lbletimemsg" runat="server" Text="Your entry time is :" ForeColor="Black"></asp:Label>
                                        <asp:Label ID="Label7" runat="server" Text="" ForeColor="Black"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label13" runat="server" Text="You are early by :" ForeColor="Black"> </asp:Label>
                                        <asp:Label ID="Label14" runat="server" Text="" ForeColor="Black"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label8" runat="server" Text="You are not allowed to enter or exit,Kindly see your supervisor"
                                            Visible="False" ForeColor="Black"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lbllate" runat="server" Text="You are late, please meet your supervisor immediately to input the reason for being late ."
                                            Visible="False" ForeColor="Black"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label9" runat="server" Text="You need authorisation from your supervisor."
                                            Visible="False" ForeColor="Black"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label10" runat="server" Text="Your last exit time was :" ForeColor="Black"></asp:Label>
                                        <asp:Label ID="lbllastexittime" runat="server" Text="" ForeColor="Black"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="HiddenButton2221" runat="server" Style="display: none" />
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel5" TargetControlID="HiddenButton2221">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" BackColor="#d1cec5" Width="600px"
                            BorderWidth="10px">
                            <table cellpadding="0" cellspacing="0" style="width: 600px">
                                <tr>
                                    <td align="right">
                                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                            Width="16px" OnClick="ImageButton1_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label17" runat="server" Text="Bye Bye !!!" ForeColor="#336699" Font-Bold="True"
                                            Font-Size="16px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="labbb" runat="server" Text="Your exit is  successfully entered." ForeColor="Black"></asp:Label>
                                        <asp:Label ID="lblgoemp" runat="server" Text="" ForeColor="Black"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label15" runat="server" ForeColor="Black" Text="Your exit time is :"></asp:Label>
                                        <asp:Label ID="lblexittime" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label16" runat="server" Text="You are early by :" ForeColor="Black"> </asp:Label>
                                        <asp:Label ID="lblouterly" runat="server" Text="" ForeColor="Black"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label19" runat="server" Text="You are not allowed to enter or exit,Kindly see your supervisor."
                                            Visible="False" ForeColor="Black"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label18" runat="server" Text="You are going early, please meet with your supervisior immediately to input the reason for going early."
                                            Visible="False" ForeColor="Black"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label20" runat="server" Text="You need authorisation from your supervisor. Please meet with your supervisor immediately."
                                            Visible="False" ForeColor="Black"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label21" runat="server" Text="Your last entry time was :" ForeColor="Black"> </asp:Label>
                                        <asp:Label ID="Label22" runat="server" Text="" ForeColor="Black"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="Button2" runat="server" Style="display: none" />
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel3" TargetControlID="Button2">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Timer ID="timer1" runat="server" Interval="15000" OnTick="timer1_Tick" Enabled="False">
                        </asp:Timer>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Panel ID="Panel6" runat="server" CssClass="modalPopup" BorderWidth="10px" BackColor="#d1cec5"
                            Width="600px">
                            <table cellpadding="2" cellspacing="2" align="center" style="width: 595px">
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lbllatemessagereason" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Label ID="Label41" runat="server" ForeColor="Red" Text="Alert !    "></asp:Label>
                                        <asp:Label ID="Label42" runat="server" ForeColor="Red" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%">
                                        <label>
                                            <asp:Label ID="Label56" runat="server" Font-Bold="false" Text="Current Time" Font-Names="Verdana"
                                                Font-Size="12px"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label43" runat="server" Text="" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%">
                                        <label>
                                            <asp:Label ID="Label57" runat="server" Font-Bold="false" Text="Your Scheduled Entry time is"
                                                Font-Names="Verdana" Font-Size="12px"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label44" runat="server" Text="" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            <asp:Label ID="Label45" runat="server" Font-Bold="false" Text="During this pay period , You are late <"></asp:Label>
                                            <asp:Label ID="Label46" runat="server" Text="" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                                            <asp:Label ID="Label47" runat="server" Font-Bold="false" Text="> Times."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 45%">
                                        <label>
                                            <asp:Label ID="Label27" runat="server" Font-Bold="false" Text="Please give the reason for being late : "></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="latereaso" runat="server" Text="" Height="100px" MaxLength="500"
                                            BorderWidth="1px" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rqsd" runat="server" ControlToValidate="latereaso"
                                            ErrorMessage="*" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            <asp:Label ID="Label28" runat="server" Font-Bold="false" Text="You need to take approval from your Attendance Admin <"></asp:Label>
                                            <asp:Label ID="Label48" runat="server" Text="" ForeColor="Black" Font-Names="Verdana"
                                                Font-Size="12px"></asp:Label>
                                            <asp:Label ID="Label49" runat="server" Font-Bold="false" Text="> for recording attendance."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            <asp:Label ID="Label50" Visible="false" runat="server" Font-Bold="false" Text="Please note, If you continue being late, you may be restricted to register your attendance."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            <asp:Label ID="Label51" Visible="false" runat="server" Font-Bold="false" Text="Your check in /check out time has been recorded, however, please contact your supervisor regarding your attendance."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnaddlater" runat="server" Text="Submit" OnClick="btnaddlater_Click"
                                            CssClass="btnSubmit" ValidationGroup="5" BorderWidth="1px" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="Button6" runat="server" Style="display: none" />
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel6" TargetControlID="Button6">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" BorderWidth="5px" BackColor="#d1cec5"
                            Width="467px">
                            <table cellpadding="0" cellspacing="0" align="center" style="width: 460px">
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lblmsggg" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="lblsumsg" runat="server" Text="Your OnlineAccounts Attendance system is blocked  for 3 wrong attempts"
                                            ForeColor="Black" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label6" runat="server" Text="Supervisor Name : " ForeColor="Black"
                                            Visible="False"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblsupname" runat="server" ForeColor="Black" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="Label23" runat="server" Text="Please ask your Onlineaccounts admin to override the attendance system block"
                                            ForeColor="Black" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblu" runat="server" Text="User Id  :" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtuerlog" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="2a"
                                            ControlToValidate="txtuerlog" SetFocusOnError="true" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblp" runat="server" Text="Password  :" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="lbltxtpass" runat="server" ForeColor="Black">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lbltxtpass"
                                            SetFocusOnError="true" ValidationGroup="2a" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnsubm" runat="server" Text="Submit" ValidationGroup="2a" OnClick="btnsubm_Click"
                                            CssClass="btnSubmit" />
                                        <asp:Button ID="btnsubm0" runat="server" OnClick="btnsubm0_Click" Text="Cancel" ValidationGroup="3"
                                            CssClass="btnSubmit" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="Button1" runat="server" Style="display: none" />
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel1" TargetControlID="Button1">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel8" runat="server" CssClass="modalPopup" BorderWidth="5px" BackColor="#d1cec5"
                            Width="400px">
                            <table id="Table2" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="subinnertblfc">
                                        CONFIRM....
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col4">
                                        <asp:Label ID="Label29" runat="server" ForeColor="Black" Font-Size="12px" Text="(Are you sure you wish to enter outtime ? As you can make entry of intime & outtime entry once in a days.)"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="Button3" CssClass="btnSubmit" Text="CONFIRM" runat="server" OnClick="Button1_Click" />
                                        &nbsp;<asp:Button ID="btncan" CssClass="btnSubmit" Text="CANCEL" runat="server" OnClick="btncan_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel8" TargetControlID="HiddenButton222">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" BorderWidth="7px" BackColor="#d1cec5"
                            Width="400px">
                            <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblwelcome" runat="server" Text="Welcome " Font-Bold="false" Font-Size="12px"></asp:Label>
                                            <asp:Label ID="Label4" runat="server" Font-Size="12px" Font-Names="Verdana"></asp:Label>
                                            <asp:Label ID="Label25" runat="server" Font-Bold="false" Text=", you have successfully entered."
                                                Font-Size="12px"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="Label26" runat="server" Font-Bold="false" Text="Your entry time : "
                                                Font-Size="12px"></asp:Label>
                                            <asp:Label ID="lblentrytimee" runat="server" Text="" Font-Size="12px" Font-Names="Verdana"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="HiddenButton2223" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel2" TargetControlID="HiddenButton2223">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" BorderWidth="7px" BackColor="#d1cec5"
                            Width="400px">
                            <table id="Table3" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label111" runat="server" Font-Bold="false" Text="Thank you " Font-Size="12px"></asp:Label>
                                            <asp:Label ID="Label211" runat="server" Font-Size="12px" Font-Names="Verdana"></asp:Label>
                                            <asp:Label ID="Label30" runat="server" Font-Bold="false" Text=", you have successfully checked out."
                                                Font-Size="12px"></asp:Label>
                                            <br />
                                            <asp:Label ID="Label31" runat="server" Font-Bold="false" Text="Your check out time : "
                                                Font-Size="12px"></asp:Label>
                                            <asp:Label ID="Label32" runat="server" Text="" Font-Size="12px" Font-Names="Verdana"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="HiddenButton22234" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender6" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel4" TargetControlID="HiddenButton22234">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel9" runat="server" CssClass="modalPopup" BorderWidth="7px" BackColor="#d1cec5"
                            Width="400px">
                            <table id="Table4" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label33" Font-Bold="false" runat="server" Font-Size="12px" Text="Sorry, No Attendance Record can be inserted this time as you have exceeded the allowed number of instances.">
                                            </asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="HiddenButton222345" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender7" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel9" TargetControlID="HiddenButton222345">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel10" runat="server" CssClass="modalPopup" BorderWidth="7px" BackColor="#d1cec5"
                            Width="400px">
                            <table id="Table5" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label34" runat="server" Font-Bold="false" Font-Size="12px" Text="You are not able to check in / check out at this time.Your last entry time was at <"></asp:Label>
                                            <asp:Label ID="Label35" runat="server" Font-Size="12px" Text="" Font-Names="Verdana"></asp:Label>
                                            <asp:Label ID="Label36" runat="server" Font-Bold="false" Font-Size="12px" Text=">, you are blocked from making another entry for <"></asp:Label>
                                            <asp:Label ID="Label37" runat="server" Font-Size="12px" Text="" Font-Names="Verdana"></asp:Label>
                                            <asp:Label ID="Label38" runat="server" Font-Bold="false" Font-Size="12px" Text="> minutes."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="HiddenButton2223456" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender8" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel10" TargetControlID="HiddenButton2223456">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel11" runat="server" CssClass="modalPopup" BorderWidth="7px" BackColor="#d1cec5"
                            Width="400px" Height="60px">
                            <table id="Table6" cellpadding="0" cellspacing="0" width="100%" align="center">
                                <tr>
                                    <td align="center">
                                        <label>
                                            <asp:Label ID="Label39" runat="server" Font-Bold="false" Font-Size="12px" Text="Sorry, you are not allowed late entry for rules."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="HiddenButton222346" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender9" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel11" TargetControlID="HiddenButton222346">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel12" runat="server" CssClass="modalPopup" BorderWidth="7px" BackColor="#d1cec5"
                            Width="400px" Height="60px">
                            <table id="Table7" cellpadding="0" cellspacing="0" width="100%" align="center">
                                <tr>
                                    <td align="center">
                                        <label>
                                            <asp:Label ID="Label40" runat="server" Font-Bold="false" Font-Size="12px" Text="Sorry, you are not allowed early entry for rules."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="HiddenButton22as" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender10" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel12" TargetControlID="HiddenButton22as">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel13" runat="server" CssClass="modalPopup" BorderWidth="7px" BackColor="#d1cec5"
                            Width="400px" Height="60px">
                            <table id="Table8" cellpadding="0" cellspacing="0" width="100%" align="center">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label52" runat="server" Font-Bold="false" Font-Size="12px" Text="You are not allowed to add your attendance, as today is not a working day in your scheduled batch."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="HiddenButton22as1" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender11" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel13" TargetControlID="HiddenButton22as1">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel14" runat="server" CssClass="modalPopup" BorderWidth="7px" BackColor="#d1cec5"
                            Width="450px" Height="90px">
                            <table id="Table9" cellpadding="0" cellspacing="0" width="100%" align="center">
                                <tr>
                                    <td align="left">
                                        <label>
                                            <asp:Label ID="Label53" Font-Bold="false" runat="server" Font-Size="12px" Text="Sorry, we cannot locate your user ID and password in our system."></asp:Label>
                                            <asp:Label ID="Label54" Font-Bold="false" runat="server" Font-Size="12px" Text="Please try again."></asp:Label>
                                            <br />
                                            <asp:Label ID="Label55" Font-Bold="false" runat="server" Font-Size="12px" Text="If you are a new employee, please consult with your supervisor to ensure that your user ID and password are updated in our system."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="HiddenButton22as12" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender12" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel14" TargetControlID="HiddenButton22as12">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel15" runat="server" CssClass="modalPopup" BorderWidth="7px" BackColor="#d1cec5"
                            Width="450px" Height="85px">
                            <table id="Table11" cellpadding="0" cellspacing="0" width="100%" align="center">
                                <tr>
                                    <td align="left">
                                        <label>
                                            <asp:Label ID="Label65" runat="server" Font-Size="12px" Font-Bold="false" Text="Your Exit entry for the day is already done at "></asp:Label>
                                            <asp:Label ID="Label66" runat="server" Font-Size="12px" Font-Bold="true" Text=""
                                                Font-Names="Verdana"></asp:Label>
                                            <br />
                                            <asp:Label ID="Label67" runat="server" Font-Size="12px" Font-Bold="false" Text="You can not change this . Please check with Attendance Admin for any correction required."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="HiddenButton22as12aa" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender14" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel15" TargetControlID="HiddenButton22as12aa">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel18" runat="server" BorderWidth="7px" BackColor="#d1cec5" Width="450px"
                            Height="60px">
                            <table id="Table12" cellpadding="0" cellspacing="0" width="100%" align="center">
                                <tr>
                                    <td align="center">
                                        <label>
                                            <asp:Label ID="Label89" runat="server" Font-Bold="false" Text="You are not allowed to add your attendance, as your Absence Note is already added."></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="hiddenccccccc" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender15" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel18" TargetControlID="hiddenccccccc">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Panel ID="pnllblmsg" runat="server" CssClass="modalPopup" BorderWidth="7px" BackColor="#d1cec5"
                            Width="400px" Height="60px">
                            <table id="Table13" cellpadding="0" cellspacing="0" width="100%" align="center">
                               <tr>
                                  <td>
                                  
                                  </td>
                               </tr>
                                 <tr>
                                    <td >
                                        <asp:Label ID="lblmsg" runat="server" ForeColor="Black"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="btnlblmsg" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender16" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="pnllblmsg" TargetControlID="btnlblmsg">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Panel ID="Panel70v" runat="server" BackColor="#d1cec5" BorderWidth="7px" Width="450px">
                <table id="Table10" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label72" runat="server" Text="Confirm Entry !" Font-Names="Verdana"
                                    Font-Bold="false" Font-Size="12px"></asp:Label>
                            </label>
                            <label>
                                <asp:Label ID="Label56a" runat="server" Text="" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                            </label>
                            <label>
                                <asp:Label ID="Label57b" runat="server" Text="" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label73" runat="server" Text="Entry Date :" Font-Names="Verdana" Font-Size="12px"
                                    Font-Bold="false"></asp:Label>
                            </label>
                            <label>
                                <asp:Label ID="Label58a" runat="server" Text="" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                                ,
                            </label>
                            <label>
                                <asp:Label ID="Label74" runat="server" Text="Entry Time :" Font-Names="Verdana" Font-Size="12px"
                                    Font-Bold="false"></asp:Label>
                            </label>
                            <label>
                                <asp:Label ID="Label59" runat="server" Text="" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="Button4a" CssClass="btnSubmit" Text="Confirm" runat="server" BorderWidth="1px"
                                OnClick="Button4a_Click" />
                            <asp:Button ID="Button4b" CssClass="btnSubmit" Text="Cancel" runat="server" BorderWidth="1px"
                                OnClick="Button4b_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label58" runat="server" Text="(Please note that you can do presence entry only once a day)"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="HiddenButton222milan" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender13" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel70v" TargetControlID="HiddenButton222milan">
            </cc1:ModalPopupExtender>
            <div style="clear: both;">
            </div>
            <asp:Panel ID="Panel881" runat="server" BackColor="#d1cec5" BorderWidth="7px" Width="450px">
                <table id="Table22" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label75" runat="server" Text="Confirm Exit !" Font-Names="Verdana"
                                    Font-Bold="false" Font-Size="12px"></asp:Label>
                            </label>
                            <label>
                                <asp:Label ID="Label59ss" runat="server" Text="" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                            </label>
                            <label>
                                <asp:Label ID="Label60" runat="server" Text="" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label76" runat="server" Text="Exit Date :" Font-Names="Verdana" Font-Size="12px"
                                    Font-Bold="false"></asp:Label>
                            </label>
                            <label>
                                <asp:Label ID="Label60a" runat="server" Text="" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                                ,
                            </label>
                            <label>
                                <asp:Label ID="Label77" runat="server" Text="Exit Time :" Font-Names="Verdana" Font-Size="12px"
                                    Font-Bold="false"></asp:Label>
                            </label>
                            <label>
                                <asp:Label ID="Label61" runat="server" Text="" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="Button44" CssClass="btnSubmit" Text="Confirm" runat="server" OnClick="Button44_Click"
                                BorderWidth="1px" />
                            <asp:Button ID="btncan12" CssClass="btnSubmit" Text="Cancel" runat="server" OnClick="btncan12_Click"
                                BorderWidth="1px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="HiddenButton2222" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1223" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel881" TargetControlID="HiddenButton2222">
            </cc1:ModalPopupExtender>
        </div>
        <!--end of right content-->
        <div style="clear: both;">
        </div>
    </div>
</asp:Content>
