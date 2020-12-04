<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="Employeepayrollsummary.aspx.cs"
    Inherits="Employeepayrollsummarywithupp" Title="Untitled Page" %>

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

        function CallPrintx(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= Panel1.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }  

  
    </script>

    <%--
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="products_box">
        <fieldset>
            <legend>
                <asp:Label ID="statuslable" runat="server" Text=" Employee Pay Slip Manage"></asp:Label>
            </legend>
            <label>
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </label>
            <div style="clear: both;">
            </div>
            <fieldset>
                <legend></legend>
                <label>
                    <asp:Label ID="Label6" runat="server" Text="Business Name"></asp:Label>
                    <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                    </asp:DropDownList>
                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" type="hidden" />
                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" type="hidden" />
                </label>
                <label>
                    <asp:Label ID="Label10" runat="server" Text="Period Type"></asp:Label>
                    <asp:DropDownList ID="ddlperiodtype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlperiodtype_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
                <label>
                    <asp:Label ID="Label7" runat="server" Text="Payperiod"></asp:Label>
                    <asp:DropDownList ID="ddlpayperiod" runat="server">
                    </asp:DropDownList>
                </label>
                <label>
                    <asp:Label ID="Label8" runat="server" Text="Employee Name" Visible="false"></asp:Label>
                    <asp:DropDownList ID="ddlemp" runat="server" Visible="false">
                    </asp:DropDownList>
                </label>
                <label>
                    <br />
                    <asp:Button ID="btncalvd" runat="server" Text="Calculate New Payroll" CssClass="btnSubmit"
                        OnClick="btncalvd_Click" Visible="true" />
                </label>
                  <div style="clear: both;">
                                    <asp:Panel ID="Paneldoc" runat="server" Width="60%" CssClass="modalPopup">
                                        <fieldset>
                                            <legend>
                                                <asp:Label ID="lbldoclab" runat="server" Text=""></asp:Label>
                                            </legend>
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 95%;"><asp:Label ID="lbltempnotallempstore" runat="server" Text="" Visible="false"></asp:Label>
                                                    <asp:Label ID="lbltempnotallempstore1" runat="server" Text="" Visible=false></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                            Width="16px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <label>
                                                            <asp:Label ID="lblheadoc" runat="server" Text="Employee Payroll Calculation is not possible for all employees at this time."></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label5" runat="server" Text="Because the following "></asp:Label>
                                                            <asp:Label ID="lblnooffemp" runat="server" Text="0"></asp:Label>
                                                            <asp:Label ID="lblnotax" runat="server" Text=" employee has not filled up employee payroll setup form"></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:GridView ID="grdemplist" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                            CssClass="mGrid" DataKeyNames="EmployeeId" HeaderStyle-HorizontalAlign="Left" 
                                                            PagerStyle-CssClass="pgr" Width="100%">
                                                            <Columns>
                                                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="Departmentname" HeaderText="Department Name" HeaderStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="DesignationName" HeaderText="Designation Name" HeaderStyle-HorizontalAlign="Left" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td colspan="2">
                                                <table>
                                                <tr>
                                                <td>
                                                <asp:RadioButton ID="rdempallow1" runat="server"  GroupName="A" AutoPostBack="true" 
                                                        oncheckedchanged="rdempallow1_CheckedChanged" />
                                                </td>
                                                <td>
                                                <label>
                                                   <asp:Label ID="Label11" runat="server" Text="Would you like to proceed with calculating payroll for other "></asp:Label>
                                                    <asp:Label ID="lblempnopo" runat="server" Text="0"></asp:Label>
                                                     <asp:Label ID="Label13" runat="server" Text="  employee?"></asp:Label>
                               
                                                   </label> 
                                                
                                                  
                                                    
                                                </td>
                                                </tr>
                                                   <tr>
                                                <td>
                                              <asp:RadioButton ID="rdempallow2" runat="server"   GroupName="A" 
                                                        AutoPostBack="true" oncheckedchanged="rdempallow1_CheckedChanged" />
                                                </td>
                                                <td>
                                                <label>
                                                   <asp:Label ID="Label14" runat="server" Text="Would you like to fill up the employee payroll setup form for above mentioned form?"></asp:Label>
                                                   
                                                   </label> 
                                                
                                                  
                                                    
                                                </td>
                                                </tr>
                                                </table>
                                                </td>
                                                </tr>
                                            </table>
                                        </fieldset></asp:Panel>
                                    <asp:Button ID="btncontv" runat="server" Style="display: none" />
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="Paneldoc" TargetControlID="btncontv" CancelControlID="ImageButton10">
                                    </cc1:ModalPopupExtender>
                            </div>
            </fieldset>
            <div style="clear: both;">
                <asp:Panel ID="pnlinsertdata" runat="server" Visible="true">
                    <table style="width: 100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btncancel0" runat="server" CausesValidation="false" OnClick="btncancel0_Click"
                                    Text="Printable Version" CssClass="btnSubmit" />
                                <input id="Button7" runat="server" visible="false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrintx('divPrint');document.getElementById('mydiv').className='closed';"
                                    class="btnSubmit" type="button" value="Print" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" style="font-size: 18px; font-family: Calibri; font-weight: bold;
                                                                color: #000000">
                                                                <asp:Label ID="lblBusiness" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-family: Calibri; font-size: 18px;
                                                                font-weight: bold;">
                                                                <asp:Label ID="lblpayt" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-family: Calibri; font-size: 18px;
                                                                font-weight: bold;">
                                                                <asp:Label ID="lblpayper" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-family: Calibri; font-size: 18px;
                                                                font-weight: bold;">
                                                                <asp:Label ID="lblempn" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="Label9" runat="server" Style="text-align: center; font-family: Calibri;
                                                                    font-size: 18px; font-weight: bold;" Text="Employee Salary Slip"> </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlcau" runat="server" Visible="false">
                                                    <fieldset>
                                                        <legend>
                                                            <asp:Label ID="lblfilemp" runat="server" Font-Bold="true" Font-Size="14px" Text=""></asp:Label>
                                                        </legend>
                                                        <table width="100%" cellpadding="0" cellspacing="0">
                                                            <%-- <tr>
                                                                        <td align="left">
                                                                            Income :
                                                                        </td>
                                                                    </tr>--%>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlhourly" runat="server" Visible="false">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:Label ID="lblhourlycount" runat="server" Font-Bold="true" Font-Size="12px" Text="Hourly Salary"></asp:Label>
                                                                                </td>
                                                                                <td colspan="1" align="right">
                                                                                    <asp:Button ID="btnaddnewrem" runat="server" Text="Add Remuneration" CssClass="btnSubmit"
                                                                                        OnClick="btnaddnewrem_Click" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:GridView ID="grdcal" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                                                        Width="100%" HeaderStyle-HorizontalAlign="Left">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Remuneration Name" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtremu" runat="server" Text='<%#Bind("RemunarationName") %>' Width="183px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtremu"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                    <asp:Label ID="lblover" runat="server" Text='<%#Bind("OverTime") %>' Visible="false"></asp:Label>
                                                                                                    <asp:DropDownList ID="ddldailyremu" Visible="false" runat="server">
                                                                                                    </asp:DropDownList>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Rate">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtrate" runat="server" Text='<%#Bind("Rate") %>' Width="50px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtrate"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Per Unit Name">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="txtperunitname" runat="server" Text='<%# Bind("perunitname") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Total Unit">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txttotunit" runat="server" Text='<%# Bind("totalunit") %>' Width="50px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txttotunit"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Actual Payable Unit">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtqlifie" runat="server" Text='<%# Bind("totalunitpay") %>' Width="50px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtqlifie"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Total Amt" HeaderStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txttotal" runat="server" Text='<%# Bind("totalamt") %>' Width="100px" />
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txttotal"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnldaily" runat="server" Visible="false">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="12px" Text="Daily Salary"></asp:Label>
                                                                                </td>
                                                                                <td colspan="1" align="right">
                                                                                    <asp:Button ID="btndaily" runat="server" Text="Add Remuneration" CssClass="btnSubmit"
                                                                                        OnClick="btndaily_Click" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:GridView ID="grddaily" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                                                        Width="100%" HeaderStyle-HorizontalAlign="Left">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Remuneration Name" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtremu" runat="server" Text='<%#Bind("RemunarationName") %>' Width="183px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtremu"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                    <asp:DropDownList ID="ddldailyremu" Visible="false" runat="server">
                                                                                                    </asp:DropDownList>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Rate">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtrate" runat="server" Text='<%#Bind("Rate") %>' Width="50px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtrate"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Per Day">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="txtperunitname" runat="server" Text='<%# Bind("perunitname") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Total Day">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txttotunit" runat="server" Text='<%# Bind("totalunit") %>' Width="50px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txttotunit"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Actual Work Day">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtqlifie" runat="server" Text='<%# Bind("totalunitpay") %>' Width="50px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtqlifie"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Total Amt" HeaderStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txttotal" runat="server" Text='<%# Bind("totalamt") %>' Width="100px" />
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txttotal"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlmonth" runat="server" Visible="false">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Size="12px" Text="Monthly Salary"></asp:Label>
                                                                                </td>
                                                                                <td colspan="1" align="right">
                                                                                    <asp:Button ID="btnmonth" runat="server" Text="Add Remuneration" CssClass="btnSubmit"
                                                                                        OnClick="btnmonth_Click" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:GridView ID="grdmonth" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                                                        Width="100%" HeaderStyle-HorizontalAlign="Left">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Remuneration Name" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtremu" runat="server" Text='<%#Bind("RemunarationName") %>' Width="183px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtremu"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                    <asp:DropDownList ID="ddldailyremu" Visible="false" runat="server">
                                                                                                    </asp:DropDownList>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Rate">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtrate" runat="server" Text='<%#Bind("Rate") %>' Width="50px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiFieldValidator2" runat="server" ControlToValidate="txtrate"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Per Month">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="txtperunitname" runat="server" Text='<%# Bind("perunitname") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Total Completed Month">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtcompletemonth" runat="server" Text='<%# Bind("completemonth") %>'
                                                                                                        Width="70px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="ReiredFieldValidator2" runat="server" ControlToValidate="txtcompletemonth"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Maximum Salary Payable for completed month">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtcompletemonthamt" runat="server" Text='<%# Bind("completedmonthamt") %>'
                                                                                                        Width="70px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcompletemonthamt"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Total Working Day in Completed Month">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txttotunit" runat="server" Text='<%# Bind("totalunit") %>' Width="50px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txttotunit"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Actual Day Work in Completed Month">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtqlifie" runat="server" Text='<%# Bind("totalunitpay") %>' Width="50px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtqlifie"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Total Amt" HeaderStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txttotal" runat="server" Text='<%# Bind("totalamt") %>' Width="100px" />
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txttotal"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlispercentage" runat="server" Visible="false">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:Label ID="Label3" runat="server" Font-Bold="true" Font-Size="12px" Text="Remunaration By Percentage"></asp:Label>
                                                                                </td>
                                                                                <td colspan="1" align="right">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:GridView ID="grdispercentage" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                                                        Width="100%" HeaderStyle-HorizontalAlign="Left">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Remunaration Name" Visible="true" HeaderStyle-Width="25%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtremu" runat="server" Text='<%#Bind("Remuname") %>' Width="200px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtremu"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="%" HeaderStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="PercentageOf" runat="server" Text='<%#Bind("per") %>' Width="70px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldVa" runat="server" ControlToValidate="PercentageOf"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="% of" HeaderStyle-Width="20%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="perof" runat="server" Text='<%#Bind("perof") %>' Width="200px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="equiredFieldVa" runat="server" ControlToValidate="perof"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Base Amount" HeaderStyle-Width="15%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtbaseamt" runat="server" Text='<%#Bind("baseamt") %>' Width="100px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RqireieldVa" runat="server" ControlToValidate="txtbaseamt"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Amount Payable" HeaderStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtpaamt" runat="server" Text='<%#Bind("Totamt") %>' Width="100px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequirieldVa" runat="server" ControlToValidate="txtpaamt"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlsales" runat="server" Visible="false">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="true" Font-Size="12px" Text="Remunaration By Percentage of sales"></asp:Label>
                                                                                </td>
                                                                                <td colspan="1" align="right">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:GridView ID="grdsales" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                                                        Width="100%" HeaderStyle-HorizontalAlign="Left">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Remunaration Name" Visible="true" HeaderStyle-Width="25%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtremu" runat="server" Text='<%#Bind("Remuname") %>' Width="200px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtremu"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="%" HeaderStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="PercentageOf" runat="server" Text='<%#Bind("per") %>' Width="70px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldVa" runat="server" ControlToValidate="PercentageOf"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="% of Sales of" HeaderStyle-Width="20%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="perof" runat="server" Text='<%#Bind("perof") %>' Width="200px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="equiredFieldVa" runat="server" ControlToValidate="perof"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Eligible Sales Amount" HeaderStyle-Width="15%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtbaseamt" runat="server" Text='<%#Bind("baseamt") %>' Width="100px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RqireieldVa" runat="server" ControlToValidate="txtbaseamt"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Amount Payable" HeaderStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtpaamt" runat="server" Text='<%#Bind("Totamt") %>' Width="100px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequirieldVa" runat="server" ControlToValidate="txtpaamt"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlleaveencash" runat="server" Visible="false">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:Label ID="lbll" runat="server" Font-Bold="true" Font-Size="12px" Text="Leave Encash"></asp:Label>
                                                                                </td>
                                                                                <td colspan="1" align="right">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:GridView ID="grdleaveencash" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                                                        Width="100%" HeaderStyle-HorizontalAlign="Left">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Leave Type" Visible="true" HeaderStyle-Width="50%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblleavet" runat="server" Text='<%#Bind("LeaveType") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Per Leave Rate" HeaderStyle-Width="12%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtleaverate" runat="server" Text='<%#Bind("txtleaverate") %>' Width="70px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequidFieldVa" runat="server" ControlToValidate="txtleaverate"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="No of Leave" HeaderStyle-Width="12%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtnoofleave" runat="server" Text='<%#Bind("perleaveno") %>' Width="70px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequredFieldVa" runat="server" ControlToValidate="txtnoofleave"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Total Amount" HeaderStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtpaamt" runat="server" Text='<%#Bind("Totamt") %>' Width="100px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequirieldVa" runat="server" ControlToValidate="txtpaamt"
                                                                                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnltaxbeni" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:Label ID="lbltaxbenlbl" runat="server" Font-Bold="true" Font-Size="12px" Text="Taxable Benefits given"></asp:Label>
                                                                                </td>
                                                                                <td colspan="1" align="right">
                                                                                    <asp:ImageButton ID="imgAdd2" runat="server" AlternateText="Add New" Height="20px"
                                                                                        ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="imgAdd2_Click" ToolTip="Add New "
                                                                                        Width="20px" />
                                                                                    <asp:ImageButton ID="imgRefresh2" runat="server" AlternateText="Refresh" Height="20px"
                                                                                        ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="imgRefresh2_Click"
                                                                                        ToolTip="Refresh" Width="20px" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:GridView ID="grdtaxbenifit" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                                                        Width="100%" HeaderStyle-HorizontalAlign="Left">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Tax Benefit name" Visible="true" HeaderStyle-Width="50%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbltaxben" runat="server" Text='<%#Bind("Taxablebenifitname") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Date" HeaderStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbltaxbn" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Amount" HeaderStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="txtamt" runat="server" Text='<%#Bind("Amount") %>' Width="100px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="middle">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="lbltin" runat="server" Text="Gross Amount Including Taxable Benefits :"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="width: 105px;">
                                                                                <asp:TextBox ID="txtbenifitgrossamt" runat="server" Width="100px">0</asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="middle">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="lbltotremunration" runat="server" Text="0" Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbltotremperc" runat="server" Text="0" Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbltotsales" runat="server" Text="0" Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblcs" runat="server" Text="Total Income Payable (Other than Benefits) :"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="width: 105px;">
                                                                                <asp:TextBox ID="txttotincome" runat="server" Width="100px">0</asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-style: normal; font-weight: bold; color: #000000; font-size: 12px;"
                                                                    align="left">
                                                                    <asp:Label ID="lblnonglab" runat="server" Font-Bold="true" Text="Non Goverment Deduction"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Button ID="btnded" runat="server" Text="Add New Deduction" CssClass="btnSubmit"
                                                                        OnClick="btnded_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grdded" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                                        Width="100%" HeaderStyle-HorizontalAlign="Left">
                                                                        <Columns>
                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                HeaderText="Deduction Name" Visible="true" HeaderStyle-Width="50%">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtdedremname" runat="server" Text='<%#Bind("DeductionName") %>'
                                                                                        Width="300px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                HeaderText="Amount" Visible="true" HeaderStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="FixedAmount" runat="server" Text='<%#Bind("Amount") %>' Width="50px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                HeaderText="%" HeaderStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="PercentageOf" runat="server" Text='<%#Bind("per") %>' Width="50px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                HeaderText="% of" HeaderStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="perof" runat="server" Text='<%#Bind("perof") %>' Width="100px"></asp:TextBox>
                                                                                    <asp:Label ID="lblrrsug" runat="server" Text='<%#Bind("RUDed") %>' Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                HeaderText="Total Amount" HeaderStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="Totamt" runat="server" Text='<%#Bind("Totamt") %>' Width="100px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="lblnongovtlabltot" runat="server" Font-Bold="true" Text="Total Non Govt Deduction :"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="width: 105px;">
                                                                                <asp:TextBox ID="txttotded" runat="server" Width="100px">0</asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlgovttax" runat="server">
                                                                        <table width="100%" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td style="font-style: normal; font-weight: bold; color: #000000; font-size: 12px;"
                                                                                    align="left">
                                                                                    <asp:Label ID="lblgovlable" runat="server" Font-Bold="true" Text=" Government Payroll Deduction :"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="grdgovded" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                                                        Width="100%" HeaderStyle-HorizontalAlign="Left">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Deduction Name" Visible="true" HeaderStyle-Width="50%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblgovtded" runat="server" Text='<%#Bind("DeductionName") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Amount" HeaderStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="txtgovtamt" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblcracc" runat="server" Text='<%#Bind("CrAccId") %>' Visible="false"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <asp:Label ID="lblgovtottaxlbl" runat="server" Font-Bold="true" Text="Total Government Payroll Deduction :"></asp:Label>
                                                                                            </td>
                                                                                            <td align="left" style="width: 105px;">
                                                                                                <asp:TextBox ID="txtgovtottax" runat="server" Width="100px">0</asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="middle">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="lblletlbl" runat="server" Text="Net Total :" Font-Bold="true"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="width: 105px;">
                                                                                <asp:TextBox ID="txtnettotal" runat="server" Width="100px">0</asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle">
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                    Allow Paid Amount
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkpaida" runat="server" AutoPostBack="True" OnCheckedChanged="chkpaida_CheckedChanged" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle">
                                                                    <asp:Panel ID="pnlamtpaid" runat="server" Visible="false">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Relavant Account
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:DropDownList ID="ddlrelacc" runat="server">
                                                                                        </asp:DropDownList>
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        Cheque No
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:TextBox ID="txtrelcheque" runat="server" Width="200px"></asp:TextBox>
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        Paid Amount
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:TextBox ID="txtrelpaidamr" runat="server" Width="100px"></asp:TextBox>
                                                                                    </label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset></asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btncalculate" runat="server" Text="Calculate" CssClass="btnSubmit"
                                                    OnClick="btncalculate_Click" ValidationGroup="5" />
                                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click"
                                                    CssClass="btnSubmit" ValidationGroup="5" />
                                                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                                    Visible="False" CssClass="btnSubmit" ValidationGroup="5" />
                                                <asp:Button ID="btnEdit" runat="server" CssClass="btnSubmit" OnClick="btnEdit_Click"
                                                    Text="Edit" ValidationGroup="5" Visible="False" />
                                                <asp:Button ID="btncancel" runat="server" CssClass="btnSubmit" Text="Cancel" ValidationGroup="5"
                                                    OnClick="btncancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div style="clear: both;">
                <asp:Panel ID="pnlGS" runat="server" Visible="false">
                    <fieldset>
                        <legend></legend>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblmccc" runat="server" ForeColor="Red" Text="First Time payroll calculation for the selected pay period. Please review the calcualtion and click submit at the bottom of the grid to create necessary accounting entries."
                                            Visible="False"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:LinkButton ID="btnSelectColums" runat="server" Text="Show more columns" ForeColor="Black"
                                                        OnClick="btnSelectColums_Click"></asp:LinkButton></label>
                                            </td>
                                            <td align="right" style="width: 230px;">
                                                <label>
                                                    <asp:Button ID="btnmpr" runat="server" CausesValidation="false" Text="Printable Version"
                                                        CssClass="btnSubmit" OnClick="btnmpr_Click" />
                                                </label>
                                                <label>
                                                    <input id="btnpm" runat="server" visible="false" onclick="document.getElementById('Div1').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                                        class="btnSubmit" type="button" value="Print" /></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnldiscolumn" runat="server" HorizontalAlign="Left" Visible="False">
                                                    <fieldset>
                                                        <legend>
                                                            <asp:Label ID="lbldiscolumn" runat="server" Text="Select Display Columns" Visible="false"></asp:Label>
                                                        </legend>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlchf" runat="server" ScrollBars="Vertical" Height="50px">
                                                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" Width="100%" RepeatColumns="5"
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="11" Text="None Gov1"></asp:ListItem>
                                                                            <asp:ListItem Value="12" Text="None Gov2"></asp:ListItem>
                                                                            <asp:ListItem Value="13" Text="None Gov3"></asp:ListItem>
                                                                            <asp:ListItem Value="14" Text="None Gov4"></asp:ListItem>
                                                                            <asp:ListItem Value="15" Text="None Gov Other"></asp:ListItem>
                                                                            <asp:ListItem Value="21" Text="Gov1"></asp:ListItem>
                                                                            <asp:ListItem Value="22" Text="Gov2"></asp:ListItem>
                                                                            <asp:ListItem Value="23" Text="Gov3"></asp:ListItem>
                                                                            <asp:ListItem Value="24" Text="Gov4"></asp:ListItem>
                                                                            <asp:ListItem Value="25" Text="Gov Other"></asp:ListItem>
                                                                            <asp:ListItem Value="1" Text="Paid/Unpaid"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Relevant Account"></asp:ListItem>
                                                                            <asp:ListItem Value="3" Text="Cheque No"></asp:ListItem>
                                                                            <asp:ListItem Value="4" Text="Paid Amount"></asp:ListItem>
                                                                        </asp:CheckBoxList>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Button ID="btnselcolgo" runat="server" CssClass="btnSubmit" Text="Go" OnClick="btnselcolgo_Click" />
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset></asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table width="100%">
                                            <tr align="center">
                                                <td>
                                                    <div id="Div1" class="closed">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center" style="font-size: 18px; font-family: Calibri; font-weight: bold;
                                                                    color: #000000">
                                                                    <asp:Label ID="lblbusna" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="text-align: center; font-family: Calibri; font-size: 18px;
                                                                    font-weight: bold;">
                                                                    <asp:Label ID="lblpaypt" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="text-align: center; font-family: Calibri; font-size: 18px;
                                                                    font-weight: bold;">
                                                                    <asp:Label ID="lblpayperiod" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Label ID="lbltrbal" runat="server" Style="text-align: center; font-family: Calibri;
                                                                        font-size: 18px; font-weight: bold;" Text="List of Employee Salary"> </asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label12" runat="server" Text="The following is the summary of employee payroll for the period"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="lblpayperiodtypelbl" runat="server" Text=""></asp:Label></label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="grdallemp" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                        EmptyDataText="No Record Found." PagerStyle-CssClass="pgr" FooterStyle-BackColor="#416271"
                                                        AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Width="100%" DataKeyNames="EmployeeId"
                                                        OnRowCommand="grdallemp_RowCommand" OnSorting="grdallemp_Sorting" HeaderStyle-HorizontalAlign="Left"
                                                        OnRowDeleting="grdallemp_RowDeleting" OnRowCreated="grdallemp_RowCreated" ShowFooter="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName" Visible="true"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblempname" runat="server" Text='<%#Bind("EmployeeName") %>'
                                                                        OnClick="lblfe1_Click" ForeColor="Black"></asp:LinkButton><asp:Label ID="lblEmployeeId"
                                                                            runat="server" Text='<%#Bind("EmployeeId") %>' Visible="false"></asp:Label></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remu" SortExpression="Remorig" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="RemId" runat="server" Text='<%#Bind("RemId") %>'></asp:Label><asp:Label
                                                                        ID="lblrem" runat="server" Text='<%#Bind("Remorig") %>'></asp:Label></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate/Period" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="115px">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblrate" runat="server" Text='<%# Bind("remrate") %>' OnClick="lblrate_Click"
                                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Actual<br>Unit" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblactunit" runat="server" Text='<%# Bind("Actunit","{0:n}") %>'
                                                                        OnClick="lblActunit_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootremunit" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" SortExpression="remamt" Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblremamt" runat="server" Text='<%# Bind("remamt","{0:n}") %>'
                                                                        OnClick="lblSlip_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootremamt" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remuneration name" SortExpression="remsalesname" Visible="true"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsaleremname" runat="server" Text='<%#Bind("remsalesname") %>'></asp:Label></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remuneration<br>Rate (in %)" SortExpression="remsalesperc"
                                                                Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsaleremperc" runat="server" Text='<%# Bind("remsalesperc","{0:n}") %>'></asp:Label></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sales Amount" SortExpression="remsalesamt" Visible="true"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblsaleremamt" runat="server" Text='<%# Bind("remunit","{0:n}") %>'
                                                                        OnClick="lblSlip_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootsalesamt" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remuneration<br> Amount" HeaderStyle-HorizontalAlign="Left"
                                                                Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblunit" runat="server" Text='<%# Bind("remsalesamt","{0:n}") %>'
                                                                        OnClick="lblSlip_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootremsalamount" runat="server" ForeColor="Black"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Other Rem" SortExpression="Remother" Visible="true"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblotherrem" runat="server" Text='<%# Bind("Remother","{0:n}") %>'
                                                                        OnClick="lblSlip_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfoototherrem" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total<br>Remuneration" SortExpression="Remtotal" Visible="true"
                                                                ItemStyle-BackColor="#ccffff" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbltotrem" runat="server" Text='<%# Bind("Remtotal","{0:n}") %>'
                                                                        OnClick="lblSlip_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootRemtotal" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle BackColor="#CCFFFF" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" SortExpression="NG1" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblng1" runat="server" Text='<%#Bind("NG1","{0:n}") %>' OnClick="lblSlip_Click"
                                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootng1" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" SortExpression="NG2" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblng2" runat="server" Text='<%#Bind("NG2","{0:n}") %>' OnClick="lblSlip_Click"
                                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootng2" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" SortExpression="NG3" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblng3" runat="server" Text='<%#Bind("NG3","{0:n}") %>' OnClick="lblSlip_Click"
                                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootng3" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" SortExpression="NG4" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblng4" runat="server" Text='<%#Bind("NG4","{0:n}") %>' OnClick="lblSlip_Click"
                                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootng4" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" SortExpression="NGother" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblng5" runat="server" Text='<%#Bind("NGother","{0:n}") %>' OnClick="lblSlip_Click"
                                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootng5" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Non-Govt<br>Deductions" SortExpression="Ded1" Visible="true"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblded1" runat="server" Text='<%# Bind("Ded1","{0:n}") %>' OnClick="lblSlip_Click"
                                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootnongovttotded" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" SortExpression="G1" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblg1" runat="server" Text='<%#Bind("G1","{0:n}") %>' OnClick="lblSlip_Click"
                                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootg1" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" SortExpression="G2" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblg2" runat="server" Text='<%#Bind("G2","{0:n}") %>' OnClick="lblSlip_Click"
                                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootg2" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" SortExpression="G3" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblg3" runat="server" Text='<%#Bind("G3","{0:n}") %>' OnClick="lblSlip_Click"
                                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootg3" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" SortExpression="G4" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblg4" runat="server" Text='<%#Bind("G4","{0:n}") %>' OnClick="lblSlip_Click"
                                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootg4" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" SortExpression="Gother" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblg5" runat="server" Text='<%#Bind("Gother","{0:n}") %>' OnClick="lblSlip_Click"
                                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootg5" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Govt<br>Deductions" SortExpression="Ded2" Visible="true"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblded2" runat="server" Text='<%#Bind("Ded2","{0:n}") %>' OnClick="lblSlip_Click"
                                                                        ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootgovtotalded" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <%-- <asp:TemplateField HeaderText="Total<br>Deductions" SortExpression="TotalDed" Visible="true"
                                                                        ItemStyle-BackColor="#ffffcc" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lblTotalDed" runat="server" Text='<%# Bind("TotalDed","{0:n}") %>'
                                                                                ForeColor="Black"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle BackColor="#FFFFCC" />
                                                                    </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Net<br>Payable" SortExpression="remnetamt" Visible="true"
                                                                ItemStyle-BackColor="#99ccff" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblnetamt" runat="server" Text='<%# Bind("remnetamt","{0:n}") %>'
                                                                        OnClick="lblSlip_Click" ForeColor="Black"></asp:LinkButton></ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblfootnetpay" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle BackColor="#99CCFF" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Paid/<br>Unpaid" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="70px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpaidamt" runat="server" Visible='<%#Bind("repa") %>' Text="Paid"></asp:Label>
                                                                    <asp:CheckBox ID="chkpaid" runat="server" Visible='<%#Bind("repab") %>' Checked="true" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderText="Relevant Account" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlaccouts" Visible="true" runat="server" Width="100px">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderText="Cheque No">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtchkno" runat="server" Width="60px"></asp:TextBox>
                                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtrate"
                                                                                                                ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                                HeaderText="Paid Amount">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtpaidamt" runat="server" Width="60px"></asp:TextBox>
                                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtrate"
                                                                                                                ErrorMessage="*" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                                    <asp:ImageButton ID="llinkbb" runat="server" ToolTip="Delete" CommandArgument='<%# Eval("SalId") %>'
                                                                        CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                        Visible="false" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-Width="20px"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="llinedit" runat="server" CommandArgument='<%# Bind("EmployeeId") %>'
                                                                        CommandName="Edit1" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <PagerStyle CssClass="pgr" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle">
                                    <asp:Button ID="btnSuball" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="btnSuball_Click"
                                        Visible="False" />
                                </td>
                            </tr>
                           
                        </table>
                    </fieldset>
                </asp:Panel>
            </div>
        </fieldset>
    </div>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
