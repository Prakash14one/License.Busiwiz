<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="CustomerServiceCall.aspx.cs" Inherits="ShoppingCart_Admin_CustomerSericeCall"
    Title="Untitled Page" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
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
        .style1
        {
            height: 37px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024px,height=768px,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }


        function mask(evt) {

            if (evt.keyCode == 13) {

                return true;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }

        }

        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
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
        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
    </script>

    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 1%">
                <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <asp:Panel ID="pnlmaster1" runat="server">
                    <fieldset>
                        <div style="float: right;">
                            <asp:Button ID="Button2" runat="server" Text="Add Customer Complaint" CssClass="btnSubmit"
                                OnClick="Button2_Click" />
                        </div>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="addinventorysite" runat="server">
                            <label>
                                <asp:Label ID="Label6" runat="server" Text="Business Name"></asp:Label>
                                <asp:DropDownList ID="ddlWarehouse" runat="server">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="Label1" runat="server" Text="From Date"></asp:Label>
                                <asp:TextBox ID="txtFromDate" runat="server" Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                    PopupButtonID="ImageButton1">
                                </cc1:CalendarExtender>
                            </label>
                            <label>
                                <br />
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_btn.jpg">
                                </asp:ImageButton>
                            </label>
                            <label>
                                <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
                                <asp:TextBox ID="txtTodate" runat="server" Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate"
                                    PopupButtonID="ImageButton2">
                                </cc1:CalendarExtender>
                            </label>
                            <label>
                                <br />
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_btn.jpg">
                                </asp:ImageButton>
                            </label>
                            <label>
                                <asp:Label ID="Label3" runat="server" Text="Status"></asp:Label>
                                <asp:DropDownList ID="ddlMainStatus" runat="server" Width="100px">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="Label20" runat="server" Text="Search by Problem Type"></asp:Label>
                                <asp:DropDownList ID="ddlseacrchbyproblemtype" runat="server">
                                </asp:DropDownList>
                            </label>
                            <div style="clear: both;">
                            </div>
                            <label>
                                <asp:Label ID="Label4" runat="server" Text="Search by Name, Phone No., or Email"></asp:Label>
                                <asp:TextBox ID="txtname" runat="server" MaxLength="20" Width="250px" onKeydown="return mask(event)"
                                    onkeyup="return check(this,/[\\/!#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9@._ ]+$/,'div1',20)"></asp:TextBox>
                                <asp:Label ID="Label27" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                <span id="div1" class="labelcount">20</span>
                                <asp:Label ID="lebebl" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ @ .)"></asp:Label>
                            </label>
                            <label>
                                <br />
                                <asp:Button ID="ImageButton3" CssClass="btnSubmit" OnClick="ImageButton3_Click" runat="server"
                                    Text="Go"></asp:Button>
                            </label>
                            <div style="clear: both;">
                            </div>
                        </asp:Panel>
                    </fieldset>
                    <div style="clear: both;">
                    </div>
                    <fieldset>
                        <legend>
                            <asp:Label ID="lblList" Text="List of Customer Complaint Calls" runat="server" Font-Bold="true"></asp:Label>
                        </legend>
                        <div style="float: right;">
                            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click1" />
                            <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                style="width: 51px;" type="button" value="Print" visible="false" />
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
                                                        <asp:Label ID="lblCompany" runat="server" Visible="false" Font-Italic="True">
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                        <asp:Label ID="Label19" runat="server" Text="Business : " Font-Italic="True"></asp:Label>
                                                        <asp:Label ID="lblBusiness" runat="server" Font-Italic="True"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                        <asp:Label ID="Label5" runat="server" Font-Italic="true" Text="List of Customer Complaint Calls"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label15" runat="server" Text="From Date :" Font-Italic="true" Font-Size="Smaller"></asp:Label>
                                                        <asp:Label ID="lblfromdateprint" runat="server" Font-Italic="true" Text="" Font-Size="Smaller"></asp:Label>
                                                        &nbsp;,
                                                        <asp:Label ID="Label17" runat="server" Text="To Date :" Font-Italic="true" Font-Size="Smaller"></asp:Label>
                                                        <asp:Label ID="lbltodateprint" runat="server" Font-Italic="true" Text="" Font-Size="Smaller"></asp:Label>
                                                        &nbsp;,
                                                        <asp:Label ID="Label16" runat="server" Text="Status :" Font-Italic="true" Font-Size="Smaller"></asp:Label>
                                                        <asp:Label ID="lblstatusprint" runat="server" Font-Italic="true" Text="" Font-Size="Smaller"></asp:Label>
                                                        &nbsp;,
                                                        <asp:Label ID="Label30" runat="server" Text="Problem Type :" Font-Italic="true" Font-Size="Smaller"></asp:Label>
                                                        <asp:Label ID="lblproblemtypeprint" runat="server" Font-Italic="true" Text="" Font-Size="Smaller"></asp:Label>
                                                        &nbsp;,
                                                        <asp:Label ID="Label18" runat="server" Text="Name,Phone No.,or Email :" Font-Italic="true"
                                                            Font-Size="Smaller"></asp:Label>
                                                        <asp:Label ID="lblnamephoneprint" runat="server" Font-Italic="true" Text="" Font-Size="Smaller"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cc11:PagingGridView ID="GridServiceCall" runat="server" OnPageIndexChanging="GridServiceCall_PageIndexChanging"
                                            AllowPaging="True" Width="100%" EmptyDataText="No Record Found." OnSelectedIndexChanged="GridServiceCall_SelectedIndexChanged"
                                            OnRowCommand="GridServiceCall_RowCommand" DataKeyNames="CustomerServiceCallMasterId"
                                            AutoGenerateColumns="False" AllowSorting="True" OnSorting="GridServiceCall_Sorting"
                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                            OnRowDeleting="GridServiceCall_RowDeleting1">
                                            <Columns>
                                                <asp:BoundField DataField="CustomerServiceCallMasterId" HeaderText="ID" Visible="true"
                                                    SortExpression="CustomerServiceCallMasterId" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%"></asp:BoundField>
                                                <asp:BoundField DataField="Wname" SortExpression="Wname" HeaderText="Business Name"
                                                    Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="15%"></asp:BoundField>
                                                <asp:BoundField DataField="EntryDate" SortExpression="EntryDate" DataFormatString="{0:MM-dd-yyyy}"
                                                    HeaderText="Call Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="7%"></asp:BoundField>
                                                <asp:BoundField DataField="comp" SortExpression="comp" HeaderText="Customer" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%"></asp:BoundField>
                                                <asp:BoundField DataField="ProblemName" SortExpression="ProblemName" HeaderText="Problem Type"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProblemTitle" SortExpression="ProblemTitle" HeaderText="Problem Title"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Servicestatus" SortExpression="Servicestatus" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%" HeaderText="Status"></asp:BoundField>
                                                <asp:TemplateField HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("CustomerServiceCallMasterId") %>'
                                                            CommandName="view" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                    HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton48" runat="server" ToolTip="Delete" CommandName="Delete"
                                                            Height="16px" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?')"
                                                            CommandArgument='<%# Eval("CustomerServiceCallMasterId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-Width="2%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("CustomerServiceCallMasterId") %>'
                                                            CommandName="viewprofile" ToolTip="View Profile" Height="20px" ImageUrl="~/Account/images/viewprofile.jpg"
                                                            Width="20px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <AlternatingRowStyle CssClass="alt" />
                                        </cc11:PagingGridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                    <div style="clear: both;">
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlmastershow123" runat="server" Visible="false">
                    <fieldset>
                        <asp:Panel ID="Panel2123" runat="server" Width="100%">
                            <table style="width: 100%">
                                <tbody>
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label7" runat="server" Text="Complaint ID"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="lblId" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 25%">
                                        </td>
                                        <td style="width: 25%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label8" runat="server" Text="Date"></asp:Label>
                                                <asp:Label ID="Label22" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtdate1"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:TextBox ID="txtdate1" runat="server" Width="100px"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/images/cal_btn.jpg" />
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="ImageButton6"
                                                    TargetControlID="txtdate1">
                                                </cc1:CalendarExtender>
                                            </label>
                                        </td>
                                        <td style="width: 25%">
                                        </td>
                                        <td style="width: 25%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label21" runat="server" Text="Business Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:DropDownList ID="ddlupdatebusiness" runat="server">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td style="width: 25%">
                                        </td>
                                        <td style="width: 25%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label9" runat="server" Text="Customer"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:DropDownList ID="ddlUserMaster" runat="server">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td style="width: 25%">
                                        </td>
                                        <td style="width: 25%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label10" runat="server" Text="Problem Title"></asp:Label>
                                                <asp:Label ID="Label23" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtprobtitle"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="Invalid Character"
                                                    SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtprobtitle"
                                                    ValidationGroup="1">
                                                </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td colspan="3">
                                            <label>
                                                <asp:TextBox ID="txtprobtitle" runat="server" Width="490px" MaxLength="60" onKeydown="return mask(event)"
                                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span1',60)"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label28" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                <span id="Span1" class="labelcount">60</span>
                                                <asp:Label ID="Label24" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label11" runat="server" Text="Problem Type"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="3">
                                            <label>
                                                <asp:DropDownList ID="ddlProbType" runat="server">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label12" runat="server" Text="Description"></asp:Label>
                                                <asp:Label ID="Label25" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdescription"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2123" runat="server"
                                                    ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z.0-9\s]*)"
                                                    ControlToValidate="txtdescription" ValidationGroup="1">
                                                </asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Max 1000 Characters "
                                                    SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1000})$" ControlToValidate="txtdescription"
                                                    ValidationGroup="1">
                                                </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td colspan="3">
                                            <label>
                                                <asp:TextBox ID="txtdescription" runat="server" Height="70px" TextMode="MultiLine"
                                                    onKeydown="return mask(event)" onkeypress="return checktextboxmaxlength(this,1000,event)"
                                                    MaxLength="1000" Width="490px" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.\s]+$/,'Span2',1000)"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label29" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                <span id="Span2" class="labelcount">1000</span>
                                                <asp:Label ID="Label26" runat="server" Text="(A-Z 0-9 _ .)" CssClass="labelcount"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <label>
                                                <asp:Label ID="Label13" runat="server" Text="Status" Visible="false"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="2">
                                            <label>
                                                <asp:DropDownList ID="ddlStatus" runat="server" Width="231px" Visible="false">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td style="width: 25%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label14" runat="server" Text="Serivce Note" Visible="false"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtServiceNotes" runat="server" Height="60px" Width="500px" TextMode="MultiLine"
                                                    Visible="false"></asp:TextBox>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="2">
                                            <asp:Button ID="ImageButton4" CssClass="btnSubmit" OnClick="ImageButton4_Click" runat="server"
                                                Text="Update" ValidationGroup="1"></asp:Button>
                                            <asp:Button ID="ImageButton5" CssClass="btnSubmit" OnClick="ImageButton5_Click" runat="server"
                                                Text="Cancel"></asp:Button>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:Panel>
                    </fieldset></asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
