<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="CanadaFaderalTd2.aspx.cs" Inherits="ShoppingCart_Admin_CanadaFederalTd2" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <style type="text/css">
        .style2
        {
            width: 14%;
        }
    </style>
<script language="javascript" type="text/javascript" >    

function CallPrint(strid)
{
        var prtContent=document.getElementById('<%= Panel4.ClientID %>');        
        var  WinPrint=window.open('','','left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
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
                
            
              alert("You have entered invalid character");
                  return false;
             }
             
             
               
            
        }  
        function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered invalid character");
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
         <div style="float: left;">
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </div>
         
          <div style="clear: both;">
                        </div>
                        
                         <div class="products_box">
                         <fieldset>
         
        <table id="tbl1132" style="width:100%">
       
       
         <tr>
        <td colspan="4" style="width:100%">
            <asp:Panel ID="Panel2" runat="server">
            <table id="td123" style="width:100%">
           
            <tr>
            <td  width="25%">
                <label>
                                                        <asp:Label ID="lblnme" runat="server" Text="Business Name"></asp:Label>
                                                    </label>
                                                    </td>
            <td  width="75%">
              <label>
               <asp:DropDownList ID="ddlstore" runat="server" Width="300Px" 
                    AutoPostBack="True" onselectedindexchanged="ddlstore_SelectedIndexChanged">
                </asp:DropDownList>
                </label>  </td>
           
            </tr>
            <tr>
            <td  width="25%">
               <label>
                                                        <asp:Label ID="Label1" runat="server" Text="Employee Name"></asp:Label>
                                                    </label>
            </td>
            <td width="75%">
              
              <label><asp:DropDownList ID="ddlemployee" runat="server" Width="300Px" 
                    AutoPostBack="True" onselectedindexchanged="ddlemployee_SelectedIndexChanged">
                </asp:DropDownList>
                </label>  </td>
          
            </tr>
            <tr>
            <td  width="25%">
                 <label>
                                                        <asp:Label ID="Label2" runat="server" Text="Tax Year"></asp:Label>
                                                    </label></td>
            <td width="75%">
                
                <label>
                 <asp:DropDownList ID="ddltaxyear" runat="server" Width="300Px">
                </asp:DropDownList>
                </label>
                </td>
          
            </tr>
            <tr>
            <td  width="25%">
               <label>
                                                        <asp:Label ID="Label3" runat="server" Text="Payroll Deduction Master"></asp:Label>
                                                    </label>
            </td>
            <td width="75%">
              <label>
               <asp:DropDownList ID="ddpayrolldeduction" runat="server" Width="300Px">
                </asp:DropDownList>
              </label>
                </td>
           
            </tr>
            <tr>
            <td  style="width:25%">
            </td>
            <td style="width:25%">
            </td>
           
            </tr>
            <tr>
            
            <td style="width:25%" >
            </td>
            <td style="width:25%" >
            </td>
            </tr>
            <tr>
           
            <td style="width:25%" >
            </td>
            <td style="width:25%" >
            </td>
            </tr>
            
            </table>
            </asp:Panel>
        </td>
        </tr>
        <tr>
        <td colspan="4" align="right">
        <input type="button" value="print" id="Button1" class="btnSubmit" runat="server" onclick="javascript:CallPrint('divPrint')" />
        </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
         <asp:Panel ID="Panel4" runat="server" BorderColor="Black" BorderStyle="Solid" 
                BorderWidth="1px">
         <table id="pnltbl4" style="width:100%">
         <tr>
        <td width="20%" style="font-weight: bold; color: #000000" >
        
            Government Of Alberta</td>
        <td align="right" style="color: #000000; font-weight: bold;"  >
            2012 ALBERTA PERSONAL TAX CREDITS RETURN</td>
        <td class="style2" >
        </td>
        <td style="font-weight: bold; color: #000000" >
            TD1AB</td>
        </tr>
        
        <tr>
        <td colspan="4" style="width:100%">
            Your employer or payer will use this form to determine the amount of your 
            provincial tax deductions.</td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            Read the back before completing this form. Complete this form based on the best 
            estimate of your circumstances.</td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            <asp:Panel ID="Panel1" runat="server">
            <table id="tbl12" style="border: 1px solid #000000; width:100%">
            <tr>
            <td colspan="4">
            </td>
            
            </tr>
            <tr>
            <td width="25%" >
                Last name</td>
            <td width="25%" >
                First name and initial(s)</td>
            <td width="25%" >
                Date of birth (YYYY/MM/DD)</td>
            <td width="25%" >
                Employee number</td>
            
            </tr>
            <tr>
            <td >
                <asp:Label ID="lbllastname" runat="server" Font-Bold="False"></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblfirstname" runat="server" Font-Bold="False"></asp:Label>
                <asp:Label ID="lblintial" runat="server"></asp:Label>
            </td>
            <td >
                <asp:Label ID="lbldateofbirth" runat="server" Font-Bold="False"></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblempno" runat="server" Font-Bold="False"></asp:Label>
            </td>
            
            </tr>
            <tr>
            <td colspan="2" >
                Address including postal code
                
            </td>
            <td >
                &nbsp;</td>
            <td  >
                Social insurance number</td>
            
            </tr>
            <tr>
            <td  colspan="2">
            
                <asp:Label ID="lbladdress" runat="server" Font-Bold="False"></asp:Label>
            
                <asp:Label ID="lblpincode" runat="server"></asp:Label>
            
            </td>
            <td >
            </td>
            <td >
                <asp:Label ID="lblsocialno" runat="server" Font-Bold="False"></asp:Label>
            </td>
            
            </tr>
            
            </table>
            </asp:Panel>
        
        </td>
        </tr>
        
        
        <tr>
        
        
        <td colspan="3" 
                style="border-bottom: 1px solid #000000;">
        
            
        
            1. Basic personal amount – Every person employed in Alberta and every pensioner 
            residing in Alberta can claim this amount. If you will have more than one 
            employer or payer at the same time in 2012, see &quot;Will you have more than one 
            employer or payer at the same time?&quot; on the next page.</td>
            <td valign="middle" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
             <asp:TextBox ID="TextBox15" runat="server" MaxLength="50"></asp:TextBox>
             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3fsd" 
                                    runat="server" Enabled="True" TargetControlID="TextBox15" 
                                    ValidChars="0147852369."/>
            </td>
        </tr>
        <tr>
        <td colspan="3">
        
            
        
           
        
            
        
        </td>
        <td></td>
        </tr>
        <tr>
        <td colspan="3" 
                style="border-bottom: 1px solid #000000;">
        
            
        
            2. Age amount – If you will be 65 or older on December 31, 2012, and your net 
            income from all sources will be $35,851, or less, enter $4,816. If your net 
            income for the year will be between $35,851 and $67,958 and you want to 
            calculate a partial claim, get the TD1AB-WS, Worksheet for the 2012 Alberta 
            Personal Tax Credits Return, and complete the appropriate section.</td>
         <td valign="bottom" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
             <asp:TextBox ID="TextBox14" runat="server" MaxLength="50"></asp:TextBox>
             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                    runat="server" Enabled="True" TargetControlID="TextBox14" 
                                    ValidChars="0147852369."/>
            </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            &nbsp;</td>
        </tr>
        <tr>
        <td colspan="3" 
                style="border-bottom: 1px solid #000000;">
            3. Pension income amount – If you will receive regular pension payments from a 
            pension plan or fund (excluding Canada Pension Plan, Quebec Pension Plan, Old 
            Age Security, or Guaranteed Income Supplement payments), enter $1,331, or your 
            estimated annual pension income, whichever is less.</td>
         <td valign="bottom" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
             <asp:TextBox ID="TextBox13" runat="server" MaxLength="50"></asp:TextBox>
             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                    runat="server" Enabled="True" TargetControlID="TextBox13" 
                                    ValidChars="0147852369."/>
            </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            &nbsp;</td>
        </tr>
        <tr>
        <td colspan="3" 
                style="border-bottom: 1px solid #000000;">
            4. Tuition and education amounts (full time and part time) – If you are a 
            student enrolled at a university, college, or educational institution certified 
            by Human Resources and Skills Development Canada, and you will pay more than 
            $100 per institution in tuition fees, complete this section. If you are enrolled 
            full time, or if you have a mental or physical disability and are enrolled part 
            time, enter the total of the tuition fees you will pay, plus $672 for each month 
            that you will be enrolled. If you are enrolled part time and do not have a 
            mental or physical disability, enter the total of the tuition fees you will pay, 
            plus $202 for each month that you will be enrolled part time.</td>
            <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                <asp:TextBox ID="TextBox12" runat="server" MaxLength="50"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                    runat="server" Enabled="True" TargetControlID="TextBox12" 
                                    ValidChars="0147852369."/>
            </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            &nbsp;</td>
        </tr>
        <tr>
        <td colspan="3" 
                style="border-bottom: 1px solid #000000;">
            5. Disability amount – If you will claim the disability amount on your income 
            tax return by using Form T2201, Disability Tax Credit Certificate, enter 
            $13,331.</td>
            <td valign="bottom" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                <asp:TextBox ID="TextBox11" runat="server" MaxLength="50"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                                    runat="server" Enabled="True" TargetControlID="TextBox11" 
                                    ValidChars="0147852369."/>
            </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            &nbsp;</td>
        </tr>
        <tr>
        <td colspan="3" 
                style="border-bottom: 1px solid #000000;">
            6. Spouse or common-law partner amount – If you are supporting your spouse or 
            common-law partner who lives with you, and whose net income for the year will be 
            less than $17,282, enter the difference between $17,282 and his or her estimated 
            net income. If your spouse&#39;s or common-law partner&#39;s net income for the year 
            will be $17,282 or more, you cannot claim this amount.</td>
            <td valign="bottom" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                <asp:TextBox ID="TextBox10" runat="server" MaxLength="50"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
                                    runat="server" Enabled="True" TargetControlID="TextBox10" 
                                    ValidChars="0147852369."/>
            </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            &nbsp;</td>
        </tr>
        <tr>
        <td colspan="3" 
                style="border-bottom: 1px solid #000000;">
            7. Amount for an eligible dependant – If you do not have a spouse or common-law 
            partner and you support a dependent relative who lives with you, and whose net 
            income for the year will be less than $17,282, enter the difference between 
            $17,282 and his or her estimated net income. If your eligible dependant&#39;s net 
            income for the year will be $17,282 or more, you cannot claim this amount.</td>
            <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                <asp:TextBox ID="TextBox9" runat="server" MaxLength="50"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" 
                                    runat="server" Enabled="True" TargetControlID="TextBox9" 
                                    ValidChars="0147852369."/>
            </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            &nbsp;</td>
        </tr>
        <tr>
        <td colspan="3" 
                style="border-bottom: 1px solid #000000;">
            8. Caregiver amount – If you are taking care of a dependant who lives with you, 
            whose net income for the year will be $15,906 or less, and who is either your or 
            your spouse&#39;s or common-law partner&#39;s:  parent or grandparent (aged 65 or 
            older); or  relative (aged 18 or older) who is dependent on you because of an 
            infirmity, enter $10,004. If the dependant&#39;s net income for the year will be 
            between $15,906 and $25,910 and you want to calculate a partial claim, get the 
            TD1AB-WS, and complete the appropriate section.</td>
            <td valign="bottom" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                <asp:TextBox ID="TextBox8" runat="server" MaxLength="50"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" 
                                    runat="server" Enabled="True" TargetControlID="TextBox8" 
                                    ValidChars="0147852369."/>
            </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            &nbsp;</td>
        </tr>
         <tr>
        <td colspan="3" 
                 style="border-bottom: 1px solid #000000;">
            9. Amount for infirm dependants age 18 or older – If you are supporting an 
            infirm dependant aged 18 or older who is your or your spouse&#39;s or common-law 
            partner&#39;s relative, who lives in Canada, and whose net income for the year will 
            be $6,609 or less, enter $10,004. You cannot claim an amount for a dependant you 
            claimed on line 8. If the dependant&#39;s net income for the year will be between 
            $6,609 and $16,613 and you want to calculate a partial claim, get the TD1AB-WS, 
            and complete the appropriate section.</td>
            <td valign="bottom" 
                 style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                <asp:TextBox ID="TextBox7" runat="server" MaxLength="50"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" 
                                    runat="server" Enabled="True" TargetControlID="TextBox7" 
                                    ValidChars="0147852369."/>
             </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            &nbsp;</td>
        </tr>
        <tr>
        <td colspan="3" 
                style="border-bottom: 1px solid #000000;">
            10. Amounts transferred from your spouse or common-law partner – If your spouse 
            or common-law partner will not use all of his or her age amount, pension income 
            amount, tuition and education amounts, or disability amount on his or her income 
            tax return, enter the unused amount.</td>
            <td valign="bottom" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                <asp:TextBox ID="TextBox6" runat="server" MaxLength="50"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" 
                                    runat="server" Enabled="True" TargetControlID="TextBox6" 
                                    ValidChars="0147852369."/>
            </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            &nbsp;</td>
        </tr>
         <tr>
        <td colspan="3" 
                 style="border-bottom: 1px solid #000000;">
            11. Amounts transferred from a dependant – If your dependant will not use all of 
            his or her disability amount on his or her income tax return, enter the unused 
            amount. If your or your spouse&#39;s or common-law partner&#39;s dependent child or 
            grandchild will not use all of his or her tuition and education amounts on his 
            or her income tax return, enter the unused amount.</td>
            <td valign="bottom" 
                 style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                <asp:TextBox ID="TextBox5" runat="server" MaxLength="50"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" 
                                    runat="server" Enabled="True" TargetControlID="TextBox5" 
                                    ValidChars="0147852369."/>
             </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            &nbsp;</td>
        </tr>
        <tr>
        <td colspan="3" 
                style="border-bottom: 1px solid #000000;">
            12. TOTAL CLAIM AMOUNT – Add lines 1 through 11. Your employer or payer will use 
            your claim amount to determine the amount of your provincial tax deductions.</td>
            <td valign="bottom" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                <asp:TextBox ID="TextBox4" runat="server" MaxLength="50"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" 
                                    runat="server" Enabled="True" TargetControlID="TextBox4" 
                                    ValidChars="0147852369."/>
            </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            &nbsp;</td>
        </tr>
        
        <tr>
        <td colspan="3">
            &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
         <tr>
        <td colspan="4" style="width:100%">
            Completing Form TD1AB</td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            Complete this form only if you are an employee working in Alberta or a pensioner 
            residing in Alberta and any of the following apply:</td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            you have a new employer or payer and you will receive salary, wages, 
            commissions, pensions, Employment Insurance benefits, or any other remuneration;</td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            you want to change amounts you previously claimed (such as when the number of 
            your eligible dependants has changed);</td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            you want to increase the amount of tax deducted at source.</td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            Sign and date it and give it to your employer or payer</td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            If you do not complete a TD1AB form, your new employer or payer will deduct 
            taxes after allowing the basic personal amount only.</td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            &nbsp;</td>
        </tr>
        <tr>
        <td colspan="3">
            Will you have more than one employer or payer at the same time?</td>
            <td></td>
        </tr>
        <tr>
        <td colspan="3">
            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;If you have more than one employer or payer at the same time and you 
            have already claimed personal tax credit amounts on another Form TD1AB for 2012, 
            you cannot claim them again. If your total income from all sources will be more 
            than the personal tax credits you claimed on another Form TD1AB, enter &quot;0&quot; on 
            line 12 on the front page and do not complete lines 2 to 11.</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            &nbsp;</td>
        </tr>
        <tr>
        <td colspan="3">
            Total income less than total claim amount</td>
            <td></td>
        </tr>
         <tr>
        <td colspan="3">
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="CheckBox2" runat="server" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Check this box if your total income for the year from all employers and 
            payers will be less than your total claim amount on line 12. Your employer or 
            payer will not deduct tax from your earnings.</td>
            <td></td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
        </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
            Additional tax to be deducted</td>
        </tr>
        <tr>
        <td colspan="3">
            If you wish to have more tax deducted, complete the section called &quot;Additional 
            tax to be deducted&quot; on the federal Form TD1.</td>
            <td></td>
        </tr>
        <tr>
        <td colspan="3">
            Reduction in tax deductions</td>
            <td>
                &nbsp;</td>
        </tr>
         <tr>
        <td colspan="3">
            You can ask to have less tax deducted if on your income tax return you are 
            eligible for deductions or non-refundable tax credits that are not listed on 
            this form (for example, periodic contributions to a Registered Retirement 
            Savings Plan (RRSP), child care or employment expenses, and charitable 
            donations). To make this request, complete Form T1213, Request to Reduce Tax 
            Deductions at Source for year(s)<asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="TextBox20_FilteredTextBoxExtender" 
                runat="server" Enabled="True" TargetControlID="TextBox20" 
                ValidChars="0147852369." />
            , to get a letter of authority from your tax services office. Give the letter of 
            authority to your employer or payer. You do not need a letter of authority if 
            your employer deducts RRSP contributions from your salary.</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
        </td>
        </tr>
        
         
        <tr>
        <td colspan="4" style="width:100%">
        </td>
        </tr>
        <tr>
        <td colspan="3">
            Certification</td>
            <td></td>
        </tr>
         <tr>
        <td colspan="3">
            I certify that the information given in this return is, to the best of my 
            knowledge, correct and complete</td>
            <td></td>
        </tr>
        <tr>
        <td colspan="2" valign="top" >
            Signature :
            <asp:TextBox ID="TextBox17" runat="server" Width="338px"></asp:TextBox>
            <asp:RegularExpressionValidator ID="REG1" runat="server" 
                                        ErrorMessage="*" Display="Dynamic"
                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"  
                                        ControlToValidate="TextBox17" ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
            <td align="right" class="style2">
                Date : 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                 ControlToValidate="TextBox16" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                        ErrorMessage="*" ControlToValidate="TextBox16"
                                        ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                   
            </td>
            <td>
                <asp:TextBox ID="TextBox16" runat="server" Width="95px"></asp:TextBox>
                
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                  PopupButtonID="ImageButton2" 
                TargetControlID="TextBox16">
             </cc1:CalendarExtender>
             <asp:ImageButton ID="ImageButton2" runat="server" 
                 ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
            

            </td>
        
        </tr>
         </table>
         </asp:Panel>
        </td>
        </tr>
        
        
        
        <tr>
        <td colspan="4" style="width:100%">
        </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%" align="center">
            <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Submit" onclick="Button3_Click" />
            &nbsp;<asp:Button ID="Button4" runat="server" CssClass="btnSubmit" Text="Update" onclick="Button4_Click" 
                Visible="False" />
            &nbsp;<asp:Button ID="Button5" runat="server" Text="Cancel" CssClass="btnSubmit" />
        </td>
        </tr>
         <tr>
        <td colspan="4" style="width:100%">
        </td>
        </tr>
       <tr>
        <td colspan="4" style="width:100%">
        
            <asp:Panel ID="Panel3" runat="server">
            <table id="tbl132" style="width:100%">
             <tr>
        <td colspan="2" width="25%" >
        <label>
                       <asp:Label ID="Label26" runat="server" Text="Filter by Business Name"></asp:Label> 
                       </label>  

             </td>
        <td colspan="2" width="75%" >
<label>
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                onselectedindexchanged="DropDownList1_SelectedIndexChanged" Width="200px">
            </asp:DropDownList>
            </label>

        </td>
        </tr>
        <tr>
        <td colspan="2" align="right" >
 <label>
                       <asp:Label ID="Label4" runat="server" Text="Filter by Batch Name"></asp:Label> 
                       </label>  

            </td>
        <td colspan="2" >
<label>
            <asp:DropDownList ID="DropDownList2" runat="server" 
                onselectedindexchanged="DropDownList2_SelectedIndexChanged" 
                AutoPostBack="True" Width="200px">
            </asp:DropDownList>
</label>
        </td>
        </tr>
        <tr>
        <td colspan="2" align="right" >
<label>
                       <asp:Label ID="Label5" runat="server" Text="Employee Name"></asp:Label> 
                       </label>  
            </td>
        <td colspan="2" >
<label>
            <asp:DropDownList ID="DropDownList3" runat="server" Width="200px">
            </asp:DropDownList>
</label>
        </td>
        </tr>
        <tr>
        <td colspan="2"  >
<label>
                       <asp:Label ID="Label6" runat="server" Text="From Date "></asp:Label> 
                       <asp:Label ID="Label8" runat="server" Text="*"></asp:Label> 
                       
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                 ControlToValidate="TextBox18" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                        ErrorMessage="*" ControlToValidate="TextBox18"
                                        ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                       </label>  
            
           
                   
           
           
            

        </td>
        <td colspan="2" >
        <label>
         <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
         </label>
         <label>
          <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                 PopupButtonID="ImageButton1" 
                TargetControlID="TextBox18">
             </cc1:CalendarExtender>
               <asp:ImageButton ID="ImageButton1" runat="server" 
                 ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
         </label>
        </td>
        </tr>
        <tr>
        <td colspan="2" >
<label>
                       <asp:Label ID="Label7" runat="server" Text="To Date"></asp:Label> 
                        <asp:Label ID="Label9" runat="server" Text="*"></asp:Label> 
                       
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                 ControlToValidate="TextBox19" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="rghjk" runat="server" 
                                        ErrorMessage="*" ControlToValidate="TextBox19"
                                        ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                        </label>
            

        </td>
        <td colspan="2" >
        <label>
            <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
            </label>
            <label>
                   
            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" 
                 PopupButtonID="ImageButton3" 
                TargetControlID="TextBox19">
             </cc1:CalendarExtender>
             <asp:ImageButton ID="ImageButton3" runat="server" 
                 ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
            </label>
        </td>
        </tr>
        <tr>
        <td colspan="2" >

        </td>
        <td colspan="2" >

            <asp:Button ID="Button6" runat="server" CssClass="btnSubmit" Text="Go" onclick="Button6_Click" 
                ValidationGroup="2" />

        </td>
        </tr>
            </table>
            </asp:Panel>
        
        </td>
        </tr>
        <tr>
        <td colspan="2" >

        </td>
        <td colspan="2" >

        </td>
        </tr>
        
        
        <tr>
        <td colspan="4" style="width:100%">
        
         <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                    AlternatingRowStyle-CssClass="alt" DataKeyNames="Id"  
                                        Width="100%" onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting" 
                                       
                                        >
                                        <Columns>
                                           <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstrname123" runat="server" Text='<%# Eval("WName") %>'></asp:Label>
                                                </ItemTemplate>
                                                 
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemp123" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                                </ItemTemplate>
                                                 
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Claim Amount"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltotalclaim123" runat="server" Text='<%# Eval("12TotalClaimAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldate123" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            
                                            
                                            <asp:ButtonField CommandName="vi" Text="Edit/View" ItemStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Left" HeaderText="Edit/View" />
                                           
                                            <asp:CommandField  ShowDeleteButton="True" ItemStyle-ForeColor="Black" HeaderText="Delete"/>
                                        </Columns>
                                       
                                    </asp:GridView>
        </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
        </td>
        </tr>
        <tr>
        <td colspan="4" style="width:100%">
        </td>
        </tr>
        
        
        </table>
        </fieldset>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>


