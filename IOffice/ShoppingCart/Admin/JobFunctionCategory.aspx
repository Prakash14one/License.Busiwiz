<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="JobFunctionCategory.aspx.cs" Inherits="ShoppingCart_Admin_Default2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/IOffice/ShoppingCart/Admin/UserControl/UControlWizardpanel.ascx" TagName="pnl"
    TagPrefix="pnl" %>
<%@ Register Src="~/IOffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
        }

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

       function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        } 
           function checktextboxmaxlength(txt, maxLen,evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
          function mask(evt)
        { 
         
           if(evt.keyCode==13 )
            { 
         
                  
             }
            
           
            if(evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219||evt.keyCode==59||evt.keyCode==186)
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }     
            
        }   
         function check(txt1, regex, reg,id,max_len)
            {
             if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
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

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="lblmsg" runat="server" Visible="False" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllegend" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnnewcat" runat="server" OnClick="btnnewcat_Click" Text="Add New Category"
                            CssClass="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel runat="server" ID="pnladdd" Width="100%" Visible="false">
                        <table width="100%">
                            <tr>
                                <td colspan="2" align="right" width="50%">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 30%">
                                                <label>
                                                    Business Name
                                                    <%-- <asp:RequiredFieldValidator ID="rdg3" runat="server" ControlToValidate="ddlbusiness"
                                                            ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlbusiness" runat="server" Width="255px">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%">
                                                <label>
                                                    Job-Function Category Name
                                                    <asp:Label ID="labdsdsd" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtCategoryName"
                                                        SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtCategoryName"
                                                        ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="txtCategoryName" runat="server" Width="250px" MaxLength="60" onKeydown="return mask(event)"
                                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div1',60)"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:Label runat="server" ID="Label5" Text="Max " CssClass="labelcount"></asp:Label>
                                                    <span id="div1">60</span>
                                                    <asp:Label ID="Label8" runat="server" Text="(A-Z 0-9 . , ? _)" CssClass="labelcount"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%">
                                                <label>
                                                    Status
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStatus"
                                                        ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                                </label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlStatus" runat="server" Width="204px">
                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                    <asp:ListItem Value="0">InActive</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                            </td>
                                            <td align="left">
                                                <label>
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="70px" OnClick="btnSubmit_Click"
                                                        ValidationGroup="1" CssClass="btnSubmit" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70px" OnClick="btnCancel_Click"
                                                        CssClass="btnSubmit" />
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="Label1" Text="List of Job Function Categories"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td align="right">
                            </td>
                            <td align="right">
                                <asp:Button ID="btnPrintVersion" runat="server" OnClick="btnPrintVersion_Click" Text="Printable Version"
                                    CssClass="btnSubmit" />
                                <input id="btnPrint" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    class="btnSubmit" type="button" value="Print" visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    Business Name
                                    <asp:DropDownList ID="ddlBusinessSearch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBusinessSearch_SelectedIndexChanged"
                                        Width="204px">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    Status
                                    <asp:DropDownList ID="ddlstatus_search" runat="server" Width="150px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlstatus_search_SelectedIndexChanged">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="lbbdffddf" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                                <asp:Label ID="lblBus" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label11" runat="server" Text="List of Job Function Categories" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                                    OnRowEditing="GridView1_RowEditing" DataKeyNames="Id" OnRowDeleting="GridView1_RowDeleting"
                                                    Width="100%" EmptyDataText="No Record Found." OnSorting="GridView1_Sorting" AllowPaging="True"
                                                    AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging" lternatingRowStyle-CssClass="alt"
                                                    PagerStyle-CssClass="pgr" CssClass="mGrid">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business Name" HeaderStyle-Width="40%" SortExpression="Name"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBusiness" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category Name" HeaderStyle-Width="40%" SortExpression="CategoryName"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblactive" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderImageUrl="~/Account/images/edit.gif">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="edit" CommandName="Edit" ToolTip="Edit" ImageUrl="~/Account/images/edit.gif"
                                                                    runat="server" CommandArgument='<%# Eval("Id") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/ShoppingCart/images/trash.jpg" HeaderText="Delete"
                                                            ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ItemStyle-Width="2%" CommandName="Delete" ToolTip="Delete"
                                                                    ImageUrl="~/Account/images/delete.gif" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
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
