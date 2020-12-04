<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="Fixedtaxdependingonstate.aspx.cs" Inherits="ShoppingCart_Admin_Fixedtaxdependingonstate"
    Title="Untitled Page" %>

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
         function mask(evt)
        { 

           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if( evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
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
            <div style="float: left;padding:1%;">
                        <asp:Label ID="Label1" runat="server" BorderColor="Red" ForeColor="Red" Visible="False"></asp:Label>
                    </div>
                    <div style="clear: both;">
                    </div>
                <fieldset>
                    
                    <label>
                    <table>
                    <tr>
                    <td>
                     <asp:Label ID="Label4" runat="server" Text=" Option 2-Set fixed rate/amount of tax for all sales  based on destination
                                        (state/province) of sale. "></asp:Label>
                                          <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
         
                    </td>
                    <td>
                    
                                               <asp:Button ID="btnback" runat="server" CssClass="btnSubmit" 
                            Text="Change Tax Method" onclick="btnback_Click" />
                    </td>
                    </tr>
                    </table>
                       
                            </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" OnClick="LinkButton1_Click" CausesValidation="False">
                            <asp:Label ID="Label3" runat="server" Text="Back to Selection of Tax Method"></asp:Label>
                        </asp:LinkButton>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="addinventoryroom" runat="server">
                        <label>
                            <asp:Label ID="Label51221" runat="server" Text="Business Name">
                            </asp:Label>
                            <br />
                            <asp:Label ID="storename"  runat="server"></asp:Label>
                        </label>
                        <label>
                           <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                            <asp:Label ID="Label5" runat="server" Text="Tax Name"></asp:Label>
                          <asp:Label ID="Label13" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbTaxName"
                                ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG14554545" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                ControlToValidate="tbTaxName" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="tbTaxName" runat="server" onKeydown="return mask(event)"  
                            onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',50)" 
                            MaxLength="50" ValidationGroup="1" AutoPostBack="True" 
                            ontextchanged="tbTaxName_TextChanged"></asp:TextBox>                            
                                   <asp:Label ID="Label37" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                 <span id="div1" class="labelcount" >50</span>
                            <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount" ></asp:Label>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="tbTaxName" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </label>
                            <label>
                            <asp:Label ID="Label15" runat="server" Text="Tax Short Name"></asp:Label>
                            <asp:Label ID="Label16" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character" Display="Dynamic"
                                SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtshortname"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtshortname"
                                ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtshortname" runat="server" onKeydown="return mask(event)"  
                            onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div12',10)" 
                            MaxLength="10" Width="100px"></asp:TextBox>                                                      
                                <asp:Label ID="Label18" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                 <span id="div12"  class="labelcount">10</span>
                                <asp:Label ID="Label17" runat="server" Text="(A-Z 0-9 _)"  CssClass="labelcount"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label7" runat="server" Text="Tax"></asp:Label>
                                        <asp:Label ID="Label14" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtAmt"
                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" 
                            ValidationGroup="1" ErrorMessage="Invalid" Display="Dynamic"></asp:RegularExpressionValidator>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" ControlToValidate="txtAmt"
                                ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtAmt" runat="server" MaxLength="10" Width="80px" 
                           onKeydown="return mask(event)"  
                            onkeyup="return check(this,/[\\/!@#$%^'_&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9.\s]+$/,'div123',10)"  ></asp:TextBox>
                            
                              <asp:Label ID="Label20" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                 <span id="div123"  class="labelcount">10</span>
                                <asp:Label ID="Label21" runat="server" Text="(0-9 .)"  CssClass="labelcount"></asp:Label>
                            <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                Enabled="True" TargetControlID="txtAmt" ValidChars="0147852369.">
                            </cc1:FilteredTextBoxExtender>                                                    
                            <%--   Chars Rem 
                                 <span id="Span1">10</span>
                                 <asp:Label ID="Label14" runat="server" Text="(0-9)"></asp:Label>--%>
                        </label>
                       
                        <label>
                       
                       <asp:RadioButtonList ID="rdlist" runat="server" RepeatDirection="Horizontal" >
                       <asp:ListItem Selected="True" Value="1" Text="%"></asp:ListItem>
                       <asp:ListItem  Value="0" Text="$"></asp:ListItem>
                       </asp:RadioButtonList>
                          <%--  <asp:CheckBox ID="CheckBox2" runat="server" Text="is %" TextAlign="Right" />--%>
                        </label>
                          <label>
                         <br />
                        <asp:DropDownList ID="ddlapply" runat="server" Width="222px">
                         <asp:ListItem Value="0" >Apply only to online sales</asp:ListItem>
                         <asp:ListItem Value="1" Selected="True">Apply to all sales, both online and retail</asp:ListItem>
                         
                        </asp:DropDownList>
                          <%--  <asp:Label ID="Label8" runat="server" Text="( Only number is possible as input value eg. 5 or 5.5 etc )"></asp:Label>
           --%>       
                        </label>
                       
                        <div style="clear: both;">
                          <asp:Label ID="Label8" runat="server" Text="Select the country and state that you wish to apply this rate to"></asp:Label>
                        </div>
                        <label>
                            <asp:Label ID="Label9" runat="server" Text="Country"></asp:Label>
                            <asp:DropDownList ID="ddlselectcountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlselectcountry_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label10" runat="server" Text="State"></asp:Label>
                            <asp:DropDownList ID="ddlstate" runat="server" >
                            </asp:DropDownList>
                        </label>
                       
                         <label>
                            <asp:Label ID="lblst" runat="server" Text="Status">
                            </asp:Label>
                             <asp:DropDownList ID="ddlstatus" runat="server">
                         <asp:ListItem Value="1" Selected="True" >Active</asp:ListItem>
                         <asp:ListItem Value="0" >Inactive</asp:ListItem>
                         
                        </asp:DropDownList>
                        </label>
                        <div style="clear: both;">
                        <br />
                        </div>
                      
                       
                            <asp:Button ID="Button3" runat="server" OnClick="imgbtnAdd_Click" CssClass="btnSubmit"
                                Text="Submit" ValidationGroup="1" />
                           
                            <asp:Button ID="ImageButton9" runat="server" CssClass="btnSubmit" 
                            OnClick="ImageButton9_Click" Text="Update" ValidationGroup="1" 
                            Visible="false" />
                       
                        <asp:Button ID="Button5" runat="server" CssClass="btnSubmit" OnClick="Button2_Click"
                                Text="Cancel" />
                       
                        <div style="clear: both;">
                        </div>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of sales taxes for selected country and state" Font-Bold="true" runat="server"></asp:Label>
                    </legend>
                      <div style="float: right;">
                        
                            <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click1" />
                            <input id="Button1" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" class="btnSubmit" visible="False" />
                       
                    </div>
                    <div style="clear: both;">
                    </div>
                     <label>
                            <asp:Label ID="Label6" runat="server" Text="Filter by Country"></asp:Label>
                            <asp:DropDownList ID="ddlfiltercountary" runat="server" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlfiltercountary_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label12" runat="server" Text="Filter by State"></asp:Label>
                            <asp:DropDownList ID="ddlfilterstate" runat="server" >
                     
                            </asp:DropDownList>
                        </label>
                        <label>
                        <br />
                         <asp:Button ID="btngo" runat="server" Text="Go" CssClass="btnSubmit" onclick="btngo_Click"
                               />
                        </label>
                        <div style="clear: both;">
                        <br />
                        </div>
                  
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <tr align="center">
                            <td>
                                <div id="mydiv" class="closed">
                                    <table width="100%">
                                        <tr align="center">
                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                <asp:Label ID="lblCompany" runat="server" Font-Italic="True" Visible="false" ></asp:Label>
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
                                                <asp:Label ID="Label11" runat="server" Font-Italic="True" Text="List of sales taxes for selected country and state"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="Id" EmptyDataText="No Record Found. " OnPageIndexChanging="GridView1_PageIndexChanging"
                                     OnRowCommand="GridView1_RowCommand"
                                    CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" 
                                    OnSorting="GridView1_Sorting" Width="100%" HeaderStyle-HorizontalAlign="Left">
                                    <Columns>
                                       
                                        <asp:TemplateField HeaderText="Country" SortExpression="CountryName" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcountry" runat="server" Text='<%#Bind("CountryName") %>'></asp:Label>
                                            </ItemTemplate>
                                          
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State" SortExpression="StateName" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstate" runat="server" Text='<%#Bind("StateName") %>'></asp:Label>
                                            </ItemTemplate>
                                          
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Tax Name" Visible="true" SortExpression="TaxName" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="18%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltaxmasterid" runat="server" Text='<%#Bind("TaxName") %>'></asp:Label>
                                            </ItemTemplate>
                                          
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Tax Short Name" Visible="true" SortExpression="Taxshortname" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltaxsterid" runat="server" Text='<%#Bind("Taxshortname") %>'></asp:Label>
                                            </ItemTemplate>
                                          
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tax Rate" SortExpression="TaxAmt" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                             <asp:Label ID="lblsingamt" runat="server" Visible="false" Text="$"></asp:Label>
                                                <asp:Label ID="lblTaxAmt" runat="server" Text='<%#Bind("TaxAmt") %>'></asp:Label>
                                                   <asp:Label ID="lblsignpers" runat="server" Visible="false" Text="%"></asp:Label>
                          
                                            </ItemTemplate>
                                           
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is % ?" SortExpression="TaxAmtInPerc" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("TaxAmtInPerc") %>'
                                                    Enabled="false" />
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apply to<br/>Online Sales" SortExpression="ApplyToallOnlineSales" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox3" runat="server" Checked='<%# Bind("ApplyToallOnlineSales") %>'
                                                    Enabled="false" />
                                            </ItemTemplate>
                                           
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apply to Online<br/>And Retail Sales" SortExpression="ApplyToAllSales"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox4" runat="server" Checked='<%# Bind("ApplyToAllSales") %>'
                                                    Enabled="false" />
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>
                                      
                                          <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllab" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Edit" ItemStyle-Width="3%" HeaderImageUrl="~/Account/images/edit.gif"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="edit" ToolTip="Edit" runat="server" CommandArgument='<%# Bind("Id") %>'
                                                    OnClick="edit_Click" ImageUrl="~/Account/images/edit.gif" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:CommandField ShowDeleteButton="True" HeaderText="Delete" />--%>
                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                            HeaderStyle-Width="3%" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="Button1" runat="server" ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                    OnClientClick="return confirm('Do you wish to delete this record?');"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Outset"
                        Width="300px">
                        <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="subinnertblfc">
                                    Confirm Delete
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" ForeColor="Black">You Sure , You Want to 
                                    Delete !</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="ImageButton5" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/Yes.png"
                                        OnClick="yes_Click" />
                                    <asp:Button ID="ImageButton6" runat="server" AlternateText="cancel" ImageUrl="~/ShoppingCart/images/No.png"
                                        OnClick="ImageButton6_Click" />
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
