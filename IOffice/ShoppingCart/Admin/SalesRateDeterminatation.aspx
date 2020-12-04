<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="SalesRateDeterminatation.aspx.cs" Inherits="ShoppingCart_SalesRateDeterminatation"
    Title="Untitled Page" %>

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Add Sales Rate Determination" Font-Bold="True"
                            Visible="False"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add Sales Rate Determination" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <div>
                            <asp:Panel ID="pnlCal" runat="server" Visible="False" Width="100%">
                                <div>
                                    <table id="Table1" width="100%">
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    Business Name
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label9" runat="server" Text="*" CssClass="labelstar"> </asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                    Applied Date
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:Label ID="lbldate" runat="server"> </asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    Rule Name
                                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                        ControlToValidate="txtRuleName" ValidationGroup="952"></asp:RegularExpressionValidator>
                                                       
                                                </label>
                                               
                                                <label>
                                                    <asp:Label ID="Label10" runat="server" Text="*" CssClass="labelstar"> </asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 75%" colspan="3">
                                                <label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRuleName"
                                                        ErrorMessage="*" ValidationGroup="952"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtRuleName" runat="server" MaxLength="25" onKeydown="return mask(event)"
                                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',25)"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <br />
                                                    Characters Remaining <span id="div1">25</span>
                                                    <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z,0-9,_)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    Category Name
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"> </asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 75%" colspan="3">
                                                <label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCategory"
                                                        ErrorMessage="*" InitialValue="0" ValidationGroup="952"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    Inventory Name
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlItemName"
                                                        ErrorMessage="*" InitialValue="0" ValidationGroup="952"> </asp:RequiredFieldValidator>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label12" runat="server" Text="*" CssClass="labelstar"> </asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:DropDownList ID="ddlItemName" runat="server" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:Label ID="Label6" runat="server" Text="Existing Sales Rate"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:Label ID="ExistingSalesRate" Text="0" runat="server"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    Select The Price of Item From Purchase Invoice
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" Font-Bold="False"
                                                        OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Highest</asp:ListItem>
                                                        <asp:ListItem Value="2">Lowest</asp:ListItem>
                                                        <asp:ListItem Value="3">Recent</asp:ListItem>
                                                        <asp:ListItem Value="4" Selected="True">Average</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    Selected Rate
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:Label ID="lblRate" Text="0" runat="server"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    Add Margin
                                                </label>
                                            </td>
                                            <td style="width: 75%" colspan="3">
                                                <label>
                                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtmar" ValidChars="0147852369." />
                                                    <asp:TextBox ID="txtmar" runat="server" Text="0" MaxLength="10" onkeyup="return mak('Span5',10,this)"></asp:TextBox>
                                                </label>
                                                <label>
                                                    %
                                                </label>
                                                <label>
                                                    Characters Remaining <span id="Span5">10</span>
                                                    <asp:Label ID="Label16" runat="server" Text="(0-9,.)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    Add Flat Rate Amount
                                                </label>
                                            </td>
                                            <td style="width: 75%" colspan="3">
                                                <label>
                                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender11" runat="server"
                                                        Enabled="True" TargetControlID="txtFlat" ValidChars="0147852369." />
                                                    <asp:TextBox ID="txtFlat" runat="server" Text="0" MaxLength="20" onkeyup="return mak('Span1',20,this)"></asp:TextBox>
                                                </label>
                                                <label>
                                                    Characters Remaining <span id="Span1">20</span>
                                                    <asp:Label ID="Label22" runat="server" Text="(0-9,.)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    Add Item Weight Factor
                                                </label>
                                                <label>
                                                    ($)
                                                </label>
                                            </td>
                                            <td style="width: 75%" colspan="3">
                                                <label>
                                                    <asp:TextBox ID="txtItemWeight" runat="server" Text="0" MaxLength="20" onkeyup="return mak('Span2',20,this)"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender1331" runat="server"
                                                        Enabled="True" TargetControlID="txtItemWeight" ValidChars="0147852369." />
                                                </label>
                                                <label>
                                                    Per
                                                </label>
                                                <label>
                                                    <asp:DropDownList ID="DropDownList1" Width="100px" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    Characters Remaining <span id="Span2">20</span>
                                                    <asp:Label ID="Label13" runat="server" Text="(0-9,.)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    Add Item Volume Factor
                                                </label>
                                                <label>
                                                    ($)
                                                </label>
                                            </td>
                                            <td style="width: 75%" colspan="3">
                                                <label>
                                                    <asp:TextBox ID="txtItemVolume" runat="server" Text="0" MaxLength="20" onkeyup="return mak('Span3',20,this)"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender221331" runat="server"
                                                        Enabled="True" TargetControlID="txtItemVolume" ValidChars="0147852369." />
                                                </label>
                                                <label>
                                                    Per</label>
                                                <label>
                                                    <asp:DropDownList ID="DropDownList2" Width="100px" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    Characters Remaining <span id="Span3">20</span>
                                                    <asp:Label ID="Label14" runat="server" Text="(0-9,.)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    Add Overhead Factor
                                                </label>
                                                <label>
                                                    ($)
                                                </label>
                                            </td>
                                            <td style="width: 75%" colspan="3">
                                                <label>
                                                    <asp:TextBox ID="txtOverhead1" runat="server" MaxLength="20" OnTextChanged="txtOverhead1_TextChanged"
                                                        onkeyup="return mak('Span4',20,this)" Text="0"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender221343331" runat="server"
                                                        Enabled="True" TargetControlID="txtOverhead1" ValidChars="0147852369." />
                                                </label>
                                                <label>
                                                    Per
                                                </label>
                                                <label>
                                                    <asp:DropDownList ID="DropDownList3" runat="server" Width="100px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    Characters Remaining <span id="Span4">20</span>
                                                    <asp:Label ID="Label15" runat="server" Text="(0-9,.)"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                <label>
                                                    Existing Sales Rate
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:Label ID="ExistRate" Text="0" runat="server"></asp:Label>
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                    New Sales Rate
                                                </label>
                                            </td>
                                            <td style="width: 25%">
                                                <label>
                                                    <asp:Label ID="lblTotalAmt" Text="0" runat="server"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                            </td>
                                            <td style="width: 25%">
                                                <asp:Button ID="Btn_Calculate" CssClass="btnSubmit" runat="server" Text="Calculate"
                                                    OnClick="Btn_Calculate_Click" />
                                                <asp:Button ID="Btn_Submit" CssClass="btnSubmit" runat="server" Text="Submit" OnClick="Btn_Submit_Click" />
                                                <asp:Button ID="Button3" CssClass="btnSubmit" runat="server" OnClick="Button3_Click"
                                                    Text="Update" Visible="False" />
                                                <asp:Button ID="Button2" CssClass="btnSubmit" runat="server" Text="Cancel" OnClick="Button2_Click" />
                                            </td>
                                            <td style="width: 25%">
                                            </td>
                                            <td style="width: 25%">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label5" runat="server" Text="List of Sales Rate Determination" Font-Bold="true"></asp:Label>
                    </legend>
                    <div style="clear: both;">
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button1_Click1" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" class="btnSubmit" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label8" runat="server" Text="Filter by Business Name"></asp:Label>
                        <asp:DropDownList ID="ddlfilterbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbusiness_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label7" runat="server" Text="Business :" Font-Size="18px"></asp:Label>
                                                    <asp:Label ID="lblbusinessprint" runat="server" Font-Size="18px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label4" runat="server" Text="List of Sales Rate Determination" Font-Size="18px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="RuleMasterId"
                                        GridLines="Both" Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        OnRowDeleting="GridView1_RowDeleting" EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("WName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rule Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="24%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrulename123" runat="server" Text='<%# Eval("RuleName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inventory Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblinventoryname123" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                    <asp:Label ID="lblinwmid123" runat="server" Text='<%# Eval("InvWMasterId") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Existing Sales Rate" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="18%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblexistingrate123" runat="server" Text='<%# Eval("ExistingSalesRate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New Sales Rate" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="18%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrateapplied123" runat="server" Text='<%# Eval("RateApplied") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton4" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("RuleMasterId") %>'
                                                        OnClick="LinkButton4_Click" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />--%>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Btn" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="2%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConfirm" runat="server" Visible="False" Width="100%">
                        <table cellpadding="0" cellspacing="0" id="Table2">
                            <tr>
                                <td colspan="4">
                                    <asp:ImageButton ID="ImageButton2" runat="server" AlternateText="calculate" ImageUrl="~/ShoppingCart/images/calculate.png"
                                        OnClick="btCal_Click" ValidationGroup="952" Visible="False" />
                                    &nbsp;
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" Font-Bold="False"
                                        Font-Size="14pt" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged1"
                                        RepeatDirection="Horizontal" Visible="False">
                                        <asp:ListItem Value="1">Purchase Invoice</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="col3">
                                    Item Name :
                                </td>
                                <td colspan="1" class="col2">
                                    <asp:TextBox ID="txtname" runat="server" Enabled="False" Visible="False"></asp:TextBox>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </td>
                                <td class="col3" colspan="1">
                                    Product No :
                                </td>
                                <td class="col2" colspan="1">
                                    <asp:TextBox ID="txtItemNo" runat="server" Enabled="False" Visible="False"></asp:TextBox>
                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col3">
                                    Selected Price :
                                </td>
                                <td colspan="1" class="col2">
                                    <asp:TextBox ID="txtPrice" runat="server" Enabled="False" Visible="False"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender2213433rr31" runat="server"
                                        Enabled="True" TargetControlID="txtPrice" ValidChars="0147852369." />
                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                </td>
                                <td class="col3" colspan="1">
                                    Margin :
                                </td>
                                <td class="col2" colspan="1">
                                    <asp:TextBox ID="txtmargin" runat="server" OnTextChanged="txtmargin_TextChanged"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender22134333331" runat="server"
                                        Enabled="True" TargetControlID="txtmargin" ValidChars="0147852369." />
                                </td>
                            </tr>
                            <tr>
                                <td class="col3">
                                    Weight Factor
                                </td>
                                <td colspan="1" class="col2">
                                    <asp:TextBox ID="txtWeight" runat="server" MaxLength="6"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender221343333311" runat="server"
                                        Enabled="True" TargetControlID="txtWeight" ValidChars="0147852369." />
                                </td>
                                <td class="col3" colspan="1">
                                    Volume Factor :
                                </td>
                                <td class="col2" colspan="1">
                                    <asp:TextBox ID="txtVolume" runat="server" MaxLength="6"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender2213433333hf11"
                                        runat="server" Enabled="True" TargetControlID="txtVolume" ValidChars="0147852369." />
                                </td>
                            </tr>
                            <tr>
                                <td class="col3">
                                    Overhead Factor :
                                </td>
                                <td colspan="1" class="col2">
                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender2213433333hf411"
                                        runat="server" Enabled="True" TargetControlID="txtOverhead" ValidChars="0147852369." />
                                    <asp:TextBox ID="txtOverhead" runat="server" MaxLength="6"></asp:TextBox>
                                </td>
                                <td class="col3" colspan="1">
                                    Flat Rate Amount :
                                </td>
                                <td class="col2" colspan="1">
                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender2213rtr433333hf11"
                                        runat="server" Enabled="True" TargetControlID="txtFlatAmt" ValidChars="0147852369." />
                                    <asp:TextBox ID="txtFlatAmt" runat="server" MaxLength="6"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="col3">
                                    &nbsp;Total Amount :
                                </td>
                                <td colspan="1" class="col2">
                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender221ee3433333hf11"
                                        runat="server" Enabled="True" TargetControlID="txtTotalAmt" ValidChars="0147852369." />
                                    <asp:TextBox ID="txtTotalAmt" runat="server" MaxLength="6"></asp:TextBox>
                                </td>
                                <td class="col3" colspan="1">
                                    Notes :
                                </td>
                                <td class="col2" rowspan="2">
                                    <asp:TextBox ID="txtNote" runat="server" Height="41px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="col3">
                                    &nbsp;
                                </td>
                                <td colspan="1" class="col2">
                                    &nbsp;
                                </td>
                                <td class="col3" colspan="1">
                                </td>
                            </tr>
                            <tr>
                                <td class="col3">
                                </td>
                                <td align="center" colspan="2">
                                    <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/submit.png"
                                        OnClick="btSubmit_Click" />
                                </td>
                                <td class="col2">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
