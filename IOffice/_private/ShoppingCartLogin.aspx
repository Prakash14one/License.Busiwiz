<%@ Page Language="C#" MasterPageFile="~/Master/Login.master" AutoEventWireup="true"
    CodeFile="ShoppingCartLogin.aspx.cs" Inherits="Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function GetMacAddress() {
        
            //This function requires following option to be enabled without prompting	
            //In Internet Options for IE 5.5 and up	
            //Tab Security (Local Internet Sites)	
            //Custom Level button	
            //"Initialize and script ActiveX control not marked as safe." option enabled	
            
            try {
                var locator = new ActiveXObject("WbemScripting.SWbemLocator");
                var service = locator.ConnectServer(".");

                //Get properties of the network devices with an active IP address	
                var properties = service.ExecQuery("SELECT * FROM Win32_NetworkAdapterConfiguration" +
                " WHERE IPEnabled=TRUE");

                var e = new Enumerator(properties);
                //Take first item from the list and return MACAddress
                var p = e.item(0);
                //             alert(e.item("ComputerName"))

                var network = new ActiveXObject('WScript.Network');

                document.getElementById('<%=usercomputername.ClientID%>').value = network.computername;
                document.getElementById('<%=compusername.ClientID%>').value = network.UserDomain + "\\" + network.UserName;
                //alert(network.computername);
              //  alert(network.UserDomain + "\\" + network.UserName);
            }
            catch (exception) {
                //alert('A');
                //        window.location = "about:blank";	
            }

            return p.MACAddress;
        }

        function showMacAddress() {
            var strmac = GetMacAddress();
            var macFound = false;

            if (strmac != null && strmac != 'Nothing') {


                document.getElementById('<%=Hidden1.ClientID%>').value = strmac;
                macFound = true;
                
            }
            else {

                hfadr.value = '';
                alert('MAC address does not exist! Call IT support.');
            }

            return macFound;
        }	

    </script>

    <%--<script type="text/javascript" language="javascript"> 
     function ShowMyModalPopup() 
        {  
         var modal = $find("<%=ModalPopupExtender9.ClientID%>");  
         modal.show(); 
         
         
         var modal2 = $find("<%=ModalPopupExtender1.ClientID%>");  
       //  modal2.dispose(); 
          modal2.hide();
          
        }
      
        
        
    </script>--%> 
    
     
    
    
     <form runat="server" id="fmLogin" autocomplete="on">
    
    <asp:Panel ID="Panel1" runat="server" Visible="false"> 
        <div class="container">
            <!-- Codrops top bar -->
            <div class="codrops-top">
            </div>
            <!--/ Codrops top bar -->
            <div id="container_demo">
                <a class="hiddenanchor" id="toregister"></a><a class="hiddenanchor" id="tologin">
                </a>
              
                <div id="wrapper">
                    <div id="login" class="animate form">
                        <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                        <h1>
                            Login
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                        </h1>
                        <p>
                            <label for="username" class="uname">
                                <input id="Hidden1" type="hidden" runat="server" />
                                <input id="usercomputername" type="hidden" runat="server" />
                                <input id="compusername" type="hidden" runat="server" />
                                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                            </label>
                        </p>
                        <asp:Panel ID="pnl_other" runat="server" Visible="false">
                         <p>
                            <label for="username" class="uname">
                                
                                <label id="Label6" runat="server">Select User Type</label> 
                            </label>
                                           <asp:DropDownList ID="ddlrolemode" runat="server" AutoPostBack="true" CssClass="cssTextbox" BackColor="#faffbd" >
                                                                </asp:DropDownList>
                                                              
                           </p>
                                           
                            
                             </asp:Panel>
                             <p>
                                 <label class="uname" for="username">
                                 <label ID="lbl_comny" runat="server">
                                 Company ID</label>
                                 </label>
                                 <asp:TextBox ID="txtcompanyid" runat="server" CssClass="cssTextbox" 
                                     TabIndex="1"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                     ControlToValidate="txtcompanyid" Display="Dynamic" EnableClientScript="False" 
                                     ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                             </p>
                             <p>
                                 <label class="uname" for="username">
                                 User Name
                                 <asp:Label ID="box" runat="server" ClientMode="Static" ForeColor="Black" 
                                     Visible="true"></asp:Label>
                                 <%--<span id="box" clientMode="Static" runat="server"></span>--%>
                                 </label>
                                 <asp:TextBox ID="txtuname" runat="server" CssClass="cssTextbox" TabIndex="2"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                     ControlToValidate="txtuname" Display="Dynamic" EnableClientScript="False" 
                                     ErrorMessage="*" ValidationGroup="1">
                            </asp:RequiredFieldValidator>
                             </p>
                             <p>
                                 <label class="youpasswd" for="password">
                                 Password
                                 </label>
                                 <asp:TextBox ID="txtpass" runat="server" CssClass="cssTextbox" TabIndex="3" 
                                     TextMode="Password"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                     ControlToValidate="txtpass" Display="Dynamic" EnableClientScript="False" 
                                     ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                             </p>
                             <p>
                                 <label class="youpasswd" for="password">
                                 <asp:CheckBox ID="chkremember" runat="server" TabIndex="4" 
                                     Text=" Remember me" />
                                 </label>
                             </p>
                             <p class="button">
                                 <%--<asp:Button ID="btnLogin" runat="server" Text="Login" ValidationGroup="vgAdminLogin" />--%>
                                 <asp:Button ID="btnsignin" runat="server" OnClick="btnsignin_Click" 
                                     TabIndex="5" Text="Sign In" ValidationGroup="1" />
                             </p>
                             <p>
                                 <%--<asp:HyperLink runat="server" Text="Forget Password" ID="hlForgetPassword"></asp:HyperLink> --%>
                                 <asp:HyperLink ID="HyperLink1" runat="server" 
                                     NavigateUrl="~/ShoppingCart/Admin/ResetLoginInformation.aspx">Reset Login Information</asp:HyperLink>
                                 <br />
                                 <asp:HyperLink ID="hplforgotpass" runat="server" 
                                     NavigateUrl="~/ShoppingCart/Admin/ForgotPassword.aspx" TabIndex="6">Forgot Password?</asp:HyperLink>
                                 <%--<asp:HyperLink runat="server" Text="New Sign Up" ID="HyperLink1"></asp:HyperLink>--%>
                                 <br />
                                 <asp:LinkButton ID="Lnkbtn1" runat="Server" OnClick="Lnkbtn1_Click" 
                                     TabIndex="7" Text="New Company Registration">
                            </asp:LinkButton>
                                 <br />
                                 <asp:LinkButton ID="linlcomp" runat="Server" OnClick="Lnkbtn1_Clickcompany" 
                                     TabIndex="7" Text="Company? Login here">
                            </asp:LinkButton>
                                 <asp:LinkButton ID="linlcandi" runat="Server" OnClick="Lnkbtn1_Clickcandi" 
                                     TabIndex="7" Text="Candidate? Login here">
                            </asp:LinkButton>
                             </p>
                             <asp:Panel ID="Panel4" runat="server" Visible="false">
                                 <table width="100%">
                                     <tr>
                                         <td>
                                             <asp:Panel ID="Panel3" runat="server" BackColor="White" BorderColor="#999999" 
                                                 BorderStyle="Solid" BorderWidth="2px" Height="42%" ScrollBars="None" 
                                                 Width="70%">
                                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                     <ContentTemplate>
                                                         <fieldset>
                                                             <table width="100%">
                                                                 <tr>
                                                                     <td align="center" colspan="2">
                                                                         <asp:Image ID="Image2" runat="server" Height="30px" 
                                                                             ImageUrl="~/ShoppingCart/images/Error.png" Width="30px" />
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td align="center" colspan="2">
                                                                         <br />
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td align="center" colspan="2">
                                                                         <label>
                                                                         Sorry ! You can only use Internet Explorer 10 &amp; up to login
                                                                         </label>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td align="center" colspan="2">
                                                                         <label>
                                                                         Please add the following websites to your &quot;Trusted Sites&quot;
                                                                         <br />
                                                                         and do configuration as explained
                                                                         <%--<a onclick="ShowMyModalPopup();">Help</a>--%>
                                                                         <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                                                                             OnClick="LinkButton1_Click">here.</asp:LinkButton>
                                                                         </label>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="2">
                                                                         <br />
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td align="center" colspan="2">
                                                                         <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td align="center" colspan="2">
                                                                         <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td align="center" colspan="2">
                                                                         <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="2">
                                                                         <br />
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td align="center" colspan="2">
                                                                         <asp:Button ID="Button1" runat="server" Font-Bold="true" Text="Close" 
                                                                             Width="50px" />
                                                                     </td>
                                                                 </tr>
                                                             </table>
                                                         </fieldset>
                                                     </ContentTemplate>
                                                 </asp:UpdatePanel>
                                             </asp:Panel>
                                             <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
                                                 BackgroundCssClass="modalBackground" CancelControlID="Button1" Drag="true" 
                                                 PopupControlID="Panel3" TargetControlID="Button212321">
                                             </cc1:ModalPopupExtender>
                                             <asp:Button ID="Button212321" runat="server" Style="display: none" />
                                         </td>
                                     </tr>
                                 </table>
                             </asp:Panel>
                             <asp:Panel ID="Panel2" runat="server">
                                 <table width="100%">
                                     <tr>
                                         <td>
                                             <asp:Panel ID="pnlMainTypeAdd" runat="server" BackColor="White" 
                                                 BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" Height="50%" 
                                                 ScrollBars="None" Width="80%">
                                                 <asp:UpdatePanel ID="UpdatePanelMainTypeAdd" runat="server" 
                                                     UpdateMode="Conditional">
                                                     <ContentTemplate>
                                                         <fieldset>
                                                             <table width="100%">
                                                                 <tr>
                                                                     <td style="width: 95%; font-size: large; font-weight: bold; text-align: center">
                                                                         Change of settings required for Internet Explorer 10 &amp; up
                                                                     </td>
                                                                     <td style="width: 5%">
                                                                         <div style="text-align: right; border-style: none">
                                                                             <asp:ImageButton ID="ImageButton8" runat="server" BorderStyle="None" 
                                                                                 Height="15px" ImageUrl="~/images/closeicon.png" OnClick="ImageButton8_Click" 
                                                                                 Width="15px" />
                                                                         </div>
                                                                     </td>
                                                                 </tr>
                                                             </table>
                                                             <asp:Panel ID="Panel11" runat="server" Visible="false">
                                                                 <label>
                                                                 1) Go to Internet Option..Security …Trusted sites..click on custom level..and 
                                                                 select option &quot;Prompt&quot; for the point
                                                                 </label>
                                                                 <div style="clear: both;">
                                                                 </div>
                                                                 <div>
                                                                     <label>
                                                                     <b>&quot;Intialize and script ActiveX control not marked as safe for scripting&quot;</b>
                                                                     <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" 
                                                                         OnClick="LinkButton2_Click">See
                                                                Image</asp:LinkButton>
                                                                     <div style="clear: both;">
                                                                     </div>
                                                                     </label>
                                                                 </div>
                                                             </asp:Panel>
                                                             <label>
                                                             <br />
                                                             1) Go to Internet Option..Security..Trusted sites..Click on sites..and add sites 
                                                             without selecting
                                                             </label>
                                                             <div style="clear: both;">
                                                             </div>
                                                             <label>
                                                             <b>&quot;Require server verification(https://) for all sites in this zone&quot;</b><asp:LinkButton 
                                                                 ID="LinkButton3" runat="server" CausesValidation="false" 
                                                                 OnClick="LinkButton3_Click">See Image</asp:LinkButton>
                                                             </label>
                                                             <div style="clear: both;">
                                                             </div>
                                                             <label>
                                                             And also set security level for this zone as &quot;Low&quot;
                                                             <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" 
                                                                 OnClick="LinkButton4_Click">See Image</asp:LinkButton>
                                                             </label>
                                                             <div style="clear: both;">
                                                             </div>
                                                             <div>
                                                                 <label>
                                                                 <br />
                                                                 2) Next time when user will login,there will be a warning from Internet explorer 
                                                                 :<b>&quot;An ActiveX Control on this page might be unsafe to interact with other 
                                                                 parts of the page. Do you want to allow this interaction?&quot;</b>
                                                                 <br />
                                                                 Select <b>&quot;yes&quot;</b> to this warning.
                                                                 </label>
                                                             </div>
                                                             <div style="clear: both;">
                                                             </div>
                                                             <label>
                                                             <br />
                                                             Please note that this script is used to enable you to reject any unauthorised 
                                                             computer or IP address to access your system.
                                                             </label>
                                                         </fieldset>
                                                     </ContentTemplate>
                                                 </asp:UpdatePanel>
                                             </asp:Panel>
                                             <cc1:ModalPopupExtender ID="ModalPopupExtender9" runat="server" 
                                                 BackgroundCssClass="modalBackground" CancelControlID="ImageButton8" Drag="true" 
                                                 PopupControlID="pnlMainTypeAdd" TargetControlID="hdnMaintypeAdd">
                                             </cc1:ModalPopupExtender>
                                             <input id="hdnMaintypeAdd" runat="Server" name="hdnMaintypeAdd" type="hidden" style="width: 4px" />
                                         </td>
                                     </tr>
                                 </table>
                             </asp:Panel>
                             <asp:Panel ID="Panel5" runat="server">
                                 <table width="100%">
                                     <tr>
                                         <td>
                                             <asp:Panel ID="Panel6" runat="server" BackColor="White" BorderColor="#999999" 
                                                 BorderStyle="Solid" BorderWidth="2px" ScrollBars="None" Width="45%">
                                                 <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                     <ContentTemplate>
                                                         <fieldset>
                                                             <div style="text-align: center;">
                                                                 <br />
                                                                 <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Scriptimg.png" />
                                                             </div>
                                                             <div style="clear: both;">
                                                             </div>
                                                             <div style="text-align: center">
                                                                 <asp:Button ID="Button2" runat="server" Font-Bold="true" Text="Close" 
                                                                     Width="50px" />
                                                             </div>
                                                         </fieldset>
                                                     </ContentTemplate>
                                                 </asp:UpdatePanel>
                                             </asp:Panel>
                                             <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                                                 BackgroundCssClass="modalBackground" CancelControlID="Button2" Drag="true" 
                                                 PopupControlID="Panel6" TargetControlID="Hidden2">
                                             </cc1:ModalPopupExtender>
                                             <input id="Hidden2" runat="Server" name="Hidden2" type="hidden" style="width: 4px" />
                                         </td>
                                     </tr>
                                 </table>
                             </asp:Panel>
                             <asp:Panel ID="Panel7" runat="server">
                                 <table width="100%">
                                     <tr>
                                         <td>
                                             <asp:Panel ID="Panel8" runat="server" BackColor="White" BorderColor="#999999" 
                                                 BorderStyle="Solid" BorderWidth="2px" ScrollBars="None" Width="45%">
                                                 <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                     <ContentTemplate>
                                                         <fieldset>
                                                             <div style="text-align: center;">
                                                                 <br />
                                                                 <asp:Image ID="Image3" runat="server" ImageUrl="~/images/Trustedsiteadd.png" />
                                                             </div>
                                                             <div style="clear: both;">
                                                             </div>
                                                             <div style="text-align: center">
                                                                 <asp:Button ID="Button3" runat="server" Font-Bold="true" Text="Close" 
                                                                     Width="50px" />
                                                             </div>
                                                         </fieldset>
                                                     </ContentTemplate>
                                                 </asp:UpdatePanel>
                                             </asp:Panel>
                                             <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" 
                                                 BackgroundCssClass="modalBackground" CancelControlID="Button3" Drag="true" 
                                                 PopupControlID="Panel8" TargetControlID="Hidden3">
                                             </cc1:ModalPopupExtender>
                                             <input id="Hidden3" runat="Server" name="Hidden3" type="hidden" style="width: 4px" />
                                         </td>
                                     </tr>
                                 </table>
                             </asp:Panel>
                             <asp:Panel ID="Panel9" runat="server">
                                 <table width="100%">
                                     <tr>
                                         <td>
                                             <asp:Panel ID="Panel10" runat="server" BackColor="White" BorderColor="#999999" 
                                                 BorderStyle="Solid" BorderWidth="2px" ScrollBars="None" Width="45%">
                                                 <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                     <ContentTemplate>
                                                         <fieldset>
                                                             <div style="text-align: center;">
                                                                 <br />
                                                                 <asp:Image ID="Image4" runat="server" ImageUrl="~/images/Trustedsite.png" />
                                                             </div>
                                                             <div style="clear: both;">
                                                             </div>
                                                             <div style="text-align: center">
                                                                 <asp:Button ID="Button4" runat="server" Font-Bold="true" Text="Close" 
                                                                     Width="50px" />
                                                             </div>
                                                         </fieldset>
                                                     </ContentTemplate>
                                                 </asp:UpdatePanel>
                                             </asp:Panel>
                                             <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" 
                                                 BackgroundCssClass="modalBackground" CancelControlID="Button4" Drag="true" 
                                                 PopupControlID="Panel10" TargetControlID="Hidden4">
                                             </cc1:ModalPopupExtender>
                                             <input id="Hidden4" runat="Server" name="Hidden4" type="hidden" style="width: 4px" />
                                         </td>
                                     </tr>
                                 </table>
                             </asp:Panel>
                             <p>
                             </p>
                             <p>
                             </p>
                        </p>
                    </div>
                </div>
             
            </div>
        </div>
    </asp:Panel>
    <asp:Label ID="Label5" ForeColor="Red" runat="server" ></asp:Label>

      <asp:Panel ID="pnl_home" runat="server">
        <div class="container">
            <!-- Codrops top bar -->
            <div class="codrops-top">
            </div>
            <!--/ Codrops top bar -->
            <div id="container_demo">
                <a class="hiddenanchor" id="A1"></a><a class="hiddenanchor" id="A2">
                </a>
               
                <div id="wrapper">
                    <div id="login" class="animate form">
                        <asp:Label ID="Label4" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                        <h1>
                            Welcome to Ijobcenter
                           
                        </h1>
                        <p>
                            
                        </p>
                        <p class="button">
                            <label for="username" class="uname">
                                LogIn as...
                            </label>
                        </p>
                        
                         <p class="button">
                              <asp:Button ID="btn_logincom" runat="server" Text="Company" OnClick="btn_logincom_Click" CssClass="button"
                                ValidationGroup="1" TabIndex="5" Width="100%" />
                        
                         </p>
                        <p class="button"  Width="100%">
                            <label for="username" class="uname">
                              
                            </label>
                            <asp:Button ID="btn_logincandi" runat="server" Text="Candidate" OnClick="btn_logincandi_Click"
                                ValidationGroup="1" TabIndex="5" Width="100%" />
                           
                        </p>
                         <p class="button"  Width="100%">
                            <label for="username" class="uname">
                              
                            </label>
                            <asp:Button ID="Button5" runat="server" Text="Other" OnClick="btn_loginOther_Click"
                                ValidationGroup="1" TabIndex="5" Width="100%" />
                           
                        </p>
                        <p>
                           
                        </p>
                        <p>
                           
                        </p>
                        
                        
                    </div>
                </div>
             
            </div>
        </div>
    </asp:Panel>

    <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" Text="V5" ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>
       </form>
</asp:Content>
