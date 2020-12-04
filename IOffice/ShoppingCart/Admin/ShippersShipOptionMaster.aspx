<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="ShippersShipOptionMaster.aspx.cs" Inherits="ShoppingCart_Admin_ShippersShipOptionMaster"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 1%">
                <asp:Label ID="statuslable" ForeColor="Red" runat="server" Text=""></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="title" Visible="false"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnaddroom" runat="server" CssClass="btnSubmit" Text="Add Shipping Option"
                            OnClick="btnaddroom_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="addinventoryroom" Visible="false" Width="100%" runat="server">
                        <label>
                            <asp:Label ID="Label5" runat="server" Text="Select Business Name"></asp:Label>
                            <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label2" runat="server" Text="Select Shipping Company"></asp:Label>
                            <asp:RequiredFieldValidator ID="req" runat="server" ControlToValidate="ddldesignation"
                                ValidationGroup="1" SetFocusOnError="true" InitialValue="0" ErrorMessage="*">
                            </asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddldesignation" runat="server">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Shipping Option 1">
                            </asp:Label>
                            <asp:Label ID="Label15" runat="server" Text="*"></asp:Label>
                            <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                ControlToValidate="txtdegnation" ValidationGroup="1">
                            </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdegnation"
                                ErrorMessage="*" ValidationGroup="1">
                            </asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtdegnation" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',20)"
                                runat="server" MaxLength="20">
                            </asp:TextBox>
                            
                            <asp:Label ID="Label14" runat="server" Text="Max" CssClass="labelcount" ></asp:Label>
                            <span id="div1" class="labelcount">20</span>
                            <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                        </label>
                       
                        <label>
                         <asp:Label ID="Label6" runat="server" Text="Shipping Option 2">
                                        </asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txt2" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                         <asp:TextBox ID="txt2" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span1',20)"
                                            runat="server" MaxLength="20">
                                        </asp:TextBox>
                                        <asp:Label ID="Label16" runat="server" Text="Max" CssClass="labelcount" ></asp:Label>
                                        <span id="Span1" class="labelcount">20</span>
                                        <asp:Label ID="Label19" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                        </label>
                         <div style="clear: both;">
                        </div>
                        <label>
                         <asp:Label ID="Label8" runat="server" Text="Shipping Option 3">
                                        </asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txt3" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                         <asp:TextBox ID="txt3" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span2',20)"
                                            runat="server" MaxLength="20">
                                        </asp:TextBox>
                                        <asp:Label ID="Label20" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> <span id="Span2" class="labelcount">20</span>
                                        <asp:Label ID="Label7" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                        </label>
                        <label>
                         <asp:Label ID="Label10" runat="server" Text="Shipping Option 4">
                                        </asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="TextBox1" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                          <asp:TextBox ID="TextBox1" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span3',20)"
                                            runat="server" MaxLength="20">
                                        </asp:TextBox>
                                          <asp:Label ID="Label21" runat="server" Text="Max" CssClass="labelcount" ></asp:Label>
                                           <span id="Span3" class="labelcount">20</span>
                                        <asp:Label ID="Label9" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                        </label>
                      
                        <label>
                        
                                        <asp:Label ID="Label12" runat="server" Text="Shipping Option 5">
                                        </asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="TextBox2" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                         <asp:TextBox ID="TextBox2" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span4',20)"
                                            runat="server" MaxLength="20">
                                        </asp:TextBox>
                                          <asp:Label ID="Label22" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                          <span id="Span4" class="labelcount">20</span>
                                        <asp:Label ID="Label11" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                   
                        </label>
                         <div style="clear: both;">
                        </div>
                          <asp:Button ID="ImageButton1" runat="server" AlternateText="insert" CssClass="btnSubmit"
                                        OnClick="ImageButton1_Click" Text="Submit" ValidationGroup="1" />
                                    <asp:Button ID="ImageButton7" CssClass="btnSubmit" runat="server" AlternateText="Update"
                                        Text="Update" Visible="false" OnClick="ImageButton7_Click" ValidationGroup="1" />
                                    <asp:Button ID="ImageButton6" CssClass="btnSubmit" runat="server" AlternateText="cancel"
                                        Text="Cancel" OnClick="ImageButtonasd2_Click" />
                                    <asp:Label ID="lblid" runat="server" Text="lblid" Visible="False"></asp:Label>
                        <div style="clear: both;">
                        </div>
                        
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Shipping Companies Shipping Options" Font-Bold="true"
                            runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            OnClick="Button1_Click1" CausesValidation="False" />
                        <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label17" runat="server" Text="Filter by Business"></asp:Label>
                    </label>
                    <label>
                        <asp:DropDownList ID="ddlfilterbusiness" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfilterbusiness_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label18" runat="server" Text="Search by Keyword"></asp:Label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Character"
                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                            ControlToValidate="txtsearch">
                        </asp:RegularExpressionValidator>
                    </label>
                    <label>
                        <asp:TextBox runat="server" ID="txtsearch" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span5',20)"
                            MaxLength="20" OnTextChanged="txtsearch_TextChanged" AutoPostBack="True"></asp:TextBox>
                    </label>
                    <label>
                        <asp:Label ID="Label23" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> <span id="Span5" class="labelcount">20</span>
                        <asp:Label ID="Label13" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Italic="True" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label runat="server" ID="name" Font-Italic="true" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label4" runat="server" Font-Italic="True" Text="List of Shipping Companies Shipping Options"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                        DataKeyNames="ShippersShipOptionMasterId" Width="100%" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                        OnRowCommand="GridView1_RowCommand" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                                        OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting1" CssClass="mGrid"
                                        GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No Record Found." AllowPaging="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbname" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shipping Company Name" SortExpression="ShippersName"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdcatname" runat="server" Text='<%# Bind("ShippersName") %>'></asp:Label>
                                                    <asp:Label ID="lblgrdCatid" runat="server" Text='<%# Bind("ShippersId") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shipping Options" SortExpression="Options" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdSubCatname" runat="server" Text='<%# Bind("Options") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shipping Option 1" SortExpression="OptionName" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblshioption1231" runat="server" Text='<%# Bind("OptionName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shipping Option 2" SortExpression="OptionName2" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblshioption12314564" runat="server" Text='<%# Bind("OptionName2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shipping Option 3" SortExpression="OptionName3" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblshioption12312" runat="server" Text='<%# Bind("OptionName3") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shipping Option 4" SortExpression="OptionName4" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblshioption1231234" runat="server" Text='<%# Bind("OptionName4") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shipping Option 5" SortExpression="OptionName5" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblshioption123123412345" runat="server" Text='<%# Bind("OptionName5") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DeptID" HeaderText="DeptID" InsertVisible="False" ReadOnly="True"
                                                SortExpression="DeptID" Visible="False"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="button3" runat="server" ToolTip="Edit" CommandName="Edit" ImageUrl="~/Account/images/edit.gif"
                                                        CommandArgument='<%# Eval("ShippersShipOptionMasterId") %>'></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/trash.jpg"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Button2" runat="server" ToolTip="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        CommandName="Delete" CommandArgument='<%# Eval("ShippersShipOptionMasterId") %>'
                                                        OnClientClick="return confirm('Do you wish to delete this record?');"></asp:ImageButton>
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
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" BorderStyle="Outset"
                        Width="300px">
                        <table cellpadding="0" cellspacing="0" style="width: 290px">
                            <tr>
                                <td>
                                    Confirm Delete
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" ForeColor="Black">You Sure , You Want to 
                                    Delete !</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="ImageButton2" runat="server" AlternateText="submit" Text="Yes" OnClick="ImageButton2_Click" />
                                    <asp:Button ID="ImageButton5" runat="server" Text="No" AlternateText="cancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel3" TargetControlID="HiddenButton222">
                    </cc1:ModalPopupExtender>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
