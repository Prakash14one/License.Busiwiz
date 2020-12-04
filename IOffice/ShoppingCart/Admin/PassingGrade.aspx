<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="PassingGrade.aspx.cs" Inherits="ShoppingCart_Admin_Default"
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
        .style1
        {
            height: 10px;
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
                <asp:Label ID="lblmessage" runat="server" ForeColor="Red" TVisible="False"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllegend" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Add New Passing Grade" OnClick="Button1_Click"
                            CssClass="btnSubmit" Width="160px" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel1" runat="server" Visible="False">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        Name
                                        <asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtname"
                                            SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtname"
                                            ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtname" runat="server" MaxLength="60" onKeydown="return mask(event)"
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
                                        Equivalent GPA
                                        <asp:Label ID="Label1" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtequiGpa"
                                            SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtequiGpa" runat="server" Width="70px" MaxLength="6" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z.0-9]+$/,'Span2',6)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label25" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="Span2" cssclass="labelcount">6</span>
                                        <asp:Label ID="Label13" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label>
                                    </label>
                                    <cc1:FilteredTextBoxExtender ID="txtEndzip_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" TargetControlID="txtequiGpa" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
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
                                        ValidationGroup="1" CssClass="btnSubmit" />
                                    <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click"
                                        ValidationGroup="1" CssClass="btnSubmit" Visible="False" />
                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                        CssClass="btnSubmit" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label18" runat="server" Text="List of Passing Grade"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnPrintVersion" runat="server" Text="Printable Version" OnClick="btnPrintVersion_Click"
                            CausesValidation="False" CssClass="btnSubmit" />
                        <input id="btnPrint" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" class="btnSubmit" />
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
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
                    </table>
                    <table width="100%">
                        <tr>
                            <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td class="style1">
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; color: Navy; font-size: 13px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" Font-Italic="True"></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label3" runat="server" Font-Italic="True" Text="List of Passing Grade "></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                                    OnRowEditing="GridView1_RowEditing" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                    OnSorting="GridView1_Sorting" PageSize="10" Width="100%" AlternatingRowStyle-CssClass="alt"
                                                    PagerStyle-CssClass="pgr" CssClass="mGrid" OnRowDeleting="GridView1_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Passing Grade Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="60%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Equivalent GPA" SortExpression="EquivallentGPA" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("EquivallentGPA") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Labesdsdl6" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                <%--<asp:CheckBox ID="CheckBox1" Enabled="false" runat="server" Checked='<%# Eval("Active") %>' />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderText="Edit" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                    ToolTip="Edit" CommandName="Edit" ImageUrl="~/Account/images/edit.gif" CausesValidation="False" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton54" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                                    CommandName="Delete" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                    ImageUrl="~/Account/images/delete.gif" CausesValidation="False" ToolTip="Delete" />
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
