<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentAutoAllocationFolder.aspx.cs" Inherits="DocumentAutoAllocationFolder"
    Title="Untitled Page" %>

<%@ Register Src="~/Account/UserControl/UControlWizardpanel.ascx" TagName="pnl" TagPrefix="pnl" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1000px,height=1000px,toolbar=1,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML,'<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }  
          function mask(evt)
        { 

           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if( evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186 ||evt.keyCode==59  )
            { 
                
            
              alert("You have entered invalid character");
                  return false;
             }
             
             
               
            
        }  
        function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered invalid character");
            }
        
        
      
        
            counter=document.getElementById(id);
            
            if(txt1.value.length <= max_len)
            {
                remaining_characters=max_len-txt1.value.length;
                counter.innerHTML=remaining_characters;
            }
        }    
            function mak(id,max_len,myele)
        {
            counter=document.getElementById(id);
            
            if(myele.value.length <= max_len)
            {
                remaining_characters=max_len-myele.value.length;
                counter.innerHTML=remaining_characters;
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
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend></legend>
                    <table width="100%">
                        <tr>
                            <td colspan="3">
                                <label>
                                    <asp:Label ID="lblconfirm" runat="server" Text=" Do you wish to setup rules for Auto Retrieve Documents from Server Folder ?"></asp:Label>
                                </label>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
                                    <asp:ListItem Value="True">Yes</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <asp:Panel ID="pnl_FtpAccount_priceplan" runat="server" Width="100%">
                    <table width="100%">
                        <tr>
                            <td>
                                <fieldset>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="3">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 25%">
                                                            <label>
                                                                <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td style="width: 75%" colspan="2">
                                                            <label>
                                                                <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label3" runat="server" Text="Server Folder Path"></asp:Label>
                                                                <asp:Label ID="Label4" runat="server" Text="*"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtServer"
                                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.\\:/a-zA-Z0-9\s]*)"
                                                                    ControlToValidate="txtServer" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td colspan="2">
                                                            <label>
                                                                <asp:TextBox ID="txtServer" runat="server" onKeydown="return mak('div1',100,this)"
                                                                    ValidationGroup="1" Width="412px" MaxLength="100"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label7" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="div1" class="labelcount">100</span>
                                                                <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ . : / \ )">
                                                                </asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td colspan="2">
                                                            <label>
                                                                <asp:Label ID="lbleg" runat="server" Text="E.g: F:\FolderName\SubFolderName"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%">
                                                            <label>
                                                                <asp:Label ID="Label5" runat="server" Text="Auto retrival time interval (in Min)"></asp:Label>
                                                                <asp:Label ID="Label9" runat="server" Text="*"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtautoretrival"
                                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td style="width: 75%" colspan="2">
                                                            <label>
                                                                <asp:TextBox ID="txtautoretrival" runat="server" Width="171px" onKeydown="return mak('Span1',4,this)"
                                                                    MaxLength="4"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                    TargetControlID="txtautoretrival" ValidChars="0123456789">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label8" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="Span1" class="labelcount">4</span>
                                                                <asp:Label ID="Label11" runat="server" CssClass="labelcount" Text="(0-9)"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%">
                                                        </td>
                                                        <td style="width: 75%" colspan="2">
                                                            <label>
                                                                <asp:CheckBox ID="Chkautoprcss" runat="server" Text="Filing Desk Approval Required" />
                                                            </label>
                                                            <br />
                                                            <a onclick="ShowMyModalPopup121();">See more details.</a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%">
                                                        </td>
                                                        <td style="width: 75%" colspan="2">
                                                            <label>
                                                                <asp:Label ID="Label10" runat="server" Text="Do you wish to set rules to auto organize documents?"> </asp:Label>
                                                                <a onclick="ShowMyModalPopup1();">See more details.</a>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%">
                                                        </td>
                                                        <td style="width: 75%" colspan="2">
                                                            <asp:RadioButtonList ID="rbtnlistsetrules" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbtnlistsetrules_SelectedIndexChanged"
                                                                RepeatDirection="Vertical">
                                                                <asp:ListItem Selected="True" Value="Set Dynamic Document Process Rule for This Folder">
                                                                  Set dynamic document process rules for this server folder <a onclick="ShowMyModalPopup();">See more details.</a>
                                                                   
                                                                 
                                                                
                                                                </asp:ListItem>
                                                                <asp:ListItem Value="Set Fixed Document Process Rule for This Folder">
                                                               
                                                              Set fixed document process rule
                                                                   <a onclick="ShowMyModalPopup1234();">See more details.</a>
                                                                </asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <tr>
                                    <td colspan="3">
                                        <asp:Panel ID="pnlsetrule" runat="server" Width="100%" Visible="False">
                                            <fieldset>
                                                <legend>
                                                    <asp:Label ID="lbllegend" runat="server" Text=" Fixed rule for each document retrieved from Server Folder"></asp:Label>
                                                </legend>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 25%">
                                                            <label>
                                                                <asp:Label ID="Label12" runat="server" Text=" Document Title"></asp:Label>
                                                                <asp:Label ID="Label13" runat="server" Text="*"></asp:Label>
                                                                <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                                                    SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtdoctitle"
                                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdoctitle"
                                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td style="width: 75%" colspan="2">
                                                            <label>
                                                                <asp:TextBox ID="txtdoctitle" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'Span2',30)"
                                                                    runat="server" Width="250px" MaxLength="30"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label20" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="Span2" class="labelcount">30</span>
                                                                <asp:Label ID="Label21" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ )"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%">
                                                            <label>
                                                                <asp:Label ID="Label14" runat="server" Text="Cabinet-Drawer-Folder"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td style="width: 75%" colspan="2">
                                                            <label>
                                                                <asp:DropDownList ID="ddldoctype" runat="server" DataTextField="doctype" DataValueField="DocumentTypeId"
                                                                    Width="550px">
                                                                </asp:DropDownList>
                                                            </label>
                                                            <span style="padding-left: 5Px; padding-right: 10Px">
                                                                <label>
                                                                    <asp:ImageButton ID="imgAdd" runat="server" AlternateText="Add New" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                        Height="20px" ToolTip="Add New " Width="20px" OnClick="imgAdd_Click" />
                                                                </label>
                                                            </span><span style="padding-left: 10Px; padding-right: 10Px">
                                                                <label>
                                                                    <asp:ImageButton ID="imgRefresh" runat="server" AlternateText="Refresh" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                                                        Height="20px" Width="20px" ToolTip="Refresh" OnClick="imgRefresh_Click" />
                                                                </label>
                                                            </span>
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
                                                        <td style="width: 25%">
                                                            <label>
                                                                <asp:Label ID="Label15" runat="server" Text="User Name"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td style="width: 75%" colspan="2">
                                                            <label>
                                                                <asp:DropDownList ID="ddlparty" runat="server" DataTextField="PartyName" DataValueField="PartyId">
                                                                </asp:DropDownList>
                                                            </label>
                                                            <span style="padding-left: 5Px; padding-right: 10Px">
                                                                <label>
                                                                    <asp:ImageButton ID="imgAdd2" runat="server" AlternateText="Add New" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                        Height="20px" ToolTip="Add New " Width="20px" OnClick="imgAdd2_Click" />
                                                                </label>
                                                            </span><span style="padding-left: 5Px; padding-right: 10Px">
                                                                <label>
                                                                    <asp:ImageButton ID="imgRefresh2" runat="server" AlternateText="Refresh" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                                                        Height="20px" Width="20px" ToolTip="Refresh" OnClick="imgRefresh2_Click" />
                                                                </label>
                                                            </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%">
                                                            <label>
                                                                <asp:Label ID="Label16" runat="server" Text="Document Description"></asp:Label>
                                                                <asp:Label ID="Label17" runat="server" Text="*"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="true" ControlToValidate="txtdocdesc"
                                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                                    ControlToValidate="txtdocdesc" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td style="width: 75%" colspan="2">
                                                            <label>
                                                                <asp:TextBox ID="txtdocdesc" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_. \s]+$/,'Span3',100)"
                                                                    TextMode="MultiLine" Width="350px" MaxLength="100" onkeypress="return checktextboxmaxlength(this,100,event)"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label6" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="Span3" class="labelcount">100</span>
                                                                <asp:Label ID="Label22" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ .)">
                                                                </asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset></asp:Panel>
                                    </td>
                                </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 25%">
                                        </td>
                                        <td style="width: 75%" colspan="2">
                                            <asp:Button ID="imgbtnsubmit" runat="server" CssClass="btnSubmit" Text="Submit" ValidationGroup="1"
                                                OnClick="imgbtnsubmit_Click" />
                                            <asp:Button ID="imgbtnUpdate" runat="server" CssClass="btnSubmit" OnClick="imgbtnUpdate_Click"
                                                Visible="False" Text="Update" ValidationGroup="1" />
                                            <asp:Button ID="imgbtnReset" runat="server" CssClass="btnSubmit" OnClick="imgbtnReset_Click"
                                                Text="Cancel" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                    <fieldset>
                    
                        <legend>
                            <asp:Label ID="lbllist" runat="server" Text="List of Server Folder for auto retrieval"></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td colspan="3">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="3">
                                                <div style="float: right;">
                                                    <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                                        OnClick="Button1_Click" />
                                                    <input id="Button1" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                                        class="btnSubmit" type="button" value="Print" visible="false" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:Label ID="Label18" runat="server" Text="Filter by Business Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 75%" colspan="2">
                                                <label>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                                        Width="205px">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                        <table id="GridTbl" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:Panel ID="Panel2" runat="server" Width="100%">
                                                            <table width="100%" cellpadding="5" cellspacing="1" border="0" style="vertical-align: top;">
                                                                <tr align="center">
                                                                    <td>
                                                                        <div id="mydiv" class="closed">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <asp:Label ID="lblCompany" runat="server" Font-Bold="true" Font-Size="20px" Font-Italic="True"
                                                                                            Visible="false"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <asp:Label ID="Label19" runat="server" Font-Bold="true" Font-Size="18px" Text="Business:"
                                                                                            Font-Italic="True"></asp:Label>
                                                                                        <asp:Label ID="lblBusiness" runat="server" Font-Bold="true" Font-Size="18px" Font-Italic="True"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Italic="True" Font-Size="18px"
                                                                                            Text="List of Server Folder for auto retrieval" ForeColor="Black"></asp:Label>
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
                                                                            OnPageIndexChanging="GridEmail_PageIndexChanging" DataKeyNames="FolderId" AllowSorting="True"
                                                                            OnSorting="GridEmail_Sorting">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Business" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left"
                                                                                    SortExpression="Wname" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblBusiness" runat="server" Text='<%# Eval("Wname")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Folder Path" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left"
                                                                                    SortExpression="FolderName" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblFolderName" runat="server" Text='<%# Eval("FolderName")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                   <asp:TemplateField HeaderText="Filing Desk Approval Required" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left"
                                                                                    SortExpression="DocumentAutoApprove" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbldocautup" runat="server" Text='<%# Eval("DocumentAutoApprove")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Auto Organize Rule" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left"
                                                                                    SortExpression="RuleType" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblrupt" runat="server" Text='<%# Eval("RuleType")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                             
                                                                                <asp:TemplateField HeaderText="Auto Retrieval Time (Mins)" ItemStyle-Width="10%"
                                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblautoret" runat="server" Text='<%# Bind("AutoInterval") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("FolderId") %>'
                                                                                            CommandName="Edit" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete1" CommandArgument='<%# Eval("FolderId") %>'
                                                                                            ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                                        <input id="hdncnfm" type="hidden" name="hdncnfm" runat="Server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Panel ID="pnlMainTypeAdd" runat="server" BackColor="White" BorderColor="#999999"
                                        Width="70%" Height="500px" BorderStyle="Solid" ScrollBars="Vertical" BorderWidth="10px">
                                        <asp:UpdatePanel ID="UpdatePanelMainTypeAdd" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <fieldset>
                                                    <div align="right">
                                                        <asp:ImageButton ID="ibtnCancelCabinetAdd" runat="server" AlternateText="Close" CausesValidation="False"
                                                            ImageUrl="~/Account/images/closeicon.png" OnClick="ibtnCancelCabinetAdd_Click" />
                                                    </div>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <div>
                                                        <label>
                                                            For dynamic document processing, you can automatically process the document and
                                                            give it a title, folder name, document type, party name etc. based on the document file name. However,
                                                            please ensure that the document file name is written using the syntax mentioned
                                                            below. Note that every time a documents is retrieved from the server folder, the
                                                            system will read the file name and dynamically process the file based on the file
                                                            name.
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
                                                        Please note, that the following rules below must be followed for a successful file
                                                        download.
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        The file name should not have any spaces. Instead use an under score between the
                                                        words. For example, ABC_DEF
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        The file name should include the following syntax. Note that not all fields are
                                                        necessary, and you can choose what fields you wish to use.
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        The system will read the name of the file, based on the name that You have given
                                                        to that file.
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        You can have the system automatically select the party name for your document if
                                                        the document file name includes anywhere the words Party=(name of party)
                                                    </label>
                                                    <label>
                                                        You can have the system automatically insert the reference number of your document,
                                                        if the document file name includes anywhere the words Ref=(reference number)
                                                    </label>
                                                    <label>
                                                        You can have the system automatically select the folder for your document, if the
                                                        document file name includes anywhere the words Folder=(folder name)
                                                    </label>
                                                       <label>
                                                You can have the system automatically select the document type for your document, if the
                                                document file name includes anywhere the words type=(document type)
                                            </label>
                                                    <label>
                                                        You can have the system automatically give the document date, if the document file
                                                        names includes anywhere the date in the format (with no slashes) DT=(MMDDYYYY)
                                                    </label>
                                                    <label>
                                                        You can have have the system automatically insert the amount for the document, if
                                                        the document file name includes anywhere the amount in the format AMT=(Amount)
                                                    </label>
                                                    <label>
                                                        You can have the system automatically insert the document title, if the document
                                                        file name includes anywhere the words Title=(title name)
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        For example: Ref=123456_Folder=SalesInvoice_Type=CashInvoice_Party=FakeCompany_Title=InvoiceFromOctober_DT=12222012_AMT=123.45
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        You can select what fields you want to be automatically read and fed into the table.
                                                        None of the fields are compulsory.
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        Furthermore, if the system does not find the folder name, or party name that You
                                                        can have mentioned, it will create a new folder or party.
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        In addition, even the first word of the party name is used, instead of the full
                                                        party name, the system will check that word into the list of the parties, and whichever
                                                        first party has that word as the first word, it will connect to that party.
                                                    </label>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <label>
                                                        Example:If FakeCompany123 is the party name in your list of parties and you only
                                                        put Party=Fake then the system will check for the word Fake in the list, and the
                                                        put the document into that party list.
                                                    </label>
                                                </fieldset>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender9" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="pnlMainTypeAdd" TargetControlID="hdnMaintypeAdd" CancelControlID="ibtnCancelCabinetAdd"
                                        Drag="true">
                                    </cc1:ModalPopupExtender>
                                    <input id="hdnMaintypeAdd" runat="Server" name="hdnMaintypeAdd" type="hidden" style="width: 4px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#999999" Width="600px"
                                        BorderStyle="Solid" BorderWidth="10px">
                                        <fieldset>
                                            <legend>Key to Follow This Rule </legend><legend align="right">
                                                <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Close" CausesValidation="False"
                                                    ImageUrl="~/Account/images/closeicon.png" OnClick="ibtnCancelCabinetAdd_Click">
                                                </asp:ImageButton></legend>
                                            <div>
                                                <label>
                                                    There are two ways of auto organizing your documents.
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <label>
                                                    The first is a fixed rule. A fixed rule is setup so that all documents you are retrieving
                                                    from a specific folder will automatically be given a fixed title, party name or
                                                    folder, depending on the rule you have set.
                                                </label>
                                                <div style="clear: both;">
                                                </div>
                                                <label>
                                                    The second is a dynamic rule. A dynamic rule is setup so that all documents you
                                                    are retrieving from a specific folder will have the file name read by the system
                                                    and will give the document title, party name and folder, and many other variables
                                                    depending on the file name.
                                                </label>
                                            </div>
                                        </fieldset>
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender10" runat="server" BackgroundCssClass="modalBackground"
                                            CancelControlID="ImageButton1" Drag="true" PopupControlID="Panel1" TargetControlID="Hidden101">
                                        </cc1:ModalPopupExtender>
                                        <input id="Hidden101" runat="Server" name="Hidden101" style="width: 4px" type="hidden" />
                                    </asp:Panel>
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
                                                    If this option is selected, the document will be sent to the filing desk for further
                                                    processing. If this option is not selected, the document would be available for
                                                    viewing by all users, depending on the document rights assigned to them.
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
                                                    For fixed document processing, you can automatically process the document and give
                                                    it a title, folder name, party name etc. based on the rule that you have set below.
                                                    Note that every time a documents is retrieved from your server folder, the system
                                                    will give the same file name and put it in the same folder, and set the same party
                                                    name as per the rule that you have set. Please note that it will not read the file
                                                    name for processing the document.
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
         
            <asp:AsyncPostBackTrigger ControlID="imgRefresh2" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
