<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="MainMenuMaster.aspx.cs" Inherits="MainMenuMaster" Title="Product Main Menu-Add,Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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


        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59) {


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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          
            <div class="products_box">
                <div style="margin-left:1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="addnewpanel" runat="server" Text="Add Main Menu" CssClass="btnSubmit" OnClick="addnewpanel_Click" />
                         <asp:Button ID="btndosyncro" runat="server" CssClass="btnSubmit"  OnClick="btndosyncro_Clickpop" Text="Do Synchronise" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Select Product"></asp:Label>
                                        <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlWebsiteSection"
                                            ErrorMessage="*" ValidationGroup="1" Display="Dynamic" ></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlWebsiteSection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWebsiteSection_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Select Master Page"></asp:Label>
                                        <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlMaster"
                                            ErrorMessage="*" ValidationGroup="1" Display="Dynamic" ></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlMaster" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMaster_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Select Language"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlanguage" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label26" runat="server" Text="Select Category"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlcategory" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>

                            <asp:Panel runat="server" ID="pnlhide" Visible="false">
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label4" runat="server" Text="Main Menu Name"></asp:Label>
                                            <asp:Label ID="Label12" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMainMenuTitle"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_.:,\s]*)" ControlToValidate="txtMainMenuTitle"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td style="width: 70%">
                                        <label>
                                            <asp:TextBox ID="txtMainMenuTitle" runat="server" Width="170px" MaxLength="50" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\a-zA-Z0-9_.:,\s]+$/,'div1',50)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label21" Text="Max" runat="server" CssClass="labelcount"></asp:Label>
                                            <span id="div1" cssclass="labelcount">50</span>
                                            <asp:Label ID="Label25" runat="server" Text="(A-Z 0-9 _ , : .)" CssClass="labelcount"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <label>
                                            <asp:Label ID="Label5" runat="server" Text="BackGround color"></asp:Label>
                                            <asp:Label ID="Label13" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator116" runat="server" ControlToValidate="txtBackgroundColor"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtBackgroundColor"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td style="width: 70%">
                                        <label>
                                            <asp:TextBox ID="txtBackgroundColor" runat="server" MaxLength="30" Width="170px"
                                                Text="AAAAAA" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>._+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span2',30)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label20" Text="Max" runat="server" CssClass="labelcount"></asp:Label>
                                            <span id="Span2" cssclass="labelcount">30</span>
                                            <asp:Label ID="Label18" runat="server" CssClass="labelcount" Text="(A-Z 0-9)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="Main Menu Title"></asp:Label>
                                        <asp:Label ID="Label14" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMainMenuName"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                            SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_.:,\s]*)" ControlToValidate="txtMainMenuName"
                                            ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        
                                            <asp:TextBox ID="txtMainMenuName" runat="server" Width="170px" MaxLength="50" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@#$%^'&*()>+={}[]|\/]/g,/^[\a-zA-Z0-9_.:,\s]+$/,'Span1',50)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label19" Text="Max" runat="server" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" class="labelcount">50</span>
                                        <asp:Label ID="Label17" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ . : ,)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Menu Index"></asp:Label>
                                        <asp:Label ID="Label15" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMenuIndex"
                                            ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtMenuIndex" runat="server" Width="170px" MaxLength="4" onkeyup="return mak('Span4',10,this)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                            TargetControlID="txtMenuIndex" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                    <label>
                                        <asp:Label ID="dfdsfd" Text="Max" runat="server" CssClass="labelcount"></asp:Label>
                                        <span id="Span4" cssclass="labelcount">4</span>
                                        <asp:Label ID="Label60" runat="server" Text="(0-9)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Active"></asp:Label>
                                        <%--<asp:Label ID="Label16" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMainMenuTitle"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" CssClass="btnSubmit"
                                        ValidationGroup="1" />
                                    <asp:Button ID="Buttonupdate" runat="server" Text="Update" ValidationGroup="1" Visible="false"
                                        CssClass="btnSubmit" OnClick="Buttonupdate_Click" />
                                    <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" CssClass="btnSubmit" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label10" runat="server" Text="List Of Main Menus"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                 Visible="false"   OnClick="Button1_Click1" />
                                <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    style="width: 51px;" type="button" value="Print" visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label style="width:60px;">
                                    <asp:Label ID="Label9" runat="server" Text="Product"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="FilterProductname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FilterProduct_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label style="width:90px;">
                                 <asp:Label ID="Label27" runat="server" Text="Master Page "></asp:Label>
                                </label> 
                                <label>
                                <asp:DropDownList ID="DDLmasterpageL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FilterMaster_SelectedIndexChanged">
                                        </asp:DropDownList>
                                </label> 
                                <label style="width:90px;">
                                <asp:Label ID="Label28" runat="server" Text="Category"></asp:Label>
                                </label> 
                                <label>
                                
                                        <asp:DropDownList ID="DDLCategoryS" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FilterCategory_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                </label> 
                              
                            </td>
                        </tr>
                        <tr>
                            <td>
                              <label style="width:60px;">
                                    <asp:Label ID="Label16" runat="server" Text="Status"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlactivestatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlactivestatus_SelectedIndexChanged">
                                        <asp:ListItem Value="2">All</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="850Px">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                      
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Main Menus"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                        <td>
                                                         <asp:Label ID="Label22" runat="server" Font-Italic="true" Text="Product Name :"></asp:Label>
                                                         <asp:Label ID="lblproductname" runat="server" Font-Italic="true" ></asp:Label>
                                                         &nbsp;

                                                         <asp:Label ID="Label23" runat="server" Font-Italic="true" Text="Status :"></asp:Label>
                                                         <asp:Label ID="lblstatus" runat="server" Font-Italic="true" ></asp:Label>

                                                        </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" OnRowDeleting="GridView1_RowDeleting"
                                                    DataKeyNames="MainMenuId" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                                                    OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand" EmptyDataText="No Record Found."
                                                    AllowSorting="True" OnSorting="GridView1_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Main Menu Title" SortExpression="MainMenuName" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="17%" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmainmenuName" runat="server" Text='<%#Bind("MainMenuName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                                <asp:TextBox ID="txtMainMenu" runat="server" MaxLength="50" Text='<%# Bind("MainMenuName")%>'> </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatormain" runat="server" ControlToValidate="txtMainMenu"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="REGxvxcv1" runat="server" ErrorMessage="invalid Character"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                                                    ControlToValidate="txtMainMenu" ValidationGroup="2"></asp:RegularExpressionValidator>
                                                            </EditItemTemplate>--%>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="17%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Background Color" SortExpression="BackColour" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="15%" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBackColor" runat="server" Text='<%#Bind("BackColour") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                                <asp:TextBox ID="txtBackColor" MaxLength="30" runat="server" Text='<%# Bind("BackColour")%>'> </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator115337722" runat="server" ControlToValidate="txtBackColor"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="invalid Character"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                                    ControlToValidate="txtBackColor" ValidationGroup="2"></asp:RegularExpressionValidator>
                                                            </EditItemTemplate>--%>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="15%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Main Menu Title" SortExpression="MainMenuTitle" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="17%" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMainMenuTitle" runat="server" Text='<%#Bind("MainMenuTitle") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                                <asp:TextBox ID="txtMainMenuTitle" MaxLength="50" runat="server" Text='<%# Bind("MainMenuTitle")%>'> </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator115335543433" runat="server"
                                                                    ControlToValidate="txtMainMenuTitle" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpresfdsfdssionValidator1" runat="server"
                                                                    ErrorMessage="invalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                                                    ControlToValidate="txtMainMenuTitle" ValidationGroup="2"></asp:RegularExpressionValidator>
                                                            </EditItemTemplate>--%>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="17%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Master Page" SortExpression="MasterPageName" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMasterPage" runat="server" Text='<%#Bind("MasterPageName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                                <asp:DropDownList ID="ddlMasterPageName" runat="server" Width="176px">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblMasterPageId" runat="server" Text='<%#Bind("MasterPage_Id") %>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatordsfds" runat="server" ControlToValidate="ddlMasterPageName"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>--%>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="15%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Language" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLanguage" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                                <asp:DropDownList ID="ddlLanguageedit" runat="server" Width="80px">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblLanguageId" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorsda" runat="server" ControlToValidate="ddlLanguageedit"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>--%>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="12%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Menu Index" SortExpression="MainMenuIndex" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMenuIn" Width="30px" runat="server" Text='<%#Bind("MainMenuIndex") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                                <asp:TextBox ID="lblMenuIndex" Width="30px" MaxLength="10" runat="server" Text='<%# Bind("MainMenuIndex")%>'> </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator101" runat="server" ControlToValidate="lblMenuIndex"
                                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBfdsoxExtender4" runat="server" Enabled="True"
                                                                    TargetControlID="lblMenuIndex" ValidChars="0123456789">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </EditItemTemplate>--%>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Active" SortExpression="Active" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkAct" runat="server" Enabled="false" Checked='<%#Bind("Active") %>' />
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%#Bind("Active") %>' />
                                                            </EditItemTemplate>--%>
                                                            <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Account/images/edit.gif"
                                                                    CommandName="Edit" CommandArgument='<%# Eval("MainMenuId") %>' ToolTip="Edit" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                                    OnClientClick="return confirm('Do you wish to delete this record?');" CommandArgument='<%# Eval("MainMenuId") %>'
                                                                    ToolTip="Delete"></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </td>
                                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
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
                                <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground" PopupControlID="Paneldoc" TargetControlID="btnreh" CancelControlID="ImageButton10">
                                </cc1:ModalPopupExtender>
                        </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
