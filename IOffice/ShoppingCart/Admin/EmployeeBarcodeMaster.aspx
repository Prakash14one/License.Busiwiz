<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="EmployeeBarcodeMaster.aspx.cs" Inherits="Add_Employee_Barcode" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <div class="products_box">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                </div>
                <asp:Panel ID="pnlhideshow" runat="server" Width="100%" Visible="false">
                    <fieldset>
                        <legend>
                            <asp:Label ID="lbladd" runat="server"></asp:Label>
                        </legend>
                        <div style="float: right;">
                            <asp:Button ID="btnshowbarcode" runat="server" Text="Add Barcode" CssClass="btnSubmit"
                                OnClick="btnshowbarcode_Click" Visible="False" />
                        </div>
                        <div style="clear: both;">
                        </div>
                        <table width="100%">
                            <tr>
                                <td width="25%">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td width="75%" colspan="2">
                                    <label>
                                        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>--%>
                                        <asp:Label ID="Label2" runat="server" Text="Employee Name"></asp:Label>
                                    </label>
                                </td>
                                <td width="20%">
                                    <label>
                                        <asp:DropDownList ID="DropDownList2" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td width="55%">
                                    <label>
                                        <asp:ImageButton ID="ImageButton49" runat="server" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                            Width="20px" AlternateText="Add New" Height="20px" ToolTip="AddNew" OnClick="ImageButton49_Click" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton48" runat="server" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                            AlternateText="Refresh" Height="20px" Width="20px" ToolTip="Refresh" OnClick="ImageButton48_Click" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label20" runat="server" Text="User Name"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lblusername" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Barcode"></asp:Label>
                                        <asp:Label ID="Label12" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                            ControlToValidate="TextBox1" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'_.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'div1',50)"></asp:TextBox>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="div1" class="labelcount">50</span>
                                        <asp:Label ID="Label14" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <%--  <tr>
                                <td colspan="3">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Valid Barcode, E.g: 1 char and 1 digit with minimum 15 Characters are must"
                                        ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{15,20})$" ControlToValidate="TextBox1"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                </td>
                            </tr>--%>
                            <tr valign="top">
                                <td valign="top">
                                    <label class="first">
                                        <asp:Label ID="Label15" runat="server" Text="Employee Number"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtempnumber" runat="server" Enabled="False"></asp:TextBox>
                                    </label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <label class="first">
                                        <asp:Label ID="Label7" runat="server" Text="Employee Code"></asp:Label>
                                        <asp:Label ID="Label16" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtemployeecode"
                                            ErrorMessage="*" ValidationGroup="1">
                                        </asp:RequiredFieldValidator>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtemployeecode" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtemployeecode" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                            Enabled="false" onkeyup="return check(this,/[\\/!@#$%^'_.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span1',20)"></asp:TextBox>
                                    </label>
                                </td>
                                <td>
                                    <%-- <label>
                                        <asp:Label ID="Label20"  CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span1" class="labelcount">20</span>
                                        <asp:Label ID="Label15"  CssClass="labelcount"  runat="server" Text="(A-Z 0-9)"></asp:Label>
                                    </label>--%>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <label class="first">
                                        <asp:Label ID="Label8" runat="server" Text="Bio Metric ID"></asp:Label>
                                        <asp:Label ID="Label17" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtbiometricid"
                                            ErrorMessage="*" ValidationGroup="1">
                                        </asp:RequiredFieldValidator>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtbiometricid" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtbiometricid" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'_.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span2',20)">
                                        </asp:TextBox>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label21" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span2" class="labelcount">20</span>
                                        <asp:Label ID="Label13" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <label class="first">
                                        <asp:Label ID="Label9" runat="server" Text="Bluetooth ID"></asp:Label>
                                        <asp:Label ID="Label18" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtbiometricid"
                                            ErrorMessage="*" ValidationGroup="1">
                                        </asp:RequiredFieldValidator>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtbluetoothid" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtbluetoothid" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'_.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span3',20)">
                                        </asp:TextBox>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label22" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="Span3" class="labelcount">20</span>
                                        <asp:Label ID="Label10" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Status">
                                        </asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlstatus" runat="server" Width="100px">
                                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="2">
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Submit" ValidationGroup="1"
                                        OnClick="Button3_Click" Visible="False" />
                                    <%--<asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button2"
                        CssClass="btnSubmit" Text="Submit" />--%>
                                    <asp:Button ID="btnupdate" runat="server" CssClass="btnSubmit" Text="Update" ValidationGroup="1"
                                        OnClick="btnupdate_Click" Visible="False" />
                                    <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" OnClick="Button4_Click"
                                        Text="Cancel" />
                                    <%--<asp:Button runat="server" ID="Button3" CssClass="btnSubmit" Text="Cancel" />--%>
                                </td>
                            </tr>
                        </table>
                        <div style="clear: both;">
                        </div>
                    </fieldset></asp:Panel>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label6" runat="server" Text="List of Employee Barcodes"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" CssClass="btnSubmit" runat="server" Text="Printable Version"
                            OnClick="Button1_Click" />
                        <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="False" />
                        <%--<asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button1"
                            CssClass="btnSubmit" Text="Print Version" />--%>
                    </div>
                    <label>
                        <asp:Label ID="Label5" runat="server" Text="Filter by Business Name"></asp:Label>
                        <asp:DropDownList ID="ddshorting" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddshorting_SelectedIndexChanged">
                        </asp:DropDownList>
                        <%--<select>
                        <option>EPlaza Store</option>
                        <option>EPlaza Store</option>
                        <option>EPlaza Store</option>
                    </select>--%>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblCompany" Font-Size="20px" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label19" runat="server" Font-Size="20px" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label11" runat="server" Text="List of Employee's Barcode" Font-Size="18px"></asp:Label>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                            <td align="left">
                                                <asp:Label ID="Label7" runat="server" Font-Size="16px" Text="Business Name :"></asp:Label>
                                                <asp:Label ID="lblfilterbusi" runat="server" Font-Size="16px"></asp:Label>
                                            </td>
                                        </tr>--%>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="grdTempData" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" DataKeyNames="EmpBarcodeMasterID"
                                        OnRowCommand="grdTempData_RowCommand" OnRowDeleting="grdTempData_RowDeleting"
                                        OnRowEditing="grdTempData_RowEditing" AllowSorting="True" OnPageIndexChanging="grdTempData_PageIndexChanging"
                                        OnSorting="grdTempData_Sorting" EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="Name" ItemStyle-Width="18%"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstoreId" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="18%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="18%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblid" runat="server" Text='<%#Bind("EmployeeName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name" SortExpression="username" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label62" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Barcode" SortExpression="Barcode" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaxYear_id" runat="server" Text='<%#Bind("Barcode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Code" SortExpression="Employeecode" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="18%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemployeecode123" runat="server" Text='<%#Bind("Employeecode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="18%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Number" SortExpression="EmployeeNo" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label61" runat="server" Text='<%# Eval("EmployeeNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bio Metric ID" SortExpression="Biometricno" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="18%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemployebionetricid123" runat="server" Text='<%#Bind("Biometricno") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="18%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bluetooth ID" SortExpression="blutoothid" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="18%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemployebluetooth123" runat="server" Text='<%#Bind("blutoothid") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="18%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="Active" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:Label ID="chkAcive" runat="server" Text='<%#Bind("Active")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnedit" runat="server" CommandName="Edit" ImageUrl="~/Account/images/edit.gif"
                                                        ToolTip="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');"></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle VerticalAlign="Top" Width="3%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <%--<asp:GridView ID="gvCustomres" runat="server" DataSourceID="customresDataSource"
                    AutoGenerateColumns="False" GridLines="None" AllowPaging="true" CssClass="mGrid"
                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                    <Columns>
                        <asp:BoundField DataField="ContactName" HeaderText="Business Name" />
                        <asp:BoundField DataField="PricePlan" HeaderText="Employee Name" />
                        <asp:BoundField DataField="PricePlan" HeaderText="Barcode" />
                        <asp:CheckBoxField DataField="Checkbox" HeaderText="Status" />
                        <asp:HyperLinkField Text="Edit" HeaderText="Edit" />
                        <asp:HyperLinkField Text="Delete" HeaderText="Delete" />
                    </Columns>
                </asp:GridView>--%>
                    <%--<asp:XmlDataSource ID="customresDataSource" runat="server" DataFile="~/App_Data/data1.xml">
                </asp:XmlDataSource>--%>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--end of right content-->
    <div style="clear: both;">
    </div>
</asp:Content>
