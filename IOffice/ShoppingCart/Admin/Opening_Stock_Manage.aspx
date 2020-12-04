<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="Opening_Stock_Manage.aspx.cs" Inherits="ShoppingCart_Admin_Opening_Balance"
    Title="Untitled Page" %>

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
            elsegridDCregister
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblerror" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <div style="float: left;">
                        <label>
                            <asp:Label ID="Label1" runat="server" Text="Initial inventory list of the company for the first accounting year "> </asp:Label>
                           
                            
                            <asp:Label ID="lbldate" runat="server" Font-Bold="True"> </asp:Label>
                             <asp:Label ID="Label7" runat="server" Text=" to "> </asp:Label>
                            <asp:Label ID="lblenddate" runat="server" Font-Bold="True"> </asp:Label>
                             <%--<asp:Label ID="Label6" runat="server" Text=")"> </asp:Label>--%>
                            <asp:Label ID="lblerror0" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                        </label>
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Add new Inventory" OnClick="Button3_Click" />
                        <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" Text="Refresh" 
                            onclick="Button4_Click"  />
                    </div>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Initial Inventory" Font-Bold="true" runat="server"></asp:Label>
                    </legend>
                    <%--   <div style="float: right;">
                    </div>--%>
                    <div style="clear: both;">
                    </div>
                    <div style="float: right;">
                       
                            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click1" CausesValidation="False" />
                            <input id="Button2" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                class="btnSubmit" type="button" value="Print" visible="false" />
                      
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <table style="width: 100%">
                            <tr>
                                <td align="left">
                                    <div style="float: left;">
                                        <label>
                                            <asp:Label ID="Label2" Text="Select by Business Name" runat="server"></asp:Label>
                                           
                                            <asp:DropDownList ID="ddlSearchByStore" runat="server" AutoPostBack="True" 
                                            OnSelectedIndexChanged="ddlSearchByStore_SelectedIndexChanged" Enabled="False">
                                            </asp:DropDownList>
                                        </label>
                                    </div>
                                </td>
                                <td align="right" style="text-align: right">
                                    <div style="float: right;">
                                       
                                           
                                       
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblcompname" runat="server" Visible="false" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label3" runat="server" Font-Italic="True" Text="Business : "></asp:Label>
                                                    <asp:Label ID="lblstore" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label4" runat="server" Font-Italic="True" Text="List of Initial Inventory"> </asp:Label>
                                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td width="100%">
                                    <asp:GridView ID="grdservicestore" runat="server" AutoGenerateColumns="False" Width="100%"
                                        DataKeyNames="InventoryMasterId" AllowSorting="True" CssClass="mGrid" GridLines="Both"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnSorting="grdservicestore_Sorting"
                                        EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business Name" Visible="false" SortExpression="WareHouseId">
                                                <ItemTemplate>
                                                    
                                                    <asp:Label ID="lblstoreid" runat="server" Text='<%#Bind("WareHouseId") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inventory_Details_Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldeteilid" runat="server" Text='<%#Bind("Inventory_Details_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="InventoryMasterId" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvMasterId" runat="server" Text='<%#Bind("InventoryMasterId") %>'></asp:Label>
                                                    <asp:Label ID="lblinvwhmasterid" runat="server" Text='<%#Bind("InventoryWarehouseMasterId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category: Sub Category: Sub Sub Category" HeaderStyle-Width="40%"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" SortExpression="CateAndName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory" runat="server" Text='<%#Bind("CateAndName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inventory Name" HeaderStyle-Width="22%" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="InventoryMasterName">
                                                <ItemTemplate>
                                                  <a href='Inventoryprofile.aspx?Invmid=<%# Eval("InventoryMasterId")%>' target="_blank">
                                                    <asp:Label ID="lblInvName" runat="server" Text='<%#Bind("InventoryMasterName") %>' ForeColor="#416271">

                                                    </asp:Label>
                                                     </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Weight/Unit" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="UnitTypeName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblunitType" runat="server" Text='<%#Bind("UnitTypeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Initial Quantity" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtopeningqty" runat="server" MaxLength="15" Width="150px" Enabled="false">

                                                    </asp:TextBox>
                                                      <asp:Label ID="lblopqty" runat="server" Visible="false"></asp:Label>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10123" runat="server" Enabled="True"
                                                        TargetControlID="txtopeningqty" ValidChars="0147852369">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtopeningqty"
                                                        ErrorMessage="*" ValidationGroup="2">

                                                    </asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cost of Product" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtopeningrate123" MaxLength="15" Width="150px" runat="server" Enabled="false">

                                                    </asp:TextBox>
                                                     <asp:Label ID="lbloprate" runat="server" Visible="false"></asp:Label>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                        TargetControlID="txtopeningrate123" ValidChars="0147852369.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10123" runat="server" ControlToValidate="txtopeningrate123"
                                                        ErrorMessage="*" ValidationGroup="2">

                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtopeningrate123"
                                                        ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ValidationGroup="2"
                                                        ErrorMessage="Invalid Digits" Display="Dynamic"> 
                                                    

                                                    </asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="Statuslabel">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkinvMasterStatus" Checked='<%#Eval("MasterActiveStatus") %>'
                                                        Enabled="false" Visible="false" runat="server" />
                                                    <asp:Label ID="lblmasterstatuslabel" runat="server" Text='<%#Bind("Statuslabel") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                            <td></td>
                            </tr>
                            <tr>
                            <td align="center">
                             <asp:Button ID="BtnChange" runat="server" OnClick="BtnChange_Click" CssClass="btnSubmit"
                                                Text="Change" ValidationGroup="1" />
                                            <asp:Button ID="imgsubmitrate" runat="server" CssClass="btnSubmit" OnClick="imgsubmitrate_Click"
                                                Text="Update and Close" ValidationGroup="2" Visible="False" />
                                            <asp:Button ID="btncancel" runat="server" CssClass="btnSubmit" 
                                            Text="Cancel" Visible="False" onclick="btncancel_Click" />
                                             <asp:Button ID="btnGoback" runat="server" CssClass="btnSubmit"
                                                Text="Go Back" ValidationGroup="1" onclick="btnGoback_Click" />
                            </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
