<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="JournalEntryCrDrCompany.aspx.cs" Inherits="ShoppingCart_Admin_JournalEntryCrDrCompany"
    Title="Untitled Page" %>

<%--<%@ Register Src="~/ShoppingCart/Admin/UserControl/UControlWizardpanel.ascx" TagName="pnl"
    TagPrefix="pnl" %>
<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
    </script>
    <asp:UpdatePanel ID="pnluppa" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <label>
                        <asp:Label ID="lblMSG" runat="server" ForeColor="Red"></asp:Label>
                    </label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <div style="float: right;">
                        <asp:Button ID="img" CssClass="btnSubmit" runat="server" Text="View Journal" OnClick="ImageButton7_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <asp:Panel ID="pnnl" Visible="false" runat="server">
                            <table>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="dd" ForeColor="Black" Text="Create Journal Entry For the Following Documents"
                                            runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <label>
                                            Doc ID
                                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                        </label>
                                        <label>
                                            Title
                                            <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                                        </label>
                                        <label>
                                            Date
                                            <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                                        </label>
                                        <label>
                                            Cabinet/Drawer/Folder
                                            <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="Panel2" runat="server">
                            <label>
                                <asp:Label ID="Label6" runat="server" Text="Business Name"></asp:Label><asp:DropDownList
                                    ID="ddlwarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="Label7" runat="server" Text="Entry No"></asp:Label><br />
                                <asp:Label ID="txtEntryNo" runat="server" Text=""></asp:Label></label>
                            <label>
                                <asp:Label ID="Label8" runat="server" Text="Date"></asp:Label><asp:Label ID="Label18"
                                    runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDate" ErrorMessage="*"
                                        ValidationGroup="2">
                                    </asp:RequiredFieldValidator><asp:TextBox ID="txtDate" runat="server" Width="100px"></asp:TextBox><cc1:MaskedEditExtender
                                        ID="MaskedEditExtender1534" runat="server" CultureName="en-AU" Enabled="True"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtDate" />
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                                    TargetControlID="txtDate">
                                </cc1:CalendarExtender>
                            </label>
                            <label>
                                <br />
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/cal_btn.jpg" />
                            </label>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="Panel1" runat="server">
                            <label>
                                <asp:Label ID="Label9" runat="server" Text="Account">
                                </asp:Label>
                                <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAccount" Display="Dynamic"
                                    ErrorMessage="*" InitialValue="0" ValidationGroup="1">
                                </asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlAccount" runat="server" Width="450px">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <br />
                                <asp:ImageButton ID="LinkButton97666667" runat="server" Height="15px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                    Width="20px" ToolTip="AddNew" OnClick="LinkButton97666667_Click" />
                            </label>
                            <label>
                                <br />
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        &nbsp;
                                        <asp:ImageButton ID="LinkButton13" runat="server" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                            OnClick="ImageButton48_Click" AlternateText="Refresh" Height="15px" Width="20px"
                                            ToolTip="Refresh" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="LinkButton13" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </label>
                            <label>
                                <asp:Label ID="Label13" runat="server" Text="Cr/Dr"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlcrdr" Display="Dynamic"
                                    ErrorMessage="*" InitialValue="0" ValidationGroup="1">
                                </asp:RequiredFieldValidator><asp:DropDownList ID="ddlcrdr" runat="server" Width="60px">
                                    <asp:ListItem>Credit</asp:ListItem>
                                    <asp:ListItem Selected="True">Debit</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="Label14" runat="server" Text="Amount">

                                </asp:Label>
                                <asp:Label ID="Label20" runat="server" Text="*" CssClass="labelstar">
                                </asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAmount"
                                    ErrorMessage="*" ValidationGroup="1" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtAmount"
                                    ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid Digits"
                                    Display="Dynamic"> 
                                </asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtAmount" MaxLength="15" Width="150px" onkeyup="return mak('Span1',15,this)"
                                    runat="server">
                                </asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender323" runat="server"
                                    Enabled="True" TargetControlID="txtAmount" ValidChars="0147852369.">
                                </cc1:FilteredTextBoxExtender>
                                <asp:Label ID="Label28" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                <span id="Span1" class="labelcount">15</span>
                                <asp:Label ID="Label5" runat="server" CssClass="labelcount" Text="(0-9 .)">

                                </asp:Label></label>
                            <label>
                                <br />
                                <asp:Button ID="ImageButton2" runat="server" Text="Add" OnClick="ImageButton2_Click"
                                    ValidationGroup="1" CssClass="btnSubmit" />
                            </label>
                            <div style="clear: both;">
                            </div>
                            <label>
                                <asp:Label ID="Label15" runat="server" Text="Memo">
                                </asp:Label>
                            </label>
                            <label>
                                <asp:TextBox ID="txtmemo" runat="server" MaxLength="500" Height="80px" Width="600px"
                                    TextMode="MultiLine" onkeypress="return checktextboxmaxlength(this,500,event)"
                                    onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z.0-9_ ]+$/,'div1',500)"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="*" Display="Dynamic"
                                    SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)" ControlToValidate="txtmemo"
                                    ValidationGroup="1">
                        
                                </asp:RegularExpressionValidator>
                                <asp:Label ID="Label21" runat="server" Text="Max " CssClass="labelcount"></asp:Label><span
                                    id="div1" class="labelcount">500</span>
                                <asp:Label ID="Label17" runat="server" CssClass="labelcount" Text="(a-z 0-9 _ .)"></asp:Label>
                            </label>
                            <label>
                                <asp:Label ID="Label1" runat="server" Visible="false" Text="Entry Type"></asp:Label><asp:Label
                                    ID="lblentrytype" runat="server" Visible="false"></asp:Label></label>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                        <div>
                            <label>
                                <asp:Label ID="lblsss" runat="Server" Text="Journal Entry- " Visible="False"></asp:Label>
                                <asp:Label ID="lbldise" runat="Server" Visible="False"></asp:Label>
                            </label>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="Panel3" runat="server">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="AccountId" HeaderText="Account ID" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Account" HeaderText="Account Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="35%" />
                                    <asp:BoundField DataField="CrDr" HeaderText="Cr/Dr" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="9%" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="9%" />
                                    <asp:BoundField DataField="Memo" HeaderText="Memo" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="35%" />
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTrDid" runat="server" Text='<%#Bind("TrDid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField CommandName="remove" ButtonType="Image" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                        ImageUrl="~/Account/images/delete.gif" Text="Delete" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="4%" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                        <div>
                            <asp:CheckBox ID="chkdoc" runat="server" />
                            <label>
                                <asp:Label ID="Label4" runat="server" Text="Do you wish to attach/upload documents after the submission of the entry?"></asp:Label>
                            </label>
                        </div>
                         <div style="clear: both;">
                        </div>
                        <div>
                           <asp:CheckBox ID="chkappentry" runat="server" Text="Approved for this entry" 
                                         Visible="False" />
                        </div>
                        <div style="clear: both;">
                        </div>
                        <div>
                            <asp:Button ID="ImageButton3" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="ImageButton3_Click"
                                ValidationGroup="2" Visible="False" />
                            <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btnSubmit" Visible="False"
                                OnClick="Button2_Click" />
                            <asp:Button ID="btnup" runat="server" ValidationGroup="2" CssClass="btnSubmit" Text="Update"
                                OnClick="btnup_Click" Visible="False" />
                            <asp:Button ID="btnedit" runat="server" CssClass="btnSubmit" Text="Edit" OnClick="btnedit_Click"
                                Visible="False" />
                        </div>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" BorderColor="#666666" BorderStyle="Outset"
                            Width="300px" BorderWidth="5px">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="subinnertblfc">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblm" runat="server" ForeColor="Black" Font-Bold="True">Please check the date.</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" ForeColor="Black" Text="Start Date of the Year is "></asp:Label>
                                        <asp:Label ID="lblstartdate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblm0" runat="server" ForeColor="Black">You can not select 
 any date earlier than that. 
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 26px">
                                        <asp:Button ID="ImageButton10" runat="server" CssClass="btnSubmit" Text="Cancel"
                                            OnClick="ImageButton10_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="HiddenButton2221" runat="server" Style="display: none" />
                        </asp:Panel>
                        <asp:Button ID="Button1" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="Panel5" TargetControlID="HiddenButton2221">
                        </cc1:ModalPopupExtender>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="pnlupd" runat="server" CssClass="modalPopup" Width="60%">
                            <div>
                                <fieldset style="border: 1px solid white;">
                                    <legend style="color: Black">
                                        <asp:Label ID="Label16" runat="server" Text="This entry is about to be changed Please confirm the update">
                                        </asp:Label>
                                    </legend>
                                    <div style="background-color: White;">
                                        <div style="float: right;">
                                            <label>
                                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="16px" />
                                            </label>
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <label>
                                            <asp:Button ID="btncon" runat="server" Text="Confirm" CssClass="btnSubmit" OnClick="btncon_Click" />
                                            <asp:Button ID="btnca" runat="server" Text="Cancel" CssClass="btnSubmit" />
                                        </label>
                                    </div>
                                </fieldset>
                            </div>
                        </asp:Panel>
                        <asp:Button ID="Button3" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlupd"
                            CancelControlID="btnca" TargetControlID="Button3">
                        </cc1:ModalPopupExtender>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
