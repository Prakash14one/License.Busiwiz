<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="SubMenuMaster.aspx.cs" Inherits="SubMenuMaster" Title="Product Sub Menu-Add,Manage" %>
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


            if (evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 ) 
            {


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
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="addnewpanel" runat="server" Text="Add Sub Menu" CssClass="btnSubmit" OnClick="addnewpanel_Click" />
                          <asp:Button ID="btndosyncro" runat="server" CssClass="btnSubmit"  OnClick="btndosyncro_Clickpop" Text="Do Synchronise" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Select Product"></asp:Label>
                                         <asp:RequiredFieldValidator ID="red" runat="server" ControlToValidate="ddlWebsiteSection"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlWebsiteSection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWebsiteSection_SelectedIndexChanged" Width="500px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Main Menu Name"></asp:Label>
                                         <asp:RequiredFieldValidator ID="RequirldValidator2" runat="server" ControlToValidate="ddlmainmenu"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlmainmenu" runat="server" Width="176px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlmainmenu_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text=" Language "></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlanguage" runat="server" Width="176px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text=" Sub Menu Title "></asp:Label>
                                        <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator116" runat="server" ControlToValidate="txtsubmenu"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtsubmenu"
                                            Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9.:,\s]*)"
                                            ValidationGroup="1"> </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtsubmenu" onKeydown="return mask(event)" MaxLength="50" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\a-zA-Z0-9_.:, ]+$/,'Span10',50)"
                                            runat="server" Width="170px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="max1" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span10" cssclass="labelcount">50</span>
                                        <asp:Label ID="Label13" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ . , :)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="Sub Menu Index"></asp:Label>
                                        <asp:Label ID="Label7" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtmenuindex"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtmenuindex"
                                            Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ValidationGroup="1"> </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtmenuindex" onKeydown="return mask(event)" MaxLength="4" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span1',4)"
                                            runat="server" Width="170px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" cssclass="labelcount">4</span>
                                        <asp:Label ID="Label14" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Active"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="Button1_Click"
                                        ValidationGroup="1" />
                                    <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Update" ValidationGroup="1"
                                        Visible="false" OnClick="Button3_Click" />
                                    <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="Button2_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>--%>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label9" runat="server" Text="List Of SubMenus"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnprint" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            CausesValidation="False" OnClick="btnprint_Click" />
                        <input id="btnin" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label10" runat="server" Text="Product"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="FilterProductname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FilterProduct_SelectedIndexChanged"
                                        Width="700px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label11" runat="server" Text=" Filter by  Main Menu"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="FilterMenu" runat="server" Width="220px" AutoPostBack="true"
                                        OnSelectedIndexChanged="FilterMenu_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label16" runat="server" Text=" Status"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlstatus" runat="server"  AutoPostBack="true"
                                    onselectedindexchanged="ddlstatus_SelectedIndexChanged">
                                        <asp:ListItem Value="0">All</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label12" runat="server" Text="List Of SubMenus" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" OnRowDeleting="GridView1_RowDeleting"
                                        DataKeyNames="SubMenuId" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                                        OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand" AllowSorting="True"
                                        OnSorting="GridView1_Sorting" EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Main Menu Name" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="MainMenuTitle">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmainmenuId" runat="server" Text='<%#Bind("MainMenuId") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblmainmenuName" runat="server" Text='<%#Bind("MainMenuTitle") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Main Menu Index" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="MainMenuIndex">
                                                <ItemTemplate>
                                                    
                                                    <asp:Label ID="lblmainmenuindex" runat="server" Text='<%#Bind("MainMenuIndex") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Language" HeaderStyle-HorizontalAlign="Left" SortExpression="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLanguage" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                              
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Menu Name" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="SubMenuName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsubmenuname" runat="server" Text='<%#Bind("SubMenuName") %>'></asp:Label>
                                                    <asp:Label ID="lblsubmenumasterId" runat="server" Text='<%#Bind("SubMenuId") %>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Menu Index" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="SubMenuIndex">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsubmenunameindex123" runat="server" Text='<%#Bind("SubMenuIndex") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Left" SortExpression="SubmenuActive">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkgrid123" runat="server" Checked='<%#Bind("SubmenuActive") %>'
                                                        Enabled="false" />
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                         
                                         
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" ToolTip="Edit" runat="server" CommandName="Edit"
                                                        ImageUrl="~/Account/images/edit.gif" CommandArgument='<%# Eval("SubMenuId") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgdelete" runat="server" ToolTip="Delete" CommandName="Delete"
                                                        ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                        CommandArgument='<%# Eval("SubMenuId") %>'></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            </tr>
                            <tr>
                            <td>
                              <asp:Panel ID="Paneldoc" runat="server" Width="55%" CssClass="modalPopup">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="Label142" runat="server" Text=""></asp:Label>
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 95%;">
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <label>
                                                        <asp:Label ID="Label143" runat="server" Text="Was this the last record you are going to add right now to this table?"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdsync" runat="server">
                                                                    <asp:ListItem Value="1" Text="Yes, this is the last record in the series of records I am inserting/editing to this table right now"></asp:ListItem>
                                                                    <asp:ListItem Value="0" Text="No, I am still going to add/edit records to this table right now"
                                                                        Selected="True"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="btnok" runat="server" CssClass="btnSubmit" Text="OK" OnClick="btndosyncro_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Paneldoc" TargetControlID="btnreh" CancelControlID="ImageButton10">
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
