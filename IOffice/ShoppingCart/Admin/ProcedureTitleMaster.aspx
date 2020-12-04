<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="ProcedureTitleMaster.aspx.cs" Inherits="ShoppingCart_Admin_ProcedureTitleMaster" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
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
        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

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
        function mask(evt, max_len) {



            if (evt.keyCode == 13) {

                return true;
            }


              if(evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219||evt.keyCode==59||evt.keyCode==186)
            { 

                alert("You have entered an invalid character");
                return false;
            }




        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="False" />
            </div>
            <div style="clear: both;">
            </div>
            <fieldset>
                <legend>
                    <asp:Label runat="server" ID="lbllegend" Text=""></asp:Label>
                </legend>
                <div style="float: right;">
                    <asp:Button ID="btnadd" runat="server" CssClass="btnSubmit" Text="Add Procedure"
                        OnClick="btnadd_Click" />
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Pnladdnew" runat="server" Visible="false">
                    <table id="innertbl" width="100%">
                        <tr>
                            <td style="width: 25%" align="right">
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 75%">
                                <label>
                                    <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" Width="250px"
                                        ValidationGroup="1" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%" align="right">
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Policy/Procedure/Rule Category Name"></asp:Label>
                                    <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                        InitialValue="0" ControlToValidate="ddlpolicyprocedurerule" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td style="width: 75%">
                                <label>
                                    <asp:DropDownList ID="ddlpolicyprocedurerule" runat="server" AutoPostBack="True"
                                        Width="250px" ValidationGroup="1" OnSelectedIndexChanged="ddlpolicyprocedurerule_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:ImageButton ID="imgadd" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                        ToolTip="AddNew" Width="20px" ImageAlign="Bottom" OnClick="imgadd_Click" />
                                    <asp:ImageButton ID="imgrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                        ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                        ImageAlign="Bottom" OnClick="imgrefresh_Click" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%" align="right">
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Policy Title"></asp:Label>
                                    <asp:Label ID="Label13" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                        InitialValue="0" ControlToValidate="ddlpolicy" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td style="width: 75%">
                                <label>
                                    <asp:DropDownList ID="ddlpolicy" runat="server" Width="250px">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:ImageButton ID="imgaddtitle" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                        ToolTip="AddNew" Width="20px" ImageAlign="Bottom" OnClick="imgaddtitle_Click" />
                                    <asp:ImageButton ID="imgrefreshtitle" runat="server" AlternateText="Refresh" Height="20px"
                                        ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                        ImageAlign="Bottom" OnClick="imgrefreshtitle_Click" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%" align="right">
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Procedure Title"></asp:Label>
                                    <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtprocedure"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                        SetFocusOnError="True" ValidationExpression="^([a-z().A-Z-,0-9\s]*)" ControlToValidate="txtprocedure"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td style="width: 75%">
                                <label>
                                    <asp:TextBox ID="txtprocedure" MaxLength="150" runat="server" Width="350px" ValidationGroup="1"
                                        onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%^'_+@&*>:;={}[]|\/]/g,/^[\a-z().A-Z0-9,-\s]+$/,'div1',150)"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label runat="server" ID="Label12" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="div1">150</span>
                                    <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 ) ( , . -)" CssClass="labelcount"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                            </td>
                            <td style="width: 75%">
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" ValidationGroup="1" OnClick="btnsubmit_Click"
                                    CssClass="btnSubmit" />
                                <asp:Button ID="btnupdate" runat="server" Text="Update" Visible="false" ValidationGroup="1"
                                    CssClass="btnSubmit" OnClick="btnupdate_Click" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                    CssClass="btnSubmit" />
                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </fieldset>
            <fieldset>
                <legend>
                    <asp:Label ID="Label9" runat="server" Text="List of Procedure Titles"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <asp:Button ID="btnprintableversion" runat="server" Text="Printable Version" OnClick="btnprintableversion_Click"
                                CssClass="btnSubmit" />
                            <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                class="btnSubmit" type="button" value="Print" visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" align="right">
                            <label>
                                <asp:Label ID="Label6" runat="server" Text="Business Name"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 75%">
                            <label>
                                <asp:DropDownList ID="ddlfilterbybusiness" runat="server" AutoPostBack="True" Width="210px"
                                    OnSelectedIndexChanged="ddlfilterbybusiness_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" align="right">
                            <label>
                                <asp:Label ID="Label7" runat="server" Text="Policy Category"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 75%">
                            <label>
                                <asp:DropDownList ID="ddlfilterpolicyprocedure" runat="server" AutoPostBack="True"
                                    Width="210px" OnSelectedIndexChanged="ddlfilterpolicyprocedure_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" align="right">
                            <label>
                                <asp:Label ID="Label8" runat="server" Text="Policy Title"></asp:Label>
                            </label>
                        </td>
                        <td style="width: 75%">
                            <label>
                                <asp:DropDownList ID="ddlfilterpolicy" runat="server" OnSelectedIndexChanged="ddlfilterpolicy_SelectedIndexChanged"
                                    Width="210px">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                <table width="100%">
                                    <tr align="center">
                                        <td>
                                            <div id="mydiv" class="closed">
                                                <table width="850Px">
                                                    <tr>
                                                        <td align="center" style="font-size: 20px; font-weight: bold;">
                                                            <asp:Label ID="lblcmpny" runat="server" Font-Italic="true" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="font-size: 20px; font-weight: bold;">
                                                            <asp:Label ID="Label10" runat="server" Text="Business: " Font-Italic="true"></asp:Label>
                                                            <asp:Label ID="lblBusiness" runat="server" Font-Italic="true" ForeColor="Black"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                            <asp:Label ID="Label11" runat="server" Text="List of Procedure Titles" Font-Italic="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <cc11:PagingGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="Id"
                                                Width="100%" EmptyDataText="No Record Found." AllowSorting="True" OnSorting="GridView1_Sorting"
                                                OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                                                OnPageIndexChanging="GridView1_PageIndexChanging">
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Procedure Title ID" SortExpression="Id" ItemStyle-Width="10%"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblruleid" runat="server" Text='<%# Eval("Id") %>' Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Business Name" SortExpression="WName" ItemStyle-Width="13%"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbusinessname123" runat="server" Text='<%# Eval("WName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category" SortExpression="Policyprocedurecategory"
                                                        ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpolicyprocedure123" runat="server" Text='<%# Eval("Policyprocedurecategory") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Policy Title" SortExpression="PolicyTitle" ItemStyle-Width="20%"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpolicytitle123" runat="server" Text='<%# Eval("PolicyTitle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Procedure Title" SortExpression="ProcedureTitle" ItemStyle-Width="20%"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblproceduretitle" runat="server" Text='<%# Eval("ProcedureTitle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id") %>'
                                                                ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                        HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ToolTip="Delete"
                                                                CommandArgument='<%# Eval("Id") %>' ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                            </asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:ButtonField CommandName="Edit" HeaderText="Edit" 
                                                    Text="Edit" ButtonType="Image" HeaderStyle-HorizontalAlign="Left"
                                                                   HeaderImageUrl="~/Account/images/edit.gif" ImageUrl="~/Account/images/edit.gif"
                                                                 ItemStyle-Width="2%" >
                                              
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="2%" />
                                                </asp:ButtonField>--%>
                                                    <%--<asp:ButtonField CommandName="Delete" HeaderText="Delete" Text="Delete" 
                                                    ButtonType="Image" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderImageUrl="~/ShoppingCart/images/trash.jpg" ImageUrl="~/Account/images/delete.gif"
                                                                 ItemStyle-Width="3%" >
                                        
                                               
                                                   <HeaderStyle HorizontalAlign="Left" />
                                                   <ItemStyle Width="3%" />
                                                </asp:ButtonField>--%>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </cc11:PagingGridView>
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
