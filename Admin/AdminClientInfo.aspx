<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" 
CodeFile="AdminClientInfo.aspx.cs" Inherits="AdminClientInfo" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type ="text/javascript" language="javascript" >
function ValidateText(i)
{
    if(i.value.length > 0)
    {
    i.value = i.value.replace(/[^\d]+/g, '');
    }
}


</script>
  
    <table cellpadding="0" cellspacing="0" id="pagetbl">
        <tr>
            <td colspan="4" class="hdng">
                Add New Client</td>
        </tr>
        <tr>
            <td style="width: 212px" class="column1">
                &nbsp;</td>
            <td class="column2">
            </td>
            <td class="column1">
            </td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column1">
                &nbsp;</td>
            <td class="column2">
            </td>
            <td class="column1">
            </td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column1">
                Name of Client :</td>
            <td class="column2">
                <asp:TextBox ID="txtCompanyName" runat="server" Width="133px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCompanyName"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
            <td class="column1">
                </td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column1">
                Login Name :</td>
            <td class="column2">
                <asp:TextBox ID="txtLoginName" runat="server" Width="133px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtLoginName"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
            <td class="column1">
                Login Password :</td>
            <td class="column2">
                <asp:TextBox ID="txtLoginPassword" runat="server" TextMode="Password" Width="133px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtLoginPassword"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="column1">
                </td>
            <td class="column2">
                </td>
            <td class="column1">
            </td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column1">
                Confirm Password</td>
            <td class="column2" colspan="2">
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Width="133px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtConfirmPassword"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtLoginPassword"
                    ControlToValidate="txtConfirmPassword" ErrorMessage="Enter Same Password"></asp:CompareValidator></td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column1">
                Address1 :</td>
            <td class="column2">
                <asp:TextBox ID="txtAdrs1" runat="server" TextMode="MultiLine" Width="133px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAdrs1"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
            <td class="column1">
                Address2 :</td>
            <td class="column2">
                <asp:TextBox ID="txtAdrs2" runat="server" TextMode="MultiLine" Width="133px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="column1">
                Country :</td>
            <td class="column2">
                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="135px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlCountry"
                    ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator></td>
            <td class="column1">
                State :</td>
            <td class="column2">
                <asp:DropDownList ID="ddlState" runat="server" Width="135px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlState"
                    ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="column1">
                City :</td>
            <td class="column2">
                <asp:TextBox ID="txtCity" runat="server" Width="133px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCity"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
            <td class="column1">
                Zip Code :</td>
            <td class="column2">
                <asp:TextBox ID="txtZipcode" runat="server" Width="133px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtZipcode"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="column1">
                Contact Person Name :</td>
            <td class="column2">
                <asp:TextBox ID="txtContactPerson" runat="server" Width="133px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtContactPerson"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
            <td class="column1">
                </td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column1">
                Phone1 :</td>
            <td class="column2">
                <asp:TextBox ID="txtPhone1" runat="server" onkeyup="return ValidateText(this)" Width="133px" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhone1"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
            <td class="column1">
                Phone2 :</td>
            <td class="column2">
                <asp:TextBox ID="txtPhone2" runat="server" onkeyup="return ValidateText(this)" Width="133px" ></asp:TextBox></td>
        </tr>
        <tr>
            <td class="column1">
                Fax1 :</td>
            <td class="column2">
                <asp:TextBox ID="txtFax1" runat="server"  onkeyup="return ValidateText(this)" Width="133px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFax1"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
            <td class="column1">
                Fax2 :</td>
            <td class="column2">
                <asp:TextBox ID="txtFax2" runat="server" onkeyup="return ValidateText(this)" Width="133px" ></asp:TextBox></td>
        </tr>
        <tr>
            <td class="column1">
                Email1 :</td>
            <td class="column2">
                <asp:TextBox ID="txtEmail1" runat="server" Width="133px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEmail1"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator></td>
            <td class="column1">
                Email2 :</td>
            <td class="column2">
                <asp:TextBox ID="txtEmail2" runat="server" Width="133px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="column1">
                Client's URL : &nbsp;</td>
            <td class="column2">
                <asp:TextBox ID="txtClientUrl" runat="server" Width="133px"></asp:TextBox></td>
            <td class="column1">
                Customer Support URL :</td>
            <td class="column2">
                <asp:TextBox ID="txtCustSupportURL" runat="server" Width="133px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="column1">
                Sales Customer Support URL :
            </td>
            <td class="column2">
                <asp:TextBox ID="txtSalesSupportURL" runat="server" Width="133px"></asp:TextBox></td>
            <td class="column1">
            </td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column1">
                Sales Phone NO :</td>
            <td class="column2">
                <asp:TextBox ID="txtSalesPhoneNO" runat="server" Width="133px"></asp:TextBox></td>
            <td class="column1">
                Sales Fax NO :</td>
            <td class="column2">
                <asp:TextBox ID="txtSalesFaxNo" runat="server" Width="133px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="column1">
                Sales Email :</td>
            <td class="column2">
                <asp:TextBox ID="txtSalesEmail" runat="server" Width="133px"></asp:TextBox></td>
            <td class="column1">
            </td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column1">
                After Sales Support Ph. No :&nbsp;</td>
            <td class="column2">
                <asp:TextBox ID="txtafterSalesSupPhoneNO" runat="server" Width="133px"></asp:TextBox></td>
            <td class="column1">
                After Sales Support Fax No :&nbsp;</td>
            <td class="column2">
                <asp:TextBox ID="txtAfterSalesSupFaxNo" runat="server" Width="133px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="column1">
                After Sales Support Email :&nbsp;</td>
            <td class="column2">
                <asp:TextBox ID="txtAfterSalesSupEmail" runat="server" Width="133px"></asp:TextBox></td>
            <td class="column1">
            </td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column1">
                Technical Support Phone No :</td>
            <td class="column2">
                <asp:TextBox ID="txtTechSupportPhoneNo" runat="server" Width="133px"></asp:TextBox></td>
            <td class="column1">
                Technical Support Fax No :</td>
            <td class="column2">
                <asp:TextBox ID="txtTechSupportFaxNo" runat="server" Width="133px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="column1">
                Technical Support Email :</td>
            <td class="column2">
                <asp:TextBox ID="txtTechSupEmail" runat="server" Width="133px"></asp:TextBox></td>
            <td class="column1">
            </td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column1">
                FTP :
            </td>
            <td class="column2">
                <asp:TextBox ID="txtFTP" runat="server" Width="133px"></asp:TextBox></td>
            <td class="column1">
            </td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column1">
                User name :</td>
            <td class="column2">
                <asp:TextBox ID="txtFTPUserName" runat="server" Width="133px"></asp:TextBox></td>
            <td class="column1">
                Password :</td>
            <td class="column2">
                <asp:TextBox ID="txtFTPPassword" runat="server" Width="133px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="column1">
            </td>
            <td class="column2">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="1" />
                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" ValidationGroup="1" /></td>
            <td class="column1">
            </td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column1">
            </td>
            <td class="column2">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label></td>
            <td class="column1">
            </td>
            <td class="column2">
            </td>
        </tr>
    </table>
</asp:Content>

