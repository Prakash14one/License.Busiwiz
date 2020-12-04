<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="AddressTypeMaster.aspx.cs" Inherits="ShoppingCart_Admin_AddressTypeMaster"
    Title="Untitled Page" %>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%;">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Label"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" CssClass="btnSubmit" runat="server" Text="Add New Address Type"
                            OnClick="btnadd_Click" />
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Address Type"></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Textname"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                ControlToValidate="Textname" ValidationGroup="1"></asp:RegularExpressionValidator>
                                 <asp:TextBox ID="Textname" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:_;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'div1',20)"
                                Width="145px"></asp:TextBox>
                                <asp:Label ID="Label60"  CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                             <span id="div1" class="labelcount">20</span>
                            <asp:Label ID="lblbf" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                        </label>
                        <label>
                           
                        </label>
                        <label>
                            
                        </label>
                        <div style="clear: both;">
                        </div>
                        <br />
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="ImageButton8_Click"
                            ValidationGroup="1" />
                        <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btnSubmit" ValidationGroup="1" Visible="false"
                            OnClick="btnupdate_Click" />
                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="ImageButton7_Click" />
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label4" runat="server" Text="List of Address Types"></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="Button4" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button4_Click" />
                        <input id="Button1" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" type="button" value="Print" visible="False" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: black; text-align:center; font-style:italic; font-weight: bold;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCompany" runat="server" Font-Size="20px" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>                                                    
                                                    <asp:Label ID="List" runat="server" Font-Size="18px">List of Address Types</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="AddressTypeMasterId"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        AllowSorting="True" Width="100%" OnRowDeleting="GridView1_RowDeleting1" AllowPaging="true"
                                        PageSize="50" OnRowEditing="GridView1_RowEditing" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        EmptyDataText="No Record Found." OnSorting="GridView1_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="AddressTypeMasterId" HeaderText="AddressTypeMasterId"
                                                InsertVisible="False" ReadOnly="True" SortExpression="AddressTypeMasterId" Visible="False" />
                                            <asp:TemplateField HeaderText="Address Type" SortExpression="Name" ItemStyle-Width="92%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="4%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnedit" runat="server" CommandName="Edit" ImageUrl="~/Account/images/edit.gif"
                                                        ToolTip="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Left"
                                                HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" CausesValidation="false" ToolTip="Delete"
                                                        ImageUrl="~/Account/images/delete.gif" ItemStyle-Width="4%" OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
