<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="ServerDatabaseUpdate.aspx.cs" Inherits="ProductUpdateLastt" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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


            if (evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59) {


                alert("You have entered an invalid character");
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) {
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="margin-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" ></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Create product database new version"></asp:Label>
                    </legend>
                    <div style="float: right;">
                      
                    </div>
                    <div style="clear: both;">
                    </div>
                    
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label10" runat="server" Text="List Of Product Database Version"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td align="right">
                               
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" DataKeyNames="Id"
                                                    EmptyDataText="No Record Found." AllowSorting="True" OnSorting="GridView1_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID" SortExpression="ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="21%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpversion" runat="server" Text='<%#Bind("ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ProductMastercodeID" SortExpression="ProductMastercodeID" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="18%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBackColor" runat="server" Text='<%#Bind("ProductMastercodeID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ServerID" SortExpression="ServerID" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcodeversion" runat="server" Text='<%#Bind("ServerID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="File Name" SortExpression="FileName" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>                                                            
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("Physicalpath") %>'></asp:Label>
                                                                <asp:Label ID="lblfilename" runat="server" Text='<%#Bind("FileName") %>'></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("DownloadStart") %>'></asp:Label>
                                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("DownloadFinish") %>'></asp:Label>
                                                                <asp:Label ID="Label4" runat="server" Text='<%#Bind("DownloadStartTime") %>'></asp:Label>
                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <%--<asp:TemplateField HeaderText="Server IP" SortExpression="ServerIP" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblserverIp" Width="30px" runat="server" Text='<%#Bind("ServerIP") %>'></asp:Label>
                                                            </ItemTemplate>
                                                          
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </td>
                                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>

                        
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
          
            <%-- <asp:PostBackTrigger ControlID="addnewpanel" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
