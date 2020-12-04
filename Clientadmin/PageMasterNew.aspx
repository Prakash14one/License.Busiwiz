<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="PageMasterNew.aspx.cs" Inherits="Page_Master" Title="PageMaster - Add,Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

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
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
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

//        function button_click(objTextBox, objBtnID) {
//            if (window.event.keyCode == 13) {
//                document.getElementById(objBtnID).focus();
//                document.getElementById(objBtnID).click();
//            }
        //        }

        function submitButton(event) {
            if (event.which == 13) {
                $('#BtnGo').trigger('click');
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin-left:1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                 <asp:Label ID="lblpagename" runat="server"  Visible="false"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <fieldset>
                <legend>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </legend>
                <div style="float: right;">
                    <asp:Button ID="addnewpanel" runat="server" Text="Add Product Page" CssClass="btnSubmit" OnClick="addnewpanel_Click" />
                        <asp:Button ID="btndosyncro" runat="server" CssClass="btnSubmit"  OnClick="btndosyncro_Clickpop" Text="Do Synchronise" />
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                    <table width="100%">
                       
                        <tr>
                            <td valign="top" style="width: 40%">
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Select Product."></asp:Label>
                                    <asp:Label ID="Label55" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlProductname"
                                        ErrorMessage="*"  ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td style="width: 70%">
                                <label>
                                    <asp:DropDownList ID="ddlProductname" Width="600px" runat="server" OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>

                         <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label39" runat="server" Text="Select Menu Category"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlcategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FilterCategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                        <tr>
                            <td style="width: 35%" valign="top">
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Main Menu"></asp:Label>
                                    <asp:Label ID="Label25" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlMainMenu"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlMainMenu" runat="server" Width="220px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%" valign="top">
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Sub Menu"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlSubmenu" runat="server" Width="220px" 
                                        colspan="3" >
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%" valign="top">
                                <label>
                                    <asp:Label ID="Label10" runat="server" Text="Page Index"></asp:Label>
                                    <asp:Label ID="Label20" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtpageindex"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtpageindex" runat="server" Width="220px" MaxLength="4" onkeyup="return mak('Span1',4,this)"></asp:TextBox>
                                </label>
                                <label  style="width: 50%">
                                    e.g. This is page index under Menu/submenu
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                        TargetControlID="txtpageindex" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%" valign="top">
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Language"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlanguage" runat="server" Width="220px" AutoPostBack="True" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                        <td>
                        
                        <lable style="width:100%">
                         <asp:Label ID="Label6" runat="server" Text="Page Folder Name"></asp:Label>
                                    <asp:Label ID="Label23" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpagename"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-z/A-Z0-9,()#\s_.]*)"
                                        ControlToValidate="txtFolderName" ValidationGroup="1"></asp:RegularExpressionValidator>
                                         <asp:Label ID="lbl_rootpath" runat="server" Text=""></asp:Label>
                                         </lable>

                                          <label style="width:100%">
                        <asp:CheckBox ID="chkpathselect" runat="server"   AutoPostBack="True" OnCheckedChanged="chkupload_CheckedChanged" Text="Enter in Textbox" TextAlign="Left"></asp:CheckBox>
                        </label> 
                                        
                        </td>
                        <td>
                         <asp:Panel ID="pnl_pathddl" runat="server">
                      <label style="width:120px">
                        <asp:DropDownList ID="ddl_MainFolder" runat="server" Width="120px" AutoPostBack="true"   OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChangedMainFolder">
                        </asp:DropDownList>
                        </label>
                      <label style="width:120px">  
                        <asp:DropDownList ID="ddl_subfolder" runat="server" Width="120px" AutoPostBack="true"   OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChangedsubFolder">
                        </asp:DropDownList>
                        </label>
                        <label style="width:120px">  
                        <asp:DropDownList ID="ddl_SubSubfolder" runat="server" Width="120px" AutoPostBack="true"   OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChangedSubsubFolder">
                        </asp:DropDownList>
                        </label>   
                      </asp:Panel>

                       <label>
                                    <asp:TextBox ID="txtFolderName" runat="server" visible="False" Width="220px" MaxLength="100" onkeyup="return mak('Span4',100,this)"></asp:TextBox>
                                </label>
                        </td>
                        
                        </tr>
                        <tr>
                            <td style="width: 35%" valign="top">
                               
                            </td>
                            <td>
                                
                               
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%" valign="top">
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label21" Text="Max" runat="server" CssClass="labelcount"></asp:Label>
                                    <span id="Span4" cssclass="labelcount">100</span>
                                    <asp:Label ID="Label22" runat="server" Text="(A-Z 0-9 / _ . , () # - )"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%" valign="top">
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="File Name"></asp:Label>
                                    <asp:Label ID="Label18" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtpagename"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s_.]*)"
                                        ControlToValidate="txtpagename" ValidationGroup="1"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtpagename" runat="server" Width="220px" MaxLength="100" onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9._\s]+$/,'Span2',100)"></asp:TextBox>
                                </label>
                                <label>
                                    e.g. PageManagement.aspx
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%" valign="top">
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label27" Text="Max" runat="server" CssClass="labelcount"></asp:Label>
                                    <span id="Span2" cssclass="labelcount">100</span>
                                    <asp:Label ID="Label17" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ .)" ></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%" valign="top">
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="Page Title"></asp:Label>
                                    <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtpagetitle"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtpagetitle" runat="server" Width="220px" MaxLength="100" ></asp:TextBox>
                                    <%--onkeyup="return mak('Span3',100,this)"--%>
                                </label>
                                <label>
                                    eg. Product page management. This will appear as item under submenu
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%" valign="top">
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label28" Text="Max" runat="server" CssClass="labelcount"></asp:Label>
                                    <span id="Span3" cssclass="labelcount">100</span>
                                    <%--<asp:Label ID="Label21" runat="server" Text="(A-Z,0-9,_,.,-)"></asp:Label>--%>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%" valign="top">
                                <label>
                                    <asp:Label ID="Label9" runat="server" Text="Page Description"></asp:Label>
                                    <asp:Label ID="Label53" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtpagedescriptin"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                   
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9/!&,'?\s_.]*)"
                                        ControlToValidate="txtpagedescriptin" ValidationGroup="1"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtpagedescriptin" runat="server" Width="350px" Height="150px" TextMode="MultiLine" MaxLength="2000" ></asp:TextBox>
                                      <%--onkeyup="return mak('div2',2000,this)" onkeypress="return checktextboxmaxlength(this,2000,event)"--%>
                                </label>
                                <label>
                                    <asp:Label ID="Label29" Text="Max" runat="server" CssClass="labelcount"></asp:Label>
                                    <span id="div2" class="labelcount">2000</span>
                                    <asp:Label ID="Label26" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . / ' , ? & ! )" visible="false"></asp:Label>
                                </label>
                               
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="width: 35%" valign="top">
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label30" Text="Max" runat="server" CssClass="labelcount"></asp:Label>
                                    <span id="Span1" cssclass="labelcount">4</span>
                                    <asp:Label ID="Label57" CssClass="labelcount" runat="server" Text="(0-9)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                        <td>
                        <label>
                        Link to Functionality                         
                        </label> 
                        
                        </td>
                        <td>
                        <asp:CheckBox ID="chkuploadFunction" runat="server" AutoPostBack="True" OnCheckedChanged="chkupload_CheckedChanged"></asp:CheckBox>
                        <asp:Panel ID="pnl_funct" runat="server" Visible="false">
                        <asp:DataList ID="datalist2" runat="server" RepeatColumns="3" DataKeyField="ID" RepeatLayout="Table"
                                                      CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                        
                                                        <ItemTemplate>
                                                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                               
                                                                <tr>
                                                              
                                                                <%--<td style="width: 45%">
                                                                   <asp:Label ID="Label26" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                        <asp:Label ID="Label42" Visible="false" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                                                </td>--%>
                                                                    <td style="width: 10%">
                                                                        <asp:CheckBox ID="chkMsg11" runat="server" />
                                                                    </td>
                                                                    <td style="width: 75%">
                                                                        <asp:Label ID="lblvtitle" runat="server" Text='<%#Eval("FunctionalityTitle") %>'></asp:Label>
                                                                        <asp:Label ID="Label51" Visible="false" runat="server" Text='<%#Eval("ID") %>'></asp:Label>

                                                                    </td>
                                                                    <td style="width:8%">
                                                                    
                                                                    Rank  
                                                                    
                                                                    </td>
                                                                    <td>
                                                                    
                                                                    <asp:TextBox ID="txtrenk" runat="server" Width="35px" ></asp:TextBox>
                                                                    
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    </asp:Panel>
                        </td>
                        </tr>
                        <tr>
                            <td style="width: 35%" valign="top">
                                <label>
                                    <asp:Label ID="Label11" runat="server" Text="Activate/Deactivate"></asp:Label>
                                </label>
                            </td>
                            <td valign="top">                              
                                    <asp:CheckBox ID="chkactive" runat="server" Checked="true" />                               
                            </td>
                        </tr>
                         <tr>
                            <td style="width: 35%" valign="top">
                                 <label>
                                    <asp:Label ID="Label35" runat="server" Text="Menu Access"></asp:Label>
                                </label>
                            </td>
                            <td valign="top">
                              
                                    <asp:CheckBox ID="chkmanuaccess" runat="server" Checked="true" />
                               
                            </td>
                        </tr>
                        <tr>
                        <td>                        
                            <table width="100%">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label7N" runat="server" Text="Upload Instruction/Help Files"></asp:Label>
                                        </label>
                                       
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                         <asp:CheckBox ID="chkupload" runat="server" AutoPostBack="True" OnCheckedChanged="chkupload_CheckedChanged">
                                        </asp:CheckBox>
                        </td>
                        </tr>
                        <asp:Panel ID="pnlup" runat="server" Visible="false">
                        <tr>
                        <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td>  
                                            <table width="100%" id="pagetbl">
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label19N" runat="server" Text=" File Tilte"></asp:Label>
                                                            <asp:Label ID="Label20N" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txttitlename" ErrorMessage="*" ValidationGroup="2">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txttitlename"  Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"   ValidationGroup="1">
                                                            </asp:RegularExpressionValidator>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:TextBox ID="txttitlename" onKeydown="return mask(event)" MaxLength="30" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span10',30)" runat="server"></asp:TextBox>
                                                        </label>
                                                        <label>
                                                            Max <span id="Span10">30</span>
                                                            <asp:Label ID="Label54" runat="server" Text="(A-Z,0-9,_)"></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="Upradio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Upradio_SelectedIndexChanged"
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Upload Audio Instruction File</asp:ListItem>
                                                            <asp:ListItem Value="2">Upload Other Files</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="pnlpdfup" runat="server" Visible="false">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Label ID="Label21N" runat="server" Text="Pdf File"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:FileUpload ID="fileuploadadattachment" runat="server" />
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
                                                        <asp:Panel ID="pnladio" runat="server" Visible="false">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Label ID="Label22N" runat="server" Text=" Audio File"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:FileUpload ID="fileuploadaudio" runat="server" />
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
                                                        <asp:Button ID="Button2N" CssClass="btnSubmit" runat="server" Text="Add" OnClick="Button2_Click"
                                                            ValidationGroup="2" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:GridView ID="gridFileAttach" runat="server" CssClass="mGrid" GridLines="Both"
                                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                            Width="100%" OnRowCommand="gridFileAttach_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblpdfurl" runat="server" Text='<%#Bind("PDFURL") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PDF URL" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltitle" runat="server" Text='<%#Bind("Title") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Audio URL" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblaudiourl" runat="server" Text='<%#Bind("AudioURL") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:ButtonField CommandName="Delete1" ItemStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderText="Delete" ImageUrl="~/Account/images/delete.gif" Text="Delete" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="testup" runat="server" Visible="false">
                                        <asp:Button ID="btnup" runat="server" CssClass="btnSubmit" OnClick="btnup_Click"
                                            Text="Upload Files" />
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        </td>
                        </tr>
                          </asp:Panel>
                        <tr>
                            <td valign="top">
                       <label style="width:300px">
                                            <asp:Label ID="Label36" runat="server" Text="Upload screen shot for  language label"></asp:Label>
                                        </label>
                            
                       
                        </td>
                        <td>
                           <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True"   oncheckedchanged="CheckBox1_CheckedChanged" >
                                        </asp:CheckBox>
                                        <table width="100%">
                               
                                  <asp:Panel ID="Panel3" runat="server" Visible="false">
                                <tr>
                                    <td>
                                     
                                            <label>
                                            <asp:FileUpload ID="fileuploadaudio0" runat="server" />
                                            </label>
                                            <label>
                                                <asp:Button ID="Button3" runat="server" Text="Upload" 
                                                onclick="Button3_Click" />
                                            </label>
                                            <label>
                                            <asp:Image ID="imglogo" runat="server" Height="106px" Width="176px" />
                                            </label>
                                            <asp:Label ID="Label37" runat="server" Text="Label" Visible="false"></asp:Label>
                                      
                                    </td>
                                </tr>
                                  </asp:Panel>
                            </table>
                        </td>
                        </tr>
                         <asp:Panel ID="pnl_autorepo" runat="server" Visible="false">
                           <tr>
                            <td style="width: 35%" valign="top">
                                 <label style="width:250px">
                                    <asp:Label ID="Label40" runat="server" Text="Is it an Autogenerated rpeport page"></asp:Label>
                                </label>
                            </td>
                            <td valign="top">
                              
                                    <asp:CheckBox ID="check_autoreport" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="check_autoreport_CheckedChanged" />
                               
                            </td>
                        </tr>
                        </asp:Panel>
                           <asp:Panel ID="pnlpage" runat="server" Visible="false">
                        <tr>
                        <td>
                        <label style="width:450px;">
                        Select the page to which this autogenerated page is linked
                        </label> 
                        </td>
                        <td>
                        <asp:DropDownList ID="ddlpagename" runat="server" Width="300px" >
                                </asp:DropDownList>
                        </td>
                        </tr>
                        </asp:Panel>
                        <tr>
                            <td style="width: 35%" valign="top">
                            </td>
                            <td align="left" valign="top">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btnSubmit"   OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="1" />
                                <asp:Button ID="Button1" runat="server" CssClass="btnSubmit"  OnClick="Button1_Click" Text="Cancel" />
                            </td>
                        </tr>

                      
                    </table>
                </asp:Panel>
            </fieldset>
            <fieldset>
                <legend>
                    <asp:Label ID="Label12" runat="server" Text="List of Product Pages"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click1" />
                            <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                style="width: 51px;" type="button" value="Print" visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 35%">
                            <label>
                                <asp:Label ID="Label13" runat="server" Text="Filter by Product"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="FilterProduct" Width="600px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FilterProduct_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                    <td>
                    <label style="width:90px;">
                                <asp:Label ID="Label38" runat="server" Text="Menu Category"></asp:Label>
                                </label>                                 
                    </td>
                    <td>
                    <label>
                                <asp:DropDownList ID="DDLCategoryS" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FilterCategorysearch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                </label> 
                    </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label14" runat="server" Text="Main Menu"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="FilterMenu" runat="server" Width="220px" AutoPostBack="true"
                                    CausesValidation="True" OnSelectedIndexChanged="FilterMenu_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label15" runat="server" Text="Sub Menu"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="FilterSubMenu" runat="server" Width="220px" CausesValidation="True">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label16" runat="server" Text="Active"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlAct" runat="server" Width="220px" CausesValidation="True">
                                    <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                       <tr>
                        <td><label>
                            <asp:Label ID="Label13Fun" runat="server" Text="Functionality Title"></asp:Label>
                            </label> 
                        </td>
                        <td>
                        <label>
                            <asp:DropDownList ID="ddlfuncti" runat="server" AutoPostBack="True"  Width="220px"
                                onselectedindexchanged="ddlfuncti_SelectedIndexChanged">
                            </asp:DropDownList>
                            </label> 
                        </td>
                        
                    </tr>

                    <tr>
                    <td>
                    <label>
                                <asp:Label ID="Label16N" runat="server" Text="Search"></asp:Label>
                            </label>
                    </td>
                    <td>
                    <asp:TextBox ID="TextBox5" runat="server"   placeholder="Search"  Font-Bold="true"  
                            Width="250px" Height="20px" onKeyDown="submitButton(event)"  ></asp:TextBox>
                    </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="BtnGo" CssClass="btnSubmit" runat="server" Text="Go" OnClick="BtnGo_Click"  />
                        </td>
                    </tr>
                    <asp:Panel runat="server" ID="hidepnl" Visible="false">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlSection" runat="server" AutoPostBack="True" Visible="false"
                                        OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" Width="400px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlgrid" runat="server" ScrollBars="Vertical">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <div id="mydiv" class="closed">
                                                <table width="100%">
                                                    <tr align="center">
                                                        <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                            <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                            <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Product Pages"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td>
                                                     <asp:Label ID="Label31" runat="server" Font-Italic="true" Text="Filter by Product :"></asp:Label>
                                                     <asp:Label ID="lblfilterbyproduct" runat="server" Font-Italic="true" ></asp:Label>
                                                     &nbsp;

                                                     <asp:Label ID="Label32" runat="server" Font-Italic="true" Text="Main Menu :"></asp:Label>
                                                     <asp:Label ID="lblmainmenu" runat="server" Font-Italic="true" ></asp:Label>

                                                      &nbsp;

                                                     <asp:Label ID="Label33" runat="server" Font-Italic="true" Text="Sub Menu :"></asp:Label>
                                                     <asp:Label ID="lblsubmenu" runat="server" Font-Italic="true" ></asp:Label>

                                                      &nbsp;

                                                     <asp:Label ID="Label34" runat="server" Font-Italic="true" Text="Status :"></asp:Label>
                                                     <asp:Label ID="lblstatus" runat="server" Font-Italic="true" ></asp:Label>

                                                    </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <asp:GridView ID="GridView1" Width="100%" runat="server" DataKeyNames="PageId" OnRowCommand="GridView1_RowCommand"
                                                    AutoGenerateColumns="False" EmptyDataText="There is no data." OnSorting="GridView1_Sorting"
                                                    AllowSorting="True" OnRowDeleting="GridView1_RowDeleting" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                    AlternatingRowStyle-CssClass="alt">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProductName" HeaderStyle-HorizontalAlign="Left" HeaderText="ProductName"
                                                            SortExpression="ProductName" >
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        
                                                        <asp:BoundField DataField="MainMenuName" HeaderStyle-HorizontalAlign="Left" HeaderText="Main Menu"
                                                            SortExpression="MainMenuName">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SubMenuName" HeaderText="Sub Menu" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="SubMenuName">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Name" HeaderText="Language Name" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="Name">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                       
                                                        <asp:BoundField DataField="PageTitle" HeaderText="Page_Title" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="PageTitle">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PageName" HeaderText="Page_Name" SortExpression="PageName"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FolderName" HeaderText="Folder_Name" SortExpression="FolderName"
                                                            HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Active" SortExpression="Active" HeaderText="Active" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <%--  <asp:BoundField DataField="PageTitle" HeaderText="Page Title" SortExpression="PageTitle" />--%>
                                                        <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderText="Edit" ImageUrl="~/Account/images/edit.gif"
                                                            Text="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:ButtonField>
                                                        <%--<asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />--%>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinkbb" ToolTip="Delete" runat="server" CommandName="Delete"
                                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                    CommandArgument='<%# Eval("PageId") %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>


                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                     <tr>
                            <td>
                              <asp:Panel ID="Paneldoc" runat="server" Width="55%" CssClass="modalPopup">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="Label142" runat="server" Text=""></asp:Label>
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 95%;">
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <label>
                                                        <asp:Label ID="Label143" runat="server" Text="Was this the last record you are going to add right now to this table?"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdsync" runat="server">
                                                                    <asp:ListItem Value="1" Text="Yes, this is the last record in the series of records I am inserting/editing to this table right now"></asp:ListItem>
                                                                    <asp:ListItem Value="0" Text="No, I am still going to add/edit records to this table right now"
                                                                        Selected="True"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="btnok" runat="server" CssClass="btnSubmit" Text="OK" OnClick="btndosyncro_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Paneldoc" TargetControlID="btnreh" CancelControlID="ImageButton10">
                                </cc1:ModalPopupExtender>
                            </td>
                            </tr>
                </table>
            </fieldset>
            <input id="PageId" name="PageId" runat="server" type="hidden" style="width: 1px" />
            <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
            <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
        </ContentTemplate>
           <Triggers>
            <asp:PostBackTrigger ControlID="Button2N" />
            <asp:PostBackTrigger ControlID="Button3" />
        </Triggers>

    </asp:UpdatePanel>
     <div style="position: fixed;bottom: 0; right:20px;">
              <asp:Label ID="lblVersion" runat="server" Text="V5" ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>
</asp:Content>
  

