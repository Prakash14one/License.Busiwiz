<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="CanadaFaderalTax.aspx.cs" Inherits="ShoppingCart_Admin_CanadaFaderalTD" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <style type="text/css">
        .open
        {
	        display:block;
        }
        .closed
        {
	        display:none;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
         <div class="products_box">
         <fieldset>
        <table id="tbl1132">
       
        <tr>
        <td colspan="2">
        </td>
        </tr>
         <tr>
        <td colspan="2">
            
            <asp:Panel ID="Panel2" runat="server">
            <fieldset>                        
            <table id="td123" style="width:100%">
            <tr>
                <td colspan="4" align="center">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td align="right">
                    <label>
                    <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                    </label>
                </td>
                <td>
                    <label>
                    <asp:DropDownList ID="ddlstore" runat="server" 
                        AutoPostBack="True" onselectedindexchanged="ddlstore_SelectedIndexChanged">
                    </asp:DropDownList>
                    </label>
                </td>
                <td style="width:25%" >
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td align="right" >
                    <label>
                    <asp:Label ID="Label2" runat="server" Text="Employee Name"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="ddlemployee" ErrorMessage="*" SetFocusOnError="True"  InitialValue="0"
                        ValidationGroup="1"></asp:RequiredFieldValidator>
                    </label>
                    </td>
                <td>
                    <label>
                    <asp:DropDownList ID="ddlemployee" runat="server"  AutoPostBack="True" 
                        onselectedindexchanged="ddlemployee_SelectedIndexChanged">
                    </asp:DropDownList>
                    </label>
                </td>
                <td style="width:25%" >
                    
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td align="right" >
                    <label>
                    <asp:Label ID="Label3" runat="server" Text="Tax Year"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue="0"
                        ControlToValidate="ddltaxyear" ErrorMessage="*" SetFocusOnError="True" 
                        ValidationGroup="1"></asp:RequiredFieldValidator>
                    </label>
                    </td>
                <td >
                    <label>
                    <asp:DropDownList ID="ddltaxyear" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddltaxyear_SelectedIndexChanged">
                    </asp:DropDownList>
                    </label>
                </td>
                <td style="width:25%" >
                    
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="right" >
                    <label>
                        <asp:Label ID="Label4" runat="server" Text="Payroll Deduction Master"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"  InitialValue="0"
                        ControlToValidate="ddpayrolldeduction" ErrorMessage="*" SetFocusOnError="True" 
                        ValidationGroup="1"></asp:RequiredFieldValidator>
                    </label>
                    </td>                
                <td colspan="2">
                    <label>
                    <asp:DropDownList ID="ddpayrolldeduction" runat="server" Width="400px">
                    </asp:DropDownList>
                    </label>
                </td>
               
            </tr>
            <tr>
                <td>
                </td>
                <td >
                </td>
                <td >
                </td>
                <td style="width:25%" >
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td >
                </td>
                <td >
                </td>
                <td style="width:25%" >
                </td>
            </tr>
            </table>
            </fieldset></asp:Panel>
        </td>
        </tr>
     
        <tr>
        <td colspan="2" style="width:100%">
        <asp:Panel ID="Panel4" runat="server" Width="100%" >
        <table id="pnltbl4" style="width:100%">
        
        <tr align="center" style="font-weight:bold">
            <td width="20%">
                <label>
                    <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Canada Revenue Agency"></asp:Label>
                </label>
              </td>
            <td align="center">
                <label>
                    <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Agence du revenu du Canada"></asp:Label>
                </label>
               
                </td>
            <td></td>
            <td></td>
        </tr>
        
        <tr>
            <td colspan="4" style="width:100%" align="center">
                <asp:Label ID="Label7" runat="server" Text="REQUEST TO REDUCE TAX DEDUCTIONS AT SOURCE FOR YEAR(S)  "></asp:Label>
                <asp:Label ID="lblyear" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <ul>
                    <li>
                        <asp:Label ID="Label8" runat="server" Text="Use this form to ask for reduced tax deductions at source for any deductions or non-refundable tax credits that are not part of the Form TD1,
                        Personal Tax Credits Return."></asp:Label>
                    </li>
                    <li>
                        <asp:Label ID="Label9" runat="server" Text="All your income tax returns that are due have to be filed and amounts paid in full before you send us this form."></asp:Label>
                    </li>
                    <li>
                        <asp:Label ID="Label10" runat="server" Text="You usually have to file this request every year. However, if you have deductible support payments that are the same or greater for more
                                than one year, you can make this request for two years."></asp:Label>
                    </li>
                    <li>
                        <asp:Label ID="Label11" runat="server" Text="Send the completed form with all supporting documents to the Taxpayer Services Division of your tax services office. You can find the
                                address on our Web site at "></asp:Label>
                        <asp:Label ID="Label12" runat="server" Font-Bold="true" Text="www.cra.gc.ca/tso "></asp:Label>
                        <asp:Label ID="Label13" runat="server" Text="or by calling us at "></asp:Label>
                        <asp:Label ID="Label14" runat="server" Font-Bold="true" Text=" 1-800-959-8281."></asp:Label>
                    </li>
                    <li>
                        <asp:Label ID="Label15" runat="server" Text="We will write to you in four to eight weeks to let you know if we have approved your request."></asp:Label>
                    </li>
                </ul>
            
           </td>
        </tr>
        <tr>
        <td colspan="4">
            <asp:Panel ID="Panel1" runat="server">
            <fieldset>
                <legend>
                    <asp:Label ID="Label16" runat="server" Text="Identification"></asp:Label>
                </legend>
                                                
            <table id="tbl12" width="100%">
           
            <tr>
                <td width="25%" >
                   <label>
                        <asp:Label ID="Label17" runat="server" Text="First name"></asp:Label>
                    </label>                 
                </td>
                <td width="25%" >
                     <label>
                        <asp:Label ID="Label18" runat="server" Text="Last name"></asp:Label>
                    </label> 
                </td>
                <td width="25%">
                    
                </td>
                <td width="25%" >
                     <label>
                        <asp:Label ID="Label19" runat="server" Text="Social insurance number"></asp:Label>
                    </label> 
                </td>                        
            </tr>
            <tr>
                <td>                    
                    <asp:Label ID="lblfirstname" CssClass="lblSuggestion"  runat="server" Font-Bold="False"></asp:Label>                    
                </td>
                <td>
                    <asp:Label ID="lbllastname" CssClass="lblSuggestion" runat="server" Font-Bold="False"></asp:Label>                    
                </td>
                <td>                    
                </td>
                <td >
                   <asp:Label ID="lblsocialno" CssClass="lblSuggestion" runat="server" Font-Bold="False"></asp:Label>
                </td>                
            </tr>
            <tr>
                <td width="25%" colspan="4" >
                   <label>
                        <asp:Label ID="Label20" runat="server" Text="Address"></asp:Label>
                    </label>                 
                </td>                                    
            </tr>
            <tr>
                <td colspan="4" >
                     <asp:Label ID="lbladdress" runat="server" CssClass="lblSuggestion" ></asp:Label>
                </td>
            </tr>
             </table>
             <table width="100%">
               <%-- <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td colspan="2" align="center">
                        
                            <asp:Label ID="Label26" runat="server" Text="Telephone"></asp:Label>
                       
                    </td>
                    
                </tr>--%>
                <tr>
                    <td width="20%">
                        <label>
                            <asp:Label ID="Label21" runat="server" Text="City"></asp:Label>
                        </label>                        
                    </td>
                    <td width="15%">
                        <label>
                            <asp:Label ID="Label22" runat="server" Text="Province or territory"></asp:Label>
                        </label>
                    </td>
                    <td width="10%">
                        <label>
                            <asp:Label ID="Label23" runat="server" Text="Postal code"></asp:Label>
                        </label>
                    </td>
                    <td width="10%">
                        <label>
                            <asp:Label ID="Label24" runat="server" Text=" Residence Phone"></asp:Label>
                        </label>
                    </td>
                    <td width="10%">
                        <label>
                            <asp:Label ID="Label25" runat="server" Text="Business Phone"></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblcity" runat="server" CssClass="lblSuggestion" ></asp:Label>
                    </td>
                     <td>
                         <asp:Label ID="lblProvince" runat="server"  CssClass="lblSuggestion" ></asp:Label>
                     </td>
                     <td>
                        <asp:Label ID="lblpostalcode" runat="server"  CssClass="lblSuggestion" ></asp:Label>
                     </td>
                     <td>
                        <asp:Label ID="lblresidenceph" runat="server"  CssClass="lblSuggestion"></asp:Label>
                     </td>
                     <td>
                        <asp:Label ID="lblbussinessph" runat="server"  CssClass="lblSuggestion"></asp:Label>
                     </td>
                </tr> 
             </table>
             <table width="100%">
                <tr>
                    <td colspan="3">
                       <label>
                            <asp:Label ID="Label30" runat="server" Text="Employer"></asp:Label>
                        </label>  
                    </td>
                </tr>
                <tr>
                    <td width="30%">
                        <label>
                            <asp:Label ID="Label27" runat="server" Text="Name"></asp:Label>
                        </label>
                    </td>
                    <td width="30%">
                        <label>
                            <asp:Label ID="Label28" runat="server" Text="Contact person"></asp:Label>
                        </label>
                    </td>
                    <td width="30%">
                        <label>
                            <asp:Label ID="Label29" runat="server" Text="Telephone and fax numbers"></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblemployername" CssClass="lblSuggestion" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblemployerCperson" CssClass="lblSuggestion" runat="server" ></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblemployerPno" CssClass="lblSuggestion" runat="server" ></asp:Label>
                    </td>
                </tr>
             </table>
                        
            </fieldset></asp:Panel>
        
        </td>
        </tr>
        <tr>
            <td colspan="4">
             <fieldset>
                <legend>
                    <asp:Label ID="Label34" runat="server" Text="Request to reduce tax on"></asp:Label>
                </legend>
                 <table width="100%">
                    <tr>
                        <td>                            
                    <asp:CheckBox ID="chksal" runat="server" Text="Salary" />
                    <asp:CheckBox ID="chklumpsum" runat="server" 
                                Text="Lump sum – if lump sum, give payment amount and details (for example, a bonus or vacation pay)" 
                                AutoPostBack="True" oncheckedchanged="chklumpsum_CheckedChanged" />                
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel5" runat="server" Width="100%" Visible="False">
                          <table width="100%">
                          <tr>
                          <td style="width:20%"></td>
                            <td style="width:20%">
                            <label>
                                    <asp:TextBox ID="txtlumpamt" runat="server"></asp:TextBox>
                                       <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtlumpamt" ValidChars="-.">
                                                    </cc1:FilteredTextBoxExtender>                          
                                          
                                </label>
                            </td>
                              <td style="width:60%">
                               <label>
                                 <asp:TextBox ID="txtlumpdetail" runat="server" Width="607px"></asp:TextBox>
                                </label>
                              </td>
                          </tr>
                          </table>
                                
                               
                            </asp:Panel>
                        </td>                        
                   </tr>                    
                 </table>                                
             </fieldset>   
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label35" runat="server" Text="Deductions from income and non-refundable tax credits"></asp:Label>
                    </legend>
                    <table width="100%">
                         <tr>
                            <td colspan="2">
                                <label>
                                <asp:Label ID="Label36" runat="server" Text="Registered retirement savings plan (RRSP) contributions"></asp:Label>
                                </label>            
                            </td>
                            <td>
                                 <asp:TextBox ID="txtrrsp" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtrrsp" ValidChars="-.">
                                                    </cc1:FilteredTextBoxExtender>      
                            </td>
                          </tr>
                          <tr>  
                            <td colspan="2">
                                <ul>
                                    <li>
                                        <asp:Label ID="Label37" runat="server" Text="Give details or a copy of the payment arrangement contract."></asp:Label>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label38" runat="server" Text="Do not include contributions deducted from your pay by your employer."></asp:Label>
                                    </li>
                                </ul>
                            </td>
                            <td>
                            
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                            <label>
                                <asp:Label ID="Label39" runat="server" Text="Child care expenses"></asp:Label>
                            </label>
                        </td>
                        <td>
                             <asp:TextBox ID="txtF1" runat="server"></asp:TextBox>
                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtF1" ValidChars="-.">
                                                    </cc1:FilteredTextBoxExtender>      
                        </td>
                        
                        </tr>
                        <tr>  
                            <td colspan="2">
                                <ul>
                                    <li>
                                        <asp:Label ID="Label40" runat="server" Text="Give details on a separate sheet."></asp:Label>
                                    </li>                                   
                                </ul>
                            </td>
                            <td>
                            
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                            <label>
                                <asp:Label ID="Label41" runat="server" Text="Support payments"></asp:Label>
                            </label>
                        </td>
                        <td>
                             <asp:TextBox ID="txtF2" runat="server"></asp:TextBox>        
                               <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtF2" ValidChars="-.">
                                                    </cc1:FilteredTextBoxExtender>                           
                        </td>
                        
                        </tr>
                        <tr>  
                            <td colspan="2">
                                <ul>
                                    <li>
                                        <asp:Label ID="Label42" runat="server" Text="Attach a copy of your court order or written agreement and Form T1158, Registration of Family Support Payments (if not previously filed);"></asp:Label>
                                    </li>   
                                    <li>
                                        <asp:Label ID="Label43" runat="server" Text="Recipient's name and social insurance number:"></asp:Label>
                                    </li>                                                                                                                                              
                                </ul>
                            </td>
                            <td>
                            
                            </td>
                        </tr>
                        <tr>
                     
                         <td align="right">
                           <asp:TextBox ID="txtF2supportrecieptname" runat="server" 
                                 Width="400px"></asp:TextBox>
                               </td>
                                 <td>
                             <asp:TextBox ID="txtF2supportrecieptsin" runat="server"></asp:TextBox>
                             
                            </td>
                            <td align="right">
                                &nbsp;      </td>
                            <td>
                                
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                            <label>
                                <asp:Label ID="Label44" runat="server" Text="Employment expenses"></asp:Label>
                            </label>
                        </td>
                        <td>
                             <asp:TextBox ID="txtempexp" runat="server"></asp:TextBox> 
                               <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtempexp" ValidChars="-.">
                                                    </cc1:FilteredTextBoxExtender>                                  
                        </td>
                       
                        </tr>
                        <tr>  
                            <td colspan="2">
                                <ul>
                                    <li>
                                        <asp:Label ID="Label45" runat="server" Text="Attach a completed Form T2200, Declaration of Conditions of Employment, and Form T777,
Statement of Employment Expenses."></asp:Label>
                                    </li>   
                                                                                                                                                                             
                                </ul>
                            </td>
                            <td>
                            
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                            <label>
                                <asp:Label ID="Label46" runat="server" Text="Carrying charges and interest expenses on investment loans"></asp:Label>
                            </label>
                        </td>
                        <td>
                             <asp:TextBox ID="txtcarrycharge" runat="server"></asp:TextBox>      
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtcarrycharge" ValidChars="-.">
                                                    </cc1:FilteredTextBoxExtender>                                        
                        </td>
                       
                        </tr>
                        <tr>  
                            <td colspan="2">
                                <ul>
                                    <li>
                                        <asp:Label ID="Label47" runat="server" Text="Attach a copy of statements from the lender confirming the purpose and amount of the loan(s) and the
interest payments to be made in the year."></asp:Label>
                                    </li>   
                                                                                                                                                                             
                                </ul>
                            </td>
                            <td>
                            
                            </td>
                        </tr>
                         <tr>
                        <td colspan="2">
                            <label>
                                <asp:Label ID="Label48" runat="server" Text="Other (for example, charitable donations or rental losses)"></asp:Label>
                            </label>
                        </td>
                        <td>
                             <asp:TextBox ID="txtotheramt" runat="server"></asp:TextBox>      
                               <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtotheramt" ValidChars="-.">
                                                    </cc1:FilteredTextBoxExtender>                           
                        </td>
                       
                        </tr>
                        <tr>  
                            <td colspan="2">
                                <ul>
                                    <li>
                                        <asp:Label ID="Label49" runat="server" Text="Attach all supporting documents. Use a separate sheet to give details if necessary."></asp:Label>
                                    </li>   
                                                                                                                                                                             
                                </ul>
                            </td>
                            <td>
                            
                            </td>
                        </tr>
                        <tr>                            
                            <td colspan="2">
                                <label>
                                    <asp:Label ID="Label50" runat="server" Text="Specify:"></asp:Label>
                                </label>
                                <label>
                                    <asp:TextBox ID="txtspecific" runat="server" Width="400px"></asp:TextBox>
                                </label>
                            </td>
                        </tr>
                         <tr>
                        <td align="right" colspan="2">
                            <asp:Label ID="Label52" Font-Bold="true" runat="server" Text="Total "></asp:Label>
                                <asp:Label ID="Label51" runat="server" Text="amounts to be deducted from income"></asp:Label>
                            
                        </td>
                        <td>
                             <asp:TextBox ID="txtgrossdedact" runat="server" Enabled="False"></asp:TextBox>
                               <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtgrossdedact" ValidChars="-.">
                                                    </cc1:FilteredTextBoxExtender>                                
                        </td>
                       
                        </tr>
                        <tr>
                        <td align="right" colspan="2">
                            <asp:Label ID="Label53" Font-Bold="true" runat="server" Text="Subtract income not subject to tax deductions at source "></asp:Label>
                                <asp:Label ID="Label54" runat="server" Text="(interest, net rental or self-employed income)"></asp:Label>
                            
                        </td>
                        <td>
                             <asp:TextBox ID="txtsubamt" runat="server" AutoPostBack="false" 
                                ></asp:TextBox> 
                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtsubamt" ValidChars=".">
                                                    </cc1:FilteredTextBoxExtender>        
                                                     
                        </td>
                       
                        </tr>
                         <tr>
                        <td align="right" colspan="2">
                            <asp:Label ID="Label55" Font-Bold="true" runat="server" Text="Net "></asp:Label>
                                <asp:Label ID="Label56" runat="server" Text="amount requested for"></asp:Label>
                            <asp:Label ID="Label57" Font-Bold="true" runat="server" Text="tax waiver "></asp:Label>
                        </td>
                        <td>
                             <asp:TextBox ID="txtfinalamt" runat="server" Enabled="False"></asp:TextBox> 
                              <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtfinalamt" ValidChars="-.">
                                                    </cc1:FilteredTextBoxExtender>                                    
                        </td>
                       
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
       
        <tr>
            <td colspan="4">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label58" runat="server" Text="Certification"></asp:Label>
                    </legend>
                    <label>
                        <asp:Label ID="Label59" runat="server" Text="I request authorization for my employer to reduce my tax deductions at source based on the information given. I certify that the information
given is, to the best of my knowledge, correct and complete."></asp:Label>
                    </label>
                    <table>
                        <tr>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtsign" runat="server"></asp:TextBox>
                                    <asp:Label ID="Label60" runat="server" Text="Signature"></asp:Label>
                                </label>
                            </td>
                            <td style="width:70%;"></td>
                            <td align="left">
                                <label>
                                    <asp:TextBox ID="txtdate" runat="server" Width="70px"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtdate" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:Label ID="Label61" runat="server" Text="Date"></asp:Label>
                                </label>
                            </td>
                            <td> <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                            PopupButtonID="ImageButton2" TargetControlID="txtdate">
                        </cc1:CalendarExtender>
                        <asp:ImageButton ID="ImageButton2" runat="server" 
                            ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                        
            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
       
        </table>
       
        </asp:Panel>
        </td>
        </tr>
        
        
        <tr>
        <td colspan="2" style="width:100%">
        </td>
        </tr>
        <tr>
        <td colspan="2" style="width:100%" align="center">
            <asp:Button ID="Button3" runat="server" Text="Submit" onclick="Button3_Click" 
                ValidationGroup="1" />
            &nbsp;<asp:Button ID="Button4" runat="server" Text="Update" onclick="Button4_Click" 
                Visible="False" ValidationGroup="1" />
            &nbsp;<asp:Button ID="Button5" runat="server" Text="Cancel" 
                onclick="Button5_Click" />
        </td>
        </tr>
        <tr>
        <td colspan="2">
        <br />
        </td>
        </tr>
         <tr>
        <td colspan="2" style="width:100%">
        
            <asp:Panel ID="Panel3" runat="server">
            <table id="tbl132" style="width:100%">
             <tr>
        <td align="right" >

         <asp:Label ID="lblfil" runat="server"   Text="Filter by Business Name :"></asp:Label></td>
        <td >

           
            <asp:DropDownList ID="ddlfilterbus" runat="server" AutoPostBack="true" 
                OnSelectedIndexChanged="ddlfilterbus_SelectedIndexChanged">
            </asp:DropDownList>
         

        </td>
        </tr>
       
        <tr>
        <td align="right" >

             <asp:Label ID="Label26" runat="server"   Text="Filter by Employee Name :"></asp:Label></td>
        <td >

        
            <asp:DropDownList ID="ddlfilteremp" runat="server" 
                OnSelectedIndexChanged="ddlfilteremp_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList>
          

        </td>
        </tr>
       <tr>
        <td align="right" >

             <asp:Label ID="Label62" runat="server"   Text="Filter by Text Year :"></asp:Label></td>
        <td >

           
            <asp:DropDownList ID="ddlfilteryear" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlfilteryear_SelectedIndexChanged">
            </asp:DropDownList>
          

        </td>
        </tr>
          <tr>
        <td align="right" >

            <asp:Label ID="Label63" runat="server"   Text="Filter by Payroll Deduction Master :"></asp:Label></td>
        <td >

            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px"
                                        type="hidden" />
                                   <input
                                        id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
            <asp:DropDownList ID="ddlfilterdedctionname" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlfilterdedctionname_SelectedIndexChanged" 
                Width="500px">
            </asp:DropDownList>
           

        </td>
        </tr>
        <tr>
        <td >

        </td>
        <td >

            &nbsp;</td>
        </tr>
            </table>
            </asp:Panel>
        
        </td>
        </tr>
       
        <tr>
        <td colspan="2" >
 <div style="float: right;">
                 <label>
                    <asp:Button ID="Button1" CssClass="btnSubmit"  runat="server" Text="Printable Version" 
                        onclick="Button1_Click" />
                    <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';" 
                        type="button" value="Print" visible="False" />
                      
                </label>
            
            </div>
           
        </td>
        </tr>
        
        
        <tr>
        <td colspan="2" style="width:100%">
        
          <asp:Panel ID="pnlgrid" runat="server"  Width="100%">
                <table id="Table2" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="center">
                        <td>
                             <div id="mydiv" class="closed">
                                 <table width="100%" style="color:Black; font-weight:bold; font-style:italic; text-align:center">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblCompany" runat="server" Font-Size="20px"></asp:Label>                                                                                        
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblemp" runat="server" Font-Size="20px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblpay" runat="server" Font-Size="20px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblyea" runat="server" Font-Size="20px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblcy" runat="server" Font-Size="20px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label32" runat="server" Font-Size="18px" Text="REQUEST TO REDUCE TAX DEDUCTIONS AT SOURCE FOR YEAR(S)" ></asp:Label>
                                        </td>
                                    </tr>                            
                                   
                                    
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                </table>                    
                            </div>
                        </td>
                    </tr>         
                    <tr>
                        <td>
                        
                        
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                                        CellPadding="4" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                                            AlternatingRowStyle-CssClass="alt" Width="100%" 
                                                        onrowediting="GridView1_RowEditing" 
                                AllowSorting="True" onsorting="GridView1_Sorting" 
                                onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting">
                        <Columns>
                         <asp:TemplateField HeaderText="Date" ItemStyle-Width="5%" SortExpression="Date" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbldme" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Business Name" ItemStyle-Width="15%" SortExpression="Wname" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblbname" runat="server" Text='<%# Eval("Wname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             
                            <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="15%" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                      
                           
                            <asp:TemplateField HeaderText="Year" ItemStyle-Width="6%" SortExpression="TaxYear_Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblno" runat="server" Text='<%# Eval("TaxYear_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Total Amt" ItemStyle-Width="6%" SortExpression="TotalAmt" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblano" runat="server" Text='<%# Eval("TotalAmt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                          <asp:CommandField ShowSelectButton="true"  HeaderImageUrl="~/Account/images/edit.gif" HeaderText="Edit" ButtonType="Image" SelectImageUrl="~/Account/images/edit.gif"
                                            ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="2%" >
                                                    <ItemStyle HorizontalAlign="Left" Width="2%"></ItemStyle>
                                                </asp:CommandField>
                                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"  HeaderStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="llinkbb" runat="server" 
                                                     CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>               </Columns>
                       
                    </asp:GridView>
                 </td>
                    </tr>     
                    </table>
                </asp:Panel>
        </td>
        </tr>
        <tr>
        <td colspan="2" style="width:100%">
           <asp:Panel ID="pnconfor" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA" BorderStyle="Outset"
                    Width="600px">
                    <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="">
                                Message....
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblddffg" runat="server" Text="Form can not be submitted , because you have not filled amount in any field." ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label31" runat="server" Text="Please enter some amount in any of the field to submit this form." ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                      
                        <tr>
                            <td align="center">
                            <asp:Button ID="btns" runat="server" BackColor="#CCCCCC" Text="Cancel"
                              />
                            
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button2" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                    ID="ModapupExtende22" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btns"
                    PopupControlID="pnconfor" TargetControlID="Button2">
                        </cc1:ModalPopupExtender>
       
        </td>
        </tr>
        <tr>
        <td colspan="2" style="width:100%">
        </td>
        </tr>
        
        
        </table>
        </fieldset>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>


