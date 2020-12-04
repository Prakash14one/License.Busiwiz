<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentFastUpload.aspx.cs" Inherits="Account_DocumentFastUpload"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">



        function RealNumWithDecimal(myfield, e, dec) {

            //myfield=document.getElementById(FindName(myfield)).value
            //alert(myfield);
            var key;
            var keychar;
            if (window.event)
                key = window.event.keyCode;
            else if (e)
                key = e.which;
            else
                return true;
            keychar = String.fromCharCode(key);
            if (key == 13) {
                return false;
            }
            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 27)) {
                return true;
            }
            else if ((("0123456789.").indexOf(keychar) > -1)) {
                return true;
            }
            // decimal point jump
            else if (dec && (keychar == ".")) {

                myfield.form.elements[dec].focus();
                myfield.value = "";

                return false;
            }
            else {
                myfield.value = "";
                return false;
            }
        }

        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }
          

            if (evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {
                alert("You have entered invalid character");
                return false;
            }




        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered invalid character");
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

    <asp:UpdatePanel ID="update1" runat="server">
        
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
                            <td colspan="2">
                                <asp:Panel ID="pnlentry" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 20%">
                                                <label>
                                                    <asp:Label ID="Label3" runat="server" Text="Entry No"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lbleno" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label1" runat="server" Text="Entry Type"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lbletype" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label2" runat="server" Text="Transaction MasterId"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lbltid" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Business Name"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlbusiness" runat="server" Width="206px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Cabinet - Drawer - Folder"></asp:Label>
                                    <asp:Label ID="Label6" runat="server" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDocType"
                                        ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlDocType" runat="server" ValidationGroup="1" Width="600px">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton49" runat="server" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                        OnClick="ImageButton49_Click" Width="20px" AlternateText="Add New" Height="20px"
                                        ToolTip="AddNew" />
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton48" runat="server" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                        OnClick="ImageButton48_Click" AlternateText="Refresh" Height="20px" Width="20px"
                                        ToolTip="Refresh" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label10" runat="server" Text="Document Type"></asp:Label>
                                    <asp:Label ID="Labelxc" runat="server" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RiredFiealidator2" runat="server" ControlToValidate="ddldt"
                                        ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddldt" runat="server" ValidationGroup="1" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddldt_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                        Width="20px" AlternateText="Add New" Height="20px" ToolTip="AddNew" OnClick="ImageButton1_Click" />
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                        AlternateText="Refresh" Height="20px" Width="20px" ToolTip="Refresh" OnClick="ImageButton2_Click" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="Document Title"></asp:Label>
                                    <asp:Label ID="Label8" runat="server" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdoctitle"
                                        ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9.,/_\s]*)"
                                        ControlToValidate="txtdoctitle" ValidationGroup="1"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtdoctitle" onKeydown="return mask(event)" onkeyup="return check(this,/[\\!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9.,/_ \s]+$/,'div1',100)"
                                        runat="server" ValidationGroup="1" Width="598px" MaxLength="100" TabIndex="2"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label ID="Label17" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="div1" class="labelcount">100</span>
                                    <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9.,/_)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label9" runat="server" Text="User Name"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlpartyname" runat="server" ValidationGroup="1" Width="400px"
                                        TabIndex="3">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton50" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                        OnClick="ImageButton50_Click" ToolTip="AddNew" Width="20px" />
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton51" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                        OnClick="ImageButton51_Click" ToolTip="Refresh" Width="20px" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 20%">
                                            <label>
                                                <asp:Label ID="Label20" runat="server" Text="Party Document Ref. Number"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequicmnredFieldValidator2" runat="server" Display="Dynamic"
                                                    ControlToValidate="txtpartdocrefno" ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegulValidator2" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9.,/_\s]*)"
                                                    ControlToValidate="txtpartdocrefno" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtpartdocrefno" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9.,/_ \s]+$/,'Span2',100)"
                                                    MaxLength="100" ValidationGroup="1" Width="180px" TabIndex="5"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label21" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                <span id="Span2" class="labelcount">100</span>
                                                <asp:Label ID="Label22" runat="server" CssClass="labelcount" Text="(A-Z 0-9.,/_)"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label11" runat="server" Text="Document Date"></asp:Label>
                                    <%--    <asp:Label ID="Label23" runat="server" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFielmndidator2" runat="server" ControlToValidate="TxtDocDate"
                                        ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                    
                    --%>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="TxtDocDate" runat="server" Width="70px" TabIndex="4"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="TxtDocDate">
                                    </cc1:CalendarExtender>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label12" runat="server" Text="Document Ref. Number"></asp:Label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9.,/_\s]*)"
                                        ControlToValidate="txtdocrefnmbr" ValidationGroup="1"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtdocrefnmbr" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9.,/_ \s]+$/,'sp11',100)"
                                        MaxLength="100" ValidationGroup="1" Width="180px" TabIndex="5"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label ID="Label13" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="sp11" class="labelcount">100</span>
                                    <asp:Label ID="Label18" runat="server" CssClass="labelcount" Text="(A-Z 0-9.,/_)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label14" runat="server" Text="Net Amount"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtnetamount" runat="server" MaxLength="10" onKeydown="return mak('Span1',10,this)"
                                        onkeypress="return RealNumWithDecimal(this,event,2);" Text="00.00" Width="180px" TabIndex="6"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label ID="Label15" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="Span1" class="labelcount">10</span>
                                    <asp:Label ID="Label19" runat="server" CssClass="labelcount" Text="(0-9 .)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label16" runat="server" Text="Add Document"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="FileUpload1"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" TabIndex="7" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="imgbtnAdd" runat="server" OnClick="imgbtnAdd_Click" Text="Add" ValidationGroup="1"
                                    CssClass="btnSubmit" TabIndex="8" />
                                <asp:Button ID="imgbtnreset" CssClass="btnSubmit" runat="server" OnClick="imgbtnreset_Click"
                                    Text="Reset" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" OnClick="Button1_Click"
                                    Text="Go Back" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Panel ID="Panel2" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="Gridreqinfo" runat="server" AllowPaging="false" DataKeyNames="documenttype"
                                                    CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    AllowSorting="True" AutoGenerateColumns="False" OnRowCommand="Gridreqinfo_RowCommand"
                                                    OnPageIndexChanging="Gridreqinfo_PageIndexChanging1" OnSelectedIndexChanged="Gridreqinfo_SelectedIndexChanged"
                                                    Width="100%" OnRowDataBound="Gridreqinfo_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Cabinet-Drawer-Folder" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddldoctypemas" runat="server" AutoPostBack="false">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lbldoc" runat="server" Text='<%#Eval("DocType") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbldocmasId" runat="server" Text='<%#Eval("documenttype") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Title" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%-- <asp:Label ID="txtdoctitlegrd" runat="server" Text='<%#Eval("DocumentTitle") %>'></asp:Label>--%>
                                                                <asp:TextBox ID="txtdoctitlegrd" runat="server" Text='<%#Eval("DocumentTitle") %>' onKeydown="return mask(event)"
                                                                       onkeyup="return check(this,/[\\!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9.,/_ \s]+$/,'Span2c',100)"
                                                                        MaxLength="100" ValidationGroup="8" ></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="Redcgg" runat="server" ErrorMessage="Invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9.,/_\s]*)"
                                        ControlToValidate="txtdoctitlegrd" ValidationGroup="8"></asp:RegularExpressionValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="File Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfilenamegrd" Visible="false" runat="server" Text='<%#Eval("documentname") %>'></asp:Label>
                                                                <asp:HyperLink ID="FileOpen" ForeColor="#426172" runat="server" Target="_blank" Text='<%# Eval("documentname") %>'
                                                                    NavigateUrl='<%# Eval("documentname") %>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            Visible="true">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddluser" runat="server" AutoPostBack="false">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblpid" runat="server" Text='<%#Eval("PartyId") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Party Doc Ref.No." HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                              
                                                                    <asp:TextBox ID="lblprn" runat="server" Text='<%#Bind("PRN") %>' onKeydown="return mask(event)"
                                                                        onkeyup="return check(this,/[\\!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9.,/_ \s]+$/,'Span2c',100)"
                                                                        MaxLength="100" ValidationGroup="8" Width="110px"></asp:TextBox>
                                                                
                                                                    <asp:RequiredFieldValidator ID="ReqValPArtyDocRefno" runat="server" Display="Dynamic"
                                                                        ControlToValidate="lblprn" ErrorMessage="*" ValidationGroup="8" Visible="false" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegulValidatorPartydoc" runat="server" ErrorMessage="Invalid Character"
                                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9.,/_\s]*)"
                                                                        ControlToValidate="lblprn" ValidationGroup="8"></asp:RegularExpressionValidator>
                                                             
                                                              <%--  <label>
                                                                    <asp:Label ID="Label21" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                    <span id="Span2c" class="labelcount">100</span>
                                                                    <asp:Label ID="Label22" runat="server" CssClass="labelcount" Text="(A-Z 0-9.,/_)"></asp:Label>
                                                                </label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Type" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" >
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddldocty" runat="server" AutoPostBack="True" Width="125px" OnSelectedIndexChanged="ddldocty_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lbldt" runat="server" Text='<%#Bind("Docty") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbldoctid" runat="server" Text='<%#Bind("DoctyId") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Ref No." HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lbldocrefno" runat="server" Text='<%#Eval("docrefno") %>' onKeydown="return mask(event)"
                                                                        onkeyup="return check(this,/[\\!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9.,/_ \s]+$/,'Span2c',100)"
                                                                        MaxLength="100" ValidationGroup="8" Width="110px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="Redcggccc" runat="server" ErrorMessage="Invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9.,/_\s]*)"
                                        ControlToValidate="lbldocrefno" ValidationGroup="8"></asp:RegularExpressionValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            HeaderText="Status" Visible="false"></asp:BoundField>
                                                        <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblwhid" runat="server" Text='<%#Eval("Whid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldocdate" runat="server" Text='<%#Eval("docdate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            Visible="false" DataField="Businessname"></asp:BoundField>
                                                        <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldocamt" runat="server" Text='<%#Eval("docamt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:ButtonField ButtonType="Image" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            HeaderImageUrl="~/Account/images/delete.gif" ImageUrl="~/Account/images/delete.gif"
                                                            HeaderText="Delete" ItemStyle-Width="2%" CommandName="del">
                                                            <ItemStyle Width="50px" />
                                                        </asp:ButtonField>
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
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="Chkautoprcss" runat="server" Text="Filing desk approval not required"
                                    Checked="True" />
                                <br />
                                <label>
                                    <asp:Label ID="lblinfpo" runat="server" Text=" (Please note that if this is selected then the document you upload will be available for viewing by users, depending on their rights. If this is not selected then the document will go to the document processing department.)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="imgbtnUpload" runat="server" CssClass="btnSubmit" AlternateText="Upload"
                                    OnClick="imgbtnUpload_Click" Visible="False" Text="Upload" ValidationGroup="8" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
       <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ImageButton50" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ImageButton51" EventName="Click" />
        </Triggers>
        
    </asp:UpdatePanel>
</asp:Content>
