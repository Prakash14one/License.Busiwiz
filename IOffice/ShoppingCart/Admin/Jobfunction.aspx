<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="Jobfunction.aspx.cs" Inherits="ShoppingCart_Admin_Default2"
    Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
        }

        function RealNumWithDecimal(myfield, e, dec) {

            //myfield=document.getElementById(FindName(myfield)).value
            //alert(myfield);
            var key;
            var keychar;
            if (window.event)
                key = window.event.keyCode;
            else if (e)
                key = e.which;
            else
                return true;
            keychar = String.fromCharCode(key);
            if (key == 13) {
                return false;
            }
            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 27)) {
                return true;
            }
            else if ((("0123456789.").indexOf(keychar) > -1)) {
                return true;
            }
            // decimal point jump
            else if (dec && (keychar == ".")) {

                myfield.form.elements[dec].focus();
                myfield.value = "";

                return false;
            }
            else {
                myfield.value = "";
                return false;
            }
        }

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


            }


            if (evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


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

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="lblmessage" runat="server" Visible="False" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllegend" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Add New Job" CausesValidation="False"
                            OnClick="Button1_Click" CssClass="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel1" runat="server" Visible="False">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 30%">
                                    <label>
                                        <asp:Label ID="lblbusiness" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                                            Width="255px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%">
                                    <label>
                                        <asp:Label ID="lbldepartmentdesg" runat="server" Text="Department - Designation"></asp:Label>
                                    </label>
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:DropDownList ID="ddldepartdesg" runat="server" Width="255px">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgdeprdesg" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                            OnClick="ImageButton48_Click" CausesValidation="False" />
                                        <asp:ImageButton ID="refimgdeprdesg" runat="server" Height="20px" ImageAlign="Bottom"
                                            ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="ImageButton49_Click"
                                            CausesValidation="False" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%">
                                    <label>
                                        <asp:Label ID="lbljobfuncategory" runat="server" Text="Job Function Category"></asp:Label>
                                        <asp:Label ID="labdsdsd" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddljobfuncat"
                                            ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:DropDownList ID="ddljobfuncat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddljobfuncat_SelectedIndexChanged"
                                            Width="255px">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton48" runat="server" Height="20px" ImageAlign="Bottom"
                                            ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="ImageButton48_Click1"
                                            CausesValidation="False" />
                                        <asp:ImageButton ID="ImageButton49" runat="server" Height="20px" ImageAlign="Bottom"
                                            ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="ImageButton49_Click1"
                                            CausesValidation="False" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%">
                                    <label>
                                        <asp:Label ID="lbljobfunsubcategory" runat="server" Text="Job Function Sub-Category"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddljobfunsubcat"
                                            ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:DropDownList ID="ddljobfunsubcat" runat="server" Width="255px">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton50" runat="server" Height="20px" ImageAlign="Bottom"
                                            ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="ImageButton50_Click"
                                            CausesValidation="False" />
                                        <asp:ImageButton ID="ImageButton51" runat="server" Height="20px" ImageAlign="Bottom"
                                            ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="ImageButton51_Click"
                                            CausesValidation="False" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%">
                                    <label>
                                        <asp:Label ID="lbljobfuntitle" runat="server" Text="Job Function Title"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtjobfuntitle"
                                            SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtjobfuntitle"
                                            ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:TextBox ID="txtjobfuntitle" runat="server" MaxLength="60" onKeydown="return mask(event)"
                                            Width="250px" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'Span4',60)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label5" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="Span4" cssclass="labelcount">60</span>
                                        <asp:Label ID="Label8" runat="server" Text="(A-Z 0-9 . , ? _)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%">
                                    <label>
                                        <asp:Label ID="lbljobdesc" runat="server" Text="Description"></asp:Label>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)" ControlToValidate="txtjobdesc"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:TextBox ID="txtjobdesc" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div2',1500)"
                                            runat="server" Height="60px" onkeypress="return checktextboxmaxlength(this,1500,event)"
                                            Width="360px" TextMode="MultiLine"></asp:TextBox>
                                        <asp:Label runat="server" ID="Label12" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="div2" cssclass="labelcount">1500</span>
                                        <asp:Label ID="Label24" CssClass="labelcount" runat="server" Text="(A-Z 0-9 . , ? _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%">
                                    <label>
                                        <asp:Label ID="lblstatus" runat="server" Text="Status"></asp:Label>
                                    </label>
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:CheckBox ID="chkstatus" runat="server" Text="Active" />
                                    </label>
                                    <label>
                                        <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <asp:Panel ID="panewsdsd" runat="server" Visible="false">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        <label>
                                            <asp:Label ID="lblattach" runat="server" Text="Attach File"></asp:Label>
                                        </label>
                                    </td>
                                    <td align="left">
                                        <label>
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                        </label>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td align="left" style="width: 30%">
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click"
                                            ValidationGroup="1" CssClass="btnSubmit" />
                                        <asp:Button ID="btnupdate" runat="server" Text="Update" Visible="False" OnClick="btnupdate_Click"
                                            ValidationGroup="1" CssClass="btnSubmit" />
                                        <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                            CssClass="btnSubmit" CausesValidation="False" />
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label18" runat="server" Text="List of Job Function"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnPrintVersion" runat="server" Text="Printable Version" OnClick="btnPrintVersion_Click"
                            CausesValidation="False" CssClass="btnSubmit" />
                        <input id="btnPrint" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" class="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    Business Name
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlbusiness1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness1_SelectedIndexChanged"
                                        Width="180px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    Department-Designation
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddldeprdesg1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldeprdesg1_SelectedIndexChanged"
                                        Width="180px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Job Function Category
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddljobfuncat1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddljobfuncat1_SelectedIndexChanged"
                                        Width="180px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    Job Function Sub Category
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddljobfunsubcat1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddljobfunsubcat1_SelectedIndexChanged"
                                        Width="180px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Status
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlstatus_search" runat="server" Width="180px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlstatus_search_SelectedIndexChanged">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <tr>
                                <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                <td colspan="4">
                                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr align="center">
                                                <td>
                                                    <div id="mydiv" class="closed">
                                                        <table width="100%">
                                                            <tr align="center">
                                                                <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                    <asp:Label ID="lblCompany" runat="server" Font-Italic="True" Text="Business : "></asp:Label>
                                                                    <asp:Label ID="Label1" runat="server" Font-Italic="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                    <asp:Label ID="Label3" runat="server" Font-Italic="True" Text="List of Job Function"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting"
                                                        OnRowEditing="GridView1_RowEditing" Width="100%" AutoGenerateColumns="False"
                                                        AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                        OnSorting="GridView1_Sorting" PageSize="10" EmptyDataText="No Record Found."
                                                        AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" CssClass="mGrid">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Business Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="12%" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department-Designation" SortExpression="DeprDesg"
                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="16%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("DeprDesg") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Category" SortExpression="CategoryName" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="12%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub Category" SortExpression="SubCategoryName" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="12%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("SubCategoryName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Job Function Title" SortExpression="Jobfunctiontitle"
                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="47%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("Jobfunctiontitle") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description" SortExpression="Jobfunctiondescription"
                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("Jobfunctiondescription") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Labesdsdl6" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                    <%--<asp:CheckBox ID="CheckBox1" Enabled="false" runat="server" Checked='<%# Eval("Status") %>' />--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="2%">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton53" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                                        CommandName="Edit" ImageUrl="~/Account/images/edit.gif" CausesValidation="False"
                                                                        ToolTip="Edit" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton54" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                                        CommandName="Delete" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                        ImageUrl="~/Account/images/delete.gif" CausesValidation="False" ToolTip="Delete" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="2%">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton52" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                                        CommandName="View" ImageUrl="~/Account/images/viewprofile.jpg" CausesValidation="False"
                                                                        ToolTip="View Profile" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
