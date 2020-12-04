<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="CanadaFaderalTD1.aspx.cs" Inherits="ShoppingCart_Admin_CanadaFaderalTD1"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
            <table style="width: 100%">
                
                <tr>
                    <td colspan="4" style="width: 100%">
                        <asp:Panel ID="Panel2" runat="server">
                        <fieldset>
                            <table style="width: 100%">
                               
                                <tr>
                                    <td align="right">
                                        <label>
                                            <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                                            <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlstore"
                                                InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlstore" runat="server" Width="300px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlstore_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <label>
                                            <asp:Label ID="Label2" runat="server" Text="Employee Name"></asp:Label>
                                            <asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlemployee"
                                                InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlemployee" runat="server" Width="300px" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <label>
                                            <asp:Label ID="Label3" runat="server" Text="Tax Year"></asp:Label>
                                            <asp:Label ID="Label7" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddltaxyear"
                                                InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddltaxyear" runat="server" Width="300px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <label>
                                            <asp:Label ID="Label4" runat="server" Text="Payroll Deduction Master"></asp:Label>
                                            <asp:Label ID="Label8" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddpayrolldeduction"
                                                InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddpayrolldeduction" runat="server" Width="300px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset></asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="right">
                        <input type="button" value="print" class="btnSubmit" id="Button1" runat="server"
                            onclick="javascript:CallPrint('divPrint')" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 100%">
                        <asp:Panel ID="Panel4" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                            <table id="pnltbl4" style="width: 100%">
                                <tr align="center">
                                    <td width="20%" style="font-weight: bold; color: #000000" align="left" class="style3">
                                        <center>
                                            Canada Revenue Agency</center>
                                    </td>
                                    <td align="right" style="color: #000000; font-weight: bold;" class="style2">
                                        <center>
                                            Agence du revenu du Canada</center>
                                    </td>
                                    <td class="style4">
                                        <center style="width: 333px; font-weight: bold;">
                                            2012 PERSONAL TAX CREDITS RETURN</center>
                                    </td>
                                    <td style="font-weight: bold; color: #000000" class="style3">
                                        TD1
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        Your employer or payer will use this form to determine the amount of your tax deductions
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        Read the back before completing this form. Complete this form based on the best
                                        estimate of your circumstances
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <table id="tbl12" style="border: 1px solid #000000; width: 100%">
                                                <tr>
                                                    <td colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="25%">
                                                        Last name
                                                    </td>
                                                    <td width="25%">
                                                        First name and initial(s)
                                                    </td>
                                                    <td width="25%">
                                                        Date of birth (YYYY/MM/DD)
                                                    </td>
                                                    <td width="25%">
                                                        Employee number
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbllastname" runat="server" Font-Bold="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblfirstname" runat="server" Font-Bold="False"></asp:Label>
                                                        <asp:Label ID="lblintial" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldateofbirth" runat="server" Font-Bold="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblempno" runat="server" Font-Bold="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        Address including postal code
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        Social insurance number
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lbladdress" runat="server" Font-Bold="False"></asp:Label>
                                                        <asp:Label ID="lblpincode" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblsocialno" runat="server" Font-Bold="False"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-bottom: 1px solid #000000;">
                                        1. Basic personal amount – Every resident of Canada can claim this amount. If you
                                        will have more than one employer or payer at the same time in 2012, see &quot;More
                                        than one employer or payer at the same time&quot; on the next page. If you are a
                                        non-resident, see &quot;Non-residents&quot; on the next page.
                                    </td>
                                    <td valign="top" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                                        <asp:TextBox ID="TextBox15" runat="server" MaxLength="50"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3fsd" runat="server" Enabled="True"
                                            TargetControlID="TextBox15" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-bottom: 1px solid #000000;">
                                        2. Child amount – Either parent (but not both), may claim $2,191 for each child
                                        born in 1995 or later, that resides with both parents throughout the year. If the
                                        child is infirm, add $2,000 to the claim for that child. Any unused portion can
                                        be transferred to that parent&#39;s spouse or common-law partner. If the child does
                                        not reside with both parents throughout the year, the parent who is entitled to
                                        claim the &quot;Amount for an eligible dependant&quot; on line 8 may also claim
                                        the child amount for that same child.
                                    </td>
                                    <td valign="bottom" style="border-bottom-style: solid; border-bottom-width: 1px;
                                        border-bottom-color: #000000">
                                        <asp:TextBox ID="TextBox14" runat="server" MaxLength="50"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            TargetControlID="TextBox14" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-bottom: 1px solid #000000;">
                                        3. Age amount – If you will be 65 or older on December 31, 2012, and your net income
                                        for the year from all sources will be $33,884 or less, enter $6,720. If your net
                                        income for the year will be between $33,884 and $78,684 and you want to calculate
                                        a partial claim, get the TD1-WS, Worksheet for the 2012 Personal Tax Credits Return,
                                        and complete the appropriate section.
                                    </td>
                                    <td valign="bottom" style="border-bottom-style: solid; border-bottom-width: 1px;
                                        border-bottom-color: #000000">
                                        <asp:TextBox ID="TextBox13" runat="server" MaxLength="50"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                            TargetControlID="TextBox13" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-bottom: 1px solid #000000;">
                                        4. Pension income amount – If you will receive regular pension payments from a pension
                                        plan or fund (excluding Canada Pension Plan, Quebec Pension Plan, Old Age Security,
                                        or Guaranteed Income Supplement payments), enter $2,000 or your estimated annual
                                        pension income, whichever is less.
                                    </td>
                                    <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                                        <asp:TextBox ID="TextBox12" runat="server" MaxLength="50"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                            TargetControlID="TextBox12" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-bottom: 1px solid #000000;">
                                        5. Tuition, education, and textbook amounts (full time and part time) – If you are
                                        a student enrolled at a university or college, or an educational institution certified
                                        by Human Resources and Skills Development Canada, and you will pay more than $100
                                        per institution in tuition fees, complete this section. If you are enrolled full
                                        time, or if you have a mental or physical disability and are enrolled part time,
                                        enter the total of the tuition fees you will pay, plus $400 for each month that
                                        you will be enrolled, plus $65 per month for textbooks. If you are enrolled part
                                        time and do not have a mental or physical disability, enter the total of the tuition
                                        fees you will pay, plus $120 for each month that you will be enrolled part time,
                                        plus $20 per month for textbooks.
                                    </td>
                                    <td valign="bottom" style="border-bottom-style: solid; border-bottom-width: 1px;
                                        border-bottom-color: #000000">
                                        <asp:TextBox ID="TextBox11" runat="server" MaxLength="50"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                            TargetControlID="TextBox11" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-bottom: 1px solid #000000;">
                                        6. Disability amount – If you will claim the disability amount on your income tax
                                        return by using Form T2201, Disability Tax Credit Certificate, enter $7,546.
                                    </td>
                                    <td valign="bottom" style="border-bottom-style: solid; border-bottom-width: 1px;
                                        border-bottom-color: #000000">
                                        <asp:TextBox ID="TextBox10" runat="server" MaxLength="50"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                            TargetControlID="TextBox10" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-bottom: 1px solid #000000;">
                                        7. Spouse or common-law partner amount – If you are supporting your spouse or common-law
                                        partner who lives with you, and whose net income for the year will be less than
                                        $10,822 ($12,822 if he or she is infirm) enter the difference between this amount
                                        and his or her estimated net income for the year. If your spouse&#39;s or common-law
                                        partner&#39;s net income for the year will be $10,822 or more ($12,822 or more if
                                        he or she is infirm), you cannot claim this amount.
                                    </td>
                                    <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000"
                                        valign="bottom">
                                        <asp:TextBox ID="TextBox9" runat="server" MaxLength="50"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                            TargetControlID="TextBox9" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-bottom: 1px solid #000000;">
                                        8. Amount for an eligible dependant – If you do not have a spouse or common-law
                                        partner and you support a dependent relative who lives with you, and whose net income
                                        for the year will be less than $10,822 ($12,822 if he or she is infirm and you did
                                        not claim the child amount for this dependant), enter the difference between this
                                        amount and his or her estimated net income. If your eligible dependant&#39;s net
                                        income for the year will be $10,822 or more ($12,822 or more if he 聯r she is infirm),
                                        you cannot claim this amount.
                                    </td>
                                    <td valign="bottom" style="border-bottom-style: solid; border-bottom-width: 1px;
                                        border-bottom-color: #000000">
                                        <asp:TextBox ID="TextBox8" runat="server" MaxLength="50"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                            TargetControlID="TextBox8" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-bottom: 1px solid #000000;">
                                        9. Caregiver amount – If you are taking care of a dependant who lives with you,
                                        whose net income for the year will be $15,033 or less, and who is either your or
                                        your spouse&#39;s or common-law partner&#39;s:  parent or grandparent (aged 65
                                        or older), enter $4,402 ($6,402 if he or she is infirm) or  relative (aged 18 or
                                        older) who is dependent on you because of an infirmity, enter $6,402. If the dependant&#39;s
                                        net income for the year will be between $15,033 and $19,435 ($15,033 and $21,435
                                        if he or she is infirm) and you want to calculate a partial claim, get the TD1-WS,
                                        and complete the appropriate section.
                                    </td>
                                    <td valign="bottom" style="border-bottom-style: solid; border-bottom-width: 1px;
                                        border-bottom-color: #000000">
                                        <asp:TextBox ID="TextBox7" runat="server" MaxLength="50"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                            TargetControlID="TextBox7" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-bottom: 1px solid #000000;">
                                        10. Amount for infirm dependants age 18 or older – If you support an infirm dependant
                                        age 18 or older who is your or your spouse&#39;s or common-law partner&#39;s relative,
                                        who lives in Canada, and whose net income for the year will be $6,420 or less, enter
                                        $6,402. You cannot claim an amount for a dependant you claimed on line 9. If the
                                        dependant&#39;s net income for the year will be between $6,420 and $12,822 and you
                                        want to calculate a partial claim, get the TD1-WS, and complete the appropriate
                                        section.
                                    </td>
                                    <td valign="bottom" style="border-bottom-style: solid; border-bottom-width: 1px;
                                        border-bottom-color: #000000">
                                        <asp:TextBox ID="TextBox6" runat="server" MaxLength="50"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                            TargetControlID="TextBox6" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-bottom: 1px solid #000000;">
                                        11. Amounts transferred from your spouse or common-law partner – If your spouse
                                        or common-law partner will not use all of his or her age amount, pension income
                                        amount, tuition, education and textbook amounts, disability amount or child amount
                                        on his or her income tax return, enter the unused amount.
                                    </td>
                                    <td valign="bottom" style="border-bottom-style: solid; border-bottom-width: 1px;
                                        border-bottom-color: #000000">
                                        <asp:TextBox ID="TextBox5" runat="server" MaxLength="50"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                            TargetControlID="TextBox5" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-bottom: 1px solid #000000;">
                                        12. Amounts transferred from a dependant – If your dependant will not use all of
                                        his or her disability amount on his or her income tax return, enter the unused amount.
                                        If your or your spouse&#39;s or common-law partner&#39;s dependent child or grandchild
                                        will not use all of his or her tuition, education, and textbook amounts on his or
                                        her income tax return, enter the unused amount.
                                    </td>
                                    <td valign="bottom">
                                        <asp:TextBox ID="TextBox4" runat="server" MaxLength="50"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                            TargetControlID="TextBox4" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-bottom: 1px solid #000000;">
                                        13. TOTAL CLAIM AMOUNT – Add lines 1 through 12. Your employer or payer will use
                                        this amount to determine the amount of your tax deductions.
                                    </td>
                                    <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000"
                                        valign="bottom">
                                        <asp:TextBox ID="TextBox3" runat="server" MaxLength="50"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                            TargetControlID="TextBox3" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        More than one employer or payer at the same time
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; If you have more than one employer or payer
                                        at the same time and you have already claimed personal tax credit amounts on another
                                        TD1 form for 2012, you cannot claim them again. If your total income from all sources
                                        will be more than the personal tax credits you claimed on another TD1 form, check
                                        this box, enter &quot;0&quot; on line 13 on the front page and do not complete lines
                                        2 to 12.
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        Total income less than total claim amount
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Check this box if your total income for the year
                                        from all employers and payers will be less than your total claim amount on line
                                        13. Your employer or payer will not deduct tax from your earnings.
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        Non-residents
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        Are you a non-resident of Canada who will include 90% or more of your world income
                                        when determining your taxable income earned in Canada in 2012? If you are unsure
                                        of your residency status, call the International Tax Services Office at 1-800-267-5177.
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckBox3" runat="server" />
                                        &nbsp;&nbsp;&nbsp; If yes, complete the previous page.
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        If no, check the box, enter &quot;0&quot; on line 13 and do not complete lines 2
                                        to 12, as you are not entitled to the personal tax cre
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        Provincial or territorial personal tax credits return
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        If your claim amount on line 13 is more than $10,822, you also have to complete
                                        a provincial or territorial personal tax credit return. If you are an employee,
                                        use the TD1 form for your province or territory of employment. If you are a pensioner,
                                        use the TD1 form for your province or territory of residence. Your employer or payer
                                        will use both this federal form and your most recent provincial or territorial TD1
                                        form to determine the amount of your tax deductions
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        If you are claiming the basic personal amount only (your claim amount on line 13
                                        is $10,822), your employer or payer will deduct provincial or territorial taxes
                                        after allowing the provincial or territorial basic personal amount.
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        Note: If you are a Saskatchewan resident supporting children under 18 at any time
                                        during 2012, you may be able to claim the child amount on Form TD1SK, 2012 Saskatchewan
                                        Personal Tax Credits Return. Therefore, you may want to complete Form TD1SK even
                                        if you are only claiming the basic personal amount on this form.
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        Deduction for living in a prescribed zone
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        If you live in the Northwest Territories, Nunavut, Yukon, or another prescribed
                                        northern zone for more than six months in a row beginning or ending in 2012, you
                                        can claim:
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        $8.25 for each day that you live in the prescribed northern zone; or
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        $16.50 for each day that you live in the prescribed northern zone if, during that
                                        time, you live in a dwelling that you maintain, and you are the only person living
                                        in that dwelling who is claiming this deduction.
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        Employees living in a prescribed intermediate zone can claim 50% of the total of
                                        the above amounts.
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        For more information, get Form T2222, Northern Residents Deductions, and the Publication
                                        T4039, Northern Residents Deductions – Places in Prescribed Zones.
                                    </td>
                                    <td valign="bottom">
                                        <asp:TextBox ID="TextBox2" runat="server" MaxLength="50"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                            ErrorMessage="*" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                            ControlToValidate="TextBox2" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                            TargetControlID="TextBox2" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        Additional tax to be deducted
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        You may want to have more tax deducted from each payment, especially if you receive
                                        other income, including non-employment income such as CPP or QPP benefits, or Old
                                        Age Security pension. By doing this, you may not have to pay as much tax when you
                                        file your income tax return. To choose this option, state the amount of additional
                                        tax you want to have deducted from each payment. To change this deduction later,
                                        complete a new Form TD1.
                                    </td>
                                    <td valign="bottom">
                                        <asp:TextBox ID="TextBox1" runat="server" MaxLength="50"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                            ErrorMessage="*" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                            ControlToValidate="TextBox1" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                            TargetControlID="TextBox1" ValidChars="0147852369." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        Reduction in tax deductions
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        You can ask to have less tax deducted if on your income tax return you are eligible
                                        for deductions or non-refundable tax credits that are not listed on this form (for
                                        example, periodic contributions to a Registered Retirement Savings Plan (RRSP),
                                        child care or employment expenses, and charitable donations). To make this request,
                                        complete Form T1213, Request to Reduce Tax Deductions at Source for year(s)
                                        <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="TextBox20_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" TargetControlID="TextBox20" ValidChars="0147852369." />
                                        , to get a letter of authority from your tax services office. Give the letter of
                                        authority to your employer or payer. You do not need a letter of authority if your
                                        employer deducts RRSP contributions from your salary.
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        Certification
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        I certify that the information given in this return is, to the best of my knowledge,
                                        correct and complete
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="top">
                                        Signature :
                                        <asp:TextBox ID="TextBox17" runat="server" Width="226px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                            SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="TextBox17"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </td>
                                    <td align="right" class="style1">
                                        Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox16" runat="server" Width="98px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                                            ErrorMessage="*" ControlToValidate="TextBox16" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                            PopupButtonID="ImageButton2" TargetControlID="TextBox16">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox16"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 100%">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 100%" align="center">
                        <asp:Button ID="Button3" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="Button3_Click"
                            ValidationGroup="1" />
                        <asp:Button ID="Button4" runat="server" Text="Update" OnClick="Button4_Click" Visible="False"
                            ValidationGroup="1" CssClass="btnSubmit" />
                        <asp:Button ID="Button5" runat="server" Text="Cancel" OnClick="Button5_Click" CssClass="btnSubmit" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 100%">
                        <asp:Panel ID="Panel3" runat="server">
                            <table id="tbl132" style="width: 100%">
                                <tr>
                                    <td style="width:25%">
                                        <label>
                                            <asp:Label ID="Label9" runat="server" Text="Filter by Business Name"></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width:25%">
                                        <label>
                                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td style="width:25%">
                                        <label>
                                            <asp:Label ID="Label10" runat="server" Text="Filter by Batch Name"></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width:25%">
                                        <label>
                                            <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                                                AutoPostBack="True" Width="200px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:25%">
                                        <label>
                                            <asp:Label ID="Label11" runat="server" Text="Employee Name"></asp:Label>
                                        </label>
                                    </td>
                                    <td style="width:25%">
                                        <label>
                                            <asp:DropDownList ID="DropDownList3" runat="server" Width="200px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td style="width:25%">
                                        
                                    </td>
                                    <td style="width:25%">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:25%">
                                        <label>
                                            <asp:Label ID="Label12" runat="server" Text="From Date"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox18"
                                                ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="rghjk" runat="server" ErrorMessage="*" ControlToValidate="TextBox18"
                                                ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td style="width:25%">
                                        <label>
                                            <asp:TextBox ID="TextBox18" runat="server" Width="165px"></asp:TextBox>
                                        </label>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                                            TargetControlID="TextBox18">
                                        </cc1:CalendarExtender>
                                        <label>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                        </label>
                                    </td>
                                     <td style="width:25%">
                                        <label>
                                            <asp:Label ID="Label13" runat="server" Text="To Date"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox19"
                                                ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                                ErrorMessage="*" ControlToValidate="TextBox19" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td style="width:25%">
                                        <label>
                                            <asp:TextBox ID="TextBox19" runat="server" Width="165px"></asp:TextBox>
                                        </label>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="ImageButton3"
                                            TargetControlID="TextBox19">
                                        </cc1:CalendarExtender>
                                        <label>
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                        </label>
                                    </td>
                                </tr>
                              
                                <tr>
                                    <td colspan="2" style="width:30%">
                                    </td>
                                    <td colspan="2">
                                        <asp:Button ID="Button6" runat="server" Text="Go" CssClass="btnSubmit" OnClick="Button6_Click" ValidationGroup="2" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 100%">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            DataKeyNames="Id" Width="100%" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting"
                            OnSelectedIndexChanging="GridView1_SelectedIndexChanging" EmptyDataText="No Record Found.">
                            <Columns>
                                <asp:TemplateField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstrname123" runat="server" Text='<%# Eval("WName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblemp123" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Claim Amount" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalclaim123" runat="server" Text='<%# Eval("13TotalClaimAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldate123" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:ButtonField CommandName="vi" Text="Edit/View" ItemStyle-ForeColor="Black" 
                                    HeaderText="Edit/View" HeaderStyle-HorizontalAlign="Left" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle ForeColor="Black" />
                                </asp:ButtonField>
                               <%-- <asp:CommandField ShowDeleteButton="True" HeaderStyle-HorizontalAlign="Left" 
                                    HeaderText="Delete" ItemStyle-ForeColor="Black" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle ForeColor="Black" />
                                </asp:CommandField>--%>
                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton48" runat="server" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" 
                                            CommandArgument='<%# Eval("Id") %>' CommandName="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr" />
                            <AlternatingRowStyle CssClass="alt" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 100%">
                    </td>
                </tr>
                
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
