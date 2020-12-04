<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentHideApprovedDoc.aspx.cs" Inherits="DocumentHideApprovedDoc"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>--%>
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
            <div style="margin-left: 1%">
                <asp:Panel ID="pnlmsg" runat="server" Width="100%" Visible="False">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                </asp:Panel>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label1" runat="server" Text="Set a rule to allow employees to view documents once they are approved by the filing desk department"></asp:Label>
                    </legend>
                    <asp:Panel ID="pnlSecureDocument_PageStatus" runat="server" Width="100%">
                        <asp:Panel Width="100%" ID="pnlID" runat="server">
                            <fieldset>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label2" runat="server" Text="Business Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="5">
                                            <label>
                                                <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="1">
                                            <label>
                                                <asp:Label ID="Label3" runat="server" Text="Apply this rule to filing desk employees"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="5">
                                           
                                                <asp:RadioButtonList ID="rbtnDesignationOpt" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" OnSelectedIndexChanged="rbtnDesignationOpt_SelectedIndexChanged">
                                                    <asp:ListItem Value="1" Selected="True"> All Users</asp:ListItem>
                                                    <asp:ListItem Value="2">Manager</asp:ListItem>
                                                    <asp:ListItem Value="3">Supervisor</asp:ListItem>
                                                    <asp:ListItem Value="4">Office Clerk</asp:ListItem>
                                                </asp:RadioButtonList>
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="1">
                                            <label>
                                                <asp:Label ID="Label4" runat="server" Text="Document Access Rights For"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="5">
                                         
                                                <asp:RadioButtonList ID="Rbtnoptions" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="Rbtnoptions_SelectedIndexChanged">
                                                    <asp:ListItem Value="1" Selected="True">All</asp:ListItem>
                                                    <asp:ListItem Value="2">Selected Time Frame</asp:ListItem>
                                                    <asp:ListItem Value="3">Selected Document ID range</asp:ListItem>
                                                </asp:RadioButtonList>
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="5">
                                            <asp:Panel ID="pnlFromDate" runat="server">
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2">
                                                            <label>
                                                                <asp:Label ID="Label5" runat="server" Text="From Date"></asp:Label>
                                                                <asp:RegularExpressionValidator ID="rghjk" runat="server" ErrorMessage="*" ControlToValidate="txtfrom"
                                                                    ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfrom"
                                                                    ErrorMessage="*" ValidationGroup="1" Width="8px"></asp:RequiredFieldValidator>
                                                            </label>
                                                            <label>
                                                                <asp:TextBox ID="txtfrom" runat="server" Width="100px"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="imgbtncalfrom" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg">
                                                                </asp:ImageButton>
                                                                <cc1:MaskedEditExtender ID="TextBox1_MaskedEditExtender2" runat="server" CultureName="en-AU"
                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfrom" />
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgbtncalfrom"
                                                                    TargetControlID="txtfrom">
                                                                </cc1:CalendarExtender>
                                                            </label>
                                                        </td>
                                                        <td colspan="2">
                                                            <label>
                                                                <asp:Label ID="Label6" runat="server" Text="To Date"></asp:Label>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                                                                    ControlToValidate="txtto" ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtto"
                                                                    ErrorMessage="*" ValidationGroup="1" Width="8px"></asp:RequiredFieldValidator>
                                                            </label>
                                                            <label>
                                                                <asp:TextBox ID="txtto" runat="server" Width="100px"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="imgbtnto" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg">
                                                                </asp:ImageButton>
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgbtnto"
                                                                    TargetControlID="txtto">
                                                                </cc1:CalendarExtender>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlFromId" runat="server">
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2">
                                                            <label>
                                                                <asp:Label ID="Label7" runat="server" Text="From ID"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="1"
                                                                    ErrorMessage="*" ControlToValidate="txtFromId">
                                                                </asp:RequiredFieldValidator>
                                                            </label>
                                                            <label>
                                                                <asp:TextBox ID="txtFromId" runat="server" MaxLength="15" onkeyup="return mak('Span2',15,this)"
                                                                    Width="150px"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label18" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="Span2" class="labelcount">15</span>
                                                                <asp:Label ID="Label20" runat="server" CssClass="labelcount" Text="(0-9)"></asp:Label>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                    TargetControlID="txtFromId" ValidChars="0123456789">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </label>
                                                        </td>
                                                        <td colspan="2">
                                                            <label>
                                                                <asp:Label ID="Label8" runat="server" Text="To ID"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="1"
                                                                    ErrorMessage="*" ControlToValidate="txtToId"></asp:RequiredFieldValidator>
                                                            </label>
                                                            <label>
                                                                <asp:TextBox ID="txtToId" runat="server" MaxLength="15" onkeyup="return mak('Span1',15,this)"
                                                                    Width="150px"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label19" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="Span1" class="labelcount">15</span>
                                                                <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(0-9)"></asp:Label>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                    TargetControlID="txtToId" ValidChars="0123456789">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Button ID="ImgBtnSubmit" OnClick="ImgBtnSubmit_Click" runat="server" Text="Submit"
                                                ValidationGroup="1" CssClass="btnSubmit"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset></asp:Panel>
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label9" runat="server" Text="Filing Desk Access Rights Rule "></asp:Label>
                            </legend>
                            <div>
                            <asp:Label ID="Label25" runat="server" Text="Note: To change the existing rule, simply set a new rule and it will automatically replace the existing rule"></asp:Label>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <table width="100%">
                                <tr>
                                    <td colspan="4">
                                        <asp:Panel runat="server" ID="pnlAll" Width="100%">
                                            <div>
                                                <label>
                                                    <asp:Label ID="Label24" runat="server" Text="For Business:"></asp:Label>
                                                    <asp:Label ID="lblbusiness1" runat="server" Text=""></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="Label14" runat="server" Text="The following types of filing desk employee designations have the right to access the following range of documents:"></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="Label21" runat="server" Text="Employee designation - Manager, Supervisor, Office Clerk"></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="lblSelectionFor" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="lblFrom" runat="server" Text="From Date"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblFromDatamain" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblTo" runat="server" Text="To Date"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblToDataMain" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblFromData" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblToData" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Panel runat="server" ID="pnlAdmin" Width="100%">
                                            <div>
                                                <label>
                                                    <asp:Label ID="Label13" runat="server" Text="For Business:"></asp:Label>
                                                    <asp:Label ID="lblbusiness2" runat="server" Text=""></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="Label10" runat="server" Text="The following type of filing desk employee designation has the right to access the following range of documents: "></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="Label22" runat="server" Text="Employee designation - Manager"></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="lblSelectionForAdmin" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="lblFromAdmin" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblFromDataAdminMain" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblToAdmin" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblToDataAdminMain" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblFromDataAdmin" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblToDataAdmin" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Panel runat="server" ID="pnlSuper" Width="100%">
                                            <div>
                                                <label>
                                                    <asp:Label ID="Label17" runat="server" Text="For Business:"></asp:Label>
                                                    <asp:Label ID="lblbusiness3" runat="server" Text=""></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="Label11" runat="server" Text="The following type of filing desk employee designation has the right to access the following range of documents:"></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="Label15" runat="server" Text="Employee designation - Supervisor"></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="lblselectionforS" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="lblFromS" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblFromDataSmain" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblToS" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblToDataSMain" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblFromDataS" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblToDataS" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Panel runat="server" ID="pnlDataEO" Width="100%">
                                            <div>
                                                <label>
                                                    <asp:Label ID="Label23" runat="server" Text="For Business:"></asp:Label>
                                                    <asp:Label ID="lblbusiness4" runat="server" Text=""></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="Label12" runat="server" Text="The following type of filing desk employee designation has the right to access the following range of documents:"></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="Label16" runat="server" Text="Employee designation - Office Clerk"></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="lblselectionforD" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <div>
                                                <label>
                                                    <asp:Label ID="lblfromD" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblfromDataDMain" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblToD" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblTodataDMain" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblfromDataD" runat="server"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:Label ID="lblToDataD" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </fieldset></asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
