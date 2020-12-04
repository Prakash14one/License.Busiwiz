<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="TD1X.aspx.cs" Inherits="ShoppingCart_Admin_TD1X"
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

    <div class="products_box">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <fieldset>
                    <legend></legend>
                    <div style="padding-left: 1%">
                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                    </div>
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label><asp:DropDownList
                            ID="ddlstore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstore_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label2" runat="server" Text="Employee Name"></asp:Label><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlemployee" ErrorMessage="*"
                            SetFocusOnError="True" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                    <asp:DropDownList
                                ID="ddlemployee" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlemployee_SelectedIndexChanged">
                            </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label3" runat="server" Text="Tax Year"></asp:Label><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator6" runat="server" InitialValue="0" ControlToValidate="ddltaxyear"
                            ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                    <asp:DropDownList
                                ID="ddltaxyear" runat="server">
                            </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label4" runat="server" Text="Payroll Deduction Master"></asp:Label><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator7" runat="server" InitialValue="0" ControlToValidate="ddpayrolldeduction"
                            ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator><asp:DropDownList
                                ID="ddpayrolldeduction" runat="server" Width="400px">
                            </asp:DropDownList>
                    </label>
                </fieldset>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnlsh" runat="server" Visible="false">                <fieldset>
                    <legend></legend>
                    <label>
                        <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Canada Revenue Agency"></asp:Label></label>
                    <label>
                        <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Agence du revenu du Canada"></asp:Label></label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label7" runat="server" Text="STATEMENT OF COMMISSION INCOME AND EXPENSES FOR PAYROLL TAX DEDUCTIONS
ÉTAT DU REVENU ET DES DÉPENSES DE COMMISSIONS AUX FINS DES RETENUES SUR LA PAIE" Font-Size="12pt"></asp:Label>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label33" runat="server" Font-Bold="true" Text="Your employer will use this form and your Form TD1, Personal Tax Credits Return to determine the amount of your tax deductions."></asp:Label>
                        <asp:Label ID="Label64" runat="server" Font-Bold="true" Text="Votre employeur utilisera ce formulaire et votre formulaire TD1, Déclaration des crédits d'impôt personnels pour déterminer l'impôt à retenir."></asp:Label>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <table>
                            <tr>
                                <td style="width: 50%;">
                                    <asp:Label ID="Label16" runat="server" Font-Bold="true" Text="Complete this form if you are an employee and you receive
commission income or a combination of commission income and
salary or wages, and you want your employer to adjust your tax
deductions taking into account your commission expenses."></asp:Label>
                                </td>
                                <td style="width: 50%;">
                                    <asp:Label ID="Label17" runat="server" Font-Bold="true" Text="Remplissez ce formulaire si vous êtes un employé, que vous recevez
un revenu de commissions combiné ou non à un salaire ou à un
traitement et que vous désirez que votre employeur ajuste vos retenues
sur la paie pour tenir compte de vos dépenses liées aux commissions."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label18" runat="server" Font-Bold="true" Text="If you choose to complete this form, give it to your employer no
later than:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label19" runat="server" Font-Bold="true" Text="Si vous choisissez de remplir ce formulaire, remettez-le à votre
employeur au plus tard:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ul> <li>
                                        <asp:Label ID="Label20" runat="server" Font-Bold="true" Text="January 31, if you had the same employer last year;"></asp:Label>
                                    </li></ul>
                                </td>
                                <td>
                                  <ul>   <li>
                                        <asp:Label ID="Label21" runat="server" Font-Bold="true" Text="le 31 janvier, si vous aviez le même employeur l'année passée;"></asp:Label>
                                    </li></ul>
                                </td>
                            </tr>
                            <tr>
                                <td> <ul>
                                    <li>
                                        <asp:Label ID="Label8" runat="server" Font-Bold="true" Text="one month after you start employment with a new employer;"></asp:Label>
                                    </li></ul>
                                </td>
                                <td>
                                   <ul>  <li>
                                        <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="un mois après votre embauche chez un nouvel employeur;"></asp:Label>
                                    </li></ul>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                 <ul>
                                    <li>
                                        <asp:Label ID="Label10" runat="server" Font-Bold="true" Text="one month after the date of any change to your personal tax
credit amounts (as shown on your Form TD1); or"></asp:Label>
                                    </li>
                                    </ul>
                                </td>
                                <td> <ul>
                                    <li>
                                        <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="un mois après la date de toute modification de vos montants de
crédits d'impôt personnels (tels qu'ils apparaissent sur votre
formulaire TD1);"></asp:Label>
                                    </li>
                                    </ul>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <ul>
                                    <li>
                                        <asp:Label ID="Label12" runat="server" Font-Bold="true" Text="one month after your estimated total remuneration for the year
or your estimated commission expenses for the year changes
substantially."></asp:Label>
                                    </li>
                                    </ul>
                                </td>
                                <td>
                                 <ul>
                                    <li>
                                        <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="un mois après tout changement important de votre rémunération
totale estimative ou des dépenses liées aux commissions estimatives
auxquelles vous avez droit pour l'année."></asp:Label>
                                    </li>
                                    </ul>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Font-Bold="true" Text="If you choose not to complete this form, or if you inform your
employer in writing that you want to cancel a previously completed
Form TD1X, your employer will deduct tax from your pay using the
'Total claim amount' figure from your Form TD1."></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label15" runat="server" Font-Bold="true" Text="Si vous choisissez de ne pas remplir ce formulaire, ou si vous informez
votre employeur par écrit que vous voulez annuler un formulaire TD1X
déjà rempli, votre employeur retiendra l'impôt sur votre paie en se
basant sur le « Montant total de la demande » indiqué sur votre
formulaire TD1."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <fieldset>
                                        <legend></legend>
                                        <label>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label23" runat="server" Text="Last name – Nom de famille"></asp:Label>
                                                        <asp:Label ID="lbllastname" runat="server" CssClass="lblSuggestion" Font-Bold="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label22" runat="server" Text="First name and initial(s) – Prénom et initiale(s)"></asp:Label>
                                                        <asp:Label ID="lblfirstname" runat="server" CssClass="lblSuggestion" Font-Bold="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label24" runat="server" Text="Employee number – Numéro d'employé"></asp:Label>
                                                        <asp:Label ID="lblemid" runat="server" CssClass="lblSuggestion" 
                                                            Font-Bold="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label28" runat="server" Text="Address – Adresse"></asp:Label>
                                                        <asp:Label ID="lbladdress" runat="server" CssClass="lblSuggestion"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label35" runat="server" Text="SIN – NAS"></asp:Label>
                                                        <asp:Label ID="lblsocialno" CssClass="lblSuggestion" runat="server" Font-Bold="False"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </label>
                                        <%-- <label>
                       </label>
                      <label>  </label>
                    
                    <label>
                      </label>--%>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label29" runat="server" Font-Bold="true" Text="If you were not paid by commission last year, enter the estimated
amounts of your remuneration and expenses for this year."></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label30" runat="server" Font-Bold="true" Text="Si vous n'étiez pas employé à commission l'année passée, inscrivez vos
montants estimatifs de rémunération et de dépenses pour cette année."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label34" runat="server" Font-Bold="true" Text="If you were paid by commission at any time last year or this year,
enter either last year's remuneration and commission expenses
amounts, or the estimated amounts of your remuneration and
commission expenses for this year."></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label36" runat="server" Font-Bold="true" Text="Si vous étiez employé à commission à un moment donné l'année
passée ou cette année, inscrivez soit vos montants de rémunération
et de dépenses liées aux commissions pour l'année passée ou vos
montants estimatifs de rémunération et de dépenses liées aux
commissions pour cette année."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label37" runat="server" Font-Bold="true" Text="For information about allowable expenses, see Guide T4044,
Employment Expenses, and Form T777, Statement of
Employment Expenses, which you can get on our Web site at
www.cra.gc.ca/forms or by calling 1-800-959-2221."></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label38" runat="server" Font-Bold="true" Text="Pour avoir des renseignements sur les dépenses admissibles,
consultez le guide T4044, Dépenses d'emploi, et le formulaire
T777, État des dépenses d'emploi. Vous pouvez obtenir ces
publications sur notre site Web à www.arc.gc.ca/formulaires ou en
composant le 1-800-959-3376."></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </label>
                    <div style="clear: both;">
                    </div>
                </fieldset>
               </asp:Panel>

                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend></legend>
                    <label>
                      <asp:Label ID="Label39" runat="server" Font-Bold="true" Text="Calculation of your estimated annual net commission income"></asp:Label>
                     <br /><asp:Label ID="Label40" runat="server" Font-Bold="true" Text="Calcul de votre revenu net estimatif de commissions pour l'année"></asp:Label>
                  
                </label>
                <div style="clear: both;">
                </div>
                    <label>
                        <table>
                            <tr>
                                <td >
                                        <asp:Label ID="Label41" runat="server" Font-Bold="true" Text="Commissions"></asp:Label>
                   
                                </td>
                               <td >
                                     
                                </td>
                                <td align="left">
                                  
                                        <asp:TextBox ID="txtcommision" runat="server" Width="120px" MaxLength="20"></asp:TextBox>
                                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                        FilterType="Custom, Numbers" TargetControlID="txtcommision" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                 <td align="left">
                                  
                                                 </td>
                                <td align="left">
                                  
                                                 </td>
                            </tr>
                             <tr>
                                <td >
                                        <asp:Label ID="Label42" runat="server" Font-Bold="true" Text="__________________________________________________"></asp:Label>
                   
                                </td>
                             <td></td>
                                <td align="left">
                                  
                                        <asp:Label ID="lbs" runat="server" Text="_______________" ></asp:Label>
                                </td>
                                 <td align="left">
                                  
                                                 </td>
                                                  <td align="left">
                                  
                                                 </td>
                            </tr>
                             <tr>
                                <td >
                                        <asp:Label ID="Label43" runat="server" Font-Bold="true" Text="Salary or wages (if it applies)"></asp:Label>
                   <br />
                    <asp:Label ID="Label46" runat="server" Font-Bold="true" Text="Salaire ou traitement (s'il y a lieu)"></asp:Label>
                   
                                </td>
                                <td ><br />
                                        <asp:Label ID="Label47" runat="server" Font-Bold="true" Text="+"></asp:Label>
                   
                                </td>
                                <td align="left">
                                  <br />
                                        <asp:TextBox ID="txtsalarywages" runat="server" Width="120px" 
                                        MaxLength="20"></asp:TextBox>
                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                        FilterType="Custom, Numbers" TargetControlID="txtsalarywages" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td></td>
                                <td align="left">
                                  
                                                 </td>
                            </tr>
                             <tr>
                                <td >
                                        <asp:Label ID="Label44" runat="server" Font-Bold="true" Text="__________________________________________________"></asp:Label>
                   
                                </td>
                              <td ><br /></td>
                                <td align="left">
                                  
                                        <asp:Label ID="Label45" runat="server" Text="_______________" ></asp:Label>
                                </td>
                                 <td align="left">
                                  
                                                 </td>
                                                 <td></td>
                            </tr>
                             <tr>
                                <td >
                                        <asp:Label ID="Label48" runat="server" Font-Bold="true" Text="Total remuneration"></asp:Label>
                   <br />
                    <asp:Label ID="Label49" runat="server" Font-Bold="true" Text="Rémunération totale"></asp:Label>
                   
                                </td>
                                <td ><br />
                                        <asp:Label ID="Label50" runat="server" Font-Bold="true" Text="="></asp:Label>
                   
                                </td>
                                <td align="left">
                                  <br />
                                        <asp:TextBox ID="txttotalrem" runat="server" Width="120px" Enabled="False"></asp:TextBox>
                                </td>
                                <td><br />>> </td>
                                <td   align="right">
                                    <br />
                                       <asp:TextBox ID="txtgrandtotal" runat="server" Width="120px" Enabled="False"></asp:TextBox>
                            
                                                 </td>
                            </tr>
                             <tr>
                                <td style="width: 60%;">
                                        <asp:Label ID="Label51" runat="server" Font-Bold="true" Text="__________________________________________________"></asp:Label>
                   
                                </td>
                              <td ><br /></td>
                                <td align="left">
                                  
                                        <asp:Label ID="Label52" runat="server" Text="_______________" ></asp:Label>
                                </td>
                                <td></td>
                                 <td align="right">
                                  
                                                
                                                 <asp:Label ID="Label65" runat="server" Text="_______________"></asp:Label>
                                                 
                                  
                                                 </td>
                            </tr>
                            
                             <tr>
                                <td colspan="3" >
                                        <asp:Label ID="Label53" runat="server" Font-Bold="true" Text="Subtract last year's commission expenses from Form T777 or this year's estimated commission expenses."></asp:Label>
                   <br />
                    <asp:Label ID="Label54" runat="server" Font-Bold="true" Text="Moins vos dépenses liées aux commissions de l'année passée selon le formulaire T777 ou vos dépenses
liées aux commissions estimatives pour cette année."></asp:Label>
                   
                                </td>
                               
                                
                                <td align="right">  <br />  <asp:Label ID="Label55" runat="server" Font-Bold="true" Text="-"></asp:Label></td>
                                <td   align="right">
                                    <br />
                                       <asp:TextBox ID="txtsublastcomm" runat="server" Width="120px" MaxLength="20"></asp:TextBox>
                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                                        FilterType="Custom, Numbers" TargetControlID="txtsublastcomm" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                 </td>
                            </tr>
                             <tr>
                                <td colspan="3">
                                        <asp:Label ID="Label56" runat="server" Font-Bold="True" 
                                            Text="______________________________________________________________________________________________"></asp:Label>
                   
                                </td>
                             
                                <td></td>
                                 <td align="right">
                                  
                                                
                                                 <asp:Label ID="Label66" runat="server" Text="_______________"></asp:Label>
                                                 
                                  
                                                 </td>
                            </tr>
                            
                               <tr>
                                <td colspan="3" >
                                        <asp:Label ID="Label57" runat="server" Font-Bold="true" Text="Estimated annual net commission income"></asp:Label>
                   <br />
                    <asp:Label ID="Label67" runat="server" Font-Bold="true" Text="Revenu net estimatif de commissions pour l'année"></asp:Label>
                   
                                </td>
                               
                                
                                <td align="right">  <br />  <asp:Label ID="Label68" runat="server" Font-Bold="true" Text="="></asp:Label></td>
                                <td   align="right">
                                    <br />
                                       <asp:TextBox ID="txtgrantcomencome" runat="server" Width="120px" 
                                        Enabled="False"></asp:TextBox>
                              <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                                        FilterType="Custom, Numbers" TargetControlID="txtgrantcomencome" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                 </td>
                            </tr>
                             <tr>
                                <td colspan="3">
                                        <asp:Label ID="Label69" runat="server" Font-Bold="True" 
                                            Text="______________________________________________________________________________________________"></asp:Label>
                   
                                </td>
                             
                                <td></td>
                                 <td align="right">
                                  
                                                
                                                 <asp:Label ID="Label70" runat="server" Text="_______________"></asp:Label>
                                                 
                                  
                                                 </td>
                            </tr>
                        </table>
                    </label>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label58" runat="server" Text="Certification – Attestation"></asp:Label></legend>
                    <label>
                        <asp:Label ID="Label59" runat="server" 
                        Text="I certify that the information given on this form is, to the best of my knowledge, correct and complete."></asp:Label>
                    <br />  <asp:Label ID="Label71" runat="server" 
                        Text="J'atteste que les renseignements fournis dans ce formulaire sont, à ma connaissance, exacts et complets."></asp:Label>
                        </label>
                    
                    <table>
                        <tr>
                            <td>
                               
                                  <asp:Label ID="Label60" runat="server"
                                        Text="Signature"></asp:Label> 
                            </td>
                            <td style="width:70%;" >
                                <asp:TextBox ID="txtsign" runat="server" Width="200px" MaxLength="200"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExplidator2" runat="server" 
                                        ErrorMessage="*" Display="Dynamic"
                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" 
                                        ControlToValidate="txtsign" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>     
                            </td>
                            <td>
                            <asp:Label ID="Label61" runat="server"
                                            Text="Date"></asp:Label>
                            </td>
                            <td align="left">
                             
                                    <asp:TextBox ID="txtdate" runat="server" Width="70px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdate" ErrorMessage="*"
                                        ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton2"
                                    TargetControlID="txtdate">
                                </cc1:CalendarExtender>
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                            </td>
                        </tr>
                        <tr>
                        <td></td>
                            <td colspan="4">
                                <label>
                                    <asp:Label ID="Label72" runat="server"
                                        Text="It is a serious offence to make a false statement."></asp:Label> <br />
                                        <asp:Label ID="Label73" runat="server"
                                        Text="Faire une fausse déclaration constitue une infraction grave."></asp:Label>
                                </label>
                            </td>
                        </tr>
                        
                        <tr>
                        
                            <td colspan="5">
                                <label>
                                    <asp:Button ID="Button3" runat="server" Text="Submit" OnClick="Button3_Click" ValidationGroup="1" />
                                    &nbsp;<asp:Button ID="Button4" runat="server" Text="Update" OnClick="Button4_Click"
                                        Visible="False" ValidationGroup="1" />
                                    &nbsp;<asp:Button ID="Button5" runat="server" Text="Cancel" OnClick="Button5_Click" />
                                </label>
                            </td>
                        </tr>
                    </table>
                    </label>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label25" runat="server" Text="List of estimated annual net commission income"></asp:Label>
                    </legend>
                    <label>
                        <asp:Label ID="lblfil" runat="server" Text="Business Name :"></asp:Label>
                        <asp:DropDownList ID="ddlfilterbus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfilterbus_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label26" runat="server" Text="Employee Name :"></asp:Label>
                        <asp:DropDownList ID="ddlfilteremp" runat="server" OnSelectedIndexChanged="ddlfilteremp_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label62" runat="server" Text="Text Year :"></asp:Label>
                        <asp:DropDownList ID="ddlfilteryear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfilteryear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label63" runat="server" Text="Payroll Deduction Master :"></asp:Label>
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                        <asp:DropDownList ID="ddlfilterdedctionname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfilterdedctionname_SelectedIndexChanged"
                            Width="500px">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="Button1" CssClass="btnSubmit" runat="server" Text="Printable Version"
                                OnClick="Button1_Click" />
                            <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" visible="False" />
                        </label>
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table id="Table2" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
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
                                                    <asp:Label ID="Label32" runat="server" Font-Size="18px" Text="List of estimated annual net commission income"></asp:Label>
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
                                        CellPadding="4" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        Width="100%" OnRowEditing="GridView1_RowEditing" AllowSorting="True" OnSorting="GridView1_Sorting"
                                        OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Date" ItemStyle-Width="5%" SortExpression="Date" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldme" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Business Name" ItemStyle-Width="15%" SortExpression="Wname"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbname" runat="server" Text='<%# Eval("Wname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="15%" SortExpression="EmployeeName"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Year" ItemStyle-Width="6%" SortExpression="TaxYear_Name"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblno" runat="server" Text='<%# Eval("TaxYear_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Estimated Annual Comm.Income" ItemStyle-Width="6%" SortExpression="TotalAmt"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblano" runat="server" Text='<%# Eval("EstannNetCommIncome") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowSelectButton="true" HeaderImageUrl="~/Account/images/edit.gif"
                                                HeaderText="Edit" ButtonType="Image" SelectImageUrl="~/Account/images/edit.gif"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                <ItemStyle HorizontalAlign="Left" Width="2%"></ItemStyle>
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnconfor" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA"
                    BorderStyle="Outset" Width="600px">
                    <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="">
                                Message....
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblddffg" runat="server" Text="Form can not be submitted , because you have not filled amount in any field."
                                    ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label31" runat="server" Text="Please enter some amount in any of the field to submit this form."
                                    ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btns" runat="server" BackColor="#CCCCCC" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button2" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                    ID="ModapupExtende22" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btns"
                    PopupControlID="pnconfor" TargetControlID="Button2">
                </cc1:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
