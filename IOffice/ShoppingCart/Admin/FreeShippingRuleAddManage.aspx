<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="FreeShippingRuleAddManage.aspx.cs" Inherits="ShoppingCart_Admin_FreeShippingRuleAddManage"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
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
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="Lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Labelp5" runat="server" Font-Bold="true" Text="List of Free Shipping Rule Names"></asp:Label>
                    </legend>
                    <label>
                        <asp:CheckBox ID="chkstep1" runat="server" AutoPostBack="True" Text="Step 1.  Add free shipping rules"
                            OnCheckedChanged="chkstep1_CheckedChanged" TextAlign="Left" />
                    </label>
                      <div style="clear: both;">
                </div>
                    <div style="width:100%; float: right;">
                        <asp:Panel ID="Panel2" runat="server" Width="100%" Visible="false">
                            <asp:GridView ID="grdFreeRule" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                PagerStyle-CssClass="pgr" Width="100%" AlternatingRowStyle-CssClass="alt" ShowFooter="True"
                                OnRowDataBound="grdFreeRule_RowDataBound" DataKeyNames="FreeShipingId" OnRowCommand="grdFreeRule_RowCommand"
                                OnRowEditing="grdFreeRule_RowEditing" OnRowCancelingEdit="grdFreeRule_RowCancelingEdit"
                                OnRowUpdating="grdFreeRule_RowUpdating" OnRowDeleting="grdFreeRule_RowDeleting"
                                PageSize="5" EmptyDataText="No Record Found." OnSorting="grdFreeRule_Sorting"
                                OnRowCreated="grdFreeRule_RowCreated">
                                <Columns>
                                    <asp:TemplateField HeaderText="Business Name" FooterStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="15%">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblwarename" Text='<%#Bind("Name") %>' runat="server" Visible="false">                                    
                                            </asp:Label>
                                            <asp:DropDownList ID="ddlwarename" runat="server" Width="98%">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlwarename" Text="*" InitialValue="0"
                                                ValidationGroup="1" ID="requiredfieldvwae" runat="server"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlware" runat="server" Width="98%">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlware" Text="*" InitialValue="0"
                                                ValidationGroup="add1" ID="requiredfieldvwaee" runat="server"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblwname" Text='<%#Bind("Name") %>' runat="server" >                                        
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Free Shipping Rule" FooterText="Add New" FooterStyle-VerticalAlign="Top"
                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFreeShipRule" runat="server" Text='<%#Bind("FreeShipingName") %>'></asp:Label>
                                            <asp:Label ID="lblmid" runat="server" Text='<%#Bind("FreeShipingId") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewSchemForFreeShip" MaxLength="20" runat="server" Width="150px"
                                                onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'_.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'div1t',20)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="txtNewSchemForFreeShip" Text="*" ValidationGroup="add1" Display="Dynamic"
                                                ID="requiredfieldvalidator1" runat="server"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtNewSchemForFreeShip" ValidationGroup="add1"></asp:RegularExpressionValidator>
                                            <asp:Label ID="Labssl22" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="div1t" class="labelcount">20</span>
                                            <asp:Label ID="Labecl2s" runat="server" Text="(A-Z 0-9)" CssClass="labelcount"></asp:Label>
                                           <%-- <asp:Button ID="btnAddFreeShippingRule" Text="Add" runat="server" ValidationGroup="add1"
                                                CssClass="btnSubmit" OnClick="btnAddFreeShippingRule_Click" />--%>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtGrdInRuleName" MaxLength="20" runat="server" Text='<%#Bind("FreeShipingName") %>'
                                                Width="150px" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'_.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'div13',20)"></asp:TextBox>
                                            <asp:Label ID="lblmid" runat="server" Text='<%#Bind("FreeShipingId") %>' Visible="false"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2227" runat="server" ControlToValidate="txtGrdInRuleName"
                                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REG12" runat="server" ErrorMessage="Invalid Character"
                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                ControlToValidate="txtGrdInRuleName" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            <asp:Label ID="Labl22" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="div13" class="labelcount">20</span>
                                            <asp:Label ID="Labecl2" runat="server" Text="(A-Z 0-9)" CssClass="labelcount"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min Order Size" FooterStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMinOrdrSize" runat="server" Text='<%#Bind("MinOrdSize") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtMinOrdrSize" runat="server" Width="70px" onkeyup="return check(this,/[\\/!@#$%^'._&*()>+:;={}[]|\/]/g,/^[\0-9\s]+$/,'div125y',10)"
                                                MaxLength="10"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtSchefalueDiscount" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" TargetControlID="txtMinOrdrSize" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpssionValior3" ControlToValidate="txtMinOrdrSize"
                                                ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"
                                                ValidationGroup="add1" Display="Dynamic"></asp:RegularExpressionValidator>
                                            in USD
                                            <asp:RequiredFieldValidator ControlToValidate="txtMinOrdrSize" Text="*" ValidationGroup="add1"
                                                ID="requiredfieldvalidator12" runat="server"></asp:RequiredFieldValidator>
                                                <br />
                                            <asp:Label ID="abelq22" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="div125y" class="labelcount">10</span>
                                            <asp:Label ID="Lablq2" runat="server" Text="(0-9)" CssClass="labelcount"></asp:Label>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtGrdInMinOrder" runat="server" MaxLength="10" Width="70px" Text='<%#Bind("MinOrdSize") %>'
                                                onkeyup="return check(this,/[\\/!@#$%^'._&*()>+:;={}[]|\/]/g,/^[\0-9\s]+$/,'div125r',10)"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="tmaVhhfalueDiscount" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" TargetControlID="txtGrdInMinOrder" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatord447" runat="server" ControlToValidate="txtGrdInMinOrder" Display="Dynamic"
                                                ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                               
                                            <asp:Label ID="abel22z" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="div125r" class="labelcount">10</span>
                                            <asp:Label ID="Labl2z" runat="server" Text="(0-9)" CssClass="labelcount"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Max Order Size" FooterStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaxOrdrSize" runat="server" onkeypress="return RealNumWithDecimal(this,event,2);"
                                                Text='<%#Bind("MaxOrderSize") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtMaxOrdrSize" runat="server" Width="70px" MaxLength="10" onkeyup="return check(this,/[\\/!@#$%^'._&*()>+:;={}[]|\/]/g,/^[\0-9\s]+$/,'div124',10)"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtSchemaVhhfalueDiunt" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" TargetControlID="txtMaxOrdrSize" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpssionValidaor3" ControlToValidate="txtMaxOrdrSize"
                                                ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"
                                                ValidationGroup="add1" Display="Dynamic"></asp:RegularExpressionValidator>
                                            in USD
                                            <asp:RequiredFieldValidator ControlToValidate="txtMaxOrdrSize" Text="*" ValidationGroup="add1"
                                                ID="requiredfieldvalidator13" runat="server"></asp:RequiredFieldValidator>
                                                <br />
                                            <asp:Label ID="abehl22" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="div124" class="labelcount">10</span>
                                            <asp:Label ID="Lahbl2" runat="server" Text="(0-9)" CssClass="labelcount"></asp:Label>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtGrdInMaxOrder" MaxLength="10" runat="server" Width="70px" Text='<%#Bind("MaxOrderSize") %>'
                                                onkeyup="return check(this,/[\\/!@#$%^'._&*()>+:;={}[]|\/]/g,/^[\0-9\s]+$/,'div125',10)"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtSchemaVhhfalueDiscount" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" TargetControlID="txtGrdInMaxOrder" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpssionValidaor3" ControlToValidate="txtGrdInMaxOrder"
                                                ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"
                                                ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator74" runat="server" ControlToValidate="txtGrdInMaxOrder"
                                                ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                               
                                            <asp:Label ID="ab5el22" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                                id="div125" class="labelcount">10</span>
                                            <asp:Label ID="Labl72" runat="server" Text="(0-9)" CssClass="labelcount"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Destination Country" FooterStyle-VerticalAlign="Top"
                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCountry" runat="server" Text='<%#Bind("CountryName") %>'></asp:Label><br />
                                            <asp:Label ID="lblCountryId" runat="server" Text='<%#Bind("CountryId") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlGrdCountry" runat="server" AutoPostBack="true" Width="98%"
                                                OnSelectedIndexChanged="ddlGrdCountry_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlGrdCountry" Text="*" InitialValue="0"
                                                ValidationGroup="add1" ID="requiredfieldvalidator14" runat="server"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlGrdCountry" runat="server" AutoPostBack="true" Width="98%"
                                                OnSelectedIndexChanged="ddlGrdCountry_SelectedIndexChanged17772">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlGrdCountry" Text="*" InitialValue="0"
                                                ValidationGroup="1" ID="requiredfieldvalidator125554" runat="server"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblCountry" runat="server" Text='<%#Bind("CountryName") %>' Visible="false"></asp:Label><br />
                                            <asp:Label ID="lblCountryId" runat="server" Text='<%#Bind("CountryId") %>' Visible="false"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Destination State" FooterStyle-VerticalAlign="Top"
                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstate" runat="server" Text='<%#Bind("StateName") %>'></asp:Label><br />
                                            <asp:Label ID="lblStateId" runat="server" Text='<%#Bind("StateId") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlGrdState" runat="server" AutoPostBack="true" Width="98%"
                                                OnSelectedIndexChanged="ddlGrdState_SelectedIndexChanged132">
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ControlToValidate="ddlGrdState" Text="*" InitialValue="0"
                                            ValidationGroup="add61" ID="requiredfieldvalidator15" runat="server"></asp:RequiredFieldValidator>--%>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlGrdState" runat="server" AutoPostBack="true" Width="98%"
                                                OnSelectedIndexChanged="ddlGrdState_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ControlToValidate="ddlGrdState" Text="*" InitialValue="0"
                                            ValidationGroup="add1" ID="requiredfieldvalidator156565" runat="server"></asp:RequiredFieldValidator>--%>
                                            <asp:Label ID="lblstate" runat="server" Text='<%#Bind("StateName") %>' Visible="false"></asp:Label><br />
                                            <asp:Label ID="lblStateId" runat="server" Text='<%#Bind("StateId") %>' Visible="false"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Destination City" FooterStyle-VerticalAlign="Top"
                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcity" runat="server" Text='<%#Bind("CityName") %>'></asp:Label>
                                            <asp:Label ID="lblCityId" runat="server" Text='<%#Bind("CityId") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlGrdCity" runat="server" Width="98%">
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ControlToValidate="ddlGrdCity" Text="*" InitialValue="0"
                                            ValidationGroup="add1" ID="requiredfieldvalidator16" runat="server"></asp:RequiredFieldValidator>--%>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlGrdCity" runat="server" Width="98%">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlGrdCity" Text="*" InitialValue="0"
                                                ValidationGroup="1" ID="requiredfieldvalidator144467676766" runat="server"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblcity" runat="server" Text='<%#Bind("CityName") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblCityId" runat="server" Text='<%#Bind("CityId") %>' Visible="false"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                
                                 <asp:CommandField  ShowEditButton="true" ValidationGroup="1" CausesValidation="true"
                                        HeaderText="Edit" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" ButtonType="Image"
                                        HeaderImageUrl="~/Account/images/edit.gif" EditImageUrl="~/Account/images/edit.gif"
                                        UpdateImageUrl="~/Account/images/UpdateGrid.JPG" CancelImageUrl="~/images/delete.gif" />
                                   
                                
                 
                               
                                     <%-- <asp:TemplateField HeaderText="Edit" ItemStyle-Width="2%" HeaderImageUrl="~/Account/images/edit.gif"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="editc" ToolTip="Edit" runat="server" CommandArgument='<%# Bind("FreeShipingId") %>' CommandName="edit"
                                                     ImageUrl="~/Account/images/edit.gif" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                             <asp:Button ID="btnAddFreeShippingRule" Text="Add" runat="server" ValidationGroup="add1"
                                                CssClass="btnSubmit" OnClick="btnAddFreeShippingRule_Click" />
                                            </FooterTemplate>
                                        </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%" FooterStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Btndele" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                          <asp:Button ID="btnAddFreeShippingRule" Text="Add" runat="server" ValidationGroup="add1"
                                                CssClass="btnSubmit" OnClick="btnAddFreeShippingRule_Click" />
                                            </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-ForeColor="#416271" ItemStyle-Width="50px" HeaderText="Select">
                                        <ItemStyle Width="50px" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </fieldset>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnlRuleDetails" runat="server" Visible="true" Width="100%">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Visible="False" Text="Product free shipping restrictions for the "></asp:Label>
                            <asp:Label ID="lblFreeshippingruleName" Font-Bold="true" Visible="false" runat="server"></asp:Label>
                            <asp:Label ID="Label7" runat="server" Font-Bold="true" Visible="false" Text=" Rule"></asp:Label>
                        </legend>
                        <label>
                            <asp:Label ID="Label3" runat="server" Font-Bold="true" Visible="true" Text="Step 2.  Set Restriction for Inventory Item Ordered"></asp:Label>
                        </label>
                        <div style="clear: both;">
                            <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div style="clear: both;">
                            <label>
                                <asp:Label ID="Label8" runat="server" Text="Select Free Shipping Rule to set Restriction "></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiFieldValidator2" runat="server" ControlToValidate="ddlWarehouse"
                                    ErrorMessage="*" ValidationGroup="gp1" InitialValue="0"></asp:RequiredFieldValidator>
                            </label>
                            <label>
                                <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <table>
                            <tr>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Restriction A:"></asp:Label>
                                    </label>
                                </td>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Minimum quantity of selected inventory items ordered to qualify for free shipping"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMinQty"
                                            ErrorMessage="*" ValidationGroup="gp1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpssionValior3" ControlToValidate="txtMinQty"
                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"
                                            ValidationGroup="gp1" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td valign="top">
                                    <label>
                                        <asp:TextBox ID="txtMinQty" runat="server" Width="120px" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;.={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div2',10)"
                                            Text="0" MaxLength="10"></asp:TextBox>
                                        <asp:Label ID="abel22" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span id="div2"
                                            class="labelcount">10</span>
                                            <asp:Label ID="Label47" runat="server" Text="(0-9)" CssClass="labelcount"></asp:Label>
                                            <cc1:FilteredTextBoxExtender ID="FiltereextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="txtMinQty" ValidChars="0147852369">
                                            </cc1:FilteredTextBoxExtender>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text="Restriction B:"></asp:Label>
                                    </label>
                                </td>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Minimum amount of selected inventory items ordered to qualify for free shipping"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMinAmt"
                                            ErrorMessage="*" ValidationGroup="gp1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtMinAmt"
                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"
                                            ValidationGroup="gp1" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td valign="top">
                                    <label>
                                        <asp:TextBox ID="txtMinAmt" runat="server" Width="120px" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.\s]+$/,'Span1',10)"
                                            Text="0" MaxLength="10"></asp:TextBox>
                                        <asp:Label ID="Label12" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span id="Span1"
                                            class="labelcount">10</span>
                                            <asp:Label ID="Label5" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label>
                                            <cc1:FilteredTextBoxExtender ID="FiltredTextBoxEtender1" runat="server" Enabled="True"
                                                TargetControlID="txtMinAmt" ValidChars="0147852369.">
                                            </cc1:FilteredTextBoxExtender>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td style="width: 468px">
                                                <label>
                                                    <asp:Label ID="Label9" runat="server" Text="Select the option for free shipping to apply to your selected items"></asp:Label></label>
                                            </td>
                                            <td>
                                             
                                                    <asp:RadioButtonList ID="rddate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rddate_SelectedIndexChanged"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0">Permanently</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="1">For Selected Dates</asp:ListItem>
                                                    </asp:RadioButtonList>
                                              
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="2">
                                    <asp:Panel ID="pnldate" runat="server" Visible="true">
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label17" runat="server" Text="To :"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtStartDate"
                                                            ErrorMessage="*" ValidationGroup="gp1"></asp:RequiredFieldValidator>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:TextBox ID="txtStartDate" runat="server" Width="70px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" PopupButtonID="ImageButton1"
                                                            TargetControlID="txtStartDate">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="TextBox1_MaskedEditExtender" runat="server" CultureName="en-AU"
                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtStartDate" />
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label10" runat="server" Text="From :"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEndDate"
                                                            ErrorMessage="*" ValidationGroup="gp1"></asp:RequiredFieldValidator>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:TextBox ID="txtEndDate" runat="server" Width="70px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton4"
                                                            TargetControlID="txtEndDate">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtEndDate" />
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Status"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="RadioButtonList1" runat="server" Width="75px">
                                            <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                    <%-- <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                                 <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                               
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                                           </asp:RadioButtonList>--%>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <div style="clear: both;">
                            <label>
                             
                                <asp:Label ID="Label11" runat="server" Text="Apply this restriction to"></asp:Label>
                            </label>
                          
                                <asp:Panel ID="Panel1" runat="server" Width="100%">
                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" Width="100%" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="0">Whole Category</asp:ListItem>
                                        <asp:ListItem Value="1">Whole Sub Category</asp:ListItem>
                                        <asp:ListItem Value="2">Whole Sub Sub Category</asp:ListItem>
                                        <asp:ListItem Value="3">Selected Inventory Items</asp:ListItem>
                                    </asp:RadioButtonList>
                                </asp:Panel>
                          
                        </div>
                        <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="ImgBtnGo" runat="server" Text=" Go " CssClass="btnSubmit" OnClick="ImgBtnGo_Click"
                                        ValidationGroup="gp1" />
                                    <%--<asp:ImageButton ID="ImgBtnGo" runat="server" OnClick="ImgBtnGo_Click" AlternateText="submit"
                                                ImageUrl="~/ShoppingCart/images/go.png" ValidationGroup="gp1" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="panel234" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="grdCat" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowCommand="grdCat_RowCommand"
                                                        Width="100%" EmptyDataText="No Record Found.">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Inventory Category" HeaderStyle-HorizontalAlign="Left"
                                                                Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcName" runat="server" Text='<%# Bind("Cname") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub Cat" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblinvsub" runat="server" Text='<%# Bind("Subname") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub Sub Cat" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsubsub" runat="server" Text='<%# Bind("Subsubname") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Inventory Name" HeaderStyle-HorizontalAlign="Left"
                                                                Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("invname") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Start Date" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Min. Qualifying Amt" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:Label ID="lblminAmt" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Min Qualifying Quantity" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMinQty" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lbl1" runat="server" Text="Apply Restriction"></asp:Label>
                                                                    <asp:CheckBox ID="chkAll" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkAll_chachedChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkApply" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Id" Visible="False" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:Button ID="ImageButton3" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btn_submit_Click"
                                                        ValidationGroup="gp1" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
                <div style="clear: both;">
                </div>
                   <asp:Panel ID="pnlEsc" runat="server" Visible="false" Width="100%">
                    <fieldset>
                 <div style="float: right;">
                        <label>
                            <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" 
                            Text="Printable Version" onclick="Button4_Click"
                               />
                            <input id="Button3" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" class="btnSubmit" visible="False" />
                        </label>
                    </div>
                    <div style="clear: both;">
                    <br />
                    </div>
             <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                      
                                <div id="mydiv" class="closed">
                                    <table width="100%">
                                        <tr align="center">
                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                <asp:Label ID="lblCompany" runat="server" Font-Italic="True" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                <asp:Label ID="Label19" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="True" ForeColor="Black">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                <asp:Label ID="lblgptext" runat="server" Font-Italic="True" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            <div style="clear: both;">
                    </div>
                        <asp:GridView ID="grdCat0123" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true"
                            PageSize="20" OnRowCommand="grdCat0123_RowCommand" DataKeyNames="FreeShippingToInvs"
                            OnRowDeleting="grdCat0123_RowDeleting" Width="100%" OnPageIndexChanging="grdCat0123_PageIndexChanging"
                            EmptyDataText="No Record Found." OnSorting="grdCat0123_Sorting" 
                            AllowSorting="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Rule Name" HeaderStyle-HorizontalAlign="Left"  SortExpression="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRuleName1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country" HeaderStyle-HorizontalAlign="Left"  SortExpression="CountryName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCountry1" runat="server" Text='<%# Bind("CountryName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Left"  SortExpression="StateName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblState1" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="City" HeaderStyle-HorizontalAlign="Left"   SortExpression="CityName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcity" runat="server" Text='<%# Bind("CityName") %>'></asp:Label>
                                    </ItemTemplate>
                                      <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventory" HeaderStyle-HorizontalAlign="Left"  SortExpression="CatSCatSSCatName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvName1" runat="server" Text='<%# Bind("CatSCatSSCatName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start Date" HeaderStyle-HorizontalAlign="Left"  SortExpression="StartDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate1" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Date" HeaderStyle-HorizontalAlign="Left"  SortExpression="EndDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate1" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Min Qty" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="7%" SortExpression="MinQty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMinQty1" runat="server" Text='<%# Bind("MinQty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Min Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" SortExpression="MinAmout">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMinAmt1" runat="server" Text='<%# Bind("MinAmout") %>'></asp:Label>
                                    </ItemTemplate>
                                  
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id" Visible="False" HeaderStyle-HorizontalAlign="Left" SortExpression="FreeShippingToInvs">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEscId" runat="server" Text='<%# Bind("FreeShippingToInvs") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="Btn" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                            OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="3%" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr" />
                            <AlternatingRowStyle CssClass="alt" />
                        </asp:GridView>
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                   </asp:Panel>
                    </fieldset></asp:Panel>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" BorderStyle="Outset"
                    Width="300px">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="lblm" runat="server" Text="Please check the date."></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Start Date of the Year is "></asp:Label>
                                    <asp:Label ID="lblstartdate" runat="server"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="lblm0" runat="server" Text="You can not select any date earlier than that."> </asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="ImageButton2" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="ImageButton2_Click" />
                            </td>
                        </tr>
                    </table>
                    &nbsp;</asp:Panel>
                <asp:Button ID="Button2" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                    ID="ModalPopupExtender1222222" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel5" TargetControlID="Button2">
                </cc1:ModalPopupExtender>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="grdFreeRule" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
