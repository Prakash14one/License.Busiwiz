<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="ShippingManager.aspx.cs" Inherits="ShoppingCart_Admin_ShippingManager"
    Title="Untitled Page" %>

<%@ Register Src="~/IOffice/ShoppingCart/Admin/UserControl/UControlWizardMaster1.ascx" TagName="pnlM"
    TagPrefix="pnlM" %>
<%@ Register Src="~/IOffice/ShoppingCart/Admin/UserControl/UControlWizardMaster2ship.ascx"
    TagName="pnl2" TagPrefix="pnl2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/IOffice/ShoppingCart/Admin/UserControl/UControlWizardpanel.ascx" TagName="pnl"
    TagPrefix="pnl" %>
<%@ Register Src="~/IOffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
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
        
         function mask(evt)
        { 

           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if(evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }     
            
        }   
         function check(txt1, regex, reg,id,max_len)
            {
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
        
          function mak(id,max_len,myele)
        {
            counter=document.getElementById(id);
            
            if(myele.value.length <= max_len)
            {
                remaining_characters=max_len-myele.value.length;
                counter.innerHTML=remaining_characters;
            }
        } 
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 1%">
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="" Visible="False"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="btnaddroom" runat="server" CssClass="btnSubmit" Text="Add Shipping Company Method"
                                OnClick="btnaddroom_Click" />
                        </label>
                    </div>
                    <asp:Panel ID="addinventoryroom" Visible="false" runat="server">
                        <div style="clear: both;">
                            <table>
                                <tr>
                                    <td valign="top">
                                        <label>
                                            <asp:Label ID="Label5" runat="server" Text="Add Shipping Company Name"></asp:Label>
                                            <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidato" SetFocusOnError="true" runat="server"
                                                ControlToValidate="ddlcarrierMethod" ErrorMessage="*" ValidationGroup="44"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                ControlToValidate="ddlcarrierMethod" ValidationGroup="44"> </asp:RegularExpressionValidator>
                                            <asp:TextBox ID="ddlcarrierMethod" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',30)"></asp:TextBox>
                                            <asp:Label ID="Label8" runat="server" Text=" Max" CssClass="labelcount"></asp:Label>
                                            <span id="div1" class="labelcount">30</span>
                                            <asp:Label ID="Label14" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                        </label>
                                    </td>
                                    <td valign="top">
                                        <label>
                                            <asp:Label ID="Label4" runat="server" Text="Country They Operate In"></asp:Label>
                                            <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlcountry1"
                                                runat="server" ErrorMessage="*" InitialValue="0" ValidationGroup="44">
                                            </asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlcountry1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcountry1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td valign="top">
                                        <asp:Panel runat="server" ID="panelgridstate" Visible="false" ScrollBars="Vertical"
                                            Width="300px" Height="65px">
                                            <asp:GridView ID="GridView2" runat="server" EmptyDataText="No Record Found." CssClass="mGrid"
                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="HeaderChkbox1" runat="server" AutoPostBack="True" OnCheckedChanged="HeaderChkbox1_CheckedChanged1" />
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="State They Operate In" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label52" runat="server" Text='<%# Eval("StateName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="state id" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label53" runat="server" Text='<%# Eval("StateID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="clear: both;">
                            <table width="100%">
                                <tr>
                                    <td style="width: 220px;">
                                        <label>
                                            <asp:Label ID="Label9" runat="server" Text="Default Shipment Tracking URL"></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width: 45px;">
                                        <label>
                                            <asp:DropDownList ID="DropDownList2" runat="server" Width="60px">
                                                <asp:ListItem Text="http://" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="https://" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td style="width: 200px;">
                                        <label>
                                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" nKeydown="return mask(event)"
                                                onkeyup="return check(this,/[\\!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9./?_\s ]+$/,'Span1',50)"></asp:TextBox>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label12" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="Span1" class="labelcount">50</span>
                                            <asp:Label ID="Label10" runat="server" Text="(A-Z 0-9 _ . / ?)" CssClass="labelcount"></asp:Label>
                                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9/?_.\s]*)"
                                                ControlToValidate="TextBox1" ValidationGroup="44"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <label>
                                            <asp:Label ID="Label13" runat="server" Text="For example: If the tracking URL of your shipping company for tracking number 123456 is http://www.usps.com/cid?123456 then input www.usps.com/cid? In the text box"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label6" runat="server" Text="Shipping Type" Visible="false"></asp:Label>
                            <asp:DropDownList ID="DropDownList1" runat="server" Visible="false">
                            </asp:DropDownList>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <div style="float: left;">
                            <asp:Button ID="ImageButton1" runat="server" OnClick="Button1_Click" ValidationGroup="44"
                                Text="Submit" CssClass="btnSubmit" />
                            <asp:Button ID="Button2" runat="server" ValidationGroup="44" Text="Update" CssClass="btnSubmit"
                                OnClick="Button2_Click1" Visible="False" />
                            <asp:Button ID="ImageButton2" runat="server" OnClick="Button2_Click" CssClass="btnSubmit"
                                Text="Cancel" />
                            </label>
                            <div style="clear: both;">
                            </div>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Shipping Company Method" Font-Bold="true" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click1" CausesValidation="False" />
                            <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" visible="false" />
                        </label>
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
                                                    <asp:Label ID="lblCompany" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                    <asp:Label ID="Label7" runat="server" Font-Italic="True" Text="List of Shipping Company Method"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        DataKeyNames="ShippingManagerID" EmptyDataText="No Record Found." OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                        OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                                        OnRowUpdating="GridView1_RowUpdating" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" OnSorting="GridView1_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Shipping Company Name" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" SortExpression="CarrierMethod">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcarier" runat="server" Text='<%#Eval("CarrierMethod") %>'></asp:Label>
                                                    <asp:Label ID="lblShipMangrId" runat="server" Text='<%#Eval("ShippingManagerID") %>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Country They Operate In" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="CountryName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCatName" runat="server" Text='<%#Eval("CountryName") %>'></asp:Label>
                                                    <asp:Label ID="lblCntId" runat="server" Text='<%#Eval("Country_ID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State They Operate In" ItemStyle-Width="50%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstName" runat="server"></asp:Label>
                                                    <asp:Label ID="lblstid" runat="server" Text='<%#Eval("State_ID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Min Wgt" Visible="false" SortExpression="MinWeight">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMinWeight" runat="server" Text='<%#Eval("MinWeight") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Max Wgt" Visible="false" SortExpression="MaxWeight">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMaxWeight" runat="server" Text='<%#Eval("MaxWeight") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shipping Method" ItemStyle-Width="40%" Visible="false"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" SortExpression="ShippingType">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblShipMethod" runat="server" Text='<%#Eval("ShippingTypeName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="40%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Note" Visible="false" SortExpression="Note">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNotes" runat="server" Text='<%#Eval("Notes") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Custom" Visible="false" SortExpression="Custom">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustom" runat="server" Text='<%#Eval("Custom") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Exclude" Visible="false" SortExpression="Exclude">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="lblExclude" runat="server" Checked='<%#Eval("Exclude") %>' Enabled="false">
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Carrier Hide" Visible="false" SortExpression="CarrierHide">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="lblCarrierHide" runat="server" Checked='<%#Eval("CarrierHide") %>'
                                                        Enabled="false"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hide" Visible="false" SortExpression="Hide">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="lblHide" runat="server" Checked='<%#Eval("Hide") %>' Enabled="false">
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="By Price" Visible="false" SortExpression="ByPrice">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="lblByPrice" runat="server" Checked='<%#Eval("ByPrice") %>' Enabled="false">
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shop Cart" Visible="false" SortExpression="ShopCart">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="lblShopCart" runat="server" Checked='<%#Eval("ShopCart") %>' Enabled="false">
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Use Rules" Visible="false" SortExpression="UseRules">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="lblUseRules" runat="server" Checked='<%#Eval("UseRules") %>' Enabled="false">
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-Width="3%"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="img11" runat="server" CausesValidation="true" ToolTip="Edit"
                                                        ImageUrl="~/Account/images/edit.gif" CommandArgument='<%#Bind("ShippingManagerID") %>'
                                                        CommandName="edit"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:ButtonField ButtonType="Image" HeaderStyle-HorizontalAlign="Left" CommandName="edit" Text
                                    HeaderText="Edit" HeaderStyle-Width="2%" Text="Edit" ImageUrl="~/Account/images/edit.gif" 
                                    HeaderImageUrl="~/Account/images/edit.gif" ItemStyle-Width="2%"></asp:ButtonField>
                                           --%>
                                            <%-- <asp:CommandField HeaderText="Delete" ShowDeleteButton="true"></asp:CommandField>--%>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Button1" runat="server" CommandName="Delete" ToolTip="Delete"
                                                        ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                        CommandArgument='<%# Eval("ShippingManagerID") %>'></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                </fieldset>
                <asp:Panel ID="Panel1" Visible="false" runat="server">
                    <table id="subinnertbl" cellpadding="0" cellspacing="0" style="width: 850Px">
                        <tr>
                            <td class="label">
                            </td>
                        </tr>
                        <asp:Panel ID="pp1" runat="server" Visible="false">
                            <tr>
                                <td class="col1" colspan="2">
                                    Country :
                                </td>
                                <td class="col2">
                                    <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged1"
                                        Width="100px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlcountry"
                                        ErrorMessage="*" ValidationGroup="44" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                                <td class="col1">
                                    State :
                                </td>
                                <td class="col2">
                                    <asp:DropDownList ID="ddlState" runat="server" Width="100px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlState"
                                        ErrorMessage="*" ValidationGroup="44" InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">
                                    Minimum Weight (LBS) :
                                </td>
                                <td class="col2">
                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" TargetControlID="txtMinWeight" ValidChars="0147852369.">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtMinWeight" runat="server" Text="0" ValidationGroup="44"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMinWeight"
                                        ErrorMessage="*" ValidationGroup="44"></asp:RequiredFieldValidator>
                                </td>
                                <td class="col1">
                                    Maximum Weight (LBS) :
                                </td>
                                <td class="col2">
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                        TargetControlID="txtMaxWeight" ValidChars="0147852369.">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtMaxWeight" runat="server" Text="0" ValidationGroup="44"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMaxWeight"
                                        ErrorMessage="*" ValidationGroup="44"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">
                                    MarkUp :
                                </td>
                                <td class="col2">
                                    <asp:RadioButton ID="rbMarkYes" runat="server" GroupName="1" Text="Yes" />
                                    <asp:RadioButton ID="rbMarkupNo" runat="server" GroupName="1" Text="No" Checked="True" />
                                </td>
                                <td class="col1">
                                </td>
                                <td class="col2">
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">
                                    Carrier unit :
                                </td>
                                <td class="col2">
                                    <asp:RadioButton ID="rbCarrierunitYes" runat="server" GroupName="2" Text="Yes" />
                                    <asp:RadioButton ID="rbCarrierunitNo" runat="server" GroupName="2" Text="No" Checked="True" />
                                </td>
                                <td class="col1">
                                    Is (%) :
                                </td>
                                <td class="col2">
                                    <asp:RadioButton ID="rbIsPerYes" runat="server" GroupName="3" Text="Yes" />
                                    <asp:RadioButton ID="rbIsperNo" runat="server" GroupName="3" Text="No" Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">
                                    Exclude :
                                </td>
                                <td class="col2">
                                    <asp:RadioButton ID="rbExcludeYes" runat="server" GroupName="4" Text="Yes" />
                                    <asp:RadioButton ID="rbExcludeNo" runat="server" GroupName="4" Text="No" Checked="True" />
                                </td>
                                <td class="col1">
                                    Carrier Hide :
                                </td>
                                <td class="col2">
                                    <asp:RadioButton ID="rbCarrierYes" runat="server" GroupName="5" Text="Yes" />
                                    <asp:RadioButton ID="rbCarrierNo" runat="server" GroupName="5" Text="No" Checked="True" />
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pa2" runat="server" Visible="false">
                            <tr>
                                <td class="col1">
                                    Hide :
                                </td>
                                <td class="col2">
                                    <asp:RadioButton ID="rbHideYes" runat="server" GroupName="6" Text="Yes" />
                                    <asp:RadioButton ID="rbHideNo" runat="server" GroupName="6" Text="No" Checked="True" />
                                </td>
                                <td class="col1">
                                    By Price :
                                </td>
                                <td class="col2">
                                    <asp:RadioButton ID="rbbyPriceYes" runat="server" GroupName="7" Text="Yes" />
                                    <asp:RadioButton ID="rbbyPriceNo" runat="server" GroupName="7" Text="No" Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">
                                    Shopping Cart :
                                </td>
                                <td class="col2">
                                    <asp:RadioButton ID="rbShopCartyes" runat="server" GroupName="8" Text="Yes" />
                                    <asp:RadioButton ID="rbShopCartNo" runat="server" GroupName="8" Text="No" Checked="True" />
                                </td>
                                <td class="col1">
                                    Use Rules :
                                </td>
                                <td class="col2">
                                    <asp:RadioButton ID="rbUserYes" runat="server" GroupName="9" Text="Yes" />
                                    <asp:RadioButton ID="rbUserno" runat="server" GroupName="9" Text="No" Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">
                                    Notes :
                                </td>
                                <td class="col2">
                                    <asp:TextBox ID="txtNotes" runat="server" ValidationGroup="44"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNotes"
                                        ErrorMessage="*" ValidationGroup="44"></asp:RequiredFieldValidator>
                                </td>
                                <td class="col1">
                                    Custom :
                                </td>
                                <td class="col2">
                                    <asp:TextBox ID="txtCustome" runat="server" ValidationGroup="44"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCustome"
                                        ErrorMessage="*" ValidationGroup="44"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td class="label">
                                <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" BorderStyle="Outset"
                                    Width="300px">
                                    <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="subinnertblfc">
                                                Confirm Delete
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label3" runat="server" ForeColor="Black">You Sure , You Want to 
                                                Delete !</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="ImageButton5" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/Yes.png"
                                                    OnClick="yes_Click" />
                                                <asp:ImageButton ID="ImageButton6" runat="server" AlternateText="cancel" ImageUrl="~/ShoppingCart/images/No.png"
                                                    OnClick="ImageButton6_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel3" TargetControlID="HiddenButton222">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="col2">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel2" runat="server" Width="100%">
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
