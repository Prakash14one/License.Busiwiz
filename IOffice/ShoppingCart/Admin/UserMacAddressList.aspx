<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="UserMacAddressList.aspx.cs" Inherits="UserMacAddressList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }  

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="statuslable" runat="server" ForeColor="Red" Text="">                            
                    </asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Add New Computer"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="Button5" runat="server" CssClass="btnSubmit" Text="Add New Computer"
                                    OnClick="Button5_Click" />
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Business"></asp:Label>
                                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="User Type"></asp:Label>
                                        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="User Name"></asp:Label>
                                        <asp:DropDownList ID="DropDownList4" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label9" runat="server" Text="Mac Address"></asp:Label>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label10" runat="server" Text="Computer Name"></asp:Label>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </label>
                                </td>
                                <td>
                                    <br />
                                    <asp:Button ID="Button6" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="Button6_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label1" runat="server" Text="List of computers allowed for access by users"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="Button3" runat="server" Text="Printable Version" CssClass="btnSubmit"
                                OnClick="Button3_Click" />
                        </label>
                        <label>
                            <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" visible="false" />
                        </label>
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Filter by Business"></asp:Label>
                                    <asp:DropDownList ID="ddlbusinessname" runat="server" OnSelectedIndexChanged="ddlbusinessname_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Filter by User Type"></asp:Label>
                                    <asp:DropDownList ID="ddlPartyType" runat="server" OnSelectedIndexChanged="ddlPartyType_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Filter by User Name"></asp:Label>
                                    <asp:DropDownList ID="ddlusername" runat="server">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="Filter by Status"></asp:Label>
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem Selected="True" Text="Request Received - Unapproved" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <br />
                                <asp:Button ID="Button1" runat="server" Text="Go" OnClick="Button1_Click" />
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;"
                                                    colspan="12">
                                                    <asp:Label ID="lblcomname" runat="server" Font-Italic="True" Visible="false"> </asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;"
                                                    colspan="12">
                                                    <asp:Label ID="Label16" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;"
                                                    colspan="12">
                                                    <asp:Label ID="Label14" runat="server" Font-Italic="true" ForeColor="Black" Text="List of computers allowed for access by users"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="grdallowedlist" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" AllowPaging="false" AutoGenerateColumns="False"
                                        Width="100%" EmptyDataText="No Record Found." PageSize="50" OnRowDataBound="grdallowedlist_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Business" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbusiness" runat="server" Text='<%# Eval("WName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="User Type" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblusertype" runat="server" Text='<%# Eval("PartType")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="User Name" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblusername" runat="server" Text='<%# Eval("Compname")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="# Allowed MAC Addresses"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" Text="" OnClick="link1_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Date Time of Request"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldatetimerequest" runat="server" Text='<%# Eval("datetimeemailgenerated")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="New Mac Address"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmacaddress" runat="server" Text='<%# Eval("MacAddress")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="New Computer Name"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrealcompname" runat="server" Text='<%# Eval("ComputerName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Status" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Visible="false" Text='<%# Eval("StatusLabel")%>'></asp:Label>
                                                    <asp:Label ID="lblmasterid" Visible="false" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                                    <asp:Label ID="lbluserid" Visible="false" runat="server" Text='<%# Eval("emailgenerateduserid")%>'></asp:Label>
                                                    <asp:DropDownList ID="DropDownList1grd" runat="server">
                                                        <asp:ListItem Text="Request Received - Unapproved" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="Button2" runat="server" Text="Submit" OnClick="Button2_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="Panel2" runat="server" Width="90%" Height="450px" ScrollBars="None"
                                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px">
                                <div>
                                   
                                        <div style="float: right;">
                                            <label>
                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="16px" />
                                            </label>
                                        </div>
                                        <div>
                                            <fieldset>
                                                <legend>List of computers for access </legend>
                                                <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Vertical">
                                                    <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" AllowPaging="false" AutoGenerateColumns="False"
                                                        EmptyDataText="No Record Found." Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Type of User" ItemStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="25%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmasteridpopup" runat="server" Visible="false" Text='<%# Eval("Id")%>'></asp:Label>
                                                                    <asp:Label ID="lblusertype" runat="server" Text='<%# Eval("PartType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="User Name" ItemStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="25%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblusernamepopup" runat="server" Text='<%# Eval("Compname")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Mac Address" ItemStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="25%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmacaddresspopup" runat="server" Text='<%# Eval("MacAddress")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Computer Name"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcompnamepopup" runat="server" Text='<%# Eval("ComputerRealName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </fieldset>
                                        </div>
                                   
                                </div>
                            </asp:Panel>
                            <asp:Button ID="Button10" runat="server" Style="display: none" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1333" runat="server" PopupControlID="Panel2"
                                TargetControlID="Button10" CancelControlID="ImageButton2">
                            </cc1:ModalPopupExtender>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
