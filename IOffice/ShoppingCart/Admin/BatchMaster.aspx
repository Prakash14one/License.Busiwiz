<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="BatchMaster.aspx.cs" Inherits="Add_Batch_Master"
    Title="Batch Master Add/Manage" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<ajaxtoolkit:toolkitscriptmanager runat="server" ID="ScriptManager1" />--%>
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
            counter = document.getElementById(id);
            alert(counter);
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnaddbatchmaster" Text="Add New Batch" runat="server" CssClass="btnSubmit"
                            OnClick="btnaddbatchmaster_Click" />
                    </div>
                    <asp:Panel ID="AddBatch" Visible="false" runat="server">
                        <label>
                            <asp:Label ID="lblBusinessname" runat="server" Text="Business Name"></asp:Label>
                            <asp:DropDownList ID="ddlstrname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstrname_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="lblBatchName" runat="server" Text="Batch Name"></asp:Label>
                            <asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttimename"
                                ErrorMessage="*" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                ControlToValidate="txttimename" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txttimename" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'div1',20)"
                                ValidationGroup="Submit"></asp:TextBox>
                            <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="div1" class="labelcount">20</span>
                            <asp:Label ID="lblbf" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ )"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <%--<label>
                            <asp:Label ID="Label7" runat="server" Text="Is this a default batch?"></asp:Label>
                        </label>--%>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label8" runat="server" Text="Do you wish to make this batch a default for the business, a department, or a designation?"></asp:Label>
                        </label>
                        <asp:CheckBox ID="chkyes" runat="server" Text="Yes" TextAlign="Left" AutoPostBack="true"
                            OnCheckedChanged="chkyes_CheckedChanged" />
                        <div style="clear: both;">
                        </div>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" Visible="false" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                            AutoPostBack="True">
                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                            <asp:ListItem Value="1" Text=""></asp:ListItem>
                            <asp:ListItem Value="2" Text=""></asp:ListItem>
                            <%--<asp:ListItem Value="3" Selected="True">For None</asp:ListItem>--%>
                        </asp:RadioButtonList>
                        <div style="clear: both">
                        </div>
                        <asp:Panel ID="Panel1" runat="server" Width="100%">
                        </asp:Panel>
                        <asp:Panel ID="pnldepartment" runat="server" Width="100%" Visible="false">
                            <table style="width: 100%" cellspacing="2">
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server" Width="100%">
                        </asp:Panel>
                        <asp:Panel ID="pnldesignation" runat="server" Width="100%" Visible="false">
                            <table style="width: 100%" cellspacing="2">
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <%--<asp:Button ID="btnDepartment" runat="server" Text="Department Popup" />
                    <asp:Button ID="btnDesignation" runat="server" Text="Designation Popup" />--%>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="lblBatchTimeZone" Text="Batch Time Zone" runat="server"></asp:Label>
                            <asp:Label ID="Label9" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddltimezone"
                                ErrorMessage="*" ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddltimezone" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label27" runat="server" Text="Status"></asp:Label>
                            <asp:DropDownList ID="ddlstatus" runat="server" Width="100px">
                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <%--<asp:CheckBox runat="server" ID="chkactive" Checked="true" Text="Status" />--%>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Effective Start Date">
                                        </asp:Label>
                                        <%--<asp:Label ID="Label25" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValsdfidator4" runat="server"
                                            ErrorMessage="*" ControlToValidate="txtstartdate" ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$">
                                        </asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtstartdate"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text="Effective End Date"></asp:Label>
                                        <%--<asp:Label ID="Label26" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                        <asp:RegularExpressionValidator ID="rghjk" runat="server" ErrorMessage="*" ControlToValidate="txtenddate"
                                            ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtenddate"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtstartdate" runat="server" Width="75px" MaxLength="10"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton2"
                                            Format="MM/dd/yyyy" TargetControlID="txtstartdate">
                                        </cc1:CalendarExtender>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtenddate" runat="server" Width="75px" MaxLength="10"></asp:TextBox>
                                        <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />--%>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton3"
                                            Format="MM/dd/yyyy" TargetControlID="txtenddate">
                                        </cc1:CalendarExtender>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <div style="clear: both;">
                        </div>
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Would you like to set up batch timings after submitting your batch name? (This is recommended)"
                            TextAlign="Right" Checked="true" />
                        <div style="clear: both;">
                        </div>
                        <br />
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                        <asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="btnsubmit"
                            CssClass="btnSubmit" Text="Submit" OnClick="ImageButton2_Click" />
                        <asp:Button ID="btnupdate" CssClass="btnSubmit" Text="Update" runat="server" OnClick="ImageButton48_Click"
                            ValidationGroup="Submit" />
                        <asp:Button ID="btncancel" CssClass="btnSubmit" Text="Cancel" runat="server" OnClick="ImageButton7_Click" />
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Batches" runat="server"></asp:Label></legend>
                    <label>
                        <asp:Label ID="lblSelectByBusiness" Text="Filter by Business Name" runat="server"></asp:Label>
                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <div style="float: right;">
                        <asp:Button ID="Button2" runat="server" Text="Printable Version" OnClick="Button2_Click"
                            CssClass="btnSubmit" />
                        <input id="Button1" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="lblCompany" runat="server" Font-Size="20px" ForeColor="Black" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Font-Size="18px" ForeColor="Black" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Size="18px" ForeColor="Black"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Font-Size="18px" ForeColor="Black" Font-Italic="true" Text="List of Batches "></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                        GridLines="Both" AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" OnRowDeleting="GridView1_RowDeleting" AllowSorting="True"
                                        OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting"
                                        EmptyDataText="No Record Found." Width="100%" PageSize="20">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="WName" ItemStyle-Width="12%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("WName") %>'></asp:Label>
                                                    <asp:Label ID="lblbatchid" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch Name" SortExpression="Name" ItemStyle-Width="12%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch Time Zone" ItemStyle-Width="25%" SortExpression="TimeFormat"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("TimeFormat") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Schedule/Start Time/End Time" ItemStyle-Width="30%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblschedule" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="Status" ItemStyle-Width="5%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="chkact" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-Width="2%" HeaderImageUrl="~/Account/images/edit.gif"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton4" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                        OnClick="LinkButton4_Click" ImageUrl="~/Account/images/edit.gif" ToolTip="Edit" />
                                                    <%--<asp:LinkButton ID="LinkButton4" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                            OnClick="LinkButton4_Click" Text="Edit"></asp:LinkButton>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:CommandField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg" DeleteImageUrl="~/Account/images/delete.gif" ButtonType="Image" ShowDeleteButton="True" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Left" />--%>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
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
            <!--end of right content-->
            <div style="clear: both;">
            </div>
            <asp:Panel ID="Panel21" runat="server" Width="620px" Height="300px">
                <div style="background-color: White;">
                    <div style="clear: both">
                    </div>
                    <asp:Panel ID="pnlof" runat="server" CssClass="modalPopup" ScrollBars="Auto">
                        <div>
                            <div style="float: right">
                                <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                    Width="15px" />
                            </div>
                            <label>
                                <asp:Label ID="lblbusidept" runat="server" Text=""></asp:Label>
                            </label>
                            <div style="clear: both">
                            </div>
                            <fieldset style="border: 1px solid white;">
                                <legend style="color: Black">
                                    <asp:Label ID="lblselectdepartment" Text="Select Department" runat="server">
                   
                                    </asp:Label></legend>
                                <asp:GridView ID="grddepartment" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    DataKeyNames="id" Width="100%" EmptyDataText="No Record Found.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Department Name" SortExpression="Departmentname" ItemStyle-Width="70%"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldepartmentname123" runat="server" Text='<%# Eval("Departmentname") %>'></asp:Label>
                                                <asp:Label ID="lbldepartmasterid" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="90%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select for Default Batch" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkactive123" runat="server" Checked="true" />
                                                <%--<asp:DropDownList ID="ddlstatusdept" runat="server" Width="100px">
                                                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                                </asp:DropDownList>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </fieldset>
                        </div>
                        <div style="clear: both">
                        </div>
                        <div style="text-align: center">
                            <asp:Button ID="btnsubmitpop1" runat="server" Text="Submit" OnClick="btnsubmitpop1_Click"
                                ValidationGroup="Submit" CssClass="btnSubmit" />
                            <asp:Button ID="btnupdatepop1" runat="server" Text="Update" OnClick="btnupdatepop1_Click"
                                Visible="False" ValidationGroup="Submit" CssClass="btnSubmit" />
                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupExtenderAddnew" runat="server" Enabled="True"
                PopupControlID="Panel21" CancelControlID="ImageButton4" TargetControlID="HiddenButton222">
            </cc1:ModalPopupExtender>
            <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
            <asp:Panel ID="Panel3" runat="server" Width="720px" Height="300px">
                <div style="background-color: White;">
                    <div style="clear: both">
                    </div>
                    <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" ScrollBars="Auto">
                        <div>
                            <div style="float: right">
                                <asp:ImageButton ID="ImageButton1" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                    Width="15px" />
                            </div>
                            <label>
                                <asp:Label ID="lbldesibusi" runat="server" Text=""></asp:Label>
                            </label>
                            <div style="clear: both">
                            </div>
                            <fieldset style="border: 1px solid white;">
                                <legend style="color: Black">Select Designation</legend>
                                <asp:GridView ID="grddesignation" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Department Name" ItemStyle-Width="35%" SortExpression="Departmentname"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldepartmentname123123" runat="server" Text='<%# Eval("Departmentname") %>'></asp:Label>
                                                <asp:Label ID="lbldepartmasterid123132" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation Name" ItemStyle-Width="35%" SortExpression="Departmentname"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldesignationname123" runat="server" Text='<%# Eval("DesignationName") %>'></asp:Label>
                                                <asp:Label ID="lbldesignationmasterid" runat="server" Text='<%# Eval("DesignationMasterId") %>'
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select for Default Batch" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkactivedesign123" Checked="true" runat="server" />
                                                <%-- <asp:DropDownList ID="ddlstatusdesi" runat="server" Width="80px">
                                                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                                </asp:DropDownList>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div style="text-align: center">
                                    <asp:Button ID="btnsubmitpop2" CssClass="btnSubmit" runat="server" Text="Submit"
                                        OnClick="btnsubmitpop2_Click" ValidationGroup="Submit" />
                                    <asp:Button ID="btnupdatepop2" runat="server" Text="Update" OnClick="btnupdatepop2_Click"
                                        Visible="False" CssClass="btnSubmit" ValidationGroup="Submit" />
                                </div>
                            </fieldset>
                            <div style="clear: both">
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                Enabled="True" PopupControlID="Panel3" CancelControlID="ImageButton1" TargetControlID="Button3">
            </cc1:ModalPopupExtender>
            <asp:Button ID="Button3" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
