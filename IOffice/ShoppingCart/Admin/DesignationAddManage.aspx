<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DesignationAddManage.aspx.cs" Inherits="Add_Designation"
    Title="Designation-Add,Manage" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

   
</script>
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
        function mask(evt) {
            counter = document.getElementById(id);
            alert(counter);
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
    <div class="products_box">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="padding-left: 12px">
                    <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="btnadd" runat="server" CssClass="btnSubmit" OnClick="btnadd_Click"
                            Text="Add Designation" />
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <table width="100%">
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Business Name: "></asp:Label>
                                    </label>
                                </td>
                                <td width="70%">
                                    <label>
                                        <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstore_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Department Name: "></asp:Label>
                                        <asp:Label ID="Label12" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatr1" runat="server" ControlToValidate="ddldesignation"
                                            ErrorMessage="*" InitialValue="0" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddldesignation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldesignation_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label style="width:30px;">
                                        <asp:ImageButton ID="imgadd" runat="server" Height="20px" ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                            OnClick="imgadd_Click" ToolTip="Add New" Width="20px" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgref" runat="server" Height="20px" ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                            OnClick="imgref_Click" ToolTip="Refresh" Width="20px" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Designation Name: "></asp:Label>
                                        <asp:Label ID="Label13" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdegnation"
                                            ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ControlToValidate="txtdegnation"
                                            Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtdegnation" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'.&amp;*()&gt;+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'div1',30)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                                        <span id="div1" class="labelcount">30</span>
                                        <asp:Label ID="Label11" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Parent Designation of Selected Department: "></asp:Label>
                                        <asp:Label ID="Label15" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlparentdesg"
                                            ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlparentdesg" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                           
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Select Role: (From License Databse)"></asp:Label>
                                          <asp:Label ID="Label18" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DDLLicense_role"
                                            ErrorMessage="*" InitialValue="0" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                     <asp:DropDownList ID="DDLLicense_role"  runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddluserrole" Visible="false" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                    <label style="width:30px;">
                                        <asp:ImageButton ID="imgaddrole" runat="server" Visible="false" Height="20px" ImageAlign="Bottom"
                                            ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" ToolTip="Add New" Width="20px"
                                            OnClick="imgaddrole_Click" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgrefrole" runat="server"  Visible="false" Height="20px" ImageAlign="Bottom"
                                            ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                            OnClick="imgrefrole_Click" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                            <td>
                             <label>
                                <asp:Label ID="Label17" runat="server" Text="Active: "></asp:Label>                                    
                                </label>
                            </td>
                            <td>
                            <label>
                            <asp:DropDownList ID="ddlactive" runat="server" >
                                        <asp:ListItem Value="1" >Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                    </label>
                            </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td>
                                    <asp:Button ID="ImageButton1" runat="server" CssClass="btnSubmit" OnClick="ImageButton1_Click"
                                        Text="Submit" ValidationGroup="1" />
                                    <asp:Button ID="btnupdate" runat="server" CssClass="btnSubmit" Text="Update" ValidationGroup="1"
                                        OnClick="btnupdate_Click" Visible="false" />
                                    <asp:Button ID="ImageButton6" runat="server" CssClass="btnSubmit" OnClick="Button2_Click"
                                        Text="Cancel" />
                                </td>
                            </tr>
                              <tr>
                                <td colspan="2">
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Set up access rights for various parts and pages of this website for designation (Recommended)"  TextAlign="Left" Checked="true" Visible="false" />
                                </td>
                                </tr>

                        </table>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label9" runat="server" Text="List of Designations"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" OnClick="Button1_Click"
                           Visible="false" Text="Printable Version" />
                        <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="False" />
                    </div>
                    <label style="width:200px;">
                        <asp:Label ID="Label5" runat="server" Text="Filter by Business Name: "></asp:Label>
                        <asp:DropDownList ID="DropDownList3" Width="200px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label style="width:200px;">
                        <asp:Label ID="Label6" runat="server" Text="Filter by Department Name: "></asp:Label>
                        <asp:DropDownList ID="DropDownList1" Width="200px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                      <label style="width:200px;">
                        <asp:Label ID="Label19" runat="server" Text="Filter by License Role: "></asp:Label>
                        <asp:DropDownList ID="ddlLicenseRoleFilter" Width="200px" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlLicenseRoleFilter_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                     <label>
                                <asp:Label ID="Label16" runat="server" Text="Active: "></asp:Label>
                                    <asp:DropDownList ID="ddlactivesearch" runat="server" Width="200px" AutoPostBack="false" OnSelectedIndexChanged="ddlactivesearch_SelectedIndexChanged">
                                        <asp:ListItem Value="2" Selected="True">ALL</asp:ListItem>
                                        <asp:ListItem Value="1" >Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                                <label style="width:50px;">
                                <br />
                                <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" OnClick="ddlactivesearch_SelectedIndexChanged" Text="Go"  />
                                </label> 
                    <label>
                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlactivesearch_SelectedIndexChanged"
                            Visible="false">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table style="color: Black; font-style: italic; text-align: center" width="100%">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Bold="true" Font-Size="20px" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label14" runat="server" Font-Bold="true" Font-Size="20px" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusines" runat="server" Font-Bold="true" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label7" runat="server" Font-Bold="true" Font-Size="18px" Text="List of Designations"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" Font-Size="16px" Text="Department : "></asp:Label>
                                                    <asp:Label ID="lblDepartment" runat="server" Font-Size="16px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid"
                                      PageSize="20" AllowPaging="true" AllowSorting="True"   DataKeyNames="DesignationMasterId" EmptyDataText="No Record Found." GridLines="Both"
                                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                       OnRowDataBound="GridView1_RowDataBound"  OnRowDeleting="GridView1_RowDeleting1" OnRowEditing="GridView1_RowEditing" OnSorting="GridView1_Sorting"
                                        PagerStyle-CssClass="pgr" Width="100%">
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Business" ItemStyle-VerticalAlign="Top"
                                                ItemStyle-Width="20%" SortExpression="Wname">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBusname" runat="server" Text='<%# Bind("Wname") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle VerticalAlign="Top" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Department" ItemStyle-Width="20%"
                                                SortExpression="Departmentname">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdcatname" runat="server" Text='<%# Bind("Departmentname") %>'></asp:Label>
                                                    <asp:Label ID="lblgrdCatid" runat="server" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lbllicenseroleid" runat="server" Text='<%# Bind("RoleId") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Designation" ItemStyle-Width="20%"  SortExpression="DesignationName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdSubCatname" runat="server" Text='<%# Bind("DesignationName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="License Role" ItemStyle-Width="20%"  SortExpression="DesignationName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbllicenserole" runat="server"  ></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Active" ItemStyle-Width="5%"  SortExpression="Active">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblactive" runat="server" Text='<%# Bind("Active") %>' ></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                          <%--  <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Website Access Rights Role" Visible="false" ItemStyle-VerticalAlign="Top" ItemStyle-Width="20%" SortExpression="RoleId">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldisplayuser" runat="server" Text='<%# Bind("Role_name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle VerticalAlign="Top" Width="20%" />
                                            </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="View Page Access" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left"  >
                                                <ItemTemplate>
                                                    <asp:Button ID="btnmanage" Text="View Page Access" CssClass="btnSubmit" runat="server" CommandArgument='<%# Eval("RoleId") %>' CommandName="Manage" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" HeaderText="Edit" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnedit" runat="server" CommandName="Edit" ImageUrl="~/Account/images/edit.gif" ToolTip="Edit" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="id" HeaderText="DeptID" InsertVisible="False" ReadOnly="True"
                                                SortExpression="id" Visible="False" />
                                          <%--  <asp:TemplateField HeaderImageUrl="~/ShoppingCart/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Delete" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Btn" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </cc11:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                    <div style="clear: both;">
                    </div>
                    <input id="DeleteStatus" runat="server" style="width: 16px" type="hidden" value="0" />
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--end of right content-->
    <div style="clear: both;">
    </div>
</asp:Content>
