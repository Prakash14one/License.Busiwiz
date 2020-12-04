<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="EmployeeLeaveType.aspx.cs" Inherits="Add_Employee_Leave_Type" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>

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
    <asp:UpdatePanel ID="Updaeelrt1" runat="server">
            <ContentTemplate>
    <div class="products_box">
        <div style="padding-left: 1%;">
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text="" Visible="False" />
        </div>
        <fieldset>
            <legend>
                <asp:Label ID="lbladd" runat="server"></asp:Label>
            </legend>
            <div style="float: right;">
                <asp:Button ID="btnadd" Text="Add Leave Type" runat="server" CssClass="btnSubmit"
                    OnClick="btnadd_Click" />
            </div>
            <asp:Panel ID="pnladd" Visible="false" runat="server">
                <label><asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label><asp:DropDownList
                        ID="ddlstorename" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstorename_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
                <label><asp:Label ID="Label2" runat="server" Text="Type of Leave (i.e. Sick day, vacation day, holiday etc.)."></asp:Label><asp:Label
                        ID="Label12" runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtleavetypename"
                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                ID="REG1" runat="server" ErrorMessage="Invalid Character" SetFocusOnError="True"
                                ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtleavetypename"
                                ValidationGroup="1">
                            </asp:RegularExpressionValidator><asp:TextBox ID="txtleavetypename" runat="server"
                                MaxLength="30" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@.#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s ]+$/,'div1',30)"></asp:TextBox><asp:Label ID="Label60"  CssClass="labelcount" runat="server" Text="Max "></asp:Label><span id="div1" class="labelcount">30</span> <asp:Label ID="Label34" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label></label>
                
                   
                <div style="clear: both;">
                </div>
                <label><asp:Label ID="Label3" runat="server" Text="Is it paid leave?"></asp:Label></label>
                <asp:RadioButtonList ID="rdpais" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                    OnSelectedIndexChanged="rdpais_SelectedIndexChanged">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                </asp:RadioButtonList>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnlpaid" runat="server" Width="100%" Visible="False">
                    <fieldset>
                        <legend>
                            <asp:Label ID="lbly" runat="server" Text="Leave Eligibility Rule"></asp:Label></legend>
                        <label>
                            <asp:Label ID="Label14" runat="server" Text="Please select the number of leaves allowed per completed period for selected designation who are in employment for selected days."></asp:Label>
                        </label>
                         <div style="clear: both;">
                </div>
                <label>
                     <asp:Label ID="Label15" runat="server" Text="Number of Leaves"></asp:Label>
                </label>
                        <label>
                            
                            <asp:TextBox ID="txtleaveno" runat="server" Text="0" MaxLength="3" Width="40px"></asp:TextBox>
                          </label>
                          <label>
                            
                            <cc1:FilteredTextBoxExtender ID="txtaddress_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtleaveno" ValidChars="">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldVaator2" runat="server" ControlToValidate="txtleaveno"
                                ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                       </label>
                        <label>
                            <asp:Label ID="Label4" runat="server" Text="Leave Allowed Per Completed"></asp:Label>
                        </label>
                        <label>
                            
                            <asp:DropDownList ID="ddlpertype" runat="server" Width="75px">
                                <asp:ListItem Text="Week" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Month" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Year" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </label>
                        <label>
                        <asp:Label ID="Label5" runat="server" Text="For Designation"></asp:Label>
                        </label>
                        <label>
                            
                            <asp:DropDownList ID="ddldesi" runat="server" Width="130px">
                            </asp:DropDownList>
                            
                        </label>
                         
                        <label>
                            <asp:Label ID="Label6" runat="server" Text="Who are in Employment for Over"></asp:Label>
                          </label>
                          <label>
                          <asp:TextBox ID="txtmoreemp" runat="server" MaxLength="3" Text="0" Width="40px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequireieldValidator2" runat="server" ControlToValidate="txtmoreemp"
                                ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                          </label>   
                            <label>
                            <asp:Label ID="Label7" runat="server" Text="days"></asp:Label>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                FilterType="Custom, Numbers" TargetControlID="txtmoreemp" ValidChars="">
                            </cc1:FilteredTextBoxExtender>
                            </label>
                            
                            
                       
                    </fieldset></asp:Panel>
                <div style="clear: both;">
                </div>
                <label>
                <asp:Label ID="lblisenca" runat="server" Text="Is this leave encashable ? " Visible="False"></asp:Label>
                
                </label>
                <asp:RadioButtonList ID="rdencashable" Visible="False" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:RadioButtonList>
                <label>
                    <asp:Label ID="lblisencaemp" runat="server" Text="Employee can receive money for not taking leave days." Visible="False"></asp:Label>
                </label>
                <div style="clear: both;">
                </div>
                <asp:Button ID="ImageButton48" CssClass="btnSubmit" Text="Update" runat="server"
                    OnClick="ImageButton48_Click" ValidationGroup="1" />
                <asp:Button ID="ImageButton2" CssClass="btnSubmit" Text="Submit" runat="server" OnClick="ImageButton2_Click1"
                    ValidationGroup="1" />
                <%--<asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button2"
                    CssClass="btnSubmit" Text="Submit" />--%>
                <asp:Button ID="ImageButton7" CssClass="btnSubmit" Text="Cancel" runat="server" OnClick="ImageButton7_Click" />
                <%--<asp:Button runat="server" ID="Button3" CssClass="btnSubmit" Text="Cancel" />--%></asp:Panel>
        </fieldset>
        <div style="clear: both;">
        </div>
        <fieldset>
            <legend>
                <asp:Label ID="Label8" runat="server" Text="List of Employee Leave Types"></asp:Label></legend>
            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
            <div style="float: right;">
                <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                    OnClick="Button1_Click" />
                <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                    class="btnSubmit" type="button" value="Print" visible="false" />
                <%--<asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button1"
                        CssClass="btnSubmit" Text="Print Version" />--%>
            </div>
            <label>
                <asp:Label ID="Label9" runat="server" Text="Filter by Business Name"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
               
            </label>
            <div style="clear: both;">
            </div>
            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                <table width="100%">
                    <tr align="center">
                        <td>
                            <div id="mydiv" class="closed">
                                <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblCompany" Font-Size="20px" runat="server" Visible="false" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label13" Font-Size="18px" runat="server" Text="Business :"></asp:Label>
                                            <asp:Label ID="lblBusiness" Font-Size="18px" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label10" runat="server" Font-Size="18px" Text="List of Employee Leave Types "></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cc11:PagingGridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                DataKeyNames="ID" Width="100%" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" AllowSorting="True"
                                OnSorting="GridView1_Sorting" EmptyDataText="No Record Found." OnRowCommand="GridView1_RowCommand"
                                AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                    <asp:TemplateField HeaderText="Business Name" SortExpression="WName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstorename" runat="server" Text='<%# Eval("WName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="40%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type of Leave" SortExpression="EmployeeLeaveTypeName"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblleavename" runat="server" Text='<%# Eval("EmployeeLeaveTypeName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="50%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paid Leave" SortExpression="Ispaidleave" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="9%">
                                        <ItemTemplate>
                                         <%--   <asp:CheckBox ID="lblchk" runat="server" Enabled="false" Checked='<%# Eval("Ispaidleave") %>'>
                                            </asp:CheckBox>--%>
                                            <asp:Label ID="lblchk" runat="server" Text='<%# Eval("Ispaidleave") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="9%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Left"
                                        HeaderImageUrl="~/Account/images/edit.gif">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                CommandName="Edit" ImageUrl="~/Account/images/edit.gif" ToolTip="Edit" />
                                            <%--<asp:LinkButton ID="LinkButton4" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                    OnClick="LinkButton4_Click">Edit</asp:LinkButton>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Button1" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="15px" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />
                           </cc11:PagingGridView> 
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%--<asp:GridView ID="gvCustomres" runat="server" DataSourceID="customresDataSource"
                AutoGenerateColumns="False" GridLines="None" AllowPaging="true" CssClass="mGrid"
                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                <Columns>
                    <asp:BoundField DataField="ContactName" HeaderText="Business Name" />
                    <asp:BoundField DataField="PricePlan" HeaderText="Employee Leave Name" />
                    <asp:CheckBoxField DataField="CheckBox" HeaderText="Paid Leave" />
                    <asp:HyperLinkField Text="Edit" HeaderText="Edit" />
                    <asp:HyperLinkField Text="Delete" HeaderText="Delete" />
                </Columns>
            </asp:GridView>--%>
            <%--<asp:XmlDataSource ID="customresDataSource" runat="server" DataFile="~/App_Data/data1.xml">
            </asp:XmlDataSource>--%>
        </fieldset>
    </div>
    <!--end of right content-->
    </ContentTemplate>
    </asp:UpdatePanel>
    <div style="clear: both;">
    </div>
</asp:Content>
