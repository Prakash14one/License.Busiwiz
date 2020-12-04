<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="EducationDegrees.aspx.cs" Inherits="ShoppingCart_Admin_Default4"
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
                        <asp:Button ID="Button1" runat="server" Text="Add New Education Degree" CssClass="btnSubmit"
                            OnClick="Button1_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel1" runat="server" Visible="False">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        Area of Study
                                        <asp:Label ID="Label2" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlareaofstudy"
                                            ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlareaofstudy" runat="server" Width="250px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlareaofstudy_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        Level of Education
                                        <asp:Label ID="Label1" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddllevelofedu"
                                            ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddllevelofedu" runat="server" Width="250px">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton50" runat="server" Height="20px" ImageAlign="Bottom"
                                            ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" CausesValidation="False" OnClick="ImageButton50_Click" />
                                        <asp:ImageButton ID="ImageButton51" runat="server" Height="20px" ImageAlign="Bottom"
                                            ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" CausesValidation="False" OnClick="ImageButton51_Click" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        Degree Name
                                    </label>
                                    <label>
                                        <asp:Label ID="Label34" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtdegreename"
                                            SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                            ErrorMessage="Invalid Character" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                            ControlToValidate="txtdegreename" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtdegreename" runat="server" MaxLength="60" onKeydown="return mask(event)"
                                            Width="255px" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'Span4',60)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label5" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="Span4" cssclass="labelcount">60</span>
                                        <asp:Label ID="Label8" runat="server" Text="(A-Z 0-9 . , ? _)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        Status
                                    </label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkstatus" runat="server" Text="Active" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        Select File
                                        <asp:Label ID="Label32" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator177" runat="server" ControlToValidate="File12"
                                            ErrorMessage="*" ValidationGroup="3"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:FileUpload ID="File12" runat="server" CssClass="btnSubmit" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnfileupload" runat="server" CssClass="btnSubmit" OnClick="btnfileupload_Click"
                                            Text="Upload File" ValidationGroup="3" />
                                    </label>
                                </td>
                            </tr>
                            <asp:Panel ID="pnltransfer" runat="server" Visible="true">
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            Select Sheet
                                            <asp:Label ID="Label9" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSheets"
                                                ErrorMessage="*" ValidationGroup="4" InitialValue="0"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td valign="bottom">
                                        <label>
                                            <asp:DropDownList ID="ddlSheets" runat="server" ValidationGroup="4">
                                            </asp:DropDownList>
                                        </label>
                                        <label>
                                            <asp:Button ID="bnt123" runat="server" Text="Transfer Data" CssClass="btnSubmit"
                                                ValidationGroup="4" OnClick="bnt123_Click" />
                                        </label>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td style="width: 30%">
                                    <asp:Label ID="Label10" runat="server" Text="Label" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btnSubmit" ValidationGroup="1"
                                            OnClick="btnsubmit_Click" />
                                        <asp:Button ID="btnupdate" runat="server" OnClick="btnupdate_Click" Text="Update"
                                            Visible="False" ValidationGroup="1" CssClass="btnSubmit" />
                                        <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                            CausesValidation="False" CssClass="btnSubmit" />
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label18" runat="server" Text="List of Education Degrees"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnPrintVersion" runat="server" Text="Printable Version" OnClick="btnPrintVersion_Click"
                            CausesValidation="False" CssClass="btnSubmit" />
                        <input id="btnPrint" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" class="btnSubmit" />
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    Area of Study
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlareaaaa" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlareaaaa_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    Status
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlstatus_search" runat="server" Width="180px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlstatus_search_SelectedIndexChanged">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="lblCompany" runat="server" Font-Italic="True" Text="Area of Study : "></asp:Label>
                                                                <asp:Label ID="Label4" runat="server" Font-Italic="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label3" runat="server" Font-Italic="true" Text="List of Education Degrees "></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="text-align: left; font-size: 14px;">
                                                                <asp:Label ID="Label7" runat="server" Font-Italic="true" Text="Status : "></asp:Label>
                                                                <asp:Label ID="lblstat" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                                    OnRowEditing="GridView1_RowEditing" AllowSorting="True" OnSorting="GridView1_Sorting"
                                                    Width="100%" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                    PageSize="10" lternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" CssClass="mGrid"
                                                    EmptyDataText="No Record Found." OnRowDeleting="GridView1_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Area of Study" SortExpression="AreaofStudy" HeaderStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("AreaofStudy") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Level of Education" SortExpression="LevelofEducation"
                                                            HeaderStyle-HorizontalAlign="left" HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("LevelofEducation") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Degree Name" SortExpression="DegreeName" HeaderStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="40%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("DegreeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Labesdsdl6" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderText="Edit" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="Edit" ImageUrl="~/Account/images/edit.gif" ToolTip="Edit" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton54" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="Delete" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                    ImageUrl="~/Account/images/delete.gif" CausesValidation="False" ToolTip="Delete" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                </asp:Panel>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlsucedata" runat="server" Visible="false">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="lblvvv" runat="server" Text="You have successfully uploaded "></asp:Label>
                                                                <asp:Label ID="lblsucmsg" runat="server" Text=""></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="lblnoofrecord" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="Label24" runat="server" Text="Degree Name have been added. "></asp:Label>
                                                            </label>
                                                            <label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="lblnotimport" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="Label25" runat="server" Text=" records had errors in importing. "></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:Button ID="Button22" Text="View the List of Records with Errors" runat="server"
                                                                    CssClass="btnSubmit" Visible="False" OnClick="Button22_Click" /></label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlgrd" runat="server" Visible="false">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="grderrorlist" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                GridLines="Both" AllowPaging="True" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                AlternatingRowStyle-CssClass="alt" DataKeyNames="No" EmptyDataText="No Record Found."
                                                                OnPageIndexChanging="grderrorlist_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Row No." Visible="true" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="DegreeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblbsness" runat="server" Text='<%# Bind("DegreeName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprodname" runat="server" Text='<%# Bind("Active") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                             <%--    
                                                            <asp:TemplateField HeaderText="Adress Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="l1" runat="server" Text='<%# Bind("addresstype") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                                    <%--                <asp:TemplateField HeaderText="Contact Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpno" runat="server" Text='<%# Bind("contactname") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="barn" runat="server" Text='<%# Bind("designation") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                                  <%--  <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblssname" runat="server" Text='<%# Bind("") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>--%>
                                                                    <%-- <asp:TemplateField HeaderText="Country" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("country") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblst" runat="server" Text='<%# Bind("state") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="City" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwight" runat="server" Text='<%# Bind("city") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Phone" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("phone1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Zipcode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRate" runat="server" Text='<%# Bind("zip") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRatdfe" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                                    <%--                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRaffte" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                                <AlternatingRowStyle CssClass="alt" />
                                                            </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="Button11" runat="server" CssClass="btnSubmit" Text="Close" OnClick="Button11_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </td> </tr> </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnfileupload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
