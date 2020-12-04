<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="EmailContentMaster.aspx.cs" Inherits="ShoppingCart_Admin_EmailContentMaster"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                        <asp:Label runat="server" ID="contntlabel" Text="" Visible="False"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" CssClass="btnSubmit" Text="Add Pre-Formatted Email Content"
                            OnClick="btnadd_Click"></asp:Button>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="addemail" Visible="false" Width="100%" runat="server">
                        <table width="100%">
                            <tr>
                                <td style="width:30%">
                                    <label>
                                        <asp:Label runat="server" ID="Label6" Text="Business Name"></asp:Label>
                                        <asp:Label runat="server" ID="Label10" Text="*" CssClass="labelstar"></asp:Label>
                                    </label>
                                </td>
                                <td style="width:70%">
                                    <label>
                                        <asp:DropDownList ID="ddlWebsiteName" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlWebsiteName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label runat="server" ID="lblbusinessname" Text="Pre-Formatted Email Name"></asp:Label>
                                        <asp:Label runat="server" ID="Label11" Text="*" CssClass="labelstar"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlEmailType" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                     <label>
                                        <asp:ImageButton ID="imgaddcatt" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                            ToolTip="AddNew" Width="20px" ImageAlign="Bottom" OnClick="imgaddcatt_Click" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgcattrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                            ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                            ImageAlign="Bottom" OnClick="imgcattrefresh_Click" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label runat="server" ID="Label7" Text="Pre-Formatted Email Content"></asp:Label>
                                        <asp:Label ID="lbl1" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmailContent"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                            SetFocusOnError="True" ValidationExpression="^([_a-zA-Z.0-9\s]*)" ControlToValidate="txtEmailContent"
                                            ValidationGroup="1">
                                            
                                        </asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1000})$"
                                            ControlToValidate="txtEmailContent" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtEmailContent" runat="server" MaxLength="1000" TextMode="MultiLine"
                                            Height="200px" Width="550px" onkeypress="return checktextboxmaxlength(this,1000,event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z.A-Z0-9_\s]+$/,'Span1',1000)">
                                        </asp:TextBox>
                                    </label>
                                    <label>
                                    <asp:Panel runat="server" ID="pnlprinthide">
                                        <asp:Label ID="Label21" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" class="labelcount">1000</span>
                                        <asp:Label ID="Label9" runat="server" Text="(A-Z 0-9 _ .)" CssClass="labelcount"></asp:Label>
                                        </asp:Panel>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label runat="server" ID="Label8" Text="Entry Date"></asp:Label>
                                        <asp:Label runat="server" ID="Label12" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextEntryDate"
                                            ErrorMessage="*" ValidationGroup="1">
                                        </asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="TextEntryDate" runat="server" Width="100px"></asp:TextBox>
                                    </label>
                                    <cc1:CalendarExtender ID="CalendarExtender2" Format="MM/dd/yyyy" runat="server" PopupButtonID="ImageButton1"
                                        TargetControlID="TextEntryDate">
                                    </cc1:CalendarExtender>
                                    <label>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                    <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnsubmit" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="Button1_Click1"
                                        ValidationGroup="1" />
                                    <asp:Button ID="btnupdate" runat="server" CssClass="btnSubmit" Text="Update" Visible="false"
                                        ValidationGroup="1" OnClick="btnupdate_Click" />
                                    <asp:Button ID="btncancel" Text="Cancel" CssClass="btnSubmit" runat="server" OnClick="btncancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllist" Text="List of Pre-Formatted Email Names"
                            Font-Bold="true"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            CausesValidation="False" OnClick="Button2_Click1" />
                        <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label3" runat="server" Text="Filter by Business Name"></asp:Label>
                        <asp:DropDownList ID="ddlbusinessfilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusinessfilter_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label visible="false">
                        <asp:Label ID="Label17" runat="server" Text="Filter by Pre-Formatted Email Name"></asp:Label>
                        <asp:DropDownList ID="ddshorting" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddshorting_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" ScrollBars="Vertical" Width="100%">
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
                                                    <asp:Label ID="Label5" runat="server" Font-Italic="True" Text="List of Pre-Formatted Email Names"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label4" Font-Italic="true" Text="Pre-Formatted Email Name :"></asp:Label>
                                                    <asp:Label ID="lblemailtypeprint" runat="server" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="EmailContentMasterId"
                                        OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                                        OnRowUpdating="GridView1_RowUpdating" OnSorting="GridView1_Sorting" OnRowCommand="GridView1_RowCommand"
                                        AllowSorting="True" OnRowDeleting="GridView1_RowDeleting" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        CssClass="mGrid" Width="100%" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="20%" HeaderText="Business Name" SortExpression="Wname">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Bind("Wname") %>'></asp:Label>
                                                    <asp:Label ID="lblSiteId" runat="server" Visible="False" Text='<%#Bind("EmailContentMasterId") %>'> 
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Pre-Formatted Email Name" SortExpression="Name" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2e" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="45%" HeaderText="Pre-Formatted Email Content"
                                                SortExpression="EmailContentDetail">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("EmailContentDetail") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderText="Entry Date"
                                                SortExpression="EntryDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEntryDate" runat="server" Text='<%# Bind("EntryDate", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left"
                                                HeaderImageUrl="~/Account/images/viewprofile.jpg">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton48" runat="server" ToolTip="View" ImageUrl="~/Account/images/viewprofile.jpg"
                                                        CommandArgument='<%# Eval("EmailContentMasterId") %>' CommandName="View" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnimageupdate" runat="server" CommandName="Edit" ToolTip="Edit"
                                                        CommandArgument='<%# Eval("EmailContentMasterId") %>' ImageUrl="~/Account/images/edit.gif">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Button1" runat="server" CommandName="Delete" ToolTip="Delete"
                                                        CommandArgument='<%# Eval("EmailContentMasterId") %>' ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
