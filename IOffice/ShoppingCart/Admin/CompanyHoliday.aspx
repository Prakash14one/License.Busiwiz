<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="CompanyHoliday.aspx.cs" Inherits="Add_Company_Holiday" %>

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
    <asp:UpdatePanel ID="pnnn1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="Label1" runat="server" Style="color: Red"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" Text="Add New Company Holiday" runat="server" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Width="100%" Visible="false">
                        <label>
                            <asp:Label ID="lblBusinessName" Text="Business Name" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlStore" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlStore_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="lblHolidayName" Text="Holiday Name" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHoliday"
                                ValidationGroup="1">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtHoliday"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtHoliday" MaxLength="50" runat="server" Width="280px" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'_.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'div1',30)"></asp:TextBox>
                             <asp:Label ID="lbbb" runat="server" Text="Max" CssClass="labelcount"></asp:Label> <span id="div1" class="labelcount">30</span>
                            <asp:Label ID="Label14" runat="server" Text="(A-Z 0-9)" CssClass="labelcount"></asp:Label>
                        </label>
                        <label class="first">
                            <asp:Label ID="lblHolidayDate" Text="Holiday Date" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdate"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rghjk" runat="server" ErrorMessage="*" ControlToValidate="txtdate"
                                ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtdate" runat="server" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgbtncal"
                                TargetControlID="txtdate">
                            </cc1:CalendarExtender>
                        </label>
                        <label>
                            <br />
                            <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label class="first">
                            <asp:Label ID="lblWorkSchedule" Text="Work schedule for the day" runat="server"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <asp:RadioButtonList ID="RdSchedule" runat="server" OnSelectedIndexChanged="RdSchedule_SelectedIndexChanged"
                            RepeatDirection="Horizontal" AutoPostBack="True">
                            <asp:ListItem Value="1" Selected="True">Full Holiday</asp:ListItem>
                            <asp:ListItem Value="0">Special work schedule</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Panel ID="pnlgrdholiday" runat="server" Width="500px" ScrollBars="Vertical"
                            Visible="true" Height="150px">
                            <asp:GridView ID="grdfullholiday" runat="server" AllowSorting="True"
                                AutoGenerateColumns="False" GridLines="Both" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found." 
                                Width="480px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Batch Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="90%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbusinessnamefullholiday" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                            <asp:Label ID="lblbatchmasteridfullholidayid" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="90%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkactivedesign123" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Panel ID="pnlgrdspecialholiday" runat="server" Width="500px" ScrollBars="Vertical"
                            Visible="false" Height="150px">
                            <asp:GridView ID="grdscheduleholiday" runat="server" AllowSorting="True"
                                AutoGenerateColumns="False" GridLines="Both" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found." 
                                Width="480px" 
                                onselectedindexchanged="grdscheduleholiday_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="Batch Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="50%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbusinessnamespecialschedule" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                            <asp:Label ID="lblbatchmasterid" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                            <asp:Label ID="lblwhid" runat="server" Text='<%# Eval("WHID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="50%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Batch Schedule" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="50%">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlbatchschedule" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="50%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkactivedesignspecialschedule123" runat="server" />
                                        
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                        <label class="first">
                            <asp:Label ID="lblschedule" runat="server" Text="Work schedule name" Visible="false"></asp:Label>
                            <asp:DropDownList ID="ddlschedule" runat="server" Visible="false">
                            </asp:DropDownList>
                        </label>
                        <div style="clear: both;">
                            <asp:GridView ID="grdscheduleholiday0" runat="server" AllowSorting="True" 
                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid" 
                                EmptyDataText="No Record Found." GridLines="Both" 
                                onselectedindexchanged="grdscheduleholiday_SelectedIndexChanged" 
                                PagerStyle-CssClass="pgr" Width="480px">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Batch Name" 
                                        ItemStyle-Width="50%" SortExpression="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbusinessnamespecialschedule0" runat="server" 
                                                Text='<%# Eval("Name")%>'></asp:Label>
                                            <asp:Label ID="lblbatchmasterid0" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                            <asp:Label ID="lblwhid0" runat="server" Text='<%# Eval("WHID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="50%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" 
                                        HeaderText="Batch Schedule" ItemStyle-Width="50%" SortExpression="Name">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlbatchschedule0" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="50%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Active" 
                                        ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkactivedesignspecialschedule124" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
                        </div>
                        <br />
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btnSubmit" ValidationGroup="1"
                            OnClick="ImageButton2_Click" />
                        <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click"
                            CssClass="btnSubmit" Visible="False" ValidationGroup="1" />
                        <asp:Button ID="ImageButton8" runat="server" Text="Cancel" CausesValidation="false" OnClick="ImageButton7_Click"
                            CssClass="btnSubmit" />
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllist" Text="List of Business Holidays" runat="server"></asp:Label></legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" OnClick="Button1_Click"
                            CssClass="btnSubmit" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" class="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="lblFilterBusiness" runat="server" Text="Filter by Business Name"></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label5" Text="From date" runat="server"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtstartdate1"
                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="10"></asp:RequiredFieldValidator>
                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtdate"
                                ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>--%>
                        <asp:TextBox ID="txtstartdate1" runat="server" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" PopupButtonID="ImageButton2"
                            TargetControlID="txtstartdate1">
                        </cc1:CalendarExtender>
                    </label>
                    <label>
                        <br />
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                    </label>
                    <label>
                        <asp:Label ID="Label6" Text="To date" runat="server"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txttodate"
                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="10"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txttodate" runat="server" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" PopupButtonID="ImageButton3"
                            TargetControlID="txttodate">
                        </cc1:CalendarExtender>
                    </label>
                    <label>
                        <br />
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                    </label>
                    <div>
                    <br />
                    <label>
                     <asp:Button ID="Button2" runat="server" Text="Go" CssClass="btnSubmit" 
                        ValidationGroup="10" onclick="Button2_Click" /></label>
                        </div>    
                  
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center;">
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="lblCompany" runat="server" Font-Size="20px" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Font-Size="20px" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Size="20px"></asp:Label>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="List of Business Holidays" Font-Size="18px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Holiday Name :" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                    <asp:Label ID="lblholiday" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                                        PageSize="15" AutoGenerateColumns="False" GridLines="Both" 
                                        CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" DataKeyNames="Company_Holiday_Id" EmptyDataText="No Record Found."
                                        Width="100%" OnSorting="GridView2_Sorting" OnPageIndexChanging="GridView2_PageIndexChanging"
                                        OnRowEditing="GridView2_RowEditing" onrowdeleting="GridView2_RowDeleting">
                                        <Columns>
                                         <asp:TemplateField HeaderText="Business Name" SortExpression="Name"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lnlwname" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Holiday Name" SortExpression="HolidayName"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblhname" runat="server" Text='<%# Eval("HolidayName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                         <%--   <asp:BoundField DataField="Name" HeaderText="Business Name" SortExpression="Name"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%" />--%>
                                          <%--  <asp:BoundField DataField="HolidayName" HeaderText="Holiday Name" SortExpression="HolidayName"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="30%" />
                                        --%>   
                                      
                                         <asp:TemplateField HeaderText="Special Work Schedule Name" SortExpression="ScheduleName"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblspecialschedulename123" runat="server" Text='<%# Eval("ScheduleName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Full Holiday?" SortExpression="holiday" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfullholiday" runat="server" Text='<%# Eval("holiday")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                              <asp:TemplateField HeaderText="Date" SortExpression="Date"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldd" runat="server" Text='<%# Eval("Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          <%--  <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="7%" />--%>
                                            <asp:TemplateField HeaderText="Edit1" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left"
                                                HeaderImageUrl="~/Account/images/edit.gif">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Edit"  OnClick="edit_Click" CommandArgument='<%# Eval("Company_Holiday_Id") %>'
                                                        CommandName="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
            <!--end of right content-->
            <div style="clear: both;">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
