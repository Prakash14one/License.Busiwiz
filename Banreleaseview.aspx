<%@ Page Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true"
    CodeFile="Banreleaseview.aspx.cs" Inherits="Banrelease" Title="Untitled Page" %>

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
                        <asp:Label ID="Label10" runat="server" Text="View Gobal IP ban History "></asp:Label>
                    </legend>
                    <table width="100%" style="min-height:500px;">
                    <tr>
                    <td valign="top">
                    <table width="100%">
                        <tr>
                            <td align="right">
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <label style="width:60px;">
                            <asp:Label ID="Label8" runat="server" Text="From"></asp:Label>
                            <asp:Label ID="Label18" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                           
                            </label>
                            <label style="width:80px;">
                            <asp:TextBox ID="TextBox1" runat="server" Width="70px"></asp:TextBox>
                            </label>
                            <label style="width:40px;">
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                            </label>                            
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="ImageButton3" TargetControlID="TextBox1">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="TextBox1">
                            </cc1:MaskedEditExtender>
                            <label style="width:40px;">
                            <asp:Label ID="Label18q" runat="server" Text="To"></asp:Label>
                            <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                           
                            </label>
                            <label style="width:80px;">
                            <asp:TextBox ID="TextBox2" runat="server" Width="70px"></asp:TextBox>
                            </label>
                            <label style="width:40px;">
                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                            </label>
                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="ImageButton4" TargetControlID="TextBox2">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="TextBox2">
                            </cc1:MaskedEditExtender>
                            <label style="width:110px;">
                            <asp:Label ID="Label1" runat="server" Text="User Name"></asp:Label>                           
                            </label>
                            <label>
                            <asp:DropDownList ID="ddlusername" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlusername_SelectedIndexChanged">
                            </asp:DropDownList>
                            </label> 
                             <label style="width:80px;">
                            <asp:Button ID="Button1" runat="server" Text="Go" CssClass="btnSubmit" OnClick="Button1_Click" />
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
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text=" Computers /IP Addresses banned history "></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                                <asp:Label ID="Label2" Visible="false"  runat="server" Font-Italic="true" Text=" No data Available "></asp:Label>
                                            </td>
                                        </tr>
                                       
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                               OnSorting="GridView1_Sorting"      PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" DataKeyNames="Id"
                                                   EmptyDataText="No Record Found." AllowSorting="true"  
                                                    onrowcommand="GridView1_RowCommand" 
                                                    onrowdatabound="GridView1_RowDataBound"   >
                                                    <Columns>
                                                   
                                                        <asp:TemplateField HeaderText="Portal Name" SortExpression="PortalName" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPortalName" runat="server" Text='<%#Bind("PortalName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Banned Date" SortExpression="DateTime" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldatetime" runat="server" Text=<%# Eval("DateTime", "{0:dd MMM yyyy}")%>  ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Company ID" SortExpression="compid" HeaderStyle-HorizontalAlign="Left" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcompanyid" runat="server" Text='<%#Bind("companyloinid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                           <asp:TemplateField HeaderText="User Name" SortExpression="UserLoginLogID" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblusernamelastused" runat="server" Text='<%#Bind("UserLoginLogID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Employee Name " SortExpression="EmpName" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpName" runat="server" Text='<%#Bind("EmpName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Computer Name" SortExpression="computername" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcomputername" runat="server" Text='<%#Bind("computername") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IP Address" SortExpression="Ipaddress" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpublicipaddress" runat="server" Text='<%#Bind("Ipaddress") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="IP Ban" SortExpression="bannedipaddress" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblipban" runat="server" Visible="false" Text='<%#Bind("bannedipaddress") %>'></asp:Label>
                                                                <asp:Label ID="lblipbandisplay" runat="server"  Text='<%#Bind("bannedipaddressdisplay") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Comp Ban" SortExpression="bannedmacaddress" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbanmac" runat="server" Visible="false" Text='<%#Bind("bannedmacaddress") %>'></asp:Label>
                                                                <asp:Label ID="lblbanmacdisplay" runat="server" Text='<%#Bind("bannedmacaddressdisplay") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ban Status" SortExpression="BanIsActive" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbanstatus" runat="server" Visible="false" Text=""></asp:Label>
                                                                <asp:Label ID="lblbanstatusdisplay" runat="server"  Text=""></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remove Ban"  HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                               <%-- <asp:ImageButton ID="Btn" runat="server" CommandName="Remove" CommandArgument='<%#Bind("Id") %>' ImageUrl="~/Account/images/delete.gif"
                                                                    OnClientClick="return confirm('Do you wish to remove this ban?');" ToolTip="Remove">
                                                                </asp:ImageButton>--%>
                                                                <asp:Button ID="Button2" runat="server" CommandName="Remove" CommandArgument='<%#Bind("Id") %>'  OnClientClick="return confirm('Do you wish to remove this ban?');" ToolTip="Remove Ban" Text="Remove Ban" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                                  <input id="Id" name="Id" runat="server" type="hidden" style="width: 1px" />
            <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
            <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    </td>
                    </tr>
                    <tr>
                    <td></td>
                    </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
