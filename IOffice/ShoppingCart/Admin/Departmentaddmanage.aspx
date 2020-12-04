<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="Departmentaddmanage.aspx.cs" Inherits="Add_Department"
    Title="Add Department" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
            counter = document.getElementById(id);
            alert(counter);
            if (evt.keyCode == 13) {

                return true;
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
    <asp:UpdatePanel ID="pbbbp" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="statuslable" ForeColor="red" runat="server" Text="" Visible="False">
                    </asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="btnadd" runat="server" Text="Add Department" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <label>
                            <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                            <asp:DropDownList ID="ddlwarehouse" runat="server" ValidationGroup="1">
                            </asp:DropDownList>
                        </label>
                        <label style="width:300px;">
                            <asp:Label ID="Label2" runat="server" Text="Department Name"></asp:Label><asp:Label
                                ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdegnation"
                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                ControlToValidate="txtdegnation" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtdegnation" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'div1',30)"></asp:TextBox>
                            <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="div1" class="labelcount">30</span>
                            <asp:Label ID="Label12" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label></label>
                           
                              <label style="width:60px;">
                                    <asp:Label ID="Label16" runat="server" Text="Status"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlactivestatus" runat="server"  >
                                        <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                          
                        <div style="clear: both;">
                        </div>
                        <br />
                        <%--  <asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button2"
                        CssClass="btnSubmit" Text="Submit" />--%>
                        <asp:Button ID="ImageButton1" Text="Submit" runat="server" OnClick="ImageButton1_Click"
                            ValidationGroup="1" CssClass="btnSubmit" />
                        <asp:Button ID="btnupdate" Text="Update" runat="server" Visible="false" ValidationGroup="1"
                            CssClass="btnSubmit" OnClick="btnupdate_Click" />
                        <%--<asp:Button runat="server" ID="Button3" CssClass="btnSubmit" Text="Cancel" />--%>
                        <asp:Button ID="ImageButton8" Text="Cancel" runat="server" OnClick="ImageButton8_Click"
                            CssClass="btnSubmit" />
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label5" runat="server" Text="List of Departments"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" CssClass="btnSubmit" runat="server" Text="Printable Version"
                            OnClick="Button1_Click" />
                        <input id="Button7" class="btnSubmit" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <label>
                        <asp:Label ID="Label3" runat="server" Text="Filter by Business Name"></asp:Label>
                        <asp:DropDownList ID="filterBus" runat="server" ValidationGroup="1" AutoPostBack="True"
                            OnSelectedIndexChanged="filterBus_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                     
                                <label>
                                <asp:Label ID="Label8" runat="server" Text="Active"></asp:Label>
                                    <asp:DropDownList ID="ddlactivesearch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="filterBus_SelectedIndexChanged">
                                        <asp:ListItem Value="2" Selected="True">ALL</asp:ListItem>
                                        <asp:ListItem Value="1" >Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                    <div style="clear: both;">
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Size="20px" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label7" runat="server" Font-Size="20px" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBus" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label11" runat="server" Text="List of Departments" Font-Size="18px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AllowSorting="True" GridLines="Both"
                                        AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No Record Found." AutoGenerateColumns="False" DataKeyNames="id"
                                        Width="100%" OnRowEditing="GridView1_RowEditing" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting" 
                                        OnRowCommand="GridView1_RowCommand">
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business" ItemStyle-Width="50%" SortExpression="Whname"
                                                ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwh" runat="server" Text='<%#Bind("Whname") %>'></asp:Label>
                                                    <asp:Label ID="lblwhid" runat="server" Visible="false" Text='<%#Bind("Whid") %>'></asp:Label>
                                                </ItemTemplate>
                                                <%--<EditItemTemplate>
                                                    <asp:DropDownList ID="ddlwarehouse1" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblwhid" runat="server" Visible="false" Text='<%#Bind("Whid") %>'></asp:Label>
                                                </EditItemTemplate>--%>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department" ItemStyle-Width="40%" SortExpression="Departmentname"
                                                ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCatName" runat="server" Text='<%#Eval("Departmentname") %>'></asp:Label>
                                                    <asp:Label ID="lblCatId" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtdesignname" MaxLength="30" runat="server" Text='<%#Eval("Departmentname") %>'
                                                        Width="200px"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="REG2" runat="server" ErrorMessage="Invalid Character"
                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                        ControlToValidate="txtdesignname" ValidationGroup="2"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="rfvd" ControlToValidate="txtdesignname" runat="server"
                                                        Text="*" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                    <asp:Label ID="lblCatId" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>
                                                </EditItemTemplate>--%><HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-Width="3%" HeaderImageUrl="~/Account/images/edit.gif"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Edit" ImageUrl="~/Account/images/edit.gif"
                                                        ToolTip="Edit" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                        
                                           <%-- <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Btn" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </cc11:PagingGridView>
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <input id="DeleteStatus" style="width: 16px" type="hidden" runat="server" value="0" />
                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA" BorderStyle="Outset"
                        Width="300px">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label4" runat="server" ForeColor="Black">Do you wish to delete this record?</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="ImageButton2" BackColor="#CCCCCC" Text="Yes" runat="server" OnClick="ImageButton2_Click"
                                        Width="70px" />
                                    <asp:Button ID="ImageButton5" BackColor="#CCCCCC" Text="Cancel" runat="server" OnClick="ImageButton5_Click"
                                        Width="70px" />
                                    <%-- <asp:ImageButton ID="ImageButton2" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/Yes.png"
                                                        OnClick="ImageButton2_Click" /><asp:ImageButton ID="ImageButton5" runat="server"
                                                            AlternateText="cancel" ImageUrl="~/ShoppingCart/images/No.png" OnClick="ImageButton5_Click" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel3" TargetControlID="HiddenButton222">
                    </cc1:ModalPopupExtender>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel6" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA" BorderStyle="Outset"
                        Width="300px">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="subinnertblfc">
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="height: 14px">
                                    <asp:Label ID="lbleditmsg" runat="server" ForeColor="Black">Sorry, Edit is not 
                                                    allowed!</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 26px">
                                    <asp:Button ID="ImageButton10" BackColor="#CCCCCC" Text="Cancel" runat="server" OnClick="ImageButton10_Click"
                                        Width="70px" />
                                    <%--<asp:ImageButton ID="ImageButton10" runat="server" AlternateText="OK" ImageUrl="~/ShoppingCart/images/cancel.png"
                                            OnClick="ImageButton10_Click" />--%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="Button3" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel6" TargetControlID="Button3">
                    </cc1:ModalPopupExtender>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA" BorderStyle="Outset"
                        Width="300px">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="subinnertblfc">
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lbldeletemsg" runat="server" ForeColor="Black">You are not 
                                                    allow to DELETE this record!</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 26px">
                                    <asp:Button ID="ImageButton3" BackColor="#CCCCCC" Text="Cancel" runat="server" OnClick="ImageButton3_Click"
                                        Width="70px" />
                                    <%--<asp:ImageButton ID="ImageButton3" runat="server" AlternateText="OK" ImageUrl="~/ShoppingCart/images/cancel.png"
                                            OnClick="ImageButton3_Click" />--%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="Button2" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel5" TargetControlID="Button2">
                    </cc1:ModalPopupExtender>
                </fieldset>
            </div>
            <!--end of right content-->
            <div style="clear: both;">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
