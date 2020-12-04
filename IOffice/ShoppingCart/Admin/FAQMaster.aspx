<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="FAQMaster.aspx.cs" Inherits="ShoppingCart_Admin_FAQMaster"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UControlWizardpanel.ascx" TagName="pnl"
    TagPrefix="pnl" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
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

    <script type="text/javascript" language="javascript">
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


            if (evt.keyCode == 188 ||   evt.keyCode == 191|| evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Add a Frequently Asked Question (FAQ)"
                            Font-Bold="True" Visible="False"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add FAQ" CssClass="btnSubmit" OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <table width="100%">
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Business Name"></asp:Label></label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text="FAQ Category Name"></asp:Label>
                                        <asp:Label ID="Label7" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="DropDownList1" runat="server">
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
                                        <asp:Label ID="Label9" runat="server" Text="Set Your Question"></asp:Label>
                                        <asp:Label ID="Label8" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfaq"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                            SetFocusOnError="True" ValidationExpression="^([_a-zA-Z?().0-9\s]*)" ControlToValidate="txtfaq"
                                            ValidationGroup="1">
                                            
                                        </asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Max 300 Characters"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,300})$"
                                            ControlToValidate="txtfaq" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtfaq" runat="server" MaxLength="300" TextMode="MultiLine" Height="65px"
                                            Width="500px" onkeypress="return checktextboxmaxlength(this,300,event)" onkeyup="return check(this,/[\\/!@#$%^'&*>+:;={}[]|\/]/g,/^[\a-z.A-Z()0-9_?\s]+$/,'Span1',300)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label21" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" >300</span>
                                        <asp:Label ID="Label2" runat="server" Text="(A-Z 0-9 _ . () ?)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label10" runat="server" Text="Answer"></asp:Label>
                                        <asp:Label ID="lbl1" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAnswer"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                            SetFocusOnError="True" ValidationExpression="^([_a-zA-Z.0-9\s]*)" ControlToValidate="txtAnswer"
                                            ValidationGroup="1">
                                            
                                        </asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Max 1000 Characters"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1000})$"
                                            ControlToValidate="txtAnswer" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtAnswer" runat="server" MaxLength="1000" TextMode="MultiLine"
                                            Height="150px" Width="500px" onkeypress="return checktextboxmaxlength(this,1000,event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z.A-Z0-9_\s]+$/,'Span2',1000)">
                                        </asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span2" >1000</span>
                                        <asp:Label ID="Label17" runat="server" Text="(A-Z 0-9 _ .)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label11" runat="server" Text="Status"></asp:Label></label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="RadioButton1" runat="server" Width="100px">
                                            <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                        <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                        <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="ImageButton1" CssClass="btnSubmit" runat="server" OnClick="Button1_Click"
                                        ValidationGroup="1" Text="Submit" />
                                    <asp:Button ID="Button3" CssClass="btnSubmit" runat="server" ValidationGroup="1"
                                        OnClick="Btnupdate_Click" Visible="false" Text="Update" />
                                    <asp:Button ID="ImageButton2" runat="server" OnClick="Button3_Click" Text="Cancel"
                                        CssClass="btnSubmit" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="legenid" Text=" List of Frequently Asked Questions"
                            Font-Bold="true"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button2" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button2_Click1" />
                        <input id="Button1" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" class="btnSubmit" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <label>
                            <asp:Label runat="server" ID="lblfilter" Text="Filter by Business Name"></asp:Label>
                            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                    </div>
                    <div>
                        <label>
                            <asp:Label runat="server" ID="Label14" Text="Filter by FAQ Category Name"></asp:Label>
                            <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </label>
                    </div>
                    <div>
                        <label>
                            <asp:Label ID="Label15" runat="server" Text="Filter by Status"></asp:Label>
                            <asp:DropDownList ID="ddlstatts" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstatts_SelectedIndexChanged">
                                <asp:ListItem Text="All"></asp:ListItem>
                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" style="text-align: center; color: #000000; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Size="20px" Font-Italic="True" Visible="false"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label6" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Size="20px" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label3" runat="server" Font-Italic="true" Font-Size="18px" ForeColor="Black"
                                                        Text="List of Frequently Asked Questions "></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="text-align: left; font-size: 13px;">
                                                    <asp:Label ID="Label4" runat="server" Font-Size="16px" Font-Italic="True" Text=" FAQ Category Name : "></asp:Label>
                                                    <asp:Label ID="lblFAQCAT" runat="server" Font-Size="16px" Font-Italic="True"> </asp:Label>
                                                    &nbsp;,
                                                    <asp:Label ID="Label5" runat="server" Font-Size="16px" Font-Italic="True" Text="Status : "></asp:Label>
                                                    <asp:Label ID="lblstatusprint" runat="server" Font-Size="16px" Font-Italic="True"> </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                        DataKeyNames="FAQMaster_Id" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        GridLines="Both" PageSize="10" OnRowEditing="GridView1_RowEditing" AllowSorting="True"
                                        OnSorting="GridView1_Sorting" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                        OnRowUpdating="GridView1_RowUpdating" AllowPaging="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
                                        OnPageIndexChanging="GridView1_PageIndexChanging1" EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business Name" ItemStyle-Width="15%" SortExpression="Wname"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBusname" runat="server" Text='<%# Bind("Wname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FAQ Category " ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="FAQCategoryName" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdcatname" runat="server" Text='<%# Bind("FAQCategoryName") %>'></asp:Label>
                                                    <asp:Label ID="lblgrdCatid" Visible="false" runat="server" Text='<%# Bind("FAQCategoryMaster_Id") %>'></asp:Label>
                                                    <asp:Label ID="lblFAQMid" Visible="false" runat="server" Text='<%# Bind("FAQMaster_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Question" ItemStyle-Width="30%" SortExpression="FAQ"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQue" runat="server" Text='<%# Bind("FAQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Answer" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="Answer">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAns" runat="server" Text='<%# Bind("Answer") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="Active">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Bind("Active")%>' Visible="false"
                                                        Enabled="false"></asp:CheckBox>
                                                    <asp:Label ID="lblAns123123" runat="server" Text='<%# Bind("Statuslabel") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("FAQMaster_Id") %>'
                                                        CommandName="Edit" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Button1" runat="server" CommandArgument='<%# Eval("FAQMaster_Id") %>'
                                                        CommandName="Delete" ImageUrl="~/Account/images/delete.gif" ToolTip="Delete"
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
