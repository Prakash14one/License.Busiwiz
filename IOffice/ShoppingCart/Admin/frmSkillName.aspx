<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="frmSkillName.aspx.cs" Inherits="ShoppingCart_Admin_Default"
    Title="Untitled Page" %>

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
            height: 33px;
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
                <asp:Label ID="lblmessage" runat="server" Visible="False" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllegend" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Add Skill Name" CssClass="btnSubmit"
                            OnClick="Button1_Click" />
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
                                        <asp:DropDownList ID="ddlWarehouse" runat="server" Width="250px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        Skill Type
                                        <asp:Label ID="labdsdsd" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddSkillType"
                                            ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddSkillType" runat="server" Width="250px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        Skill Name
                                        <asp:Label ID="Label34" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtName"
                                            SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                            ErrorMessage="Invalid Character" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                            ControlToValidate="txtName" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtName" runat="server" Width="250px" MaxLength="60" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div1',60)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label13" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="div1" cssclass="labelcount">60</span>
                                        <asp:Label ID="Label15" runat="server" Text="(A-Z 0-9 . , ? _)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="Label9" runat="server" Text="Label" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" Text="Submit"
                                        CssClass="btnSubmit" ValidationGroup="1" />
                                    <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click"
                                        CssClass="btnSubmit" Visible="False" ValidationGroup="1" />
                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                        CssClass="btnSubmit" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label18" runat="server" Text="List of Skill Name"></asp:Label>
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
                                    Business Name
                                    <asp:DropDownList ID="ddBusiness" runat="server" Width="230px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddBusiness_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    Skill Type
                                    <asp:DropDownList ID="ddSkillTypeFilter" runat="server" Width="230px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddSkillTypeFilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            <td align="center">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="lblCompany" runat="server" Font-Italic="True" Text="Business : "></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label3" runat="server" Text="List of Skill Name" Font-Italic="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                                    OnRowEditing="GridView1_RowEditing" AllowSorting="True" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                    OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting" EmptyDataText="No Record Found"
                                                    lternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" CssClass="mGrid"
                                                    AllowPaging="True">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business Name" SortExpression="BusinessName" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LabelBusiness" runat="server" Text='<%# Eval("BusinessName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Skill Type" SortExpression="SkillType" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="30%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("SkillType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Skill Name" SortExpression="SkillName" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="40%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("SkillName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="40%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                    ToolTip="Edit" CommandName="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton49" runat="server" OnClientClick="return confirm ('Do you wish delete this Record ?');"
                                                                    ToolTip="Delete" CommandArgument='<%# Eval("ID") %>' CommandName="Delete" ImageUrl="~/Account/images/delete.gif" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
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
