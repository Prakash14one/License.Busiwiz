<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="EOQManagement.aspx.cs" Inherits="ShoppingCart_Admin_EOQManagement" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
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
    function ShowMyModalPopup5() 
        {  
         var modal5 = $find("<%=ModalPopupExtender5.ClientID%>");  
         modal5.show(); 
          
        }
        function ShowMyModalPopup4() 
        {  
         var modal4 = $find("<%=ModalPopupExtender4.ClientID%>");  
         modal4.show(); 
          
        }
        
         function ShowMyModalPopup3() 
        {  
         var modal3 = $find("<%=ModalPopupExtender3.ClientID%>");  
         modal3.show(); 
          
        }
         function ShowMyModalPopup2() 
        {  
         var modal2 = $find("<%=ModalPopupExtender2.ClientID%>");  
         modal2.show(); 
          
        }
    </script>

    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="btnadd" CssClass="btnSubmit" runat="server" Text="Add Cost for the Vendor"
                            OnClick="btnadd_Click" />
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label2" runat="server" Text="I. Inventory Carrying Cost Calculation"></asp:Label>
                            </legend>
                            <label>
                                <asp:Label ID="Label3" runat="server" Text="Business Name"></asp:Label>
                                <asp:DropDownList ID="ddlstrname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstrname_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="Label4" runat="server" Text="Site Name"></asp:Label>
                                <asp:Label ID="Label52" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlsite"
                                    ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlsite" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlsite_SelectedIndexChanged"
                                    Width="175px">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <br />
                                <asp:ImageButton ID="ImageButton50" runat="server" Height="20px" ImageAlign="Bottom"
                                    ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" ToolTip="AddNew" Width="20px"
                                    OnClick="ImageButton50_Click" />
                            </label>
                            <label>
                                <br />
                                <asp:ImageButton ID="ImageButton51" runat="server" Height="20px" ImageAlign="Bottom"
                                    ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                    OnClick="ImageButton51_Click1" />
                            </label>
                            <div style="clear: both;">
                            </div>
                            <label>
                                <asp:Label ID="Label5" runat="server" Text="A) Average warehousing cost per cubic feet"></asp:Label>
                            </label>
                            <label class="cssLabelCompany_Information_Ans">
                                <asp:Label ID="lblavgwhcost" runat="server" Text="0" CssClass="lblSuggestion"></asp:Label>
                            </label>
                            <div style="clear: both;">
                            </div>
                            <label>
                                <asp:Label ID="Label6" runat="server" Text="B) Effictive interest cost for carrying inventory"></asp:Label>
                                <div style="clear: both;">
                                </div>
                                <asp:Label ID="Label1" runat="server" Text="(For eample: interest on your line of credit)"></asp:Label>
                            </label>
                            <label class="cssLabelCompany_Information_Ans">
                                <asp:Label ID="txtintperyear" runat="server" Text="0" CssClass="lblSuggestion"></asp:Label>
                            </label>
                            <div style="clear: both;">
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label8" runat="server" Text="II. Ordering Cost"></asp:Label>
                            </legend>
                            <table width="100%">
                                <tr valign="top">
                                    <td width="60%">
                                        <label>
                                            <asp:Label ID="Label9" runat="server" Text="Select Vendor"></asp:Label>
                                            <asp:Label ID="Label44" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlvendor"
                                                InitialValue="0" ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                    </td>
                                    <td width="39%">
                                        <label>
                                            <asp:DropDownList ID="ddlvendor" runat="server" Width="98px">
                                            </asp:DropDownList>
                                        </label>
                                        <label>
                                            <asp:ImageButton ID="ImageButton1" runat="server" Height="20px" ImageAlign="Bottom"
                                                ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" ToolTip="AddNew" Width="20px"
                                                OnClick="ImageButton1_Click" />
                                        </label>
                                        <label>
                                            <asp:ImageButton ID="ImageButton2" runat="server" Height="20px" ImageAlign="Bottom"
                                                ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                                OnClick="ImageButton2_Click" />
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="Label10" runat="server" Text="Number Of products purchased from this vendor"></asp:Label>
                                            <asp:Label ID="Label45" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="ddc" runat="server" ControlToValidate="txtnumofproduct"
                                                SetFocusOnError="true" ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <a onclick="ShowMyModalPopup2();">Note</a>
                                        </label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtnumofproduct" Style="text-align: right" runat="server" Width="95"
                                                MaxLength="10" onkeyup="return mak('Span4',10,this)">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTttttextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="txtnumofproduct" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span4" class="labelcount">10</span>
                                            <asp:Label ID="Label12" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="Label18" runat="server" Text="Normal Volume Size per order"></asp:Label>
                                            <asp:Label ID="Label46" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtvolumesize"
                                                SetFocusOnError="true" ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <a onclick="ShowMyModalPopup3();">Note</a>
                                        </label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtvolumesize" Style="text-align: right" runat="server" MaxLength="10"
                                                Width="95px" onkeyup="return mak('Span7',10,this)">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBohggfxExtender2" runat="server" Enabled="True"
                                                TargetControlID="txtvolumesize" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label7" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span7" class="labelcount">10</span>
                                            <asp:Label ID="Label19" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="Label20" runat="server" Text="Order Placement Cost"></asp:Label>
                                            <asp:Label ID="Label47" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtreqcost"
                                                SetFocusOnError="true" ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic"
                                                ControlToValidate="txtreqcost" ValidationGroup="1" ValidationExpression="^(-)?\d+(\.\d\d)?$"
                                                runat="server" ErrorMessage="Invalid Digits"> </asp:RegularExpressionValidator>
                                            <a onclick="ShowMyModalPopup4();">Note</a>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label32" runat="server" Text="$"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtreqcost" Style="text-align: right" runat="server" MaxLength="10"
                                                Width="95px" onkeyup="return mak('Span1',10,this)">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTggdfgdfextBoxExtender3" runat="server"
                                                Enabled="True" TargetControlID="txtreqcost" ValidChars="0123456789.">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label39" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span1" class="labelcount">10</span>
                                            <asp:Label ID="Label13" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="Label21" runat="server" Text="Number of days to get inventory after placing order"></asp:Label>
                                            <asp:Label ID="Label48" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtnumofdays"
                                                SetFocusOnError="true" ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtnumofdays" Style="text-align: right" runat="server" MaxLength="10"
                                                Width="95px" onkeyup="return mak('Span2',10,this)">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxhfgyhfghExtender4" runat="server"
                                                Enabled="True" TargetControlID="txtnumofdays" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label40" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span2" class="labelcount">10</span>
                                            <asp:Label ID="Label14" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="Label22" runat="server" Text="Normal freight cost to get the above order from vendor to your warehouse"></asp:Label>
                                            <asp:Label ID="Label49" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtfreightcost"
                                                SetFocusOnError="true" ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtfreightcost"
                                                ValidationGroup="1" ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server"
                                                ErrorMessage="Invalid Digits"> </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label33" runat="server" Text="$"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtfreightcost" Style="text-align: right" runat="server" MaxLength="10"
                                                Width="95px" onkeyup="return mak('Span3',10,this)">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTe123xtBoxExtender5" runat="server" Enabled="True"
                                                TargetControlID="txtfreightcost" ValidChars="0123456789.">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label41" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span3" class="labelcount">10</span>
                                            <asp:Label ID="Label15" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="Label23" runat="server" Text="Receiving, Inspecting, Stocking per normal regular size order cost"></asp:Label>
                                            <asp:Label ID="Label50" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtsizecost"
                                                SetFocusOnError="true" ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtsizecost"
                                                ValidationGroup="1" ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server"
                                                ErrorMessage="Invalid Digits"> </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label34" runat="server" Text="$"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtsizecost" Style="text-align: right" runat="server" MaxLength="10"
                                                Width="95px" onkeyup="return mak('Span5',10,this)">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FiltefffredTextBoxExtender6" runat="server" Enabled="True"
                                                TargetControlID="txtsizecost" ValidChars="0123456789.">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label42" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span5" class="labelcount">10</span>
                                            <asp:Label ID="Label16" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <label>
                                            <asp:Label ID="Label24" runat="server" Text="Invoice Processing Cost"></asp:Label>
                                            <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtinvoicecost"
                                                SetFocusOnError="true" ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtinvoicecost"
                                                ValidationGroup="1" ValidationExpression="^(-)?\d+(\.\d\d)?$" Display="Dynamic"
                                                runat="server" ErrorMessage="Invalid Digits"> </asp:RegularExpressionValidator>
                                            <a onclick="ShowMyModalPopup5();">Note</a>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label35" runat="server" Text="$"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtinvoicecost" Style="text-align: right" runat="server" MaxLength="10"
                                                Width="95px" onkeyup="return mak('Span6',10,this)">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTexgggggtBoxExtender7" runat="server" Enabled="True"
                                                TargetControlID="txtinvoicecost" ValidChars="0123456789.">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label43" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                            <span id="Span6" class="labelcount">10</span>
                                            <asp:Label ID="Label17" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btncalc" runat="server" Text="Calculate" ValidationGroup="1" CssClass="btnSubmit"
                                            OnClick="btncalc_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label25" runat="server" Text="Total order cost for this vendor"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label36" runat="server" Text="$"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lbltotalcost" runat="server" Text="0" CssClass="lblSuggestion"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label26" runat="server" Text="Total order cost per product for this vendor"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label37" runat="server" Text="$"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="lbltotalcostperproduct" CssClass="lblSuggestion" runat="server" Text="0"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" width="100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click"
                                            ValidationGroup="1" CssClass="btnSubmit" />
                                        <asp:Button ID="btnreset" runat="server" Text="Cancel" CausesValidation="false" CssClass="btnSubmit"
                                            OnClick="btnreset_Click" />
                                        <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click"
                                            ValidationGroup="1" CssClass="btnSubmit" Visible="False" />
                                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CausesValidation="false"
                                            OnClick="btncancel_Click" CssClass="btnSubmit" Visible="False" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label27" runat="server" Text="EOQ/Ordering Cost Per Vendor"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btncancel0" runat="server" CssClass="btnSubmit" CausesValidation="false"
                            Text="Printable Version" OnClick="btncancel0_Click" />
                        <input id="Button7" runat="server" visible="false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" type="button" value="Print" />
                    </div>
                    <label>
                        <asp:Label ID="lblwnamefilter" runat="server" Text="Business Name"></asp:Label>
                        <asp:DropDownList ID="ddlsearchByStore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsearchByStore_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="lblvendor" runat="server" Text="Select Vendor"></asp:Label>
                        <asp:DropDownList ID="ddlfiltervendor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfiltervendor_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
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
                                                    <asp:Label ID="lblcompany" runat="server" Font-Size="20px" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label38" runat="server" Font-Size="20px" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblbusiness" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label11" runat="server" Text="EOQ/Ordering Cost Per Vendor" Font-Size="18px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblDivision" runat="server" Text="Vendor Name : " Font-Size="16px"
                                                        Font-Bold="false"></asp:Label>
                                                    <asp:Label ID="lblcostbyven" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr style="vertical-align: top;">
                                <td>
                                    <cc11:PagingGridView ID="grid" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                        Width="100%" AllowSorting="True" AllowPaging="true" PageSize="10" CellPadding="4"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No Record Found." OnRowEditing="grid_RowEditing" OnRowDeleting="grid_RowDeleting"
                                        OnPageIndexChanging="grid_PageIndexChanging" OnSorting="grid_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Party Name" SortExpression="Contactperson" Visible="true"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpartyname" runat="server" Text='<%# Eval("Contactperson")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No. of Products Per Order" SortExpression="NumOfProduct"
                                                Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnoproduct" runat="server" Text='<%# Eval("NumOfProduct")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Volume Size Per Order" SortExpression="VolumeSize"
                                                Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvolumesize" runat="server" Text='<%# Eval("VolumeSize")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ordering and Requisition Cost" SortExpression="OrderAndReqCost"
                                                Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblreqcost" runat="server" Text='<%# Eval("OrderAndReqCost")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Freight Cost" SortExpression="FreightCost" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfreightcost" runat="server" Text='<%# Eval("FreightCost")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Recieving Costs" SortExpression="PerSizeCost" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpersize" ToolTip="Recieving, Inspecting, and Stocking Costs" runat="server"
                                                        Text='<%# Eval("PerSizeCost")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Processing cost" SortExpression="ProcessCost"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprocesscost" runat="server" Text='<%# Eval("ProcessCost")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Ordering Cost Per Product" SortExpression="TotalCostperProduct"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcostperproduct" runat="server" Text='<%# Eval("TotalCostperProduct")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Ordering Cost" SortExpression="TotalCost" HeaderStyle-Width="4%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltotalcost" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnedit" runat="server" CommandName="Edit" ImageUrl="~/Account/images/edit.gif"
                                                        ToolTip="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:CommandField HeaderText="Edit" ShowEditButton="True" ValidationGroup="2" ButtonType="Image"
                                                EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/updategrid.jpg"
                                                CancelImageUrl="~/images/delete.gif" HeaderImageUrl="~/Account/images/edit.gif"
                                                ItemStyle-Width="2%" />--%>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc11:PagingGridView>
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel4" runat="server" BackColor="White" BorderColor="#999999" Width="600px"
                                BorderStyle="Solid" BorderWidth="10px">
                                <fieldset>
                                    <div align="right">
                                        <asp:ImageButton ID="ImageButton3" runat="server" AlternateText="Close" CausesValidation="False"
                                            ImageUrl="~/Account/images/closeicon.png"></asp:ImageButton>
                                    </div>
                                    <div style="clear: both;">
                                    </div>
                                    <div>
                                        <label>
                                            If you purchase 20 (qty) of the same item (computer), this only is considered 1
                                            item. If you are purchasing 20 computers and 10 keyboards, this is considered only
                                            2 items.
                                        </label>
                                    </div>
                                </fieldset>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="ImageButton3" Drag="true" PopupControlID="Panel4" TargetControlID="Hidden2">
                                </cc1:ModalPopupExtender>
                                <input id="Hidden2" runat="Server" name="Hidden2" style="width: 4px" type="hidden" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <div style="clear: both;">
                </div>
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#999999" Width="600px"
                                BorderStyle="Solid" BorderWidth="10px">
                                <fieldset>
                                    <div align="right">
                                        <asp:ImageButton ID="ImageButton4" runat="server" AlternateText="Close" CausesValidation="False"
                                            ImageUrl="~/Account/images/closeicon.png"></asp:ImageButton>
                                    </div>
                                    <div style="clear: both;">
                                    </div>
                                    <div>
                                        <label>
                                            Total volume of the skids per regular order. For example, you are buying 2 skids
                                            that are 4*4*4 feet. The total volume would be 64 cubic feet plus 64 cubic feet
                                            for a total of 128 cubic feet.
                                        </label>
                                    </div>
                                </fieldset>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="ImageButton4" Drag="true" PopupControlID="Panel1" TargetControlID="Hidden1">
                                </cc1:ModalPopupExtender>
                                <input id="Hidden1" runat="Server" name="Hidden1" style="width: 4px" type="hidden" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <div style="clear: both;">
                </div>
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="#999999" Width="600px"
                                BorderStyle="Solid" BorderWidth="10px">
                                <fieldset>
                                    <div align="right">
                                        <asp:ImageButton ID="ImageButton5" runat="server" AlternateText="Close" CausesValidation="False"
                                            ImageUrl="~/Account/images/closeicon.png"></asp:ImageButton>
                                    </div>
                                    <div style="clear: both;">
                                    </div>
                                    <div>
                                        <label>
                                            This includes the cost of all employees involved in preparing the requisition and
                                            preparing an order.
                                        </label>
                                    </div>
                                </fieldset>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="ImageButton5" Drag="true" PopupControlID="Panel2" TargetControlID="Hidden3">
                                </cc1:ModalPopupExtender>
                                <input id="Hidden3" runat="Server" name="Hidden3" style="width: 4px" type="hidden" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <div style="clear: both;">
                </div>
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel3" runat="server" BackColor="White" BorderColor="#999999" Width="600px"
                                BorderStyle="Solid" BorderWidth="10px">
                                <fieldset>
                                    <div align="right">
                                        <asp:ImageButton ID="ImageButton6" runat="server" AlternateText="Close" CausesValidation="False"
                                            ImageUrl="~/Account/images/closeicon.png"></asp:ImageButton>
                                    </div>
                                    <div style="clear: both;">
                                    </div>
                                    <div>
                                        <label>
                                            This includes the cost of receiving the invoice, making a payment and accounting.
                                        </label>
                                    </div>
                                </fieldset>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="ImageButton6" Drag="true" PopupControlID="Panel3" TargetControlID="Hidden4">
                                </cc1:ModalPopupExtender>
                                <input id="Hidden4" runat="Server" name="Hidden4" style="width: 4px" type="hidden" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
