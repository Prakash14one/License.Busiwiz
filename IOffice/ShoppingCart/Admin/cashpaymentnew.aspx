<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="cashpaymentnew.aspx.cs" Inherits="ShoppingCart_Admin_cashpaymentnew"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function mask(evt, max_len) {



            if (evt.keyCode == 13) {

                return true;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }




        }
        function Setlbl() {
            alert(document.getElementById('<%=txtmemo1.ClientID%>').innerHTML);
            document.getElementById('<%=txtmemo1.ClientID%>').innerHTML = document.getElementById('<%=txtmmo.ClientID%>').innerHTML;
        }
        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');

                alert("You have entered11 an invalid character");

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left:1%">
                    <asp:Label ID="lblmsg" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                <legend></legend>
                <div>
                 <table style="width: 100%" >
                            <tr> <td ></td>
                             <td style="width: 200px" align="right">
                              <label>
                                        <asp:Label ID="Label36" runat="server" Text="View Previous Entries"></asp:Label>
                                    </label>
                                </td>
                                <td  align="right" style="width: 10%">
                                <asp:CheckBox ID="chkdc" runat="server" AutoPostBack="true" 
                                        oncheckedchanged="chkdc_CheckedChanged" />
                                </td>
                                <td align="right">
                                <asp:Button ID="btncashre" runat="server" Text="Bank Transactions/Bank Book"  
                                        CssClass="btnSubmit" onclick="btncashre_Click"/>
                                </td>
                                </tr>
                                </table>
                </div>
                    <div>
                    <asp:Panel ID="pnldc" runat="server" Visible="false">
                        <table>
                            <tr>
                                <td style="width: 25%">
                                </td>
                                <td >
                                    <label>
                                        <asp:Button ID="btnprivious" Text="<<" runat="server" CssClass="btnSubmit"
                                            OnClick="btnprivious_Click" />
                                    </label>
                                    </td><td>
                                    <label>
                                        <asp:Label ID="Label38" runat="server" Text="Search "></asp:Label>
                                    </label>
                                      </td><td>
                                    <label>
                                        <asp:TextBox ID="txtserachno" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtserachno" Display="Dynamic"
                                            ErrorMessage="*" SetFocusOnError="true" ValidationGroup="3"></asp:RequiredFieldValidator>
                                    </label>
                                      </td><td>
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="From"></asp:Label>
                                    </label>
                                      </td><td>
                                    <label>
                                        <asp:Label ID="lbltotentry" runat="server"></asp:Label>
                                    </label>
                                     </td><td>
                                    <label>
                                        <asp:Button ID="btnnext" Text=">>" runat="server" CssClass="btnSubmit" OnClick="btnnext_Click" />
                                    </label>
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td></td>
                        <td align="center"> 
                                        <asp:Button ID="btngoservh" Text="Go" runat="server" CssClass="btnSubmit" OnClick="btngoservh_Click"
                                            ValidationGroup="3" />
                                  </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <asp:Panel ID="pnnl" Visible="false" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="dd" Text="Cash Payment Entry For the Following Documents" runat="server"
                                                Font-Bold="True"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label1" runat="server" Text="Doc ID"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label3" runat="server" Text="Title"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label4" runat="server" Text="Date"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label5" runat="server" Text="Cabinet/Drower/Folder"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <div>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="Entry Number"></asp:Label>
                                    </label>
                                </td>
                                <td >
                                    <label>
                                        <asp:Label ID="lblentrynumber" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td >
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Entry Type"></asp:Label>
                                    </label>
                                </td>
                                <td >
                                    <label>
                                        <asp:Label ID="lblEntryType" runat="server" Text="Cash Payment"></asp:Label>
                                    </label>
                                </td>
                                <td  >
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Date"></asp:Label>
                                        <asp:Label ID="Label32" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtTodate"
                                            Display="Dynamic" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtTodate"
                                            ValidationGroup="2">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtTodate" runat="server" Width="70px" AutoPostBack="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtTodate_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="txtTodate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="TextBox1_MaskedEditExtender1" runat="server" CultureName="en-AU"
                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtTodate" />
                                    </label>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Business Name"></asp:Label>
                                        <asp:Label ID="Label33" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlwarehouse"
                                            ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td >
                                    <label>
                                        <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text="Cash/Bank Account"></asp:Label>
                                        <asp:Label ID="Label34" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAccount"
                                            Display="Dynamic" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtpayamount"
                                            ValidationGroup="2">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlAccount" runat="server" AutoPostBack="True" 
                                            OnSelectedIndexChanged="ddlAccount_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="lnkadd" runat="server" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                            Height="20px" ImageAlign="Bottom" Width="20px" ToolTip="AddNew" OnClick="lnkadd_Click"
                                            />
                                             </label>
                                             <label>
                                        <asp:ImageButton ID="lnkadd0" runat="server" Height="20px" Width="20px" ToolTip="Refresh"
                                            ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="lnkadd0_Click" />
                                    </label>
                                </td>
                                <td >
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Balance as on"></asp:Label>
                                          
                                    </label>
                                    <label>
                                    <asp:Label ID="lblbdate" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td >
                                    <label>
                                        <asp:Label ID="lblbalanceason" runat="server" Text="0000000"></asp:Label>
                                    </label>
                                     <label>
                                        <asp:ImageButton ID="lnkadd3" runat="server" Height="20px" ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                            OnClick="lnkadd3_Click" ToolTip="Refresh Balance" Width="20px" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label21" runat="server" Text="Payment Amount"></asp:Label>
                                        <asp:Label ID="Label55" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpayamount" Display="Dynamic"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtpayamount" Display="Dynamic"
                                            ValidationGroup="2">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtpayamount" Display="Dynamic"
                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td colspan="5" >
                                    <label>
                                        <asp:TextBox ID="txtpayamount" runat="server" OnTextChanged="txtpayamount_TextChanged"
                                            MaxLength="30" onkeyup="return mak('Span2',30,this)"></asp:TextBox>
                                              <cc1:FilteredTextBoxExtender ID="txtEndzip_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" TargetControlID="txtpayamount" ValidChars="0123456789-.">
                                    </cc1:FilteredTextBoxExtender>
                                    </label>
                                  
                               
                                    <label>
                                         <asp:Label ID="Label23" runat="server" Text="Max "  CssClass="labelcount"></asp:Label><span id="Span2" class="labelcount">30</span>
                                        <asp:Label ID="Label28" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label></label></td>
                               
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Memo
                                    </label>
                                </td>
                                <td  colspan="5">
                                    <label>
                                        <asp:TextBox ID="txtmmo" runat="server" Width="610px" 
                                        onKeydown="return mask(event)"  
                                       onkeyup="return check(this,/[\\/!@#$%^'&*>+:;={}()[]|\/]/g,/^[\a-zA-Z.0-9_\s]+$/,'div1',500)"
                                        Height="56px" TextMode="MultiLine" AutoPostBack="True" 
                                        ontextchanged="txtmmo_TextChanged" ></asp:TextBox></label><label><asp:Label ID="Label22" runat="server" Text="Max "  CssClass="labelcount"></asp:Label><span id="div1" class="labelcount">500</span>
                                        <asp:Label ID="Label29" runat="server" Text="(a-z 0-9 _ .)" CssClass="labelcount">
 </asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td  colspan="4">
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Make payment to user on account</asp:ListItem><asp:ListItem Value="0">Make payment entry to record expenses</asp:ListItem></asp:RadioButtonList></td>
                                 <td  colspan="2">
                                 <label>
                                    <asp:CheckBox ID="chktrackcost" runat="server" AutoPostBack="True" Text="Track cost for the Project"
                                        OnCheckedChanged="chktrackcost_CheckedChanged" Visible="False" />
                                        </label>
                                </td>
                              
                            </tr>
                            
                              <tr>
                                <td  colspan="6">
                                    <asp:Panel ID="pnltcost" Visible="false" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 51%" colspan="6">
                                            <label>
                                                <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Text="Business" Value="0"></asp:ListItem><asp:ListItem Text="Department" Value="1"></asp:ListItem><asp:ListItem Text="Division" Value="2"></asp:ListItem><asp:ListItem Text="Employee" Value="3"></asp:ListItem></asp:RadioButtonList></label></td>
                                        <td style="width: 34%">
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                        <td style="width: 17%">
                                            <label>
                                                <asp:Label ID="lblwname" runat="server" Text="Business Name"></asp:Label></label></td><td style="width: 17%">
                                            <label>
                                                <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstore_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td style="width: 17%">
                                        </td>
                                        <td style="width: 17%">
                                        </td>
                                        <td style="width: 17%">
                                        </td>
                                        <td style="width: 17%">
                                        </td>
                                    </tr>
                                    <asp:Panel ID="pnldiv" runat="server" Width="100%" Visible="False">
                                        <tr>
                                            <td style="width: 17%">
                                                <label>
                                                    <asp:Label ID="Label17" runat="server" Text="Division Name"></asp:Label></label></td><td style="width: 17%">
                                                <label>
                                                    <asp:DropDownList ID="ddlbusiness" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td style="width: 17%">
                                            </td>
                                            <td style="width: 17%">
                                            </td>
                                            <td style="width: 17%">
                                            </td>
                                            <td style="width: 17%">
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlemp" runat="server" Width="100%" Visible="False">
                                        <tr>
                                            <td style="width: 17%">
                                                <label>
                                                    <asp:Label ID="Label18" runat="server" Text="Employee Name"></asp:Label></label></td><td style="width: 17%">
                                                <label>
                                                    <asp:DropDownList ID="ddlemployee" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td style="width: 17%">
                                            </td>
                                            <td style="width: 17%">
                                            </td>
                                            <td style="width: 17%">
                                            </td>
                                            <td style="width: 17%">
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td style="width: 17%">
                                            <label>
                                                <asp:Label ID="Label19" runat="server" Text="Project Name"></asp:Label><asp:Label ID="Label35" runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlproject"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator></label></td><td style="width: 17%">
                                            <label>
                                                <asp:DropDownList ID="ddlproject" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlproject_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td style="width: 17%">
                                            <asp:Button ID="btnsall" runat="server" CssClass="btnSubmit" OnClick="btnsall_Click"
                                                Text="Show All" />
                                        </td>
                                        <td style="width: 17%">
                                        </td>
                                        <td style="width: 17%">
                                        </td>
                                        <td style="width: 17%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 17%">
                                            <label>
                                                <asp:Label ID="Label20" runat="server" Text="Status"></asp:Label></label></td><td style="width: 17%">
                                            <label>
                                                <asp:DropDownList ID="ddlstatus" runat="server">
                                                    <asp:ListItem Selected="True" Value="Pending">Pending</asp:ListItem><asp:ListItem Value="Completed">Completed</asp:ListItem></asp:DropDownList></label></td><td style="width: 17%">
                                        </td>
                                        <td style="width: 17%">
                                        </td>
                                        <td style="width: 17%">
                                        </td>
                                        <td style="width: 17%">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                         
                               </td>
                               </tr>
                        <tr>
                        
                        <td colspan="6">
                           <asp:Panel ID="pnlparty" runat="server">
                           <table>
                                <tr>
                                    <td >
                                        <label>
                                            <asp:Label ID="lblparttyp" Text="User Type" runat="server"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlpartytypetype"
                                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="1" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator></label></td><td >
                                        <label>
                                            <asp:DropDownList ID="ddlpartytypetype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpartytypetype_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td >
                                        <label>
                                            <asp:Label ID="lblpartname" Text="User Name" runat="server"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlpartynamename"
                                                Display="Dynamic" ErrorMessage="*" InitialValue="0"  SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator></label></td>
                                              <td>
                                        <label>
                                            <asp:DropDownList ID="ddlpartynamename" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpartynamename_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td >
                                        <label>
                                            <asp:ImageButton ID="LinkButton97666667" runat="server" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                Height="20px" ImageAlign="Bottom" Width="20px" ToolTip="AddNew" OnClick="LinkButton97666667_Click1" />
                                               </label> <label>
                                            <asp:ImageButton ID="LinkButton1" runat="server" Height="20px" Width="20px" ToolTip="Refresh"
                                                ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton1_Click" />
                                        </label>
                                    </td>
                                    <td >
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            <asp:CheckBox ID="chkappamount" runat="server" Text="Apply this amount to unpaid invoices"
                                                AutoPostBack="True" OnCheckedChanged="chkappamount_CheckedChanged" Visible="False" />
                                        </label>
                                    </td>
                                    <td  colspan="4">
                                        <label>
                                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Checked="true" Text="Select invoices to apply this payment"
                                                OnCheckedChanged="CheckBox1_CheckedChanged" Visible="False" />
                                        </label>
                                   
                                </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        </tr>
                            <tr>
                                <td  colspan="6">
                                    <label>
                                        <asp:CheckBox ID="chkdoc" Text="Do you want to attach/ upload documents after the submission of the entry ?"
                                            runat="server" />
                                    </label>
                                </td>
                               
                            </tr>
                              <tr>
                                <td  colspan="6">
                                    <label>
                                         <asp:CheckBox ID="chkappentry" runat="server" Text="Approved for this entry" 
                                         Visible="False" />
                                    </label>
                                </td>
                               
                            </tr>
                              </tr>
                            <tr>
                                <td  colspan="6">
                                  <asp:Panel ID="Panel4" runat="server" GroupingText="Make payment to user on account"
                    Visible="False">
                    <table style="width: 100%">
                        <tr>
                            <td colspan="6">
                                <asp:GridView ID="GridView2" runat="server" EmptyDataText="No Records Found" AutoGenerateColumns="false"
                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    Visible="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkbox" runat="server" Checked="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date")%>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="EntryType" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblentrytype" runat="server" Text='<%# Bind("EntryType")%>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="EntryNo" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblentryNo" runat="server" Text='<%# Bind("EntryNo")%>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblentrytypeid" runat="server" Text='<%# Bind("EntryTypeId")%>' Visible="false"></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltranid" runat="server" Text='<%# Bind("TransactionId")%>' Visible="false"></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="OriginalAmount" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltrnamt" runat="server" Text='<%# Bind("TranAmount")%>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="AmountDue" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbaldue" runat="server" Text='<%# Bind("DueBalance")%>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnewamt" runat="server"></asp:TextBox><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1455" runat="server" Enabled="True"
                                                    TargetControlID="txtnewamt" ValidChars="0123456789.">
                                                </cc1:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            
                            <td  colspan="6" align="center">
                                <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" Text="Submit"
                                    ValidationGroup="1" CssClass="btnSubmit" />
                                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Cancel" CssClass="btnSubmit" />
                                <asp:Button ID="btnupdatepaety" runat="server" OnClick="btnupdatepaety_Click" Text="Update"
                                    Visible="False" CssClass="btnSubmit" />
                                <asp:Button ID="btneditgene" runat="server" OnClick="btneditgene_Click" Text="Edit"
                                    Visible="False" CssClass="btnSubmit" />
                            </td>
                          
                        </tr>
                    </table>
                </asp:Panel>
                                </td>
                                </tr>
                                
                                <tr>
                                <td  colspan="6">
                                 <asp:Panel ID="Panel3" runat="server" GroupingText="Make payment entry to record Expenses"
                    Visible="False">
                  
                                <table style="width: 100%">
                                    <tr>
                                        <td >
                                            <label>
                                                <asp:Label ID="Label24" runat="server" Text="Select Account"></asp:Label></label><label><asp:ImageButton ID="lnkadd1" runat="server" Height="20px" ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                    OnClick="lnkadd_Click" ToolTip="AddNew" ValidationGroup="2" Width="20px" />
                                                <asp:ImageButton ID="lnkadd2" runat="server" Height="20px" ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                                    OnClick="lnkadd2_Click" ToolTip="Refresh" Width="20px" />
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label30" runat="server" Text="Amount (Max 30, 0-9 .)"></asp:Label></label></td><td>
                                            <label>
                                                <asp:Label ID="Label31" runat="server" Text="Memo (Max 300, A-Z, 0-9 . _)"></asp:Label></label></td></tr>
                                     <tr>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlAccountname1" runat="server" AutoPostBack="True" Width="400px"
                                                    OnSelectedIndexChanged="ddlAccountname1_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtAmount1" runat="server" 
                                                ontextchanged="txtAmount1_TextChanged" AutoPostBack="true"></asp:TextBox></label><label><asp:Label ID="lblamt1" runat="server" ForeColor="Red"></asp:Label></label>
                                                </td><td>
                                            <label>
                                                <asp:TextBox ID="txtmemo1" runat="server" Width="500px" MaxLength="300" ></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1112" runat="server"
                                                    ErrorMessage="Invalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                    ControlToValidate="txtmemo1" ValidationGroup="1">
 </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlAccountname2" runat="server" AutoPostBack="True" Width="400px"
                                                    OnSelectedIndexChanged="ddlAccountname2_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtAmount2" runat="server"  ontextchanged="txtAmount1_TextChanged" AutoPostBack="true"  ></asp:TextBox></label><label><asp:Label ID="lblamt2" runat="server" ForeColor="Red"></asp:Label></label></td><td>
                                            <label>
                                                <asp:TextBox ID="txtmemo2" runat="server" Width="500px" MaxLength="300"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1546" runat="server"
                                                    ErrorMessage="Invalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                    ControlToValidate="txtmemo2" ValidationGroup="1">
 </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlAccountname3" runat="server" AutoPostBack="True" Width="400px"
                                                    OnSelectedIndexChanged="ddlAccountname3_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtAmount3" runat="server" ontextchanged="txtAmount1_TextChanged" AutoPostBack="true"  ></asp:TextBox></label><label><asp:Label ID="lblamt3" runat="server" ForeColor="Red"></asp:Label></label></td><td>
                                            <label>
                                                <asp:TextBox ID="txtmemo3" runat="server" Width="500px" MaxLength="300"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                    ControlToValidate="txtmemo3" ValidationGroup="1">
 </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlAccountname4" runat="server" AutoPostBack="True" Width="400px"
                                                    OnSelectedIndexChanged="ddlAccountname4_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtAmount4" runat="server" ontextchanged="txtAmount1_TextChanged" AutoPostBack="true"  ></asp:TextBox></label><label><asp:Label ID="lblamt4" runat="server" ForeColor="Red"></asp:Label></label></td><td>
                                            <label>
                                                <asp:TextBox ID="txtmemo4" runat="server" Width="500px" MaxLength="300"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                    ControlToValidate="txtmemo4" ValidationGroup="1">
 </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlAccountname5" runat="server" AutoPostBack="True" Width="400px"
                                                    OnSelectedIndexChanged="ddlAccountname5_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtAmount5" runat="server" ontextchanged="txtAmount1_TextChanged" AutoPostBack="true"  ></asp:TextBox></label><label><asp:Label ID="lblamt5" runat="server" ForeColor="Red"></asp:Label></label></td><td>
                                            <label>
                                                <asp:TextBox ID="txtmemo5" runat="server" Width="500px" MaxLength="300"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                    ControlToValidate="txtmemo5" ValidationGroup="1">
 </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlAccountname6" runat="server" AutoPostBack="True" Width="400px"
                                                    OnSelectedIndexChanged="ddlAccountname6_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtAmount6" runat="server" ontextchanged="txtAmount1_TextChanged" AutoPostBack="true"  ></asp:TextBox></label><label><asp:Label ID="lblamt6" runat="server" ForeColor="Red"></asp:Label></label></td><td>
                                            <label>
                                                <asp:TextBox ID="txtmemo6" runat="server" Width="500px" MaxLength="300"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                    ControlToValidate="txtmemo6" ValidationGroup="1">
 </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlAccountname7" runat="server" AutoPostBack="True" Width="400px"
                                                    OnSelectedIndexChanged="ddlAccountname7_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtAmount7" runat="server" ontextchanged="txtAmount1_TextChanged" AutoPostBack="true"  ></asp:TextBox></label><label><asp:Label ID="lblamt7" runat="server" ForeColor="Red"></asp:Label></label></td><td>
                                            <label>
                                                <asp:TextBox ID="txtmemo7" runat="server" Width="500px" MaxLength="300"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                    ControlToValidate="txtmemo7" ValidationGroup="1">
 </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlAccountname8" runat="server" AutoPostBack="True" Width="400px"
                                                    OnSelectedIndexChanged="ddlAccountname8_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtAmount8" runat="server" ontextchanged="txtAmount1_TextChanged" AutoPostBack="true"  ></asp:TextBox></label><label><asp:Label ID="lblamt8" runat="server" ForeColor="Red"></asp:Label></label></td><td>
                                            <label>
                                                <asp:TextBox ID="txtmemo8" runat="server" Width="500px" MaxLength="300"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                    ControlToValidate="txtmemo8" ValidationGroup="1">
 </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlAccountname9" runat="server" AutoPostBack="True" Width="400px"
                                                    OnSelectedIndexChanged="ddlAccountname9_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtAmount9" runat="server"  ></asp:TextBox></label><label><asp:Label ID="lblamt9" runat="server" ForeColor="Red"></asp:Label></label></td>
                                         <td>
                                            <label>
                                                <asp:TextBox ID="txtmemo9" runat="server" Width="500px" MaxLength="300"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                                    ControlToValidate="txtmemo9" ValidationGroup="1">
 </asp:RegularExpressionValidator>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label27" runat="server" Text="Grand Total"></asp:Label></label></td><td>
                                            <label>
                                                <asp:Label ID="lblgtotal" runat="server"></asp:Label></label></td><td>
                                        </td>
                                    </tr>
                                       <tr>
                            
                            <td align="center" colspan="3">
                                <asp:Button ID="btnsubradio2" runat="server" OnClick="btnsubradio2_Click" Text="Submit"
                                    ValidationGroup="2" CssClass="btnSubmit" />
                                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Cancel" CssClass="btnSubmit" />
                                <asp:Button ID="btnupdateAccc" runat="server" OnClick="btnupdateAccc_Click" Text="Update"
                                    Visible="False" ValidationGroup="2" CssClass="btnSubmit" />
                                <asp:Button ID="btnedit" runat="server" Text="Edit" Visible="False" OnClick="btnedit_Click"
                                    CssClass="btnSubmit" />
                            </td>
                           
                        </tr>
                           <tr>
                            <td colspan="3">
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                    TargetControlID="txtAmount1" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                    TargetControlID="txtAmount2" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                    TargetControlID="txtAmount3" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                    TargetControlID="txtAmount4" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                    TargetControlID="txtAmount5" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                    TargetControlID="txtAmount6" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                    TargetControlID="txtAmount7" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                    TargetControlID="txtAmount8" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                    TargetControlID="txtAmount9" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                                </table>
                          </asp:Panel>
                                </td>
                                </tr>
                        </table>
                    </div>
                </fieldset>
              
                <div style="clear: both;">
                </div>
               
            </div>
            <table id="subinnertbl" style="width: 100%">
                <tr>
                    <td>
                        <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Outset"
                            Width="300px">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblm" runat="server" ForeColor="Black">Please check your date.</asp:Label></td></tr><tr>
                                    <td  style="height: 26px">
                                        <asp:Label ID="Label2" runat="server" ForeColor="Black" Text="The start date of the Year is "></asp:Label><asp:Label ID="lblstartdate" runat="server"></asp:Label><asp:Label ID="Label25" runat="server" Text="."></asp:Label></td></tr><tr>
                                    <td  style="height: 26px">
                                        <asp:Label ID="lblm0" runat="server" ForeColor="Black">You cannot select 
                                                    any date earlier than the start of the year date. </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" >
                                        <asp:Button ID="ImageButton2f" runat="server" Text="Cancel" CssClass="btnSubmit"
                                            />
                                    </td>
                                </tr>
                            </table>
                            &nbsp;</asp:Panel><asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel5" TargetControlID="HiddenButton222" CancelControlID="ImageButton2f">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkadd0" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlwarehouse" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlpartytypetype" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlpartynamename" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="chkappamount" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="CheckBox1" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlAccountname1" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlAccountname2" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlAccountname3" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlAccountname4" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlAccountname5" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlAccountname6" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlAccountname7" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlAccountname8" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlAccountname9" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnnext" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnprivious" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btngoservh" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkadd2" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btneditgene" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
