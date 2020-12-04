<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="Wizardcompanyinformation.aspx.cs" Inherits="ShoppingCart_Admin_Wizardcompanyinformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../javaScript/JScript.js" language="javascript"></script>

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

    <script language="javascript" type="text/javascript">

        function validationstatus() {
            //document.aspnetForm.ctl00_ContentPlaceHolder1_Label2.style.visibility=false;

            if (ValidateForm_phonenumber(document.aspnetForm.ctl00_ContentPlaceHolder1_txtphone) == 1) {
                alert("Please Enter your Phone Number")

                return false;
            }
            else if (ValidateForm_phonenumber(document.aspnetForm.ctl00_ContentPlaceHolder1_txtphone) == 2) {
                alert("Please Enter a Valid Phone Number")

                return false;

            }
            else {
                return true;
            }

        }
    
    </script>

    <asp:UpdatePanel ID="update" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                <fieldset>
                    <div style="clear: both;">
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="btnmanage" runat="server" Text="Manage" CssClass="btnSubmit" OnClick="btnmanage_Click" />
                    </div>
                    <legend>1. Company Information </legend>
                    <asp:Panel ID="Panel2" runat="server" Visible="False">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" Height="25px" RepeatDirection="Horizontal">
                            <asp:ListItem>Are you Setup New Company?</asp:ListItem>
                            <asp:ListItem>Existing Company</asp:ListItem>
                        </asp:RadioButtonList>
                        <div style="clear: both;">
                        </div>
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" Height="25px" RepeatDirection="Horizontal"
                            AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged"
                            Visible="False">
                            <asp:ListItem Value="0">Only Product</asp:ListItem>
                            <asp:ListItem Value="1">Only Service</asp:ListItem>
                            <asp:ListItem Value="2">Both</asp:ListItem>
                        </asp:RadioButtonList>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="lblboth" runat="server" Text="What you wish to show your home page for shoppingcart :"
                                Visible="False"></asp:Label>
                        </label>
                        <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal"
                            Visible="False">
                            <asp:ListItem Value="1">Products</asp:ListItem>
                            <asp:ListItem Value="2">Service</asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Company Name"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 20%">
                                <label>
                                    <asp:Label ID="txtcompanyname" CssClass="lblSuggestion" runat="server"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 20%">
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Country"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="lblcountry" runat="server" CssClass="lblSuggestion" Text=""></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Contact Person Name "></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="txtcontactpersonname" CssClass="lblSuggestion" runat="server"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="State"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="lblstate" runat="server" CssClass="lblSuggestion" Text=""></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label9" runat="server" Text="Contact Person Designation"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="txtcontactpersondesi" CssClass="lblSuggestion" runat="server"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label10" runat="server" Text="City"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="lblcity" runat="server" CssClass="lblSuggestion" Text=""></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label11" runat="server" Text="Address1"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="txtaddress1" runat="server" CssClass="lblSuggestion" Text=""></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label12" runat="server" Text="Fax"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="lblfax" runat="server" CssClass="lblSuggestion" Text=""></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label13" runat="server" Text="Address2"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="txtAddress2" runat="server" CssClass="lblSuggestion" Text=""></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label14" runat="server" Text="Pincode"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="lblpincode" runat="server" CssClass="lblSuggestion" Text=""></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label15" runat="server" Text="E-mail"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="lblemail" runat="server" CssClass="lblSuggestion" Text=""></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label16" runat="server" Text="Phone"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="lblphone" runat="server" CssClass="lblSuggestion" Text=""></asp:Label>
                                </label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblacchead" Text="2. Accounting Year Master Information" runat="server"></asp:Label>
                    </legend>
                    <label>
                        <asp:Label ID="lblyearcacc" Text="Select your First Accounting year for your business"
                            runat="server"></asp:Label><asp:HyperLink ID="lblopenaccy" Target="_blank" runat="server"></asp:HyperLink>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlaccy" runat="server">
                        <table width="100%" cellspacing="3">
                            <tr>
                                <td colspan="10%">
                                    <label>
                                        <asp:Label ID="lblf1" Text="Business Name" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td colspan="30%">
                                    <label>
                                        <asp:Label ID="lble2" Text="Normal Start Date of your Accounting year" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td colspan="15%">
                                    <label>
                                        <asp:Label ID="Label5" Text="First Accounting Year Start Year" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td colspan="15%">
                                    <label>
                                        <asp:Label ID="Label6" Text="First Accounting Year Start Date" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td colspan="15%">
                                    <label>
                                        <asp:Label ID="Label7" Text="First Accounting Year End Date" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr align="left">
                                <td colspan="10%">
                                    <label>
                                        <asp:DropDownList ID="ddlbus" runat="server" Width="100px" OnSelectedIndexChanged="ddlbus_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td colspan="30%">
                                    <label>
                                        <asp:Label ID="Label17" Text="Day" runat="server"></asp:Label></label>
                                    <label>
                                        <asp:DropDownList ID="ddlacday" Width="40px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlacday_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label18" Text="Month" runat="server"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="ddlacmonth" Width="40px" runat="server" OnSelectedIndexChanged="ddlacmonth_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td colspan="15%" align="left">
                                    <label>
                                        <asp:Label ID="Label19" Text="Year" runat="server"></asp:Label></label>
                                    <label>
                                        <asp:DropDownList ID="ddlacyear" runat="server" Width="80px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlacyear_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td colspan="15%">
                                    <label>
                                        <asp:Label ID="lblfysdate" Text="" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td colspan="15%">
                                    <label>
                                        <asp:Label ID="lblfyedate" Text="" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:Button ID="btnconf" runat="server" CssClass="btnSubmit" Text="Confirm Now" OnClick="btnconf_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <asp:Panel Visible="false" ID="Panel1" runat="server">
                <fieldset>
                    <legend>3. Eplaza Master Information </legend>
                    <label>
                        <asp:Label ID="Label20" runat="server" Text="Your Master URL for Shopping Cart is "></asp:Label>
                        <asp:Label ID="lblwebsite" runat="server" Text="Label" Font-Bold="true"></asp:Label>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label21" runat="server" Text="Please ensure your domain name"></asp:Label>
                        <asp:Label ID="lbldomain" runat="server" Text="Label" Font-Bold="true"></asp:Label>
                        <asp:Label ID="Label22" runat="server" Text="shopping cart to that url is forwarded to above mentioned URL at Your Domain Registers"></asp:Label>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label23" runat="server" Text="You can also access your shopping cart using following URL based on Domain name you gave at the time of registration "></asp:Label>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td style="width: 25%">
                                <label>
                                    <asp:Label ID="Label24" runat="server" Text="Company Shoppingcart Url"></asp:Label>
                                </label>
                            </td>
                            <td align="left" style="width: 75%">
                                <label>
                                    <asp:Label ID="lblhttp" runat="server" Text="http://"></asp:Label>
                                    <asp:Label ID="txtwebsite1" runat="server"></asp:Label>/<asp:Label ID="lblshop" runat="server"
                                        Text="ShoppingCart/default.aspx"></asp:Label>?Cid=<asp:Label ID="lblcid" runat="server"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label25" runat="server" Text="Paypal Id"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="txtPaypalId" runat="server"></asp:Label>
                                </label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>4. General Website Related Information </legend>
                    <label>
                        <asp:Label ID="lblLogo" runat="server" Text="Company Logo "></asp:Label>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnllogo" runat="server" Width="180px">
                        <asp:Button ID="btnChange" Text="Change" runat="server" Visible="false" OnClick="btnChange_Click" />
                        <asp:Image ID="imgLogo" runat="server" Height="106px" Width="176px" />
                    </asp:Panel>
                    <asp:FileUpload ID="FileUpload1" runat="server" Visible="False" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="FileUpload1"
                        ErrorMessage="*" ValidationGroup="7412" Visible="False"></asp:RequiredFieldValidator>
                    <asp:Label ID="Label1" runat="server" Text="Logo Size : 176 X 106" Visible="False"></asp:Label>
                    <asp:Button ID="imgBtnImageUpdate" Text="Update Image" runat="server" OnClick="imgBtnImageUpdate_Click"
                        Visible="False" />
                    <asp:Button ID="ImgBtncancel" Text="Cancel" runat="server" OnClick="ImgBtncancel_Click"
                        Visible="False" />
                </fieldset>
                </asp:Panel>
                <asp:ImageButton ID="imgbReset" Visible="false" runat="server" ImageUrl="~/ShoppingCart/images/reset.png"
                    OnClick="imgbReset_Click" AlternateText="reset" />
                &nbsp;<asp:ImageButton ID="ImgBtnSubmit" Visible="false" runat="server" ImageUrl="~/ShoppingCart/images/submit.png"
                    OnClick="ImgBtnSubmit_Click" ValidationGroup="1" />
                &nbsp;&nbsp;<asp:ImageButton ID="Imgbtnedit" runat="server" ImageUrl="~/ShoppingCart/images/EditNew.png"
                    OnClick="Imgbtnedit_Click" Visible="False" Width="80px" />
                &nbsp;<asp:ImageButton ID="Imgbtnupdate" runat="server" ImageUrl="~/ShoppingCart/images/update.png"
                    OnClick="Imgbtnupdate_Click" Visible="False" ValidationGroup="1" OnClientClick="return validationstatus()" />
                <table id="innertbl" style="width: 850px;" cellspacing="5">
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="pnconfor" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA"
                                BorderStyle="Outset" Width="300px">
                                <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="center">
                                            Confirmation....
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col4">
                                            <asp:Label ID="lblddffg" runat="server" Text="Notice : Please note that you will not be able to change this information in future.You will be able to feed Opening balances of all accounts for this accounting year only."
                                                ForeColor="Black"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnapd" runat="server" BackColor="#CCCCCC" Text="Confirm" OnClick="btnapd_Click" />
                                            <asp:Button ID="btns" runat="server" BackColor="#CCCCCC" Text="Cancel" OnClick="btns_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Button ID="HiddenButton222" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                                ID="ModalPopupExtender122" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="btns" PopupControlID="pnconfor" TargetControlID="HiddenButton222">
                            </cc1:ModalPopupExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <%--PostBackUrl="~/ShoppingCart/Admin/WizardCompanyWebsitMaster.aspx"--%>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
