<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="ProductDefaultRoleAddManage.aspx.cs" Inherits="UserRoleforePriceplan" Title="UserRole for Priceplan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function ChangeCheckBoxState(id, checkstate) {

            var chb = document.getElementById(id);
            if (chb != null)
                chb.checked = checkstate;
        }

        function ChangeAllCheckBoxStates(checkstate) {

            if (CheckBoxIDs != null) {
                for (var i = 0; i < CheckBoxIDs.length; i++)
                    ChangeCheckBoxState(CheckBoxIDs[i], checkstate);
            }
        }

        function ChangeHeaderState() {

            if (CheckBoxIDs != null) {
                for (var i = 1; i < CheckBoxIDs.length; i++) {
                    var chb = document.getElementById(CheckBoxIDs[i]);
                    if (!chb.checked) {
                        ChangeCheckBoxState(CheckBoxIDs[0], false);
                        return;
                    }
                }
                ChangeCheckBoxState(CheckBoxIDs[0], true);
            }
        }


    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <fieldset>
                <legend>
                    <asp:Label ID="Label4" runat="server" Text="Set Default Role / Deaprtment / Desgination For Product"></asp:Label>
                </legend>
                <div style="clear: both;">
                </div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            <label>
                                Select Product/Version 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ControlToValidate="ddlProductname" ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            <label>
                                <asp:DropDownList ID="ddlProductname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged" Width="200px">
                                </asp:DropDownList>
                            </label>
                        </td>
                        <td>
                        </td>
                    </tr>
                   
                    
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label3" runat="server" Text="Create Default Department"></asp:Label></label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel3" runat="server" Width="100%" ScrollBars="Auto">
                                <asp:GridView ID="GridView3" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"
                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Department Names" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbldeptname" runat="server" Text='<%# Bind("DefaultDeptName") %>'  MaxLength="80"></asp:TextBox>
                                                <asp:Label ID="txtdeptid" runat="server" Text='<%# Bind("DefaultDeptId") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Designation-1" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbldesigname" runat="server" MaxLength="80" Width="95%" Text='<%# Bind("DefaultDesiName") %>'></asp:TextBox>
                                                 <asp:Label ID="txtdesigid" runat="server" Visible="false" Text='<%# Bind("DefaultDesitId") %>'  ></asp:Label>
                                                 <asp:Label ID="txtroleid" runat="server" Text='<%# Bind("DefaultRoleId") %>' Visible="false"></asp:Label>                                     
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Designation-2" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbldesigname2" runat="server" MaxLength="80" Width="95%" Text='<%# Bind("DefaultDesiName2") %>'></asp:TextBox>
                                                 <asp:Label ID="txtdesigid2" runat="server" Text='<%# Bind("DefaultDesitId2") %>' Visible="false"></asp:Label>
                                                 <asp:Label ID="txtroleid2" runat="server" Text='<%# Bind("DefaultRoleId2") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Designation-3" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbldesigname3" runat="server" MaxLength="80" Width="95%" Text='<%# Bind("DefaultDesiName3") %>'></asp:TextBox>
                                                <asp:Label ID="txtdesigid3" runat="server" Visible="false" Text='<%# Bind("DefaultDesitId3") %>'></asp:Label>
                                                <asp:Label ID="txtroleid3" runat="server" Text='<%# Bind("DefaultRoleId3") %>' Visible="false"></asp:Label>                                     
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Designation-4" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                             <asp:TextBox ID="lbldesigname4" runat="server" MaxLength="80" Width="95%" Text='<%# Bind("DefaultDesiName4") %>'></asp:TextBox>
                                             <asp:Label ID="txtdesigid4" runat="server" Visible="false" Text='<%# Bind("DefaultDesitId4") %>'></asp:Label>
                                             <asp:Label ID="txtroleid4" runat="server" Text='<%# Bind("DefaultRoleId4") %>' Visible="false"></asp:Label>                                     
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Designation-5" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                 <asp:TextBox ID="lbldesigname5" runat="server" MaxLength="80" Width="95%" Text='<%# Bind("DefaultDesiName5") %>'></asp:TextBox>
                                                   <asp:Label ID="txtdesigid5" runat="server" Visible="false" Text='<%# Bind("DefaultDesitId5") %>'></asp:Label>
                                                  <asp:Label ID="txtroleid5" runat="server" Text='<%# Bind("DefaultRoleId5") %>' Visible="false"></asp:Label>                                     
                                         </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Designation-6" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                   <asp:TextBox ID="lbldesigname6" runat="server" MaxLength="80" Width="95%" Text='<%# Bind("DefaultDesiName6") %>'></asp:TextBox>
                                                    <asp:Label ID="txtdesigid6" runat="server" Visible="false" Text='<%# Bind("DefaultDesitId6") %>'></asp:Label>
                                                  <asp:Label ID="txtroleid6" runat="server" Text='<%# Bind("DefaultRoleId6") %>' Visible="false"></asp:Label>                                     
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <input runat="server" id="Hidden5" type="hidden" name="hdnProductDetailId" style="width: 3px" />
                            <input id="Hidden6" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                            <input id="Hidden7" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                            <input runat="server" id="Hidden8" type="hidden" name="hdnProductId" style="width: 3px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                   
                 
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="0" id="Table1" style="width: 100%">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="Button1" runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btnSubmit"  ValidationGroup="1" Visible="False" />
                                        <asp:Button ID="Button2" runat="server" Text="Edit" Visible="False" OnClick="BtnEdit_Click" CssClass="btnSubmit" />
                                        <asp:Button ID="Button3" runat="server" Text="Update" CssClass="btnSubmit" ValidationGroup="1" OnClick="BtnUpdate_Click" Visible="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                  
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
