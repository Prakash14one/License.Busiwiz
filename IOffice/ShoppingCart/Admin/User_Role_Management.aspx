<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="User_Role_Management.aspx.cs" Inherits="Add_User_Role_Management" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
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
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Assign Roles to Users" Font-Bold="True"
                            Visible="False"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Assign Role" CssClass="btnSubmit" OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <label>
                            <asp:Label ID="Label4" Text="Business Name" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlbusinessname" runat="server" OnSelectedIndexChanged="ddlbusinessname_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="lblUserName12" Text="User Type : User Name" runat="server"></asp:Label>
                            <asp:DropDownList ID="dpdUserName" runat="server" OnSelectedIndexChanged="dpdUserName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <br />
                            <asp:ImageButton ID="imgadd" runat="server" Height="20px" ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                ToolTip="Add New" Width="20px" OnClick="imgadd_Click" />
                        </label>
                        <label>
                            <br />
                            <asp:ImageButton ID="imgref" runat="server" Height="20px" ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                ToolTip="Refresh" Width="20px" OnClick="imgref_Click" />
                        </label>
                        <label>
                            <asp:Label ID="lblRoleName" Text="Role Name" runat="server"></asp:Label>
                            <asp:DropDownList ID="dpdRoleName" runat="server">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <br />
                            <asp:ImageButton ID="imgaddrem" runat="server" Height="20px" ImageAlign="Bottom"
                                ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" ToolTip="Add New" Width="20px"
                                OnClick="imgaddrem_Click" />
                        </label>
                        <label>
                            <br />
                            <asp:ImageButton ID="imgrefrem" runat="server" Height="20px" ImageAlign="Bottom"
                                ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                OnClick="imgrefrem_Click" />
                        </label>
                        <label>
                            <asp:Label ID="lblStatus" Text="Status" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlstatus" runat="server" Width="100px">
                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblid" runat="server" Visible="False"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <div style="clear: both;">
                        </div>
                        <div>
                            <br />
                            <asp:Button ID="btSubmit" runat="server" OnClick="btSubmit_Click" CssClass="btnSubmit"
                                Text="Submit" />
                            <asp:Button ID="btnupdate" runat="server" CssClass="btnSubmit" Text="Update" OnClick="btnupdate_Click"
                                Visible="False" />
                            <asp:Button ID="btCancel" runat="server" OnClick="btCancel_Click" CssClass="btnSubmit"
                                Text="Cancel" />
                        </div>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblListUserRole" Text="List of User and Role" Font-Bold="true" runat="server"></asp:Label></legend>
                    <div style="float: right;">
                        <asp:Button ID="Button8" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button8_Click" />
                        <input id="Button2" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" type="button" value="Print" visible="False" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label8" runat="server" Text="Filter by Business"></asp:Label>
                        <asp:DropDownList ID="ddlfilterbybusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbybusiness_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label9" runat="server" Text="Filter by User Type"></asp:Label>
                        <asp:DropDownList ID="ddlfilterbyusertype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbyusertype_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label10" runat="server" Text="Filter by User Name"></asp:Label>
                        <asp:DropDownList ID="ddlfilterbyusername" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbyusername_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label11" runat="server" Text="Filter by Role"></asp:Label>
                        <asp:DropDownList ID="ddlfilterbyrole" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbyrole_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label12" runat="server" Text="Filter by Status"></asp:Label>
                        <asp:DropDownList ID="ddlfilterbystatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbystatus_SelectedIndexChanged"
                            Width="100px">
                            <asp:ListItem Text="All" Selected="True" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label13" runat="server" Text="Search"></asp:Label>
                        <asp:RegularExpressionValidator ID="REG1123" runat="server" ErrorMessage="Invalid Character"
                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                            ControlToValidate="txtsearchbyusername"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtsearchbyusername" runat="server" AutoPostBack="True" MaxLength="20"
                            OnTextChanged="txtsearchbyusername_TextChanged" onKeydown="return mask(event)"
                            onkeyup="return check(this,/[\\/!@#$%^'_.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'div1',20)">
                        </asp:TextBox>
                        <asp:Label ID="Label19" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                        <span id="div1" class="labelcount">20</span>
                        <asp:Label ID="lblsadjk" runat="server" Text="(A-Z 0-9 )" CssClass="labelcount">
                        </asp:Label>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblCompany" Font-Bold="true" runat="server" Font-Size="20px" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label17" Font-Bold="true" runat="server" Text="Business :" Font-Size="20px"></asp:Label>
                                                    <asp:Label ID="lblbusinessprint" Font-Bold="true" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label5" Font-Bold="true" runat="server" Font-Size="18px" Text="List of User and Role"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label14" runat="server" Font-Size="16px" Font-Bold="false" Text="User Type :"></asp:Label>
                                                    <asp:Label ID="lblusertypeprint" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                    &nbsp;,
                                                    <asp:Label ID="Label15" runat="server" Font-Size="16px" Font-Bold="false" Text="User Name : "></asp:Label>
                                                    <asp:Label ID="lblusernameprint" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                    &nbsp;,
                                                    <asp:Label ID="Label16" runat="server" Font-Size="16px" Font-Bold="false" Text="Role Name : "></asp:Label>
                                                    <asp:Label ID="lblrolenameprint" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                    &nbsp;,
                                                    <asp:Label ID="Label18" runat="server" Font-Size="16px" Font-Bold="false" Text="Status : "></asp:Label>
                                                    <asp:Label ID="lblstatusprint" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        Width="100%" EmptyDataText="No Record Found." OnRowCommand="GridView1_RowCommand"
                                        OnRowEditing="GridView1_RowEditing" OnRowUpdated="GridView1_RowUpdated" DataKeyNames="User_Role_id"
                                        OnRowDeleting="GridView1_RowDeleting" AllowSorting="True" OnSorting="GridView1_Sorting"
                                        AllowPaging="true" PageSize="20" OnPageIndexChanging="GridView1_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="Wname" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbusinessname123" runat="server" Text='<%# Eval("Wname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Type" SortExpression="PartType" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblusertypa123" runat="server" Text='<%# Eval("PartType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name of User" SortExpression="Compname" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Compname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Role Name" SortExpression="Role_name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Role_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="ActiveDeactive" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="chk12" runat="server" Text='<%# Eval("ActiveDeactive") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1" runat="server" ToolTip="Edit" CommandName="Edit"
                                                        CommandArgument='<%# Eval("User_Role_id") %>' ImageUrl="~/Account/images/edit.gif">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="2%"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton2" runat="server" ToolTip="Delete" CommandName="Delete"
                                                        CommandArgument='<%# Eval("User_Role_id") %>' ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc11:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
