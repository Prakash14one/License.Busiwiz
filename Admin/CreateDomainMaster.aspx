<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="CreateDomainMaster.aspx.cs" Inherits="Admin_CreateDomainMaster" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" style="width: 625px">
        <tr>
            <td class="hdng">
                Add new Domain<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" style="width: 642px">
                    <tbody>
                        <tr>
                            <td style="height: 32px" class="column1">
                                Client Name :
                            </td>
                            <td width="200px" style="height: 32px" class="column2" colspan="2">
                                <asp:DropDownList ID="ddlClientname" runat="server" Width="165px" OnSelectedIndexChanged="ddlClientname_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlClientname"
                                    ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                            <td style="height: 32px" class="column2" colspan="1">
                            </td>
                        </tr>
                        <tr>
                            <td class ="column1">
                                Company Name:
                            </td>
                            <td class="column2" colspan="2">
                                <asp:DropDownList ID="ddlCompany" runat="server" Widht="165px" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"
                                 AutoPostBack="True" >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCompany"
                                    ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                            <td class="column2" colspan="1">
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="column1">
                                Product Name :
                            </td>
                            <td class="column2" colspan="2">
                                <asp:DropDownList ID="ddlProductList" runat="server" AutoPostBack="True" DataTextField="ProductName"
                                    DataValueField="ProductId" OnSelectedIndexChanged="ddlProductList_SelectedIndexChanged"
                                    Width="222px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProductList"
                                    ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                            <td class="column2" colspan="1">
                            </td>
                        </tr>
                        <tr>
                            <td class="column1" style="padding-top: 10px;">
                                Domain Name :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                http://<asp:TextBox ID="txtdomainName" runat="server" Width="135px"></asp:TextBox>
                                <asp:Label ID="lblDomain" runat="server" Text=""></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdomainName"
                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator><br />
                                &nbsp;&nbsp;<asp:Label ID="lblShowText" runat="server" Width="232px" Text="E.g. :  XYZ"></asp:Label>
                            </td>
                            <td class="column2" colspan="1" style="padding-top: 10px;">
                                <asp:Button ID="btnCheckDomain" OnClick="btnCheckDomain_Click" runat="server" Width="90px"
                                    Text="Check Availability" ValidationGroup="2"></asp:Button>&nbsp;
                                <asp:Label ID="lblDomainAVl1" runat="server" Font-Bold="False" ForeColor="#C00000"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="column1">
                            </td>
                            <td class="column2" style="width: 254px">
                                <asp:Button ID="BntKeyG" OnClick="BntKeyG_Click" runat="server" Width="80px" Text="Generate Key"
                                    ValidationGroup="1"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="column1">
                            </td>
                            <td class="column2" style="width: 254px">
                            </td>
                        </tr>
                        <tr>
                            <td class="column1">
                                &nbsp;License Key :
                            </td>
                            <td class="column2" colspan="2">
                                <asp:Label ID="lblLKey" runat="server" Width="278px" Height="16px"></asp:Label>
                            </td>
                            <td class="column2">
                            </td>
                        </tr>
                        <tr>
                            <td class="column1">
                            </td>
                            <td class="column2" style="width: 254px">
                            </td>
                        </tr>
                        <tr>
                            <td class="column1">
                                &nbsp;Hash Key :
                            </td>
                            <td class="column2" style="width: 254px">
                                <asp:Label ID="lblHKey" runat="server" Width="337px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="column2" style="height: 18px">
                            </td>
                            <td class="column2" style="height: 18px">
                            </td>
                        </tr>
                        <tr>
                            <td class="column1">
                            </td>
                            <td class="column2">
                                <asp:Button ID="btnSave" OnClick="BtnSave_Click" runat="server" Text="Save" ValidationGroup="1">
                                </asp:Button>
                            </td>
                            <td class="column2">
                            </td>
                            <td class="column2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblmsg" runat="server" Width="297px" Text="Label" Visible="False"></asp:Label>
                            </td>
                            <td class="column2" colspan="1">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    <%--<table cellpadding="0" cellspacing="0" style="width: 571px">
        <tr>
            <td>
                
            </td>
        </tr>
        <tr>
            <td>
                <%-- <asp:UpdatePanel ID="UpdatePanelHelp1" runat="server" UpdateMode="always">
       
        <ContentTemplate>--%>
    <table id="Table2" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="4" class="hdng">
                Customer List
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td class="column2">
            </td>
            <td class="column1">
            </td>
            <td class="column2">
            </td>
        </tr>
        <tr>
            <td class="column2" colspan="4">
                <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Auto" Width="645px">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridBack" DataKeyNames="TempDomainId" EmptyDataText="There is no data."
                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                        PageSize="5">
                        <Columns>
                            <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderText="Edit" ImageUrl="~/images/edit.gif"
                                Text="Button" />
                            <asp:BoundField DataField="TempDomainName" HeaderText="Domain Name" />
                            <asp:BoundField DataField="Allocated" HeaderText="Allocated" />
                            <asp:BoundField DataField="Active" HeaderText="Active" />
                            <asp:BoundField DataField="DatabaseName" HeaderText="Database Name" />
                            <asp:BoundField DataField="CompanyName" HeaderText="Client Name" />
                            <asp:BoundField DataField="CompanyName" HeaderText="Allocated to" />
                            <asp:BoundField DataField="PricePlanAmount" HeaderText="Price/Month" />
                            <asp:BoundField DataField="LicenseKey" HeaderText="LicenseKey" />
                        </Columns>
                        <PagerStyle CssClass="GridPager" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                        <RowStyle CssClass="GridRowStyle" />
                        <FooterStyle CssClass="GridFooter" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="column2" colspan="4">
            </td>
        </tr>
    </table>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <div style="border-right: #946702 1px solid; border-top: #946702 1px solid; left: 45%;
                visibility: visible; vertical-align: middle; border-left: #946702 1px solid;
                width: 196px; border-bottom: #946702 1px solid; position: absolute; top: 65%;
                height: 51px; background-color: #ffe29f" id="IMGDIV" align="center" runat="server"
                valign="middle">
                <table width="645px">
                    <tbody>
                        <tr>
                            <td>
                                <asp:Image ID="Image11" runat="server" ImageUrl="~/images/preloader.gif"></asp:Image>
                            </td>
                            <td>
                                <asp:Label ID="lblprogress" runat="server" ForeColor="#946702" Text="Please Wait"
                                    Font-Bold="True" Font-Size="16px" Font-Names="Arial"></asp:Label><br />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
