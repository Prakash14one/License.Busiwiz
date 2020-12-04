<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="EmployerRemittance.aspx.cs"
    Inherits="EmployerRemittance" Title="Untitled Page" %>

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
        .mGridA
        {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-size: 13px !important;
        }
        .mGridA a
        {
            font-size: 15px !important;
            color: White;
        }
        .mGridA a:hover
        {
            font-size: 15px !important;
            color: White;
            text-decoration: underline;
        }
        .mGridA td
        {
            padding: 0px;
            border: solid 1px #c1c1c1;
            color: #717171;
        }
        .mGridA th
        {
            padding: 4px 2px;
            color: #fff;
            background-color: #416271;
            border-left: solid 1px #525252;
            font-size: 14px !important;
        }
        .mGridA .alt
        {
            background: #fcfcfc url(grd_alt.png) repeat-x top;
        }
        .mGridA .pgr
        {
            background-color: #416271;
        }
        .mGridA .ftr
        {
            background-color: #416271;
            font-size: 15px !important;
            color: White;
            border: solid 1px #525252;
        }
        .mGridA .pgr table
        {
            margin: 5px 0;
        }
        .mGridA .pgr td
        {
            border-width: 0;
            padding: 0 6px;
            border-left: solid 1px #666;
            font-weight: bold;
            color: #fff;
            line-height: 12px;
        }
        .mGridA .pgr a
        {
            color: Gray;
            text-decoration: none;
        }
        .mGridA .pgr a:hover
        {
            color: #000;
            text-decoration: none;
        }
        .mGridA input[type="checkbox"]
        {
            margin-top: 5px !important;
            width: 10px !important;
            float: left !important;
        }
        .mGridA input[type="radio"]
        {
            margin-top: 5px !important;
            width: 100px !important;
            float: left !important;
        }
        /*Ravi Patel  Start 26th Sep 2012*/.Attend input[type="checkbox"]
        {
            margin: 0px 5px 5px 5px !important;
            width: 20px !important;
            float: none !important;
        }
        .WorkingDay
        {
            width: 200px;
        }
        .WorkingDay input[type="checkbox"]
        {
            margin: 0px 5px 5px 5px !important;
            width: 20px !important;
            float: none !important;
        }
        .Attend input[type="text"]
        {
            width: 50px;
        }
        .radiobuttonlabel
        {
            margin: -10px 0px 0px 20px !important;
        }
        /*Ravi Patel End 26th Sep 2012*//*Ravi Patel Start 27th Sep 2012*/.radiobuttonlabelEmployePayroll
        {
            margin: -2px 0px 0px 20px !important;
        }
        .InventoryCheckbox
        {
            padding-top: 9px;
        }
        .InventoryCheckbox input[type="checkbox"]
        {
            margin: 5px 5px 0px 5px !important;
            width: 20px !important;
            float: none !important;
        }
        /*Ravi Patel End 27th Sep 2012*/.labelstar
        {
            color: Red;
        }
        .labelcount
        {
            font-size: 10px;
        }
        .lblcount
        {
            font-size: 10px;
        }
    </style>

    <script language="javascript" type="text/javascript">


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

    <asp:UpdatePanel ID="udfd" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="statuslable" runat="server" Text="Employer Payroll Govt Remittances"></asp:Label>
                    </legend>
                    <label>
                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <fieldset>
                        <legend></legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="right">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label6" runat="server" Text="Business"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" Width="150px"
                                                        OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <input id="hdnsortExp11" runat="Server" name="hdnsortExp" type="hidden" />
                                                    <input id="hdnsortDir11" runat="Server" name="hdnsortDir" type="hidden" />
                                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" type="hidden" />
                                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" type="hidden" /></label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label1" runat="server" Text="Year"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlYear" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label10" runat="server" Text="Pay Period"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlperiod" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkemployee" runat="server" />
                                            </td>
                                            <td>
                                                <label>
                                                    Show Employee Deduction Details</label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Button ID="btnGo" runat="server" Text=" Go " CssClass="btnSubmit" OnClick="btnGo_Click" />
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <div style="clear: both;">
                        <asp:Panel ID="pnlsh" runat="server" Visible="false">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lblGr1" runat="server" Text=""></asp:Label></legend>
                                <div style="clear: both;">
                                    <label>
                                        List of Employer Payroll Govt Remittances</label>
                                    <div style="float: right;">
                                        <asp:Button ID="btnmpr" runat="server" CausesValidation="false" Text="Printable Version"
                                            CssClass="btnSubmit" OnClick="btnmpr_Click" />
                                        <input id="btnpm" runat="server" visible="false" onclick="document.getElementById('Div1').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                            class="btnSubmit" type="button" value="Print" />
                                    </div>
                                </div>
                                <div style="clear: both;">
                                </div>
                                <table style="width: 100%">
                                    <%--<tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <label>
                                                            List of Employer Payroll Govt Remittances</label>
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
                                            </table>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="right">
                                            <asp:Panel ID="Panel1" runat="server" Visible="true">
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr align="center">
                                                        <td>
                                                            <div id="Div1" class="closed">
                                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="lbltrbal" runat="server" Style="text-align: center; font-family: Calibri;
                                                                                font-size: 18px; font-weight: bold;" Text="List of Employer Remittance for payroll deductions"> </asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="text-align: center; font-size: 16px; font-weight: bold; "">
                                                                            <asp:Label ID="lblbusnn" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="text-align: center; font-size: 16px; font-weight: bold; "">
                                                                            <asp:Label ID="lblpayperiod" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlgpay" runat="server" Visible="true" Width="100%" ScrollBars="None">
                                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                                    ShowFooter="true" FooterStyle-BackColor="#416271" 
                                                                    EmptyDataText="No Record Found." PagerStyle-CssClass="pgr" CssClass="mGridA"
                                                                    Width="100%" DataKeyNames="TaxId" HeaderStyle-HorizontalAlign="Left" OnRowCreated="GridView1_RowCreated"
                                                                    OnSorting="GridView1_Sorting">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Tax Names" SortExpression="TaxNames" Visible="true"
                                                                            HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbltaxname" runat="server" Text='<%#Bind("TaxNames") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle BackColor="#8EB4BF" />
                                                                            <HeaderStyle BackColor="#8EB4BF" />
                                                                            <ItemStyle BackColor="#8EB4BF" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                            <FooterTemplate>
                                                                                <label>
                                                                                    <asp:Label ID="lblfoll" runat="server" Text="Total" Font-Size="15px" ForeColor="White"></asp:Label>
                                                                                </label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Deduction<br> Amt" SortExpression="EmpContamt" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblempcontemt" runat="server" Text='<%# Bind("EmpContamt","{0:n}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <label>
                                                                                    <asp:Label ID="lblfootempcontemt" runat="server" Font-Size="15px" ForeColor="White"></asp:Label>
                                                                                </label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Deduction<br> Date" SortExpression="Deddate" Visible="true"
                                                                            HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldeddate" runat="server" Text='<%# Bind("Deddate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="% of Emp<br>Deduction" SortExpression="ContRate" Visible="true"
                                                                            HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblconrate" runat="server" Text='<%# Bind("ContRate","{0:n}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle BackColor="#8EB4BF" />
                                                                            <HeaderStyle BackColor="#8EB4BF" />
                                                                            <ItemStyle BackColor="#8EB4BF" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount" SortExpression="ContAmt" Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcontamt" runat="server" Text='<%# Bind("ContAmt","{0:n}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle BackColor="#8EB4BF" />
                                                                            <HeaderStyle BackColor="#8EB4BF" />
                                                                            <ItemStyle BackColor="#8EB4BF" />
                                                                            <FooterTemplate>
                                                                                <label>
                                                                                    <asp:Label ID="lblfootcontamt" runat="server" Font-Size="15px" ForeColor="White"></asp:Label>
                                                                                </label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Total Remittance<br> Required" Visible="true" SortExpression="totremreqi"
                                                                            HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbltotremireq" runat="server" Text='<%# Bind("totremreqi","{0:n}") %>'></asp:Label>
                                                                                <asp:Label ID="lblcracc" runat="server" Text="0" Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblempacccId" runat="server" Text="0" Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <label>
                                                                                    <asp:Label ID="lblfoottotremireq" runat="server" Font-Size="15px" ForeColor="White"></asp:Label>
                                                                                </label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remittance<br> Date" Visible="true" HeaderStyle-Width="90px"
                                                                            SortExpression="totremreqi" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="lblremdatetax" runat="server" Width="80px" Text='<%# Bind("Remdate")%>'></asp:TextBox>
                                                                                <cc1:CalendarExtender ID="Calendaer1" runat="server" PopupButtonID="txtremcondate"
                                                                                    TargetControlID="lblremdatetax">
                                                                                </cc1:CalendarExtender>
                                                                                <cc1:MaskedEditExtender ID="MasEditExtender3" runat="server" Enabled="True" Mask="99/99/9999"
                                                                                    MaskType="Date" TargetControlID="lblremdatetax" />
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <label>
                                                                                  <%--  <asp:Label ID="lblgcn" runat="server" Text="Gen Rem Date" ForeColor="White"></asp:Label>--%>
                                                                                    <asp:TextBox ID="txtremcondate" Width="80px" runat="server"></asp:TextBox>
                                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtremcondate"
                                                                                        TargetControlID="txtremcondate">
                                                                                    </cc1:CalendarExtender>
                                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureName="en-AU"
                                                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtremcondate" />
                                                                                </label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount<br> remitted" Visible="true" SortExpression="amtremited"
                                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="83px">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="lblremamt" runat="server" Width="80px" Text='<%# Bind("Remamttax")%>'
                                                                                    AutoPostBack="true" OnTextChanged="txtbas1_TextChanged"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBbbbboxExtender1" runat="server" Enabled="True"
                                                                                    FilterType="Custom, Numbers" TargetControlID="lblremamt" ValidChars=".">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <label>
                                                                                    <asp:TextBox ID="lblfootremamt" Enabled="false" runat="server" Width="80px"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                                        FilterType="Custom, Numbers" TargetControlID="lblfootremamt" ValidChars=".">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Select Cash/Bank A/c <br>Used for Remittance" Visible="true"
                                                                            SortExpression="totremreqi" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlcashac" Width="155px" runat="server">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remittance Notes" Visible="true" SortExpression="RemNotes"
                                                                             HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="180px">
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="lblddd" runat="server" Text="Remittance Notes" ForeColor="White"></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="lblremnotes" runat="server" Text='<%# Bind("RemNotes")%>' Width="170px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <label>
                                                                                   <%-- <asp:Label ID="lblgns" runat="server" ForeColor="White" Text="General Notes"></asp:Label>--%>
                                                                                    <asp:TextBox ID="txtdetail" runat="server" Width="170px"></asp:TextBox>
                                                                                </label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle CssClass="pgr" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <%--<AlternatingRowStyle CssClass="alt" />--%>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="Button1" runat="server" Text="Save Remittance Details" CssClass="btnSubmit"
                                                                OnClick="btnsave_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Panel ID="pnlGrdemp" runat="server" Visible="true" Width="100%" ScrollBars="Horizontal">
                                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                List of Deductions and Employer Contribution Required for each employee
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="grdallemp" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                                                EmptyDataText="No Record Found." FooterStyle-BackColor="#416271" PagerStyle-CssClass="pgr"
                                                                                CssClass="mGridA" Width="100%" DataKeyNames="EmployeeId" OnSorting="grdallemp_Sorting"
                                                                                HeaderStyle-HorizontalAlign="Left" OnRowCreated="grdallemp_RowCreated" ShowFooter="True"
                                                                                AllowPaging="True" PageSize="40">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName" Visible="true"
                                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblempname" runat="server" Text='<%#Bind("EmployeeName") %>' ForeColor="Black"></asp:Label><asp:Label
                                                                                                ID="lblEmployeeId" runat="server" Text='<%#Bind("EmployeeId") %>' Visible="false"></asp:Label></ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                        <FooterTemplate>
                                                                                           
                                                                                                <asp:Label ID="lblfoll" runat="server" Text="Total" Font-Size="15px" ForeColor="White"></asp:Label>
                                                                                            
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="" SortExpression="G1" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblg1" runat="server" Text='<%#Bind("G1","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                                        <FooterStyle BackColor="#8EB4BF" />
                                                                                        <HeaderStyle BackColor="#8EB4BF" />
                                                                                        <ItemStyle BackColor="#8EB4BF" />
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblfootg1" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="" SortExpression="G2" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblg2" runat="server" Text='<%#Bind("G2","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                                        <FooterStyle BackColor="#8EB4BF" />
                                                                                        <HeaderStyle BackColor="#8EB4BF" />
                                                                                        <ItemStyle BackColor="#8EB4BF" />
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblfootg2" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="" SortExpression="G3" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblg3" runat="server" Text='<%#Bind("G3","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                                        <FooterStyle BackColor="#8EB4BF" />
                                                                                        <HeaderStyle BackColor="#8EB4BF" />
                                                                                        <ItemStyle BackColor="#8EB4BF" />
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblfootg3" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="" SortExpression="G4" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblg4" runat="server" Text='<%#Bind("G4","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                                        <FooterStyle BackColor="#8EB4BF" />
                                                                                        <HeaderStyle BackColor="#8EB4BF" />
                                                                                        <ItemStyle BackColor="#8EB4BF" />
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblfootg4" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="" SortExpression="G5" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblg5" runat="server" Text='<%#Bind("G5","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                                        <FooterStyle BackColor="#8EB4BF" />
                                                                                        <HeaderStyle BackColor="#8EB4BF" />
                                                                                        <ItemStyle BackColor="#8EB4BF" />
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblfootg5" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="" SortExpression="NG1" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblng1" runat="server" Text='<%#Bind("NG1","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblfootng1" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="" SortExpression="NG2" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblng2" runat="server" Text='<%#Bind("NG2","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblfootng2" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="" SortExpression="NG3" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblng3" runat="server" Text='<%#Bind("NG3","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblfootng3" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="" SortExpression="NG4" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblng4" runat="server" Text='<%#Bind("NG4","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblfootng4" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="" SortExpression="NG5" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblng5" runat="server" Text='<%#Bind("NG5","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblfootng5" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Total Remittance<br>Required" SortExpression="TotRemreq"
                                                                                        Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbltotremreq" runat="server" Text='<%# Bind("TotRemreq","{0:n}") %>'
                                                                                                ForeColor="Black"></asp:Label></ItemTemplate>
                                                                                        <FooterStyle BackColor="#8EB4BF" />
                                                                                        <HeaderStyle BackColor="#8EB4BF" />
                                                                                        <ItemStyle BackColor="#8EB4BF" />
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblfootremreq" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <FooterStyle BackColor="#416271" />
                                                                                <PagerStyle CssClass="pgr" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset></asp:Panel>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
