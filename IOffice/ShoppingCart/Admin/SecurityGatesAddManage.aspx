<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="SecurityGatesAddManage.aspx.cs" Inherits="ShoppingCart_Admin_Default"
    Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

   
         function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        } 

        function mask(evt) {

            if (evt.keyCode == 13) {


            }


            if (evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
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
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="lblmessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllegend" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button4" runat="server" Text="Add New Security Gate" OnClick="Button4_Click"
                            Width="170px" CssClass="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel1" runat="server" Visible="False">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        Business Name
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlbusiness" runat="server" Width="250px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        Security Gate Name
                                        <asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtsecure"
                                            SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtsecure"
                                            ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtsecure" runat="server" MaxLength="60" onKeydown="return mask(event)"
                                            Width="250px" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'Span4',60)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label5" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="Span4" cssclass="labelcount">60</span>
                                        <asp:Label ID="Label8" runat="server" Text="(A-Z 0-9 . , ? _)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        Location of the Gate
                                        <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtlocation"
                                            SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtlocation"
                                            ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtlocation" runat="server" MaxLength="60" onKeydown="return mask(event)"
                                            Width="250px" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'Span1',60)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label10" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="Span1" cssclass="labelcount">60</span>
                                        <asp:Label ID="Label11" runat="server" Text="(A-Z 0-9 . , ? _)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        Status
                                    </label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkstatus" runat="server" Text="Active" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="Label9" runat="server" Text="Label" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" Text="Submit"
                                        CssClass="btnSubmit" ValidationGroup="1" />
                                    <asp:Button ID="btnupdate" runat="server" Text="Update" Visible="False" OnClick="btnupdate_Click"
                                        CssClass="btnSubmit" ValidationGroup="1" />
                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                        CssClass="btnSubmit" ValidationGroup="1" CausesValidation="False" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label18" runat="server" Text="List of Security Gates"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnPrintVersion" runat="server" Text="Printable Version" OnClick="btnPrintVersion_Click"
                            CausesValidation="False" CssClass="btnSubmit" />
                        <input id="btnPrint" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" class="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    Business Name
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlbusiness1" runat="server" Width="200px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlbusiness1_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    Status
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlstatus_search" runat="server" Width="180px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlstatus_search_SelectedIndexChanged">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            <td colspan="3">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td colspan="2" class="style1">
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="lblCompany" runat="server" Font-Italic="True" Text="Business : "></asp:Label>
                                                                <asp:Label ID="Label1" runat="server" Font-Italic="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label3" runat="server" Font-Italic="True" Text="List of Security Gates "></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                                                    AllowPaging="True" AllowSorting="True" OnSorting="GridView1_Sorting" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                    PageSize="10" EmptyDataText="No Record Found." AlternatingRowStyle-CssClass="alt"
                                                    PagerStyle-CssClass="pgr" CssClass="mGrid">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business Name" SortExpression="BusinessName" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("BusinessName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Security Gate Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="25%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Location" SortExpression="Location" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="30%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" SortExpression="Active" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Labesdsdl6" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                <%--<asp:CheckBox ID="CheckBox1" Enabled="false" runat="server" Checked='<%# Eval("Active") %>' />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                                    CommandName="Edit" ImageUrl="~/Account/images/edit.gif" ToolTip="Edit" CausesValidation="False" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/ShoppingCart/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton49" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                                    CommandName="Delete" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                    ImageUrl="~/Account/images/delete.gif" ToolTip="Delete" CausesValidation="False" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
