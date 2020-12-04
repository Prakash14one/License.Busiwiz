<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
 AutoEventWireup="true" CodeFile="ExamName.aspx.cs" Inherits="Default2" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<script language="javascript" type="text/javascript">
    function CallPrint(strid) {
        var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
        var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
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

        if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {

          //  alert("You have entered an invalid character");
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
                
    </script>

 <script language="javascript" type="text/javascript">

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
        function mask(evt) {

            if (evt.keyCode == 13) {


            }
            if (evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


             //   alert("You have entered an invalid character");
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');
             //   alert("You have entered an invalid character");
            }

            counter = document.getElementById(id);

            if (txt1.value.length <= max_len) {
                remaining_characters = max_len - txt1.value.length;
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

<asp:UpdatePanel ID="pnnn1" runat="server">
<ContentTemplate>
<div class="products_box">
<div style="padding-left: 1%"></div>
<asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
</div>
<div style="clear: both"></div>

 <fieldset>
 <legend style="height: 24px">
        <asp:Label ID="lbllegend" runat="server"></asp:Label></legend>
         <div style="clear: both;"></div>
         <div style="float: right">
                        <asp:Button ID="btnAddBusCat" runat="server" Text="Add new Exam" Width="180px"
                            OnClick="btnAddBusCat_Click" CssClass="btnSubmit" />
                    </div>
                    <div style="clear: both"></div>

                    <asp:Panel runat="server" ID="pnladdd" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%">
                                    <label>School Name</label></td>
                                <td valign="bottom">
                                   <asp:DropDownList ID="ddlSchoolName" runat="server" Width="143px"></asp:DropDownList></td>
                            </tr>
                            <tr>
                              <%-- <td valign="top" style="width: 25%; margin-left: 80px;">
                                   <label>                                  
                                    Exam Name
                                                                 <asp:Label ID="asdsadsad" runat="server" CssClass="labelstar" Text="*"></asp:Label>                                    
                                    <asp:RequiredFieldValidator ID="redsd" runat="server" 
                                        ControlToValidate="txtExamName" ErrorMessage="*" SetFocusOnError="true" 
                                        ValidationGroup="save"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REG1" runat="server" 
                                        ControlToValidate="txtExamName" ErrorMessage="Invalid Character" 
                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)" 
                                        ValidationGroup="save" Width="100%"></asp:RegularExpressionValidator>                                       
                                    </label> 
                                </td>--%>
                                <td valign="top" style="width: 25%">
                                    <label>
                                          Exam Name                           
                     <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="redsd" runat="server" ControlToValidate="txtExamName"
                                            SetFocusOnError="true" ValidationGroup="save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtExamName"
                                            ValidationGroup="save"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                    <asp:TextBox ID="txtExamName" runat="server" MaxLength="15" 
                                        onKeydown="return mask(event)" 
                                        onkeyup="return check(this,/[\\/!._@#$%^'&amp;*()&gt;+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span2',15)" 
                                        TabIndex="5" Width="143px"></asp:TextBox>
                                    <%-- <cc1:FilteredTextBoxExtender ID="txtExamName_FilteredTextBoxExtender1" runat="server"
                                                Enabled="True" TargetControlID="txtExamName" ValidChars="0147852369">
                                            </cc1:FilteredTextBoxExtender>--%>
                                    </label>
                                    <label>
                                    <asp:Label ID="Label5" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                    <span ID="Span2" cssclass="labelcount">15</span>
                                    <asp:Label ID="Label6" runat="server" CssClass="labelcount" Text="(A-Z 0-9)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <%--<tr>
                                <td style="width: 25%">
                                    <label>
                                        Status
                                    </label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="cbActive" runat="server" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="width: 25%">
                                    <label>Status</label></td>
                                <td valign="bottom">
                                    <asp:DropDownList ID="ddlStatusEN" runat="server" Width="143px" 
                                        AutoPostBack="True">                                        
                                        <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>                                        
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                </td>
                                <td>
                                    <asp:Button ID="btnsave" runat="server" CssClass="btnSubmit" 
                                        OnClick="btnsave_Click" Text="Submit" ValidationGroup="save" />
                                    <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" 
                                        OnClick="Button3_Click" Text="Update" ValidationGroup="save" Visible="false" />
                                    <asp:Button ID="btncancel" runat="server" CssClass="btnSubmit" 
                                        OnClick="btncancel_Click" Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>       
</fieldset>

 <fieldset>
                    <legend>
                        <asp:Label ID="Label4" runat="server" Text="Exam Name"></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            OnClick="Button1_Click1" />
                        <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" />
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                 <label>
                                    School Name
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlSchoolName_Search" runat="server" Width="180px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlSchoolName_Search_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label> 
                                <label>
                                    Status
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlStatusENActive" runat="server" Width="180px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlStatusENActive_SelectedIndexChanged">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">

                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="850Px">
                                                        <tr align="center">
                                                            <td align="center" style="text-align:center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Exams"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="text-align: left; font-size: 14px;">
                                                                 <asp:Label ID="Label3" runat="server" Font-Italic="true" Text="SchoolName : "></asp:Label>
                                                                 <asp:Label ID="lblSchoolName" runat="server" Font-Italic="true"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="Label1" runat="server" Font-Italic="true" Text="Status : "></asp:Label>
                                                                <asp:Label ID="lblstat" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">

                                            <asp:GridView ID="gvExamName" runat="server" CellPadding="4" EnableModelValidation="True" 
                                        ForeColor="#333333" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        AllowPaging="True" AllowSorting="True" DataKeyNames="Id" Width="100%" EmptyDataText="No Record Found"
                                        OnPageIndexChanging="gvExamName_PageIndexChanging"
                                        OnRowCommand="gvExamName_RowCommand" OnRowDeleting="gvExamName_RowDeleting" 
                                        OnRowEditing="gvExamName_RowEditing" AutoGenerateColumns="False">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="School Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamSchoolNm" runat="server" Text='<%# Bind("School")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Exam Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamNm" runat="server" Text='<%# Bind("ExamName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("StatusMode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/UserControl/images/edit.gif"
                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton49" ImageUrl="~/Account/UserControl/images/edit.gif"
                                                        runat="server" ToolTip="Edit" CommandArgument='<%# Eval("Id") %>' CommandName="Edit" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                        ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/UserControl/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" />
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
                    </table>
                </fieldset>

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>




