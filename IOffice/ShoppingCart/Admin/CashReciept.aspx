<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="CashReciept.aspx.cs" Inherits="CashReciept"
    Title="Cash Reciept" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/jscript" language="javascript">
        function temp() {


        }
    </script>

    <script language="javascript" type="text/javascript">
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

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <div style="float: left;">
                        <asp:Label ID="Label2" runat="server" Text="Label" ForeColor="Red" Font-Bold="False"></asp:Label>
                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <asp:Panel ID="pnnl" Visible="false" runat="server">
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        <label>
                                            <asp:Label ID="dd" Text="Cash Reciept Entry For the Following Documents" runat="server"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col2" align="center">
                                        <label>
                                            Doc ID
                                            <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                        </label>
                                        <label>
                                            Title
                                            <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                                        </label>
                                        <label>
                                            Date
                                            <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                                        </label>
                                        <label>
                                            Cabinet/Drawer/Folder
                                            <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <div>
                        <label>
                            Business Name
                            <asp:RequiredFieldValidator ID="ddddff" runat="server" ControlToValidate="ddlwarehouse"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            Date
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
                                ErrorMessage="*" ControlToValidate="txtTodate">
                            </asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtTodate" runat="server" Width="100px" ValidationGroup="1"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                                TargetControlID="txtTodate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1534" runat="server" CultureName="en-AU"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtTodate" />
                        </label>
                        <label>
                            <br />
                            <asp:ImageButton ID="ImageButton1" OnClick="ImageButton1_Click" runat="server" ImageUrl="~/images/cal_btn.jpg">
                            </asp:ImageButton>
                        </label>
                        <label>
                            Entry Number
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1"
                                ErrorMessage="*" ControlToValidate="txtenteryNumber"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtenteryNumber" runat="server" ValidationGroup="1" Enabled="False"></asp:TextBox>
                        </label>
                        <label>
                            Cash/Banking account
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAccount"
                                InitialValue="0" ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlAccount" runat="server" ValidationGroup="1" AutoPostBack="False">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <br />
                            <asp:ImageButton ID="lnkadd" runat="server" Height="15px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                Width="20px" ToolTip="AddNew" OnClick="LinkButton97666667_Click" />
                        </label>
                        <label>
                            <br />
                            <asp:ImageButton ID="lnkadd0" runat="server" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                OnClick="lnkadd0_Click" AlternateText="Refresh" Height="15px" Width="20px" ToolTip="Refresh" />
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            Amount
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
                                ErrorMessage="*" ControlToValidate="txtAmount">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtAmount"
                                ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ValidationGroup="1"
                                ErrorMessage="Invalid Digits" Display="Dynamic"> 
                            </asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtAmount" runat="server" onkeyup="return mak('Span1',15,this)"
                                ValidationGroup="1" MaxLength="15" OnTextChanged="txtAmount_TextChanged" AutoPostBack="True">0</asp:TextBox>
                            <asp:Label ID="Fgdf" runat="server" Text="Max " CssClass="labelcount"></asp:Label>
                            <span id="Span1" class="labelcount">15</span>
                            <asp:Label ID="Label15" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label>
                            <cc1:FilteredTextBoxExtender ID="txtaddress_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtAmount" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </label>
                        <label>
                            Memo
                            <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                ControlToValidate="txtMemo" ValidationGroup="1">
                        
                            </asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtMemo" runat="server" MaxLength="500" Width="400px" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z.0-9_ ]+$/,'div1',500)"></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" Text="Max " CssClass="labelcount"></asp:Label>
                            <span id="div1" class="labelcount">500</span>
                            <asp:Label ID="Label17" runat="server" Text="(a-z 0-9 _ .)" CssClass="labelcount">
                            </asp:Label>
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="panelafamt" runat="server" Visible="false">
                        <div>
                            <asp:RadioButtonList Style="text-align: left" ID="rbClassType" runat="server" AutoPostBack="True"
                                RepeatDirection="Horizontal" OnSelectedIndexChanged="rbClassType_SelectedIndexChanged">
                                <asp:ListItem Value="2">Receive payment From User</asp:ListItem>
                                <asp:ListItem Value="1">Other Receipt</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="Panel3" runat="server">
                            <label>
                                <asp:Label ID="lblpartytype" Text="User Type" runat="server"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldVadator14" runat="server" ControlToValidate="ddlpartytype" SetFocusOnError="true"
                                    ErrorMessage="*" ValidationGroup="1" InitialValue="0">
                                </asp:RequiredFieldValidator><asp:DropDownList ID="ddlpartytype" runat="server" OnSelectedIndexChanged="ddlpartytype_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="lblpartname" Text="User Name" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlPartyName" runat="server" ValidationGroup="1" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <br />
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                    Height="20px" ImageAlign="Bottom" Width="20px" ToolTip="AddNew" OnClick="LinkButton97666667_Click1" />
                            </label>
                            <label>
                                <br />
                                <asp:ImageButton ID="ImageButton4" runat="server" Height="20px" Width="20px" ToolTip="Refresh"
                                    ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton1_Click" />
                            </label>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="pnlinvdata" runat="server" Visible="true">
                            <label>
                                <asp:CheckBox ID="chkappamount" runat="server" AutoPostBack="True" OnCheckedChanged="chkappamount_CheckedChanged"
                                    Text="Apply this amount to invoice/sales order" Visible="False" />
                            </label>
                            <label>
                                <asp:CheckBox ID="chkbkinvoice" runat="server" OnCheckedChanged="chkbkinvoice_CheckedChanged"
                                    Text="Select Invoice to apply this payment" Visible="False" AutoPostBack="True" />
                            </label>
                            <label>
                                <asp:CheckBox ID="chkorder" runat="server" Text="Select Sales order to apply this payment"
                                    Visible="False" AutoPostBack="True" OnCheckedChanged="chkorder_CheckedChanged" />
                            </label>
                            <div style="clear: both;">
                            </div>
                            <asp:Panel ID="Panel2" runat="server" Visible="False">
                                <table>
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="GridView2" runat="server" EmptyDataText="No Record Found." AutoGenerateColumns="false"
                                                CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                Visible="False" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Apply">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkbox" runat="server" Checked="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblentrytypeid" runat="server" Text='<%# Bind("EntryTypeId")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Entry Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblentrytype" runat="server" Text='<%# Bind("EntryType")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Entry No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblentryNo" runat="server" Text='<%# Bind("EntryNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Order No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsalesorderNo" runat="server" Text='<%# Bind("SalesOrderNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsalesorderId" runat="server" Text='<%# Bind("SalesOrderId")%>'
                                                                Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Original Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltrnamt" runat="server" Text='<%# Bind("TranAmount")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount Due">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbaldue" runat="server" Text='<%# Bind("DueBalance")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTrnId" runat="server" Text='<%# Bind("TransactionId")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Apply Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtnewamt" runat="server"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="txtEndzip_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtnewamt" ValidChars="0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div style="clear: both;">
                            </div>
                            <asp:Panel ID="Panel1" runat="server" Visible="False">
                                <table id="Table1" width="100%">
                                    <tr>
                                        <td style="width: 40%">
                                            <label>
                                                Select Account Name
                                            </label>
                                            <label>
                                                <%--<asp:HyperLink ID="HyperLink1" runat="server" Font-Size="12px" Target="_blank" NavigateUrl="~/ShoppingCart/Admin/AccountMaster.aspx"
                                Font-Bold="True">Add New</asp:HyperLink>--%>
                                                <asp:ImageButton ID="LinkButton97666667" runat="server" Height="15px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                    Width="20px" ToolTip="AddNew" OnClick="LinkButton112244_Click" />
                                            </label>
                                            <label>
                                                <%--<asp:LinkButton ID="LinkButton2" runat="server" Font-Size="12px" Font-Bold="True"
                                OnClick="LinkButton2_Click">Refresh</asp:LinkButton>--%>
                                                <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                                    OnClick="LinkButton2_Click" AlternateText="Refresh" Height="15px" Width="20px"
                                                    ToolTip="Refresh" />
                                            </label>
                                        </td>
                                        <td style="width: 25%">
                                            <label>
                                                Amount
                                                <asp:Label ID="Label13" runat="server" Text="(Max 30 Digit(0-9 .) "></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 25%">
                                            <label>
                                                Memo<asp:Label ID="Label14" runat="server" Text="(Max 300 Chars(A-Z 0-9 _ .) "></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 10%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlAccountname1" runat="server" OnSelectedIndexChanged="ddlAccountname1_SelectedIndexChanged1"
                                                AutoPostBack="True" Width="350px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount1" runat="server" MaxLength="30"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender323" runat="server"
                                                Enabled="True" TargetControlID="txtAmount1" ValidChars="0147852369.">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblamt1" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtmemo1" runat="server" MaxLength="300"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1112" runat="server"
                                                ErrorMessage="Invalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtmemo1" ValidationGroup="1">
                        
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlAccountname2" runat="server" OnSelectedIndexChanged="ddlAccountname2_SelectedIndexChanged"
                                                AutoPostBack="True" Width="350px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount2" runat="server" MaxLength="30"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                TargetControlID="txtAmount2" ValidChars="0147852369.">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblamt2" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtmemo2" runat="server" MaxLength="300"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1546" runat="server"
                                                ErrorMessage="Invalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtmemo2" ValidationGroup="1">
                        
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlAccountname3" runat="server" OnSelectedIndexChanged="ddlAccountname3_SelectedIndexChanged"
                                                AutoPostBack="True" Width="350px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount3" runat="server" MaxLength="30"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                                TargetControlID="txtAmount3" ValidChars="0147852369.">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblamt3" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtmemo3" runat="server" MaxLength="300"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtmemo3" ValidationGroup="1">
                        
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlAccountname4" runat="server" OnSelectedIndexChanged="ddlAccountname4_SelectedIndexChanged"
                                                AutoPostBack="True" Width="350px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount4" runat="server" MaxLength="30"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                                TargetControlID="txtAmount4" ValidChars="0147852369.">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblamt4" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtmemo4" runat="server" MaxLength="300"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtmemo4" ValidationGroup="1">
                        
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlAccountname5" runat="server" OnSelectedIndexChanged="ddlAccountname5_SelectedIndexChanged"
                                                AutoPostBack="True" Width="350px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount5" runat="server" MaxLength="30"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                                TargetControlID="txtAmount5" ValidChars="0147852369.">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblamt5" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtmemo5" runat="server" MaxLength="300"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtmemo5" ValidationGroup="1">
                        
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlAccountname6" runat="server" OnSelectedIndexChanged="ddlAccountname6_SelectedIndexChanged"
                                                AutoPostBack="True" Width="350px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount6" runat="server" MaxLength="30"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                                TargetControlID="txtAmount6" ValidChars="0147852369.">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblamt6" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtmemo6" runat="server" MaxLength="300"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtmemo6" ValidationGroup="1">
                        
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlAccountname7" runat="server" OnSelectedIndexChanged="ddlAccountname7_SelectedIndexChanged"
                                                AutoPostBack="True" Width="350px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount7" runat="server" MaxLength="30"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" Enabled="True"
                                                TargetControlID="txtAmount7" ValidChars="0147852369.">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblamt7" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtmemo7" runat="server" MaxLength="300"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtmemo7" ValidationGroup="1">
                        
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlAccountname8" runat="server" OnSelectedIndexChanged="ddlAccountname8_SelectedIndexChanged"
                                                AutoPostBack="True" Width="350px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount8" runat="server" MaxLength="30"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                                TargetControlID="txtAmount8" ValidChars="0147852369.">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblamt8" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtmemo8" runat="server" MaxLength="300"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtmemo8" ValidationGroup="1">
                        
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlAccountname9" runat="server" OnSelectedIndexChanged="ddlAccountname9_SelectedIndexChanged"
                                                AutoPostBack="True" Width="350px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount9" runat="server" MaxLength="30"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" Enabled="True"
                                                TargetControlID="txtAmount9" ValidChars="0147852369.">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblamt9" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtmemo9" runat="server" MaxLength="300"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtmemo9" ValidationGroup="1">
                        
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Grand Total
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblgratot" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div style="clear: both;">
                            </div>
                            <div>
                                <label>
                                    <asp:CheckBox ID="chkdoc" runat="server" Text="Do you want to attach/upload documents related to this entry?" />
                                </label>
                            </div>
                            <div style="clear: both;">
                                <label>
                                    <asp:CheckBox ID="chkappentry" runat="server" Text="Approved for this entry" Visible="False" />
                                </label>
                            </div>
                            <div style="clear: both;">
                                <br />
                            </div>
                            <asp:Button ID="btnSubmit" CssClass="btnSubmit" Text="Submit" runat="server" ValidationGroup="1"
                                OnClick="btnSubmit_Click" />
                            <asp:Button ID="Button1" CssClass="btnSubmit" Text="Cancel" runat="server" OnClick="Button1_Click" />
                            <asp:Button ID="Button2" CssClass="btnSubmit" runat="server" Text="Edit" Visible="False">
                            </asp:Button>
                            <asp:Button ID="Button3" CssClass="btnSubmit" OnClick="Button3_Click" runat="server"
                                Text="Delete" Visible="False"></asp:Button>
                            <asp:Button ID="btnupdate" CssClass="btnSubmit" OnClick="Button2_Click" runat="server"
                                Text="Update" Visible="False" ValidationGroup="1"></asp:Button>
                            <div style="clear: both;">
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </fieldset>
            </div>
            <asp:Panel ID="Panel5" runat="server" CssClass="modalPopup" Width="35%">
                <fieldset>
                    <legend></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="lblm" runat="server">Please check the date.</asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Start Date of the Year is "></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="lblstartdate" runat="server"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="lblm0" runat="server">You can not select 
                                                    anydate earlier than that. </asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 26px">
                                <asp:Button ID="ImageButton2" Text="Cancel" runat="server" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel5" TargetControlID="HiddenButton222" CancelControlID="ImageButton2">
            </cc1:ModalPopupExtender>
            <div style="clear: both;">
            </div>
            <cc1:FilteredTextBoxExtender ID="txtEndzip_FilteredTextBoxExtender" runat="server"
                Enabled="True" TargetControlID="txtAmount" ValidChars="0123456789.">
            </cc1:FilteredTextBoxExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                TargetControlID="txtAmount1" ValidChars="0123456789.">
            </cc1:FilteredTextBoxExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                TargetControlID="txtAmount2" ValidChars="0123456789.">
            </cc1:FilteredTextBoxExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                TargetControlID="txtAmount3" ValidChars="0123456789.">
            </cc1:FilteredTextBoxExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                TargetControlID="txtAmount4" ValidChars="0123456789.">
            </cc1:FilteredTextBoxExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                TargetControlID="txtAmount5" ValidChars="0123456789.">
            </cc1:FilteredTextBoxExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                TargetControlID="txtAmount6" ValidChars="0123456789.">
            </cc1:FilteredTextBoxExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                TargetControlID="txtAmount7" ValidChars="-0123456789.">
            </cc1:FilteredTextBoxExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                TargetControlID="txtAmount8" ValidChars="0123456789.">
            </cc1:FilteredTextBoxExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                TargetControlID="txtAmount9" ValidChars="0123456789.">
            </cc1:FilteredTextBoxExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
