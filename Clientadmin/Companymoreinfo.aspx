<%@ Page Language="C#" MasterPageFile="~/afterloginMasterPage.master" AutoEventWireup="true"
    CodeFile="Companymoreinfo.aspx.cs" Inherits="Companymoreinfo" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ccolumn11" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

    <table id="pagetbl" style="width: 100%">
        <tr>
            <td style="font-size: 18px; font-weight: bold; color: #336699; font-family: Calibri;
                width: 50%;">
                Account Master Configuration Form
            </td>
            <td>
                <asp:Button ID="Change" runat="server" Text="Change" Width="61px" Visible="false"
                    OnClick="change_Click" CausesValidation="False" ValidationGroup="1" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="font-size: 16px; font-weight: bold; color: #336699; font-family: Calibri;">
                Company Information
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Company Name :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="lblcname" runat="server" Width="225px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="lblcname"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:Label ID="lbl_com" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Contact Person Name :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="txtcontactper" runat="server" Height="16px" Width="225px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcontactper"
                    ErrorMessage="*">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Contact Person Designation :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="txtcodesi" runat="server" Height="16px" Width="225px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcodesi"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 25%">
            </td>
            <td style="width: 75%">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="font-size: 16px; font-weight: bold; color: #336699; font-family: Calibri;
                height: 21px;">
                Contact Information
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Address :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="txtadd" runat="server" ValidationGroup="1" TextMode="MultiLine"
                    Width="225px" Height="40px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                        runat="server" ControlToValidate="txtadd" ErrorMessage="*" ValidationGroup="1"
                        Width="1px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Country :
            </td>
            <td style="width: 75%">
                <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="True" DataTextField="CountryName"
                    DataValueField="CountryId" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged"
                    ValidationGroup="1" Width="225px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlcountry"
                    ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri; height: 30px;" align="right">
                State/Province :
            </td>
            <td style="width: 75%; height: 30px;">
                <asp:DropDownList ID="ddlstate" runat="server" DataTextField="StateName" DataValueField="StateId"
                    Width="225px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlstate"
                    ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                City :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="txtcity" runat="server" CssClass="txtbox" ValidationGroup="1" Width="225px"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtcity" ErrorMessage="*"
                    ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                ZIP/Postal Code :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="txtzipcode" runat="server" Width="225px"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Phone Number :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="txtphn" onkeypress="return RealNumWithDecimal(this,event,2);" runat="server"
                    ValidationGroup="1" MaxLength="10" Width="225px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtphn" ErrorMessage="*"
                        ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
          <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
               Cell Phone Number: :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="txtmobileno" onkeypress="return RealNumWithDecimal(this,event,2);"
                    runat="server" ValidationGroup="1" MaxLength="10" Width="225px"></asp:TextBox>
            </td>
        </tr>
        
      
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Fax Number :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="txtfax" onkeypress="return RealNumWithDecimal(this,event,2);" runat="server"
                    MaxLength="12" Width="225px"></asp:TextBox>
            </td>
        </tr>
       
       
      
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
            </td>
            <td style="width: 75%">
                <asp:CheckBox ID="chkcheck" runat="server" Visible="false" Checked="True" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="font-size: 16px; font-weight: bold; color: #336699; font-family: Calibri;">
              <%--  Site Appearance Information--%>
              Company Site Information
            </td>
        </tr>
         
          <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
            Logo:
            </td>
            <td style="width: 75%">
                <asp:ImageButton ID="ImageButton1" runat="server" Width="176px" Height="106px" />
            </td>
        </tr>
         <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
              
            </td>
            <td style="width: 75%">
             <asp:Button ID="Button1" runat="server" Text="Change Company Site Logo" Width="205px" OnClick="Butsubmit_Clickchange" />
                
            </td>
        </tr>
        
     


          <tr>
           <td style="width: 25%; font-family: Calibri;" align="right">
                                <%--Site URL :--%>Business Website URL :
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtbusisiteurl" runat="server" Width="355px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtbusisiteurl"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>

           
        </tr>
         <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                <%--Description :--%>
                Aboutus :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="txtbusdesc" runat="server" TextMode="MultiLine" Width="355px"></asp:TextBox>
                <asp:Label ID="lbl_busdesc" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Product/Service Provided :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="txtserviceprovided" runat="server" TextMode="MultiLine" Width="355px"></asp:TextBox>
                <asp:Label ID="lbl_serviceprovided" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>



        <tr>
            <td colspan="2" style="font-size: 16px; font-weight: bold; color: #336699; font-family: Calibri;">
               <%-- Online Business Releted Information--%>
               Business Directory Information
            </td>
        </tr>
        <tr>
            <td colspan="2">
                (Optional: for Busi-Directory but highly recommended for all users)
                <br />
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Website Logo :
            </td>
            <td style="width: 75%">
                <asp:FileUpload ID="FileUpload1" runat="server" Width="225px" />&nbsp;&nbsp;<asp:Button
                    ID="Butsubmit" runat="server" Text="Submit" Width="65px" OnClick="Butsubmit_Click" />
                <asp:Label ID="lblimage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
          <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
            
            </td>
            <td style="width: 75%">
                <asp:ImageButton ID="imgdisplay" runat="server" Width="176px" Height="106px" />
                <br />
                (Note: will not show on your Company Site)
            </td>
        </tr>
        <tr>
         <td style="width: 25%; font-family: Calibri;" align="right">
                Business Website URL :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="txthostedsite" runat="server" Width="225px"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txthostedsite"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
               Shopping Cart Domain Name :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="urltext" runat="server" Width="225px"></asp:TextBox>
                <asp:Label ID="bbb" runat="server" ForeColor="#336699" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
        <td>
        
        </td>
        <td>
        (Optional: for eplaza.com)
        </td>
        </tr>
          <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Type:
            </td>
            <td style="width: 75%">
                <asp:DropDownList ID="ddlbustype" runat="server" AutoPostBack="True" ValidationGroup="1"
                    Width="225px" OnSelectedIndexChanged="ddlbustype_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Category:
            </td>
            <td style="width: 75%">
                <asp:DropDownList ID="ddlbuscategory" runat="server" AutoPostBack="True" Width="225px"
                    OnSelectedIndexChanged="ddlbuscategory_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Sub Category:
            </td>
            <td style="width: 75%">
                <asp:DropDownList ID="ddlbussubcat" runat="server" AutoPostBack="True" Width="225px"
                    OnSelectedIndexChanged="ddlbussubcat_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Sub Sub Category:
            </td>
            <td style="width: 75%">
                <asp:DropDownList ID="ddlsubsubcat" runat="server" AutoPostBack="false" Width="225px">
                </asp:DropDownList>
            </td>
        </tr>
   
        <tr>
            <td colspan="2" style="font-size: 16px; font-weight: bold; color: #336699; font-family: Calibri;">
                Mail Server Information
            </td>
        </tr>
                    <tr>
                        <td style="width: 25%; font-family: Calibri;" align="right">
                            Master Email ID :
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="Txtmastereid" runat="server" Width="150px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="Txtmastereid"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td style="width: 25%; font-family: Calibri;" align="right">
                        </td>
                        <td style="width: 25%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%; font-family: Calibri;" align="right">
                            Incoming Server(POP) :
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtincomingserver" runat="server" Width="150px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtincomingserver"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="width: 25%; font-family: Calibri;" align="right">
                            Incoming Server User ID :
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtincomingserveruserid" runat="server" Width="150px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtincomingserveruserid"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="width: 25%; font-family: Calibri;" align="right">
                            Incoming Server Password :
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="incomingserverpassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="incomingserverpassword"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                        </td>
                       
                    </tr>
                    <tr>
                     <td style="width: 25%; font-family: Calibri;" align="right">
                            Outgoing Server(SMTP) :
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtoutgoingserver" runat="server" Width="150px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtoutgoingserver"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                     <td style="width: 25%; font-family: Calibri;" align="right">
                            Outgoing Server User ID :
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtoutgoingserveruserid" runat="server" Width="150px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtoutgoingserveruserid"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>

                <tr>
                 <td style="width: 25%; font-family: Calibri;" align="right">
                            Outgoing Server Password :
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="outgoingserverpassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="outgoingserverpassword"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                        </td>
                </tr>
        

           <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
            </td>
            <td style="width: 75%">
                <asp:Button ID="btnsubmited" runat="server" Text="Submit" Width="65px" OnClick="btnsubmited_Click"
                    ValidationGroup="1" />
                <asp:Button ID="btnclear" runat="server" Text="Reset" Width="59px" />
            </td>
        </tr>
       <asp:Panel ID="Panel1" runat="server" Visible="false" Width="100%">

       
         <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Cell Number Country Code :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="txtmobilecc" onkeypress="return RealNumWithDecimal(this,event,2);"
                    runat="server" ValidationGroup="1" MaxLength="10" Width="225px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Email :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="txtemail" runat="server" ValidationGroup="1" Width="225px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtemail"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail"
                    ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Phone Number Country Code :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="txtphonecc" onkeypress="return RealNumWithDecimal(this,event,2);"
                    runat="server" ValidationGroup="1" MaxLength="10" Width="225px"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td style="width: 25%; font-family: Calibri;" align="right">
                Paypal Email ID :
            </td>
            <td style="width: 75%">
                <asp:TextBox ID="Textpaypal" runat="server" Width="225px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Textpaypal"
                    ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
        </tr>
      
        
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlsqlconn" runat="server" Visible="false" Width="100%">
                    <table style="width: 100%;" align="center">
                        <tr>
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="font-size: 16px; font-weight: bold; color: #336699; font-family: Calibri;">
                                Company Sql Server Connection Information :
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%; font-family: Calibri;" align="right">
                                Database Name :
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtsqldbname" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtsqldbname"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 25%; font-family: Calibri;" align="right">
                            </td>
                            <td style="width: 25%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%; font-family: Calibri;" align="right">
                                Connection URL/IP :
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtsqlserurlip" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtsqlserurlip"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 25%; font-family: Calibri;" align="right">
                                Port :
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtport" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtport"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%; font-family: Calibri;" align="right">
                                User Id :
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtuserid" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtuserid"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 25%; font-family: Calibri;" align="right">
                                Password :
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtpass" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtpass"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="font-size: 16px; font-weight: bold; color: #336699; font-family: Calibri;">
                                Company Busicontroler Information :
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <br />
                            </td>
                        </tr>
                        <tr>
                           
                            <td style="width: 25%; font-family: Calibri;" align="right">
                                Database Name :
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtbusidatabasevabe" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtbusidatabasevabe"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%; font-family: Calibri;" align="right">
                                Connection URL/IP :
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtbusiurlip" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtbusiurlip"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 25%; font-family: Calibri;" align="right">
                                Port :
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtbusiport" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtbusiport"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%; font-family: Calibri;" align="right">
                                User Id :
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtbusiuserid" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtbusiuserid"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 25%; font-family: Calibri;" align="right">
                                Password :
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtbusipass" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtbusipass"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="font-size: 16px; font-weight: bold; color: #336699; font-family: Calibri;">
               <%-- Business Information--%>
               Mail Server Information
            </td>
        </tr>
      
      
     
        </asp:Panel>
    </table>
</asp:Content>
