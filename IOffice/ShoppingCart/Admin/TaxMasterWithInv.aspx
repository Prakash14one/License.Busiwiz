<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="TaxMasterWithInv.aspx.cs" Inherits="Shoppingcart_Admin_TaxMasterWithInv"
    Title="TaxMasterWithInv" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        #innertbl
        {
            width: 100%;
        }
    </style>

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
      function SelectAllCheckboxes1(spanChk) 
      {
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

//          var grd = document.getElementById('<%=grdInvMasters0.ClientID %>');
//          var childch = "CheckBox1";
//          var chmain = document.getElementById("chkAll1");
//          var intype = grd.getElementByTagName("input")
//          alert(intype);
//          for (var i = 0; i < intype.length; i++) {
//              alert(i);
//              if (intype[i].type == 'checkbox' && intype[i].id.indexOf(childch, 0) >= 0)
//                  intype[i].checked = chmain.checked;
//            
//             

          //}
      }
      function SelectAllTextbox(spanChk) {
    
        
        
          // Added as ASPX uses SPAN for checkbox
          var oItem = spanChk.children;
          
          var theBox =spanChk.type
          var theBox = (spanChk.type == "text") ?
         spanChk : spanChk.children.item[0];

          
          xState = theBox.value;
        
         
         
          elm = theBox.form.elements;

          for (i = 0; i < elm.length; i++)
              if (elm[i].type == "text" &&
              elm[i].id != theBox.id) {
              //elm[i].click();
                
                      
                
                  elm[i].value = theBox.value;
                 
              //elm[i].checked=xState;
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
    </script>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbloption" Text="Option 3-Set tax rate/amount for different products individually and regionally."></asp:Label>
                        <asp:Button ID="btnback" runat="server" CssClass="btnSubmit" Text="Change Tax Method"
                            OnClick="btnback_Click" />
                    </legend>
                    <div style="clear: both;">
                        <asp:Label ID="Label15" runat="server" Text="Add tax for country/state"></asp:Label>
                    </div>
                    <label>
                        <asp:Label runat="server" ID="businesslbl" Text="Business Name"></asp:Label>
                        <asp:DropDownList ID="ddlWarehouse" runat="server" Enabled="false" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" Width="152px">
                        </asp:DropDownList>
                    </label>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                    <label> 
                        <asp:Label runat="server" ID="Label1" Text="Tax Name"></asp:Label>
                        <asp:Label ID="lbcv" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                        <asp:RequiredFieldValidator ID="rrfd" runat="server" ControlToValidate="txttax" ValidationGroup="1"
                            SetFocusOnError="true" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="Invalid Character"
                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                            ControlToValidate="txttax" ValidationGroup="1">
                        
                        </asp:RegularExpressionValidator>
                        <asp:TextBox ID="txttax" runat="server" Width="130px" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:.;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',30)"
                            MaxLength="30" ontextchanged="txttax_TextChanged" AutoPostBack="True"></asp:TextBox>
                     <asp:Label ID="Label13" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> <span id="div1"  class="labelcount">
                                                            30</span>
                        <asp:Label ID="Label2" runat="server" Text="(A-Z 0-9 _)"  CssClass="labelcount"></asp:Label>
                       
                    </label>
                    <label>
                        <asp:Label ID="Label19" runat="server" Text="Tax Short Name"></asp:Label>
                        <asp:Label ID="Label20" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                            ControlToValidate="txtshortname" ValidationGroup="1"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtshortname"
                            ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtshortname" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div12',10)"
                            MaxLength="10" Width="100px"></asp:TextBox>
                        <asp:Label ID="Label22" runat="server" Text="Max " CssClass="labelcount" ></asp:Label><span id="div12" class="labelcount">
                                                            10</span>
                        <asp:Label ID="Label21" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                    </label>
                     </ContentTemplate>
                     <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txttax" EventName="TextChanged" />
                                                          
                                                        </Triggers>
                    </asp:UpdatePanel>
                    <label>
                        <asp:Label runat="server" ID="lblcountry" Text="Select Country "></asp:Label>
                        <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged1"
                            Width="152px">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label runat="server" ID="lblstate" Text="Select State"></asp:Label>
                        <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                            Width="152px">
                        </asp:DropDownList>
                    </label>
                     <div style="clear: both;">
                    
                    </div>
                      <div style="padding-left: 0%">
                       
                        <asp:Button ID="imgsubmittax" runat="server" CssClass="btnSubmit"  OnClick="imgsubmittax_Click"
                            ValidationGroup="1" Text="Submit" />
                   
                        <asp:Button ID="btncan" runat="server"  Text="Cancel" CssClass="btnSubmit" OnClick="btncan_Click" />
                   </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text=" Back to Selection of Tax Method"
                            Visible="false" OnClick="LinkButton1_Click">
                        </asp:LinkButton>
                    </label>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllist" Text=" List of Taxes"></asp:Label>
                    </legend>
                     <div style="float: right;">
                        <asp:Button ID="Button2" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button2_Click1" />
                        <input id="Button1" runat="server" visible="false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            class="btnSubmit" type="button" value="Print" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label16" runat="server" Text="Filter by Country"></asp:Label>
                        <asp:DropDownList ID="ddlfiltercountary" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfiltercountary_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label17" runat="server" Text="Filter by State"></asp:Label>
                        <asp:DropDownList ID="ddlfilterstate" runat="server">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <br />
                        <asp:Button ID="btngo" runat="server" Text="Go" CssClass="btnSubmit" OnClick="btngo_Click" />
                    </label>
                    <div style="clear: both;">
                    <br />
                    </div>
                   
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <div id="mydiv" class="closed">
                            <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblcompany" runat="server" Font-Size="20px" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblbusiness" runat="server" Font-Size="20px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label14" runat="server" Text="List of Taxes" Font-Size="18px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:GridView ID="gridtaxname" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="Both"
                            PageSize="15" Width="100%" DataKeyNames="Id" EmptyDataText="No Record Found."
                            OnRowCommand="gridtaxname_RowCommand" OnRowEditing="gridtaxname_RowEditing" OnRowDeleting="gridtaxname_RowDeleting"
                            OnSorting="gridtaxname_Sorting" AllowSorting="True">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltid" runat="server" Text='<%#Bind("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country" HeaderStyle-HorizontalAlign="Left" SortExpression="CountryName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcon" runat="server" Text='<%#Bind("CountryName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Left" SortExpression="StateName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstate" runat="server" Width="80px" Text='<%# Bind("StateName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Name" HeaderStyle-HorizontalAlign="Left" SortExpression="Taxname">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltax" runat="server" Text='<%#Bind("Taxname") %>' Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Short Name" HeaderStyle-HorizontalAlign="Left"
                                    SortExpression="Taxshortname">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltaxs" runat="server" Text='<%#Bind("Taxshortname") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <%--<asp:ButtonField ButtonType="Image" HeaderStyle-HorizontalAlign="Left" CommandName="edit"
                                    HeaderText="Edit" HeaderStyle-Width="40px" Text="Edit" ImageUrl="~/Account/images/edit.gif"
                                    HeaderImageUrl="~/Account/images/edit.gif" ItemStyle-Width="2%"></asp:ButtonField>--%>
                                    
                                     <asp:TemplateField HeaderText="Edit" ItemStyle-Width="3%" HeaderImageUrl="~/Account/images/edit.gif"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="edit" ToolTip="Edit" runat="server" CommandArgument='<%# Bind("Id") %>'
                                                    ImageUrl="~/Account/images/edit.gif" CommandName="edit" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                    HeaderStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="llinkbb" runat="server" ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                            OnClientClick="return confirm('Do you wish to delete this record?');"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:ButtonField  HeaderStyle-HorizontalAlign="Left" ButtonType="Link" CommandName="delete" HeaderText="Delete" 
                            HeaderStyle-Width= "40px"  Text="Delete" >
                             <HeaderStyle Width="40px" />
                        </asp:ButtonField>--%>
                                <asp:ButtonField ButtonType="Button" CommandName="setrates"  HeaderStyle-Width="8%" HeaderStyle-HorizontalAlign="Left"
                                    HeaderText="Set Tax Rates" Text="Set Tax Rates" ItemStyle-ForeColor="#416271">
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel3" runat="server" Visible="false" Width="100%">
                    <fieldset>
                        <legend>
                            <asp:Label runat="server" ID="Label3" Text="Set Tax Rates for the Product"></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td style="width: 20%;">
                                    <label>
                                        <asp:Label runat="server" ID="lasdd" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 30%;">
                                    <label>
                                        <asp:Label ID="lblbn" runat="server" CssClass="lblSuggestion"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 20%;">
                                    <label>
                                        <asp:Label runat="server" ID="Label4" Text="Tax Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 30%;">
                                    <label>
                                        <asp:Label ID="lbltname" runat="server" CssClass="lblSuggestion"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    <label>
                                        <asp:Label runat="server" ID="Label5" Text="Country Name"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lblcouname" runat="server" CssClass="lblSuggestion"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label runat="server" ID="Label6" Text="State Name"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lblstatename" runat="server" CssClass="lblSuggestion"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    <label>
                                        <asp:Label runat="server" ID="Label7" Text="Select"></asp:Label>
                                    </label>
                                </td>
                                <td colspan="3">
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="0">By Category</asp:ListItem>
                                        <asp:ListItem Value="1">By Name / Barcode / Product No.</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Panel ID="pnlInvCat" Visible="false" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%;">
                                                    <label>
                                                        <asp:Label runat="server" ID="Label8" Text="Category"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 30%;">
                                                 <label>
                                                    <asp:DropDownList ID="ddlInvCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvCat_SelectedIndexChanged"
                                                        Width="150px">
                                                    </asp:DropDownList>
                                                    </label>
                                                </td>
                                                <td style="width: 20%;">
                                                    <label>
                                                        <asp:Label runat="server" ID="Label9" Text="Sub Category"></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 30%;">
                                                 <label>
                                                    <asp:DropDownList ID="ddlInvSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubCat_SelectedIndexChanged"
                                                        Width="150px">
                                                    </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label runat="server" ID="Label10" Text="Sub Sub Category"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                 <label>
                                                    <asp:DropDownList ID="ddlInvSubSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubSubCat_SelectedIndexChanged"
                                                        Width="150px">
                                                    </asp:DropDownList>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label runat="server" ID="Label11" Text="Inventory Name"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                 <label>
                                                    <asp:DropDownList ID="ddlInvName" runat="server" Width="150px" OnSelectedIndexChanged="ddlInvName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Panel ID="pnlInvName" runat="server" Visible="false" Width="100%">
                                        <table id="lftpnl" align="center" runat="server" width="100%">
                                            <tr>
                                                <td style="width: 20%;">
                                                    <label>
                                                        <asp:Label runat="server" ID="Label12" Text="Name"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="ReredFieldValidator1" runat="server" ControlToValidate="txtSearchInvName"
                                                            Display="Dynamic" ErrorMessage="*" ValidationGroup="f" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatlkor2" runat="server" ErrorMessage="Invalid Character"
                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                                            ControlToValidate="txtSearchInvName" ValidationGroup="f"></asp:RegularExpressionValidator>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:TextBox ID="txtSearchInvName" runat="server" MaxLength="30" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'div2',30)"></asp:TextBox>
                                                        <asp:Label ID="Label37" runat="server" Text="Max" CssClass="labelcount"  ></asp:Label> <span id="div2" class="labelcount">
                                                    30</span>
                                                        <asp:Label ID="Label47" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <div>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdmasterrate" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="True" 
                                            OnSelectedIndexChanged="rdmasterrate_SelectedIndexChanged">
                                            <asp:ListItem Value="1" Text="Set Master Tax Rate for Selected Products"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Set Tax Rates for Individual Products" 
                                                Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <asp:Panel ID="pnltaxex" runat="server" Visible="False">
                                <fieldset>
                                    <legend></legend>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>
                                                    <asp:Label ID="Label18" runat="server" Text="Set Master Tax Rate for Seleted Products "></asp:Label>
                                                </label>
                                            </td>
                                            <td width="90px">
                                                <label>
                                                    <asp:TextBox ID="txttrate" runat="server" Text="0" Width="60px" MaxLength="8"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txttrate"
                                                        ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"
                                                        Display="Dynamic"> 
                                                    </asp:RegularExpressionValidator>
                                                    <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" TargetControlID="txttrate" ValidChars="0147852369.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" ControlToValidate="txttrate"
                                                        Display="Dynamic" ErrorMessage="*" ValidationGroup="f" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:Label ID="lblmsgrange" runat="server" ForeColor="Red"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdamtapp" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Value="1" Text="%"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="$"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:CheckBox ID="chkonline" runat="server" Text="Apply this to Online Sales Only" />
                                                <asp:CheckBox ID="chkboth" runat="server" Text="Apply this to Both Online and Retails Sales" />
                                                <%-- <asp:RadioButtonList ID="rsapp" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="1" Text="Apply this to Online Sales Only"></asp:ListItem>
                                 <asp:ListItem  Value="2" Text="Apply this to Both Online and Retails Sales"></asp:ListItem>
                                </asp:RadioButtonList>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset></asp:Panel>
                        </div>
                        <div align="center">
                            <asp:Button ID="btnSearchGo" runat="server" Visible="false" OnClick="btnSearchGo_Click"
                                Text=" Go " CssClass="btnSubmit" ValidationGroup="f" />
                            <asp:Button ID="ImgBtnSearchGo" CssClass="btnSubmit" Text=" Go " runat="server" OnClick="ImgBtnSearchGo_Click"
                                ValidationGroup="f" Visible="false" />
                                <asp:Button ID="btngcanc" CssClass="btnSubmit" Text="Cancel" runat="server" onclick="btngcanc_Click" 
                               />
                        </div>
                        <div style="clear: both;">
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Visible="False" CssClass="panelpro"
                                    Width="100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdInvMasters0" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="Both"
                                                    PageSize="15" OnRowCommand="grdInvMasters_RowCommand" Width="100%" 
                                                    EmptyDataText="No Record Found." AllowSorting="True" 
                                                    onsorting="grdInvMasters0_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Product No." SortExpression="ProductNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductNo0" runat="server" Text='<%#Bind("ProductNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                            <ItemStyle VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category" SortExpression="CateAndName" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="22%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCatego0" runat="server" Text='<%#Bind("CateAndName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="22%" />
                                                            <ItemStyle VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inventory Name" HeaderStyle-HorizontalAlign="Left"  SortExpression="Name" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvNa0" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                                            <ItemStyle VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false" HeaderText="Tax Name" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtname" runat="server" Text='<%# Bind("TaxtName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                            <ItemStyle VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left"  HeaderText="Tax Rate" SortExpression="Tax" HeaderStyle-Width="10%">
                                                           
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtTaxinper" runat="server" Width="60px" Text='<%# Bind("Tax") %>'
                                                                    MaxLength="8"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtTaxinper"
                                                                    ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"
                                                                    Display="Dynamic"> 
                                                                </asp:RegularExpressionValidator>
                                                                <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                                                    Enabled="True" TargetControlID="txtTaxinper" ValidChars="0147852369.">
                                                                </cc1:FilteredTextBoxExtender>
                                                                <asp:Label ID="lblgtrate" runat="server" ForeColor="Red"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            <ItemStyle VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Tax Rate Is" SortExpression="taxper" HeaderStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rdamtappgd" SelectedValue='<%#Bind("taxper") %>' runat="server"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Value="1" Text="%"></asp:ListItem>
                                                                    <asp:ListItem Value="0" Text="$"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Taxable") %>' />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <%--   <input id="chkAll1" onclick="javascript:SelectAllCheckboxes1(this);" 
                                                            type="checkbox" /> --%>
                                                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_chachedChanged" /><asp:Label
                                                                    ID="lblInvN0" runat="server" Text="Is(%)"></asp:Label>
                                                            </HeaderTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="ApplyToAllSales" HeaderStyle-Width="12%" >
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chec" runat="server" Checked='<%# Bind("ApplyToAllSales") %>' />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <%-- <input id="chkAll11" onclick="javascript:SelectAllCheckboxes1(this);" runat="server"
                                                            type="checkbox" />--%>
                                                                <asp:Label ID="lbluIn" runat="server" Text="Rate in Effect for Online and Retail Sales"></asp:Label>
                                                                <br />
                                                                <asp:CheckBox ID="chkAll1" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll1_chachedChanged" />
                                                            </HeaderTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="ApplyToallOnlineSales" Visible="true" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckB" runat="server" Checked='<%# Bind("ApplyToallOnlineSales") %>' />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <%-- <input id="ch" onclick="javascript:SelectAllCheckboxes1(this);" runat="server"
                                                            type="checkbox" />--%>
                                                                <asp:Label ID="lblIn" runat="server" Text="Rate in Effect for Online Sales" Visible="true"></asp:Label>
                                                                <br />
                                                                <asp:CheckBox ID="chkAll2" runat="server" AutoPostBack="true" Visible="true" OnCheckedChanged="chkAll2_chachedChanged" />
                                                            </HeaderTemplate>
                                                           
                                                            <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                                           
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" SortExpression="Active">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chtstasus1" runat="server" Checked='<%# Bind("Active") %>' />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <%--   <input id="chest" onclick="javascript:SelectAllCheckboxes1(this);" runat="server"
                                                            type="checkbox" />--%><asp:Label ID="lblInvNa" runat="server" Text="Status"></asp:Label>
                                                                <asp:CheckBox ID="chkAll3" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll3_chachedChanged" />
                                                            </HeaderTemplate>
                                                            <HeaderStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <%-- <asp:ButtonField CommandName="Select1" Text="Select" Visible="false" />--%>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="InvwhMasterId"
                                                            Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvWHMid" runat="server" Text='<%#Bind("InventoryWarehouseMasterId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="taxableid" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltaxabilityid" runat="server" Text='<%#Bind("InvTaxabilityId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSearchGo" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <div align="center">
                            <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click"
                                Visible="false" ValidationGroup="2" CssClass="btnSubmit" />
                        </div>
                    </fieldset></asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
