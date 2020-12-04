<%@ Page  Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="servermaster.aspx.cs" Inherits="Master_Default" Title="Server Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
    </style>
    <script type="text/javascript" language="javascript">
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

        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
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
        function mask(evt, max_len) {



            if (evt.keyCode == 13) {

                return true;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }

        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
         </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button3" />
        </Triggers>
    </asp:UpdatePanel>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"> </asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <div style="float: right;">
                    <label>
                        <asp:Button ID="addnewpanel" runat="server" Text="Add Satellite Server" CssClass="btnSubmit"
                            OnClick="pnladdnew_click" /></label>
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label99" runat="server" Text="Satellite Server Type"> </asp:Label>
                        </legend>
                        <table width="100%">
                        <tr>
                                <td colspan="2" style="text-align: right;">
                                    <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Download Satellite server configuration wizard"
                                        OnClick="Button3_Click" />
                                </td>
                            </tr>
                          
                             <tr>
                                    <td style="width: 20%">
                                    <label>
                                        <asp:Label ID="Label60" runat="server" Text="Select Server Computer: "> </asp:Label>
                                        <asp:Label ID="Label80" runat="server" CssClass="labelstar" Text="*"> </asp:Label>                                        
                                    </label>
                                </td>
                                <td >  
                                <label>
                                        <asp:DropDownList ID="DDLProductMasterindividual" runat="server" AutoPostBack="True" Width="200px"   onselectedindexchanged="ProductMasterindividual_SelectedIndexChanged">
                                        </asp:DropDownList>                              
                                </label> 
                                <label>
                               ( View Only Individual's	Category Type added as ComputerProductForSaleorLease )
                                </label> 
                                </td>
                            </tr>
                           
                            <tr>
                            <td colspan="2" valign="top">
                            <label>                            
                            <asp:RadioButtonList  ID="RblServerType" runat="server" AutoPostBack="True"  Checked="true" OnSelectedIndexChanged="RblServerType_SelectedIndexChanged"  RepeatDirection="Vertical">
                                    <asp:ListItem Selected="True" Value="2" Text="Hosting at Customer's own server at its own location" ></asp:ListItem>
                                    <asp:ListItem Selected="True" Value="0" Text="This is a common server to host other protals's customer application. Maximum Number of companies that can be hosted on this server" ></asp:ListItem>
                                  <asp:ListItem Value="1" Text="This Server may be given out on lease or sale basis to customer" ></asp:ListItem>                                                                    
                             </asp:RadioButtonList>
                            </label> 
                                <label style="margin-top:72px">
                                    <asp:TextBox ID="txtMaxNoOfCompany" runat="server" MaxLength="10" onkeyup="return mak('Span13',10,this)" Width="60px" Visible="true"> </asp:TextBox>
                                </label>                            
                            </td>
                            </tr>
                            
       
                              <asp:Panel runat="server" ID="pnlLeasSharedSale" Visible="false">
                            <tr>
                                <td colspan="4">   
                                  <asp:CheckBox ID="chk_ServerMonthlyExclusiveLease" runat="server" AutoPostBack="True" Checked="false" oncheckedchanged="chk_ServerMonthlyExclusiveLease_CheckedChanged" Text="This Server Available For Monthly Exclusive Lease ?" />                                                                
                                </td>
                               
                            </tr>
                             <tr>
                                <td colspan="4">                                
                                  <asp:CheckBox ID="chk_ServerMonthlySharedLease" runat="server" AutoPostBack="True" Checked="false" oncheckedchanged="chk_ServerMonthlyExclusiveLease_CheckedChanged" Text="This Servers Available For Monthly Shared Lease ?&nbsp;&nbsp;"/>
                                        <asp:Panel runat="server" ID="pnlshared" Visible="false">
                                                 <label>
                                        <asp:Label ID="Label84" runat="server" Text="Max. No of companies to share  :&nbsp;" CssClass="labelcount"  > </asp:Label>
                                    </label>
                               
                                 <label style="width:100px;">
                                         <asp:TextBox ID="txtNoofcompanycanuse" runat="server" Text="0"  MaxLength="10" onkeyup="return mak('Span12',10,this)" Width="100px"> </asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtNoofcompanycanuse" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label style="width:60px;">
                                                <asp:Label ID="Label86" runat="server" Text="Max " CssClass="labelcount"></asp:Label>
                                                 <span id="Span12" cssclass="labelcount">10</span>
                                        </label> 
                                         </asp:Panel>    
                                </td>
                               
                            </tr>
                            
                             <tr>
                                <td colspan="4">                                
                                  <asp:CheckBox ID="chk_ServersforSell" runat="server" AutoPostBack="True" Checked="false" oncheckedchanged="chk_ServerMonthlyExclusiveLease_CheckedChanged" Text="This Servers Available For Sell ?&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                                     
                                </td>
                               
                            </tr>
                            </asp:Panel>
                          
                        </table>
                    </fieldset>

                      <%---------------------%>
                    <%---------------------%>
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label5" runat="server" Text="Add Satellite Server"> </asp:Label>
                        </legend>
                        <table width="100%">
                            
                          
                            <tr>
                                  <td valign="top">
                                 <label>
                                        <asp:Label ID="lblServerName" runat="server" Text="Server Name:"> </asp:Label>
                                        <asp:Label ID="Label34" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator117" runat="server" ControlToValidate="txtServerName"
                                            ErrorMessage="*" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                            ErrorMessage="Invalid Character" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                            ControlToValidate="txtServerName" ValidationGroup="1"> </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td valign="top">
                                  <label style="width:220px">
                                        <asp:TextBox ID="txtServerName" runat="server" MaxLength="10" onkeyup="return mak('Span24',10,this)"> </asp:TextBox>
                                    </label>
                                    <label style="width:50px">
                                        <asp:Label ID="Label103" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                        <span id="Span24" cssclass="labelcount">10</span>
                                    </label>
                                 
                                </td>
                               <td valign="top">
                                    <label>
                                        <asp:Label ID="lblserverdetail" runat="server" Text="Server Detail:"> </asp:Label>
                                        <asp:Label ID="Label2" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtserverdetail"
                                            ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                            ValidationExpression="^([a-z?.,A-Z0-9_\s]*)" ControlToValidate="txtserverdetail"
                                            ValidationGroup="1"> </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td valign="top">
                                    <label>
                                        <asp:TextBox ID="txtserverdetail" runat="server" Width="200"  MaxLength="200" onkeyup="return mak('Span2',200,this)"></asp:TextBox>
                                    </label>
                                    <label style="width:80px">
                                        <asp:Label ID="Label23" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                        <span id="Span2" cssclass="labelcount">200</span>
                                    </label>
                                </td>
                            </tr>
                            
                            
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblserverloction" runat="server" Text="Server Loction:"> </asp:Label>
                                        <asp:Label ID="Label1" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtserverloction"
                                            ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                            ValidationExpression="^([a-z?.,A-Z0-9_\s]*)" ControlToValidate="txtserverloction"
                                            ValidationGroup="1"> </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label style="width:220px">
                                        <asp:TextBox ID="txtserverloction" runat="server" MaxLength="100" onkeyup="return mak('Span1',100,this)"></asp:TextBox>
                                    </label>
                                    <label style="width:70px">
                                        <asp:Label ID="Label22" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                        <span id="Span1" cssclass="labelcount">100</span>
                                    </label>
                                </td>
                                  <td style="width: 20%">
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Mac Address:"> </asp:Label>
                                        <asp:Label ID="Label14" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtmacaddress"
                                            ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                            ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtmacaddress" ValidationGroup="1"> </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 30%">
                                    <label>
                                        <asp:TextBox ID="txtmacaddress" runat="server" MaxLength="150" onkeyup="return mak('Span6',50,this)"></asp:TextBox>
                                    </label>
                                    <label style="width:80px">
                                        <asp:Label ID="Label15" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                        <span id="Span6" cssclass="labelcount">50</span>
                                    </label>
                                </td>

                               
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label20" runat="server" Text="Server Computer Name:"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                            ValidationGroup="1" ControlToValidate="txtcomputername"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label style="width:220px">
                                        <asp:TextBox ID="txtcomputername" runat="server" AutoPostBack="true" MaxLength="200"
                                            onkeyup="return mak('Span10',200,this)" OnTextChanged="txtcomputername_TextChanged"> </asp:TextBox>
                                    </label>
                                    <label style="width:70px">
                                        <asp:Label ID="Label24" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                        <span id="Span10" cssclass="labelcount">200</span>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lblBusiwizsatellitesiteurl" runat="server" Text="Busiwiz Satellite website URL:"></asp:Label>
                                        <asp:Label ID="lbl_dublicaturl" runat="server" Text="" ForeColor="Red"> </asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtBusiwizsatellitesiteurl" runat="server" MaxLength="50" OnTextChanged="txtcomputername_TextChanged2" AutoPostBack="true"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label27" runat="server" Text="Your Computer is In:" CssClass="labelcount"> </asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                                        <asp:ListItem Text="Workgroup" Selected="True" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Domain" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <asp:Panel runat="server" ID="Panel1" Visible="false">
                                    <td>
                                        <label>
                                            <asp:Label ID="Label28" runat="server" Text="Doiman Name" CssClass="labelcount" > </asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtdomainname" runat="server" Width="150px" MaxLength="100" OnTextChanged="txtcomputername_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:Label ID="Label32" runat="server" Text="e.g. Safestserver" Font-Size="10px"> </asp:Label>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label35" runat="server" Text="."> </asp:Label>
                                        </label>
                                        <label>
                                            <asp:TextBox ID="txtdomaingrpname" runat="server" Width="40px" MaxLength="100" OnTextChanged="txtcomputername_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:Label ID="Label33" runat="server" Text="e.g. Com" Font-Size="10px"> </asp:Label>
                                        </label>
                                    </td>
                                </asp:Panel>
                            </tr>
                           
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblpublicIp" runat="server" Text="Server Public IP Address:"> </asp:Label>
                                        <asp:Label ID="Label55" runat="server" Text="*" CssClass="labelstar"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtpubip"
                                            ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtpubip"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtpubip" runat="server" MaxLength="50" onkeyup="return mak('Span25',20,this)"> </asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtpubip" ValidChars="012.3456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                    <label style="width:70px">
                                        <asp:Label ID="Label56" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span25" cssclass="labelcount">20</span>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lblIpaddress" runat="server" Text="Server Local IP Address:"> </asp:Label>
                                        <asp:Label ID="Label65" runat="server" Text="*" CssClass="labelstar"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtIpaddress"
                                            ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtIpaddress"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtIpaddress" runat="server" MaxLength="50" onkeyup="return mak('Span21',50,this)"> </asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtIpaddress" ValidChars="012.3456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                    <label style="width:70px">
                                        <asp:Label ID="Label43" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span21" cssclass="labelcount">20</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label62" runat="server" Text="Status:"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlstatus" runat="server" Width="100px">
                                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td> 
                                <label> 
                                 <asp:Label ID="Label44" runat="server" Text="Server Computer Full Name:"> </asp:Label>
                                 <asp:Label ID="lbl_dublicat" runat="server" Text="" ForeColor="Red"> </asp:Label>
                                </label> 
                                </td>
                                <td>
                                      <label>
                                            <asp:TextBox ID="txtservercomputerfullname" runat="server" MaxLength="100" OnTextChanged="txtcomputername_TextChanged1" AutoPostBack="true"></asp:TextBox>
                                        </label>
                                </td>
                            </tr>
                          <asp:Panel ID="Panel3" Visible="false" runat="server">
                                <tr>
                                    <td>
                                        <label>
                                         <%--   <asp:Label ID="Label57" runat="server" Text="Server Computer Full Name"> </asp:Label>--%>
                                        </label>
                                      
                                        <label>
                                            <asp:Label ID="Label36" runat="server" Text="Redistributed Software to be installed on Server"></asp:Label>
                                            <asp:Label ID="Label37" runat="server" Text="*" CssClass="labelstar"> </asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddldistributedsoftwaremaster" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddldistributedsoftwaremaster_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label38" runat="server" Text="License Key"></asp:Label>
                                            <asp:Label ID="Label47" runat="server" Text="*" CssClass="labelstar"> </asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddllicensekeymaster" runat="server">
                                            </asp:DropDownList>
                                        </label>
                                        <label>
                                            <asp:Button ID="Button1" runat="server" Text="Add" OnClick="Button1_Click" />
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Panel ID="Panel2" runat="server" Width="100%">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="id"
                                                Width="100%" EmptyDataText="No Record Found.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="software Licensekey Id" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsoftwarelicensekeymasterid" runat="server" Text='<%# Eval("ID") %>'> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Software Name" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="50%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsoftwarename" runat="server" Text='<%# Eval("redistributed_software_name") %>'> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Server Full Name" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbllicsencekey" runat="server" Text='<%# Eval("Licensekey") %>'> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblUserId" runat="server" Text="Busiwiz Created Administrator UserID"></asp:Label>
                                            <asp:Label ID="Label61" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtUserId"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtUserId" runat="server" MaxLength="50" Enabled="false" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:.;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span22',50)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label49" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="Span22" cssclass="labelcount">50</span>
                                            <asp:Label ID="Label50" CssClass="labelcount" runat="server" Text=""></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblwindowpassword" runat="server" Text=" Password"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtwindowpassword" runat="server" Enabled="false" onkeyup="return mak('Span23',50,this)"
                                                Width="200px" TextMode="Password"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label52" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                            <span id="Span23" cssclass="labelcount">50</span>
                                            <asp:Label ID="Label53" CssClass="labelcount" runat="server"> </asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label63" runat="server" Text="Busiwiz Created Simple UserID"></asp:Label>
                                            <asp:Label ID="Label64" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtsimplebusiwizuser"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtsimplebusiwizuser" runat="server" MaxLength="50" Enabled="False"
                                                onkeyup="return check(this,/[\\/!@#$%^'&*()>+:.;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span27',50)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label66" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="Span27" cssclass="labelcount">50</span>
                                            <asp:Label ID="Label67" CssClass="labelcount" runat="server" Text=""></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label68" runat="server" Text=" Password"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtsimpleuserpassword" runat="server" Enabled="false" onkeyup="return mak('Span28',50,this)"
                                                Width="200px" TextMode="Password"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label69" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                            <span id="Span28" cssclass="labelcount">50</span>
                                            <asp:Label ID="Label70" CssClass="labelcount" runat="server"> </asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label71" runat="server" Text="Default Database MDF path"> </asp:Label>
                                            <asp:Label ID="Label72" runat="server" Text="*" CssClass="labelstar"> </asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtdefaultdatabasemdfpath"
                                                ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtdefaultdatabasemdfpath" runat="server" MaxLength="100" onkeyup="return mak('Span29',100,this)"> </asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label73" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="Span29" cssclass="labelcount">100</span>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label74" runat="server" Text="Default Database LDF path"> </asp:Label>
                                            <asp:Label ID="Label75" runat="server" Text="*" CssClass="labelstar"> </asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtdefaultdatabaseldfpath"
                                                ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtdefaultdatabaseldfpath" runat="server" MaxLength="50" onkeyup="return mak('Span30',100,this)"> </asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label76" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="Span30" cssclass="labelcount">100</span>
                                        </label>
                                    </td>
                                </tr>
                    </asp:Panel>
                        </table>
                    </fieldset>
                  <%--  <fieldset>
                        <legend>Server Folder Details</legend>
                        <table width="100%">
                            <tr>
                                <td style="width: 20%">
                                    <label>
                                        <asp:Label ID="lblfolderpathformastercode" runat="server" Text="Folder Path For Master Code"> </asp:Label>
                                        <asp:Label ID="Label12" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtfolderpathformastercode"
                                            ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 30%">
                                    <label>
                                        <asp:TextBox ID="txtfolderpathformastercode" runat="server" MaxLength="100" onkeyup="return mak('Span9',100,this)"> </asp:TextBox>
                                    </label>
                                    <label style="width:70px">
                                        <asp:Label ID="Label31" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                        <span id="Span9" cssclass="labelcount">100</span>
                                    </label>
                                </td>
                                <td style="width: 20%">
                                    <label>
                                        <asp:Label ID="lblserverdefaultpathforiis" runat="server" Text="Server Default Path For website files"> </asp:Label>
                                        <asp:Label ID="Label9" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtserverdefaultpathforiis"
                                            ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 30%">
                                    <label>
                                        <asp:TextBox ID="txtserverdefaultpathforiis" runat="server" MaxLength="100" onkeyup="return mak('Span16',100,this)"> </asp:TextBox>
                                    </label>
                                    <label style="width:70px">
                                        <asp:Label ID="Label39" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                        <span id="Span16" cssclass="labelcount">100</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblserverdefaultpathformdf" runat="server" Text="Server Default Path For SQL Database MDF File"> </asp:Label>
                                        <asp:Label ID="Label10" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtserverdefaultpathformdf"
                                            ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtserverdefaultpathformdf" runat="server" MaxLength="100" onkeyup="return mak('Span7',100,this)"> </asp:TextBox>
                                    </label>
                                    <label style="width:70px">
                                        <asp:Label ID="Label29" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                        <span id="Span7" cssclass="labelcount">100</span>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lblserverdefaultpathforfdf" runat="server" Text="Server Default Path For SQL Database LDF File"> </asp:Label>
                                        <asp:Label ID="Label11" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtserverdefaultpathforfdf"
                                            ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtserverdefaultpathforfdf" runat="server" MaxLength="100" onkeyup="return mak('Span8',100,this)"> </asp:TextBox>
                                    </label>
                                    <label style="width:70px">
                                        <asp:Label ID="Label30" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                        <span id="Span8" cssclass="labelcount">100</span>
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>--%>




                          <fieldset>
                        <legend>
                            <asp:Label ID="Label29" runat="server" Text="Security key for silent pages "></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td colspan="6">
                                    <br />
                                    <label>
                                    <asp:Label ID="Label203" runat="server" Text="Please create alpha numeric secuirity key   to verify the authentic parameter received on silent pages " Font-Bold="true"></asp:Label>
                                 
                                 </label>
                                   <br />
                                </td>
                            </tr>
                            <tr>
                            <td style="width: 15px">
                            <label>
                              <asp:Label ID="Label30" runat="server" Text="SecurityKey1"  ></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                            
                                  <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
                            
                            </td>
                              <td style="width: 15px">
                                <label>
                              <asp:Label ID="Label31" runat="server" Text="Plus Dynamic Content"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                                  <asp:DropDownList ID="DropDownList2" runat="server">

                              <%--  <asp:ListItem Text="All" Value="0"></asp:ListItem>--%>
                                        <asp:ListItem Text="First Letter of Today" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Second Letter of Today" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Third Letter of Today" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Fourth Letter of Today" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Fifth Letter of Today" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Sixth Letter of Today" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Seventh Letter of Today" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Eighth Letter of Today" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Nineth Letter of Today" Value="9"></asp:ListItem>
                                         <asp:ListItem Text="Tenth Letter of Today" Value="10"></asp:ListItem>


                                  </asp:DropDownList>
                            </td>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label39" runat="server" Text="And"></asp:Label>
                              </label>
                            </td>
                            
                       <td style="width: 15px">
                            
                              <asp:DropDownList ID="DropDownList3" runat="server">
                                <asp:ListItem Text="Current date" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Yesterday's date" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="2 days back date" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="3 days back date" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="4 days back date" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="5 days back date" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="6 days back date" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="7 days back date" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="8 days back date" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="9 days back date" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="10 days back date" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="11 days back date" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="12 days back date" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="13 days back date" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="14 days back date" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="15 days back date" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="16 days back date" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="17 days back date" Value="18"></asp:ListItem>
                                         <asp:ListItem Text="18 days back date" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="19 days back date" Value="10"></asp:ListItem>

                                        <asp:ListItem Text="20 days back date" Value="21"></asp:ListItem>

                                  </asp:DropDownList>
                            </td>

                           
                            </tr>
                                                        <tr>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label186" runat="server" Text="SecurityKey2"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                            
                                  <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                            
                            </td>
                              <td style="width: 15px">
                                <label>
                              <asp:Label ID="Label187" runat="server" Text="Plus Dynamic Content"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                                  <asp:DropDownList ID="DropDownList4" runat="server">
                                   <asp:ListItem Text="First Letter of Today" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Second Letter of Today" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Third Letter of Today" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Fourth Letter of Today" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Fifth Letter of Today" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Sixth Letter of Today" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Seventh Letter of Today" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Eighth Letter of Today" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Nineth Letter of Today" Value="9"></asp:ListItem>
                                         <asp:ListItem Text="Tenth Letter of Today" Value="10"></asp:ListItem>

                                  </asp:DropDownList>
                            </td>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label188" runat="server" Text="And"></asp:Label>
                              </label>
                            </td>
                            
                       <td style="width: 15px">
                            
                              <asp:DropDownList ID="DropDownList5" runat="server">
                                <asp:ListItem Text="Current date" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Yesterday's date" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="2 days back date" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="3 days back date" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="4 days back date" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="5 days back date" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="6 days back date" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="7 days back date" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="8 days back date" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="9 days back date" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="10 days back date" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="11 days back date" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="12 days back date" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="13 days back date" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="14 days back date" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="15 days back date" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="16 days back date" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="17 days back date" Value="18"></asp:ListItem>
                                         <asp:ListItem Text="18 days back date" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="19 days back date" Value="10"></asp:ListItem>

                                        <asp:ListItem Text="20 days back date" Value="21"></asp:ListItem>
                                  </asp:DropDownList>
                            </td>

                           
                            </tr>
                                                      <tr>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label189" runat="server" Text="SecurityKey3"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                            
                                  <asp:TextBox ID="TextBox21" runat="server"></asp:TextBox>
                            
                            </td>
                              <td style="width: 15px">
                                <label>
                              <asp:Label ID="Label190" runat="server" Text="Plus Dynamic Content"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                                  <asp:DropDownList ID="DropDownList6" runat="server">
                                   <asp:ListItem Text="First Letter of Today" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Second Letter of Today" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Third Letter of Today" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Fourth Letter of Today" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Fifth Letter of Today" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Sixth Letter of Today" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Seventh Letter of Today" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Eighth Letter of Today" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Nineth Letter of Today" Value="9"></asp:ListItem>
                                         <asp:ListItem Text="Tenth Letter of Today" Value="10"></asp:ListItem>

                                  </asp:DropDownList>
                            </td>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label191" runat="server" Text="And"></asp:Label>
                              </label>
                            </td>
                            
                       <td style="width: 15px">
                            
                              <asp:DropDownList ID="DropDownList7" runat="server">
                                <asp:ListItem Text="Current date" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Yesterday's date" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="2 days back date" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="3 days back date" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="4 days back date" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="5 days back date" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="6 days back date" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="7 days back date" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="8 days back date" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="9 days back date" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="10 days back date" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="11 days back date" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="12 days back date" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="13 days back date" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="14 days back date" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="15 days back date" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="16 days back date" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="17 days back date" Value="18"></asp:ListItem>
                                         <asp:ListItem Text="18 days back date" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="19 days back date" Value="10"></asp:ListItem>

                                        <asp:ListItem Text="20 days back date" Value="21"></asp:ListItem>
                                  </asp:DropDownList>
                            </td>

                           
                            </tr>
                                                       <tr>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label192" runat="server" Text="SecurityKey4"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                            
                                  <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
                            
                            </td>
                              <td style="width: 15px">
                                <label>
                              <asp:Label ID="Label193" runat="server" Text="Plus Dynamic Content"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                                  <asp:DropDownList ID="DropDownList8" runat="server">
                                   <asp:ListItem Text="First Letter of Today" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Second Letter of Today" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Third Letter of Today" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Fourth Letter of Today" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Fifth Letter of Today" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Sixth Letter of Today" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Seventh Letter of Today" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Eighth Letter of Today" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Nineth Letter of Today" Value="9"></asp:ListItem>
                                         <asp:ListItem Text="Tenth Letter of Today" Value="10"></asp:ListItem>

                                  </asp:DropDownList>
                            </td>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label194" runat="server" Text="And"></asp:Label>
                              </label>
                            </td>
                            
                       <td style="width: 15px">
                            
                              <asp:DropDownList ID="DropDownList9" runat="server">
                                <asp:ListItem Text="Current date" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Yesterday's date" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="2 days back date" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="3 days back date" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="4 days back date" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="5 days back date" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="6 days back date" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="7 days back date" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="8 days back date" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="9 days back date" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="10 days back date" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="11 days back date" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="12 days back date" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="13 days back date" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="14 days back date" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="15 days back date" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="16 days back date" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="17 days back date" Value="18"></asp:ListItem>
                                         <asp:ListItem Text="18 days back date" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="19 days back date" Value="10"></asp:ListItem>

                                        <asp:ListItem Text="20 days back date" Value="21"></asp:ListItem>
                                  </asp:DropDownList>
                            </td>

                           
                            </tr>
                                                        <tr>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label195" runat="server" Text="SecurityKey5"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                            
                                  <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox>
                            
                            </td>
                              <td style="width: 15px">
                                <label>
                              <asp:Label ID="Label196" runat="server" Text="Plus Dynamic Content"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                                  <asp:DropDownList ID="DropDownList10" runat="server">
                                   <asp:ListItem Text="First Letter of Today" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Second Letter of Today" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Third Letter of Today" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Fourth Letter of Today" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Fifth Letter of Today" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Sixth Letter of Today" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Seventh Letter of Today" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Eighth Letter of Today" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Nineth Letter of Today" Value="9"></asp:ListItem>
                                         <asp:ListItem Text="Tenth Letter of Today" Value="10"></asp:ListItem>

                                  </asp:DropDownList>
                            </td>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label197" runat="server" Text="And"></asp:Label>
                              </label>
                            </td>
                            
                       <td style="width: 15px">
                            
                              <asp:DropDownList ID="DropDownList11" runat="server">
                                <asp:ListItem Text="Current date" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Yesterday's date" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="2 days back date" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="3 days back date" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="4 days back date" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="5 days back date" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="6 days back date" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="7 days back date" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="8 days back date" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="9 days back date" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="10 days back date" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="11 days back date" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="12 days back date" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="13 days back date" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="14 days back date" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="15 days back date" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="16 days back date" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="17 days back date" Value="18"></asp:ListItem>
                                         <asp:ListItem Text="18 days back date" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="19 days back date" Value="10"></asp:ListItem>

                                        <asp:ListItem Text="20 days back date" Value="21"></asp:ListItem>
                                  </asp:DropDownList>
                            </td>

                           
                            </tr>
                                                         <tr>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label198" runat="server" Text="SecurityKey6"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                            
                                  <asp:TextBox ID="TextBox24" runat="server"></asp:TextBox>
                            
                            </td>
                              <td style="width: 15px">
                                <label>
                              <asp:Label ID="Label199" runat="server" Text="Plus Dynamic Content"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                                  <asp:DropDownList ID="DropDownList12" runat="server">
                                   <asp:ListItem Text="First Letter of Today" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Second Letter of Today" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Third Letter of Today" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Fourth Letter of Today" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Fifth Letter of Today" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Sixth Letter of Today" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Seventh Letter of Today" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Eighth Letter of Today" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Nineth Letter of Today" Value="9"></asp:ListItem>
                                         <asp:ListItem Text="Tenth Letter of Today" Value="10"></asp:ListItem>

                                  </asp:DropDownList>
                            </td>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label200" runat="server" Text="And"></asp:Label>
                              </label>
                            </td>
                            
                       <td style="width: 15px">
                            
                              <asp:DropDownList ID="DropDownList13" runat="server">
                                <asp:ListItem Text="Current date" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Yesterday's date" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="2 days back date" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="3 days back date" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="4 days back date" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="5 days back date" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="6 days back date" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="7 days back date" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="8 days back date" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="9 days back date" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="10 days back date" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="11 days back date" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="12 days back date" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="13 days back date" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="14 days back date" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="15 days back date" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="16 days back date" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="17 days back date" Value="18"></asp:ListItem>
                                         <asp:ListItem Text="18 days back date" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="19 days back date" Value="10"></asp:ListItem>

                                        <asp:ListItem Text="20 days back date" Value="21"></asp:ListItem>
                                  </asp:DropDownList>
                            </td>

                           
                            </tr>
                                        <tr>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label201" runat="server" Text="SecurityKey7"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                            
                                  <asp:TextBox ID="TextBox25" runat="server"></asp:TextBox>
                            
                            </td>
                              <td style="width: 15px">
                                <label>
                              <asp:Label ID="Label202" runat="server" Text="Plus Dynamic Content"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                                  <asp:DropDownList ID="DropDownList14" runat="server">
                                   <asp:ListItem Text="First Letter of Today" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Second Letter of Today" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Third Letter of Today" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Fourth Letter of Today" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Fifth Letter of Today" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Sixth Letter of Today" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Seventh Letter of Today" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Eighth Letter of Today" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Nineth Letter of Today" Value="9"></asp:ListItem>
                                         <asp:ListItem Text="Tenth Letter of Today" Value="10"></asp:ListItem>

                                  </asp:DropDownList>
                            </td>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label204" runat="server" Text="And"></asp:Label>
                              </label>
                            </td>
                            
                       <td style="width: 15px">
                            
                              <asp:DropDownList ID="DropDownList15" runat="server">
                                <asp:ListItem Text="Current date" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Yesterday's date" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="2 days back date" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="3 days back date" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="4 days back date" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="5 days back date" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="6 days back date" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="7 days back date" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="8 days back date" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="9 days back date" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="10 days back date" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="11 days back date" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="12 days back date" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="13 days back date" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="14 days back date" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="15 days back date" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="16 days back date" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="17 days back date" Value="18"></asp:ListItem>
                                         <asp:ListItem Text="18 days back date" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="19 days back date" Value="10"></asp:ListItem>

                                        <asp:ListItem Text="20 days back date" Value="21"></asp:ListItem>
                                  </asp:DropDownList>
                            </td>

                           
                            </tr>
                                        <tr>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label205" runat="server" Text="SecurityKey8"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                            
                                  <asp:TextBox ID="TextBox26" runat="server"></asp:TextBox>
                            
                            </td>
                              <td style="width: 15px">
                                <label>
                              <asp:Label ID="Label206" runat="server" Text="Plus Dynamic Content"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                                  <asp:DropDownList ID="DropDownList16" runat="server">
                                   <asp:ListItem Text="First Letter of Today" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Second Letter of Today" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Third Letter of Today" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Fourth Letter of Today" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Fifth Letter of Today" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Sixth Letter of Today" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Seventh Letter of Today" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Eighth Letter of Today" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Nineth Letter of Today" Value="9"></asp:ListItem>
                                         <asp:ListItem Text="Tenth Letter of Today" Value="10"></asp:ListItem>

                                  </asp:DropDownList>
                            </td>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label207" runat="server" Text="And"></asp:Label>
                                </label>
                            </td>
                            
                       <td style="width: 15px">
                            
                              <asp:DropDownList ID="DropDownList17" runat="server">
                             
                                <asp:ListItem Text="Current date" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Yesterday's date" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="2 days back date" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="3 days back date" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="4 days back date" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="5 days back date" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="6 days back date" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="7 days back date" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="8 days back date" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="9 days back date" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="10 days back date" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="11 days back date" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="12 days back date" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="13 days back date" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="14 days back date" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="15 days back date" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="16 days back date" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="17 days back date" Value="18"></asp:ListItem>
                                         <asp:ListItem Text="18 days back date" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="19 days back date" Value="10"></asp:ListItem>

                                        <asp:ListItem Text="20 days back date" Value="21"></asp:ListItem>
                                 
                                  </asp:DropDownList>
                            </td>

                           
                            </tr>
                                        <tr>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label208" runat="server" Text="SecurityKey9"></asp:Label>
                                </label>
                            </td>
                              <td style="width: 15px">
                            
                                  <asp:TextBox ID="TextBox27" runat="server"></asp:TextBox>
                            
                            </td>
                              <td style="width: 15px">
                                <label>
                              <asp:Label ID="Label209" runat="server" Text="Plus Dynamic Content"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                                  <asp:DropDownList ID="DropDownList18" runat="server">
                                   <asp:ListItem Text="First Letter of Today" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Second Letter of Today" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Third Letter of Today" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Fourth Letter of Today" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Fifth Letter of Today" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Sixth Letter of Today" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Seventh Letter of Today" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Eighth Letter of Today" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Nineth Letter of Today" Value="9"></asp:ListItem>
                                         <asp:ListItem Text="Tenth Letter of Today" Value="10"></asp:ListItem>

                                  </asp:DropDownList>
                            </td>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label210" runat="server" Text="And"></asp:Label>
                              </label>
                            </td>
                            
                       <td style="width: 15px">
                            
                              <asp:DropDownList ID="DropDownList19" runat="server">
                                <asp:ListItem Text="Current date" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Yesterday's date" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="2 days back date" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="3 days back date" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="4 days back date" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="5 days back date" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="6 days back date" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="7 days back date" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="8 days back date" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="9 days back date" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="10 days back date" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="11 days back date" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="12 days back date" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="13 days back date" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="14 days back date" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="15 days back date" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="16 days back date" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="17 days back date" Value="18"></asp:ListItem>
                                         <asp:ListItem Text="18 days back date" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="19 days back date" Value="10"></asp:ListItem>

                                        <asp:ListItem Text="20 days back date" Value="21"></asp:ListItem>
                                  </asp:DropDownList>
                            </td>

                           
                            </tr>
                                        <tr>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label211" runat="server" Text="SecurityKey10"></asp:Label>
                                </label>
                            </td>
                              <td style="width: 15px">
                            
                                  <asp:TextBox ID="TextBox28" runat="server"></asp:TextBox>
                            
                            </td>
                              <td style="width: 15px">
                                <label>
                              <asp:Label ID="Label212" runat="server" Text="Plus Dynamic Content"></asp:Label>
                              </label>
                            </td>
                              <td style="width: 15px">
                                  <asp:DropDownList ID="DropDownList20" runat="server">
                                   <asp:ListItem Text="First Letter of Today" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Second Letter of Today" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Third Letter of Today" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Fourth Letter of Today" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Fifth Letter of Today" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Sixth Letter of Today" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Seventh Letter of Today" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Eighth Letter of Today" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Nineth Letter of Today" Value="9"></asp:ListItem>
                                         <asp:ListItem Text="Tenth Letter of Today" Value="10"></asp:ListItem>

                                  </asp:DropDownList>
                            </td>
                            <td style="width: 15px">
                              <label>
                              <asp:Label ID="Label213" runat="server" Text="And"></asp:Label>
                              </label>
                            </td>
                            
                       <td style="width: 15px">
                            
                              <asp:DropDownList ID="DropDownList21" runat="server">
                                <asp:ListItem Text="Current date" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Yesterday's date" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="2 days back date" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="3 days back date" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="4 days back date" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="5 days back date" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="6 days back date" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="7 days back date" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="8 days back date" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="9 days back date" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="10 days back date" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="11 days back date" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="12 days back date" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="13 days back date" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="14 days back date" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="15 days back date" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="16 days back date" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="17 days back date" Value="18"></asp:ListItem>
                                         <asp:ListItem Text="18 days back date" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="19 days back date" Value="10"></asp:ListItem>

                                        <asp:ListItem Text="20 days back date" Value="21"></asp:ListItem>
                                  </asp:DropDownList>
                            </td>

                           
                            </tr>
                     
                        </table>
                    </fieldset>









                       <fieldset>
                        <legend>
                            <asp:Label ID="Label214" runat="server" Text="DNS Server "></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label215" runat="server" Text="Is it  a registered dns server"></asp:Label>
                                       
                                        <%--<asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtDatabaseName" ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Yes" />
                                            <%--onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span1',20)"--%>
                                    </label>
                                   
                                </td>
                            </tr>
                          
                             <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>








                                            <fieldset>
                        <legend>
                            <asp:Label ID="Label100" runat="server" Text="Master Code"></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label104" runat="server" Text="Folder Path"></asp:Label>
                                        <asp:Label ID="Label106" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtDatabaseName"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtDatabaseName" ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="txtDatabaseName" runat="server" Width="200px" ValidationGroup="1"
                                            MaxLength="100"  onkeyup="return mak('Span13',100,this)"></asp:TextBox>
                                            <%--onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span1',20)"--%>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label107" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span13" cssclass="labelcount">100</span>
                                        <asp:Label ID="Label110" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ /)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label111" runat="server" Text="FTP URL"></asp:Label>
                                        <asp:Label ID="Label112" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtDatabaseServerurl"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="txtDatabaseServerurl" runat="server" ValidationGroup="1" Width="200px"
                                            MaxLength="50" onkeyup="return mak('Span22',50,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label113" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span14" cssclass="labelcount">50</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label114" runat="server" Text="User Id"></asp:Label>
                                        <asp:Label ID="Label115" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtDBUserId"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtDBUserId" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="txtDBUserId" runat="server" Width="200px" ValidationGroup="1" MaxLength="20"
                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span11',20)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label116" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span15" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label117" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label118" runat="server" Text="Password"></asp:Label>
                                        <asp:Label ID="Label119" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txtDBPassword"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="txtDBPassword" runat="server" ValidationGroup="1" MaxLength="20"
                                            TextMode="Password" Width="200px" onkeyup="return mak('Span16',20,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label120" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span17" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label121" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @ + -)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label122" runat="server" Text="Access Port"></asp:Label>
                                        <asp:Label ID="Label123" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtDatabaseAccessPort"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtDatabaseAccessPort" runat="server" Width="200px" ValidationGroup="1"
                                            MaxLength="20" onkeyup="return mak('Span6',20,this)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                            TargetControlID="txtDatabaseAccessPort" ValidChars="0147852369." />
                                    </label>
                                    <label>
                                        <asp:Label ID="Label124" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span18" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label125" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                 <label style="width:50px;">
                                        <asp:Button ID="Button6" runat="server" Text="Test FTP" 
                                        onclick="Button6_Click" />
                                     <asp:Label ID="Label9" runat="server" Text="" ForeColor="Green"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>


                    <fieldset>
                        <legend>
                            <asp:Label ID="Label126" runat="server" Text="Server Default Path For website files "></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label127" runat="server" Text="Folder Path"></asp:Label>
                                        <asp:Label ID="Label128" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtDatabaseName"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtDatabaseName" ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="TextBox4" runat="server" Width="200px" ValidationGroup="1"
                                            MaxLength="100" onkeyup="return mak('Span19',100,this)"></asp:TextBox>
                                            <%--onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span1',20)"--%>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label129" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span19" cssclass="labelcount">100</span>
                                        <asp:Label ID="Label130" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ /)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label131" runat="server" Text="FTP URL"></asp:Label>
                                        <asp:Label ID="Label132" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txtDatabaseServerurl"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="TextBox5" runat="server" ValidationGroup="1" Width="200px"
                                            MaxLength="50" onkeyup="return mak('Span22',50,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label133" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span26" cssclass="labelcount">50</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label134" runat="server" Text="User Id"></asp:Label>
                                        <asp:Label ID="Label135" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtDBUserId"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtDBUserId" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="TextBox6" runat="server" Width="200px" ValidationGroup="1" MaxLength="20"
                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span11',20)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label136" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span32" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label137" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label138" runat="server" Text="Password"></asp:Label>
                                        <asp:Label ID="Label139" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtDBPassword"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="TextBox7" runat="server" ValidationGroup="1" MaxLength="20"
                                            TextMode="Password" Width="200px" onkeyup="return mak('Span33',20,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label140" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span33" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label141" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @ + -)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label142" runat="server" Text="Access Port"></asp:Label>
                                        <asp:Label ID="Label143" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtDatabaseAccessPort"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="TextBox8" runat="server" Width="200px" ValidationGroup="1"
                                            MaxLength="20" onkeyup="return mak('Span6',20,this)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                            TargetControlID="txtDatabaseAccessPort" ValidChars="0147852369." />
                                    </label>
                                    <label>
                                        <asp:Label ID="Label144" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span34" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label145" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                               <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                 <label style="width:50px;">
                                        <asp:Button ID="Button7" runat="server" Text="Test FTP" 
                                        onclick="Button7_Click" />
                                         <asp:Label ID="Label10" runat="server" Text="" ForeColor="Green"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>

                       <fieldset>
                        <legend>
                            <asp:Label ID="Label146" runat="server" Text="Server Default Path For SQL Database MDF File"></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label147" runat="server" Text="Folder Path"></asp:Label>
                                        <asp:Label ID="Label148" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="txtDatabaseName"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtDatabaseName" ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="TextBox9" runat="server" Width="200px" ValidationGroup="1"
                                            MaxLength="100"  onkeyup="return mak('Span35',100,this)"></asp:TextBox>
                                            <%--onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span1',20)"--%>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label149" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span35" cssclass="labelcount">100</span>
                                        <asp:Label ID="Label150" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ /)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label151" runat="server" Text="FTP URL"></asp:Label>
                                        <asp:Label ID="Label152" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="txtDatabaseServerurl"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="TextBox10" runat="server" ValidationGroup="1" Width="200px"
                                            MaxLength="50" onkeyup="return mak('Span22',50,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label153" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span36" cssclass="labelcount">50</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label154" runat="server" Text="User Id"></asp:Label>
                                        <asp:Label ID="Label155" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txtDBUserId"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtDBUserId" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="TextBox11" runat="server" Width="200px" ValidationGroup="1" MaxLength="20"
                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span11',20)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label156" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span37" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label157" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label158" runat="server" Text="Password"></asp:Label>
                                        <asp:Label ID="Label159" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="txtDBPassword"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="TextBox12" runat="server" ValidationGroup="1" MaxLength="20"
                                            TextMode="Password" Width="200px" onkeyup="return mak('Span16',20,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label160" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span38" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label161" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @ + -)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label162" runat="server" Text="Access Port"></asp:Label>
                                        <asp:Label ID="Label163" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="txtDatabaseAccessPort"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="TextBox13" runat="server" Width="200px" ValidationGroup="1"
                                            MaxLength="20" onkeyup="return mak('Span6',20,this)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                            TargetControlID="txtDatabaseAccessPort" ValidChars="0147852369." />
                                    </label>
                                    <label>
                                        <asp:Label ID="Label164" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span39" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label165" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                               <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                 <label style="width:50px;">
                                        <asp:Button ID="Button8" runat="server" Text="Test FTP" onclick="Button8_Click" 
                                        
                                         />
                                         <asp:Label ID="Label11" runat="server" Text="" ForeColor="Green"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>

                      <fieldset>
                        <legend>
                            <asp:Label ID="Label166" runat="server" Text="Server Default Path For SQL Database LDF File "></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label167" runat="server" Text="Folder Path"></asp:Label>
                                        <asp:Label ID="Label168" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ControlToValidate="txtDatabaseName"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtDatabaseName" ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="TextBox14" runat="server" Width="200px" ValidationGroup="1"
                                            MaxLength="100"  onkeyup="return mak('Span40',100,this)"></asp:TextBox>
                                            <%--onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span1',20)"--%>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label169" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span40" cssclass="labelcount">100</span>
                                        <asp:Label ID="Label170" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ /)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label171" runat="server" Text="FTP URL"></asp:Label>
                                        <asp:Label ID="Label172" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" ControlToValidate="txtDatabaseServerurl"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="TextBox15" runat="server" ValidationGroup="1" Width="200px"
                                            MaxLength="50" onkeyup="return mak('Span22',50,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label173" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span41" cssclass="labelcount">50</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label174" runat="server" Text="User Id"></asp:Label>
                                        <asp:Label ID="Label175" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" ControlToValidate="txtDBUserId"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtDBUserId" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="TextBox16" runat="server" Width="200px" ValidationGroup="1" MaxLength="20"
                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span11',20)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label176" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span42" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label177" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label178" runat="server" Text="Password"></asp:Label>
                                        <asp:Label ID="Label179" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" ControlToValidate="txtDBPassword"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="TextBox17" runat="server" ValidationGroup="1" MaxLength="20"
                                            TextMode="Password" Width="200px" onkeyup="return mak('Span16',20,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label180" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span43" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label181" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @ + -)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label182" runat="server" Text="Access Port"></asp:Label>
                                        <asp:Label ID="Label183" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" ControlToValidate="txtDatabaseAccessPort"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="TextBox18" runat="server" Width="200px" ValidationGroup="1"
                                            MaxLength="20" onkeyup="return mak('Span6',20,this)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                            TargetControlID="txtDatabaseAccessPort" ValidChars="0147852369." />
                                    </label>
                                    <label>
                                        <asp:Label ID="Label184" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span44" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label185" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                               <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                 <label style="width:50px;">
                                        <asp:Button ID="Button9" runat="server" Text="Test FTP" 
                                        onclick="Button9_Click"  />
                                           <asp:Label ID="Label12" runat="server" Text="" ForeColor="Green"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>



                    <fieldset>
                        <legend>SQL Server Detail</legend>
                        <table width="100%">
                            <tr>
                                <td colspan="4">
                                    <label>
                                        <asp:Label ID="Label17" runat="server" Text="Do you have SQL server 2012 Standard and above already installed at the above server ?"></asp:Label>
                                        <asp:Label ID="Label21" runat="server" Text="*" CssClass="labelstar"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadioButtonList1"
                                            ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                    </label>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No (Busiwiz sets up for me)" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel runat="server" ID="pnlsqldetail" Visible="false">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="4">
                                       <label style="width:100%;">
                                            You will need to create two new sql 2012 server instances installed at the above
                                            mentioned server. One for Satellite Master server database and another one for all
                                            busiwiz companies databases. Both these instances will be for exclusive purpose
                                            of database needed for busiwiz sites. Safestserver.com will have exclusive right
                                            to decide which database to attach and which database to be remove. Please ensure that
                                            both this instances should have Mixed Authentication (Windows + SQL Authentication)
                                            selected as authentication type of the instance. Please also ensure that SA password
                                            for both instances is same. All these will be done automatically . So Please do
                                            not run any other databases on any of this two server for security reasons.
                                            <br />
                                            After you have created two new instances , please fill in the following details
                                            about those two instances .
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                       <label style="width:100%;">
                                            1) Details of Sql Server 2012 for Satellite server master
                                            <br />
                                            You have to enable TCP IP protocol only in the Instance of Satellite server master
                                            and not in the instance for company master.
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        <label>
                                            <asp:Label ID="Label3" runat="server" Text="Server Master Sql Instance Name"> </asp:Label>
                                            <asp:Label ID="Label4" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtdefaultsqlinstance"
                                                ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                                ValidationExpression="^([a-z?.,A-Z0-9_\s]*)" ControlToValidate="txtdefaultsqlinstance"
                                                ValidationGroup="1"> </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:TextBox ID="txtdefaultsqlinstance" runat="server" MaxLength="15" onkeyup="return mak('Span3',15,this)"> </asp:TextBox>
                                        </label>
                                        <label style="width:70px">
                                            <asp:Label ID="Label6" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                            <span id="Span3" cssclass="labelcount">15</span>
                                        </label>
                                    </td>
                                    <td style="width: 20%">
                                        <label>
                                            <asp:Label ID="Label77" runat="server" Text="Default DatabaseName"></asp:Label>
                                            <asp:Label ID="Label78" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtdefaultdatabasename"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:TextBox ID="txtdefaultdatabasename" runat="server" MaxLength="20" onkeyup="return mak('Span31',20,this)"> </asp:TextBox>
                                        </label>
                                        <label style="width:70px">
                                            <asp:Label ID="Label79" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="Span31" cssclass="labelcount">20</span>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblSapassword" runat="server" Text="Sa Password"> </asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtSapassword" runat="server" MaxLength="50" TextMode="Password"
                                                onkeyup="return mak('Span5',50,this)" Width="200px"> </asp:TextBox>
                                        </label>
                                        <label style="width:70px">
                                            <asp:Label ID="Label25" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                            <span id="Span5" cssclass="labelcount">50</span>
                                            <asp:Label ID="Label26" CssClass="labelcount" runat="server"> </asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 20%">
                                        <label>
                                            <asp:Label ID="lblport" runat="server" Text="Port"> </asp:Label>
                                            <asp:Label ID="Label7" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtport"
                                                ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Character"
                                                ValidationExpression="^([a-z?.,A-Z0-9_\s]*)" ControlToValidate="txtport" ValidationGroup="1"> </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:TextBox ID="txtport" runat="server" MaxLength="10" onkeyup="return mak('Span4',10,this)"> </asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                TargetControlID="txtport" ValidChars="0147852369" />
                                        </label>
                                        <label style="width:70px">
                                            <asp:Label ID="Label105" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                            <span id="Span4" cssclass="labelcount">10</span>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <label>
                                            2) Details of SQL server 2012 company master
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblSqlinstancename" runat="server" Text="Company Sql Instance Name"> </asp:Label>
                                            <asp:Label ID="Label19" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtSqlinstancename"
                                                ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                                                ErrorMessage="Invalid Character" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                                ControlToValidate="txtSqlinstancename" ValidationGroup="1"> </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtSqlinstancename" runat="server" MaxLength="15" onkeyup="return mak('Span20',15,this)"> </asp:TextBox>
                                        </label>
                                        <label style="width:70px">
                                            <asp:Label ID="Label41" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                            <span id="Span20" cssclass="labelcount">15</span>
                                            <asp:Label ID="Label48" CssClass="labelcount" runat="server" Text=""> </asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblcommasterport" runat="server" Text="Port"> </asp:Label>
                                            <asp:Label ID="Label46" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtcompnayport"
                                                ErrorMessage="*" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Character"
                                                ValidationExpression="^([a-z?.,A-Z0-9_\s]*)" ControlToValidate="txtport" ValidationGroup="1"> </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                         <label>
                                            <asp:TextBox ID="txtcompnayport" runat="server" MaxLength="10" onkeyup="return mak('Span4',10,this)" OnTextChanged="txtcompnayport_TextChanged"> </asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                TargetControlID="txtcompnayport" ValidChars="0147852369" />
                                        </label>
                                        <label style="width:70px">
                                            <asp:Label ID="Label45" runat="server" Text="Max" CssClass="labelcount"> </asp:Label>
                                            <span id="Span11" cssclass="labelcount">10</span>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                    
                    <fieldset>
                        <legend>Server Administrator Details </legend>
                        <table width="100%">
                            <tr>
                                <td style="width: 20%">
                                    <label>
                                        <asp:Label ID="Label87" runat="server" Text="Name:"></asp:Label>
                                        <asp:Label ID="Label88" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtname"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 30%">
                                    <label>
                                        <asp:TextBox ID="txtname" runat="server" MaxLength="50"> </asp:TextBox>
                                    </label>
                                </td>
                                <td style="width: 20%">
                                    <label>
                                        <asp:Label ID="Label89" runat="server" Text="Home Phone:"></asp:Label>
                                        <asp:Label ID="Label90" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txthomephone"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 30%">
                                    <label>
                                        <asp:TextBox ID="txthomephone" runat="server" MaxLength="50"> </asp:TextBox>
                                           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                            TargetControlID="txthomephone" ValidChars="0147852369." />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label95" runat="server" Text="Country Name:"></asp:Label>
                                        <asp:Label ID="Label42" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="ddlcountry"
                                            InitialValue="0" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlcountry" DataTextField="CountryName" DataValueField="CountryId"
                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label97" runat="server" Text="Mobile Company Name:"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlcarriername" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label91" runat="server" Text="Mobile Phone:"></asp:Label>
                                        <asp:Label ID="Label92" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtftpuserid"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtmobilephoneadmin" runat="server" MaxLength="50"> </asp:TextBox>
                                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                            TargetControlID="txtmobilephoneadmin" ValidChars="0147852369." />
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label93" runat="server" Text="Email:"></asp:Label>
                                        <asp:Label ID="Label94" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtftppassword"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtadminemail"
                                            ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtadminemail" runat="server" MaxLength="50"> </asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>



                    <fieldset>
                        <legend>Default Backup FTP account details </legend>
                        <table width="100%">
                            <tr>
                                <td colspan="4">
                                    <label style="width:100%;">
                                        Please mention details of the FTP account where data of all clients on your server
                                        will be backedup as per clients backup plan
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <label style="width:100px;">
                                        <asp:Label ID="Label51" runat="server" Text="FTP URL:"></asp:Label>
                                        <%--<asp:Label ID="Label54" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtftpurl"   ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                    </label>
                                </td>
                                <td style="width: 30%">
                                    <label>
                                        <asp:TextBox ID="txtftpurl" runat="server" MaxLength="50"> </asp:TextBox>
                                    </label>
                                </td>
                                <td style="width: 20%">
                                    <label>
                                        <asp:Label ID="Label81" runat="server" Text="FTP Port:"></asp:Label>
<%--                                        <asp:Label ID="Label82" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtftpport" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                    </label>
                                </td>
                                <td style="width: 30%">
                                    <label>
                                        <asp:TextBox ID="txtftpport" runat="server" MaxLength="50"> </asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <label style="width:100px;">
                                        <asp:Label ID="Label83" runat="server" Text="FTP User ID:"></asp:Label>
                                        <%--<asp:Label ID="Label84" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtftpuserid" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtftpuserid" runat="server" MaxLength="50"> </asp:TextBox>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label85" runat="server" Text="FTP Password:"></asp:Label>
                                        <%--<asp:Label ID="Label86" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtftppassword"  ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtftppassword" runat="server" Width="200px" MaxLength="50" TextMode="Password"></asp:TextBox>
                                    </label>
                                  <label style="width:50px;">
                                        <asp:Button ID="Button2" runat="server" Text="Test FTP" OnClick="Button2_Click" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                            <td>
                            <label style="width:100px;">
                                    <asp:Label ID="lbllocati" runat="server" Text="Location:"></asp:Label>
                                    </label> 
                            </td>
                            <td>  
                            
                                 <label>
                                        <asp:TextBox ID="txtLocation" runat="server" Width="200px" MaxLength="50" ></asp:TextBox>
                                    </label>                          
                            </td>
                            <td>
                                   
                            <label>
                                    <asp:Label ID="Label57" runat="server" Text="FTP Detail:"></asp:Label>
                                    </label> 
                            </td>
                            <td>  
                            
                                 <label>
                                        <asp:TextBox ID="txtdesc" runat="server" Width="200px" MaxLength="50" ></asp:TextBox>
                                    </label> 
                                
                            </td>
                            </tr>
                            <tr>
                             <td>
                             <label style="width:100px;">
                              <asp:Label ID="Label54" runat="server" Text="FTP Folder:"></asp:Label>
                              
                             </label> 
                            </td>
                            <td>
                           <label style="width:200px;">
                           <asp:TextBox ID="txtftpfolder" runat="server" Width="200px" MaxLength="50" ></asp:TextBox>
                           </label> 
                                   <label style="width:150px;">
                              <asp:Label ID="Label58" runat="server" Text="Ex: /C3FTPBack" 
                                    style="color: #FF0000"></asp:Label>     
                                   </label> 
                                    
                            </td>
                            <td>    
                            <label style="width:120px;">
                                   Set as default
                                    </label> 
                           
                                    <asp:CheckBox ID="ckdefult"   runat="server" >
                                        </asp:CheckBox>
                                                             
                            </td>
                           
                            <td>
                                   <label style="width:90px;">
                                   Active
                                    </label> 
                               <label style="width:150px;  margin-top: 0px;">
                                   <asp:CheckBox ID="ckbactive"   runat="server" >
                                        </asp:CheckBox>
                                    </label> 
                                   
                                     <label style="width:50px;">
                                            <asp:Button ID="Button2N" CssClass="btnSubmit" runat="server"  Text="Add" OnClick="Button2_Clickadd"   />                           
                                        </label> 
                            </td>
                            </tr>
                            <tr>
                            <td colspan="4">
                              <asp:GridView ID="gridFileAttach" runat="server"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid"  GridLines="Both" OnRowCommand="gridFileAttach_RowCommand"  PagerStyle-CssClass="pgr" Width="100%">
                              <Columns>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" 
                                   HeaderText="FTP URL">
                                   <ItemTemplate>
                                       <asp:Label ID="Label1" runat="server" Text='<%#Bind("FTPurl") %>'></asp:Label>                              
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" HeaderText="FTP Port">
                                   <ItemTemplate>
                                       <asp:Label ID="lblFTPPort" runat="server" Text='<%#Bind("FTPPort") %>'></asp:Label>                                       
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" HeaderText="UserId">
                                   <ItemTemplate>
                                       <asp:Label ID="lblusrid" runat="server" Text='<%#Bind("FTPUserId") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" HeaderText="FTP Folder">
                                   <ItemTemplate>
                                       <asp:Label ID="lblFTPfolder" runat="server" Text='<%#Bind("FTPfolder") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" HeaderText="Location">
                                   <ItemTemplate>
                                       <asp:Label ID="lbllocation" runat="server" Text='<%#Bind("location") %>'></asp:Label>
                                      <asp:Label ID="lbldesc" runat="server" Text='<%#Bind("Description") %>' Visible="false"></asp:Label>                                      
                                        <asp:Label ID="lblpass" runat="server" Text='<%#Bind("FTPPassword") %>' Visible="false"></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" HeaderText="Active">
                                   <ItemTemplate>
                                        <asp:Label ID="lblactive" runat="server" Text='<%#Bind("active") %>' Visible="true"></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" HeaderText="Default">
                                   <ItemTemplate>
                                       <asp:Label ID="lblselectdefauly" runat="server" Text='<%#Bind("selectdefauly") %>' Visible="true"></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" Visible="false"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnedit" runat="server"  ToolTip="Edit" CommandName="Edite" ImageUrl="~/Account/images/edit.gif" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="0.5%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Set Default"  HeaderStyle-HorizontalAlign="Left" Visible="false"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30px">
                                                <ItemTemplate>                                                    
                                                      <asp:LinkButton ID="LinkButtonSet"     runat="server" CommandName="set"   Text="Set Default" Style="color:#000;"></asp:LinkButton>
                                                      <asp:Label ID="lblserverid" runat="server"  Visible="false"></asp:Label>
                                                      <asp:Label ID="lblid" runat="server"  Visible="false"></asp:Label>
                                                
                                                <asp:Label ID="Label59" runat="server"  Visible="false"></asp:Label></ItemTemplate>
                                                  </asp:TemplateField>       

                                               <asp:TemplateField HeaderText="Test FTP"  HeaderStyle-HorizontalAlign="Left" Visible="false"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30px">
                                                <ItemTemplate>                                                    
                                                      <asp:LinkButton ID="LinkButton1"     runat="server" CommandName="Test"   Text="Test FTP" Style="color: #000;"  ></asp:LinkButton>                                                      
                                                </ItemTemplate>
                                              </asp:TemplateField>                                
                               
                               <asp:ButtonField CommandName="Delete1" HeaderStyle-HorizontalAlign="Left"  HeaderText="Remove" ImageUrl="~/Account/images/delete.gif"  ItemStyle-ForeColor="Black" Text="Remove" />
                           </Columns>
                       </asp:GridView>  
                            </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset>
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnadd" runat="server" Text="Submit" OnClick="btnadd_Click" CssClass="btnSubmit"
                                         />
                                    <asp:Button ID="btnupdate" runat="server" CssClass="btnSubmit" Text="Update" OnClick="btnupdate_Click"
                                        Visible="False"  />
                                    <asp:Button ID="btncancel" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="btncancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label18" runat="server" Text="List of Satellite Servers" Font-Italic="True"></asp:Label>
                    </legend>
                    <div style="float: right;">                        
                        <label>
                            <asp:Button ID="btnprint" runat="server" CssClass="btnSubmit" Text="Printable Version" Visible="false"   CausesValidation="False" OnClick="btnprint_Click" />
                            <input id="Button5" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';" type="button" value="Print" visible="False" />
                        </label>
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                               
                                <label style="width:80px;">
                                    <asp:Label ID="Label40" runat="server" Text="Search:"></asp:Label>
                                </label>
                                <label>
                                    <asp:TextBox ID="TextBox1" MaxLength="50" runat="server"></asp:TextBox>
                                </label>
                                
                                <label style="width:100px;">
                                    <asp:Label ID="Label96" runat="server" Text="Server Type: "></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlServerType" runat="server">
                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Common Server" Value="1"></asp:ListItem>                                        
                                        <asp:ListItem Text="Monthly Exclusive Lease" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Monthly Shared Lease" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Sell" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Customer's own server" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                                 <label style="width:80px;">
                                    <asp:Label ID="Label16" runat="server" Text="Status: "></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                        <asp:ListItem Selected="True"  Text="Active" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                                <label >
                                    <asp:Button ID="Button4" runat="server" Text="Go" OnClick="Button4_Click" />
                                </label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td align="right" style="text-align:right;" >
                                 <asp:CheckBox ID="chk_conn" runat="server" Text="Check Server Connection" oncheckedchanged="chk_uploadcode_CheckedChanged"  />
                                
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label8" runat="server" Font-Italic="true" Text="List of Satellite Servers "></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="id"
                                                    Width="100%" EmptyDataText="No Record Found." OnRowCommand="GridView2_RowCommand" OnRowDataBound="GridView2_RowDataBound"
                                                    OnRowDeleting="GridView2_RowDeleting" OnRowEditing="GridView2_RowEditing" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:TemplateField Visible="false" HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="8%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldatetime" runat="server" Text='<%# Eval("DateCreated","{0:MM/dd/yyy}") %>'> </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Server Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("ServerName") %>'> </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Server Full Name" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblserverfullname" runat="server" Text='<%# Eval("ServerComputerFullName") %>'> </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Server loction" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblserverloction" runat="server" Text='<%# Eval("serverloction") %>'> </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Public IP" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPublicIp" runat="server" Text='<%# Eval("PublicIp") %>'> </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Local IP" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbllocalIp" runat="server" Text='<%# Eval("Ipaddress") %>'> </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Company SQL instance" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSqlinstancename" runat="server" Text='<%# Eval("Sqlinstancename") %>'> </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Port" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblport" runat="server" Text='<%# Eval("port") %>'> </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Satellite Site URL" HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBusiwizsatellitesiteurl" runat="server" Text='<%# Eval("Busiwizsatellitesiteurl") %>'> </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Encrypt Key" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblenck" runat="server" Text='<%# Eval("Enckey") %>'> </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="All FTP Connection Staus" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblftpstatus" runat="server" > </asp:Label>
                                                                <%--Text='<%# Eval("Default_FTPUrl") %>'--%>
                                                                  <asp:Label ID="lblftpserID" runat="server" Text='<%# Eval("ID") %>' Visible="false" > </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Server On Staus" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblserverstatus" runat="server" > </asp:Label>
                                                                <%--Text='<%# Eval("Default_FTPUserId") %>'--%>
                                                                  <asp:Label ID="lblserverserID" runat="server" Text='<%# Eval("ID") %>' Visible="false" > </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Statuslabel") %>'> </asp:Label>
                                                                  <asp:Label ID="lblserID" runat="server" Text='<%# Eval("ID") %>' Visible="false" > </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total No of Company Host" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                              <ItemTemplate>                                                                    
                                                                    <asp:LinkButton ID="linl_totalnoofcompany"     runat="server" CommandName="viewplan"  CommandArgument='<%# Eval("ID") %>' Text="View Host Company" Style="color: #000;"  ></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                              
                                                        <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ID") %>'   ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Width="3%" HeaderImageUrl="~/images/trash.jpg">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ToolTip="Delete"  CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                                </asp:ImageButton>
                                                            </ItemTemplate>
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
                        <td style="width:100%">
                         
                <asp:Panel ID="pnladdress" runat="server" BorderStyle="Outset" ScrollBars="Vertical" Height="500px" Width="80%" BackColor="#CCCCCC" BorderColor="#666666">
                           
                                 <table width="100%">
                                 <tr>
                                 <td>
                                    <asp:Label ID="lbl_servername" runat="server" Text="" Height="24px" style="color: #FFFFFF"></asp:Label>
                                 </td>
                                    <td align="right" style="10%"> 
                                          <asp:ImageButton ID="ibtnCancelCabinetAdd" runat="server" ImageUrl="~/Account/images/closeicon.png"
                                                AlternateText="Close" CausesValidation="False" ToolTip="Close"></asp:ImageButton>
                                    </td>
                                 </tr>
                                <tr>
                                    <td colspan="2">
                                    <label>
                                        <asp:Label ID="Label101" runat="server" Text="Filter By  Portal"></asp:Label>
                                        <asp:DropDownList ID="ddlportal" Width="200px"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlportal_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label102" runat="server" Text="Filter By Plan"></asp:Label>
                                        <asp:DropDownList ID="ddlsortPlan" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </label>  
                      
                                    <label style="width:150px;">
                                    License start date
                                    <asp:DropDownList ID="ddllicensestart" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddllicensestart_SelectedIndexChanged" Width="150px" >
                                                      <asp:ListItem Value="0" Text="--Select All--"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Today"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="This Week"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="This Month"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="This Year"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="Custom"></asp:ListItem>                                        
                                                    </asp:DropDownList>
                                    </label> 
                                                         <asp:Panel ID="pnllicensedate" runat="server" Visible="false">
                                                          <label style="width:80px;">
                                                                        <asp:Label ID="Label108" runat="server" Text="Start Date"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="TextBox2"  ErrorMessage="*" ValidationGroup="3" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                        <asp:TextBox ID="TextBox2" Width="80px" runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" PopupButtonID="ImageButton1" TargetControlID="TextBox2">
                                                                        </cc1:CalendarExtender>
                                                                    </label>
                                                                    <label style="width:40px;">
                                                                    <br />
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                                                    </label>
                                                                  
                                                                    <label style="width:80px;">
                                                                        <asp:Label ID="Label109" runat="server" Text="End Date"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="TextBox3" ErrorMessage="*" ValidationGroup="3" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                        <asp:TextBox ID="TextBox3" Width="80px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddllicensestart_SelectedIndexChanged"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy" PopupButtonID="ImageButton2"  TargetControlID="TextBox3">
                                                                        </cc1:CalendarExtender>
                                                                    </label>
                                                                    <label style="width:40px;">
                                                                    <br />
                                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                                                    </label>
                                                                    </asp:Panel>
                                                    </td>
                                                    </tr>
                                                 <asp:Panel ID="Panel4" runat="server" Visible="false">
                                                    <tr>
                                                    <td colspan="2">
                 
                                                        <label>
                                                                <asp:DropDownList ID="ddlActive" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlActive_SelectedIndexChanged" Visible="false" >
                                                                <asp:ListItem>---Select All---</asp:ListItem>
                                                                <asp:ListItem Selected="True"  Value="1">Active</asp:ListItem>
                                                                <asp:ListItem Value="0">Inactive </asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="Label98" runat="server" Text=""  Width="190px" Height="24px"></asp:Label>
                                                            <asp:DropDownList ID="ddlfilters" Visible="false"  runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlActive_SelectedIndexChanged">
                                                                <asp:ListItem>---Show All Record---</asp:ListItem>
                                                                <asp:ListItem Selected="True"  Value="1">Active filter only</asp:ListItem>
                                                                <asp:ListItem Value="0">Inactive filter only</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </label> 
                   
                                                            <label>
                                                                <asp:TextBox ID="txtsortsearch" runat="server" Visible="false"> </asp:TextBox>
                                                            </label>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
            
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:GridView ID="GV_companyshow" runat="server" AutoGenerateColumns="false" DataKeyNames="CompanyId"
                                                                            EmptyDataText="No Record Found." AllowPaging="True" Width="100%" Height="500px"
                                                                            CssClass="mGrid"  PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  
                                                                            PageSize="10" OnPageIndexChanging="GV_companyshow_PageIndexChanging"
                                                                            OnRowCommand="GV_companyshow_RowCommand" OnRowDeleting="GV_companyshow_RowDeleting" OnRowEditing="GV_companyshow_RowEditing1"
                                                                            OnRowDataBound="GV_companyshow_RowDataBound">
                                                                            <Columns>                                        
                                                                                <asp:TemplateField HeaderText="CompanyLoginId" SortExpression="CompanyLoginId" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblcompanlogin" runat="server" Text='<%# Bind("CompanyLoginId")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="15%" />
                                                                                </asp:TemplateField>
                                                                                  <asp:TemplateField HeaderText="Portal Name" SortExpression="ContactPerson " ItemStyle-Width="25%"  HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblContactPerson" runat="server" Text='<%# Bind("PortalName")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="15%" />
                                                                                </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="PricePlan Name" SortExpression="PricePlanName" ItemStyle-Width="25%"  HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_planid" runat="server" Text='<%# Bind("PricePlanId")%>' Visible="false"></asp:Label>
                                                    
                                                                                         <asp:Label ID="lbl_LinkButton1" runat="server" Text='<%# Bind("PricePlanName")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="15%" />
                                                                                </asp:TemplateField>
                                          
                                                                                <asp:TemplateField HeaderText="Company Name" SortExpression="CompanyName" ItemStyle-Width="15%"  HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblemaildisplaynameName" runat="server" Text='<%# Bind("CompanyName")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="15%" />
                                                                                </asp:TemplateField>
                                           
                                           
                                                                                 <asp:BoundField DataField="LicenseDate" HeaderStyle-HorizontalAlign="Left" HeaderText="License Date" SortExpression="LicenseDate" DataFormatString="{0:d}" >
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="License Period (Days)" SortExpression="LicensePeriod" ItemStyle-Width="12%" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_noofday" runat="server" Text='<%# Bind("LicensePeriod")%>'></asp:Label>
                                                                                         <asp:Label ID="lbllicensedathide" runat="server" Text='<%# Bind("LicenseDate")%>' Visible="false"></asp:Label>
                                                                                         <asp:Label ID="lblserverid" runat="server" Text='<%# Bind("ServerId")%>' Visible="false" ></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="12%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="License End date" SortExpression="LicensePeriod" ItemStyle-Width="12%" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_enddate" runat="server" Text=""></asp:Label>
                                                  
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="12%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Delete"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" Visible="false" >
                                                                                    <ItemTemplate>
                                                                                    <%--HeaderImageUrl="~/images/Com_download.png"--%>
                                                                                        <asp:ImageButton ID="imgbtndelete" runat="server" Height="25px"  CommandArgument='<%# Eval("CompanyId") %>' ToolTip="Delete" CommandName="Delete" ImageUrl="~/images/deletegreen.png" />
                                                                                         <asp:ImageButton ID="imgbtnrestore" runat="server" Height="25px" CommandArgument='<%# Eval("CompanyId") %>' ToolTip="Restore" CommandName="Restore" ImageUrl="~/images/cantaccess.jpg" Visible="false" Enabled="false"  />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="0.5%" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                            
                                            
                                                                            </Columns>
                                                                            <PagerStyle CssClass="pgr" />
                                                                            <AlternatingRowStyle CssClass="alt" />
                                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                         </table> 
            
     
                    </asp:Panel>
                      <asp:ImageButton ID="ImageButton5" runat="server" Visible="true" ToolTip="." />
                     
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="pnladdress" TargetControlID="ImageButton5" X="150" Y="100" CancelControlID="ibtnCancelCabinetAdd">
                </cc1:ModalPopupExtender>
                        </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
       
       

    <%--Extra--%>
     
    <asp:Panel ID="pnlpr" runat="server" ScrollBars="Horizontal" Height="1px" Width="2px" Visible="false" >
                                 <label >
                                        <asp:Label ID="Label82" runat="server" Text="How this server will be used: "> </asp:Label>                                                                          
                                    </label>
                                                <asp:GridView ID="GvRoleName" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"  EmptyDataText="There is no data." AllowSorting="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                 OnRowCommand="GvRoleName_RowCommand"  Width="250px" ShowHeader="false"  OnPageIndexChanging="GvRoleName_PageIndexChanging" OnSorting="GvRoleName_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Role Name" SortExpression="Role_id" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="180px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblp" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                <asp:Label ID="lblroleid" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="cbHeader" runat="server" OnCheckedChanged="ch1_chachedChanged" AutoPostBack="true" />
                                                                <asp:Label ID="check" runat="server" ForeColor="White" Text=""  />
                                                            </HeaderTemplate>
                                                            <ItemTemplate >
                                                                <asp:CheckBox ID="cbItem"  runat="server"  OnCheckedChanged="cbItem_chachedChanged" AutoPostBack="true"  />
                                                                <asp:CheckBox ID="chkdef"  runat="server" Visible="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                 <input id="hdnFileName" runat="server" name="hdnFileName" style="width: 1px" type="hidden" />
                                                 <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                 <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                   </asp:Panel>
</asp:Content>
