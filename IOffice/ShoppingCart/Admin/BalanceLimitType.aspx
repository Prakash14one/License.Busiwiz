<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="BalanceLimitType.aspx.cs" Inherits="Add_Department"
    Title="Add Department" %>

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

    <asp:UpdatePanel ID="pnnn1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                &nbsp;
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text="" Visible="False"></asp:Label>
                <fieldset>
                    <label>
                        <asp:Label ID="lblBalanceLimitType" Text="Balance Limit Type" runat="server"></asp:Label>
                        <asp:RequiredFieldValidator ID="fff" runat="server" ErrorMessage="*" SetFocusOnError="true"
                            ValidationGroup="1" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                            ControlToValidate="TextBox1" ValidationGroup="1">
                        </asp:RegularExpressionValidator>
                        <asp:TextBox ID="TextBox1" runat="server" MaxLength="20" onKeydown="return mask(event)"
                            onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',20)">
                        </asp:TextBox>
                    </label>
                    <label>
                        <br />
                        <asp:Label ID="Label12" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                        <span id="div1" class="labelcount">20</span>
                        <asp:Label ID="lbljshg" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Button ID="ImageButton3" Text="Submit" CssClass="btnSubmit" runat="server" OnClick="ImageButton3_Click"
                        ValidationGroup="1" />
                    <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" Visible="false" Text="Update"
                        OnClick="Button4_Click" />
                    <asp:Button ID="ImageButton4" Text="Cancel" CssClass="btnSubmit" runat="server" OnClick="ImageButton4_Click" />
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend><b>
                        <asp:Label ID="lblBalanceType" Text="List of Balance Limit Type" runat="server"></asp:Label></b></legend>
                    <label>
                        <asp:Label ID="lblSelectbyBalanceType" runat="server" Text="Select by Balance Type"></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="205px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </label>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="Button1" CssClass="btnSubmit" runat="server" Text="Printable Version"
                                OnClick="Button1_Click" />
                            <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                class="btnSubmit" type="button" value="Print" visible="false" />
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td colspan="2">
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" colspan="2" style="color: Black; text-align: center; font-size: 20px;
                                                    font-weight: bold;">
                                                    <asp:Label ID="lblcomname" Visible="false" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="lblhead" runat="server" Text="List of Balance Limit Type" ForeColor="Black"
                                                        Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="lblfinal" Text="Balance Type :" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="Label3" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <asp:GridView ID="GridView1" runat="server" PageSize="20" AutoGenerateColumns="False"
                                        Width="100%" EmptyDataText="No Record Found." GridLines="None" AllowPaging="true"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                                        OnRowUpdating="GridView1_RowUpdating" OnSorting="GridView1_Sorting" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                        AllowSorting="True" DataKeyNames="BalanceLimitTypeId">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Balance Limit Type" SortExpression="BalanceLimitType"
                                                ItemStyle-Width="90%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("BalanceLimitType") %>'></asp:Label>
                                                    <asp:Label ID="l1" runat="server" Text='<%#Eval("BalanceLimitTypeId") %>' Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("BalanceLimitTypeId") %>'
                                                        CommandName="Edit1" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="3%" HeaderImageUrl="~/ShoppingCart/images/trash.jpg" Visible="true">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete1" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" CommandArgument='<%#Eval("BalanceLimitTypeId") %>'
                                                        ToolTip="Delete"></asp:ImageButton>
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
            <div style="clear: both;">
            </div>
            <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Outset"
                Width="300px">
                <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" ForeColor="Black">You Sure , You want to Delete this Record?</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="ImageButton5" BackColor="#CCCCCC" Text="Submit" runat="server" OnClick="yes_Click" />
                            <asp:Button ID="ImageButton6" BackColor="#CCCCCC" Text="Cancel" runat="server" OnClick="ImageButton6_Click" />
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
                            <asp:Label ID="lbleditmsg" runat="server" ForeColor="Black">
                        Sorry, you can not edit this record
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="height: 26px">
                            <asp:Button ID="ImageButton10" BackColor="#CCCCCC" Text="Submit" runat="server" OnClick="ImageButton10_Click" />
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
                            <asp:Label ID="lbldeletemsg" runat="server" ForeColor="Black">
                        Sorry, you can not delete this record
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="height: 26px">
                            <asp:Button ID="ImageButton11" BackColor="#CCCCCC" Text="Submit" runat="server" OnClick="ImageButton11_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="Button2" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel5" TargetControlID="Button2">
            </cc1:ModalPopupExtender>
            <div style="clear: both;">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
