<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="Product_BatchMaster.aspx.cs" Inherits="AddProduct" Title="Product Batch Master" %>

<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>

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
        function OpenNewWin(url) {
            //  alert(url);
            var x = window.open(url, 'MynewWin', 'width=1000, height=1000,toolbar=2');
            x.focus();
        }
    </script>

    <div style="float: left;">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div style="clear: both;">
    </div>
    <fieldset>
        <legend>
            <asp:Label ID="Label19" runat="server" Text="Product Batch Add / Manage "></asp:Label>
        </legend>
        <div style="float: right;">
            <asp:Button ID="addnewpanel" runat="server" Text="Product Batch Add / Manage " 
                CssClass="btnSubmit" onclick="addnewpanel_Click" />
        </div>
        <div style="clear: both;">
        </div>
        <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
            <table width="100%">
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                                       
                                    <td style="width: 20%">
                                        <label>
                                            <asp:Label ID="Label8" runat="server" Text="Product Model: "></asp:Label>
                                        </label>
                                    </td>
                                    <td colspan="2">
                                        <label>
                                            <asp:DropDownList ID="ddlModel" runat="server" Width="190px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                           
                      
                  
                </tr>
                <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label2" runat="server" Text="Supplier Name: "></asp:Label>
                        </label>                    
                    </td>
                    <td colspan="2">
                          <asp:DropDownList ID="ddlsupplier" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                    <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label3" runat="server" Text="Product Component: "></asp:Label>
                        </label>                    
                    </td>
                    <td colspan="2">
                          <asp:DropDownList ID="ddlcomponent" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>

                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label9" runat="server" Text="Batch Name: "></asp:Label>
                            <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbanchname"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Inavalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z/.0-9_\s]*)"
                                ControlToValidate="txtbanchname" ValidationGroup="1"></asp:RegularExpressionValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtbanchname" runat="server" Width="190px" MaxLength="50" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z/.0-9_\s]+$/,'div1',50)"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="max1" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="div1" cssclass="labelcount">50</span>
                            <asp:Label ID="Label25" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                        </label>
                    </td>
                </tr>
                  <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label4" runat="server" Text="Batch No: "></asp:Label>
                            <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtbanchno"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Inavalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z/.0-9_\s]*)"
                                ControlToValidate="txtbanchno" ValidationGroup="1"></asp:RegularExpressionValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtbanchno" runat="server" Width="190px" MaxLength="50" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z/.0-9_\s]+$/,'Span1',50)"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="Label6" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span1" cssclass="labelcount">50</span>
                            <asp:Label ID="Label7" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                        </label>
                    </td>
                </tr>

                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label17" runat="server" Text="Purchase Invoice No: "></asp:Label>
                            <asp:Label ID="Label18" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Inavalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z/.0-9_\s]*)"
                                ControlToValidate="txtinvoiceno" ValidationGroup="1"></asp:RegularExpressionValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtinvoiceno" runat="server" Width="190px" MaxLength="50" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z/.0-9_\s]+$/,'Span3',50)"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="Label20" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span3" cssclass="labelcount">50</span>
                            <asp:Label ID="Label27" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                        </label>
                    </td>
                </tr>

                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label34" runat="server" Text="Quantity Purchased: "></asp:Label>
                            <asp:Label ID="Label35" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtqtypurchased"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                              <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtqtypurchased" ValidChars="012.3456789">
                                        </cc1:FilteredTextBoxExtender>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtqtypurchased" runat="server" Width="190px" MaxLength="20" 
                                onkeyup="return check('Span4',20,this)"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtqtypurchased" ValidChars="012.3456789">
                                        </cc1:FilteredTextBoxExtender>
                        </label>
                        <label>
                            <asp:Label ID="Label36" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span4" cssclass="labelcount">20</span>
                            <asp:Label ID="Label37" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                        </label>
                    </td>
                </tr>

                 <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label38" runat="server" Text="Markup Percentage Over Effective Cost: "></asp:Label>
                            <asp:Label ID="Label39" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtmarkupper"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtmarkupper" ValidChars="012.3456789">
                                        </cc1:FilteredTextBoxExtender>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtmarkupper" runat="server" Width="190px" MaxLength="20" 
                                onkeyup="return check('Span5',20,this)"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="Label41" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span5" cssclass="labelcount">20</span>
                            <asp:Label ID="Label42" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                        </label>
                    </td>
                </tr>
                               

                   <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label47" runat="server" Text="Total Cost: "></asp:Label>
                            <asp:Label ID="Label48" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txttotalcost"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txttotalcost" ValidChars="012.3456789">
                                        </cc1:FilteredTextBoxExtender>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txttotalcost" runat="server" Width="190px" MaxLength="20" 
                                onkeyup="return check('Span8',20,this)"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="Label49" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span8" cssclass="labelcount">20</span>
                            <asp:Label ID="Label50" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                        </label>
                    </td>
                </tr>
                    
                     <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label11" runat="server" Text="Total Cost Per Unit: "></asp:Label>
                            <asp:Label ID="Label12" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txttotalcost"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txttotalcostperunit" ValidChars="012.3456789">
                                        </cc1:FilteredTextBoxExtender>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txttotalcostperunit" runat="server" Width="190px" MaxLength="20" 
                                onkeyup="return check('Span2',20,this)"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="Label15" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span2" cssclass="labelcount">20</span>
                            <asp:Label ID="Label16" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                        </label>
                    </td>
                </tr>

                <tr>
                 
                                    <td style="width: 20%" valign="top"> 
                                        <label>
                                            <asp:Label ID="Label14" runat="server" Text="Description: "></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <cc2:HtmlEditor ID="txtdescription" runat="server"></cc2:HtmlEditor>
                                    </td>
                               
                </tr>

                <tr>
                    <td style="width: 20%" valign="top">
                        <label>
                            <asp:Label ID="Label10" runat="server" Text="Specification: "></asp:Label>
                            <asp:Label ID="Label21" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                           
                        </label>
                    </td>
                    <td colspan="2">
                        <label tyle="width:500px">
                           <cc2:HtmlEditor ID="txtspecification" runat="server"></cc2:HtmlEditor>
                        </label>
                       
                    </td>
                    
                </tr>              
                      <%----------%>

                       <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label26" runat="server" Text="Volume Discount Min. Qty.: "></asp:Label>
                            <asp:Label ID="Label28" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSalePrice"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                             
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtSalePrice" runat="server" Width="190px" MaxLength="20" 
                                onkeyup="return check('Span4',20,this)"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtSalePrice" ValidChars="012.3456789">
                                </cc1:FilteredTextBoxExtender>
                        </label>
                        <label>
                            <asp:Label ID="Label29" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span6" cssclass="labelcount">20</span>
                            <asp:Label ID="Label31" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                        </label>
                    </td>
                </tr>

                 <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label32" runat="server" Text="Volume Discount Amt. Per Unit: "></asp:Label>
                            <asp:Label ID="Label33" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCurrency" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtCurrency" ValidChars="012.3456789">
                                </cc1:FilteredTextBoxExtender>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtCurrency" runat="server" Width="190px" MaxLength="20" onkeyup="return check('Span5',20,this)"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="Label43" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span7" cssclass="labelcount">20</span>
                            <asp:Label ID="Label44" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                        </label>
                    </td>
                </tr>

               



                     <%----------%>
                      <%-----------------------%>
                         
                <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label54" runat="server" Text="Sale Price: "></asp:Label>
                            <asp:Label ID="Label55" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtSalePrice"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                             
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="TextBox1" runat="server" Width="190px" MaxLength="20" 
                                onkeyup="return check('Span4',20,this)"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtSalePrice" ValidChars="012.3456789">
                                </cc1:FilteredTextBoxExtender>
                        </label>
                        <label>
                            <asp:Label ID="Label56" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span9" cssclass="labelcount">20</span>
                            <asp:Label ID="Label57" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                        </label>
                    </td>
                </tr>

                 <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label58" runat="server" Text="Currency: "></asp:Label>
                            <asp:Label ID="Label59" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtCurrency" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                               
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="TextBox2" runat="server" Width="190px" MaxLength="20" ></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="Label60" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span10" cssclass="labelcount">20</span>
                            <asp:Label ID="Label61" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                        </label>
                    </td>
                </tr>
                               
  <tr>
                    <td style="width:20%">
                        <label>
                            <asp:Label ID="Label45" runat="server" Text="Start Date: "></asp:Label>
                            <asp:Label ID="Label46" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtStartdate"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label style="width:171px;">
                            <asp:TextBox ID="txtStartdate" runat="server" Width="171px"></asp:TextBox>
                        </label>
                        <label style="width:40px">
                            <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                        </label>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartdate"
                            PopupButtonID="imgbtnEndDate">
                        </cc1:CalendarExtender>
                        
                    </td>                   
                </tr>
                              <tr>
                    <td style="width:20%">
                        <label>
                            <asp:Label ID="Label52" runat="server" Text="End Date: "></asp:Label>
                            <asp:Label ID="Label53" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEndDate"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label style="width:171px;">
                            <asp:TextBox ID="txtEndDate" runat="server" Width="171px"  AutoPostBack="True" OnTextChanged="txtEndDate_TextChanged"></asp:TextBox>
                        </label>
                        <label style="width:40px">
                            <asp:ImageButton ID="imgbtnCalEnddate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                        </label>
                        <label>
                             <asp:Label ID="lblenddateerror" runat="server" Text="" 
                            style="color: #FF3300" ></asp:Label>
                        </label>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" PopupButtonID="imgbtnCalEnddate">
                        </cc1:CalendarExtender>
                        
                    </td>
                   
                </tr>    

                 
                                  

                      <%----------------------%>
                                                    
                  <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label13" runat="server" Text="Active: "></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:CheckBox ID="chkboxActiveDeactive" runat="server" Text="Active" />
                        </label>
                    </td>
                   
                </tr>
                
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                            ValidationGroup="1" CssClass="btnSubmit" />
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="Button1_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </fieldset>
    <fieldset>
        <legend>
            <asp:Label ID="Label30" runat="server" Text="List of Products Batch"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="3" align="right">
                    <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version"
                      Visible="false"  OnClick="Button1_Click1" />
                    <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                        style="width: 51px;" type="button" value="Print" visible="false" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                   
                    <label style="width:120px">
                        <asp:Label ID="Label40" runat="server" Text="Product Model: "></asp:Label>
                    </label>
                    <label  style="width:180px">
                    <asp:DropDownList ID="ddlproductmodelfilter" DataTextField="ProductName" DataValueField="ProductId"
                                                runat="server" 
                                                Width="180px">
                                            </asp:DropDownList>
                    </label> 
                     <label style="width:120px">
                        <asp:Label ID="Label22" runat="server" Text="Supplier Name: "></asp:Label>
                    </label>
                    <label style="width:180px">
                                   <asp:DropDownList ID="ddlsupplierfilter" runat="server" Width="180px">
                                            </asp:DropDownList>                    
                    </label> 
                    <label style="width:90px">
                        <asp:Label ID="Label23" runat="server" Text="Component: "></asp:Label>
                    </label>
                    <label>
                                   <asp:DropDownList ID="ddlcomponentfilter" runat="server" Width="190px">
                                            </asp:DropDownList>
                    
                    </label> 
                    
                </td>
            </tr>
            <tr>
            <td colspan="3">
             <label  style="width:120px">
                        <asp:Label ID="Label1" runat="server" Text="Status: "></asp:Label>
                    </label>
                    <label  style="width:180px">
                        <asp:DropDownList ID="ddlstatus" runat="server"  Width="180px">
                           
                            <asp:ListItem Text="All" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Active"></asp:ListItem>
                            <asp:ListItem Text="Deactive"></asp:ListItem>
                        </asp:DropDownList>
                    </label>
                    <label>
                    <asp:Button ID="BtnGo" CssClass="btnSubmit" runat="server" Text="Go" OnClick="BtnGo_Click"  />
                    </label> 
            </td>
            </tr>
           
            <tr>
                <td colspan="3">
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <%--<asp:UpdatePanel ID="UpdatePanelSalesOrder" runat="server">
                                            <ContentTemplate>--%>
                        <table width="100%">
                            <tr>
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="850Px">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <%--    <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="Label67" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>--%>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Products"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                
	
	
	
                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="ProductBAtchMasterID" AutoGenerateColumns="False"
                                        OnRowCommand="GridView1_RowCommand" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                      OnRowDeleting="GridView1_RowDeleting"    AlternatingRowStyle-CssClass="alt" AllowSorting="True" Width="100%" OnSorting="GridView1_Sorting"
                                       PageSize="15" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:BoundField DataField="BatchName" HeaderStyle-HorizontalAlign="Left" HeaderText="Batch Name" SortExpression="BatchName" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="BatchNo" HeaderStyle-HorizontalAlign="Left" HeaderText="Batch No" ItemStyle-Width="6%" SortExpression="BatchNo">
                                                <ItemStyle Width="8%" />
                                            </asp:BoundField>
                                         
                                         
                                             <asp:BoundField DataField="PurchaseInvoiceNo" HeaderStyle-HorizontalAlign="Left" HeaderText="Purchase Inv. No."  SortExpression="PurchaseInvoiceNo" ItemStyle-Width="5%">
                                                <ItemStyle Width="8%" />
                                            </asp:BoundField>
                                           
                                            <asp:BoundField DataField="QtyPurchased" HeaderStyle-HorizontalAlign="Left" HeaderText="Qty Purchased" ItemStyle-Width="6%" SortExpression="Qty Purchased">
                                                <ItemStyle Width="6%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MarkUpPercentageOverEffectiveCost" HeaderStyle-HorizontalAlign="Left" HeaderText="MarkUp Per. Over Effective Cost"  ItemStyle-Width="6%"  SortExpression="MarkUpPercentageOverEffectiveCost">
                                                <ItemStyle Width="6%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalCost" HeaderStyle-HorizontalAlign="Left" HeaderText="Total Cost"  ItemStyle-Width="6%"  SortExpression="TotalCost">
                                                <ItemStyle Width="6%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BatchDescsort" HeaderStyle-HorizontalAlign="Left" HeaderText="Batch Description"    SortExpression="BatchDescsort">
                                                <ItemStyle />
                                            </asp:BoundField>
                                            
                                                 <asp:BoundField DataField="Active" SortExpression="Active" HeaderStyle-HorizontalAlign="Left" HeaderText="Active" ItemStyle-Width="2%">
                                                <ItemStyle Width="2%" />
                                            </asp:BoundField>
                                            <asp:ButtonField ButtonType="Image" HeaderStyle-HorizontalAlign="Left"  CommandName="edit1" HeaderText="Edit" ImageUrl="~/Account/images/edit.gif" Text="Button" HeaderImageUrl="~/Account/images/edit.gif" >
                                             <ItemStyle Width="3%" />
                                            </asp:ButtonField>
                                             <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"  OnClientClick="return confirm('Do you wish to delete this record?');" CommandArgument='<%# Eval("ProductBAtchMasterID") %>' ToolTip="Delete"></asp:ImageButton>
                                                </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
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
                </td>
            </tr>
             
        </table>
    </fieldset>
    <br />
    <br />
    <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
    <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
    <input runat="server" id="hdnProductDetailId" type="hidden" name="hdnProductDetailId"
        style="width: 3px" />
    <input runat="server" id="hdnProductId" type="hidden" name="hdnProductId" style="width: 3px" />
</asp:Content>
