<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="GroupMaster.aspx.cs" Inherits="Add_Group_Master" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
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
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
        function Button7_onclick() {

        }
        function mask(evt) {
            counter = document.getElementById(id);
            alert(counter);
            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


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
        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
    </script>

    <asp:UpdatePanel ID="pnnn1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" ForeColor="Red" runat="server" Text="" Visible="False"></asp:Label>
                </div>
                <asp:Panel ID="Panel1" runat="server" Visible="false">
                    <fieldset>
                        <legend>
                            <asp:Label ID="lbladd" Text="" runat="server"></asp:Label>
                        </legend>
                        <label>
                            <asp:Label ID="lblBusiness" Text="Business Name" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlwarehouse" Width="300px" runat="server" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="lblClassType" Text="Class Type - Name" runat="server"></asp:Label>
                            <asp:Label ID="Label4" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcity"
                                Display="Dynamic" ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlcity" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlcity_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="lblGroupName" Text="Group Name" runat="server"></asp:Label>
                            <asp:Label ID="Label3" Text="*" CssClass="labelstar" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcity"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                ControlToValidate="txtcity" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtcity" runat="server" ForeColor="Black" Width="294px" MaxLength="40"
                                onkeyup="return check(this,/[\\/!@.'#$%^&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',40)"></asp:TextBox>
                            <div style="clear: both;">
                            </div>
                            <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="div1" class="labelcount">40</span>
                            <asp:Label ID="Label34" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="lblDescription" Text="Description" runat="server"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.,a-zA-Z0-9\s]*)"
                                ControlToValidate="txtdesc" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                            ErrorMessage="Please enter 100 chars only" Display="Dynamic" 
                            SetFocusOnError="True" ValidationExpression="^([\S\s]{0,100})$"  
                            ControlToValidate="txtdesc" ValidationGroup="1"></asp:RegularExpressionValidator> --%>
                            <div style="clear: both;">
                            </div>
                            <asp:TextBox ID="txtdesc" runat="server" ForeColor="Black" Width="230px" Height="80px"
                                MaxLength="100" onKeydown="return mask(event)" TextMode="MultiLine" onkeypress="return checktextboxmaxlength(this, 100, event)"
                                onkeyup="return check(this,/[\\/!@'#$%^&*()>+:;={}[]|\/]/g,/^[\,.a-zA-Z0-9_ \s]+$/,'Span1',100)"></asp:TextBox>
                            <br />
                            <asp:Label ID="Label6" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span1" class="labelcount">100</span>
                            <asp:Label ID="Label5" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . ,)"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblStatus" Text="Status" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlstatus" runat="server" Width="100px" Enabled="false">
                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <%-- <asp:CheckBox ID="chbxActiveStatus" runat="server" Text="Active" Enabled="False" />--%>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <asp:Button ID="img1" Text="Update" runat="server" ValidationGroup="1" OnClick="img1_Click"
                            Visible="False" CssClass="btnSubmit" />
                        <asp:Button ID="img2" Text="Cancel" runat="server" OnClick="img2_Click" Visible="false"
                            CssClass="btnSubmit" />
                    </fieldset>
                </asp:Panel>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblListAccountGroup" Text="List of Account Groups" runat="server"></asp:Label></legend>
                    <div style="float: right;">
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button1_Click" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" class="btnSubmit" />
                    </div>
                    <label>
                        <asp:Label ID="lblSearchBusiness" Text="Filter by Business Name" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" Width="180px"
                            OnSelectedIndexChanged="ddlstore_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="lblSearchClass" Text="Select by Class Type" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlSearchByCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchByCountry_SelectedIndexChanged"
                            Width="180px">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="lblSearchClassName" Text="Select by Class Name" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlSearchByState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchByState_SelectedIndexChanged"
                            Width="180px">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table id="GridTbl" width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" style="color: Black; font-style: italic">
                                                    <asp:Label ID="lblcomname" runat="server" Font-Bold="True" Font-Size="20px" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="color: Black; font-style: italic">
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="18px" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblbusinessprint" runat="server" Font-Bold="True" Font-Size="18px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="color: Black; font-style: italic">
                                                    <asp:Label ID="lblhead" runat="server" Font-Bold="True" Font-Size="18px" Text="List of Account Groups"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="color: Black; font-style: italic">
                                                    <asp:Label ID="lblclaat" runat="server" Font-Size="16px" Text="Class Type :"></asp:Label>
                                                    <asp:Label ID="lblctype" runat="server" Font-Size="16px"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="lblcma" runat="server" Font-Size="16px" Text="Class Master :"></asp:Label>
                                                    <asp:Label ID="lblcmast" runat="server" Font-Size="16px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AllowSorting="True" DataKeyNames="Id"
                                        AutoGenerateColumns="False" GridLines="Both" AllowPaging="true" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowEditing="GridView1_RowEditing"
                                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                        OnSorting="GridView1_Sorting" PageSize="20" EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="GroupId" SortExpression="GroupId" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgroupid" runat="server" Text='<%#Bind("id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="Name" ItemStyle-Width="20%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstorename" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class Type : Class Name" SortExpression="country"
                                                ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcountrystate" runat="server" Text='<%# Bind("country") %>'></asp:Label>
                                                    <asp:Label ID="lblstateid" runat="server" Text='<%# Bind("ClassId") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Group Name" SortExpression="groupdisplayname" ItemStyle-Width="30%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("groupdisplayname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-Width="3%" SortExpression="active"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="CheckBox1" runat="server" Text='<%# Bind("active") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnedit" runat="server" CommandName="Edit" ImageUrl="~/Account/images/edit.gif"
                                                        ToolTip="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:ButtonField ButtonType="Image" CommandName="Edit" HeaderText="Edit" Text="Edit"
                                                ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif"
                                                ImageUrl="~/Account/images/edit.gif" />--%>
                                        </Columns>
                                    </cc11:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
            <!--end of right content-->
            <div style="clear: both;">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
