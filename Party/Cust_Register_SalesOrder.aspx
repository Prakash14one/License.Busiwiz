<%@ Page Language="C#" MasterPageFile="~/customer/Master/CustomerMaster.master"
    AutoEventWireup="true" CodeFile="Cust_Register_SalesOrder.aspx.cs" Inherits="ShoppingCart_Admin_Register_SalesOrder"
    Title="Register_SalesOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
        function Button3_onclick() {

        }

    </script>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <div style="padding-left: 0%">
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                    </div>
                   
                    <div style="clear: both;">
                    </div>
                    
                        <asp:Panel runat="Server" ID="panel9">
                            <table width="100%">
                                <tr>
                                    <td width="50%" rowspan="3" valign="bottom">
                                        <asp:RadioButtonList ID="rbtnlist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbtnlist_SelectedIndexChanged"
                                                RepeatDirection="Horizontal" Width="100%">
                                                <asp:ListItem Value="1">From Date/To date</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="2">Period</asp:ListItem>
                                                <asp:ListItem Value="3">Month / Year</asp:ListItem>
                                            </asp:RadioButtonList>
                                    </td>
                                    <td width="50%">
                                        <asp:Panel ID="pnlfromdatetodate" runat="server" Visible="False" >
                                            <table width="100%" >
                                                <tr>
                                                    <td style="width: 15%" valign="bottom">
                                                        <label>
                                                            <asp:Label ID="Label13" runat="server" Text="From"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td style="width: 85%" valign="bottom">
                                                        <label>
                                                            <asp:TextBox ID="txtfromdate" Width="75px" runat="server"></asp:TextBox>
                                                        </label>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtfromdate"
                                                            TargetControlID="txtfromdate">
                                                        </cc1:CalendarExtender>
                                                        <label>
                                                            <asp:Label ID="Label14" runat="server" Text="To"></asp:Label>
                                                        </label>
                                                         <label>
                                                            <asp:TextBox ID="txttodate" Width="75px" runat="server"></asp:TextBox>
                                                        </label>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txttodate"
                                                            TargetControlID="txttodate">
                                                        </cc1:CalendarExtender>
                                                        <%-- <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />--%>
                                                        <%-- <asp:ImageButton ID="imgbtncal2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />--%>
                                                    </td>                                                   
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>                                        
                                        <asp:Panel ID="pnlperiod" runat="server" Visible="False">
                                            <table id="Table7" width="100%">
                                                <tr>
                                                    <td style="width: 15%" valign="bottom">
                                                        <label>
                                                        <asp:Label ID="Label17" runat="server" Text="Period"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td style="width: 85%" valign="bottom">
                                                        <label>
                                                        <asp:DropDownList ID="ddlperiod" runat="server">
                                                        </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        
                                        <asp:Panel ID="pnlmonthyear" runat="server" Visible="False">
                                            <table id="Table3" width="100%">
                                                <tr>
                                                    <td style="width: 15%" valign="bottom">
                                                        <label>
                                                        <asp:Label ID="Label18" runat="server" Text="Year"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td style="width: 85%" valign="bottom">
                                                        <label>
                                                        <asp:DropDownList ID="ddlyear" runat="server" Width="100px">
                                                        </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                        <asp:Label ID="Label19" runat="server" Text="Month"></asp:Label>
                                                        </label>
                                                        <label>
                                                        <asp:DropDownList ID="ddlmonth" runat="server" Width="150px">
                                                        </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td width="25%">
                                         <label>
                                            <asp:Label ID="Label2" runat="server" Text="Business Name"></asp:Label>
                                            <asp:RequiredFieldValidator ID="vdxed" runat="server" ControlToValidate="ddlwarehouse"
                                                InitialValue="0" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1">
                                            </asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlwarehouse" Enabled="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td width="25%">
                                        <label>
                                            <asp:Label ID="Label5" runat="server" Text="Sales Person"></asp:Label>
                                             <asp:DropDownList ID="ddlSalesPerson" runat="server">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td width="25%">
                                        <label>
                                            <asp:Label ID="Label7" runat="server" Text="Payment Type"></asp:Label>
                                            <asp:DropDownList ID="ddlPaymentType" runat="server">
                                            </asp:DropDownList>
                                           
                                        </label>
                                        
                                    </td>
                                    <td width="25%">
                                        <label>
                                             <asp:Label ID="Label8" runat="server" Text="Party Type"></asp:Label>
                                            <asp:DropDownList ID="ddlParty" runat="server">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label6" runat="server" Text="Type of Sale"></asp:Label>
                                           <asp:DropDownList ID="ddlTypeOfSale" runat="server">
                                            </asp:DropDownList>
                                        </label>
                                        
                                    </td>
                                    <td>
                                         <label>
                                            <asp:Label ID="Label24" runat="server" Text="Payment Status"></asp:Label>
                                            <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                            <asp:DropDownList ID="ddlpaymentstatus" runat="server">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                          <label>
                                            <asp:Label ID="Label26" runat="server" Text="Delivery Status"></asp:Label>
                                            <asp:Label ID="Label27" runat="server" Text=""></asp:Label>
                                            <asp:DropDownList ID="ddldeliverystatus" runat="server">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Panel ID="pnlColumnSelect" runat="server" HorizontalAlign="Left" Width="100%">
                                <asp:CheckBoxList ID="chkboxSelectGridColm" runat="server" EnableTheming="True" RepeatColumns="7"
                                    Width="100%">
                                    <asp:ListItem Selected="True" Value="0">Date</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="1">Order No.</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="2">Sales Person</asp:ListItem>
                                    <%--<asp:ListItem Selected="True" Value="3">Entry #</asp:ListItem>--%>
                                    <asp:ListItem Selected="True" Value="3">Packing Slip No.</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="4">Invoice #</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="5">Party Name</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="6">Net Amount</asp:ListItem>
                                    <asp:ListItem Selected="False" Value="7">Tax1</asp:ListItem>
                                    <asp:ListItem Selected="False" Value="8">Tax2</asp:ListItem>
                                    <asp:ListItem Selected="False" Value="9">Tax3</asp:ListItem>
                                    <asp:ListItem Selected="False" Value="10">Handling Charge</asp:ListItem>
                                    <asp:ListItem Selected="False" Value="11">Shipping Charge</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="12">Gross Amount</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="13">Delivery Status</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="14">Payment Status</asp:ListItem>
                                </asp:CheckBoxList>
                            </asp:Panel>
                                    </td>                                    
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Button ID="ImageButton3" runat="server" Text="Go" CssClass="btnSubmit" OnClick="Button1_Click"
                                            ValidationGroup="1" />
                                         <asp:Button ID="btnSelectColm" runat="server" OnClick="btnSelectColm_Click" CssClass="btnSubmit"
                                            Text="Select Columns" />
                                    </td>
                                    
                                </tr>
                            </table>
                            <table width="100%">                                                                                                                                                                                            
                                
                                <tr>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:DropDownList ID="ddlInvSubSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubSubCat_SelectedIndexChanged"
                                                Width="150px" Visible="False">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:DropDownList ID="ddlInvCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvCat_SelectedIndexChanged"
                                                Visible="False" Width="150px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:DropDownList ID="ddlInvName" runat="server" Width="150px" Visible="False">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:DropDownList ID="ddlInvSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubCat_SelectedIndexChanged"
                                                Visible="False" Width="150px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtSearchInvName" runat="server" Width="145px" Visible="False" Wrap="False"></asp:TextBox>
                                                        <input id="hdnForInv" runat="Server" name="hdnforInv" style="width: 1px" type="hidden" />
                                                        <input id="hdnforTypeofSale" runat="Server" name="hdnforTypeofSale" style="width: 1px" type="hidden" />
                                                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                    <td style="width: 25%">
                                        
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Panel ID="pnnl" Visible="false" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" align="left">
                                                        <asp:Label ID="dd" ForeColor="Black" Text="Attach The Following Document For Sales Order"
                                                            runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="col2" align="center" colspan="2">
                                                        Doc ID :
                                                        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                                        &nbsp;&nbsp;&nbsp; Title :
                                                        <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                                                        &nbsp;&nbsp;&nbsp; Date
                                                        <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                                                        &nbsp;&nbsp;&nbsp; Cabinet/Drower/Folder :
                                                        <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <asp:Button ID="imgin" runat="server" Visible="false" Text="Submit" OnClick="imgin_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Panel runat="Server" ID="panel2" Width="100%">
                                            <table cellpadding="0" cellspacing="0" id="Table2" width="100%">
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:Label ID="lblHeading" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        Sales Order
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        Order Detail :
                                                    </td>
                                                    <td colspan="2">
                                                        Customer Information :
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Order :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblemail" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Date :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        Billing Address :
                                                    </td>
                                                    <td colspan="2">
                                                        Shipping Address :
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Name :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBName" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        Name :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSName" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Address :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBAddress" runat="server" Font-Bold="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        Address :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSAddress" runat="server" Font-Bold="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Country :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBCountry" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        Country :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSCountry" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        State :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBState" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        State :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSState" runat="server" Font-Bold="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="col1">
                                                        City :
                                                    </td>
                                                    <td class="col2">
                                                        <asp:Label ID="LblBCity" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="col1">
                                                        City :
                                                    </td>
                                                    <td class="col2">
                                                        <asp:Label ID="LblSCity" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="col1">
                                                        Phone :
                                                    </td>
                                                    <td class="col2">
                                                        <asp:Label ID="lblBPhone" runat="server" Font-Bold="False"></asp:Label>
                                                    </td>
                                                    <td class="col1">
                                                        Phone :
                                                    </td>
                                                    <td class="col2">
                                                        <asp:Label ID="lblSPhone" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="col1">
                                                        Zip :
                                                    </td>
                                                    <td class="col2">
                                                        <asp:Label ID="lblBZip" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="col1">
                                                        Zip :
                                                    </td>
                                                    <td class="col2">
                                                        <asp:Label ID="lblSZip" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="col1">
                                                    </td>
                                                    <td class="col2">
                                                    </td>
                                                    <td class="col1">
                                                    </td>
                                                    <td class="col2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="subinnertblfc" colspan="4">
                                                        Payment Detail :
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="col1">
                                                    </td>
                                                    <td class="col2">
                                                    </td>
                                                    <td class="col1">
                                                    </td>
                                                    <td class="col2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="col1">
                                                        Payment By :
                                                    </td>
                                                    <td class="col2">
                                                        <asp:Label ID="lblPayBy" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="col1">
                                                    </td>
                                                    <td class="col2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="col1">
                                                        Payment Status :
                                                    </td>
                                                    <td class="col2">
                                                        <asp:Label ID="lblPayStatus" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="col1">
                                                    </td>
                                                    <td class="col2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="col1">
                                                    </td>
                                                    <td class="col2">
                                                    </td>
                                                    <td class="col1">
                                                    </td>
                                                    <td class="col2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="subinnertblfc" colspan="4">
                                                        Item Details :
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:BoundField DataField="Name" HeaderText="Item"></asp:BoundField>
                                                                <asp:BoundField DataField="Qty" HeaderText="Qty"></asp:BoundField>
                                                                <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                                                <asp:BoundField DataField="PromotionDiscountAmount" HeaderText="Promotion Discount">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="InventoryVolumeDiscountAmount" HeaderText="Volume Discount">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PackHandlingAmount" HeaderText="Handling Charges"></asp:BoundField>
                                                                <asp:BoundField DataField="Amount" HeaderText="Total"></asp:BoundField>
                                                                <asp:BoundField DataField="WarehouseName" HeaderText="Warehouse" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="subinnertblfc" colspan="4">
                                                        Totals
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="col1">
                                                    </td>
                                                    <td colspan="2" class="col2">
                                                        &nbsp;
                                                    </td>
                                                    <td class="col2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="col1">
                                                        Sub Total :
                                                    </td>
                                                    <td class="col2">
                                                        <asp:Label ID="lblSubTotal" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="col1">
                                                        Discount :
                                                    </td>
                                                    <td class="col2">
                                                        <asp:Label ID="lblCDisc" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="col1">
                                                        Handling Charges :
                                                    </td>
                                                    <td class="col2">
                                                        <asp:Label ID="lblOVHCharge" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="col1">
                                                        Shipping Charges :
                                                    </td>
                                                    <td class="col2">
                                                        <asp:Label ID="lblShipCharge" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="col1">
                                                        Tax :
                                                    </td>
                                                    <td class="col2">
                                                        <asp:Label ID="lblTax" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="col1">
                                                        Totals :
                                                    </td>
                                                    <td class="col2">
                                                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="4">
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/ShoppingCart/images/back.png"
                                                            OnClick="ImageButton4_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Panel ID="Panel5" runat="server" BackColor="White" BorderColor="#C0C0FF" BorderStyle="Outset"
                                            Height="200px" Width="500px">
                                            <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td class="subinnertblfc" colspan="2" width="90%">
                                                            Add Payment Information
                                                        </td>
                                                        <td class="subinnertblfc" width="5%">
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                                OnClick="ImageButton3_Click" Width="16px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col1">
                                                            Sales Order #
                                                        </td>
                                                        <td class="col2">
                                                            <asp:Label ID="lblSalesOrderNoFromGrid" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col1">
                                                            Payment Type :
                                                        </td>
                                                        <td class="col2">
                                                            <asp:DropDownList ID="ddlPaymentTypeForGrid" runat="server" Width="105px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPaymentTypeForGrid"
                                                                ErrorMessage="*" InitialValue="0" ValidationGroup="hh1"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col1">
                                                            Payment Amount :
                                                        </td>
                                                        <td class="col2">
                                                            <asp:TextBox ID="txtPayAmtForGrid" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPayAmtForGrid"
                                                                ErrorMessage="*" ValidationGroup="hh1"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col1">
                                                            Order Status :
                                                        </td>
                                                        <td class="col2">
                                                            <asp:DropDownList ID="ddlOrderStatusForGrid" runat="server" Width="105px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOrderStatusForGrid"
                                                                ErrorMessage="*" InitialValue="0" ValidationGroup="hh1"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col1">
                                                        </td>
                                                        <td class="col2">
                                                            <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" Text="Send Email To Party" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col1">
                                                            <asp:ImageButton ID="ImgBtnSubmit" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/submit.png"
                                                                OnClick="ImgBtnSubmit_Click" ValidationGroup="hh1" />
                                                        </td>
                                                        <td class="col2">
                                                            <asp:ImageButton ID="ImgBtnCancel" runat="server" AlternateText="Cancel" ImageUrl="~/ShoppingCart/images/cancel.png"
                                                                OnClick="ImgBtnCancel_Click" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            &nbsp;
                                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                                PopupControlID="Panel5" TargetControlID="HiddenButton">
                                            </cc1:ModalPopupExtender>
                                            <asp:Button ID="HiddenButton" runat="server" OnClick="HiddenButton_Click" Style="display: none" /></asp:Panel>
                                        <asp:Panel ID="Panel6" runat="server" BackColor="White" BorderColor="#C0C0FF" BorderStyle="Outset"
                                            Height="300px" Width="500px">
                                            <table id="Table5" cellpadding="0" cellspacing="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td class="subinnertblfc" colspan="2" width="90%">
                                                            Add Notes
                                                        </td>
                                                        <td class="subinnertblfc" width="90%">
                                                            &nbsp;
                                                        </td>
                                                        <td class="subinnertblfc" width="90%">
                                                            &nbsp;
                                                        </td>
                                                        <td class="subinnertblfc" width="5%">
                                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                                OnClick="ImageButton3_Click" Width="16px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col1">
                                                            Sales Order #
                                                        </td>
                                                        <td class="col2">
                                                            <asp:Label ID="lblSalesOrderNoFromGrid0" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="col2">
                                                            &nbsp;
                                                        </td>
                                                        <td class="col2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col1" colspan="2">
                                                        </td>
                                                        <td class="col1">
                                                            &nbsp;
                                                        </td>
                                                        <td class="col1">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col1">
                                                            Form Type :
                                                        </td>
                                                        <td class="col2">
                                                            <asp:DropDownList ID="ddlFormTypeForAddNote" runat="server" OnSelectedIndexChanged="ddlFormTypeForAddNote_SelectedIndexChanged"
                                                                Width="130px" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="col3">
                                                            Form #
                                                        </td>
                                                        <td class="col2">
                                                            <asp:Label ID="lblFormNoForAddNotes" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col1">
                                                            Form Status :
                                                        </td>
                                                        <td class="col2">
                                                            <asp:DropDownList ID="ddlStatusMasterForAddNotes" runat="server" Width="130px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="col2">
                                                            &nbsp;
                                                        </td>
                                                        <td class="col2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col1" colspan="4">
                                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CssClass="GridBack">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="FormType">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFormType" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Form#">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFormNo" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStatusMaster" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                               
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col1">
                                                            <asp:ImageButton ID="ImageButton5" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/submit.png"
                                                                OnClick="ImgBtnSu6AddNote_Click" ValidationGroup="hh1" />
                                                        </td>
                                                        <td class="col2">
                                                            <asp:ImageButton ID="ImageButton6" runat="server" AlternateText="Cancel" ImageUrl="~/ShoppingCart/images/cancel.png"
                                                                OnClick="ImageButton6_Click" />
                                                        </td>
                                                        <td class="col2">
                                                            &nbsp;
                                                        </td>
                                                        <td class="col2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            &nbsp;
                                            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                                PopupControlID="Panel6" TargetControlID="Button12">
                                                <%--BackgroundCssClass="modalBackground"--%>
                                            </cc1:ModalPopupExtender>
                                            <asp:Button ID="Button12" runat="server" OnClick="HiddenButton1_Click" Style="display: none" />
                                        </asp:Panel>
                                        <asp:Panel ID="Panel7" runat="server" CssClass="modalPopup" Width="300px">
                                            <fieldset style="border: 1px solid white;">
                                                <legend style="color: Black">Confirm Delete </legend>
                                                <div style="background-color: White;">
                                                    <div style="float: right;">
                                                        <label>
                                                            <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                                Width="16px" OnClick="ImageButton12_Click" />
                                                        </label>
                                                    </div>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <div>
                                                        <table id="Table6" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    Confirm Delete
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label25" runat="server" ForeColor="Black">You Sure , You Want to 
                                    Delete !
                                                                    </asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Button ID="ImageButton7" runat="server" Text="Yes" OnClick="yes_Click" />
                                                                    <asp:Button ID="ImageButton8" runat="server" Text="Cancel" OnClick="Button661_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </fieldset></asp:Panel>
                                        <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                                            PopupControlID="Panel7" TargetControlID="HiddenButton222">
                                        </cc1:ModalPopupExtender>
                                    </td>
                                </tr>
                               
                            </table>
                        </asp:Panel>
                   
                    <div style="clear: both;">
                    </div>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Sales Orders" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click1" />
                            <input id="btnPrint" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" visible="false" />
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <%--<asp:Panel ID="Panel3" runat="server" Height="1000px" ScrollBars="None" Width="100%">
                        <center>
                            <asp:Label ID="" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Black"></asp:Label>
                            </center>
                        <center>
                            <asp:Label ID="" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Black"></asp:Label></center>
                        <center>
                            <asp:Label ID="" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Black"
                                Text="Sales Register Report"></asp:Label></center>
                       
                    </asp:Panel>--%>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                 <asp:Label ID="lblBusnesshea" runat="server" Text="Business : " Font-Italic="True"></asp:Label>
                                              
                                                    <asp:Label ID="lblcompname" runat="server" Font-Italic="True"> </asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="lblstore" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="lvlrr" runat="server" Font-Italic="true" Text="List of Sales Orders "></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblclaat" runat="server" Font-Bold="True" Font-Size="13px" Text=" Type Of Sales :"></asp:Label>
                                                    <asp:Label ID="lbltypeofsalesprint" runat="server" Font-Bold="True" Font-Size="13px"></asp:Label>
                                                    ,
                                                    <asp:Label ID="lblcma" runat="server" Font-Bold="True" Font-Size="13px" Text="Sales Person :">
                                                    </asp:Label>
                                                    <asp:Label ID="lblsalesperson" runat="server" Font-Bold="True" Font-Size="13px"></asp:Label>
                                                    ,
                                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="13px" Text="Payment Type :"></asp:Label>
                                                    <asp:Label ID="lblpaymenttype" runat="server" Font-Bold="True" Font-Size="13px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="13px" Text="Party :"></asp:Label>
                                                    <asp:Label ID="lblpartyprint" runat="server" Font-Bold="True" Font-Size="13px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="SalesOrderId"
                                        OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        ShowFooter="True" OnRowDataBound="GridView1_RowDataBound" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" CssClass="mGrid" GridLines="Both" FooterStyle-CssClass="ftr"
                                        OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting" EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" HeaderStyle-Width="30px"  Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%-- <a href='SalesOrderDetailThankyou.aspx?id= <%# Eval("SalesOrderId")%>'>--%>
                                                    <a href='SalesOrderDetailThankyou.aspx?id=<%# Eval("SalesOrderId")%>&amp;id2=<%#Eval("EntryNumber") %>'
                                                        target="_blank">
                                                        <asp:Label ID="lblentrytyp2e" runat="server" ForeColor="Black" Text='<%#Bind("SalesOrderId") %>'></asp:Label>
                                                        </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:ButtonField CommandName="SalesOrderId"  Text="SalesOrderId" HeaderText="Order No">
                                            <HeaderStyle CssClass="theHeader" />
                                            <ItemStyle CssClass="theHeader" />
                                           
                                        </asp:ButtonField>--%>
                                            <%--<asp:BoundField DataField="SalesOrderId" HeaderText="Order No"></asp:BoundField>--%>
                                            <asp:BoundField DataField="SalesOrderDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                            <%-- <asp:TemplateField HeaderText="EntryType">
                                            <ItemTemplate>
                                                <asp:Label ID="lblentrytype" runat="server" Text='<%#Bind("Entry_Type_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Sales Person" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalesman" runat="server" Text='<%#Bind("AccountName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales Invoice#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href='SalesInvoiceShow.aspx?id=<%# Eval("SalesOrderId")%>&id2=<%#Eval("EntryNumber") %>&id3=<%#Eval("SalesChallanMasterId") %>'
                                                        target="_blank">
                                                        
                                                          <asp:Label ID="lblSalesOrder" Visible="false" runat="server" Text='<%#Bind("SalesOrderId") %>'></asp:Label>
                                                    <asp:Label ID="lblSalesChallan" Visible="false" runat="server" Text='<%#Bind("SalesChallanMasterId") %>'></asp:Label>
                                                    <asp:Label ID="lblEntryTypeId" Visible="false" runat="server" Text='<%#Bind("EntryTypeId") %>'></asp:Label>
                                                    <asp:Label ID="lblentryno" runat="server" ForeColor="Black" Text='<%#Bind("EntryNumber") %>'>
                                                        </asp:Label>
                                                         
                                                    </a>
                                                   
                                                        
                                                    <%-- <a id="sales" runat="server" target="_blank">
                                                        <asp:Label ID="lblentryno" runat="server" Text='<%#Bind("EntryNumber") %>' ForeColor="Black"></asp:Label>
                                                    </a>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Packing Slip#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a id="ptik" href='SalesRelatedReport2.aspx?id=<%# Eval("SalesOrderId")%>&id2=<%#Eval("SalesChallanMasterId") %>'
                                                        target="_blank">
                                                        <asp:Label ID="lblPackingSlipNo" runat="server" Text='<%#Bind("SalesChallanMasterId") %>' ForeColor="Black"></asp:Label>
                                                    </a>
                                                     <%--<a runat="server" id="ptik" target="_blank">
                                                        <asp:Label ID="lblPackingSlipNo" runat="server" Text='<%#Bind("SalesChallanMasterId") %>' ForeColor="Black"></asp:Label>
                                                    </a>--%>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Invoice#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoiceNo" runat="server" Text='<%#Bind("InvoiceNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Party Name" SortExpression="PartyName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdPartyName" runat="server" Text='<%# Bind("PartyName") %>'></asp:Label>
                                                    <asp:Label ID="lblgrdPartyId" runat="server" Text='<%# Bind("PartyID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnetamont" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblnetamontfooter" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tax1" ItemStyle-Width="30px" HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltax1" runat="server" Text='<%# Bind("Tax1Amt") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltax1footer" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tax2" ItemStyle-Width="30px" HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltaxx2" runat="server" Text='<%# Bind("Tax2Amt") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltaxx2footer" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tax3" ItemStyle-Width="30px" HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltax3" runat="server" Text='<%# Bind("Tax3Amt") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltax3footer" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Handling Charges" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltax2" runat="server" Text='<%# Bind("HandlingCharges") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltax2footer" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shiping Charges" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblshipcharge" runat="server" Text='<%# Bind("ShippingCost") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblshipchargefooter" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gross Amount" SortExpression="GrossAmount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdgrossAmt" runat="server" Text='<%# Bind("GrossAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgrdgrossAmtfooter" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="Payment Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                 <%--  <asp:Label ID="lblattach" runat="server" Visible="false" Text='<%# Bind("RelatedTableId") %>'></asp:Label>
                                            --%>
                                                    <asp:Label ID="lblpaysta" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Delivary Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesatat" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:ButtonField CommandName="detail" Text="Detail">
                                            <HeaderStyle CssClass="theHeader" />
                                            <ItemStyle CssClass="theHeader" />
                                           
                                        </asp:ButtonField>--%>
                                            <%--<asp:ButtonField Text="Add Pay Info" ItemStyle-Width="100px" CommandName="AddPayInfo" />--%>
                                            <%-- <asp:ButtonField Text="Add Note" CommandName="AddNote" />--%>
                                            <asp:TemplateField HeaderText="Docs" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>  
                                                <asp:Label ID="lbldocno" runat="server"></asp:Label>
                                                    <asp:ImageButton ID="img1" runat="server" ImageUrl="~/ShoppingCart/images/Docimg.png"
                                                        AlternateText="" Height="22px" Width="22px" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Tranction_Master_Id")%>'
                                                        CommandName="AddDoc"></asp:ImageButton>
                                                      
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField CommandName="Delete" ButtonType="Image" ImageUrl="~/Account/images/delete.gif" Visible="false"
                                                HeaderImageUrl="~/ShoppingCart/images/trash.jpg" Text="Delete" HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="30px">
                                                </asp:ButtonField>
                                            <asp:TemplateField HeaderText="Add Doc" Visible="false">
                                                <ItemTemplate>
                                                    <a href="AccEntryDocUp.aspx?Tid=<%#DataBinder.Eval(Container.DataItem, "Tranction_Master_Id")%>"
                                                        target="_blank">
                                                        <asp:Label ID="lbladdd" runat="server" ForeColor="Black" Text="Add Doc"></asp:Label>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel8" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Outset"
                        Width="300px">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblm" runat="server" ForeColor="Black">Please check the date.</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 26px">
                                    <asp:Label ID="Label3" runat="server" ForeColor="Black" Text="Start Date of the Year is "></asp:Label>
                                    <asp:Label ID="lblstartdate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 26px">
                                    <asp:Label ID="lblm0" runat="server" ForeColor="Black">You can not select 
                                                            anydate earlier than that. </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 26px">
                                    <asp:ImageButton ID="ImageButton9" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/cancel.png"
                                        OnClick="ImageButton2_Click" />
                                </td>
                            </tr>
                        </table>
                       </asp:Panel>
                    <asp:Button ID="Button1" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel8" TargetControlID="Button1">
                    </cc1:ModalPopupExtender>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Paneldoc" runat="server" Height="270px" Width="565px">
                        <div style="float: right;">
                             <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                            OnClick="ImageButtondsfdsfdsf123_Click" Width="16px" />
                        </div>
                        <div style="clear: both">
                        </div>
                         <div style="background-color:White">
                          <asp:Panel ID="pnlof" runat="server" CssClass="modalPopup"  ScrollBars="Auto">  
                        <fieldset style="border: 1px solid white;">
                            <legend style="color: Black">
                               <asp:Label ID="Label20" runat="server" Text="List Of Document"></asp:Label> 
                             </legend>
                             <label>
                                <asp:Label ID="Label21" runat="server" Text="List of documents attched to Entry Type: "></asp:Label> 
                                <asp:Label ID="lbldocentrytype" runat="server" Font-Bold="True" ForeColor="#336699"></asp:Label>
                                <asp:Label ID="Label22" runat="server" Text="Entry No: "></asp:Label> 
                                <asp:Label ID="lbldocentryno" runat="server" Font-Bold="True" ForeColor="#336699"></asp:Label>
                             </label>
                              <asp:Panel ID="pvgris" runat="server" Height="100%" ScrollBars="Both" Width="100%">
                                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                    Width="100%" OnRowCommand="grd_RowCommand" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" CssClass="mGrid" HeaderStyle-HorizontalAlign="Left">
                                    <Columns>
                                        <asp:BoundField DataField="Datetime" HeaderText="Date" />
                                        <asp:BoundField DataField="IfilecabinetDocId" HeaderText="Id" />
                                        <asp:BoundField DataField="Titlename" HeaderText="Title" />
                                        <asp:BoundField DataField="Filename" HeaderText="Cabinet-Drawer-Folder" />
                                        <asp:TemplateField HeaderText="View Doc">
                                            <ItemTemplate>
                                                <a onclick="window.open('viewdocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "IfilecabinetDocId")%>', 'welcome','width=1200,height=700,menubar=no,status=no')"
                                                    href="javascript:void(0)" target="_blank">&nbsp;View Doc</a></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                             <div style="clear: both">
                            </div>
                            <asp:Button ID="ImageButton11" runat="server" Text="Back" OnClick="ImageButton4123_Click" />
                           
                        </fieldset></asp:Panel>
                        </div>
                        </asp:Panel>
                    <asp:Button ID="Button4" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Paneldoc" TargetControlID="Button4">
                    </cc1:ModalPopupExtender>
                    <div style="clear: both;">
                    </div>
                     <asp:Panel ID="pnlselectcolm" runat="server" Height="270px" Width="565px">
                         <div style="background-color:White">
                          <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup"  ScrollBars="Auto">  
                        <fieldset style="border: 1px solid white;">
                            <legend>
                                <asp:Label ID="Label23" runat="server" Text="Select Columns "></asp:Label> 
                            </legend>
                             
                             <div style="clear: both">
                            </div>
                            <asp:Button ID="Button3" runat="server" Text="Submit" CssClass="btnSubmit"  />
                        </fieldset></asp:Panel>
                        
                        </div>
                     </asp:Panel>
                      <asp:Button ID="Button5" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="pnlselectcolm" TargetControlID="Button5" CancelControlID="Button3">
                    </cc1:ModalPopupExtender>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
