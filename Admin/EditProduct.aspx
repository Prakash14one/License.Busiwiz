<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="EditProduct.aspx.cs" Inherits="EditProduct" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" id="pagetbl">
        <tr>
            <td colspan="3" class="hdng">
                Client's Update Product</td>
        </tr>
        <tr>
            <td style="width: 212px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Panel ID="pnlProduct" runat="server" Visible="False" Width="100%">
                <table cellpadding="0" cellspacing="0" id="pagetbl">
                <tr>
                <td class="column1" style="width: 33%">
                    &nbsp;Product Name :
                </td>
                <td class="column2">
                    &nbsp;<asp:TextBox ID="txtProductName" runat="server" Width="190px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUrl" ErrorMessage="*"
                        ValidationGroup="1"></asp:RequiredFieldValidator></td>
                   </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="column1">
                Product
                URL :</td>
            <td class="column2">
                <asp:TextBox ID="txtUrl" runat="server" Width="190px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                    ValidationGroup="1" ControlToValidate="txtPricePlanURL"></asp:RequiredFieldValidator></td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="column1">
                Price Plan URL :</td>
            <td class="column2">
                <asp:TextBox ID="txtPricePlanURL" runat="server" Width="190px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUrl"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="column1">
                Version No :</td>
            <td class="column2">
                <asp:TextBox ID="txtVersionNo" runat="server" Width="190px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtVersionNo"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="column1">
                Active / Deactive :</td>
            <td class="column2">
                <asp:CheckBox ID="chkboxActiveDeactive" runat="server" Text="Active" /></td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="column1">
                Start Date :</td>
            <td class="column2">
                <asp:TextBox ID="txtStartdate" runat="server" Width="181px"></asp:TextBox>
                <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                TargetControlID="txtStartdate"
                                                                            PopupButtonID="imgbtnEndDate">
                                                                        </cc1:CalendarExtender>
                
                </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="column1">
                End Date :</td>
            <td class="column2">
                <asp:TextBox ID="txtEndDate" runat="server" Width="182px"></asp:TextBox>
                <asp:ImageButton ID="imgbtnCalEnddate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                TargetControlID="txtEndDate"
                                                                            PopupButtonID="imgbtnCalEnddate">
                                                                        </cc1:CalendarExtender>
                
                </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="column1">
                Upload your project Set up :&nbsp;</td>
            <td class="column2">
                <asp:FileUpload ID="fuploadProjectSetup" runat="server" Width="191px" /></td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="column1">
                Upload your Project's Database file :&nbsp;</td>
            <td class="column2">
                <asp:FileUpload ID="FuploadDbFile" runat="server" Width="191px" /></td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="column1">
                Upload any other Set UP File(Attach DB) :</td>
            <td class="column2">
                <asp:FileUpload ID="FUploadExtra" runat="server" Width="191px" /></td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="column1">
            </td>
            <td class="column2">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                    ValidationGroup="1" /></td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
    </table>
    <input runat="server"  id="hdnProductDetailId" type="hidden" name="hdnProductDetailId" style="width: 3px"  />
    <input runat="server"  id="hdnProductId" type="hidden" name="hdnProductId" style="width: 3px"  />
</asp:Content>

