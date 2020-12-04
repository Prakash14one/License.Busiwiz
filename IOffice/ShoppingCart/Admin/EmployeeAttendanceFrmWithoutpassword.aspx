<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="EmployeeAttendanceFrmWithoutpassword.aspx.cs" Inherits="Add_Employee_Attendancefrm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
       function keyup(adf) {
           var hhh = adf;

           if (hhh == "15") {

               document.getElementById('<%= btnGo.ClientID %>').focus();
           }
       }
    </script>

    <asp:UpdatePanel ID="upr" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <label>
                        <asp:Label ID="CompanyName" runat="server" Font-Size="12pt" Font-Names="Verdana"
                            Font-Bold="True"></asp:Label></label>
                    <label>
                        <asp:Label ID="lbldt" runat="server"></asp:Label></label>
                    <label>
                        <asp:Label ID="lbltime" runat="server"></asp:Label></label>
                    <label>
                        <asp:Label ID="lbluserid" runat="server"></asp:Label></label>
                    <asp:Label ID="lbldtate" runat="server"></asp:Label>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td width="25%">
                                <label>
                                    <asp:Label ID="lblBusinesslbl" runat="server" Text="Business Name"></asp:Label>
                                    <asp:DropDownList ID="ddlwarehouse" runat="server" Width="200px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td width="75%">
                                <label>
                                    <asp:Label ID="lblBatchNamelbl" runat="server" Text="Batch Name"></asp:Label>
                                    <asp:DropDownList ID="ddlbatchmaster" runat="server" Width="200px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlbatchmaster_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="cssLabelCompany_Information">
                                    <asp:Label ID="lblCurrentDate" Text="Current Date" runat="server"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label class="cssLabelCompany_Information_Ans">
                                    <asp:Label CssClass="lblSuggestion" ID="Label1date" runat="server"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="cssLabelCompany_Information">
                                    <asp:Label ID="Label5" Text="Current Time" runat="server"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label class="cssLabelCompany_Information_Ans">
                                    <asp:Label CssClass="lblSuggestion" ID="time22" runat="server"></asp:Label>
                                </label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <asp:Panel ID="pnlrul8" runat="server" Visible="true">
                        <table width="100%">
                            <tr>
                                <td width="25%">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Entry" Font-Bold="True" Style="font-size: medium;
                                            font-weight: normal"></asp:Label>
                                    </label>
                                </td>
                                <td width="75%">
                                    <label>
                                        <asp:Label ID="lbl1" runat="server" Text="Exit" Font-Bold="True" Style="font-size: medium;
                                            font-weight: normal"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Absent Employees" Font-Bold="True"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Present Employees" Font-Bold="True"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="Dept" Font-Bold="True"></asp:Label>
                                        <asp:DropDownList ID="drpdepartment1" runat="server" AutoPostBack="True" DataTextField="Dept_Name"
                                            DataValueField="Dept_ID" OnSelectedIndexChanged="drpdepartment1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lbldept2" runat="server" Text="Dept" Font-Bold="True"></asp:Label>
                                        <asp:DropDownList ID="drpdepartment2" runat="server" AutoPostBack="True" DataTextField="Dept_Name"
                                            DataValueField="Dept_ID" OnSelectedIndexChanged="drpdepartment2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        <asp:Label ID="lblbarno" runat="server" Text="Enter Employee Barcode" Font-Bold="True"
                                            Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtbartext" runat="server" TabIndex="1" Width="150px" AutoPostBack="True"
                                            Visible="false" onkeyup="keyup(this.value.length)" OnTextChanged="txtbartext_TextChanged"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                    <asp:Button ID="btnGo" runat="server" ValidationGroup="1" CssClass="btnSubmit" Visible="false"
                                        Text="Go" OnClick="btnGo_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="PanelGridEmp" runat="server" Height="200px" Width="400px" ScrollBars="Vertical">
                                        <asp:GridView ID="GridEmpInTime" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeMasterID"
                                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            OnPageIndexChanging="GridEmpInTime_PageIndexChanging1" OnRowCommand="GridEmpInTime_RowCommand"
                                            EmptyDataText="No Employee Found." Height="100%" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="EmployeeMasterID" HeaderText="Emp ID" ItemStyle-Width="20%" />
                                                <asp:ButtonField CommandName="Enter" DataTextField="EmployeeName" ButtonType="Button"
                                                    ItemStyle-Width="80%" HeaderStyle-HorizontalAlign="Left" ControlStyle-Width="100%"
                                                    Text="EmployeeName" HeaderText="Employee Name" AccessibleHeaderText="txtemployeename">
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="EmployeeNo" HeaderText="Employee Number" ItemStyle-Width="30%"
                                                    HeaderStyle-HorizontalAlign="Left" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                                <td>
                                    <asp:Panel ID="panelOutTime" runat="server" Height="200px" Width="400px" ScrollBars="Vertical">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            DataKeyNames="AttendanceId" OnRowCommand="GridView1_RowCommand" Height="100%"
                                            Width="100%" EmptyDataText="No Employee Found." OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                            <Columns>
                                                <asp:BoundField DataField="AttendanceId" HeaderText="AttendanceId" ItemStyle-Width="5%" />
                                                <asp:BoundField DataField="DeptID" HeaderText="Dept Id" ItemStyle-Width="5%" />
                                                <%--  <asp:BoundField DataField="EmployeeMasterID" HeaderText="Emp ID" ItemStyle-Width="10%" />--%>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemp" runat="server" Text='<%# Eval("EmployeeMasterID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:ButtonField CommandName="Entry" DataTextField="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="70%" ControlStyle-Width="100%" Text="Employee Name" ButtonType="Button"
                                                    HeaderText="Employee Name"></asp:ButtonField>
                                                <asp:BoundField DataField="EmployeeNo" HeaderText="Employee Number" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="30%" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="panel1" runat="server" Width="100%">
                    <fieldset>
                        <legend>
                            <asp:Label ID="lblInTimelbl" Text="In Time LogIn" runat="server"></asp:Label>
                            <%--  <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/closeicon.jpeg"
                        OnClick="ImageButton5_Click1" Height="20px" />--%>
                        </legend>
                        <label>
                            <asp:Label ID="lblUserlbl" Text="User Name" runat="server"></asp:Label>
                            <asp:Label ID="Label29" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtusername"
                                ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtusername" runat="server" TabIndex="1" Width="170px"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="lblPassword" Text="Password" runat="server"></asp:Label>
                            <asp:Label ID="Label30" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtpassword"
                                ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator><br />
                            <asp:TextBox ID="txtpassword" runat="server" TabIndex="2" TextMode="Password" Width="170px"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="lblUserNote" Text="User Note" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtnote" runat="server" TextMode="MultiLine" TabIndex="3"></asp:TextBox>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <asp:Button ID="btnsignin" runat="server" Text="Sign In" OnClick="btnsignin_Click"
                            TabIndex="4" ValidationGroup="2" CssClass="btnSubmit" />
                    </fieldset></asp:Panel>
                <%-- <asp:Button ID="Button4" runat="server" Style="display: none" />
            <cc1:modalpopupextender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground" PopupControlID="Panel1" TargetControlID="Button4">
            </cc1:modalpopupextender> --%>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnlruldis8" runat="server" Width="100%">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label31" Text="" runat="server"></asp:Label>
                        </legend>
                        <label>
                            <asp:Label ID="Label32" Text="Sorry you are not allowed to use this type of form for your business. However, you can use the following forms for attendance:"
                                runat="server"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <div style="clear: both;">
                            <a href="../../../EmployeeAttendanceFrmMaster.aspx" target="_blank">attendance using
                                barcode</a><br />
                            <a href="../../../EmpAttendancewithuserId.aspx" target="_blank">attendance using employee
                                user ID</a><br />
                            <a href="../../../EmpAttendancewithempcode.aspx" target="_blank">attendance using employee
                                number</a><br />
                        </div>
                    </fieldset></asp:Panel>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="panel2" runat="server" Width="100%">
                    <fieldset>
                        <legend>
                            <asp:Label ID="lblouttimeloginlbl" Text="Out Time LogIn" runat="server"></asp:Label>
                            <%-- <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/closeicon.jpeg"
                        OnClick="ImageButton3_Click1" Height="20px" />--%>
                        </legend>
                        <label>
                            <asp:Label ID="lblOutUser" Text="User Name" runat="server"></asp:Label>
                            <asp:Label ID="Label12" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox1"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="TextBox1" runat="server" TabIndex="1" Width="170px"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="lbloutpassword" Text="Password" runat="server"></asp:Label>
                            <asp:Label ID="Label17" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox2"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator><br />
                            <asp:TextBox ID="TextBox2" runat="server" TabIndex="2" TextMode="Password" Width="170px"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="Label9" Text="User Note" runat="server"></asp:Label><br />
                            <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" TabIndex="3"></asp:TextBox>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" ValidationGroup="1"
                            Text="Sign Out" OnClick="Button1_Click" TabIndex="4" />
                    </fieldset></asp:Panel>
                <%--<asp:Button ID="Button5" runat="server" Style="display: none" />
              <cc1:modalpopupextender ID="ModalPopupExtender6" runat="server" BackgroundCssClass="modalBackground" PopupControlID="panel2" TargetControlID="Button5">
              </cc1:modalpopupextender>--%>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel5" runat="server" BackColor="#E4E4E4" BorderColor="#999999" BorderStyle="Solid"
                    Width="600px" BorderWidth="10px">
                    <table cellpadding="0" cellspacing="0" style="width: 600px">
                        <tr>
                            <td class="subinnertblfc">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label7" runat="server" Text="Welcome To Office !!!" ForeColor="#336699"
                                    Font-Bold="True" Font-Size="16px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblm" runat="server" ForeColor="#336699"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label111" runat="server" Text="Your Attandance is successfully entered."
                                    ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblempnamemsg" runat="server" ForeColor="Black" Text="Employee Name :"></asp:Label>
                                <asp:Label ID="lblempname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lbletimemsg" runat="server" Text="Your entry time is :" ForeColor="Black"></asp:Label>
                                <asp:Label ID="Label8" runat="server" Text="" ForeColor="Black"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label13" runat="server" Text="You are early by :" ForeColor="Black"> </asp:Label>
                                <asp:Label ID="Label14" runat="server" Text="" ForeColor="Black"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label3" runat="server" Text="You are not allowed to enter or exit,Kindly see your supervisor"
                                    Visible="False" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lbllate" runat="server" Text="You are late, please meet your superwiser immediately to input the reason for being late ."
                                    Visible="False" ForeColor="Black"></asp:Label>
                                <br />
                                <asp:Label ID="Label10" runat="server" Text="You need authorisation from your supervisor. Please meet with your supervisor immediately."
                                    Visible="False" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label11" runat="server" Text="Your last exit time was :" ForeColor="Black"></asp:Label>
                                <asp:Label ID="lbllastexittime" runat="server" Text="" ForeColor="Black"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="HiddenButton2221" runat="server" Style="display: none" />
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel5" TargetControlID="HiddenButton2221">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel3" runat="server" BackColor="#E4E4E4" BorderColor="#999999" BorderStyle="Solid"
                    Width="600px" BorderWidth="10px">
                    <table cellpadding="0" cellspacing="0" style="width: 600px">
                        <tr>
                            <td class="subinnertblfc">
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label15" runat="server" Text="Bye Bye !!!" ForeColor="#336699" Font-Bold="True"
                                    Font-Size="16px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="labbb" runat="server" Text="Your exit is  successfully entered." ForeColor="Black"></asp:Label>
                                <asp:Label ID="lblgoemp" runat="server" Text="" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label16" runat="server" ForeColor="Black" Text="Your exit time is :"></asp:Label>
                                <asp:Label ID="lblexittime" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label18" runat="server" Text="You are early by :" ForeColor="Black"> </asp:Label>
                                <asp:Label ID="lblouterly" runat="server" Text="" ForeColor="Black"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label19" runat="server" Text="You are not allowed to enter or exit,Kindly see your supervisor."
                                    Visible="False" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label191" runat="server" Text="You are going early, please meet with your supervisior immediately to input the reason for going early."
                                    Visible="False" ForeColor="Black"></asp:Label>
                                <br />
                                <asp:Label ID="Label20" runat="server" Text="You need authorisation from your supervisor. Please meet with your supervisor immediately."
                                    Visible="False" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label21" runat="server" Text="Your last entry time was :" ForeColor="Black"> </asp:Label>
                                <asp:Label ID="Label22" runat="server" Text="" ForeColor="Black"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="Button2" runat="server" Style="display: none" />
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel3" TargetControlID="Button2">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel4" runat="server" BackColor="#E8E8E8" BorderColor="Gray" BorderStyle="Outset"
                    Width="467px">
                    <table cellpadding="0" cellspacing="0" align="center" style="width: 460px">
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lblmsggg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Label ID="lblsumsg" runat="server" Text="Your OnlineAccounts Attendance system is blocked  for 3 wrong attempts"
                                    ForeColor="Black" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label23" runat="server" Text="Supervisor Name : " ForeColor="Black"
                                    Visible="False"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblsupname" runat="server" ForeColor="Black" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Label ID="Label24" runat="server" Text="Please ask your Onlineaccounts admin to override the attendance system block"
                                    ForeColor="Black" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label25" runat="server" Text="User Id  :" ForeColor="Black"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtuerlog" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="2a"
                                    ControlToValidate="txtuerlog" SetFocusOnError="true" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label26" runat="server" Text="Password  :" ForeColor="Black"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="lbltxtpass" runat="server" ForeColor="Black">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lbltxtpass"
                                    SetFocusOnError="true" ValidationGroup="2a" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnsubm" runat="server" Text="Submit" ValidationGroup="2a" 
                                    OnClick="btnsubm_Click" />
                                <asp:Button ID="btnsubm0" runat="server" OnClick="btnsubm0_Click" Text="Cancel" ValidationGroup="3" />
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="Button3" runat="server" Style="display: none" />
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel4" TargetControlID="Button3">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                
                
                <asp:Panel ID="Panel8" runat="server" BackColor="#E8E8E8" BorderColor="Gray" BorderStyle="Outset"
                    Width="300px">
                    <table id="Table2" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                              
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label33" runat="server" ForeColor="Black" Text="Are you sure you wish to enter intime ? As you can make entry of intime once in a day."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="Button4" CssClass="btnSubmit" Text="CONFIRM" runat="server" OnClick="Button1_Click" />
                                &nbsp;<asp:Button ID="btncan" CssClass="btnSubmit" Text="CANCEL" runat="server" OnClick="btncan_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel8" TargetControlID="HiddenButton222">
                </cc1:ModalPopupExtender>
                
                
                
                <div style="clear: both;">
                </div>
                
                
                
                <asp:Panel ID="Panel88" runat="server" BackColor="#E8E8E8" BorderColor="Gray" BorderStyle="Outset"
                    Width="300px">
                    <table id="Table22" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label333" runat="server" ForeColor="Black" Text="Are you sure you wish to enter outtime ? As you can make entry of outtime once in a day."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="Button44" CssClass="btnSubmit" Text="CONFIRM" runat="server" 
                                    onclick="Button44_Click" />
                                &nbsp;<asp:Button ID="btncan12" CssClass="btnSubmit" Text="CANCEL" 
                                    runat="server" onclick="btncan12_Click"/>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="HiddenButton2222" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1223" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel88" TargetControlID="HiddenButton2222">
                </cc1:ModalPopupExtender>
                
                
                
                
                <div style="clear: both;">
                </div>
                
                
                
                
                
                
                <asp:Panel ID="Panel6" runat="server" BackColor="#E8E8E8" BorderColor="Gray" BorderStyle="Outset"
                    Width="467px">
                    <table cellpadding="2" cellspacing="2" align="center" style="width: 460px">
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lbllatemessagereason" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Label ID="Label28" runat="server" Text="You are late, please  input the reason for being deviation. otherwise your attendance not approved."
                                    ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label27" runat="server" Text="input the reason for being deviation :"
                                    ForeColor="Black"></asp:Label>
                            </td>
                            <td align="left" width="60%">
                                <asp:TextBox ID="latereaso" runat="server" Text="" Height="100px" MaxLength="500"
                                    TextMode="MultiLine" Width="260px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqsd" runat="server" ControlToValidate="latereaso"
                                    ErrorMessage="*" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnaddlater" runat="server" Text="Add" 
                                    OnClick="btnaddlater_Click" ValidationGroup="5" />
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="Button6" runat="server" Style="display: none" />
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel6" TargetControlID="Button6">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <label>
                    <asp:Timer ID="timer1" runat="server" Interval="15000" OnTick="timer1_Tick" Enabled="False">
                    </asp:Timer>
                    <asp:Timer ID="timer2" runat="server" Enabled="False" Interval="600000" OnTick="timer2_Tick">
                    </asp:Timer>
                </label>
                <div style="clear: both;">
                </div>
                <label>
                    <asp:Label ID="lblmsg" runat="server" Style="color: #CC0000"></asp:Label>
                    <asp:Label ID="lblentry" runat="server" Text=" " Font-Bold="True" Style="color: #CC0000"></asp:Label>
                </label>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlwarehouse" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlbatchmaster" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="drpdepartment1" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="drpdepartment2" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="GridEmpInTime" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnsignin" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnGo" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="timer1" EventName="Tick" />
            <asp:AsyncPostBackTrigger ControlID="btnaddlater" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
