<%@ Page Title="" Language="C#" MasterPageFile="~/MobileIJOB.master" AutoEventWireup="true" CodeFile="Moborder_now.aspx.cs" Inherits="Moborder_now" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<h2></h2>
     <div class="panel-group">

    <div class="panel panel-primary">
      <div class="panel-heading">Selected Plan Detail</div>
      <div class="panel-body">
      <div class="form-group">
      <label for="portalname">Portal-Product/Version Name:</label>
    <asp:Label runat="server" ForeColor="#5badff" ID="Label4" Text="">
                                </asp:Label>
                                <asp:Label runat="server" ForeColor="#5badff" ID="lblproductId" Text="">
                                </asp:Label>
                                <asp:Label ID="ddlVersion" Text="test" ForeColor="#5badff" runat="server"></asp:Label>
                          
    </div>
    <div class="form-group">
      <label for="portalname">Subscription Plan:</label>
    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="#5badff" OnClick="LinkButton1_Click"> </asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" ForeColor="#999999" runat="server" Font-Italic="true"
                                    OnClick="LinkButton2_Click">Change Plan</asp:LinkButton>
                                    <asp:Label ID="lblPricePlanId" runat="server" Visible="False"></asp:Label>
    </div>
    <div class="form-group">
      <label for="portalproduct">Portal-Product/Version Name:</label>
     <asp:Label ID="Label2" ForeColor="#5badff" runat="server" Text="$">
                                </asp:Label>
                                <asp:Label ID="lblamt" ForeColor="#5badff" runat="server" Text="">
                                </asp:Label>
    </div>
      </div>
    </div>
    <div class="panel panel-primary">
      <div class="panel-heading">Company Information</div>
      <div class="panel-body">
       <div class="form-group">
      <label for="cname">Company Name:</label>
        <asp:TextBox ID="txtcompanyname" runat="server" CssClass="form-control" 
               placeholder="Enter Company Name" required></asp:TextBox>
    </div>
     <div class="form-group">
      <label for="cname">Contact Person name:</label>
        <asp:TextBox ID="txtcontactprsn" runat="server" CssClass="form-control" 
             placeholder="Enter Contact Person name" required></asp:TextBox>
    </div>
     <div class="form-group">
      <label for="cname">Designation:</label>
        <asp:TextBox ID="txtcontactprsndsg" runat="server" CssClass="form-control" 
             placeholder="Enter Designation" required></asp:TextBox>
    </div>

     <div class="form-group">
      <label for="cname">Email:</label>
        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" 
             placeholder="Enter Email" required></asp:TextBox>
    </div>
     <div class="form-group">
      <label for="cname">Address:</label>
        <asp:TextBox ID="txtadd" runat="server" TextMode="MultiLine" 
             CssClass="form-control" placeholder="Enter Address" required></asp:TextBox>
    </div>
    <div class="form-group">
      <label for="cname">Country:</label>
        <asp:DropDownList ID="ddlcountry" runat="server" CssClass="form-control" 
            onselectedindexchanged="ddlcountry_SelectedIndexChanged" 
            AutoPostBack="True">
        </asp:DropDownList>
           </div>
           <div class="form-group">
      <label for="cname">State:</label>
        <asp:DropDownList ID="ddlstate" runat="server" CssClass="form-control">
        </asp:DropDownList>
           </div>
           <div class="form-group">
      <label for="cname">City:</label>
               <asp:TextBox ID="txtcity" runat="server" CssClass="form-control"></asp:TextBox>
           </div>
              <div class="form-group">
      <label for="cname">Zip Code:</label>
      <asp:TextBox ID="txtzipcode" runat="server" CssClass="form-control" 
                      placeholder="Enter Zip Code" required></asp:TextBox>
           </div>
              <div class="form-group">
      <label for="cname">Phone No  with  countrycode:</label>
    <asp:TextBox ID="txtphn" runat="server" CssClass="form-control" placeholder="Enter Phone No" 
                      required></asp:TextBox>
           </div>
               <div class="form-group">
      <label for="cname">Mobile :</label>
    <asp:TextBox ID="txtmobileno" runat="server" CssClass="form-control" 
                       placeholder="Enter Mobile " required></asp:TextBox>
           </div>
               <div class="form-group">
      <label for="cname">Company ID:</label>
    <asp:TextBox ID="txtcompanyid" runat="server" CssClass="form-control" 
                       placeholder="Enter Company ID" required AutoPostBack="True" 
                       ontextchanged="txtcompanyid_TextChanged"></asp:TextBox>
                        <asp:Label ID="lblCompanyIDAVl" runat="server"></asp:Label>
           </div>
               <div class="form-group">
      <label for="cname">Admin UserID:</label>
    <asp:TextBox ID="txtadminuserid" runat="server" CssClass="form-control" 
                       placeholder="Enter Admin UserID" required></asp:TextBox>
           </div>
             <div class="form-group">
      <label for="cname">Password :</label>
    <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" 
                     placeholder="Enter Password" TextMode="Password" required></asp:TextBox>
           </div>
             <div class="form-group">
      <label for="cname">Confirm Password :</label>
    <asp:TextBox ID="txtcnfrmpassword" runat="server" CssClass="form-control" 
                     placeholder="Enter Confirm Password" TextMode="Password" required></asp:TextBox>
           </div>

              <div class="form-group">
      
         
         <label for="Etext">Fill up Captcha Code Text:</label>
        <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control" required></asp:TextBox>
        </div>
        <div class="form-group">
       <div class="row">
        <div class="col-sm-3">
       <%-- <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
             CaptchaMinTimeout="5" CaptchaMaxTimeout="240" BorderWidth="100px"
            FontColor="#D20B0C" NoiseColor="#B1B1B1" />--%>
            </div>
             <div class="col-sm-3">
              <asp:ImageButton ID="ImageButton1" ImageUrl="~/image/refresh.png" runat="server" CausesValidation="false" />
            <%-- <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Invalid. Please try again." OnServerValidate="ValidateCaptcha"
            runat="server" ForeColor="Red"/>--%>
            </div>
           </div> 

        </div>
            <div class="checkbox">
    <label><asp:CheckBox ID="Chk" runat="server" Text="I Accept the Terms and Condition"></asp:CheckBox>
    I Accept the Terms and Conditions</label>
      <asp:Panel runat="server" ScrollBars="Auto" Height="70px" ID="pnlTermsCondition">
                        <asp:Label ID="lbltermsofuse" runat="server"></asp:Label>
                    </asp:Panel>
  </div>

  <div class="form-group">

 
    <asp:Button ID="btncontinue" runat="server" class="btn btn-success" Text="Submit" 
          onclick="btncontinue_Click" />

   
    <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" class="btn btn-danger" Text="Reset" />
  
    </div>
     <div class="form-group">
         <asp:Label ID="lblmsg" runat="server" Text="Label" Font-Bold="True" 
             Font-Size="12pt" ForeColor="Red"></asp:Label>
     </div>
      </div>
    </div>
   
  </div>
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
   <input id="hdnProductId" name="hdnProductId" runat="server" type="hidden" />
   <asp:TextBox ID="Textpaypal" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="Textcancel" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="Textreturn" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="Textnotify" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtpaymentnotifyurl" runat="server" Width="154px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txthostedsite" runat="server" Width="154px" Visible="false"></asp:TextBox>

  </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

