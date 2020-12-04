<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="PageVersionForm.aspx.cs" Inherits="SubMenuMaster" Title="Untitled Page" %>

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
            <div style="margin-left:1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladdlabel" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="addnewpanel" runat="server" Text="Add Page Version" CssClass="btnSubmit"
                            OnClick="addnewpanel_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                        <table width="100%">
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Product Name"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator117" runat="server" ControlToValidate="ddlWebsite"
                                            ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                        <asp:DropDownList ID="ddlWebsite" Width="500px" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlWebsite_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Main Menu"></asp:Label>
                                        
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                        <asp:DropDownList ID="ddlMainMenu" runat="server" Width="220px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="Sub Menu"></asp:Label>
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                        <asp:DropDownList ID="ddlSubmenu" runat="server" Width="220px" AutoPostBack="True"
                                            colspan="3" OnSelectedIndexChanged="ddlSubmenu_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="Page"></asp:Label>
                                        <asp:Label ID="Label7" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlPage"
                                            ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                        <asp:DropDownList ID="ddlPage" runat="server" Width="500px" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:LinkButton ID="LinkButton3" runat="server" BackColor="#99CCFF" OnClick="LinkButton1_Click">Add New Page</asp:LinkButton>
                                    </label>
                                    <label>
                                        <asp:LinkButton ID="LinkButton1" runat="server" BackColor="#99CCFF" OnClick="LinkButton1_Click1">Refresh</asp:LinkButton>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Page Name"></asp:Label>
                                        <asp:Label ID="Label12" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPageName"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                        <asp:TextBox ID="txtPageName" runat="server" Width="500px" Enabled="False"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label10" runat="server" Text="e.g.(Upload Original&amp;Version page for work upload page.)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label11" runat="server" Text="Version No"></asp:Label>
                                        <asp:Label ID="Label21" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtVersionNo"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                        <asp:TextBox ID="txtVersionNo" runat="server" Enabled="False"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Version Name"></asp:Label>
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                        <asp:TextBox ID="txtVersionName" runat="server"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Active"></asp:Label>
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label17" runat="server" Text="On clicking submit, do you wish to add task related to this page version ? Yes"></asp:Label>
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                         <asp:CheckBox ID="chkonsubmit" runat="server" Checked="true" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    &nbsp;
                                </td>
                                <td align="left" width="70%">
                                    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="Button1_Click"
                                        ValidationGroup="1" />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnSubmit" ValidationGroup="1"
                                        Visible="False" OnClick="btnUpdate_Click" />
                                    <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" CssClass="btnSubmit" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="List of Page Versions"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            CausesValidation="False" OnClick="Button4_Click" />
                        <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <table width="100%">
                        <tr>
                                <td>
                                    <label>
                                        Show All Records (Including Inactive)
                                    </label>
                                    
                                </td>
                                <td>
                                    <label>
                                    <asp:CheckBox ID="CHKSHOWALL" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="CHKSHOWALL_CheckedChanged" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text=" Product Name"></asp:Label>
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                        <asp:DropDownList ID="FilterProduct" runat="server" AutoPostBack="True" Width="500px"
                                            OnSelectedIndexChanged="FilterProduct_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label18" runat="server" Text="Main Menu"></asp:Label>
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                        <asp:DropDownList ID="FilterMenu" runat="server" AutoPostBack="true" CausesValidation="True"
                                            OnSelectedIndexChanged="FilterMenu_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label19" runat="server" Text="Sub Menu"></asp:Label>
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                        <asp:DropDownList ID="FilterSubMenu" runat="server" CausesValidation="True" 
                                        AutoPostBack="True" onselectedindexchanged="FilterSubMenu_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                             <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text=" Page Name"></asp:Label>
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                          <asp:DropDownList ID="ddlfilterpage" runat="server" Width="500px" AutoPostBack="True" >
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label20" runat="server" Text=" Active"></asp:Label>
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                        <asp:DropDownList ID="ddlAct" runat="server" CausesValidation="True">
                                            <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="True" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="False" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Show Uncertified Pages
                                    </label>
                                   
                                </td>
                                <td>
                                    <label>
                                         <asp:CheckBox ID="CHKUNALLPAGE" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="CHKUNALLPAGE_CheckedChanged"/>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                </td>
                                <td width="70%">
                                    <asp:Button ID="BtnGo" CssClass="btnSubmit" runat="server" Text="Go" OnClick="BtnGo_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr align="center">
                                                <td>
                                                    <div id="mydiv" class="closed">
                                                        <table width="100%">
                                                            <tr align="center">
                                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                    <asp:Label ID="Label9" runat="server" Text="List of Page Versions" Font-Italic="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="None" Width="100%">
                                                        <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" EmptyDataText="No Record Found."
                                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" Width="100%" 
                                                            CellPadding="0" OnRowDeleting="GridView1_RowDeleting" DataKeyNames="Id" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                                            OnRowCommand="GridView1_RowCommand" AllowSorting="True" OnSorting="GridView1_Sorting">
                                                            <Columns>
                                                                <asp:BoundField DataField="MasterPage_Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Section_MasterPage"
                                                                    SortExpression="MasterPage_Name" Visible="false" />
                                                                    
                                                                <asp:BoundField DataField="MainMenuName" HeaderStyle-HorizontalAlign="Left" HeaderText="Main_Menu"
                                                                    SortExpression="MainMenuName" ItemStyle-Width="15%" />
                                                                <asp:BoundField DataField="SubMenuName" HeaderStyle-HorizontalAlign="Left" HeaderText="Sub_Menu"
                                                                    SortExpression="SubMenuName" ItemStyle-Width="15%"  />
                                                                <asp:TemplateField HeaderText="Page_Title" HeaderStyle-HorizontalAlign="Left" SortExpression="PageTitle" ItemStyle-Width="25%" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPageName" runat="server" Text='<%#Bind("PageTitle") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" SortExpression="Date" ItemStyle-Width="5%" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDate" runat="server" HeaderStyle-HorizontalAlign="Left" Text='<%#Bind("Date","{0:MM/dd/yyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Page Version" HeaderStyle-HorizontalAlign="Left" SortExpression="PageName" ItemStyle-Width="25%" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVersionName" runat="server" Text='<%#Bind("PageName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Version_No" HeaderStyle-HorizontalAlign="Left" SortExpression="VersionNo" ItemStyle-Width="5%" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVersionNo" runat="server" Text='<%#Bind("VersionNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Folder_Name" HeaderStyle-HorizontalAlign="Left" SortExpression="FolderName" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFolderName" runat="server" Text='<%#Bind("FolderName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Left" SortExpression="Active" ItemStyle-Width="5%" >
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkActive" runat="server" Checked='<%#Bind("Active") %>' Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:ButtonField CommandName="editview" HeaderImageUrl="~/Account/images/edit.gif"
                                                                    ButtonType="Image" ImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderText="Edit" />
                                                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgdelete" ToolTip="Delete" runat="server" CommandName="Delete"
                                                                            ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                            CommandArgument='<%# Eval("Id") %>'></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                            <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                        </table>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
