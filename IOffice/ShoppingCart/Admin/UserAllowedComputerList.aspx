<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    CodeFile="UserAllowedComputerList.aspx.cs" Inherits="UserAllowedComputerList"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="right_content">
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="From Date"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtDate" runat="server" Width="75px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtdateopen_CalendarExtender" runat="server" TargetControlID="txtDate"
                                        PopupButtonID="ImageButton4">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="TextBox1_MaskedEditExtender" runat="server" CultureName="en-AU"
                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDate" />
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/ShoppingCart/images/cal_btn.jpg"
                                        Width="16px" Height="16px" />
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="To Date"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txttodate" runat="server" Width="75px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txttodate"
                                        PopupButtonID="ImageButton1">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txttodate" />
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_btn.jpg"
                                        Width="16px" Height="16px" />
                                </label>
                            </td>
                            <td>
                                <label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" Text="0 New Request"
                                        OnClick="LinkButton1_Click"></asp:LinkButton>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Filter by user Type"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlPartyType" runat="server">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="Search by Username"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="filter by Status"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem Selected="True" Text="All" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <asp:Button ID="Button1" runat="server" Text="Go" OnClick="Button1_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label1" runat="server" Text="List of computers allowed for access by users"></asp:Label>
                    </legend>
                    <asp:GridView ID="grdallowedlist" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                        AlternatingRowStyle-CssClass="alt" AllowPaging="True" AutoGenerateColumns="False"
                        EmptyDataText="No Record Found." PageSize="10">
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Type of User" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lbltypeofuser" runat="server" Text='<%# Eval("PartType")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="User Name" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblusername" runat="server" Text='<%# Eval("Compname")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Department" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lbldepartment" runat="server" Text='<%# Eval("Departmentname")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Designation" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("DesignationName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Computer Allowed"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblcompallowed" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Date Time of Request"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lbldatetimerequest" runat="server" Text='<%# Eval("DateandTimeRequest")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Computer Given Name"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblcompgiven" runat="server" Text='<%# Eval("ComputerGivenName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Computer Real Name"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblrealcompname" runat="server" Text='<%# Eval("ComputerRealName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Public IP" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblpublicip" runat="server" Text='<%# Eval("PublicIPAddress")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Local IP" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lbllocalip" runat="server" Text='<%# Eval("ComputerIPAddress")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Mac Address" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblmacaddress" runat="server" Text='<%# Eval("MacAddress")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Computer User"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblcomputeruser" runat="server" Text='<%# Eval("ComputerUser")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Status" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("StatusLabel")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </fieldset>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="Panel2" runat="server" Width="90%" Height="600px" ScrollBars="None"
                                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="10px">
                                <div>
                                    <fieldset>
                                        <div style="float: right;">
                                            <label>
                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="16px" />
                                            </label>
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <div>
                                            <fieldset>
                                                <legend>List of request from users to add comuters for access </legend>
                                                <asp:Panel ID="Panel1" runat="server" Height="450px" ScrollBars="Vertical">
                                                    <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" AllowPaging="True" AutoGenerateColumns="False"
                                                        EmptyDataText="No Record Found." Width="100%" PageSize="10">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Type of User" ItemStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltypeofuserpopup" runat="server" Text='<%# Eval("PartType")%>'></asp:Label>
                                                                    <asp:Label ID="lblusermacaddressmasterid" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="User Name" ItemStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblusernamepopup" runat="server" Text='<%# Eval("Compname")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Department" ItemStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldepartmentpopup" runat="server" Text='<%# Eval("Departmentname")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Designation" ItemStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldesignationpopup" runat="server" Text='<%# Eval("DesignationName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Computer Allowed"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcompallowedpopup" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Date Time of Request"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldatetimerequestpopup" runat="server" Text='<%# Eval("DateandTimeRequest")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Computer Given Name"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcompgivenpopup" runat="server" Text='<%# Eval("ComputerGivenName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Computer Real Name"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrealcompnamepopup" runat="server" Text='<%# Eval("ComputerRealName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Public IP" ItemStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpublicippopup" runat="server" Text='<%# Eval("PublicIPAddress")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Local IP" ItemStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbllocalippopup" runat="server" Text='<%# Eval("ComputerIPAddress")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Mac Address" ItemStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmacaddresspopup" runat="server" Text='<%# Eval("MacAddress")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Computer User"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcomputeruserpopup" runat="server" Text='<%# Eval("ComputerUser")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Status" ItemStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="DropDownList2" runat="server">
                                                                        <asp:ListItem Selected="True" Value="2" Text="Pending"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="Approve"></asp:ListItem>
                                                                        <asp:ListItem Value="0" Text="Reject"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    </asp:Panel>
                                            </fieldset>
                                        </div>
                                        <div style="text-align:center">
                                            <asp:Button ID="Button2" runat="server" Text="Submit" onclick="Button2_Click" />
                                            <asp:Button ID="Button3" runat="server" Text="Cancel" />
                                        </div>
                                    </fieldset>
                                </div>
                            </asp:Panel>
                            <asp:Button ID="Button10" runat="server" Style="display: none" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1333" runat="server" PopupControlID="Panel2"
                                TargetControlID="Button10" CancelControlID="ImageButton2">
                            </cc1:ModalPopupExtender>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
