<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="CompanyLogoSelection.aspx.cs" Inherits="BusinessCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
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
    <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadddd" runat="server" Text="Add New Company Logo to Display on Homepage"
                            OnClick="btnadddd_Click" Width="180px" CssClass="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel runat="server" ID="pnladdd" Visible="false">
                        <table width="100%">
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label2s" runat="server" Text="Country"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="ddlcountry1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcountry1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="State"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="ddlstate1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstate1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label2d" runat="server" Text="City"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="ddlcity1" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        Search
                                    </label>
                                    <label>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Button ID="Button3" runat="server" Text="Go" CssClass="btnSubmit" OnClick="Button3_Click" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel runat="server" Height="300px" ScrollBars="Vertical" ID="panellogo">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                            AutoGenerateColumns="False" AlternatingRowStyle-CssClass="alt" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Logo" HeaderStyle-Width="7%" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image100" runat="server" Height="60px" ImageUrl='<%# Eval("logourl") %>'
                                                            Width="80px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="45%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcomname" runat="server" Text='<%# Eval("Sitename")%>'></asp:Label>
                                                        <asp:Label ID="lblcomid" Visible="false" runat="server" Text='<%# Eval("CompanyId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="25%" HeaderText="Location">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label47111" runat="server" Text='<%# Eval("CityName")%>'></asp:Label>
                                                        ,
                                                        <asp:Label ID="Label34" runat="server" Text='<%# Eval("StateName")%>'></asp:Label>
                                                        ,
                                                        <asp:Label ID="Label35" runat="server" Text='<%# Eval("CountryName")%>'></asp:Label>
                                                        <asp:Label ID="lblcountryid" Visible="false" runat="server" Text='<%# Eval("CountryId")%>'></asp:Label>
                                                        <asp:Label ID="lblstateid" Visible="false" runat="server" Text='<%# Eval("StateId")%>'></asp:Label>
                                                        <asp:Label ID="lblcityid" Visible="false" runat="server" Text='<%# Eval("CityId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Display Location" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="DropDownList1" runat="server" Width="250px">
                                                            <asp:ListItem Selected="True" Text="-Select-" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Show Everywhere" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Display in entire Business country" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Display in entire Business state" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Display in entire Business city" Value="4"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="Button4" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="Button4_Click" />
                                    <asp:Button ID="Button6" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="Button6_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label4" runat="server" Text="List of Company Logos to Display on Homepage"></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            OnClick="Button1_Click1" />
                        <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" />
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="850Px">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Company Logos to Display on Homepage"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="Gridview2" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    EmptyDataText="No Record Found." GridLines="Both" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                    AlternatingRowStyle-CssClass="alt" OnRowCommand="Gridview2_RowCommand" OnRowDeleting="Gridview2_RowDeleting"
                                                    OnRowEditing="Gridview2_RowEditing" OnRowCancelingEdit="Gridview2_RowCancelingEdit"
                                                    OnRowUpdating="Gridview2_RowUpdating">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Logo" HeaderStyle-Width="7%" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Image ID="Image1001" runat="server" Height="60px" ImageUrl='<%# Eval("logourl") %>'
                                                                    Width="80px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Company Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="45%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcomname1" runat="server" Text='<%# Eval("Sitename")%>'></asp:Label>
                                                                <asp:Label ID="lblcomid1" Visible="false" runat="server" Text='<%# Eval("CompanyId")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="25%" HeaderText="Location">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label471111" runat="server" Text='<%# Eval("CityName")%>'></asp:Label>
                                                                ,
                                                                <asp:Label ID="Label341" runat="server" Text='<%# Eval("StateName")%>'></asp:Label>
                                                                ,
                                                                <asp:Label ID="Label351" runat="server" Text='<%# Eval("CountryName")%>'></asp:Label>
                                                                <asp:Label ID="lblcountryid1" Visible="false" runat="server" Text='<%# Eval("Country1")%>'></asp:Label>
                                                                <asp:Label ID="lblstateid1" Visible="false" runat="server" Text='<%# Eval("State1")%>'></asp:Label>
                                                                <asp:Label ID="lblcityid1" Visible="false" runat="server" Text='<%# Eval("City1")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Display Location" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbllocation" runat="server" Text='<%# Eval("location")%>'></asp:Label>
                                                                <asp:Label ID="lbllogevry" runat="server" Text='<%# Eval("EverywhereDisplay")%>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lbllogcoun" runat="server" Text='<%# Eval("CountryID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbllogstat" runat="server" Text='<%# Eval("StateID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbllogcity" runat="server" Text='<%# Eval("CityID")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="DropDownList11" runat="server" Width="250px">
                                                                    <asp:ListItem Selected="True" Text="-Select-" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Show Everywhere" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Display in entire Business country" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Display in entire Business state" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="Display in entire Business city" Value="4"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                    ForeColor="Red" ValidationGroup="1" ControlToValidate="DropDownList11" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowEditButton="True" HeaderText="Edit" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" ButtonType="Image" HeaderImageUrl="~/Account/images/edit.gif"
                                                            ValidationGroup="1" EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/UpdateGrid.JPG"
                                                            CancelImageUrl="~/images/delete.gif"></asp:CommandField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                    ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                                    OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
