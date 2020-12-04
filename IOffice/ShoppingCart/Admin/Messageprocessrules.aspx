<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MessageProcessRules.aspx.cs" Inherits="ShoppingCart_Admin_MessageProcessRules"
    Title="Untitled Page" %>

<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
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
            
           
            if( evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }
             
             
               
            
        }  
        function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered an invalid character");
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
    </script>

    <%--<table width="100%">
                <tr>
                    <td>
                        <pnlhelp:pnlhelp ID="pnlHlp" runat="server" />
                    </td>
                </tr>
             
            </table>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllege" Text="" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadddd" runat="server" Text="Add New Message Process Rule" Width="180px"
                            CssClass="btnSubmit" OnClick="btnadddd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel runat="server" ID="pnladdd" Width="100%" Visible="false">
                        <table style="width: 100%">
                            <tr>
                                <td width="25%">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td width="75%">
                                    <label>
                                        <asp:DropDownList ID="ddlstore" runat="server" Width="250px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlstore_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="In Out Going Email ID"></asp:Label>
                                        <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="ffgf" runat="server" ControlToValidate="ddlinoutemailmasterid"
                                            ErrorMessage="*" ValidationGroup="7" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td width="75%">
                                    <label>
                                        <asp:DropDownList ID="ddlinoutemailmasterid" runat="server" Width="250px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlinoutemailmasterid_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="When to Execute rule " Font-Size="Large"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                        </table>
                       
                        <table width="100%">
                            <tr>
                                <td width="25%">
                                    <label>
                                        <asp:CheckBox ID="chkexecute" runat="server" Text="Execute On Send" />
                                    </label>
                                </td>
                                <td width="25%">
                                    <label>
                                        <asp:CheckBox runat="server" ID="chkexreci" Text="Execute On Receive" />
                                    </label>
                                </td>
                                <td width="22%">
                                    <label>
                                        <asp:CheckBox runat="server" ID="chkifsp" Text="If Spam For Email ID" />
                                    </label>
                                </td>
                                <td width="28%">
                                    <label>
                                        <asp:CheckBox runat="server" ID="ifbilmasterspam" Text="If Belong To Master Spam List" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Subject Line Contains Word"></asp:Label>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            ValidationExpression="^([a-zA-Z0-9_ \s]*)" ControlToValidate="txtsubline" ValidationGroup="7"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td width="25%" valign="top">
                                    <label>
                                        <asp:TextBox ID="txtsubline" runat="server" Width="200px" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'Span2',30)"
                                            MaxLength="30"></asp:TextBox>
                                    </label>
                                    <br />
                                </td>
                                <td width="22%" valign="top">
                                    <label>
                                        <asp:Label runat="server" ID="sadasd" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span2" cssclass="labelcount">30</span>
                                        <asp:Label ID="Label21" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                                <td width="28%" valign="top">
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="From Party"></asp:Label>
                                        <asp:Label ID="Label8" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="ffgf0" runat="server" ControlToValidate="ddlparty"
                                            ErrorMessage="*" InitialValue="0" ValidationGroup="7"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlparty" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text=" Message Detail Contain Word"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                            ValidationExpression="^([a-zA-Z0-9_ \s]*)" ControlToValidate="txtmessagesdeword"
                                            ValidationGroup="7"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td width="25%" valign="top">
                                    <label>
                                        <asp:TextBox ID="txtmessagesdeword" runat="server" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'Span1',30)"
                                            Width="200px" MaxLength="30"></asp:TextBox>
                                    </label>
                                </td>
                                <td width="22%" valign="top">
                                    <label>
                                        <asp:Label runat="server" ID="Label10" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" cssclass="labelcount">30</span>
                                        <asp:Label ID="Label9" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                                <td width="28%">
                                </td>
                            </tr>
                        </table>
                       
                        <table width="100%">
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="What to do when condition met" Font-Size="Large"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                        </table>
                        
                        <table width="100%">
                            <tr>
                                <td width="25%">
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Move To Folder ID"></asp:Label>
                                        <asp:Label ID="Labsdfsdfsdfsdel20" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredsdfsfsfsdfdsfFieldValidator1" runat="server"
                                            ControlToValidate="ddlmovefolder" ErrorMessage="*" InitialValue="0" ValidationGroup="7"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td width="25%">
                                    <label>
                                        <asp:DropDownList ID="ddlmovefolder" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td width="25%">
                                </td>
                                <td width="25%">
                                    <asp:CheckBox runat="server" ID="chkdelete" Text="Delete" />
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <br />
                                </td>
                                <td width="25%">
                                    <asp:CheckBox runat="server" ID="chktempdelete" Text="Permanently Delete" />
                                </td>
                                <td width="25%">
                                </td>
                                <td width="25%">
                                    <asp:CheckBox runat="server" ID="chkmovespam" Text="Move To Spam" />
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text="Forward To Email ID"></asp:Label>
                                        <asp:Label ID="Labsdfel20" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldVadsfdslidator1" runat="server" ControlToValidate="ddlforwordemailiD"
                                            ErrorMessage="*" InitialValue="0" ValidationGroup="7"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td width="25%">
                                    <label>
                                        <asp:DropDownList ID="ddlforwordemailiD" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td width="25%">
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Rule Priority Number"></asp:Label>
                                    </label>
                                </td>
                                <td width="25%">
                                    <label>
                                        <asp:TextBox ID="txtrulepriority" runat="server" onKeyup="return mak('Span3',10,this)"
                                            Width="200px" MaxLength="10"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            TargetControlID="txtrulepriority" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:Label runat="server" ID="Label18" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span3" cssclass="labelcount">10</span>
                                        <asp:Label ID="Label11" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Button ID="btninsert" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="btninsert_Click"
                                        ValidationGroup="7" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btnSubmit"
                                        OnClick="btnCancel_Click" Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                        
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllist" runat="server" Text="List of Message Process Rules"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td>
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
                                    <asp:Label ID="Label17" runat="server" Text="Filter by Business"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlfilterstore" runat="server" Width="203px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlfilterstore_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblCompany" runat="server" Font-Bold="true" Font-Size="20px" Font-Italic="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="Label19" runat="server" Font-Bold="true" Font-Size="20px" Text="Business:"
                                                                    Font-Italic="True"></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" Font-Bold="true" Font-Size="20px" Font-Italic="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="Label13" runat="server" Font-Bold="true" Font-Italic="True" Font-Size="18px"
                                                                    Text="List of Message Process Rules " ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:GridView AutoGenerateColumns="False" ID="GridEmail" runat="server" Width="100%"
                                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    EmptyDataText="No Record Found." DataKeyNames="ID" AllowSorting="True" OnRowCommand="GridEmail_RowCommand"
                                                    OnRowEditing="GridEmail_RowEditing" OnRowDeleting="GridEmail_RowDeleting" OnSorting="GridEmail_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Email ID" HeaderStyle-HorizontalAlign="Left" SortExpression="EmailId">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbleid" runat="server" Text='<%# Bind("EmailId")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Folder Name" HeaderStyle-HorizontalAlign="Left" SortExpression="Foldername">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfolname" runat="server" Text='<%# Bind("Foldername")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From Party" HeaderStyle-HorizontalAlign="Left" SortExpression="Compname">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ddlfparty" runat="server" Text='<%# Bind("Compname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Move To Spam" HeaderStyle-HorizontalAlign="Left" SortExpression="MoveToSpam">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkms" runat="server" Checked='<%# Bind("MoveToSpam") %>' Enabled="false" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Permanent Delete" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="PermanentlyDelete">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkperdel" runat="server" Checked='<%# Bind("PermanentlyDelete") %>'
                                                                    Enabled="false" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <%--  <asp:ButtonField CommandName="Edit" ButtonType="Image" HeaderStyle-HorizontalAlign="Left"
                                                                                HeaderText="Edit" Text="Edit" ImageUrl="~/Account/images/edit.gif">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:ButtonField>--%>
                                                        <%--<asp:ButtonField CommandName="Delete1" ButtonType="Image" HeaderStyle-HorizontalAlign="Left"
                                                                                HeaderText="Delete" Text="Delete" ImageUrl="~/Account/images/delete.gif"></asp:ButtonField>--%>
                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-Width="2%" HeaderImageUrl="~/Account/images/edit.gif"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton49" ImageUrl="~/Account/images/edit.gif" runat="server"
                                                                    ToolTip="Edit" CommandArgument='<%# Eval("ID") %>' CommandName="Edit" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                    ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                                    OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                                <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                                <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                                <input id="hdnMaintypeAdd" runat="Server" name="hdnMaintypeAdd" type="hidden" style="width: 4px" />
                                                <input id="Hidden1" runat="Server" name="Hidden1" type="hidden" style="width: 4px" />
                                            </td>
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
