<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="TaxMasterAndMoreInfo_New.aspx.cs" Inherits="Shoppingcart_Admin_TaxMasterAndMoreInfo_New"
    Title="TaxMasterAndMoreInfo" %>

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
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" ></asp:Label>
                    </div>
                    <div style="clear: both;">
                    </div>
                <fieldset>
                   
                    <label>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Option - 1 Set fixed rate/amount of tax for all sales only, regardless of destination of sale. "></asp:Label>
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                </td>
                                <td>
                                    <asp:Button ID="btnback" runat="server" CssClass="btnSubmit" Text="Change Tax Method"
                                        OnClick="btnback_Click" />
                                </td>
                            </tr>
                        </table>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Visible="false"
                            CausesValidation="False">
                            <%--<font color="black"><b>Back to Selection of Tax Method </b></font>--%>
                            <asp:Label ID="Label3" runat="server" Text="Back to Selection of Tax Method"></asp:Label>
                        </asp:LinkButton>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="addinventoryroom" runat="server">
                        <label>
                            <asp:Label ID="Label5" runat="server" Text="Business Name">
                            </asp:Label>
                            <br />
                            <asp:Label ID="lblstorename" runat="server"></asp:Label>
                        </label>
                        <label>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="Label6" runat="server" Text="Tax Name"></asp:Label>
                                    <asp:Label ID="Label8" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RegularExpressionValidator ID="REG14554545" runat="server" ErrorMessage="Invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                        ControlToValidate="tbTaxName" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbTaxName"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tbTaxName" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',50)"
                                        MaxLength="50" AutoPostBack="True" OnTextChanged="tbTaxName_TextChanged"></asp:TextBox>
                                   <asp:Label ID="Label20" runat="server" Text="Max " CssClass="labelcount" ></asp:Label><span id="div1" class="labelcount">50</span>
                                    <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="tbTaxName" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </label>
                        <label>
                            <asp:Label ID="Label10" runat="server" Text="Tax Short Name"></asp:Label>
                            <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                ControlToValidate="txtshortname" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtshortname"
                                ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtshortname" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div12',10)"
                                MaxLength="10" Width="100px"></asp:TextBox>
                             <asp:Label ID="Label14" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> <span id="div12" class="labelcount" >10</span>
                            <asp:Label ID="Label12" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label7" runat="server" Text="Tax"></asp:Label>
                            <asp:Label ID="Label13" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbPer"
                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="True">
                            </asp:RequiredFieldValidator>
                            <asp:Label ID="lblmsgrange" runat="server" ForeColor="Red"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="tbPer"
                                ValidationGroup="1" Display="Dynamic" ValidationExpression="^(-)?\d+(\.\d\d)?$"
                                runat="server" ErrorMessage="Invalid"> </asp:RegularExpressionValidator>
                            <asp:TextBox ID="tbPer" runat="server" MaxLength="10" Width="80px" onKeydown="return mask(event)"  
                            onkeyup="return check(this,/[\\/!@#$%^'_&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9.\s]+$/,'div123',10)" ></asp:TextBox>
                             <asp:Label ID="Label15" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> 
                                 <span id="div123"  class="labelcount">10</span>
                                <asp:Label ID="Label21" runat="server" Text="(0-9 .)"  CssClass="labelcount"></asp:Label>
                            <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                Enabled="True" TargetControlID="tbPer" ValidChars="0147852369.">
                            </cc1:FilteredTextBoxExtender>
                            <%--  Chars Remain 
                                 <span id="Span1">10</span>
                                <asp:Label ID="Label10" runat="server" Text="(0-9)" ></asp:Label>--%>
                        </label>
                        <label>
                            <asp:RadioButtonList ID="rdlist" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="1" Text="%"></asp:ListItem>
                                <asp:ListItem Value="0" Text="$"></asp:ListItem>
                            </asp:RadioButtonList>
                            <%-- <asp:CheckBox ID="chkper" Visible="false" runat="server" Text="is %" />
                            <asp:Label ID="Label8" runat="server" Text="( Only number is possible as input value eg. 5 or 5.5 etc )" Visible="false"></asp:Label>
         <div style="clear: both;">
                        </div> --%>
                        </label>
                      
                        <label>
                            <br />
                            <asp:DropDownList ID="ddlapply" runat="server" Width="220px">
                                <asp:ListItem Value="0">Apply only to online sales</asp:ListItem>
                                <asp:ListItem Value="1" Selected="True">Apply to all sales, both online and retail</asp:ListItem>
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="lblst" runat="server" Text="Status">
                            </asp:Label>
                            <asp:DropDownList ID="ddlstatus" runat="server" Width="75px">
                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                            </asp:DropDownList>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Button ID="ImageButton9" runat="server" CssClass="btnSubmit" Text="Update" OnClick="ImageButton9_Click"
                                Visible="false" ValidationGroup="1" />
                            <asp:Button ID="ImageButton1" CssClass="btnSubmit" runat="server" Text="Submit" OnClick="Button1_Click"
                                ValidationGroup="1" />
                            <asp:Button ID="ImageButton2" CssClass="btnSubmit" runat="server" Text="Cancel" OnClick="ImageButton2_Click" />
                        </label>
                        <div style="clear: both;">
                        </div>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Taxes" Font-Bold="true" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click1" />
                            <input id="Button3" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" class="btnSubmit" visible="False" />
                        </label>
                    </div>
                    <div style="clear: both;">
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
                                                <asp:Label ID="Label9" runat="server" Font-Italic="True" Text="List of Taxes "></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="TaxTypeMasterId" EmptyDataText="No Record Found. " CssClass="mGrid"
                                    GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    HorizontalAlign="Left" Width="100%" OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ids" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmoreinfoid" runat="server" Text='<%#Bind("TaxTypeMasterId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tax Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcountry" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tax Short Name " SortExpression="Taxshortname" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcous" runat="server" Text='<%#Bind("Taxshortname") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Percentage" SortExpression="Percentage" HeaderStyle-HorizontalAlign="Left"
                                            Visible="false" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltaxpers" runat="server" Text='<%#Bind("Percentage") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Left"
                                            Visible="false" ItemStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaxAmt" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tax Rate" SortExpression="TaxRate" HeaderStyle-HorizontalAlign="Left"
                                            Visible="true" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsingamt" runat="server" Visible="false" Text="$"></asp:Label>
                                                <asp:Label ID="lbltaxa" runat="server" Text='<%#Bind("TaxRate") %>'></asp:Label>
                                                <asp:Label ID="lblsignpers" runat="server" Visible="false" Text="%"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apply Only to Online Sales" SortExpression="Applytoonlinesales"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="9%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("Applytoonlinesales") %>'
                                                    Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apply to Both Online And Retail Sales" SortExpression="ApplyAllsalesandretail"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="13%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox3" runat="server" Checked='<%# Bind("ApplyAllsalesandretail") %>'
                                                    Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="4%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllab" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-Width="2%" HeaderImageUrl="~/Account/images/edit.gif"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="edit" runat="server" ToolTip="Edit" CommandArgument='<%# Bind("TaxTypeMasterId") %>'
                                                    OnClick="edit_Click" ImageUrl="~/Account/images/edit.gif" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                            HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="Button1" runat="server" ToolTip="Delete" CommandName="Delete"
                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                </asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA" BorderStyle="Outset"
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
                                    <asp:Button ID="Button1" runat="server" Text="YES" BackColor="#CCCCCC" />
                                    <asp:Button ID="Button2" runat="server" Text="NO" BackColor="#CCCCCC" />
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
