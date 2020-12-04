<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="EmployeeAttendanceReportDetailemployee.aspx.cs" Inherits="Add_Employee_Attendance_Report_Detail" %>
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
            <fieldset>
                  <%--<legend>Employee Attendance Report Detail</legend>--%>
                <label>
                    <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                    <asp:DropDownList ID="ddlStore" runat="server" AutoPostBack="True" onselectedindexchanged="ddlStore_SelectedIndexChanged" >
                            </asp:DropDownList>
                    <%--<select>
                        <option>EPlaza Store</option>
                        <option>EPlaza Store</option>
                        <option>EPlaza Store</option>
                    </select>--%>
                </label>
                <label>
                    <asp:Label ID="Label2" runat="server" Text="Batch Name"></asp:Label>
                    <asp:DropDownList ID="ddlbatchname" runat="server" AutoPostBack="True" onselectedindexchanged="ddlbatchname_SelectedIndexChanged">
                            </asp:DropDownList>
                    <%--<select>
                        <option>EPlaza Store</option>
                        <option>EPlaza Store</option>
                        <option>EPlaza Store</option>
                    </select>--%>
                </label>
                <label>
                    <asp:Label ID="Label3" runat="server" Text="Employee Name"></asp:Label>
                    <asp:DropDownList ID="ddlemployeename"  runat="server"></asp:DropDownList>
                    <%--<select>
                        <option>EPlaza Store</option>
                        <option>EPlaza Store</option>
                        <option>EPlaza Store</option>
                    </select>--%>
                </label>
                <label>
                    <asp:Label ID="Label4" runat="server" Text="Present Status"></asp:Label>
                     <asp:DropDownList ID="ddlpresentstatus" runat="server" Width="165px">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Absent</asp:ListItem>
                            <asp:ListItem Value="2">Present</asp:ListItem>
                            <asp:ListItem Value="3">Late Entry In Time</asp:ListItem>
                            <asp:ListItem Value="4">Early Entry In Time</asp:ListItem>
                            <asp:ListItem Value="5">Late Departure Out Time</asp:ListItem>
                            <asp:ListItem Value="6">Early Departure Out Time</asp:ListItem>
                            </asp:DropDownList>
                    <%--<select>
                        <option>EPlaza Store</option>
                        <option>EPlaza Store</option>
                        <option>EPlaza Store</option>
                    </select>--%>
                </label>
                <div style="clear: both;">
                </div>
                <label>
                     <asp:Label ID="Label5" runat="server" Text="From Date"></asp:Label>                                        
                        <asp:TextBox ID="txteenddate" runat="server" Width="70px" AutoPostBack="True"></asp:TextBox>                            
                   
                <cc1:calendarextender ID="Calendarextender5" Format="MM/dd/yyyy" runat="server" TargetControlID="txteenddate"
                    PopupButtonID="txteenddate" >
                </cc1:calendarextender>               
             <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                 ControlToValidate="txteenddate" ErrorMessage="*" ValidationGroup="4"> </asp:RequiredFieldValidator>  
                  <%--<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="images/Calendar.png" Width="16px" Height="16px" />--%>
                        <%--<input id="Text7" class="txtInputMed" name="postcode1" type="text" value="" />--%>
                        <%--<img src="images/cal_actbtn.jpg" height="16" width="16" />--%>
                    
                </label>
                <label>
                    <asp:Label ID="Label6" runat="server" Text="To Date"></asp:Label>
                    <asp:TextBox ID="txtenddateto" runat="server" AutoPostBack="True" Width="70px"></asp:TextBox>                
                <cc1:calendarextender ID="Calendarextender51" runat="server" 
                    PopupButtonID="txtenddateto" TargetControlID="txtenddateto" 
                      Format="MM/dd/yyyy">
                </cc1:calendarextender>                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtenddateto" ErrorMessage="*" ValidationGroup="4"> </asp:RequiredFieldValidator>  
                    <%--<asp:ImageButton ID="ImageButton1" Width="16px" Height="16px" runat="server" 
                    ImageUrl="images/Calendar.png" />--%>
                        <%--<input id="Text2" class="txtInputMed" name="postcode1" type="text" value="" />--%>
                        <%--<img src="images/cal_actbtn.jpg" height="16" width="16" />--%>
                </label>
                <div style="clear: both;">
                </div>
                <br />
                    <asp:Button ID="Button1" runat="server" Text=" Go " CssClass="btnSubmit" onclick="Button1_Click" />
                    <%--<asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button2"
                        CssClass="btnSubmit" Text="Submit" />--%>
                
                <label>
                    <%--<asp:Button runat="server" ID="Button3" CssClass="btnSubmit" Text="Cancel" />--%>
                </label>
            </fieldset>
            <div style="clear: both;">
            </div>
            <fieldset>
                <legend> <asp:Label ID="Label12" runat="server" Text="List of Employee Attendance"></asp:Label></legend>
                <div style="float: right;">
                    
                        <asp:Button ID="btnprintableversion" runat="server" Text="Printable Version" 
                            onclick="btnprintableversion_Click" CssClass="btnSubmit" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                             type="button" value="Print" visible="False" class="btnSubmit" />
                        <%--<asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button1"
                            CssClass="btnSubmit" Text="Print Version" />--%>
                    
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnlgrid" runat="server" Width="100%" >
            <table width="100%">
                                <tr align="center">
                                    <td>
                                        <div id="mydiv" class="closed">
                                           <table width="100%" style="color:Black; font-style:italic; text-align:center">
                                            <tr>
                                                <td align="center">                                                  
                                                    <asp:Label ID="lblcompanyname" Font-Bold="true" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">   
                                                  <asp:Label ID="Label11" Font-Bold="true" runat="server" Font-Size="20px" Text="Business :"></asp:Label>     
                                                    <asp:Label ID="lblbusiness" Font-Bold="true" runat="server" Font-Size="20px"></asp:Label>                                               
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label7" runat="server" Font-Bold="true" Font-Size="18px" Text="List of Employee Attendance Report"></asp:Label>            
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="font-size:16px">
                                                    <asp:Label ID="Label8" runat="server" Text="Batch Name :"></asp:Label>     
                                                     <asp:Label ID="lblbatchnameprint" runat="server"  ForeColor="Black"></asp:Label>
                                                    <asp:Label ID="Label9" runat="server" Text="Employee Name :"></asp:Label>                                             
                                                    <asp:Label ID="lblemployeenameprint" runat="server"  ForeColor="Black"></asp:Label>
                                                    <asp:Label ID="Label10" runat="server" Text="Present Status :"></asp:Label>
                                                    <asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>                                                    
                                                    <asp:Label ID="lbldateprint" runat="server"  ForeColor="Black"></asp:Label>
                                                     <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                      <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                </td>
                                            </tr>
                                          
                                                </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr >
                                    <td>
                            <asp:GridView ID="gridattendance" runat="server" AutoGenerateColumns="False" 
                                            DataKeyNames="AttendanceId" AllowPaging="True" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                                AlternatingRowStyle-CssClass="alt" Width="100%"  AllowSorting="True" 
                                             
                                EmptyDataText="No Record Found." onrowcommand="gridattendance_RowCommand" 
                                            onpageindexchanging="gridattendance_PageIndexChanging" PageSize="15" 
                                            onsorting="gridattendance_Sorting">
                                <Columns>
                                            <asp:TemplateField HeaderText="Batch Name" SortExpression="BatchName"  HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblbatchname123" runat="server" Text='<%# Eval("BatchName")%>'></asp:Label>
                                            </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                          
                                        
                                       <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblemployeename123" runat="server" Text='<%# Eval("EmployeeName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="25%" />
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblinstructiondate123" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                                
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="7%" />
                                                
                                            </asp:TemplateField>
                                      
                                         
                                         <asp:TemplateField HeaderText="Regular Start Time" ItemStyle-Width="7%" SortExpression="InTime" HeaderStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblintime" runat="server" Text='<%# Eval("InTime")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="In Time" ItemStyle-Width="7%" SortExpression="InTimeforcalculation" HeaderStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblintimeforcalculation" runat="server" Text='<%# Eval("InTimeforcalculation")%>'></asp:Label>
                                            </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
                                                 <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Late/Early In Time" ItemStyle-Width="7%" SortExpression="LateInMinuts" HeaderStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lbllateinminutes" runat="server" ForeColor="Red" Text='<%# Eval("LateInMinuts")%>'></asp:Label>
                                            </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                            
                                            
                                            <asp:TemplateField HeaderText="Regular Out Time" ItemStyle-Width="7%" SortExpression="OutTime" HeaderStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblregularouttime" runat="server" Text='<%# Eval("OutTime")%>'></asp:Label>
                                            </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Out Time" ItemStyle-Width="7%" SortExpression="OutTimeforcalculation" HeaderStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblouttimecalculate" runat="server" Text='<%# Eval("OutTimeforcalculation")%>'></asp:Label>
                                            </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
                                                 <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Late/Early Out Time" ItemStyle-Width="7%" SortExpression="OutInMinuts" HeaderStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lbllateoutminutes" ForeColor="Red" runat="server" Text='<%# Eval("OutInMinuts")%>'></asp:Label>
                                            </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
                                                 <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Total Work Hour" SortExpression="TotalHourWork" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lbltotalworkhour" runat="server" Text='<%# Eval("TotalHourWork")%>'></asp:Label>
                                            </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
                                                 <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Status" SortExpression="Status" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                            </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Left" />
                                                  <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Approved" SortExpression="approve" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblapp" runat="server" Text='<%# Eval("approve")%>'></asp:Label>
                                            </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
                                                 <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                           <asp:TemplateField  HeaderImageUrl="~/Account/images/viewprofile.jpg" 
                                                ItemStyle-Width="2%" Visible="False">              
                                             <ItemTemplate>
                                                <asp:ImageButton ID="lbladdd" runat="server"  CommandArgument='<%# Eval("AttendanceId") %>' CommandName="view"  ToolTip="View Profile" Height="20px" ImageUrl="~/Account/images/viewprofile.jpg" Width="20px"></asp:ImageButton>                      
                                             </ItemTemplate>
                                               <ItemStyle Width="2%" />
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
                        <asp:BoundField DataField="ContactName" HeaderText="Batch Name" />
                        <asp:BoundField DataField="PricePlan" HeaderText="Employee Name" />
                        <asp:BoundField DataField="PricePlan" HeaderText="Date" />
                        <asp:BoundField DataField="PricePlan" HeaderText="Regular Start Time" />
                        <asp:BoundField DataField="PricePlan" HeaderText="In Time" />
                        <asp:BoundField DataField="PricePlan" HeaderText="Late/Early In Time" />
                        <asp:BoundField DataField="PricePlan" HeaderText="Regular Out Time" />
                        <asp:BoundField DataField="PricePlan" HeaderText="Out Time" />
                        <asp:BoundField DataField="PricePlan" HeaderText="Late/Early Out Time" />
                        <asp:BoundField DataField="PricePlan" HeaderText="Total Work Hour" />
                        <asp:HyperLinkField Text="Edit" HeaderText="Edit" />
                    </Columns>
                </asp:GridView>--%>
                <%--<asp:XmlDataSource ID="customresDataSource" runat="server" DataFile="~/App_Data/data1.xml">
                </asp:XmlDataSource>--%>
            </fieldset>
        </div>
        <!--end of right content-->
        <div style="clear: both;">
        </div>
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
