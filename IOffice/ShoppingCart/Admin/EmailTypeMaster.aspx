<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="EmailTypeMaster.aspx.cs" Inherits="ShoppingCart_Admin_EmailTypeMaster"
    Title="Untitled Page" %>

<%--<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>--%>
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
            <div style="padding-left: 1%">
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="title" Text="" Visible="false"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" CssClass="btnSubmit" Text="Add Email type"
                            OnClick="btnadd_Click"></asp:Button>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="addemail" Visible="false" Width="100%" runat="server">
                        <table width="100%">
                            <tr>
                                <td style="width: 40%">
                                    <label>
                                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                        <asp:Label ID="lblbname" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 60%">
                                    <label>
                                        <asp:DropDownList ID="DropDownList1" runat="server" ValidationGroup="1">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label runat="server" ID="lblemailtype" Text="Pre-Formatted Email Name"></asp:Label>
                                        <asp:Label runat="server" ID="Label4" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Textname"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            SetFocusOnError="True" ValidationExpression="^([+@a-zA-Z0-9_\s\-\.]*)" ControlToValidate="Textname"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="Textname" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%^'&*()>:;={}[]|\/]/g,/^[\@+a-zA-Z0-9_ \s\-\.]+$/,'div1',50)"
                                            Width="350px" MaxLength="50"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" class="labelcount">50</span>
                                        <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 _ @ + . -)" CssClass="labelcount"></asp:Label></label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <label>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Draft email format after submitting this record"
                                            Checked="True" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="left">
                                    <asp:Button ID="ImageButton1" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="Button1_Click1"
                                        ValidationGroup="1" />
                                    <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btnSubmit" Visible="false"
                                        ValidationGroup="1" OnClick="btnupdate_Click" />
                                    <asp:Button ID="ImageButton3" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="ImageButton3_Click"
                                        CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Pre-Formatted Email Names" Font-Bold="True"
                            runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            OnClick="Button1_Click" CausesValidation="False" />
                        <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label17" runat="server" Text="Filter by Business"></asp:Label>
                        <asp:DropDownList ID="ddshorting" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddshorting_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
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
                                                    <asp:Label ID="lblCompany" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label runat="server" ID="name" Font-Italic="true" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label7" runat="server" Font-Italic="True" Text="List of Pre-Formatted Email Names"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="EmailTypeId"
                                        AllowSorting="True" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating"
                                        OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                        OnSorting="GridView1_Sorting" CssClass="mGrid" PagerStyle-CssClass="pgr" EmptyDataText="No Record Found."
                                        AlternatingRowStyle-CssClass="alt">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Left" SortExpression="Whname"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwh" runat="server" Text='<%#Bind("Whname") %>'></asp:Label>
                                                    <asp:Label ID="lblwhid" runat="server" Visible="false" Text='<%#Bind("Whid") %>'></asp:Label>
                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pre-Formatted Email Name" ItemStyle-HorizontalAlign="Left" SortExpression="ename" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="55%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblename" runat="server" Text='<%# Bind("ename") %>'></asp:Label>
                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EmailTypeId" InsertVisible="False" SortExpression="EmailTypeId"
                                                Visible="False" HeaderStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("EmailTypeId") %>'></asp:Label>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View/Manage Format" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton48" runat="server" ToolTip="View/Manage Format" CommandArgument='<%# Eval("EmailTypeId") %>'
                                                        CommandName="viewandmanage" ImageUrl="~/Account/images/edit.gif" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imagebuttonedit" runat="server" CommandName="Edit" ImageUrl="~/Account/images/edit.gif"
                                                        CommandArgument='<%# Eval("EmailTypeId") %>' ToolTip="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%" >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Button1" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" BorderStyle="Outset"
                        Width="300px">
                        <table cellpadding="0" cellspacing="0" style="width: 290px">
                            <tr>
                                <td>
                                    Confirm Delete
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label5" runat="server" ForeColor="Black">You Sure , You Want to 
                                    Delete !</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="ImageButton2" runat="server" AlternateText="submit" Text="Yes" OnClick="ImageButton2_Click" />
                                    <asp:Button ID="ImageButton5" runat="server" Text="No" AlternateText="cancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel3" TargetControlID="HiddenButton222">
                    </cc1:ModalPopupExtender>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
