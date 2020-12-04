<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="Priceplanrestriction.aspx.cs" Inherits="Priceplanrestriction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
     
function Setlbl()
{
    var ddl1 = document.getElementById('<%= txtrestgroupname.ClientID %>');
    document.getElementById('<%=lblGn1.ClientID%>').innerHTML = ddl1.value;
    var ddl = document.getElementById('<%= txtrestgroup.ClientID %>');
    document.getElementById('<%=lblGn2.ClientID%>').innerHTML = ddl.value + ddl1.value;
  
}
function Setlbl1() {
    var ddlq = document.getElementById('<%= txtrestgroupname.ClientID %>');
    var ddl = document.getElementById('<%= txtrestgroup.ClientID %>');
    document.getElementById('<%=lblGn2.ClientID%>').innerHTML = ddl.value + ddlq.value;
    
}

    </script>

    <asp:UpdatePanel ID="pnl" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="addnewpanel" runat="server" Text="Add Price Plan Restriction" CssClass="btnSubmit"
                            OnClick="addnewpanel_Click" />
                            <asp:Button ID="btndosyncro" runat="server" CssClass="btnSubmit" OnClick="btndosyncro_Clickpop"
                                        Text="Do Synchronise" Visible="true" />
                    </div>
                    <div style="clear: both;">
                    
                    </div>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                        <table width="100%">
                            <tr>
                                <td>
                                </td>
                                <td style="width: 300px">
                                    <label>
                                        Product/Version
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                            SetFocusOnError="true" InitialValue="0" ValidationGroup="1" ControlToValidate="ddlproduct"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlproduct" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <label>
                                        Portal Name<asp:RequiredFieldValidator ID="reqportal" runat="server" ErrorMessage="*"
                                            SetFocusOnError="true" InitialValue="0" ControlToValidate="ddlportal" ValidationGroup="1"> </asp:RequiredFieldValidator></label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlportal" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <label>
                                        Name of the restriction
                                    </label>
                                    <asp:RequiredFieldValidator ID="reqnamerest" runat="server" ErrorMessage="*" SetFocusOnError="true"
                                        ControlToValidate="txtnameofrest" ValidationGroup="1"></asp:RequiredFieldValidator></label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtnameofrest" runat="server" MaxLength="300" Width="342px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td valign="top">
                                    <label>
                                        Text of Question in Price Plan selection
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                            SetFocusOnError="true" ControlToValidate="txttxtquestpps" ValidationGroup="1"></asp:RequiredFieldValidator></label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txttxtquestpps" runat="server" MaxLength="500" Width="466px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <label>
                                        Table on which restriction set<asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                            runat="server" ErrorMessage="*" SetFocusOnError="true" InitialValue="0" ControlToValidate="ddltable"
                                            ValidationGroup="1"> </asp:RequiredFieldValidator></label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddltable" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltable_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgadddept" runat="server" Height="20px" ImageUrl="~/images/AddNewRecord.jpg"
                                            OnClick="LinkButton97666667_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgdeptrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                            ImageUrl="~/images/DataRefresh.jpg" OnClick="LinkButton13_Click" ToolTip="Refresh"
                                            Width="20px" ImageAlign="Bottom" />
                                    </label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <label>
                                        Field on which restiction set
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                    SetFocusOnError="true" InitialValue="0" ValidationGroup="1" ControlToValidate="ddlfeildrest"></asp:RequiredFieldValidator>
   --%>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlfeildrest" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td valign="top">
                                    <label>
                                        if foreign field what ID is to be restricted
                                    </label>
                                </td>
                                <td colspan="2">
                                <label>
                                    <asp:TextBox ID="txtfieldIdRest" runat="server" MaxLength="20" Width="100px" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td valign="top">
                                    <label>
                                        Restriction Group Name
                                    </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtrestgroupname" ClientIDMode="Static" runat="server" onchange="Setlbl();"
                                            MaxLength="500" Width="100px" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td valign="top">
                                    <label>
                                        Restrictions in group of
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                            SetFocusOnError="true" ControlToValidate="txtrestgroup" ValidationGroup="1"></asp:RequiredFieldValidator></label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtrestgroup" ClientIDMode="Static" runat="server" onchange="Setlbl1();" MaxLength="20" Width="100px" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTexender1" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtrestgroup" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblGn1" runat="server"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="3">
                                    <label>
                                        ( You can set restrictions like 1 gb or 2 gb or 3 gb of storage space …thus in group<br />
                                        of 1 OR 100 documents or 200 Documents ..thus in group of 100)
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td valign="top">
                                    <label>
                                        Price for additional group
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                            SetFocusOnError="true" ControlToValidate="txtrestgroup" ValidationGroup="1"></asp:RequiredFieldValidator></label>
                                    <label>
                                        <asp:Label ID="lblGn2" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtaddprice" runat="server" MaxLength="20" Width="100px" />
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                        FilterType="Custom, Numbers" TargetControlID="txtaddprice" ValidChars=".0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                                <td>
                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btnsubmit_Click"
                                        ValidationGroup="1" />&nbsp;<asp:Button ID="btnedit" runat="server" OnClick="btnedit_Click"
                                            Text="Cancel" CssClass="btnSubmit" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label1" runat="server" Text="List of Price Plan Restriction"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="3">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <label>
                                                Filter By Product/Version</label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlpversion" runat="server" OnSelectedIndexChanged="ddlpversion_SelectedIndexChanged1"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                Filter By Portal</label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlfilterbyportal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbyportal_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                    HeaderStyle-HorizontalAlign="Left" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false"
                                    AllowSorting="true" Width="100%" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
                                    EmptyDataText="No Record Found." OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Product/Version" SortExpression="productversion">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbb" runat="server" Text='<%#bind("productversion") %>'>></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Table" SortExpression="TableName">
                                            <ItemTemplate>
                                                <asp:Label ID="lnln1" runat="server" Text='<%#bind("TableName") %>'>></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name of the restriction" SortExpression="NameofRest">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%#bind("NameofRest") %>'>></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Text of Question in Price Plan selection" SortExpression="TextofQueinSelection">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltxtse" runat="server" Text='<%#bind("TextofQueinSelection") %>'>></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Restrictions in group of" SortExpression="Restingroup">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrestgro" runat="server" Text='<%#bind("Restingroup") %>'>></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price for additional group" SortExpression="Priceaddingroup">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladdgroup" runat="server" Text='<%#bind("Priceaddingroup") %>'>></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("Id") %>'
                                                    CommandName="edit1" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="2%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="llinkbb" ToolTip="Delete" runat="server" CommandName="delete"
                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                    CommandArgument='<%# Eval("Id") %>'></asp:ImageButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Panel ID="Paneldoc" runat="server" Width="55%" CssClass="modalPopup">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 95%;">
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <label>
                                                        <asp:Label ID="Label20" runat="server" Text="Was this the last record you are going to add right now to this table?"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdsync" runat="server">
                                                                    <asp:ListItem Value="1" Text="Yes, this is the last record in the series of records I am inserting/editing to this table right now"></asp:ListItem>
                                                                    <asp:ListItem Value="0" Text="No, I am still going to add/edit records to this table right now"
                                                                        Selected="True"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="btnok" runat="server" CssClass="btnSubmit" Text="OK" OnClick="btndosyncro_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Paneldoc" TargetControlID="btnreh" CancelControlID="ImageButton10">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlproduct" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
