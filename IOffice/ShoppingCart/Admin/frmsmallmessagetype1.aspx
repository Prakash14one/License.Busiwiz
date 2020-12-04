<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="frmsmallmessagetype1.aspx.cs" Inherits="ShoppingCart_Admin_frmsmallmessagetype1"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .style8
        {
            height: 22px;
        }
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
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= Panel6.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');

            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }

        function mask(evt) {

            if (evt.keyCode == 13) {

                return true;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered invalid character");
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered invalid character");
            }

            counter = document.getElementById(id);

            if (txt1.value.length <= max_len) {
                remaining_characters = max_len - txt1.value.length;
                counter.innerHTML = remaining_characters;
            }
        } 
        

    </script>

    <div style="padding-left: 2%">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div style="clear: both;">
    </div>
    <fieldset>
        <legend>
            <asp:Label ID="lbllegend" runat="server" Text=""></asp:Label>
        </legend>
        <div style="float: right;">
            <asp:Button ID="btnmsgtype" runat="server" Text="Add Message Type" OnClick="btnmsgtype_Click"
                CssClass="btnSubmit" />
        </div>
        <div style="clear: both;">
        </div>
        <asp:Panel runat="server" ID="panelforadd" Visible="false">
            <table style="width: 100%" cellspacing="3" cellpadding="2">
                <tr>
                    <td colspan="2" align="right">
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="right">
                        <label>
                            <asp:Label ID="Label1" runat="server" Text="Small Message Type"></asp:Label>
                            <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtmessagetype"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                ControlToValidate="txtmessagetype" ValidationGroup="1"></asp:RegularExpressionValidator>
                        </label>
                    </td>
                    <td style="width: 70%">
                        <label>
                            <asp:TextBox ID="txtmessagetype" runat="server" MaxLength="60" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',60)"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label runat="server" ID="sadasd" Text="Max " CssClass="labelcount"></asp:Label>
                            <span id="div1" cssclass="labelcount">60</span>
                            <asp:Label ID="lblbl" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="width: 70%">
                        <asp:Button ID="Button26" runat="server" CssClass="btnSubmit" OnClick="Button26_Click"
                            Text="Submit" ValidationGroup="1" />
                        <asp:Button ID="Button27" runat="server" CssClass="btnSubmit" Text="Update" OnClick="Button27_Click"
                            ValidationGroup="1" Visible="False" />
                        <asp:Button ID="Button28" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="Button28_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </fieldset>
    <fieldset>
        <legend>
            <asp:Label ID="Label2" runat="server" Text="List of Message Types"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right" style="text-align: right">
                    <%--<input id="Button1" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                    style="background-color: #CCCCCC; width: 51px;" type="button" value="Print" />--%>
                    <asp:Button ID="btnprintableversion" CssClass="btnSubmit" runat="server" Text="Printable Version"
                        OnClick="btnprintableversion_Click" />
                    <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                        type="button" value="Print" visible="False" class="btnSubmit" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="Panel6" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="850Px">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Italic="True" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                    <asp:Label ID="Label6" runat="server" Font-Italic="True" Text="List of Message Types"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                 <cc11:PagingGridView  ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" DataKeyNames="SmallmesstypeId"
                                        OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                                        OnRowCommand="GridView1_RowCommand" AllowSorting="True" OnSorting="GridView1_Sorting"
                                        AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        OnRowEditing="GridView1_RowEditing">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Small Message Type" SortExpression="Smallmesstype"
                                                ItemStyle-Width="90%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmessagetype123" runat="server" Text='<%#Bind("Smallmesstype") %>'></asp:Label>
                                                    <asp:Label ID="lblmessagetypeid123" runat="server" Text='<%#Bind("SmallmesstypeId") %>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="90%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit" CommandArgument='<%# Eval("SmallmesstypeId") %>'
                                                        ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif"
                                                ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Button1" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
