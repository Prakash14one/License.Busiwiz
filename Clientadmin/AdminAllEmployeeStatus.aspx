<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="AdminAllEmployeeStatus.aspx.cs" Inherits="Employee_Work_Status" %>

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
            height: 28px;
        }
    </style>
    <%--   <script language="javascript" type="text/javascript">


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
    </script>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="right_content">
                <div id="ctl00_ucTitle_PNLTITLE">
                    <div class="divHeaderLeft">
                        <div style="float: left; width: 50%;">
                            <h2>
                                <span id="ctl00_ucTitle_lbltitle">Employee Work Report- Add,Manage</span>
                            </h2>
                        </div>
                        <div style="float: right; width: 50%">
                            <div id="ctl00_ucTitle_pnlshow">
                            </div>
                        </div>
                    </div>
                </div>
                <div style="clear: both;">
                </div>
                <div id="ctl00_ucTitle_pnlhelp" style="border: solid 1px black">
                    <h3>
                        <span id="ctl00_ucTitle_lblDetail" style="font-family: Tahoma; font-size: 7pt; font-weight: bold;">
                            Employee Work Report- Add,Manage </span>
                    </h3>
                </div>
            </div>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
          
                <fieldset>
                    <legend>
                        <asp:Label ID="Label30" runat="server" Text="List of Employee Work Status"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="3" class="style1">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="right">
                                <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version" />
                                <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    style="width: 51px;" type="button" value="Print" visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <asp:Button ID="Button5" runat="server" Text="Select Display Columns" CssClass="btnSubmit"
                                    OnClick="Button5_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div style="float: left;">
                                    <asp:Panel Width="100%" ID="Panel6" runat="server" Visible="False">
                                        <div>
                                            <asp:CheckBox ID="chkdept" runat="server" Checked="false" />
                                            <label>
                                                <asp:Label ID="lbldept" runat="server" Text="Dept Name"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chkdate" runat="server" Checked="false" />
                                            <label>
                                                <asp:Label ID="lblchkdate" runat="server" Text="MonthlyGoal"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chkemployee" runat="server" Checked="false" />
                                            <label>
                                                <asp:Label ID="lblchkemployee" runat="server" Text="WeeklyGoal"></asp:Label>
                                            </label>
                                           <%-- <asp:CheckBox ID="chkproduct" runat="server" Checked="false" />--%>
                                           <%-- <label>
                                                <asp:Label ID="lblchkproduct" runat="server" Text="Project Name"></asp:Label>
                                            </label>--%>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    <asp:Label ID="Label9" runat="server" Text="Filter By Date:"></asp:Label>
                                </label>
                                <label>
                                    <asp:TextBox ID="txtsortdate" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtsortdate_TextChanged">
                                    </asp:TextBox>
                                    <cc1:CalendarExtender ID="calsortdate" runat="server" PopupButtonID="ImageButton3"
                                        TargetControlID="txtsortdate">
                                    </cc1:CalendarExtender>
                                </label>
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Filter By Employee :"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" Width="180px"
                                        OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="Gray" Width="600"
                                    BorderStyle="Solid" BorderWidth="5px">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="height: 9px">
                                                &nbsp;
                                            </td>
                                            <td align="right" style="height: 9px">
                                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                                    Width="16px" Height="16px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    You will need to add our Monthlygoal which allows download of Busicontroller for
                                                    your Product. Busicontroller will allow to regulate your license terms with your
                                                    customer.</label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td colspan="2">
                                                <asp:Button ID="Button13" runat="server" Text="OK" CssClass="btnSubmit" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Button ID="Button11" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel2" TargetControlID="Button11">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <%--    <div id="mydiv" class="closed">
                                                    <table width="850Px">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                               
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of MonthlyGoal"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdWeeklygoal" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                                                    AllowSorting="True" AllowPaging="true" PageSize="10"
                                                    Width="100%" EmptyDataText="No Record Found." OnRowCommand="grdWeeklygoal_RowCommand"
                                                    OnRowDeleting="grdWeeklygoal_RowDeleting" OnRowEditing="grdWeeklygoal_RowEditing"
                                                    OnRowUpdating="grdWeeklygoal_RowUpdating" 
                                                    onpageindexchanging="grdWeeklygoal_PageIndexChanging">
                                                    <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                                                    
                                                       
                                                        <%--  
                                                          <asp:BoundField DataField="DailyGoal_dailyWorksStatus" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Work Status" ItemStyle-Width="30%" SortExpression="DailyGoal_dailyWorksStatus">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="30%" />
                                                        </asp:BoundField>--%>
                                                        <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Dept Name"
                                                            ItemStyle-Width="8%" SortExpression="Name">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="8%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Name1" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%"
                                                            HeaderText="Employee Name" SortExpression="Name1">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="15%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MonthlyGoalMaster_MonthlyGoalTitle" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Monthly Goal" ItemStyle-Width="15%" SortExpression="MonthlyGoalMaster_MonthlyGoalTitle">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="15%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="WeeklyGoalMaster_Title" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Weekly Goal" ItemStyle-Width="15%" SortExpression="WeeklyGoalMaster_Title">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="15%" />
                                                        </asp:BoundField>
                                                      <%-- 20-11 <asp:BoundField DataField="ProjectMaster_ProjectTitle" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Project Title" ItemStyle-Width="30%" SortExpression="WeeklyGoalMaster_Week">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="30%" />
                                                        </asp:BoundField>--%>
                                                        <asp:BoundField DataField="DailyGoal_date" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%"
                                                            HeaderText="Date" SortExpression="DailyGoal_date" DataFormatString="{0:dd.MM.yyyy}">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DailyGoal_dailywoktitle" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="15%" HeaderText="Task" SortExpression="DailyGoal_dailywoktitle">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="15%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DailyGoal_dailyworksreport" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="5%" HeaderText="TaskReport" SortExpression="DailyGoal_dailyworksreport">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="DailyDailyGoal_BudgetHour" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="5%" HeaderText="BudgetHour" SortExpression="DailyGoal_dailyworksreport">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="DailyDailyGoal_ActualHour" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="5%" HeaderText="ActualHour" SortExpression="DailyGoal_dailyworksreport">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DailyDailyGoal_RequestedHour" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="5%" HeaderText="RequestedHour" SortExpression="DailyDailyGoal_RequestedHour">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DailyGoal_dailyWorksStatus" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="TaskStatus" ItemStyle-Width="30%" SortExpression="DailyGoal_dailyWorksStatus">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="30%" />
                                                        </asp:BoundField>
                                                        <%--<asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Account/images/edit.gif"
                                                                    CommandName="Edit" CommandArgument='<%# Eval("DailyGoal_Id") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="3%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgdelete" ToolTip="Delete" runat="server" ImageUrl="~/Account/images/delete.gif"
                                                                    CommandName="Delete" CommandArgument='<%# Eval("DailyGoal_Id") %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>
                                                        <%--<asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Account/images/edit.gif"
                                                                    CommandName="Edit" CommandArgument='<%# Eval("EmpDaily_Id") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="3%" />
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgdelete" ToolTip="Delete" runat="server" ImageUrl="~/Account/images/delete.gif"
                                                                    CommandName="Delete" CommandArgument='<%# Eval("EmpDaily_Id") %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                </asp:GridView>
                                            </td>
                                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
