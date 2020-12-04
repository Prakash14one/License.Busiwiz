<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="Problemtype.aspx.cs" Inherits="Problemtype" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                &nbsp&nbsp
                <asp:Label ID="lblmasg" runat="server" Visible="False" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" Visible="false" runat="server" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" CssClass="btnSubmit" Text="Add New Customer Problem Type"
                            OnClick="btnadd_Click"></asp:Button>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="panel1" runat="server" Width="100%" Visible="false">
                       
                        <div>
                            <label>
                                <asp:Label ID="La7bel1" runat="server" Text="Customer Problem Type"></asp:Label>
                                <asp:Label ID="Label3" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtproblm"
                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtproblm"
                                    ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                    ValidationGroup="1">
                                </asp:RegularExpressionValidator>
                            </label>
                            <div style="clear: both;">
                            </div>
                            <label>
                                <asp:TextBox ID="txtproblm" onKeydown="return mask(event)" MaxLength="25" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span2',25)"
                                    runat="server" ValidationGroup="1"></asp:TextBox>
                                <asp:Label ID="Label12" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                <span id="Span2" class="labelcount">25</span>
                                <asp:Label ID="lbljshg" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                            </label>
                            <div style="clear: both;">
                            </div>
                            <asp:Button ID="btnsubmit" CssClass="btnSubmit" runat="server" Text="Submit" ValidationGroup="1"
                                OnClick="btnsubmit_Click" />
                            <asp:Button ID="btnupdate" CssClass="btnSubmit" runat="server" Text="Update" ValidationGroup="1"
                                OnClick="btnupdate_Click" Visible="False" />
                            <asp:Button ID="btncancel" CssClass="btnSubmit" runat="server" Text="Cancel" OnClick="btncancel_Click" />
                        </div>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllgn" runat="server" Text="List of Customer Problem Types" Font-Bold="true"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnprint" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            CausesValidation="False" OnClick="btnprint_Click" />
                        <input id="btnin" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblcompny" runat="server" Text="" Font-Italic="True" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label11" runat="server" Text="List of Customer Problem Types" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView2" Width="100%" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="No Record Found." AllowSorting="true" DataKeyNames="ProblemTypeId"
                                        CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        OnSorting="GridView2_Sorting" OnRowCommand="GridView2_RowCommand" OnRowDeleting="GridView2_RowDeleting"
                                        OnRowEditing="GridView2_RowEditing" OnRowUpdating="GridView2_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Customer Problem Type" SortExpression="ProblemName"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmasterId" runat="server" Text='<%#Bind("ProblemName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imagebuttonedit" runat="server" ToolTip="Edit" CommandName="Edit"
                                                        ImageUrl="~/Account/images/edit.gif" CommandArgument='<%# Eval("ProblemTypeId") %>'>
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgdelete" runat="server" ToolTip="Delete" CommandName="Delete"
                                                        ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                        CommandArgument='<%# Eval("ProblemTypeId") %>'></asp:ImageButton>
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
                    
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
