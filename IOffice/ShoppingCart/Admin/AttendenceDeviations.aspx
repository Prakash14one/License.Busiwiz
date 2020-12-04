<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="AttendenceDeviations.aspx.cs" Inherits="Add_Attendence_Deviations" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script runat="server">

   
</script>

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

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }  
    </script>

    <asp:UpdatePanel ID="pnnn1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="statuslable" runat="server" Text="" Style="color: #CC0000"></asp:Label>
                </div>
                <fieldset>
                    <%--  <legend>Add Employee Payroll</legend>--%>
                    <asp:RadioButtonList ID="rdlist" runat="server" OnSelectedIndexChanged="rdlist_SelectedIndexChanged"
                        RepeatDirection="Horizontal" AutoPostBack="True">
                        <asp:ListItem Selected="True" Value="0">Single employee by pay period</asp:ListItem>
                        <asp:ListItem Value="1">All employees by date</asp:ListItem>
                    </asp:RadioButtonList>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlpay"  runat="server">
                   <table >
                    <tr>
                    <td>
                      <label>
                            <asp:Label ID="lblBatchName" Text="Batch" runat="server"></asp:Label>
                            </label>  
                              </td> 
                       <td>  <label>
                            <asp:DropDownList ID="ddbatch" runat="server" AutoPostBack="True" 
                            Width="350px" OnSelectedIndexChanged="ddbatch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                      </td> 
                       <td>
                        <label>
                            <asp:Label ID="lblEmployee" Text="Employee Name" runat="server"></asp:Label>
                             </label>  
                               </td> 
                          <td> 
                           <label>
                            <asp:DropDownList ID="ddlemp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlemp_SelectedIndexChanged"
                                Width="240px">
                            </asp:DropDownList>
                        </label>
                    
                  </td> 
                  <td>
                       <label>
                            <asp:Label ID="lblPayPeriod" Text="Pay Period" runat="server"></asp:Label>
                             </label>    </td>  
                     <td>  <label>
                            <asp:DropDownList ID="ddlpayperiod" runat="server"
                                Width="240px">
                            </asp:DropDownList>
                        </label>
                     
                </td>
                      <td >
                    
                       <label>
                      
                          <asp:Button ID="Button1" runat="server" Text=" Go " CssClass="btnSubmit" OnClick="btnfilter_Click" 
                        ValidationGroup="1" />
                      </label>
                    </td>
                    </tr>
                   </table>
                    
                    
                    
                    
                    
                    
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnldatewise" runat="server">
                      <table >
                    <tr>
                    <td>
                      <label>
                            <asp:Label ID="lblBatchDate" Text="Batch" runat="server"></asp:Label>
                            </label>
                            </td><td>
                            <label>
                              <asp:DropDownList ID="ddldatebatch" runat="server" AutoPostBack="True" 
                            OnSelectedIndexChanged="ddldatebatch_SelectedIndexChanged" Width="340px">
                            </asp:DropDownList>
                            </label>
                    </td>
                      <td>
                       <label>
                            <asp:Label ID="lblEmployee0" runat="server" Text="Employee Name"></asp:Label>
                           
                        </label>
                      </td>
                      
                          <td>
                          <label> <asp:DropDownList ID="ddlempbatch" runat="server" Width="240px">
                            </asp:DropDownList></label>
                      </td>
                          <td>
                           <label>
                            <asp:Label ID="lblDate" runat="server" Text="From Date"></asp:Label>
                            <asp:RequiredFieldValidator ID="rddd2" runat="server" ControlToValidate="txtdate"
                                ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                            
                        </label>
                      </td>
                          <td>
                          <label><asp:TextBox ID="txtdate" runat="server" Width="70px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgbtncal"
                                TargetControlID="txtdate">
                            </cc1:CalendarExtender></label>
                      </td>
                      <td>  <label>
                            <asp:Label ID="lblDate0" runat="server" Text="To Date"></asp:Label>
                            <asp:RequiredFieldValidator ID="rddd1" runat="server" ControlToValidate="txttodate"
                                ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                         
                        </label></td>
                       <td>
                       <label>   <asp:TextBox ID="txttodate" runat="server" Width="70px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txate0_CalendarExtender" runat="server" PopupButtonID="imgbtncal"
                                TargetControlID="txttodate">
                            </cc1:CalendarExtender></label></td>
                        <td>
                           <label>
                     
                          <asp:Button ID="btnfilter" runat="server" Text=" Go " OnClick="btnfilter_Click" CssClass="btnSubmit"
                         ValidationGroup="1" />
                        </label></td>
                    </tr>
                    </table>
                      
                          
                      
                       
                       
                      
                     
                    </asp:Panel>
                    <div style="clear: both;">
                    <br />
                    </div>
                  
                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblhead" runat="server" Text="Head"></asp:Label></legend>
                    <div style="float: right;">
                        <asp:Button ID="btncancel0" runat="server" CausesValidation="false" OnClick="btncancel0_Click"
                            Text="Printable Version" CssClass="btnSubmit" />
                        <input id="Button7" runat="server" visible="false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" class="btnSubmit" /></td>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%" ScrollBars="None">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="lblcompany" Font-Size="20px" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" Font-Size="20px" runat="server" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusiness" Font-Size="20px" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblheadtext" Font-Size="18px" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="font-size: 16px; font-weight: normal; font-style: italic;">
                                                    <asp:Label ID="lblbatch" runat="server"></asp:Label>
                                                    <asp:Label ID="lblpay" runat="server"></asp:Label>
                                                    <asp:Label ID="lblemp" runat="server"></asp:Label>
                                                    <asp:Label ID="lbldatefrom" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" EmptyDataText="No Record Found."
                                        OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                                        OnRowUpdating="GridView1_RowUpdating" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                        OnSorting="GridView1_Sorting" AllowSorting="True" GridLines="Both" AllowPaging="true"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="False" 
                                        onpageindexchanging="GridView1_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Employee Name" HeaderStyle-Width="15%" SortExpression="EmployeeName"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblempname" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                    <asp:Label ID="AttendanceId" runat="server" Text='<%# Bind("AttendanceId") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reg. In<br>Time" HeaderStyle-Width="65px" SortExpression="InTime"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtreqintime" runat="server" Text='<%# Eval("InTime")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual<br>In Time" HeaderStyle-Width="65px" SortExpression="InTimeforcalculation"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtactintime" runat="server" Width="35px" Enabled="true" Text='<%# Eval("InTimeforcalculation")%>'>
                                                    </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtactintime"
                                                        ErrorMessage="Only HH:MM" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$"
                                                        ValidationGroup="2"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="ghttt1t" runat="server" ControlToValidate="txtactintime"
                                                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="2" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="txtaddress_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtactintime" ValidChars=":">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblactintime" runat="server" Text='<%# Bind("InTimeforcalculation") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reg. Out<br>Time" HeaderStyle-Width="65px" SortExpression="OutTime"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtreqouttime" runat="server" Text='<%# Eval("OutTime")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual<br>Out Time" HeaderStyle-Width="70px" SortExpression="OutTimeforcalculation"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtactouttime" runat="server" Width="35px" Enabled="true" Text='<%# Eval("OutTimeforcalculation")%>'>
                                                    </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularEessionValidator3" runat="server" ControlToValidate="txtactouttime"
                                                        ErrorMessage="Only HH:MM" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$"
                                                        ValidationGroup="2"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="gttt" runat="server" ControlToValidate="txtactouttime"
                                                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="2" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="txtaddressr_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtactouttime" ValidChars=":">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblactouttime" runat="server" Text='<%# Bind("OutTimeforcalculation") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch Reg.<br>Hours" HeaderStyle-Width="80px" SortExpression="BatchRequiredhours"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtbatchreqhour" runat="server" Text='<%# Bind("BatchRequiredhours") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Actual Hours<br>Worked" HeaderStyle-Width="90px" SortExpression="BatchRequiredhours"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtacworkho" runat="server" Text='<%# Bind("TotalHourWork") %>'></asp:Label>
                                                </ItemTemplate>
                                                  <EditItemTemplate>
                                                    <asp:TextBox ID="txtactwork" runat="server" Width="35px" Enabled="true" Text='<%# Eval("TotalHourWork")%>'>
                                                    </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="Regupressialidator3" runat="server" ControlToValidate="txtactwork"
                                                        ErrorMessage="Only HH:MM" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$"
                                                        ValidationGroup="2"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="gtt1t" runat="server" ControlToValidate="txtactwork"
                                                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="2" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="txtaddlteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtactwork" ValidChars=":">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deviation Note" SortExpression="note" HeaderStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtdevnote" runat="server" Width="150px" TextMode="MultiLine" Height="50px"
                                                        Enabled="true" Text='<%# Eval("note")%>'></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                                        SetFocusOnError="True" ValidationExpression="^([.,_a-zA-Z0-9\s]*)" ControlToValidate="txtdevnote"
                                                        ValidationGroup="2"></asp:RegularExpressionValidator>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldevnote" runat="server" Text='<%# Bind("note") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approved By Supervisor/Admin " HeaderStyle-Width="15%"
                                                SortExpression="sname" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsname" runat="server" Text='<%# Bind("sname") %>'></asp:Label>
                                                    <asp:Label ID="lblsuprid" runat="server" Visible="false" Text='<%# Bind("SuprviserId") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Half Day Leave" HeaderStyle-Width="2%" SortExpression="ConsiderHalfDayLeave"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkhalf" runat="server" Enabled="false" Checked='<%# Bind("ConsiderHalfDayLeave") %>'>
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="chkhalfday" runat="server" Checked='<%# Bind("ConsiderHalfDayLeave") %>'>
                                                    </asp:CheckBox>
                                                </EditItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Full Day Leave" HeaderStyle-Width="2%" SortExpression="ConsiderFullDayLeave"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkfull" runat="server" Enabled="false" Checked='<%# Bind("ConsiderFullDayLeave") %>'>
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="chkfullday" runat="server" Checked='<%# Bind("ConsiderFullDayLeave") %>'>
                                                    </asp:CheckBox>
                                                </EditItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approved" HeaderStyle-Width="2%" SortExpression="Varify"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chlapp" runat="server" Enabled="false" Checked='<%# Bind("Varify") %>'>
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="chlapproved" runat="server" Checked='<%# Bind("Varify") %>'></asp:CheckBox>
                                                </EditItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:CommandField ButtonType="Image" ValidationGroup="2" HeaderStyle-Width="2%" HeaderImageUrl="~/Account/images/edit.gif"
                                                UpdateImageUrl="~/Account/images/UpdateGrid.jpg" CancelImageUrl="~/images/delete.gif"
                                                EditImageUrl="~/Account/images/edit.gif" ShowEditButton="True" ShowHeader="True"
                                                HeaderText="Edit" EditText="Update" HeaderStyle-HorizontalAlign="Left" CancelText="Cancel"
                                                UpdateText="Update">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:CommandField>
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rdlist" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddbatch" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlpayperiod" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlemp" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnfilter" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCancelingEdit" />
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowEditing" />
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowUpdating" />
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="Sorting" />
            <asp:AsyncPostBackTrigger ControlID="btncancel0" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
