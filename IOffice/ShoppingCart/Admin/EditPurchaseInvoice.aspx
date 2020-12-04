<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="EditPurchaseInvoice.aspx.cs" Inherits="ShoppingCart_Admin_EditPurchaseInvoice"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        function Test() {
            alert("In");
        }
        function MadeAddition(myfield, e) {
            var qty = document.getElementById("ctl00$ContentPlaceHolder1$txtqty").value;
            var sprice = document.getElementById("ctl00$ContentPlaceHolder1$txtsalesprice").value;
            var TotalC;
            TotalC = qty * sprice;
            document.getElementById("ctl00_ContentPlaceHolder1_lblAmount").innerHTML = TotalC;
        }
        function MadeDeduction(myfield, e) {
            //alert("IN") ;
            var Tax1 = document.getElementById("ctl00$ContentPlaceHolder1$txttax1").value;
            var Tax2 = document.getElementById("ctl00$ContentPlaceHolder1$txttax2").value;
            var Tax3 = document.getElementById("ctl00$ContentPlaceHolder1$txttax3").value;
            var Discount = document.getElementById("ctl00_ContentPlaceHolder1_txtdiscount").value;
            var ShippingCharge = document.getElementById("ctl00$ContentPlaceHolder1$txtshippingcharges").value;
            var HandlingCharge = document.getElementById("ctl00$ContentPlaceHolder1$txthandlingcharges").value;
            var OtherCharge = document.getElementById("ctl00$ContentPlaceHolder1$txtothercharges").value;
            var SubTotal = document.getElementById("ctl00_ContentPlaceHolder1_lblSubTotal").innerHTML;
            //ctl00_ContentPlaceHolder1_lblGrossAmount 
            //alert(qty) ;
            //alert(sprice) ;
            var TotalC;
            // alert("Total 1 " + TotalC); 
            var ttt = (Tax1) + (Tax2) + (Tax3) + (Discount) + (ShippingCharge) + (HandlingCharge) + (OtherCharge);
            alert(ttt);
            TotalC = SubTotal - ttt;
            alert("Total " + TotalC);
            //document.getElementById("ctl00_ContentPlaceHolder1_lblAmount").value  = TotalC   ;
            document.getElementById("ctl00_ContentPlaceHolder1_lblGrossAmount").innerHTML = TotalC;
            alert("out");
            //TotalAmt.value = TotalC ;
            //alert(TotalC); 

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
            <div style="padding-left: 1%">
                <asp:Label ID="lblmmssgg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                <asp:Label ID="Label8" runat="server" ForeColor="Red"></asp:Label>
                <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <%--   <asp:Panel ID="pnnl" Visible="false" runat="server">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="Label16" runat="server" Text="Doc ID "></asp:Label>
                                    <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Text="Title"></asp:Label>
                                    <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label18" runat="server" Text="Date"></asp:Label>
                                    <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label19" runat="server" Text="Cabinet/Drawer/Folder"></asp:Label>
                                    <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>--%>
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label45" runat="server" Text="Create Purchase Invoice Entry"></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel3" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="3" align="right" style="height: 34px">
                                                    <asp:Button ID="btnnewinv" runat="server" Text="Create New Invoice" CssClass="btnSubmit"
                                                        OnClick="btnnewinv_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label38" runat="server" Text="Business Location"></asp:Label>
                                                    </label>
                                                </td>
                                                <td colspan="2">
                                                    <label>
                                                        <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged"
                                                            Style="margin-left: 0px" Enabled="False">
                                                        </asp:DropDownList>
                                                        <input id="hdnWHid" runat="server" style="width: 2px" type="hidden" />
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label39" runat="server" Text=" Date"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="txtDate" runat="server" Width="75px"></asp:TextBox>
                                                    </label>
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calender.jpg" />
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton1"
                                                            TargetControlID="txtDate">
                                                        </cc1:CalendarExtender>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label41" runat="server" Text="Entry No."></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="lblEntryNo" runat="server"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label40" runat="server" Text="Purchased"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                        <asp:ListItem Selected="True" Value="1">On Credit</asp:ListItem>
                                                        <asp:ListItem Value="2">With Cash</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:Panel ID="pnlcash" runat="server">
                                                        <label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcashtype"
                                                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="11"></asp:RequiredFieldValidator>
                                                            <asp:DropDownList ID="ddlcashtype" runat="server" AutoPostBack="True" Style="margin-left: 0px"
                                                                Width="151px">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </asp:Panel>
                                                </td>
                                                <td align="left">
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label1" runat="server" Text="Payment Due Date"> </asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtpayduedate"
                                                                        ErrorMessage="*" ValidationGroup="11"> </asp:RequiredFieldValidator>
                                                    </label>
                                                </td>
                                                <td>
                                                   <label>
                                                                    <asp:TextBox ID="txtpayduedate" runat="server" Width="100px"></asp:TextBox>
                                                                </label>
                                                                <label>
                                                                     <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/calender.jpg" />
                                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="ImageButton4"
                                                        TargetControlID="txtpayduedate">
                                                    </cc1:CalendarExtender>
                                                                </label>
                                                </td>
                                                <td align="left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <label>
                                                                    <asp:Label ID="lblpartname" Text="Vendor Name" runat="server"></asp:Label>
                                                                    <asp:Label ID="Label63" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlpartyName"
                                                                        Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="true" ValidationGroup="11"></asp:RequiredFieldValidator>
                                                                </label>
                                                            </td>
                                                            <td align="left" valign="top">
                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                    <ContentTemplate>
                                                                        <label>
                                                                            <asp:DropDownList ID="ddlpartyName" runat="server" Width="151px">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                        <label>
                                                                            <asp:ImageButton ID="imgadddivision" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                                OnClick="imgadddivision_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                                                        </label>
                                                                        <label>
                                                                            <asp:ImageButton ID="LinkButton3" runat="server" AlternateText="Refresh" Height="20px"
                                                                                ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton3_Click"
                                                                                ToolTip="Refresh" Width="20px" ImageAlign="Bottom" />
                                                                        </label>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="imgadddivision" EventName="Click" />
                                                                        <asp:AsyncPostBackTrigger ControlID="LinkButton3" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td align="left" valign="top">
                                                                <label>
                                                                    <asp:Label ID="Label42" runat="server" Text="Invoice No."></asp:Label>
                                                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([().a-zA-Z0-9_\s]*)"
                                                                        ControlToValidate="txtInvNo" ValidationGroup="11"></asp:RegularExpressionValidator>
                                                                </label>
                                                            </td>
                                                            <td align="left" valign="top">
                                                                <label>
                                                                    <asp:TextBox ID="txtInvNo" MaxLength="15" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_().\s]+$/,'div1',15)"
                                                                        runat="server" Text="0" Width="100px"></asp:TextBox>
                                                                    <asp:Label ID="Label29" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                    <span id="div1" class="labelcount">15</span>
                                                                    <asp:Label ID="lblkjsadfgh" runat="server" Text="(A-Z 0-9 _ . ())" CssClass="labelcount"></asp:Label>
                                                                </label>
                                                                <%-- <label>
                                                       
                                                    </label>--%>
                                                            </td>
                                                            <td valign="top">
                                                                <label>
                                                                    <asp:Label ID="Label44" runat="server" Text="Tracking No."></asp:Label>
                                                                    <%--  <asp:Label ID="Label46" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                                                        ControlToValidate="txtTrackingNo" ValidationGroup="11"></asp:RegularExpressionValidator>
                                                                    <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtTrackingNo"
                                                            ErrorMessage="*" SetFocusOnError="true" ValidationGroup="11"></asp:RequiredFieldValidator>
                                          --%>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="txtTrackingNo" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'div2',15)"
                                                                        runat="server" Text="0" MaxLength="15" Width="100px"></asp:TextBox>
                                                                    <asp:Label ID="Label37" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                    <span id="div2" class="labelcount">15</span>
                                                                    <asp:Label ID="Label47" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td valign="top">
                                                                <label>
                                                                    <asp:Label ID="Label43" runat="server" Text="Transporter Name"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <label>
                                                                            <asp:DropDownList ID="ddlTransporter" runat="server" Width="151px">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                        <label>
                                                                            <asp:ImageButton ID="ImageButton2" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                                OnClick="imgadddivision_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                                                        </label>
                                                                        <label>
                                                                            <asp:ImageButton ID="ImageButton3" runat="server" AlternateText="Refresh" Height="20px"
                                                                                ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                                                                ImageAlign="Bottom" OnClick="ImageButton3_Click" />
                                                                        </label>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="ImageButton2" EventName="Click" />
                                                                        <asp:AsyncPostBackTrigger ControlID="ImageButton3" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--    <asp:CheckBox ID="chk" runat="server" />--%>
                                    <label>
                                        <asp:Label ID="lbld" runat="server" Text="Do you wish to seperately account for taxes paid on purchases?"></asp:Label></label>
                                    <label>
                                        <asp:LinkButton ID="lnkmore" runat="server" Text="More Info" OnClick="lnkmore_Click"></asp:LinkButton></label>
                                    <asp:RadioButtonList ID="rdl1" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                        OnSelectedIndexChanged="rdl1_SelectedIndexChanged" Enabled="False">
                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlitem" runat="server">
                                        <label>
                                            <asp:Label ID="vb" runat="server" Text="Are items taxed at different rates?"></asp:Label></label><asp:RadioButtonList
                                                ID="CheckBox2" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                OnSelectedIndexChanged="CheckBox2_SelectedIndexChanged" 
                                            Enabled="False">
                                                <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                            </asp:RadioButtonList>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlmain" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="Label59" Text="Select Items Purchased" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged"
                                                        RepeatDirection="Horizontal" Width="80%">
                                                        <asp:ListItem Value="0">Select by category</asp:ListItem>
                                                        <asp:ListItem Value="1">Select by name</asp:ListItem>
                                                        <asp:ListItem Value="2">Select by barcode</asp:ListItem>
                                                        <asp:ListItem Value="3">Select by product no.</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <a href="javascript:void(0)" onclick="window.open('InventoryMaster.aspx')" style="color: blue"
                                                        target="_blank">
                                                        <%--<span style="text-decoration: underline">Add New</span>--%></a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="PnlCategory" runat="server" Visible="false">
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="25%">
                                                                    <label>
                                                                        <asp:Label ID="Label5" runat="server" Text="Category "></asp:Label></label>
                                                                </td>
                                                                <td width="75%">
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlCategory" runat="server" Width="450px">
                                                                        </asp:DropDownList>
                                                                        <input id="hdnCat" runat="server" name="hdnCat" style="width: 1px" type="hidden" />
                                                                        <asp:Panel ID="pnlInv" runat="server">
                                                                            <a href="javascript:void(0)" onclick="window.open('updateInventoryMasterPopUp.aspx?InventoryWarehouseMasterId=<%=InventoryWarehouseMasterId%>')"
                                                                                style="color: blue" target="_blank">
                                                                                <%--<span style="font-size: 8pt; color: #0000ff">Update
                                                    Item </span>--%></a></asp:Panel>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="25%">
                                                                    <label>
                                                                        <asp:Label ID="Label6" runat="server" Text="Inventory Name "></asp:Label></label>
                                                                </td>
                                                                <td width="75%">
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlItemname" runat="server" Width="346px">
                                                                        </asp:DropDownList>
                                                                        <input id="hdnNm" runat="server" name="hdnNm" style="width: 1px" type="hidden" />
                                                                        <asp:Panel ID="pnlName" runat="server">
                                                                            <a href="javascript:void(0)" onclick="window.open('updateInventoryMasterPopUp.aspx?InventoryWarehouseMasterId=<%=InventoryWarehouseMasterIdN%>')"
                                                                                style="color: blue" target="_blank">
                                                                                <%--<span style="font-size: 8pt; color: #0000ff">Update
                                                    Item </span>--%></a></asp:Panel>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="PnlMainBarcode" runat="server">
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td width="25%">
                                                                    <label>
                                                                        <asp:Label ID="Label7" runat="server" Text="Barcode"></asp:Label><asp:RegularExpressionValidator
                                                                            ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                                                            ControlToValidate="txtBar" ValidationGroup="11"></asp:RegularExpressionValidator></label>
                                                                </td>
                                                                <td width="75%">
                                                                    <label>
                                                                        <asp:TextBox ID="txtBar" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'Span12',20)"
                                                                            MaxLength="20" Width="100px"></asp:TextBox><label><asp:Label ID="Label13" runat="server"
                                                                                Text="Max" CssClass="labelcount"></asp:Label><span id="Span12" class="labelcount">20</span>
                                                                                <asp:Label ID="Label14" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label></label><input
                                                                                    id="hdnBarcode" runat="server" name="hdnBarcode" style="font-size: 8pt; width: 1px"
                                                                                    type="hidden" />
                                                                        <a href="javascript:void(0)" onclick="window.open('updateInventoryMasterPopUp.aspx?InventoryWarehouseMasterId=<%=InventoryWarehouseMasterIdB%>')"
                                                                            style="color: Blue" target="_blank">
                                                                            <%--Update Item--%>
                                                                        </a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <asp:Label Visible="false" ID="lblInvname1" runat="server">Inventory Name :</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label Visible="false" ID="lblInvName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="pnlproduct" runat="server">
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td width="25%">
                                                                    <label>
                                                                        <asp:Label ID="lblProductno" runat="server" Text="Product No."></asp:Label></label>
                                                                </td>
                                                                <td width="75%">
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlProductNo" runat="server" Width="348px">
                                                                        </asp:DropDownList>
                                                                        <input id="hdnPNo" runat="server" name="hdnPNo" style="font-size: 8pt; width: 1px"
                                                                            type="hidden" />
                                                                        <asp:Panel ID="pnlPO" runat="server">
                                                                            <a href="javascript:void(0)" onclick="window.open('updateInventoryMasterPopUp.aspx?InventoryWarehouseMasterId=<%=InventoryWarehouseMasterIdP%>')"
                                                                                target="_blank">
                                                                                <%--<span style="font-size: 8pt; color: #0000ff">Update Item </span>--%>
                                                                            </a>
                                                                        </asp:Panel>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Button ID="btngo" runat="server" CssClass="btnSubmit" Text="Go" OnClick="btngo_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" valign="top">
                                                    <asp:Panel ID="Panel2" Visible="false" runat="server">
                                                        <table align="left">
                                                            <tr>
                                                                <td valign="bottom">
                                                                    <label>
                                                                        <asp:Label ID="Label33" runat="server" Text="No. Of Cases"></asp:Label><asp:Label
                                                                            ID="Label48" runat="server" Text="*" CssClass="labelstar"></asp:Label><cc1:FilteredTextBoxExtender
                                                                                ID="FieredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtInvNoCase"
                                                                                ValidChars="0147852369">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtInvNoCase"
                                                                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator><asp:TextBox
                                                                                ID="txtInvNoCase" MaxLength="15" runat="server" Width="50px"></asp:TextBox></label>
                                                                </td>
                                                                <td align="bottom">
                                                                    <label>
                                                                        <asp:Label ID="Label35" runat="server" Text="Unit in Case"></asp:Label><asp:Label
                                                                            ID="Label49" runat="server" Text="*" CssClass="labelstar"></asp:Label><cc1:FilteredTextBoxExtender
                                                                                ID="FilteredTextBoxExtnder1" runat="server" Enabled="True" TargetControlID="txtUnitPrCase"
                                                                                ValidChars="0147852369">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtUnitPrCase"
                                                                            ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator><asp:TextBox
                                                                                ID="txtUnitPrCase" MaxLength="15" runat="server" Width="50px"></asp:TextBox></label>
                                                                </td>
                                                                <td valign="bottom">
                                                                    <label>
                                                                        <asp:Label ID="Label36" runat="server" Text="Price/Case"></asp:Label><asp:Label ID="Label50"
                                                                            runat="server" Text="*" CssClass="labelstar"></asp:Label><cc1:FilteredTextBoxExtender
                                                                                ID="FilteredTextBoxExender1" runat="server" Enabled="True" TargetControlID="txtPricePrCase"
                                                                                ValidChars="0147852369.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        <asp:RegularExpressionValidator ID="RegularEressionValidator15" ControlToValidate="txtPricePrCase"
                                                                            SetFocusOnError="true" ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server"
                                                                            ErrorMessage="Invalid" ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                                                                ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPricePrCase"
                                                                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator><asp:TextBox
                                                                                    ID="txtPricePrCase" MaxLength="15" runat="server" Width="50px"></asp:TextBox></label>
                                                                </td>
                                                                <asp:Panel ID="pnltsh" runat="server" Visible="false">
                                                                    <td valign="bottom">
                                                                        <label>
                                                                            <asp:Label ID="lblt1" runat="server" Text="Tax1" Visible="false"></asp:Label><asp:RegularExpressionValidator
                                                                                ID="RegularExpressionValidator3" ControlToValidate="txtt1" ValidationExpression="^(-)?\d+(\.\d\d)?$"
                                                                                runat="server" ErrorMessage="Invalid" Display="Dynamic" ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox
                                                                                    ID="txtt1" MaxLength="15" runat="server" Visible="false"></asp:TextBox></label>
                                                                    </td>
                                                                    <td valign="bottom" width="70px">
                                                                        <label>
                                                                            <asp:Label ID="lblt2" runat="server" Text="Tax2" Visible="false"></asp:Label><asp:RegularExpressionValidator
                                                                                ID="RegularExpressionValidator4" ControlToValidate="txtt2" ValidationExpression="^(-)?\d+(\.\d\d)?$"
                                                                                runat="server" ErrorMessage="Invalid" Display="Dynamic" ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox
                                                                                    ID="txtt2" MaxLength="15" runat="server" Visible="false"></asp:TextBox></label>
                                                                    </td>
                                                                    <td valign="bottom" width="70px">
                                                                        <label>
                                                                            <asp:Label ID="lblt3" runat="server" Text="Tax3" Visible="False"></asp:Label><asp:RegularExpressionValidator
                                                                                ID="RegularExpressihnValidator22" ControlToValidate="txtt3" ValidationExpression="^(-)?\d+(\.\d\d)?$"
                                                                                runat="server" ErrorMessage="Invalid" Display="Dynamic" ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox
                                                                                    ID="txtt3" MaxLength="15" runat="server" Visible="false"></asp:TextBox></label>
                                                                    </td>
                                                                </asp:Panel>
                                                                <td valign="bottom">
                                                                    <label>
                                                                        <asp:Label ID="Label31" runat="server" Text="Batch No."></asp:Label><%--    <asp:Label ID="Label15" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                            TargetControlID="txtInvNoCase" ValidChars="ABCDEFGSIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtBatch"
                                                            Display="Dynamic" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                                                        <asp:TextBox ID="txtBatch" runat="server" Style="font-weight: bold" Width="50px"
                                                                            MaxLength="10"></asp:TextBox></label>
                                                                </td>
                                                                <td valign="bottom">
                                                                    <label>
                                                                        <asp:Label ID="Label32" runat="server" Text=" Date "></asp:Label><asp:RequiredFieldValidator
                                                                            ID="Requiredalidator16" runat="server" SetFocusOnError="true" ControlToValidate="txtExDate"
                                                                            Display="Static" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator><cc1:MaskedEditExtender
                                                                                ID="MaskedEditExtender1534" runat="server" CultureName="en-AU" Enabled="True"
                                                                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtExDate" />
                                                                        <asp:TextBox ID="txtExDate" runat="server" Style="text-align: center" Width="73px"></asp:TextBox></label>
                                                                </td>
                                                                <td valign="bottom">
                                                                    <label>
                                                                        <asp:Label ID="Label27" runat="server" Text="Invoice Quantity" Visible="false"></asp:Label><br />
                                                                        <asp:Label ID="txtInvQty" MaxLength="15" Visible="false" runat="server"></asp:Label></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7">
                                                                    <table>
                                                                        <tr>
                                                                            <td valign="bottom">
                                                                                <label>
                                                                                    <asp:CheckBox ID="chkfreeq" AutoPostBack="true" runat="server" Text="Are you receiving any free quantities?"
                                                                                        OnCheckedChanged="chkfreeq_CheckedChanged" TextAlign="Left" />
                                                                                </label>
                                                                            </td>
                                                                            <td valign="bottom">
                                                                                <asp:Panel ID="pnlfreeq" runat="server" Visible="false">
                                                                                    <label>
                                                                                        <asp:Label ID="Label34" runat="server" Text="Free Cases"></asp:Label><cc1:FilteredTextBoxExtender
                                                                                            ID="FilteredextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtFreeCases"
                                                                                            ValidChars="0147852369">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                        <asp:TextBox ID="txtFreeCases" MaxLength="15" runat="server" Width="50px"></asp:TextBox></label></asp:Panel>
                                                                            </td>
                                                                            <td valign="bottom">
                                                                                <label>
                                                                                    <asp:CheckBox ID="ckhq" runat="server" TextAlign="Left" AutoPostBack="true" Text="Have you received the correct quantity?"
                                                                                        Checked="true" OnCheckedChanged="ckhq_CheckedChanged" />
                                                                                </label>
                                                                            </td>
                                                                            <asp:Panel ID="pnlquarrectqua" runat="server" Visible="false">
                                                                                <td valign="bottom">
                                                                                    <label>
                                                                                        <asp:Label ID="lblrnocase" runat="server" Text="Received No. of cases"></asp:Label><cc1:FilteredTextBoxExtender
                                                                                            ID="FilteredTtBoxExtender1" runat="server" Enabled="True" TargetControlID="txtRecNoCase"
                                                                                            ValidChars="0147852369">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                        <asp:TextBox ID="txtRecNoCase" MaxLength="15" runat="server" Width="50px" Text="0"></asp:TextBox></label>
                                                                                </td>
                                                                                <td valign="bottom" width="100px">
                                                                                    <label>
                                                                                        <asp:Label ID="lblrqty" runat="server" Visible="false" Text="Received Qty"></asp:Label><asp:TextBox
                                                                                            ID="txtRecdQty" MaxLength="15" Visible="false" runat="server" Width="50px" Text="0"
                                                                                            Enabled="False"></asp:TextBox></label>
                                                                                </td>
                                                                            </asp:Panel>
                                                                            <td valign="bottom">
                                                                                <label>
                                                                                    <%--<asp:Label ID="Label28" runat="server" Text="Rate"></asp:Label>
                                                                    <asp:Label ID="Label52" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                  
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRate11"
                                                                        Display="Dynamic" ErrorMessage="*" ValidationGroup="5"></asp:RequiredFieldValidator>--%>
                                                                                    <asp:TextBox ID="txtRate11" MaxLength="15" runat="server" Width="50px" Visible="false"
                                                                                        Enabled="False">0</asp:TextBox></label>
                                                                            </td>
                                                                            <td valign="bottom">
                                                                                <label>
                                                                                    <%--   <asp:Label ID="Label30" runat="server" Text="Total"></asp:Label>--%>
                                                                                    <asp:TextBox ID="txtTotals" MaxLength="15" runat="server" Width="50px" Enabled="False"
                                                                                        Visible="false">0</asp:TextBox></label>
                                                                            </td>
                                                                            <td valign="bottom">
                                                                                <asp:Button ID="btnCalCase" runat="server" OnClick="btnCalCase_Click" Text="Calculate"
                                                                                    ValidationGroup="1" CssClass="btnSubmit" Visible="False" />
                                                                                <asp:Button ID="btAdd" runat="server" OnClick="btAdd_Click" Text=" Add " ValidationGroup="1"
                                                                                    CssClass="btnSubmit" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="Panel7" runat="server" Width="100%">
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="InventoryWarehouseMasterId"
                                                            Width="100%" OnRowCommand="GridView1_RowCommand1" OnRowDeleting="GridView1_RowDeleting">
                                                            <Columns>
                                                                <asp:BoundField DataField="InventoryWarehouseMasterId" HeaderText="ID" InsertVisible="False"
                                                                    Visible="false" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="InventoryWarehouseMasterId" />
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIWMid" runat="server" Text='<% # Bind("InventoryWarehouseMasterId") %>'></asp:Label></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Category Name" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                                                                         <asp:Label ID="Label9" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                                                                     
                                                                        
                                                                        </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Inventory Name" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label></ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Product No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Labele3" runat="server" Text='<%# Bind("Barcode") %>' Width="60px"></asp:Label></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Weight/Unit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("UnitValues") %>'></asp:Label></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Batch No." HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtBatchNo" runat="server" Text='<%# Bind("BatchNo") %>' Width="60px"
                                                                            Enabled="false"></asp:TextBox></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Expiry Date (mm/dd/yyyy)" HeaderStyle-Width="9%" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1534" runat="server" CultureName="en-AU"
                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtExpDate" />
                                                                        <asp:TextBox ID="txtExpDate" runat="server" Text='<%# Bind("ExpiryDate") %>' Width="75px"
                                                                            Enabled="false"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender3" runat="server"
                                                                                PopupButtonID="txtExpDate" TargetControlID="txtExpDate">
                                                                            </cc1:CalendarExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Inv Qty" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                                                            TargetControlID="txtInvQty" ValidChars="0147852369.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                        <asp:TextBox ID="txtInvQty" runat="server" Text='<%# Bind("InvQty") %>' Width="60px"
                                                                            Enabled="false"></asp:TextBox></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13111111" runat="server"
                                                                            Enabled="True" TargetControlID="txtrate" ValidChars="0147852369.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                        <asp:TextBox ID="txtrate" runat="server" Text='<%# Bind("Rate") %>' Width="60px"
                                                                            Enabled="false"></asp:TextBox></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rec'd Qty" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender131111" runat="server" Enabled="True"
                                                                            TargetControlID="txtRecdQty" ValidChars="0147852369.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                        <asp:TextBox ID="txtRecdQty" runat="server" Text='<%# Bind("RecdQty") %>' Width="60px"
                                                                            Enabled="false"></asp:TextBox>
                                                                    <asp:Label ID="lblredmask" runat="server" Text="*" Visible="false" ForeColor="Red"></asp:Label>
                                                                            </ItemTemplate>
                                                                           
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Shortage<br> Qty" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="lblshortage" runat="server" Enabled="false" Text='<%# Bind("Shortage") %>'
                                                                            Width="60px"></asp:TextBox></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Shortage<br>Amount" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="lblshortageamt" runat="server" Enabled="false" Text='<%# Bind("ShortageAmt") %>'
                                                                            Width="60px"></asp:TextBox></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tax1" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgt1" runat="server" Text='<%# Bind("Tax1") %>' Width="40px" Enabled="false"></asp:TextBox></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tax2" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgt2" runat="server" Text='<%# Bind("Tax2") %>' Width="40px" Enabled="false"></asp:TextBox></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tax3" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgt3" runat="server" Text='<%# Bind("Tax3") %>' Width="40px" Enabled="false"></asp:TextBox></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender131151" runat="server" Enabled="True"
                                                                            TargetControlID="txtRecdQty" ValidChars="0147852369.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                        <asp:TextBox ID="Totals" runat="server" Text='<%# Bind("Totals") %>' Width="80px"
                                                                            Enabled="false"></asp:TextBox></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Btn" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                                            OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                                        </asp:ImageButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle Width="3%" />
                                                                </asp:TemplateField>
                                                                <asp:ButtonField CommandName="Edit" Text="Edit" Visible="False" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                                            <td align="right">
                                                                <label>
                                                                    <asp:Label ID="Label20" runat="server" Text="Invoice Amount"></asp:Label><asp:Label
                                                                        ID="Label62" runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RegularExpressionValidator
                                                                            ID="RegularExpressionValidator19" runat="server" ErrorMessage="*" Display="Dynamic"
                                                                            SetFocusOnError="True" ValidationExpression="^([0-9.]*)" ControlToValidate="txtValurRecd"
                                                                            ValidationGroup="11"></asp:RegularExpressionValidator></label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="txtValurRecd" runat="server" ReadOnly="True">0</asp:TextBox><%-- <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" TargetControlID="txtValurRecd" ValidChars="0147852369.">
                                        </cc1:FilteredTextBoxExtender>--%>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <label>
                                                                    <asp:Label ID="Label21" runat="server" Text="Shortage Amount"></asp:Label><asp:Label
                                                                        ID="Label53" runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                                                            ID="RequiredFiedValidator10" runat="server" Display="Dynamic" ControlToValidate="txtShort"
                                                                            ErrorMessage="*" ValidationGroup="11"></asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                                                                ID="FiteredTextBoxExtender1311121" runat="server" Enabled="True" TargetControlID="txtShort"
                                                                                ValidChars="0147852369.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                    <%-- <asp:RegularExpressionValidator ID="RegularExpssionValidator20" ControlToValidate="txtShort" SetFocusOnError="true"
                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"> </asp:RegularExpressionValidator>
--%>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="txtShort" runat="server" Text="0" ReadOnly="True"></asp:TextBox></label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <label>
                                                                    <asp:Label ID="Label22" runat="server" Text="Net Amount"></asp:Label><asp:Label ID="Label54"
                                                                        runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                                                            ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtInvAmt" ErrorMessage="*"></asp:RequiredFieldValidator><%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtInvAmt"
                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"> </asp:RegularExpressionValidator>
                     --%>
                                                                </label>
                                                            </td>
                                                            <td align="right">
                                                                <label>
                                                                    <asp:TextBox ID="txtInvAmt" MaxLength="15" runat="server" ReadOnly="True">0</asp:TextBox></label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <asp:Panel ID="pnltax1" runat="server" Visible="false">
                                                                <td align="right">
                                                                    <label>
                                                                        <asp:Label ID="Label23" runat="server" Text="Tax1"></asp:Label><asp:Label ID="lbltaxper1"
                                                                            runat="server" Visible="false"></asp:Label><asp:Label ID="Label55" runat="server"
                                                                                Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                                                                    runat="server" ControlToValidate="txtTax1" ErrorMessage="*"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                                        ID="RegularExpressionValidator8" ControlToValidate="txtTax1" ValidationExpression="^(-)?\d+(\.\d\d)?$"
                                                                                        runat="server" ErrorMessage="Invalid"> </asp:RegularExpressionValidator>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:TextBox ID="txtTax1" MaxLength="15" runat="server">0</asp:TextBox></label>
                                                                </td>
                                                            </asp:Panel>
                                                        </tr>
                                                        <tr>
                                                            <asp:Panel ID="pnltax2" runat="server" Visible="false" Style="margin-bottom: 0px">
                                                                <td align="right">
                                                                    <label>
                                                                        <asp:Label ID="Label24" runat="server" Text="Tax2"></asp:Label><asp:Label ID="lbltaxper2"
                                                                            runat="server" Visible="false"></asp:Label><asp:Label ID="Label56" runat="server"
                                                                                Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                                                                                    runat="server" ControlToValidate="txtTax2" ErrorMessage="*"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                                        ID="RegularExpressionValidator9" ControlToValidate="txtTax2" ValidationExpression="^(-)?\d+(\.\d\d)?$"
                                                                                        runat="server" ErrorMessage="Invalid"> </asp:RegularExpressionValidator>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:TextBox ID="txtTax2" MaxLength="15" runat="server">0</asp:TextBox></label>
                                                                </td>
                                                            </asp:Panel>
                                                        </tr>
                                                        <tr>
                                                            <asp:Panel ID="pnltax3" runat="server" Visible="false">
                                                                <td align="right">
                                                                    <label>
                                                                        <asp:Label ID="Label60" runat="server" Text="Tax3"></asp:Label><asp:Label ID="lbltaxper3"
                                                                            runat="server" Visible="false"></asp:Label><asp:Label ID="lbltxtop" runat="server"
                                                                                Visible="False">0</asp:Label><asp:Label ID="Label61" runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                                                                    ID="RequiredFieldValidapotor16" runat="server" ControlToValidate="txtTax3" ErrorMessage="*"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                                        ID="RegularExpressioalidator22" ControlToValidate="txtTax3" ValidationExpression="^(-)?\d+(\.\d\d)?$"
                                                                                        runat="server" ErrorMessage="Invalid"> </asp:RegularExpressionValidator>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:TextBox ID="txtTax3" MaxLength="15" runat="server">0</asp:TextBox></label>
                                                                </td>
                                                            </asp:Panel>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <label>
                                                                    <asp:Label ID="Label25" runat="server" Text="Shipping and Other Charges"></asp:Label><asp:Label
                                                                        ID="Label57" runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                                                            ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtShippCharge"
                                                                            ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                                ID="RegularExpressionValidator10" ControlToValidate="txtShippCharge" ValidationExpression="^(-)?\d+(\.\d\d)?$"
                                                                                runat="server" ErrorMessage="Invalid" Display="Dynamic"></asp:RegularExpressionValidator></label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="txtShippCharge" MaxLength="15" runat="server">0</asp:TextBox></label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <label>
                                                                    <asp:Label ID="Label26" runat="server" Text="Total Invoice Amount"></asp:Label><asp:Label
                                                                        ID="Label58" runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                                                            ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtNetAmount"
                                                                            ErrorMessage="*"></asp:RequiredFieldValidator><%--<asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="txtNetAmount"
                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"> </asp:RegularExpressionValidator>--%>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="txtNetAmount" MaxLength="15" runat="server" ReadOnly="True">0</asp:TextBox></label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkdoc" runat="server" Text="Do you wish to attach or upload any documents related to this entry?" />
                                                                <br />
                                                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Add Inventory received qty to inventory on hand for shopping cart.?"
                                                                    Visible="False" />
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td colspan="2" align="center">
                                                                <asp:Button ID="btCal" runat="server" OnClick="btCal_Click" Text="Recalculate and Confirm"
                                                                    CssClass="btnSubmit" ValidationGroup="11" />
                                                                <%-- <asp:Button ID="btConfirm" runat="server" OnClick="btConfirm_Click" Text="Confirm"
                                        ValidationGroup="11" Visible="False" CssClass="btnSubmit" />--%>
                                                                <asp:Button ID="btSubmit" runat="server" OnClick="btSubmit_Click" Text="Update" ValidationGroup="11"
                                                                    Visible="False" CssClass="btnSubmit" />
                                                                  
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton5"
                                                                    TargetControlID="txtExDate">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="2" align="left">
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="2" align="left">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="txttinfo1" runat="server"></asp:Label></label><label><asp:Label ID="txttinfo2"
                                                                        runat="server"></asp:Label></label><label><asp:Label ID="txttinfo3" runat="server"></asp:Label></label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Panel ID="Panel5" runat="server" CssClass="modalPopup" Width="300px">
                                        <fieldset>
                                            <legend></legend>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="height: 18px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="lblm0" runat="server">There is no tax account set to account 
                                                        tax on purchases. Would you like to create a new tax account? </asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="btnyes" CssClass="btnSubmit" runat="server" Text="Yes" OnClick="btnyes_Click" />
                                                        &nbsp;<asp:Button ID="btncan" CssClass="btnSubmit" runat="server" Text="Cancel" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Panel>
                                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                                        ID="ModalPopupExtender12222" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="Panel5" TargetControlID="HiddenButton222" CancelControlID="btncan">
                                    </cc1:ModalPopupExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" Width="75%">
                                        <fieldset>
                                            <legend></legend>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            For example, if you purchased Inventory A for $1000, which you plan to sell at a
                                                            later date, you would be taxed $100 on your purchase.<br />
                                                            When you go to sell Inventory A for $2000 you may be required to pay 10% (which
                                                            would be $200) in tax.
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            By clicking on this option, your purchase account would be debited with $1000 and
                                                            $100 would be debited as input tax credit,<br />
                                                            which you can later offset against your liability to pay tax on sales you make.
                                                            If you do not click on this option,<br />
                                                            please note that the complete $1100 would be debited to your purchase account.
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            If you are allowed input tax credit in your business, we recommend using this option.
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="Button1" CssClass="btnSubmit" runat="server" Text="Cancel" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Panel>
                                    <asp:Button ID="Button3" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                                        ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="Panel4" TargetControlID="Button3" CancelControlID="Button1">
                                    </cc1:ModalPopupExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Panel ID="Panel6" runat="server" CssClass="modalPopup" Width="70%">
                                        <fieldset>
                                            <legend></legend>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Please note that, you cannot change this option selection once you've made it. If
                                                            you wish to change your selection,<br />
                                                            you will have to create a new invoice by clicking the button at the top of the page.
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="btnk" CssClass="btnSubmit" runat="server" Text=" OK " OnClick="btnk_Click" />
                                                        <asp:Button ID="btnccc" CssClass="btnSubmit" runat="server" Text="Cancel" OnClick="btnccc_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        &nbsp;&nbsp;&nbsp;</asp:Panel>
                                    <asp:Button ID="Button4" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                                        ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="Panel6" TargetControlID="Button4">
                                    </cc1:ModalPopupExtender>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                   </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
