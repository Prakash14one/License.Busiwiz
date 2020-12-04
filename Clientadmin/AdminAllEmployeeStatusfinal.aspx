<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="AdminAllEmployeeStatusfinal.aspx.cs" Inherits="Employee_Work_Status" %>

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
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        } 
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%--<div id="right_content">
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
            </div>--%>
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
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3" align="right">
                              <%--  <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" 
                                    Text="Printable Version" onclick="Button3_Click" />
                                     <input id="Button1" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false"  />--%>
                              <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" 
                            Text="Printable Version" onclick="Button3_Click" />
                        <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false"  />
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
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                               <table width="100%">
                                <div id="mydiv">
                                                
                                                    
                                                    <tr>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                          
                                                        <td width="25%">
                                                            <asp:Label ID="Label34" runat="server" BorderStyle="None" Font-Size="Larger" 
                                                                ForeColor="Black" Text="Employee Work Status Report"></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                                 &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                    </tr>
                                          
                                                    <tr>
                                                        <td colspan="4">
                                                        <div style="float: left;">
                                    <asp:Panel Width="100%" ID="Panel6" runat="server" Visible="False">
                                        <div>
                                            <asp:CheckBox ID="chkdept" runat="server" Checked="false" AutoPostBack="True" 
                                                oncheckedchanged="chkdept_CheckedChanged" />
                                            <label>
                                                <asp:Label ID="lbldept" runat="server" Text="Dept Name"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chkdate" runat="server" Checked="false" AutoPostBack="True" 
                                                oncheckedchanged="chkdate_CheckedChanged" />
                                            <label>
                                                <asp:Label ID="lblchkdate" runat="server" Text="MonthlyGoal"></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="chkemployee" runat="server" Checked="false" 
                                                AutoPostBack="True" oncheckedchanged="chkemployee_CheckedChanged" />
                                            <label>
                                                <asp:Label ID="lblchkemployee" runat="server" Text="WeeklyGoal"></asp:Label>
                                           </label>
                                           <%--<label>
                                           <asp:CheckBox ID="chkproduct" runat="server" Checked="false" />
                                           </label>
                                           <labeL>
                                                <asp:Label ID="lblchkproduct" runat="server" Text="Project Name"></asp:Label>
                                            </label>--%>
                                        </div>
                                   </asp:Panel>
                                </div>
                                                          </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            <asp:Label ID="Label9" runat="server" Text="Filter By Date From:"></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtsortdate" runat="server" AutoPostBack="true" 
                                                                OnTextChanged="txtsortdate_TextChanged" Width="100px">
                                    </asp:TextBox>
                                                            <cc1:CalendarExtender ID="calsortdate" runat="server" 
                                                                PopupButtonID="ImageButton3" TargetControlID="txtsortdate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:Label ID="Label2" runat="server" Text="Filter By Date To:"></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txttodate" runat="server" AutoPostBack="true" 
                                                                OnTextChanged="txttodate_TextChanged" Width="100px">
                                    </asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                                PopupButtonID="ImageButton3" TargetControlID="txttodate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            <asp:Label ID="Label6" runat="server" Text="Filter By Department :"></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:DropDownList ID="ddlsortdept" runat="server" AutoPostBack="True" 
                                                                OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="180px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:Label ID="Label1" runat="server" Text="Filter By Employee :"></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" 
                                                                OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" Width="180px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            <asp:Label ID="lblMonthlyGoal" runat="server" Text="Filter By Monthly Goal :"></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:DropDownList ID="ddlmonthlygoal" runat="server" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlmonthlygoal_SelectedIndexChanged" Width="180px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:Label ID="lblWeeklyGoal" runat="server" Text="Filter By Weekly Goal : "></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:DropDownList ID="ddlWeeklyGoal" runat="server" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlWeeklyGoal_SelectedIndexChanged" Width="180px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                        <td width="25%">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            <asp:Label ID="Label3" runat="server" Text="Filter By Task Status : "></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:DropDownList ID="ddltask" runat="server" AutoPostBack="True" 
                                                                onselectedindexchanged="ddltask_SelectedIndexChanged" Width="180px">
                                                                <asp:ListItem>---Select All---</asp:ListItem>
                                                                <asp:ListItem>Complete</asp:ListItem>
                                                                <asp:ListItem>Pending</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="25%">
                                                        </td>
                                                        <td width="25%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:GridView ID="grdWeeklygoal" runat="server" AllowPaging="True" 
                                                                AllowSorting="True" AlternatingRowStyle-CssClass="alt" 
                                                                AutoGenerateColumns="False" CssClass="mGrid" EmptyDataText="No Record Found." 
                                                                onpageindexchanging="grdWeeklygoal_PageIndexChanging" 
                                                                OnRowCommand="grdWeeklygoal_RowCommand" 
                                                                OnRowDeleting="grdWeeklygoal_RowDeleting" 
                                                                OnRowEditing="grdWeeklygoal_RowEditing" 
                                                                OnRowUpdating="grdWeeklygoal_RowUpdating" PagerStyle-CssClass="pgr" 
                                                                Width="100%">
                                                                <AlternatingRowStyle CssClass="alt" />
                                                                <Columns>
                                                          
                                                                    <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" 
                                                                        HeaderText="Dept Name" SortExpression="Name">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Name1" HeaderStyle-HorizontalAlign="Left" 
                                                                        HeaderText="Employee" SortExpression="Name1">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="MonthlyGoalMaster_MonthlyGoalTitle" 
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Monthly Goal" 
                                                                        SortExpression="MonthlyGoalMaster_MonthlyGoalTitle">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="WeeklyGoalMaster_Title" 
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Weekly Goal" 
                                                                        SortExpression="WeeklyGoalMaster_Title">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DailyGoal_date" DataFormatString="{0:dd.MM.yyyy}" 
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Date" 
                                                                        SortExpression="DailyGoal_date">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DailyGoal_dailywoktitle" 
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Task" 
                                                                        SortExpression="DailyGoal_dailywoktitle">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DailyGoal_dailyworksreport" 
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Task Report" 
                                                                        SortExpression="DailyGoal_dailyworksreport">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="superwiserreview" HeaderText="Supervisor Review" />
                                                                    <asp:BoundField DataField="DailyDailyGoal_BudgetHour" 
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Bud Hr" 
                                                                        SortExpression="DailyGoal_dailyworksreport">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DailyDailyGoal_ActualHour" 
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Act Hr" 
                                                                        SortExpression="DailyGoal_dailyworksreport">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DailyDailyGoal_RequestedHour" 
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Req Hr" 
                                                                        SortExpression="DailyDailyGoal_RequestedHour">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DailyGoal_dailyWorksStatus" 
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Task Status" 
                                                                        SortExpression="DailyGoal_dailyWorksStatus">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                               
                                                </div>
                                                </table>
                                                </asp:Panel>
                                            </td>
                                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                        </tr>
                               
                                <asp:Button ID="Button11" runat="server" Style="display: none" />
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
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
                               <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel2" TargetControlID="Button11">
                                </cc1:ModalPopupExtender>    
                               
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
