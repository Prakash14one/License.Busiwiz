<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="wizardDocumentEmailDownload.aspx.cs" Inherits="WizardAccount_DocumentEmailDownload"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ioffice/Account/UserControl/UControlWizardpanel.ascx" TagName="pnl" TagPrefix="pnl" %>

<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
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

    <script type="text/javascript" language="javascript">
        function ShowMyModalPopup() {
            var modal = $find("<%=ModalPopupExtender9.ClientID%>");
            modal.show();

        }
        function ShowMyModalPopup1() {
            var modal2 = $find("<%=ModalPopupExtender10.ClientID%>");
            modal2.show();

        }
         function ShowMyModalPopup121() 
        {  
         var modal21 = $find("<%=ModalPopupExtender1.ClientID%>");  
         modal21.show(); 
          
        }
          function ShowMyModalPopup1234() 
        {  
         var modal234 = $find("<%=ModalPopupExtender2.ClientID%>");  
         modal234.show(); 
          
        }
    </script>

    <script language="javascript" type="text/javascript">

        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= Panel11.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=0,status=0');
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


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186 || evt.keyCode == 59) {


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
         function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 1%">
                <asp:Label ID="lblmsg" runat="server" Visible="false" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <div>
                        <label>
                      
                            <asp:Label ID="lbldoyou" runat="server" Text="Do You Wish to Setup Rules to Auto Retrieve Documents From Email Account ?"></asp:Label>
                        </label>
                       
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem>No</asp:ListItem>
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                            </asp:RadioButtonList>
                      
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <asp:Panel ID="Panel1" runat="server" Width="100%" Visible="False">
                            <table width="100%">
                                <tr>
                                    <td style="width: 25%">
                                        <label>
                                            <asp:Label ID="Label4" runat="server" Text="Business Name"></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 75%">
                                        <label>
                                            <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label5" runat="server" Text="Incoming Email Server(PoP3)"></asp:Label>
                                            <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtServer"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9@._]*)" ControlToValidate="txtServer"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtServer" runat="server" ValidationGroup="1" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.@ \s]+$/,'Span6',50)"
                                                MaxLength="50"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label29" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="Span6" class="labelcount">50</span>
                                            <asp:Label ID="Label23" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ @ .)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label12" runat="server" Text="E.g: mail.ifilecabinet.com"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label13" runat="server" Text="Auto Retrieval Time Interval(In Minutes)"></asp:Label>
                                            <asp:Label ID="Label24" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtautoretrival"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtautoretrival" runat="server" onKeydown="return mak('Span1',4,this)"
                                                MaxLength="4"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtEndzip_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" TargetControlID="txtautoretrival" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label8" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="Span1" class="labelcount">4</span>
                                            <asp:Label ID="Label20" runat="server" CssClass="labelcount" Text="(0-9)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label6" runat="server" Text="Email ID"></asp:Label>
                                            <asp:Label ID="Label25" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9@._]*)"
                                                ControlToValidate="txtEmail" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtEmail" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.@ \s]+$/,'Span2',50)"
                                                Width="200px" MaxLength="50"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label27" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="Span2" class="labelcount">50</span>
                                            <asp:Label ID="Label21" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ @ .)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label7" runat="server" Text="Password"></asp:Label>
                                            <asp:Label ID="Label26" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPwd"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.+-@a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtPwd" ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtPwd" runat="server" Width="200px" 
                                                TextMode="Password" MaxLength="30" ></asp:TextBox>
                                            <%--onKeydown="return mak('Span3',30,this)"--%>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label28" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="Span3" class="labelcount">30</span>
                                            <asp:Label ID="Label14" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ . @ + - )"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:CheckBox ID="Chkautoprcss" runat="server" Text="Filing Desk Approval Required" />
                                        </label>
                                        <br />
                                        <a onclick="ShowMyModalPopup121();">See more details.</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label3" runat="server" Text="Do you wish to create rules to auto organize your documents?"></asp:Label>
                                        </label>
                                        <label>
                                            <a onclick="ShowMyModalPopup1();">See more details.</a>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:RadioButtonList ID="rbtnlistsetrules" runat="server" AutoPostBack="True" RepeatDirection="Vertical"
                                                OnSelectedIndexChanged="rbtnlistsetrules_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="Set Dynamic Document Process Rule for This Email Account">
                                                   Set dynamic document process rule for this Email account
                                                    <a onclick="ShowMyModalPopup();">See more details.</a></asp:ListItem>
                                                <asp:ListItem Value="Set Fixed Document Process Rule for This Email Account">
                                                    
                                                    Set fixed document process rule
                                                     <a onclick="ShowMyModalPopup1234();">See more details.</a>
                                                    
                                                </asp:ListItem>
                                            </asp:RadioButtonList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlsetrule" runat="server" Visible="False" Width="100%">
                                            <div style="float: left;">
                                                <asp:Label ID="lbllegend" runat="server" Text="Fixed Rule for Retrieval of Documents From Email Account"
                                                    Font-Bold="true"></asp:Label>
                                            </div>
                                             <div style="clear: both;">
                    </div>
                                            <table cellpadding="0" cellspacing="3" width="100%">
                                                <tr>
                                                    <td colspan="2">
                                                        Please note that the below document title, cabinet, drawer, folder, party name 
                                                        and document description will be given to all documents retrieved from your 
                                                        Email account.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                        <label>
                                                            <asp:Label ID="Label15" runat="server" Text=" Document Title "></asp:Label>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*"
                                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s_]*)"
                                                                ControlToValidate="txtdoctitle" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtdoctitle"
                                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                        </label>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <label>
                                                            <asp:TextBox ID="txtdoctitle" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'Span4',30)"
                                                                Width="184px" MaxLength="30"></asp:TextBox>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="Label30" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                            <span id="Span4" class="labelcount">30</span>
                                                            <asp:Label ID="Label16" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                        </label>
                                                        <asp:CheckBox ID="chkboxoremail" runat="server" Text="Subject of Email" Visible="false"
                                                            Width="128px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label9" runat="server" Text="Cabinet-Drawer-Folder"></asp:Label>
                                                           
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:DropDownList ID="ddldoctype" runat="server" DataTextField="doctype" DataValueField="DocumentTypeId"
                                                                Width="600px">
                                                            </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgAdd" runat="server" AlternateText="Add New" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                Height="20px" ToolTip="Add New " Width="20px" OnClick="imgAdd_Click" />
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgRefresh" runat="server" AlternateText="Refresh" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                                                Height="20px" Width="20px" ToolTip="Refresh" OnClick="imgRefresh_Click" />
                                                        </label>
                                                    </td>
                                                </tr>
                                                   <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label36" runat="server" Text="Document Type"></asp:Label>
                                 <asp:Label ID="Labelxc" runat="server" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RiredFiealidator2" runat="server" ControlToValidate="ddldt"
                                        ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddldt" runat="server" ValidationGroup="1" 
                                     >
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                     Width="20px" AlternateText="Add New" Height="20px"
                                        ToolTip="AddNew" onclick="ImageButton4_Click"  />
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                        AlternateText="Refresh" Height="20px" Width="20px"
                                        ToolTip="Refresh" onclick="ImageButton5_Click"  />
                                </label>
                            </td>
                        </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label17" runat="server" Text="Party Name"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:DropDownList ID="ddlparty" runat="server" DataTextField="PartyName" DataValueField="PartyId">
                                                            </asp:DropDownList>
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgAdd2" runat="server" AlternateText="Add New" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                Height="20px" ToolTip="Add New " Width="20px" OnClick="imgAdd2_Click" />
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgRefresh2" runat="server" AlternateText="Refresh" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                                                Height="20px" Width="20px" ToolTip="Refresh" OnClick="imgRefresh2_Click" />
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label18" runat="server" Text="Document Description"></asp:Label>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                                ErrorMessage="*" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,300})$"
                                                                ControlToValidate="txtdocdesc" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z.0-9\s_]*)"
                                                                ControlToValidate="txtdoctitle" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:TextBox ID="txtdocdesc" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_. \s]+$/,'Span5',100)"
                                                                TextMode="MultiLine" Width="400px" Height="75px" MaxLength="100" onkeypress="return checktextboxmaxlength(this,100,event)"></asp:TextBox>
                                                        </label>
                                                        <label>
                                                            <asp:Label ID="Label32" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                            <span id="Span5" class="labelcount">100</span>
                                                            <asp:Label ID="Label19" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ .)"></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="imgbtnsubmit" runat="server" CssClass="btnSubmit" Text="Submit" ValidationGroup="1"
                                            OnClick="imgbtnsubmit_Click" />
                                        <asp:Button ID="imgbtnUpdate" runat="server" CssClass="btnSubmit" Text="Update" OnClick="imgbtnUpdate_Click"
                                            Visible="False" />
                                        <asp:Button ID="imgbtnReset" runat="server" CssClass="btnSubmit" Text="Cancel" 
                                            OnClick="imgbtnReset_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllist" runat="server" Text="List of Setup Rules for Auto Retrieval from Email"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <div style="float: right;">
                                    <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                        OnClick="Button2_Click" />
                                    <input id="Button1" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        class="btnSubmit" type="button" value="Print" visible="false" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label10" runat="server" Text="Select by Business"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel11" runat="server" Width="100%">
                                    <table id="Table3" width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblcom" runat="server" Font-Italic="True" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="Label22" runat="server" Text="Business:" Font-Italic="True"></asp:Label>
                                                                <asp:Label ID="lblCompany" runat="server" Font-Italic="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label2" runat="server" Font-Italic="True" Text="List of Setup Rules for Auto Retrieval from Email"
                                                                    ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView AutoGenerateColumns="False" ID="GridEmail" runat="server" Width="100%"
                                                    OnRowCommand="GridEmail_RowCommand" OnRowEditing="GridEmail_RowEditing" OnRowDeleting="GridEmail_RowDeleting"
                                                    OnRowCancelingEdit="GridEmail_RowCancelingEdit" EmptyDataText="No Record Found."
                                                    CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    AllowPaging="True" OnPageIndexChanging="GridEmail_PageIndexChanging" DataKeyNames="DocumentEmailDownloadID"
                                                    OnRowUpdating="GridEmail_RowUpdating" AllowSorting="True" OnSorting="GridEmail_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business" SortExpression="Wname" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblwname" runat="server" Text='<%# Eval("Wname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mail Server" SortExpression="ServerName" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblServerName" runat="server" Text='<%# Eval("Username")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Email ID" SortExpression="EmailId" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("EmailId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="DocumentAutoApprove" HeaderText="Filing Desk Approval Required"
                                                            HeaderStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="RuleType" HeaderText="Auto Organize Rule" HeaderStyle-HorizontalAlign="Left" />
                                                        <asp:TemplateField HeaderText="Auto Retrieval Time (Mins)" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblautoret" runat="server" Text='<%# Bind("AutoRetrival") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:CommandField ButtonType="Image" ShowEditButton="True" ShowHeader="True" HeaderText="Edit"
                                                            HeaderImageUrl="~/Account/images/edit.gif" EditImageUrl="~/Account/images/edit.gif"
                                                            ValidationGroup="qq" EditText="Edit" CancelText="Cancel" UpdateText="Update"
                                                            HeaderStyle-HorizontalAlign="Left"></asp:CommandField>--%>
                                                        <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("DocumentEmailDownloadID") %>'
                                                                    CommandName="Edit1" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="Delete" CommandName="Delete1"
                                                                    ImageUrl="~/Account/images/delete.gif" CommandArgument='<%# Bind("DocumentEmailDownloadID") %>'
                                                                    OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                                <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                                <input id="hdncnfm" type="hidden" name="hdncnfm" runat="Server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlMainTypeAdd" runat="server" BackColor="White" BorderColor="#999999"
                                    Width="70%" Height="500px" ScrollBars="Vertical" BorderStyle="Solid" BorderWidth="10px">
                                    <fieldset>
                                        <div align="right">
                                            <asp:ImageButton ID="ibtnCancelCabinetAdd" runat="server" AlternateText="Close" CausesValidation="False"
                                                ImageUrl="~/Account/images/closeicon.png" />
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <div>
                                            <label>
                                                For dynamic document processing, you can automatically process the document 
                                            and give it a title, folder name, document type, party name etc. based on the document file 
                                            name. However, please ensure that the document file name is written using the 
                                            syntax mentioned below. Note that every time a documents is retrieved from your 
                                            Email account, the system will read the file name and dynamically process the 
                                            file based on the file name.
                                            </label>
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <label>
                                            Syntax Rules :
                                        </label>
                                        <div style="clear: both;">
                                        </div>
                                        <label>
                                            Please note, that the following rules below must be followed for a 
                                        successful file download.
                                        </label>
                                        <div style="clear: both;">
                                        </div>
                                        <label>
                                            The file name should not have any spaces. Instead use an under score between 
                                        the words. For example, ABC_DEF
                                        </label>
                                        <div style="clear: both;">
                                        </div>
                                        <label>
                                            The file name should include the following syntax. Note that not all fields 
                                        are necessary, and you can choose what fields you wish to use.
                                        </label>
                                        <div style="clear: both;">
                                        </div>
                                        <label>
                                            The system will read the name of the file, based on the name that You have 
                                        given to that file.
                                        </label>
                                        <div style="clear: both;">
                                        </div>
                                        <label>
                                            You can have the system automatically select the party name for your 
                                        document if the document file name includes anywhere the words Party=(name of 
                                        party)
                                        </label>
                                        <label>
                                            You can have the system automatically insert the reference number of your 
                                        document, if the document file name includes anywhere the words Ref=(reference 
                                        number)
                                        </label>
                                        <label>
                                            You can have the system automatically select the folder for your document, 
                                        if the document file name includes anywhere the words Folder=(folder name)
                                        </label>
                                        <label>
                                                You can have the system automatically select the document type for your document, if the
                                                document file name includes anywhere the words type=(document type)
                                            </label>
                                        <label>
                                            You can have the system automatically give the document date, if the 
                                        document file names includes anywhere the date in the format (with no slashes) 
                                        DT=(MMDDYYYY)
                                        </label>
                                        <label>
                                            You can have have the system automatically insert the amount for the 
                                        document, if the document file name includes anywhere the amount in the format 
                                        AMT=(Amount)
                                        </label>
                                        <label>
                                            You can have the system automatically insert the document title, if the 
                                        document file name includes anywhere the words Title=(title name)
                                        </label>
                                        <div style="clear: both;">
                                        </div>
                                        <label>
                                            For example: 
                                        Ref=123456_Folder=SalesInvoice_Type=CashInvoice_Party=FakeCompany_Title=InvoiceFromOctober_DT=12222012_AMT=123.45
                                        </label>
                                        <div style="clear: both;">
                                        </div>
                                        <label>
                                            You can select what fields you want to be automatically read and fed into 
                                        the table. None of the fields are compulsory.
                                        </label>
                                        <div style="clear: both;">
                                        </div>
                                        <label>
                                            Furthermore, if the system does not find the folder name, or party name that 
                                        You can have mentioned, it will create a new folder or party.
                                        </label>
                                        <div style="clear: both;">
                                        </div>
                                        <label>
                                            In addition, even the first word of the party name is used, instead of the 
                                        full party name, the system will check that word into the list of the parties, 
                                        and whichever first party has that word as the first word, it will connect to 
                                        that party.
                                        </label>
                                        <div style="clear: both;">
                                        </div>
                                        <label>
                                            Example:If FakeCompany123 is the party name in your list of parties and you 
                                        only put Party=Fake then the system will check for the word Fake in the list, 
                                        and the put the document into that party list.
                                        </label>
                                    </fieldset></asp:Panel>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender9" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="pnlMainTypeAdd" TargetControlID="hdnMaintypeAdd" CancelControlID="ibtnCancelCabinetAdd">
                                </cc1:ModalPopupExtender>
                                <input id="hdnMaintypeAdd" runat="Server" name="hdnMaintypeAdd" type="hidden" style="width: 4px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlconfirmmsg" runat="server" BorderStyle="Outset" Height="100px"
                                    Width="300px" BackColor="#CCCCCC" BorderColor="#666666">
                                    <table id="innertbl1" width="100%">
                                        <tr>
                                            <td>
                                                <table id="subinnertbl1" cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td class="secondtblfc1">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="pnlconfirmmsgub" runat="server" Width="300Px">
                                                                    <table cellspacing="0" cellpadding="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    Do you wish to delete this record?
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Button ID="imgconfirmok" runat="server" Text="Yes" OnClick="imgconfirmok_Click" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="imgconfirmcalcel" runat="server" Text="No" OnClick="imgconfirmcalcel_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <cc1:ModalPopupExtender ID="mdlpopupconfirm" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="pnlconfirmmsg" TargetControlID="hdnconfirm" CancelControlID="imgconfirmcalcel"
                                    X="250" Y="-200" Drag="true">
                                </cc1:ModalPopupExtender>
                                <input id="hdnconfirm" runat="Server" name="hdnconfirm" type="hidden" style="width: 4px" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="#999999" Width="600px"
                                    BorderStyle="Solid" BorderWidth="10px">
                                    <fieldset>
                                        <legend>Key to Follow This Rule </legend>
                                        <div align="right">
                                            <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Close" CausesValidation="False"
                                                ImageUrl="~/Account/images/closeicon.png"></asp:ImageButton>
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <div>
                                            <label>
                                                There are two ways of auto organizing your documents.
                                            </label>
                                            <div style="clear: both;">
                                            </div>
                                            <label>
                                                The first is a fixed rule. A fixed rule is setup so that all documents you 
                                            are retrieving from a specific folder will automatically be given a fixed title, 
                                            party name or folder, depending on the rule you have set.
                                            </label>
                                            <div style="clear: both;">
                                            </div>
                                            <label>
                                                The second is a dynamic rule. A dynamic rule is setup so that all documents 
                                            you are retrieving from a specific folder will have the file name read by the 
                                            system and will give the document title, party name and folder, and many other 
                                            variables depending on the file name.
                                            </label>
                                        </div>
                                    </fieldset></asp:Panel>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender10" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="ImageButton1" PopupControlID="Panel2" TargetControlID="Hidden101">
                                </cc1:ModalPopupExtender>
                                <input id="Hidden101" runat="Server" name="Hidden101" style="width: 4px" type="hidden" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="Panel3" runat="server" BackColor="White" BorderColor="#999999" Width="600px"
                                    BorderStyle="Solid" BorderWidth="10px">
                                    <fieldset>
                                        <div align="right">
                                            <asp:ImageButton ID="ImageButton2" runat="server" AlternateText="Close" CausesValidation="False"
                                                ImageUrl="~/Account/images/closeicon.png"></asp:ImageButton>
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <div>
                                            <label>
                                                If this option is selected, the document will be sent to the filing desk for 
                                            further processing. In this case, no documents can be viewed by any user, other 
                                            than users in the filing desk department with the appropriate rights. If this 
                                            option is not selected, the document would be available for viewing by all 
                                            users, depending on the document rights assigned to them.
                                            </label>
                                        </div>
                                    </fieldset>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                        CancelControlID="ImageButton2" Drag="true" PopupControlID="Panel3" TargetControlID="Hidden1">
                                    </cc1:ModalPopupExtender>
                                    <input id="Hidden1" runat="Server" name="Hidden1" style="width: 4px" type="hidden" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="Panel4" runat="server" BackColor="White" BorderColor="#999999" Width="600px"
                                    BorderStyle="Solid" BorderWidth="10px">
                                    <fieldset>
                                        <div align="right">
                                            <asp:ImageButton ID="ImageButton3" runat="server" AlternateText="Close" CausesValidation="False"
                                                ImageUrl="~/Account/images/closeicon.png"></asp:ImageButton>
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <div>
                                            <label>
                                                For fixed document processing, you can automatically process the document 
                                            and give it a title, folder name, party name etc. based on the rule that you 
                                            have set below. Note that every time a documents is retrieved from your FTP 
                                            account, the system will give the same file name and put it in the same folder, 
                                            and set the same party name as per the rule that you have set. Please note that 
                                            it will not read the file name for processing the document.
                                            </label>
                                        </div>
                                    </fieldset>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                        CancelControlID="ImageButton3" Drag="true" PopupControlID="Panel4" TargetControlID="Hidden2">
                                    </cc1:ModalPopupExtender>
                                    <input id="Hidden2" runat="Server" name="Hidden2" style="width: 4px" type="hidden" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="imgRefresh" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgRefresh2" EventName="Click"></asp:AsyncPostBackTrigger>
           


        </Triggers>
     
    </asp:UpdatePanel>
</asp:Content>
