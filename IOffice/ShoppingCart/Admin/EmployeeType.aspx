<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="EmployeeType.aspx.cs" Inherits="ShoppingCart_Admin_EmployeeType" %>

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
            counter = document.getElementById(id);
            alert(counter);
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
        .style1
        {
            height: 28px;
        }
    </style>
    <div class="products_box">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="padding-left: 12px">
                    <asp:Label ID="Label1" runat="server" BorderColor="Red" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" Text="Add New Employee Type" runat="server" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <asp:Panel ID="pnladd" Visible="false" runat="server">
                        <label>
                            <asp:Label ID="Label2" runat="server" Text="Employee Type Name"></asp:Label>
                            <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbEmployeeTypeName"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                ControlToValidate="tbEmployeeTypeName" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="tbEmployeeTypeName" runat="server" Width="145px" MaxLength="30"
                                onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&amp;*()&gt;+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'div1',30)"></asp:TextBox>
                            <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="div1" class="labelcount">30</span>
                            <asp:Label ID="Label9" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <br />
                        <asp:Button ID="ImageButton1" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="Button1_Click"
                            ValidationGroup="1" />
                        <asp:Button ID="btnupdate" runat="server" CssClass="btnSubmit" Text="Update" Visible="false"
                            ValidationGroup="1" OnClick="btnupdate_Click" />
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                        <asp:Button ID="ImageButton2" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="Button2_Click" />
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label3" runat="server" Text="List of Employee Types"></asp:Label></legend>
                    <label>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="145px" AutoPostBack="True"
                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Visible="False">
                        </asp:DropDownList>
                    </label>
                    <div style="float: right;">
                        <asp:Button ID="Button4" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button4_Click" />
                        <input id="Button1" class="btnSubmit" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table id="GridTbl" width="100%">
                            <tr align="center">
                                <td class="style1">
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblcomname" runat="server" Font-Size="20px" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblhead" runat="server" Font-Size="18px" Text="List of Employee Types"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" DataKeyNames="EmployeeTypeId" GridLines="Both" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" OnSorting="GridView1_Sorting"
                                        Width="100%" EmptyDataText="No Record Found." OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                        OnRowEditing="GridView1_RowEditing">
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Employee Types" SortExpression="EmployeeTypeName"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployeeTypeName" runat="server" Text='<%#Bind("EmployeeTypeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-Width="3%" HeaderImageUrl="~/Account/images/edit.gif"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Edit" ImageUrl="~/Account/images/edit.gif"
                                                        ToolTip="Edit" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%"
                                                HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Delete" CausesValidation="false"
                                                        ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                        ToolTip="Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </cc11:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel3" runat="server" BackColor="#C0C0FF" BorderColor="#C0C0FF" BorderStyle="Outset"
                        Width="300px">
                        <table id="Table1" cellpadding="0" cellspacing="0" width="100%" bgcolor="#CCCCCC">
                            <tr>
                                <td class="subinnertblfc">
                                    Confirm Delete
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label4" runat="server" ForeColor="Black">You Sure , You Want to 
                                    Delete !</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:ImageButton ID="ImageButton5" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/Yes.png"
                                        OnClick="yes_Click" />
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
                    <asp:Panel ID="Panel6" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" BorderStyle="Outset"
                        Width="300px">
                        <table cellpadding="0" cellspacing="0" width="100%" bgcolor="#CCCCCC">
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="height: 14px">
                                    <asp:Label ID="lbleditmsg" runat="server" ForeColor="Black">
                                                        Sorry, you can not edit this record
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 26px">
                                    <asp:Button ID="ImageB10" runat="server" Text="Cancel" OnClick="ImageB10_Click" />
                                    <%--<asp:ImageButton ID="ImageB10" runat="server" AlternateText="OK" ImageUrl="~/ShoppingCart/images/cancel.png"
                                                        OnClick="ImageB10_Click" />--%>
                                </td>
                            </tr>
                        </table>
                        &nbsp;</asp:Panel>
                    <asp:Button ID="Button3" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                        ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel6" TargetControlID="Button3">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" BorderStyle="Outset"
                        Width="300px">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lbldeletemsg" runat="server" ForeColor="Black">
                                                    Sorry, you can not delete this record</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 26px">
                                    <asp:Button ID="Ima3" runat="server" Text="Cancel" OnClick="Ima3_Click" />
                                    <%--<asp:ImageButton ID="Ima3" runat="server" AlternateText="OK" ImageUrl="~/ShoppingCart/images/cancel.png"
                                                        OnClick="Ima3_Click" />--%>
                                </td>
                            </tr>
                        </table>
                        &nbsp;</asp:Panel>
                    <asp:Button ID="Button2" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                        ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel5" TargetControlID="Button2">
                    </cc1:ModalPopupExtender>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--end of right content-->
    <div style="clear: both;">
    </div>
</asp:Content>
