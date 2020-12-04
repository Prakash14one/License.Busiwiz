<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master" AutoEventWireup="true" 
CodeFile="PayrollSetupPage.aspx.cs" Inherits="ShoppingCart_Admin_PayrollSetupPage" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }
        }

        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }

        function check(txt1, regex, reg, id, max_len) {

            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
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
        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
            }
        }     
    </script>

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
    <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>             
                <div style="clear: both;">
                </div>
            
                <fieldset>
                    <legend>
                        <asp:Label ID="lblheadmessage" runat="server" Text="List of Payroll setup Page"></asp:Label>
                    </legend>
                    <tr>
                        <td>
                            <label>
                                Select Business
                            </label>
                            <label>
                                <asp:DropDownList ID="ddlstproductname" runat="server" AutoPostBack="true" onselectedindexchanged="ddlstproductname_SelectedIndexChanged"  
                                >
                                </asp:DropDownList>
                            </label>                           
                        </td>
                    </tr>
                    <div style="float: right">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            OnClick="Button1_Click" />
                        <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="850Px">
                                             <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label30" runat="server" Font-Italic="true" Text="List of Payroll Page Details"></asp:Label>
                                                </td>
                                            </tr>
                                                                                  
                                             <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="Label10" runat="server" Font-Italic="true" Font-Size="20px" Text=" Business Name : "></asp:Label>
                                                    <asp:Label ID="lblproduct" runat="server" Font-Italic="true" Font-Size="20px"></asp:Label>
                                                  
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="margin-left: 40px">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="ID"
                                        EmptyDataText="No Record Found." AllowPaging="True" Width="100%" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                                        onrowdatabound="GridView1_RowDataBound" 
                                        onrowcommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Country" SortExpression="Country" Visible="false" ItemStyle-Width="8%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcountry" runat="server" Text='<%# Bind("CountryName")%>'></asp:Label>
                                                     <asp:Label ID="lblidm" Visible="false" runat="server" Text='<%# Bind("ID")%>'></asp:Label>
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>    
                                                                                 
                                             <asp:TemplateField HeaderText="Province" HeaderStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatename" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>          
                                                                                                 
                                            <asp:TemplateField HeaderText="Form Links"  ItemStyle-Width="20%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                 <%--   <asp:Label ID="lblpagetitle" runat="server" Text='<%# Bind("pgtit")%>'></asp:Label>--%>
                                               
                                                 <asp:LinkButton ID="lnkbtnpgnm" CommandName="pg" CommandArgument='<%# Eval("PageId") %>'  runat="server">
                                                 <asp:Label ForeColor="Black" ID="lblpagetitle" runat="server" Text='<%# Bind("pgtit")%>'></asp:Label>
                                                 </asp:LinkButton>
                                                
                                                </ItemTemplate>                                               
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Form Description"  ItemStyle-Width="66%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpagehelp" runat="server" Text='<%# Bind("pgdesc")%>'></asp:Label>
                                                </ItemTemplate>                                               
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Downloadable PDF File"  ItemStyle-Width="12%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>     
                                                 <asp:Label ForeColor="Black" ID="lblpagename" runat="server" Text='<%# Bind("pgnm")%>' Visible="false"></asp:Label>                                              
                                                     <asp:LinkButton ID="lnkbtnpgnm1" ForeColor="Black"   Text="Download Form"  runat="server" OnClick="lblpen_Click11">                                                  
                                                 </asp:LinkButton>                                                 
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
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers><asp:PostBackTrigger ControlID="GridView1" /></Triggers>
    </asp:UpdatePanel>
</asp:Content>

