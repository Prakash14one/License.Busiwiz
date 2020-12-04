<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="ProjectInvoice.aspx.cs" Inherits="ProjectInvoice"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">


        function SelectAllCheckboxes1(spanChk) {
            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                //                  alert(theBox.id);
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();

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
        .style1
        {
            height: 66px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <table width="100%">
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label1" runat="server" Text="Business"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlwarehouse" runat="server" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:Label ID="Label6" runat="server" Text="Invoice Type"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlinvtype" runat="server" Width="100px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlinvtype_SelectedIndexChanged">
                                <asp:ListItem Text="Cash Invoice" Value="1"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="Credit Invoce" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label2" runat="server" Text="Client"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlclient" runat="server" OnSelectedIndexChanged="ddlclient_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:Label ID="Label3" runat="server" Text="Project Status"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="100px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="lblcashtax" runat="server" Text="Cash Account"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlcash" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <label>
                                <asp:Label ID="Label4" runat="server" Text="Project"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlproject" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlproject_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <fieldset>
                    <legend>1. Material Used </legend>
                    <table width="100%">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Material Used for the Project"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="1">All</asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True">Unbilled Material</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td align="right">
                                <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Add Material Used for the Project"
                                    OnClick="Button1_Click" />
                                <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Refresh" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    EmptyDataText="No Record Found." AllowPaging="True" PageSize="6" ShowFooter="True"
                                    DataKeyNames="Mid">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcate" runat="server" Text='<%#Bind("InventoryCatName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Category" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcates" runat="server" Text='<%#Bind("InventorySubCatName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Sub Category" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcatess" runat="server" Text='<%#Bind("InventorySubSubName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpname" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Used<br> Units" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblunit" runat="server" Text='<%#Bind("Qty") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Billing Status" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblt" runat="server" Text='<%#Bind("bilst") %>'></asp:Label>
                                                <asp:Label ID="lblpcid" runat="server" Text='<%#Bind("pcid") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcost" runat="server" Text='<%#Bind("Rate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="List Sales Rate" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsalesrateor" runat="server" Width="50px" MaxLength="10" Text='<%#Bind("SalesRate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Applied Sales Rate" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblsalesrate" runat="server" Width="70px" MaxLength="10" Text='<%#Bind("SalesRate") %>'
                                                    OnTextChanged="txtMaterial_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="Ordervgf" runat="server" ControlToValidate="lblsalesrate"
                                                    Display="Dynamic" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="txtaddress_FilteredTextr" runat="server" Enabled="True"
                                                    FilterType="Custom, Numbers" TargetControlID="lblsalesrate" ValidChars=".">
                                                </cc1:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgt" runat="server" Font-Bold="true" Text="Grand Total"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Cost" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcharges" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalfooter" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total To be<br> Charge" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblchargestobe" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotaltobefooter" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Margin" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmargin" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Margin(%)" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmarginper" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblbillde" runat="server" Text="Bill Now"></asp:Label>
                                                <br />
                                                <asp:CheckBox ID="btnckmaster" runat="server" OnCheckedChanged="h1_chachedChanged"
                                                    AutoPostBack="true" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkc" runat="server" OnCheckedChanged="otherMaterial_chachedChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>2. Labour Used </legend>
                    <table width="100%">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label11" runat="server" Text="Labour Used for the Project"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                        <asp:ListItem Value="1">All</asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True">Unbilled Labour</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td align="right">
                                <asp:Button ID="Button3" runat="server" Text="Add Labour" CssClass="btnSubmit" OnClick="Button3_Click" />
                                <asp:Button ID="brnlabrefresh" runat="server" Text="Refresh" CssClass="btnSubmit" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False"
                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    EmptyDataText="No Record Found." ShowFooter="True" DataKeyNames="Id">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Service" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlservice" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlitem123_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="equiredFValidator1" runat="server" ControlToValidate="ddlservice"
                                                    Display="Dynamic" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldate" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfromtime" runat="server" Text='<%#Bind("FromTime") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEnddate" runat="server" Text='<%#Bind("FromToTime") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemp" runat="server" Text='<%#Bind("Employee") %>'></asp:Label>
                                                <asp:Label ID="lblempid" runat="server" Text='<%#Bind("EmployeeId") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Rate" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemprare" runat="server" Text='<%#Bind("Rate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Number of Hours" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhor" runat="server" Text='<%#Bind("Hrs") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="List Sales Rate" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsalesrateor" runat="server" Text='<%#Bind("SalesRate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Applied Sales Rate" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblsalesrate" runat="server" Width="70px" MaxLength="10" Text='<%#Bind("SalesRate") %>'
                                                    OnTextChanged="txtlabour_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="g2" runat="server" ControlToValidate="lblsalesrate"
                                                    Display="Dynamic" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="txtaddress_Filteextr" runat="server" Enabled="True"
                                                    FilterType="Custom, Numbers" TargetControlID="lblsalesrate" ValidChars=".">
                                                </cc1:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Billing Status" HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="102px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblt" runat="server" Text='<%#Bind("bilst") %>'></asp:Label>
                                                <asp:Label ID="labourId" runat="server" Text='<%#Bind("labourId") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgt" runat="server" Font-Bold="true" Text="Grand Total"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Cost" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcharges" runat="server" Text='<%#Bind("Cost") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalfooter" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total To be<br>Charge" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblchargestobe" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotaltobefooter" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Margin" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmargin" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Margin(%)" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmarginper" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblbillde" runat="server" Text="Bill Now"></asp:Label>
                                                <br />
                                                <asp:CheckBox ID="btnckmaster" runat="server" OnCheckedChanged="h2_chachedChanged"
                                                    AutoPostBack="true" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkc" runat="server" OnCheckedChanged="otherlabour_chachedChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>3. Other Charges </legend>
                    <table width="100%">
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnover" runat="server" Text="View Total Overhead" CssClass="btnSubmit"
                                    OnClick="btnover_Click" />
                                <asp:Button ID="Button5" runat="server" Text="Add Charges" CssClass="btnSubmit" OnClick="Button5_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Panel ID="panel3" runat="server" Width="100%" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="6">
                                                <label>
                                                    <asp:Label ID="lblcathead" runat="server" Text="Category"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label7" runat="server" Text="Service"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFValidator1" runat="server" ControlToValidate="ddlservices"
                                                        Display="Dynamic" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                </label>
                                                <label>
                                                    <asp:DropDownList ID="ddlservices" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlservices_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label19" runat="server" Text="Note"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="tdff" runat="server" ControlToValidate="txtnote"
                                                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txtnote" runat="server" MaxLength="100" Width="400px"></asp:TextBox>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label8" runat="server" Text="List Sales Rate"></asp:Label></label>
                                                <label>
                                                    <asp:Label ID="lbllistsalrate" runat="server" Text="0"></asp:Label></label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label20" runat="server" Text="Applied Sales Rate"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RedFieldValidator1" runat="server" ControlToValidate="txtrate"
                                                        Display="Dynamic" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txtrate" runat="server" Width="70px"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txtadds_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtrate" ValidChars=".">
                                                    </cc1:FilteredTextBoxExtender>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label21" runat="server" Text="Quantity"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequdeldValidator2" runat="server" ControlToValidate="txtqty"
                                                        Display="Dynamic" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txtqty" runat="server" Width="70px"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FieredTextBoxExtender1" runat="server" Enabled="True"
                                                        FilterType="Custom, Numbers" TargetControlID="txtqty" ValidChars="">
                                                    </cc1:FilteredTextBoxExtender>
                                                </label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Button ID="Button6" runat="server" Text="Add" CssClass="btnSubmit" ValidationGroup="2"
                                                    OnClick="Button6_Click" />
                                                &nbsp;<asp:Button ID="Button7" runat="server" Text="Cancel" CssClass="btnSubmit"
                                                    OnClick="Button7_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:GridView ID="GridView3" runat="server" Width="100%" AutoGenerateColumns="False"
                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    EmptyDataText="No Record Found." AllowPaging="True" DataKeyNames="Id" ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Service" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="25%"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblservice" runat="server" Text='<%#Bind("Service") %>'></asp:Label>
                                                <asp:Label ID="lblserviceId" runat="server" Text='<%#Bind("ServiceId") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Note" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50%"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgnote" runat="server" Text='<%#Bind("Text") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgqty" runat="server" Text='<%#Bind("Qty") %>'></asp:Label>
                                                <asp:Label ID="lblId" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="List Sales Rate" HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsalesrateor" runat="server" Text='<%#Bind("Rateor") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Applied Sales Rate" HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrate" runat="server" Text='<%#Bind("Rate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblttt" runat="server" Font-Bold="true" Text="Grand Total"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="7%"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgtotal" runat="server" Text='<%#Bind("Total") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotaltobefooter" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblbillde" runat="server" Text="Bill Now"></asp:Label>
                                                <asp:CheckBox ID="btnckmaster" runat="server" OnCheckedChanged="h3_chachedChanged"
                                                    AutoPostBack="true" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkc" runat="server" Checked='<%#Bind("chk") %>' OnCheckedChanged="otherchk_chachedChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>4. Finished Goods Details</legend>
                    <table width="100%">
                        <tr>
                            <td colspan="6" class="style1">
                                <label>
                                    <asp:Label ID="Label31" runat="server" Text="Is this work order complete"></asp:Label>
                                </label>
                                <asp:CheckBox ID="chkworder" runat="server" AutoPostBack="True" OnCheckedChanged="chkworder_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Panel ID="pnlworkorderinv" runat="server" Visible="false">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="6">
                                                <label>
                                                    <asp:Label ID="Label32" runat="server" Text="How many finished inventory was created and is being billed to customer ?"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lbblds" runat="server" Text="Cat:Sub:Subsub"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <label>
                                                            <asp:DropDownList ID="ddlcatesub" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcatesub_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgadddivision" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                 ToolTip="AddNew" Width="20px" ImageAlign="Bottom" 
                                                            onclick="imgadddivision_Click" />
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="LinkButton3" runat="server" AlternateText="Refresh" Height="20px"
                                                                ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" 
                                                                ToolTip="Refresh" Width="20px" ImageAlign="Bottom" 
                                                            onclick="LinkButton3_Click1" />
                                                        </label>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="imgadddivision" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="LinkButton3" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label27" runat="server" Text="Inventory Name"></asp:Label>
                                                </label>
                                            </td>
                                          
                                            <td align="left" valign="top">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <label>
                                                           <asp:DropDownList ID="ddlinvname" runat="server">
                                                    </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="ImageButton2" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                ToolTip="AddNew" Width="20px" ImageAlign="Bottom" 
                                                            onclick="ImageButton2_Click" />
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="ImageButton3" runat="server" AlternateText="Refresh" Height="20px"
                                                                ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" 
                                                                ToolTip="Refresh" Width="20px" ImageAlign="Bottom" 
                                                            onclick="ImageButton3_Click" />
                                                        </label>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ImageButton2" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="ImageButton3" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label30" runat="server" Text="Number of Units"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="rqf" runat="server" 
                                                    ControlToValidate="lblunits" ErrorMessage="*" SetFocusOnError="true" 
                                                    Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="lblunits" runat="server" Text="1" Width="60px"></asp:TextBox>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                            <td>
                            </td>
                            <td width="50%">
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label26" runat="server" Text="Margin"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="lblmarginamt" runat="server" Text=""></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="Label29" runat="server" Text="Margin %"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="lblMarginpercen" runat="server" Text=""></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="Label25" runat="server" Text="Total net Invoice Amount"></asp:Label>
                                    <asp:Label ID="lblTotalnetamt" runat="server" Text=""></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <asp:Button ID="Button4" runat="server" Text="View Margin Summary" CssClass="btnSubmit"
                                    OnClick="Button4_Click" />
                                <asp:Button ID="Button8" runat="server" Text="Create an Invoice Now" CssClass="btnSubmit"
                                    OnClick="Button8_Click" ValidationGroup="1" />
                                <asp:Button ID="Button9" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="Button9_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Panel ID="Paneldoc" runat="server" Width="60%" CssClass="modalPopup">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="lbldoclab" runat="server" Text="View Margin Summary"></asp:Label>
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
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblAm" runat="server" Text="Already Billed" Font-Bold="true" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label9" runat="server" Text="Billed Now" Font-Bold="true" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label24" runat="server" Text="Grand Total" Font-Bold="true" ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblmaterialtext" runat="server" Text="Total Material Cost" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblmaterialoldbill" runat="server" Text="0.00" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblmaterialNowbill" runat="server" Text="0.00" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="Label12" runat="server" Text="Total Labour Cost" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lbllabouroldbill" runat="server" Text="0.00" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lbllabourNowbill" runat="server" Text="0.00" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="Label10" runat="server" Text="Total Other Charges" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblotheroldbill" runat="server" Text="0.00" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblotherNowbill" runat="server" Text="0.00" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td align="right">
                                                                ____________
                                                            </td>
                                                            <td align="right">
                                                                ___________
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label16" runat="server" Text="A" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="Label13" runat="server" Text="Total Work Order Cost" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblalreadytotalbill" runat="server" Text="0.00" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblnewtotalbill" runat="server" Text="0.00" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lbltotalcost" runat="server" Text="0.00" ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label14" runat="server" Text="B" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="Label15" runat="server" Text="Total Sales" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblalreadytotalsale" runat="server" Text="0.00" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblnewsale" runat="server" Text="0.00" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lbltotsales" runat="server" Text="0.00" ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label17" runat="server" Text="C" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="Label18" runat="server" Text="Margin" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblnewmargin" runat="server" Text="0.00" ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label22" runat="server" Text="D" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="Label23" runat="server" Text="Margin %" ForeColor="Black"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblnewmarginper" runat="server" Text="0.00" ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="Button10" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Paneldoc" TargetControlID="Button10" CancelControlID="ImageButton10">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Panel ID="Panel1" runat="server" Width="80%" CssClass="modalPopup">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="Label28" runat="server" Text="View Overhead Allocation"></asp:Label>
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 95%;">
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="grdaccount" runat="server" AllowSorting="True" AlternatingRowStyle-CssClass="alt"
                                                                    AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="Id" EmptyDataText="No Record Found."
                                                                    GridLines="Both" PagerStyle-CssClass="pgr">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Group Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgroupid" runat="server" Text='<%#Bind("groupid") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblgroupname" runat="server" Text='<%#Bind("groupdisplayname") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Account Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblaccountmasterid" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblaccountname" runat="server" Text='<%#Bind("AccountName") %>'></asp:Label>
                                                                                <asp:Label ID="lblaccountid" runat="server" Text='<%#Bind("AccountId") %>' Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Selected Period Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblamount" runat="server" Text='<%#Bind("AmountForPeriod") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Amount to be Allocated">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtamountallocate" Text='<%#Bind("AmountApplied") %>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Select Allocation Method">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ddlallocation" runat="server" Text='<%#Bind("AllName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle CssClass="pgr" />
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="Button11" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel1" TargetControlID="Button11" CancelControlID="ImageButton1">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
