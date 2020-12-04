<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="FaqCategoryMaster.aspx.cs" Inherits="ShoppingCart_Admin_FaqCategoryMaster"
    Title="FAQ Category Master" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
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

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Add a New Frequently Asked Question Category"
                            Font-Bold="True" Visible="False"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add FAQ Category" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Select Business Name">
                            </asp:Label>
                            <asp:Label ID="Label8" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                            <asp:DropDownList ID="ddwarehouse" runat="server">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label4" runat="server" Text="Frequently Asked Question Category Name ">
                                   
                            </asp:Label>
                            <asp:Label ID="lbllable" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                ErrorMessage="*" ValidationGroup="1">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ControlToValidate="TextBox1"
                                Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                ValidationGroup="1">
                            </asp:RegularExpressionValidator>
                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&amp;*()&gt;.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',30)"
                                Width="300px">
                            </asp:TextBox>
                            <asp:Label ID="Label12" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="div1" class="labelcount">30</span>
                            <asp:Label ID="lbljshg" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <asp:Button ID="ImageButton1" runat="server" CssClass="btnSubmit" OnClick="Button1_Click"
                            Text="Submit" ValidationGroup="1" />
                        <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" OnClick="Button2_Click1"
                            Text="Update" ValidationGroup="1" Visible="False" />
                        <asp:Button ID="ImageButton2" runat="server" CssClass="btnSubmit" OnClick="Button2_Click"
                            Text="Cancel" />
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="list" Text="List of Categories for Frequently Asked Questions "
                            Font-Bold="true"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button1_Click1" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" class="btnSubmit" value="Print" visible="false" />
                    </div>
                    <label>
                        <asp:Label ID="Label6" runat="server" Text="Select by Business Name "></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td colspan="2">
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; color: #000000; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" Font-Italic="true" runat="server" Visible="false"></asp:Label><br />
                                                    <asp:Label ID="Label7" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="lblCompany0" Font-Italic="true" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; color: Navy; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label5" Font-Italic="true" runat="server" ForeColor="Black" Text="List of Categories for Frequently Asked Questions"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="text-align: center;">
                                        <%-- Filter By Category :--%>
                                        <asp:DropDownList ID="ddlfaqcategory0" Visible="false" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlfaqcategory_SelectedIndexChanged" Width="138px">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <cc11:PagingGridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        DataKeyNames="FAQCategoryMaster_Id" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        GridLines="Both" AlternatingRowStyle-CssClass="alt" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                        OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                                        OnRowUpdating="GridView1_RowUpdating" OnSorting="GridView1_Sorting" Width="100%"
                                        AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" EmptyDataText="No Record Found.">
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Business Name"
                                                SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwh" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                    <asp:Label ID="lblwhid" runat="server" Visible="false" Text='<%#Bind("WareHouseId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Frequently Asked Question Category Name" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="FAQCategoryName" ItemStyle-Width="75%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfqName" runat="server" Text='<%#Bind("FAQCategoryName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("FAQCategoryMaster_Id") %>'
                                                        CommandName="Edit" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderImageUrl="~/ShoppingCart/images/trash.jpg" HeaderText="Delete"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("FAQCategoryMaster_Id") %>'
                                                        CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfqId" runat="server" Text='<%#Bind("FAQCategoryMaster_Id") %>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </cc11:PagingGridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" BorderStyle="Outset"
                                        Width="300px">
                                        <table align="center" id="Table1" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    Confirm Delete
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" ForeColor="Black">You Sure , You Want to Delete !</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:ImageButton ID="ImageButton5" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/Yes.png" />
                                                    <asp:ImageButton ID="ImageButton6" runat="server" AlternateText="cancel" ImageUrl="~/ShoppingCart/images/No.png"
                                                        OnClick="ImageButton6_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="Panel3" TargetControlID="HiddenButton222">
                                    </cc1:ModalPopupExtender>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
