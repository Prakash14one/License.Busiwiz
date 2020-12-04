<%@ Page Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true"
    CodeFile="Banrelease.aspx.cs" Inherits="Banrelease" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
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

    <asp:UpdatePanel ID="pnlid" runat="server">
    
        <ContentTemplate>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Size="16px"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label10" runat="server" Text="List of Computers /IP Addresses banned "></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" 
                                    Text="Printable Version" onclick="Button3_Click" />
                                <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    style="width: 51px;" type="button" value="Print" visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Filter by company
                                    <asp:DropDownList ID="ddlcompanylist" runat="server">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    Filter by Computer Name
                                    <asp:DropDownList ID="ddlcomputername" runat="server">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    Filter by Public IP Address
                                    <asp:DropDownList ID="ddlpublicipaddress" runat="server">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    Filter by Private IP Address
                                    <asp:DropDownList ID="ddlprivateip" runat="server">
                                    </asp:DropDownList>
                                </label>
                                <div style="clear: both;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="lbldatefrom" runat="server" Text="From "></asp:Label>
                                </label>
                                <label>
                                    <asp:TextBox ID="txtFromDate" runat="server" Width="100px"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDate"
                                        PopupButtonID="ImageButton2">
                                    </cc1:CalendarExtender>
                                </label>
                                <label>
                                    <asp:Label ID="lbldateto" runat="server" Text="To "></asp:Label>
                                </label>
                                <label>
                                    <asp:TextBox ID="txtToDate" runat="server" Width="100px"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:ImageButton ID="imgbtncal2" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                        PopupButtonID="imgbtncal2">
                                    </cc1:CalendarExtender>
                                </label>
                                <label>
                                    Filter by Ban Status
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlbanstatus" runat="server">
                                        <asp:ListItem  Value="2" Text="All"></asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1" Text="Active"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Button ID="Button1" runat="server" Text="Go" OnClick="Button1_Click" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Computers /IP Addresses banned"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" DataKeyNames="Id"
                                                    EmptyDataText="No Record Found." AllowSorting="false" 
                                                    onrowcommand="GridView1_RowCommand" 
                                                    onrowdatabound="GridView1_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Banned Date and Time " SortExpression="DateTime" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldatetime" runat="server" Text='<%#Bind("DateTime") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IP Ban" SortExpression="bannedipaddress" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblipban" runat="server" Visible="false" Text='<%#Bind("bannedipaddress") %>'></asp:Label>
                                                                <asp:Label ID="lblipbandisplay" runat="server"  Text='<%#Bind("bannedipaddressdisplay") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Computer Ban" SortExpression="bannedmacaddress" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbanmac" runat="server" Visible="false" Text='<%#Bind("bannedmacaddress") %>'></asp:Label>
                                                                <asp:Label ID="lblbanmacdisplay" runat="server" Text='<%#Bind("bannedmacaddressdisplay") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Company ID" SortExpression="compid" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcompanyid" runat="server" Text='<%#Bind("compid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Computer Name" SortExpression="computername" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcomputername" runat="server" Text='<%#Bind("computername") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Public IP Address" SortExpression="Ipaddress" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpublicipaddress" runat="server" Text='<%#Bind("Ipaddress") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Private IP Address" SortExpression="computerip" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcomputeripaddress" runat="server" Text='<%#Bind("computerip") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="User Name Last Used" SortExpression="UserNameLastAccess"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblusernamelastused" runat="server" Text='<%#Bind("UserNameLastAccess") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ban Status" SortExpression="BanIsActive" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbanstatus" runat="server" Visible="false" Text=""></asp:Label>
                                                                <asp:Label ID="lblbanstatusdisplay" runat="server"  Text=""></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Release Ban"  HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                               <%-- <asp:ImageButton ID="Btn" runat="server" CommandName="Remove" CommandArgument='<%#Bind("Id") %>' ImageUrl="~/Account/images/delete.gif"
                                                                    OnClientClick="return confirm('Do you wish to remove this ban?');" ToolTip="Remove">
                                                                </asp:ImageButton>--%>
                                                                <asp:Button ID="Button2" runat="server" CommandName="Remove" CommandArgument='<%#Bind("Id") %>'  OnClientClick="return confirm('Do you wish to remove this ban?');" ToolTip="Remove Ban" Text="Release Ban" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
