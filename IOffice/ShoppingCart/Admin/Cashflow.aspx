<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="Cashflow.aspx.cs"
    Inherits="Cashflow" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlreport.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024px,height=768px,toolbar=1,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }



        function itemUpdate() {

            var amt = document.getElementById("amount");
            var t = document.getElementById("ctl00_contentMasterPage_TextBox1");
            amt.value = t.value;


            var order = document.getElementById("item_number");
            var a = document.getElementById("ctl00_contentMasterPage_txtorder");
            order.value = a.value;


            //   
            var name = document.getElementById("first_name");
            var b = document.getElementById("ctl00_contentMasterPage_txtname");
            name.value = b.value;

            var lname = document.getElementById("last_name");
            var c = document.getElementById("ctl00_contentMasterPage_txtname");
            lname.value = c.value;

            var address = document.getElementById("address1");
            var d = document.getElementById("ctl00_contentMasterPage_txtAddress");
            address.value = d.value;

            var city = document.getElementById("city");
            var e = document.getElementById("ctl00_contentMasterPage_txtCitypay");
            city.value = e.value;

            var state = document.getElementById("state");
            var f = document.getElementById("ctl00_contentMasterPage_txtstate");
            state.value = f.value;

            var zip = document.getElementById("zip");
            var g = document.getElementById("ctl00_contentMasterPage_txtzip");
            zip.value = g.value;

            var phone = document.getElementById("night_phone_a");
            var h = document.getElementById("ctl00_contentMasterPage_txtphone");
            phone.value = h.value;


            return;
        }
    </script>

    <asp:UpdatePanel ID="upo1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <legend>Cash Flow</legend>
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%">
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table width="100%">
                                                <tr>
                                                    <td align="right" style="width: 25%;">
                                                        Business Name
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddwarehouse"
                                                            ErrorMessage="*" InitialValue="0" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddwarehouse" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="right" style="width: 140px;">
                                                        Cash Flow as on
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdateto"
                                                            ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td align="left" style="width: 85px;">
                                                        <asp:TextBox ID="txtdateto" runat="server" Width="80px"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/cal_btn.jpg" />
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton4"
                                                            TargetControlID="txtdateto">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdateto" />
                                                    </td>
                                                    <td align="left" style="width: 35%;">
                                                        <asp:Button ID="imgbtngo" runat="server" CssClass="btnSubmit" OnClick="imgbtngo_Click"
                                                            Text="Go" ValidationGroup="1" Visible="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 28px">
                                        </td>
                                        <td align="left" colspan="2" style="height: 28px">
                                        </td>
                                        <td style="height: 28px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="1" align="center">
                                        </td>
                                        <td>
                                        </td>
                                        <td style="width: 13%;" align="right">
                                        </td>
                                        <td style="width: 20%;" align="right">
                                            <label>
                                                <asp:DropDownList ID="ddlExport" runat="server" AutoPostBack="True" Width="130px"
                                                    OnSelectedIndexChanged="ddlExport_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                            <label>
                                                <input type="button" value="Print" id="btnPrint" runat="server" class="btnSubmit"
                                                    onclick="javascript:CallPrint('pnlreport')" />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <div>
                                                <asp:Panel ID="pnlreport" runat="server" Visible="false" ScrollBars="None" Width="100%">
                                                    <div>
                                                        <table width="100%" cellpadding="0" cellspacing="0" visible="true">
                                                            <tr>
                                                                <td style="width: 25%;">
                                                                </td>
                                                                <td style="width: 50%;">
                                                                    <asp:Panel ID="Panel3" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 100%;" colspan="2" align="center">
                                                                                    <asp:Label ID="lblcompname" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 100%;" colspan="2" align="center">
                                                                                    <center>
                                                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Medium" Text="Business : "></asp:Label><asp:Label
                                                                                            ID="lblstore" runat="server" Font-Size="Medium"></asp:Label></center>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 100%;" colspan="2" align="center">
                                                                                    <center>
                                                                                        <asp:Label ID="Label6" runat="server" Text="Cash Flow As On " Font-Bold="True" Font-Size="Medium"></asp:Label>&nbsp;
                                                                                        <asp:Label ID="lblcasondate" runat="server" Font-Size="Medium"></asp:Label></center>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                    <asp:Label ID="Label5" runat="server" Text="Figures in " Font-Bold="true"></asp:Label><asp:Label
                                                                                        ID="lblcurr" runat="server" Text="Label" Font-Bold="true"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="lblincomesta" Text="Profit before taxation"
                                                                                        Enabled="false" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                    <asp:Label ID="lblincomamt" runat="server" Text="0"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="lvvla" Text="Adjustments for :" Enabled="true" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                            <td colspan="1">
                                                                            <table width="100%">
                                                                            <tr>
                                                                              <td style="width: 80%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="lbltextdepamor" Text="Depreciation and Amortization"
                                                                                        Enabled="true" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 20%;" align="right">
                                                                                    <asp:Label ID="lbldeprmortize" runat="server" Text="0"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                            </td>
                                                                              <td style="width: 25%;" align="left"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                <asp:Panel ID="pnlgain" runat="server" Visible="true">
                                                                                    <asp:DataList ID="datalistrevgain" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                                                        ShowFooter="False" ShowHeader="False" Width="100%">
                                                                                        <ItemTemplate>
                                                                                        <table width="100%"  cellpadding="0" cellspacing="0"></table>
                                                                                        <tr>
                                                                                        <td style="width: 80%;height: 15px;">
                                                                                          <asp:LinkButton ForeColor="Black" ID="lnkacc" Text='<%#Bind("AccountName")%>'
                                                                                        Enabled="false" runat="server"></asp:LinkButton>
                                                                                        </td>
                                                                                        <td style="width: 20%;height: 15px;" align="right">
                                                                                          <asp:Label  ID="lnkaccamt" Text='<%# Bind("Amount","{0:n}") %>'
                                                                                          runat="server"></asp:Label>
                                                                                        </td>
                                                                                        </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                                <td style="width: 25%;" align="left">
                                                                                    <%-- <asp:Label ID="lblincworkingcap" runat="server" Text="0"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                               <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                 <asp:Panel ID="pnlexp" runat="server" Visible="true">
                                                                                    <asp:DataList ID="Datalistexploss" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                                                        ShowFooter="False" ShowHeader="False" Width="100%">
                                                                                      <ItemTemplate>
                                                                                        <table width="100%"  cellpadding="0" cellspacing="0"></table>
                                                                                        <tr>
                                                                                        <td style="width: 80%;height: 15px;">
                                                                                          <asp:LinkButton ForeColor="Black" ID="lnkacc" Text='<%#Bind("AccountName")%>'
                                                                                        Enabled="false" runat="server"></asp:LinkButton>
                                                                                        </td>
                                                                                        <td style="width: 20%;height: 15px;" align="right">
                                                                                          <asp:Label  ID="lnkaccamt" Text='<%# Bind("Amount","{0:n}") %>'
                                                                                          runat="server"></asp:Label>
                                                                                        </td>
                                                                                        </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                                <td style="width: 25%;" align="left">
                                                                                    <%-- <asp:Label ID="lblincworkingcap" runat="server" Text="0"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            
                                                                              <tr>
                                                                            <td colspan="1">
                                                                            <table width="100%">
                                                                            <tr>
                                                                              <td style="width: 80%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="lnkinsrestexp" Text="Interest Expense"
                                                                                        Enabled="true" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 20%;" align="right">
                                                                                    <asp:Label ID="lblintexamt" runat="server" Text="0"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                            </td>
                                                                              <td style="width: 25%;" align="right">
                                                                                 <asp:Label ID="lblincbef" runat="server" Text="0"></asp:Label>
                                                                              </td>
                                                                            </tr>
                                                                          
                                                                          
                                                                            
                                                                            
                                                                              <tr>
                                                                      <td colspan="2">
                                                                     <asp:Panel ID="pnlaccrecivable" runat="server">
                                                                     <table width="100%" cellpadding="0" cellspacing="0">
                                                               
                                                                <tr>
                                                                    <td style="width:75%;" align="left">
                                                                        <asp:LinkButton ForeColor="Black" ID="lblaccreceivable" runat="server" Enabled="false"></asp:LinkButton>
                                                                     <asp:Label ID="lblaccreclst" runat="server" Visible="false"></asp:Label>
                                                                    </td>
                                                                  
                                                                    <td style="width: 75%;" align="right">
                                                                        <asp:Label ID="lblaccrecamt" runat="server"></asp:Label>
                                                                    </td>
                                                                   
                                                                </tr>
                                                                </table>
                                                                </asp:Panel>
                                                                </td>
                                                                </tr>
                                                                   <tr>
                                                                      <td colspan="2">
                                                                  <asp:Panel ID="pnlnotesreceived" runat="server" Visible="false">
                                                                     <table width="100%" cellpadding="0" cellspacing="0">
                                                              
                                                                    <tr>
                                                                        <td style="width:75%;" align="left">
                                                                            <asp:LinkButton ForeColor="Black" ID="lblnotrec" runat="server" Enabled="false"></asp:LinkButton>
                                                                              <asp:Label ID="lblnotreclst" Visible="false" runat="server"></asp:Label>
                                                                        </td>
                                                                        
                                                                        <td style="width: 25%;" align="right">
                                                                            <asp:Label ID="lblnotrecamt" runat="server"></asp:Label>
                                                                        </td>
                                                                     
                                                                    </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                </td>
                                                                </tr>
                                                                  <tr>
                                                                      <td colspan="2">
                                                                  <asp:Panel ID="pnlinv" runat="server" Visible="true">
                                                                     <table width="100%" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="width:75%;" align="left">
                                                                        <asp:LinkButton ForeColor="Black" ID="lblinv" runat="server" Enabled="false"></asp:LinkButton>
                                                                           <asp:Label ID="lblinvlst" runat="server" Visible="false"></asp:Label>
                                                                    </td>
                                                                    
                                                                    <td style="width: 25%;" align="right">
                                                                        <asp:Label ID="lblinvamt" runat="server"></asp:Label>
                                                                    </td>
                                                                  
                                                                </tr>
                                                                </table>
                                                                </asp:Panel>
                                                                </td>
                                                                </tr>
                                                                <tr>
                                                                      <td colspan="2">
                                                                <asp:Panel ID="pnlprepaidexe" runat="server" Visible="true">
                                                                     <table width="100%" cellpadding="0" cellspacing="0">
                                                               
                                                                    <tr>
                                                                        <td style="width:75%;" align="left">
                                                                            <asp:LinkButton ForeColor="Black" ID="lblprepaidexp" runat="server" Enabled="false"></asp:LinkButton>
                                                                            
                                                                            <asp:Label ID="lblprepaidexplst" runat="server" Visible="false"></asp:Label>
                                                                        </td>
                                                                        
                                                                        <td style="width: 25%;" align="right">
                                                                            <asp:Label ID="lblprepaidexpamt"  runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                </td>
                                                                </tr>
                                                                 <tr>
                                                                      <td colspan="2">
                                                                 <asp:Panel ID="pnlothercurrasset" runat="server">
                                                                     <table width="100%" cellpadding="0" cellspacing="0">
                                                            
                                                                    <tr>
                                                                        <td style="width:75%;" align="left">
                                                                            <asp:LinkButton ForeColor="Black" ID="lblorhecurrasset" runat="server"  Enabled="false"></asp:LinkButton>
                                                                             <asp:Label ID="lblorhecurrassetlst" Visible="false" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                        <td style="width: 25%;" align="right">
                                                                            <asp:Label ID="lblorhecurrassetamt" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                    </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                </td>
                                                                </tr>
                                                                
                                                                   <tr>
                                                                      <td colspan="2">
                                                                 <asp:Panel ID="pnlaccpay" runat="server">
                                                                     <table width="100%" cellpadding="0" cellspacing="0">
                                                            
                                                                    <tr>
                                                                        <td style="width:75%;" align="left">
                                                                            <asp:LinkButton ForeColor="Black" ID="lblaccpay" runat="server"  Enabled="false"></asp:LinkButton>
                                                                             <asp:Label ID="lblaccpaylst" Visible="false" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                        <td style="width: 25%;" align="right">
                                                                            <asp:Label ID="lblaccpayamt" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                    </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                </td>
                                                                </tr>
                                                                              
                                                                             <tr>
                                                                      <td colspan="2">
                                                                 <asp:Panel ID="pnlnotespay" runat="server">
                                                                     <table width="100%" cellpadding="0" cellspacing="0">
                                                            
                                                                    <tr>
                                                                        <td style="width:75%;" align="left">
                                                                            <asp:LinkButton ForeColor="Black" ID="lblnotepay" runat="server"  Enabled="false"></asp:LinkButton>
                                                                             <asp:Label ID="lblnotepaylst" Visible="false" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                        <td style="width: 25%;" align="right">
                                                                            <asp:Label ID="lblnotepayamt" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                    </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                </td>
                                                                </tr>
                                                                    <tr>
                                                                      <td colspan="2">
                                                                 <asp:Panel ID="pnlintrestpay" runat="server">
                                                                     <table width="100%" cellpadding="0" cellspacing="0">
                                                            
                                                                    <tr>
                                                                        <td style="width:75%;" align="left">
                                                                            <asp:LinkButton ForeColor="Black" ID="lblintpay" runat="server"  Enabled="false"></asp:LinkButton>
                                                                             <asp:Label ID="lblintpaylst" Visible="false" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                        <td style="width: 25%;" align="right">
                                                                            <asp:Label ID="lblintpayamt" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                    </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                </td>
                                                                </tr>
                                                                    <tr>
                                                                      <td colspan="2">
                                                                 <asp:Panel ID="pnltaxpay" runat="server">
                                                                     <table width="100%" cellpadding="0" cellspacing="0">
                                                            
                                                                    <tr>
                                                                        <td style="width:75%;" align="left">
                                                                            <asp:LinkButton ForeColor="Black" ID="lbltaxpay" runat="server"  Enabled="false"></asp:LinkButton>
                                                                             <asp:Label ID="lbltxtpaylst" Visible="false" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                        <td style="width: 25%;" align="right">
                                                                            <asp:Label ID="lbltaxpayamt" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                    </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                </td>
                                                                </tr>
                                                                    <tr>
                                                                      <td colspan="2">
                                                                 <asp:Panel ID="pnlothercurrentlia" runat="server">
                                                                     <table width="100%" cellpadding="0" cellspacing="0">
                                                            
                                                                    <tr>
                                                                        <td style="width:75%;" align="left">
                                                                            <asp:LinkButton ForeColor="Black" ID="lblothcurr" runat="server"  Enabled="false"></asp:LinkButton>
                                                                             <asp:Label ID="lblothcurrlst" Visible="false" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                        <td style="width: 25%;" align="right">
                                                                            <asp:Label ID="lblothcurramt" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                    </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                </td>
                                                                </tr>
                                                                    <tr>
                                                                      <td colspan="2">
                                                                 <asp:Panel ID="pnlcpartltermdept" runat="server">
                                                                     <table width="100%" cellpadding="0" cellspacing="0">
                                                            
                                                                    <tr>
                                                                        <td style="width:75%;" align="left">
                                                                            <asp:LinkButton ForeColor="Black" ID="lnkcpartlongtext" runat="server"  Enabled="false"></asp:LinkButton>
                                                                             <asp:Label ID="lblcpartlonglist" Visible="false" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                        <td style="width: 25%;" align="right">
                                                                            <asp:Label ID="lblcpartlongamt" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                    </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                </td>
                                                                </tr> 
                                                                
                                                                
                                                                 <tr>
                                                                      <td colspan="2">
                                                                 <asp:Panel ID="pnlspart12m" runat="server">
                                                                     <table width="100%" cellpadding="0" cellspacing="0">
                                                            
                                                                    <tr>
                                                                        <td style="width:75%;" align="left">
                                                                            <asp:LinkButton ForeColor="Black" ID="lnkspart12mtext" runat="server"  Enabled="false"></asp:LinkButton>
                                                                             <asp:Label ID="lblspart12mlist" Visible="false" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                        <td style="width: 25%;" align="right">
                                                                            <asp:Label ID="lblspart12mamt" runat="server"></asp:Label>
                                                                        </td>
                                                                       
                                                                    </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                </td>
                                                                </tr> 
                                                                            <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                    --------------
                                                                                </td>
                                                                            </tr>
                                                                            <tr bgcolor="#C1E0FF">
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="Label9" Font-Bold="True" Font-Size="15px" runat="server" Text="Cash generated from operations"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                    <asp:Label ID="lblcashgeneopration" runat="server" Text="0"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="LinkButton6" Text="Interest paid"
                                                                                        Enabled="false" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                    <asp:Label ID="lblinrestpaidcash" runat="server" Text="0"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="LinkButton7" Text="Income taxes paid"
                                                                                        Enabled="false" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                    <asp:Label ID="lblinctaxpaidcash" runat="server" Text="0"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                           
                                                                            <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                    --------------
                                                                                </td>
                                                                            </tr>
                                                                            <tr bgcolor="#C1E0FF">
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="lblnetcashpoeration" Font-Bold="True" Font-Size="15px" runat="server" Text="Net cash from operating activities"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                    <asp:Label ID="lblnetcashpoerationamt" runat="server" Text="0"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                              <tr >
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="Label8" Font-Bold="True" Font-Size="15px" runat="server" 
                                                                                        Text="Cash Flows From Investing Activities"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="left">
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                             <tr >
                                                                                <td style="width: 100%;" colspan="2">
                                                                                 <asp:Panel ID="pnlproccashpurinvestact" runat="server">
                                                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                                             <tr >
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="Label16" Font-Bold="true"  runat="server" 
                                                                                        Text="Proceeds from the purchase of" Font-Size="14px"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="left" >
                                                                                     
                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:DataList ID="datainvestpur" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                                                        ShowFooter="False" ShowHeader="False" Width="100%">
                                                                                      <ItemTemplate>
                                                                                        <table width="100%"  cellpadding="0" cellspacing="0"></table>
                                                                                        <tr>
                                                                                        <td style="width: 80%;height: 15px;">
                                                                                          <asp:LinkButton ForeColor="Black" ID="lnkacc" Text='<%#Bind("GroupName")%>'
                                                                                        Enabled="false" runat="server"></asp:LinkButton>
                                                                                        </td>
                                                                                        <td style="width: 20%;height: 15px;" align="right">
                                                                                          <asp:Label  ID="lnkaccamt" Text='<%# Bind("Amount","{0:n}") %>'
                                                                                          runat="server"></asp:Label>
                                                                                        </td>
                                                                                        </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right" valign="bottom">
                                                                                <asp:Label ID="lblpurcamt" runat="server" Text="0"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                            </asp:Panel>
                                                                            </td>
                                                                            </tr>
                                                                             <tr >
                                                                                <td style="width: 100%;" colspan="2">
                                                                                 <asp:Panel ID="pnlsaleof" runat="server">
                                                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                                            <tr >
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="Label17" Font-Bold="true"  runat="server" 
                                                                                        Text="Proceeds from the Sale of" Font-Size="14px"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="left">
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                              <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:DataList ID="datasale" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                                                        ShowFooter="False" ShowHeader="False" Width="100%">
                                                                                      <ItemTemplate>
                                                                                        <table width="100%"  cellpadding="0" cellspacing="0"></table>
                                                                                        <tr>
                                                                                        <td style="width: 80%;height: 15px;">
                                                                                          <asp:LinkButton ForeColor="Black" ID="lnkacc" Text='<%#Bind("GroupName")%>'
                                                                                        Enabled="false" runat="server"></asp:LinkButton>
                                                                                        </td>
                                                                                        <td style="width: 20%;height: 15px;" align="right">
                                                                                          <asp:Label  ID="lnkaccamt" Text='<%# Bind("Amount","{0:n}") %>'
                                                                                          runat="server"></asp:Label>
                                                                                        </td>
                                                                                        </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right" valign="bottom">
                                                                                <asp:Label ID="lblsaleamt" runat="server" Text="0"></asp:Label>
                                                                                
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                            </asp:Panel>
                                                                            </td>
                                                                            </tr>
                                                                            <%--  <tr>
                                                                            <td colspan="1">
                                                                            <table width="100%">
                                                                            <tr>
                                                                              <td style="width: 80%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="LinkButton1" Text="Interest Received"
                                                                                        Enabled="true" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 20%;" align="right">
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                            </td>
                                                                              <td style="width: 25%;" align="right">
                                                                                 <asp:Label ID="lblinstreciva" runat="server" Text="0.00"></asp:Label>
                                                                              </td>
                                                                            </tr>
                                                                             <tr>
                                                                            <td colspan="1">
                                                                            <table width="100%">
                                                                            <tr>
                                                                              <td style="width: 80%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="LinkButton2" Text="Dividend Received"
                                                                                        Enabled="true" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 20%;" align="right">
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                            </td>
                                                                              <td style="width: 25%;" align="right">
                                                                                 <asp:Label ID="lbldivirecv" runat="server" Text="0.00"></asp:Label>
                                                                              </td>
                                                                            </tr>--%>
                                                                            <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                   --------------
                                                                                </td>
                                                                            </tr>
                                                                            <tr bgcolor="#C1E0FF">
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="lnlnetinvestcash" Font-Bold="True" Font-Size="15px" runat="server" Text="Net cash from investing activities"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                    <asp:Label ID="lnlnetinvestcashamt" runat="server" Text="0"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                             <tr >
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="Label11" Font-Bold="True" Font-Size="15px" runat="server" 
                                                                                        Text="Cash Flows From Financing Activities"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="left">
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                             <tr >
                                                                                <td style="width: 100%;" colspan="2">
                                                                                 <asp:Panel ID="pnlissue" runat="server">
                                                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                                               
                                                                                 <tr >
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="Label18" Font-Bold="true"  runat="server" 
                                                                                        Text="Proceeds from issue of" Font-Size="14px"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="left" >
                                                                                     
                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:DataList ID="DataIssue" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                                                        ShowFooter="False" ShowHeader="False" Width="100%">
                                                                                      <ItemTemplate>
                                                                                        <table width="100%"  cellpadding="0" cellspacing="0"></table>
                                                                                        <tr>
                                                                                        <td style="width: 80%;height: 15px;">
                                                                                          <asp:LinkButton ForeColor="Black" ID="lnkacc" Text='<%#Bind("GroupName")%>'
                                                                                        Enabled="false" runat="server"></asp:LinkButton>
                                                                                        </td>
                                                                                        <td style="width: 20%;height: 15px;" align="right">
                                                                                          <asp:Label  ID="lnkaccamt" Text='<%# Bind("Amount","{0:n}") %>'
                                                                                          runat="server"></asp:Label>
                                                                                        </td>
                                                                                        </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right" valign="bottom">
                                                                                <asp:Label ID="lblnetissue" runat="server" Text="0"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                              
                                                                                </table>
                                                                                  </asp:Panel>
                                                                             </td>
                                                                             </tr>
                                                                             <tr >
                                                                                <td style="width: 100%;" colspan="2">
                                                                                 <asp:Panel ID="pnlborrow" runat="server">
                                                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                                            <tr >
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="Label20" Font-Bold="true"  runat="server" 
                                                                                        Text="Proceeds from long-term borrowings " Font-Size="14px"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="left">
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                              <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:DataList ID="datalongbro" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                                                        ShowFooter="False" ShowHeader="False" Width="100%">
                                                                                      <ItemTemplate>
                                                                                        <table width="100%"  cellpadding="0" cellspacing="0"></table>
                                                                                        <tr>
                                                                                        <td style="width: 80%;height: 15px;">
                                                                                          <asp:LinkButton ForeColor="Black" ID="lnkacc" Text='<%#Bind("GroupName")%>'
                                                                                        Enabled="false" runat="server"></asp:LinkButton>
                                                                                        </td>
                                                                                        <td style="width: 20%;height: 15px;" align="right">
                                                                                          <asp:Label  ID="lnkaccamt" Text='<%# Bind("Amount","{0:n}") %>'
                                                                                          runat="server"></asp:Label>
                                                                                        </td>
                                                                                        </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right" valign="bottom">
                                                                                <asp:Label ID="lblnetbrowser" runat="server" Text="0"></asp:Label>
                                                                                
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                            </asp:Panel>
                                                                            </td>
                                                                            </tr>
                                                                               <tr>
                                                                            <td colspan="1">
                                                                            <table width="100%">
                                                                            <tr>
                                                                              <td style="width: 80%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="LinkButton3" Text="Dividends paid"
                                                                                        Enabled="true" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 20%;" align="right">
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                            </td>
                                                                              <td style="width: 25%;" align="right">
                                                                                 <asp:Label ID="lbldividpaid" runat="server" Text="0.00"></asp:Label>
                                                                              </td>
                                                                            </tr>
                                                                             <tr >
                                                                                <td style="width: 100%;" colspan="2">
                                                                                 <asp:Panel ID="pnlpaysa" runat="server">
                                                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                                            <tr >
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="Label21" Font-Bold="true"  runat="server" 
                                                                                        Text="Payment for" Font-Size="14px"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="left">
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                              <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:DataList ID="datapayment" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                                                        ShowFooter="False" ShowHeader="False" Width="100%">
                                                                                      <ItemTemplate>
                                                                                        <table width="100%"  cellpadding="0" cellspacing="0"></table>
                                                                                        <tr>
                                                                                        <td style="width: 80%;height: 15px;">
                                                                                          <asp:LinkButton ForeColor="Black" ID="lnkacc" Text='<%#Bind("GroupName")%>'
                                                                                        Enabled="false" runat="server"></asp:LinkButton>
                                                                                        </td>
                                                                                        <td style="width: 20%;height: 15px;" align="right">
                                                                                          <asp:Label  ID="lnkaccamt" Text='<%# Bind("Amount","{0:n}") %>'
                                                                                          runat="server"></asp:Label>
                                                                                        </td>
                                                                                        </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right" valign="bottom">
                                                                                <asp:Label ID="lblnetpayment" runat="server" Text="0"></asp:Label>
                                                                                
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                            </asp:Panel>
                                                                            </td>
                                                                            </tr>
                                                                              <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                     --------------
                                                                                </td>
                                                                            </tr>
                                                                            <tr bgcolor="#C1E0FF">
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="lblnettextfi" Font-Bold="True" Font-Size="15px" runat="server" Text="Net cash from financing activities"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                    <asp:Label ID="lblnetcashfinanact" runat="server" Text="0"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="Label13" Font-Bold="True" Font-Size="15px" runat="server" Text="Net increase in cash and cash equivalents"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                    <asp:Label ID="lblcashequivalents" runat="server" Text="0"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="Label14" Font-Bold="True" Font-Size="15px" runat="server" Text="Cash and cash equivalents at beginning of period"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                    <asp:Label ID="lblopcashbal" runat="server" Text="0"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 75%;" align="left">
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                    =============
                                                                                </td>
                                                                            </tr>
                                                                            <tr bgcolor="#C1E0FF">
                                                                                <td style="width: 75%;" align="left">
                                                                                    <asp:Label ID="Label12" Font-Bold="True" Font-Size="15px" runat="server" Text="Cash and cash equivalents at end of period"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;" align="right">
                                                                                    <asp:Label ID="lblcloscashbank" runat="server" Text="0"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td style="width: 25%;">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <div>
                                                <asp:Panel ID="Panel2" runat="server" Visible="false" ScrollBars="None" Width="100%"
                                                    d>
                                                    <div>
                                                        <table width="100%" cellpadding="0" cellspacing="0" visible="false">
                                                            <tr>
                                                                <td colspan="5" style="width: 100%;" align="center">
                                                                    <center>
                                                                        <asp:Label ID="Label1" runat="server" Text="Income Statement From " Font-Bold="True"
                                                                            Font-Size="Medium"></asp:Label>&nbsp;
                                                                        <asp:Label ID="lblstartdate" runat="server" Font-Size="Medium"></asp:Label>&nbsp;
                                                                        <asp:Label ID="Label2" runat="server" Text=" to" Font-Bold="True" Font-Size="Medium"></asp:Label>&nbsp;
                                                                        <asp:Label ID="lblenddate" runat="server" Font-Size="Medium"></asp:Label></center>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 52%;">
                                                                </td>
                                                                <td style="width: 12%;">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="axs" runat="server" Text="Current Year" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td style="width: 12%;">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="axs1" runat="server" Text="Last Year" Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 52%;" align="left">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="axs2" runat="server" Text="Figures in " Font-Bold="true"></asp:Label><asp:Label
                                                                        ID="lblcurrency" runat="server" Text="Label" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td style="width: 12%;">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="Label15" runat="server" Text="Figures in " Font-Bold="true"></asp:Label><asp:Label
                                                                        ID="lbllcurrency" runat="server" Text="Label" Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="pnlsalesnet" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 52%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="lblsalesnet" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblsalamt" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblsalamtlst" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="pnlcostofgoods" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 52%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="lblcstofgoodsold" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblcstofgoodsoldamt" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblcofglst" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="pnlproductexp" runat="server">
                                                                        <table width="100%">
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 52%;" align="left">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    ____________
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    ____________
                                                                </td>
                                                            </tr>
                                                            <tr bgcolor="#C1E0FF">
                                                                <td style="width: 52%;" align="left">
                                                                    <asp:Label ID="lblgrossprofit" ForeColor="Black" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="lblgrossamt" runat="server" Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="lblgrossamtlst" runat="server" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 52%;" align="left">
                                                                    <asp:LinkButton ForeColor="Black" ID="lblproductionexpenses" runat="server"></asp:LinkButton>
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="lblproductionexpensesamt" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="lblproductionexplst" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="pnlsalesmarketexp" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 52%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="lblsalexp" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblsalexpamt" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblsalexplst" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="pnlgenadminexp" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 52%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="lblgenadmexp" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblgenadmamt" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblgenadmlst" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="pnlotherexp" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 52%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="lblotheropratingexandloss" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblotheropratingexandlossamt" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblotheropratingexandlosslst" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="pnldepr" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 52%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="lbldep" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lbldepamt" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lbldeplst" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="pnlgrossline" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 52%;" align="left">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    ____________
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    ____________
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr bgcolor="#C1E0FF">
                                                                <td style="width: 52%;" align="left">
                                                                    <asp:Label ID="Label24" ForeColor="Black" Text="Total Operating Expenses and Amortization"
                                                                        runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="lbltotexpamt" ForeColor="Black" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="lbltotexplst" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 52%;" align="left">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    ____________
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 13%;" align="right">
                                                                    ____________
                                                                </td>
                                                            </tr>
                                                            <tr bgcolor="#C1E0FF">
                                                                <td style="width: 52%;" align="left">
                                                                    <asp:Label ID="lblopeincome" ForeColor="Black" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="lblopeamt" runat="server" Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="lblopelst" runat="server" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="pnlotherrevandgain" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 52%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="lblotherrevn" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblothrevamt" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblothrevlst" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="pnlotheexpandloses" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 52%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="lblothexpen" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblothexpamt" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblothexplst" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="pnlintexp" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 52%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="lblintrestexp" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblintrestexpamt" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblintrestexplst" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 52%;" align="left">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    ____________
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    ____________
                                                                </td>
                                                            </tr>
                                                            <tr bgcolor="#C1E0FF">
                                                                <td style="width: 52%;" align="left">
                                                                    <asp:Label ID="lblincomebefore" ForeColor="Black" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="lblincbefamt" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="lblincberlst" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="pnlunusealor" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 52%;" align="left">
                                                                                    <asp:LinkButton ForeColor="Black" ID="lblusualinfrequent" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblusualinfrqamt" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                </td>
                                                                                <td style="width: 12%;" align="right">
                                                                                    <asp:Label ID="lblusfrelst" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 52%;" align="left">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    ____________
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    ____________
                                                                </td>
                                                            </tr>
                                                            <tr bgcolor="#C1E0FF">
                                                                <td style="width: 52%;" align="left">
                                                                    <asp:Label ID="lblincomebeforeit" ForeColor="Black" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="lblincbeforitamt" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="lblincbeforitlst" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            
                                                            
                                                            
                                                             <tr>
                                                                      <td colspan="4">
                                                                     <asp:Panel ID="pnlcash" runat="server">
                                                                     <table width="100%">
                                                                      <tr>
                                                                    <td style="width:61%;" align="left">
                                                                        <asp:LinkButton ForeColor="Black" ID="lblcash" runat="server" ></asp:LinkButton>
                                                                    </td>
                                                                    <td style="width: 13%;" align="right">
                                                                    </td>
                                                                    <td style="width: 13%;" align="right">
                                                                        <asp:Label ID="lblcashamt" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 13%;" align="right">
                                                                        <asp:Label ID="lblcashlast" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                     </table>
                                                                     </asp:Panel>
                                                                      </tr>
                                                                
                                                              
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Outset"
                                                Width="300px">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="width: 13%;" align="right">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblm" runat="server" ForeColor="Black">Please check your date.</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label3" runat="server" ForeColor="Black" Text="The start date of the Year is "></asp:Label><asp:Label
                                                                ID="lblstartdate0" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblm0" runat="server" ForeColor="Black">You cannot select 
                                                    any date earlier/later than the start/end of the year date.</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="height: 26px">
                                                            <asp:Button ID="ImageButton2" runat="server" Text="Cancel" CssClass="btnSubmit" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                &nbsp;</asp:Panel>
                                            <asp:Button ID="Button1" runat="server" Style="display: none" />
                                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                                PopupControlID="Panel5" TargetControlID="Button1" CancelControlID="ImageButton2">
                                            </cc1:ModalPopupExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
