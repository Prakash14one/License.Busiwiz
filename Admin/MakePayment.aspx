<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="~/Admin/MakePayment.aspx.cs" Inherits="MakePayment" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script language="javascript" type="text/javascript">

 function RealNumWithDecimal(myfield, e, dec)
{

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
   if(key==13)
{
return false;
}
if ((key==null) || (key==0) || (key==8) || (key==9) || (key==27) )
{
return true;
}
else if ((("0123456789.").indexOf(keychar) > -1))
{
return true;
}
// decimal point jump
else if (dec && (keychar == "."))
{

 myfield.form.elements[dec].focus();
  myfield.value="";
 
return false;
}
else
{
  myfield.value="";
  return false;
}
}
    </script>

   <table id="pagetbl" cellpadding="0" cellspacing="0">
        <tr>
            <td class="hdng">
                Make Payment<asp:Label ID="lblmsg"  runat="server" BackColor="#FF8080" Width="100%" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td>
    <table id="pagetbl" cellpadding="0" cellspacing="0">
    <tr > <td colspan="3"></td> </tr>
    </table>
                <table id="pagetbl" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="subhdng">
                            Company Information</td>
                    </tr>
                    <tr>
                        <td class="downbg">
                            <table id="pagetbl" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="column1" colspan="2">
                                    </td>
                                    <td class="column2">
                                        &nbsp;</td>
                                    <td class="c2">
                                    </td>
                                    <td class="c2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="column1" colspan="2">Company Name :</td>
                                    <td class="column2" colspan="2">
             <asp:Label ID="lblcompanyname" runat="server" CssClass="label"></asp:Label></td>
                                    <td class="c2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="column1" colspan="2" style="height: 25px">Contact Person Name :</td>
                                    <td class="column2" colspan="2" style="height: 25px">
               <asp:Label ID="lblcontPerson" runat="server" CssClass="label"></asp:Label></td>
                                    <td class="c2" style="height: 25px">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="column1" colspan="2">Contact Person Designation:</td>
                                    <td class="column2" colspan="2">
             <asp:Label ID="lblCOnDesi" runat="server" CssClass="label"></asp:Label></td>
                                    <td class="c2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="column1" colspan="2">Address :</td>
                                    <td class="column2" colspan="2">
               <asp:Label ID="lbladdress" runat="server" CssClass="label"></asp:Label></td>
                                    <td class="c2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="column1" colspan="2">Email :</td>
                                    <td class="column2" colspan="2">
             
                <asp:Label ID="lblemail" runat="server" CssClass="label"></asp:Label></td>
                                    <td class="c2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="column1" colspan="2">Phone :</td>
                                    <td class="column2" colspan="2">
            <asp:Label ID="lblphone" runat="server" CssClass="label"></asp:Label></td>
                                    <td class="c2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="column1" colspan="2">Fax :</td>
                                    <td class="column2" colspan="2">
                <asp:Label ID="lblfax" runat="server" CssClass="label"></asp:Label></td>
                                    <td class="c2">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="subhdng">
                            Account Information</td>
                    </tr>
                    <tr>
                        <td class="downbg">
                            <table id="pagetbl" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="column1">
                                    </td>
                                    <td class="column2">
                                        &nbsp;</td>
                                    <td class="column1">
                                    </td>
                                    <td class="column2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="column1">Subscription Plan :</td>
                                    <td class="column2" colspan="2">
                <asp:Label ID="lblplan" runat="server" CssClass="label"></asp:Label></td>
                                    <td class="column2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="column1">Amount :</td>
                                    <td class="column2" colspan="2">
                <asp:Label ID="lblamt" runat="server" Font-Bold="False"></asp:Label>
                                    </td>
                                    <td class="column2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="column1">
                                       Company ID :</td>
                                    <td class="column2" colspan="2">
            <asp:Label ID="lblCompId" runat="server" CssClass="label"></asp:Label></td>
                                    <td class="column2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="column1" style="height: 18px">Admin User ID :</td>
                                    <td class="column2" colspan="2" style="height: 18px">
           <asp:Label ID="lblAdminUID" runat="server" CssClass="label"></asp:Label></td>
                                    <td class="column2" style="height: 18px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="subhdng">
                            Hosting Site Info</td>
                    </tr>
                    <tr>
                        <td class="downbg">
                            <table id="pagetbl" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
            <asp:Panel runat="server" ID="pnlDomain" Width="100%">
            <table cellpadding="0" cellspacing="0" id="pagetbl">
            <tr>
            <td class="column1">
                Domain http://</td>
            <td class="column2">
             <asp:Label ID="lblDomain" runat="server" CssClass="label"></asp:Label>.ifilecabinet.com</td>
            </tr>--%>
            </table>
            </asp:Panel>
              <%--  <asp:Panel ID="pnlServer" runat="server" Width="100%">
                    <table id="pagetbl" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="column1">Sql Server Name :</td>
                            <td class="column2" colspan="2">
                               <asp:Label ID="lblSqlServerName" runat="server" CssClass="label"></asp:Label></td>
                            <td class="column2">
                            </td>
                        </tr>
                        <tr>
                            <td class="column1">
                               User Name :</td>
                            <td class="column2" colspan="2">
                               <asp:Label ID="lblSqlServerUName" runat="server" CssClass="label"></asp:Label></td>
                            <td class="column2">
                            </td>
                        </tr>
                        <tr>
                            <td class="column1">
                                Password :</td>
                            <td class="column2" colspan="2">
                               <asp:Label ID="lblSqlServerUPassword" runat="server" CssClass="label"></asp:Label></td>
                            <td class="column2">
                            </td>
                        </tr>
                         <tr>
                            <td class="column1">
                               Database Name :</td>
                            <td class="column2" colspan="2">
                               <asp:Label ID="lblDataBaseName" runat="server" CssClass="label"></asp:Label></td>
                             <td class="column2">
                             </td>
                        </tr>
                    </table>
                </asp:Panel>
              --%>                      </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
       <asp:ImageButton ID="submit" runat="server" AlternateText="Make payments with PayPal - it's fast, free and secure!"
                    ImageUrl="https://www.paypal.com/en_US/i/btn/btn_paynowCC_LG.gif" OnClick="submit_Click"
                    PostBackUrl="https://www.paypal.com/cgi-bin/webscr"
                    Style="width: 150px; height: 50px" Width="177px" /></td>
                    </tr>
                    <tr>
                        <td>
              <input name="cmd" style="width: 123px" type="hidden" value="_xclick" /><input name="business" style="width: 121px" type="hidden" value="va@aacpa.us" /><input name="item_name" style="width: 121px" type="hidden" value="Busiwiz.com - Online Document Management" /><input name="item_number" style="width: 119px" type="hidden" value="<%=orderno %>" /><input name="amount" style="width: 115px" type="hidden" value="<%=amount %>" /><input name="page_style" style="width: 111px" type="hidden" value="Primary" /><input name="no_shipping" style="width: 107px" type="hidden" value="0" /><input name="return" style="width: 118px" type="hidden" value="http://license.busiwiz.com/admin/CustomerPaymentAfter.aspx?id=<%=orderno %>" /><input name="cancel_return" style="width: 116px" type="hidden" value="http://license.busiwiz.com/MakePayment.aspx?id=<%=orderno %>" /><input name="no_note" style="width: 114px" type="hidden" value="1" /><input name="currency_code"
                    style="width: 110px" type="hidden" value="USD" /><input name="lc" style="width: 99px" type="hidden" value="US" /><input name="bn" style="width: 109px" type="hidden" value="PP-BuyNowBF" /><%-- <input name="first_name" style="width: 108px" type="hidden" value="<%=name %>" />
                <input name="last_name" style="width: 119px" type="hidden" value="<%=name %>" />
                <input name="address1" style="width: 102px" type="hidden" value="<%=address %>" 
/&gt; <INPUT style="WIDTH: 110px" type=hidden value="<%=city %>" name="city" /> 
<INPUT style="WIDTH: 93px" type=hidden value="<%=state %>" name="state" /> 
<INPUT style="WIDTH: 77px" type=hidden value="<%=zip %>" name="zip" /> <INPUT 
style="WIDTH: 98px" type=hidden value="<%=phone %>" name="night_phone_a" 
/>--%&gt;<%--   <input name="salesorder" type="hidden" value='<%orderno%>' />--%>
                <%-- <input name="first_name" style="width: 108px" type="hidden" value="<%=name %>" />
                <input name="last_name" style="width: 119px" type="hidden" value="<%=name %>" />
                <input name="address1" style="width: 102px" type="hidden" value="<%=address %>" />
                <input name="city" style="width: 110px" type="hidden" value="<%=city %>" />
                <input name="state" style="width: 93px" type="hidden" value="<%=state %>" />
                <input name="zip" style="width: 77px" type="hidden" value="<%=zip %>" /&gt; <INPUT style="WIDTH: 98px" type=hidden value="<%=phone %>" name="night_phone_a" />--%&gt;<%--   <input name="salesorder" type="hidden" value='<%orderno%>' />--%>
                <input name="notify_url" style="width: 113px" type="hidden" value="http://license.busiwiz.com/Admin/Notify.aspx" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

