<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="Inupdate.aspx.cs" Inherits="ShoppingCart_Admin_Inupdate"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">


        function mask(evt) {

            if (evt.keyCode == 13) {

                return true;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }




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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <fieldset>
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <table id="subinnertbl" width="100%">
                            <tr >
                                <td>
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="Update Inventory"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:Label ID="lblINvName" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="Inventory Master ID"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:Label ID="lblInvId" runat="server" Font-Bold="True"></asp:Label>
                                    <input id="hdnCatDetaiId" runat="server" name="hdnCatDetaiId" style="width: 1px"
                                        type="hidden" />
                                </td>
                            </tr>
                            <tr >
                                <td>
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Name"></asp:Label>
                                        <asp:Label runat="server" ID="lblmain" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
                                            ErrorMessage="*" ControlToValidate="txtname" Width="1px"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtname" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtname" Width="350px" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',40)"
                                            MaxLength="40"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Max" CssClass="labelcount" ></asp:Label>
                                        <span id="div1" class="labelcount">40</span>
                                        <asp:Label ID="lblkjsadfgh" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Product No."></asp:Label>
                                        <asp:Label runat="server" ID="Label15" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1"
                                            ErrorMessage="*" ControlToValidate="txtproductno"></asp:RequiredFieldValidator>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator5456" runat="server"  ValidationGroup="1"
                                        ErrorMessage="Invalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"  
                                        ControlToValidate="txtproductno"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtproductno" MaxLength="20" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span1',20)"
                                            runat="server"></asp:TextBox>
                                    </label>
                                    <label>
                                       <asp:Label ID="Label4" runat="server" Text="Max" CssClass="labelcount" ></asp:Label>
                                         <span id="Span1" class="labelcount">20</span>
                                        <asp:Label ID="Label16" runat="server" Text="(A-Z 0-9)" CssClass="labelcount"></asp:Label>
                                      
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label9" runat="server" Text="Category: Sub Category: Sub Sub Category"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlinventorysubsubid" runat="server"  Width="350px"  AppendDataBoundItems="true">
                                           
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label10" runat="server" Text="Weight"></asp:Label>
                                        <asp:Label runat="server" ID="Label2" Text="*"></asp:Label>
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="1"
                                            ErrorMessage="*" ControlToValidate="txtWeight"></asp:RequiredFieldValidator>
                                            
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtWeight"
                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ValidationGroup="1"
                                            ErrorMessage="Invalid Digit"> </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtWeight" runat="server" onkeyup="return mak('Span2',15,this)"
                                            MaxLength="15" ></asp:TextBox>
                                    </label>
                                    <label>
                                      <asp:DropDownList ID="ddllbs" runat="server" Width="80px">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                      
                                        <asp:Label ID="Label21" runat="server" Text="Max" CssClass="labelcount" ></asp:Label>
                                         <span id="Span2" class="labelcount">15</span>
                                        <asp:Label ID="Label17" runat="server" Text="(0-9 .)" CssClass="labelcount"></asp:Label>
                                        
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            TargetControlID="txtWeight" ValidChars="0147852369.">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label11" runat="server" Text="Barcode"></asp:Label>
                                        <asp:Label runat="server" ID="Label18" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
                                            ErrorMessage="*" ControlToValidate="txtBarcode"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtBarcode" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtBarcode" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span3',20)"
                                            MaxLength="20" ></asp:TextBox>
                                             </label>
                                              <label>
                                        <asp:Label ID="Label22" runat="server" Text="Max" CssClass="labelcount" ></asp:Label>
                                         <span id="Span3" class="labelcount">20</span>
                                        <asp:Label ID="Label19" runat="server" Text="(A-Z 0-9)" CssClass="labelcount"></asp:Label>
                                    </label>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text="Description"></asp:Label>
                                       
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtdescription" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please enter 300 Chars"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,300})$"
                                            ControlToValidate="txtdescription" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="vertical-align: top">
                                    <label>
                                        <asp:TextBox ID="txtdescription" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z.A-Z0-9_\s]+$/,'Span4',300)"
                                            MaxLength="300" runat="server" TextMode="MultiLine" onkeypress="return checktextboxmaxlength(this,300,event)" Width="350px" Height="60px"></asp:TextBox>
                                         <asp:Label ID="Label23" runat="server" Text="Max" CssClass="labelcount" ></asp:Label>
                                          <span id="Span4" class="labelcount">300</span>
                                        <asp:Label ID="Label20" runat="server" Text="(A-Z 0-9 _ .)" CssClass="labelcount"></asp:Label>
                                    </label>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Inventory Updated On"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="1"
                                            ErrorMessage="*" ControlToValidate="txtqtyondatestarted"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td >
                                    <label>
                                        <asp:TextBox ID="txtqtyondatestarted" runat="server" Width="100px"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txtqtyondatestarted">
                                        </cc1:MaskedEditExtender>
                                        <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton2" TargetControlID="txtqtyondatestarted"
                                            runat="server" >
                                        </cc1:CalendarExtender>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Inventory Status "></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkActive" runat="server" Checked="True" Visible="false" />
                                      <asp:DropDownList ID="ddlstatus" runat="server" Width="100px">
                                <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlqtytypemasterid" runat="server" AppendDataBoundItems="true"
                                        Visible="False" Width="307px">
                                        <asp:ListItem Selected="True">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                 <asp:Button ID="imgbtnSubmit" runat="server" Text="Update" ValidationGroup="1" OnClick="imgbtnSubmit_Click"
                                        CssClass="btnSubmit" />
                                    <asp:Button ID="imgbtnCancel" runat="server" Text="Cancel" OnClick="imgbtnCancel_Click"
                                        CssClass="btnSubmit" />
                                </td>
                            </tr>
                            
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
