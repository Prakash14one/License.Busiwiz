<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="wzWareHouseTimeZoneAddMaster.aspx.cs" Inherits="Add_WareHouse_TimeZone_Master" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
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

    <asp:UpdatePanel ID="pnnn1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 1%">
                <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" />
            </div>
            <div class="products_box">
                <asp:Panel ID="pnladd" Visible="false" runat="server">
                    <fieldset>
                        <legend>
                            <asp:Label ID="lbladd" runat="server"></asp:Label>
                        </legend>
                        <label>
                            <asp:Label ID="Label2" runat="server" Text="Business Name" />
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="250px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Time Zone" />
                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddList">
                            </asp:DropDownList>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <br />
                        <asp:Button ID="ImageButton2" Visible="false" runat="server" CssClass="btnSubmit"
                            Text="Submit" OnClick="ImageButton2_Click" ValidationGroup="1" />
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" OnClick="Button2_Click"
                            Text="Update" ValidationGroup="1" Visible="False" />
                        <asp:Button ID="ImageButton3" runat="server" CssClass="btnSubmit" OnClick="Cancel_Click"
                            Text="Cancel" />
                    </fieldset></asp:Panel>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblListBusinessTimeZone" Text="List of Business Time Zones" runat="server"></asp:Label></legend>
                    <label>
                        <asp:Label ID="lblSelectBusiness" runat="server" Text="Filter by Business Name" />
                        <asp:DropDownList ID="DropDownList3" runat="server" Width="210px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </label>
                    <div style="float: right;">
                        <asp:Button ID="Button" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button_Click" />
                        <input id="Button2" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" class="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td colspan="4">
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="text-align: center; color: Black; font-weight: bold; font-style: italic;">
                                            <tr align="center">
                                                <td colspan="2">
                                                    <asp:Label ID="lblCompany" Font-Size="20px" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="Label1" Font-Size="20px" runat="server" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblCompany0" Font-Size="20px" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="2">
                                                    <asp:Label ID="Label4" runat="server" Font-Size="18px" Text="List of Business Time Zones"></asp:Label>
                                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <cc11:PagingGridView ID="GridView1" runat="server" AutoGenerateColumns="false" GridLines="Both"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        DataKeyNames="ID" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                                        OnRowEditing="GridView1_RowEditing" AllowSorting="True" OnSorting="GridView1_Sorting"
                                        EmptyDataText="No Record Found." Width="100%" 
                                        onpageindexchanging="GridView1_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" DataField="ID"
                                                HeaderText="ID" Visible="false" />
                                            <asp:TemplateField HeaderText="Business ID" ItemStyle-HorizontalAlign="Left" Visible="false"
                                                HeaderStyle-HorizontalAlign="Left" SortExpression="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbbname" runat="server" Text='<%#Bind("WHID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Business Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="Name" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Time Zone" HeaderStyle-HorizontalAlign="Left" SortExpression="tname">
                                                <ItemTemplate>
                                                    <asp:Label ID="tname" runat="server" Text='<%#Bind("tname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Name" HeaderText="Business Name" SortExpression="Name" ItemStyle-Width="40%" HeaderStyle-HorizontalAlign="Left"/>--%>
                                            <%--<asp:BoundField DataField="tname" HeaderText="Time Zone" SortExpression="tname" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%"/>--%>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Edit" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton4" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                        ToolTip="Edit" CommandName="Edit" ImageUrl="~/Account/images/edit.gif" OnClick="LinkButton4_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Left" ButtonType="Image" DeleteImageUrl="~/Account/images/delete.gif" HeaderImageUrl="~/ShoppingCart/images/trash.jpg" />--%>
                                        </Columns>
                                    </cc11:PagingGridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
            <div style="clear: both;">
            </div>
            <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA" BorderStyle="Outset"
                Width="300px">
                <table id="Table2" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label5" runat="server" Text="Note: By changing the time zone of the business, the attendance of all empoyees will reflect this change. For example, changing from Eastern to Pacific time, will change the start and end times of all employees for the business. If the timing for employees is 9am to 5pm Eastern time, this will change to 9am to 5pm Pacific time."></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="Button4" CssClass="btnSubmit" Text="Confirm" runat="server" OnClick="Button1_Click" />
                            &nbsp;<asp:Button ID="Button5" CssClass="btnSubmit" Text="Cancel" runat="server"
                                OnClick="Button5_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel3" TargetControlID="HiddenButton222">
            </cc1:ModalPopupExtender>
            <!--end of right content-->
            <div style="clear: both;">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
