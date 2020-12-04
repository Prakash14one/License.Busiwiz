<%@ Page Language="C#" MasterPageFile="~/ioffice/MainHome.master" AutoEventWireup="true"
    CodeFile="EmpAtt.aspx.cs" Inherits="EmpAtt" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">
    function keyup(adf)
   {
      var hhh=adf;
  
      if(hhh=="15")
      {
            
           document.getElementById('<%= btnGo.ClientID %>').focus();
      }
    }
    </script>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    
    <div id="right_content">
        <h2>
             Attendance Master
             </h2>
        <div class="products_box">
            <fieldset style="padding: 0 2% 0 35%">
          <legend>Attendance by Barcode</legend>
          
          <table style="width:100%;height:100%;">
          <tr>
          <td>
           <label class="first" for="title1">
                    <asp:Label ID="Label24" runat="server" Text="Time Zone" ></asp:Label>
                </label>
          </td>
          </tr>
          <tr>
          <td>
            <label for="firstName1">
                
                    <asp:DropDownList ID="ddltimezone" runat="server" AutoPostBack="True" 
                                            
                    onselectedindexchanged="ddltimezone_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                        
                                        
                                        <asp:Label ID="lbldt" runat="server" ></asp:Label>
                                    <asp:Label ID="lbltime" runat="server" ></asp:Label>
                                        
                  
                </label>
          </td>
          </tr>
          <tr>
          <td>
           <label for="lastName1">
                    <asp:Label ID="Label3" runat="server" Text="Current Date" ></asp:Label>
                     <asp:Label ID="Label1date" runat="server" ></asp:Label>
                
                </label>
          </td>
          </tr>
          <tr>
          <td>
            <div style="text-align: center; font-size: 50px; border:2px solid #416271; padding:10px; width: 80%; vertical-align: text-top;
                    border: solid 2 black !important;" >
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" RenderMode="Block">                                        
                                            <ContentTemplate>
                                                 <asp:Timer ID="TimerTime" runat="server" Interval="1000" 
                                            ontick="TimerTime_Tick">
                                            </asp:Timer>
                                                 <asp:Label ID="time22" Visible="false" runat="server" Font-Bold="True"></asp:Label>
                                                 <asp:Label ID="lblhour" runat="server"  ></asp:Label>
                                                 <asp:Label ID="lblmin" runat="server"  ></asp:Label>
                                                 <asp:Label ID="lblsec" runat="server"  ></asp:Label>
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
                    <asp:Label ID="lbluserid" runat="server" ></asp:Label>
                    <asp:Label ID="lbldtate" runat="server"></asp:Label>
                </label>
          </td>
          </tr>
          <tr>
          <td>
         <%--   <div style="text-align:left;">
                 <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                </div>--%>
          </td>
          </tr>
          <tr>
          <td align="left" style="width:300px;">
                                                                           
                                                    <label>
                                                        <asp:Label ID="lbldateto" runat="server" Text="To "></asp:Label>
                                                    </label>
                                                    <label>
                                                     <asp:TextBox ID="txtdateto" runat="server" Width="100px"></asp:TextBox>
                                                         <%--  <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureName="en-AU"
                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdateto" />--%>
                                                           <%-- <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                    PopupButtonID="imgbtn1" TargetControlID="txtdateto">
                                    </cc1:CalendarExtender>--%>
                                
                                                    </label>
                                                  <%--  <label> <asp:ImageButton ID="imgbtn1" runat="server" ImageUrl="~/images/cal_btn.jpg"  /></label>--%>
                                                    <label>    
              <asp:TextBox ID="txttimemaster" runat="server" Width="69px" MaxLength="5"></asp:TextBox></label>
          </td>
          </tr>
          <tr>
          <td>
             <asp:Panel ID="Panel7" runat="server" >
               
             
            
              <label><asp:Label ID="Label4" runat="server" Text="Barcode"></asp:Label><asp:RequiredFieldValidator ID="rrts" runat="server" ControlToValidate="txtbartext" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="2">
                                            </asp:RequiredFieldValidator><asp:TextBox ID="txtbartext" runat="server" 
                                            AutoPostBack="True" onkeyup="keyup(this.value.length)" Width="150px" ontextchanged="txtbartext_TextChanged">
                                            </asp:TextBox></label>
            
            
           <label><br /><asp:Button ID="btnGo" runat="server" ValidationGroup="2" Text="Go" 
                     onclick="btnGo_Click" Width="31px"  /></label>
            
             
                 </asp:Panel>
          </td>
          </tr>
          <tr>
          <td style="height: 16px">
            <div style="text-align:left;">
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
            <td >
            </td>
        </tr>
          <tr>
                            <td>
                                <asp:Panel ID="Panel5" runat="server"   CssClass="modalPopup" BackColor="#d1cec5"
                                    Width="600px" BorderWidth="10px">
                                    <table cellpadding="0" cellspacing="0" style="width:600px" >
                                        <tr>
                                            <td align="right">
                                                <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/closeicon.jpeg" runat="server"
                        Width="16px" onclick="ImageButton4_Click"  />
                                                </td>
                                        </tr>
                                         <tr>
                                            <td align="center">
                                                 <asp:Label ID="Label12" runat="server" 
                                                     Text="Welcome To Office !!!" ForeColor="#336699" Font-Bold="True" 
                                                     Font-Size="16px"></asp:Label>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblm" runat="server" ForeColor="#336699"></asp:Label>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                 <asp:Label ID="Label11" runat="server" 
                                                     Text="Your Attandance is successfully entered." ForeColor="Black"></asp:Label>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblempnamemsg" runat="server" ForeColor="Black" 
                                                    Text="Employee Name :"></asp:Label>
                                                <asp:Label ID="lblempname" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" >
                                                <asp:Label ID="lbletimemsg" runat="server" Text="Your entry time is :" 
                                                    ForeColor="Black"></asp:Label>
                                                 <asp:Label ID="Label7" runat="server" Text="" ForeColor="Black"> </asp:Label>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td align="center" >
                                                <asp:Label ID="Label13" runat="server" Text="You are early by :" ForeColor="Black"> </asp:Label>
                                                 <asp:Label ID="Label14" runat="server" Text="" ForeColor="Black"> </asp:Label>
                                            </td>
                                        </tr>
                                      
                                        <tr>
                                            <td align="center" >
                                             <asp:Label ID="Label8" runat="server" 
                                                    Text="You are not allowed to enter or exit,Kindly see your supervisor" 
                                                    Visible="False" ForeColor="Black"></asp:Label>
                                               
                                                
                                            </td>
                                        </tr>
                                         <tr>
                                            <td align="center" >
                                             <asp:Label ID="lbllate" runat="server" 
                                                    Text="You are late, please meet your supervisor immediately to input the reason for being late ." 
                                                    Visible="False" ForeColor="Black"></asp:Label> <br />
                                                 <asp:Label ID="Label9" runat="server" 
                                                    Text="You need authorisation from your supervisor." 
                                                    Visible="False" ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" >
                                               <asp:Label ID="Label10" runat="server" Text="Your last exit time was :"  
                                                    ForeColor="Black"></asp:Label>
                                                 <asp:Label ID="lbllastexittime" runat="server" Text=""  ForeColor="Black"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" >
                                                 </td>
                                        </tr>
                                    </table>
                                    <asp:Button ID="HiddenButton2221" runat="server" Style="display: none" />
                                   </asp:Panel>
                                <cc1:ModalPopupExtender
                                    ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel5" TargetControlID="HiddenButton2221">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
           
             <tr>
                            <td >
                                <asp:Panel ID="Panel3" runat="server"  CssClass="modalPopup" BackColor="#d1cec5"
                                    Width="600px" BorderWidth="10px">
                                    <table cellpadding="0" cellspacing="0" style="width:600px" >
                                        <tr>
                                            <td align="right">
                                                 <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/closeicon.jpeg" runat="server"
                        Width="16px" onclick="ImageButton1_Click"   />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                 <asp:Label ID="Label17" runat="server" 
                                                     Text="Bye Bye !!!" ForeColor="#336699" Font-Bold="True" Font-Size="16px"></asp:Label>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                 <asp:Label ID="labbb" runat="server" 
                                                     Text="Your exit is  successfully entered." ForeColor="Black"></asp:Label>
                                                <asp:Label ID="lblgoemp" runat="server" Text="" ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label15" runat="server" ForeColor="Black" 
                                                    Text="Your exit time is :"></asp:Label>
                                                <asp:Label ID="lblexittime" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" >
                                                <asp:Label ID="Label16" runat="server" Text="You are early by :" ForeColor="Black"> </asp:Label>
                                                 <asp:Label ID="lblouterly" runat="server" Text="" ForeColor="Black"> </asp:Label>
                                            </td>
                                        </tr>
                                  
                                        <tr>
                                            <td align="center" >  <asp:Label ID="Label19" runat="server" 
                                                    Text="You are not allowed to enter or exit,Kindly see your supervisor." 
                                                    Visible="False" ForeColor="Black"></asp:Label>
                                               
                                                
                                            </td>
                                        </tr>
                                        
                                         <tr>
                                            <td align="center" >
                                            <asp:Label ID="Label18" runat="server" 
                                                    Text="You are going early, please meet with your supervisior immediately to input the reason for going early." 
                                                    Visible="False" ForeColor="Black"></asp:Label> <br />
                                                 <asp:Label ID="Label20" runat="server" 
                                                    Text="You need authorisation from your supervisor. Please meet with your supervisor immediately." 
                                                    Visible="False" ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td align="center" >
                                               <asp:Label ID="Label21" runat="server" Text="Your last entry time was :"  ForeColor="Black"> </asp:Label>
                                                 <asp:Label ID="Label22" runat="server" Text=""  ForeColor="Black"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" >
                                                 </td>
                                        </tr>
                                    </table>
                                    <asp:Button ID="Button2" runat="server" Style="display: none" />
                                   </asp:Panel>
                                <cc1:ModalPopupExtender
                                    ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel3" TargetControlID="Button2">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
        <tr>
            <td >
            <asp:Timer ID="timer1" runat="server" Interval="15000" ontick="timer1_Tick" 
                    Enabled="False"></asp:Timer>  
                    
              
            </td>
        </tr>
           <tr>
                                <td class="label">
                                    <asp:Panel ID="Panel6" runat="server"   CssClass="modalPopup" BorderWidth="10px" BackColor="#d1cec5"
                                    Width="467px">
                                        <table cellpadding="2" cellspacing="2" align="center" style="width: 460px" >
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="lbllatemessagereason" runat="server" 
                                                     ForeColor="Red" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Label ID="Label28" runat="server" Text="You are late/early, please  input the reason for being deviation. otherwise your attendance not approved." 
                                                     ForeColor="Black"></asp:Label>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="right" >
                                                    <asp:Label ID="Label27" runat="server" Text="input the reason for being deviation :" 
                                                     ForeColor="Black"></asp:Label>
                                                </td>
                                                <td align="left" width="50%">
                                                <asp:TextBox ID="latereaso" runat="server" Text="" Height="100px" MaxLength="500" 
                                                        TextMode="MultiLine" Width="210px"></asp:TextBox>
                                                <asp:RequiredFieldValidator id="rqsd" runat="server" ControlToValidate="latereaso" 
                                                        ErrorMessage="*" ValidationGroup="5" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                <asp:Button ID="btnaddlater" runat="server" Text="Add" 
                                                        onclick="btnaddlater_Click" CssClass="btnSubmit" />
                                                </td>
                                                </tr>
                                        </table>
                                        <asp:Button ID="Button6" runat="server" Style="display: none" />
                                    </asp:Panel>
                                    <cc1:ModalPopupExtender
                                    ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel6" TargetControlID="Button6">
                                    </cc1:ModalPopupExtender>
                                </td>
                            </tr>
        
            <tr>
                                <td class="label">
                                    <asp:Panel ID="Panel1" runat="server"  CssClass="modalPopup" BorderWidth="5px" BackColor="#d1cec5"
                                    Width="467px">
                                        <table cellpadding="0" cellspacing="0" align="center" style="width: 460px" >
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="lblmsggg" runat="server" 
                                                     ForeColor="Red"></asp:Label>
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
                                                    <asp:Label ID="Label6" runat="server" Text="Supervisor Name : " 
                                                     ForeColor="Black" Visible="False"></asp:Label>
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
                                                    <asp:Label ID="lblu" runat="server" Text="User Id  :" ForeColor="Black" 
                                                ></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtuerlog" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="2" ControlToValidate="txtuerlog" SetFocusOnError="true" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblp" runat="server" Text="Password  :" ForeColor="Black"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="lbltxtpass" runat="server"  ForeColor="Black">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lbltxtpass" SetFocusOnError="true" ValidationGroup="2" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" >
                                                    <asp:Button ID="btnsubm" runat="server" Text="Submit" ValidationGroup="2" 
                                                    onclick="btnsubm_Click" CssClass="btnSubmit" />
                                                    <asp:Button ID="btnsubm0" runat="server" onclick="btnsubm0_Click" 
                                                    Text="Cancel" ValidationGroup="3" CssClass="btnSubmit" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Button ID="Button1" runat="server" Style="display: none" />
                                    </asp:Panel>
                                    <cc1:ModalPopupExtender
                                    ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel1" TargetControlID="Button1">
                                    </cc1:ModalPopupExtender>
                                </td>
                            </tr>
                            <tr>
                            <td>
                              <asp:Panel ID="Panel8" runat="server"  CssClass="modalPopup" BorderWidth="5px" BackColor="#d1cec5"
                    Width="467px">
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
                                <asp:Button ID="Button3" CssClass="btnSubmit" Text="CONFIRM" runat="server" 
                                    OnClick="Button1_Click" />
                                &nbsp;<asp:Button ID="btncan" CssClass="btnSubmit"  Text="CANCEL" 
                                    runat="server" onclick="btncan_Click" 
                                     />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender
                    ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel8" TargetControlID="HiddenButton222" >
                </cc1:ModalPopupExtender>
                            </td>
                            </tr>
                             <tr>
                            <td>
                              <asp:Panel ID="pnllblmsg" runat="server"  CssClass="modalPopup" BorderWidth="5px" BackColor="#d1cec5"
                    Width="467px" Height="100px">
                    <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                      <tr>
                            <td>
                            </td>
                      </tr>
                      <tr>
                            <td class="col4">
                                   <asp:Label ID="lblmsg" runat="server" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                </asp:Panel>
                <asp:Button ID="btnlblmsg" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender
                    ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="pnllblmsg" TargetControlID="btnlblmsg" >
                </cc1:ModalPopupExtender>
                            </td>
                            </tr>
    </table>
   
        </div>
        <!--end of right content-->
        <div style="clear: both;">
        </div>
    </div>

   
</asp:Content>
