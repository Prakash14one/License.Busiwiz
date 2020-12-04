<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="IpAddressAllowed.aspx.cs" Inherits="Manage_Ip_Address_Allowed" %>

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

    <script language="javascript">
         function mask(evt)
          {
               // alert(evt.srcElement.value.length);
               var part = evt.srcElement.value.split('*');
               evt.keyCode
               if (part.length> 1) 
                {
                   return false;
                }
              else if(evt.srcElement.value.length>0)
               {   
                   if(evt.keyCode==42)
                       {
                       return false;
                       }
                }
           
                
            
            }
            
            
function checktextboxmaxlength(txt, maxLen, evt) {
            try {
           
               if (evt.srcElement.value>255) 
                {
                 txt.value = txt.value.substring(0, maxLen-1);
                   return false;
                }
               
            }
            catch (e) {

            }
        }
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
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
        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
            }
        } 
    </script>

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }  
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="statuslable" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <asp:Panel ID="Panel1" Width="100%" runat="server" Visible="false">
                <fieldset>
                    
                    <div style="clear: both;">
                    </div>
                    <label class="first">
                        <asp:Label ID="lblhe" runat="server" Text="Do you wish to set website access restrictions for the admin sections of your website based on the public IP address of the admin users/employees?"></asp:Label>
                        <%--<asp:RadioButtonList ID="RadioButtonList2" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Selected="True">Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:RadioButtonList>--%>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:RadioButtonList ID="rdlist1" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdlist1_SelectedIndexChanged"
                        AutoPostBack="True">
                        <asp:ListItem Text="Yes" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" ></asp:ListItem>
                    </asp:RadioButtonList>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <%--<asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button2"
                        CssClass="btnSubmit" Text="Submit" />--%>
                    </label>
                    <label>
                        <%--<asp:Button runat="server" ID="Button3" CssClass="btnSubmit" Text="Cancel" />--%>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlbtn" runat="server" Visible="False">
                        <asp:Button ID="btnaddn" runat="server" CssClass="btnSubmit" Text="Add new allowed IP Rule"
                            OnClick="btnaddn_Click" Visible="False" />
                    </asp:Panel>
                    <%--<asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button6"
                        CssClass="btnSubmit" Text="Add New Allow Ip Rule" />--%>
                    <div style="clear: both;">
                    </div>
                </fieldset></asp:Panel>
                <asp:Panel ID="pnlusercid" Width="100%" runat="server">
                    <fieldset>
                        <asp:RadioButtonList ID="rdipright" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdipright_SelectedIndexChanged"
                            AutoPostBack="True">
                            <asp:ListItem Text="Do you wish to set an allowed IP address for all the users of your company?&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;or" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Do you wish to set an allowed IP address for a specific user of your company?"></asp:ListItem>
                        </asp:RadioButtonList>
                    </fieldset>
                     <asp:Panel ID="pnlexample1" runat="server" Visible="False">
                     <fieldset>
                     <label>
                     <asp:Label ID="Label12" runat="server" Text="For example, you can set the public IP address given by your ISP for your internet connection shared by all office members of your business as an allowed IP address so that all the users of your office would be able to log in to the system using that internet connection. "></asp:Label>
                     </label>
                     </fieldset></asp:Panel>
                      <asp:Panel ID="pnlexample2" runat="server" Visible="False">
                     <fieldset>
                       <label>
                     <asp:Label ID="Label16" runat="server" Text="For example, you can see the public IP address given by the ISP for the home connection of any user as the allowed IP address for that user only so that user can log in from home also into the system. "></asp:Label>
                     </label>
                     </fieldset></asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnls" runat="server" Width="100%" Visible="false">
                        <fieldset>
                            <legend>
                                <asp:Label ID="lblselecttype" Visible="false" runat="server" Text="The following IP address will be allowed to login to the admin sections of your website for any admin/employee of your company."></asp:Label>
                            </legend>
                            <div style="float: right;">
                                <asp:Button ID="Button2" CssClass="btnSubmit" runat="server" Text="Add New IP Address"
                                    OnClick="Button2_Click" />
                            </div>
                             <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlusershow" runat="server" Visible="false">
                       <table width="100%">
                                    <tr>
                                     <td width="25%">
                                      <label>
                                                    <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                                                    
                                                    <asp:DropDownList ID="ddlbusiness" runat="server" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </label>
                                     </td>
                                        <td width="25%">
                                         <label>
                                                    <asp:Label ID="Label2" runat="server" Text="Select User ID"></asp:Label>
                                                    <asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddluser"
                                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                                    
                                                    <asp:DropDownList ID="ddluser" runat="server"  AutoPostBack="true"
                                                onselectedindexchanged="ddluser_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                     </td>
                                        <td width="25%">
                                     </td>
                                        <td width="25%">
                                     </td>
                                     </tr>
                                     </table>
                                               
                                               
                                            </asp:Panel>
                                            
                     <div style="clear: both;">
                    </div>
                  
                            <asp:Panel ID="pnlvs" runat="server" Visible="false">
                               
                                <table width="100%">
                                 <asp:Panel ID="pnlcurrentip" runat="server"  Visible="false">
                                 
                                    <tr>
                                 
                                        <td width="25%">
                                            <label>
                                                <asp:Label ID="crtip" runat="server" Text="Your current IP address is:"></asp:Label>
                                            </label>
                                           
                                        </td>
                                         <td width="25%">
                                          <label>
                                                <asp:Label ID="lbldynip" runat="server"></asp:Label>
                                            </label>
                                             
                                        </td>
                                        <td width="25%">
                                          <asp:Button ID="btnadtolist" runat="server" Text="Add to List" CssClass="btnSubmit"
                                                OnClick="btnadtolist_Click" />
                                           
                                        </td>
                                         <td width="25%">
                                          <label>
                                                <asp:Label ID="lblalready" runat="server" Text="IP address already on the list" Visible="false"></asp:Label>
                                            </label> 
                                        </td>
                                        
                                  
                                    </tr>
                                    </asp:Panel>
                                    <tr>
                                         <td width="25%">
                                          <label>
                                                <asp:Label ID="Label3" runat="server" Text="Add Allowed IP Address"></asp:Label>
                                                <asp:Label ID="Label7" runat="server" Text="*"></asp:Label>
                                            </label>
                                            
                                        </td>
                                         <td width="25%">
                                          <label>
                                                <asp:TextBox ID="txtIpAddress" runat="server" MaxLength="3" Width="40px" onkeypress="return mask(event)"
                                                    onkeyup="return checktextboxmaxlength(this,3,event)"></asp:TextBox>
                                                <asp:Label ID="Label9" runat="server" Text="(0-255 *)"></asp:Label>
                                                <cc1:FilteredTextBoxExtender ID="txtEndzip_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtIpAddress" ValidChars="0123456789*">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIpAddress"
                                                    ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                             <label>
                                                <asp:TextBox ID="txtip1" runat="server" MaxLength="3" Width="40px" onkeypress="return mask(event)"
                                                    onkeyup="return checktextboxmaxlength(this,3,event)"></asp:TextBox>
                                                <asp:Label ID="Label13" runat="server" Text="(0-255 *)"></asp:Label>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                    TargetControlID="txtip1" ValidChars="0123456789*">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtip1"
                                                    ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                              <label>
                                                <asp:TextBox ID="txtip2" runat="server" MaxLength="3" Width="40px" onkeypress="return mask(event)"
                                                    onkeyup="return checktextboxmaxlength(this,3,event)"></asp:TextBox>
                                                <asp:Label ID="Label14" runat="server" Text="(0-255 *)"></asp:Label>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                    TargetControlID="txtip2" ValidChars="0123456789*">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequFieldValidator4" runat="server" ControlToValidate="txtip2"
                                                    ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                             <label>
                                                <asp:TextBox ID="txtip3" runat="server" MaxLength="3" Width="40px" onkeypress="return mask(event)"
                                                    onkeyup="return checktextboxmaxlength(this,3,event)"></asp:TextBox>
                                                <asp:Label ID="Label15" runat="server" Text="(0-255 *)"></asp:Label>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                    TargetControlID="txtip3" ValidChars="0123456789*">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtip3"
                                                    ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                           
                                        </td>
                                         <td width="25%">
                                            <asp:Button ID="btnaddn0" CssClass="btnSubmit" runat="server" Text="Add to the Allowed IP Address List"
                                    ValidationGroup="1" OnClick="btnaddn0_Click" />
                                        </td>
                                         <td width="25%">
                                        </td>
                                      
                                       
                                    </tr>
                                    <tr>
                                        <td width="25%">
                                        </td>
                                       <td colspan="3">
                                            <label>
                                                <asp:Label ID="dgfs" runat="server" Text="When * is inputed, any value from 0-255 is an allowed value."></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                                <div style="clear: both;">
                                </div>
                               
                            </asp:Panel>
                        </fieldset>
                        <fieldset>
                            <legend>
                                <asp:Label ID="lblheadc" runat="server" Text="List of Allowed IP Addresses for Any Admin and Employee Users of the Company"></asp:Label></legend>
                            <div style="float: right;">
                                <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                                    OnClick="Button1_Click" />
                                <input id="Button7" class="btnSubmit" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    type="button" value="Print" visible="false" />
                            </div>
                            <div style="clear: both;">
                            </div>
                            <asp:Panel ID="pnlfilteruser" runat="server" Visible="false">
                                <label>
                                    <asp:Label ID="Label10" runat="server" Text="Business Name"></asp:Label>
                                    <%-- </label>
                    <label>--%>
                                    <asp:DropDownList ID="ddlfilterbus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label11" runat="server" Text="Employee Name - User Name"></asp:Label>
                                    <asp:DropDownList ID="ddlfilteruser" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilteruser_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </asp:Panel>
                            <div style="clear: both;">
                            </div>
                            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                <table width="100%">
                                    <tr align="center">
                                        <td>
                                            <div id="mydiv" class="closed">
                                                <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="lblCompany" Font-Size="20px" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="lblbusiness" runat="server" Font-Size="20px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="Label4" runat="server" Font-Size="18px" Text="List of Allowed Ip Address for Access"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label5" runat="server" Font-Size="16px" Text="User :"></asp:Label>
                                                            <asp:Label ID="lbluserp" runat="server" Font-Size="16px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlusergrid" runat="server">
                                                <asp:GridView ID="grduser" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AutoGenerateColumns="False" CellPadding="0" DataKeyNames="Id" Width="100%" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="grduser_PageIndexChanging"
                                                    OnSorting="grduser_Sorting" EmptyDataText="No Record Found." PageSize="20" OnRowDeleting="grduser_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business Name" SortExpression="wname" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblwname" runat="server" Text='<%#Eval("wname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblempname" runat="server" Text='<%#Eval("EmployeeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="User Name" SortExpression="Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblumae" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Allowed IP Address" SortExpression="IpAddress" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIp" runat="server" Text='<%#Eval("IpAddress") %>'></asp:Label>
                                                                <asp:Label ID="IblId" runat="server" Text='<%#Eval("Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                            HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                                    ToolTip="Delete" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                                </asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <%--<PagerStyle CssClass="GridPager" />
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                                                        <FooterStyle CssClass="GridFooter" />
                                                        <RowStyle CssClass="GridRowStyle" />--%>
                                                </asp:GridView>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlms" Visible="false" runat="server">
                                                                       <label>												
													  <asp:Label ID="Label8" runat="server" Text="This is a great tool for the security of your data."></asp:Label>
													  <br />
													  <br />
                                     <asp:Label ID="bfff" runat="server" Text="For example, you can set the IP address of your office as an allowed IP to access your website.<br /> 														
Only the computers in the local network of your office would be able to access the admin sections of your website and no one else.	<br /> 														
You can also set multiple IP addresses on the 'allowed' list.  <br /> 															
For example, your office IP address, your home IP address, your employee IP address, etc.	<br /> 	<br /> 														
														
You can also set another IP address rule for any specific employee/admin which they can use to log into your website.  (Example, Home IP address, another office IP address)	<br /> 	<br /> 														
														
You must have a static IP address from your ISP to use this feature effectively.<br /> 															
If you do not know your static IP public address, you can inquire with your ISP."	></asp:Label>
</label>
                                            </asp:Panel>
                                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </fieldset></asp:Panel>
                    <div style="clear: both;">
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
       
    </asp:UpdatePanel>
</asp:Content>
