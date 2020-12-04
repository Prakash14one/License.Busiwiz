 <%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="RetailInvoice_Edit.aspx.cs" Inherits="RetailInvoice_Edit"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function confirmFn() {
            document.getElementById("hdnValue").value = confirm("Are you sure?");
        }




        function itemUpdate() {

            var amt = document.getElementById("amount");
            var t = document.getElementById("ctl00_contentMasterPage_TextBox1");
            amt.value = t.value;


            //   var order=document.getElementById("item_number");
            //   var a=document.getElementById("ctl00_contentMasterPage_txtorder");
            //   order.value=a.value;


            //   
            //   var name=document.getElementById("first_name");
            //   var b=document.getElementById("ctl00_contentMasterPage_txtname");
            //   name.value=b.value;

            //    var lname=document.getElementById("last_name");
            //   var c=document.getElementById("ctl00_contentMasterPage_txtname");
            //   lname.value=c.value;
            //   
            //    var address=document.getElementById("address1");
            //   var d=document.getElementById("ctl00_contentMasterPage_txtAddress");
            //   address.value=d.value;
            //   
            //    var city=document.getElementById("city");
            //   var e=document.getElementById("ctl00_contentMasterPage_txtCitypay");
            //   city.value=e.value;
            //   
            //    var state=document.getElementById("state");
            //   var f=document.getElementById("ctl00_contentMasterPage_txtstate");
            //   state.value=f.value;
            //   
            //    var zip=document.getElementById("zip");
            //   var g=document.getElementById("ctl00_contentMasterPage_txtzip");
            //   zip.value=g.value;
            //   
            //    var phone=document.getElementById("night_phone_a");
            //   var h=document.getElementById("ctl00_contentMasterPage_txtphone");
            //   phone.value=h.value;


            return;
        }


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
        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
            }
        }      
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <div style="padding-left: 0%">
                        <label>
                            <asp:Label ID="lblqty" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            <asp:Label ID="lblmsg1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <label>
                            <asp:Panel ID="pnnl" Visible="false" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td colspan="4" align="left">
                                            Create cash sales invoice entry for the following documents
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            Doc ID :
                                            <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width: 25%">
                                            Title :
                                            <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width: 25%">
                                            Date
                                            <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width: 25%">
                                            Cabinet/Drawer/Folder :
                                            <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <fieldset>
                        <div>
                            <label>
                                <asp:Label ID="Label4" runat="server" Text="Business Name"> </asp:Label>
                            </label>
                            <label>
                                <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" Enabled="False">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="Label35" runat="server" Text="Sales Invoice Number "> </asp:Label>
                            </label>
                              <label>
                                <asp:Label ID="lblinvoiceno" runat="server" Text=""> </asp:Label>
                            </label>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <div>
                            <label>
                                <asp:Label ID="Label5" runat="server" Text="Payment Option"> </asp:Label>
                            </label>
                            <asp:Panel ID="Panel2" runat="server">
                                <asp:RadioButtonList ID="rbt_pay_method" runat="server" OnSelectedIndexChanged="rbt_pay_method_SelectedIndexChanged"
                                    RepeatDirection="Horizontal" AutoPostBack="True" AppendDataBoundItems="True">
                                </asp:RadioButtonList>
                            </asp:Panel>
                        </div>
                         <div style="clear: both;">
                        </div>
                       <asp:Panel ID="pnlamtdue" runat="server" Enabled="true">
                          <label>
                                                <asp:Label ID="Label46" runat="server" Text="Number of Credit Days"> </asp:Label>
                                                <asp:RequiredFieldValidator ID="ReqdFieldValidator4" runat="server" ControlToValidate="txtnumberofduedate"
                                                    ErrorMessage="*" ValidationGroup="4"> </asp:RequiredFieldValidator>
                                                  </label>
  <label>                                                
                            <asp:TextBox ID="txtnumberofduedate" 
                            runat="server" Width="100px" AutoPostBack="True" ontextchanged="txtGoodsDate_TextChanged"
>0</asp:TextBox>
                                           
                                            </label>
                       <label>
                                                <asp:Label ID="Label41" runat="server" Text="Payment Due Date"> </asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpayduedate"
                                                    ErrorMessage="*" ValidationGroup="4"> </asp:RequiredFieldValidator>
                                                  </label>
  <label>                                                
                            <asp:TextBox ID="txtpayduedate" runat="server" 
                            Width="100px" Enabled="False" ></asp:TextBox>
                                               
                                            </label>
                                            <label> <cc1:MaskedEditExtender ID="MaskedEdExtender2" runat="server" CultureName="en-AU"
                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtpayduedate" /></label>
   </asp:Panel>
                    </fieldset>
                   
                      <div style="clear: both;">
                    </div>
                    <div>
                        <asp:Panel ID="pnlCash" runat="server" Visible="false" Width="100%">
                            <fieldset>
                                <legend>Select Bank Account </legend>
                                <div>
                                    <label>
                                        <asp:Label ID="Label20" runat="server" Text="Bank Account"></asp:Label>
                                    </label>
                                    <label>
                                     
                                                <asp:DropDownList ID="ddlCash" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            
                                    </label>
                                     <label>
                        
                                 <asp:ImageButton ID="lnkadd" runat="server" Height="15px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                    Width="20px" ToolTip="AddNew" OnClick="LinkButton97666667_Click" />
                          </label>
                          <label>
                        
                            <asp:ImageButton ID="lnkadd0" runat="server" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                    OnClick="lnkadd0_Click" AlternateText="Refresh" Height="15px" Width="20px"
                                    ToolTip="Refresh" />
                       
                </label>
                <label><asp:Button ID="btncheque" runat="server" Visible="false" Text="Add Cheque Details" 
                                        onclick="btncheque_Click"  /> </label>
                                   
                                </div>
                            </fieldset></asp:Panel>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <asp:Panel ID="Panel1" runat="server" Visible="False" Width="100%">
                            <fieldset>
                                <legend>Add Cheque Details </legend>
                                <div>
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Cheque No."> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtChequeNo"
                                            ErrorMessage="*" ValidationGroup="4"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtChequeNo" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" TargetControlID="txtChequeNo" ValidChars="0147852369">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Bank Name"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtbankname"
                                            ErrorMessage="*" ValidationGroup="4"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtbankname" runat="server"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Transit ID"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="txtTransitId"
                                            ErrorMessage="*" ValidationGroup="4"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtTransitId" runat="server"></asp:TextBox>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Branch Name"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="txtBranchname"
                                            ErrorMessage="*" ValidationGroup="4"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtBranchname" runat="server"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text="Branch Country"> </asp:Label>
                                        <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Branch State/Province"> </asp:Label>
                                        <asp:DropDownList ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged1">
                                        </asp:DropDownList>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label17" runat="server" Text="Branch City"></asp:Label>
                                        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="txtCity"
                                            ErrorMessage="*" ValidationGroup="4"></asp:RequiredFieldValidator>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label18" runat="server" Text="Branch Zip/Postal Code"></asp:Label>
                                        <asp:TextBox ID="txtZipcode" runat="server" onkeypress="return RealNumWithDecimal(this,event,2);"></asp:TextBox>
                                    </label>
                                </div>
                            </fieldset></asp:Panel>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <asp:Panel ID="pnlCredit" runat="server" Visible="False" Width="100%">
                            <fieldset>
                                <legend>Add Credit Card </legend>
                                <div>
                                    <label>
                                        <asp:Label ID="Label19" runat="server" Text="First Name"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" ControlToValidate="txtFName"
                                            ErrorMessage="*" ValidationGroup="4"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtFName" runat="server"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label21" runat="server" Text="Last Name"></asp:Label>
                                        <asp:TextBox ID="txtLname" runat="server"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label29" runat="server" Text="Phone No."></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ControlToValidate="txtPhonenoCreditCard"
                                            ErrorMessage="*" ValidationGroup="4"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                            TargetControlID="txtPhonenoCreditCard" ValidChars="0147852369+-()">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtPhonenoCreditCard" runat="server"></asp:TextBox>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label24" runat="server" Text="Credit Card No."></asp:Label>
                                        <asp:TextBox ID="txtCCno" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            TargetControlID="txtCCno" ValidChars="0147852369-">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="txtCCno"
                                            ErrorMessage="*" ValidationGroup="4"></asp:RequiredFieldValidator>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label25" runat="server" Text="Security Code "></asp:Label>
                                        <asp:TextBox ID="txtSecureCodeForCC" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ControlToValidate="txtSecureCodeForCC"
                                            ErrorMessage="*" ValidationGroup="4"></asp:RequiredFieldValidator>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label28" runat="server" Text="Expiry Date"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ControlToValidate="txtYear"
                                            ErrorMessage="*" ValidationGroup="4"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtYear" runat="server" MaxLength="4"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtExpDate"
                                            TargetControlID="txtYear">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1534" runat="server" CultureName="en-AU"
                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtYear" />
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label31" runat="server" Text="Country"></asp:Label>
                                        <asp:DropDownList ID="ddlcountryCR" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcountryCR_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ControlToValidate="ddlcountryCR"
                                            ErrorMessage="*" ValidationGroup="4" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label32" runat="server" Text="State/Province"></asp:Label>
                                        <asp:DropDownList ID="ddlstateCR" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstateCR_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server" ControlToValidate="ddlstateCR"
                                            ErrorMessage="*" InitialValue="0" ValidationGroup="4"></asp:RequiredFieldValidator>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label26" runat="server" Text="City"></asp:Label>
                                        <asp:DropDownList ID="ddlcityCR" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="ddlcityCR"
                                            ErrorMessage="*" InitialValue="0" ValidationGroup="4"></asp:RequiredFieldValidator>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label30" runat="server" Text="Address"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtaddresscredit" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ControlToValidate="txtaddresscredit"
                                            ErrorMessage="*" ValidationGroup="4"></asp:RequiredFieldValidator>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label27" runat="server" Text="Zip/Postal Code"></asp:Label>
                                        <asp:TextBox ID="txtZipcodecredit" runat="server" onkeypress="return RealNumWithDecimal(this,event,2);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequirldValidator4" runat="server" ControlToValidate="txtZipcodecredit"
                                            ErrorMessage="*" ValidationGroup="4" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </label>
                                </div>
                            </fieldset></asp:Panel>
                    </div>
                     <div style="clear: both;">
                    </div>
                     <fieldset>
                     
                                        <div>
                                        
                                      
                                          <label>
                                                <asp:Label ID="Label34" runat="server" Text="Party"></asp:Label>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlParty"
                                                    ErrorMessage="*" InitialValue="0" ValidationGroup="4"></asp:RequiredFieldValidator>
                                               
                                               </label> 
                                               <label>
                                              
                                                <asp:DropDownList ID="ddlParty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlParty_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label6" runat="server" Text="Date"> </asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtGoodsDate"
                                                    ErrorMessage="*" ValidationGroup="4"> </asp:RequiredFieldValidator>
                                                  </label>
  <label>                                                <asp:TextBox ID="txtGoodsDate" runat="server" Width="100px"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtGoodsDate" />
                                            </label>
                                            <label>
                                              
                                                <asp:ImageButton ID="imgCal" OnClick="imgCal_Click" runat="server" ImageUrl="~/images/calender.jpg">
                                                </asp:ImageButton>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label33" runat="server" Text="Sales Person"></asp:Label>
                                              </label>  <label>
                                                 <asp:DropDownList ID="ddlsalespersion" runat="server" AutoPostBack="True" >
                                                </asp:DropDownList>
                                              
                                            </label>
                                          
                                          
                                        </div>
                                          <div style="clear: both;">
                    </div>
                    <table width="100%">
                                         <div style="clear: both;">
                                               <tr>
                                    <td  valign="top">
                                    
                            <asp:Panel ID="pnlBillAddress" runat="server" Visible="true">
                                <table id="tblship0" runat="server" >
                                 
                                    <tr>
                                        <td >
                                           <label> Billing Address </label></td>
                                        <td  colspan="2">
                                        
                                            <asp:Button ID="btAddBill" runat="server"  CssClass="btnSubmit"
                                             Text="Change" onclick="btAddBill_Click"   />
                                          </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            </td>
                                        <td>
                                        
                                            <asp:Label ID="lblName" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                             <asp:TextBox ID="txtNameb" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
                                        </td>
                                          </tr>
                                          <tr>
                                          <td >
                                            </td>
                                              <td>
                                               <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                              </td>
                                           <td>
                                             <asp:TextBox ID="TxtEmailb" runat="server" Visible="false" MaxLength="30"></asp:TextBox>
                                            </td>
                                           </tr>
                                            <tr>
                                            <td >
                                            </td>
                                              <td>
                                                <asp:Label ID="lblShippingAdd" runat="server"></asp:Label>
                                      
                                              </td>
                                           <td>
                                              <asp:TextBox ID="txtShippingAddb" runat="server" Visible="false" MaxLength="300"></asp:TextBox>
                                   
                                            </td>
                                           </tr>  
                                                           
                                           <tr>
                                           <td >
                                            </td>
                                              <td>
                                                 <asp:Label ID="lblCity" runat="server" Visible="true"></asp:Label>
                                      
                                              </td>
                                           <td>
                                             <asp:TextBox ID="txtCityb" runat="server" Visible="false" MaxLength="30"></asp:TextBox>
                                     
                                            </td>
                                           </tr>  
                                             
                                          
                                                  
                                             <tr>
                                             <td >
                                            </td>
                                              <td>
                                              <asp:Label ID="lblState" runat="server"></asp:Label>
                                      
                                              </td>
                                           <td>
                                            <asp:TextBox ID="txtStateb" runat="server" Visible="false" MaxLength="30"></asp:TextBox>
                                          
                                            </td>
                                           </tr>  
                                          
                                           
                                              
                                           <tr>
                                           <td >
                                            </td>
                                              <td>
                                                <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                              </td>
                                           <td>
                                               <asp:TextBox ID="txtCountryb" runat="server" Visible="false" MaxLength="30"></asp:TextBox>
                                            
                                   </td>
                                           </tr>  
                                          
                                          
                                                      <tr>
                                                      <td >
                                            </td>
                                              <td>
                                              <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                              </td>
                                           <td>
                                              <asp:TextBox ID="txtPhoneb" runat="server" Visible="false" MaxLength="30"></asp:TextBox>
                                                  
                                   </td>
                                           </tr>  
                                            
                                           
                                                  
                                            <tr>
                                            <td >
                                            </td>
                                              <td>
                                               <asp:Label ID="lblzip" runat="server"></asp:Label>
                                              </td>
                                           <td>
                                             <asp:TextBox ID="txtzipb" runat="server" Visible="false" MaxLength="30"></asp:TextBox>                
                                   </td>
                                           </tr>  
                                         
                                          
                                           
                                        
                                        
                                </table>
                            </asp:Panel>
                                    </td>
                                    <td valign="top">
                            <asp:Panel ID="pnlShippAddress" runat="server" Visible="true">
                                <table runat="server">
                                   
                                    <tr>
                                        <td>
                                           
                                             <label>   <asp:Label ID="lblshippingtitle" runat="server" Text="Shipping address is the same as the billing address"></asp:Label></label>
                                </td>
                                        <td  colspan="2"> <asp:CheckBox ID="chkbill" runat="server" Checked="true" AutoPostBack="True" 
                                                oncheckedchanged="chkbill_CheckedChanged" />
                                                <label>
                                                <asp:Button ID="btnship" runat="server"  CssClass="btnSubmit" Visible="false"
                                             Text="Save" onclick="btnship_Click"   />
                                             </label>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                          
                                           
                                        </td>
                                        <td  >
                                         
                                            <asp:Label ID="lblName1" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                              <asp:TextBox ID="txtName1" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
                                              </td>
                                             </tr>
  <tr>
                                        <td align="left" valign="top">
                                         </td>
                                        <td  >
                                         
                                             <asp:Label ID="lblEmail1" runat="server"></asp:Label> 
                                            </td>
                                            <td>
                                           <asp:TextBox ID="txtEmail1" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
                                              </td>
                                             </tr>
                                      
                                        <tr>
                                        <td align="left" valign="top" style="height: 28px">
                                         </td>
                                        <td style="height: 28px"  >
                                         
                                        <asp:Label ID="lblShippingAdd1" runat="server"></asp:Label> 
                                            </td>
                                            <td style="height: 28px">
                                          <asp:TextBox ID="txtShippingAdd1" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
                                              </td>
                                             </tr>
                                           
                                           
                                            <tr>
                                        <td align="left" valign="top">
                                         </td>
                                        <td  >
                                         
                                          <asp:Label ID="lblCity1" runat="server" Visible="true"></asp:Label> 
                                            </td>
                                            <td>
                                        <asp:TextBox ID="txtCity1" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
                                              </td>
                                             </tr>
                                       
                                           
                                          <tr>
                                        <td align="left" valign="top">
                                         </td>
                                        <td  >
                                         
                                           <asp:Label ID="lblState1" runat="server"></asp:Label> 
                                            </td>
                                            <td>
                                        <asp:TextBox ID="txtState1" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
                                              </td>
                                             </tr>
                                           
                                               <tr>
                                        <td align="left" valign="top">
                                         </td>
                                        <td  >
                                         
                                       <asp:Label ID="lblCountry1" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                          <asp:TextBox ID="txtCountry1" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
                                              </td>
                                             </tr> 
                                              <tr>
                                        <td align="left" valign="top">
                                         </td>
                                        <td  >
                                         
                                       <asp:Label ID="lblPhone1" runat="server"></asp:Label> 
                                            </td>
                                            <td>
                                        <asp:TextBox ID="txtPhone1" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
                                              </td>
                                             </tr> 
                                          
                                          
                                          <tr>
                                        <td align="left" valign="top">
                                         </td>
                                        <td  >
                                         
                                       <asp:Label ID="lblzip1" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                       <asp:TextBox ID="txtzip1" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
                                              </td>
                                             </tr> 
                                           
                                            
                                            
                                </table>
                            </asp:Panel>
                                    </td>
                                </tr>
                                </div>
                                </table>
                     
                     
                      <div style="clear: both;">
                    </div>
                                          <label>
                                                <asp:Label ID="Label37" runat="server" Text="Terms"></asp:Label>
                                      </label><label>
                                                <asp:TextBox ID="txtterms" runat="server" MaxLength="500" ></asp:TextBox>
                                                
                                            </label>
                                            <label>
                                                <asp:Label ID="Label38" runat="server" Text="Purchase Order"> </asp:Label>
                                               </label> <label>
                                                <asp:TextBox ID="txtperchaseorder" runat="server" MaxLength="15" ></asp:TextBox>
                                            </label>
                                 
                    <div style="clear: both;">
                        <label> <asp:Label ID="Label40" runat="server" Text="Shipping Info"></asp:Label></label>
                        <asp:CheckBox ID="chkshipinfo" runat="server" AutoPostBack="True" 
                            oncheckedchanged="chkshipinfo_CheckedChanged" /> 
                    </div>
                
                     <div style="clear: both;">
                     
                      <asp:Panel ID="pnlshipinfo" runat="server" Visible="false">
                                                              
                                                          
                                                               <label>
                                                                    <asp:Label ID="Label43" runat="server" Text="Transporter Name"></asp:Label>
                                                               </label><label>
                                                                            <asp:DropDownList ID="ddlTransporter" runat="server">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                        <label>
                                                                            <asp:ImageButton ID="ImageButton1" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                                OnClick="imgadddivision_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                                                        </label>
                                                                        <label>
                                                                            <asp:ImageButton ID="ImageButton3" runat="server" AlternateText="Refresh" Height="20px"
                                                                                ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                                                                ImageAlign="Bottom" OnClick="ImageButton3_Click" />
                                                                        </label>
                                                                    <label> 
                                                                    <asp:Label ID="Label44" runat="server" Text="Tracking No."></asp:Label>
                                                                               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                                                        ControlToValidate="txtTrackingNo" ValidationGroup="11"></asp:RegularExpressionValidator>
                                                                
                                                               </label> <label>
                                                                    <asp:TextBox ID="txtTrackingNo" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'div2',15)"
                                                                        runat="server" Text="0" MaxLength="15" Width="100px"></asp:TextBox>
                                                                 </label>   <label> 
                                                                
                                                                    <asp:Label ID="Label39" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                    <span id="div2" class="labelcount">15</span>
                                                                    <asp:Label ID="Label47" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                                                </label>
                                                          
                                                               
                                                                   
                                                               </asp:Panel>
                                                                </div>
                                                                  <div style="clear: both;">
                    </div>
                                                           
                </fieldset>
                <fieldset>
                    <div>
                        <table style="width: 100%">
                           <tr>
                           <td>
                             <fieldset>
                             <legend><asp:Label ID="lbladdinv" runat="server" Text=""></asp:Label> </legend>
                             <table>
                             <tr>
                              <td>
                                                <label>
                                                <asp:Label ID="Label2" runat="server" Text="Add to Invoice "></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                               
                                                    <asp:RadioButtonList ID="rdinvoice" runat="server" AutoPostBack="True" 
                                                       
                                                        RepeatDirection="Horizontal" 
                                                        onselectedindexchanged="rdinvoice_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">Product</asp:ListItem>
                                                       
                                                        <asp:ListItem Value="2">Service</asp:ListItem>
                                                    </asp:RadioButtonList>
                                               
                                            </td>
                             </tr>
                             </table>
                             </fieldset>
                           </td>
                           </tr>
                          
                          <tr>
                           <td>
                           <asp:Panel ID="pnlinv" runat="server" Visible="false">
                           <table style="width: 100%">
                            <tr>
                                <td>
                                    <fieldset>
                                    <table width="100%">
                                    <tr>
                                    <td>
                                    <table>
                                      <tr>
                                            <td>
                                                <label>
                                                <asp:Label ID="Label22" runat="server" Text="Search by "></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <asp:Panel ID="Panel4" runat="server">
                                                    <asp:RadioButtonList ID="rbList" runat="server" AutoPostBack="True" 
                                                        OnSelectedIndexChanged="rbList_SelectedIndexChanged" 
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="2" Selected="True">Category</asp:ListItem>
                                                        <%--     <asp:ListItem Value="1">Using Barcode</asp:ListItem>--%>
                                                        <asp:ListItem >Name/Product No./Barcode</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </asp:Panel>
                                            </td>
                                            <td> <asp:CheckBox ID="chkdefchk" runat="server" Text="" />
                                            </td><td>
                                            <label><asp:Label ID="lbllab" runat="server" Text="Remember my selection"></asp:Label></label>
                                           
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch" 
                                                    Visible="false">
                                                    <label>
                                                    <asp:TextBox ID="txtsearch" runat="server" 
                                                       ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                        ControlToValidate="txtsearch" ErrorMessage="*" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <label>
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btnSubmit" 
                                                        OnClick="btnSearch_Click" Text="Search" ValidationGroup="1" />
                                                    </label>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                    </td>
                                    </tr>
                                      
                                         <tr>
                                <td>
                                   <asp:Panel ID="PnlCategory" runat="server" DefaultButton="BtnAdd1" Width="100%">
                               
                                            <div>
                                 <table >
                                 <tr>
                                 <td> <label>
                                                  <asp:Label ID="lblcathead" runat="server" Text="Category"></asp:Label>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label></td>
                                                <td>
                                                
                                               <label>
                                                     <asp:Label ID="lblItemHead" runat="server" Text="Item"></asp:Label> 
                                                    <asp:DropDownList ID="ddlItem" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>  </td>
                                                <td>
                                                     <label>
                                                   <asp:Label ID="lblitemnohead" runat="server" Text="Product No."></asp:Label>   
                                                    <asp:TextBox ID="txtProdNo" runat="server" Width="80px" Enabled="False"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" ValidationGroup="3"
                                                        ErrorMessage="*" ControlToValidate="txtProdNo" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </label>
                                                </td>
                                                <td>
                                                    <asp:Panel ID="pnlunit" runat="server" >
                                                <label>
                                                    Unit
                                                    <asp:TextBox ID="txtUnit" runat="server" Width="45px" Enabled="False"></asp:TextBox>
                                                </label>
                                             
                                                <label>
                                                    Unit Type
                                                    <asp:DropDownList ID="ddlunit" runat="server" Width="70px" OnSelectedIndexChanged="ddlunit_SelectedIndexChanged"
                                                        Enabled="False">
                                                    </asp:DropDownList>
                                                </label>
                                                   </asp:Panel>
                                                </td>
                                                <td>
                                                 <label> <asp:Label ID="lblorderqty" runat="server" Text="Qty Order"></asp:Label>   
                                                    
                                                    <asp:TextBox ID="txtQty" runat="server" Width="60px"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender354534" runat="server" Enabled="True"
                                                        TargetControlID="txtQty" ValidChars="0147852369.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="3"
                                                        ErrorMessage="*" ControlToValidate="txtQty" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </label>
                                                </td>
                                                <td>     <label>
                                                   Sales Price
                                                    <br />
                                                    <asp:LinkButton ID="linkprice" runat="server" OnClick="lblRate_Click" ForeColor="Black"
                                                        Text="0.00"></asp:LinkButton>
                                                </label></td>
                                               
                                                <td>     <label>
                                                   Actual Price
                                                  
                                                    <asp:TextBox ID="lblRate" runat="server" 
                                                        Text="0.00" Width="70px"></asp:TextBox>
                                                           <cc1:FilteredTextBoxExtender ID="FilterextBoxExtender3" runat="server" Enabled="True"
                                                        TargetControlID="lblRate" ValidChars="0147852369.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequFieldValidator4" runat="server" ValidationGroup="3"
                                                        ErrorMessage="*" ControlToValidate="lblRate" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </label></td>
                                                <td>
                                                <asp:Panel ID="pnlpro" runat="server" Visible="false">
                                                     <label>
                                                    Promo Rate
                                                    <br />
                                                    <asp:Label ID="lblPromoRate" runat="server" Text="0.00"></asp:Label>
                                                </label>
                                                </asp:Panel>
                                                </td>
                                                <td>
                                                    <asp:Panel ID="pnlbulk" runat="server" Visible="false">
                                                <label>
                                                    Bulk Rate
                                                    <br />
                                                    <asp:Label ID="lblBulkDisc" runat="server" Text="0.00"></asp:Label>
                                                </label>
                                                <label>
                                                    Bulk Qty
                                                    <br />
                                                    <asp:Label ID="lblBulkQty" runat="server" Text="0.00"></asp:Label>
                                                </label>
                                               
                                                
                                                   </asp:Panel>
                                                </td>
                                                 <td>    <asp:Panel ID="pnlavgcost" runat="server" >  <label>
                                                   Avg Rate
                                                    <br />
                                                 <asp:Label ID="lblavgRate" runat="server" 
                                                        Text="0.00"></asp:Label>
                                                </label></asp:Panel></td>
                                                <td>
                                                <asp:Panel ID="pnlqtyo" runat="server" >
                                                <label>
                                                    Qty On Hand
                                                    <br />
                                                    <asp:Label ID="lblQtyOnHand" runat="server" Text="0"></asp:Label>
                                                </label>
                                                </asp:Panel>
                                                </td>
                                               
                                                <td>  <label>
                                               
                                                    <asp:Button ID="BtnAdd1" runat="server" CssClass="btnSubmit" OnClick="BtnAdd1_Click"
                                                        Text="Add" ValidationGroup="3" Width="50px" />
                                                </label></td>
                                 </tr>
                                 </table>
                                      
                                               
                                              
                                           
                                            
                                                   
                                           
                                           
                                            
                                              
                                            
                                               
                                           
                                        
                                    </div>
                                       
                             </asp:Panel>
                               </td>
                            </tr>
                                <tr>
                                <td>
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="InventoryWarehouseMasterId"
                                        EmptyDataText="No Record" OnRowCommand="GridView2_RowCommand" Visible="false"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                        GridLines="Both" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="InventoryWarehouseMasterId" HeaderText="ID" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="category" HeaderText="Category" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="ProductNo" HeaderText="Product No." HeaderStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Unit" HeaderText="Unit" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="unitname" HeaderText="Unit Type" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender354534et" runat="server"
                                                        Enabled="True" TargetControlID="TextBox1" ValidChars="0147852369." >
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="TextBox1" runat="server" Text="1"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender354534hfghf" runat="server"
                                                        Enabled="True" TargetControlID="txtQty" ValidChars="0147852369.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="txtQty" runat="server" Width="65px" Text="1"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty On Hand" HeaderStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                  
                                                    <asp:Label ID="lblqtyonhand" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:BoundField DataField="Rate" HeaderText="Sales Rate" HeaderStyle-HorizontalAlign="Left" />
                                       <asp:TemplateField HeaderText="Actual Rate" HeaderStyle-HorizontalAlign="Left">
                                              
                                                <ItemTemplate>
                                                    <cc1:FilteredTextBoxExtender ID="FilteBoxExt354534hfghf" runat="server"
                                                        Enabled="True" TargetControlID="txtactrate" ValidChars="0147852369.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="txtactrate" runat="server" Width="65px" Text='<%#Bind("Rate") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Avg.rate" HeaderStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                  
                                                    <asp:Label ID="lblavgr" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField CommandName="add" Text="Add" ItemStyle-ForeColor="#416271" />
                                            
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                              <tr>
                            <td >
                            <asp:Panel ID="pnlprod" runat="server" Visible="false">
                            
                          
                            <table width="100%">
                            <tr>
                              <td>
                                <label><asp:Label ID="lblgrdhead" runat="server" Text="List of Products Added to Invoice"></asp:Label> </label>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" CssClass="mGrid" GridLines="Both" Width="100%"
                                        OnRowCommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting" 
                                        ShowFooter="True">
                                        <Columns>
                                            <asp:BoundField DataField="InventoryWarehouseMasterId" HeaderText="ID" Visible="false"
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Name" HeaderText="Name" 
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductNo" HeaderText="Product No." 
                                                HeaderStyle-HorizontalAlign="Left" >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                           
                                            <asp:BoundField DataField="Unit" HeaderText="Unit" 
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UnitType" HeaderText="Unit Type" 
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Ordered Qty" HeaderStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" TargetControlID="TextBox4" ValidChars="0147852369.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("OderedQty") %>' AutoPostBack="true" Width="75px" OnTextChanged="textqty_TextChanged" ></asp:TextBox>
                                                <asp:Label ID="lblinvwm" runat="server" Text='<%# Bind("InventoryWarehouseMasterId") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblPromoDis" runat="server" Text='<%# Bind("PromoDis") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblVolumeDis" runat="server" Text='<%# Bind("VolumeDis") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Rate" HeaderText="Price" 
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="promoprice" HeaderText="Promo Rate" 
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="bulkprice" HeaderText="Bulk Rate" 
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AppliedRate" HeaderText="Applied Rate" 
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" >
                                                <HeaderTemplate>
                                                    <asp:Label ID="ht1" runat="server"></asp:Label></HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltax1" runat="server"></asp:Label>
                                                    <asp:Label ID="lbltax113" runat="server" ></asp:Label>
                                                 
                                                    <asp:Label ID="lbltax112" runat="server" Text="0"></asp:Label>
                                                </ItemTemplate>
                                            
                                                <HeaderStyle HorizontalAlign="Left" />
                                            
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" >
                                                <HeaderTemplate>
                                                    <asp:Label ID="ht2" runat="server"></asp:Label></HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltax2" runat="server"></asp:Label>
                                                    <asp:Label ID="lbltax123" runat="server" ></asp:Label>
                                               
                                                    <asp:Label ID="lbltax122" runat="server" Text="0"></asp:Label>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Left" />
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" >
                                                <HeaderTemplate>
                                                    <asp:Label ID="ht3" runat="server"></asp:Label></HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltax3" runat="server"></asp:Label>
                                                    <asp:Label ID="lbltax133" runat="server" ></asp:Label>
                                                   
                                                    <asp:Label ID="lbltax132" runat="server" Text="0"></asp:Label>
                                                </ItemTemplate>
                                              
                                                <HeaderStyle HorizontalAlign="Left" />
                                              
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Total"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltot" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Total Avg Cost"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                 <asp:Label ID="lblavgrate" runat="server" Text='<%# Bind("AvgRate") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblavgcost" runat="server" Text='<%# Bind("AvgCost") %>'></asp:Label>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Markup"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmarkup" runat="server" Text='<%# Bind("Markup") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Qty On Hand" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblqtyonhand" runat="server"  Text='<%# Bind("QtyonHand") %>' Visible="true"></asp:Label>
                                                     <asp:Label ID="lblredmask" runat="server" Text="*" Visible="false" ForeColor="Red"></asp:Label>
                                                </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                 <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:ButtonField CommandName="remove" Visible="false" Text="Remove" />
                                            <asp:TemplateField HeaderText="Wid" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwhidg" runat="server" Text='<%# Bind("InventoryWarehouseMasterId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Btn" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                                            OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                                        </asp:ImageButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle Width="3%" />
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
                           
                                <tr>
                            <td >
                            <asp:Panel ID="pnlserv" runat="server" Visible="false">
                            
                          
                            <table width="100%">
                            <tr>
                              <td>
                                <label><asp:Label ID="Label45" runat="server" Text="List of Services Added to Invoice"></asp:Label> </label>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" CssClass="mGrid" GridLines="Both" Width="100%"
                                        onrowdeleting="GridView3_RowDeleting" 
                                        ShowFooter="True">
                                        <Columns>
                                            <asp:BoundField DataField="InventoryWarehouseMasterId" HeaderText="ID" Visible="false"
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Name" HeaderText="Name" 
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductNo" HeaderText="Service No." 
                                                HeaderStyle-HorizontalAlign="Left" >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                           
                                            <asp:BoundField DataField="Unit" HeaderText="Unit"  Visible="false"
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UnitType" HeaderText="Unit Type"  Visible="false"
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Number of Services Provided/Hours Ordered" HeaderStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" TargetControlID="TextBox4" ValidChars="0147852369.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("OderedQty") %>' AutoPostBack="true" Width="75px" OnTextChanged="textqty_TextChanged" ></asp:TextBox>
                                                <asp:Label ID="lblinvwm" runat="server" Text='<%# Bind("InventoryWarehouseMasterId") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblPromoDis" runat="server" Text='<%# Bind("PromoDis") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblVolumeDis" runat="server" Text='<%# Bind("VolumeDis") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Rate" HeaderText="Price" 
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="promoprice" HeaderText="Promo Rate" 
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="bulkprice" HeaderText="Bulk Rate" Visible="false"
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AppliedRate" HeaderText="Applied Rate" 
                                                HeaderStyle-HorizontalAlign="Left"  >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" >
                                                <HeaderTemplate>
                                                    <asp:Label ID="ht1" runat="server"></asp:Label></HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltax1" runat="server"></asp:Label>
                                                    <asp:Label ID="lbltax113" runat="server" ></asp:Label>
                                                 
                                                    <asp:Label ID="lbltax112" runat="server" Text="0"></asp:Label>
                                                </ItemTemplate>
                                            
                                                <HeaderStyle HorizontalAlign="Left" />
                                            
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" >
                                                <HeaderTemplate>
                                                    <asp:Label ID="ht2" runat="server"></asp:Label></HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltax2" runat="server"></asp:Label>
                                                    <asp:Label ID="lbltax123" runat="server" ></asp:Label>
                                               
                                                    <asp:Label ID="lbltax122" runat="server" Text="0"></asp:Label>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Left" />
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" >
                                                <HeaderTemplate>
                                                    <asp:Label ID="ht3" runat="server"></asp:Label></HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltax3" runat="server"></asp:Label>
                                                    <asp:Label ID="lbltax133" runat="server" ></asp:Label>
                                                   
                                                    <asp:Label ID="lbltax132" runat="server" Text="0"></asp:Label>
                                                </ItemTemplate>
                                              
                                                <HeaderStyle HorizontalAlign="Left" />
                                              
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Total"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltot" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Wid" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwhidg" runat="server" Text='<%# Bind("InventoryWarehouseMasterId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Btn" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                                            OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                                        </asp:ImageButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle Width="3%" />
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
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblMsg" runat="server" Text="If you have changed any quantities, click on the update button to view the changes."> </asp:Label>
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnSubmit" OnClick="btnUpdate_Click" />
                                    </label>
                                </td>
                            </tr>
                          
                           
                                    </table>
                                   
                                     
                                    </fieldset>
                                </td>
                            </tr>
                          
                        
                          
                            <tr>
                                <td>
                                    <fieldset>
                                     
                                            <table >
                                                <tr>
                                                    <td >
                                                        <label>
                                                            Gross Total
                                                        </label>
                                                    </td>
                                                    <td >
                                                        <label>
                                                            <asp:Label ID="lblGTotal" runat="server">0.00</asp:Label>
                                                        </label>
                                                    </td>
                                                    <td >
                                                        
                                                    </td>
                                                    <td >
                                                       
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td> <label>
                                                           <asp:Label ID="lblcusdisname" runat="server"></asp:Label>
                                                        </label></td>
                                                    </tr>
                                                <tr>
                                                    <td >
                                                   
                                                        <label>
                                                            Total Customer Discount
                                                        </label>
                                                       
                                                       
                                                       
                                                    </td>
                                                    
                                                        <td >
                                                            <label>
                                                            <asp:Label ID="lblCustDisc" runat="server">0.00</asp:Label>
                                                            </label>
                                                            
                                                     
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                   
                                                     <tr>
                                                    <td>
                                                     <label>
                                                           <asp:Label ID="lblorderdiscname" runat="server"></asp:Label>
                                                        </label>
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                            Order Value Discount
                                                            </label>
                                                           
                                                             
                                                        </td>
                                                        <td>
                                                            <label>
                                                            <asp:Label ID="lblOrderDisc" runat="server">0.00</asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                   
                                                     <tr>
                                                        <td>
                                                            <label>
                                                          Net Total
                                                            </label>
                                                              
                                                        </td>
                                                        <td>
                                                            <label>
                                                            <asp:Label ID="lblnettot" runat="server">0.00</asp:Label>
                                                            </label>
                                                        </td>
                                                        
                                                      <td></td>
                                                      <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Panel ID="pnltxt1" runat="server" Visible="false">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                            <asp:Label ID="txt1" runat="server"> </asp:Label>
                                                                            <asp:Label ID="txt1rat" runat="server" Text=""> </asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                            <asp:Label ID="txt1value" runat="server">0</asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Panel ID="pnltxt2" runat="server" Visible="false">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                            <asp:Label ID="txt2" runat="server"></asp:Label>
                                                                            <asp:Label ID="txt2rat" runat="server" Text=""></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                            <asp:Label ID="txt2value" runat="server">0</asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Panel ID="pnltxt3" runat="server" Visible="false">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                            <asp:Label ID="txt3" runat="server"></asp:Label>
                                                                            <asp:Label ID="txt3rat" runat="server" Text=""></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                            <asp:Label ID="txt3value" runat="server">0</asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                      <tr>
                                                        <td colspan="4">
                                                            <asp:Panel ID="pnlline" runat="server" Visible="false">
                                                            ________________</asp:Panel>
                                                            </td>
                                                            </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                            <asp:Label ID="lbltaxName" runat="server"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                            <asp:Label ID="lblTax" runat="server">0.00</asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                            <asp:Label ID="Label23" runat="server" Text="Total"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                            <asp:Label ID="lblTotal" runat="server">0.00</asp:Label>
                                                            </label>
                                                        </td>
                                                          <td>
                                                         <asp:Panel ID="pnlcosgood" runat="server" Visible="false">
                                                          <label>
                                                       
                                                             Total Cost Of Goods Sold
                                                         
                                                            </label>
                                                              <label>
                                                            <asp:Label ID="lblcostofgoods" runat="server">0.00</asp:Label>
                                                            </label>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                        <asp:Panel ID="pnmarkup" runat="server" Visible="false">
                                                         <label>
                                                         Markup on Invoice
                                                            </label>
                                                            <label>
                                                            <asp:Label ID="lblmarkupinv" runat="server">0.00</asp:Label>
                                                            </label>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                        <asp:Label ID="Label36" runat="server" 
                                                                            Text="Send a copy of the invoice to the customer"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkcust" runat="server" />
                                                                        
                                                                    </td>
                                                                    <td>   </label>
                                                                        <label>
                                                                            <asp:ImageButton ID="btnaddcusm" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                                 ToolTip="AddNew" Width="20px" ImageAlign="Bottom" 
                                                                            onclick="btnaddcusm_Click" />
                                                                        </label>
                                                                        <label>
                                                                            <asp:ImageButton ID="cusmre" runat="server" AlternateText="Refresh" Height="20px"
                                                                                ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                                                                ImageAlign="Bottom" onclick="cusmre_Click" />
                                                                        </label></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td>
                                                                        <label>
                                                                        <asp:Label ID="Label42" runat="server" 
                                                                            Text="Would you like to attach any documents to this invoice?"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkDocup" runat="server" />
                                                                        
                                                                    </td>
                                                                    </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btnSubmit" 
                                                                OnClick="btnSubmit_Click" Text="Update" ValidationGroup="4" Visible="true" />
                                                            <asp:Button ID="btnPrintCustomer" runat="server" CssClass="btnSubmit" 
                                                                OnClick="btnPrintCustomer_Click" Text="Print Customer Copy" Visible="False" />
                                                          
                                                            <asp:Button ID="btnSubmit0" runat="server" CssClass="btnSubmit" 
                                                                OnClick="btncancel_Click" Text="Edit" Visible="false" />
                                                        </td>
                                                    </tr>
                                              
                                            </table>
                                   
                                   </fieldset>
                                    </td>
                            </tr>
                           </table> 
                           </asp:Panel>
                           </td>
                           </tr>
                        </table>
                    </div>
                </fieldset>
             
                <tr>
                    <td>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                            PopupButtonID="imgCal" TargetControlID="txtGoodsDate">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                            PopupButtonID="txtYear" TargetControlID="txtYear">
                        </cc1:CalendarExtender>
                        <asp:Panel ID="Paneldate" runat="server" BackColor="#CCCCCC" 
                            BorderColor="Black" BorderStyle="Outset" Width="300px">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="subinnertblfc">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblm" runat="server" ForeColor="Black">Please check the date.</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 26px">
                                        <asp:Label ID="Label3" runat="server" ForeColor="Black" 
                                            Text="Start Date of the Year is "></asp:Label>
                                        <asp:Label ID="lblstartdate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 26px">
                                        <asp:Label ID="lblm0" runat="server" ForeColor="Black">You can not select anydate earlier than that. </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="ImageButton2" runat="server" CssClass="btnSubmit" 
                                            OnClick="ImageButton2_Click" Text="Cancel" />
                                    </td>
                                </tr>
                            </table>
                            &nbsp;</asp:Panel>
                        <asp:Button ID="HiddenButtondate" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" 
                            BackgroundCssClass="modalBackground" PopupControlID="Paneldate" 
                            TargetControlID="HiddenButtondate">
                        </cc1:ModalPopupExtender>
                        <asp:Label ID="lblpromo" runat="server" Text="0" Visible="false"></asp:Label>
                        <asp:Label ID="lblcust" runat="server" Text="0" Visible="false"></asp:Label>
                        <asp:Label ID="lblvolume" runat="server" Text="0" Visible="false"></asp:Label>
                        <asp:HiddenField ID="hidcust" runat="server" />
                        <asp:HiddenField ID="hidvoulume" runat="server" />
                        <asp:HiddenField ID="hidpromotionnal" runat="server" />
                    </td>
                </tr>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
